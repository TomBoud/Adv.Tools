using Adv.Tools.Abstractions.Revit;
using Autodesk.Revit.DB;
using System.Linq;

namespace Adv.Tools.RevitAddin.Models
{
    public class RevitFailureMessage : IFailureMessage
    {
        private readonly FailureMessage _failureMessage;

        public RevitFailureMessage(FailureMessage failureMessage)
        {
            _failureMessage = failureMessage;
        }

        public string Description { get => _failureMessage.GetDescriptionText(); set => Description = value; } 
        public int ItemsCount { get => _failureMessage.GetFailingElements().ToArray().Length; set => ItemsCount = value; }
        public string Severity { get => _failureMessage.GetSeverity().ToString(); set => Severity = value; }

        

    }
}
