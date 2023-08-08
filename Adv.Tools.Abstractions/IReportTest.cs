using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Adv.Tools.Abstractions
{
    public interface IReportTest<T,U,V,W>
    {
        string ReportName { get; set; }
        string ModelName { get; set; }
        string Lod { get; set; }
        Guid ModelGuid { get; set; }

        IList<T> ExistingObjects { get; set; }
        IList<U> ExpectedObjects { get; set; }
        IList<V> ResultObjects { get; set; }
        IList<W> ModelObjects { get; set; }

        IList<U> GetExpectedObjects();
        IList<W> GetModelObjects();

        string GetReportScore();
        void RunReportLogic();
        
    }
}
