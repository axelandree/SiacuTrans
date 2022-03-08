using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetListItem
{
    [DataContract(Name = "ListItemRequest")]
    public class ListItemRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strNameFunction { get; set; }
        [DataMember]
        public string strFlagCode { get; set; }
        [DataMember]
        public string fileName { get; set; }
    }
}
