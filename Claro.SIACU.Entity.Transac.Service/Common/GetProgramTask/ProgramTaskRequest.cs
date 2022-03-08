using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetProgramTask
{
    [DataContract]
    public class ProgramTaskRequest : Claro.Entity.Request
    {
        [DataMember]
        public string SessionId { get; set; }
        [DataMember]
        public string TransactionId { get; set; }
        [DataMember]
        public string StrIdLista { get; set; }

    }
}
