using Adv.Tools.Abstractions.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public class ConfigSharedParamModel : IExpectedSharedPara
    {
        private int id;
        private string modelName;
        private string modelGuid;
        private string discipline;
        private string parameter;
        private string guid;

        public ConfigSharedParamModel(IExpectedSharedPara sharedParam)
        {
            id = sharedParam.Id;
            modelName = sharedParam.ModelName;
            modelGuid = sharedParam.ModelGuid;
            discipline = sharedParam.Discipline;
            parameter = sharedParam.Parameter;
            guid = sharedParam.GUID;
        }

        public int Id { get => id; set => id = value; }
        public string ModelName { get => modelName; set => modelName = value; }
        public string ModelGuid { get => modelGuid; set => modelGuid = value; }
        public string Discipline { get => discipline; set => discipline = value; }
        public string Parameter { get => parameter; set => parameter = value; }
        public string GUID { get => guid; set => guid = value; }
    }
}
