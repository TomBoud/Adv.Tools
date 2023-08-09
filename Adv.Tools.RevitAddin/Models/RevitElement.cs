
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Adv.Tools.Abstractions.Revit;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitElement : IElement
    {
        private readonly Element _element;

        public RevitElement(Element elemant)
        {
            _element = elemant;
        }

        #region Properties
        public string Name { get { return _element.Name; } set { Name = value; } }
        public string LevelName { get { return GetLevelName(); } set { LevelName = value; } }
        public string CategoryName { get { return _element?.Category?.Name ?? string.Empty; } set { CategoryName = value; } }
        public string DocumentName { get { return _element.Document.Title; } set { DocumentName = value; } }

        public long ElementId
        {
            get
            {
#if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
                return Convert.ToInt64(_element.Id.IntegerValue);
#else
                return _element.Id.Value;
#endif
            }
            set
            {
                ElementId = value;
            }
        }
        public long LevelId
        {
            get
            {
#if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
                return Convert.ToInt64(_element.LevelId.IntegerValue);
#else
                return _element.LevelId.Value;
#endif
            }
            set
            {
                ElementId = value;
            }
        }
        
        public Guid Guid { get { return new Guid(_element.UniqueId); } set { Guid = value; } }
        #endregion

        #region Methods
        private string GetLevelName()
        {
            Level level = _element.Document.GetElement(_element.LevelId) as Level;

            if (level is null)
            {
                return string.Empty;
            }
            else
            {
                return level.Name;
            }
        }
        #endregion
    }
}
