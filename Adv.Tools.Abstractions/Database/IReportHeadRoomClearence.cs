using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportHeadRoomClearence
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string ObjectName { get; set; }
        string ObjectFamily { get; set; }
        string ObjectLevel { get; set; }
        string ObjectId { get; set; }
    }
}
