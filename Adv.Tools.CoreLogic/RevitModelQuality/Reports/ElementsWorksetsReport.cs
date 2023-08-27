
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ElementsWorksetsReport : IReportModelQuality
    {
        public string ReportName { get => nameof(ElementsWorksetsReport); set => ReportName = value; }
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

            double totalObjects = ExistingObjects.Cast<IElement>().Count();
            double falseFound = ResultObjects.Cast<IReportElementsWorkset>().Count();
            double checkScore = 100 * falseFound / totalObjects;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");

        }
        public void RunReportBusinessLogic()
        {

            var _resultObjects = new List<IReportElementsWorkset>();
            var _expectedWorksets = ExpectedObjects.Cast<IExpectedWorkset>();
            var _elements = ExistingObjects.Cast<IElement>();
            var _doc = DocumentObjects.Cast<IExpectedDocument>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            // Get Collection of the elements foreach workset if they should not be related to it
            foreach (var worksetName in _expectedWorksets.Select(x => x.WorksetName).Distinct().ToList())
            {
                var elementsOnWorkset = _elements.Where(x => x.WorksetName.Equals(worksetName));
                var allowedCategoryIds = _expectedWorksets.Where(x=>x.WorksetName.Equals(worksetName));

                foreach (var element in elementsOnWorkset)
                {
                    if (!allowedCategoryIds.Any(x => x.CategoryId.Equals(element.CategoryId)))
                    {
                        var report = new ElementsWorksetModel()
                        {
                            ModelName = _doc.ModelName,
                            ModelGuid = _doc.ModelGuid,
                            Discipline = _doc.Discipline,
                            ObjectCategory = element.CategoryName,
                            ObjectId = element.ElementId.ToString(),
                            ObjectName = element.Name,
                        };

                        _resultObjects.Add(report);
                    }
                }
            }

            ResultObjects = _resultObjects;
        }
    }
}
