using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigSharedParamRepo
    {
        void Add(ExpectedSharedPara model);
        void Edit(ExpectedSharedPara model);
        void Delete(int id);
        void DeletAllViewData();
        IEnumerable<ExpectedSharedPara> GetAllViewData();
        IEnumerable<ExpectedSharedPara> GetByValue(string value);
        IEnumerable<ExpectedDocument> GetDocumentsData();
    }
}
