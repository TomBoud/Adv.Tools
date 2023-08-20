using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IDocumnet
    {
        string Title { get; set; }
        Guid Guid { get; set; }
        string ProjectId { get; set; }

        //Project Position
        double EastWest { get; set; }
        double NorthSouth { get; set; }
        double Elevation { get; set; }
        double Angle { get; set; }

        //Site Location
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
