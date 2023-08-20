using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    public interface IConfigExpectedMainView
    {
        event EventHandler ShowConfigDocumentsView;
        event EventHandler ShowConfigWorksetsView;
        event EventHandler ShowConfigLevelsGridsView;
        event EventHandler ShowConfigMidpSheetsView;
        event EventHandler ShowConfigProjectInfoView;
        event EventHandler ShowConfigSharedParaView;
        event EventHandler ShowConfigSiteLocationView;
        event EventHandler ShowConfigTidpCodeView;

        void RunUIApplication();
    }
}
