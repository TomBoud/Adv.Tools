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
        void DeletAllViewData();
        IEnumerable<ExpectedProjectInfo> GetAllViewData();
        IEnumerable<ExpectedProjectInfo> GetByValue(string value);
        IEnumerable<ExpectedDocument> GetDocumentsData();

    }
}
