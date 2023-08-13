using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportSharedParameter : IReportSharedParameter
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
