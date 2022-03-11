using System.Runtime.Serialization;

namespace Claro.Entity
{
    [DataContract(Namespace = "Claro")]
    public class AuditRequest
    {
        [AuditProperty("idTransaccion")]
        [AuditProperty("Transaccion")]
        [DataMember(Name = "transaction", IsRequired = true, Order = 0)]
        public string Transaction { get; set; }


        [AuditProperty("ipAplicacion")]
        [AuditProperty("IPAddress")]
        [DataMember(Name = "ipAddress", IsRequired = true, Order = 1)]
        public string IPAddress { get; set; }


        [AuditProperty("nombreAplicacion")]
        [AuditProperty("aplicacion")]
        [AuditProperty("ApplicacionName")]
        [DataMember(Name = "applicationName", IsRequired = true, Order = 2)]
        public string ApplicationName { get; set; }


        [AuditProperty("usuarioAplicacion")]
        [AuditProperty("usrAplicacion")]
        [AuditProperty("UserName")]
        [DataMember(Name = "userName", IsRequired = true, Order = 3)]
        public string UserName { get; set; }


        [AuditProperty("idSession")]
        [AuditProperty("IDSession")]
        [AuditProperty("Session")]
        [DataMember(Name = "Session", IsRequired = true, Order = 4)]
        public string Session { get; set; }
    }
}
