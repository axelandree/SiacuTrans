using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common.GetDatTempInteraction
{
    [DataContract]
    public class DatTempInteractionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vInteraccionID { get; set; }
    }
}
