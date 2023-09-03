using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigProjectInfoRepo
    {
        void Add(ExpectedProjectInfo model);
        void Edit(ExpectedProjectInfo model);
        void Delete(int id);
        IEnumerable<ExpectedProjectInfo> GetAllWorksets();
        IEnumerable<ExpectedProjectInfo> GetByValue(string value);
    }
}
