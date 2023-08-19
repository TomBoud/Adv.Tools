using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using Adv.Tools.DataAccess.MySql.Models;
using System.Reflection;
using Adv.Tools.Abstractions;
using System.Threading;

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
        public async Task<List<T>> LoadDataSelectAll<T>(string databaseName)
        {
            string sqlQuery = $"SELECT * FROM {databaseName}.{typeof(T).Name}";

            try
            {
                return await LoadData<T, dynamic>(sqlQuery, new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SaveData<T>(string sqlQuery, T parameters)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    await connection.ExecuteAsync(sqlQuery, parameters);
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task SaveDataByPropertiesMappping<T>(string databaseName, List<T> data)
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            string sqlQuery =
                $"INSERT INTO {databaseName}.{nameof(T)} " +
                $"({string.Join(",", props.Select(x => x.Name))}) " +
                $"VALUES ({string.Join(",", props.Select(x => $"@{x.Name}"))})";

            try
            {
               await SaveData(sqlQuery, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(List<ExpectedModel> expectedModels)
        {
            if (expectedModels is null || expectedModels.Count == 0)
                return;

            string updateQuery =
                //$"UPDATE {_databaseName}.{_tableName} " +
                $"SET {nameof(ExpectedModel.ModelName)} = @{nameof(ExpectedModel.ModelName)} " +
                $"WHERE {nameof(ExpectedModel.Id)} = @{nameof(ExpectedModel.Id)}";

            // Create a parameter object that contains all the values
            var parameters = expectedModels.Select(model => new { model.ModelName, model.Id });

            try
            {
                //await _dataAccess.SaveData(updateQuery, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task DeleteData<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
        public async Task DeleteDataAllRows<T>(string databaseName)
        {
            string sqlQuery = $"DELETE FROM {databaseName}.{nameof(T)}";

            try
            {
                await DeleteData<T, dynamic>(sqlQuery, new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteTableIfExists(string databaseName,string tableName)
        {
            string sqlQuery = $"DROP TABLE IF EXISTS {databaseName}.{tableName}";

            try
            {
                await ExecuteSqlQueryAsync(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public async Task<int> ExecuteSqlQueryAsync(string sqlQuery)
        {
            try
            {
                using (IDbConnection connection = new MySqlConnection(_connectionString))
                {
                    return await connection.ExecuteAsync(sqlQuery);
                };
            }
            catch (Exception ex)
            {
                throw ex;
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
