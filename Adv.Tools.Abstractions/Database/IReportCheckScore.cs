using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportCheckScore
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string CheckName { get; set; }
        string CheckLod { get; set; }
        string CheckScore { get; set; }
    }
}
