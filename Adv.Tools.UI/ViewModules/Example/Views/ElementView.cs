using Adv.Tools.UI.Example.Presenters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Represents a namesapce which contains all the app specific UI Views.
/// </summary>
namespace Adv.Tools.UI.Example.Views
{
    /// <summary>
    /// Win-Form UI code implementation of the <see cref="IElementView"/>
    /// </summary>
    public partial class ElementView : Form, IElementView
    {

        // This View Fields
        private bool isEdit;
        private bool isSuccessful;
        private string message;
        private string elementDocument;
        private string elementCategory;
        private string elementName;
        private string elementId;

        // This View Constructor
        public ElementView()
        {
            InitializeComponent();
            // Method to delegate View events to cutsom handlers
            AssociateAndRaiseViewEvents();
        }

        /// <summary>
        /// This Method controls the behavior of the form for each event
        /// </summary>
        private void AssociateAndRaiseViewEvents()
        {
            // Show
            Show_btn.Click += delegate { ShowElementsEvent?.Invoke(this, EventArgs.Empty); };
            // Delete
            Delete_btn.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected element(s)?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteElementEvent?.Invoke(this, EventArgs.Empty);
                }
            };
            // Search
            Search_btn.Click += delegate { SearchElementEvent?.Invoke(this, EventArgs.Empty); };
            SearchElement_txb.KeyUp += (s, e) =>
            {
                SearchElementEvent?.Invoke(this, EventArgs.Empty);
            };

        }

        // This View interface Properties implementation
        public string ElementId { get { return elementId; } set { elementId = value; } }
        public string ElmentName { get { return elementName; } set { elementName = value; } }
        public string ElementCategory { get { return elementCategory; } set { elementCategory = value; } }
        public string ElementDocument { get { return elementDocument; } set { elementDocument = value; } }
        public string SerachValue { get { return SearchElement_txb.Text; } set { SearchElement_txb.Text = value; } }
        public string Message { get { return message; } set { message = value; } }
        public bool IsEdit { get { return isEdit; } set { isEdit = value; } }
        public bool IsSuccessful { get { return isSuccessful; } set { isSuccessful = value; } }

        // This View interface Events implementation
        public event EventHandler DeleteElementEvent;
        public event EventHandler ShowElementsEvent;
        public event EventHandler SearchElementEvent;

        // This View interface Methods implementation
        public void SetElementListBindingSource(BindingSource elementList)
        {
            ElementDisplay_dgv.DataSource = elementList;
        }
        public void ShowFormDialog()
        {
            this.ShowDialog();
        }
    }
}
