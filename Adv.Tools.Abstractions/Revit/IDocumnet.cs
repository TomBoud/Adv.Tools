using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IDocument
    {
        //Project Identity
        string Title { get; set; }
        Guid Guid { get; set; }
        Guid ProjectGuid { get; set; }
        string DbProjectId { get; set; }

        //Project Position
        double EastWest { get; set; }
        double NorthSouth { get; set; }
        double Elevation { get; set; }
        double Angle { get; set; }

        //Site Location
        double Latitude { get; set; }
        double Longitude { get; set; }

        //Project Information
        string Name { get; set; }
        string Number { get; set; }
        string Status { get; set; }
        string Address { get; set; }
        string Author { get; set; }
        string OrganizationName { get; set; }
        string OrganizationDescription { get; set; }
        string ClientName { get; set; }
        string BuildingName { get; set; }
    }
}
