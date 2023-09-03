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
        event EventHandler ShowConfigLevelsMonitorView;
        event EventHandler ShowConfigGridsMonitorView;
        event EventHandler ShowConfigProjectInfoView;
        event EventHandler ShowConfigSharedParaView;

        void RunUIApplication();
    }
}
