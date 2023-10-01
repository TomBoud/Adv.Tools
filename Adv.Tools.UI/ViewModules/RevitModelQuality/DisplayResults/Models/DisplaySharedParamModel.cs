using Adv.Tools.Abstractions.DbEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models
{
    public class ReportSharedParameter : IReportSharedParameter
    {
        private int id;
        private string modelGuid;
        private string modelName;
        private string discipline;
        private string parameterName;
        private string guid;
        private bool isFound;
        private string isFoundHeb;

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
        [Required(ErrorMessage = "Revit Model Guid is required")]
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

        public string ParameterName 
        { 
            get => parameterName; 
            set => parameterName = value; 
        }

        public string GUID 
        { 
            get => guid; 
            set => guid = value; 
        }

        public bool IsFound 
        { 
            get => isFound; 
            set => isFound = value; 
        }

        public string IsFoundHeb 
        { 
            get => isFoundHeb; 
            set => isFoundHeb = value; 
        }
    }
}
