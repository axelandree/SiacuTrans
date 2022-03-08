using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SECURITY = Claro.SIACU.Entity.Transac.Service.Common;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM
{
    [DataContract(Name = "SaveAuditMRequestCommon")]
    public class SaveAuditMRequest : Claro.Entity.Request
    {
        //[DataMember]
        //public SECURITY.AuditEWS sAuditoria { get; set; }
        [DataMember]
        public string vCuentaUsuario { get; set; }
        [DataMember]
        public string vIpCliente { get; set; }
        [DataMember]
        public string vIpServidor { get; set; }
        [DataMember]
        public string vMonto { get; set; }
        [DataMember]
        public string vNombreCliente { get; set; }
        [DataMember]
        public string vNombreServidor { get; set; }
        [DataMember]
        public string vServicio { get; set; }
        [DataMember]
        public string vTelefono { get; set; }
        [DataMember]
        public string vTexto { get; set; }
        [DataMember]
        public string vTransaccion { get; set; }

    }
}
