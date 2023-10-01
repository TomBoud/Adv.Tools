using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class RoomDataModel : IReportRoomData
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
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
        public string Discipline { get; set; }
    }
}
