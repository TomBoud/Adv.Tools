using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views
{
    public partial class DisplayElementWorksetView : Form, IDisplayElementWorksetView
    {
        //Singleton
        private static DisplayElementWorksetView instance;

        //Constructor
        public DisplayElementWorksetView()
        {
            InitializeComponent();
            AssociateAndRaiseViewEvents();
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Discipline { get; set; }
        public string ObjectName { get; set; }
        public string ObjectCategory { get; set; }
        public string ObjectId { get; set; }


        public string SearchValue { get => search_textBox.Text; set => search_textBox.Text = value; }
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }

        //Events
        public event EventHandler ExportEvent;
        public event EventHandler SearchEvent;
        public event EventHandler ClearAllEvent;

        //Methods
        public void SetBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }

        public void ShowThisUI()
        {
            this.Show();
        }

        public static DisplayElementWorksetView GetInstance(Form parentMdiContainer)
        {
            if (instance is null || instance.IsDisposed)
            {
                instance = new DisplayElementWorksetView();
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

          
            //Export Event
            export_button.Click += delegate
            {
                ExportEvent?.Invoke(this, EventArgs.Empty);
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

        }


    }
}
