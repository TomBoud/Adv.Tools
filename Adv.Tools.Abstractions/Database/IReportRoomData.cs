namespace Adv.Tools.Abstractions.Database
{
    public interface IReportRoomData
    {
        string Area { get; set; }
        string BaseFinish { get; set; }
        string BaseOffset { get; set; }
        string CeilingFinish { get; set; }
        string Department { get; set; }
        string Discipline { get; set; }
        string FloorFinish { get; set; }
        bool HasBaseFinish { get; set; }
        bool HasCeilingFinish { get; set; }
        bool HasDefaultName { get; set; }
        bool HasDepartment { get; set; }
        bool HasFloorFinish { get; set; }
        bool HasName { get; set; }
        bool HasNumber { get; set; }
        bool HasWallFinish { get; set; }
        int Id { get; set; }
        bool IsShaft { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectLevel { get; set; }
        string RoomName { get; set; }
        string RoomNumber { get; set; }
        string TotalHeight { get; set; }
        string UpperLevel { get; set; }
        string UpperOffset { get; set; }
        string WallFinish { get; set; }
    }
}