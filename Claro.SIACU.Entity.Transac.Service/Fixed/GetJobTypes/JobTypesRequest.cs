using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetJobTypes
{
    [DataContract(Name = "JobTypesRequestHfc")]
    public class JobTypesRequest : Claro.Entity.Request
    {
        [DataMember]
        public int p_tipo { get; set; }

    }
}
