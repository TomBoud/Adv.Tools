﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IExpectedLevelsMonitor
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string SourceModelName { get; set; }
        string SourceModelGuid { get; set; }
    }
}
