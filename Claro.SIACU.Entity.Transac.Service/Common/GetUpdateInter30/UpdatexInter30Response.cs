using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetUpdateInter30
{
    [DataContract(Name = "UpdatexInter30Response")]
    public class UpdatexInter30Response
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
    }
}
