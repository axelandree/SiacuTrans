using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInteractionClient
{
       [DataContract(Name = "GetInteractionClientRequestCommon")]
    public class InteractionClientRequest:Claro.Entity.Request
    {
       [DataMember]
       public  string straccount{get;set;}
       
       [DataMember]
       public  string strtelephone{get;set;}


       [DataMember]
       public  int intcontactobjid1{get;set;}


       [DataMember]
       public  int intsiteobjid1{get;set;}

       [DataMember]
       public  string strtipification{get;set;}

       [DataMember]
       public int intnrorecordshow { get; set; }
    }
}
