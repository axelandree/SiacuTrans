using System.Runtime.Serialization;

namespace Claro.Entity
{
    [DataContract(Namespace = "AuditService")]
    public class Audit
    {
        [AuditProperty("TrackCode")]
        [DataMember(Name = "TrackCode", IsRequired = true, Order = 0)]
        public string TrackCode { get; set; }
        [AuditProperty("ApplicationCode")]
        [DataMember(Name = "ApplicationCode", IsRequired = true, Order = 1)]
        public string ApplicationCode { get; set; }
        [AuditProperty("IpClient")]
        [DataMember(Name = "IpClient", IsRequired = true, Order = 2)]
        public string IpClient { get; set; }
        [AuditProperty("ClientName")]
        [DataMember(Name = "ClientName", IsRequired = true, Order = 3)]
        public string ClientName { get; set; }
        [AuditProperty("ServerName")]
        [DataMember(Name = "ServerName", IsRequired = true, Order = 4)]
        public string ServerName { get; set; }
        [AuditProperty("SearchValue")]
        [DataMember(Name = "SearchValue", IsRequired = true, Order = 5)]
        public string SearchValue { get; set; }
        [DataMember(Name = "Message", IsRequired = true, Order = 6)]
        public string Message { get; set; }
    }
}
