using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportSpaceData : IReportSpaceData , IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SpaceName { get; set; }
        public string SpaceNumber { get; set; }
        public string RoomName { get; set; }
        public string RoomNumber { get; set; }
        public string ObjectLevel { get; set; }
        public string ObjectId { get; set; }
        public string UpperLevel { get; set; }
        public string UpperOffset { get; set; }
        public string BaseOffset { get; set; }
        public string TotalHeight { get; set; }
        public string Area { get; set; }
        public bool IsShaft { get; set; }
        public bool HasDefaultName { get; set; }
        public bool HasName { get; set; }
        public bool HasNumber { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(SpaceName)}` text, " +
               $"`{nameof(SpaceNumber)}` text, " +
               $"`{nameof(RoomName)}` text, " +
               $"`{nameof(RoomNumber)}` text, " +
               $"`{nameof(ObjectLevel)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(UpperLevel)}` text, " +
               $"`{nameof(UpperOffset)}` text, " +
               $"`{nameof(BaseOffset)}` text, " +
               $"`{nameof(TotalHeight)}` text, " +
               $"`{nameof(Area)}` text, " +
               $"`{nameof(IsShaft)}` TINYINT, " +
               $"`{nameof(HasDefaultName)}` TINYINT, " +
               $"`{nameof(HasName)}` TINYINT, " +
               $"`{nameof(HasNumber)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
