using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration
{
    [DataContract(Name = "BiometricConfigurationRequest")]
    public class BiometricConfigurationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public ValidateIdentityHeaderRequest head { get; set; }
        [DataMember]
        public string codigoPDV { get; set; }
        [DataMember]
        public string sistema { get; set; }
        [DataMember]
        public string codOperacion { get; set; }
        [DataMember]
        public string idPadre { get; set; }
        [DataMember]
        public string codCanal { get; set; }
        [DataMember]
        public string codModalVenta { get; set; }
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string usuarioCtaRed { get; set; }
        [DataMember]
        public string idHijo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public List<ValidateIdentityOptionalList> listaOpcional { get; set; }
    }
}
