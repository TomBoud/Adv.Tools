using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportModelSheet : IReportModelSheet
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string ProjBrowSheetName { get; set; }
        public string ProjBrowSheetNumber { get; set; }
        public string SheetTidpCodeValue { get; set; }
        public bool IsDeafultName { get; set; }
        public bool IsDeafultNumber { get; set; }
        public bool IsDeafultDrawnBy { get; set; }
        public bool IsDeafultCheckedBy { get; set; }
        public bool IsDeafultApprovedBy { get; set; }
        public bool IsDefaultDesignedBy { get; set; }
        public bool HasRevisionDate { get; set; }
        public bool HasRevisionNumber { get; set; }
        public bool HasRevisionDescription { get; set; }
        public bool HasTitleBlock { get; set; }
        public bool HasSharedParamValues { get; set; }
        public bool HasScale { get; set; }
        public bool HasHebName { get; set; }
    }
}
