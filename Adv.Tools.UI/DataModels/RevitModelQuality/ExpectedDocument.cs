using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.DataModels.RevitModelQuality
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
    }
}
