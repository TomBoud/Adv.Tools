using Adv.Tools.Abstractions.Revit;
using Adv.Tools.UI.DataModels.RevitModelQuality;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Repositories;
using Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views;
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections;
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
        private BindingSource bindingSource;
        private IEnumerable<Models.ExpectedDocument> bindingList;

        //Constructor
        public ConfigDocumentPresenter(IConfigDocumentView view, IConfigDocumentsRepo repo, IEnumerable revitObjects)
        {
            //Construct the presenter
            this.bindingSource = new BindingSource();
            this.view = view;
            this.reposetory = repo;
            //Subscribe event handlers
            this.view.SearchEvent += SearchReport;
            this.view.AddNewEvent += AddNewEvent;
            this.view.EditedEvent += EditedEvent;
            this.view.SaveEvent += SaveEvent;
            this.view.DeleteEvent += DeleteEvent;
            this.view.CancelEvent += CancelEvent;
            //Set reports binding source
            this.view.SetBindingSource(this.bindingSource);
            this.view.SetDisciplineTypes();
            this.view.SetDocumentNames(revitObjects.OfType<IDocument>());
            //Load reposts list view
            LoadAllDocumentsList();
        }

        private void CancelEvent(object sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void EditedEvent(object sender, EventArgs e)
        {
            var selected = (ExpectedDocument)bindingSource.Current;
            
            view.Id = selected.Id;
            view.ModelName = selected.ModelName;
            view.ModelGuid = selected.ModelGuid;
            view.Discipline = selected.Discipline;
            view.FolderId = selected.FolderId;
            view.ProjectId = selected.ProjectId;
            view.HubId = selected.HubId;
            view.PositionSource = selected.PositionSource;
            view.IsEdit = true;

        }

        private void DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                var selected = (ExpectedDocument)bindingSource.Current;
                reposetory.Delete(selected.Id);
                view.IsSuccessful = true;
                view.Message = "";
                LoadAllDocumentsList();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            var model = new ExpectedDocument();
            model.Id = Convert.ToInt32(view.Id);
            model.ModelName = view.ModelName;
            model.ModelGuid = view.ModelGuid;
            model.Discipline = view.Discipline;
            model.HubId = view.HubId;
            model.FolderId = view.FolderId;
            model.ProjectId = view.ProjectId;   
            model.PositionSource = view.PositionSource;

            try
            {
                if(view.IsEdit)
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
                LoadAllDocumentsList();
                CleanViewFields();
            }
            catch(Exception ex)
            {
                view.IsSuccessful = false;
                view.Message = ex.Message;
            }
        }

        private void CleanViewFields()
        {
            view.Id = 0;
            view.ModelName = string.Empty;
            view.ModelGuid = string.Empty;
            view.Discipline = string.Empty;
            view.HubId = string.Empty;
            view.FolderId = string.Empty;
            view.ProjectId = string.Empty;
            view.PositionSource = string.Empty;
        }

        private void AddNewEvent(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }

        //Methods
        private void LoadAllDocumentsList()
        {
            bindingList = reposetory.GetAllDocuments();
            bindingSource.DataSource = bindingList;
        }
        private void SearchReport(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.view.SearchValue))
                bindingList = reposetory.GetAllDocuments();

            else bindingList = reposetory.GetByValue(this.view.SearchValue);

            bindingSource.DataSource = bindingList;

        }
    }
}
