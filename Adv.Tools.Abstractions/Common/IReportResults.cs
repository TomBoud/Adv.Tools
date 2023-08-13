using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common.Enums;

namespace Adv.Tools.Abstractions.Common
{
    public interface IReportResults<T>
    {
        string ReportName { get; set; }
        string ModelName { get; set; }
        Guid ModelGuid { get; set; }
        string Discipline { get; set; }
        LodType Lod { get; set; }
        DisciplineType[] Disciplines { get; set; }
        IList<T> ResultObjects { get; set; }
        string GetReportScore();
        Task RunReportLogic();
        DisciplineType[] GetDisciplines();

    }
}
