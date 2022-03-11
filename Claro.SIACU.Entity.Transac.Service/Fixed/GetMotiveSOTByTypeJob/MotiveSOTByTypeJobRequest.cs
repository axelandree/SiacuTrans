using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetMotiveSOTByTypeJob
{
    [DataContract]
    public class MotiveSOTByTypeJobRequest : Claro.Entity.Request
    {
        [DataMember]
        public int tipTra { get; set; }
    }
}
