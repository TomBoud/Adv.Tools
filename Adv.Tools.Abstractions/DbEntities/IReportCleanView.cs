using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportCleanView
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string ViewName { get; set; }
        string ObjectId { get; set; }
        string ViewType { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }
        bool HasAnnotations { get; set; }
    }
}
