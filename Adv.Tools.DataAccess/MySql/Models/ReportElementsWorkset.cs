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

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
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
