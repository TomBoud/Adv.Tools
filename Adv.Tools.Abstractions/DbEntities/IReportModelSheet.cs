namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportModelSheet
    {
        string Discipline { get; set; }
        bool HasHebName { get; set; }
        bool HasRevisionDate { get; set; }
        bool HasRevisionDescription { get; set; }
        bool HasRevisionNumber { get; set; }
        bool HasScale { get; set; }
        bool HasSharedParamValues { get; set; }
        bool HasTitleBlock { get; set; }
        int Id { get; set; }
        bool IsDefaultApprovedBy { get; set; }
        bool IsDefaultCheckedBy { get; set; }
        bool IsDefaultDrawnBy { get; set; }
        bool IsDefaultName { get; set; }
        bool IsDefaultNumber { get; set; }
        bool IsDefaultDesignedBy { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ProjBrowSheetName { get; set; }
        string ProjBrowSheetNumber { get; set; }
        string SheetTidpCodeValue { get; set; }
    }
}