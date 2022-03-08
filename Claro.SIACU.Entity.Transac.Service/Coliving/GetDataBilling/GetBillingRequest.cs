using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetBillingRequest
    {
        [DataMember(Name = "csId")]
        public string csId { get; set; }
        [DataMember(Name = "csIdPub")]
        public string csIdPub { get; set; }
        [DataMember(Name = "indicador")]
        public string indicador { get; set; }
        [DataMember(Name = "modo")]
        public string modo { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<ListOptional> listaOpcional { get; set; }
    }
}
