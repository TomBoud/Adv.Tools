using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    public interface IConfigProjectInfoView
    {
        //Properties - Fields
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string Parameter { get; set; }
        string Value { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Event
        event EventHandler DefaultEvent;
        event EventHandler ExportEvent;
        event EventHandler ImportEvent;
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditedEvent;
        event EventHandler DeleteEvent;
        event EventHandler ClearAllEvent;
        event EventHandler SaveEvent;

        //Methods
        void SetBindingSource(BindingSource bindingList);
        void ShowThisUI();
    }
}
