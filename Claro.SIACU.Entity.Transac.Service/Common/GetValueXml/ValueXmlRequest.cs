using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValueXml
{
    [DataContract]
    public class ValueXmlRequest : Claro.Entity.Request
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string Clave { get; set; }
    }
}
