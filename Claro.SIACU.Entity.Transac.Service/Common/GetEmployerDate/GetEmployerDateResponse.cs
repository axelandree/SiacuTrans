using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEmployerDate
{
     [DataContract(Name = "GetEmployerDateResponseCommon")]
    public class GetEmployerDateResponse 
    {

        [DataMember]
         public string strCode { get; set; }

        [DataMember]
        public string strCodeArea { get; set; }
        [DataMember]
        public string strCodeCargo { get; set; }
        [DataMember]
        public string strCodeAddress { get; set; }
        [DataMember]
        public string strCodeBoss { get; set; }
        [DataMember]
        public string strEmail { get; set; }
        [DataMember]
        public string strDesArea { get; set; }
        [DataMember]
        public string strDesCargo { get; set; }
        [DataMember]
        public string strDesAddress { get; set; }
        [DataMember]
        public string strDesBoss { get; set; }
        [DataMember]
        public string strlogin { get; set; }
        [DataMember]
        public string strNomb { get; set; }
        [DataMember]
        public string strApPat { get; set; }
        [DataMember]
        public string strApMat { get; set; }

    }
}
