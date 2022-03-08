using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetFacturePDI
{
    [DataContract]
    public class FacturePDIRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCodeCustomer { get; set; }
    }
}
