

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
    public class ProjectInfoReport : IReportModelQuality
    {
        public string ReportName { get => nameof(ProjectInfoReport); set => ReportName = nameof(ProjectInfoReport); }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IDocumnet ReportDocument { get; set; }
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
            double totalObjects = ResultObjects.Cast<IReportProjectInfo>().Count();
            double trueFound = ResultObjects.Cast<IReportProjectInfo>().Where(x=>x.IsCorrect).Count();

            double checkScore = 100 * trueFound / totalObjects;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }

        public Task RunReportBusinessLogic()
        {
            var expectedModel = DocumentObjects.Cast<IExpectedDocument>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            var documnetProps = ExistingObjects.Cast<IDocumnet>()
                .FirstOrDefault(x => x.Guid.Equals(ReportDocument.Guid))
                .GetType().GetProperties().ToList();

            var expectedInformation = ExpectedObjects.Cast<IExpectedProjectInfo>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()))
                .GetType().GetProperties().ToList();

            var _resultObjects = new List<IReportProjectInfo>();

            foreach (var expectedProperty in expectedInformation)
            {
                var documnetProperty = documnetProps.FirstOrDefault(x => x.Name.Equals(expectedProperty.Name));
                if (expectedProperty is null) { continue; }

                var report = new ProjectInfoModel()
                {
                    ModelName = expectedModel.ModelName,
                    ModelGuid = expectedModel.ModelGuid,
                    Disicpline = expectedModel.Disicpline,
                    ExpectedValue = (string)expectedProperty?.GetValue(expectedProperty.Name,null) ?? string.Empty,
                    InfoName = documnetProperty.Name,
                    InfoValue = (string)documnetProperty?.GetValue(documnetProperty.Name,null) ?? string.Empty,
                };

                if (report.ExpectedValue.Equals(report.InfoValue))
                {
                    report.IsCorrect = true;
                    report.IsCorrectHeb = "ערך תקין";
                }
                else
                {
                    report.IsCorrect = false;
                    report.IsCorrectHeb = "ערך שגוי";
                }

                _resultObjects.Add(report);
            }

            ResultObjects = _resultObjects;
            return Task.CompletedTask;
        }
    }
}
