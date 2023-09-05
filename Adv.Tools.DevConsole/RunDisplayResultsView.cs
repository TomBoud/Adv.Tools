using Adv.Tools.DataAccess.MySql;
using Adv.Tools.RevitAddin.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DevConsole
{
    public class RunDisplayResultsView
    {
        public RunDisplayResultsView()
        {

            var mySql = new MySqlDataAccess("Server=192.168.10.1;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";

            IDisplayResultsMainView view = new DisplayResultsMainView();
            new DisplayResultsMainPresenter(view, mySql, databaseName);

            view.RunUIApplication();

        }
    }
}
