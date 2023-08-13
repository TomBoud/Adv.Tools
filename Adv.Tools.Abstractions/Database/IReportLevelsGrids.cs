namespace Adv.Tools.Abstractions.Database
{
    public interface IReportLevelsGrids
    {
        string Disicpline { get; set; }
        int Id { get; set; }
        bool IsCopyMonitor { get; set; }
        string IsCopyMonitorHeb { get; set; }
        bool IsOriginValid { get; set; }
        string IsOriginValidHeb { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
        string ObjectOrigin { get; set; }
        string ObjectType { get; set; }
    }
}