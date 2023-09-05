using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.RevitAddin.Handlers;
using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Represents a namesapce for managing the scripts to be executed in Autodesk Revit.
/// </summary>
namespace Adv.Tools.RevitAddin.Commands.RevitModelQuality
{
    /// <summary>
    /// Represents the RevitCmd class for executing a specific function in Autodesk Revit.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class DisplayResultsCommand : IExternalCommand
    {

        private readonly string _connectionString = Properties.DataAccess.Default.ProdDb;

        /// <summary>
        /// Main entrance to the class when called by the Revit.exe UI.
        /// </summary>
        /// <param name="commandData">The ExternalCommandData object which provided by the Revit.exe app.</param>
        /// <param name="message">The message string for Result.Failed event.</param>
        /// <param name="elements">The ElementSet object for Result.Failed event.</param>
        /// <returns>The Result object.</returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Reference to Revit.exe application objects
            var app = commandData.Application.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;

            //Reference to support objects
            var access = new MySqlDataAccess(_connectionString);
            var links = new List<Document>();
            var tasks = new List<Task>();

            //Acquire the Revit models for which the reports to be executed
            foreach (Document linkedModel in app.Documents)
            {
                if (linkedModel.IsLinked) { links.Add(linkedModel); }
            }

            //Reference the Adv.Tools.UI input objects
            var reports = new List<IReportModelQuality>
            {
                new ElementsWorksetsReport()
                {
                    ReportDocument = new RevitDocument(doc)
                }
            };

            //Acquire the data needed for the reports logic
            foreach (var report in reports)
            {
                var document = links.FirstOrDefault(x => x.GetCloudModelPath().GetModelGUID().Equals(report.ReportDocument.Guid));
                var dataHandler = new RevitModelQualityDataHandler(access, document, report.ReportDocument.DbProjectId);

                tasks.Add(Task.Run(async () =>
                {
                    await dataHandler.InitializeReportDataAsync(report);
                }).ContinueWith(async _ =>
                {
                    await dataHandler.ActivateReportBusinessLogicAsync(report);
                }).ContinueWith(async _ =>
                {
                    await dataHandler.SaveReportResultsDataAsync(report);
                }).ContinueWith(async _ =>
                {
                    await dataHandler.SaveReportScoreDataAsync(report);
                }));
            }

            Task.WaitAll(tasks.ToArray());
            return Result.Succeeded;
        }

    }
}
