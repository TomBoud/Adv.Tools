using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportElementsWorkset
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
        string ObjectFamily { get; set; }
        string ObjectCategory { get; set; }
        string ObjectWorkset { get; set; }

    }
}
