using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
      [DataContract(Name = "QueryUserOttRequest")]
   public class QueryUserOttRequest
    {
        [DataMember(Name = "invokeMethod")]
        public string invokeMethod { get; set; }

        [DataMember(Name = "correlatorId")]
        public string correlatorId { get; set; }

        [DataMember(Name = "countryId")]
        public string countryId { get; set; }

        [DataMember(Name = "startDate")]
        public string startDate { get; set; }

        [DataMember(Name = "endDate")]
        public string endDate { get; set; }

        [DataMember(Name = "employeeId")]
        public string employeeId { get; set; }

        [DataMember(Name = "origin")]
        public string origin { get; set; }

        [DataMember(Name = "extraData")]
        public ExtraData extraData { get; set; }

        [DataMember(Name = "serviceName")]
        public string serviceName { get; set; }

        [DataMember(Name = "providerId")]
        public string providerId { get; set; }

        [DataMember(Name = "iccidManager")]
        public string iccidManager { get; set; }

        //[DataMember(Name = "extensionInfo")]
        //public List<ExtensionInfo> extensionInfo { get; set; }

    }
}
