
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            string query = $"SELECT * FROM {_tableName}";
            var parameters = new { };

            return await _dataAccess.LoadData<ExpectedModel, dynamic>(query, parameters);
        }

        public async Task InsertAsync(List<ExpectedModel> expectedModels)
        {
            PropertyInfo[] props = typeof(ExpectedModel).GetProperties(BindingFlags.Public);

            string query = 
                $"INSERT INTO {_databaseName}.{_tableName} " +
                $"({string.Join(",", props.Select(x => x.Name))}) " +
                $"VALUES ({string.Join(",", props.Select(x => $"@{x.Name}"))})";

            await _dataAccess.SaveData(query, expectedModels);
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
            await _dataAccess.SaveData(updateQuery, parameters);
        }

        public async Task DeleteAsync(List<ExpectedModel> expectedModels)
        {


        }
    }
}
