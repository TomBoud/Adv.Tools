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
    [CollectionDefinition("2"), Order(2)]
    public class MySqlDataAccess_InsertTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string _dbName = Properties.DataAccess.Default.DevMySqlAccess;

        [Fact, Order(1)]
        public async void TestSaveByInsertValuesAsync_Successful()
        {
            //Stage
            var data = new List<ReportCheckScore>();
            for (int i = 0; i < 5; i++)
            {
                data.Add(
                    new ReportCheckScore()
                    {
                        ModelName = "ThisStringBeOverwitted",
                        ModelGuid = "testModelGUID",
                        Discipline = "ThisStringBeOverwitted",
                        CheckLod = "ThisStringBeOverwitted",
                        CheckScore = "ThisStringBeOverwitted",
                        CheckName = $"ReportNumber{i}",
                        IsActive = true,
                    });
            }

            //Act
            var executeTask = _access.SaveByInsertValuesAsync(_dbName, data);
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }

        [Fact, Order(2)]
        public async void TestSaveByInsertUpdateOnDuplicateKeysAsync_Successful()
        {
            //Stage
            var data = new List<ReportCheckScore>();
            for (int i = 0; i < 5; i++)
            {
                data.Add(
                    new ReportCheckScore()
                    {
                        ModelName = "ThisIsAnUpdatedString",
                        ModelGuid = "testModelGUID",
                        Discipline = "ThisIsAnUpdatedString",
                        CheckLod = "ThisIsAnUpdatedString",
                        CheckScore = "ThisShouldBeUpdatedLater",
                        CheckName = $"ReportNumber{i}",
                        IsActive = true,
                    });
            }
            for (int i = 0; i < 5; i++)
            {
                data.Add(
                    new ReportCheckScore()
                    {
                        ModelName = "NewInsertedData",
                        ModelGuid = "NewInsertedData",
                        Discipline = "NewInsertedData",
                        CheckLod = "NewInsertedData",
                        CheckScore = "NewInsertedData",
                        CheckName = $"ReportNumber{i + 5}",
                        IsActive = true,
                    });
            }

            //Act
            var executeTask = _access.SaveByInsertUpdateOnDuplicateKeysAsync(_dbName, data);
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }
    }
}
