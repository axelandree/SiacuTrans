
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateXInter29
{
    [DataContract(Name = "UpdateXInter29ResponseCommon")]
    public class UpdateXInter29Response
    {
        [DataMember]
        public string Flag { get; set; }
        [DataMember]     
        public string  Mensaje { get; set; }
    }
}
