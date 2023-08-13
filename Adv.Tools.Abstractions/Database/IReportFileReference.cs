using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportFileReference
    {
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string LinkName { get; set; }
        string Status { get; set; }
        string Reffrence { get; set; }
        bool IsReffOk { get; set; }
        string IsReffOkHeb { get; set; }
        bool IsStatusOk { get; set; }
        string IsStatusOkHeb { get; set; }
    }
}
