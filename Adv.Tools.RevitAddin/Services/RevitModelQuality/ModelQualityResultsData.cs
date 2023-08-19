using Adv.Tools.CoreLogic.RevitModelQuality;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityResultsData
    {
        public IEnumerable RersultObjects { get; set; }

        private readonly IReportModelQuality _report;
        private readonly IDbDataAccess _dbAccess;

        public ModelQualityResultsData(IReportModelQuality report, IDbDataAccess access)
        {
            _report = report;
            _dbAccess = access;
        }

        public void SaveResultsToDatabase()
        {
            if (_report is ElementsWorksetsReport)
            {
                SaveElementsWorksetsResults();
            }
            if (_report is MissingWorksetsReport)
            {
                SaveMissingWorksetsResults();
            }
        }

        private void SaveElementsWorksetsResults()
        {
            
        }

        private void SaveMissingWorksetsResults()
        {

        }

    }
}
