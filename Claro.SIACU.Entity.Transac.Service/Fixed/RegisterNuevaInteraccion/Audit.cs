using System;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion
{
    [DataContract]
    public class Audit
    {
        [DataMember]
        private string txId;
        [DataMember]
        private string errorCode;
        [DataMember]
        private string errorMsg;
    }
}
