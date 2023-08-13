using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Common.Enums;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class MissingWorksetsReport : IReportResults<IReportMissingWorkset>
    {
        public string ReportName { get => nameof(MissingWorksetsReport); set => ReportName = nameof(MissingWorksetsReport); }
        public string ModelName { get => _doc?.Title ?? string.Empty; set => ModelName = value; }
        public Guid ModelGuid { get => _doc?.Guid ?? Guid.Empty; set => ModelGuid = value; }
        public string Discipline { get => DocumentObjects.FirstOrDefault(x => x.ModelGuid.Equals(_doc.Guid.ToString())).Disicpline; set => Discipline = value; }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IList<IWorkset> ExistingObjects { get => _worksets; set => ExistingObjects = value; }
        public IList<IExpectedWorkset> ExpectedObjects { get; set; }
        public IList<IExpectedDocument> DocumentObjects { get; set; }
        public IList<IReportMissingWorkset> ResultObjects { get; set; }
        

        private readonly LodType _lod;
        private readonly IDocumnet _doc;
        private readonly IList<IWorkset> _worksets;

        public MissingWorksetsReport(IDocumnet doc, IList<IWorkset> worksets)
        {
            _doc = doc;
            _worksets = worksets;

            ExpectedObjects = new List<IExpectedWorkset>();
            DocumentObjects = new List<IExpectedDocument>();
            ResultObjects = new List<IReportMissingWorkset>();
        }

        public string GetReportScore()
        {
            double totalExpected = ExpectedObjects.Count;
            double correctFound = ResultObjects.Where(x => x.IsFound == true).Count();
            double checkScore = 100 * correctFound / totalExpected;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        public Task RunReportLogic()
        {
            ResultObjects.Clear();

            foreach (var exectedItem in ExpectedObjects)
            {
                if(ExistingObjects.Any(x=>x.Name.Equals(exectedItem.WorksetName)))
                {
                    var workset = ExpectedObjects.FirstOrDefault(x=>x.WorksetName.Equals(exectedItem.WorksetName));
                    var report = new MissingWorksetModel()
                    {
                        ModelName = workset.ModelName,
                        ModelGuid = workset.ModelGuid,
                        Disicpline = workset.Disicpline,
                        WorksetName = workset.WorksetName,
                        ObjectId = workset.ModelName,
                        IsFound = true,
                        IsFoundHeb = "קיים",
                    };

                    ResultObjects.Add(report);
                }
                else
                {
                    var expectedModel = DocumentObjects.FirstOrDefault(x => x.ModelGuid.Equals(ModelGuid));
                    var report = new MissingWorksetModel()
                    {
                        ModelName = expectedModel.ModelName,
                        ModelGuid = expectedModel.ModelGuid,
                        Disicpline = expectedModel.Disicpline,
                        WorksetName = exectedItem.WorksetName,
                        ObjectId = string.Empty,
                        IsFound = false,
                        IsFoundHeb = "חסר",  
                    };

                    ResultObjects.Add(report);
                }
            }

            return Task.CompletedTask;
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
    }
}
