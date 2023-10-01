
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ElementsWorksetsReport : IReportModelQuality
    {
        
        //Properties
        public ReportType ReportName { get => ReportType.ReportElementsWorkset; }
        public LodType Lod { get => LodType.Lod100; }
        public IDocument ReportDocument { get; set; }
        public IEnumerable RvtDataObjects { get; set; }
        public IEnumerable DbDataObjects { get; set; }
        public IEnumerable DocumentObjects { get; set; }
        public IEnumerable ResultObjects { get; set; }

        //Private Methods
        private string GetReportScoreAsString()
        {
            //Get and Parse this report result objects
            var results = ResultObjects?.OfType<IReportProjectInfo>() ?? null;
            if (results is null) { return string.Empty; }

            //Initialize vars and Count all positive (true) values for all the results
            double totalObjects = RvtDataObjects.OfType<IElement>().Count();
            double falseFound = results.Count();

            //Calculate final score and return  in a string format
            double checkScore = 100 * falseFound / totalObjects;
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");

        }
        private void RunReportCoreLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportElementsWorkset>();

            //Initialize existing objects data type
            var _existingElements = RvtDataObjects?.OfType<IElement>().ToList();

            //Initialize expected objects data type
            var _expectedWorksets = DbDataObjects?.OfType<IExpectedWorkset>().ToList();
            if (_expectedWorksets.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            foreach (var worksetName in _expectedWorksets.Select(x => x.WorksetName).Distinct().ToList())
            {
                var elementsOnWorkset = _existingElements.Where(x => x.WorksetName.Equals(worksetName));
                var allowedCategoryIds = _expectedWorksets.Where(x => x.WorksetName.Equals(worksetName));

                foreach (var element in elementsOnWorkset)
                {
                    if (!allowedCategoryIds.Any(x => x.CategoryId.Equals(element.CategoryId)))
                    {
                        var report = new ElementsWorksetModel()
                        {
                            ModelName = _expectedDoc.ModelName,
                            ModelGuid = _expectedDoc.ModelGuid,
                            Discipline = _expectedDoc.Discipline,
                            ObjectCategory = element.CategoryName,
                            ObjectId = element.ElementId.ToString(),
                            ObjectName = element.Name,
                        };

                        _resultObjects.Add(report);
                    }
                }
            }

            //Assign Report Results
            ResultObjects = _resultObjects;
        }

        //Public Tasks
        public async Task ExecuteReportCoreLogicAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    RunReportCoreLogic();
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task GetReportRevitObjectsAsync(IModelQualityHandler rvtAccess)
        {
            try
            {
                await Task.Run(() =>
                {
                    RvtDataObjects = rvtAccess.GetElementsByExpectedCategoryId(DbDataObjects);
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task GetReportDatabaseObjectsAsync(IDbDataAccess dbAccess)
        {
            try
            {
                DocumentObjects = await dbAccess.LoadDataSelectAllAsync<IExpectedDocument>(ReportDocument.DbProjectId);
                DbDataObjects = await dbAccess.LoadDataSelectAllAsync<IExpectedWorkset>(ReportDocument.DbProjectId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task SaveReportResultsDataAsync(IDbDataAccess dbAccess)
        {
            try
            {
                var parameters = new { ModelGuid = ReportDocument.Guid };
                var results = ResultObjects.Cast<IReportElementsWorkset>().ToList();

                Func<Task>[] functions = new Func<Task>[]
                {
                    async () => await dbAccess.DeleteDataWhereParametersAsync<IReportElementsWorkset,dynamic>(ReportDocument.DbProjectId, parameters),
                    async () => await dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(ReportDocument.DbProjectId, results),
                };

                await dbAccess.ExecuteWithTransaction(functions);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task SaveReportScoreDataAsync(IDbDataAccess dbAccess)
        {
            try
            {
                var checkScoreData = new List<IReportCheckScore>
                {
                    new CheckScoreModel
                    {
                       Id = 0,
                       CheckLod = ((int)Lod).ToString(),
                       CheckScore = GetReportScoreAsString(),
                       CheckName = ReportName.ToString(),
                       Discipline = string.Empty,
                       ModelGuid = ReportDocument.Guid.ToString(),
                       ModelName = ReportDocument.Title,
                       IsActive = true,
                    }
                };

                await dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(ReportDocument.DbProjectId, checkScoreData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
