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
    public class ConfigCleanRepo : IConfigCleanRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigCleanRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void Add(ExpectedCleanView model)
        {
            var data = new List<ExpectedCleanView>() { model };
            await _dataAccess.SaveByInsertValuesAsync(_databaseName, data);
        }

        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedCleanView>(_databaseName, id);
        }

        public async void DeletAllViewData()
        {
            await _dataAccess.DeleteAllData<ExpectedCleanView>(_databaseName);
        }

        public async void Edit(ExpectedCleanView model)
        {
            var data = new List<ExpectedCleanView>() { model };
            await _dataAccess.SaveByUpdateValuesAsync(_databaseName, data);
        }

        public IEnumerable<ExpectedCleanView> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedCleanView>(_databaseName);
        }

        public IEnumerable<ExpectedCleanView> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedCleanView>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.ViewName) ||
                 regex.IsMatch(result.ViewType) ||
                 regex.IsMatch(result.Discipline))
                .ToList();
        }

        public IEnumerable<ExpectedDocument> GetDocumentsData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);
        }
    }
}
