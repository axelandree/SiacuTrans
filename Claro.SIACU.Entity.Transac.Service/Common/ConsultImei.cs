using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ConsultImei
    {
        [DataMember]
        public DateTime Date_hours { get; set; }
        [DataMember]
        public string Nro_phone { get; set; }
        [DataMember]
        public string Nro_imei { get; set; }
        [DataMember]
        public string Date_hour_start { get; set; }
        [DataMember]
        public string Date_hour_end { get; set; }
        [DataMember]
        public string mark { get; set; }
        [DataMember]
        public string model { get; set; }
        [DataMember]
        public string mark_model { get; set; }
    }
}
