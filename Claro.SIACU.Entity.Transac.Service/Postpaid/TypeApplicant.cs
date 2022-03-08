using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "TypeApplicant")]
    public class TypeApplicant
    {
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string Apellidos { get; set; }
        [DataMember]
        public string TipoDocumento { get; set; }
        [DataMember]
        public string NroDocumento { get; set; }
        [DataMember]
        public string TipoSolicitante { get; set; }
    }
}
