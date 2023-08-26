namespace Adv.Tools.Abstractions.Database
{
    public interface IReportModelPlace
    {
        string Discipline { get; set; }
        int Id { get; set; }
        string LevelName { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
        string ObjectType { get; set; }
    }
}