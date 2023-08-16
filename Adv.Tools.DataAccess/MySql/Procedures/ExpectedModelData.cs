
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.DataAccess.MySql.Models;

namespace Adv.Tools.DataAccess.MySql.Procedures
{
    public class ExpectedModelData
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _tableName;
        private readonly string _databaseName;

        public ExpectedModelData(IDbDataAccess dataAccess, string databaseName, string tableName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
            _tableName = tableName;
        }

        public async Task<List<ExpectedModel>> SelectAllAsync()
        {
            string query = $"SELECT * FROM {_databaseName}.{_tableName}";

            try
            {
                return await _dataAccess.LoadData<ExpectedModel, dynamic>(query, new { });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task InsertAsync(List<ExpectedModel> expectedModels)
        {
            PropertyInfo[] props = typeof(ExpectedModel).GetProperties();

            string query = 
                $"INSERT INTO {_databaseName}.{_tableName} " +
                $"({string.Join(",", props.Select(x => x.Name))}) " +
                $"VALUES ({string.Join(",", props.Select(x => $"@{x.Name}"))})";

            try
            {
                await _dataAccess.SaveData(query, expectedModels);
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
                $"UPDATE {_databaseName}.{_tableName} " +
                $"SET {nameof(ExpectedModel.ModelName)} = @{nameof(ExpectedModel.ModelName)} " +
                $"WHERE {nameof(ExpectedModel.Id)} = @{nameof(ExpectedModel.Id)}";

            // Create a parameter object that contains all the values
            var parameters = expectedModels.Select(model => new { model.ModelName, model.Id });

            try
            {
                await _dataAccess.SaveData(updateQuery, parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task DeleteAsync(List<ExpectedModel> expectedModels)
        {


        }

        public async Task CreateTableIfNotExists()
        {
            string sqlQuery =
                $"CREATE TABLE IF NOT EXISTS {_databaseName}.{_tableName} " +
                $"(`{nameof(ExpectedModel.Id)}` int NOT NULL AUTO_INCREMENT, " +
                $"`{nameof(ExpectedModel.ModelName)}` text, " +
                $"`{nameof(ExpectedModel.ModelGuid)}` text, " +
                $"`{nameof(ExpectedModel.HubId)}` text, " +
                $"`{nameof(ExpectedModel.ProjectId)}` text, " +
                $"`{nameof(ExpectedModel.FolderId)}` text, " +
                $"`{nameof(ExpectedModel.Disicpline)}` text, " +
                $"`{nameof(ExpectedModel.PositionSource)}` text, " +
                $"`{nameof(ExpectedModel.PositionSourceGuid)}` text, " +
                $"PRIMARY KEY (`{nameof(ExpectedModel.Id)}`))";

            try
            {
                await _dataAccess.SaveData<ExpectedModel>(sqlQuery, null);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteTableIfExist()
        {
            string sqlQuery = $"DROP TABLE IF EXISTS  {_databaseName}.{_tableName}";

            try
            {
                await _dataAccess.SaveData<ExpectedModel>(sqlQuery, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
