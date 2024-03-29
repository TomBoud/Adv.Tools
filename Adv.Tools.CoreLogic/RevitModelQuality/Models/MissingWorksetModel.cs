﻿
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class MissingWorksetModel : IReportMissingWorkset
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string WorksetName { get; set; }
        public string ObjectId { get; set; }
        public bool IsFound { get; set; }
        public string IsFoundHeb { get; set; }
        public string Discipline { get; set; }
    }
}
