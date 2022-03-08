using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class BiometryKeys
    {
        public string strTipoDocumentos { get; set; }        
        public string strCodigoTipoDocumentoDNIValidacionBiometrica { get; set; }
        public string strMensajeValidacionBiometrica1 { get; set; }
        public string strMensajeValidacionBiometrica2 { get; set; }
        public string strMensajeValidacionBiometrica3 { get; set; }
        public string strMensajeValidacionBiometrica4 { get; set; }
        public string strMensajeValidacionBiometricaMenos2 { get; set; }
        public string strMensajeValidacionBiometricaMenos4 { get; set; }
        public string strMensajeValidacionBiometricaMenos5 { get; set; }
        public string strMensajeValidacionBiometrica0 { get; set; }
        public string strMensajeValidacionBiometricaOtros { get; set; }
        public string strKeyTransaccionDesbloqueoLinea { get; set; }
        public string Url { get; set; }
        public string strDomainBiometria { get; set; }
        public string strMensajeBlackList { get; set; }
        public string strFlagValidaBlackList { get; set; }
        public string strFlagPermisoBiometria { get; set; }        

    }
}