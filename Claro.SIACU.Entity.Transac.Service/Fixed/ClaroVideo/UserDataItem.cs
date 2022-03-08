using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name="UserDataitem")]
    public class UserDataItem
    {
        [DataMember(Name="key")]
        public string key { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }
    }
}
