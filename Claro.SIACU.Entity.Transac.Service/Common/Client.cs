using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common
{
     [DataContract]
    public class Client
    {
        [DataMember]
        public string OBJID_CONTACTO{get; set;}
         [DataMember]
        public  string OBJID_SITE{get; set;}
         [DataMember]
        public  string TELEFONO{get; set;}
         [DataMember]
        public  string CUENTA{get; set;}
         [DataMember]
        public  string MODALIDAD{get; set;}
         [DataMember]
        public  string SEGMENTO{get; set;}
         [DataMember]
        public  string ROL_CONTACTO{get; set;}
         [DataMember]
        public  string ESTADO_CONTACTO{get; set;}
         [DataMember]
        public  string ESTADO_CONTRATO{get; set;}
         [DataMember]
        public  string ESTADO_SITE{get; set;}
         [DataMember]
        public  string S_NOMBRES{get; set;}
         [DataMember]
        public  string S_APELLIDOS{get; set;}
         [DataMember]
        public  string NOMBRES {get; set;}
         [DataMember]
        public  string APELLIDOS {get; set;}
         [DataMember]
        public  string NOMBRE_COMPLETO {get; set;}
         [DataMember]
        public  string DOMICILIO{get; set;}
         [DataMember]
        public  string URBANIZACION{get; set;}
         [DataMember]
        public  string REFERENCIA{get; set;}
         [DataMember]
        public  string CIUDAD{get; set;}
         [DataMember]
        public  string DISTRITO{get; set;}
         [DataMember]
        public  string DEPARTAMENTO{get; set;}
         [DataMember]
        public  string ZIPCODE{get; set;}
         [DataMember]
        public  string EMAIL{get; set;}
         [DataMember]
        public  string TELEF_REFERENCIA{get; set;}
         [DataMember]
        public  string FAX{get; set;}
         [DataMember]
        public  string FECHA_NAC{get; set;}
         [DataMember]
        public  string SEXO{get; set;}
         [DataMember]
        public  string ESTADO_CIVIL{get; set;}
         [DataMember]
        public  string ESTADO_CIVIL_ID{get; set;}
         [DataMember]
        public  string TIPO_DOC{get; set;}
         [DataMember]
        
        public  string NRO_DOC{get; set;}
         [DataMember]
        public  DateTime FECHA_ACT{get; set;}
         [DataMember]
        public  string PUNTO_VENTA{get; set;}
         [DataMember]
        public  int FLAG_REGISTRADO{get; set;}
         [DataMember]
        public  string OCUPACION{get; set;}
         [DataMember]
        public  string CANT_REG{get; set;}
         [DataMember]
        public  string FLAG_EMAIL{get; set;}
         [DataMember]
        public  string MOTIVO_REGISTRO{get; set;}
         [DataMember]
        public  string FUNCION{get; set;}
         [DataMember]
        public  string CARGO{get; set;}
         [DataMember]
        public  string LUGAR_NACIMIENTO_ID{get; set;}
         [DataMember]
        public  string LUGAR_NACIMIENTO_DES{get; set;}
         
         [DataMember]
        public  string P_FLAG_CONSULTA{get; set;}
         [DataMember]
        public  string P_MSG_TEXT{get; set;}
         [DataMember]
        public  string USUARIO{get; set;}
         [DataMember]
        public  string RAZON_SOCIAL{get; set;}
         [DataMember]
        public  string DNI_RUC{get; set;}
         [DataMember]
        public  string PROVINCIA{get; set;}
         [DataMember]
        public  string ASESOR{get; set;}
         [DataMember]
        public  string CONSULTOR{get; set;}
         [DataMember]
        public  string CICLO_FACTURACION{get; set;}
         [DataMember]
        public  string CREDIT_SCORE{get; set;}
         [DataMember]
        public  string TOTAL_CUENTAS{get; set;}
         [DataMember]
        public  string DEPOSITO{get; set;}
         [DataMember]
        public  string ESTADO_CUENTA{get; set;}
         [DataMember]
        public  string RENTA{get; set;}
         [DataMember]
        public  string TIPO_CLIENTE{get; set;}
         [DataMember]
        public  string TOTAL_LINEAS{get; set;}
         [DataMember]
        public  string LIMITE_CREDITO{get; set;}
         [DataMember]
        public  string RESPONSABLE_PAGO{get; set;}
         [DataMember]
        public  string REPRESENTANTE_LEGAL{get; set;}
         [DataMember]
        public  string CONTACTO_CLIENTE{get; set;}
         [DataMember]
        public  string TELEFONO_CONTACTO{get; set;}
         [DataMember]
        public  string CALLE_FAC{get; set;}
         [DataMember]
        public  string POSTAL_FAC{get; set;}
         [DataMember]
        public  string URBANIZACION_FAC{get; set;}
         [DataMember]
        public  string DEPARTEMENTO_FAC{get; set;}
         [DataMember]
        public  string PROVINCIA_FAC{get; set;}
         [DataMember]
        public  string DISTRITO_FAC{get; set;}
         [DataMember]
        public  string NOMBRE_COMERCIAL{get; set;}
         [DataMember]
        public  string CALLE_LEGAL{get; set;}
         [DataMember]
        public  string POSTAL_LEGAL{get; set;}
         [DataMember]
        public  string URBANIZACION_LEGAL{get; set;}
         [DataMember]
        public  string DEPARTEMENTO_LEGAL{get; set;}
         [DataMember]
        public  string PROVINCIA_LEGAL{get; set;}
         [DataMember]
        public  string DISTRITO_LEGAL{get; set;}
         [DataMember]
        public  string PAIS_LEGAL{get; set;}
         [DataMember]
         public string REFERENCIA_LEGAL { get; set; }
         [DataMember]
        public  string PAIS_FAC{get; set;}
         [DataMember]
        public  string CUSTOMER_ID{get; set;}
         [DataMember]
        public  string CONTRATO_ID{get; set;}
         [DataMember]
        public  string NICHO{get; set;}
         [DataMember]
        public  string DIRECCION_DESPACHO{get; set;}
         [DataMember]
        public  string FORMA_PAGO{get; set;}
         [DataMember]
        public  string COD_TIPO_CLIENTE{get; set;}

         [DataMember]
        public  string TEXTO_NOTAS{get; set;}
         [DataMember]
        public  string SEGMENTO2{get; set;}
         [DataMember]
        public  string LONG_NRO_DOC{get; set;}
         [DataMember]
        public  string USU_ASEG{get; set;}
         [DataMember]
        public  long NACIONALIDAD{get; set;}


         //Datos Adicionales
         [DataMember]
         public string TELEFONO_REFERENCIA_1 { get; set; }
         [DataMember]
         public string TELEFONO_REFERENCIA_2 { get; set; }

         [DataMember(Name = "ContactCntCode")]
         public string COD_CNT { get; set; }
         [DataMember(Name = "ContactCustomerCode")]
         public string CODIGO_CLIENTE { get; set; }
     
         [DataMember]
         public string TIPO_DOC_RL { get; set; }
     
    }
}
