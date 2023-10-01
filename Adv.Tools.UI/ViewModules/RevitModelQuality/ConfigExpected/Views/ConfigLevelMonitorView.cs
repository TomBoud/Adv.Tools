using Adv.Tools.Abstractions.DbEntities;
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
    public partial class ConfigLevelMonitorView : Form, IConfigLevelMonitorView
    {
        //Singleton
        private static ConfigLevelMonitorView instance;

        //Constructor
        public ConfigLevelMonitorView()
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
            get => discipline_textBox.Text;
            set => discipline_textBox.Text = value;
        }
        public string SourceModelName
        {
            get => sourceName_comboBox.Text;
            set => sourceName_comboBox.Text = value;
        }
        public string SourceModelGuid
        {
            get => sourceGuid_textBox.Text;
            set => sourceGuid_textBox.Text = value;
        }
        public string SearchValue
        {
            get => search_textBox.Text;
            set => search_textBox.Text = value;
        }
        public bool IsEdit { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }

        //Event
        public event EventHandler ModelSelectEvent;
        public event EventHandler SourceSelectEvent;
        public event EventHandler DefaultEvent;
        public event EventHandler ExportEvent;
        public event EventHandler ImportEvent;
        public event EventHandler SearchEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditedEvent;
        public event EventHandler CancelEvent;

        //Methods
        public void SetBindingSource(BindingSource itemsList)
        {
            dataGridView.DataSource = itemsList;
        }
        public void SetDocumentNames(IEnumerable<IExpectedDocument> documents)
        {
            modelName_comboBox.Items.Clear();
            sourceName_comboBox.Items.Clear();

            modelName_comboBox.Items.AddRange(documents.Select(x => x.ModelName).ToArray());
            sourceName_comboBox.Items.AddRange(documents.Select(x => x.ModelName).ToArray());
        }
        public void ShowThisUI()
        {
            this.Show();
        }
        public static ConfigLevelMonitorView GetInstance(Form parentMdiContainer)
        {
            if (instance is null || instance.IsDisposed)
            {
                instance = new ConfigLevelMonitorView();
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

            //Source selection
            sourceName_comboBox.SelectedIndexChanged += delegate
            {
                SourceSelectEvent?.Invoke(this, EventArgs.Empty);
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

                modelName_comboBox.SelectedIndex = -1;
                modelName_comboBox.Enabled = true;
                modelNameComment_label.ForeColor = Color.DarkGreen;
                modelNameComment_label.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                modelNameComment_label.Text = "Selection Required";

                sourceName_comboBox.SelectedIndex = -1;
                sourceNameComment_label.ForeColor = Color.DarkGreen;
                sourceNameComment_label.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                sourceNameComment_label.Text = "Selection Required";

                details_tabPage.Text = "Add New Levels Monitor to a Revit Model";

            };

            //Edit Selected Model
            edit_button.Click += delegate
            {
                EditedEvent?.Invoke(this, EventArgs.Empty);

                if (IsEdit)
                {
                    tabControl.TabPages.Remove(display_tabPage);
                    tabControl.TabPages.Add(details_tabPage);

                    modelName_comboBox.Enabled = false;
                    modelNameComment_label.ForeColor = Color.Black;
                    modelNameComment_label.Font = new Font("Microsoft Sans Serif", 12);
                    modelNameComment_label.Text = "(Read-Only)";

                    sourceNameComment_label.ForeColor = Color.DarkGreen;
                    sourceNameComment_label.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                    sourceNameComment_label.Text = "Selection Required";

                    details_tabPage.Text = "Edit Selected Model Settings";
                }
                else
                {
                    MessageBox.Show(Message);
                }

            };

            //Delete Selected Model
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
        }
    }
}
