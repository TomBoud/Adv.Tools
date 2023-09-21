using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;
using Xunit;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.DataAccess.MySql.Models;


namespace Adv.Tools.DataAccess.Tests.xUnit.MySql
{
    [CollectionDefinition("1"), Order(1)]
    public class MySqlDataAccess_ExecuteTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string _dbName = Properties.DataAccess.Default.DevMySqlAccess;

        [Fact, Order(1)]
        public async void TestExecuteSqlQueryAsync_Successful()
        {
            //Stage
            var task = _access.ExecuteSqlQueryAsync("SELECT COUNT(*) FROM information_schema.tables");

            //Act
            var result = await task;

            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
            Assert.Equal(-1, result);
        }

        [Fact, Order(2)]
        public async void TestExecuteWithTransaction_Successful()
        {
            //Stage
            Func<Task> function = async () => await _access.ExecuteSqlQueryAsync($"DROP DATABASE IF EXISTS {_dbName}");

            //Act
            var executeTask = _access.ExecuteWithTransaction(function);
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }

        [Fact, Order(3)]
        public async void TestExecuteBuildMySqlDataBase_Successful()
        {
            //Stage
            var task = _access.ExecuteBuildMySqlDataBase(_dbName);

            //Act
            await task;

            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

    }
}
