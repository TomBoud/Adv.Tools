using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigGridsMonitorRepo
    {
        void Add(ConfigGridMonitorModel model);
        void Edit(ConfigGridMonitorModel model);
        void Delete(int id);
        IEnumerable<ConfigGridMonitorModel> GetAllWorksets();
        IEnumerable<ConfigGridMonitorModel> GetByValue(string value);
    }
}
