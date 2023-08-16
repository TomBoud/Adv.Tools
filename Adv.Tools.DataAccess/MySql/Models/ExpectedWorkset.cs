
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedWorkset : IExpectedWorkset
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string WorksetName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }

        public string GetMySqlTableMapping(string databaseName, string tableName)
        {
            string sqlQuery =
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{tableName} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(CategoryId)}` text, " +
               $"`{nameof(CategoryName)}` text, " +
               $"`{nameof(WorksetName)}` text, " +
               $"`{nameof(Disicpline)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
