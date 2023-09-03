using Adv.Tools.Abstractions.Database;
using Adv.Tools.Abstractions.Revit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    public interface IConfigGridMonitorView
    {
        //Properties - Fields
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string SourceModelName { get; set; }
        string SourceModelGuid { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Event
        event EventHandler SourceSelectEvent;
        event EventHandler ModelSelectEvent;
        event EventHandler ExportEvent;
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditedEvent;
        event EventHandler DeleteEvent;
        event EventHandler CancelEvent;
        event EventHandler SaveEvent;

        //Methods
        void SetBindingSource(BindingSource bindingList);
        void SetDocumentNames(IEnumerable<IExpectedDocument> documents);
        void ShowThisUI();
    }
}
