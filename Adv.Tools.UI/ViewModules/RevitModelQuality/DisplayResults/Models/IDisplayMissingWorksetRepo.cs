using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models
{
    public interface IDisplayMissingWorksetRepo
    {
        IEnumerable<ReportMissingWorkset> GetAllViewData();
        IEnumerable<ReportMissingWorkset> GetByValue(string value);
        void DeleteAllViewData();
    }
}
