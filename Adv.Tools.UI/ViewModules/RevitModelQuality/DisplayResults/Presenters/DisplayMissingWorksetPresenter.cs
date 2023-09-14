using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Presenters
{
    public class DisplayMissingWorksetPresenter
    {
        //Fields
        private IDisplayMissingWorksetView view;
        private IDisplayMissingWorksetRepo repository;
        private BindingSource bindingSource;
        private IEnumerable<ReportMissingWorkset> bindingList;

        //Constructor
        public DisplayMissingWorksetPresenter(IDisplayElementWorksetView view, IDisplayElementWorksetRepo repo)
        {
            //Construct the presenter
            this.bindingSource = new BindingSource();
            this.view = view;
            this.repository = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchDocument;
            this.view.ExportEvent += ExportEvent;
            this.view.ClearAllEvent += ClearAllEvent;
            //Set binding source
            this.view.SetBindingSource(this.bindingSource);
            //Load active settings
            LoadAllViewData();
        }


        //Delegees
        private void ClearAllEvent(object sender, EventArgs e)
        {
            try
            {
                repository.DeleteAllViewData();
                view.IsSuccessful = true;
                view.Message = "All Data was Deleted successfully";
                LoadAllViewData();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void ExportEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var folder = helper.GetSaveFolderPath();

            if (string.IsNullOrEmpty(folder) is false)
            {
                var source = bindingSource.List as IEnumerable<ExpectedGridMonitor>;
                var table = helper.ConvertListToDataTable(source.ToList());

                helper.ExportDataTableAsExcelFile(table, folder);
            }
        }


        //Methods
        private void LoadAllViewData()
        {
            bindingList = repository.GetAllViewData();
            bindingSource.DataSource = bindingList;
        }

        private void SearchDocument(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = repository.GetAllViewData();

            else bindingList = repository.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;
        }
    }
}
