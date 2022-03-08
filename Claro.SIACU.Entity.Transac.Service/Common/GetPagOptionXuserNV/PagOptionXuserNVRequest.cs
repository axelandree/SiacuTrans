using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPagOptionXuserNV
{
    [DataContract(Name = "PagOptionXuserNVRequest")] 
    public class PagOptionXuserNVRequest :  Claro.Entity.Request
    {
        [DataMember]
        public int IntUser { get; set; }
        [DataMember]
        public int IntAplicationCode { get; set; }
    }
}
