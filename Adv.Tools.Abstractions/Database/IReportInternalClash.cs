namespace Adv.Tools.Abstractions.Database
{
    public interface IReportInternalClash
    {
        int Id { get; set; }
        string ClashCategory { get; set; }
        string ClashElementId { get; set; }
        string ClashElementName { get; set; }
        string ClashLevelName { get; set; }
        string Discipline { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string SourceCategory { get; set; }
        string SourceElementId { get; set; }
        string SourceElementName { get; set; }
        string SourceLevelName { get; set; }
    }
}