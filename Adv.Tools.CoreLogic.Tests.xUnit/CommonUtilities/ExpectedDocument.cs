using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.Tests.xUnit.CommonUtilities
{
    public class ExpectedDocument : IExpectedDocument
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string HubId { get; set; }
        public string ProjectId { get; set; }
        public string FolderId { get; set; }
        public string Discipline { get; set; }
        public string PositionSource { get; set; }

        public ExpectedDocument(int seed)
        {
            // Use the seed value to generate values for properties
            Id = seed;
            ModelName = $"Model-{seed}";
            ModelGuid = GenerateGuidFromSeed(seed).ToString();
            HubId = $"Hub-{seed}";
            ProjectId = $"Project-{seed}";
            FolderId = $"Folder-{seed}";
            Discipline = $"Discipline-{seed}";
            PositionSource = $"PositionSource-{seed}";
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
