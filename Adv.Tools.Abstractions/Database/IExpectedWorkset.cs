using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedWorkset
    {
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string WorksetName { get; set; }
        string CategoryName { get; set; }
        string CategoryId { get; set; }
    }
}
