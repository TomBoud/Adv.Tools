
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportCheckScore : IReportCheckScore, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string CheckName { get; set; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
        

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(CheckName)}` text, " +
               $"`{nameof(CheckLod)}` text, " +
               $"`{nameof(CheckScore)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
