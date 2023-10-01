using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IExpectedTidpCode
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string Position { get; set; }
        string UsageType { get; set; }
        string Name { get; set; }
        string Guid { get; set; }
    }
}
