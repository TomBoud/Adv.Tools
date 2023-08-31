using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigGridsMonitorRepo
    {
        void Add(ExpectedGridsMonitor model);
        void Edit(ExpectedGridsMonitor model);
        void Delete(int id);
        IEnumerable<ExpectedGridsMonitor> GetAllViewData();
        IEnumerable<ExpectedGridsMonitor> GetByValue(string value);
    }
}
