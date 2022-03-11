using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDataLine
{
   [DataContract]
    public class DataLineResponse
    {
       [DataMember]
       [DbColumn("TELEFONO")]
       public string TELEFONO {get; set;}
       
       [DataMember]
       [DbColumn("ESTADO")]
       public string ESTADO {get; set;}
       
       [DataMember]
       [DbColumn("MOTIVO")]
       public string MOTIVO {get; set;}
       
       [DataMember]
       [DbColumn("FEC_ESTADO")]
       public string FEC_ESTADO {get; set;}
       
       [DataMember]
       [DbColumn("PLAN")]
       public string PLAN {get; set;}
       
       [DataMember]
       [DbColumn("PLAZO_CONTRATO")]
       public string PLAZO_CONTRATO {get; set;}
       
       [DataMember]
       [DbColumn("ICCID")]
       public string ICCID {get; set;}

       [DataMember]
       [DbColumn("IMSI")]
       public string IMSI {get; set;}
       
       [DataMember]
       [DbColumn("CAMPANIA")]
       public string CAMPANIA {get; set;}
       
       [DataMember]
       [DbColumn("P_VENTA")]
       public string P_VENTA {get; set;}
       
       [DataMember]
       [DbColumn("NICHO_ID")]
       public string NICHO_ID {get; set;}
       
       [DataMember]
       [DbColumn("VENDEDOR")]
       public string VENDEDOR {get; set;}
       
       [DataMember]
       [DbColumn("CO_ID")]
       public string CO_ID {get; set;}
       
       [DataMember]
       [DbColumn("FECHA_ACT")]
       public string FECHA_ACT {get; set;}
       
       [DataMember]
       [DbColumn("FLAG_PLATAFORMA")]
       public string FLAG_PLATAFORMA {get; set;}
       
       [DataMember]
       [DbColumn("PIN1")]
       public string PIN1 {get; set;}
       
       [DataMember]
       [DbColumn("PUK1")]
       public string PUK1 {get; set;}
       
       [DataMember]
       [DbColumn("PIN2")]
       public string PIN2 {get; set;}
       
       [DataMember]
       [DbColumn("PUK2")]
       public string PUK2 {get; set;}
       
       [DataMember]
       [DbColumn("CODIGO_PLAN_TARIFARIO")]
       public string CODIGO_PLAN_TARIFARIO { get; set; }

    }
}
