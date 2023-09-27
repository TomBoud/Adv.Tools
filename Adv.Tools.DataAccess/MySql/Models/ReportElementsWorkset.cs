using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportElementsWorkset : IReportElementsWorkset ,IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCategory { get; set; }
        public string ObjectId { get; set; }
        public string ObjectFamily { get; set; }
        public string ObjectWorkset { get; set; }

        public void InitializeFromInterface(IReportElementsWorkset source)
        {
            Id = source?.Id ?? 0;
            ModelName = source?.ModelName ?? string.Empty;
            ModelGuid = source?.ModelGuid ?? string.Empty;
            Discipline = source?.Discipline ?? string.Empty;
            ObjectName = source?.ObjectName ?? string.Empty;
            ObjectCategory = source?.ObjectCategory ?? string.Empty;
            ObjectId = source?.ObjectId ?? string.Empty;
            ObjectFamily = source?.ObjectFamily ?? string.Empty;
            ObjectWorkset = source?.ObjectWorkset ?? string.Empty;
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
               $"`{nameof(ObjectName)}` text, " +
               $"`{nameof(ObjectCategory)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(ObjectFamily)}` text, " +
               $"`{nameof(ObjectWorkset)}` text, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
