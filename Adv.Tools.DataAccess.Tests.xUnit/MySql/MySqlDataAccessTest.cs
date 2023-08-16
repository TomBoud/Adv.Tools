using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;
using Xunit;
using Adv.Tools.DataAccess.MySql;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql
{
    [Order(1)]
    public class MySqlDataAccessTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);

        [Fact,Order(1)]
        public async void TestDatabaseConnection_Successful()
        {
            //Stage
            var task = _access.ExecuteSqlQueryAsync("SELECT COUNT(*) FROM information_schema.tables");

            //Act
            var result = await task;

            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Equal(-1, result);
        }


    }
}
