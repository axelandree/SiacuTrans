using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract]
    public class tabConsultaPlanTFIPost
    {
        [DataMember(Name = "provider")]
        public string provider { get; set; }
        [DataMember(Name = "tariff")]
        public string tariff { get; set; }
        [DataMember(Name = "suscriber")]
        public string suscriber { get; set; }
        [DataMember(Name = "desc_plan")]
        public string desc_plan { get; set; }
    }
}
