
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigWorksetsRepo
    {
        void Add(ExpectedWorkset model);
        void Edit(ExpectedWorkset model);
        void Delete(int id);
        IEnumerable<ExpectedWorkset> GetAllWorksets();
        IEnumerable<ExpectedWorkset> GetByValue(string value);
    }
}
