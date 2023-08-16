using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Adv.Tools.Abstractions.Revit;
using System.Text.RegularExpressions;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitDocument : IDocumnet
    {
        private readonly Document _document;

        public RevitDocument(Document document)
        {
            _document = document;
        }

        #region Public Properties
        public string Title { get { return _document.Title; } set { Title = value; } }
        public Guid  Guid { get { return _document.GetCloudModelPath().GetModelGUID(); } set { Guid = value; } }
        public string ProjectId { get => GetDocumentGuidAsValidDbName(); set => ProjectId = value; }
        #endregion

        #region Public Properties
        private string GetDocumentGuidAsValidDbName()
        {
            string ProjectGuid = _document.GetCloudModelPath().GetProjectGUID().ToString();
            string ProjectId = Regex.Replace(ProjectGuid, "[^a-zA-Z0-9_]", "");
            return ProjectId;
        }
        #endregion
    }
}
