using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigDocumentsRepo
    {
        void Add(ConfigDocumentModel model);
        void Edit(ConfigDocumentModel model);
        void Delete(int id);
        IEnumerable<ConfigDocumentModel> GetAllWorksets();
        IEnumerable<ConfigDocumentModel> GetByValue(string value);
    }
}
