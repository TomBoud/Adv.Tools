using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportFileReference : IReportFileReference, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LinkName { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public bool IsReffOk { get; set; }
        public string IsReffOkHeb { get; set; }
        public bool IsStatusOk { get; set; }
        public string IsStatusOkHeb { get; set; }

        public void InitializeFromInterface(IReportFileReference source)
        {
            Id = source?.Id ?? 0;
            ModelName = source?.ModelName ?? string.Empty;
            ModelGuid = source?.ModelGuid ?? string.Empty;
            Discipline = source?.Discipline ?? string.Empty;
            LinkName = source?.LinkName ?? string.Empty;
            Status = source?.Status ?? string.Empty;
            Reference = source?.Reference ?? string.Empty;
            IsReffOk = source?.IsReffOk ?? false;
            IsReffOkHeb = source?.IsReffOkHeb ?? string.Empty;
            IsStatusOk = source?.IsStatusOk ?? false;
            IsStatusOkHeb = source?.IsStatusOkHeb ?? string.Empty;
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
               $"`{nameof(LinkName)}` text, " +
               $"`{nameof(Status)}` text, " +
               $"`{nameof(Reference)}` text, " +
               $"`{nameof(IsReffOk)}` TINYINT, " +
               $"`{nameof(IsReffOkHeb)}` text, " +
               $"`{nameof(IsStatusOk)}` TINYINT, " +
               $"`{nameof(IsStatusOkHeb)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
