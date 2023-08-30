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
    public class ConfigWorksetRepo : IConfigWorksetsRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigWorksetRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public void Add(ExpectedWorkset configWorkset)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ExpectedWorkset configWorkset)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpectedWorkset> GetAllWorksets()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedWorkset>(_databaseName);
        }

        public IEnumerable<ExpectedWorkset> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
