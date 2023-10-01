using Adv.Tools.Abstractions.Revit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Adv.Tools.Abstractions.Common
{
    public interface IModelQualityHandler
    {
        IEnumerable<IRevitLinkType> GetRevitLinkTypes();
        IEnumerable GetElementsByExpectedCategoryId(IEnumerable expectedObjects);
        IEnumerable<IWorkset> GetUserCreatedWorksets();
        IEnumerable<IElement> GetLevelsAsElements();
        IEnumerable<IElement> GetGridsAsElements();
        IEnumerable<IFailureMessage> GetDocumentFailureMessages();
    }
}
