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
using System.Threading;
using Adv.Tools.Abstractions.Common;

namespace Adv.Tools.DataAccess.MySql
{
    public class MySqlDataAccess : IDbDataAccess
    {
        private readonly string _connectionString;

        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        //Load
        public async Task<List<T>> LoadDataAsync<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                var rows = await connection.QueryAsync<T>(sqlQuery, parameters);
                return rows.ToList();
            }
        }
        public List<T> LoadData<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                var rows = connection.Query<T>(sqlQuery, parameters);
                return rows.ToList();
            }
        }
        public List<T> LoadDataSelectAll<T>(string databaseName)
        {
            string sqlQuery = $"SELECT * FROM {databaseName}.{typeof(T).Name}";

            try
            {
                return LoadData<T, dynamic>(sqlQuery, new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<T>> LoadDataSelectAllAsync<T>(string databaseName)
        {
            string sqlQuery = $"SELECT * FROM {databaseName}.{typeof(T).Name}";

            try
            {
                return await LoadDataAsync<T, dynamic>(sqlQuery, new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Save
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
        public async Task SaveByUpdateValuesAsync<T>(string databaseName, List<T> data)
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            string sqlQuery =
                $"INSERT INTO {databaseName}.{typeof(T).Name} " +
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
        public async Task SaveByInsertValuesAsync<T>(string databaseName, List<T> data)
        {
            PropertyInfo[] props = typeof(T).GetProperties();

            string sqlQuery =
                $"INSERT INTO {databaseName}.{typeof(T).Name} " +
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
        public async Task SaveByInsertUpdateOnDuplicateKeysAsync<T>(string databaseName,List<T> data)
        {
            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var properties = typeof(T).GetProperties();

                string tableName = typeof(T).Name;
                string columns = string.Join(", ", properties.Select(p => p.Name));
                string parametersPlaceholder = string.Join(", ", properties.Select(p => "@" + p.Name));
                string updateAssignments = string.Join(", ", properties.Select(p => p.Name + " = VALUES(" + p.Name + ")"));

                string query = $"INSERT INTO {databaseName}.{tableName} ({columns}) VALUES ({parametersPlaceholder}) " +
                               $"ON DUPLICATE KEY UPDATE {updateAssignments}";
                
                await dbConnection.ExecuteAsync(query, data);
            }
        }

        //Delete
        public async Task DeleteData<T, U>(string sqlQuery, U parameters)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
        public async Task DeleteAllTableDataAsync<T>(string databaseName)
        {
            using (IDbConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    await connection.ExecuteAsync($"truncate {databaseName}.{typeof(T).Name}");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public async Task DeleteDataById<T>(string databaseName,int Id)
        {
            string sqlQuery = $"DELETE FROM {databaseName}.{nameof(T)} Where Id={Id}";

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

        //Execute
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
        public async Task ExecuteBuildMySqlDataBase(string databaseName)
        {
            var buildTasks = new List<Func<Task>>();
            // Get all types from the currently executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            var types = assembly.GetTypes();

            // Filter the types that implement the interface
            var modelEntities = types.Where(t => typeof(IDbModelEntity).IsAssignableFrom(t) && t.IsClass).ToList();

            foreach (var entity in modelEntities)
            {
                var dbModelEntity = Activator.CreateInstance(entity) as IDbModelEntity;
                var query = dbModelEntity.GetCreateTableQuery(databaseName);
                buildTasks.Add( () => ExecuteSqlQueryAsync(query));
            }

            await ExecuteWithTransaction(buildTasks.ToArray());

        }

    }
}
