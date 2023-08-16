using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.DataAccess.MySql.Procedures;
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

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql
{
    public class ExpectedModelDataTest
    {
        
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DatabaseName;

        [Fact, Order(1)]
        public async void TestDeleteTableIfExists_Successful()
        {
            //Stage
            var expectedModel = new ExpectedModelData(_access, TestDataBaseName, nameof(ExpectedModel));
            //Act
            var task = expectedModel.DeleteTableIfExist();
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
        }

        [Fact, Order(2)]
        public async void TestCreateTable_Successful()
        {
            //Stage
            var expectedModel = new ExpectedModelData(_access, TestDataBaseName, nameof(ExpectedModel));
            //Act
            var task = expectedModel.CreateTableIfNotExists();
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
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
                    Disicpline = "testDiscipline",
                    FolderId = "testFolderId",
                    HubId = "testHubId",
                    ModelGuid = "testGuid",
                    PositionSource = "testPositoion",
                    PositionSourceGuid = "testGrid",
                    ProjectId = "testId",
                }
            };
            //Act
            var expectedModel = new ExpectedModelData(_access, TestDataBaseName, nameof(ExpectedModel));
            var task = expectedModel.InsertAsync(models);
            await task;
            //Assert
            Assert.True(task.Status.Equals(TaskStatus.RanToCompletion));
        }

        [Fact, Order(4)]
        public async void TestDataBaseLoad_Successful()
        {
            //Stage
            var expectedModel = new ExpectedModelData(_access, TestDataBaseName, nameof(ExpectedModel));
            //Act
            var models = await expectedModel.SelectAllAsync();
            //Assert
            Assert.NotNull(models); 
            Assert.True(models?.Count() > 0);
        }

    }
}
