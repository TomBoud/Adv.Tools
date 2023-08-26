namespace Adv.Tools.Abstractions.Database
{
    public interface IReportProjectWarning
    {
        string Description { get; set; }
        string Discipline { get; set; }
        int Id { get; set; }
        string Items { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string Severity { get; set; }
    }
}