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
            worksets_button.Click += delegate { ShowConfigWorksetsView?.Invoke(this, EventArgs.Empty); };
        }

        public event EventHandler ShowConfigDocumentsView;
        public event EventHandler ShowConfigWorksetsView;
        public event EventHandler ShowConfigLevelsGridsView;
        public event EventHandler ShowConfigMidpSheetsView;
        public event EventHandler ShowConfigProjectInfoView;
        public event EventHandler ShowConfigSharedParaView;
        public event EventHandler ShowConfigSiteLocationView;
        public event EventHandler ShowConfigTidpCodeView;

        public void RunUIApplication()
        {
            Application.EnableVisualStyles();
            Application.Run(this);
        }
    }
}
