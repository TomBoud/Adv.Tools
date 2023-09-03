
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigGridMonitorRepo
    {
        void Add(ExpectedGridMonitor model);
        void Edit(ExpectedGridMonitor model);
        void Delete(int id);
        IEnumerable<ExpectedGridMonitor> GetAllViewData();
        IEnumerable<ExpectedGridMonitor> GetByValue(string value);
        IEnumerable<ExpectedDocument> GetDocumentsData();
    }
}
