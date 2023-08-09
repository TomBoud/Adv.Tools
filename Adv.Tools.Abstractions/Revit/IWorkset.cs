using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IWorkset
    {
        string Name { get; set; }
        int Id { get; set; }
        Guid Guid { get; set; }
    }
}
