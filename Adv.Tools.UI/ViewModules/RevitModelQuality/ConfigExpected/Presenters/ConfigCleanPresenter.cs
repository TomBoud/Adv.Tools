using Adv.Tools.Abstractions.Revit;
using Adv.Tools.UI.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigCleanPresenter
    {
        //Fields
        private IConfigCleanView view;
        private IConfigCleanRepo reposetory;
        private BindingSource bindingSource;
        private IEnumerable<ExpectedCleanView> bindingList;

        //Constructor
        public ConfigCleanPresenter(IConfigCleanView view, IConfigCleanRepo repo)
        {
            //Construct the presenter
            this.bindingSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.DefaultEvent += DefaultSettings;
            this.view.ExportEvent += ExportEvent;
            this.view.SearchEvent += SearchWorkset;
            this.view.ImportEvent += ImportSettings;
            this.view.DeleteEvent += DeleteEvent;
            this.view.ClearAllEvent += ClearAllEvent;
            //Set reports binding source
            this.view.SetBindingSource(this.bindingSource);
            //Load reposts list view
            LoadAllViewData();
        }


        //Methods
        private void LoadAllViewData()
        {
            bindingList = reposetory.GetAllViewData();
            bindingSource.DataSource = bindingList;
        }
        private void ClearAllEvent(object sender, EventArgs e)
        {
            try
            {
                reposetory.DeletAllViewData();
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
        private void DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                var selected = (ExpectedCleanView)bindingSource.Current;
                reposetory.Delete(selected.Id);
                view.IsSuccessful = true;
                view.Message = $"The {selected.ViewName} was Removed successfully"; ;
                LoadAllViewData();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }
        private void DefaultSettings(object sender, EventArgs e)
        {
            var bindingList = new List<ExpectedCleanView>();
            //var expectedDocs = reposetory.GetDocumentsData();
            bindingSource.DataSource = bindingList;
        }
        private void ExportEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var folder = helper.GetSaveFolderPath();

            if (string.IsNullOrEmpty(folder) is false)
            {
                var source = bindingSource.List as IEnumerable<ExpectedCleanView>;
                var table = helper.ConvertListToDataTable(source.ToList());

                helper.ExportDataTableAsExcelFile(table, folder);
            }
        }
        private void ImportSettings(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();

            var path = helper.GetExcelFilePath();
            var stream = helper.GetExcelFileAsStream(path);
            var ds = helper.GetExcelFileAsDataSet(stream);
            var newList = helper.GetExcelTableAsList<ExpectedCleanView>(ds, nameof(ExpectedCleanView));

            bindingSource.DataSource = newList;
        }
        private void SearchWorkset(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = reposetory.GetAllViewData();

            else bindingList = reposetory.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;

        }
    }
}
