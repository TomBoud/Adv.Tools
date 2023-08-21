﻿using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ProjectInfoModel : IReportProjectInfo
    {
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string InfoName { get; set; }
        public string InfoValue { get; set; }
        public string ExpectedValue { get; set; }
        public bool IsCorrect { get; set; }
        public string IsCorrectHeb { get; set; }
    }
}