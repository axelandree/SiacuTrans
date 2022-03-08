using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPagOptionXuser
{
    [DataContract(Name = "PagOptionXuserRequest")]
    public class PagOptionXuserRequest : Claro.Entity.Request
    {
        [DataMember]
        public int IntUser { get; set; }
        [DataMember]
        public int IntAplicationCode { get; set; }
    }
}
