using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportMissingWorkset : IReportMissingWorkset ,IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string WorksetName { get; set; }
        public string ObjectId { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(WorksetName)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(IsFoundHeb)}` text, " +
               $"`{nameof(IsFound)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }

}
