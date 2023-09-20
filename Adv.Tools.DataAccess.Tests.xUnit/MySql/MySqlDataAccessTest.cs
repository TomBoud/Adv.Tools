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
    [Order(1)]
    public class MySqlDataAccessTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);
        private readonly string _dbName = "MySqlDataAccessTest";

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
            Func<Task> function = async () => await _access.ExecuteSqlQueryAsync("DROP DATABASE IF EXISTS _dbName");

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

        [Fact, Order(4)]
        public async void TestDeleteAllTableDataAsync_Successful()
        {
            //Stage
            var executeTask = _access.DeleteAllTableDataAsync<ReportElementsWorkset>(_dbName);

            //Act
            await executeTask;

            //Assert
            Assert.True(executeTask.IsCompleted);
            Assert.False(executeTask.IsFaulted);
            Assert.Null(executeTask.Exception);
        }

        [Fact, Order(5)]
        public async void TestSaveByInsertValuesAsync_Successful()
        {
            //Stage
            var data = new List<ReportCheckScore>();
            for(int i =0; i<5; i++)
            {
                data.Add(
                    new ReportCheckScore()
                    {
                        ModelName = "ThisStringBeOverwitted",
                        ModelGuid = "ThisStringBeOverwitted",
                        Discipline = "ThisStringBeOverwitted",
                        CheckLod = "ThisStringBeOverwitted",
                        CheckScore = "ThisStringBeOverwitted",
                        CheckName = $"ReportNumber{i}",
                        RowGuid = $"327D5F04-A5C6-4216-AA4C-FF0FD8E653E{i}",
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

        [Fact, Order(6)]
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
                        ModelGuid = "ThisIsAnUpdatedString",
                        Discipline = "ThisIsAnUpdatedString",
                        CheckLod = "ThisIsAnUpdatedString",
                        CheckScore = "ThisShouldBeUpdatedLater",
                        CheckName = $"ReportNumber{i}",
                        RowGuid = $"327D5F04-A5C6-4216-AA4C-FF0FD8E653E{i}",
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
                        CheckName = $"ReportNumber{i+6}",
                        RowGuid = $"327D5F04-A5C6-4216-AA4C-FF0FD8E653E{i+6}",
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
