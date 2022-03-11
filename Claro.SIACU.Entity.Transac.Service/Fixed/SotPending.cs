using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class SotPending
    {
        [DataMember]
        public string StrCoId { get; set; }
        [DataMember]
        public string StrTipTra { get; set; }
    }
}
