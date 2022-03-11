using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class AccountStatusDetailH
    {
        [DataMember(Name = "xNombreCliente")]
        public string xNombreCliente { get; set; }

        [DataMember(Name = "xDeudaActual")]
        public string xDeudaActual { get; set; }

        [DataMember(Name = "xDeudaVencida")]
        public string xDeudaVencida { get; set; }

        [DataMember(Name = "xTotalMontoDisputa")]
        public string xTotalMontoDisputa { get; set; }

        [DataMember(Name = "xFechaUltFactura")]
        public string xFechaUltFactura { get; set; }

        [DataMember(Name = "xFechaUtlPago")]
        public string xFechaUtlPago { get; set; }

        [DataMember(Name = "xCodCuenta")]
        public string xCodCuenta { get; set; }

        [DataMember(Name = "xCodCuentaAlterna")]
        public string xCodCuentaAlterna { get; set; }

        [DataMember(Name = "xDescUbigeo")]
        public string xDescUbigeo { get; set; }

        [DataMember(Name = "xTipoCliente")]
        public string xTipoCliente { get; set; }

        [DataMember(Name = "xEstadoCuenta")]
        public string xEstadoCuenta { get; set; }

        [DataMember(Name = "xFechaActivacion")]
        public string xFechaActivacion { get; set; }

        [DataMember(Name = "xCicloFacturacion")]
        public string xCicloFacturacion { get; set; }

        [DataMember(Name = "xLimiteCredito")]
        public string xLimiteCredito { get; set; }

        [DataMember(Name = "xCreditScore")]
        public string xCreditScore { get; set; }

        [DataMember(Name = "xTipoPago")]
        public string xTipoPago { get; set; }

        [DataMember(Name = "xDetalleTrx")]
        public TrxDetail xDetalleTrx { get; set; }
    }
}
