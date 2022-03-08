using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlans
{
    [DataContract(Name="PlansRequestHfc")]
    public class PlansRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strPlano { get; set; }
        [DataMember]
        public string strOferta { get; set; }
        [DataMember]
        public string strTipoProducto { get; set; }
        [DataMember]
        public string strOffice { get; set; }
        [DataMember]
        public string strOfficeDefault { get; set; }
        [DataMember]
        public string strFlagEjecution { get; set; }


        
    }
}
