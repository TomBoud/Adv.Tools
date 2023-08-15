using Adv.Tools.CoreLogic.RevitModelQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityUserData
    {
        private readonly IReportModelQuality _report;
        
        public ModelQualityUserData(IReportModelQuality report)
        {
            _report = report;
        }


    }
}
