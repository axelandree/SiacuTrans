using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class RetentionCancelServicesModel
    {
        public bool bGeneratedPDF { get; set; }
        public string strFullPathPDF { get; set; } 
        public FixedTransacService.JobType[] JobTypes { get; set; }
        public string CustomerRequestId { get; set; }
        public string JobTypeComplementarySalesHFC { get; set; }

        public string MessageSelectJobTypes { get; set; }
        public string MessageSelectDate { get; set; }
        public string MessageErrorUbigeo { get; set; }
        public string MessageValidate { get; set; }
        public string MessageNoValidate { get; set; }
        public string MessageNsTimeZone { get; set; }
        public string DNI_RUC { get; set; }
        public string TelefonoReferencia { get; set; }

        //
        public string Id { get; set; }
        public string Reintegro { get; set; }
        public string Customer { get; set; }
        public string Cuenta { get; set; }
        public string Plan { get; set; }
        public string Telephone { get; set; }
        public string Total { get; set; }
        public string TotalSms { get; set; }
        public string TotalMms { get; set; }
        public string TotalGprs { get; set; }
        public string FecAgCU { get; set; }
        public string Accion { get; set; }
        public string CargoFinal { get; set; }
        public string ContractId { get; set; }
        public string CodePlanInst { get; set; }
        public string CurrentUser { get; set; }
        public string Transaction { get; set; }
        public string Note { get; set; }
        public string CodeTipification { get; set; }
        public string MonthEmision { get; set; }
        public string YearEmision { get; set; }
        public string CacDac { get; set; }
        public string Sn { get; set; }
        public string IpServidor { get; set; }
        public string IdSession { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public string InvoceNumber { get; set; }
        public string NroDoc { get; set; }
        public string LastName { get; set; }
        public string RepresentLegal { get; set; }
        public string StardDate { get; set; }
        public string EndDate { get; set; }
        public string CodePerfil { get; set; }
        public string NameComplet { get; set; }
        public string TypeClient { get; set; }
        public string CodTypeClient { get; set; }
        public string RazonSocial { get; set; }
        public string Periodo { get; set; }
        public string TypeDoc { get; set; }
        public string localad { get; set; }
        public string DescCacDac { get; set; }
        public string LocalAdd { get; set; }
        public string vValidateETA { get; set; }
        public string vdDateProgramming { get; set; }
        public string vSchedule { get; set; }
        public string vCodigoRequestAct { get; set; }
        public string vJobTypes { get; set; }
        public string vMotiveSot { get; set; }
        public string CodMotiveSot { get; set; }
        public string vServicesType { get; set; }
        public string vAttachedQuantity { get; set; }
        public string hidSupJef { get; set; }
        public string DesAccion { get; set; }
        public string DesSubMotivo { get; set; }
        public string TotalInversion { get; set; }
        public string LegalDepartament { get; set; }
        public string PagoAPADECE { get; set; }
        public string Reference { get; set; }
        public string Departament_Fact { get; set; }
        public string AdressDespatch { get; set; }
        public string District { get; set; }
        public string Pais_Fac { get; set; }
        public string Provincia { get; set; }
        public string Destinatarios { get; set; }
        public string IdConsulta { get; set; }
        public string Account { get; set; }
        public string BillingCycle { get; set; }
        public string Msisdn { get; set; }
        public string Reason { get; set; }
        public string fechaActual { get; set; }
        public string flagNdPcs { get; set; }
        public string FlagOccApadece { get; set; }
        public string MontoFidelizacion { get; set; }
        public string MontoPCs { get; set; }
        public string DocumentNumber { get; set; }
        public string PlaneCodeBilling { get; set; }
        public string SubMotivePCS { get; set; }
        public string Observation { get; set; }
        public string MotivePCS { get; set; }
        public string montoPenalidad { get; set; }
        public string AreaPCs { get; set; }
        public string CodigoInteraction { get; set; }
        public string CodigoService { get; set; }
        public string DateProgrammingSot { get; set; }
        public string FringeHorary { get; set; }
        public string Trace { get; set; }
        public string TypeWork { get; set; }
        public string Aplica { get; set; }
        public string ValidaETA { get; set; }
        public string TotInversion { get; set; }
        public string OBJID_SITE { get; set; }
        public string FechaCompromiso { get; set; }
        public string FechaProgramacion { get; set; }
        public string NroCelular { get; set; }
        public string DesMotivos { get; set; }
        public string District_Fac { get; set; }
        public string Provincia_Fac { get; set; }
        public string Code_Plane_Inst { get; set; }
        public string User_Sistemas { get; set; }
        public string User_Aplicacion { get; set; }
        public string Password_User { get; set; }
        public string Ejecuta_Transaction { get; set; }
        public bool Flag_Email { get; set; }
        public string GeneroCaso { get; set; }
        public string Segmento { get; set; }
        public string ProductType { get; set; }

        #region Proy-32650
        public typeHFCLTE typeHFCLTE { get; set; }
        public string idContrato { get; set; }
        public int idCampana { get; set; }
        public string idPorcentaje { get; set; }
        public string montoTotalSA { get; set; }
        public string mesDesc { get; set; }
        public string mesVal { get; set; }
        public string snCode { get; set; }
        public string costInst { get; set; }//costo para tipis y constancia
        public string costoWSInst { get; set; }//costo para enviar al servicio
        public string flagCargFijoServAdic { get; set; }
        public string desServicioPVU { get; set; }
        public string codServAdic { get; set; }
        public string descServAdic { get; set; }
        public string montTariRete { get; set; }
        public string name { get; set; }
        public string costoServiciosinIGV { get; set; }
        public string costoServicioconIGV { get; set; }
        public string emailUsuario { get; set; }
        public string objIdContacto { get; set; }
        public string contactoCliente { get; set; }
        public string fechaNac { get; set; }
        public string idLugarNac { get; set; }
        public string sexo { get; set; }
        public string idEstadoCivil { get; set; }
        public string cargo { get; set; }
        public string fax { get; set; }
        public bool updateDataMen { get; set; }
        public string lugarNac { get; set; }
        public string estadoCivil { get; set; }
        public bool aplicaPromoFact { get; set; }
        public string clase { get; set; }
        public string subClase { get; set; }
        public string objIdSite { get; set; }
        public string planContract { get; set; }
        public typeRETEFIDE typeRETEFIDE { get; set; }
        public string ValAccion { get; set; }

        public string ReferenceOfTransaction { get; set; } //Resultado
        public string DiscountDescription { get; set; }
        public string RetentionBonusServAdic { get; set; }
        public string RegularBonusServAdic { get; set; }
        public string Constancia { get; set; }
        public string flagServDeco { get; set; }
        public string CodigoAsesor { get; set; }
        public string NombreAsesor { get; set; }
        public string mesDescripcion { get; set; }
        public string Modalidad { get; set; }

        //PROY-32650  II
        public string GRUPOSERV { get; set; }
        public string  ValorIGV { get; set; }
        public string JobTypesAccion { get; set; }
        public string FringeHoraryAccion { get; set; }
        public string PlanActual { get; set; }
        public string EstadoLinea { get; set; }
        public string Ubigeo { get; set; }
        public string spCode { get; set; }
        public string CantidadEquipo { get; set; }
        public string IdEquipo { get; set; }
        public string CodTipEquipo { get; set; }
        public string DescripcionGrupo { get; set; }
        public string CostoRegularSinIGV { get; set; }
        public string CostoRegularIGV { get; set; }
        public string CargoBono { get; set; }
        public string JobSubTypesAccion { get; set; }
        public DateTime FechaProgramacionAccion { get; set; }
        public string CodigoPoblado { get; set; }
        public string CodSerPvu { get; set; }
        public string MontoDescuento { get; set; }
        public string SOT { get; set; }
        public DateTime FechaActivacion { get; set; }
        public string IdAccion { get; set; }
        public string PaqueteODeco { get; set; }
        //PROY-32650  II

        //INICIO - PROY-140319 III
        public string bonoId { get; set; }
        public string PeriodoBono { get; set; }
        public string codId { get; set; }
        public string BonoRetentionFidelizacion { get; set; }
        public string VigenciaRetFid { get; set; }
        public string InternetActual { get; set; }
        //FIN- PROY-140319 III
        #endregion
    }

    public enum typeHFCLTE
    {
        HFC = 0,
        LTE = 1,
    }
    public enum typeRETEFIDE
    {
        RETE = 0,
        FIDE = 1,
        //MENO=2,
    }
}