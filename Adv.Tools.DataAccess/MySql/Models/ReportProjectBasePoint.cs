using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportProjectBasePoint : IReportProjectBasePoint
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LinkedEastWest { get; set; }
        public string LinkedNorthSouth { get; set; }
        public string LinkedElevation { get; set; }
        public string LinkedLatitude { get; set; }
        public string LinkedLongitude { get; set; }
        public string LinkedAngle { get; set; }
        public string ExpectedEastWest { get; set; }
        public string ExpectedNorthSouth { get; set; }
        public string ExpectedElevation { get; set; }
        public string ExpectedLatitude { get; set; }
        public string ExpectedLongitude { get; set; }
        public string ExpectedAngle { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsLocation { get; set; }
        public bool IsBasePoint { get; set; }
        public string IsCorrectHeb { get; set; }
        public string IsLocationHeb { get; set; }
        public string IsBasePointHeb { get; set; }
    }
}
