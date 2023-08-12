using Adv.Tools.RevitAddin.Application.Components.RevitAppTabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.RibbonPanels
{
    public class ModelChecker : IAppRibbonPanel
    {
        public string TabName => AdvToolsTab.TabName;
        public string PanelName => "Model Checker";
    }

}
