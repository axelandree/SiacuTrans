using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetReconeService
{
    [DataContract]
    public class ReconeServiceResponse
    {
        [DataMember]
        public string IdTransaction { get; set; }
        [DataMember]
        public string Result { get; set; }
        [DataMember]
        public bool BoolResult { get; set; }    
    }
}
