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
    public class ExpectedGridMonitor : IExpectedGridMonitor
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string sourceModelName;
        private string sourceModelGuid;


        [DisplayName("ID")]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get => id; set => id = value; }

        [DisplayName("Name")]
        public string ModelName { get => modelName; set => modelName = value; }

        [DisplayName("Guid")]
        public string ModelGuid { get => modelGuid; set => modelGuid = value; }

        [DisplayName("Discipline")]
        public string Discipline { get => discipline; set => discipline = value; }

        [DisplayName("Source Name")]
        public string SourceModelName { get => sourceModelName; set => sourceModelName = value; }

        [DisplayName("Source Guid")]
        public string SourceModelGuid { get => sourceModelGuid; set => sourceModelGuid = value; }
    }
}
