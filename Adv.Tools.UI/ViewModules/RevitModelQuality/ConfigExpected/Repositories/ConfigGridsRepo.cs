using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories
{
    public class ConfigGridsRepo : IConfigGridsMonitorRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigGridsRepo(IDbDataAccess dataAccess, string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
        }

        public void Add(ExpectedGridsMonitor configWorkset)
        {
            throw new NotImplementedException();
        }

        public async void Delete(int id)
        {
            await _dataAccess.DeleteDataById<ExpectedGridsMonitor>(_databaseName, id);
        }

        public void Edit(ExpectedGridsMonitor configWorkset)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpectedGridsMonitor> GetAllViewData()
        {
            return _dataAccess.LoadDataSelectAll<ExpectedGridsMonitor>(_databaseName);
        }

        public IEnumerable<ExpectedGridsMonitor> GetByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
