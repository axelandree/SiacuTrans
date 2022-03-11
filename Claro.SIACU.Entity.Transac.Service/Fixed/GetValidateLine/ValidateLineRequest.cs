using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetValidateLine
{
    [DataContract(Name = "contarLineasRequest")]
    public class ValidateLineRequest : Claro.Entity.Request
    {

        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string nombreCampo { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public string straplicativo { get; set; }

    }
}
