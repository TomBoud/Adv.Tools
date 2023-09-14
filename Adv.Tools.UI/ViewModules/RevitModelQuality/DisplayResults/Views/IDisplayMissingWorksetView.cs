using DocumentFormat.OpenXml.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views
{
    public interface IDisplayMissingWorksetView
    {
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
        string WorksetName { get; set; }
        string ObjectId { get; set; }
        bool IsFound { get; set; }
        string IsFoundHeb { get; set; }

        string SearchValue { get; set; }
        string Message { get; set; }
        bool IsSuccessful { get; set; }

        //Event
        event EventHandler ExportEvent;
        event EventHandler SearchEvent;
        event EventHandler ClearAllEvent;


        //Methods
        void SetBindingSource(BindingSource bindingList);
        void ShowThisUI();
    }
}
