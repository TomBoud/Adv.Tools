using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components
{
    public class AppRibbonPanel : IAppRibbonPanel
    {
        public string TabName => _tabName;

        private readonly UIControlledApplication _application;
        private readonly string _tabName;

        public AppRibbonPanel(UIControlledApplication application, string tabName)
        {
            _application = application;
            _tabName = tabName;
        }

        public RibbonPanel CreateNewPanel(string panelName)
        {
           return _application.CreateRibbonPanel(_tabName, panelName);
        }
    }
}
