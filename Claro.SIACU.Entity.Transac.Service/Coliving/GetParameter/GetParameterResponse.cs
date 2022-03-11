using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter
{
    [DataContract]
    public class GetParameterResponse
    {
        [DataMember]
        public List<ListParameter> LstParameter { get; set; }
        [DataMember]
        public string Mensaje { get; set; }
    }
}
