using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ProjectBasePointReports : IReportModelQuality
    {
        public string ReportName { get => "ReportProjectBasePoint"; set => ReportName = "ReportProjectBasePoint"; }
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
        public string GetReportScore()
        {
            double checkScore = 0;
            //Cast results property to a valid list
            var results = ResultObjects.Cast<IReportProjectBasePoint>();
            if (results is null) { return string.Empty; }
            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportProjectBasePoint).GetProperties()
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
        public void RunReportBusinessLogic()
        {
            //Initialize Business Logic Parameters
            decimal ratio = 30.48M; // Conversion from native API "feet" units to metric
            var _resultObjects = new List<IReportProjectBasePoint>();
            var _expectedLocation = ExpectedObjects?.OfType<IExpectedSiteLocation>()?.FirstOrDefault(x=>x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
           
            //Return nothing if there was no data provided
            if (_expectedLocation is null) { ResultObjects = _resultObjects; return; }

            var report = new ProjectBasePointModel()
            {
                ModelName = _expectedLocation.ModelName,
                Discipline = _expectedLocation.Discipline,
                ModelGuid = _expectedLocation.ModelGuid,

                ExpectedEastWest = (Convert.ToDecimal(_expectedLocation.EastWest) * ratio).ToString("0.00"),
                ExpectedNorthSouth = (Convert.ToDecimal(_expectedLocation.NorthSouth) * ratio).ToString("0.00"),
                ExpectedElevation = (Convert.ToDecimal(_expectedLocation.Elevation) * ratio).ToString("0.00"),
                ExpectedAngle = _expectedLocation.Angle.ToString(),
                ExpectedLatitude = _expectedLocation.Latitude.ToString(),
                ExpectedLongitude = _expectedLocation.Longitude.ToString(),

                LinkedNorthSouth = (Convert.ToDecimal(ReportDocument.NorthSouth) * ratio).ToString("0.00"),
                LinkedEastWest = (Convert.ToDecimal(ReportDocument.EastWest) * ratio).ToString("0.00"),
                LinkedElevation = (Convert.ToDecimal(ReportDocument.Elevation) * ratio).ToString("0.00"),
                LinkedAngle = ReportDocument.Angle.ToString(),
                LinkedLatitude = ReportDocument.Latitude.ToString(),
                LinkedLongitude = ReportDocument.Longitude.ToString(),
            };

            if (report.ExpectedNorthSouth != report.LinkedNorthSouth) { report.IsBasePoint = false; report.IsBasePointHeb = "נקודת יחוס שגויה"; }
            else if (report.ExpectedEastWest != report.LinkedEastWest) { report.IsBasePoint = false; report.IsBasePointHeb = "נקודת יחוס שגויה"; }
            else if (report.ExpectedElevation != report.LinkedElevation) { report.IsBasePoint = false; report.IsBasePointHeb = "נקודת יחוס שגויה"; }
            else if (report.ExpectedAngle != report.LinkedAngle) { report.IsBasePoint = false; report.IsBasePointHeb = "נקודת יחוס שגויה"; }
            else { report.IsBasePoint = true; report.IsBasePointHeb = "נקודת יחוס תקינה"; }
            if (report.ExpectedLatitude != report.LinkedLatitude) { report.IsLocation = false; report.IsLocationHeb = "מיקום גאוגרפי שגוי"; }
            else if (report.ExpectedLongitude != report.LinkedLongitude) { report.IsLocation = false; report.IsLocationHeb = "מיקום גאוגרפי שגוי"; }
            else { report.IsLocation = true; report.IsBasePointHeb = "מיקום גאוגרפי תקין"; }
            if (report.IsLocation != true || report.IsBasePoint != true) { report.IsCorrect = false; report.IsCorrectHeb = "קורדינטות שגויות"; }
            else { report.IsCorrect = true; report.IsCorrectHeb = "קורדינטות תקינות"; }
            
            _resultObjects.Add(report);
            ResultObjects = _resultObjects;
        }
    }
}
