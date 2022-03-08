
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateXInter29
{
    [DataContract(Name = "UpdateXInter29RequestCommon")]
    public class UpdateXInter29Request : Claro.Entity.Request
    {
        [DataMember]
        public string IdInteract { get; set; }
        [DataMember]
        public string Text { get; set; }
        [DataMember]
        public string Order { get; set; }
    }
}
