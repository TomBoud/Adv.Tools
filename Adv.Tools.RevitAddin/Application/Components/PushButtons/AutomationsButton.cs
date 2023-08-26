using Adv.Tools.RevitAddin.Application.Components.RibbonPanels;
using Adv.Tools.RevitAddin.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.PushButtons
{
    public class AutomationsButton : IAppPushButton
    {
        public string Name => "Automations";
        public string Description => "Automations";
        public Type RibbonPanelType => typeof(Automations);
        public string AssemblyPath => TriggerClassType.Assembly.Location;
        public string TriggerClassName => TriggerClassType.FullName;
        public Icon Icon => Properties.Resources.utilities;
        public Type TriggerClassType => typeof(RevitModelQualityCommand);

    }
}
