using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class Striations
    {
        [DataMember]
        public string TIPO_TRIADO { get; set; }
        [DataMember]
        public int NUM_TRIO { get; set; }
        [DataMember]
        public string TELEFONO { get; set; }
        [DataMember]
        public string FACTOR { get; set; }
        [DataMember]
        public string DESTINO_TRIO { get; set; }
        [DataMember]
        public string TIPO_DESTINO { get; set; }
        [DataMember]
        public string CODIGO_TIPO_DESTINO { get; set; }
        [DataMember]
        public string PORTABILIDAD { get; set; }
    }
}
