using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteraction
{
    [DataContract]
    public class InsertInteractionResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rInteraccionId { get; set; }
        [DataMember]
        public string rFlagInsercion { get; set; }
        [DataMember]
        public string rMsgText { get; set; }
    }
}
