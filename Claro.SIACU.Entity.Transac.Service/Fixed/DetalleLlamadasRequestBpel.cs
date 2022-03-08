using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class DetalleLlamadasRequestBpel
    {
        [DataMember]
        public string TipoConsulta { get; set; }
        [DataMember]
        public string CodigoCliente { get; set; }
        [DataMember]
        public string Msisdn { get; set; }
        [DataMember]
        public string FechaInicio { get; set; }
        [DataMember]
        public string FechaFin { get; set; }
        [DataMember]
        public ContactUserBpel ContactUserBpel { get; set; }
        [DataMember]
        public CustomerClfyBpel CustomerClfyBpel { get; set; }
        [DataMember]
        public InteractionBpel InteractionBpel { get; set; }
        [DataMember]
        public InteractionPlusBpel InteractionPlusBpel { get; set; }
        [DataMember]
        public string FlagContingencia { get; set; }
        [DataMember]
        public string TipoProducto { get; set; }
        [DataMember]
        public string Periodo { get; set; }
        [DataMember]
        public string IpCliente { get; set; }
        [DataMember]
        public string FlagEnvioCorreo { get; set; }
        [DataMember]
        public string FlagConstancia { get; set; }
        [DataMember]
        public string FlagGenerarOcc { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
        [DataMember]
        public string TipoConsultaContrato { get; set; }
        [DataMember]
        public string ValorContrato { get; set; }
    }
}
