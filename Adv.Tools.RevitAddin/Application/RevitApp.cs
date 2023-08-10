using Adv.Tools.RevitAddin.Commands;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Adv.Tools.DataAccess.Autodesk.AppStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.RevitAddin.Application.Components;
using Adv.Tools.RevitAddin.Application.Components.PushButtons;
using Adv.Tools.RevitAddin.Properties;
using Autodesk.Internal.InfoCenter;
using System.Drawing;
using System.Reflection;
using Autodesk.Revit.DB.Structure;
using System.ComponentModel;

/// <summary>
/// Represents a namesapce for managing the user interface in Autodesk Revit.
/// </summary>
namespace Adv.Tools.RevitAddin.Application
{
    
    
    
    /// <summary>
    /// Represents the main entrance point for the Revit add-in.
    /// </summary>
    [Regeneration(RegenerationOption.Manual)]
    public class RevitApp : IExternalApplication
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string[] RibbonPanels =
        {
            "Project Settings",
            "Model Checker",
            "Elemets Counter",
            "ArcStr Correlator",
            "Warning Scanner",
            "Cloud Operations",
            "Custom Functions" 
        };
        
        private readonly Dictionary<string,List<(Type,Type,Icon)>> AdvTools;

        /// <summary>
        /// Called when the add-in is started up.
        /// </summary>
        /// <param name="application">The Revit.exe application object.</param>
        /// <returns>The result of the startup operation.</returns>
        public Result OnStartup(UIControlledApplication application)
        {
            var appRibbonPanel = new AppRibbonPanel(application, nameof(AdvTools));

            foreach(string panelName in AdvTools.Keys)
            {
                var panel = appRibbonPanel.CreateNewPanel(panelName);

                foreach(var componnet in AdvTools[panelName])
                {
                    var appPushButton = new AppPushButton(componnet.Item2, componnet.Item3);
                    var button = new componnet.Item1()


                }

            }



            
            if (!(ribbonPanels.FirstOrDefault(x => x.Name.Equals("Project Settings")) is null))
            {
                var ribbonPanel = ribbonPanels.FirstOrDefault(x => x.Name.Equals("Project Settings"));
                var appPushButton = new AppPushButton(typeof(RevitCmd), Resources.configs);
                var configButton = new ConfigurationsButton(appPushButton);
                configButton.InitializeButton(ribbonPanel);
            }


            if (!(ribbonPanels.FirstOrDefault(x => x.Name.Equals("Model Checker")) is null))
            {
                var ribbonPanel = ribbonPanels.FirstOrDefault(x => x.Name.Equals("Model Checker"));
                var appPushButton = new AppPushButton(typeof(RevitCmd), Resources.configs);
                var configButton = new ConfigurationsButton(appPushButton);
                configButton.InitializeButton(ribbonPanel);
            }



            return Result.Succeeded;
        }

        /// <summary>
        /// Called when the add-in is shut down.
        /// </summary>
        /// <param name="application">The Revit.exe application object.</param>
        /// <returns>The result of the shutdown operation.</returns>
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        

    }
}
