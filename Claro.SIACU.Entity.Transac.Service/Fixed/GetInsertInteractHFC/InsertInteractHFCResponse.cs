using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertInteractHFC
{
    [DataContract]
    public class InsertInteractHFCResponse
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
