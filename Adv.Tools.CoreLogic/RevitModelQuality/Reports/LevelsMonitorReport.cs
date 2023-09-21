

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
        public string ReportName { get => "ReportLevelsMonitor"; set => ReportName = "ReportLevelsMonitor"; }
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
            var results = ResultObjects.OfType<IReportLevelsMonitor>().ToList();
            if (results.Count.Equals(0)) { return string.Empty; }
            
            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportLevelsMonitor).GetProperties()
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
            var _resultObjects = new List<IReportLevelsMonitor>();

            //Initialize existing objects data type
            var _existingLevels = ExistingObjects?.OfType<IElement>().ToList();

            //Initialize expected objects data type
            var _expectedLevels = ExpectedObjects?.OfType<IExpectedLevelsMonitor>().ToList();
            if (_expectedLevels.Count.Equals(0)) { ResultObjects = _resultObjects; return; }

            //Initialize user defined documents data type
            var _expectedDoc = DocumentObjects?.OfType<IExpectedDocument>()?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedDoc is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
            foreach (var expectedLevel in _expectedLevels)
            {
                foreach (var level in _existingLevels)
                {
                    var report = new LevelsMonitorModel()
                    {
                        ModelName = _expectedDoc.ModelName,
                        Discipline = _expectedDoc.Discipline,
                        ModelGuid = _expectedDoc.ModelGuid,
                        ObjectId = level?.ElementId.ToString() ?? string.Empty,
                        ObjectName = level?.Name ?? string.Empty,
                        ObjectType = level?.CategoryName ?? string.Empty,
                        IsCopyMonitor = level.IsMonitoring,
                        IsCopyMonitorHeb = string.Empty,
                        IsOriginValid = false,
                        ObjectOrigin = string.Empty,
                        IsOriginValidHeb = string.Empty,
                    };

                    if (report.IsCopyMonitor)
                    {
                        report.IsCopyMonitorHeb = "מוניטור פעיל";

                        if (level.MonitoredDoc != null)
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
                    _resultObjects.Add(report);
                }
            }

            //Assign Report Results
            ResultObjects = _resultObjects;
        }
    }
}
