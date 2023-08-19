using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;
using Xunit;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql.Models
{
    public class ReportCheckScoreTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DatabaseName;

        [Fact, Order(1)]
        public async void TestDataBaseLoad_Successful()
        {
            //Stage
            var task = _access.LoadDataSelectAllAsync<ReportCheckScore>(TestDataBaseName);
            //Act
            var models = await task;
            //Assert
            Assert.NotNull(models);
            Assert.True(models?.Count() > 0);
        }
    }
}
