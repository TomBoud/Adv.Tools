
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Adv.Tools.Abstractions.Revit;
using System.Windows.Controls;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitElement : IElement
    {
        private readonly Element _element;

        #region Private Fields
        private string _name;
        private string _levelName;
        private string _categoryName;
        private string _documentName;
        private long _categoryId;
        private string _worksetName;
        private long _elementId;
        private long _levelId;
        private bool _isMonitoring;
        private long _monitoredId;
        private IDocument _monitoredDoc;
        #endregion
        
        public RevitElement() { }
        public RevitElement(Element element)
        {
            _element = element;
            _name = element.Name;
            _levelName = GetLevelName();
            _categoryName = element?.Category?.Name ?? string.Empty;
            _documentName = element.Document.Title;
            _categoryId = GetCategoryId();
            _worksetName = element.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM).AsValueString();
            _elementId = GetElementId();
            _levelId = GetLevelId();
            _isMonitoring = element.IsMonitoringLinkElement();
            _monitoredId = GetMonitoredLinkElementIds();
            _monitoredDoc = GetMonitoredDocument();
        }

        #region Private Methods
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
        private long GetElementId()
        {

            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            return Convert.ToInt64(_element.Id.IntegerValue);
            #else

            return _element.Id.Value;
            #endif

        }
        private long GetLevelId()
        {
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            return Convert.ToInt64(_element.LevelId.IntegerValue);
            #else
            return _element.LevelId.Value;
            #endif
        }
        private long GetCategoryId()
        {
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            return Convert.ToInt64(_element.Category.Id.IntegerValue);
            #else
            return _element.Category.Id.Value;
            #endif
        }
        private long GetMonitoredLinkElementIds()
        {
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            return Convert.ToInt64(_element.GetMonitoredLinkElementIds().FirstOrDefault().IntegerValue);
            #else
            return _element.GetMonitoredLinkElementIds().FirstOrDefault().Value;
            #endif
        }
        private IDocument GetMonitoredDocument()
        {
            //Skip the search if there is no active monitor
            if (_element.IsMonitoringLinkElement())
            {
                //Get the first monitored element Id
                var MonitoredElementId = _element.GetMonitoredLinkElementIds().FirstOrDefault();
                if (MonitoredElementId is null) { return null; }

                //Get collection of all external file references of elemet documnet
                var FileReferenceIds = ExternalFileUtils.GetAllExternalFileReferences(_element.Document);
                if (FileReferenceIds.Count.Equals(0)) { return null; }

                //Search for the monitored document
                foreach (var id in FileReferenceIds)
                {
                    //Try to get the file as an element from the document
                    var file = _element.Document.GetElement(id);
                    if (file is null) { continue; }

                    //Try to cast the file as RevitLinkType object
                    var rvtLinkType = file as RevitLinkType;
                    if (rvtLinkType is null) { continue; }

                    //Try to get the monitored element from the casted documnet
                    var MonitoredElement = rvtLinkType.Document.GetElement(MonitoredElementId);
                    if (MonitoredElement is null) { continue; }

                    //If the element is a valid object return the document 
                    return new RevitDocument(MonitoredElement.Document);
                }
            }

            return null;
        }
        #endregion

        #region Public Properties
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string LevelName
        {
            get => _levelName;
            set => _levelName = value;
        }

        public string CategoryName
        {
            get => _categoryName;
            set => _categoryName = value;
        }

        public string DocumentName
        {
            get => _documentName;
            set => _documentName = value;
        }

        public long CategoryId
        {
            get => _categoryId;
            set => _categoryId = value;
        }

        public string WorksetName
        {
            get => _worksetName;
            set => _worksetName = value;
        }

        public long ElementId
        {
            get => _elementId;
            set => _elementId = value;
        }

        public long LevelId
        {
            get => _levelId;
            set => _levelId = value;
        }

        public bool IsMonitoring
        {
            get => _isMonitoring;
            set => _isMonitoring = value;
        }

        public long MonitoredId
        {
            get => _monitoredId;
            set => _monitoredId = value;
        }

        public IDocument MonitoredDoc
        {
            get => _monitoredDoc;
            set => _monitoredDoc = value;
        }
        #endregion

    }
}
