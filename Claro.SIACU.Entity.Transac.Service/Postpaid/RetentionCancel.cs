using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "RetentionCancel")]
    public class RetentionCancel : Claro.Entity.Request
    {

       [DataMember]
       public string CustomerId { get; set; }
       [DataMember]
       public string NroTelefono {get;set;}
       [DataMember]
       public string Estado_Acuerdo { get; set; }
       [DataMember]
       public string Fecha_Fin_Acuerdo { get; set; }
       [DataMember]
       public string Cago_Fijo_Total { get; set; }
       [DataMember]
       public string flgBuscar { get; set; }
        
    }
}
