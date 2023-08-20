
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class SharedParameterModel : IReportSharedParameter
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string ParameterName { get; set; }
        public string GUID { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }
        
    }
}
