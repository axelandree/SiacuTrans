using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetAccountBillingRequest
    {
        [DataMember(Name = "IdCuenta")]
        public string idCuenta { get; set; }
        [DataMember(Name = "codCuenta")]
        public string codCuenta { get; set; }
        [DataMember(Name = "nombreCuenta")]
        public string nombreCuenta { get; set; }
        [DataMember(Name = "idMedioFacturacion")]
        public string idMedioFacturacion { get; set; }
        [DataMember(Name = "idCliente")]
        public string idCliente { get; set; }
        [DataMember(Name = "idClientePub")]
        public string idClientePub { get; set; }
        [DataMember(Name = "saldoCuentaFacturacion")]
        public string saldoCuentaFacturacion { get; set; }
        [DataMember(Name = "flagReclamo")]
        public string flagReclamo { get; set; }
        [DataMember(Name = "tipoFactura")]
        public string tipoFactura { get; set; }
        [DataMember(Name = "idTipoFactura")]
        public string idTipoFactura { get; set; }
        [DataMember(Name = "idTipoFacturaPublico")]
        public string idTipoFacturaPublico { get; set; }
        [DataMember(Name = "indicadorUso")]
        public string indicadorUso { get; set; }
        [DataMember(Name = "ultimaFechaFacturacion")]
        public string ultimaFechaFacturacion { get; set; }
        [DataMember(Name = "saldoAnterior")]
        public string saldoAnterior { get; set; }
        [DataMember(Name = "moneda")]
        public string moneda { get; set; }
        [DataMember(Name = "flagPrimaria")]
        public string flagPrimaria { get; set; }
        [DataMember(Name = "flagFacturacionDividida")]
        public string flagFacturacionDividida { get; set; }
        [DataMember(Name = "estado")]
        public string estado { get; set; }
       
    }
}
