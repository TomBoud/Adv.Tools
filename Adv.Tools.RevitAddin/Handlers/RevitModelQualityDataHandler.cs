using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Adv.Tools.RevitAddin.Handlers
{
    public class RevitModelQualityDataHandler
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
            report.DocumentObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedModel>(_databaseName);
            report.ReportDocument = new RevitDocument(_document);

            if (report is ElementsWorksetsReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedWorkset>(_databaseName);
                report.ExistingObjects = GetElementsByExpectedCategoryId(report.ExpectedObjects);
            }
            else if (report is MissingWorksetsReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedWorkset>(_databaseName);
                report.ExistingObjects = GetUserCreatedWorksets();
            }
            else if (report is FileReferenceReport)
            {
                report.ExpectedObjects = Enumerable.Empty<object>();
                report.ExistingObjects = GetRevitLinkTypes();
            }
            else if (report is LevelsMonitorReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedGridsMonitor>(_databaseName);
                report.ExistingObjects = GetLevelsAsElements();
            }
            else if (report is GridsMonitorReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedGridsMonitor>(_databaseName);
                report.ExistingObjects = GetGridsAsElements();
            }
            else if (report is ProjectWarningReport)
            {
                report.ExpectedObjects = Enumerable.Empty<object>();
                report.ExistingObjects = GetDocumentFailureMessages();
            }
            else if (report is ProjectBasePointReports)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedSiteLocation>(_databaseName);
                report.ExistingObjects = Enumerable.Empty<object>();
            }
            else if (report is ProjectInfoReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedProjectInfo>(_databaseName);
                report.ExistingObjects = Enumerable.Empty<object>();
            }
            else if (report is SharedParameterReport)
            {
                report.ExpectedObjects = await _dbAccess.LoadDataSelectAllAsync<ExpectedSharedPara>(_databaseName);
                report.ExistingObjects = Enumerable.Empty<object>();
            }
        }
        public async Task ActivateReportBusinessLogicAsync(IReportModelQuality report)
        {
            await Task.Run(() =>
            {
                report.RunReportBusinessLogic();
            });
        }
        public async Task SaveReportResultsDataAsync(IReportModelQuality report)
        {

            if (report is ElementsWorksetsReport)
            {
                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportElementsWorkset>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportElementsWorkset, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);

            }
            else if (report is MissingWorksetsReport)
            {
                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportElementsWorkset>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportMissingWorkset, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);

            }
            else if (report is FileReferenceReport)
            {
                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportFileReference>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportFileReference, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);
            }
            else if (report is LevelsMonitorReport)
            {

                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportLevelsMonitor>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportLevelsMonitor, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);
            }
            else if (report is GridsMonitorReport)
            {
                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportGridsMonitor>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportGridsMonitor, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);
            }
            else if (report is ProjectWarningReport)
            {

                var parameters = new { ModelGuid = report.ReportDocument.Guid };
                var results = report.ResultObjects.Cast<ReportProjectWarning>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await _dbAccess.DeleteDataWhereParametersAsync<ReportProjectWarning, dynamic>(_databaseName, parameters),
                    async () => await _dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(_databaseName,results),
                };

                await _dbAccess.ExecuteWithTransaction(functions);
            }
        }
        public async Task SaveReportScoreDataAsync(IReportModelQuality report)
        {
            var checkScoreData = new List<ReportCheckScore>
            {
                new ReportCheckScore
                {
                       Id = 0,
                       CheckLod = ((int)report.Lod).ToString(),
                       CheckScore = report.GetReportScoreAsString(),
                       CheckName = report.ReportName,
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
        private IEnumerable GetElementsByExpectedCategoryId(IEnumerable expectedObjects)
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
        private IEnumerable<IWorkset> GetUserCreatedWorksets()
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
        private IEnumerable<IRevitLinkType> GetRevitLinkTypes()
        {
            var collector = new FilteredElementCollector(_document);
            var linkTypes = collector.WhereElementIsNotElementType()
                .OfClass(typeof(RevitLinkType)).Cast<RevitLinkType>().ToList();

            foreach (var file in linkTypes)
            {
                yield return new RevitLinkTypeFile(file);
            }

        }

        /// <summary>
        /// Get  All Levels in the Revit Model as IElement
        /// </summary>
        private IEnumerable<IElement> GetLevelsAsElements()
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
        private IEnumerable<IElement> GetGridsAsElements()
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
        private IEnumerable<IFailureMessage> GetDocumentFailureMessages()
        {
            var warnings = _document.GetWarnings().ToList();

            foreach (var warning in warnings)
            {
                yield return new RevitFailureMessage(warning);
            }

        }
    }
}
