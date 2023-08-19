

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.RevitModelQuality.Views;
using Adv.Tools.UI.RevitModelQuality.Models;
using Adv.Tools.UI.RevitModelQuality.Repository;
using Adv.Tools.UI.RevitModelQuality.Presenters;
//This is for testing only
using Adv.Tools.DataAccess.MySql;

namespace Adv.Tools.UI.Example
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();

            //This is for testing only
            var mySql = new MySqlDataAccess("Server=localhost;Port=3306;user id=Admin;password=QAZ56okm;CharSet=utf8;");
            var databaseName = "b4f912b9ef6ab43578c73e05d7e9a13d7";
            var tableName = "reportcheckscore";

            IConfigReportView view = new ConfigReportView();
            IConfigReportRepo repo = new ConfigReportRepo(mySql,databaseName,tableName);

            new ConfigReportPresenter(view, repo);

            Application.Run((Form)view);

        }
    }
}
