using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        bool IsMonitoring { get; set; }
        long MonitoredId { get; set; }
        IDocument MonitoredDoc { get; set; }
    }
}
