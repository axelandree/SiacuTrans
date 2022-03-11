using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract(Name = "SpecificationAttachmentOnBase")]
    public class SpecificationAttachmentOnBase
    {
        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "type")]
        public string type { get; set; }

        [DataMember(Name = "listEntitySpectAttach")]
        public entitySpecAttachExtension listEntitySpectAttach { get; set; }
    }


    [DataContract(Name = "metadatosOnBase")]
    public class metadatosOnBase
    {
        [DataMember(Name = "attributeName")]
        public string attributeName { get; set; }

        [DataMember(Name = "attributeValue")]
        public string attributeValue { get; set; }

    }

    [DataContract(Name = "entitySpecAttachExtensionOnBase")]
    public class entitySpecAttachExtension
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }

        [DataMember(Name = "fileBase64")]
        public string fileBase64 { get; set; }
    }
    
}
