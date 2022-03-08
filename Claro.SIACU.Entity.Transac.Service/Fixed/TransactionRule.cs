using System.Runtime.Serialization;
using Claro.Data;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract(Name="TransactionRule")]
    public class TransactionRule
    {
        [DbColumn("REATV_SUBCLASE")]
        [DataMember]
        public string REATV_SUBCLASE { get; set; }
        [DbColumn("REATV_REGLA")]
        [DataMember]
        public string REATV_REGLA { get; set; }
        [DbColumn("REATV_NOTA")]
        [DataMember]
        public string REATV_NOTA { get; set; }
    }
}
