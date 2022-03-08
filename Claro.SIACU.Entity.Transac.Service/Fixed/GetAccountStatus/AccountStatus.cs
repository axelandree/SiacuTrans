using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
   [DataContract]
   public class AccountStatus
    {
       [DataMember(Name = "xDetalleEstadoCuentaCab")]
       public List<AccountStatusDetailH> xDetalleEstadoCuentaCab { get; set; }
    }
}
