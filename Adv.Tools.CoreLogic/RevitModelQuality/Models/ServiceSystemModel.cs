﻿using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.CoreLogic.RevitModelQuality.Models
{
    public class ServiceSystemModel : IReportServiceSystem
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string LevelName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string IsValueAcceptable { get; set; }
    }
}