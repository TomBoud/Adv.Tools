using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportModelGroup : IReportModelGroup, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string GroupedObjects { get; set; }
        public string GroupedGroups { get; set; }
        public bool IsNameCompliance { get; set; }
        public bool IsUniLevel { get; set; }
        public bool IsNestedGroups { get; set; }

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
               $"`{nameof(GroupedObjects)}` text, " +
               $"`{nameof(GroupedGroups)}` text, " +
               $"`{nameof(IsNameCompliance)}` TINYINT, " +
               $"`{nameof(IsUniLevel)}` TINYINT, " +
               $"`{nameof(IsNestedGroups)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
