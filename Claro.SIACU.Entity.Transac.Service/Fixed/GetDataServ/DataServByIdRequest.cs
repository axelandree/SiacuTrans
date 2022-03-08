using Claro.Data;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDataServ
{
    //[DataContract(Name = "DataServByIdRequesLTE")]
    public class DataServByIdRequest : Claro.Entity.Request
    {
        [DbColumn("STRIDSERV")]
        [DataMember]
        public string strIdServ { get; set; }

    }
}
