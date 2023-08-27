using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ConfigCleanViewModel : IExpectedCleanView
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string viewType;
        private string viewName;

        public ConfigCleanViewModel(IExpectedCleanView cleanView)
        {
            cleanView.Id = id;
            cleanView.ModelName = modelName;
            cleanView.ModelGuid = modelGuid;
            cleanView.Discipline = discipline;
            cleanView.ViewType = viewType;
            cleanView.ViewName = viewName;
        }

        public int Id { get => id; set => id = value; }
        public string ModelName { get => modelName; set => modelName = value; }
        public string ModelGuid { get => modelGuid; set => modelGuid = value; }
        public string Discipline { get => discipline; set => discipline = value; }
        public string ViewType { get => viewType; set => viewType = value; }
        public string ViewName { get => viewName; set => viewName = value; }
    }
}
