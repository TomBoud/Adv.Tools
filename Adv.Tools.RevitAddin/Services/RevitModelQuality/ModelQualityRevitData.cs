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
using System.Windows.Controls;
using Adv.Tools.Abstractions.Database;
using Autodesk.Revit.DB.Electrical;

namespace Adv.Tools.RevitAddin.Services.RevitModelQuality
{
    public class ModelQualityRevitData
    {

        private readonly Document _documnet;
        private readonly IReportModelQuality _report;
        
        public IEnumerable ExistingObjects { get => GetReportDataFromRevitModel(); }
        public IEnumerable ExpectedObjects { get; set; }

        public ModelQualityRevitData(IReportModelQuality report, Document documnet)
        {
            _report = report;
            _documnet = documnet;
        }

        public IEnumerable GetReportDataFromRevitModel()
        {
            if (_report is ElementsWorksetsReport)
            {
                return GetExistingElementsByExpectedCategoryId();
            }
            if (_report is MissingWorksetsReport)
            {
                return GetExistingUserCreatedWorksets();
            }

            return null;
        }

        /// <summary>
        /// Get all the Elements which assosiated with the Allowed Category Ids of IExpectedWorkset
        /// </summary>
        private IEnumerable<IElement> GetExistingElementsByExpectedCategoryId()
        {
            //Cast expected objects to the relevant context
            var expectedWorksets = ExpectedObjects.Cast<IExpectedWorkset>().ToList();
            //Filter workset which are not relevant for the documnet
            var documnetWorksets = expectedWorksets.Where(x => x.ModelGuid.Equals(_documnet.GetCloudModelPath().GetModelGUID().ToString()));
            //Get distinct list of Category Ids
            var distinctCategoryIds = documnetWorksets.Select(x => x.CategoryId).Distinct().ToList();
            //Parse string CategoryIds as ElementIds
            #if REVIT2023 || REVIT2022 || REVIT2021 || REVIT2020
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(int.Parse).Select(id => new ElementId(id)).ToList();
            #else
            ICollection<ElementId> allowedCategories = distinctCategoryIds.Select(long.Parse).Select(id => new ElementId(id)).ToList();
            #endif
            //Query the Revit Documnet Model for the Elements
            var collector = new FilteredElementCollector(_documnet);
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
        private IEnumerable<IWorkset> GetExistingUserCreatedWorksets()
        {
            FilteredWorksetCollector collector = new FilteredWorksetCollector(_documnet);
            var filteredCollection = collector.OfKind(WorksetKind.UserWorkset).ToWorksets();

            foreach (var workset in filteredCollection)
            {
                yield return new RevitWorkset(workset);
            }
        }
    }
}
