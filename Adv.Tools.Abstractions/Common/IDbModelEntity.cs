using System;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Common
{
    public interface IDbModelEntity
    {
        int Id { get; set; }
        string GetCreateTableQuery(string databaseName);
    }
}
