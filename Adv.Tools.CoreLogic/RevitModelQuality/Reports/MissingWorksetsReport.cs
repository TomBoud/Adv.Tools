using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class MissingWorksetsReport : IReportModelQuality
    {
        //Properties
        public ReportType ReportName { get => ReportType.ReportMissingWorkset; }
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
            var results = ResultObjects?.OfType<IReportMissingWorkset>() ?? null;
            if (results is null) { return string.Empty; }

            //Initialize vars and Count all positive (true) values for all the results
            double totalObjects = RvtDataObjects.OfType<IElement>().ToList().Count;
            double falseFound = results.Where(x => x.IsFound).ToList().Count;

            //Calculate final score and return  in a string format
            double checkScore = 100 * falseFound / totalObjects;
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        private void RunReportCoreLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportMissingWorkset>();

            //Initialize existing objects data type
            var _existingWorksets = RvtDataObjects?.OfType<IWorkset>().ToList();

            //Initialize expected objects data type
            var _expectedWorksets = DbDataObjects?.OfType<IExpectedWorkset>().ToList();
            if (_expectedWorksets.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            var distinctExpectedWorksets = _expectedWorksets.GroupBy(workset => workset.WorksetName).Select(group => group.First()).ToList();
            foreach (var distinctWorkset in distinctExpectedWorksets)
            {
                if (_existingWorksets.Any(x => x.Name.Equals(distinctWorkset.WorksetName)))
                {
                    var existingWorkset = _existingWorksets.FirstOrDefault(x => x.Name.Equals(distinctWorkset.WorksetName));

                    var report = new MissingWorksetModel()
                    {
                        ModelName = _expectedDoc.ModelName,
                        ModelGuid = _expectedDoc.ModelGuid,
                        Discipline = _expectedDoc.Discipline,
                        WorksetName = existingWorkset.Name,
                        ObjectId = existingWorkset.Id.ToString(),
                        IsFound = true,
                        IsFoundHeb = "קיים",
                    };

                    _resultObjects.Add(report);
                }
                else
                {
                    var report = new MissingWorksetModel()
                    {
                        ModelName = _expectedDoc.ModelName,
                        ModelGuid = _expectedDoc.ModelGuid,
                        Discipline = _expectedDoc.Discipline,
                        WorksetName = distinctWorkset.WorksetName,
                        ObjectId = string.Empty,
                        IsFound = false,
                        IsFoundHeb = "חסר",
                    };

                    _resultObjects.Add(report);
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

                var functions = new Func<Task>[]
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
                var expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid));

                var checkScoreData = new List<IReportCheckScore>
                {
                    new CheckScoreModel
                    {
                       Id = 0,
                       ModelName = expectedDoc.ModelName,
                       ModelGuid = expectedDoc.ModelGuid,
                       Discipline = expectedDoc.Discipline,
                       CheckName = ReportName.ToString(),
                       CheckLod = ((int)Lod).ToString(),
                       CheckScore = GetReportScoreAsString(),
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
