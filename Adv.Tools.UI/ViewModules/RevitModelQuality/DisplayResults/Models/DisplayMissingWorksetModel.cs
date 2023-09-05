using Adv.Tools.Abstractions.Database;
using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models
{
    public class ReportMissingWorkset : IReportMissingWorkset
    {

        
        //Fields
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string worksetName;
        private string objectId;
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

        public string WorksetName 
        {
            get => worksetName;
            set => worksetName = value;
        }
        
        public string ObjectId 
        {
            get => objectId;
            set => objectId = value;
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
