using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetListDataAccountBillingRequest
    {
        [DataMember(Name = "cuentaCambioSaldo")]
        public string cuentaCambioSaldo { get; set; }
        [DataMember(Name = "codCuenta")]
        public string codCuenta { get; set; }
        [DataMember(Name = "nombreCuenta")]
        public string nombreCuenta { get; set; }
        [DataMember(Name = "flagDetalleLLamadas")]
        public string flagDetalleLlamadas { get; set; }
        [DataMember(Name = "secuencialDirFactura")]
        public string secuencialDirFactura { get; set; }
        [DataMember(Name = "secuencialDirFacturaTemp")]
        public string secuencialDirFacturaTemp { get; set; }
        [DataMember(Name = "saldoActualCuentaFact")]
        public string saldoActualCuentaFact { get; set; }
        [DataMember(Name = "flagReclamo")]
        public string flagReclamo { get; set; }
        [DataMember(Name = "nivelCambioMoneda")]
        public string nivelCambioMoneda { get; set; }
        [DataMember(Name = "cuentaTipo")]
        public string cuentaTipo { get; set; }
        [DataMember(Name = "idTipoFacturaVinculada")]
        public string idTipoFacturaVinculada { get; set; }       
    }
}
