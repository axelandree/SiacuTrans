using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber
{
    public class ChangePhoneNumberRequest : Claro.Entity.Request
    {      
        [DataMember]
        public string NUMBER_PHONES { get; set; }

        [DataMember]
        public string CLASIFICATION_RED { get; set; }

        [DataMember]
        public string CUSTOMER_TYPE { get; set; }

        [DataMember]
        public string NATIONAL_CODE { get; set; }

        [DataMember]
        public string PHONE_TYPE { get; set; }

        [DataMember]
        public string HLR { get; set; }

        [DataMember]
        public string PHONE { get; set; }

        [DataMember]
        public string CONTRACT { get; set; }

        [DataMember]
        public string CURRENT_PHONE { get; set; }

        [DataMember]
        public string NEW_PHONE { get; set; }

        [DataMember]
        public string EST_TRASLADO { get; set; }

        [DataMember]
        public double COST { get; set; }

        [DataMember]
        public string FLAG_FIDELIZE { get; set; }

        [DataMember]
        public string APPLICATION_ID { get; set; }

        [DataMember]
        public string APPLICATION_PWD { get; set; }

        [DataMember]
        public string USER { get; set; }

        [DataMember]
        public string COSTMEDNO { get; set; }

        [DataMember]
        public string FLAG_CHANGECHIP { get; set; }

        [DataMember]
        public string FLAG_LOCUTION { get; set; }
    }
}
