using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportMidpSheet : IReportMidpSheet
    {
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ProjBrowSheetName { get; set; }
        public string ProjBrowSheetNumber { get; set; }
        public string SheetRevDate { get; set; }
        public bool IsDateOk { get; set; }
        public string DateHeb { get; set; }
        public string SheetRevSequence { get; set; }
        public bool IsSequenceOk { get; set; }
        public string SequenceHeb { get; set; }
        public string SheetScale { get; set; }
        public bool IsScaleOk { get; set; }
        public string ScaleHeb { get; set; }
        public string SheetHebName { get; set; }
        public bool IsHebNameOk { get; set; }
        public string HebNameHeb { get; set; }
        public string SheetCode { get; set; }
        public bool IsSheetCodeOk { get; set; }
        public string SheetCodeHeb { get; set; }
        public bool IsSheetTidp { get; set; }
        public string SheetTidpHeb { get; set; }
    }
}
