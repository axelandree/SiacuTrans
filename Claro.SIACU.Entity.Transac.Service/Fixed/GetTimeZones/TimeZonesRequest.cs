using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTimeZones
{
    [DataContract(Name = "TimeZonesRequestHfc")]
    public class FranjasHorariasRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strAnUbigeo { get; set; }
        [DataMember]
        public string strAnTiptra { get; set; }
        [DataMember]
        public string strAdFecagenda { get; set; }

    }
}
