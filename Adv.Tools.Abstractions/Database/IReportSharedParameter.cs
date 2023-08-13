namespace Adv.Tools.Abstractions.Database
{
    public interface IReportSharedParameter
    {
        string Disicpline { get; set; }
        string GUID { get; set; }
        int Id { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ParameterName { get; set; }
    }
}