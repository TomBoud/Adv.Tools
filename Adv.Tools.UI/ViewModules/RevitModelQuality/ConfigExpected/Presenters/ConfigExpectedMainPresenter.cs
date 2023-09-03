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
using System.Collections;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigExpectedMainPresenter
    {
        private IConfigExpectedMainView mainView;
        private IDbDataAccess dataAccess;
        private readonly string databaseName;
        private readonly IEnumerable revitObjects;
        
        public ConfigExpectedMainPresenter(IConfigExpectedMainView mainView,IEnumerable revitObjects, IDbDataAccess dataAccess, string databaseName)
        {
            this.mainView = mainView;
            this.dataAccess = dataAccess;
            this.databaseName = databaseName;
            this.revitObjects = revitObjects;
            //Subscribe delegated events to private methods
            this.mainView.ShowConfigDocumentsView += ShowConfigDocumentsView;
            this.mainView.ShowConfigWorksetsView += ShowConfigWorksetsView;
            this.mainView.ShowConfigGridsMonitorView += ShowConfigGridsMonitorView;
            this.mainView.ShowConfigLevelsMonitorView += ShowConfigLevelsMonitorView;


        }

        private void ShowConfigLevelsMonitorView(object sender, EventArgs e)
        {
            IConfigLevelMonitorView view = ConfigLevelMonitorView.GetInstance((Form)mainView);
            IConfigLevelMonitorRepo repo = new ConfigLevelMonitorRepo(dataAccess, databaseName);
            new ConfigLevelMonitorPresenter(view, repo);
            view.ShowThisUI();
        }
        private void ShowConfigGridsMonitorView(object sender, EventArgs e)
        {
            IConfigGridMonitorView view = ConfigGridMonitorView.GetInstance((Form)mainView);
            IConfigGridMonitorRepo repo = new ConfigGridMonitorRepo(dataAccess, databaseName);
            new ConfigGridMonitorPresenter(view, repo);
            view.ShowThisUI();
        }
        private void ShowConfigWorksetsView(object sender, EventArgs e)
        {
            IConfigWorksetView view = ConfigWorksetView.GetInstance((Form)mainView);
            IConfigWorksetsRepo repo = new ConfigWorksetRepo(dataAccess, databaseName);
            new ConfigWorksetPresenter(view, repo);
            view.ShowThisUI();
        }
        private void ShowConfigDocumentsView(object sender, EventArgs e)
        {
            IConfigDocumentView view = ConfigDocumentView.GetInstance((Form)mainView);
            IConfigDocumentsRepo repo = new ConfigDocumentRepo(dataAccess, databaseName);
            new ConfigDocumentPresenter(view, repo, revitObjects);
            view.ShowThisUI();
        }
    }
}
