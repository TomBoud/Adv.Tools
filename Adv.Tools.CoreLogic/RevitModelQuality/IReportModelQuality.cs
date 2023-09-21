using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;

namespace Adv.Tools.CoreLogic.RevitModelQuality
{
    public interface IReportModelQuality
    {
        string ReportName { get; set; }
        LodType Lod { get; set; }
        DisciplineType[] Disciplines { get; set; }
        IEnumerable ResultObjects { get; set; }
        IEnumerable ExistingObjects { get; set; }
        IEnumerable ExpectedObjects { get; set; }
        IEnumerable DocumentObjects { get; set; }
        IDocument ReportDocument { get; set; }

        string GetReportScoreAsString();
        void RunReportBusinessLogic();
        DisciplineType[] GetDisciplines();

    }
}
