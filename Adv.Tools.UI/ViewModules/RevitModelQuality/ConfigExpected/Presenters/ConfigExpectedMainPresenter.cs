using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories;
using Adv.Tools.Abstractions.Common;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigExpectedMainPresenter
    {
        private IConfigExpectedMainView mainView;
        private IDbDataAccess dataAccess;
        private readonly string databaseName;
        
        public ConfigExpectedMainPresenter(IConfigExpectedMainView mainView, IDbDataAccess dataAccess, string databaseName)
        {
            this.mainView = mainView;
            this.dataAccess = dataAccess;
            this.databaseName = databaseName;

            //Subscribe delegated events to private methods
            this.mainView.ShowConfigWorksetsView += ShowConfigWorksetsView;
        }

        //Delegated Methods
        private void ShowConfigWorksetsView(object sender, EventArgs e)
        {
            IConfigWorksetView view = ConfigWorksetView.GetInstance((Form)mainView);
            IConfigWorksetsRepo repo = new ConfigWorksetRepo(dataAccess, databaseName);
            new ConfigWorksetPresenter(view, repo);
            view.ShowThisUI();
        }
    }
}
