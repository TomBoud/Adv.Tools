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

        //Documnet Identity
        public string Title { get => _document.Title; set => Title = value; }
        public Guid  Guid { get => _document.GetCloudModelPath().GetModelGUID(); set => Guid = value; }
        public string ProjectId { get => GetDocumentProjectGuidAsValidDbName(); set => ProjectId = value; }

        //Project Position
        public double EastWest { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).EastWest; set => EastWest = value; }
        public double NorthSouth { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).NorthSouth; set => NorthSouth = value; }
        public double Elevation { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Elevation; set => Elevation = value; }
        public double Angle { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Angle; set => Angle = value; }

        //Site Location
        public double Latitude { get => _document.SiteLocation.Latitude; set => Latitude = value; }
        public double Longitude { get => _document.SiteLocation.Longitude; set => Longitude = value; }
        #endregion

        #region Public Properties
        private string GetDocumentProjectGuidAsValidDbName()
        {
            var ProjectGuid = _document.GetCloudModelPath().GetProjectGUID();
            return Regex.Replace(ProjectGuid.ToString(), "[^a-zA-Z0-9_]", "");
        }
        #endregion
    }
}
