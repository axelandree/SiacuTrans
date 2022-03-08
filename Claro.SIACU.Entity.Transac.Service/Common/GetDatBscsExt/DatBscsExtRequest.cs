using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDatBscsExt
{
    [DataContract]
    public class DatBscsExtRequest : Claro.Entity.Request
    {
        [DataMember]
        public string NroTelephone{ get; set; }
        [DataMember]
        public double CodeNewPlan { get; set; }
        [DataMember]
        public double NroFacture { get; set; }
        [DataMember]
        public double CargFixedCurrent { get; set; }
        [DataMember]
        public double CargFixedNewPlan { get; set; }
    }
}
