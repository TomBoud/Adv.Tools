using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.DisplayResults.Models
{
    public class ReportElementsWorkset : IReportElementsWorkset
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string objectName;
        private string objectCategory;
        private string objectId;

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

        public string ObjectName 
        { 
            get => objectName;
            set => objectName = value;
        }

        public string ObjectCategory 
        { 
            get => objectCategory; 
            set => objectCategory = value;
        }

        public string ObjectId 
        { 
            get => objectId; 
            set => objectId = value;
        }
    }
}
