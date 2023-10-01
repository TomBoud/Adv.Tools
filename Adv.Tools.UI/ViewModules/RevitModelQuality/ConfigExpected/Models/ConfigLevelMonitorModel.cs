using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ExpectedLevelMonitor : IExpectedLevelsMonitor
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string sourceModelName;
        private string sourceModelGuid;

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

        [DisplayName("Source Name")]
        [Required(ErrorMessage = "Source Model Name is required")]
        public string SourceModelName 
        { 
            get => sourceModelName; 
            set => sourceModelName = value; 
        }

        [DisplayName("Source Guid")]
        [Required(ErrorMessage = "Source Model Guid is required")]
        public string SourceModelGuid 
        { 
            get => sourceModelGuid; 
            set => sourceModelGuid = value; 
        }
    }
}
