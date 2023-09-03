using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigCleanRepo
    {
        void Add(ExpectedCleanView model);
        void Edit(ExpectedCleanView model);
        void Delete(int id);
        void DeletAllViewData();
        IEnumerable<ExpectedCleanView> GetAllViewData();
        IEnumerable<ExpectedCleanView> GetByValue(string value);
    }
}
