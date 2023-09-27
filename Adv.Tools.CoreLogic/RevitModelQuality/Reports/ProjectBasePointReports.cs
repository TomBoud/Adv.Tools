using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.CoreLogic.RevitModelQuality.Models;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Reports
{
    public class ProjectBasePointReports : IReportModelQuality
    {
        private readonly decimal ratio = 30.48M; // Conversion from native API "feet" units to "metric" units

        public string ReportName { get => "ReportProjectBasePoint"; set => ReportName = "ReportProjectBasePoint"; }
        public DisciplineType[] Disciplines { get => GetDisciplines(); set => Disciplines = value; }
        public LodType Lod { get => LodType.Lod100; set => Lod = value; }
        public IDocument ReportDocument { get; set; }
        public IEnumerable RvtDataObjects { get; set; }
        public IEnumerable DbDataObjects { get; set; }
        public IEnumerable DocumentObjects { get; set; }
        public IEnumerable ResultObjects { get; set; }

        public Task ExecuteReportBusinessLogic()
        {
            throw new NotImplementedException();
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

        public Task GetReportDatabaseObjectsAsync(IDbDataAccess rvtAccess)
        {
            throw new NotImplementedException();
        }

        public Task GetReportRevitObjectsAsync(IRvtDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }

        public string GetReportScoreAsString()
        {
            //Cast results property to a valid list
            var results = ResultObjects?.OfType<IReportProjectBasePoint>() ?? null;
            if (results is null) { return string.Empty; }

            //Get all bool properties
            PropertyInfo[] boolProperties = typeof(IReportProjectBasePoint).GetProperties()
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
            var _resultObjects = new List<IReportProjectBasePoint>();

            // Initialize expected objects data type
            var _expectedLocation = DbDataObjects?.OfType<IExpectedSiteLocation>()
                ?.FirstOrDefault(x => x.ModelGuid.Equals(ReportDocument.Guid.ToString()));
            if (_expectedLocation is null) { ResultObjects = _resultObjects; return; }

            //Perform Report Business Logic
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

            //Assign Report Results
            _resultObjects.Add(report);
            ResultObjects = _resultObjects;
        }

        public Task SaveReportResultsDataAsync(IDbDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }

        public Task SaveReportScoreDataAsync(IDbDataAccess dbAccess)
        {
            throw new NotImplementedException();
        }
    }
}
