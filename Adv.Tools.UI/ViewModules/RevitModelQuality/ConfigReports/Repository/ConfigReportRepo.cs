
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using System.Text.RegularExpressions;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Repository
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

        public IEnumerable<ConfigReportModel> GetAllReports()
        {
            var results = _dataAccess.LoadDataSelectAll<ReportCheckScore>(_databaseName);
            var reports = new List<ConfigReportModel>();

            foreach (var report in results.OrderByDescending(x => x.Id).ToList())
            {
                yield return new ConfigReportModel(report);
            }
        }

        public IEnumerable<ConfigReportModel> GetByValue(string value)
        {
            var results = _dataAccess.LoadDataSelectAll<ReportCheckScore>(_databaseName);

            var pattern = Regex.Escape(value);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);

            var filteredResults = results.Where(result =>
                 regex.IsMatch(result.Id.ToString()) ||
                 regex.IsMatch(result.ModelName) ||
                 regex.IsMatch(result.CheckName) ||
                 regex.IsMatch(result.CheckLod))
                .ToList();

            foreach (var report in filteredResults.OrderByDescending(x => x.Id).ToList())
            {
                yield return new ConfigReportModel(report);
            }
        }
    }
}

