using Adv.Tools.RevitAddin.Commands;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Drawing;
using Adv.Tools.RevitAddin.Application.Components.RibbonPanels;

namespace Adv.Tools.RevitAddin.Application.Components.PushButtons
{

    public class ConfigurationsButton : IAppPushButton
    {
        public string ButtonName => "Configurations";
        public string ButtonDescription => "Configurations";
        public Type ButtonRibbonPanel => typeof(ProjectSettings);
        public string AssemblyPath => ClassToTrigger.Assembly.Location;
        public string TriggerClassName => ClassToTrigger.FullName;
        public Icon ButtonIcon => Properties.Resources.configs;
        public Type ClassToTrigger => typeof(RevitCmd);
    }
}
