using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Adv.Tools.DataAccess.Autodesk.AppStore
{
    [Serializable]
    [DataContract]
    public class EntitlementModel
    {
        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string AppId { get; set; }

        [DataMember]
        public bool IsValid { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
