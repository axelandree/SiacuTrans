using System.Collections.Generic;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReceipts
{
    [DataContract]
    public class ReceiptsResponse
    {
        [DataMember]
        public List<EntitiesFixed.GenericItem> LstReceipts { get; set; }
        [DataMember]
        public string MSG_ERROR { get; set; }
    }
}
