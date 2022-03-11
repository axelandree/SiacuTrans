using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetPagOptionXuserNV
{
    [DataContract(Name = "PagOptionXuserNVResponse")]
    public class PagOptionXuserNVResponse
    {
        [DataMember]
        public string ErrMessage { get; set; }
        [DataMember]
        public string CodeErr { get; set; }
        [DataMember]
        public List<ConsultSecurity> ListConsultSecurity { get; set; }
    }
}
