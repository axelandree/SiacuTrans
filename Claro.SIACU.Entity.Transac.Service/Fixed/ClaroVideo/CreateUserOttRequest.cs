using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "CreateUserOttRequest")]
    public class CreateUserOttRequest
    {
        [DataMember(Name = "invokeMethod")]
        public string invokeMethod { get; set; }

        [DataMember(Name = "countryId")]
        public string countryId { get; set; }

        [DataMember(Name = "employeeId")]
        public string employeeId { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "origin")]
        public string origin { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "lastName")]
        public string lastName { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "motherLastName")]
        public string motherLastName { get; set; }

        [DataMember(Name = "serviceName")]
        public string serviceName { get; set; }

        [DataMember(Name = "providerId")]
        public string providerId { get; set; }

        [DataMember(Name = "iccidManager")]
        public string iccidManager { get; set; }

        [DataMember(Name = "extensionInfo")]
        public List<ExtensionInfo> extensionInfo { get; set; }
    }
}
