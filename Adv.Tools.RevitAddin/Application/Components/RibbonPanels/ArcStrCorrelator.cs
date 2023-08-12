using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.RibbonPanels
{
    public class ArcStrCorrelator : IAppRibbonPanel
    {
        public string TabName => RevitAppTabs.AdvToolsTab.TabName;

        public string PanelName => "ArcStr Correlator";
    }
}
