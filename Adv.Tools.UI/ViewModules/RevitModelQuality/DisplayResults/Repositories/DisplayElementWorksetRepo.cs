using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Repositories
{
    public class DisplayElementWorksetRepo : IDisplayElementWorksetRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public DisplayElementWorksetRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void DeleteAllViewData()
        {
            await _dataAccess.DeleteAllData<ReportElementsWorkset>(_databaseName);
        }
        public IEnumerable<ReportElementsWorkset> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ReportElementsWorkset>(_databaseName);
        }
        public IEnumerable<ReportElementsWorkset> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ReportElementsWorkset>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.ObjectCategory) ||
                 regex.IsMatch(result.ObjectName) ||
                 regex.IsMatch(result.ObjectId) ||
                 regex.IsMatch(result.Discipline))
                .ToList();
        }
    }
}
