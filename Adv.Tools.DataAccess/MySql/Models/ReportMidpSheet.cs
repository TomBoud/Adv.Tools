using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportMidpSheet : IReportMidpSheet, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ProjBrowSheetName { get; set; }
        public string ProjBrowSheetNumber { get; set; }
        public string SheetRevDate { get; set; }
        public bool IsDateOk { get; set; }
        public string DateHeb { get; set; }
        public string SheetRevSequence { get; set; }
        public bool IsSequenceOk { get; set; }
        public string SequenceHeb { get; set; }
        public string SheetScale { get; set; }
        public bool IsScaleOk { get; set; }
        public string ScaleHeb { get; set; }
        public string SheetHebName { get; set; }
        public bool IsHebNameOk { get; set; }
        public string HebNameHeb { get; set; }
        public string SheetCode { get; set; }
        public bool IsSheetCodeOk { get; set; }
        public string SheetCodeHeb { get; set; }
        public bool IsSheetTidp { get; set; }
        public string SheetTidpHeb { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(ProjBrowSheetName)}` text, " +
               $"`{nameof(ProjBrowSheetNumber)}` text, " +
               $"`{nameof(SheetRevDate)}` text, " +
               $"`{nameof(IsDateOk)}` TINYINT, " +
               $"`{nameof(DateHeb)}` text, " +
               $"`{nameof(SheetRevSequence)}` text, " +
               $"`{nameof(IsSequenceOk)}` TINYINT, " +
               $"`{nameof(SequenceHeb)}` text, " +
               $"`{nameof(SheetScale)}` text, " +
               $"`{nameof(IsScaleOk)}` TINYINT, " +
               $"`{nameof(ScaleHeb)}` text, " +
               $"`{nameof(SheetHebName)}` text, " +
               $"`{nameof(IsHebNameOk)}` TINYINT, " +
               $"`{nameof(HebNameHeb)}` text, " +
               $"`{nameof(SheetCode)}` text, " +
               $"`{nameof(SheetCodeHeb)}` text, " +
               $"`{nameof(IsSheetCodeOk)}` TINYINT, " +
               $"`{nameof(IsSheetTidp)}` TINYINT, " +
               $"`{nameof(SheetTidpHeb)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
