using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
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
    public class ConfigDocumentRepo : IConfigDocumentsRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigDocumentRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public async void Add(ExpectedDocument model)
        {
            var data = new List<ExpectedDocument>() {  model };
            await _dataAccess.SaveByInsertValuesAsync(_databaseName, data);
        }

        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedDocument>(_databaseName, id);
        }

        public async void Edit(ExpectedDocument model)
        {
            var data = new List<ExpectedDocument>(){ model };    
            await _dataAccess.SaveByUpdateValuesAsync(_databaseName, data);
        }

        public IEnumerable<ExpectedDocument> GetAllDocuments()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);
        }

        public IEnumerable<ExpectedDocument> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            return results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.Discipline))
                .ToList();
        }
    }
}
