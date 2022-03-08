using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateInter30
{
    [DataContract(Name = "UpdatexInter30Request")]
    public class UpdatexInter30Request : Claro.Entity.Request
    {
        [DataMember]
        public string p_objid { get; set; }
        [DataMember]
        public string p_texto { get; set; }
    }
}
