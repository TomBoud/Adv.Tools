﻿using Adv.Tools.DataAccess.MySql.Models;
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
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevMySqlString);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DevMySqlModels;

        [Fact, Order(1)]
        public async void TestDeleteTable_Successful()
        {
            //Stage
            var task = _access.DeleteTableIfExists(TestDataBaseName, nameof(ReportCheckScore));
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
            var query = new ReportCheckScore().GetCreateTableQuery(TestDataBaseName);
            var task = _access.ExecuteSqlQueryAsync(query);
            //Act
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

        [Fact, Order(3)]
        public void TestUniqueDbEntityHashCode_Successful()
        {
            //Stage
            var model = new ReportCheckScore()
            {
                ModelGuid = "testModelGuid",
                CheckName = "testCheckName",
            };

            //Act
            var hashCode = model.GetUniqueDbEntityHashCode();

            //Assert
            Assert.True(hashCode.Equals(1429552731));

        }

        [Fact, Order(4)]
        public async void TestDataBaseInsert_Successful()
        {
            //Stage
            
            var model = new ReportCheckScore()
            {
                Id = 0,
                ModelName = "testModel",
                ModelGuid = "testGuid",
                Discipline = "testDiscipline",
                CheckLod = "100",
                CheckName = "Name",
                CheckScore = "95.5",
                IsActive = true,
            };

            //Act
            var models = new List<ReportCheckScore>() { model };
            var task = _access.SaveByInsertValuesAsync(TestDataBaseName, models);
            await task;
            //Assert
            Assert.True(task.IsCompleted);
            Assert.False(task.IsFaulted);
            Assert.Null(task.Exception);
        }

        [Fact, Order(5)]
        public async void TestDataBaseLoad_Successful()
        {
            //Stage
            var task = _access.LoadDataSelectAllAsync<ReportCheckScore>(TestDataBaseName);
            //Act
            var models = await task;
            //Assert
            Assert.Null(task.Exception);
            Assert.NotNull(models);
            Assert.True(models?.Count() > 0);
        }
    }
}
