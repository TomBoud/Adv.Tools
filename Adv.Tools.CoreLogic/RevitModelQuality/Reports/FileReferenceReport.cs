

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
        public string ReportName { get => nameof(FileReferenceReport); set => ReportName = nameof(FileReferenceReport); }
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
            double checkScore = 0;
            //Cast results property to a valid list
            var results = ResultObjects.Cast<IReportFileReference>();
            //Check for casting failure (avoid accessing null instance)
            if (results is null) { return string.Empty; }
            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportFileReference).GetProperties()
                    .Where(prop => prop.PropertyType == typeof(bool)).ToArray();
            //Check for bool properties existence (avoid zero division)
            if (boolProperties.Length.Equals(0)) { return string.Empty; }
            //Count all positive (true) values for all the results
            foreach(var result in results)
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

        public Task RunReportBusinessLogic()
        {
            //Initizlize the expected results List<T>
            var resultObjects = new List<IReportFileReference>();
            //The revit type files which where found linked to the reported model
            var existingRevitLinks = ExistingObjects.Cast<IRevitLinkType>();
            //Get all the revit files which should to be found as linked to the parent
            var expectedRevitLinks = ExpectedObjects.Cast<IExpectedDocument>()
                .Where(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()) is false);
            //Get the the reported model as a IExpectedDocument object
            var expectedDoc = DocumentObjects.Cast<IExpectedDocument>()
                .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            foreach (var file in expectedRevitLinks)
            {
                var linkFileType = existingRevitLinks.FirstOrDefault(x => x.FileGuid.ToString().Equals(file.ModelGuid));

                var report = new ReportFileReference()
                {
                    ModelName = expectedDoc.ModelName,
                    Disicpline = expectedDoc.Disicpline,
                    ModelGuid = expectedDoc.ModelGuid,
                    LinkName = linkFileType?.FileName ?? string.Empty,
                    Status = linkFileType?.LinkedFileStatus ?? string.Empty,
                    Reference = linkFileType?.AttachmentType ?? string.Empty,
                };

                if (report.Status != "Loaded") { report.IsStatusOk = false; report.IsStatusOkHeb = "סטטוס לא תקין"; }
                else { report.IsStatusOk = true; report.IsStatusOkHeb = "סטטוס תקין"; }

                if (report.Reference != "Overlay") { report.IsReffOk = false; report.IsReffOkHeb = "ייחוס לא תקין"; }
                else { report.IsReffOk = true; report.IsReffOkHeb = "ייחוס תקין"; }

                resultObjects.Add(report);
            }

            ResultObjects = resultObjects;
            return Task.CompletedTask;
        }
    }
}
