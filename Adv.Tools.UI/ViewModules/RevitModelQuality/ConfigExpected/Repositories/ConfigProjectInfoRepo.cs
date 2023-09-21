using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigProjectInfoRepo : IConfigProjectInfoRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigProjectInfoRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void Add(ExpectedProjectInfo model)
        {
            var data = new List<ExpectedProjectInfo>() { model };
            await _dataAccess.SaveByInsertValuesAsync(_databaseName, data);
        }
        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataByIdAsync<ExpectedProjectInfo>(_databaseName, id);
        }
        public async void DeletAllViewData()
        {
            await _dataAccess.DeleteAllTableDataAsync<ExpectedProjectInfo>(_databaseName);
        }
        public async void Edit(ExpectedProjectInfo model)
        {
            var data = new List<ExpectedProjectInfo>() { model };
            await _dataAccess.SaveByUpdateValuesAsync(_databaseName, data);
        }
        public IEnumerable<ExpectedProjectInfo> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedProjectInfo>(_databaseName);
        }
        public IEnumerable<ExpectedProjectInfo> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedProjectInfo>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.Parameter) ||
                 regex.IsMatch(result.Discipline))
                .ToList();
        }
        public IEnumerable<ExpectedDocument> GetDocumentsData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);
        }
    }
}
