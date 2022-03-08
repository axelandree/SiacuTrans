using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    [DataContract]
    public class ImplementLoyalty
    {
        [DataMember]
        public string CustomerId;
        [DataMember]
        public string DireccionFacturacion;
        [DataMember]
        public string NotasDireccion;
        [DataMember]
        public string Distrito;
        [DataMember]
        public string Provincia;
        [DataMember]
        public string CodigoPostal;
        [DataMember]
        public string Departamento;
        [DataMember]
        public string Pais;
        [DataMember]
        public string FlagDireccFact;
        [DataMember]
        public string UsuarioReg;
        [DataMember]
        public string FechaReg;
    }
}
