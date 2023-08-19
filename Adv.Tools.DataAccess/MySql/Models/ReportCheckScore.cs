
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ReportCheckScore : IReportCheckScore
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string CheckName { get; set; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
    }
}
