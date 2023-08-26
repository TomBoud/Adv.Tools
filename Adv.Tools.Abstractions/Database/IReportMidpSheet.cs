
namespace Adv.Tools.Abstractions.Database
{
    public interface IReportMidpSheet
    {
        string DateHeb { get; set; }
        string Discipline { get; set; }
        string HebNameHeb { get; set; }
        bool IsDateOk { get; set; }
        bool IsHebNameOk { get; set; }
        bool IsScaleOk { get; set; }
        bool IsSequenceOk { get; set; }
        bool IsSheetCodeOk { get; set; }
        bool IsSheetTidp { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ProjBrowSheetName { get; set; }
        string ProjBrowSheetNumber { get; set; }
        string ScaleHeb { get; set; }
        string SequenceHeb { get; set; }
        string SheetCode { get; set; }
        string SheetCodeHeb { get; set; }
        string SheetHebName { get; set; }
        string SheetRevDate { get; set; }
        string SheetRevSequence { get; set; }
        string SheetScale { get; set; }
        string SheetTidpHeb { get; set; }
    }
}