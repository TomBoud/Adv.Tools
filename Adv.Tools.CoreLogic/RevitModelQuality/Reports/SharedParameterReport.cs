

using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.DbEntities;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class SharedParameterReport : IReportModelQuality
    {
        //Properties
        public ReportType ReportName { get => ReportType.ReportSharedParameter; }
        public LodType Lod { get => LodType.Lod100; }
        public IDocument ReportDocument { get; set; }
        public IEnumerable RvtDataObjects { get; set; }
        public IEnumerable DbDataObjects { get; set; }
        public IEnumerable DocumentObjects { get; set; }
        public IEnumerable ResultObjects { get; set; }

        //Private Methods
        private string GetReportScoreAsString()
        {
            double checkScore = 0;

            //Get and Parse this report result objects
            var results = ResultObjects?.OfType<IReportSharedParameter>() ?? null;
            if (results is null) { return string.Empty; }

            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportSharedParameter).GetProperties()
                    .Where(prop => prop.PropertyType == typeof(bool)).ToArray();

            //Check for bool properties existence (avoid zero division)
            if (boolProperties.Length.Equals(0)) { return string.Empty; }

            //Count all positive (true) values for all the results
            foreach (var result in results)
            {
                foreach (PropertyInfo property in boolProperties)
                {
                    bool propertyValue = (bool)property.GetValue(result);
                    if (propertyValue.Equals(true))
                    {
                        checkScore++;
                    }
                }
            }

            //Calculate final score and return  in a string format
            checkScore = 100 * checkScore / (boolProperties.Length * results.Count());
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        private void RunReportCoreLogic()
        {

            var _expectedSharedParams = DbDataObjects.OfType<IExpectedSharedPara>();
            var _existingSharedParams = RvtDataObjects.OfType<ISharedParameterElement>();
            var _resultObjects = new List<IReportSharedParameter>();

            foreach (var param in _expectedSharedParams)
            {
                var report = new SharedParameterModel()
                {
                    ModelName = param.ModelName,
                    Discipline = param.Discipline,
                    ParameterName = param.Parameter,
                    GUID = param.GUID,
                };

                if (_existingSharedParams.Any(x => x.GuidValue.ToString().Equals(param.GUID)))
                {
                    report.IsFound = true;
                    report.IsFoundHeb = "קיים במודל";
                }
                else
                {
                    report.IsFound = false;
                    report.IsFoundHeb = "לא קיים";
                }

                _resultObjects.Add(report);
            }

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
                    RvtDataObjects = Enumerable.Empty<object>();
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
                DbDataObjects = await dbAccess.LoadDataSelectAllAsync<IExpectedSharedPara>(ReportDocument.DbProjectId);
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
                var results = ResultObjects.Cast<IReportSharedParameter>().ToList();

                var functions = new Func<Task>[]
                {
                    async () => await dbAccess.DeleteDataWhereParametersAsync<IReportSharedParameter,dynamic>(ReportDocument.DbProjectId, parameters),
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
                string databaseName = ReportDocument.DbProjectId;

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

                await dbAccess.SaveByInsertUpdateOnDuplicateKeysAsync(databaseName, checkScoreData);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
