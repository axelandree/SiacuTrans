using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class TrxDetail
    {
        [DataMember(Name = "xDetalleEstadoCuenta")]
        public List<AccountStatusDetail> xDetalleEstadoCuenta { get; set; }
    }
}
