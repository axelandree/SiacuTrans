using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSiacParameter
{
    [DataContract(Name = "SiacParameterRequestPostPaid")]
    public class SiacParameterRequest : Claro.Entity.Request
    {
        [DataMember]
        public int p_ParametroID { get; set; }
    }
}
