using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes
{
    [DataContract(Name = "ParameterBusinnesRequest")]
    public class ParameterBusinnesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdList { get; set; }
    }
	
}
