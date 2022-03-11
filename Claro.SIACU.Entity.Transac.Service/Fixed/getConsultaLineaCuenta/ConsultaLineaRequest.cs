using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta
{

    [DataContract]
    public class ConsultaLineaRequest : Tools.Entity.Request
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Value { get; set; }

    }
}
