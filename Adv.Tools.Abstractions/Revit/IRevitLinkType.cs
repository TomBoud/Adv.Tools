using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IRevitLinkType
    {
        string LinkedFileStatus { get; set; }
        string AttachmentType { get; set; }
        string FileName { get; set; }
        Guid FileGuid { get; set; }
    }
}
