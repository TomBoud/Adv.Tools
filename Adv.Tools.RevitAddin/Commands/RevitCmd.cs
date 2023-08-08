using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Adv.Tools.Abstractions;
using Adv.Tools.DataAccess.Autodesk.AppStore;
using Adv.Tools.UI;
using Adv.Tools.UI.Models;
using Adv.Tools.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Represents a namesapce for managing the scripts to be executed in Autodesk Revit.
/// </summary>
namespace Adv.Tools.RevitAddin.Commands
{
    /// <summary>
    /// Represents the RevitCmd class for executing a specific function in Autodesk Revit.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class RevitCmd : IExternalCommand
    {
        /// <summary>
        /// Main entrance to the class when called by the Revit.exe UI.
        /// </summary>
        /// <param name="commandData">The ExternalCommandData object which provided by the Revit.exe app.</param>
        /// <param name="message">The message string for Result.Failed event.</param>
        /// <param name="elements">The ElementSet object for Result.Failed event.</param>
        /// <returns>The Result object.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Reffrence to Revit.exe application objects
            var app = commandData.Application.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;

            //Check if the user is autherized to use this commmand
            if (Properties.AppStore.Default.AuthRequired)
            {
                if (CommandProofingStatus(app.LoginUserId) is false)
                {
                    return Result.Cancelled;
                };
            }

            //Absstract revit objects for the UI
            IDocumnet idoc = new RevitDocument(doc);
            var iElements = GetActiveViewElemets(doc);

            //Start the UI to get the user input
            ElementRepo.ElementDataRepo = iElements.Select(ele => new ElementModel(ele)).ToList();
            Program.Main();

            //Get the user interactions results
            //var userPrefernces = GetUserPrefernces(userInterface);

            //Execute this command logic
            //var logicOutput = ExecuteCommandLogic(doc, userPrefernces);

            //Return feedback to the user
            //TaskDialog.Show(string.Empty, logicOutput?.ToString() ?? string.Empty);
            return Result.Succeeded;
        }

        /// <summary>
        /// Executes the command logic.
        /// </summary>
        /// <param name="doc">The Revit API Document object.</param>
        /// <param name="userPreferences">The list of user preferences.</param>
        /// <returns>The logic output.</returns>
        private object ExecuteCommandLogic(Document doc, object userInput)
        {
            var userPrefernces = userInput as IList<(PropertyInfo, object)>;
            var collecotrElemetns = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(Wall)).ToElements();

            var revitElements = new List<RevitElement>();
            collecotrElemetns.ToList().ForEach(element =>
            {
                if (element.Name.Contains(userPrefernces.First().Item1.Name))
                {
                    revitElements.Add(new RevitElement(element));
                }
            });

            return null;
        }

        /// <summary>
        /// Checks if a user has entitlement to use the application.
        /// </summary>
        /// <param name="userId">The user ID of the user to check.</param>
        /// <returns>True if the user has entitlement, false otherwise.</returns>
        private bool UserHasEntitlement(string userId)
        {
            string baseUrl = Properties.AppStore.Default.BaseUrl;
            string endPoint = Properties.AppStore.Default.EndPoint;
            string appId = Properties.AppStore.Default.AppId;

            var appStore = new EntitlementApi(baseUrl, endPoint);

            return appStore.CheckEntitlement(appId, userId);
        }

        /// <summary>
        /// Executes a command with proofing to determine if the user is authorized to use the application.
        /// </summary>
        /// <param name="userId">The user ID of the user executing the command.</param>
        /// <returns>The result of the command execution.</returns>
        private bool CommandProofingStatus(string userId)
        {
            //Check if the user is Signed In using Autodesk License Manager
            if (string.IsNullOrEmpty(userId))
            {
                TaskDialog.Show("Entitlement API", "Please Sign In to use this app");
                //User is NOT allowed to use this command while Signed out
                return false;
            }

            //Compare the time span between the grace date to today
            if (Properties.AppStore.Default.GraceDateTime > DateTime.Now)
            {
                //User is allowed to use this command for the duration of a grace period
                return true;
            }
            else
            {
                try
                {
                    //Run the Entitlement API to check if the user is allowed
                    if (UserHasEntitlement(userId))
                    {
                        //The user is autherized, extend the grace date to temporary skip proofing
                        //Get the grace settings
                        var settings = Properties.AppStore.Default;
                        //Extend the allowed grace time
                        settings.GraceDateTime = DateTime.Today.AddDays(7);
                        //Update new grace settings
                        settings.Save();
                        return true;
                    }
                    else
                    {
                        //User is NOT autherized to use this command, return feedback about it
                        TaskDialog.Show("Entitlement API", "You are not authorized to use this app");
                        return false;
                    }
                }
                //Allow the user a short grace period in case of an error
                catch (Exception)
                {
                    //Get the grace settings
                    var settings = Properties.AppStore.Default;
                    var timeSpan = settings.GraceTime;
                    //Extend the allowed grace time
                    settings.GraceDateTime = DateTime.Today.Add(timeSpan);
                    //Update new grace settings
                    settings.Save();
                    return true;
                }
            }
        }

        private IEnumerable<IElement> GetActiveViewElemets(Document doc)
        {
            var collecotrElemetns = new FilteredElementCollector(doc, doc.ActiveView.Id)
               .WhereElementIsNotElementType();

            var revitElements = new List<RevitElement>();
            collecotrElemetns.ToElements().ToList().ForEach(element =>
            {
                revitElements.Add(new RevitElement(element));
            });

            return revitElements.Cast<IElement>().AsEnumerable();
        }
    }
}
