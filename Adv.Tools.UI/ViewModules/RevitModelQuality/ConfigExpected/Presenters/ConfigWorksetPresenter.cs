

using System;
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
            this.view.DefaultEvent += DefaultSettings;
            this.view.ExportEvent += ExportWorksets;
            this.view.SearchEvent += SearchWorkset;
            this.view.ImportEvent += ImportSettings;
            //Set reports binding source
            this.view.SetBindingSource(this.bindingSource);
            //Load reposts list view
            LoadAllWorksetsList();
        }

        //Methods
        private void LoadAllWorksetsList()
        {
            bindingList = reposetory.GetAllWorksets();
            bindingSource.DataSource = bindingList;
        }

        private void DefaultSettings(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var bindingList = new List<ExpectedWorkset>();
            var stream = new MemoryStream(Resource.DefaultSettings);

            var ds = helper.GetExcelFileAsDataSet(stream);
            var list = helper.GetExcelTableAsList<ExpectedWorkset>(ds,nameof(ExpectedWorkset));

            foreach (var item in list)
            {
                bindingList.Add(item);
            }

            bindingSource.DataSource = bindingList;
        }

        private void ExportWorksets(object sender, EventArgs e)
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

        private void ImportSettings(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();

            var path = helper.GetExcelFilePath();
            var stream = helper.GetExcelFileAsStream(path);
            var ds = helper.GetExcelFileAsDataSet(stream);
            var newList = helper.GetExcelTableAsList<ExpectedWorkset>(ds, nameof(ExpectedWorkset));
           
            bindingSource.DataSource = newList;
        }

        private void SearchWorkset(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = reposetory.GetAllWorksets();

            else bindingList = reposetory.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;

        }
    }
}
