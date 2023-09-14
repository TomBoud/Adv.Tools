using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.DataAccess.Autodesk.AppStore;
using System.Net;
using Xunit;
using Xunit.Extensions.Ordering;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql.Models
{
    public class ExpectedModelTest
    {
        
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DatabaseName;

        [Fact, Order(1)]
        public async void TestDeleteTable_Successful()
        {
            //Stage
            var task = _access.DeleteTableIfExists(TestDataBaseName,nameof(ExpectedModel));
            //Act
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

        [Fact, Order(2)]
        public async void TestCreateTable_Successful()
        {
            //Stage
            var query = new ExpectedModel().GetCreateTableQuery(TestDataBaseName);
            var task = _access.ExecuteSqlQueryAsync(query);
            //Act
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

        [Fact, Order(3)]
        public async void TestDataBaseInsert_Successful()
        {
            //Stage
            var models = new List<ExpectedModel>
            {
                new ExpectedModel()
                {
                    ModelName = "testModel",
                    Discipline = "testDiscipline",
                    FolderId = "testFolderId",
                    HubId = "testHubId",
                    ModelGuid = "testGuid",
                    PositionSource = "testPositoion",
                    PositionSourceGuid = "testGrid",
                    ProjectId = "testId",
                }
            };
            //Act
            var task = _access.SaveByInsertValuesAsync(TestDataBaseName, models);
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

        [Fact, Order(4)]
        public async void TestDataBaseLoad_Successful()
        {
            //Stage
            var task = _access.LoadDataSelectAllAsync<ExpectedModel>(TestDataBaseName);
            //Act
            var models = await task;
            //Assert
            Assert.Null(task.Exception);
            Assert.NotNull(models); 
            Assert.True(models?.Count() > 0);
        }

    }
}
