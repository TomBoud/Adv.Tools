using Adv.Tools.UI.Example.Models;
using Adv.Tools.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.Example.Repositories
{
    /// <summary>
    /// This class represenets the implementaion of <see cref="IElementRepo"/> for this UI operations.
    /// The objects can be in-memory or in a database which will require SQL operations here.
    /// </summary>
    public class ElementRepo : IElementRepo
    {
        public static List<ElementModel> ElementDataRepo { get; set; }

        public void Add(ElementModel elementModel)
        {
            ElementDataRepo.Add(elementModel);
        }

        public void Delete(ElementModel elementModel)
        {
            ElementDataRepo.Remove(elementModel);
        }

        public IEnumerable<ElementModel> GetAll()
        {
            return ElementDataRepo.AsEnumerable();
        }

        public IEnumerable<ElementModel> GetByValue(string value)
        {
            var result = new List<ElementModel>();

            result.AddRange(ElementDataRepo.Where(x => x.Id.ToString().Contains(value)).ToArray());
            result.AddRange(ElementDataRepo.Where(x => x.Name.Contains(value)).ToArray());
            result.AddRange(ElementDataRepo.Where(x => x.LevelName.Contains(value)).ToArray());
            result.AddRange(ElementDataRepo.Where(x => x.CategoryName.Contains(value)).ToArray());

            return result.AsEnumerable();
        }
    }
}
