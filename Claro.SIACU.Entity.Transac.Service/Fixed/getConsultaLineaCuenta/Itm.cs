using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.getConsultaLineaCuenta
{
    [DataContract]
   public class Itm
    {
        [DataMember]
        public string origenCuenta { get; set; }
        [DataMember]
        public string codCuenta { get; set; }
        [DataMember]
        public string coId { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string actCuentaProd { get; set; }
        [DataMember]
        public string migCuentaProd { get; set; }
        [DataMember]
        public string origenRegistro { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string usrCrea { get; set; }
        [DataMember]
        public string usrModif { get; set; }
        [DataMember]
        public string fchCreacion { get; set; }
        [DataMember]
        public string fchModif { get; set; }
        [DataMember]
        public string custCode { get; set; }



    }
}
