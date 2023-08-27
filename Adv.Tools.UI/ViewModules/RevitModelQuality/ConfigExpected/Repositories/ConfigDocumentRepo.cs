using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public void Add(ConfigDocumentModel model)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ConfigDocumentModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfigDocumentModel> GetAllDocuments()
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedDocument>(_databaseName);

            foreach (var item in results.OrderByDescending(x => x.Id).ToList())
            {
                yield return new ConfigDocumentModel(item);
            }
        }

        public IEnumerable<ConfigDocumentModel> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
