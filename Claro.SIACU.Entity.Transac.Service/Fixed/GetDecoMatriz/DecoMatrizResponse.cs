using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoMatriz
{
    [DataContract]
    public class DecoMatrizResponse
    {
        [DataMember]
        public string CantidadMaxima { get; set; }

        [DataMember]
        public List<DecoMatriz> ListaMatrizDecos { get; set; }
    }
}
