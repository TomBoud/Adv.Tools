using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedLevelsGrids
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string Category { get; set; }
        string SourceModelName { get; set; }
        string SourceModelGuid { get; set; }
    }
}
