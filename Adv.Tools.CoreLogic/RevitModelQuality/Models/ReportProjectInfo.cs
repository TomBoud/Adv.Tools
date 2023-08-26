using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ReportProjectInfo : IReportProjectInfo
    {
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string InfoName { get; set; }
        public string InfoValue { get; set; }
        public string ExpectedValue { get; set; }
        public bool IsCorrect { get; set; }
        public string IsCorrectHeb { get; set; }
        public string Discipline { get; set; }
    }
}
