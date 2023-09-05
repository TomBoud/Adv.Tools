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
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using System.Windows.Documents;

namespace Adv.Tools.RevitAddin.Commands.RevitModelQuality
{

    

    [Transaction(TransactionMode.Manual)]
    public class ConfigExpectedCommand : IExternalCommand
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
            var revitObjects = new List<object>();
            var access = new MySqlDataAccess(_connectionString);
            var revitDocument = new RevitDocument(doc) { IsActiveModel = true };

            //Acquire the Revit Objects for the UI operation
            revitObjects.Add(revitDocument);
            revitObjects.AddRange(AcquireAllLinkedModels(app.Documents));

            //Activate The Main UI Container
            IConfigExpectedMainView view = new ConfigExpectedMainView();
            new ConfigExpectedMainPresenter(view, revitObjects, access, revitDocument.DbProjectId);
            view.RunUIApplication();

            return Result.Succeeded;
        }

        private IEnumerable<object> AcquireAllLinkedModels(DocumentSet documentSet)
        {
            var result = new List<object>();

            foreach (Document document in documentSet)
            {
                if (document.IsLinked) 
                { 
                    result.Append(new RevitDocument(document)); 
                }
            }

            return result;
        }
    }
}
