namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportSharedParameter
    {
        string Discipline { get; set; }
        string GUID { get; set; }
        int Id { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ParameterName { get; set; }
    }
}