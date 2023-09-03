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
    public partial class ConfigProjectInfoView : Form, IConfigProjectInfoView
    {
        //Singleton
        private static ConfigProjectInfoView instance;

        //Constructor
        public ConfigProjectInfoView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }


        public string SearchValue { get => search_textBox.Text; set => search_textBox.Text = value; }
        public bool IsEdit { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }


        //Events
        public event EventHandler DefaultEvent;
        public event EventHandler ExportEvent;
        public event EventHandler ImportEvent;
        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditedEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler ClearAllEvent;
        public event EventHandler SaveEvent;

        //Methods
        public void SetBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }

        public void ShowThisUI()
        {
            this.Show();
        }

        public static ConfigProjectInfoView GetInstance(Form parentMdiContainer)
        {
            if (instance is null || instance.IsDisposed)
            {
                instance = new ConfigProjectInfoView();
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

            //Clear Event
            clearAll_button.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete all data?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result is DialogResult.Yes)
                {
                    ClearAllEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Default Event
            default_button.Click += delegate
            {
                DefaultEvent?.Invoke(this, EventArgs.Empty);
            };

            //Export Event
            export_button.Click += delegate
            {
                ExportEvent?.Invoke(this, EventArgs.Empty);
            };

            //Import Event
            import_button.Click += delegate
            {
                ImportEvent?.Invoke(this, EventArgs.Empty);
            };

            //Search Event
            search_button.Click += delegate
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };
            search_textBox.KeyUp += (s, e) =>
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //Delete Event
            delete_button.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result is DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Save Event
            save_button.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(Message);
            };
        }


    }
}
