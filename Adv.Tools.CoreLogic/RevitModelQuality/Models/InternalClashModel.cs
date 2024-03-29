﻿
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class InternalClashModel : IReportInternalClash
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SourceLevelName { get; set; }
        public string SourceCategory { get; set; }
        public string SourceElementName { get; set; }
        public string SourceElementId { get; set; }
        public string ClashCategory { get; set; }
        public string ClashLevelName { get; set; }
        public string ClashElementName { get; set; }
        public string ClashElementId { get; set; }
    }
}
