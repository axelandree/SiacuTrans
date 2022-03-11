using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetPlanesTFI
{
    [DataContract]
    public class responseDataObtenerTabConsultaPlanTFIPost
    {
        [DataMember]
        public List<tabConsultaPlanTFIPost> listaTabConsultaPlanTFIPost { get; set; }
    }
}
