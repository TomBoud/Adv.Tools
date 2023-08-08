using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Represents a namesapce which contains all the app specific custom UI Views.
/// </summary>
namespace Adv.Tools.UI.Views
{
    /// <summary>
    /// Wrapper for the UI <see cref="ElementView" /> according to the MVP pattern.
    /// This interaface should include only the view specific properties, events and methods.
    /// </summary>
    public interface IElementView
    {
        //Properties - Fields
        string ElementId { get; set; }
        string ElmentName { get; set; }
        string ElementCategory { get; set; }
        string ElementDocument { get; set; }

        string SerachValue { get; set; }
        string Message { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }

        //Events
        event EventHandler DeleteElementEvent;
        event EventHandler ShowElementsEvent;
        event EventHandler SearchElementEvent;

        //Methods
        void SetElementListBindingSource(BindingSource elementList);
        void ShowFormDialog();
    }
}
