using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IFailureMessage
    {
        string Description {get; set;}
        int ItemsCount { get; set; }
        string Severity { get; set; }
    }
}
