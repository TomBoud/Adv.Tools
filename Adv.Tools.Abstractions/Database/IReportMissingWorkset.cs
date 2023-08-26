using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportMissingWorkset
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string WorksetName { get; set; }
        string ObjectId { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }
    }
}
