using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterSARRequest : Claro.Entity.Request
    {
        [DataMember]
        public RegisterCaseType RegistrarCaso { get; set; }
        [DataMember]
        public RegisterDetailCaseType RegistrarPlantillaCaso { get; set; }
        [DataMember]
        public string pPlantillaDinamica { get; set; }
        [DataMember]
        public AddPantyDynamicType pAgregarPlantillaDinamica { get; set; }
        [DataMember]
        public UpdateStateSARType ActualizarEstadoSAR { get; set; }
        [DataMember]
        public RegisterClaimType RegistrarReclamo { get; set; }
        [DataMember]
        public RegisterServiceType RegistrarServicio { get; set; }
        [DataMember]
        public RegisterSARType RegistrarSAR { get; set; }
        [DataMember]
        public RegisterPointSourceType RegistrarPuntoOrigen { get; set; }
        [DataMember]
        public ParametersType ListaRequestOpcional { get; set; }
    }
}
