using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportInternalClash : IReportInternalClash
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string SourceLevelName { get; set; }
        public string SourceCategory { get; set; }
        public string SourceElementName { get; set; }
        public string SourceElementId { get; set; }
        public string ClashCategory { get; set; }
        public string ClashLevelName { get; set; }
        public string ClashElementName { get; set; }
        public string ClashElementId { get; set; }
    }
}
