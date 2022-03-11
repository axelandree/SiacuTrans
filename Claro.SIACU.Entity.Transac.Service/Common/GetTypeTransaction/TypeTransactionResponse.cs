using System.Collections.Generic;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTypeTransaction
{
    [DataContract]
    public class TypeTransactionResponse
    {
        [DataMember]
        public List<EntitiesFixed.GenericItem> TypeTransaction { get; set; }
    }
}
