using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetInsertTransaction
{
    [DataContract]
    public class InsertTransactionResponse
    {
        [DataMember]
        public string intNumSot { get; set; }
        [DataMember]
        public string rintResCod { get; set; }
        [DataMember]
        public string rstrResDes { get; set; }
    }
}
