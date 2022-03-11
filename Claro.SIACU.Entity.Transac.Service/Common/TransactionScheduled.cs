using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class TransactionScheduled
    {
        [DataMember]
        public string CO_ID { get; set; }
        [DataMember]
        public string CUSTOMER_ID { get; set; }
        [DataMember]
        public string SERVD_FECHAPROG { get; set; }
        [DataMember]
        public string SERVD_FECHA_REG { get; set; }
        [DataMember]
        public string SERVD_FECHA_EJEC { get; set; }
        [DataMember]
        public string SERVC_ESTADO { get; set; }
        [DataMember]
        public string DESC_ESTADO { get; set; }
        [DataMember]
        public string SERVC_ESBATCH { get; set; }
        [DataMember]
        public string SERVV_MEN_ERROR { get; set; }
        [DataMember]
        public string SERVV_COD_ERROR { get; set; }
        [DataMember]
        public string SERVV_USUARIO_SISTEMA { get; set; }
        [DataMember]
        public string SERVV_ID_EAI_SW { get; set; }
        [DataMember]
        public string SERVI_COD { get; set; }
        [DataMember]
        public string DESC_SERVI { get; set; }
        [DataMember]
        public string SERVV_MSISDN { get; set; }
        [DataMember]
        public string SERVV_ID_BATCH { get; set; }
        [DataMember]
        public string SERVV_USUARIO_APLICACION { get; set; }
        [DataMember]
        public string SERVV_EMAIL_USUARIO_APP { get; set; }
        [DataMember]
        public string SERVV_XMLENTRADA { get; set; }
        [DataMember]
        public string SERVC_NROCUENTA { get; set; }
        [DataMember]
        public string SERVC_CODIGO_INTERACCION { get; set; }
        [DataMember]
        public string SERVC_PUNTOVENTA { get; set; }
        [DataMember]
        public string SERVC_TIPO_SERV { get; set; }
        [DataMember]
        public string SERVC_CO_SER { get; set; }
        [DataMember]
        public string SERVC_TIPO_REG { get; set; }
        [DataMember]
        public string SERVC_DES_CO_SER { get; set; }

        [DataMember]
        public string ID_PARAMETRO { get; set; }
        [DataMember]
        public string CODIGOC { get; set; }
        [DataMember]
        public string CODIGONPAGE { get; set; }
        [DataMember]
        public string DESCRIPCION { get; set; }
        [DataMember]
        public string ABREVIATURA { get; set; }
        [DataMember]
        public string ESTADO { get; set; }
        [DataMember]
        public string ID_DETALLE { get; set; }
        [DataMember]
        public string DESCRIPCION_DET { get; set; }
        [DataMember]
        public string ABREVIATURA_DET { get; set; }
        [DataMember]
        public string ESTADO_DET { get; set; }

    }
}
