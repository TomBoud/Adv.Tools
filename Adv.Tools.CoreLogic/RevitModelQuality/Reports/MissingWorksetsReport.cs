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
        public string ReportName { get => "ReportMissingWorksets"; set => ReportName = "ReportMissingWorksets"; }
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
            var results = ResultObjects?.OfType<IReportMissingWorkset>() ?? null;
            if (results is null) { return string.Empty; }

            //Initialize vars and Count all positive (true) values for all the results
            double totalObjects = ExistingObjects.OfType<IElement>().ToList().Count;
            double falseFound = results.Where(x => x.IsFound).ToList().Count;

            //Calculate final score and return  in a string format
            double checkScore = 100 * falseFound / totalObjects;
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        public void RunReportBusinessLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportMissingWorkset>();

            //Initialize existing objects data type
            var _existingWorksets = ExistingObjects?.OfType<IWorkset>().ToList();

            //Initialize expected objects data type
            var _expectedWorksets = ExpectedObjects?.OfType<IExpectedWorkset>().ToList();
            if (_expectedWorksets.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            var distinctExpectedWorksets = _expectedWorksets.GroupBy(workset => workset.WorksetName).Select(group => group.First()).ToList();
            foreach (var distinctWorkset in distinctExpectedWorksets)
            {
                if(_existingWorksets.Any(x=>x.Name.Equals(distinctWorkset.WorksetName)))
                {
                    var existingWorkset = _existingWorksets.FirstOrDefault(x => x.Name.Equals(distinctWorkset.WorksetName));

                    var report = new MissingWorksetModel()
                    {
                        ModelName = _expectedDoc.ModelName,
                        ModelGuid = _expectedDoc.ModelGuid,
                        Discipline = _expectedDoc.Discipline,
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
                        ModelName = _expectedDoc.ModelName,
                        ModelGuid = _expectedDoc.ModelGuid,
                        Discipline = _expectedDoc.Discipline,
                        WorksetName = distinctWorkset.WorksetName,
                        ObjectId = string.Empty,
                        IsFound = false,
                        IsFoundHeb = "חסר",  
                    };

                    _resultObjects.Add(report);
                }
            }

            //Assign Report Results
            ResultObjects = _resultObjects;
        }
    }
}
