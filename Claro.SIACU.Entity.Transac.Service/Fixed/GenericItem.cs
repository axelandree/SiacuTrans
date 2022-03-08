using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    //[Serializable]
    [DataContract]
    public class GenericItem
    {
        public GenericItem() { }

        public GenericItem(string vCodigo, string vDescripcion)
        {
            Codigo = vCodigo;
            Descripcion = vDescripcion;
        }
        public GenericItem(string vCodigo, string vCodigo2, string vDescripcion)
        {
            Codigo = vCodigo;
            Codigo2 = vCodigo2;
            Descripcion = vDescripcion;
        }

        [DataMember]
        public string Codigo { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Codigo2 { get; set; }
        [DataMember]
        public string Codigo3 { get; set; }
        [DataMember]
        public string Descripcion2 { get; set; }
        [DataMember]
        public string Numero { get; set; }
        [DataMember]
        public string Estado { get; set; }
        [DataMember]
        public string Fecha { get; set; }
        [DataMember]
        public int Cod_tipo_servicio { get; set; }
        [DataMember]
        public string Id_motivo { get; set; }
        [DataMember]
        public string Tipo { get; set; }
        [DataMember]
        public string Agrupador { get; set; }
        [DataMember]
        public string Condicion { get; set; }
        
        [DataMember]
        public string ParameterID { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Valor_C { get; set; }
        [DataMember]
        public string msisdn { get; set; }

    }
}