using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportMissingWorkset : IReportMissingWorkset
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string WorksetName { get; set; }
        public string ObjectId { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }


        public string GetMySqlTableMapping(string databaseName, string tableName)
        {
            string sqlQuery =
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{tableName} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Disicpline)}` text, " +
               $"`{nameof(WorksetName)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(IsFoundHeb)}` text, " +
               $"`{nameof(IsFound)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }

}
