using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertDetail
{
     [DataContract(Name = "InsertDetailResponseCommon")]
   public  class InsertDetailResponse
    {
         [DataMember]
         public bool ProcessSucces { get; set; }

         [DataMember]
         public string strFlagInsercion { get; set; }


    }
}
