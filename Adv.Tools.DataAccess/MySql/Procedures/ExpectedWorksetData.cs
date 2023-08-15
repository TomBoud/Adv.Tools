using Adv.Tools.DataAccess.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.MySql.Procedures
{
    public class ExpectedWorksetData
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _tableName;
        private readonly string _databaseName;

        public ExpectedWorksetData(IDbDataAccess dataAccess, string databaseName, string tableName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
            _tableName = tableName;
        }

        public async Task<List<ExpectedWorkset>> SelectAllAsync()
        {
            string query = $"SELECT * FROM {_tableName}";
            var parameters = new { };

            return await _dataAccess.LoadData<ExpectedWorkset, dynamic>(query, parameters);
        }

        public async Task InsertAsync(List<ExpectedWorkset> expectedModels)
        {
            PropertyInfo[] props = typeof(ExpectedWorkset).GetProperties(BindingFlags.Public);

            string query =
                $"INSERT INTO {_databaseName}.{_tableName} " +
                $"({string.Join(",", props.Select(x => x.Name))}) " +
                $"VALUES ({string.Join(",", props.Select(x => $"@{x.Name}"))})";

            await _dataAccess.SaveData(query, expectedModels);
        }



        
    }
}

