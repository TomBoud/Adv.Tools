using Adv.Tools.UI.Models;
using Adv.Tools.UI.Presenters;
using Adv.Tools.UI.Repositories;
using Adv.Tools.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();

            IElementView view = new ElementView();
            IElementRepo repo = new ElementRepo();

            new ElementPresenter(view, repo);

            Application.Run((Form)view);

        }
    }
}
