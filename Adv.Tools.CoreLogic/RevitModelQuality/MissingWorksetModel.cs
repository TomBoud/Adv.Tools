using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.RevitModelQuality
{
    public class MissingWorksetModel : IReportMissingWorkset
    {
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string WorksetName { get; set; }
        public string ObjectId { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }
    }
}
