using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.UI.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigProjectInfoPresenter
    {
        //Fields
        private IConfigProjectInfoView view;
        private IConfigProjectInfoRepo reposetory;
        private BindingSource bindingSource;
        private IEnumerable<ExpectedProjectInfo> bindingList;
        private IEnumerable revitObjects;

        //Constructor
        public ConfigProjectInfoPresenter(IConfigProjectInfoView view, IConfigProjectInfoRepo repo, IEnumerable revitObjects)
        {
            //Construct the presenter
            this.revitObjects = revitObjects;
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
                var selected = (ExpectedProjectInfo)bindingSource.Current;
                reposetory.Delete(selected.Id);
                view.IsSuccessful = true;
                view.Message = "";
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
            var bindingList = new List<ExpectedProjectInfo>();
            var expectedDocs = reposetory.GetDocumentsData();
            
            var activeDoc = revitObjects?.OfType<IDocument>()
                ?.FirstOrDefault(x=>x.IsActiveModel) ?? null;
            
            var props = typeof(IDocument).GetProperties()
                .Where(prop => 
                    prop.Name.Equals(nameof(IDocument.Name)) ||
                    prop.Name.Equals(nameof(IDocument.Number)) ||
                    prop.Name.Equals(nameof(IDocument.Status)) ||
                    prop.Name.Equals(nameof(IDocument.Address)) ||
                    prop.Name.Equals(nameof(IDocument.Author)) ||
                    prop.Name.Equals(nameof(IDocument.OrganizationName)) ||
                    prop.Name.Equals(nameof(IDocument.OrganizationDescription)) ||
                    prop.Name.Equals(nameof(IDocument.ClientName)) ||
                    prop.Name.Equals(nameof(IDocument.BuildingName)))
                .ToList();
           
            
            foreach (var doc in expectedDocs)
            {
                foreach(var prop in props)
                {
                    var newInfo = new ExpectedProjectInfo()
                    {
                        ModelName = doc.ModelName,
                        ModelGuid = doc.ModelGuid,
                        Discipline = doc.Discipline,
                        Parameter = prop.Name,
                    };

                    if(activeDoc is null)
                    {
                        newInfo.Value = string.Empty;
                    }
                    else
                    {
                        newInfo.Value = prop?.GetValue(activeDoc)?.ToString() ?? string.Empty;
                    }

                    bindingList.Add(newInfo);

                }
            }

            bindingSource.DataSource = bindingList;
        }
        private void ExportEvent(object sender, EventArgs e)
        {
            var helper = new ExcelFilesHelper();
            var folder = helper.GetSaveFolderPath();

            if (string.IsNullOrEmpty(folder) is false)
            {
                var source = bindingSource.List as IEnumerable<ExpectedProjectInfo>;
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
            var newList = helper.GetExcelTableAsList<ExpectedProjectInfo>(ds, nameof(ExpectedProjectInfo));

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
