namespace Adv.Tools.Abstractions.Database
{
    public interface IReportServiceSystem
    {
        string Disicpline { get; set; }
        int Id { get; set; }
        string IsValueAcceptable { get; set; }
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