using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class BilledCallsDetailHFC
    {
        [DataMember]
        public string CurrentNumber { get; set; }
        [DataMember]
        public string StrDate { get; set; }
        [DataMember]
        public string StrHour { get; set; }
        [DataMember]
        public string DestinationPhone { get; set; }
        [DataMember]
        public string NroCustomer { get; set; }
        [DataMember]
        public string Consumption { get; set; }
        [DataMember]
        public string CostSoles { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Destination { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public string TypeCalls { get; set; }
        [DataMember]
        public string CargOrig { get; set; }
        [DataMember]
        public string  TelephoneDest { get; set; }
        [DataMember]
        public string TelephoneDestExport { get; set; }
        [DataMember]
        public int NroRegistre { get; set; }
    }
}
