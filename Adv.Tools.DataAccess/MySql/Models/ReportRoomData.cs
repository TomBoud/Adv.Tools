using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using crypto;
using Org.BouncyCastle.Utilities;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportRoomData : IReportRoomData, IDbModelEntity
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string RoomName { get; set; }
        public string RoomNumber { get; set; }
        public string Department { get; set; }
        public string ObjectLevel { get; set; }
        public string ObjectId { get; set; }
        public string UpperLevel { get; set; }
        public string UpperOffset { get; set; }
        public string BaseOffset { get; set; }
        public string TotalHeight { get; set; }
        public string Area { get; set; }
        public string BaseFinish { get; set; }
        public string FloorFinish { get; set; }
        public string WallFinish { get; set; }
        public string CeilingFinish { get; set; }
        public bool IsShaft { get; set; }
        public bool HasDefaultName { get; set; }
        public bool HasName { get; set; }
        public bool HasNumber { get; set; }
        public bool HasDepartment { get; set; }
        public bool HasBaseFinish { get; set; }
        public bool HasFloorFinish { get; set; }
        public bool HasWallFinish { get; set; }
        public bool HasCeilingFinish { get; set; }

        public string GetCreateTableQuery(string databaseName)
        {
            string sqlQuery =
               $"CREATE SCHEMA IF NOT EXISTS {databaseName} DEFAULT CHARACTER SET utf8mb4;" +
               $"CREATE TABLE IF NOT EXISTS {databaseName}.{GetType().Name} " +
               $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
               $"`{nameof(ModelName)}` text, " +
               $"`{nameof(ModelGuid)}` text, " +
               $"`{nameof(Discipline)}` text, " +
               $"`{nameof(RoomName)}` text, " +
               $"`{nameof(RoomNumber)}` text, " +
               $"`{nameof(Department)}` text, " +
               $"`{nameof(ObjectLevel)}` text, " +
               $"`{nameof(ObjectId)}` text, " +
               $"`{nameof(UpperLevel)}` text, " +
               $"`{nameof(UpperOffset)}` text, " +
               $"`{nameof(BaseOffset)}` text, " +
               $"`{nameof(TotalHeight)}` text, " +
               $"`{nameof(Area)}` text, " +
               $"`{nameof(BaseFinish)}` text, " +
               $"`{nameof(FloorFinish)}` text, " +
               $"`{nameof(WallFinish)}` text, " +
               $"`{nameof(CeilingFinish)}` text, " +
               $"`{nameof(IsShaft)}` TINYINT, " +
               $"`{nameof(HasDefaultName)}` TINYINT, " +
               $"`{nameof(HasName)}` TINYINT, " +
               $"`{nameof(HasNumber)}` TINYINT, " +
               $"`{nameof(HasDepartment)}` TINYINT, " +
               $"`{nameof(HasBaseFinish)}` TINYINT, " +
               $"`{nameof(HasFloorFinish)}` TINYINT, " +
               $"`{nameof(HasWallFinish)}` TINYINT, " +
               $"`{nameof(HasCeilingFinish)}` TINYINT, " +
               $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }

    }
}
