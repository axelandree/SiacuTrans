using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInfoInteractionTemplate
{
    [DataContract]
    public class InfoInteractionTemplateResponse
    {
        [DataMember]
        public InteractionTemplate InteractionTemplate { get; set; }
        [DataMember]
        public string vFLAG_CONSULTA { get; set; }
        [DataMember]
        public string vMSG_TEXT { get; set; }
    }
}
