using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert
{
    [DataContract]

    public class AddDayWorkResponse
    {
        [DataMember]
        public string FechaResultado { get; set; }

        [DataMember]
        public int CodError { get; set; }

        [DataMember]
        public string DescError { get; set; }

    }
}
