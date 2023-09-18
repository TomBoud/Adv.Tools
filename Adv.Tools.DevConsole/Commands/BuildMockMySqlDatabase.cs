using Adv.Tools.Abstractions.Common;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.RevitAddin.Application.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DevConsole.Commands
{
    public class BuildMockMySqlDatabase
    {
        private readonly MySqlDataAccess _access = new MySqlDataAccess(Properties.Settings.Default.DevDbConStr);
        private readonly string _dbName = "b4f912b9ef6ab43578c73e05d7e9a13d7";
        //"b4f912b9ef6ab43578c73e05d7e9a13d7" Properties.Settings.Default.DatabaseName
        public BuildMockMySqlDatabase()
        {
            Console.Clear();

            IEnumerable modelEntities = Assembly.GetAssembly(_access.GetType()).GetTypes()
                .Where(t => typeof(IDbModelEntity).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract).ToList();

            foreach (Type modelEntity in modelEntities)
            {
                var instance = Activator.CreateInstance(modelEntity) as IDbModelEntity;
                string query = instance.GetCreateTableQuery(_dbName);

                Console.WriteLine($"Building Mock MySql Database for {modelEntity.Name}");
                CreateDbTable(query);
            }

            Console.ReadLine();
        }

        private async void CreateDbTable(string query)
        {

            await _access.ExecuteSqlQueryAsync(query);

        }

    }
}
