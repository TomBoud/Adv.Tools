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
    public partial class ConfigExpectedMainView : Form, IConfigExpectedMainView
    {
        public ConfigExpectedMainView()
        {
            InitializeComponent();

            models_button.Click += delegate { ShowConfigDocumentsView?.Invoke(this, EventArgs.Empty); };
            worksets_button.Click += delegate { ShowConfigWorksetsView?.Invoke(this, EventArgs.Empty); };
            grids_button.Click += delegate { ShowConfigGridsMonitorView?.Invoke(this, EventArgs.Empty); };
            levels_button.Click += delegate { ShowConfigLevelsMonitorView?.Invoke(this, EventArgs.Empty); };
            projectInfo_button.Click += delegate { ShowConfigProjectInfoView?.Invoke(this, EventArgs.Empty); };
            sharedParams_button.Click += delegate { ShowConfigSharedParaView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowConfigDocumentsView;
        public event EventHandler ShowConfigWorksetsView;
        public event EventHandler ShowConfigLevelsMonitorView;
        public event EventHandler ShowConfigGridsMonitorView;
        public event EventHandler ShowConfigProjectInfoView;
        public event EventHandler ShowConfigSharedParaView;

        public void RunUIApplication()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
