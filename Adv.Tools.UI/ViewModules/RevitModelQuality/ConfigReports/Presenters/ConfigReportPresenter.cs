using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Views;



namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Presenters
{
    public class ConfigReportPresenter
    {

        //Fields
        private IConfigReportView view;
        private IConfigReportRepo reposetory;
        private BindingSource reportsSource;
        private IEnumerable<ConfigReportModel> reportsList;
        private IEnumerable<ConfigReportModel> activeReports;

        //Constructor
        public ConfigReportPresenter(IConfigReportView view, IConfigReportRepo repo)
        {
            //Construct the presenter
            this.reportsSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchReport;
            this.view.CloseEvent += CloseEvent;
            this.view.EnableEvent += EnableEvent;
            this.view.DisableEvent += DisableEvent;
            //Set reports binding source
            this.view.SetPetListBindingSource(this.reportsSource);
            //Load reposts list view
            LoadAllReportsList();
        }

        private void DisableEvent(object sender, EventArgs e)
        {
            
            throw new NotImplementedException();
        }

        private void EnableEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        //Methods
        public IEnumerable<ConfigReportModel> GetActiveReportsList()
        {
            return activeReports;
        }

        private void LoadAllReportsList()
        {
            reportsList = reposetory.GetAllReports();
            reportsSource.DataSource = reportsList;
        }

        //Events
        private void CloseEvent(object sender, EventArgs e)
        {
            reportsList = reportsSource.List as IEnumerable<ConfigReportModel>;
            activeReports = reportsList.Where(x=>x.IsActive).ToList();
        }

        private void SearchReport(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(this.view.SearchValue))
                reportsList = reposetory.GetAllReports();

            else reportsList = reposetory.GetByValue(this.view.SearchValue);

            reportsSource.DataSource = reportsList;

        }
    }
}
