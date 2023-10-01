
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedSharedPara : IExpectedSharedPara , IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string Parameter { get; set; }
        public string GUID { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
              $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4; " +
              $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
              $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
              $"`{nameof(ModelName)}` text, " +
              $"`{nameof(ModelGuid)}` text, " +
              $"`{nameof(Discipline)}` text, " +
              $"`{nameof(Parameter)}` text, " +
              $"`{nameof(GUID)}` text, " +
              $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
