using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigLevelMonitorRepo : IConfigLevelMonitorRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigLevelMonitorRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void Add(ExpectedLevelMonitor model)
        {
            var data = new List<ExpectedLevelMonitor>() { model };
            await _dataAccess.SaveByInsertValuesAsync(_databaseName, data);
        }
        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedLevelMonitor>(_databaseName, id);
        }
        public async void Edit(ExpectedLevelMonitor model)
        {
            var data = new List<ExpectedLevelMonitor>() { model };
            await _dataAccess.SaveByUpdateValuesAsync(_databaseName, data);
        }
        public IEnumerable<ExpectedLevelMonitor> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedLevelMonitor>(_databaseName);
        }
        public IEnumerable<ExpectedLevelMonitor> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedLevelMonitor>(_databaseName);

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
