using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetFactureDBTO
{
    [DataContract]
    public class FactureDBTORequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCodeCustomer { get; set; }
    }
}
