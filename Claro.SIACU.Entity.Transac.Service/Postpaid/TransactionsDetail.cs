using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "TransactionsDetail")]
    public class TransactionsDetail
    {
        [DataMember]
        [Data.DbColumn("CO_ID")]
        public int CO_ID { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_ESBATCH")]
        public string SERVC_ESBATCH { get; set; }
        [DataMember]
        [Data.DbColumn("CO_ID")]
        public string SERVC_ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("SERVD_FECHA_EJEC")]
        public string SERVD_FECHA_EJEC { get; set; }
        [DataMember]
        [Data.DbColumn("SERVD_FECHA_REG")]
        public string SERVD_FECHA_REG { get; set; }
        [DataMember]
        [Data.DbColumn("SERVD_FECHAPROG")]
        public string SERV_FECHAPROG { get; set; }
        [DataMember]
        [Data.DbColumn("SERVI_COD")]
        public int SERVI_COD { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_EMAIL_USUARIO_APP")]
        public string SERVV_EMAIL_USUARIO_APP { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_ID_BATCH")]
        public string SERVV_ID_BATCH { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_MSISDN")]
        public string SERVV_MSISDN { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_USUARIO_APLICACION")]
        public string SERVV_USUARIO_APLICACION { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_USUARIO_SISTEMA")]
        public string SERVV_USUARIO_SISTEMA { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_XMLENTRADA")]
        public string SERVV_XMLENTRADA { get; set; }
        [DataMember]
        [Data.DbColumn("DESC_ESTADO")]
        public string DESC_ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("DESC_SERVI")]
        public string DESC_SERVI { get; set; }
        [DataMember]
        [Data.DbColumn("CODIGO_PRODUCTO")]
        public int CODIGO_PRODUCTO { get; set; }
        
        
        [DataMember]
        [Data.DbColumn("TIPO_TRANSACCION")]
        public string TIPO_TRANSACCION { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_DESCOSER")]
        public string SERVC_DESCOSER { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_TIPOREG")]
        public string SERVC_TIPOREG { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_TIPOSER")]
        public string SERVC_TIPOSER { get; set; }
        [DataMember]
        [Data.DbColumn("TIPO_ESTADO")]
        public string TIPO_ESTADO { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_NROCUENTA")]
        public string SERVC_NROCUENTA { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_CODIGO_INTERACCION")]
        public string SERVC_CODIGO_INTERACCION { get; set; } 
        [DataMember]
        [Data.DbColumn("SERVC_PUNTOVENTA")]
        public string SERVC_PUNTOVENTA { get; set; }
        [DataMember]
        [Data.DbColumn("SERVV_MEN_ERROR")]
        public string SERVV_MEN_ERROR { get; set; }
        [DataMember]
        [Data.DbColumn("SERVC_CODIGO_PROGRAMACION")]
        public string SERVC_CODIGO_PROGRAMACION { get; set; }
        [DataMember]
        [Data.DbColumn("COD_PRODUCTO")]
        public int COD_PRODUCTO { get; set; }
    }
}
