using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigCleanViewsRepo
    {
        void Add(ConfigCleanViewModel model);
        void Edit(ConfigCleanViewModel model);
        void Delete(int id);
        IEnumerable<ConfigCleanViewModel> GetAllWorksets();
        IEnumerable<ConfigCleanViewModel> GetByValue(string value);
    }
}
