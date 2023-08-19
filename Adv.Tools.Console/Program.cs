using Adv.Tools.DataAccess.MySql;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Repository;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            //This is for testing only
            var mySql = new MySqlDataAccess("Server=localhost;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";

            IConfigReportView view = new ConfigReportView();
            IConfigReportRepo repo = new ConfigReportRepo(mySql, databaseName);

            new ConfigReportPresenter(view, repo);

            view.RunUIApplication();
        }
    }
}
