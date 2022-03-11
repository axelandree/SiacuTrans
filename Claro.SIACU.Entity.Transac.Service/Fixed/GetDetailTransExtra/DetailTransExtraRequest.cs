using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDetailTransExtra
{
    [DataContract]
    public class DetailTransExtraRequest : Claro.Entity.Request
    {
        [DataMember]
        public int CodSolOt { get; set; }
        [DataMember]
        public int iCustomerId { get; set; }
        [DataMember]
        public string vDireccionFacturacion { get; set; }
        [DataMember]
        public string vNotaDireccion { get; set; }
        [DataMember]
        public string vDistrito { get; set; }
        [DataMember]
        public string vProvincia { get; set; }
        [DataMember]
        public string vCodigoPostal { get; set; }
        [DataMember]
        public string vDepartamento { get; set; }
        [DataMember]
        public string vPais { get; set; }
        [DataMember]
        public Nullable<int> iFlagDireccFact { get; set; }
        [DataMember]
        public string vFechaReg { get; set; }
        [DataMember]
        public string vUsuarioReg { get; set; }
        [DataMember]
        public Nullable<double> iMonto { get; set; }
        [DataMember]
        public string vFechaVig { get; set; }
        [DataMember]
        public string vObservacion { get; set; }
        [DataMember]
        public string vAplicacion { get; set; }
        [DataMember]
        public string vCodId { get; set; }
        [DataMember]
        public Nullable<int> iFlag_Cobro { get; set; }
        
    }
}
