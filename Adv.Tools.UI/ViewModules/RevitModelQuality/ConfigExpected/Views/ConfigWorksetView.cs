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
    public partial class ConfigWorksetView : Form, IConfigWorksetView
    {
        //Singleton
        private static ConfigWorksetView instance;

        //Constructor
        public ConfigWorksetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string WorksetName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
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
        public void SetPetListBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }

        public void ShowThisUI()
        {
            this.Show();
        }

        public static ConfigWorksetView GetInstance(Form parentMdiContainer)
        {
            if(instance is null || instance.IsDisposed)
            {
                instance = new ConfigWorksetView();
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
