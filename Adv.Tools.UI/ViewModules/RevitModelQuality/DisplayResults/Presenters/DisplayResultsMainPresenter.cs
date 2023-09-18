using Adv.Tools.Abstractions.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Repositories;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Presenters
{
    public class DisplayResultsMainPresenter
    {
        private IDisplayResultsMainView mainView;
        private IDbDataAccess dataAccess;
        private readonly string databaseName;

        public DisplayResultsMainPresenter(IDisplayResultsMainView mainView, IDbDataAccess dataAccess, string databaseName)
        {
            this.mainView = mainView;
            this.dataAccess = dataAccess;
            this.databaseName = databaseName;
            //Subscribe delegated events to private methods
            this.mainView.ShowDisplayElementWorksetView += ShowDisplayElementWorksetView;
            this.mainView.ShowDisplayElementWorksetView += ShowDisplayMissingWorksetsView;
        }

        private void ShowDisplayElementWorksetView(object sender, EventArgs e)
        {
            IDisplayElementWorksetView view = DisplayElementWorksetView.GetInstance((Form)mainView);
            IDisplayElementWorksetRepo repo = new DisplayElementWorksetRepo(dataAccess, databaseName);
            new DisplayElementWorksetPresenter(view, repo);
            view.ShowThisUI();
        }

        private void ShowDisplayMissingWorksetsView(object sender, EventArgs e)
        {
            IDisplayMissingWorksetView view = DisplayMissingWorksetView.GetInstance((Form)mainView);
            IDisplayMissingWorksetRepo repo = new DisplayMissingWorksetRepo(dataAccess, databaseName);
            new DisplayMissingWorksetPresenter(view, repo);
            view.ShowThisUI();
        }
    }
}
