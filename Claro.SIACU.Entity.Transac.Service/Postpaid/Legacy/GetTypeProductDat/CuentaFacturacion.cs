using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class CuentaFacturacion
    {
        [DataMember(Name = "cuentaCliente")]
        public CuentaCliente cuentaCliente { get; set; }
    }
}
