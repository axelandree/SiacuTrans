using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetReceipts
{
    [DataContract]
    public class ReceiptsRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCODCLIENTE { get; set; }
    }
}
