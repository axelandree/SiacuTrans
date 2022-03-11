using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "AuditRegister")]
    public class AuditRegister
    {
        [DataMember]
        public string strIdTransaccion { get; set; }
        [DataMember]
        public string strServicio { get; set; }
        [DataMember]
        public string strIpCliente { get; set; }
        [DataMember]
        public string strNombreCliente { get; set; }
        [DataMember]
        public string strIpServidor { get; set; }
        [DataMember]
        public string strNombreServidor { get; set; }
        [DataMember]
        public string strMonto { get; set; }
        [DataMember]
        public string strCuentaUsuario { get; set; }
        [DataMember]
        public string strTelefono { get; set; }
        [DataMember]
        public string strTexto { get; set; }

    }
}
