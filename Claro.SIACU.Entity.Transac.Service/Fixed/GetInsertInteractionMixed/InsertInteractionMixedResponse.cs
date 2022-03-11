using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractionMixed
{
    [DataContract]
    public class GetInsertInteractionMixedResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rInteraccionId { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public string rFlagInsercionInteraccion { get; set; }
        [DataMember]
        public string rMsgTextInteraccion { get; set; }
        [DataMember]
        public string rCodigoRetornoTransaccion { get; set; }
        [DataMember]
        public string rMsgTextTransaccion { get; set; }
    }
}
