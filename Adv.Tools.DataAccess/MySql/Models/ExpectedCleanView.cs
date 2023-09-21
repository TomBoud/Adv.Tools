using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedCleanView : IExpectedCleanView, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ViewType { get; set; }
        public string ViewName { get; set; }


        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4; " +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(ViewType)}` text, " +
               $"`{nameof(ViewName)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
