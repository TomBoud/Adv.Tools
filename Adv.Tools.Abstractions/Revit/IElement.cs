﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IElement
    {
        string Name { get; set; }
        string LevelName { get; set; }
        string DocumentName { get; set; }
        string CategoryName { get; set; }
        long CategoryId { get; set; }
        string WorksetName { get; set; }

        long ElementId { get; set; }
        long LevelId { get; set; }
    }
}
