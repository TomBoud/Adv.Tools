using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Adv.Tools.Abstractions.Revit
{
    public interface ISharedParameterElement
    {
        string Name { get; set; }
        Guid GuidValue { get; set; }
    }
}
