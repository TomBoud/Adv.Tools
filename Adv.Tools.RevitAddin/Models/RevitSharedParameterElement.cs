using Adv.Tools.Abstractions.Revit;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitSharedParameterElement : ISharedParameterElement
    {
        private readonly SharedParameterElement _parameter;

        public RevitSharedParameterElement(SharedParameterElement parameter)
        {
            _parameter = parameter;
        }

        public string Name { get => _parameter.Name; set => Name = value; }    
        public Guid GuidValue { get => _parameter.GuidValue; set => GuidValue = value; }

    }
}
