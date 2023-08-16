using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Adv.Tools.DataAccess.Autodesk.AppStore;
using Adv.Tools.UI;
using Adv.Tools.UI.Models;
using Adv.Tools.UI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adv.Tools.Abstractions.Revit;
using System.Threading.Tasks;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.RevitAddin.Services.RevitModelQuality;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;

/// <summary>
/// Represents a namesapce for managing the scripts to be executed in Autodesk Revit.
/// </summary>
namespace Adv.Tools.RevitAddin.Commands
{
    /// <summary>
    /// Represents the RevitCmd class for executing a specific function in Autodesk Revit.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class RevitModelQuality : IExternalCommand
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
            var links = new List<Document>();

            //Refrence to Adv.Tools.UI input objects
            var reports = new List<IReportModelQuality>();
            reports.Add(new ElementsWorksetsReport()
            {
                 ReportDocumnet = new RevitDocument(doc)
            });

            //Aquire the Revit models for which the reports to be excuted
            foreach (Document linkedModel in app.Documents)
            {
                if (linkedModel.IsLinked)
                {
                    string guid = linkedModel.GetCloudModelPath().GetModelGUID().ToString();
                    if(reports.Any(x=>x.ReportDocumnet.Guid.Equals(guid)))
                    {
                        links.Add(linkedModel);
                    }
                }
            }

            //Aquire the data needed for the reports logic
            foreach (var report in reports)
            {
                var userExpectedData = new ModelQualityUserData(report, new MySqlDataAccess(Properties.DataAccess.Default.ProdDb));
                var revitObjectsData = new ModelQualityRevitData(report, doc, userExpectedData.ExpectedObjects);
                
                report.ExistingObjects = revitObjectsData.ExistingObjects;
                report.ExpectedObjects = userExpectedData.ExpectedObjects;
            }

            //Run Reports Logic Algoritem
            foreach(var item in reports)
            {
                item.RunReportLogic();
            }

            //Save Results in the Database
            foreach (var report in reports)
            {
                var dataAccess = new MySqlDataAccess(Properties.DataAccess.Default.ProdDb);
                var reportResults = new ModelQualityResultsData(report, dataAccess);

                reportResults.SaveResultsToDatabase();
            }

            return Result.Succeeded;
        }

    }
}
