using Dapper;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql
{
    public class IMySqlDataAccess
    {

        public async Task<List<T>> LoadData<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlQuery, parameters);
                return rows.ToList();
            }
        }

        public async Task SaveData<T>(string sqlQuery, T parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
    }
}