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

            tabControl.TabPages.Remove(details_tabPage);
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //Properties
        public int Id 
        {
            get => int.Parse(itemId_textBox.Text);
            set => itemId_textBox.Text = value.ToString();
        }
        public string ModelName 
        { 
            get => modelName_comboBox.Text;
            set => modelName_comboBox.Text = value; 
        }
        public string ModelGuid 
        { 
            get => modelGuid_textBox.Text; 
            set => modelGuid_textBox.Text = value; 
        }
        public string Discipline 
        {
            get => discipline_comboBox.Text;
            set => discipline_comboBox.Text = value;
        }
        public string HubId 
        { 
            get => hubId_textBox.Text;
            set => hubId_textBox.SelectedText = value; 
        }
        public string ProjectId 
        { 
            get => projectId_textBox.Text; 
            set=> projectId_textBox.Text = value; 
        }
        public string FolderId 
        { 
            get => folderId_textBox.Text; 
            set => folderId_textBox.Text = value; 
        }
        public string PositionSource { get; set; }
        public string PositionSourceGuid { get; set; }

        public string SearchValue { get => search_textBox.Text; set => search_textBox.Text = value; }
        public bool IsEdit { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        //Events
        public event EventHandler ModelSelectEvent;
        public event EventHandler ExportEvent;
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
            discipline_comboBox.Items.AddRange(Enum.GetValues(typeof(DisciplineType))
                .Cast<DisciplineType>().Select(x => x.ToString()).ToArray());
        }
        public void SetDocumentNames(IEnumerable<IDocument> documents)
        {
            modelName_comboBox.Items.AddRange(documents.Select(x => x.Title).ToArray());
            coordinates_comboBox.Items.AddRange(documents.Select(x => x.Title).ToArray());
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
            //Export settings
            export_button.Click += delegate
            {
                ExportEvent?.Invoke(this, EventArgs.Empty);
            };

            //Document selection
            modelName_comboBox.SelectedIndexChanged += delegate
            {
                ModelSelectEvent?.Invoke(this, EventArgs.Empty);
            };

            //Search for settings
            search_button.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            search_textBox.KeyUp += (s, e) =>
            {
                SearchEvent?.Invoke(this, EventArgs.Empty);
            };

            //Cancel settings changes
            cancel_button.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to abort the changes?", "Question",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result is DialogResult.Yes)
                {
                    CancelEvent?.Invoke(this, EventArgs.Empty);
                    tabControl.TabPages.Remove(details_tabPage);
                    tabControl.TabPages.Add(display_tabPage);
                }
            };

            //Save settings changes
            save_button.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl.TabPages.Remove(details_tabPage);
                    tabControl.TabPages.Add(display_tabPage);
                }
                MessageBox.Show(Message);
            };

            //Add new Model
            add_button.Click += delegate 
            { 
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                
                tabControl.TabPages.Remove(display_tabPage);
                tabControl.TabPages.Add(details_tabPage);
                modelName_comboBox.Enabled = true;

                details_tabPage.Text = "Add New Linked Model";
            };

            //Edit Selected Model
            edit_button.Click += delegate
            {
                EditedEvent?.Invoke(this, EventArgs.Empty);
                
                tabControl.TabPages.Remove(display_tabPage);
                tabControl.TabPages.Add(details_tabPage);
                modelName_comboBox.Enabled = false;

                modelNameComment_label.Text = "(Read-Only)";
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
