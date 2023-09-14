using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportModelSheet : IReportModelSheet, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ProjBrowSheetName { get; set; }
        public string ProjBrowSheetNumber { get; set; }
        public string SheetTidpCodeValue { get; set; }
        public bool IsDefaultName { get; set; }
        public bool IsDefaultNumber { get; set; }
        public bool IsDefaultDrawnBy { get; set; }
        public bool IsDefaultCheckedBy { get; set; }
        public bool IsDefaultApprovedBy { get; set; }
        public bool IsDefaultDesignedBy { get; set; }
        public bool HasRevisionDate { get; set; }
        public bool HasRevisionNumber { get; set; }
        public bool HasRevisionDescription { get; set; }
        public bool HasTitleBlock { get; set; }
        public bool HasSharedParamValues { get; set; }
        public bool HasScale { get; set; }
        public bool HasHebName { get; set; }

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
               $"`{nameof(SheetTidpCodeValue)}` text, " +
               $"`{nameof(IsDefaultName)}` TINYINT, " +
               $"`{nameof(IsDefaultNumber)}` TINYINT, " +
               $"`{nameof(IsDefaultDrawnBy)}` TINYINT, " +
               $"`{nameof(IsDefaultCheckedBy)}` TINYINT, " +
               $"`{nameof(IsDefaultApprovedBy)}` TINYINT, " +
               $"`{nameof(IsDefaultDesignedBy)}` TINYINT, " +
               $"`{nameof(HasRevisionDate)}` TINYINT, " +
               $"`{nameof(HasRevisionNumber)}` TINYINT, " +
               $"`{nameof(HasRevisionDescription)}` TINYINT, " +
               $"`{nameof(HasTitleBlock)}` TINYINT, " +
               $"`{nameof(HasSharedParamValues)}` TINYINT, " +
               $"`{nameof(HasScale)}` TINYINT, " +
               $"`{nameof(HasHebName)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
