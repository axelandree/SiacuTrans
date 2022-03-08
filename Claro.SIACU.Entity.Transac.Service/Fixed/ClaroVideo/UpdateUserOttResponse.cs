using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
     [DataContract(Name = "updateUserOttResponse")]
    public class UpdateUserOttResponse
    {
         [DataMember(Name = "resultCode")]
         public string resultCode { get; set; }

         [DataMember(Name = "resultMessage")]
         public string resultMessage { get; set; }

         [DataMember(Name = "correlatorId")]
         public string correlatorId { get; set; }

         [DataMember(Name = "countryId")]
         public string countryId { get; set; }

         [DataMember(Name = "serviceName")]
         public string serviceName { get; set; }

         [DataMember(Name = "providerId")]
         public string providerId { get; set; }

         [DataMember(Name = "extensionInfo")]
         public List<ExtensionInfo> extensionInfo { get; set; }
         
    }
}
