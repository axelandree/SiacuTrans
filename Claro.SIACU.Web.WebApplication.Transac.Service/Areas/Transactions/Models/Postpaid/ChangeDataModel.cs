using System;
using Claro.SIACU.Web.WebApplication.Transac.Service;
using System.Collections.Generic;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class ChangeDataModel
    {
        public string strIdSession { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool blnInteract { get; set; }
        public string strFullPathPDF { get; set; }
        public FixedTransacService.JobType[] JobTypes { get; set; }
        public string strCustomerId { get; set; }
        public string JobTypeComplementarySalesHFC { get; set; }
        public bool chkEmail { get; set; }
        public string MessageSelectJobTypes { get; set; }
        public string MessageSelectDate { get; set; }
        public string MessageErrorUbigeo { get; set; }
        public string MessageValidate { get; set; }
        public string MessageNoValidate { get; set; }
        public string MessageNsTimeZone { get; set; }
        public string DNI_RUC { get; set; }
        public string strTelReferencia { get; set; }
        public bool Flag_Email { get; set; }
        public string fullNameUser { get; set; }
        //
        public string strObjidContacto { get; set; }
        public string strCacDac { get; set; }
        public string strCacDacId { get; set; }
        public string Customer { get; set; }
        public string Accion { get; set; }
        public string strCargo { get; set; }
        public string strDNI { get; set; }
        public string strPhone { get; set; }
        public string strMovil { get; set; }
        public string strFax { get; set; }
        public string strMail { get; set; }
        public string strNombreComercial { get; set; }
        public string strContactoCliente { get; set; }
        public DateTime dateFechaNacimiento { get; set; }
        public string strNacionalidad { get; set; }
        public string strNacionalidadId { get; set; }
        public string strSexo { get; set; }
        public string strEstadoCivil { get; set; }
        public string strEstadoCivilId { get; set; }
        public string hidSupJef { get; set; }
        public string DesAccion { get; set; }
        public string strNameComplet { get; set; }
        public string strCodigoAplicativo { get; set; }
        public string strPasswordAplicativo { get; set; }
        public string strTipoDocumento { get; set; }
        public string strTransaccion { get; set; }
        public string account { get; set; }
        public string RepresentLegal { get; set; }
        public string strNote { get; set; }
        public string AdressDespatch { get; set; }
        public string strTelefono { get; set; }
        public string strMailChange { get; set; }
        public string strApellidos { get; set; }

        public string strApellidosPat { get; set; }
        public string strApellidosMat { get; set; }
        public string strNombres { get; set; }
        public string strRazonSocial { get; set; }

        public string CurrentUser { get; set; }
        public string strMotivo { get; set; }
        public string strFlagPlataforma { get; set; }
        public string strTipoCliente { get; set; }
        public string strCuenta { get; set; }
        public int intSeqIn { get; set; }

        #region tipificacion
        public string tipo { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string notes { get; set; }
        public string tipoCode { get; set; }
        public string claseCode { get; set; }
        public string subClaseCode { get; set; }
        #endregion

        public string strCodAgente { get; set; }
        public string strNombAgente { get; set; }
        public string strEmailSend { get; set; }
        public string strTipoDocumentoRL { get; set; }
        public string strTxtTipoDocumento { get; set; }
        public string strTxtTipoDocumentoRL { get; set; }
        #region TipiTemplate
        public TemplateInteractionModel oChangeDataTemplate { get; set; }
        #endregion
        public string FLAG_CUSTOMER { get; set; }
        public string FLAG_DATA_MINOR { get; set; }


        //cm
        public string lugarNacimiento { get; set; }
        public string bmId { get; set; }
        //cm

        public string direccionLegal { get; set; }
        public string direccionReferenciaLegal { get; set; }
        public string paisLegal { get; set; }
        public string departamentoLegal { get; set; }
        public string provinciaLegal { get; set; }
        public string distritoLegal { get; set; }
        public string urbanizacionLegal { get; set; }
        public string codigoPostalLegal { get; set; }
        public string referenciaFact { get; set; }

        public string adrNote1 { get; set; }
        public string FECHA_ACT { get; set; }
        //Datos Sensibles Cel.1
        public string strTipoDocumentoAnt { get; set; }
        public string strNroDocAnt { get; set; }
        public string strRazonSocialNew { get; set; }
        public string strNombreComercialNew { get; set; }
        public string DNIRUCNew { get; set; }
        public bool strParticipante { get; set; }

        public string listaRL { get; set; }
        public string listaRLOLD { get; set; }
        public string NameRepLegalCurrent { get; set; }

        public List<listaRepresentanteLegal> listaRepresentanteLegal { get; set; }
    }
}