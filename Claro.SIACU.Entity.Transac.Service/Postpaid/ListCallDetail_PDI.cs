using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("FACTCALLDETAIL")]
    [DataContract(Name = "ListCallDetail_PDITransactions")]
   public class ListCallDetail_PDI
    {
        [DataMember]
        [Data.DbColumn("TELEFONO")]
        public string TELEFONO { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA")]
        public string FECHA { get; set; }
        [DataMember]
        [Data.DbColumn("HORA")]
        public string HORA { get; set; }
        [DataMember]
        [Data.DbColumn("TELEFONO_DESTINO")]
        public string TELEFONO_DESTINO { get; set; }
        [DataMember]
        [Data.DbColumn("CONSUMO")]
        public string CONSUMO { get; set; }
        [DataMember]
        [Data.DbColumn("CARGO_ORIGINAL")]
        public string CARGO_ORIGINAL { get; set; }
        [DataMember]
        [Data.DbColumn("TIPO_LLAMADA")]
        public string TIPO_LLAMADA { get; set; }
        [DataMember]
        [Data.DbColumn("DESTINO")]
        public string DESTINO { get; set; }
        [DataMember]
        [Data.DbColumn("OPERADOR")]
        public string OPERADOR { get; set; }
        [DataMember]
        [Data.DbColumn("CALLDATE")]
        public string CALLDATE { get; set; }
        [DataMember]
        [Data.DbColumn("CALLTIME")]
        public string CALLTIME { get; set; }
    }
}
