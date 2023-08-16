using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace Adv.Tools.RevitAddin.Application.Components
{
    internal interface IAppRibbonPanel
    {
        string TabName { get; }
        string  PanelName { get; }
        int Position { get; }
    }
}
