using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValueXml
{
    [DataContract]
    public class ValueXmlResponse
    {
        [DataMember]
        public string ValueFromXml { get; set; }
    }
}
