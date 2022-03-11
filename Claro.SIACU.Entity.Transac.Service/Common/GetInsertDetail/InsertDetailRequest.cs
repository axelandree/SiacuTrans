using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertDetail
{
    [DataContract(Name="InsertDetailREquestCommon")]
   public  class InsertDetailRequest:Claro.Entity.Request
    {

        [DataMember]
        public InteractionDet Item { get; set; }
    }
}
