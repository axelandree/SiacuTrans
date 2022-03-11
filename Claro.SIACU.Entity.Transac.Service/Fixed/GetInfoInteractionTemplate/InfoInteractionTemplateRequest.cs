using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInfoInteractionTemplate
{
    [DataContract]
    public class InfoInteractionTemplateRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vInteraccionID { get; set; }
    }
}
