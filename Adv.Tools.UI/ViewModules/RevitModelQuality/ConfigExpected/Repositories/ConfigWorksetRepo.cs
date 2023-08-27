using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigWorksetRepo : IConfigWorksetRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigWorksetRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public void Add(ConfigWorksetModel configWorkset)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ConfigWorksetModel configWorkset)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfigWorksetModel> GetAllWorksets()
        {
            var results = _dataAccess.LoadDataSelectAll<ExpectedWorkset>(_databaseName);

            foreach (var item in results.OrderByDescending(x => x.Id).ToList())
            {
                yield return new ConfigWorksetModel(item);
            }
        }

        public IEnumerable<ConfigWorksetModel> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
