

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Adv.Tools.UI.Common;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigWorksetPresenter
    {
        //Fields
        private IConfigWorksetView view;
        private IConfigWorksetsRepo reposetory;
        private BindingSource bindingSource;
        private IEnumerable<ExpectedWorkset> bindingList;

        //Constructor
        public ConfigWorksetPresenter(IConfigWorksetView view, IConfigWorksetsRepo repo)
        {
            //Construct the presenter
            this.bindingSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.DefaultEvent += DefaultEvent;
            this.view.ExportEvent += ExportEvent;
            this.view.SearchEvent += SearchEvent;
            this.view.ImportEvent += ImportEvent;
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
                reposetory.DeleteAllViewData();
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
                var selected = (ExpectedWorkset)bindingSource.Current;
                reposetory.Delete(selected.Id);
                view.IsSuccessful = true;
                view.Message = $"{selected.CategoryName} was Removed from {selected.WorksetName} successfully";
                LoadAllViewData();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }
        private void DefaultEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var bindingList = new List<ExpectedWorkset>();
            var stream = new MemoryStream(Resource.DefaultSettings);

            var ds = helper.GetExcelFileAsDataSet(stream);
            var worksetslist = helper.GetExcelTableAsList<ExpectedWorkset>(ds,nameof(ExpectedWorkset));
            var documnetsList = reposetory.GetDocumentsData();

            foreach (var docItem in documnetsList)
            {
                worksetslist.Where(x => x.Discipline.Equals(docItem.Discipline))
                    .ToList().ForEach(defaultItem =>
                    {
                        defaultItem.ModelName = docItem.ModelName;
                        defaultItem.ModelGuid = docItem.ModelGuid;
                        bindingList.Add(defaultItem);
                    });
            }

            bindingSource.DataSource = bindingList;
        }
        private void ExportEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var folder = helper.GetSaveFolderPath();

            if(string.IsNullOrEmpty(folder) is false)
            {
                var source = bindingSource.List as IEnumerable<ExpectedWorkset>;
                var table = helper.ConvertListToDataTable(source.ToList());
                
                helper.ExportDataTableAsExcelFile(table,folder);
            }
        }
        private void ImportEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();

            var path = helper.GetExcelFilePath();
            var stream = helper.GetExcelFileAsStream(path);
            var ds = helper.GetExcelFileAsDataSet(stream);
            var newList = helper.GetExcelTableAsList<ExpectedWorkset>(ds, nameof(ExpectedWorkset));
           
            bindingSource.DataSource = newList;
        }
        private void SearchEvent(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = reposetory.GetAllViewData();

            else bindingList = reposetory.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;

        }
    }
}
