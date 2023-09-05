using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigWorksetRepo : IConfigWorksetsRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigWorksetRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public void Add(ExpectedWorkset model)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedWorkset>(_databaseName, id);
        }
        public async void DeleteAllViewData()
        {
            await _dataAccess.DeleteAllData<ExpectedWorkset>(_databaseName);
        }
        public void Edit(ExpectedWorkset model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpectedWorkset> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedWorkset>(_databaseName);
        }

        public IEnumerable<ExpectedWorkset> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedWorkset>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.Discipline))
                .ToList();
        }

        public IEnumerable<ExpectedDocument> GetDocumentsData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);
        }
    }
}
