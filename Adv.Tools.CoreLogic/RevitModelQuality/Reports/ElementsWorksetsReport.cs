
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ElementsWorksetsReport : IReportModelQuality
    {
        public string ReportName { get => "ReportElementsWorksets"; set => ReportName = "ReportElementsWorksets"; }
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
        public string GetReportScoreAsString()
        {
            //Get and Parse this report result objects
            var results = ResultObjects?.OfType<IReportProjectInfo>() ?? null;
            if (results is null) { return string.Empty; }

            //Initialize vars and Count all positive (true) values for all the results
            double totalObjects = ExistingObjects.OfType<IElement>().Count();
            double falseFound = results.Count();

            //Calculate final score and return  in a string format
            double checkScore = 100 * falseFound / totalObjects;
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");

        }
        public void RunReportBusinessLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportElementsWorkset>();

            //Initialize existing objects data type
            var _existingElements = ExistingObjects?.OfType<IElement>().ToList();

            //Initialize expected objects data type
            var _expectedWorksets = ExpectedObjects?.OfType<IExpectedWorkset>().ToList();
            if (_expectedWorksets.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            foreach (var worksetName in _expectedWorksets.Select(x => x.WorksetName).Distinct().ToList())
            {
                var elementsOnWorkset = _existingElements.Where(x => x.WorksetName.Equals(worksetName));
                var allowedCategoryIds = _expectedWorksets.Where(x=>x.WorksetName.Equals(worksetName));

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
    }
}
