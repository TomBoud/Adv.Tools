using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class MissingWorksetsReport : IReportModelQuality
    {
        public string ReportName { get => nameof(MissingWorksetsReport); set => ReportName = nameof(MissingWorksetsReport); }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IDocumnet ReportDocumnet { get; set; }
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
            double falseFound = ResultObjects.Cast<IReportMissingWorkset>().Where(x => x.IsFound == true).Count();

            double checkScore = 100 * falseFound / totalObjects;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }

        public Task RunReportLogic()
        {
            var expectedDocumnet = DocumentObjects.Cast<IExpectedDocument>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocumnet.Guid.ToString()));

            var expectedWorksets = ExpectedObjects.Cast<IExpectedWorkset>();
            var _existingWorksets = ExistingObjects.Cast<IWorkset>();
            var _resultObjects = new List<IReportMissingWorkset>();

            var distinctExpectedWorksets = expectedWorksets.GroupBy(workset => workset.WorksetName).Select(group => group.First()).ToList();
            foreach (var distinctWorkset in distinctExpectedWorksets)
            {
                if(_existingWorksets.Any(x=>x.Name.Equals(distinctWorkset.WorksetName)))
                {
                    var existingWorkset = _existingWorksets.FirstOrDefault(x => x.Name.Equals(distinctWorkset.WorksetName));

                    var report = new MissingWorksetModel()
                    {
                        ModelName = expectedDocumnet.ModelName,
                        ModelGuid = expectedDocumnet.ModelGuid,
                        Disicpline = expectedDocumnet.Disicpline,
                        WorksetName = existingWorkset.Name,
                        ObjectId = existingWorkset.Id.ToString(),
                        IsFound = true,
                        IsFoundHeb = "קיים",
                    };

                    _resultObjects.Add(report);
                }
                else
                {
                    var report = new MissingWorksetModel()
                    {
                        ModelName = expectedDocumnet.ModelName,
                        ModelGuid = expectedDocumnet.ModelGuid,
                        Disicpline = expectedDocumnet.Disicpline,
                        WorksetName = distinctWorkset.WorksetName,
                        ObjectId = string.Empty,
                        IsFound = false,
                        IsFoundHeb = "חסר",  
                    };

                    _resultObjects.Add(report);
                }
            }

            ResultObjects = _resultObjects;
            return Task.CompletedTask;
        }
    }
}
