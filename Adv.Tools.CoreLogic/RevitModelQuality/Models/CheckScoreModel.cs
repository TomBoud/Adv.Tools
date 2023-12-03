using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class CheckScoreModel : IReportCheckScore
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string CheckName { get; set; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
        public string CheckStatus { get; set; }
    }
}
