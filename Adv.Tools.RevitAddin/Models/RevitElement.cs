
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

        public RevitElement(Element elemant)
        {
            _element = elemant;
        }

        #region Public Properties
        public string Name { get { return _element.Name; } set { Name = value; } }
        public string LevelName { get { return GetLevelName(); } set { LevelName = value; } }
        public string CategoryName { get { return _element?.Category?.Name ?? string.Empty; } set { CategoryName = value; } }
        public string DocumentName { get { return _element.Document.Title; } set { DocumentName = value; } }
        public long CategoryId { get => GetCategoryId(); set { CategoryId = value; } }
        public string WorksetName { get { return _element.get_Parameter(BuiltInParameter.ELEM_PARTITION_PARAM).AsValueString(); } set { WorksetName = value; } }

        public long ElementId { get => GetElementId(); set { ElementId = value; } }
        public long LevelId  { get => GetLevelId(); set  { ElementId = value; } }

        public bool IsMonitoring { get => _element.IsMonitoringLinkElement(); set => IsMonitoring = value; }
        public long MonitoredId { get => GetMonitoredLinkElementIds(); set => MonitoredId = value; }
        public IDocument MonitoredDoc { get => GetMonitoredDocumnet(); set=> MonitoredDoc = value; }
        #endregion


        #region Provate Methods
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
        private IDocument GetMonitoredDocumnet()
        {
            //Skip the search if there is no active monitor
            if(_element.IsMonitoringLinkElement())
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
    }
}
