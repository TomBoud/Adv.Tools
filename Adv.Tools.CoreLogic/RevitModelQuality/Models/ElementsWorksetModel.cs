﻿
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ElementsWorksetModel : IReportElementsWorkset
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCategory { get; set; }  
        public string ObjectId { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ObjectFamily { get; set; }
        public string ObjectWorkset { get; set; }
    }
}
