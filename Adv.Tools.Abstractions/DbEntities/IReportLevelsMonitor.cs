﻿namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportLevelsMonitor
    {
        string Discipline { get; set; }
        int Id { get; set; }
        bool IsCopyMonitor { get; set; }
        string IsCopyMonitorHeb { get; set; }
        bool IsOriginValid { get; set; }
        string IsOriginValidHeb { get; set; }
        string ModelGuid { get; set; }
        string ModelName { get; set; }
        string ObjectId { get; set; }
        string ObjectName { get; set; }
        string ObjectOrigin { get; set; }
    }
}