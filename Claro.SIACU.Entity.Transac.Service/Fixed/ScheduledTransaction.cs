using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class ScheduledTransaction
    {
       [DataMember]
       public string CO_ID {get;set;}
       [DataMember]
       public string DESC_STATE { get; set; }
       [DataMember]
       public string DESC_SERVICE { get; set; }
       [DataMember]
       public string CUSTOMER_ID { get; set; }
       [DataMember]
       public string SERVC_CO_SER { get; set; }
       [DataMember]
       public string SERVC_CODE_INTERACTION { get; set; }
       [DataMember]

       public string SERVC_DES_CO_SER { get; set; }
       [DataMember]
       public string SERVC_ISBATCH { get; set; }
       [DataMember]
       public string SERVC_STATE { get; set; }
       [DataMember]
       public string SERVC_NUMBERACCOUNT { get; set; }
       [DataMember]
       public string SERVC_POINTSALE { get; set; }
       [DataMember]
       public string SERVC_TYPE_REG { get; set; }
       [DataMember]
       public string SERVC_TYPE_SERV { get; set; }
       [DataMember]
       public string SERVD_DATE_EJEC { get; set; }
       [DataMember]
       public string SERVD_DATE_REG { get; set; }
       [DataMember]
       public string SERVD_DATEPROG { get; set; }
       [DataMember]
       public string SERVI_COD { get; set; }
       [DataMember]

       public string SERVV_COD_ERROR { get; set; }
       [DataMember]
       public string SERVV_EMAIL_USER_APP { get; set; }
       [DataMember]
       public string SERVV_ID_BATCH { get; set; }
       [DataMember]
       public string SERVV_ID_EAI_SW { get; set; }
       [DataMember]
       public string SERVV_MEN_ERROR { get; set; }
       [DataMember]
       public string SERVV_MSISDN { get; set; }
       [DataMember]
       public string SERVV_USER_APLICATION { get; set; }
       [DataMember]
       public string SERVV_USER_SYSTEM { get; set; }
       [DataMember]
       public string SERVV_XMLENTRY { get; set; } 
    }
}
