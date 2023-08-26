using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ReportModelSheet : IReportModelSheet
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string ProjBrowSheetName { get; set; }
        public string ProjBrowSheetNumber { get; set; }
        public string SheetTidpCodeValue { get; set; }
        public bool IsDefaultName { get; set; }
        public bool IsDefaultNumber { get; set; }
        public bool IsDefaultDrawnBy { get; set; }
        public bool IsDefaultCheckedBy { get; set; }
        public bool IsDefaultApprovedBy { get; set; }
        public bool IsDefaultDesignedBy { get; set; }
        public bool HasRevisionDate { get; set; }
        public bool HasRevisionNumber { get; set; }
        public bool HasRevisionDescription { get; set; }
        public bool HasTitleBlock { get; set; }
        public bool HasSharedParamValues { get; set; }
        public bool HasScale { get; set; }
        public bool HasHebName { get; set; }
        public string Discipline { get; set; }
    }
}
