﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IReportMissingWorkset
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string WorksetName { get; set; }
        string ObjectId { get; set; }
        bool IsFound { get; set; }
        bool IsFoundHeb { get; set; }
    }
}
