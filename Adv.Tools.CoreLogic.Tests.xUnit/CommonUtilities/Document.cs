using Adv.Tools.Abstractions.Revit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.Tests.xUnit.CommonUtilities
{
    public class Document : IDocument
    {
        public string Title { get; set; }
        public Guid Guid { get; set; }
        public Guid ProjectId { get; set; }
        public string FolderId { get; set; }
        public string HubId { get; set; }
        public string DbProjectId { get; set; }
        public bool IsActiveModel { get; set; }
        public double EastWest { get; set; }
        public double NorthSouth { get; set; }
        public double Elevation { get; set; }
        public double Angle { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public string Author { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationDescription { get; set; }
        public string ClientName { get; set; }
        public string BuildingName { get; set; }

        public Document(int seed)
        {
            // Populate properties based on the seed value
            Title = $"Title-{seed}";
            Guid = GenerateGuidFromSeed(seed);
            ProjectId = GenerateGuidFromSeed(seed + 1); // You can modify this logic as needed
            FolderId = $"FolderId-{seed}";
            HubId = $"HubId-{seed}";
            DbProjectId = $"DbProjectId-{seed}";
            IsActiveModel = seed % 2 == 0; // Example: set IsActiveModel based on seed
            EastWest = seed * 0.1; // Example: set EastWest based on seed
            NorthSouth = seed * 0.2; // Example: set NorthSouth based on seed
            Elevation = seed * 0.3; // Example: set Elevation based on seed
            Angle = seed * 0.4; // Example: set Angle based on seed
            Latitude = seed * 0.5; // Example: set Latitude based on seed
            Longitude = seed * 0.6; // Example: set Longitude based on seed
            Name = $"Name-{seed}";
            Number = $"Number-{seed}";
            Status = $"Status-{seed}";
            Address = $"Address-{seed}";
            Author = $"Author-{seed}";
            OrganizationName = $"OrganizationName-{seed}";
            OrganizationDescription = $"OrganizationDescription-{seed}";
            ClientName = $"ClientName-{seed}";
            BuildingName = $"BuildingName-{seed}";
        }

        private Guid GenerateGuidFromSeed(int seed)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] seedBytes = BitConverter.GetBytes(seed);
                byte[] hashBytes = md5.ComputeHash(seedBytes);
                return new Guid(hashBytes);
            }
        }

    }
}
