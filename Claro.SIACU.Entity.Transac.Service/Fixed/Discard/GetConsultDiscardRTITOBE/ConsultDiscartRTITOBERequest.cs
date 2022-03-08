using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ConsultDiscartRTITOBERequest
    {
        [DataMember]
        public ConsultarDescartesRtiRequest consultarDescartesRtiRequest { get; set; }
    }
}
