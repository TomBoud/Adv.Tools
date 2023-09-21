using Adv.Tools.DataAccess.MySql;
using Adv.Tools.DataAccess.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql
{
    [CollectionDefinition("3"), Order(3)]
    public class MySqlDataAccess_LoadTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string _dbName = Properties.DataAccess.Default.DevMySqlAccess;

        [Fact, Order(1)]
        public async void TestLoadDataSelectAllAsync_Successful()
        {
            //Stage
            var executeTask = _access.LoadDataSelectAllAsync<ReportCheckScore>(_dbName);
            
            //Act
            var list = await executeTask;

            //Assert
            Assert.NotEmpty(list);
            Assert.NotNull(list);
            Assert.True(list.Count > 0);
        }
    }
}
