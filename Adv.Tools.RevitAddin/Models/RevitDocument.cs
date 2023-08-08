using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Adv.Tools.Abstractions;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitDocument : IDocumnet
    {
        private readonly Document _document;

        public RevitDocument(Document document)
        {
            _document = document;
        }

        #region Properties
        public string Title { get { return _document.Title; } set { Title = value; } }
        public Guid  Guid { get { return _document.GetCloudModelPath().GetModelGUID(); } set { Guid = value; } }
        #endregion
    }
}
