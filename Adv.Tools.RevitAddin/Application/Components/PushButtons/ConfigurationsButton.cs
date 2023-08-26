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
        public string Name => "Configurations";
        public string Description => "Configurations";
        public Type RibbonPanelType => typeof(ProjectSettings);
        public string AssemblyPath => TriggerClassType.Assembly.Location;
        public string TriggerClassName => TriggerClassType.FullName;
        public Icon Icon => Properties.Resources.configs;
        public Type TriggerClassType => typeof(RevitModelQualityCommand);
    }
}
