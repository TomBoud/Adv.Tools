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
    public class ReportInternalClash : IReportInternalClash ,IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SourceLevelName { get; set; }
        public string SourceCategory { get; set; }
        public string SourceElementName { get; set; }
        public string SourceElementId { get; set; }
        public string ClashCategory { get; set; }
        public string ClashLevelName { get; set; }
        public string ClashElementName { get; set; }
        public string ClashElementId { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(SourceLevelName)}` text, " +
               $"`{nameof(SourceCategory)}` text, " +
               $"`{nameof(SourceElementName)}` text, " +
               $"`{nameof(SourceElementId)}` text, " +
               $"`{nameof(ClashCategory)}` text, " +
               $"`{nameof(ClashLevelName)}` text, " +
               $"`{nameof(ClashElementName)}` text, " +
               $"`{nameof(ClashElementId)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
