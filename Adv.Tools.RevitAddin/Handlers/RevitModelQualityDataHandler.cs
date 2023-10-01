using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Handlers
{
    public class RevitModelQualityDataHandler : IRvtDataAccess
    {
        private readonly Document _document;
        private readonly IDbDataAccess _dbAccess;
        private readonly string _databaseName;

        public RevitModelQualityDataHandler(IDbDataAccess dbAccess, Document document, string databaseName)
        {
            _document = document;
            _dbAccess = dbAccess;
            _databaseName = databaseName;
        }

        public async Task InitializeReportDataAsync(IReportModelQuality report)
        {
            report.DocumentObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedDocument>(_databaseName);
            report.ReportDocument = new RevitDocument(_document);

            if (report is ElementsWorksetsReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedWorkset>(_databaseName);
                report.RvtDataObjects = GetElementsByExpectedCategoryId(report.DbDataObjects);
            }
            else if (report is MissingWorksetsReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedWorkset>(_databaseName);
                report.RvtDataObjects = GetUserCreatedWorksets();
            }
            else if (report is FileReferenceReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedDocument>(_databaseName);
                report.RvtDataObjects = GetRevitLinkTypes();
            }
            else if (report is LevelsMonitorReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedGridsMonitor>(_databaseName);
                report.RvtDataObjects = GetLevelsAsElements();
            }
            else if (report is GridsMonitorReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedGridsMonitor>(_databaseName);
                report.RvtDataObjects = GetGridsAsElements();
            }
            else if (report is ProjectWarningReport)
            {
                report.DbDataObjects = Enumerable.Empty<object>();
                report.RvtDataObjects = GetDocumentFailureMessages();
            }
            else if (report is ProjectBasePointReports)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedSiteLocation>(_databaseName);
                report.RvtDataObjects = Enumerable.Empty<object>();
            }
            else if (report is ProjectInfoReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedProjectInfo>(_databaseName);
                report.RvtDataObjects = Enumerable.Empty<object>();
            }
            else if (report is SharedParameterReport)
            {
                report.DbDataObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedSharedPara>(_databaseName);
                report.RvtDataObjects = Enumerable.Empty<object>();
            }
        }
  
        public async Task SaveReportResultsDataAsync(IReportModelQuality report)
        {


            //Define the functions to be executed
            Func<Task>[] functions = new Func<Task>[]
            {
                    //async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportElementsWorkset, dynamic>(_databaseName, parameters),
                    //async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName, results),
            };

            //Execute data access operations
            await _dbAccess.ExecuteWithTransaction(functions);
        }
        public async Task SaveReportScoreDataAsync(IReportModelQuality report)
        {
            var checkScoreData = new List<ReportCheckScore>
            {
                new ReportCheckScore
                {
                       Id = 0,
                       CheckLod = ((int)report.Lod).ToString(),
                       CheckScore = "",
                       CheckName = report.ReportName.ToString(),
                       Discipline = string.Empty,
                       ModelGuid = report.ReportDocument.Guid.ToString(),
                       ModelName = report.ReportDocument.Title,
                       IsActive = true,
                }
            };

            await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName, checkScoreData);
        }

        /// <summary>
        /// Get all the Elements which associated with the Allowed Category Ids of IExpectedWorkset
        /// </summary>
        public IEnumerable GetElementsByExpectedCategoryId(IEnumerable expectedObjects)
        {
            //Cast expected objects to the relevant context
            var expectedWorksets = expectedObjects.OfType<IExpectedWorkset>().ToList();
            //Filter workset which are not relevant for the document
            var documnetWorksets = expectedWorksets.Where(x => x.ModelGuid.Equals(_document.GetCloudModelPath().GetModelGUID().ToString()));
            //Get distinct list of Category Ids
            var distinctCategoryIds = documnetWorksets.Select(x => x.CategoryId).Distinct().ToList();
            //Parse string CategoryIds as ElementIds
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(int.Parse).Select(id => new ElementId(id)).ToList();
            #else
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(long.Parse).Select(id => new ElementId(id)).ToList();
            #endif
            //Query the Revit Documnet Model for the Elements
            var collector = new FilteredElementCollector(_document);
            var multicategoryfilter = new ElementMulticategoryFilter(allowedCategories);
            var collection = collector.WherePasses(multicategoryfilter).WhereElementIsNotElementType().ToElements().ToArray();
            //Yield Return the elements
            for (int i = 0; i < collection.Length; i++)
            {
                yield return new RevitElement(collection[i]);
            }
        }

        /// <summary>
        /// Get all User defined worksets in the Revit Model
        /// </summary>
        public IEnumerable<IWorkset> GetUserCreatedWorksets()
        {
            var collector = new FilteredWorksetCollector(_document);
            var filteredCollection = collector.OfKind(WorksetKind.UserWorkset).ToWorksets();

            foreach (var workset in filteredCollection)
            {
                yield return new RevitWorkset(workset);
            }
        }

        /// <summary>
        /// Get all RevitLinkType file References in the Revit Model
        /// </summary>
        public IEnumerable<IRevitLinkType> GetRevitLinkTypes()
        {
            var results = new List<IRevitLinkType>();
            
            ICollection<ElementId> colls = ExternalFileUtils.GetAllExternalFileReferences(_document);
            if (colls.Count.Equals( 0)) { return results; }
            
            foreach (var id in colls)
            {
                Element ele = _document.GetElement(id);
                if (ele is null) 
                {
                    continue; 
                }
                if(ele is RevitLinkType) 
                {
                    var file = ele as RevitLinkType;
                    results.Add(new RevitLinkTypeFile(file));
                }
            }
            return results;
        }

        /// <summary>
        /// Get  All Levels in the Revit Model as IElement
        /// </summary>
        public IEnumerable<IElement> GetLevelsAsElements()
        {

            var collector = new FilteredElementCollector(_document);
            var levels = collector.WhereElementIsNotElementType().OfClass(typeof(Level)).ToList();

            foreach (var level in levels)
            {
                yield return new RevitElement(level);
            }

        }

        /// <summary>
        /// Get All Grids in the Revit Model
        /// </summary>
        public IEnumerable<IElement> GetGridsAsElements()
        {

            var collector = new FilteredElementCollector(_document);
            var grids = collector.WhereElementIsNotElementType().OfClass(typeof(Grid)).ToList();

            foreach (var grid in grids)
            {
                yield return new RevitElement(grid);
            }

        }

        /// <summary>
        /// Get All FailureMessages in the Revit Model
        /// </summary>
        public IEnumerable<IFailureMessage> GetDocumentFailureMessages()
        {
            var warnings = _document.GetWarnings().ToList();

            foreach (var warning in warnings)
            {
                yield return new RevitFailureMessage(warning);
            }

        }
    }
}
