﻿using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ModelPlaceModel : IReportModelPlace
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string ObjectId { get; set; }
        public string Discipline { get; set; }
    }
}
