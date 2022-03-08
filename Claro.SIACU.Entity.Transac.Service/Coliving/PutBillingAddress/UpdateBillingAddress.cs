using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress
{
     [DataContract]
    public class UpdateBillingAddress
    {
 
        [DataMember(Name = "actualizarDireccionFacturacionRequest")]
        public UpdateBillingAddressRequest BillingAddressRequest { get; set; }

     
    }
}
