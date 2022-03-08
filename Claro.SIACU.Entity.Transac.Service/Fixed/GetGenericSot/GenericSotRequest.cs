using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetGenericSot
{
    [DataContract]
    public class GenericSotRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCuId {get;set;}
        [DataMember]
        public string vCoId {get;set;}
        [DataMember]
        public int vTipTra { get; set;}
        [DataMember]
        public string vFeProg {get;set;} 
        [DataMember]
        public string vFranja { get; set;}
        [DataMember]
        public string vCodMotivo {get;set;} 
        [DataMember]
        public string vObserv {get;set;}
        [DataMember]
        public string vPlano {get;set;} 
        [DataMember] 
        public string vUser {get;set;} 
        [DataMember]
        public string idTipoServ {get;set;} 
        [DataMember]
        public string cargo {get;set;}
        [DataMember]
        public int vintCantidadAnexo { get; set; }
    }
}
