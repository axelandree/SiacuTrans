using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea
{
    [DataContract(Name = "MotiveByAreaRequest")]
    public class MotiveByAreaRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdArea { get; set; }
    }
}
