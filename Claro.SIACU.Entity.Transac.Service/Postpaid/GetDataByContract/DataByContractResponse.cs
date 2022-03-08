
using OPlanMigration = Claro.SIACU.Entity.Transac.Service.Postpaid;
using System.Runtime.Serialization;
using System.Collections.Generic;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataByContract
{
    [DataContract(Name = "DataByContractResponsePostPaid")]
    public class DataByContractResponse
    {   
        [DataMember]
        public List<OPlanMigration.DataByContract> lstDataContract {get;set;}
        [DataMember]
        public List<OPlanMigration.DataByContractInfo> lstDataContractInfo { get; set; }
    }
}
