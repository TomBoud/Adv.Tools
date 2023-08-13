
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Common.Enums;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ElementsWorksetsReport : IReportResults<IReportElementsWorkset>
    {
        public string ReportName { get => nameof(ElementsWorksetsReport); set => ReportName = nameof(ElementsWorksetsReport); }
        public string ModelName { get => _doc?.Title ?? string.Empty; set => ModelName = value; }
        public Guid ModelGuid { get => _doc?.Guid ?? Guid.Empty; set => ModelGuid = value; }
        public string Discipline { get=> DocumentObjects.FirstOrDefault(x=>x.ModelGuid.Equals(_doc.Guid.ToString())).Disicpline; set => Discipline=value; }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IList<IElement> ExistingElements { get => _elements; set => ExistingElements = value; }
        public IList<IExpectedWorkset> ExpectedWorksets { get; set; }
        public IList<IExpectedDocument> DocumentObjects { get; set; }
        public IList<IReportElementsWorkset> ResultObjects { get; set; }

        private readonly IDocumnet _doc;
        private readonly IList<IElement> _elements;

        public ElementsWorksetsReport(IDocumnet doc, IList<IElement> elements)
        {
            _doc = doc;
            _elements = elements;

            ExpectedWorksets = new List<IExpectedWorkset>();
            DocumentObjects = new List<IExpectedDocument>();
            ResultObjects = new List<IReportElementsWorkset>();
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
        
        public string GetReportScore()
        {

            double totalObjects = ExistingElements.Count;
            double falseFound = ResultObjects.Count;
            double checkScore = 100 * falseFound / totalObjects;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");

        }

        public Task RunReportLogic()
        {
            ResultObjects.Clear();

            // Get Collection of the elements foreach workset if they should not be related to it
            foreach (var worksetName in ExpectedWorksets.Select(x => x.WorksetName).Distinct().ToList())
            {
                var elementsOnWorkset = ExistingElements.Where(x => x.WorksetName.Equals(worksetName));
                var allowedCategoryIds = ExpectedWorksets.Where(x=>x.WorksetName.Equals(worksetName));

                foreach (var element in elementsOnWorkset)
                {
                    if (!allowedCategoryIds.Any(x => x.CategoryId.Equals(element.CategoryId)))
                    {
                        var report = new ElementsWorksetModel()
                        {
                            ModelName = this.ModelName,
                            ModelGuid = this.ModelGuid.ToString(),
                            Disicpline = this.Discipline,
                            ObjectCategory = element.CategoryName,
                            ObjectId = element.ElementId.ToString(),
                            ObjectName = element.Name,
                        };
                        
                        ResultObjects.Add(report);
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
