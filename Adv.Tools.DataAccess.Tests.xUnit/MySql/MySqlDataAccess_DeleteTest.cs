
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.DataAccess.MySql.Models;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql
{
    [CollectionDefinition("4"),Order(4)]
    public class MySqlDataAccess_DeleteTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string _dbName = Properties.DataAccess.Default.DevMySqlAccess;


        [Fact, Order(2)]
        public async void TestDeleteDataByIdAsyncAsync_Successful()
        {
            //Stage
            var executeTask = _access.DeleteDataByIdAsync<ReportCheckScore>(_dbName, 6);
            
            //Act
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }

        [Fact, Order(2)]
        public async void TestDeleteDataWhereParametersAsync_Successful()
        {
            //Stage
            var data = new { ModelGuid = "NewInsertedDataOk0" };

            //Act
            var executeTask = _access.DeleteDataWhereParametersAsync<ReportCheckScore,dynamic>(_dbName, data);
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }

        [Fact, Order(3)]
        public async void TestDeleteAllTableDataAsync_Successful()
        {
            //Stage
            var executeTask = _access.DeleteAllTableDataAsync<ReportCheckScore>(_dbName);

            //Act
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }
    }
}
