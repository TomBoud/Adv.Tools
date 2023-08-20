using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ConfigWorksetModel : IExpectedWorkset
    {
        //Fields
        private int id;
        private string name;
        private string catName;
        private string catId;
        private string docName;
        private string docGuid;
        private string docDisicpline;
        
        //Constructor
        public ConfigWorksetModel(IExpectedWorkset workset)
        {
            id = workset.Id;
            name = workset.WorksetName;
            catName = workset.CategoryName;
            catId = workset.CategoryId;
            docName = workset.ModelName;
            docGuid = workset.ModelGuid;
            docDisicpline = workset.Disicpline;
        }

        //Properties
        [DisplayName("Report ID")]
        [Required(ErrorMessage = "Revit Model Name is required")]
        public int Id { get => id; set => id = value; }
        
        [DisplayName("Model Name")]
        [Required(ErrorMessage = "Revit Model Name is required")]
        public string ModelName { get => docName; set => docName = value; }

        [DisplayName("Model GUID")]
        [Required(ErrorMessage = "Revit Model GUID is required")]
        public string ModelGuid { get => docGuid; set => docGuid = value; }

        [DisplayName("Model Disicpline")]
        [Required(ErrorMessage = "Revit Model Disicpline is required")]
        public string Disicpline { get => docDisicpline; set => docDisicpline = value; }

        [DisplayName("Workset Name")]
        [Required(ErrorMessage = "Workset Name is required")]
        public string WorksetName { get => name; set=> name = value; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get => catName; set=> catName= value; }

        [DisplayName("Category Id")]
        [Required(ErrorMessage = "Category Id is required")]
        public string CategoryId { get => catId; set=> catId = value; }
    }
}
