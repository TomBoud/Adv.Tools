using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    public partial class ConfigDocumentView : Form, IConfigDocumentView
    {
        //Singleton
        private static ConfigDocumentView instance;

        //Constructor
        public ConfigDocumentView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string HubId { get; set; }
        public string ProjectId { get; set; }
        public string FolderId { get; set; }
        public string PositionSource { get; set; }
        public string PositionSourceGuid { get; set; }


        public string SearchValue { get => search_textBox.Text; set => search_textBox.Text = value; }
        public bool IsEdit { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        

        //Events
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditedEvent;
        public event EventHandler DeleteEvent;

        //Methods
        public void SetBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }

        public void ShowThisUI()
        {
            this.Show();
        }

        public static ConfigDocumentView GetInstance(Form parentMdiContainer)
        {
            if (instance is null || instance.IsDisposed)
            {
                instance = new ConfigDocumentView();
                instance.MdiParent = parentMdiContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                instance.WindowState = FormWindowState.Normal;
                instance.BringToFront();
            }

            return instance;
        }


        private void AssociateAndRaiseViewEvents()
        {
            search_button.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            search_textBox.KeyUp += (s, e) =>
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };
        }


    }
}
