using Adv.Tools.Abstractions.Revit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.CoreLogic.Tests.xUnit.CommonUtilities
{
    public class RevitLinkType : IRevitLinkType
    {
        public string LinkedFileStatus { get; set; }
        public string AttachmentType { get; set; }
        public string FileName { get; set; }
        public Guid FileGuid { get; set; }

        public RevitLinkType(int seed)
        {
            LinkedFileStatus = $"LinkedFileStatus-{seed}";
            AttachmentType = $"AttachmentType-{seed}";
            FileName = $"FileName-{seed}";
            FileGuid = GenerateGuidFromSeed(seed);
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
