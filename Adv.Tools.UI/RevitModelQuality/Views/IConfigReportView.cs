using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.RevitModelQuality.Views
{
    public interface IConfigReportView
    {
        //Properties - Fields
        string ReportId { get; set; }
        string ReportName { get; set; }
        string ReportLod { get; set; }
        string ReportScore { get; set; }
        string DocumentName { get; set; }
        string DocumenGuid { get; set; }

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

        //Methods
        void SetPetListBindingSource(BindingSource reportsList);
        void Show(); //Optional
    }
}
