using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class CaseTemplate : Claro.Entity.Request
    {

        [DataMember]
        public string NOMBRE_TRANSACCION { get; set; }
        [DataMember]
        public int ID_PLANTILLA { get; set; }
        [DataMember]
        public string ID_CASO { get; set; }
        [DataMember]
        public string X_CAS_1 { get; set; }
        [DataMember]
        public string X_CAS_2 { get; set; }
        [DataMember]
        public string X_CAS_3 { get; set; }
        [DataMember]
        public string X_CAS_4 { get; set; }
        [DataMember]
        public string X_CAS_5 { get; set; }
        [DataMember]
        public string X_CAS_6 { get; set; }
        [DataMember]
        public string X_CAS_7 { get; set; }
        [DataMember]
        public double X_CAS_8 { get; set; }
        [DataMember]
        public double X_CAS_9 { get; set; }
        [DataMember]
        public double X_CAS_10 { get; set; }
        [DataMember]
        public double X_CAS_11 { get; set; }
        [DataMember]
        public double X_CAS_12 { get; set; }
        [DataMember]
        public double X_CAS_13 { get; set; }
        [DataMember]
        public double X_CAS_14 { get; set; }
        [DataMember]
        public string X_CAS_15 { get; set; }
        [DataMember]
        public string X_CAS_16 { get; set; }
        [DataMember]
        public string X_CAS_17 { get; set; }
        [DataMember]
        public string X_CAS_18 { get; set; }
        [DataMember]
        public string X_CAS_19 { get; set; }
        [DataMember]
        public string X_CAS_20 { get; set; }
        [DataMember]
        public string X_CAS_21 { get; set; }
        [DataMember]
        public double X_CAS_22 { get; set; }
        [DataMember]
        public double X_CAS_23 { get; set; }
        [DataMember]
        public double X_CAS_24 { get; set; }
        [DataMember]
        public double X_CAS_25 { get; set; }
        [DataMember]
        public double X_CAS_26 { get; set; }
        [DataMember]
        public double X_CAS_27 { get; set; }
        [DataMember]
        public double X_CAS_28 { get; set; }
        [DataMember]
        public string X_CAS_29 { get; set; }
        [DataMember]
        public string X_CAS_30 { get; set; }
        [DataMember]
        public DateTime X_SUSPENSION_DATE { get; set; }
        [DataMember]
        public DateTime X_REACTIVATION_DATE { get; set; }
        [DataMember]
        public double X_SUSPENSION_QT { get; set; }
        [DataMember]
        public string X_CONCLUSIONS { get; set; }
        [DataMember]
        public string X_TEST_MADE { get; set; }
        [DataMember]
        public string X_PROBLEM_DESCRIPTION { get; set; }
        [DataMember]
        public string X_ADDRESS { get; set; }
        [DataMember]
        public string X_DOCUMENT_NUMBER { get; set; }
        [DataMember]
        public string X_CALL_DURATION { get; set; }
        [DataMember]
        public double X_CALL_COST { get; set; }
        [DataMember]
        public string X_SYSTEM_STATUS { get; set; }
        [DataMember]
        public string X_FLAG_VARIATION { get; set; }
        [DataMember]
        public DateTime X_SEARCH_DATE { get; set; }
        [DataMember]
        public DateTime X_VARIATION_DATE { get; set; }
        [DataMember]
        public DateTime X_LAST_QUERY { get; set; }
        [DataMember]
        public DateTime X_PROBLEM_DATE { get; set; }
        [DataMember]
        public DateTime X_PURCHASE_DATE { get; set; }
        [DataMember]
        public DateTime X_RECHARGE_DATE { get; set; }
        [DataMember]
        public DateTime X_REQUEST_DATE { get; set; }
        [DataMember]
        public string X_REQUEST_PLACE { get; set; }
        [DataMember]
        public string X_FLAG_GPRS { get; set; }
        [DataMember]
        public double X_COMPLAINT_AMOUNT { get; set; }
        [DataMember]
        public string X_LINES { get; set; }
        [DataMember]
        public string X_ERROR_MESSAGE { get; set; }
        [DataMember]
        public string X_MODEL { get; set; }
        [DataMember]
        public string X_MARK { get; set; }
        [DataMember]
        public string X_BAND { get; set; }
        [DataMember]
        public string X_REPOSITION_REASON { get; set; }
        [DataMember]
        public string X_CHURN_REASON { get; set; }
        [DataMember]
        public string X_CELLULAR_NUMBER { get; set; }
        [DataMember]
        public string X_CLARIFY_VARIATION { get; set; }
        [DataMember]
        public string X_FLAG_SEND_RECEIVE { get; set; }
        [DataMember]
        public string X_CUSTOMER_NAME { get; set; }
        [DataMember]
        public string X_PREPAID_CARD_NUMBER { get; set; }
        [DataMember]
        public string X_NUMBERS_COMMUNICATION { get; set; }
        [DataMember]
        public string X_REFERENCE_NUMBER { get; set; }
        [DataMember]
        public string X_OST_NUMBER { get; set; }
        [DataMember]
        public string X_FRIENDS_FAMILY { get; set; }
        [DataMember]
        public string X_COUNTRY_OPERATOR { get; set; }
        [DataMember]
        public string X_OLD_PLAN { get; set; }
        public string X_NEW_PLAN { get; set; }
        public string X_CURRENT_PLAN { get; set; }
        public string X_CAMPAIGN { get; set; }
        public string X_BILL_NUMBER_COMPLAINT { get; set; }
        [DataMember]
        public string X_REFERENCE_ADDRESS { get; set; }
        [DataMember]
        public double X_CURRENT_BALANCE { get; set; }
        [DataMember]
        public double X_LAST_BALANCE { get; set; }
        [DataMember]
        public double X_BALANCE_REQUESTED { get; set; }
        [DataMember]
        public string X_CUSTOMER_SEGMENT { get; set; }
        [DataMember]
        public string X_SERVICE_PROBLEM { get; set; }
        [DataMember]
        public string X_FLAG_OTHER_PROBLEMS { get; set; }
        [DataMember]
        public string X_OPERATOR_PROBLEM { get; set; }
        [DataMember]
        public string X_CONTACT_TIME_TERM { get; set; }
        [DataMember]
        public string X_STORE { get; set; }
        [DataMember]
        public string X_DIAL_TYPE { get; set; }
        [DataMember]
        public string X_PROBLEM_LOCATION { get; set; }
        [DataMember]
        public string X_FLAG_ADDITIONAL_SERVICES { get; set; }
        [DataMember]
        public string X_FLAG_WAP { get; set; }
        [DataMember]

        public string MSISDN { get; set; }
        [DataMember]
        public double MONTO_RECARGA { get; set; }
        [DataMember]
        public string BOLSA { get; set; }
        [DataMember]
        public string TICKET { get; set; }
        [DataMember]
        public string ACCION { get; set; }
        [DataMember]
        public int NRO_DIAS { get; set; }
        [DataMember]
        public string NRO_TELEFONO { get; set; }
        [DataMember]
        public string NRO_TARJETA { get; set; }
        [DataMember]
        public string X_CLARO_LDN1 { get; set; }
        [DataMember]
        public string X_CLARO_LDN2 { get; set; }
        [DataMember]
        public string X_CLARO_LDN3 { get; set; }
        [DataMember]
        public string X_CLARO_LDN4 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL1 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL2 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL3 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL4 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL5 { get; set; }
        [DataMember]
        public string X_CLAROLOCAL6 { get; set; }
        [DataMember]
        public string X_FIXED_NUMBER { get; set; }
        [DataMember]
        public string X_LDI_NUMBER { get; set; }
        [DataMember]
        public string X_OLD_CLARO_LDN1 { get; set; }
        [DataMember]
        public string X_OLD_CLARO_LDN2 { get; set; }
        [DataMember]
        public string X_OLD_CLARO_LDN3 { get; set; }
        [DataMember]
        public string X_OLD_CLARO_LDN4 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL1 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL2 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL3 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL4 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL5 { get; set; }
        [DataMember]
        public string X_OLD_CLAROLOCAL6 { get; set; }
        [DataMember]
        public string X_OLD_FIXED_NUMBER { get; set; }
        [DataMember]
        public string X_OLD_LDI_NUMBER { get; set; }
        [DataMember]
        public string X_OLD_IMSI { get; set; }
        [DataMember]
        public string X_DEACTIVATION_REASON { get; set; }
        [DataMember]
        public string X_IDCLARIFY_VARIATION { get; set; }
        [DataMember]
        public string FLAT_INSERCION { get; set; }
        [DataMember]
        public string MESSAGE { get; set; }
        [DataMember]
        public string FLAG_CREACION { get; set; } 
    }
}
