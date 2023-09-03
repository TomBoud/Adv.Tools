using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ExpectedProjectInfo : IExpectedProjectInfo
    {

        //Fields
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string parameter;
        private string value;


        //Properties
        [DisplayName("ID")]
        [Required(ErrorMessage = "Id is required")]
        public int Id
        {
            get => id;
            set => id = value;
        }
        [DisplayName("Model Name")]
        [Required(ErrorMessage = "Revit Model Name is required")]
        public string ModelName
        {
            get => modelName;
            set => modelName = value;
        }
        [DisplayName("Model GUID")]
        [Required(ErrorMessage = "Revit Model GUID is required")]
        public string ModelGuid
        {
            get => modelGuid;
            set => modelGuid = value;
        }
        [DisplayName("Model Discipline")]
        [Required(ErrorMessage = "Revit Model Discipline is required")]
        public string Discipline
        {
            get => discipline;
            set => discipline = value;
        }
        [DisplayName("Information Type")]
        [Required(ErrorMessage = "Parameter Name is required")]
        public string Parameter 
        { 
            get => parameter; 
            set => parameter = value; 
        }
        [DisplayName("Information Value")]
        [Required(ErrorMessage = "Parameter Value is required")]
        public string Value 
        { 
            get => value; 
            set => this.value = value; 
        }
    }
}
