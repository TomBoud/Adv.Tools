using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigProjectInfoRepo
    {
        void Add(ConfigProjectInfoModel model);
        void Edit(ConfigProjectInfoModel model);
        void Delete(int id);
        IEnumerable<ConfigProjectInfoModel> GetAllWorksets();
        IEnumerable<ConfigProjectInfoModel> GetByValue(string value);
    }
}
