using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedDocument
    {
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string HubId { get; set; }
        string ProjectId { get; set; }
        string FolderId { get; set; }
        string Disicpline { get; set; }
        string PositionSource { get; set; }
        string PositionSourceGuid { get; set; }
    }
}
