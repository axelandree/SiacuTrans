using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.DeleteProgramTask
{
    [DataContract]
    public class DeleteProgramTaskResponse
    {
        [DataMember]
        public bool ResponseStatus { get; set; }
    }
}
