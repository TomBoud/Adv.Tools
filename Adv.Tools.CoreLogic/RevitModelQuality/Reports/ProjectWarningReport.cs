

using Adv.Tools.Abstractions.Common;
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
        public string ReportName { get => "ReportProjectWarning"; set => ReportName = "ReportProjectWarning"; }
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
            //Get and Parse this report result objects
            var results = ResultObjects?.OfType<IReportProjectWarning>() ?? null;
            if (results is null) { return string.Empty; }

            //Calculate final score and return  in a string format
            double failuresCount = results.Count();
            double checkScore = Math.Max(0, 100 - failuresCount * 0.5);
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }

        public void RunReportBusinessLogic()
        {
            var expectedModel = DocumentObjects.OfType<IExpectedDocument>()
              .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            var documnetFailures = RvtDataObjects.OfType<IFailureMessage>();
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
