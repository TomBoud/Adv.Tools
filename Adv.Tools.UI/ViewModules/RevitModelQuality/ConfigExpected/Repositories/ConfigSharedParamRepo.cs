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
    public class ConfigSharedParamRepo : IConfigSharedParamRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigSharedParamRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public void Add(ExpectedSharedPara model)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedSharedPara>(_databaseName, id);
        }
        public async void DeletAllViewData()
        {
            await _dataAccess.DeleteAllData<ExpectedSharedPara>(_databaseName);
        }
        public void Edit(ExpectedSharedPara model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpectedSharedPara> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedSharedPara>(_databaseName);
        }

        public IEnumerable<ExpectedSharedPara> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedSharedPara>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.Discipline) ||
                 regex.IsMatch(result.Parameter))
                .ToList();
        }

        public IEnumerable<ExpectedDocument> GetDocumentsData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);
        }
    }
}
