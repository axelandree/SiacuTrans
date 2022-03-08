using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta
{

    [DataContract]
  public  class ConsultaLineaResponse
    {
        [DataMember]
        public string ResponseValue { get; set; }
        [DataMember]
        public string ResponseDescription { get; set; }
        [DataMember]
        public Itm itm { get; set; }

    }
}
