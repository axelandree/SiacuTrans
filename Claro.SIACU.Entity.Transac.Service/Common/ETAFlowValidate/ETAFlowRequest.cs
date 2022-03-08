using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.ETAFlowValidate
{
    [DataContract(Name = "ETAFlowRequestHfc")]
    public class ETAFlowRequest : Claro.Entity.Request
    {
        [DataMember]
        public string as_origen { get; set; }
        [DataMember]
        public string av_idplano { get; set; }
        [DataMember]
        public string av_ubigeo { get; set; }
        [DataMember]
        public int an_tiptra { get; set; }
        [DataMember]
        public string an_tipsrv { get; set; }
    }
}
