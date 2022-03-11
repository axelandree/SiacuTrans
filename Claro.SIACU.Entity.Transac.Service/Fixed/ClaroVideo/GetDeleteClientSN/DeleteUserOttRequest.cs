using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetDeleteClientSN
{
    [DataContract(Name = "deleteUserOttRequest")]
    public class DeleteUserOttRequest
    {
        [DataMember(Name = "invokeMethod")]
        public string invokeMethod { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "countryId")]
        public string countryId { get; set; }

        [DataMember(Name = "paymentMethod")]
        public string paymentMethod { get; set; }

        [DataMember(Name = "userId")]
        public string userId { get; set; }

        [DataMember(Name = "account")]
        public string account { get; set; }

        [DataMember(Name = "employeeId")]
        public string employeeId { get; set; }

        [DataMember(Name = "origin")]
        public string origin { get; set; }

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
