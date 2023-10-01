
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using System;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportCheckScore : IReportCheckScore, IDbModelEntity
    {

        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get => modelGuid; set => modelGuid = value; }
        public string Discipline { get; set; }
        public string CheckName { get => checkName; set=> checkName = value; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
        public long HashCode { get => GetUniqueDbEntityHashCode(); }
        public bool IsActive { get; set; }

        private string modelGuid;
        private string checkName;

        public long GetUniqueDbEntityHashCode()
        {
            // Concatenate the three input strings
            string combinedInput = modelGuid + checkName;

            // Calculate a basic hash code from the combined string
            int hashCode = combinedInput.GetHashCode();

            // Convert the hash code to a positive number
            long positiveHashCode = Math.Abs((long)hashCode);

            return positiveHashCode;
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
               $"`{nameof(HashCode)}` BIGINT UNSIGNED, " +
               $"`{nameof(CheckLod)}` text, " +
               $"`{nameof(CheckScore)}` text, " +
               $"`{nameof(IsActive)}` TINYINT, " +
               $"UNIQUE KEY `{nameof(HashCode)}_UNIQUE` (`{nameof(HashCode)}`), " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
