using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigCleanViewsRepo
    {
        void Add(ExpectedCleanView model);
        void Edit(ExpectedCleanView model);
        void Delete(int id);
        IEnumerable<ExpectedCleanView> GetAllViewData();
        IEnumerable<ExpectedCleanView> GetByValue(string value);
    }
}
