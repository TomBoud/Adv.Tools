using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.Views
{
    public partial class ConfigReportView : Form, IConfigReportView
    {
        
        //Constructor
        public ConfigReportView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public string ReportId { get; set; }
        public string ReportName { get; set; }
        public string ReportLod { get; set; }
        public string ReportScore { get; set; }
        public string DocumentName { get; set; }
        public string DocumenGuid { get; set; }
        public string SearchValue { get => search_textBox.Text ; set => search_textBox.Text = value; }
        public bool IsEdit { get; set; }
        public bool IsEnabled{ get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditedEvent;
        public event EventHandler DeleteEvent;

        //Methods
        public void SetPetListBindingSource(BindingSource reportsList)
        {
           dataGridView.DataSource = reportsList;
        }

        public void RunUIApplication()
        {
            Application.Run(this);
        }

        private void AssociateAndRaiseViewEvents()
        {
            search_button.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            search_button.KeyUp += (s, e) =>
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };
        }
    }
}
