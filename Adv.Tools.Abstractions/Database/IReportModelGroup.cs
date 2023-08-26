namespace Adv.Tools.Abstractions.Database
{
    public interface IReportModelGroup
    {
        string Discipline { get; set; }
        string GroupedGroups { get; set; }
        string GroupedObjects { get; set; }
        int Id { get; set; }
        string IsNameCompliance { get; set; }
        string IsNestedGroups { get; set; }
        string IsUniLevel { get; set; }
        string LevelName { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
    }
}