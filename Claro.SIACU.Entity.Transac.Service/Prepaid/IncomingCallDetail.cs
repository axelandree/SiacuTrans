using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid
{
    [DataContract]
    public class IncomingCallDetail
    {
        [DataMember]
        public string NroOrd { get; set; }
        [DataMember]
        [Data.DbColumn("NUMEROA")]
        public string NumberA { get; set; }
        [DataMember]
        [Data.DbColumn("FECHA")]
        public string Date { get; set; }
        [DataMember]
        [Data.DbColumn("HORA_INICIO")]
        public string StartHour { get; set; }
        [DataMember]
        [Data.DbColumn("NUMEROB")]
        public string NumberB { get; set; }
        [DataMember]
        [Data.DbColumn("DURACION")]
        public string Duration { get; set; }
    }
}
