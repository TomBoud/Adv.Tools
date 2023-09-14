﻿using Adv.Tools.DataAccess.MySql;
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
    public class ReportMidpSheetTest
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.DataAccess.Default.DevDb);
        private readonly string TestDataBaseName = Properties.DataAccess.Default.DatabaseName;

        [Fact, Order(1)]
        public async void TestDeleteTable_Successful()
        {
            //Stage
            var task = _access.DeleteTableIfExists(TestDataBaseName, nameof(ReportMidpSheet));
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
            var query = new ReportMidpSheet().GetCreateTableQuery(TestDataBaseName);
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
            var models = new List<ReportMidpSheet>
            {
                new ReportMidpSheet()
                {
                    Id =0,
                    ModelName = "testModel",
                    ModelGuid = "testGuid",
                    Discipline = "testDiscipline",
                    DateHeb = "תקין",
                    HebNameHeb = "בדיקה",
                    IsDateOk = true,
                    IsHebNameOk = true,
                    IsScaleOk = true,
                    IsSequenceOk = true,
                    IsSheetCodeOk = true,
                    IsSheetTidp  = true,
                    ProjBrowSheetName = "testName",
                    ProjBrowSheetNumber = "testNumber",
                    ScaleHeb = "testScale",
                    SequenceHeb = "testSeq",
                    SheetCode = "testSheet",
                    SheetCodeHeb = "בדיקה",
                    SheetHebName = "בדיקה",
                    SheetRevDate = "testDate",
                    SheetRevSequence = "testRev",
                    SheetScale ="testScale",
                    SheetTidpHeb = "בדיקה",
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
            var task = _access.LoadDataSelectAllAsync<ReportMidpSheet>(TestDataBaseName);
            //Act
            var models = await task;
            //Assert
            Assert.Null(task.Exception);
            Assert.NotNull(models);
            Assert.True(models?.Count() > 0);
        }
    }
}
