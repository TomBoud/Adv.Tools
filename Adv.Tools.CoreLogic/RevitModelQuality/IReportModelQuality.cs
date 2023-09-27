using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;

namespace Adv.Tools.CoreLogic.RevitModelQuality
{
    public interface IReportModelQuality
    {
        string ReportName { get; set; }
        LodType Lod { get; set; }
        IEnumerable ResultObjects { get; set; }
        IEnumerable RvtDataObjects { get; set; }
        IEnumerable DbDataObjects { get; set; }
        IEnumerable DocumentObjects { get; set; }
        IDocument ReportDocument { get; set; }

        string GetReportScoreAsString();
        void RunReportBusinessLogic();

        Task ExecuteReportBusinessLogic();
        Task GetReportRevitObjectsAsync(IRvtDataAccess dbAccess);
        Task GetReportDatabaseObjectsAsync(IDbDataAccess rvtAccess);
        Task SaveReportResultsDataAsync(IDbDataAccess dbAccess);
        Task SaveReportScoreDataAsync(IDbDataAccess dbAccess);
    }
}
