using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class ValidateIdentityResponse
    {
        public ValidateIdentityResponse()
        {
            status = new ValidateIdentityStatusResponse();
            auditoriaReniec = new ValidateIdentityAuditReniecResponse();
            datosBiometria = new ValidateIdentityBiometryDataResponse();
            listaOpcional = new List<ValidateIdentityOptionalList>();
        }
        public ValidateIdentityStatusResponse status { get; set; }
        public ValidateIdentityAuditReniecResponse auditoriaReniec { get; set; }
        public ValidateIdentityBiometryDataResponse datosBiometria { get; set; }
        public List<ValidateIdentityOptionalList> listaOpcional { get; set; }
    }
}
