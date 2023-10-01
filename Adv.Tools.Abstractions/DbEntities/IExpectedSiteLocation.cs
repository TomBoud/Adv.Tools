using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IExpectedSiteLocation
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string EastWest { get; set; }
        string NorthSouth { get; set; }
        string Elevation { get; set; }
        string Angle { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
    }
}
