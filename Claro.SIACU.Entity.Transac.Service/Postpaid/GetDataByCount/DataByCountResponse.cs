
using System.Collections.Generic;
using System.Runtime.Serialization;
using OPlanMigration = Claro.SIACU.Entity.Transac.Service.Postpaid;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataByCount
{
    [DataContract(Name = "DataByCountResponsePostPaid")]
    public class DataByCountResponse
    {
        [DataMember]
        public string NivelCount { get; set; }
        [DataMember]
        public List<OPlanMigration.DataByCount> lstDataCount { get; set; }
        [DataMember]
        public List<OPlanMigration.DataByContractInfo> lstDataContractInfo { get; set; }
    }
}
