namespace Adv.Tools.Abstractions.Database
{
    public interface IReportModelSheet
    {
        string Disicpline { get; set; }
        bool HasHebName { get; set; }
        bool HasRevisionDate { get; set; }
        bool HasRevisionDescription { get; set; }
        bool HasRevisionNumber { get; set; }
        bool HasScale { get; set; }
        bool HasSharedParamValues { get; set; }
        bool HasTitleBlock { get; set; }
        int Id { get; set; }
        bool IsDeafultApprovedBy { get; set; }
        bool IsDeafultCheckedBy { get; set; }
        bool IsDeafultDrawnBy { get; set; }
        bool IsDeafultName { get; set; }
        bool IsDeafultNumber { get; set; }
        bool IsDefaultDesignedBy { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ProjBrowSheetName { get; set; }
        string ProjBrowSheetNumber { get; set; }
        string SheetTidpCodeValue { get; set; }
    }
}