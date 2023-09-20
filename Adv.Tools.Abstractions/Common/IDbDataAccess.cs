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
        Task SaveByInsertValuesAsync<T>(string databaseName, List<T> data);
        Task SaveByUpdateValuesAsync<T>(string databaseName, List<T> data);
        Task<List<T>> LoadDataSelectAllAsync<T>(string databaseName);
        List<T> LoadDataSelectAll<T>(string databaseName);
        Task DeleteData<T, U>(string databaseName, U parameters);
        Task DeleteAllTableDataAsync<T>(string databaseName);
        Task DeleteDataById<T>(string databaseName, int Id);
        Task ExecuteWithTransaction(params Func<Task>[] tasks);
        Task SaveByInsertUpdateOnDuplicateKeysAsync<T>(string databaseName, List<T> data);
        Task ExecuteBuildMySqlDataBase(string databaseName);
    }
}
