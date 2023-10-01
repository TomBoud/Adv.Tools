using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class HeadClearanceModel : IReportHeadRoomClearance
    {
        public int Id {get; set;}
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }       
        public string Discipline { get; set; }
        public string ObjectName { get; set; }
        public string ObjectFamily { get; set; }
        public string ObjectLevel { get; set; }
        public string ObjectId { get; set; }
    }
}
