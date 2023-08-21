﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Views
{
    public interface IConfigWorksetView
    {
        //Properties - Fields
        int Id { get; set; }
        string ModelName { get; set; }
        string ModelGuid { get; set; }
        string Disicpline { get; set; }
        string WorksetName { get; set; }
        string CategoryName { get; set; }
        string CategoryId { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Event
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditedEvent;
        event EventHandler DeleteEvent;

        //Methods
        void SetPetListBindingSource(BindingSource reportsList);
    }
}