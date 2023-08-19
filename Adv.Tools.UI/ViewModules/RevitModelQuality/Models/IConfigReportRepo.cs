using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.Models
{
    public interface IConfigReportRepo
    {
        void Add(ConfigReportModel qualityModel);
        void Edit(ConfigReportModel qualityModel);
        void Delete(int id);
        IEnumerable<ConfigReportModel> GetAll();
        IEnumerable<ConfigReportModel> GetByValue(string value);
    }
}
