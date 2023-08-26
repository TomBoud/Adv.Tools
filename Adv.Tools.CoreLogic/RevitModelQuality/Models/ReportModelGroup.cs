using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ReportModelGroup : IReportModelGroup
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string GroupedObjects { get; set; }
        public string GroupedGroups { get; set; }
        public string IsNameCompliance { get; set; }
        public string IsUniLevel { get; set; }
        public string IsNestedGroups { get; set; }
        public string Discipline { get; set; }
    }
}
