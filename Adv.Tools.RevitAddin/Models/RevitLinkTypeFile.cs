
using System;
using Autodesk.Revit.DB;
using Adv.Tools.Abstractions.Revit;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitLinkTypeFile : IRevitLinkType
    {
        private readonly RevitLinkType _revitLinkTypeFile;

        public RevitLinkTypeFile(RevitLinkType revitLinkTypeFile)
        {
            _revitLinkTypeFile = revitLinkTypeFile;
        }

        public string LinkedFileStatus { get => _revitLinkTypeFile.GetExternalFileReference().GetLinkedFileStatus().ToString(); set => LinkedFileStatus = value; }
        public string AttachmentType { get => _revitLinkTypeFile.AttachmentType.ToString(); set => AttachmentType = value; }
        public string FileName { get => _revitLinkTypeFile.Document.Title; set => FileName = value; }
        public Guid FileGuid { get => _revitLinkTypeFile.Document.GetCloudModelPath().GetModelGUID(); set => FileGuid = value; }
    }
}
