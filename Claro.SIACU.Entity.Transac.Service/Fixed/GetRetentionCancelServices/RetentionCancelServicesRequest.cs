using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert
{

    [DataContract]
    public class RetentionCancelServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public int vNivel { get; set; }

        [DataMember]
        public string vtransaction { get; set; }

        [DataMember]
        public int vEstado { get; set; }

        [DataMember]
        public int vTipoLista { get; set; }

        [DataMember]
        public int vIdMotive { get; set; }

        [DataMember]
        public int vIdTypeWork { get; set; }

        [DataMember]
        public int ParameterID { get; set; }
        
        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public string Tipo { get; set; }

        [DataMember]
        public string Valor { get; set; }

        [DataMember]
        public string Message { get; set; }

        // GetObtainPenalidadExt  
        [DataMember]
        public string NroTelefono { get; set; }

        [DataMember]
        public DateTime FechaPenalidad { get; set; }

        [DataMember]
        public double NroFacturas { get; set; }

        [DataMember]
        public double CargoFijoActual { get; set; }

        [DataMember]
        public double CargoFijoNuevoPlan { get; set; }

        [DataMember]
        public double DiasxMes { get; set; }

        [DataMember]
        public double CodNuevoPlan { get; set; }

        [DataMember]
        public double ValorApadece { get; set; }   

        [DataMember]
        public double CodMessage { get; set; }

        [DataMember]
        public int Phone { get; set; }

        [DataMember]
        public int CodId { get; set; }

        #region Proy-32650
        [DataMember]
        public int vBonoID { get; set; }

        [DataMember]
        public int CustumerId { get; set; }

        [DataMember]
        public string SnCode { get; set; }

        [DataMember]
        public int TmCodeCom { get; set; }
        #endregion

    }
}
