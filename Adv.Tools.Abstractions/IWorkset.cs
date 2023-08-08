using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions
{
    public interface IWorkset
    {
        string Name { get; set; }
        int Id { get; set; }
        Guid Guid { get; set; }
    }
}
