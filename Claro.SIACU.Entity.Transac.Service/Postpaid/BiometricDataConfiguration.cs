using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class BiometricDataConfiguration
    {
        public string ovencCodigo { get; set; }
        public string ovenvDescripcion { get; set; }
        public string gpdvvCodigo { get; set; }
        public string toficCodigo { get; set; }
        public string ovencEstado { get; set; }
        public string canacCodigo { get; set; }
        public string canavDescripcion { get; set; }
        public string tprocCodigo { get; set; }
        public string soxsvCodOperacion { get; set; }
        public string soxsvDescOperacion { get; set; }
        public string soxsvSistema { get; set; }
        public string soxpnFlagHuellero { get; set; }
        public string soxpnFlagBiometria { get; set; }
        public string soxpnFlagIncapacidad { get; set; }
        public string soxpnTipoVerificacionBio { get; set; }
        public string soxpnFlagNoBiometriaReniec { get; set; }
        public string soxpnFlagNoBiometriaDc { get; set; }
        public string soxpvMensaje { get; set; }
        public string soxpnTipoError { get; set; }
        public string soxpnFlagFinVenta { get; set; }
        public string soxpnFlagIdValidator { get; set; }
        public string soxpnFlagFirmaDigital { get; set; }
        public string soxpnTipoVerificacionFirma { get; set; }
        public string soxpnFlagEmail { get; set; }
        public string ovencSupervisorHuellaBc { get; set; }
    }
}
