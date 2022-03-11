using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetPagOptionXuser
{
    [DataContract(Name = "PagOptionXuserResponse")]
    public class PagOptionXuserResponse
    {
        [DataMember]
        public string ErrMessage { get; set; }
        [DataMember]
        public string CodeErr { get; set; }
        [DataMember]
        public List<ConsultSecurity> ListConsultSecurity { get; set; }
    }
}
