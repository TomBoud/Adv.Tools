using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess
{
    public interface IDbDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters);
        Task SaveData<T>(string sqlQuery, T parameters);
        Task DeleteData<T, U>(string sqlQuery, U parameters);
        Task ExecuteWithTransaction(params Func<Task>[] tasks);
    }
}
