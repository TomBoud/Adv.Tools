using Adv.Tools.RevitAddin.Commands;
using Adv.Tools.RevitAddin.Properties;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components
{
    public interface IAppPushButton
    {
        string Name { get; }
        string Description { get; }
        string AssemblyPath { get; }
        string TriggerClassName { get; }
        Icon Icon { get; }
        Type TriggerClassType { get; }
        Type RibbonPanelType { get; }
    }
}
