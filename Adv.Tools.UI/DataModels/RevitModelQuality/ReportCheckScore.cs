﻿using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.DataModels.RevitModelQuality
{
    public class ReportCheckScore : IReportCheckScore
    {
        public int Id {get; set;}
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string CheckName { get; set; }
        public string CheckLod { get; set; }
        public string CheckScore { get; set; }
    }
}
