using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportMissingWorkset
    {
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string WorksetName { get; set; }
        string ObjectId { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }
    }
}
