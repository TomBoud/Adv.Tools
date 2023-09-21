using Adv.Tools.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Extensions.Ordering;
using Xunit;
using Adv.Tools.DataAccess.MySql.Models;

namespace Adv.Tools.DataAccess.Tests.xUnit.MySql.Models
{
    public class ReportProjectBasePointTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DevMySqlModels;

        [Fact, Order(1)]
        public async void TestDeleteTable_Successful()
        {
            //Stage
            var task = _access.DeleteTableIfExists(TestDataBaseName, nameof(ReportProjectBasePoint));
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
            var query = new ReportProjectBasePoint().GetCreateTableQuery(TestDataBaseName);
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
            var models = new List<ReportProjectBasePoint>
            {
                new ReportProjectBasePoint()
                {
                    Id =0,
                    ModelName = "testModel",
                    ModelGuid = "testGuid",
                    Discipline = "testDiscipline",
                    ExpectedAngle = "testAngle",
                    ExpectedEastWest = "testEast",
                    ExpectedElevation = "testElevation",
                    ExpectedLatitude = "testLat",
                    ExpectedLongitude = "testLong",
                    ExpectedNorthSouth = "testNorth",
                    IsBasePoint = true,
                    IsBasePointHeb = "בדיקה",
                    IsCorrect = true,
                    IsCorrectHeb ="בדיקה",
                    IsLocation = true,
                    IsLocationHeb ="בדיקה",
                    LinkedAngle ="testAngle",
                    LinkedEastWest = "testEast",
                    LinkedElevation = "testElevation",
                    LinkedLatitude = "testLat",
                    LinkedLongitude = "testLong",
                    LinkedNorthSouth = "testNorth",
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
            var task = _access.LoadDataSelectAllAsync<ReportProjectBasePoint>(TestDataBaseName);
            //Act
            var models = await task;
            //Assert
            Assert.Null(task.Exception);
            Assert.NotNull(models);
            Assert.True(models?.Count() > 0);
        }
    }
}
