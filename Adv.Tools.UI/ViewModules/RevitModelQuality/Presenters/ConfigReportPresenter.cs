using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.Views;



namespace Adv.Tools.UI.ViewModules.RevitModelQuality.Presenters
{
    public class ConfigReportPresenter
    {
        //Fields
        private IConfigReportView view;
        private IConfigReportRepo reposetory;
        private BindingSource reportsSource;
        private IEnumerable<ConfigReportModel> reportsList;

        //Constructor
        public ConfigReportPresenter(IConfigReportView view, IConfigReportRepo repo)
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
            reportsList = reposetory.GetAll();
            reportsSource.DataSource = reportsList;
        }
        private void SearchReport(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.view.SearchValue))
                reportsList = reposetory.GetAll();

            else reportsList = reposetory.GetByValue(this.view.SearchValue);

            reportsSource.DataSource = reportsList;

        }
    }
}
