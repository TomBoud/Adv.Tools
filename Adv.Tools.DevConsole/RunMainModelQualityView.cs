using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.DataAccess.MySql;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.RevitAddin.Models;

namespace Adv.Tools.DevConsole
{
    public class RunMainModelQualityView
    {
        public RunMainModelQualityView()
        {

            //This is for testing only
            List<object> list = new List<object>()
            {
                new RevitDocument(){ Title = "YOH_EL_R22" },
                new RevitDocument(){ Title = "YOH_ME_R22" },
                new RevitDocument(){ Title = "YOH_AR_R22" },
                new RevitDocument(){ Title = "YOH_PL_R22" },
                new RevitDocument(){ Title = "YOH_ST_R22" },
                new RevitElement() { Name = "TestElement"}
            };

            var mySql = new MySqlDataAccess("Server=192.168.10.1;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";

            IConfigExpectedMainView view = new ConfigExpectedMainView();
            new ConfigExpectedMainPresenter(view, list, mySql, databaseName);

            view.RunUIApplication();

        }

    }
}
