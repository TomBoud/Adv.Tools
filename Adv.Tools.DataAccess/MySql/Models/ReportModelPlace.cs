using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportModelPlace : IReportModelPlace, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(LevelName)}` text, " +
               $"`{nameof(ObjectName)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(ObjectType)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
