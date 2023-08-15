using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.DataAccess.MySql.Models;
using Adv.Tools.DataAccess.MySql.Procedures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityUserData
    {
        public IEnumerable ExpectedObjects { get; set; }

        private readonly IReportModelQuality _report;
        
        public ModelQualityUserData(IReportModelQuality report)
        {
            _report = report;
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
            var access = new MySqlDataAccess("ConnectionString");
            var expected = new ExpectedWorksetData(access, _report.ReportDocumnet.MySqlDb, nameof(ExpectedWorkset));

            return expected.SelectAllAsync().Result;
        }

    }
}
