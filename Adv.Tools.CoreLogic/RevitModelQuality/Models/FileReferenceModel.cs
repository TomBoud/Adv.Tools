﻿using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class FileReferenceModel : IReportFileReference
    {
        public int Id { get; set; }
        public string ModelName { get; set; }   
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string LinkName { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
        public bool IsReffOk { get; set; }
        public string IsReffOkHeb { get; set; }
        public bool IsStatusOk { get; set; }
        public string IsStatusOkHeb { get; set; }
    }
}
