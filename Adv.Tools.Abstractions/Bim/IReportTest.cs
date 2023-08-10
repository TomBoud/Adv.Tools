using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Adv.Tools.Abstractions.Bim
{
    public interface IReportTest
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
