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
        public string ReportName { get => nameof(ProjectBasePointReports); set => ReportName = nameof(ProjectBasePointReports); }
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

        public Task RunReportLogic()
        {
            decimal ratio = 30.48M; // Conversion from native API feet standard to metric standard
            var _compareDocument = ExistingObjects.Cast<IDocumnet>().FirstOrDefault();
            var _expectedLocation = ExpectedObjects.Cast<IExpectedSiteLocation>()
                .FirstOrDefault(x=>x.ModelGuid.Equals(ReportDocumnet.Guid.ToString()));
           

            var report = new ProjectBasePointReport()
            {
                ModelName = _expectedLocation.ModelName,
                Disicpline = _expectedLocation.Disicpline,
                ModelGuid = _expectedLocation.ModelGuid,

                ExpectedEastWest = (Convert.ToDecimal(_expectedLocation.EastWest) * ratio).ToString("0.00"),
                ExpectedNorthSouth = (Convert.ToDecimal(_expectedLocation.NorthSouth) * ratio).ToString("0.00"),
                ExpectedElevation = (Convert.ToDecimal(_expectedLocation.Elevation) * ratio).ToString("0.00"),
                ExpectedAngle = _expectedLocation.Angle.ToString(),
                ExpectedLatitude = _expectedLocation.Latitude.ToString(),
                ExpectedLongitude = _expectedLocation.Longitude.ToString(),

                LinkedNorthSouth = (Convert.ToDecimal(_compareDocument.NorthSouth) * ratio).ToString("0.00"),
                LinkedEastWest = (Convert.ToDecimal(_compareDocument.EastWest) * ratio).ToString("0.00"),
                LinkedElevation = (Convert.ToDecimal(_compareDocument.Elevation) * ratio).ToString("0.00"),
                LinkedAngle = _compareDocument.Angle.ToString(),
                LinkedLatitude = _compareDocument.Latitude.ToString(),
                LinkedLongitude = _compareDocument.Longitude.ToString(),
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

            ResultObjects = new List<IReportProjectBasePoint> { report };

            return Task.CompletedTask;
        }
    }
}
