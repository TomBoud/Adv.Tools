using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedTidpCode
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string Position { get; set; }
        string UsageType { get; set; }
        string Name { get; set; }
        string Guid { get; set; }
    }
}
