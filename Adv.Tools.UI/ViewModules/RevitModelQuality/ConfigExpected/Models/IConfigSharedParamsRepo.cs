using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigSharedParamsRepo
    {
        void Add(ConfigSharedParamModel model);
        void Edit(ConfigSharedParamModel model);
        void Delete(int id);
        IEnumerable<ConfigSharedParamModel> GetAllWorksets();
        IEnumerable<ConfigSharedParamModel> GetByValue(string value);
    }
}
