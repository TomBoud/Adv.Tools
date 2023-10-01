using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ExpectedSharedPara : IExpectedSharedPara
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string parameter;
        private string guid;

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
        public string Parameter 
        { 
            get => parameter; 
            set => parameter = value; 
        }
        public string GUID 
        { 
            get => guid; 
            set => guid = value; 
        }
    }
}
