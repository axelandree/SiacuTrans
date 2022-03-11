using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetJobTypeConfigIP
{
    [DataContract]
    public class JobTypesConfigIPResponse
    {
        [DbColumn("TIPTRA")]
        [DataMember]
        public string TIPTRA {get; set;}
        [DbColumn("DESCRIPCION")]
        [DataMember]
        public string DESCRIPCION {get; set;}
        [DbColumn("FLAG_ACTIVA")]
        [DataMember]
        public string FLAG_ACTIVA {get; set;}
        [DbColumn("TIPO_SERV")]
        [DataMember]
        public string TIPO_SERV {get; set;}
         
    }
}
