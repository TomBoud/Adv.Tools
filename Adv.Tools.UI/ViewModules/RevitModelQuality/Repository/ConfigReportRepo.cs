
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Models;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.DataAccess.MySql;

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


            //var results = await _dataAccess.LoadDataSelectAll<ReportCheckScore>(_databaseName);

            var mySql = new MySqlDataAccess("Server=192.168.10.1;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";

            var results = mySql.LoadDataSelectAll<ReportCheckScore>(databaseName);
            var reports = new List<ConfigReportModel>();

            foreach (var report in results.OrderByDescending(x => x.Id).ToList())
            {
                reports.Add(new ConfigReportModel(report));
            }

            return reports;
        }

        public IEnumerable<ConfigReportModel> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAllAsync<ReportCheckScore>(_databaseName).Result;

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

