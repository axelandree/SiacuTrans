using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class ParameterData
    {
        //public Int32 Id_Parameter;
        [DataMember]
        public string Value_C { get; set; }
        [DataMember]
        public double Value_N { get; set; }
        [DataMember]
        public string Value_L { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
