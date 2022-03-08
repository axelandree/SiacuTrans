using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class AccountStatusDetail
    {
        [DataMember(Name = "xTipoDocumento")]
        public string xTipoDocumento { get; set; }

        [DataMember(Name = "xNroDocumento")]
        public string xNroDocumento { get; set; }

        [DataMember(Name = "xDescripDocumento")]
        public string xDescripDocumento { get; set; }

        [DataMember(Name = "xEstadoDocumento")]
        public string xEstadoDocumento { get; set; }

        [DataMember(Name = "xFechaRegistro")]
        public string xFechaRegistro { get; set; }

        [DataMember(Name = "xFechaEmision")]
        public string xFechaEmision { get; set; }

        [DataMember(Name = "xFechaVencimiento")]
        public string xFechaVencimiento { get; set; }

        [DataMember(Name = "xTipoMoneda")]
        public string xTipoMoneda { get; set; }

        [DataMember(Name = "xMontoDocumento")]
        public string xMontoDocumento { get; set; }

        [DataMember(Name = "xMontoFco")]
        public string xMontoFco { get; set; }

        [DataMember(Name = "xMontoFinan")]
        public string xMontoFinan { get; set; }

        [DataMember(Name = "xSaldoDocumento")]
        public string xSaldoDocumento { get; set; }

        [DataMember(Name = "xSaldoFco")]
        public string xSaldoFco { get; set; }

        [DataMember(Name = "xSaldoFinan")]
        public string xSaldoFinan { get; set; }

        [DataMember(Name = "xMontoSoles")]
        public string xMontoSoles { get; set; }

        [DataMember(Name = "xMontoDolares")]
        public string xMontoDolares { get; set; }

        [DataMember(Name = "xCargo")]
        public string xCargo { get; set; }

        [DataMember(Name = "xAbono")]
        public string xAbono { get; set; }

        [DataMember(Name = "xSaldoCuenta")]
        public string xSaldoCuenta { get; set; }

        [DataMember(Name = "xNroOperacionPago")]
        public string xNroOperacionPago { get; set; }

        [DataMember(Name = "xFechaPago")]
        public string xFechaPago { get; set; }

        [DataMember(Name = "xFormaPago")]
        public string xFormaPago { get; set; }

        [DataMember(Name = "xDocAnio")]
        public string xDocAnio { get; set; }

        [DataMember(Name = "xDocMes")]
        public string xDocMes { get; set; }

        [DataMember(Name = "xDocAnioVenc")]
        public string xDocAnioVenc { get; set; }

        [DataMember(Name = "xDocMesVenc")]
        public string xDocMesVenc { get; set; }

        [DataMember(Name = "xFlagCargoCta")]
        public string xFlagCargoCta { get; set; }

        [DataMember(Name = "xNroTicket")]
        public string xNroTicket { get; set; }

        [DataMember(Name = "xMontoReclamado")]
        public string xMontoReclamado { get; set; }

        [DataMember(Name = "xTelefono")]
        public string xTelefono { get; set; }

        [DataMember(Name = "xUsuario")]
        public string xUsuario { get; set; }

        [DataMember(Name = "xIdDocOrigen")]
        public string xIdDocOrigen { get; set; }

        [DataMember(Name = "xDescripExtend")]
        public string xDescripExtend { get; set; }

        [DataMember(Name = "xIdDocOAC")]
        public string xIdDocOAC { get; set; }
    }
}
