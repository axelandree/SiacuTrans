using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ConsultReniec")]
    public class ConsultReniec
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string NroTransRequest { get; set; }
        [DataMember]
        public string DniConsultado { get; set; }
        [DataMember]
        public string PuntoVenta { get; set; }
        [DataMember]
        public string SolinCodigo { get; set; }
        [DataMember]
        public string CodExitoError { get; set; }
        [DataMember]
        public string MensajeProceso { get; set; }
        [DataMember]
        public DateTime FechaHoraTranRequest { get; set; }
        [DataMember]
        public string UsuarioReg { get; set; }
        [DataMember]
        public string MensajeServicio { get; set; }
        [DataMember]
        public string NroTransResponse { get; set; }
        [DataMember]
        public DateTime FechaHoraResponse { get; set; }
        [DataMember]
        public string Aplicacion { get; set; }
        [DataMember]
        public string DniAutorizado { get; set; }
        [DataMember]
        public string tipoValidacion { get; set; }
        [DataMember]
        public string Proceso { get; set; }
        [DataMember]
        public string Region { get; set; }
        [DataMember]
        public string TipoPuntoVenta { get; set; }
        [DataMember]
        public string NombrePuntoVenta { get; set; }
        [DataMember]
        public string NombreVendedor { get; set; }
        [DataMember]
        public string NumeroLinea { get; set; }
        [DataMember]
        public string NombreCliente { get; set; }
        [DataMember]
        public string Escenario { get; set; }
        [DataMember]
        public string BiometricaONoBiometrica { get; set; }
        [DataMember]
        public DateTime FechaTransaccion { get; set; }
    }
}
