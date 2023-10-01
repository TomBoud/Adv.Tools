using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportGenericModel : IReportGenericModel, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ObjectName { get; set; }
        public string ObjectFamily { get; set; }
        public string ObjectLevel { get; set; }
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
               $"`{nameof(ObjectName)}` text, " +
               $"`{nameof(ObjectFamily)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(ObjectLevel)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
