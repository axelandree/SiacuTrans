
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataByCount
{
    [DataContract(Name = "DataByCountRequestPostPaid")]
    public class DataByCountRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerId { get; set; }
    }
}
