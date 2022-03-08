using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSiacParameter
{
    [DataContract(Name = "SiacParameterResponse")]
    public class SiacParameterResponse
    {
        [DataMember]
        public string p_msg_text { get; set; }

        [DataMember]
        public List<SiacParameter> ListSiacParametro { get; set; }
    }
}
