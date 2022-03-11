using System;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class InteractionTemplate
    {
        public InteractionTemplate()
        {

        }

        [DataMember]
        public string NOMBRE_TRANSACCION { get; set; }
        [DataMember]
        public string X_NRO_INTERACCION { get; set; }
        [DataMember]
        public string X_INTER_1 { get; set; }
        [DataMember]
        public string X_INTER_2 { get; set; }
        [DataMember]
        public string X_INTER_3 { get; set; }
        [DataMember]
        public string X_INTER_4 { get; set; }
        [DataMember]
        public string X_INTER_5 { get; set; }
        [DataMember]
        public string X_INTER_6 { get; set; }
        [DataMember]
        public string X_INTER_7 { get; set; }
        [DataMember]
        public double X_INTER_8 { get; set; }
        [DataMember]
        public double X_INTER_9 { get; set; }
        [DataMember]
        public double X_INTER_10 { get; set; }
        [DataMember]
        public double X_INTER_11 { get; set; }
        [DataMember]
        public double X_INTER_12 { get; set; }
        [DataMember]
        public double X_INTER_13 { get; set; }
        [DataMember]
        public double X_INTER_14 { get; set; }
        [DataMember]
        public string X_INTER_15 { get; set; }
        [DataMember]
        public string X_INTER_16 { get; set; }
        [DataMember]
        public string X_INTER_17 { get; set; }
        [DataMember]
        public string X_INTER_18 { get; set; }
        [DataMember]
        public string X_INTER_19 { get; set; }
        [DataMember]
        public string X_INTER_20 { get; set; }
        [DataMember]
        public string X_INTER_21 { get; set; }
        [DataMember]
        public double X_INTER_22 { get; set; }
        [DataMember]
        public double X_INTER_23 { get; set; }
        [DataMember]
        public double X_INTER_24 { get; set; }
        [DataMember]
        public double X_INTER_25 { get; set; }
        [DataMember]
        public double X_INTER_26 { get; set; }
        [DataMember]
        public double X_INTER_27 { get; set; }
        [DataMember]
        public double X_INTER_28 { get; set; }
        [DataMember]
        public string X_INTER_29 { get; set; }
        [DataMember]
        public string X_INTER_30 { get; set; }
        [DataMember]
        public double X_PLUS_INTER2INTERACT { get; set; }
        [DataMember]
        public double X_ADJUSTMENT_AMOUNT { get; set; }
        [DataMember]
        public string X_ADJUSTMENT_REASON { get; set; }
        [DataMember]
        public string X_ADDRESS { get; set; }
        [DataMember]
        public string X_AMOUNT_UNIT { get; set; }
        [DataMember]
        public DateTime X_BIRTHDAY { get; set; }
        [DataMember]
        public string X_CLARIFY_INTERACTION { get; set; }
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
        public string X_CONTACT_PHONE { get; set; }
        [DataMember]
        public string X_DNI_LEGAL_REP { get; set; }
        [DataMember]
        public string X_DOCUMENT_NUMBER { get; set; }
        [DataMember]
        public string X_EMAIL { get; set; }
        [DataMember]
        public string X_FIRST_NAME { get; set; }
        [DataMember]
        public string X_FIXED_NUMBER { get; set; }
        [DataMember]
        public string X_FLAG_CHANGE_USER { get; set; }
        [DataMember]
        public string X_FLAG_LEGAL_REP { get; set; }
        [DataMember]
        public string X_FLAG_OTHER { get; set; }
        [DataMember]
        public string X_FLAG_TITULAR { get; set; }
        [DataMember]
        public string X_IMEI { get; set; }
        [DataMember]
        public string X_LAST_NAME { get; set; }
        [DataMember]
        public string X_LASTNAME_REP { get; set; }
        [DataMember]
        public string X_LDI_NUMBER { get; set; }
        [DataMember]
        public string X_NAME_LEGAL_REP { get; set; }
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
        public string X_OLD_DOC_NUMBER { get; set; }
        [DataMember]
        public string X_OLD_FIRST_NAME { get; set; }
        [DataMember]
        public string X_OLD_FIXED_PHONE { get; set; }
        [DataMember]
        public string X_OLD_LAST_NAME { get; set; }
        [DataMember]
        public string X_OLD_LDI_NUMBER { get; set; }
        [DataMember]
        public string X_OLD_FIXED_NUMBER { get; set; }
        [DataMember]
        public string X_OPERATION_TYPE { get; set; }
        [DataMember]
        public string X_OTHER_DOC_NUMBER { get; set; }
        [DataMember]
        public string X_OTHER_FIRST_NAME { get; set; }
        [DataMember]
        public string X_OTHER_LAST_NAME { get; set; }
        [DataMember]
        public string X_OTHER_PHONE { get; set; }
        [DataMember]
        public string X_PHONE_LEGAL_REP { get; set; }
        [DataMember]
        public string X_REFERENCE_PHONE { get; set; }
        [DataMember]
        public string X_REASON { get; set; }
        [DataMember]
        public string X_MODEL { get; set; }
        [DataMember]
        public string X_LOT_CODE { get; set; }
        [DataMember]
        public string X_FLAG_REGISTERED { get; set; }
        [DataMember]
        public string X_REGISTRATION_REASON { get; set; }
        [DataMember]
        public string X_CLARO_NUMBER { get; set; }
        [DataMember]
        public string X_MONTH { get; set; }
        [DataMember]
        public string X_OST_NUMBER { get; set; }
        [DataMember]
        public string X_BASKET { get; set; }
        [DataMember]
        public DateTime X_RECHARGE_DATE { get; set; }
        [DataMember]
        public DateTime X_EXPIRE_DATE { get; set; }
        [DataMember]
        public string ID_INTERACCION { get; set; }
        [DataMember]
        public string Script_IMEI { get; set; }
        [DataMember]
        public string SESSION_ID { get; set; }
        [DataMember]
        public string MSISDN { get; set; }
        [DataMember]
        public string P_OLD_ID_CONTACTO { get; set; }
        [DataMember]
        public string P_USUARIO_ID { get; set; }
        [DataMember]
        public string TIENE_DATOS { get; set; }
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
        public string X_ADDRESS5 { get; set; }
        [DataMember]
        public double X_CHARGE_AMOUNT { get; set; }
        [DataMember]
        public string X_CITY { get; set; }
        [DataMember]
        public string X_CONTACT_SEX { get; set; }
        [DataMember]
        public string X_DEPARTMENT { get; set; }
        [DataMember]
        public string X_DISTRICT { get; set; }
        [DataMember]
        public string X_EMAIL_CONFIRMATION { get; set; }
        [DataMember]
        public string X_FAX { get; set; }
        [DataMember]
        public string X_FLAG_CHARGE { get; set; }
        [DataMember]
        public string X_MARITAL_STATUS { get; set; }
        [DataMember]
        public string X_OCCUPATION { get; set; }
        [DataMember]
        public string X_POSITION { get; set; }
        [DataMember]
        public string X_REFERENCE_ADDRESS { get; set; }
        [DataMember]
        public string X_TYPE_DOCUMENT { get; set; }
        [DataMember]
        public string X_ZIPCODE { get; set; }
        [DataMember]
        public string X_ICCID { get; set; }
        [DataMember]
        public string X_TRIO_ANTIGUO { get; set; }
    }
}
