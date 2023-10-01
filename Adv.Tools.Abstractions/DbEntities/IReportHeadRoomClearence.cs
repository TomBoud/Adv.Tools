using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IReportHeadRoomClearance
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string ObjectName { get; set; }
        string ObjectFamily { get; set; }
        string ObjectLevel { get; set; }
        string ObjectId { get; set; }
    }
}
