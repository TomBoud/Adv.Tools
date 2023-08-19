
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.DataAccess;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.Abstractions;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityUserData
    {
        public IEnumerable ExpectedObjects { get => GetReportDataFromDatabase(); }

        private readonly IReportModelQuality _report;
        private readonly IDbDataAccess _dbAccess;
        
        public ModelQualityUserData(IReportModelQuality report, IDbDataAccess access)
        {
            _report = report;
            _dbAccess = access;
        }

        public IEnumerable GetReportDataFromDatabase()
        {
            if (_report is ElementsWorksetsReport)
            {
                return GetExpectedWorksetsFromMySql();
            }
            if (_report is MissingWorksetsReport)
            {
                return GetExpectedWorksetsFromMySql();
            }

            return null;
        }

        private IEnumerable GetExpectedWorksetsFromMySql() 
        {
            return _dbAccess.LoadDataSelectAll<ExpectedWorkset>(_report.ReportDocumnet.ProjectId, nameof(ExpectedWorkset)).Result;
        }

    }
}
