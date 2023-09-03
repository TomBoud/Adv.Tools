using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigLevelMonitorRepo
    {
        void Add(ExpectedLevelMonitor model);
        void Edit(ExpectedLevelMonitor model);
        void Delete(int id);
        IEnumerable<ExpectedLevelMonitor> GetAllViewData();
        IEnumerable<ExpectedLevelMonitor> GetByValue(string value);
        IEnumerable<ExpectedDocument> GetDocumentsData();
    }
}
