using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportMissingWorkset : IReportMissingWorkset
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string WorksetName { get; set; }
        public string ObjectId { get; set; }
        public bool IsFound { get; set; }
        public bool IsFoundHeb { get; set; }
    }

}
