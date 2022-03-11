using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC
{
    [DataContract]
    public class RegistarInstaDecoAdiHFCRequest : Claro.Entity.Request
    {
        [DataMember]
        public Customer Customer { get; set; }
        /*
         * ContractId
         * CustomerID
         * LastName
         * Name
         * FullName
         * LegalCountry
         * Departament
         * Province
         * District
         * Address
         * Email
         * DocumentType
         * DocumentNumber
         * BillingCycle
         * BusinessName
         * PhoneReference
         * Telephone
         * CodigoService
         * 
         * DateProgrammingSot
         * FringeHorary
         * LegalAgent
         */
        [DataMember]
        public ServiceByPlan ServiceByPlan { get; set; }

        /*
         * TIPO_SERVICIO
         * IDEQUIPO
         * CANT_EQUIPO
         * SNCODE
         * SPCODE
         * GRUPO_SERV
         * Codtipequ
         */
        [DataMember]
        public string DescripcionServicio { get; set; }
        [DataMember]
        public string CodigoTipoTrabajo { get; set; }
        [DataMember]
        public string CodigoPlano { get; set; }
        [DataMember]
        public string CodigoCampana { get; set; }
        [DataMember]
        public string CargoFijoCIGV { get; set; }
        [DataMember]
        public string CargoFijoSIGV { get; set; }
        [DataMember]
        public string MontoIGV { get; set; }
        [DataMember]
        public string AplicacaBono { get; set; }
        [DataMember]
        public string FlagTBono { get; set; }
        [DataMember]
        public string PeriodoBono { get; set; }
        [DataMember]
        public string CargoBono { get; set; }
        [DataMember]
        public string CostoInstalacion { get; set; }
        //**//
        [DataMember]
        public string CurrentPlan { get; set; }
        [DataMember]
        public string NumeroClaro { get; set; }
        [DataMember]
        public DateTime FechaActivacion { get; set; }
        [DataMember]
        public string EstadoLinea { get; set; }
        [DataMember]
        public string Fidelizar { get; set; }
        [DataMember]
        public string CodigoUbigeo { get; set; }
        [DataMember]
        public string CodigoZona { get; set; }
        [DataMember]
        public List<listaRequestOpcional> listaRequestOpcional { get; set; }

        [DataMember]
        public string CanalAtencion { get; set; }

        [DataMember]
        public string CodigoSistema { get; set; }

        [DataMember]
        public DateTime FechaProgramacion { get; set; }

        [DataMember]
        public string FranjaHoraria { get; set; }
    }
}
