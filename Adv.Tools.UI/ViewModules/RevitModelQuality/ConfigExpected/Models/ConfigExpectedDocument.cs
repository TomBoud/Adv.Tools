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
    public class ExpectedDocument : IExpectedDocument
    {
        //Fields
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string hubId;
        private string projectId;
        private string folderId;
        private string positionSource;

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

        [DisplayName("Hub Id")]
        public string HubId 
        { 
            get => hubId; 
            set => hubId = value; 
        }

        [DisplayName("Project Id")]
        public string ProjectId 
        { 
            get => projectId; 
            set => projectId = value; 
        }

        [DisplayName("Folder Id")]
        public string FolderId 
        { 
            get => folderId; 
            set => folderId = value; 
        }

        [DisplayName("URS Name")]
        [Required(ErrorMessage = "Source URS Model Name is required")]
        public string PositionSource 
        { 
            get => positionSource; 
            set => positionSource = value; 
        }
    }
}
