using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetSaveOCC
{
    [DataContract]
    public class SaveOCCResponse
    {
        [DataMember]
        public bool rSalida { get; set; }
        [DataMember]
        public string rResultado { get; set; }
        [DataMember]
        public string rCodResult { get; set; }
    }
}
