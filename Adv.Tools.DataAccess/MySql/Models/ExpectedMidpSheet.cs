using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedMidpSheet : IExpectedMidpSheet
    {
        public int Id {get; set;}
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SheetCodeInputText { get; set; }
        public string SheetNameInputText { get; set; }
        public bool IsSheetNameBuiltIn { get; set; }
        public string SheetScaleInputText { get; set; }
        public string SheetNameParamName { get; set; }
        public string SheetNameParamGuid { get; set; }  
        public string SheetScaleParamName { get; set; }
        public string SheetScaleParamGuid { get; set; }
    }
}
