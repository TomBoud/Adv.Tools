using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportSpaceData : IReportSpaceData
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SpaceName { get; set; }
        public string SpaceNumber { get; set; }
        public string RoomName { get; set; }
        public string RoomNumber { get; set; }
        public string ObjectLevel { get; set; }
        public string ObjectId { get; set; }
        public string UpperLevel { get; set; }
        public string UpperOffset { get; set; }
        public string BaseOffset { get; set; }
        public string TotalHeight { get; set; }
        public string Area { get; set; }
        public bool IsShaft { get; set; }
        public bool HasDefaultName { get; set; }
        public bool HasName { get; set; }
        public bool HasNumber { get; set; }
    }
}
