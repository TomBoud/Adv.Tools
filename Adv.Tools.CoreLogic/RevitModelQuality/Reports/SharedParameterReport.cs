

using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
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
        public string ReportName { get => "ReportSharedParameter"; set => ReportName = "ReportSharedParameter"; }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IDocument ReportDocument { get; set; }
        public IEnumerable RvtDataObjects { get; set; }
        public IEnumerable DbDataObjects { get; set; }
        public IEnumerable DocumentObjects { get; set; }
        public IEnumerable ResultObjects { get; set; }

        public Task ExecuteReportBusinessLogic()
        {
            throw new NotImplementedException();
        }

        public DisciplineType[] GetDisciplines()
        {
            return new DisciplineType[]
            {
                DisciplineType.Structural,
                DisciplineType.Architectural,
                DisciplineType.Electrical,
                DisciplineType.Mechanical,
                DisciplineType.Plumbing,
                DisciplineType.Landscape,
            };
        }

        public Task GetReportDatabaseObjectsAsync(IDbDataAccess rvtAccess)
        {
            throw new NotImplementedException();
        }

        public Task GetReportRevitObjectsAsync(IRvtDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }

        public string GetReportScoreAsString()
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
        public void RunReportBusinessLogic()
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

        public Task SaveReportResultsDataAsync(IDbDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }

        public Task SaveReportScoreDataAsync(IDbDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }
    }
}
