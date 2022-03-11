using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.DeleteProgramTaskHfc
{
    [DataContract]
    public class DeleteProgTaskHfcResponse
    {
        [DataMember]
        public bool ResultStatus { get; set; }
    }
}
