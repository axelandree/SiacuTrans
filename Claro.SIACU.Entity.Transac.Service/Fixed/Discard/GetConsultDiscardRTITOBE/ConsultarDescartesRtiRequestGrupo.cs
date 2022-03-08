using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ConsultarDescartesRtiRequestGrupo
    {
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string coIdPub { get; set; }
        [DataMember]
        public string msisdn { get; set; }
        [DataMember]
        public string tipoLinea { get; set; }
        [DataMember]
        public string customerId { get; set; }
        [DataMember]
        public string flagConvivencia { get; set; }
        [DataMember]
        public string grupoDescarte { get; set; }
        [DataMember]
        public List<ItemOpcional> listaOpcional { get; set; }
    }
}
