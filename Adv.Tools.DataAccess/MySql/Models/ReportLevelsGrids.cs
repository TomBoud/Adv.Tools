using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportLevelsGrids : IReportLevelsGrids
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectOrigin { get; set; }
        public bool IsCopyMonitor { get; set; }
        public string IsCopyMonitorHeb { get; set; }
        public bool IsOriginValid { get; set; }
        public string IsOriginValidHeb { get; set; }
    }
}
