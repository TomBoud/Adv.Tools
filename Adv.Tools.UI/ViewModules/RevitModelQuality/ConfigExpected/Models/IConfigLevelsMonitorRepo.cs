using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigLevelsMonitorRepo
    {
        void Add(ConfigLevelMonitorModel model);
        void Edit(ConfigLevelMonitorModel model);
        void Delete(int id);
        IEnumerable<ConfigLevelMonitorModel> GetAllWorksets();
        IEnumerable<ConfigLevelMonitorModel> GetByValue(string value);
    }
}
