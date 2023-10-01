using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IExpectedDocument
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string HubId { get; set; }
        string ProjectId { get; set; }
        string FolderId { get; set; }
        string Discipline { get; set; }
        string PositionSource { get; set; }
    }
}
