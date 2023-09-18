using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Adv.Tools.Abstractions.Database;
using Adv.Tools.UI.DataModels.RevitModelQuality;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigReports.Models
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
        private string docDiscipline;
        private bool isActive;

        public ConfigReportModel(IReportCheckScore reportCheckScore)
        {
            id = reportCheckScore.Id;
            name = reportCheckScore.CheckName;
            lod = reportCheckScore.CheckLod;
            score = reportCheckScore.CheckScore;
            docName = reportCheckScore.ModelName;
            docGuid = reportCheckScore.ModelGuid;
            docDiscipline= reportCheckScore.Discipline; 
            isActive = reportCheckScore.IsActive;
        }

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

        [DisplayName("Model Discipline")]
        [Required(ErrorMessage = "Revit Model Discipline is required")]
        public string Discipline { get => docDiscipline; set => docDiscipline = value; }

        public bool IsActive { get => isActive; set => isActive = value; }

    }
}
