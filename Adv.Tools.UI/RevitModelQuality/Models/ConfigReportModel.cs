using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.UI.RevitModelQuality.Models
{
    public class ConfigReportModel : IReportCheckScore
    {
        //Fields
        private int id;
        private string name;
        private string lod;
        private string score;
        private string docName;
        private string docGuid;
        private string docDisicpline;

        //Properties
        [DisplayName("Report ID")]
        public int Id { get => id; set => id = value; }
        
        [DisplayName("Report Name")]
        [Required(ErrorMessage = "Report Name is required")]
        public string CheckName { get => name; set => name = value; }

        [DisplayName("Report LOD")]
        [Required(ErrorMessage = "Report LOD Type is required")]
        public string CheckLod { get => lod; set => lod = value; }
        
        [DisplayName("Report Score")]
        public string CheckScore { get => score; set => score = value; }
        
        [DisplayName("Model Name")]
        [Required(ErrorMessage = "Revit Model Name is required")]
        public string ModelName { get => docName; set => docName = value; }

        [DisplayName("Model GUID")]
        [Required(ErrorMessage = "Revit Model GUID is required")]
        public string ModelGuid { get => docGuid; set => docGuid = value; }

        [DisplayName("Model Disicpline")]
        [Required(ErrorMessage = "Revit Model Disicpline is required")]
        public string Disicpline { get => docDisicpline; set => docDisicpline = value; }
      
    }
}
