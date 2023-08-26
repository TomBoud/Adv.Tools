

using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class LevelsMonitorReport : IReportModelQuality
    {
        public string ReportName { get => nameof(LevelsMonitorReport); set => ReportName = nameof(LevelsMonitorReport); }
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
            var results = ResultObjects.Cast<IReportLevelsMonitor>();
            if (results is null) { return string.Empty; }
            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportLevelsMonitor).GetProperties()
                    .Where(prop => prop.PropertyType == typeof(bool)).ToArray();
            //Check for bool properties existence (avoid zero division)
            if (boolProperties.Length.Equals(0)) { return string.Empty; }
            //Count all positive (true) values for all the results
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

        public Task RunReportBusinessLogic()
        {

            var expectedDocumnet = DocumentObjects.Cast<IExpectedDocument>()
               .FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));

            var expectedLevelsGrids = ExpectedObjects.Cast<IExpectedLevelsGrids>()
                .Where(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString())).ToList();

            var docLevelsGrids = ExistingObjects.Cast<IElement>();
            var resultObjects = new List<IReportLevelsMonitor>();
            
            foreach (var expectedLevelGrid in expectedLevelsGrids)
            {
                foreach (var element in docLevelsGrids)
                {
                    var report = new ReportLevelsMonitor()
                    {
                        ModelName = expectedDocumnet.ModelName,
                        Discipline = expectedDocumnet.Discipline,
                        ModelGuid = expectedDocumnet.ModelGuid,
                        ObjectId = element?.ElementId.ToString() ?? string.Empty,
                        ObjectName = element?.Name ?? string.Empty,
                        ObjectType = element?.CategoryName ?? string.Empty,
                        IsCopyMonitor = element.IsMonitoring,
                        IsCopyMonitorHeb = string.Empty,
                        IsOriginValid = false,
                        ObjectOrigin = string.Empty,
                        IsOriginValidHeb = string.Empty,
                    };

                    if (report.IsCopyMonitor)
                    {
                        report.IsCopyMonitorHeb = "מוניטור פעיל";

                        if (element.MonitoredDoc != null)
                        {
                            report.IsOriginValid = true;
                            report.IsOriginValidHeb = "מודל מקור תקין";
                        }
                        else
                        {
                            report.IsOriginValid = false;
                            report.IsOriginValidHeb = "מודל מקור שגוי";
                        }
                    }
                    else
                    {
                        report.IsCopyMonitorHeb = "מוניטור לא פעיל";
                        report.IsOriginValid = false;
                        report.IsOriginValidHeb = "מודל מקור לא ידוע";
                    }
                    resultObjects.Add(report);
                }
            }
            ResultObjects = resultObjects;
            return Task.CompletedTask;
        }
    }
}
