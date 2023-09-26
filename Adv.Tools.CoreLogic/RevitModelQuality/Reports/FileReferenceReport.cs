

using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class FileReferenceReport : IReportModelQuality
    {
        public string ReportName { get => "ReportFileReference"; set => ReportName = "ReportFileReference"; }
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
            
            //Cast results property to a valid list
            var results = ResultObjects?.OfType<IReportFileReference>() ?? null;
            if (results is null) { return string.Empty; }
            
            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportFileReference).GetProperties()
                    .Where(prop => prop.PropertyType == typeof(bool)).ToArray();
            
            //Check for bool properties existence (avoid zero division)
            if (boolProperties.Length.Equals(0)) { return string.Empty; }
            
            //Count all positive (true) values for all the results
            double checkScore = 0;
            foreach (var result in results)
            {
                foreach (PropertyInfo property in boolProperties)
                {
                    bool propertyValue = (bool)property.GetValue(result);
                    if (propertyValue.Equals(true))
                    {
                        checkScore++;
                    }
                }
            }

            //Calculate final score and return  in a string format
            checkScore = 100 * checkScore / (boolProperties.Length * results.Count());
            return double.IsNaN(checkScore) ? string.Empty : checkScore.ToString("0.#");
        }
        public void RunReportBusinessLogic()
        {
            //Initialize Result objects return data type
            var _resultObjects = new List<IReportFileReference>();

            //Initialize existing objects data type
            var _existingRevitLinks = ExistingObjects.OfType<IRevitLinkType>().ToList();

            //Initialize expected objects data type
            var _expectedRevitLinks = ExpectedObjects.OfType<IExpectedDocument>()
                .Where(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()) is false).ToList();
            if (_expectedRevitLinks.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            foreach (var file in _expectedRevitLinks)
            {
                var linkFileType = _existingRevitLinks.FirstOrDefault(x => x.FileGuid.ToString().Equals(file.ModelGuid));

                var report = new ReportFileReference()
                {
                    ModelName = _expectedDoc.ModelName,
                    Discipline = _expectedDoc.Discipline,
                    ModelGuid = _expectedDoc.ModelGuid,
                    LinkName = linkFileType?.FileName ?? string.Empty,
                    Status = linkFileType?.LinkedFileStatus ?? string.Empty,
                    Reference = linkFileType?.AttachmentType ?? string.Empty,
                };

                if (report.Status != "Loaded") { report.IsStatusOk = false; report.IsStatusOkHeb = "סטטוס לא תקין"; }
                else { report.IsStatusOk = true; report.IsStatusOkHeb = "סטטוס תקין"; }

                if (report.Reference != "Overlay") { report.IsReffOk = false; report.IsReffOkHeb = "ייחוס לא תקין"; }
                else { report.IsReffOk = true; report.IsReffOkHeb = "ייחוס תקין"; }

                _resultObjects.Add(report);
            }

            //Assign Report Results
            ResultObjects = _resultObjects;
        }
    }
}
