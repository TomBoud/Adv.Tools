﻿using System;
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

        // Private fields
        private string _title;
        private Guid _guid;
        private Guid _projectId;
        private string _folderId;
        private string _hubId;
        private string _dbProjectId;
        private double _eastWest;
        private double _northSouth;
        private double _elevation;
        private double _angle;
        private double _latitude;
        private double _longitude;
        private string _name;
        private string _number;
        private string _status;
        private string _address;
        private string _author;
        private string _organizationName;
        private string _organizationDescription;
        private string _clientName;
        private string _buildingName;

        public RevitDocument() { }

        public RevitDocument(Document document)
        {
            _document = document;
            _title = document.Title;
            _guid = document.GetCloudModelPath().GetModelGUID();
            _projectId = document.GetCloudModelPath().GetProjectGUID();
            _dbProjectId = Regex.Replace(document.GetProjectId(), "[^a-zA-Z0-9_]", "");
            _folderId = document.GetCloudFolderId(true);
            _hubId = document.GetHubId();
            _eastWest = document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).EastWest;
            _northSouth = document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).NorthSouth;
            _elevation = document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Elevation;
            _angle = document.ActiveProjectLocation.GetProjectPosition(XYZ.Zero).Angle;
            _latitude = document.SiteLocation.Latitude;
            _longitude = document.SiteLocation.Longitude;
            _name = document.ProjectInformation.Name;
            _number = document.ProjectInformation.Number;
            _status = document.ProjectInformation.Status;
            _address = document.ProjectInformation.Address;
            _author = document.ProjectInformation.Author;
            _organizationName = document.ProjectInformation.OrganizationName;
            _organizationDescription = document.ProjectInformation.OrganizationDescription;
            _clientName = document.ProjectInformation.ClientName;
            _buildingName = document.ProjectInformation.BuildingName;
        }

        // Public properties
        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public Guid Guid
        {
            get => _guid;
            set => _guid = value;
        }

        public Guid ProjectId
        {
            get => _projectId;
            set => _projectId = value;
        }
        public string FolderId 
        {
            get => _folderId;
            set => _folderId = value;
        }
        public string HubId 
        {
            get => _hubId;
            set => _hubId = value;
        }

        public string DbProjectId
        {
            get => _dbProjectId;
            set => _dbProjectId = value;
        }

        public bool IsActiveModel
        {
            get;
            set;
        }

        public double EastWest
        {
            get => _eastWest;
            set => _eastWest = value;
        }

        public double NorthSouth
        {
            get => _northSouth;
            set => _northSouth = value;
        }

        public double Elevation
        {
            get => _elevation;
            set => _elevation = value;
        }

        public double Angle
        {
            get => _angle;
            set => _angle = value;
        }

        public double Latitude
        {
            get => _latitude;
            set => _latitude = value;
        }

        public double Longitude
        {
            get => _longitude;
            set => _longitude = value;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Number
        {
            get => _number;
            set => _number = value;
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public string Address
        {
            get => _address;
            set => _address = value;
        }

        public string Author
        {
            get => _author;
            set => _author = value;
        }

        public string OrganizationName
        {
            get => _organizationName;
            set => _organizationName = value;
        }

        public string OrganizationDescription
        {
            get => _organizationDescription;
            set => _organizationDescription = value;
        }

        public string ClientName
        {
            get => _clientName;
            set => _clientName = value;
        }

        public string BuildingName
        {
            get => _buildingName;
            set => _buildingName = value;
        }

    }
}
