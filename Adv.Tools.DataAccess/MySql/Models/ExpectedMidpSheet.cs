﻿using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedMidpSheet : IExpectedMidpSheet, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SheetCodeInputText { get; set; }
        public string SheetNameInputText { get; set; }
        public bool IsSheetNameBuiltIn { get; set; }
        public string SheetScaleInputText { get; set; }
        public string SheetNameParamName { get; set; }
        public string SheetNameParamGuid { get; set; }
        public string SheetScaleParamName { get; set; }
        public string SheetScaleParamGuid { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
              $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4; " +
              $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
              $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
              $"`{nameof(ModelName)}` text, " +
              $"`{nameof(ModelGuid)}` text, " +
              $"`{nameof(Discipline)}` text, " +
              $"`{nameof(SheetCodeInputText)}` text, " +
              $"`{nameof(SheetNameInputText)}` text, " +
              $"`{nameof(IsSheetNameBuiltIn)}` TINYINT, " +
              $"`{nameof(SheetScaleInputText)}` text, " +
              $"`{nameof(SheetNameParamName)}` text, " +
              $"`{nameof(SheetNameParamGuid)}` text, " +
              $"`{nameof(SheetScaleParamName)}` text, " +
              $"`{nameof(SheetScaleParamGuid)}` text, " +
              $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
