using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions.Revit
{
    public interface IDocumnet
    {
        string Title { get; set; }
        Guid Guid { get; set; }
        string ProjectId { get; set; }
    }
}
