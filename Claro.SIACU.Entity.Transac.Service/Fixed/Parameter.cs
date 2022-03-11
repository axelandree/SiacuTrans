using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name = "Parameter")]
    public class Parameter
    {
        [DataMember(Name = "parametro")]
        public decimal PARAMETRO_ID { get; set; }
        [DataMember(Name = "campo")]
        public string CAMPO { get; set; }
        [DataMember(Name= "valor")]
        public string VALOR { get; set; }
        [DataMember(Name = "Url")]
        public string Url { get; set; }
        [DataMember(Name = "Credentials")]
        public System.Net.ICredentials Credentials { get; set; }
        [DataMember(Name = "Timeout")]
        public int Timeout { get; set; }
    }
}
