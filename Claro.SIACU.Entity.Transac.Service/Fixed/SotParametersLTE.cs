using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "SotParametersLTE")]
    public class SotParametersLTE
    {
        [DataMember]
        public string strTramaCab { get; set; }
        [DataMember]
        public string strLstTipEqu { get; set; }
        [DataMember]
        public string strLstCoser { get; set; }
        [DataMember]
        public string strLstSnCode { get; set; }
        [DataMember]
        public string strLstSpCode { get; set; }
        [DataMember]
        public string strTramaBody { get; set; }
        [DataMember]
        public string idProcess { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string customerId { get; set; }
    }
}
