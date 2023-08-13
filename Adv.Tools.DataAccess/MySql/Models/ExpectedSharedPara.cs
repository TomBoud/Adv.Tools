
using Adv.Tools.Abstractions.Database;

namespace Adv.Tools.DataAccess.MySql.Models
{
    public class ExpectedSharedPara : IExpectedSharedPara
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelGuid { get; set; }
        public string Disicpline { get; set; }
        public string Parameter { get; set; }
        public string GUID { get; set; }
    }
}
