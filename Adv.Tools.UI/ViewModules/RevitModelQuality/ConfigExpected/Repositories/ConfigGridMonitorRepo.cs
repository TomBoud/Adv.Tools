using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigGridMonitorRepo : IConfigGridMonitorRepo
    {

        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigGridMonitorRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void Add(ExpectedGridMonitor model)
        {
            var data = new List<ExpectedGridMonitor>() { model };
            await _dataAccess.SaveByInsertValuesAsync(_databaseName, data);
        }
        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedGridMonitor>(_databaseName, id);
        }
        public async void Edit(ExpectedGridMonitor model)
        {
            var data = new List<ExpectedGridMonitor>() { model };
            await _dataAccess.SaveByUpdateValuesAsync(_databaseName, data);
        }
        public IEnumerable<ExpectedGridMonitor> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedGridMonitor>(_databaseName);
        }
        public IEnumerable<ExpectedGridMonitor> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedGridMonitor>(_databaseName);

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
