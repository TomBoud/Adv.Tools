using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions.Common
{
    public interface IDbDataAccess
    {
        Task<List<T>> LoadDataAsync<T, U>(string sqlQuery, U parameters);
        Task SaveData<T>(string sqlQuery, T parameters);
        Task<List<T>> LoadDataSelectAllAsync<T>(string databaseName);
        List<T> LoadDataSelectAll<T>(string databaseName);

        Task DeleteData<T, U>(string sqlQuery, U parameters);
        Task ExecuteWithTransaction(params Func<Task>[] tasks);

        Task SaveByInsertUpdateOnDuplicateKeysAsync<T>(List<T> data, T keyProperties);
    }
}
