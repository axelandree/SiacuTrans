using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetRegisterPlanService
{
    [DataContract(Name = "RegisterPlanRequestTransactions")]
    public class RegisterPlanRequest : Claro.Entity.Request
    {
         [DataMember]
        public string ID_INTERACCION { get; set; }
         [DataMember]
         public string COD_SERVICIO { get; set; }
         [DataMember]
         public string DES_SERVICIO { get; set; }
         [DataMember]
         public string MOTIVO_EXCLUYE { get; set; }
         [DataMember]
         public string CARGO_FIJO { get; set; }
         [DataMember]
         public string PERIODO { get; set; }
         [DataMember]
        public string USUARIO { get; set; }
    }
}
