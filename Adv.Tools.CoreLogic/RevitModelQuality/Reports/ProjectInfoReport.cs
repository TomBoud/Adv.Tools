

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
        public string ReportName { get => "ReportProjectInfo"; set => ReportName = "ReportProjectInfo"; }
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
            double totalObjects = ResultObjects.Cast<IReportProjectInfo>().Count();
            double trueFound = ResultObjects.Cast<IReportProjectInfo>().Where(x=>x.IsCorrect).Count();

            double checkScore = 100 * trueFound / totalObjects;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        public void RunReportBusinessLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportProjectInfo>();

            //Initialize existing objects data type
            var _existingInfo = ReportDocument.GetType().GetProperties().ToList();

            //Initialize expected objects data type
            var _expectedInfo = ExpectedObjects.Cast<IExpectedProjectInfo>()
                ?.Where(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()))
                .GetType().GetProperties().ToList();
            if (_expectedInfo.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            foreach (var property in _expectedInfo)
            {
                var existingProperty = _existingInfo.FirstOrDefault(x => x.Name.Equals(property.Name));
                if (property is null) { continue; }

                var report = new ProjectInfoModel()
                {
                    ModelName = _expectedDoc.ModelName,
                    ModelGuid = _expectedDoc.ModelGuid,
                    Discipline = _expectedDoc.Discipline,
                    ExpectedValue = (string)property?.GetValue(property.Name,null) ?? string.Empty,
                    InfoName = existingProperty.Name,
                    InfoValue = (string)existingProperty?.GetValue(existingProperty.Name,null) ?? string.Empty,
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

            //Assign Report Results
            ResultObjects = _resultObjects;
        }
    }
}
