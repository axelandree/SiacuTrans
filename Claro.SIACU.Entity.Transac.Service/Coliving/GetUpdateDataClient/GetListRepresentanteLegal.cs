using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetListRepresentanteLegal
    {
        [DataMember(Name = "cuReptipdoc")]
         public string cuReptipdoc { get; set; }
        [DataMember(Name = "cuRepnumdoc")]
        public string cuRepnumdoc { get; set; }
        [DataMember(Name = "cuRepnombres")]
        public string cuRepnombres { get; set; }
        [DataMember(Name = "cuRepapepat")]
        public string cuRepapepat { get; set; }
        [DataMember(Name = "cuRepapemat")]
         public string cuRepapemat { get; set; }
        [DataMember(Name = "idCurep")]
        public int idCurep { get; set; }
        [DataMember(Name = "cuRepstatus")]
        public string cuRepstatus { get; set; }
    }
}
