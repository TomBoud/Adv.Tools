

using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ProjectWarningReport : IReportModelQuality
    {
        public string ReportName { get => nameof(ProjectWarningReport); set => ReportName = nameof(ProjectWarningReport); }
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
            double failuresCount = ResultObjects.Cast<IReportProjectWarning>().Count();
            double checkScore = Math.Max(0, 100 - failuresCount * 0.5);

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }

        public Task RunReportBusinessLogic()
        {
            var expectedModel = DocumentObjects.Cast<IExpectedDocument>()
              .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            var documnetFailures = ExistingObjects.Cast<IFailureMessage>();
            var resultObjects = new List<IReportProjectWarning>();

 
            foreach (var failure in documnetFailures)
            {
                var report = new ProjectWarningModel()
                {
                    ModelName = expectedModel.ModelName,
                    Discipline = expectedModel.Discipline,
                    ModelGuid = expectedModel.ModelGuid,
                    Description = failure.Description,
                    Items = failure.ItemsCount.ToString(),
                    Severity = failure.Severity,
                };

                resultObjects.Add(report);
            }

            ResultObjects = resultObjects;
            return Task.CompletedTask;
        }
    }
}
