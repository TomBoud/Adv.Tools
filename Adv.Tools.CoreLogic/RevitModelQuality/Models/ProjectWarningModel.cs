using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ProjectWarningModel : IReportProjectWarning
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string Severity { get; set; }
        public string Description { get; set; }
        public string Items { get; set; }
    }
}
