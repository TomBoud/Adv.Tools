using System;
using System.Collections;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;

namespace Adv.Tools.Abstractions.Common
{
    public interface IReportModelQuality
    {
        ReportType ReportName { get; }
        LodType Lod { get; }
        IDocument ReportDocument { get; set; }
        IEnumerable ResultObjects { get; set; }
        IEnumerable RvtDataObjects { get; set; }
        IEnumerable DbDataObjects { get; set; }
        IEnumerable DocumentObjects { get; set; }
        
        Task ExecuteReportCoreLogicAsync();
        Task GetReportRevitObjectsAsync(IRvtDataAccess dbAccess);
        Task GetReportDatabaseObjectsAsync(IDbDataAccess rvtAccess);
        Task SaveReportResultsDataAsync(IDbDataAccess dbAccess);
        Task SaveReportScoreDataAsync(IDbDataAccess dbAccess);
    }
}
