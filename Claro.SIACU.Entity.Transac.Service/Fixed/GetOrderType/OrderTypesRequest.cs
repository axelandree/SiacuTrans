using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetOrderType
{
    [DataContract(Name = "OrderTypesRequestHfc")]
    public class OrderTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vIdtiptra { get; set; }

    }
}
