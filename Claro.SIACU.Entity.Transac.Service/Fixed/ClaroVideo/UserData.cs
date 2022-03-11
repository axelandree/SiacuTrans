using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "UserData")]
    public class UserData
    {
        [DataMember(Name = "item")]
        public List<UserDataItem> item { get; set; }
    }
}
