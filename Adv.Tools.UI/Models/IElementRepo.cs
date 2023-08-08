using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.Models
{
    public interface IElementRepo
    {
        void Add(ElementModel elementModel);
        void Delete(ElementModel elementModel);
        IEnumerable<ElementModel> GetAll();
        IEnumerable<ElementModel> GetByValue(string value);
    }
}
