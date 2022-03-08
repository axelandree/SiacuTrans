using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress
{
   
    [DataContract(Name = "Response")]
    public class BillingAddressResponse
    {
        [DataMember]
        public List<Opcional> listaOpcional { get; set; }
        [DataMember]
        public AuditResponse responseAudit { get; set; } 
    }
}
