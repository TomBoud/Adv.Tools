using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigSharedParamsRepo
    {
        void Add(ExpectedSharedPara model);
        void Edit(ExpectedSharedPara model);
        void Delete(int id);
        IEnumerable<ExpectedSharedPara> GetAllWorksets();
        IEnumerable<ExpectedSharedPara> GetByValue(string value);
    }
}
