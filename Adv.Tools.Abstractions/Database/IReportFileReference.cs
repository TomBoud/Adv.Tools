using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportFileReference
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string LinkName { get; set; }
        string Status { get; set; }
        string Reference { get; set; }
        bool IsReffOk { get; set; }
        string IsReffOkHeb { get; set; }
        bool IsStatusOk { get; set; }
        string IsStatusOkHeb { get; set; }
    }
}
