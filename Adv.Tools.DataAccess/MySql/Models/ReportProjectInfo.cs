using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportProjectInfo : IReportProjectInfo, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string InfoName { get; set; }
        public string InfoValue { get; set; }
        public string ExpectedValue { get; set; }
        public bool IsCorrect { get; set; }
        public string IsCorrectHeb { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(InfoName)}` text, " +
               $"`{nameof(InfoValue)}` text, " +
               $"`{nameof(ExpectedValue)}` text, " +
               $"`{nameof(IsCorrect)}` TINYINT, " +
               $"`{nameof(IsCorrectHeb)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
