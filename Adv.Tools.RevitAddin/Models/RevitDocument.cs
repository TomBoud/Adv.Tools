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
    public class RevitDocument : IDocument
    {
        private readonly Document _document;

        public RevitDocument(Document document)
        {
            _document = document;
        }

        #region Public Properties
        //Document Identity
        public string Title { get => _document.Title; set => Title = value; }
        public Guid  Guid { get => _document.GetCloudModelPath().GetModelGUID(); set => Guid = value; }
        public Guid ProjectGuid { get => _document.GetCloudModelPath().GetProjectGUID(); set => ProjectGuid = value; }
        public string DbProjectId { get => GetDocumentProjectGuidAsValidDbName(); set => DbProjectId = value; }

        //Project Position
        public double EastWest { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).EastWest; set => EastWest = value; }
        public double NorthSouth { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).NorthSouth; set => NorthSouth = value; }
        public double Elevation { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Elevation; set => Elevation = value; }
        public double Angle { get => _document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Angle; set => Angle = value; }

        //Site Location
        public double Latitude { get => _document.SiteLocation.Latitude; set => Latitude = value; }
        public double Longitude { get => _document.SiteLocation.Longitude; set => Longitude = value; }
        #endregion

        //Project Information
        public string Name { get => _document.ProjectInformation.Name; set=> Name=value; }
        public string Number { get => _document.ProjectInformation.Number; set => Number = value; }
        public string Status {  get=> _document.ProjectInformation.Status; set => Status = value; }
        public string Address { get => _document.ProjectInformation.Address; set => Address = value; }
        public string Author { get => _document.ProjectInformation.Author; set => Author = value; }
        public string OrganizationName { get => _document.ProjectInformation.OrganizationName; set => OrganizationName = value; }
        public string OrganizationDescription { get => _document.ProjectInformation.OrganizationDescription; set => OrganizationDescription = value; }      
        public string ClientName { get => _document.ProjectInformation.ClientName; set => ClientName = value; }
        public string BuildingName { get => _document.ProjectInformation.BuildingName; set => BuildingName = value; }


        #region Public Properties
        private string GetDocumentProjectGuidAsValidDbName()
        {
            var ProjectGuid = _document.GetCloudModelPath().GetProjectGUID();
            return Regex.Replace(ProjectGuid.ToString(), "[^a-zA-Z0-9_]", "");
        }
        #endregion
    }
}
