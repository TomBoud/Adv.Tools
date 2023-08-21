﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.UI.ViewModules.RevitModelQuality.ConfigExpected.Models
{
    public interface IConfigWorksetRepo
    {
        void Add(ConfigWorksetModel configWorkset);
        void Edit(ConfigWorksetModel configWorkset);
        void Delete(int id);
        IEnumerable<ConfigWorksetModel> GetAllWorksets();
        IEnumerable<ConfigWorksetModel> GetByValue(string value);
    }
}