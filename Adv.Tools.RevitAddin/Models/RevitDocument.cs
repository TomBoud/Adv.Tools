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
        public string MySqlDb { get => GetDocumentGuidAsValidDbName(); set => MySqlDb = value; }
        #endregion

        #region Public Properties
        private string GetDocumentGuidAsValidDbName()
        {
            string modelGuid = _document.GetCloudModelPath().GetModelGUID().ToString();
            string cleanName = Regex.Replace(modelGuid, "[^a-zA-Z0-9_]", "");
            return cleanName;
        }
        #endregion
    }
}
