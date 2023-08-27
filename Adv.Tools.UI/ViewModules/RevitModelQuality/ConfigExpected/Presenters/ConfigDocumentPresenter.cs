using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigDocumentPresenter
    {
        //Fields
        private IConfigDocumentView view;
        private IConfigDocumentsRepo reposetory;
        private BindingSource reportsSource;
        private IEnumerable<ConfigDocumentModel> documentsList;

        //Constructor
        public ConfigDocumentPresenter(IConfigDocumentView view, IConfigDocumentsRepo repo)
        {
            //Construct the presenter
            this.reportsSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchReport;
            //Set reports binding source
            this.view.SetBindingSource(this.reportsSource);
            //Load reposts list view
            LoadAllDocumentsList();
        }

        //Methods
        private void LoadAllDocumentsList()
        {
            documentsList = reposetory.GetAllDocuments();
            reportsSource.DataSource = documentsList;
        }
        private void SearchReport(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                documentsList = reposetory.GetAllDocuments();

            else documentsList = reposetory.GetByValue(this.view.SearchValue);

            reportsSource.DataSource = documentsList;

        }
    }
}
