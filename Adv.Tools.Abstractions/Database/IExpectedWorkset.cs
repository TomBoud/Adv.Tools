using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Database
{
    public interface IExpectedWorkset
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string WorksetName { get; set; }
        string CategoryName { get; set; }
        string CategoryId { get; set; }
    }
}
