
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedModel : IExpectedDocument
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string HubId { get; set; }
        public string ProjectId { get; set; }
        public string FolderId { get; set; }
        public string Disicpline { get; set; }
        public string PositionSource { get; set; }
        public string PositionSourceGuid { get; set; }

        public string GetMySqlTableMapping(string databaseName)
        {
            string sqlQuery =
                $"CREATE TABLE IF NOT EXISTS {databaseName}.{nameof(ExpectedModel)} " +
                $"(`{nameof(Id)}` int NOT NULL AUTO_INCREMENT, " +
                $"`{nameof(ModelName)}` text, " +
                $"`{nameof(ModelGuid)}` text, " +
                $"`{nameof(HubId)}` text, " +
                $"`{nameof(ProjectId)}` text, " +
                $"`{nameof(FolderId)}` text, " +
                $"`{nameof(Disicpline)}` text, " +
                $"`{nameof(PositionSource)}` text, " +
                $"`{nameof(PositionSourceGuid)}` text, " +
                $"PRIMARY KEY (`{nameof(Id)}`))";

            return sqlQuery;
        }
    }
}
