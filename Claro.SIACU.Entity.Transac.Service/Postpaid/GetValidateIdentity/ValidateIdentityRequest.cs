using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class ValidateIdentityRequest
    {
        public ValidateIdentityRequest()
        {
            head = new ValidateIdentityHeaderRequest();
            datosAuditReniec = new ValidateIdentityAuditReniecRequest();
            datosBiometria = new ValidateIdentityBiometryDataRequest();
            datosBiometria.huellasBiometrica = new List<ValidateIdentityFingerprintData>();
            listaOpcional = new List<ValidateIdentityOptionalList>();
        }
        public ValidateIdentityHeaderRequest head { get; set; }
        public string codigoPDV { get; set; }
        public string sistema { get; set; }
        public string oficina { get; set; }
        public string flagBiometria { get; set; }
        public string codOperacion { get; set; }
        public string tipoOperacion { get; set; }
        public string codCanal { get; set; }
        public string linea { get; set; }
        public string tipoOrigen { get; set; }
        public string tipoError { get; set; }
        public ValidateIdentityAuditReniecRequest datosAuditReniec { get; set; }
        public ValidateIdentityBiometryDataRequest datosBiometria { get; set; }
        public List<ValidateIdentityOptionalList> listaOpcional { get; set; }
    }
}
