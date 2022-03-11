using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class CuentaCliente
    {
        [DataMember(Name = "cliente")]
        public Cliente cliente { get; set; }
        [DataMember(Name = "idCliente")]
        public string idCliente { get; set; }
        [DataMember(Name = "idPublicoCliente")]
        public string idPublicoCliente { get; set; }
        [DataMember(Name = "nroCuenta")]
        public string nroCuenta { get; set; }
    }
}
