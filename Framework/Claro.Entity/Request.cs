using System.Runtime.Serialization;

namespace Claro.Entity
{
    [DataContract(Namespace = "Claro")]
    public abstract class Request
    {
        [DataMember(Name = "audit", IsRequired = true, Order = 0)]
        public AuditRequest Audit { get; set; }
        [DataMember(Name = "AuditService", IsRequired = false, Order = 2)]
        public Audit AuditService { get; set; }
    }
}
