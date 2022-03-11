using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser
{
     [DataContract(Name = "audit")]
    public class AuditResponse
    {
         [DataMember]
         public string idTransaccion { get; set; }
         [DataMember]
         public string codigoRespuesta { get; set; }
         [DataMember]
         public string mensajeRespuesta { get; set; }
    }
}
