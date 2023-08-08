using Adv.Tools.UI.Models;
using Adv.Tools.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// Represents a namesapce which contains all the Presenters of this UI project
/// </summary>
namespace Adv.Tools.UI.Presenters
{
    /// <summary>
    /// Coupler between <see cref="IElementView" /> and <see cref="IElementRepo" /> for the UI consuming app.
    /// This class is the I/O point for the backend app which consumes the <see cref="ElementView" />.
    /// </summary>
    public class ElementPresenter
    {

        //Fields
        private IElementView view;
        private IElementRepo repo;
        private IEnumerable<ElementModel> elementList;
        private BindingSource elementsBindingSource;

        //Constructor
        public ElementPresenter(IElementView view, IElementRepo repo)
        {
            // Couple the view & repo interfaces
            this.view = view;
            this.repo = repo;
            // Define Data Source (or inheret from an argument)
            this.elementsBindingSource = new BindingSource();
            // Subscribe event methods to the view event handlers
            this.view.SearchElementEvent += SearchElement;
            this.view.DeleteElementEvent += DeleteElement;
            this.view.ShowElementsEvent += ShowElemets;
            // Set the View Data Source
            this.view.SetElementListBindingSource(elementsBindingSource);
            // Load Element list view
            LoadAllElementList();
        }

        // Methods
        private void LoadAllElementList()
        {
            // Get data from the Repository
            elementList = repo.GetAll();
            // Forward the Data to the View
            elementsBindingSource.DataSource = elementList;
        }

        private void SearchElement(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(view.SerachValue);
            if (emptyValue is false)
            {
                elementList = repo.GetByValue(view.SerachValue);
            }
            else
            {
                elementList = repo.GetAll();
            }

            elementsBindingSource.DataSource = elementList;
        }

        private void ShowElemets(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DeleteElement(object sender, EventArgs e)
        {
            try
            {
                var element = (ElementModel)elementsBindingSource.Current;
                repo.Delete(element);
                view.IsSuccessful = true;
                view.Message = "Element delete operation accepted";
                repo.GetAll();
            }
            catch (Exception)
            {
                view.IsSuccessful = false;
                view.Message = "An error ocured, coud not register delete operation";
            }

        }


    }
}
