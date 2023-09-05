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
    public partial class DisplayResultsMainView : Form, IDisplayResultsMainView
    {
        public DisplayResultsMainView()
        {
            InitializeComponent();

            elementsWorksets_button.Click += delegate { ShowDisplayElementWorksetView?.Invoke(this, EventArgs.Empty); };

        }


        public event EventHandler ShowDisplayElementWorksetView;

        public void RunUIApplication()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
