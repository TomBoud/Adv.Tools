using Adv.Tools.DataAccess.MySql;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Repository;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DevConsole.Commands
{
    public class RunConfigReportView
    {
        public RunConfigReportView()
        {

            //This is for testing only
            var mySql = new MySqlDataAccess("Server=192.168.10.1;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";

            IConfigReportView view = new ConfigReportView();
            IConfigReportRepo repo = new ConfigReportRepo(mySql, databaseName);
            new ConfigReportPresenter(view, repo);

            view.RunUIApplication();


        }
    }
}
