using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetRegisterPlanService
{
    [DataContract(Name = "RegisterPlanResponseTransactions")]
    public class RegisterPlanResponse
    {
        [DataMember]
        public string CodRegServicioPlan { get; set; }
    }
}
