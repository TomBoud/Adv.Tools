using Adv.Tools.Abstractions.Revit;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitWorkset : IWorkset
    {
        private readonly Workset _workset;

        public RevitWorkset(Workset workset)
        {
            _workset = workset;
        }

        #region Properties
        public string Name { get { return _workset.Name; } set { Name = value; } }
        public int Id { get { return _workset.Id.IntegerValue; } set { Id = value; } }
        public Guid Guid { get { return _workset.UniqueId; } set { Guid = value; } }
        #endregion

    }
}
