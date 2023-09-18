using Adv.Tools.RevitAddin.Application.Components.RibbonPanels;
using Adv.Tools.RevitAddin.Commands.RevitModelQuality;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Application.Components.PushButtons
{
    public class ExecuteReportsButton : IAppPushButton
    {
        public string Name => "Run Tests";
        public string Description => "Run Tests";
        public Type RibbonPanelType => typeof(ModelChecker);
        public Type TriggerClassType => typeof(ExecuteReportsCommand);
        public string AssemblyPath => TriggerClassType.Assembly.Location;
        public string TriggerClassName => TriggerClassType.FullName;
        public Icon Icon => Properties.Resources.tests;
        
    }
}
