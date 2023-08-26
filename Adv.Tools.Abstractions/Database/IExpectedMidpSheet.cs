using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedMidpSheet
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string SheetCodeInputText { get; set; }
        string SheetNameInputText { get; set; }
        bool IsSheetNameBuiltIn { get; set; }
        string SheetScaleInputText { get; set; }
        string SheetNameParamName { get; set; }
        string SheetNameParamGuid { get; set; }
        string SheetScaleParamName { get; set; }
        string SheetScaleParamGuid { get; set; }
    }
}
