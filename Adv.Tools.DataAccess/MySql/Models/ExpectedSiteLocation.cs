using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedSiteLocation : IExpectedSiteLocation
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string EastWest { get; set; }
        public string NorthSouth { get; set; }
        public string Elevation { get; set; }
        public string Angle { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public string GetMySqlTableMapping(string databaseName, string tableName)
        {
            string sqlQuery =
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{tableName} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Disicpline)}` text, " +
               $"`{nameof(EastWest)}` text, " +
               $"`{nameof(NorthSouth)}` text, " +
               $"`{nameof(Elevation)}` text, " +
               $"`{nameof(Angle)}` text, " +
               $"`{nameof(Latitude)}` text, " +
               $"`{nameof(Longitude)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
