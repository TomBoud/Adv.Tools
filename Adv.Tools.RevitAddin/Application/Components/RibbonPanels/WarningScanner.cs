﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.RibbonPanels
{
    public class WarningScanner : IAppRibbonPanel
    {
        public string TabName => RevitAppTabs.AdvToolsTab.TabName;
        public string PanelName => "Warning Scanner";
        public int Position => 5;
    }
}
