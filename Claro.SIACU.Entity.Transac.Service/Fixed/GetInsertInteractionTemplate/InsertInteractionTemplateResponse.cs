using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractionTemplate
{
    [DataContract]
    public class GetInsertInteractionTemplateResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
        [DataMember]
        public string rCodigoRetornoTransaccion { get; set; }
        [DataMember]
        public string rMensajeErrorTransaccion { get; set; }
    }
}
