

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigWorksetPresenter
    {
        //Fields
        private IConfigWorksetView view;
        private IConfigWorksetRepo reposetory;
        private BindingSource reportsSource;
        private IEnumerable<ConfigWorksetModel> reportsList;

        //Constructor
        public ConfigWorksetPresenter(IConfigWorksetView view, IConfigWorksetRepo repo)
        {
            //Construct the presenter
            this.reportsSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchReport;
            //Set reports binding source
            this.view.SetPetListBindingSource(this.reportsSource);
            //Load reposts list view
            LoadAllReportsList();
        }

        //Methods
        private void LoadAllReportsList()
        {
            reportsList = reposetory.GetAllWorksets();
            reportsSource.DataSource = reportsList;
        }
        private void SearchReport(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                reportsList = reposetory.GetAllWorksets();

            else reportsList = reposetory.GetByValue(this.view.SearchValue);

            reportsSource.DataSource = reportsList;

        }
    }
}
