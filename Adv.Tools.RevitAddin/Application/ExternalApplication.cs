using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Adv.Tools.RevitAddin.Application.Components;
using Adv.Tools.DataAccess.MySql;

/// <summary>
/// Represents a namesapce for managing the user interface in Autodesk Revit.
/// </summary>
namespace Adv.Tools.RevitAddin.Application
{
    /// <summary>
    /// Represents the main entrance point for the Revit add-in.
    /// </summary>
    [Regeneration(RegenerationOption.Manual)]
    public class ExternalApplication : IExternalApplication
    {
        /// <summary>
        /// Called when the add-in is started up.
        /// </summary>
        /// <param name="application">The Revit.exe application object.</param>
        /// <returns>The result of the startup operation.</returns>
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                application.CreateRibbonTab(Components.RevitAppTabs.AdvToolsTab.TabName);

                var ribbonPanelTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IAppRibbonPanel).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();
                var ribbonPanels = ribbonPanelTypes.Select(ribbonPanelType => Activator.CreateInstance(ribbonPanelType) as IAppRibbonPanel).OrderBy(panel => panel.Position).ToList();

                foreach (var ribbonPanel in ribbonPanels)
                {
                    application.CreateRibbonPanel(ribbonPanel.TabName, ribbonPanel.PanelName);
                }

                var pushButtonTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IAppPushButton).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();

                foreach (var pushButton in pushButtonTypes)
                {
                    var pushButtonInstance = Activator.CreateInstance(pushButton) as IAppPushButton;
                    var ribbonPanelInstance = Activator.CreateInstance(pushButtonInstance.RibbonPanelType) as IAppRibbonPanel;

                    var appPushButton = new AppPushButton(pushButtonInstance);
                    var appRibbonPanel = application.GetRibbonPanels(ribbonPanelInstance.TabName).FirstOrDefault(x => x.Name.Equals(ribbonPanelInstance.PanelName));

                    _ = appRibbonPanel.AddItem(appPushButton.ButtonData) as PushButton;
                }

            }
            catch (Exception ex)
            {
                var mainDialog = new TaskDialog("Adv Tools UI error")
                {
                    MainInstruction = ex.Message,
                    MainContent = "Please contact the developer for assistance",
                    MainIcon = TaskDialogIcon.TaskDialogIconError,
                    CommonButtons = TaskDialogCommonButtons.Ok,
                };

                mainDialog.Show();
                return Result.Failed;
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
