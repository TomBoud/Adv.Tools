using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Adv.Tools.DataAccess.MySql
{
    public class MySqlDataAccess : IDbDataAccess
    {
        private readonly string _connectionString;

        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        
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
        public async Task DeleteData<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
        public async Task ExecuteWithTransaction(params Func<Task>[] tasks)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var task in tasks)
                        {
                            await task();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
