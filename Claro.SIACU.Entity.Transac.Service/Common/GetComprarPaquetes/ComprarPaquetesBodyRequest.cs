using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetComprarPaquetes 
{
    //INI - RF-02Evalenzs
    [DataContract(Name = "ComprarPaquetesBodyRequest")]

    public class ComprarPaquetesBodyRequest
    {
    //    public comprarPaquetesRequest comprarPaquetesRequest { get; set; }
        [DataMember]
        public string msisdn { get; set; }
        [DataMember]
        public string monto { get; set; }
        [DataMember]
        public string paquete { get; set; }
        [DataMember]
        public string customerId { get; set; }
        [DataMember]
        public string planBase { get; set; }
        [DataMember]
        public string tipoProducto { get; set; }
        [DataMember]
        public string tipoCliente { get; set; }
        [DataMember]
        public string cicloFact { get; set; }
        [DataMember]
        public string fechaAct { get; set; }
        [DataMember]
        public string cargoFijo { get; set; }
        [DataMember]
        public string tipoPago { get; set; }
        [DataMember]
        public string departamento { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string distrito { get; set; }
        [DataMember]
        public List<ComprarPaquetesListaOpcionalType> listaOpcionalType { get; set; }
    }
    //public class comprarPaquetesRequest
    //{
      
    //}
  
}
