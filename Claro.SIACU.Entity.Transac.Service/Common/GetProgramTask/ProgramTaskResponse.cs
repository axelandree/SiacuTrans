using System.Collections.Generic;
using System.Runtime.Serialization;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetProgramTask
{
    [DataContract]
    public class ProgramTaskResponse
    {
        [DataMember]
        public List<EntitiesFixed.GenericItem> ListProgramTask { get; set; }
    }
}
