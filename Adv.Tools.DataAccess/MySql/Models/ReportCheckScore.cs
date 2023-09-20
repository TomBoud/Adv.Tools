
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using System;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportCheckScore : IReportCheckScore, IDbModelEntity
    {
        public int Id { get; set; }
        public string RowGuid { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string CheckName { get; set; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
        public bool IsActive { get; set; }

        public string GetUniqueDbEntityGuid()
        {
            // Concatenate the three input strings
            string combinedInput = ModelName + ModelGuid + CheckName;

            // Convert the concatenated string to bytes
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(combinedInput);

            // Generate a GUID from the bytes
            Guid generatedGuid = new Guid(inputBytes);

            return generatedGuid.ToString();
        }
        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(CheckName)}` text, " +
               $"`{nameof(RowGuid)}` varchar(45) DEFAULT NULL, " +
               $"`{nameof(CheckLod)}` text, " +
               $"`{nameof(CheckScore)}` text, " +
               $"`{nameof(IsActive)}` TINYINT, " +
               $"UNIQUE KEY `{nameof(RowGuid)}_UNIQUE` (`{nameof(RowGuid)}`), " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
