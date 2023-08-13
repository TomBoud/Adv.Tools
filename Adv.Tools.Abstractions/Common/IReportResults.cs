using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Adv.Tools.Abstractions.Common.Enums;

namespace Adv.Tools.Abstractions.Common
{
    public interface IReportResults
    {
        string ReportName { get; set; }
        string ModelName { get; set; }
        Guid ModelGuid { get; set; }
        LodType Lod { get; set; }
        DisciplineType[] Disciplines { get; set; }
        string GetReportScore();
        void RunReportLogic();
    }
}
