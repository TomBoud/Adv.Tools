using Adv.Tools.CoreLogic.RevitModelQuality;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using Adv.Tools.CoreLogic.RevitModelQuality.Reports;
using Adv.Tools.RevitAddin.Models;
using System.Collections;
using Adv.Tools.Abstractions.Revit;
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityRevitData
    {

        private readonly Document _document;
        private readonly IReportModelQuality _report;
        private readonly IEnumerable _expected;
        
        public IEnumerable ExistingObjects { get => GetReportDataFromRevitModel(); }

        public ModelQualityRevitData(IReportModelQuality report, Document document, IEnumerable expectedObjects)
        {
            _report = report;
            _document = document;
            _expected = expectedObjects;
        }

        /// <summary>
        /// Mapping function for data query based on the Report type
        /// </summary>
        public IEnumerable GetReportDataFromRevitModel()
        {
            
            if (_report is ElementsWorksetsReport)
            {
                return GetElementsByExpectedCategoryId();
            }
            if (_report is MissingWorksetsReport)
            {
                return GetUserCreatedWorksets();
            }
            if (_report is FileReferenceReport)
            {
                return GetRevitLinkTypes();
            }
            if (_report is LevelsMonitorReport)
            {
                return GetLevelsAsElements();
            }
            if (_report is GridsMonitorReport)
            {
                return GetGridsAsElements();
            }
            if(_report is ProjectWarningReport)
            {
                return GetDocumnetFailureMessages();
            }

            return null;
        }
       
        /// <summary>
        /// Get all the Elements which associated with the Allowed Category Ids of IExpectedWorkset
        /// </summary>
        private IEnumerable<IElement> GetElementsByExpectedCategoryId()
        {
            //Cast expected objects to the relevant context
            var expectedWorksets = _expected.Cast<IExpectedWorkset>().ToList();
            //Filter workset which are not relevant for the document
            var documnetWorksets = expectedWorksets.Where(x => x.ModelGuid.Equals(_document.GetCloudModelPath().GetModelGUID().ToString()));
            //Get distinct list of Category Ids
            var distinctCategoryIds = documnetWorksets.Select(x => x.CategoryId).Distinct().ToList();
            //Parse string CategoryIds as ElementIds
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(int.Parse).Select(id => new ElementId(id)).ToList();
            #else
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(long.Parse).Select(id => new ElementId(id)).ToList();
            #endif
            //Query the Revit Documnet Model for the Elements
            var collector = new FilteredElementCollector(_document);
            var multicategoryfilter = new ElementMulticategoryFilter(allowedCategories);
            var collection = collector.WherePasses(multicategoryfilter).WhereElementIsNotElementType().ToElements().ToArray();
            //Yield Return the elements
            for(int i=0; i<collection.Length;i++)
            {
                yield return new RevitElement(collection[i]);
            }
        }
       
        /// <summary>
        /// Get all User defined worksets in the Revit Model
        /// </summary>
        private IEnumerable<IWorkset> GetUserCreatedWorksets()
        {
            var collector = new FilteredWorksetCollector(_document);
            var filteredCollection = collector.OfKind(WorksetKind.UserWorkset).ToWorksets();

            foreach (var workset in filteredCollection)
            {
                yield return new RevitWorkset(workset);
            }
        }
      
        /// <summary>
        /// Get all RevitLinkType file References in the Revit Model
        /// </summary>
        private IEnumerable<IRevitLinkType> GetRevitLinkTypes()
        {
            var collector = new FilteredElementCollector(_document);
            var linkTypes = collector.WhereElementIsNotElementType()
                .OfClass(typeof(RevitLinkType)).Cast<RevitLinkType>().ToList();

            foreach (var file in linkTypes)
            {
                yield return new RevitLinkTypeFile(file);
            }

        }

        /// <summary>
        /// Get  All Levels in the Revit Model as IElement
        /// </summary>
        private IEnumerable<IElement> GetLevelsAsElements()
        {

            var collector = new FilteredElementCollector(_document);
            var levels = collector.WhereElementIsNotElementType().OfClass(typeof(Level)).ToList();

            foreach (var level in levels)
            {
                yield return new RevitElement(level);
            }

        }
        
        /// <summary>
        /// Get All Grids in the Revit Model
        /// </summary>
        private IEnumerable<IElement> GetGridsAsElements()
        {

            var collector = new FilteredElementCollector(_document);
            var grids = collector.WhereElementIsNotElementType().OfClass(typeof(Grid)).ToList();

            foreach (var grid in grids)
            {
                yield return new RevitElement(grid);
            }

        }

        /// <summary>
        /// Get All FailureMessages in the Revit Model
        /// </summary>
        private IEnumerable<IFailureMessage> GetDocumnetFailureMessages()
        {
            var warnings = _document.GetWarnings().ToList();

            foreach(var warning in warnings)
            {
                yield return new RevitFailureMessage(warning);
            }

        }
    }
}
