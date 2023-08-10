using Adv.Tools.RevitAddin.Commands;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace Adv.Tools.RevitAddin.Application.Components.PushButtons
{

    public class ConfigurationsButton
    {
        public string ButtonName { get => "Configurations"; }
        public string ButtonDescription { get => "Configurations"; }

        private readonly IAppPushButton _appPushButton;

        public ConfigurationsButton(IAppPushButton appPushButton)
        {
           _appPushButton = appPushButton;
        }

        public void InitializeButton(RibbonPanel ribbonPanel)
        {
            var button = _appPushButton.AddToPanel(ribbonPanel, ButtonName, ButtonDescription);
            _appPushButton.SetIcon(button);
        }
    }
}
