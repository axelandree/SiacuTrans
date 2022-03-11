
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class AccountStatusResponse
    {
        [DataMember]
        public Audit audit { get; set; }
        [DataMember]
        public AccountStatus xEstadoCuenta { get; set; }
    }
}
