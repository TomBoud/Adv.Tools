using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ModelGroupModel : IReportModelGroup
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string GroupedObjects { get; set; }
        public string GroupedGroups { get; set; }
        public bool IsNameCompliance { get; set; }
        public bool IsUniLevel { get; set; }
        public bool IsNestedGroups { get; set; }
        
    }
}
