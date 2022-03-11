using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response
{
    [DataContract]
    public class ResponseCBIO
    {
        [DataMember]
        public string replegal { get; set; }
    }
}
