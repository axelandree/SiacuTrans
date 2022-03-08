using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response
{
    [DataContract]
    public class listaRepresentanteLegal
    {
        [DataMember]
        public int idCurep { get; set; }
        [DataMember]
        public string cuReptipdoc { get; set; }
        [DataMember]
        public string cuRepnumdoc { get; set; }
        [DataMember]
        public string cuRepnombres { get; set; }
        [DataMember]
        public string cuRepapepat { get; set; }
        [DataMember]
        public string cuRepapemat { get; set; }
        [DataMember]
        public string cuRepstatus { get; set; }
    }
}
