using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ConfigProjectInfoModel : IExpectedProjectInfo
    {

        //Fields
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string parameter;
        private string value;

        //Constructor
        public ConfigProjectInfoModel(IExpectedProjectInfo projectInfo)
        {
            modelName = projectInfo.ModelName;
            modelGuid = projectInfo.ModelGuid;
            discipline = projectInfo.Discipline;
            parameter = projectInfo.Parameter;
            value = projectInfo.Value;
        }

        //Properties
        [DisplayName("ID")]
        [Required(ErrorMessage = "Id is required")]
        public int Id { get => id; set => id = value; }

        [DisplayName("Name")]
        public string ModelName { get => modelName; set => modelName = value; }

        [DisplayName("Guid")]
        public string ModelGuid { get => modelGuid; set => modelGuid = value; }

        [DisplayName("Discipline")]
        public string Discipline { get => discipline; set => discipline = value; }

        [DisplayName("Parameter")]
        public string Parameter { get => parameter; set => parameter = value; }

        [DisplayName("Value")]
        public string Value { get => value; set => this.value = value; }
    }
}
