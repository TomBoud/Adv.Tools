using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportCleanView : IReportCleanView, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ViewName { get; set; }
        public string ObjectId { get; set; }
        public string ViewType { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }
        public bool HasAnnotations { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(ViewName)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(ViewType)}` text, " +
               $"`{nameof(IsFound)}` TINYINT, " +
               $"`{nameof(IsFoundHeb)}` text, " +
               $"`{nameof(HasAnnotations)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
