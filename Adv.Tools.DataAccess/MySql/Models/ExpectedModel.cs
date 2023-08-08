﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedModel
    {
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string HubId { get; set; }
        public string ProjectId { get; set; }
        public string FolderId { get; set; }
        public string Disicpline { get; set; }
        public string PositionSource { get; set; }
        public string PositionSourceGuid { get; set; }
    }
}
