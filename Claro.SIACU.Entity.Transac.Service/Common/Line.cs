using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    [DataContract]
    public class Line
    {
        [DataMember]
        public string PhoneNumber{get;set;}
        [DataMember]
        public string LineStatus{get;set;}
        [DataMember]
        public string MainBalance{get;set;}
        [DataMember]
        public string ExpirationDateBalance { get; set; }
        [DataMember]
        public string TriosChanguesFree {get;set;}
        [DataMember]
        public string TariffChangesFree {get;set;}
        [DataMember]
        public string TariffPlan{get;set;}
        [DataMember]
        public string ActivationDate{get;set;}
        [DataMember]
        public string DolDate { get; set; }
        [DataMember]
        public string ExpDate_Line{get;set;}
        [DataMember]
        public string NumIMSI{get;set;}
        [DataMember]
        public string StatusIMSI{get;set;}
        [DataMember]
        public string NumICCID{get;set;}
        [DataMember]
        public string NumFamFriends{get;set;}
        [DataMember]
        public string TriacionType{get;set;}
        [DataMember]
        public string ProviderID{get;set;}
        [DataMember]

        public DateTime DateStatus{get;set;}
        [DataMember]
        public string Reason{get;set;}
        [DataMember]
        public string Plan{get;set;}
        [DataMember]
        public string TermContract { get; set; }
        [DataMember]
        public string Sale { get; set; }
        [DataMember]
        public string Bell { get; set; }
        //[DataMember]
        //public DateTime InsertedThe { get; set; }
        //[DataMember]
        //public string  InsertedBy { get; set; }
        //[DataMember]
        //public DateTime ValidFrom { get; set; }
        //[DataMember]
        //public string  ChangeBy { get; set; }
        [DataMember]
        public string FlagPlatform { get; set; }
        [DataMember]
        public string PIN1 { get; set; }
        [DataMember]
        public string  PIN2 { get; set; }
        [DataMember]
        public string PUK1 { get; set; }
        [DataMember]
        public string PUK2 { get; set; }
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string CodPlanTariff { get; set; }

        [DataMember]
        //public string _Plazo_Contrato{get;set;}[DataMember]
        //public string _Vendedor{get;set;}[DataMember]
        //public string _Campana{get;set;}[DataMember]
        //public DateTime _Introducido_El{get;set;}[DataMember]
        //public string _Introducido_Por{get;set;}[DataMember]
        //public DateTime _Valido_Desde{get;set;}[DataMember]
        //public string _Cambiado_Por{get;set;}[DataMember]
        //public string _Flag_Plataforma{get;set;}[DataMember]
        //public string _PIN1{get;set;}[DataMember]
        //public string _PIN2{get;set;}[DataMember]
        //public string _PUK1{get;set;}[DataMember]
        //public string _PUK2{get;set;}[DataMember]
        //public string _ContratoID{get;set;}[DataMember]
        //public string _Cod_Plan_Tarifario{get;set;}[DataMember]

        public string CNTNumber { get; set; }
        [DataMember]
        public string IsCNTPossible { get; set; }
        [DataMember]
        public string SubscriberStatus { get; set; }
        //[DataMember]
        //public string StateAgreement { get; set; }
        //[DataMember]
        //public string DateEndAgreement { get; set; }
        //[DataMember]
        //public string FlagTFI { get; set; }
        //[DataMember]
        //public string Account { get; set; }
        //[DataMember]
        //public string DateDeactivation { get; set; }
        //[DataMember]
        //public string TicklerCode { get; set; }

        [DataMember]
        public string FlagLoadDataLine{get;set;}
        [DataMember]
        public string IsTFI { get; set; }
        [DataMember]
        public string IsDTH { get; set; }
        [DataMember]
        public string OutstandingBalance { get; set; }
        [DataMember]
        public string MinuteBalance_Select { get; set; }
        [DataMember]
        public string ExpDate_Select { get; set; }
        [DataMember]
        public string IsSelect{get;set;}
        //[DataMember]

        //public string _Fecha_Baja{get;set;}[DataMember]
        //public string _Fecha_Desactivacion{get;set;}[DataMember]
        //public string _Fecha_Recarga{get;set;}[DataMember]
        //public string _Descripción_Fecha{get;set;}[DataMember]
        //public Double Amount_Last_Recharge { get; set; }
    }
}
