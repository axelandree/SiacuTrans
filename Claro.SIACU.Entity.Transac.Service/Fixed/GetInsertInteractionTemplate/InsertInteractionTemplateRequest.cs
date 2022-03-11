using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractionTemplate
{
    [DataContract]
    public class GetInsertInteractionTemplateRequest : Claro.Entity.Request
    {
        [DataMember]
        public InteractionTemplate InteractionTemplate { get; set; }
        [DataMember]
        public string vInteraccionId { get; set; }
        [DataMember]
        public string vNroTelefono { get; set; }
        [DataMember]
        public string vUSUARIO_SISTEMA { get; set; }
        [DataMember]
        public string vUSUARIO_APLICACION { get; set; }
        [DataMember]
        public string vPASSWORD_USUARIO { get; set; }
        [DataMember]
        public bool vEjecutarTransaccion { get; set; }
    }
}
