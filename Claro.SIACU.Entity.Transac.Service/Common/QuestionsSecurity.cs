using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class QuestionsSecurity
    {
        [DataMember]
        [Data.DbColumn("PEGEN_OBJID")]
        public int IdQuestions { get; set; }
        [DataMember]
        [Data.DbColumn("PEGEV_DESCPREGUNTA")]
        public string Description { get; set; }
    }
}
