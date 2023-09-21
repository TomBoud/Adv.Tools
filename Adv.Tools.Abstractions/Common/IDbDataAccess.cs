using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.Abstractions.Common
{
    public interface IDbDataAccess
    {
        //Save
        Task SaveByInsertValuesAsync<T>(string databaseName, List<T> data);
        Task SaveByUpdateValuesAsync<T>(string databaseName, List<T> data);

        //Load
        Task<List<T>> LoadDataSelectAllAsync<T>(string databaseName);
        List<T> LoadDataSelectAll<T>(string databaseName);
        
        //Delete
        Task DeleteAllTableDataAsync<T>(string databaseName);
        Task DeleteDataByIdAsync<T>(string databaseName, int Id);
        Task DeleteDataByParametersAsync<T>(string databaseName, T Parameters);
        
        //Execute
        Task ExecuteWithTransaction(params Func<Task>[] tasks);
        Task SaveByInsertUpdateOnDuplicateKeysAsync<T>(string databaseName, List<T> data);
        Task ExecuteBuildMySqlDataBase(string databaseName);
    }
}
