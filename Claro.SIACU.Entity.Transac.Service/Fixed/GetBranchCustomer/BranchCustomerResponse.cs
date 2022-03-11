using Claro.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBranchCustomer
{
    [DataContract]
    public class BranchCustomerResponse
    {
       [DbColumn("CODSOLOT")]
       [DataMember]
       public string CODSOLOT {get;set;}
       [DbColumn("TIPTRA")]
       [DataMember]
       public string TIPTRA{ get; set;}
       [DbColumn("DESCRIPCION")]
       [DataMember]
       public string DESCRIPCION {get; set;}
       [DbColumn("SUCURSAL")]
       [DataMember]
       public string SUCURSAL {get; set;}
       [DbColumn("CODCLIENTE")]
       [DataMember]
       public string CODCLIENTE {get; set;}
       [DbColumn("CONTRATO")]
       [DataMember]
       public string CONTRATO { get; set; }
    }
}
