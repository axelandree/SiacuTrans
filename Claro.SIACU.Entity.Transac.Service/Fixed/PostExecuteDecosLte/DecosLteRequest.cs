using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostExecuteDecosLte
{
    [DataContract]
    public class DecosLteRequest : Claro.Entity.Request
    {
        [DataMember]
        public InteractionBpel Interaction { get; set; }

        [DataMember]
        public InteractionPlusBpel InsInteractionPlus { get; set; }

        [DataMember]
        public string StrIdSession { get; set; }

        [DataMember]
        public string StrContractId { get; set; }

        [DataMember]
        public string StrCustomerId { get; set; }

        [DataMember]
        public AuditRegister AuditRegister { get; set; }

        [DataMember]
        public DecoCustomer DecoCustomer { get; set; }

        [DataMember]
        public SotPending SotPending { get; set; }

        [DataMember]
        public string FlagConting { get; set; }

        [DataMember]
        public List<LteDecoder> LstDecoders { get; set; }

        [DataMember]
        public RegistrarProcesoPostVentaLte RegistrarProcesoPostVenta { get; set; }

        [DataMember]
        public GenerateConstancy GenerateConstancy { get; set; }

        [DataMember]
        public ImplementLoyalty RealizarFidelizacion { get; set; }

        [DataMember]
        public ImplementOcc RealizarOcc { get; set; }

        [DataMember]
        public string EtaValidation { get; set; }

        [DataMember]
        public RegistrarEtaSeleccion RegistrarEtaSeleccion { get; set; }

        [DataMember]
        public RegistrarEta RegistrarEta { get; set; }
    }
}
