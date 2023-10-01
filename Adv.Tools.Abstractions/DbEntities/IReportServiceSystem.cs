namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportServiceSystem
    {
        string Discipline { get; set; }
        int Id { get; set; }
        bool IsValueAcceptable { get; set; }
        string LevelName { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
        string ObjectType { get; set; }
        string ParameterName { get; set; }
        string ParameterValue { get; set; }
    }
}