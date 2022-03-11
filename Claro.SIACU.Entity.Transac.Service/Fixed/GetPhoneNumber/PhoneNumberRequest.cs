using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPhoneNumber
{
    public class PhoneNumberRequest : Claro.Entity.Request
    {
        [DataMember]
        public string NUMBER_PHONES { get; set; }

        [DataMember]
        public string CLASIF_RED { get; set; }

        [DataMember]
        public string CUSTOMER_TYPE { get; set; }

        [DataMember]
        public string NATIONAL_CODE { get; set; }

        [DataMember]
        public string TYPE_PHONE { get; set; }

        [DataMember]
        public string TYPE_NUMBER { get; set; }

        [DataMember]
        public string CONTRACT_ID { get; set; }

        [DataMember]
        public string CUSTOMER_ID { get; set; }

        [DataMember]
        public string FLAG_FIDEL_TRANS { get; set; }

        [DataMember]
        public string COST_TRANS { get; set; }

        [DataMember]
        public string COST_TRANS_IGV { get; set; }

        [DataMember]
        public string FLAG_LOCU { get; set; }

        [DataMember]
        public string FLAG_FIDEL_LOCU { get; set; }

        [DataMember]
        public string COST_LOCU { get; set; }

        [DataMember]
        public string COST_LOCU_IGV { get; set; }

        [DataMember]
        public string FLAG_SEND_EMAIL { get; set; }

        [DataMember]
        public string EMAIL { get; set; }

        [DataMember]
        public string NRO_TELEF { get; set; }

        [DataMember]
        public string HLR_CODE { get; set; }

        [DataMember]
        public string FLAG_PLAN_TIPI { get; set; }

        [DataMember]
        public string CONTACT { get; set; }

        [DataMember]
        public string FULL_NAME { get; set; }

        [DataMember]
        public string DOCUMENT { get; set; }

        [DataMember]
        public string POINT_ATTENTION { get; set; }

        [DataMember]
        public string NOTES { get; set; }

        [DataMember]
        public string CONTACTOBJID { get; set; }

        [DataMember]
        public string CUSTIDOBJID { get; set; }

        [DataMember]
        public string RESULT { get; set; }

        [DataMember]
        public string TYPE { get; set; }

        [DataMember]
        public string CLASS { get; set; }

        [DataMember]
        public string SUBCLASS { get; set; }

        [DataMember]
        public string METHOD { get; set; }

        [DataMember]
        public string TIPO_INTER { get; set; }

        [DataMember]
        public string USER_PROCESS { get; set; }
    }
}
