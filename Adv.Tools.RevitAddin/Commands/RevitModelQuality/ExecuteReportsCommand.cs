using Adv.Tools.DataAccess.MySql;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.RevitAddin.Application;
using Adv.Tools.RevitAddin.Models;
using Adv.Tools.Abstractions.DbEntities;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Repository;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Views;
using Adv.Tools.RevitAddin.Application.Components;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.RevitAddin.Handlers;


namespace Adv.Tools.RevitAddin.Commands.RevitModelQuality
{
    /// <summary>
    /// Represents the RevitCmd class for executing a specific function in Autodesk Revit.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class ExecuteReportsCommand : IExternalCommand
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

            var app = commandData.Application.Application;
            var doc = commandData.Application.ActiveUIDocument.Document;
            var links = RevitDataAccess.GetLinedRevitModels(app.Documents);

            var dbAccess = new MySqlDataAccess(_connectionString);
            var dbName = new RevitDocument(doc).DbProjectId;
            
            //Acquire user input about which reports to be executed
            IConfigReportView view = new ConfigReportView();
            IConfigReportRepo repo = new ConfigReportRepo(dbAccess, dbName);
            var presenter = new ConfigReportPresenter(view, repo);
            view.RunUIApplication();

            //Parse user input as a list of IReportModelQuality objects
            var reportInstances = new List<IReportModelQuality>();
            var reportTypes = Assembly.GetAssembly(typeof(IReportModelQuality)).GetTypes()
                .Where(t => typeof(IReportModelQuality).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();

            //Execute Reports
            var tasks = new List<Task>();
            foreach (var reportType in reportTypes)
            {
                foreach (var rvtModel in links)
                {
                    var rvtHandler = new ModelQualityHandler(rvtModel);
                    var reportInstance = Activator.CreateInstance(reportType) as IReportModelQuality;
                    
                    reportInstance.ReportDocument = new RevitDocument(rvtModel);
                    tasks.Add(Task.Run(async () => await ExecuteReportRoutineAsync(reportInstance, dbAccess, rvtHandler)));
                }
            }

            Task.WaitAll(tasks.ToArray());
            return Result.Succeeded;
        }

        private async Task ExecuteReportRoutineAsync(IReportModelQuality report, IDbDataAccess dbAccess, IModelQualityHandler rvtHandler)
        {
            await report.GetReportDatabaseObjectsAsync(dbAccess);
            await report.GetReportRevitObjectsAsync(rvtHandler);
            await report.ExecuteReportCoreLogicAsync();
            await report.SaveReportResultsDataAsync(dbAccess);
            await report.SaveReportScoreDataAsync(dbAccess);
        }
    }
}
