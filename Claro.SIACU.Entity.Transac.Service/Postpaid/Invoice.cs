using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("FACT")]
    [DataContract(Name = "InvoiceTransactions")]
 public   class Invoice
 {
     [DataMember]
     [Data.DbColumn("INVOICENUMBER")]
     public string INVOICENUMBER { get; set; }
     [DataMember]
     [Data.DbColumn("CCNAME")]
     public string CCNAME { get; set; }
     [DataMember]
     [Data.DbColumn("CONTACTCLIENT")]
     public string CONTACTCLIENT { get; set; }
     [DataMember]
     [Data.DbColumn("CCADDR1")]
     public string CCADDR1 { get; set; }
     [DataMember]
     [Data.DbColumn("CCADDR2")]
     public string CCADDR2 { get; set; }
     [DataMember]
     [Data.DbColumn("DISTRITO")]
     public string DISTRITO { get; set; }
     [DataMember]
     [Data.DbColumn("PROVINCIA")]
     public string PROVINCIA { get; set; }
     [DataMember]
     [Data.DbColumn("DEPARTAMENTO")]
     public string DEPARTAMENTO { get; set; }
     [DataMember]
     [Data.DbColumn("NRODOC")]
     public string NRODOC { get; set; }
     [DataMember]
     [Data.DbColumn("FECHAINICIO")]
     public string FECHAINICIO { get; set; }
     [DataMember]
     [Data.DbColumn("FECHAFIN")]
     public string FECHAFIN { get; set; }
     [DataMember]
     [Data.DbColumn("FECHAEMISION")]
     public string FECHAEMISION { get; set; }
     [DataMember]
     [Data.DbColumn("FECHAVENCIMIENTO")]
     public string FECHAVENCIMIENTO { get; set; }
     [DataMember]
     [Data.DbColumn("CODCLIENTE")]
     public string CODCLIENTE { get; set; }
     [DataMember]
     [Data.DbColumn("NROCICLO")]
     public string NROCICLO { get; set; }
     [DataMember]
     [Data.DbColumn("PERIODO")]
     public string PERIODO { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALPREVCHARGES")]
     public string TOTALPREVCHARGES { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALPAYMENTSRCVD")]
     public string TOTALPAYMENTSRCVD { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALPREVBALANCE")]
     public string TOTALPREVBALANCE { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALCURRENTCHARGES")]
     public string TOTALCURRENTCHARGES { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALTAXES")]
     public string TOTALTAXES { get; set; }
     [DataMember]
     [Data.DbColumn("TOTALAMOUNTDUE")]
     public string TOTALAMOUNTDUE { get; set; }
     [DataMember]
     [Data.DbColumn("MES")]
     public string MES { get; set; }
     [DataMember]
     [Data.DbColumn("ANHO")]
     public string ANHO { get; set; }
     [DataMember]
     [Data.DbColumn("VERSION")]
     public string VERSION { get; set; }
    }
}
