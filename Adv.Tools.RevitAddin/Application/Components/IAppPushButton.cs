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
        string AssemblyPath { get; }
        string TriggerClassName { get; }
        void SetIcon(PushButton button);
        PushButton AddToPanel(RibbonPanel panel, string buttonName, string buttonDescription);
    }
}
