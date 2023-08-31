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
    public partial class ConfigGridsMonitorView : Form, IConfigGridsMonitorView
    {
        //Singleton
        private static ConfigGridsMonitorView instance;

        //Constructor
        public ConfigGridsMonitorView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string SourceModelName { get; set; }
        public string SourceModelGuid { get; set; }

        public string SearchValue { get; set; }
        public bool IsEdit { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        //Event
        public event EventHandler DefaultEvent;
        public event EventHandler ExportEvent;
        public event EventHandler ImportEvent;
        public event EventHandler SearchEvent;
        public event EventHandler DeleteEvent;
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

        public static ConfigGridsMonitorView GetInstance(Form parentMdiContainer)
        {
            if (instance is null || instance.IsDisposed)
            {
                instance = new ConfigGridsMonitorView();
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
