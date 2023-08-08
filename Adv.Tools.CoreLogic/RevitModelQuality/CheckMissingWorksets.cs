using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.DataAccess.MySql.Models;

namespace Adv.Tools.RevitAddin.Reports
{
    public class CheckMissingWorksets : IReportTest<IWorkset, ExpectedWorkset, ReportMissingWorkset, ExpectedModel>
    {
        public string ReportName { get { return nameof(CheckMissingWorksets); } set { ReportName = nameof(CheckMissingWorksets); } }
        public string ModelName { get { return _doc?.Title ?? string.Empty; } set { ModelName = value; } }
        public Guid ModelGuid { get { return _doc?.Guid ?? Guid.Empty; } set { ModelGuid = value; } }
        public string Lod { get { return "100"; } set { Lod = "100"; } }
        public IList<IWorkset> ExistingObjects { get { return _worksets.ToList(); } set => ExistingObjects = value; }
        public IList<ExpectedWorkset> ExpectedObjects { get { return ExpectedObjects; } set => ExpectedObjects = value; }
        public IList<ReportMissingWorkset> ResultObjects { get { return ResultObjects; } set => ResultObjects = value; }
        public IList<ExpectedModel> ModelObjects { get { return ModelObjects; } set => ModelObjects = value; }

        private readonly IDocumnet _doc;
        private readonly IEnumerable<IWorkset> _worksets;

        public CheckMissingWorksets(IDocumnet doc, IEnumerable<IWorkset> worksets)
        {
            _doc = doc;
            _worksets = worksets;
        }

        public IList<ExpectedModel> GetModelObjects()
        {
            return new List<ExpectedModel>();
        }


        public IList<ExpectedWorkset> GetExpectedObjects()
        {
            return new List<ExpectedWorkset>();
        }

        public string GetReportScore()
        {
            double totalExpected = ExpectedObjects.Count;
            double correctFound = ResultObjects.Where(x => x.IsFound == true).Count();
            double checkScore = 100 * correctFound / totalExpected;

            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }

        public void RunReportLogic()
        {
            ResultObjects = new List<ReportMissingWorkset>();

            foreach (var exectedItem in ExpectedObjects)
            {
                if(ExistingObjects.Any(x=>x.Name.Equals(exectedItem.WorksetName)))
                {
                    var workset = ExpectedObjects.FirstOrDefault(x=>x.WorksetName.Equals(exectedItem.WorksetName));
                    var report = new ReportMissingWorkset()
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
                    var expectedModel = ModelObjects.FirstOrDefault(x => x.ModelGuid.Equals(ModelGuid));
                    var report = new ReportMissingWorkset()
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
        }
    }
}
