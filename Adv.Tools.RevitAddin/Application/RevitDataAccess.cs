using Adv.Tools.Abstractions.Common;
using Adv.Tools.Abstractions.Revit;
using System;
using System.Collections;
using System.Collections.Generic;
using Adv.Tools.RevitAddin.Models;
using Autodesk.Revit.DB;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adv.Tools.Abstractions.DbEntities;

namespace Adv.Tools.RevitAddin.Application
{
    public class RevitDataAccess : IRvtDataAccess
    {
        private readonly Document _document;

        public RevitDataAccess(Document document)
        {
            _document = document;
        }

        public static List<Document> GetLinedRevitModels(DocumentSet documentSet)
        {
            var result = new List<Document>();

            foreach (Document linkedModel in documentSet)
            {
                if (linkedModel.IsLinked)
                {
                    result.Add(linkedModel);
                }
            }

            return result;
        }


        /// <summary>
        /// Get all the Elements which associated with the Allowed Category Ids of IExpectedWorkset
        /// </summary>
        public IEnumerable GetElementsByExpectedCategoryId(IEnumerable DbDataObjects)
        {
            //Cast expected objects to the relevant context
            var expectedWorksets = DbDataObjects.OfType<IExpectedWorkset>().ToList();
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
            for (int i = 0; i < collection.Length; i++)
            {
                yield return new RevitElement(collection[i]);
            }
        }

        /// <summary>
        /// Get all User defined worksets in the Revit Model
        /// </summary>
        public IEnumerable<IWorkset> GetUserCreatedWorksets()
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
        public IEnumerable<IRevitLinkType> GetRevitLinkTypes()
        {
            var results = new List<IRevitLinkType>();

            ICollection<ElementId> colls = ExternalFileUtils.GetAllExternalFileReferences(_document);
            if (colls.Count.Equals(0)) { return results; }

            foreach (var id in colls)
            {
                Element ele = _document.GetElement(id);
                if (ele is null)
                {
                    continue;
                }
                if (ele is RevitLinkType)
                {
                    var file = ele as RevitLinkType;
                    results.Add(new RevitLinkTypeFile(file));
                }
            }
            return results;
        }

        /// <summary>
        /// Get  All Levels in the Revit Model as IElement
        /// </summary>
        public IEnumerable<IElement> GetLevelsAsElements()
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
        public IEnumerable<IElement> GetGridsAsElements()
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
        public IEnumerable<IFailureMessage> GetDocumentFailureMessages()
        {
            var warnings = _document.GetWarnings().ToList();

            foreach (var warning in warnings)
            {
                yield return new RevitFailureMessage(warning);
            }

        }
    }
}
