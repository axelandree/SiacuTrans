using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class AnswerSecurity
    {
        [DataMember]
        [Data.DbColumn("REGEN_OBJIDPREGUNTA")]
        public int IdAnswer { get; set; }
        [DataMember]
        [Data.DbColumn("REGEV_DESCRESPUESTA")]
        public string Description { get; set; }
    }
}
