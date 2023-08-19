
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Models;
using Adv.Tools.UI.DataModels.RevitModelQuality;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.Repository
{
    public class ConfigReportRepo : IConfigReportRepo
    {
        private readonly IDbDataAccess _dataAccess;
        private readonly string _databaseName;

        public ConfigReportRepo(IDbDataAccess dataAccess,string databaseName)
        {
            _dataAccess = dataAccess;
            _databaseName = databaseName;
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
           var results = _dataAccess.LoadDataSelectAll<ReportCheckScore>(_databaseName).Result;
           
            foreach (var report in results.OrderByDescending(x => x.Id).ToList())
            {
                yield return new ConfigReportModel(report);
            }
        }

        public IEnumerable<ConfigReportModel> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ReportCheckScore>(_databaseName).Result;

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
                    yield return new ConfigReportModel(report);
                }
            }
        }
    }
}

