using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetJobTypes
{
    [DataContract(Name = "JobTypesResponseHfc")]
    public class JobTypesResponse
    {
        [DataMember]
        public List<JobType> JobTypes { get; set; }
    }
}
