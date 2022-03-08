using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityStatusResponse")]
    public class ValidateIdentityStatusResponse
    {
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string descripcionRespuesta { get; set; }
        [DataMember]
        public string ubicacionError { get; set; }
        [DataMember]
        public System.DateTime fecha { get; set; }
        [DataMember]
        public string origen { get; set; }
    }
}
