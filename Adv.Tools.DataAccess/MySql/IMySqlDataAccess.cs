using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql
{
    public interface IMySqlDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters);
        Task SaveData<T>(string sqlQuery, T parameters);

    }
}