using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportFileReference : IReportFileReference
    {
        public string ModelName { get; set; }   
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string LinkName { get; set; }
        public string Status { get; set; }
        public string Reffrence { get; set; }
        public bool IsReffOk { get; set; }
        public string IsReffOkHeb { get; set; }
        public bool IsStatusOk { get; set; }
        public string IsStatusOkHeb { get; set; }
    }
}
