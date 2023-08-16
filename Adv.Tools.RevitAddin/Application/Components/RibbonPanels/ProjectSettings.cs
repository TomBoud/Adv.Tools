using Adv.Tools.RevitAddin.Application.Components.RevitAppTabs;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.RibbonPanels
{
    public class ProjectSettings : IAppRibbonPanel
    {
        public string TabName => AdvToolsTab.TabName;
        public string PanelName => "Project Settings";
        public int Position => 1;
    }
}
