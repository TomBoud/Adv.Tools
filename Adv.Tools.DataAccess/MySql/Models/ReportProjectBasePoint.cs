using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportProjectBasePoint : IReportProjectBasePoint, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LinkedEastWest { get; set; }
        public string LinkedNorthSouth { get; set; }
        public string LinkedElevation { get; set; }
        public string LinkedLatitude { get; set; }
        public string LinkedLongitude { get; set; }
        public string LinkedAngle { get; set; }
        public string ExpectedEastWest { get; set; }
        public string ExpectedNorthSouth { get; set; }
        public string ExpectedElevation { get; set; }
        public string ExpectedLatitude { get; set; }
        public string ExpectedLongitude { get; set; }
        public string ExpectedAngle { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsLocation { get; set; }
        public bool IsBasePoint { get; set; }
        public string IsCorrectHeb { get; set; }
        public string IsLocationHeb { get; set; }
        public string IsBasePointHeb { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(LinkedEastWest)}` text, " +
               $"`{nameof(LinkedNorthSouth)}` text, " +
               $"`{nameof(LinkedElevation)}` text, " +
               $"`{nameof(LinkedLatitude)}` text, " +
               $"`{nameof(LinkedLongitude)}` text, " +
               $"`{nameof(LinkedAngle)}` text, " +
               $"`{nameof(ExpectedEastWest)}` text, " +
               $"`{nameof(ExpectedNorthSouth)}` text, " +
               $"`{nameof(ExpectedElevation)}` text, " +
               $"`{nameof(ExpectedLatitude)}` text, " +
               $"`{nameof(ExpectedLongitude)}` text, " +
               $"`{nameof(ExpectedAngle)}` text, " +
               $"`{nameof(IsCorrect)}` TINYINT, " +
               $"`{nameof(IsLocation)}` TINYINT, " +
               $"`{nameof(IsBasePoint)}` TINYINT, " +
               $"`{nameof(IsCorrectHeb)}` text, " +
               $"`{nameof(IsLocationHeb)}` text, " +
               $"`{nameof(IsBasePointHeb)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
