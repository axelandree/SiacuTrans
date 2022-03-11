using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ConsultDiscartRTITOBERequestGrupo
    {
        [DataMember]
        public ConsultarDescartesRtiRequestGrupo consultarDescartesRtiRequest { get; set; }
    }
}
