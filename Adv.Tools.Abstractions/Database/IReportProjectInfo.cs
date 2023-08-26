namespace Adv.Tools.Abstractions.Database
{
    public interface IReportProjectInfo
    {
        string Discipline { get; set; }
        string ExpectedValue { get; set; }
        string InfoName { get; set; }
        string InfoValue { get; set; }
        bool IsCorrect { get; set; }
        string IsCorrectHeb { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
    }
}