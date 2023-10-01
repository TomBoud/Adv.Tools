namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportSpaceData
    {
        string Area { get; set; }
        string BaseOffset { get; set; }
        string Discipline { get; set; }
        bool HasDefaultName { get; set; }
        bool HasName { get; set; }
        bool HasNumber { get; set; }
        int Id { get; set; }
        bool IsShaft { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectLevel { get; set; }
        string RoomName { get; set; }
        string RoomNumber { get; set; }
        string SpaceName { get; set; }
        string SpaceNumber { get; set; }
        string TotalHeight { get; set; }
        string UpperLevel { get; set; }
        string UpperOffset { get; set; }
    }
}