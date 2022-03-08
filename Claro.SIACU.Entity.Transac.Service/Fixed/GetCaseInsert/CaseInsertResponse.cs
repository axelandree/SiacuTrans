using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert
{
    [DataContract]
    public class CaseInsertResponse
    {
        [DataMember]
        public string rCasoId { get; set; }

        [DataMember]
        public string rFlagInsercion { get; set; }

        [DataMember]
        public string rMsgText { get; set; }

    }
}
