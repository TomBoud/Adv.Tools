

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
        public IEnumerable ExistingObjects { get; set; }
        public IEnumerable ExpectedObjects { get; set; }
        public IEnumerable DocumentObjects { get; set; }
        public IEnumerable ResultObjects { get; set; }

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
        public string GetReportScore()
        {
            double checkScore = 0;
            //Cast results property to a valid list
            var results = ResultObjects.Cast<IReportSharedParameter>();
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
            var _doc = DocumentObjects.Cast<IExpectedDocument>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            var _expectedSharedParams = ExpectedObjects.Cast<IExpectedSharedPara>();
            var _existingSharedParams = ExistingObjects.Cast<ISharedParameterElement>();
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

                if (_existingSharedParams.Any(x => x.GuidValue.Equals(new Guid(param.GUID))))
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
    }
}
