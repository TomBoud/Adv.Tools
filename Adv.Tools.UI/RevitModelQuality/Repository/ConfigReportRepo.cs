
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.RevitModelQuality.Models;

namespace Adv.Tools.UI.RevitModelQuality.Repository
{
    public class ConfigReportRepo : IConfigReportRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;
        private readonly string _tableName;


        public ConfigReportRepo(IDbDataAccess dataAccess,string databaseName, string tableName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
            _tableName = tableName;
        }


        public void Add(ConfigReportModel qualityModel)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(ConfigReportModel qualityModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ConfigReportModel> GetAll()
        {
           var results = _dataAccess.LoadDataSelectAll<IReportCheckScore>(_databaseName, _tableName).Result;
           
            foreach (var report in results.OrderByDescending(x => x.Id).ToList())
            {
                yield return report as ConfigReportModel;
            }
        }

        public IEnumerable<ConfigReportModel> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<IReportCheckScore>(_databaseName, _tableName).Result;

            var uniqueReports = new HashSet<int>();
            var filteredResults = results.Where(result =>
                result.Id.ToString().Contains(value) ||
                result.ModelName.Contains(value) ||
                result.CheckName.Contains(value) ||
                result.CheckLod.Contains(value));

            foreach (var report in filteredResults)
            {
                if (uniqueReports.Add(report.Id))
                {
                    yield return report as ConfigReportModel;
                }
            }
        }
    }
}

