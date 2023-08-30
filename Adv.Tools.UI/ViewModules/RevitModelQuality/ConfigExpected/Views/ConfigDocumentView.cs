using Adv.Tools.Abstractions.Enums;
using Adv.Tools.Abstractions.Revit;
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

            tabControl1.TabPages.Remove(details_tabPage);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //Properties
        public int Id { get; set; }
        public string ModelName { get => modelName_textBox.Text; set => modelName_textBox.Text = value; }
        public string ModelGuid { get => modelGuid_textBox.Text; set => modelGuid_textBox.Text = value; }
        public string Discipline { get; set; }
        public string HubId { get => hubId_textBox.Text; set => hubId_textBox.Text = value; }
        public string ProjectId { get => projectId_textBox.Text; set=> projectId_textBox.Text = value; }
        public string FolderId { get => folderId_textBox.Text; set => folderId_textBox.Text = value; }
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
        public event EventHandler CancelEvent;
        public event EventHandler SaveEvent;

        //Methods

        public void SetBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }
        public void SetDisciplineTypes()
        {
            discipline_comboBox.DataSource = Enum.GetValues(typeof(DisciplineType)).Cast<DisciplineType>().Select(x => x.ToString()).ToArray();
            discipline_comboBox.SelectedIndex = -1;
        }
        public void SetDocumentNames(IEnumerable<IDocument> documents)
        {
            coordinates_comboBox.DataSource = documents.Select(x => x.Title).ToArray();
            coordinates_comboBox.SelectedIndex = -1;
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
            //Search for settings
            search_button.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            search_textBox.KeyUp += (s, e) =>
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //Save Settings Changes
            save_button.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Remove(details_tabPage);
                    tabControl1.TabPages.Add(display_tabPage);
                }
                MessageBox.Show(Message);
            };

            //Add new Model
            add_button.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(display_tabPage);
                tabControl1.TabPages.Add(details_tabPage);
                details_tabPage.Text = "Add New Linked Model";
            };

            //Edit Selected Model
            edit_button.Click += delegate
            {
                EditedEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(display_tabPage);
                tabControl1.TabPages.Add(details_tabPage);
                details_tabPage.Text = "Edit Selected Model Settings";
            };

            //Delete Selected Model
            delete_button.Click += delegate 
            { 
                var result = MessageBox.Show("Are you sure you want to delete the selected?","Warning",
                    MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
                
                if(result is DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }

            };
        }

        
    }
}
