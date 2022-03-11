using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetAgreement
{
    [DataContract(Name = "AgreementResquest")]
    public class AgreementResquest : Claro.Entity.Request
    {
        [DataMember]
        public string msisdn { get; set; }
        [DataMember]
        public string CoId { get; set; }
        [DataMember]
        public string FechaTransaccion { get; set; }
        [DataMember]
        public string CargoFijoNuevo { get; set; }
        [DataMember]
        public string MontoApadece { get; set; }
        [DataMember]
        public string FlagEquipo { get; set; }
    }
}
