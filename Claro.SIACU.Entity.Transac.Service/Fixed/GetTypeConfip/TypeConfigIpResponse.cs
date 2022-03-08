using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTypeConfip
{
    [DataContract]
    public class TypeConfigIpResponse
    {
        [DbColumn("CODIGON")]
        [DataMember]
        public int CODIGON { get; set; }
        [DbColumn("DESCRIP")]
        [DataMember]
        public string DESCRIP { get; set; }
    }
}
