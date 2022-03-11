using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
        public  class InsertTemplateInteraction
    {

        [DataMember]
        public string _NOMBRE_TRANSACCION { get; set; }
        [DataMember]
        public string _X_NRO_INTERACCION { get; set; }
        [DataMember]
        public  string _X_INTER_1{get;set;}
        [DataMember]
        public  string _X_INTER_2{get;set;}
        [DataMember]
        public  string _X_INTER_3{get;set;}
        [DataMember]
        public  string _X_INTER_4{get;set;}
        [DataMember]
        public  string _X_INTER_5{get;set;}
        [DataMember]
        public  string _X_INTER_6{get;set;}
        [DataMember]
        public  string _X_INTER_7{get;set;}
        [DataMember]
        public  double _X_INTER_8{get;set;}
        [DataMember]

        public  double _X_INTER_9{get;set;}
        [DataMember]
        public  double _X_INTER_10{get;set;}
        [DataMember]
        public  double _X_INTER_11{get;set;}
        [DataMember]
        public  double _X_INTER_12{get;set;}
        [DataMember]
        public  double _X_INTER_13{get;set;}
        [DataMember]
        public  double _X_INTER_14{get;set;}
        [DataMember]
        public  string _X_INTER_15{get;set;}
        [DataMember]
        public  string _X_INTER_16{get;set;}
        [DataMember]
        public  string _X_INTER_17{get;set;}
        [DataMember]
        public  string _X_INTER_18{get;set;}
        [DataMember]
        public  string _X_INTER_19{get;set;}
        [DataMember]
        public  string _X_INTER_20{get;set;}
        [DataMember]
        public  string _X_INTER_21{get;set;}
        [DataMember]
        public  double _X_INTER_22{get;set;}
        [DataMember]
        public  double _X_INTER_23{get;set;}
        [DataMember]
        public  double _X_INTER_24{get;set;}
        [DataMember]
        public  double _X_INTER_25{get;set;}
        [DataMember]
        public  double _X_INTER_26{get;set;}
        [DataMember]
        public  double _X_INTER_27{get;set;}
        [DataMember]
        public  double _X_INTER_28{get;set;}
        [DataMember]
        public  string _X_INTER_29{get;set;}
        [DataMember]
        public  string _X_INTER_30{get;set;}
        [DataMember]
        public  double _X_PLUS_INTER2INTERACT{get;set;}
        [DataMember]
        public  double _X_ADJUSTMENT_AMOUNT{get;set;}
        [DataMember]
        public  string _X_ADJUSTMENT_REASON{get;set;}
        [DataMember]
        public  string _X_ADDRESS{get;set;}
        [DataMember]
        public  string _X_AMOUNT_UNIT{get;set;}
        [DataMember]
        public  DateTime _X_BIRTHDAY{get;set;}
        [DataMember]
        public  string _X_CLARIFY_INTERACTION{get;set;}
        [DataMember]
        public  string _X_CLARO_LDN1{get;set;}
        [DataMember]
        public  string _X_CLARO_LDN2{get;set;}
        [DataMember]
        public  string _X_CLARO_LDN3{get;set;}
        [DataMember]
        public  string _X_CLARO_LDN4{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL1{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL2{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL3{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL4{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL5{get;set;}
        [DataMember]
        public  string _X_CLAROLOCAL6{get;set;}
        [DataMember]
        public  string _X_CONTACT_PHONE{get;set;}
        [DataMember]
        public  string _X_DNI_LEGAL_REP{get;set;}
        [DataMember]
        public  string _X_DOCUMENT_NUMBER{get;set;}
        [DataMember]
        public  string _X_EMAIL{get;set;}
        [DataMember]
        public  string _X_FIRST_NAME{get;set;}
        [DataMember]
        public  string _X_FIXED_NUMBER{get;set;}
        [DataMember]
        public  string _X_FLAG_CHANGE_USER{get;set;}
        [DataMember]
        public  string _X_FLAG_LEGAL_REP{get;set;}
        [DataMember]
        public  string _X_FLAG_OTHER{get;set;}
        [DataMember]
        public  string _X_FLAG_TITULAR{get;set;}
        [DataMember]
        public  string _X_IMEI{get;set;}
        [DataMember]
        public  string _X_LAST_NAME{get;set;}
        [DataMember]
        public  string _X_LASTNAME_REP{get;set;}
        [DataMember]
        public  string _X_LDI_NUMBER{get;set;}
        [DataMember]
        public  string _X_NAME_LEGAL_REP{get;set;}
        [DataMember]
        public  string _X_OLD_CLARO_LDN1{get;set;}
        [DataMember]
        public  string _X_OLD_CLARO_LDN2{get;set;}
        [DataMember]
        public  string _X_OLD_CLARO_LDN3{get;set;}
        [DataMember]
        public  string _X_OLD_CLARO_LDN4{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL1{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL2{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL3{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL4{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL5{get;set;}
        [DataMember]
        public  string _X_OLD_CLAROLOCAL6{get;set;}
        [DataMember]
        public  string _X_OLD_DOC_NUMBER{get;set;}
        [DataMember]
        public  string _X_OLD_FIRST_NAME{get;set;}
        [DataMember]
        public  string _X_OLD_FIXED_PHONE{get;set;}
        [DataMember]
        public  string _X_OLD_LAST_NAME{get;set;}
        [DataMember]
        public  string _X_OLD_LDI_NUMBER{get;set;}
        [DataMember]
        public  string _X_OLD_FIXED_NUMBER{get;set;}
        [DataMember]
        public  string _X_OPERATION_TYPE{get;set;}
        [DataMember]
        public  string _X_OTHER_DOC_NUMBER{get;set;}
        [DataMember]
        public  string _X_OTHER_FIRST_NAME{get;set;}
        [DataMember]
        public  string _X_OTHER_LAST_NAME{get;set;}
        [DataMember]
        public  string _X_OTHER_PHONE{get;set;}
        [DataMember]
        public  string _X_PHONE_LEGAL_REP{get;set;}
        [DataMember]
        public  string _X_REFERENCE_PHONE{get;set;}
        [DataMember]
        public  string _X_REASON{get;set;}
        [DataMember]
        public  string _X_MODEL{get;set;}
        [DataMember]
        public  string _X_LOT_CODE{get;set;}
        [DataMember]
        public  string _X_FLAG_REGISTERED{get;set;}
        [DataMember]
        public  string _X_REGISTRATION_REASON{get;set;}
        [DataMember]
        public  string _X_CLARO_NUMBER{get;set;}
        [DataMember]
        public  string _X_MONTH{get;set;}
        [DataMember]
        public  string _X_OST_NUMBER{get;set;}
        [DataMember]
        public  string _X_BASKET{get;set;}
        [DataMember]
        public DateTime _X_RECHARGE_DATE { get; set; }
        [DataMember]
        public  DateTime _X_EXPIRE_DATE{get;set;}
        [DataMember]
        public string _ID_INTERACCION { get; set; }
        [DataMember]
        public string _Script_IMEI { get; set; }
        [DataMember]
        public string _SESSION_ID { get; set; }
        [DataMember]
        public string _MSISDN { get; set; }
        [DataMember]
        public string _P_OLD_ID_CONTACTO { get; set; }
        [DataMember]
        public string _P_USUARIO_ID { get; set; }
        [DataMember]
        public string _TIENE_DATOS { get; set; }

        [DataMember]
        public string _TICKET { get; set; }
        [DataMember]
        public string _ACCION { get; set; }
        [DataMember]
        public int _NRO_DIAS { get; set; }
        [DataMember]
        public string _NRO_TELEFONO { get; set; }
        [DataMember]
        public string _NRO_TARJETA { get; set; }
        [DataMember]
        public string _X_ADDRESS5{get;set;}
        [DataMember]
        public double _X_CHARGE_AMOUNT{get;set;}
        [DataMember]
        public string _X_CITY{get;set;}
        [DataMember]
        public string _X_CONTACT_SEX{get;set;}
        [DataMember]
        public string _X_DEPARTMENT{get;set;}
        [DataMember]
        public string _X_DISTRICT{get;set;}
        [DataMember]
        public string _X_EMAIL_CONFIRMATION{get;set;}
        [DataMember]
        public string _X_FAX{get;set;}
        [DataMember]
        public string _X_FLAG_CHARGE{get;set;}
        [DataMember]
        public string _X_MARITAL_STATUS{get;set;}
        [DataMember]
        public string _X_OCCUPATION{get;set;}
        [DataMember]
        public string _X_POSITION{get;set;}
        [DataMember]
        public string _X_REFERENCE_ADDRESS{get;set;}
        [DataMember]
        public string _X_TYPE_DOCUMENT{get;set;}
        [DataMember]
        public string _X_ZIPCODE{get;set;}
        [DataMember]
        public string _X_ICCID{get;set;}
        [DataMember]
        public string _X_TRIO_ANTIGUO { get; set; }

    }
}
