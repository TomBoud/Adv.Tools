using Adv.Tools.Abstractions.Revit;
using Adv.Tools.UI.Common;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Presenters
{
    public class ConfigGridMonitorPresenter
    {
        //Fields
        private IConfigGridMonitorView view;
        private IConfigGridMonitorRepo reposetory;
        private BindingSource bindingSource;
        private IEnumerable<ExpectedGridMonitor> bindingList;

        //Constructor
        public ConfigGridMonitorPresenter(IConfigGridMonitorView view, IConfigGridMonitorRepo repo)
        {
            //Construct the presenter
            this.bindingSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchDocument;
            this.view.ExportEvent += ExportEvent;
            this.view.AddNewEvent += AddNewEvent;
            this.view.EditedEvent += EditedEvent;
            this.view.SaveEvent += SaveEvent;
            this.view.DeleteEvent += DeleteEvent;
            this.view.CancelEvent += CancelEvent;
            this.view.ModelSelectEvent += ModelSelectEvent;
            this.view.SourceSelectEvent += SourceSelectEvent;
            //Set additional data sources
            this.view.SetDocumentNames(reposetory.GetDocumentsData());
            //Set binding source
            this.view.SetBindingSource(this.bindingSource);
            //Load active settings
            LoadAllViewData();
        }

        

        //Delegees
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
        private void ModelSelectEvent(object sender, EventArgs e)
        {
            var doc = reposetory.GetDocumentsData()?.FirstOrDefault(x => x.ModelName.Equals(view.ModelName));

            if (doc is null)
            {
                CleanViewFields();
            }
            else
            {
                view.ModelGuid = doc.ModelGuid;
                view.Discipline = doc.Discipline;
            }
        }
        private void SourceSelectEvent(object sender, EventArgs e)
        {
            var doc = reposetory.GetDocumentsData()?.FirstOrDefault(x => x.ModelName.Equals(view.SourceModelName));

            if (doc is null)
            {
                CleanViewFields();
            }
            else
            {
                view.SourceModelGuid = doc.ModelGuid;
            }
        }
        private void CancelEvent(object sender, EventArgs e)
        {
            CleanViewFields();
        }
        private void EditedEvent(object sender, EventArgs e)
        {
            var selected = (ExpectedGridMonitor)bindingSource.Current;

            if(selected is null)
            {
                view.IsEdit = false;
                view.Message = "Nothing Selected, Please try again";
            }
            else
            {
                view.Id = selected.Id;
                view.ModelName = selected.ModelName;
                view.ModelGuid = selected.ModelGuid;
                view.Discipline = selected.Discipline;
                view.SourceModelName = selected.SourceModelName;
                view.SourceModelGuid = selected.SourceModelGuid;
                view.IsEdit = true;
            }
        }
        private void DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                var selected = (ExpectedGridMonitor)bindingSource.Current;
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
        private void SaveEvent(object sender, EventArgs e)
        {
            var model = new ExpectedGridMonitor();
            model.Id = Convert.ToInt32(view.Id);
            model.ModelName = view.ModelName;
            model.ModelGuid = view.ModelGuid;
            model.Discipline = view.Discipline;
            model.SourceModelName = view.SourceModelName;
            model.SourceModelGuid = view.SourceModelGuid;

            try
            {
                if (view.IsEdit)
                {
                    reposetory.Edit(model);
                    view.Message = "Model Settings Edited Successfully";
                }
                else
                {
                    reposetory.Add(model);
                    view.Message = "A new Model Settings Added Successfully";
                }
                view.IsSuccessful = true;
                LoadAllViewData();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }
        private void AddNewEvent(object sender, EventArgs e)
        {
            view.Id = 0;
            view.ModelName = string.Empty;
            view.ModelGuid = string.Empty;
            view.Discipline = string.Empty;
            view.SourceModelName = string.Empty;
            view.SourceModelGuid = string.Empty;
            view.IsEdit = false;
        }

        //Methods
        private void CleanViewFields()
        {
            view.Id = 0;
            view.ModelName = string.Empty;
            view.ModelGuid = string.Empty;
            view.Discipline = string.Empty;
            view.SourceModelName = string.Empty;
            view.ModelGuid = string.Empty;
        }
        private void LoadAllViewData()
        {
            bindingList = reposetory.GetAllViewData();
            bindingSource.DataSource = bindingList;
        }
        private void SearchDocument(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = reposetory.GetAllViewData();

            else bindingList = reposetory.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;
        }
    }
}
