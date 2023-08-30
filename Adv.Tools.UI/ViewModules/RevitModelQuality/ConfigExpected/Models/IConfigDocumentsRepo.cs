using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigDocumentsRepo
    {
        void Add(ExpectedDocument model);
        void Edit(ExpectedDocument model);
        void Delete(int id);
        IEnumerable<ExpectedDocument> GetAllDocuments();
        IEnumerable<ExpectedDocument> GetByValue(string value);
    }
}
