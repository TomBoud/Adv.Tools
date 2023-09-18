using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Views
{
    public interface IDisplayResultsMainView
    {
        event EventHandler ShowDisplayElementWorksetView;
        event EventHandler ShowDisplayElementWorksetsView;

        void RunUIApplication();
    }
}
