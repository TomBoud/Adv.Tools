﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.DbEntities
{
    public interface IExpectedProjectInfo 
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string Parameter { get; set; }
        string Value { get; set; }
    }
}
