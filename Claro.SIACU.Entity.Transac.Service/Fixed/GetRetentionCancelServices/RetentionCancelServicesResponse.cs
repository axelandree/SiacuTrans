using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert
{
    [DataContract]
    public class RetentionCancelServicesResponse
    {
        [DataMember]
        public List<GenericItem> AccionTypes { get; set; }

        // GetObtainPenalidadExt

        [DataMember]
        public double AcuerdoIdSalida { get; set; }

        [DataMember]
        public double DiasPendientes { get; set; }

        [DataMember]
        public double CargoFijoDiario { get; set; }

        [DataMember]
        public double PrecioLista { get; set; }

        [DataMember]
        public double PrecioVenta { get; set; }

        [DataMember]
        public double PenalidadPCS { get; set; }

        [DataMember]
        public double PenalidaAPADECE { get; set; }

        [DataMember]
        public bool Resultado { get; set; }

        [DataMember]
        public double NroFacturas { get; set; }

        [DataMember]
        public double CargoFijoActual { get; set; }

        [DataMember]
        public double CargoFijoNuevoPlan { get; set; }

        [DataMember]
        public int CodId { get; set; }

        [DataMember]
        public double ValorApadece { get; set; }

        [DataMember]
        public string CodMessage { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public int Phone { get; set; }

        #region Proy-32650
        [DataMember]
        public List<CurrentDiscountFixedCharge> CurrentDiscounts { get; set; }
        #endregion
    }
}
