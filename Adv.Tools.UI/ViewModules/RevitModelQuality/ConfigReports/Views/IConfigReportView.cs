using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Views
{
    public interface IConfigReportView
    {
        //Properties - Fields
        string ReportId { get; set; }
        string ReportName { get; set; }
        string ReportLod { get; set; }
        string ReportScore { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Discipline { get; set; }
       

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsEnabled { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Event
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditedEvent;
        event EventHandler DeleteEvent;
        event EventHandler CloseEvent;

        //Methods
        void SetPetListBindingSource(BindingSource reportsList);
        void RunUIApplication();
    }
}
