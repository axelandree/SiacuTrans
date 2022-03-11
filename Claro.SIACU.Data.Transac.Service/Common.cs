using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using Fun = Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service;
using Claro.Data;
using Claro.SIACU.Data.Transac.Service.Configuration;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ClaroService = Claro.SIACU.Transac.Service;
using REGISTAUDIT = Claro.SIACU.ProxyService.Transac.Service.RegistroAuditoriaWS;
using OPTIONAUDIT = Claro.SIACU.ProxyService.Transac.Service.ConsultaOpcionesAuditoriaWS;
using AUDIT = Claro.SIACU.ProxyService.Transac.Service.AuditoriaWS;
using SECURITY = Claro.SIACU.Entity.Transac.Service.Common;
using EntitiesConsultaSeguridad = Claro.SIACU.ProxyService.Transac.Service.SIACUConsultaSeguridad;
using Claro.SIACU.Entity.Transac.Service.Prepaid;
using AUDIT_CREDENTIALS = Claro.SIACU.ProxyService.Transac.Service.RedirectWS;
using System.Net;
using Claro.SIACU.Entity.Transac.Service.Common.GetContractByPhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterServiceCommercial;
using EntitiesOpcionesAuditoria = Claro.SIACU.ProxyService.Transac.Service.ConsultaOpcionesAuditoriaWS;
using Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM;
using Claro.SIACU.ProxyService.Transac.Service.ServiciosPostpagoWS;
using COMMON_IGV = Claro.SIACU.ProxyService.Transac.Service.SIACU.ConsultIGV;
using COMMON_ENVIOCORREOSB = Claro.SIACU.ProxyService.Transac.Service.SIACU.EnvioCorreoSB;
using GENERATEPDF = Claro.SIACU.ProxyService.Transac.Service.WSGeneratePDF;
using COMMON_IMEI = Claro.SIACU.ProxyService.Transac.Service.WSDwh;
using COMMON_MARKMODEL = Claro.SIACU.ProxyService.Transac.Service.EbsSWSAP;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Xml.Serialization;
using Claro.Web;
using Constant = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultImei;
using Claro.SIACU.Entity.Transac.Service.Common.GetOffice;
using SIACU_INTERAC = Claro.SIACU.ProxyService.Transac.Service.SIACU.Interacciones;
using EntitiesCommon = Claro.SIACU.Entity.Transac.Service.Common;
using ADMCU = Claro.SIACU.ProxyService.Transac.Service.SIACFixed.CrewManagement;
using PuntosAtencionWS = Claro.SIACU.ProxyService.Transac.Service.SIACU.PuntosAtencion;
using Claro.Transversal.ProxyService.ConsultaClaves;
using ServiceModel = System.ServiceModel;
using Claro.SIACU.Entity.Transac.Service.Common.GetValidateCollaborator;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign;
using Claro.SIACU.Entity.Transac.Service.Common.GetConsultServiceBono;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterBonoSpeed;
using Claro.SIACU.Entity.Transac.Service.Common.PostValidateDeliveryBAV;
using Claro.SIACU.Entity.Transac.Service.Common.GetReadDataUser;
using Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign;
using Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign;
using COMMON_EnviarNotificacion = Claro.SIACU.ProxyService.Transac.Service.SIACUEnviarNotificacion;
using CONSULTA_SECURITY = Claro.SIACU.ProxyService.Transac.Service.SIACUConsultaSeguridad;
using AUDIT_SECURITY = Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions;
using LDEDP = Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE;
using Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LDE;

using AUDIT_SECURITYDP = Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LPOPU;
using LPOPU = Claro.SIACU.ProxyService.Transac.Service.SIACSecurity.Permissions.LPOPU;

using CargaDocumentoOnBase = Claro.SIACU.ProxyService.Transac.Service.CargaDocumentOnBaseWS;
using Claro.SIACU.Entity.Transac.Service.Common.TargetOnBase;

using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.Configuration;
using System.Collections;
using Claro.SIACU.Entity.Transac.Service.Common.GetUploadDocumentOnBase;

//using ONBASELOAD = Claro.SIACU.ProxyService.IFI.SIACU.OnBaseLoadDocumentIFI;
using onBaseLoad = Claro.SIACU.ProxyService.Transac.Service.SIACU.OnBaseLoad;
using Claro.SIACU.Entity.Transac.Service.Common.GetGenerateConstancy;

namespace Claro.SIACU.Data.Transac.Service
{
    public class Common
    {
        #region Generar Constancia
        /// <summary>
        /// Método que genera Constancia PDF
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns></returns>
        public static bool GenerateConstancyPDF(string idSession,string transaction,ParametersGeneratePDF parameters, ref string strErroMsg)
        {
            bool blnResult;

            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateConstancyPDF()");
                string xml = BuildXML_Constancy(parameters);
                Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"));

                if (String.IsNullOrEmpty(parameters.StrCasoInter)) {
                    parameters.StrCasoInter = "X";
                } 
                Claro.Web.Logging.Info(idSession, transaction,"After strNamePDF:  "+strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();

                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;

                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;

                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;

                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;

                string strInputTrama = Fun.Functions.CreateXML(objGenerarPDFRequest);

                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });

                string strOutputTrama =Fun.Functions.CreateXML(objGenerarPDFResponse);

                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    blnResult = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    blnResult = true;
                }

                GenerateTrazability(idSession, transaction, parameters.StrNombreArchivoTransaccion, strInputTrama, strOutputTrama, parameters.StrCasoInter);

            }
            catch (Exception ex)
            {
                strErroMsg = SIACU.Transac.Service.Functions.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                blnResult = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateConstancyPDF()");
            return blnResult;
        }

        public static GenerateConstancyResponse GetConstancyPDFWithOnbase(string idSession, string transaction, ParametersGeneratePDF parameters, ref string strErroMsg)
        {
            GenerateConstancyResponse objConstancyResponse = new GenerateConstancyResponse();
            objConstancyResponse.Generated = false;
            byte[] databytes;
            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateConstancyPDF()");
                string xml = BuildXML_Constancy(parameters);
                Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"));
                if (String.IsNullOrEmpty(parameters.StrCasoInter))
                {
                    parameters.StrCasoInter = "X";
                }
                Claro.Web.Logging.Info(idSession, transaction, "After strNamePDF:  " + strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();
                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;
                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;
                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;
                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;
                string strInputTrama = Fun.Functions.CreateXML(objGenerarPDFRequest); //Fun.CreateXML(objGenerarPDFRequest);
                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });
                string strOutputTrama = Fun.Functions.CreateXML(objGenerarPDFResponse); // Fun.CreateXML(objGenerarPDFResponse);
                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    objConstancyResponse.Generated = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    string strServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
                    string strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");
                    string strNameResultPDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", strServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                    
                    objConstancyResponse.Document = Convert.ToString(objGenerarPDFRequestDriver.driver);
                    objConstancyResponse.FullPathPDF =  strNameResultPDF;  
                    objConstancyResponse.Path = objGenerarPDFRequestOutput.directory;
                    objConstancyResponse.Generated = true;
                    objConstancyResponse.Drive = objGenerarPDFRequestDriver.driver;

                    
                    bool dfsf = DisplayFileFromServerSharedFile(idSession, transaction, strNameResultPDF, out databytes);
                    objConstancyResponse.bytesConstancy = dfsf ? databytes : null;

                }
            }
            catch (Exception ex)
            {
                strErroMsg = Claro.Utils.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                objConstancyResponse.Generated = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateConstancyPDF()");
            return objConstancyResponse;
        }

        public static bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            bool blnResult;
            FileInfo fiInfo = null;
            FileStream fs = null;
            BinaryReader br = null;
            mydata = null;
            Siacu_Impersonation obj = new Siacu_Impersonation();
            try
            {
                string user = string.Empty;
                string pass = string.Empty;
                string domain = string.Empty;
                bool credentials = false;


                credentials = Get_Credentials(strIdSession, strTransaction, ConfigurationManager.AppSettings("ConexionSamba"), out user, out pass, out domain);
                if (obj.impersonateValidUser(user, domain, pass))
                {
                    fiInfo = new FileInfo(strPath);
                    fs = new FileStream(fiInfo.ToString(), FileMode.Open);
                    br = new BinaryReader(fs);
                    mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));

                    blnResult = true;
                }
                else
                {

                    blnResult = false;
                }

                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (br != null)
                {
                    br.Close();
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }

            obj.undoImpersonatiom();

            return blnResult;
        }

        public static bool Get_Credentials(string strIdSession, string strTransaction, string strConexion, out string struser, out string strpass, out string strdomain)
        {
            try
            {
                ConfigurationRecord ConfigurationCredentials = new ConfigurationRecord(strConexion);
                struser = ConfigurationCredentials.LeerUsuario();

                strpass = ConfigurationCredentials.LeerContrasena();

                strdomain = ConfigurationCredentials.LeerBaseDatos();


                return true;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Claro.Utils.GetExceptionMessage(ex));

                struser = string.Empty;
                strpass = string.Empty;
                strdomain = string.Empty;
                return false;
            }
        }


        /// <summary>
        ///  Método que construye XML
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static string BuildXML_Constancy(ParametersGeneratePDF parameters)
        {
            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<PLANTILLA>";
            xml += "<FORMATO_TRANSACCION>" + parameters.StrNombreArchivoTransaccion + "</FORMATO_TRANSACCION>";
            xml += "<NRO_SERVICIO>" + parameters.StrNroServicio + "</NRO_SERVICIO>";
            xml += "<TITULAR_CLIENTE>" + parameters.StrTitularCliente + "</TITULAR_CLIENTE>";
            xml += "<CONTACTO_CLIENTE>" + parameters.StrContactoCliente + "</CONTACTO_CLIENTE>";
            xml += "<PLAN_ACTUAL>" + parameters.StrPlanActual + "</PLAN_ACTUAL>";
            xml += "<CENTRO_ATENCION_AREA>" + parameters.StrCentroAtencionArea + "</CENTRO_ATENCION_AREA>";
            xml += "<TIPO_DOC_IDENTIDAD>" + parameters.StrTipoDocIdentidad + "</TIPO_DOC_IDENTIDAD>";
            xml += "<NRO_DOC_IDENTIDAD>" + parameters.StrNroDocIdentidad + "</NRO_DOC_IDENTIDAD>";
            xml += "<FECHA_TRANSACCION_PROGRAM>" + parameters.StrFechaTransaccionProgram + "</FECHA_TRANSACCION_PROGRAM>";
            xml += "<CASO_INTER>" + parameters.StrCasoInter + "</CASO_INTER>";
            xml += "<REPRES_LEGAL>" + parameters.StrRepresLegal + "</REPRES_LEGAL>";
            xml += "<NRO_DOC_REP_LEGAL>" + parameters.StrNroDocRepLegal + "</NRO_DOC_REP_LEGAL>";
            xml += "<CUSTOMER_ID>" + parameters.StrCustomerId + "</CUSTOMER_ID>";
            xml += "<CUENTA_POSTPAGO>" + parameters.StrCuentaPostpago + "</CUENTA_POSTPAGO>";
            xml += "<NOTAS>" + parameters.StrNotas + "</NOTAS>";
            xml += "<TELF_REFERENCIA>" + parameters.StrTelfReferencia + "</TELF_REFERENCIA>";
            xml += "<CICLO_FACTURACION>" + parameters.StrCicloFacturacion + "</CICLO_FACTURACION>";
            xml += "<FECHA_EJECUCION>" + parameters.StrFechaEjecucion + "</FECHA_EJECUCION>";
            xml += "<COD_USUARIO>" + parameters.StrCodUsuario + "</COD_USUARIO>";
            xml += "<NOMBRE_AGENTE_USUARIO>" + parameters.StrNombreAgenteUsuario + "</NOMBRE_AGENTE_USUARIO>";
            xml += "<APLICA_EMAIL>" + parameters.StrAplicaEmail + "</APLICA_EMAIL>";
            xml += "<EMAIL>" + parameters.StrEmail + "</EMAIL>";
            xml += "<APLICA_OTRO_CONTACTO>" + parameters.StrAplicaOtroContacto + "</APLICA_OTRO_CONTACTO>";
            xml += "<CONTACTO_OTRO>" + parameters.StrContactoOtro + "</CONTACTO_OTRO>";
            xml += "<NRO_DOC_CONTACTO_OTRO>" + parameters.StrNroDocContactoOtro + "</NRO_DOC_CONTACTO_OTRO>";
            xml += "<MOTIVO_PARIENTE>" + parameters.StrMotivoPariente + "</MOTIVO_PARIENTE>";
            xml += "<TELF_OTRO_CONTACTO>" + parameters.StrTelfOtroContacto + "</TELF_OTRO_CONTACTO>";
            xml += "<CANAL_ATENCION>" + parameters.StrCanalAtencion + "</CANAL_ATENCION>";
            xml += "<FLAG_PLANTILLA_PLAZO>" + parameters.StrFlagPlantillaPlazo + "</FLAG_PLANTILLA_PLAZO>";
            xml += "<ESCENARIO_SERVICIO_COM>" + parameters.StrEscenarioServicioCom + "</ESCENARIO_SERVICIO_COM>";
            xml += "<APLICA_PROGRAMACION>" + parameters.StrAplicaProgramacion + "</APLICA_PROGRAMACION>";
            xml += "<CF_SERVICIO_COM>" + parameters.StrCfServicioCom + "</CF_SERVICIO_COM>";
            xml += "<FECHA_PLAZO>" + parameters.StrFechaPlazo + "</FECHA_PLAZO>";
            xml += "<PLAZO>" + parameters.StrPlazo + "</PLAZO>";
            xml += "<CANAL_ATENCION>" + parameters.StrCanalAtencion + "</CANAL_ATENCION>";
            
            xml += "<BOLSA_SOLES_ADICIONALES>" + parameters.StrBolsaSolesAdicionales + "</BOLSA_SOLES_ADICIONALES>";
            xml += "<IMEI>" + parameters.StrImei + "</IMEI>";
            xml += "<MARCA_EQUIPO>" + parameters.StrMarcaEquipo + "</MARCA_EQUIPO>";
            xml += "<MODELO_EQUIPO>" + parameters.StrModeloEquipo + "</MODELO_EQUIPO>";
            xml += "<TRANSACCION_BLOQUEO>" + parameters.StrTransaccionBloqueo + "</TRANSACCION_BLOQUEO>";
            xml += "<MOTIVO_TIPO_BLOQUEO>" + parameters.StrMotivoTipoBloqueo + "</MOTIVO_TIPO_BLOQUEO>";
            xml += "<TOPE_CONSUMO>" + parameters.StrTopeConsumo + "</TOPE_CONSUMO>";
            xml += "<FECHA_EJEC_TOPE_CONS>" + parameters.StrFechaEjecTopeCons + "</FECHA_EJEC_TOPE_CONS>";
            xml += "<FLAG_GRILA_ATP>" + parameters.StrFlagGrilaAtp + "</FLAG_GRILA_ATP>";
            xml += "<ESCENARIO_MIGRACION>" + parameters.StrEscenarioMigracion + "</ESCENARIO_MIGRACION>";
            xml += "<NUEVO_PLAN>" + parameters.StrNuevoPlan + "</NUEVO_PLAN>";
            xml += "<CF_TOTAL_NUEVO>" + parameters.StrCfTotalNuevo + "</CF_TOTAL_NUEVO>";
            xml += "<MONTO_APADECE>" + parameters.StrMontoApadece + "</MONTO_APADECE>";
            xml += "<MONTO_PCS>" + parameters.StrMontoPcs + "</MONTO_PCS>";
            xml += "<MOTIVO_CANCELACION>" + parameters.StrMotivoCancelacion + "</MOTIVO_CANCELACION>";
            xml += "<ESCENARIO_RETENCION>" + parameters.StrEscenarioRetencion + "</ESCENARIO_RETENCION>";
            xml += "<ACCION_RETENCION>" + parameters.StrAccionRetencion + "</ACCION_RETENCION>";
            xml += "<MODALIDAD>" + parameters.StrModalidad + "</MODALIDAD>";
            xml += "<PRODUCTOS>" + parameters.StrProductos + "</PRODUCTOS>";

            xml += "<PUNTOS_CC_ANTES_TRANS>" + parameters.StrPuntosCcAntesTrans + "</PUNTOS_CC_ANTES_TRANS>";
            xml += "<CANTIDAD_TOTAL_CANJE_DEV>" + parameters.StrCantidadTotalCanjeDev + "</CANTIDAD_TOTAL_CANJE_DEV>";
            xml += "<TIPO_DOC_FACT>" + parameters.StrTipoDocFact + "</TIPO_DOC_FACT>";
            xml += "<NRO_DOC_FACT>" + parameters.StrNroDocFact + "</NRO_DOC_FACT>";
            xml += "<DIRECCION_POSTAL>" + parameters.StrDireccionPostal + "</DIRECCION_POSTAL>";
            xml += "<DISTRITO_POSTAL>" + parameters.StrDistritoPostal + "</DISTRITO_POSTAL>";
            xml += "<PROVINCIA_POSTAL>" + parameters.StrProvinciaPostal + "</PROVINCIA_POSTAL>";
            xml += "<DEPARTAMENTO_POSTAL>" + parameters.StrDepartamentoPostal + "</DEPARTAMENTO_POSTAL>";
            xml += "<FECHA_EMISION_DOC_FACT>" + parameters.StrFechaEmisionDocFact + "</FECHA_EMISION_DOC_FACT>";
            xml += "<FECHA_VENC_DOC_FACT>" + parameters.StrFechaVencDocFact + "</FECHA_VENC_DOC_FACT>";

            xml += "<IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>" + parameters.StrImporteConceptoAjusteSinIgv + "</IMPORTE_CONCEPTO_AJUSTE_SIN_IGV>";
            xml += "<SUBTOTAL_AJUSTE_SIN_IGV>" + parameters.StrSubtotalAjusteSinIgv + "</SUBTOTAL_AJUSTE_SIN_IGV>";
            xml += "<IGV_TAX>" + parameters.StrIgvTax + "</IGV_TAX>";
            xml += "<TOTAL_CON_IGV>" + parameters.StrTotalConIgv + "</TOTAL_CON_IGV>";
            xml += "<TOTAL_AJUSTE>" + parameters.StrTotalAjuste + "</TOTAL_AJUSTE>";
            xml += "<MOTIVO_CAMBIO_SIM>" + parameters.StrMotivoCambioSim + "</MOTIVO_CAMBIO_SIM>";
            xml += "<NUEVO_SIM>" + parameters.StrNuevoSim + "</NUEVO_SIM>";
            xml += "<COSTO_TRANSACCION>" + parameters.StrCostoTransaccion + "</COSTO_TRANSACCION>";
            xml += "<FLAG_4G>" + parameters.StrFlag4G + "</FLAG_4G>";
            xml += "<SIM_4G_LTE>" + parameters.StrSim4GLte + "</SIM_4G_LTE>";
            xml += "<ESTADO_SERVICIO_4G>" + parameters.StrEstadoServicio4G + "</ESTADO_SERVICIO_4G>";
            xml += "<NRO_DOC_REF>" + parameters.StrNroDocIdentidadRef + "</NRO_DOC_REF>";
            xml += "<FECHA_EMISION_DOC_REF>" + parameters.StrFechaEmisionDocRef + "</FECHA_EMISION_DOC_REF>";
            xml += "<COD_DESBLOQ>" + parameters.StrCodDesbloqueo + "</COD_DESBLOQ>";
            xml += "<PAIS_POSTAL>" + parameters.StrPaisA + "</PAIS_POSTAL>";
            xml += "<CODIGO_POSTAL>" + parameters.StrCodigoLocalA + "</CODIGO_POSTAL>";
            xml += "<CENTRO_POBLADO_ACTUAL>" + parameters.StrCentroPobladoActual + "</CENTRO_POBLADO_ACTUAL>";
            xml += "<REFERENCIA>" + parameters.StrReferenciaActual + "</REFERENCIA>";
            xml += "<DIRECCION_DESTINO>" + parameters.StrDireccionPostalC + "</DIRECCION_DESTINO>";
            xml += "<DESCRIP_TRANSACCION>" + parameters.StrDescripTransaccion + "</DESCRIP_TRANSACCION>";
            xml += "<REFERENCIA_DESTINO>" + parameters.StrReferenciaDestino + "</REFERENCIA_DESTINO>";
            xml += "<DEPARTAMENTO_DESTINO>" + parameters.StrDepartamentoLocalB + "</DEPARTAMENTO_DESTINO>";
            xml += "<DISTRITO_DESTINO>" + parameters.StrDistrtitoLocalB + "</DISTRITO_DESTINO>";
            xml += "<CODIGO_POSTAL_DESTINO>" + parameters.StrCodigoLocalB + "</CODIGO_POSTAL_DESTINO>";
            xml += "<PAIS_DESTINO>" + parameters.StrPaisB + "</PAIS_DESTINO>";
            xml += "<PROVINCIA_DESTINO>" + parameters.StrProvinciaLocalB + "</PROVINCIA_DESTINO>";
            xml += "<CENTRO_POBLADO_DESTINO>" + parameters.StrCentroPobladoDestino + "</CENTRO_POBLADO_DESTINO>";
            xml += "<CORREO_SOLICITUD>" + parameters.StrEmail + "</CORREO_SOLICITUD>";
            xml += "<APLICA_CAMBIO_DIREC>" + parameters.StrAplicaCambioDireccion + "</APLICA_CAMBIO_DIREC>";
            xml += "<APLICA_CAMBIO_NOMBRE>" + parameters.StrAplicaCambioNombre + "</APLICA_CAMBIO_NOMBRE>";
            xml += "<FECHA_SUSP>" + parameters.StrFechaSuspension + "</FECHA_SUSP>";
            xml += "<FECHA_ACTIVACION>" + parameters.StrFechaActivacion + "</FECHA_ACTIVACION>";
            xml += "<COSTO_REACTIVACION>" + parameters.StrCostoReactivacion + "</COSTO_REACTIVACION>";
            xml += "<FLAG_TRASLADO>" + parameters.StrFlagExterInter + "</FLAG_TRASLADO>";
            xml += "<FECHA_AUTORIZACION>" + parameters.StrFechaTransaccionProgram + "</FECHA_AUTORIZACION>";

            xml += "<ACCION_EJECUTAR>" + parameters.strAccionEjecutar + "</ACCION_EJECUTAR>";
            xml += "<NRO_ANTERIOR>" + parameters.strNroAnterior + "</NRO_ANTERIOR>";
            xml += "<NRO_NUEVO>" + parameters.strNroNuevo + "</NRO_NUEVO>";
            xml += "<LOCUCION>" + parameters.strLocucion + "</LOCUCION>";
            xml += "<COSTO_LOCUCION>" + parameters.strCostoLocucion + "</COSTO_LOCUCION>";
            xml += "<DURACION_LOCUCION>" + parameters.strDuracionLocucion + "</DURACION_LOCUCION>";
            xml += "<ENVIO_CORREO>" + parameters.strEnvioCorreo + "</ENVIO_CORREO>";
            xml += "<CONTRATO>" + parameters.strContrato + "</CONTRATO>";

            if (!String.IsNullOrEmpty(parameters.strPuntoDeAtencion))
                xml += "<PUNTO_DE_ATENCION>" + parameters.strPuntoDeAtencion + "</PUNTO_DE_ATENCION>";
            
            xml += "<NRO_DOC>" + parameters.strNroDoc + "</NRO_DOC>";
            xml += "<FECHA_TRANSACCION>" + parameters.strFechaTransaccion + "</FECHA_TRANSACCION>";
            xml += "<CASO_INTERACCION>" + parameters.strCasoInteraccion + "</CASO_INTERACCION>";
            xml += "<TRANSACCION_DESCRIPCION>" + parameters.strTransaccionDescripcion + "</TRANSACCION_DESCRIPCION>";
            xml += "<COSTO_TRANSACCIÓN>" + parameters.strCostoTransaccion + "</COSTO_TRANSACCIÓN>";
            xml += "<DIRECCION_CLIENTE_ACTUAL>" + parameters.strDireccionClienteActual + "</DIRECCION_CLIENTE_ACTUAL>";
            xml += "<REFERENCIA_TRANSACCION_ACTUAL>" + parameters.strRefTransaccionActual + "</REFERENCIA_TRANSACCION_ACTUAL>";
            xml += "<DISTRITO_CLIENTE_ACTUAL>" + parameters.strDistritoClienteActual + "</DISTRITO_CLIENTE_ACTUAL>";
            xml += "<CODIGO_POSTAL_ACTUAL>" + parameters.strCodigoPostalActual + "</CODIGO_POSTAL_ACTUAL>";
            xml += "<PAIS_CLIENTE_ACTUAL>" + parameters.strPaisClienteActual + "</PAIS_CLIENTE_ACTUAL>";
            xml += "<PROVINCIA_CLIENTE_ACTUAL>" + parameters.strProvClienteActual + "</PROVINCIA_CLIENTE_ACTUAL>";
            xml += "<DIRECCION_CLIENTE_DESTINO>" + parameters.strDirClienteDestino + "</DIRECCION_CLIENTE_DESTINO>";
            xml += "<REFERENCIA_TRANSACCION_DESTINO>" + parameters.strRefTransaccionDestino + "</REFERENCIA_TRANSACCION_DESTINO>";
            xml += "<DEPARTAMENTO_CLIENTE_DESTINO>" + parameters.strDepClienteDestino + "</DEPARTAMENTO_CLIENTE_DESTINO>";
            xml += "<DISTRITO_CLIENTE_DESTINO>" + parameters.strDistClienteDestino + "</DISTRITO_CLIENTE_DESTINO>";
            xml += "<APLICA_CAMBIO_DIR_FACT>" + parameters.strAplicaCambioDirFact + "</APLICA_CAMBIO_DIR_FACT>";
            xml += "<CODIGO_POSTALL_DESTINO>" + parameters.strCodigoPostallDestino + "</CODIGO_POSTALL_DESTINO>";
            xml += "<PAIS_CLIENTE_DESTINO>" + parameters.strPaisClienteDestino + "</PAIS_CLIENTE_DESTINO>";
            xml += "<PROVINCIA_CLIENTE_DESTINO>" + parameters.strProvClienteDestino + "</PROVINCIA_CLIENTE_DESTINO>";
            xml += "<CODIGO_PLANO_DESTINO>" + parameters.strCodigoPlanoDestino + "</CODIGO_PLANO_DESTINO>";
            xml += "<ENVIO_MAIL>" + parameters.strEnviomail + "</ENVIO_MAIL>";
            xml += "<CORREO_CLIENTE>" + parameters.strCorreoCliente + "</CORREO_CLIENTE>";
            xml += "<FLAG_TIPO_TRASLADO>" + parameters.strflagTipoTraslado + "</FLAG_TIPO_TRASLADO>";
            xml += "<CENTRO_ATENCIÓN>" + parameters.strCentroAtencion + "</CENTRO_ATENCIÓN>";
            xml += "<CONTRATO_CLIENTE>" + parameters.strContratoCliente + "</CONTRATO_CLIENTE>";
            xml += "<DEPARTAMENTO_CLIENTE_ACTUAL>" + parameters.strDepClienteActual + "</DEPARTAMENTO_CLIENTE_ACTUAL>";

            xml += "<FEC_INICIAL_REPORTE>" + parameters.StrFecInicialReporte + "</FEC_INICIAL_REPORTE>";
            xml += "<FEC_FINAL_REPORTE>" + parameters.StrFecFinalReporte + "</FEC_FINAL_REPORTE>";
            xml += "<MONTO_OCC>" + parameters.StrMontoOCC + "</MONTO_OCC>";
            xml += "<CONTENIDO_COMERCIAL>" + parameters.StrContenidoComercial + "</CONTENIDO_COMERCIAL>";
            xml += "<CONTENIDO_COMERCIAL2>" + parameters.StrContenidoComercial2 + "</CONTENIDO_COMERCIAL2>";

            xml += "<TITULO_INSTALACION>" + parameters.StrTituloInstalacion + "</TITULO_INSTALACION>";
            xml += "<TITULO_DESINSTALACION>" + parameters.StrTituloDesinstalacion + "</TITULO_DESINSTALACION>";
            xml += "<NRO_CONTRATO>" + parameters.StrNumeroContrato + "</NRO_CONTRATO>";
            xml += "<FLAG_TIPO_DECO>" + parameters.StrFlagTipoDeco + "</FLAG_TIPO_DECO>";
            xml += "<DIRECCION>" + parameters.StrDireccion + "</DIRECCION>";
            xml += "<NOTAS_DIRECCION>" + parameters.StrNotasDireccion + "</NOTAS_DIRECCION>";
            xml += "<DEPARTAMENTO>" + parameters.StrDepartamento + "</DEPARTAMENTO>";
            xml += "<DISTRITO>" + parameters.StrDistrito + "</DISTRITO>";
            xml += "<PAIS>" + parameters.StrPais + "</PAIS>";
            xml += "<PROVINCIA>" + parameters.StrProvincia + "</PROVINCIA>";
            xml += "<CODIGO_PLANO>" + parameters.StrCodigoPlano + "</CODIGO_PLANO>";
            xml += "<FECHA_COMPROMISO>" + parameters.StrFechaCompromiso + "</FECHA_COMPROMISO>";
            xml += "<X_ETIQUETA_2>" + parameters.StrXEtiqueta2 + "</X_ETIQUETA_2>";
            xml += "<NOMBRE_SERVICIO>" + parameters.StrNombreServicio + "</NOMBRE_SERVICIO>";

            if (parameters.ListDecoder != null)
                foreach (var item in parameters.ListDecoder)
                {
                    xml += "<NOMBRE_EQUIPO>" + item.StrNombreEquipo + "</NOMBRE_EQUIPO>";
                    xml += "<TIPO_SERVICIO>" + item.StrTipoServicio + "</TIPO_SERVICIO>";
                    xml += "<CARGO_FIJO_SIN_IGV>" + item.StrCargoFijoSinIGV + "</CARGO_FIJO_SIN_IGV>";   
                }

            xml += "<TIPO_EQUIPO>" + parameters.StrTipoEquipo + "</TIPO_EQUIPO>";
            xml += "<GRUPO_SERVICIO>" + parameters.StrGrupoServicio + "</GRUPO_SERVICIO>";
            xml += "<CARGO_FIJO>" + parameters.StrCargoFijo + "</CARGO_FIJO>";
            xml += "<CANTIDAD_INSTALAR>" + parameters.StrCantidadInstalar + "</CANTIDAD_INSTALAR>";
            xml += "<CANTIDAD_DESINSTALAR>" + parameters.StrCantidadDesinstalar + "</CANTIDAD_DESINSTALAR>";
            xml += "<CARGO_FIJO_CON_IGV>" + parameters.StrCargoFijoConIGV + "</CARGO_FIJO_CON_IGV>";
            xml += "<FIDELIZAR>" + parameters.StrFidelizar + "</FIDELIZAR>";
            xml += "<COSTO_INSTALACION>" + parameters.StrCostoInstalacion + "</COSTO_INSTALACION>";
            xml += "<COSTO_DESINSTALACION>" + parameters.StrCostoDesinstalacion + "</COSTO_DESINSTALACION>";
            xml += "<ENVIAR_EMAIL>" + parameters.StrEnviarEmail + "</ENVIAR_EMAIL>";
            xml += "<TEXTO>" + parameters.StrTexto + "</TEXTO>";
            xml += "<SUB_MOTIVO_CANCELACION>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO_CANCELACION>";
            xml += "<ACCIONES_OFRECIDAS>" + parameters.StrAccion + "</ACCIONES_OFRECIDAS>";
            xml += "<SEGMENTO>" + parameters.StrSegmento + "</SEGMENTO>";
            xml += "<DIRECCION_INSTALACION>" + parameters.strDireccionInstalac + "</DIRECCION_INSTALACION>";
            xml += "<CANTIDAD_PUNTOS>" + parameters.StrCantidadCc + "</CANTIDAD_PUNTOS>";
            xml += "<PAIS_CLIENTE_ACTUAL>"+parameters.strPaisClienteActual+"</PAIS_CLIENTE_ACTUAL>";
            xml += "<TIPO_TRANSACCION>" + parameters.StrTipoTransaccion + "</TIPO_TRANSACCION>";
            xml += "<ID_CU_ID>" + parameters.StrCustomerId + "</ID_CU_ID>";
            xml += "<CONTRATO_CANCELAR>" + parameters.strContrato + "</CONTRATO_CANCELAR>";
            xml += "<FECHA_CANCELACION>" + parameters.StrFechaCancel + "</FECHA_CANCELACION>";
            xml += "<FECHA_HORA_DE_ATENCION>" + parameters.strFechaHoraAtención + "</FECHA_HORA_DE_ATENCION>";

            xml += "<MONTO_REINTEGRO>" + parameters.strMontoReintegro + "</MONTO_REINTEGRO>";
            xml += "<SERV_COMERCIAL>" + parameters.strServComercial + "</SERV_COMERCIAL>";
            xml += "<CARGO_FIJO_PROMOCION>" + parameters.strCargoFijoPromocion + "</CARGO_FIJO_PROMOCION>";
            xml += "<ACCION_EJECUTADA>" + parameters.strAccionEjecutiva + "</ACCION_EJECUTADA>";
            xml += "<PROGRAMADO>" + parameters.strProgramado + "</PROGRAMADO>";
            xml += "<NUM_PER_PROM>" + parameters.strNumPerProm + "</NUM_PER_PROM>";
            xml += "<REP_LEG_NRO_DOCUMENTO>" + parameters.strRepLegNroDocumento + "</REP_LEG_NRO_DOCUMENTO>";
            if (parameters.flagCargFijoServAdic == "")
            xml += "<DIRECCION_INSTALACION>" + parameters.strDireccionInstalcion + "</DIRECCION_INSTALACION>";
            xml += "<NRO_SOT>" + parameters.strNroSot + "</NRO_SOT>";

            xml += "<NOMBRES_APELLIDOS_CLIENTE>" + parameters.StrNombresApellidosCliente + "</NOMBRES_APELLIDOS_CLIENTE>";
            xml += "<NUMERO_LINEA>" + parameters.StrNumeroLinea + "</NUMERO_LINEA>";
            xml += "<NRO_CASO>" + parameters.StrNroCaso + "</NRO_CASO>";
            xml += "<CODIGO_ASESOR>" + parameters.StrCodigoAsesor + "</CODIGO_ASESOR>";
            xml += "<NOMBRE_ASESOR>" + parameters.StrNombreAsesor + "</NOMBRE_ASESOR>";

            xml += "<FLAG_FIRMA_DIGITAL>" + parameters.StrFlagFirmaDigital + "</FLAG_FIRMA_DIGITAL>";
            xml += "<NRO_SOLICITUD>" + parameters.StrNroSolicitud + "</NRO_SOLICITUD>";
            xml += "<NRO_DOCUMENTO>" + parameters.StrNroDocumento + "</NRO_DOCUMENTO>";
            xml += "<FECHA_HORA>" + parameters.StrFechaHora + "</FECHA_HORA>";
            xml += "<HUELLA_MINUCIA_CLIENTE>" + parameters.StrHuellaMinuciaCliente + "</HUELLA_MINUCIA_CLIENTE>";
            xml += "<HUELLA_ENCODE>" + parameters.StrHuellaEncode + "</HUELLA_ENCODE>";

            xml += "<IMEI_EQUIPOS>" + parameters.StrImeiEquipos + "</IMEI_EQUIPOS>";
            xml += "<MARCA_MODELO>" + parameters.StrMarcaModelo + "</MARCA_MODELO>";
            xml += "<LINEAS_ASOCIADAS>" + parameters.StrLineasAsociadas + "</LINEAS_ASOCIADAS>";
            xml += "<NOMBRES_SOLICITANTE>" + parameters.StrNombresSolicitante + "</NOMBRES_SOLICITANTE>";
            xml += "<NRO_DOC_SOLICITANTE>" + parameters.StrNroDocSolicitante + "</NRO_DOC_SOLICITANTE>";
            xml += "<IMEI_FISICO>" + parameters.StrImeiFisico + "</IMEI_FISICO>";

            if (parameters.flagCargFijoServAdic != "")
            {
                xml += "<CODIGO_AGENTE>" +  parameters.StrCodigoAsesor + "</CODIGO_AGENTE>";
            }else
	        {
            xml += "<CODIGO_AGENTE>" + parameters.StrNombreAsesor + "</CODIGO_AGENTE>";
	        }
            
            xml += "<PLAN_ANTERIOR>" + parameters.StrPlanAnterior + "</PLAN_ANTERIOR>";
            xml += "<CORREO_AUTORIZADO>" + parameters.StrAplicaEmail + "</CORREO_AUTORIZADO>";

            #region Proy-32650
            if (parameters.flagCargFijoServAdic != "")
            {
                xml += "<DESCUENTO>" + parameters.Descuento + "</DESCUENTO>";
                xml += "<VIGENCIA>" + parameters.Vigencia + "</VIGENCIA>";
                xml += "<FECHA_VIGENCIA>" + parameters.Vigencia + "</FECHA_VIGENCIA>";

                //xml += "<PUNTO_ATENCION>" + parameters.StrCentroAtencionArea + "</PUNTO_ATENCION>";
                xml += "<FECHA_ATENCION>" + parameters.strFechaTransaccion + "</FECHA_ATENCION>";
                //xml += "<TITULAR>" + parameters.StrTitularCliente + "</TITULAR>";
                //xml += "<REPRESENTANTE_LEGAL>" + parameters.StrRepresLegal + "</REPRESENTANTE_LEGAL>";
                //xml += "<TELEFONO_REFERENCIA>" + parameters.StrTelfReferencia + "</TELEFONO_REFERENCIA>";
                xml += "<TRANSACCION>" + parameters.StrDescripTransaccion + "</TRANSACCION>";
                xml += "<ACCION>" + parameters.StrAccion + "</ACCION>";
                xml += "<RESULTADO>" + parameters.StrReferenciaDestino + "</RESULTADO>";
                //xml += "<MOTIVOS>" + parameters.StrMotivoCancelacion + "</MOTIVOS>";
                //xml += "<SUB_MOTIVO>" + parameters.StrSubMotivoCancel + "</SUB_MOTIVO>";
                //xml += "<REINTEGRO>" + parameters.strMontoReintegro + "</REINTEGRO>";
                //xml += "<ENVIO_CORREO>" + parameters.StrEnviarEmail + "</ENVIO_CORREO>";
                //xml += "<CORREO>" + parameters.StrEmail + "</CORREO>";
                xml += "<CONSTANCIA>" + parameters.Constancia + "</CONSTANCIA>";
                xml += "<FLAG_SERVADIC>" + parameters.flagCargFijoServAdic + "</FLAG_SERVADIC>";
                xml += "<FLAG_SERV_DECO>" + parameters.flagServDeco+ "</FLAG_SERV_DECO>";

                xml += "<NOMBRE_SERVICIOS>" + parameters.StrNombreServicio + "</NOMBRE_SERVICIOS>";
            }
            if (parameters.flagCargFijoServAdic == "2")
            {
                xml += "<INTERNET_ACTUAL>" + parameters.strInternetActual + "</INTERNET_ACTUAL>";
                xml += "<BONO_FIDELIZACION>" + parameters.strBonoRetencionFidelizacion + "</BONO_FIDELIZACION>";
                xml += "<VIGENCIA_BONO>" + parameters.Vigencia + "</VIGENCIA_BONO>"; 
                xml += "<FLAG_INCVELOC>1</FLAG_INCVELOC>"; 
                

            }

            #endregion

       #region AMCO
         
            xml += "<PUNTO_ATENCION>" + parameters.strPuntoAtencion + "</PUNTO_ATENCION>";
            xml += "<TITULAR>" + parameters.strTitular + "</TITULAR>";
            xml += "<REPRESENTANTE_LEGAL>" + parameters.strRepresentante + "</REPRESENTANTE_LEGAL>";
            xml += "<TIPO_DOC>" + parameters.strTipoDoc + "</TIPO_DOC>";
            xml += "<FECHA_ACTUALIZACION>" + parameters.strFechaAct + "</FECHA_ACTUALIZACION>";
         
            if (parameters.strTipoConstanciaAMCO != null ? parameters.strTipoConstanciaAMCO != "" ? true : false : false)
            {
                switch (parameters.strTipoConstanciaAMCO)
                {
                    case "3":
                        if (parameters.ListService != null ? parameters.ListService.Count() > 0 ? true : false : false)
                        {
                            foreach (ClaroVideoServiceConstancy item in parameters.ListService)
                            {
                                xml += "<BAJA_SERVICIOS>" + item.strBajaServicios + "</BAJA_SERVICIOS>";
                            }
                        }
                        break;
                    case "4":
                        if (parameters.ListDevice != null ? parameters.ListDevice.Count() > 0 ? true : false : false)
                        {
                            foreach (ClaroVideoDeviceConstancy item in parameters.ListDevice)
                            {
                                xml += "<DISPOSITIVO_ID>" + item.strDispotisitivoID + "</DISPOSITIVO_ID>";
                                xml += "<DISPOSITIVO_NOMBRE>" + item.strDispotisitivoNom + "</DISPOSITIVO_NOMBRE>";
                                xml += "<FECHA_DESACTIVACION>" + item.strFechaDesac + "</FECHA_DESACTIVACION>";
                            }
                        }
                        break;
                    case "5":
                        if (parameters.ListSuscriptcion != null ? parameters.ListSuscriptcion.Count() > 0 ? true : false : false)
                        {
                            foreach (ClaroVideoSubscriptionConstancy item in parameters.ListSuscriptcion)
                            {
                                xml += "<SUSCRIPCION_TITULO>" + item.strSuscTitulo + "</SUSCRIPCION_TITULO>";
                                xml += "<SUSCRIPCION_ESTADO>" + item.strSuscEstado + "</SUSCRIPCION_ESTADO>";
                                xml += "<SUSCRIPCION_PERIODO>" + item.strSuscPeriodo + "</SUSCRIPCION_PERIODO>";
                                xml += "<SUSCRIPCION_SERVICIO>" + item.strSuscServicio + "</SUSCRIPCION_SERVICIO>";
                                xml += "<SUSCRIPCION_PRECIO>" + item.strSuscPrecio + "</SUSCRIPCION_PRECIO>";
                                xml += "<SUSCRIPCION_FECHA_REGISTRO>" + item.strSuscFechReg + "</SUSCRIPCION_FECHA_REGISTRO>";
                            }
                        }
                        break;
                    case "6":
                        if (parameters.ListSuscriptcion != null ? parameters.ListSuscriptcion.Count() > 0 ? true : false : false)
                        {
                            foreach (ClaroVideoSubscriptionConstancy item in parameters.ListSuscriptcion)
                            {
                                xml += "<SUSCRIPCION_TITULO>" + item.strSuscTitulo + "</SUSCRIPCION_TITULO>";
                                xml += "<SUSCRIPCION_ESTADO>" + item.strSuscEstado + "</SUSCRIPCION_ESTADO>";
                                xml += "<SUSCRIPCION_PERIODO>" + item.strSuscPeriodo + "</SUSCRIPCION_PERIODO>";
                                if(item.strSuscServicio != null ? item.strSuscServicio.Length > 0 ? true : false : false)
                                {
                                    xml += "<SUSCRIPCION_SERVICIO>" + item.strSuscServicio + "</SUSCRIPCION_SERVICIO>";
                                }                                
                                xml += "<SUSCRIPCION_PRECIO>" + item.strSuscPrecio + "</SUSCRIPCION_PRECIO>";
                                xml += "<SUSCRIPCION_FECHA_REGISTRO>" + item.strSuscFechReg + "</SUSCRIPCION_FECHA_REGISTRO>";
                            }
                        }
                        break;
                }
            }
            #endregion

            #region PROY-140513
            xml += "<CUENTA>" + parameters.strCuenta + "</CUENTA>";
            xml += "<TIPO_CLIENTE>" + parameters.strTipoCliente + "</TIPO_CLIENTE>";
            xml += "<PRODUCTO_ADQUIRIDO>"+parameters.strPaqueteVelDegradada + "</PRODUCTO_ADQUIRIDO>";
            xml += "<VIGENCIA>" + parameters.strVigenciaPaquete + "</VIGENCIA>";
            xml += "<TIPO_PAGO>" + parameters.strTipoVenta + "</TIPO_PAGO>";
            xml += "<PRECIO_PAQUETE>" + parameters.strPrecioPaquete + "</PRECIO_PAQUETE>";
            xml += "<CORREO>" + parameters.StrEmail + "</CORREO>";
            xml += "<MB_INCLUIDOS>" + parameters.strMBIncluidos + "</MB_INCLUIDOS>";
            xml += "<NUMERO_SERVICIO>" + parameters.strNumeroServicio + "</NUMERO_SERVICIO>";
			

            #endregion
            xml += "</PLANTILLA>";

            return xml;
        }

        public static void GenerateTrazability(string strIdSession, string strTransaction,string strTransactionName, string strTramaInput, string strTramaOutput, string interactionId)
        {
            try
            {
                HttpClient client = new HttpClient();
                string strBaseUrlTrazability = ConfigurationManager.AppSettings("strBaseUrlTrazability");

                client.BaseAddress = new Uri(strBaseUrlTrazability);

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("idTransaccion", strTransaction);
                client.DefaultRequestHeaders.Add("msgid", Claro.Constants.NumberZeroString);
                client.DefaultRequestHeaders.Add("timeStamp", DateTime.Today.ToString());
                client.DefaultRequestHeaders.Add("userId", strIdSession);

                string fechaRegistro = DateTime.Now.ToString("ddMMyyyy");
                var trazabilidad = new Trazability();

                trazabilidad.idTransaccion = strTransaction;
                trazabilidad.idInteraccion = interactionId;
                trazabilidad.tipoTransaccion = strTransactionName;
                trazabilidad.tarea = ConfigurationManager.AppSettings("strConstanciaTrazabilidadTarea");
                trazabilidad.fechaRegistro = fechaRegistro;
                trazabilidad.tramaInput = strTramaInput;
                trazabilidad.tramaOutput = strTramaOutput;
                trazabilidad.descripcion = "Trazabilidad generada desde SIACUNICO";

                var json_object = JsonConvert.SerializeObject(trazabilidad);

                HttpContent content = new StringContent(json_object.ToString(), Encoding.UTF8);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string strMethodTrazability = ConfigurationManager.AppSettings("strMethodTrazability");
                Claro.Web.Logging.Info(strIdSession, strTransaction, "strMethodTrazability: " + strMethodTrazability.ToString());
                var response = client.PostAsync(strMethodTrazability, content).Result;
      
                if (response.IsSuccessStatusCode)
                {
                    Claro.Web.Logging.Info(strIdSession, strTransaction, "Trama generada correctamente");
                }
                else
                {
                    Claro.Web.Logging.Error(strIdSession, strTransaction, response.StatusCode + " Message: " + response.ReasonPhrase);
                } 
            }
            catch (Exception ex)
            {
                
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }
            
        }
        #endregion

    
        public static List<Entity.Transac.Service.Common.ListItem> GetMotiveSot(string strIdSession, string strTransaction)
        {


            DbParameter[] parameters = 
            {
                new DbParameter("srv_cur", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.Transac.Service.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_POST_SGA_P_CONSULTA_MOTIVO, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.Transac.Service.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.Transac.Service.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODMOTOT"]),
                        Description = Convert.ToString(reader["MOTIVO"])
                    });
                }
            });

            return listItem;
        }

        public static bool ValidateSchedule(string strIdSession, string strTransaction, SECURITY.GetSchedule.ScheduleRequest objScheduleRequest)
        {
             
            string an_disponible = string.Empty;
            string av_mensaje = string.Empty;
            try
            {
                
                DbParameter[] parameters = {
                       new DbParameter("an_tiptra", DbType.Int64,255, ParameterDirection.Input,Convert.ToInt( objScheduleRequest.vJobTypes)),
					   new DbParameter("av_ubigeo", DbType.String,255,ParameterDirection.Input,objScheduleRequest.vUbigeo),
					   new DbParameter("ad_fecha_prog", DbType.Date,255,ParameterDirection.Input, Convert.ToDate(objScheduleRequest.vCommitmentDate)),
                       new DbParameter("av_franja", DbType.String,255,ParameterDirection.Input, objScheduleRequest.vTimeZona),
                       new DbParameter("an_disponible", DbType.Int64,255, ParameterDirection.Output),
                       new DbParameter("an_codcon", DbType.Int64,255, ParameterDirection.Output),
                       new DbParameter("av_codcuadrilla", DbType.String,255, ParameterDirection.Output),
                       new DbParameter("an_resultado", DbType.Int64,255, ParameterDirection.Output),
					   new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
			   };

               DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_COD_UBIGEO, parameters);
                an_disponible = parameters[4].Value.ToString();
               av_mensaje = parameters[8].Value.ToString();
                
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Info(strIdSession, strTransaction, ex.Message);
            }
            if (an_disponible.Equals(Claro.Constants.NumberZeroString))
            {
                return true;
            }
            else
            {
                return false;
            }
             
        }
    
       
        public static bool InsertLogTrx(string strAplicacion, string strTransaccion, string strOpcion, string strAccion, string strPhone,
            string strIdInteraction, string strIdTypification, string strUser,
            string strIPPCClient, string strPCClient, string strIPServer, string strNameServer, 
            string strInputParameters, string strOutpuParameters, ref string flagInserction)
        {
            bool response = false;
            DbParameter[] parameters = {
									new DbParameter("P_APLICACION", DbType.String,10,ParameterDirection.Input,strAplicacion),
									new DbParameter("P_TRANSACCION", DbType.String,50,ParameterDirection.Input,strTransaccion),
									new DbParameter("P_OPCION", DbType.String,20,ParameterDirection.Input,strOpcion),
									new DbParameter("P_ACCION", DbType.String,20,ParameterDirection.Input,strAccion),
									new DbParameter("P_TELEFONO", DbType.String,20,ParameterDirection.Input,strPhone),
									new DbParameter("P_INTERACCION_ID", DbType.String,20,ParameterDirection.Input,strIdInteraction),
                                    new DbParameter("P_TIPIFICACION_ID", DbType.String,10,ParameterDirection.Input,strIdTypification),
									new DbParameter("P_USUARIO_LOGIN", DbType.String,30,ParameterDirection.Input,strUser),
									new DbParameter("P_IP_CLIENTE", DbType.String,15,ParameterDirection.Input,strIPPCClient),
                                    new DbParameter("P_NOMBRE_CLIENTE", DbType.String,30,ParameterDirection.Input,strPCClient),
									new DbParameter("P_IP_SERVIDOR", DbType.String,15,ParameterDirection.Input,strIPServer),
									new DbParameter("P_NOMBRE_SERVIDOR", DbType.String,30,ParameterDirection.Input,strNameServer),
                                    new DbParameter("P_PARAMS_IN", DbType.String,400,ParameterDirection.Input,strInputParameters),
									new DbParameter("P_PARAMS_OUT", DbType.String,60,ParameterDirection.Input,strOutpuParameters),
									new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output)
								};

            DbFactory.ExecuteNonQuery("S", strTransaccion, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_COMMON_SP_INSERTAR_LOG_TRX, parameters);
                
            flagInserction = Functions.CheckStr(parameters[parameters.Length - 1].Value);
            if (flagInserction.Equals("OK"))
            {
                response = true;
            }

            return response;
        }

        //public static List<ListItem> GetItemsMigratedLists(Int64 codigo)
        //{
        //    List<ListItem> list = new List<ListItem>();
        //    ListItem entity = null;
        //    DbParameter[] parameters = {
        //                            new DbParameter("P_OBJID",DbType.Int64,ParameterDirection.Input,codigo * -1),
        //                            new DbParameter("P_FLAG_CONSULTA",DbType.String,ParameterDirection.Output),
        //                            new DbParameter("P_MSG_TEXT",DbType.String,ParameterDirection.Output),
        //                            new DbParameter("P_LIST",DbType.Object,ParameterDirection.Output)
        //                        };
        //    DbFactory.ExecuteReader("S", "T", DbConnectionConfiguration.SIAC_DBSIAC, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters, (IDataReader reader) =>
        //    {
        //        while (reader.Read())
        //        {
        //            entity = new ListItem()
        //            { 
        //                Code = Functions.CheckStr(reader["CODIGO"]),
        //                Description =Functions.CheckStr(reader["NOMBRE"])  
        //            };
        //            list.Add(entity);
        //        }
        //    });  
        //    return list;
        //}

        
        
        public static List<ListItem> GetListClientDocumentType()
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_CLARIFY_SP_CUSTOMER_DOC_TYPE, "RESULT", "TIPO_DOC", "TIPO_DOC");
        }

        public static ParameterData GetParameterData(string name, ref string message)
        {
            return GetParameterData("s", "t", name, ref message);
        }

        public static ParameterData GetParameterData(string sesion, string transaction, string name, ref string message)
        {
            ParameterData entity = null;
            DbParameter[] parameters = {
                new DbParameter("P_NOMBRE", DbType.String, 255,ParameterDirection.Input, name),
                new DbParameter("P_MENSAJE", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(sesion, transaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_COMMON_SP_OBTENER_DATO, parameters, (IDataReader reader) =>
            {
                if (reader.Read())
                {
                    entity = new ParameterData()
                    {
                        Description = Functions.CheckStr(reader["descripcion"]),
                        Value_C = Functions.CheckStr(reader["valor_C"]),
                        Value_N = Functions.CheckDbl(reader["valor_N"])
                    }; 
                } 
            });
  
            message = parameters[1].Value.ToString();
            return entity;
        }

        private static List<ListItem> ListItems(IDbConnectionConfiguration idbConnectC, IDbCommandConfig idbCommandC, string outputParameter, string column01, string column02)
        {
            List<ListItem> list = new List<ListItem>();
            DbParameter[] parameters = {
                new DbParameter(outputParameter, DbType.Object, ParameterDirection.Output)
            };

            DbFactory.ExecuteReader("S", "T", idbConnectC, idbCommandC, parameters, (IDataReader reader) =>
            {
                while (reader.Read())
                {
                    ListItem entity = new ListItem();
                    entity.Code = Functions.CheckStr(reader[column01]);
                    entity.Description = Functions.CheckStr(reader[column02]);
                    list.Add(entity);
                }
            });  
            return list;
        }
         
        public static bool UpdateNotes(string strIdSession, string strTransaction, string p_objid, string p_text, string p_order, out string rFlag, out string rMessage)
        {
            DbParameter[] parameters = {
                                           new DbParameter("P_INTERACT_ID", DbType.String,255, ParameterDirection.Input),
                                           new DbParameter("P_TEXTO", DbType.String,1000, ParameterDirection.Input),
                                           new DbParameter("P_ORDEN", DbType.String,1, ParameterDirection.Input),
                                           new DbParameter("P_FLAG_INSERCION", DbType.String,255, ParameterDirection.Output),
                                           new DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output) 
                                       };

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
        
            if (!string.IsNullOrEmpty(p_objid))
                parameters[0].Value = p_objid;

            if (!string.IsNullOrEmpty(p_text))
                parameters[1].Value = p_text;

            if (!string.IsNullOrEmpty(p_order))
                parameters[2].Value = p_order;

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_AUDIT, parameters);
            rFlag = Functions.CheckStr(parameters[parameters.Length - 2].Value);
            rMessage = Functions.CheckStr(parameters[parameters.Length - 1].Value);
            return rFlag.Equals("OK");
        }
        /// <summary>
        /// Método que obtiene las reglas comerciales.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strSubClase">Cod Sub Clase</param>
        /// <returns>Devuelve las reglas de atención de la transacción.</returns>
        public static List<Entity.Transac.Service.Common.BusinessRules> GetBusinessRules(string strIdSession, string strTransaction, string strSubClase)
        {
            List<Claro.SIACU.Entity.Transac.Service.Common.BusinessRules> lstBusinessRules = new List<Claro.SIACU.Entity.Transac.Service.Common.BusinessRules>();

            try
            {
            DbParameter[] parameters = {
                new DbParameter("P_SUSCLASE", DbType.String, ParameterDirection.Input, strSubClase),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            lstBusinessRules = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Transac.Service.Common.BusinessRules>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_CONSULTAR_REGLAS_ATENCION, parameters);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            
            return lstBusinessRules;
        }

        /// <summary>
        /// Método que obtiene las regiones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <returns>Devuelve las regiones.</returns>

        public static List<Entity.Transac.Service.Common.Region> GetRegions(string strIdSession, string strTransaction)
        {
            List<Claro.SIACU.Entity.Transac.Service.Common.Region> lstRegions;

            DbParameter[] parameters = {
                new DbParameter("CUR_RPTA", DbType.Object, ParameterDirection.Output)
            };

            lstRegions = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Transac.Service.Common.Region>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_CONSULTAR_REGIONES, parameters);
            return lstRegions;
        }

        /// <summary>
        /// Método que devuele el tipo de establecimiento.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="code">Código</param>
        /// <returns>Devuelve listado de tipos de establecimiento.</returns>
        public static List<Entity.Transac.Service.Common.ListItem> GetCacDacType(string strIdSession, string strTransaction, int code)
        {
            code = code * -1;

            DbParameter[] parameters = 
            {
                new DbParameter("P_OBJID", DbType.Int64, ParameterDirection.Input, code),
                new DbParameter("P_FLAG_CONSULTA", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,225, ParameterDirection.Output),
                new DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
            };

            List<Entity.Transac.Service.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.Transac.Service.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.Transac.Service.Common.ListItem
                    {
                        Code = Convert.ToString(reader["CODIGO"]),
                        Description = Convert.ToString(reader["NOMBRE"])
                    });
                }
            });

            return listItem;
        }


        /// <summary>
        /// Método que devuelve el código de la lista.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="nameList">Nombre de lista</param>
        /// <returns>Devuelve código de lista.</returns>
        public static int GetCodeList(string strIdSession, string strTransaction, string nameList)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_TITLE", DbType.String,3, ParameterDirection.Input, nameList),
                new DbParameter("P_OBJID", DbType.String,225, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_CODIGO, parameters);

            return Convert.ToInt(parameters[1].Value.ToString());
        }


        public static string GetValueXml(string strIdSession, string strTransaction, string fileName, string vClave)
        {
            var strApplicationPath = new Utils().GetApplicationPath();
            //var strXmlPath = strApplicationPath + fileName;
            var strXmlPath = string.Format("{0}{1}\\{2}", strApplicationPath,Claro.SIACU.Transac.Service.Constants.FileSiacutData, fileName);//DataTransac
            if (strXmlPath == "") { return ""; }
            try
            {
                //Open a FileStream on the Xml file
                var docIn = new FileStream(strXmlPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var xmlDocument = new XmlDocument();
                //Load the Xml Document
                xmlDocument.Load(docIn);

                //Get a node
                var nodeXml = xmlDocument.GetElementsByTagName(vClave);
                if (nodeXml.Item(0) != null)
                {
                    string strGetValue = nodeXml.Item(0).InnerText;
                    return strGetValue;
                }

                return "";

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                return "";
            }
        }

        public static List<ListItem> GetListValuesXML(string strIdSession, string strTransaction, string strNameFunction, string strFlagCodeAdic, string fileName)
        {

            string strApplicationPath = new Claro.Utils().GetApplicationPath();
            string fullPath = strApplicationPath + fileName;
            Claro.Web.Logging.Info("123456789", "123456", "fullPath " + fullPath);

            XmlDocument documento = new XmlDocument();
            documento.Load(fullPath);
            XmlNodeList nodeList = documento.SelectNodes("descendant::" + strNameFunction + "/item");

            var lstItem = new List<ListItem>();

            for (int i = 0; i < nodeList.Count; i++)
            {
                var objItemVM = new ListItem();
                switch (strFlagCodeAdic)
                {
                    case "1":
                        objItemVM = new ListItem(nodeList.Item(i).ChildNodes[0].InnerText,
                                                 nodeList.Item(i).ChildNodes[1].InnerText,
                                                 nodeList.Item(i).ChildNodes[2].InnerText);
                        break;
                    case "2":
                        objItemVM = new ListItem(nodeList.Item(i).ChildNodes[0].InnerText,
                                                 nodeList.Item(i).ChildNodes[2].InnerText,
                                                 nodeList.Item(i).ChildNodes[1].InnerText);
                        break;
                    default:
                        objItemVM = new ListItem(nodeList.Item(i).ChildNodes[0].InnerText,
                                                 nodeList.Item(i).ChildNodes[1].InnerText);
                        break;
                }
                lstItem.Add(objItemVM);
            }
            Claro.Web.Logging.Info("123456789", "123456", "Salida # " + lstItem.Count);
            return lstItem;
        }

        /// <summary>
        /// Método que obtiene las tipificaciones.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name="strTransactionName">Transacción de la tipificacion</param>
        /// <returns>Devuelve las tipificaciones.</returns>
        public static List<Entity.Transac.Service.Common.Typification> GetTypification(string strIdSession, string strTransaction, string strTransactionName)
        {
            List<Claro.SIACU.Entity.Transac.Service.Common.Typification> lstTypification = null;
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetTypification Capa Data -> Parametro de entrada strTransactionName: " + strTransactionName); // Temporal
            DbParameter[] parameters = {
                new DbParameter("P_TRANSACCION", DbType.String, ParameterDirection.Input,strTransactionName),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                lstTypification = DbFactory.ExecuteReader<List<Claro.SIACU.Entity.Transac.Service.Common.Typification>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTENER_TIPIFICACIONES, parameters);

                
                Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "End a GetTypification Capa Data Parametro de salida: lstTypification : " + ((lstTypification == null) ? "0 o null" : lstTypification.Count.ToString()).ToString());

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction,"Error GetTypification" + ex.Message);
            }
            
            return lstTypification;
        }

        //Name: ListarFacturas DALlamadas
        public static List<EntitiesFixed.GenericItem> GetReceipts(string strIdSession, string strTransaction, string vCODCLIENTE, ref string MSG_ERROR)
        {

            var salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters = 
            {
                new DbParameter("K_CODIGOCLIENTE", DbType.String, 24, ParameterDirection.Input, vCODCLIENTE),
                new DbParameter("K_ERRMSG", DbType.String, 255, ParameterDirection.Output),												   
                new DbParameter("K_LISTA", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DBTO,
                        DbCommandConfiguration.SIACU_TOLS_OBTENERDATOSDECUENTA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Codigo = Functions.CheckStr(reader["InvoiceNumber"]) + "$" +
                                             Functions.CheckStr(reader["FechaInicio"]) + "$" +
                                             Functions.CheckStr(reader["FechaFin"]),
                                    Descripcion = Functions.CheckStr(reader["FECHAEMISION"]),
                                    Codigo2 = Functions.CheckStr(reader["Periodo"])
                                };

                                salida.Add(item);
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                MSG_ERROR = parameters[parameters.Length - 1].Value.ToString();
            }

            return salida;
        }

        
        public static List<ListItem> GetCivilStatus(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_MARITAL_STATUS, "RESULT", "ESTADO_CIVIL", "ESTADO_CIVIL");
        }

        public static List<ListItem> GetOccupationClient(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_CUSTOMER_OCCUPATION, "RESULT", "OCUPACION", "OCUPACION");
        }

        public static List<ListItem> GetReasonRegistry(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_PREPAID_REGISTRATION_REASON, "RESULT", "MOTIVO_REGISTRO", "MOTIVO_REGISTRO");
        }

        public static List<ListItem> GetBrandList(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTAR_MARCA, "P_RESULTADO", "ID_TIPO", "TIP_DESC");
        }

        public static List<ConsultDepartment> GetConsultDepartment(string strIdSession, string strTransaction, Int32 CodRegion, Int32 CodDepartment, Int32 CodState)
        {
            List<ConsultDepartment> listConsultDepartment = null;
            DbParameter[] parameters = {
                                           new DbParameter("P_CODIGOREGION",DbType.Int32,ParameterDirection.Input,CodRegion),
                                           new DbParameter("P_CODIGODEPARTAMENTO", DbType.Int32,ParameterDirection.Input,CodDepartment),
                                           new DbParameter("P_CODIGOESTADO", DbType.Int32, ParameterDirection.Input, CodState),
                                           new DbParameter("P_RESULTADO", DbType.Object,ParameterDirection.Output)
                                       };
            listConsultDepartment = DbFactory.ExecuteReader<List<ConsultDepartment>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTAR_DEPARTAMENTO, parameters);

            return listConsultDepartment;
        }

        public static List<ListItem> GetBrandModel(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTAR_MARCA_MODELO, "p_cursor", "ID_MODELO", "MODELO");
        }

        public static List<ConsultProvince> GetConsultProvince(string strIdSession, string strTransaction, Int32 CodDepartment, Int32 CodProvince, Int32 CodState)
        {
            List<ConsultProvince> listConsultProvince = null;
            DbParameter[] parameters = {
                                           new DbParameter("P_CODIGODEPARTAMENTO",DbType.Int32,ParameterDirection.Input,CodDepartment),
                                           new DbParameter("P_CODIGOPROVINCIA", DbType.Int32,ParameterDirection.Input,CodProvince),
                                           new DbParameter("P_CODIGOESTADO", DbType.Int32, ParameterDirection.Input, CodState),
                                           new DbParameter("P_RESULTADO", DbType.Object,ParameterDirection.Output)
                                       };
            listConsultProvince = DbFactory.ExecuteReader<List<ConsultProvince>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTAR_PROVINCIA, parameters);

            return listConsultProvince;
        }

        public static List<ConsultDistrict> GetConsultDistrict(string strIdSession, string strTransaction, Int32 CodProvince, Int32 CodDistrict, Int32 CodState)
        {
            List<ConsultDistrict> listConsultDistrict = null;
            DbParameter[] parameters = {
                                           new DbParameter("P_CODIGOPROVINCIA",DbType.Int32,ParameterDirection.Input, CodProvince),
                                           new DbParameter("P_CODIGODISTRITO", DbType.Int32,ParameterDirection.Input,CodDistrict),
                                           new DbParameter("P_CODIGOESTADO", DbType.Int32, ParameterDirection.Input, CodState),
                                           new DbParameter("P_RESULTADO", DbType.Object,ParameterDirection.Output)
                                       };
            listConsultDistrict = DbFactory.ExecuteReader<List<ConsultDistrict>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_LISTAR_DISTRITO, parameters);
            return listConsultDistrict;
        }

        public static List<ListItem> GetConsultNationality(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_BIRTHPLACE, "RESULT", "NACIONALIDAD", "NACIONALIDAD");
        }

        public static List<ListItem> GetServicesVAS(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_SERVICIOS_VAS, "result", "X_SERVICIOS_VAS", "X_SERVICIOS_VAS");
        }

        public static List<ListItem> GetMigratedElements(string strIdSession, string strTransaction, Int64 Id)
        {
            List<ListItem> listMigratedElements = new List<ListItem>();
            DbParameter[] parameters = {
                                           new  DbParameter("P_OBJID", DbType.Int64, ParameterDirection.Input, Id*-1),
                                           new  DbParameter("P_FLAG_CONSULTA", DbType.String,255, ParameterDirection.Output),
                                           new  DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output),
                                           new  DbParameter("P_LIST", DbType.Object, ParameterDirection.Output)
                                       };
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_SHOW_LIST_ELEMENT, parameters, (IDataReader reader) =>
            {
                ListItem entity = null;
                while (reader.Read())
                {
                    entity = new ListItem();
                    entity.Code = Functions.CheckStr(reader["CODIGO"]);
                    entity.Description = Functions.CheckStr(reader["NOMBRE"]);
                    listMigratedElements.Add(entity);
                }
            });
            return listMigratedElements;
        }


        #region Interacciones-


        ///funcion obtenerinteraccionescliente
        ///    /// <summary>
        /// Método que devuelve el código de la lista.
        /// </summary>
        /// <param name="strIdSession">Id de sesión</param>
        /// <param name="strTransaction">Id de transacción</param>
        /// <param name=">straccount"</param>cuenta
        /// / <param name=">strtelephone"</param>telefono
        /// / <param name=">intcontactobjid1"</param>id de contacto
        /// <returns>Devuelve código de lista.</returns>
        ///
        public static List<Iteraction> GetIteractionClient(string strIdSession, string strTransaction, string strAccount, string strTelephone, long intContactobjid1, long intSiteobjid1, string strTipification, int intNrorecordshow, out  string strFlagcreation, out string strMessage)
        {

            List<Iteraction> list = new List<Iteraction>();
            DbParameter[] parameters = new DbParameter[]
            {
            new DbParameter ("P_PHONE", DbType.String,255,ParameterDirection.Input, strTelephone),
		    new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input,strAccount),
		    new DbParameter("P_SITEOBJID_1", DbType.String,255,ParameterDirection.Input,intSiteobjid1),
		    new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,intContactobjid1),
		    new DbParameter("P_TIPIFICACION", DbType.String,255,ParameterDirection.Input,strTipification),
		    new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
			new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output),
		    new DbParameter("INTERACT", DbType.Object,ParameterDirection.Output)
            };

            //List<IteractionClient> result = null;
           // DbFactory.ExecuteReader<List<IteractionClient>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_INTERACT,parameters,));
            
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_INTERACT, parameters, (IDataReader reader) =>
                {
                
                //result= new List<IteractionClient>();
                //      Iteraction entity = null;
                    int contador = 0;
                    if (intNrorecordshow < 0)
                        intNrorecordshow = 0;

                    while (reader.Read())
	                {
                        if (intNrorecordshow <= contador)
                        {
                            break;
                        }
                        contador++;

                        list.Add(new Iteraction()
                        {

                    ID_INTERACCION = Functions.CheckStr(reader["ID_INTERACCION"]),
                            FECHA_CREACION = Functions.CheckStr(reader["FECHA_CREACION"]),
                    START_DATE = Functions.CheckStr(reader["START_DATE"]),
                    TELEFONO = Functions.CheckStr(reader["TELEFONO"]),
                    TIPO = Functions.CheckStr(reader["TIPO"]),
                    CLASE = Functions.CheckStr(reader["CLASE"]),
                    SUBCLASE = Functions.CheckStr(reader["SUBCLASE"]),
                    TIPIFICACION = Functions.CheckStr(reader["TIPIFICACION"]),
                    TIPO_CODIGO = Functions.CheckStr(reader["TIPO_CODIGO"]),
                    CLASE_CODIGO = Functions.CheckStr(reader["CLASE_CODIGO"]),
                    SUBCLASE_CODIGO = Functions.CheckStr(reader["SUBCLASE_CODIGO"]),
                    INSERTADO_POR = Functions.CheckStr(reader["INSERTADO_POR"]),
                    TIPO_INTER = Functions.CheckStr(reader["TIPO_INTER"]),
                    METODO = Functions.CheckStr(reader["METODO"]),
                    RESULTADO = Functions.CheckStr(reader["RESULTADO"]),
                    HECHO_EN_UNO = Functions.CheckStr(reader["HECHO_EN_UNO"]),
                    AGENTE = Functions.CheckStr(reader["AGENTE"]),
                    NOMBRE_AGENTE = Functions.CheckStr(reader["NOMBRE_AGENTE"]),
                    APELLIDO_AGENTE = Functions.CheckStr(reader["APELLIDO_AGENTE"]),
                    ID_CASO = Functions.CheckStr(reader["ID_CASO"]),
                    SERVICIO = Functions.CheckStr(reader["SERVICIO"]),
                    INCONVENIENTE = Functions.CheckStr(reader["INCONVENIENTE"])
                         });
	         
	                }
                
                });
            strFlagcreation = Functions.CheckStr(parameters[5].Value.ToString()).ToString();
            strMessage = Functions.CheckStr(parameters[6].Value.ToString()).ToString();

            return list;
        
        }



        //REGISTRAR DETALLE DE INTERACCION
        //nombre original InsSubClaseDet
        public static bool InsertRecordSubClaseDetail(string strSesion, string strTransaction, InteractionSubClasDetail objitem, out int intCoderror, out string strMessage)
        {
            DbParameter[] parameters = new DbParameter[]{

                new DbParameter("inCASO", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inINTERACT_ID", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inTIPO_CODE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inCLASE_CODE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inSUBCLASE_CODE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inSERVAFECT_CODE", DbType.String, 255 ,ParameterDirection.Input),
                new DbParameter("inINCONVEN_CODE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inTIPO", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inCLASE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inSUBCLASE", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inSERVAFECT", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inINCONVEN", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("inUSUARIO", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("ouCOD_ERR", DbType.Int32,ParameterDirection.Output),
                new DbParameter("ouMSG_ERR", DbType.String,255,ParameterDirection.Output)};
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i].Value = System.DBNull.Value;
            }
            parameters[0].Value = objitem.CASOID;
            parameters[1].Value = objitem.INTERACT_ID;
            parameters[2].Value = objitem.TIPO_CODIGO;
            parameters[3].Value = objitem.CLASE_CODIGO;
            parameters[4].Value = objitem.SUBCLASE_CODIGO;
            parameters[5].Value = objitem.SERVAFECT_CODE;
            parameters[6].Value = objitem.INCONVEN_CODE;
            parameters[7].Value = objitem.TIPO;
            parameters[8].Value = objitem.CLASE;
            parameters[9].Value = objitem.SUBCLASE;
            parameters[10].Value = objitem.SERVAFECT;
            parameters[11].Value = objitem.INCONVEN;
            parameters[12].Value = objitem.USUARIO_PROCESO;
            
            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION, parameters);
            intCoderror = Convert.ToInt(parameters[13].Value.ToString());
            strMessage = parameters[14].Value.ToString();

            return strMessage.Equals(KEY.AppSettings("InteractionMessage"));
;

        }

        //registra plantilla interaccion
        //nombre original InsertarPlantillaInteraccion
        //se necesista testear mas
        public static bool RegistrationInsertTemplateInteraction(string strSesion, string strTransaction, InsertTemplateInteraction objitem, string strInteraccionId, out string strFlagInsercion, out string strMessage)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: entra a RegistrationInsertTemplateInteraction", "");

        DbParameter[] parameters = {
            new DbParameter("P_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_INTER_1",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_2",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_3",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_4",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_5",DbType.String,1500,ParameterDirection.Input),
				new DbParameter("P_INTER_6",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_7",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_8",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_9",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_10",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_11",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_12",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_13",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_14",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_15",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_16",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_17",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_18",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_19",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_20",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_21",DbType.String,40,ParameterDirection.Input),
				new DbParameter("P_INTER_22",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_23",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_24",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_25",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_26",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_27",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_28",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_INTER_29",DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_INTER_30",DbType.String,32000,ParameterDirection.Input),
				new DbParameter("P_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
				new DbParameter("P_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
				new DbParameter("P_ADJUSTMENT_REASON",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_ADDRESS",DbType.String,100,ParameterDirection.Input),
				new DbParameter("P_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_BIRTHDAY",DbType.Date,ParameterDirection.Input),
				new DbParameter("P_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL2",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_EMAIL",DbType.String,100,ParameterDirection.Input),
				new DbParameter("P_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_IMEI",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_REASON",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_MODEL",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_LOT_CODE",DbType.String,50,ParameterDirection.Input),
				new DbParameter("P_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
				new DbParameter("P_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
				new DbParameter("P_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_MONTH",DbType.String,30,ParameterDirection.Input),
				new DbParameter("P_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_BASKET",DbType.String,50,ParameterDirection.Input),
			    new DbParameter("P_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
			    new DbParameter("P_ADDRESS5",DbType.String,200,ParameterDirection.Input),
			    new DbParameter("P_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
			    new DbParameter("P_CITY",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
			    new DbParameter("P_DISTRICT",DbType.String,200,ParameterDirection.Input),
			    new DbParameter("P_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_FAX",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
			    new DbParameter("P_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_OCCUPATION",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_POSITION",DbType.String,30,ParameterDirection.Input),
			    new DbParameter("P_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
			    new DbParameter("P_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
			    new DbParameter("P_ZIPCODE",DbType.String,20,ParameterDirection.Input),
				new DbParameter("P_ICCID",DbType.String,20,ParameterDirection.Input),
				new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
				new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
				new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)	
        
        };
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }

            int i = 0;
            DateTime dateBegin = new DateTime(1, 1, 1);


            if (strInteraccionId != null)
            {
                parameters[i].Value = strInteraccionId;
                objitem._X_PLUS_INTER2INTERACT = Functions.CheckDbl(strInteraccionId);
            }

            i++;
            if (objitem._X_INTER_1 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_1);

            i++;
            if (objitem._X_INTER_2 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_2);

            i++;
            if (objitem._X_INTER_3 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_3);

            i++;
            if (objitem._X_INTER_4 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_4);

            i++;
            if (objitem._X_INTER_5 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_5);

            i++;
            if (objitem._X_INTER_6 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_6);

            i++;
            if (objitem._X_INTER_7 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_7);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_8);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_9);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_10);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objitem._X_INTER_11);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_12);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_13);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_14);

            i++;
            if (objitem._X_INTER_15 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_15);

            i++;
            if (objitem._X_INTER_16 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_16);

            i++;
            if (objitem._X_INTER_17 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_17);

            i++;
            if (objitem._X_INTER_18 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_18);

            i++;
            if (objitem._X_INTER_19 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_19);

            i++;
            if (objitem._X_INTER_20 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_20);

            i++;
            if (objitem._X_INTER_21 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_21);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objitem._X_INTER_22);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objitem._X_INTER_23);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objitem._X_INTER_24);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_25);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_26);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_27);

            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_INTER_28);

            i++;
            if (objitem._X_INTER_29 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_INTER_29);

            i++;
            if (objitem._X_INTER_30 != null)
            {
                string strValor = Functions.CheckStr(objitem._X_INTER_30);
                if (strValor == "")
                    strValor = ".";
                else
                    parameters[i].Value = Functions.CheckStr(objitem._X_INTER_30);
            }
            else
            {
                parameters[i].Value = " ";
            }
            i++;

            parameters[i].Value = Functions.CheckDbl(objitem._X_PLUS_INTER2INTERACT);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objitem._X_ADJUSTMENT_AMOUNT);

            i++;
            if (objitem._X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_ADJUSTMENT_REASON);

            i++;
            if (objitem._X_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_ADDRESS);

            i++;
            if (objitem._X_AMOUNT_UNIT != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_AMOUNT_UNIT);

            i++;
            if (objitem._X_BIRTHDAY != dateBegin)
                parameters[i].Value = Functions.CheckDate(objitem._X_BIRTHDAY);

            i++;
            if (objitem._X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARIFY_INTERACTION);

            i++;
            if (objitem._X_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARO_LDN1);

            i++;
            if (objitem._X_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARO_LDN2);

            i++;
            if (objitem._X_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARO_LDN3);

            i++;
            if (objitem._X_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARO_LDN4);

            i++;
            if (objitem._X_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL1);

            i++;
            if (objitem._X_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL2);

            i++;
            if (objitem._X_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL3);

            i++;
            if (objitem._X_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL4);

            i++;
            if (objitem._X_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL5);

            i++;
            if (objitem._X_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLAROLOCAL6);

            i++;
            if (objitem._X_CONTACT_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CONTACT_PHONE);

            i++;
            if (objitem._X_DNI_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_DNI_LEGAL_REP);

            i++;
            if (objitem._X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_DOCUMENT_NUMBER);

            i++;
            if (objitem._X_EMAIL != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_EMAIL);

            i++;
            if (objitem._X_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FIRST_NAME);

            i++;
            if (objitem._X_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FIXED_NUMBER);

            i++;
            if (objitem._X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_CHANGE_USER);

            i++;
            if (objitem._X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_LEGAL_REP);

            i++;
            if (objitem._X_FLAG_OTHER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_OTHER);

            i++;
            if (objitem._X_FLAG_TITULAR != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_TITULAR);

            i++;
            if (objitem._X_IMEI != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_IMEI);

            i++;
            if (objitem._X_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_LAST_NAME);

            i++;
            if (objitem._X_LASTNAME_REP != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_LASTNAME_REP);

            i++;
            if (objitem._X_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_LDI_NUMBER);

            i++;
            if (objitem._X_NAME_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_NAME_LEGAL_REP);

            i++;
            if (objitem._X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLARO_LDN1);

            i++;
            if (objitem._X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLARO_LDN2);

            i++;
            if (objitem._X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLARO_LDN3);

            i++;
            if (objitem._X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLARO_LDN4);

            i++;
            if (objitem._X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL1);

            i++;
            if (objitem._X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL2);

            i++;
            if (objitem._X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL3);

            i++;
            if (objitem._X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL4);

            i++;
            if (objitem._X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL5);

            i++;
            if (objitem._X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_CLAROLOCAL6);

            i++;
            if (objitem._X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_DOC_NUMBER);

            i++;
            if (objitem._X_OLD_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_FIRST_NAME);

            i++;
            if (objitem._X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_FIXED_PHONE);

            i++;
            if (objitem._X_OLD_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_LAST_NAME);

            i++;
            if (objitem._X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_LDI_NUMBER);

            i++;
            if (objitem._X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OLD_FIXED_NUMBER);

            i++;
            if (objitem._X_OPERATION_TYPE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OPERATION_TYPE);

            i++;
            if (objitem._X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OTHER_DOC_NUMBER);

            i++;
            if (objitem._X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OTHER_FIRST_NAME);

            i++;
            if (objitem._X_OTHER_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OTHER_LAST_NAME);

            i++;
            if (objitem._X_OTHER_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OTHER_PHONE);

            i++;
            if (objitem._X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_PHONE_LEGAL_REP);

            i++;
            if (objitem._X_REFERENCE_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_REFERENCE_PHONE);

            i++;
            if (objitem._X_REASON != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_REASON);

            i++;
            if (objitem._X_MODEL != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_MODEL);

            i++;
            if (objitem._X_LOT_CODE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_LOT_CODE);

            i++;
            if (objitem._X_FLAG_REGISTERED != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_REGISTERED);

            i++;
            if (objitem._X_REGISTRATION_REASON != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_REGISTRATION_REASON);

            i++;
            if (objitem._X_CLARO_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CLARO_NUMBER);

            i++;
            if (objitem._X_MONTH != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_MONTH);

            i++;
            if (objitem._X_OST_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OST_NUMBER);

            i++;
            if (objitem._X_BASKET != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_BASKET);

            i++;
            if (objitem._X_EXPIRE_DATE != dateBegin)
                parameters[i].Value = Functions.CheckDate(objitem._X_EXPIRE_DATE);

            i++;
            if (objitem._X_ADDRESS5 != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_ADDRESS5);
            i++;
            parameters[i].Value = Functions.CheckDbl(objitem._X_CHARGE_AMOUNT);
            i++;
            if (objitem._X_CITY != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CITY);
            i++;
            if (objitem._X_CONTACT_SEX != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_CONTACT_SEX);
            i++;
            if (objitem._X_DEPARTMENT != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_DEPARTMENT);
            i++;
            if (objitem._X_DISTRICT != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_DISTRICT);
            i++;
            if (objitem._X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_EMAIL_CONFIRMATION);
            i++;
            if (objitem._X_FAX != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FAX);
            i++;
            if (objitem._X_FLAG_CHARGE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_FLAG_CHARGE);
            i++;
            if (objitem._X_MARITAL_STATUS != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_MARITAL_STATUS);
            i++;
            if (objitem._X_OCCUPATION != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_OCCUPATION);
            i++;
            if (objitem._X_POSITION != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_POSITION);
            i++;
            if (objitem._X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_REFERENCE_ADDRESS);
            i++;
            if (objitem._X_TYPE_DOCUMENT != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_TYPE_DOCUMENT);
            i++;
            if (objitem._X_ZIPCODE != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_ZIPCODE);

            i++;
            if (objitem._X_ICCID != null)
                parameters[i].Value = Functions.CheckStr(objitem._X_ICCID);

         
            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER, parameters);

             strFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
             strMessage = parameters[parameters.Length - 1].Value.ToString();
             Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de RegistrationInsertTemplateInteraction", "strFlagInsercion: " + strFlagInsercion + "strMessage: " + strMessage);

             Logging.Info(strSesion, strTransaction, "BACK_INTERACTION" + strFlagInsercion);
             Logging.Info(strSesion, strTransaction, "BACK_INTERACTION" + strMessage);

             return (strFlagInsercion.Equals(KEY.AppSettings("InteractionMessage")));
            //return (result>0);

        }
        //insertar plantilla de interaccion
        //nombre original InsetarPlantillaInteraccion
        //usa diferent sp que la funcion de arriba
        //falta testear

        public static bool InsTemplateInteraction(string strSesion, string strTransaction, InsertTemplateInteraction objItem, string strInteractionid, out string flagInsercion, out string strMessage)
        {

            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetInserInteractionTemplatInsTemplateInteractioneresponse", "");

            DbParameter[] parameters = new DbParameter[] { 
            
                                                   new DbParameter("PN_SECUENCIAL",DbType.Double,ParameterDirection.Input),
												   new DbParameter("PV_NRO_INTERACCION",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("PV_INTER_1",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_2",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_3",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_4",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_5",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_6",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_7",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PN_INTER_8",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_9",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_10",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_11",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_12",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_13",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_14",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_INTER_15",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_16",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_17",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_18",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_19",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_20",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_INTER_21",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PN_INTER_22",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_23",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_24",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_25",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_26",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_27",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_INTER_28",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_INTER_29",DbType.String,255,ParameterDirection.Input),
												   new DbParameter("PC_INTER_30",DbType.String,ParameterDirection.Input),
												   new DbParameter("PN_PLUS_INTER2INTERACT",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PN_ADJUSTMENT_AMOUNT",DbType.Double,ParameterDirection.Input),
												   new DbParameter("PV_ADJUSTMENT_REASON",DbType.String,	20,ParameterDirection.Input),
												   new DbParameter("PV_ADDRESS",DbType.String,100,ParameterDirection.Input),
												   new DbParameter("PV_AMOUNT_UNIT",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PD_BIRTHDAY",DbType.Date,ParameterDirection.Input),
												   new DbParameter("PV_CLARIFY_INTERACTION",DbType.String,15,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_CONTACT_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_DNI_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_DOCUMENT_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_EMAIL",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_FIRST_NAME",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_CHANGE_USER",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_LEGAL_REP",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_OTHER",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_TITULAR",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_IMEI",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_LASTNAME_REP",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_NAME_LEGAL_REP",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLARO_LDN4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL1",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL2",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL3",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL4",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL5",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_CLAROLOCAL6",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIXED_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OLD_LDI_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OLD_FIXED_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OPERATION_TYPE",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_DOC_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_FIRST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_LAST_NAME",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OTHER_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_PHONE_LEGAL_REP",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_REFERENCE_PHONE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_REASON",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_MODEL",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_LOT_CODE",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_REGISTERED",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_REGISTRATION_REASON",DbType.String,80,ParameterDirection.Input),
												   new DbParameter("PV_CLARO_NUMBER",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_MONTH",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_OST_NUMBER",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_BASKET",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PD_EXPIRE_DATE",DbType.Date,ParameterDirection.Input),
												   new DbParameter("PV_ADDRESS5",DbType.String,200,ParameterDirection.Input),
												   new DbParameter("PN_CHARGE_AMOUNT",DbType.Decimal,ParameterDirection.Input),
												   new DbParameter("PV_CITY",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_CONTACT_SEX",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_DEPARTMENT",DbType.String,40,ParameterDirection.Input),
												   new DbParameter("PV_DISTRICT",DbType.String,200,ParameterDirection.Input),
												   new DbParameter("PV_EMAIL_CONFIRMATION",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_FAX",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_FLAG_CHARGE",DbType.String,1,ParameterDirection.Input),
												   new DbParameter("PV_MARITAL_STATUS",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_OCCUPATION",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_POSITION",DbType.String,30,ParameterDirection.Input),
												   new DbParameter("PV_REFERENCE_ADDRESS",DbType.String,50,ParameterDirection.Input),
												   new DbParameter("PV_TYPE_DOCUMENT",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_ZIPCODE",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("PV_ICCID",DbType.String,20,ParameterDirection.Input),
												   new DbParameter("ID_INTERACCION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("FLAG_CREACION",DbType.String,255,ParameterDirection.Output),
												   new DbParameter("MSG_TEXT",DbType.String,255,ParameterDirection.Output)										
            };

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            int i = 0;
            DateTime dateStart = new DateTime(1, 1, 1);
            if (strInteractionid != null)
            {
                parameters[i].Value = Functions.CheckDbl(strInteractionid);
            }

            i++;
            if (strInteractionid != null)
            {
                parameters[i].Value = strInteractionid;
                objItem._X_PLUS_INTER2INTERACT = Functions.CheckDbl(strInteractionid);
            }

            i++;
            if (objItem._X_INTER_1 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_1);

            i++;
            if (objItem._X_INTER_2 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_2);

            i++;
            if (objItem._X_INTER_3 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_3);

            i++;
            if (objItem._X_INTER_4 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_4);

            i++;
            if (objItem._X_INTER_5 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_5);

            i++;
            if (objItem._X_INTER_6 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_6);

            i++;
            if (objItem._X_INTER_7 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_7);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_8);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_9);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_10);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objItem._X_INTER_11);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_12);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_13);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_14);

            i++;
            if (objItem._X_INTER_15 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_15);

            i++;
            if (objItem._X_INTER_16 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_16);

            i++;
            if (objItem._X_INTER_17 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_17);

            i++;
            if (objItem._X_INTER_18 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_18);

            i++;
            if (objItem._X_INTER_19 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_19);

            i++;
            if (objItem._X_INTER_20 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_20);

            i++;
            if (objItem._X_INTER_21 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_21);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objItem._X_INTER_22);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objItem._X_INTER_23);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objItem._X_INTER_24);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_25);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_26);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_27);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_INTER_28);

            i++;
            if (objItem._X_INTER_29 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_29);

            i++;
            if (objItem._X_INTER_30 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_INTER_30);

            i++;

            parameters[i].Value = Functions.CheckDbl(objItem._X_PLUS_INTER2INTERACT);

            i++;

            parameters[i].Value = Functions.CheckDblDB(objItem._X_ADJUSTMENT_AMOUNT);

            i++;
            if (objItem._X_ADJUSTMENT_REASON != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_ADJUSTMENT_REASON);

            i++;
            if (objItem._X_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_ADDRESS);

            i++;
            if (objItem._X_AMOUNT_UNIT != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_AMOUNT_UNIT);

            i++;
            if (objItem._X_BIRTHDAY != dateStart)
                parameters[i].Value = Functions.CheckDate(objItem._X_BIRTHDAY);

            i++;
            if (objItem._X_CLARIFY_INTERACTION != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARIFY_INTERACTION);

            i++;
            if (objItem._X_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARO_LDN1);

            i++;
            if (objItem._X_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARO_LDN2);

            i++;
            if (objItem._X_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARO_LDN3);

            i++;
            if (objItem._X_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARO_LDN4);

            i++;
            if (objItem._X_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL1);

            i++;
            if (objItem._X_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL2);

            i++;
            if (objItem._X_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL3);

            i++;
            if (objItem._X_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL4);

            i++;
            if (objItem._X_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL5);

            i++;
            if (objItem._X_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLAROLOCAL6);

            i++;
            if (objItem._X_CONTACT_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CONTACT_PHONE);

            i++;
            if (objItem._X_DNI_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_DNI_LEGAL_REP);

            i++;
            if (objItem._X_DOCUMENT_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_DOCUMENT_NUMBER);

            i++;
            if (objItem._X_EMAIL != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_EMAIL);

            i++;
            if (objItem._X_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FIRST_NAME);

            i++;
            if (objItem._X_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FIXED_NUMBER);

            i++;
            if (objItem._X_FLAG_CHANGE_USER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_CHANGE_USER);

            i++;
            if (objItem._X_FLAG_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_LEGAL_REP);

            i++;
            if (objItem._X_FLAG_OTHER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_OTHER);

            i++;
            if (objItem._X_FLAG_TITULAR != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_TITULAR);

            i++;
            if (objItem._X_IMEI != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_IMEI);

            i++;
            if (objItem._X_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_LAST_NAME);

            i++;
            if (objItem._X_LASTNAME_REP != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_LASTNAME_REP);

            i++;
            if (objItem._X_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_LDI_NUMBER);

            i++;
            if (objItem._X_NAME_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_NAME_LEGAL_REP);

            i++;
            if (objItem._X_OLD_CLARO_LDN1 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLARO_LDN1);

            i++;
            if (objItem._X_OLD_CLARO_LDN2 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLARO_LDN2);

            i++;
            if (objItem._X_OLD_CLARO_LDN3 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLARO_LDN3);

            i++;
            if (objItem._X_OLD_CLARO_LDN4 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLARO_LDN4);

            i++;
            if (objItem._X_OLD_CLAROLOCAL1 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL1);

            i++;
            if (objItem._X_OLD_CLAROLOCAL2 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL2);

            i++;
            if (objItem._X_OLD_CLAROLOCAL3 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL3);

            i++;
            if (objItem._X_OLD_CLAROLOCAL4 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL4);

            i++;
            if (objItem._X_OLD_CLAROLOCAL5 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL5);

            i++;
            if (objItem._X_OLD_CLAROLOCAL6 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_CLAROLOCAL6);

            i++;
            if (objItem._X_OLD_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_DOC_NUMBER);

            i++;
            if (objItem._X_OLD_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_FIRST_NAME);

            i++;
            if (objItem._X_OLD_FIXED_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_FIXED_PHONE);

            i++;
            if (objItem._X_OLD_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_LAST_NAME);

            i++;
            if (objItem._X_OLD_LDI_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_LDI_NUMBER);

            i++;
            if (objItem._X_OLD_FIXED_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OLD_FIXED_NUMBER);

            i++;
            if (objItem._X_OPERATION_TYPE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OPERATION_TYPE);

            i++;
            if (objItem._X_OTHER_DOC_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OTHER_DOC_NUMBER);

            i++;
            if (objItem._X_OTHER_FIRST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OTHER_FIRST_NAME);

            i++;
            if (objItem._X_OTHER_LAST_NAME != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OTHER_LAST_NAME);

            i++;
            if (objItem._X_OTHER_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OTHER_PHONE);

            i++;
            if (objItem._X_PHONE_LEGAL_REP != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_PHONE_LEGAL_REP);

            i++;
            if (objItem._X_REFERENCE_PHONE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_REFERENCE_PHONE);

            i++;
            if (objItem._X_REASON != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_REASON);

            i++;
            if (objItem._X_MODEL != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_MODEL);

            i++;
            if (objItem._X_LOT_CODE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_LOT_CODE);

            i++;
            if (objItem._X_FLAG_REGISTERED != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_REGISTERED);

            i++;
            if (objItem._X_REGISTRATION_REASON != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_REGISTRATION_REASON);

            i++;
            if (objItem._X_CLARO_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CLARO_NUMBER);

            i++;
            if (objItem._X_MONTH != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_MONTH);

            i++;
            if (objItem._X_OST_NUMBER != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OST_NUMBER);

            i++;
            if (objItem._X_BASKET != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_BASKET);

            i++;
            if (objItem._X_EXPIRE_DATE != dateStart)
                parameters[i].Value = Functions.CheckDate(objItem._X_EXPIRE_DATE);

            i++;
            if (objItem._X_ADDRESS5 != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_ADDRESS5);
            i++;
            parameters[i].Value = Functions.CheckDbl(objItem._X_CHARGE_AMOUNT);
            i++;
            if (objItem._X_CITY != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CITY);
            i++;
            if (objItem._X_CONTACT_SEX != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_CONTACT_SEX);
            i++;
            if (objItem._X_DEPARTMENT != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_DEPARTMENT);
            i++;
            if (objItem._X_DISTRICT != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_DISTRICT);
            i++;
            if (objItem._X_EMAIL_CONFIRMATION != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_EMAIL_CONFIRMATION);
            i++;
            if (objItem._X_FAX != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FAX);
            i++;
            if (objItem._X_FLAG_CHARGE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_FLAG_CHARGE);
            i++;
            if (objItem._X_MARITAL_STATUS != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_MARITAL_STATUS);
            i++;
            if (objItem._X_OCCUPATION != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_OCCUPATION);
            i++;
            if (objItem._X_POSITION != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_POSITION);
            i++;
            if (objItem._X_REFERENCE_ADDRESS != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_REFERENCE_ADDRESS);
            i++;
            if (objItem._X_TYPE_DOCUMENT != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_TYPE_DOCUMENT);
            i++;
            if (objItem._X_ZIPCODE != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_ZIPCODE);

            i++;
            if (objItem._X_ICCID != null)
                parameters[i].Value = Functions.CheckStr(objItem._X_ICCID);


            //int result = 0;
            //result = DbFactory.ExecuteNonQuery(strsesion, strtransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_PLUS_INTER, parameters);

            //rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
            //rMsgText = parameters[parameters.Length - 1].Value.ToString();

            //return (rFlagInsercion.Equals("OK"));

            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_X_PLUS_INTER, parameters);

            flagInsercion = parameters[parameters.Length - 2].Value.ToString();
            strMessage = parameters[parameters.Length - 1].Value.ToString();

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetInserInteractionTemplatInsTemplateInteractioneresponse", " flagInsercion: " + flagInsercion + "strMessage: " + strMessage);

            return (flagInsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }


        //insertar interaccion
        //la funcion original se llama insertarinteraccion
        //falta probar
          //public static bool InsertInteraction(string strsesion, string strtransaction, InsertInteract item, ref string interactionid, ref string strflaginsercion, ref string msgtext)
        public static bool InsertInteraction(string strSesion, string strTransaction, Iteraction objItem, out string strInteractionid, out string strFlaginsercion, out string strMessage)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a InsertInteraction", "");

            DbParameter[] parameters = new DbParameter[]
                         {
                                  new DbParameter("PN_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
							      new DbParameter("PN_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
								  new DbParameter("PV_ACCOUNT", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_PHONE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_TIPO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_CLASE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_SUBCLASE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
							      new DbParameter("PV_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_AGENTE", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PN_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_NOTAS", DbType.String,4000,ParameterDirection.Input),
								  new DbParameter("PV_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("PV_RESULTADO", DbType.String,255,ParameterDirection.Input),
								  new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
								  new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
								  new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
                      };
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
            int i = 0;
            if (objItem.OBJID_CONTACTO != null)
                parameters[0].Value = 0;

            i++;
            if (objItem.OBJID_SITE != null)
                parameters[1].Value = Functions.CheckInt64(objItem.OBJID_SITE);

            i++;
            if (objItem.CUENTA != null)
                parameters[2].Value = objItem.CUENTA.ToString();

            i++;
            if (objItem.TELEFONO != null)
                parameters[3].Value = objItem.TELEFONO.ToString();
            i++;
            if (objItem.TIPO != null)
                parameters[4].Value = objItem.TIPO.ToString();

            i++;
            if (objItem.CLASE != null)
                parameters[5].Value = objItem.CLASE.ToString();

            i++;
            if (objItem.SUBCLASE != null)
                parameters[6].Value = objItem.SUBCLASE.ToString();

            i++;
            if (objItem.METODO != null)
                parameters[7].Value = objItem.METODO.ToString();

            i++;
            if (objItem.TIPO_INTER != null)
                parameters[8].Value = objItem.TIPO_INTER.ToString();

            i++;
            if (objItem.AGENTE != null)
                parameters[9].Value = objItem.AGENTE.ToString();

            i++;
            if (objItem.USUARIO_PROCESO != null)
                parameters[10].Value = objItem.USUARIO_PROCESO.ToString();

            i++;
            if (objItem.HECHO_EN_UNO != null)
                parameters[11].Value = objItem.HECHO_EN_UNO.ToString();

            i++;
            if (objItem.NOTAS != null)
                parameters[12].Value = objItem.NOTAS.ToString();

            i++;
            if (objItem.FLAG_CASO != null)
                parameters[13].Value = objItem.FLAG_CASO.ToString();

            i++;
            if (objItem.RESULTADO != null)
                parameters[14].Value = objItem.RESULTADO.ToString();

            int result = 0;
            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_INSERTAR_INTERACT, parameters);

            strInteractionid = Functions.CheckStr(parameters[15].Value.ToString());
            strFlaginsercion = Functions.CheckStr(parameters[16].Value.ToString());
            strMessage = Functions.CheckStr(parameters[17].Value.ToString());

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de InsertInteraction", "strInteractionid: " + strInteractionid + "strFlaginsercion: " + strFlaginsercion + "strMessage: " + strMessage);

            return (strFlaginsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }
        //funcion insertar de interaccion , registra interaccion
        //nombre original: insertar
        //public bool Insert(string strsesion, string strtransaction, InsertInteract item, ref string interactionid, ref string strflaginsercion, ref string msgtext)
         public static bool Insert(string strSesion, string strTransaction, Iteraction objItem, out string intTeractionid, out string strFlaginsercion, out string strMessage)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a Insert", "");

            DbParameter[] parameters = new DbParameter[] 
            {
            new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
				new DbParameter("P_SITEOBJID_1", DbType.String,ParameterDirection.Input),
				new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
				new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
				new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
				new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
				new DbParameter("MSG_TEXT", DbType.String,500,ParameterDirection.Output)			
            
            };



            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;

            }
            if (objItem.OBJID_CONTACTO != null)
                parameters[0].Value = Functions.CheckInt64(objItem.OBJID_CONTACTO);

          
            if (objItem.OBJID_SITE != null)
                parameters[1].Value = Functions.CheckInt64(objItem.OBJID_SITE);

         
            if (objItem.CUENTA != null)
                parameters[2].Value = objItem.CUENTA.ToString();

         
            if (objItem.TELEFONO != null)
                parameters[3].Value = objItem.TELEFONO.ToString();
          
            if (objItem.TIPO != null)
                parameters[4].Value = objItem.TIPO.ToString();

          
            if (objItem.CLASE != null)
                parameters[5].Value = objItem.CLASE.ToString();

         
            if (objItem.SUBCLASE != null)
                parameters[6].Value = objItem.SUBCLASE.ToString();

           
            if (objItem.METODO != null)
                parameters[7].Value = objItem.METODO.ToString();

           
            if (objItem.TIPO_INTER != null)
                parameters[8].Value = objItem.TIPO_INTER.ToString();

         
            if (objItem.AGENTE != null)
                parameters[9].Value = objItem.AGENTE.ToString();

            
            if (objItem.USUARIO_PROCESO != null)
                parameters[10].Value = objItem.USUARIO_PROCESO.ToString();

          
            if (objItem.HECHO_EN_UNO != null)
                parameters[11].Value = objItem.HECHO_EN_UNO.ToString();

           
            if (objItem.NOTAS != null)
                parameters[12].Value = objItem.NOTAS.ToString();

           
            if (objItem.FLAG_CASO != null)
                parameters[13].Value = objItem.FLAG_CASO.ToString();

            
            if (objItem.RESULTADO != null)
                parameters[14].Value = objItem.RESULTADO.ToString();

            int result = 0;

            result = DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);
            intTeractionid = Functions.CheckStr(parameters[15].Value);
            if (intTeractionid.ToUpper().Equals("NULL")) intTeractionid = "";
            strFlaginsercion = Functions.CheckStr(parameters[16].Value);
            strMessage = Functions.CheckStr(parameters[17].Value);

            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a Insert", "intTeractionid: " + intTeractionid + "strFlaginsercion: " + strFlaginsercion + "strMessage: " + strMessage);



            return (strFlaginsercion.Equals(KEY.AppSettings("InteractionMessage")));
        }

        //obtenerciente
        //obtieen al cliente
        public static Client GetClient(string strSesion, string strTransaction, string strPhone, string strAccount, string strContactobjid, string strflagreg, out  string strflagquery, out string strMessage)
        {
            Claro.Web.Logging.Info("Session: 270492", "Transaction: Entra a GetClient", "strphone" + strPhone + "//straccount : " + strAccount + "// strContactobjid : " + strContactobjid + "//strflagreg : " + strflagreg);

            if (strContactobjid == "")
                strContactobjid = null;
            DbParameter[] parameters = new DbParameter[]
            {
            new DbParameter("P_PHONE", DbType.String,20,ParameterDirection.Input,strPhone),
			new DbParameter("P_ACCOUNT", DbType.String,80,ParameterDirection.Input,strAccount),
			new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input,Convert.ToInt64(strContactobjid)),
		    new DbParameter("P_FLAG_REG", DbType.String,20,ParameterDirection.Input,strflagreg),												
			new DbParameter("P_FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output),
			new DbParameter("P_MSG_TEXT", DbType.String,255,ParameterDirection.Output),
			new DbParameter("CUSTOMER", DbType.Object, ParameterDirection.Output)
            
            };


            Client entity = null;
            DbFactory.ExecuteReader(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CUSTOMER_CLFY, parameters, (IDataReader reader) =>
            {

                if (reader.Read())
                {

                    entity = new Client();
                    entity.OBJID_CONTACTO = Functions.CheckStr(reader["OBJID_CONTACTO"]);
                    entity.OBJID_SITE = Functions.CheckStr(reader["OBJID_SITE"]);
                    entity.TELEFONO = Functions.CheckStr(reader["TELEFONO"]);
                    entity.CUENTA = Functions.CheckStr(reader["CUENTA"]);
                    entity.MODALIDAD = Functions.CheckStr(reader["MODALIDAD"]);
                    entity.SEGMENTO = Functions.CheckStr(reader["SEGMENTO"]);
                    entity.ROL_CONTACTO = Functions.CheckStr(reader["ROL_CONTACTO"]);
                    entity.ESTADO_CONTACTO = Functions.CheckStr(reader["ESTADO_CONTACTO"]);
                    entity.ESTADO_CONTRATO = Functions.CheckStr(reader["ESTADO_CONTRATO"]);
                    entity.ESTADO_SITE = Functions.CheckStr(reader["ESTADO_SITE"]);
                    entity.S_NOMBRES = Functions.CheckStr(reader["S_NOMBRES"]);
                    entity.S_APELLIDOS = Functions.CheckStr(reader["S_APELLIDOS"]);
                    entity.NOMBRES = Functions.CheckStr(reader["NOMBRES"]);
                    entity.APELLIDOS = Functions.CheckStr(reader["APELLIDOS"]);
                    entity.DOMICILIO = Functions.CheckStr(reader["DOMICILIO"]);
                    entity.URBANIZACION = Functions.CheckStr(reader["URBANIZACION"]);
                    entity.REFERENCIA = Functions.CheckStr(reader["REFERENCIA"]);
                    entity.CIUDAD = Functions.CheckStr(reader["CIUDAD"]);
                    entity.DISTRITO = Functions.CheckStr(reader["DISTRITO"]);
                    entity.DEPARTAMENTO = Functions.CheckStr(reader["DEPARTAMENTO"]);
                    entity.ZIPCODE = Functions.CheckStr(reader["ZIPCODE"]);
                    entity.EMAIL = Functions.CheckStr(reader["EMAIL"]);
                    entity.TELEF_REFERENCIA = Functions.CheckStr(reader["TELEF_REFERENCIA"]);
                    entity.FAX = Functions.CheckStr(reader["FAX"]);
                    DateTime fecha = new DateTime(1, 1, 1);
                    if (reader["FECHA_NAC"] != DBNull.Value)
                        if (Functions.CheckDate(reader["FECHA_NAC"]) != fecha)
                            entity.FECHA_NAC = Functions.CheckDate(reader["FECHA_NAC"]).ToShortDateString();
                    entity.SEXO = Functions.CheckStr(reader["SEXO"]);
                    entity.ESTADO_CIVIL = Functions.CheckStr(reader["ESTADO_CIVIL"]);
                    entity.TIPO_DOC = Functions.CheckStr(reader["TIPO_DOC"]);
                    entity.NRO_DOC = Functions.CheckStr(reader["NRO_DOC"]);
                    entity.FECHA_ACT = Functions.CheckDate(reader["FECHA_ACT"]);
                    entity.PUNTO_VENTA = Functions.CheckStr(reader["PUNTO_VENTA"]);
                    entity.FLAG_REGISTRADO = Functions.CheckInt(reader["FLAG_REGISTRADO"]);
                    entity.OCUPACION = Functions.CheckStr(reader["OCUPACION"]);
                    entity.CANT_REG = Functions.CheckStr(reader["CANT_REG"]);
                    entity.FLAG_EMAIL = Functions.CheckStr(reader["FLAG_EMAIL"]);
                    entity.MOTIVO_REGISTRO = Functions.CheckStr(reader["MOTIVO_REGISTRO"]);
                    entity.FUNCION = Functions.CheckStr(reader["FUNCION"]);
                    entity.CARGO = Functions.CheckStr(reader["CARGO"]);
                    entity.LUGAR_NACIMIENTO_DES = Functions.CheckStr(reader["LUGAR_NAC"]);

                }
            });
            strflagquery = Functions.CheckStr(parameters[4].Value.ToString());
            strMessage = Functions.CheckStr(parameters[5].Value.ToString());

            Claro.Web.Logging.Info("Session: 270492", "Transaction: sale de GetObtClient", "Flagquery" + strflagquery + "//MsgText : " + strMessage);


            return entity;
        }



       //funcion insertarinteraccionesnegocio2
        public static bool InsertBusinessInteraction2(string strSesion, string strTransaction, Iteraction objItem, out string strInteractionId, out string strFlagInsertion, out string strMessage)
        {
            DbParameter[] parameters = new DbParameter[] {
                                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input), 
								new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input), 
                                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),												   
								new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),													   
								new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_HECHO_EN_UNO", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)};
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            parameters[0].Value = objItem.OBJID_CONTACTO;
            parameters[1].Value = objItem.OBJID_SITE;
            parameters[2].Value = objItem.CUENTA;
            parameters[3].Value = objItem.TELEFONO;
            parameters[4].Value = objItem.TIPO;
            parameters[5].Value = objItem.CLASE;
            parameters[6].Value = objItem.SUBCLASE;
            parameters[7].Value = objItem.METODO;
            parameters[8].Value = objItem.TIPO_INTER;
            parameters[9].Value = objItem.AGENTE;
            parameters[10].Value = objItem.USUARIO_PROCESO;
            parameters[11].Value = objItem.HECHO_EN_UNO;
            parameters[12].Value = objItem.NOTAS;
            parameters[13].Value = objItem.FLAG_CASO;
            parameters[14].Value = objItem.RESULTADO;

            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);
             
            strInteractionId = Functions.CheckStr(parameters[parameters.Length - 3].Value);
            strFlagInsertion = Functions.CheckStr(parameters[parameters.Length - 2].Value);
            strMessage = Functions.CheckStr(parameters[parameters.Length - 1].Value);

            if (String.IsNullOrEmpty(strMessage)) strMessage = KEY.AppSettings("InteractionMessage").ToString();
             
            return (result > 0); 
        }

        //insertar detalle
        //insertarinteracion detalle
        //insertarDetalle

        public static bool InsertDetail(string strSesion, string strTransaction, InteractionDet objItem, out string strFlagInsercion)
        {

            DbParameter[] parameters = new DbParameter[]
         {
            new DbParameter("v_objectID", DbType.Int64,ParameterDirection.Input),
		    new DbParameter("v_productID", DbType.Int64,ParameterDirection.Input),
			new DbParameter("v_productType", DbType.String,255,ParameterDirection.Input),
		    new DbParameter("v_productName", DbType.String,255,ParameterDirection.Input),
			new DbParameter("v_campaign", DbType.String,255,ParameterDirection.Input),
		    new DbParameter("v_points", DbType.Double,ParameterDirection.Input),
		    new DbParameter("v_amount", DbType.Double,ParameterDirection.Input),
		    new DbParameter("v_flgResidue", DbType.String,1,ParameterDirection.Input),
			new DbParameter("v_recharge", DbType.Double,ParameterDirection.Input),
			new DbParameter("v_aplicationID", DbType.Int64,ParameterDirection.Input),
		    new DbParameter("v_flgRegis", DbType.Int32,ParameterDirection.Output)
           
         };
            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }

            
            if (objItem.object_id != null)
                parameters[0].Value = Functions.CheckInt64(objItem.object_id);

            
            if (objItem.product_id != null)
                parameters[1].Value = Functions.CheckInt64(objItem.product_id);

           
            if (objItem.product_type != null)
                parameters[2].Value = objItem.product_type;

          
            if (objItem.product_name != null)
                parameters[3].Value = objItem.product_name;
          

            if (objItem.campaign != null)
                parameters[4].Value = objItem.campaign;

           
            if (objItem.points != null)
                parameters[5].Value = objItem.points;

           
            if (objItem.amount != null)
                parameters[6].Value = objItem.amount;

            
            if (objItem.flg_residue != null)
                parameters[7].Value = objItem.flg_residue;

          
            if (objItem.recharge_repayment != null)
                parameters[8].Value = objItem.recharge_repayment;

          
            if (objItem.application_id != null)
                parameters[9].Value = objItem.application_id;
            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_INS_DET_INTERACCION_DETALLE, parameters);
            strFlagInsercion = Functions.CheckStr(parameters[10].Value);
            return (strFlagInsercion.Equals(KEY.AppSettings("InteractionCero")));   
        }

        #endregion



        public static EntitiesFixed.GetCustomer.CustomerResponse GetValidateCustomer(EntitiesFixed.GetCustomer.GetCustomerRequest oGetCustomerRequest)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_phone", DbType.String,30, ParameterDirection.Input, oGetCustomerRequest.vPhone),
                new DbParameter("p_contactobjid", DbType.Int32,225, ParameterDirection.Output),
                new DbParameter("p_flag_insert", DbType.String,225, ParameterDirection.Output),
                new DbParameter("p_msg_text", DbType.String,225, ParameterDirection.Output)
            };
            EntitiesFixed.GetCustomer.CustomerResponse oCustomerResponse = new EntitiesFixed.GetCustomer.CustomerResponse();
            try
            {
                Claro.Web.Logging.ExecuteMethod(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SA_SP_SEARCH_CONTACT_USERLDI, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, ex.Message);

                Web.Logging.Info(oGetCustomerRequest.Audit.Session, oGetCustomerRequest.Audit.Transaction, "");
            }
            finally {
                oCustomerResponse.contactobjid =  parameters[parameters.Length - 3].Value.ToString();
                oCustomerResponse.vFlagInsert = parameters[parameters.Length - 2].Value.ToString();
                oCustomerResponse.rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }
            return oCustomerResponse;
        }

        #region FDQ
        public static SECURITY.GetSaveAudit.SaveAuditResponse SaveAudit(string strIdSession, string strTransaction, string vCuentaUsuario, string vIpCliente, string vIpServidor, string vMonto, string vNombreCliente, string vNombreServidor,
                                      string vServicio, string vTelefono, string vTexto, string vTransaccion)
        {

            SECURITY.GetSaveAudit.SaveAuditResponse objAuditResp = null;

            try
            {
                objAuditResp = new SECURITY.GetSaveAudit.SaveAuditResponse();
                AUDIT.EbsAuditoriaService objAuditoria = new AUDIT.EbsAuditoriaService();
                AUDIT.RegistroResponse objAudiResponse = new AUDIT.RegistroResponse();
                AUDIT.RegistroRequest objAudiRequest = new AUDIT.RegistroRequest();

                objAudiRequest.cuentaUsuario = vCuentaUsuario;
                objAudiRequest.monto = vMonto;
                objAudiRequest.servicio = vServicio;
                objAudiRequest.telefono = vTelefono;
                objAudiRequest.texto = vTexto;
                objAudiRequest.transaccion = vTransaccion;

                objAudiRequest.ipCliente = vIpCliente;
                objAudiRequest.nombreCliente = vNombreCliente;
                objAudiRequest.ipServidor = vIpServidor;
                objAudiRequest.nombreServidor = vNombreServidor;

                objAudiResponse = Claro.Web.Logging.ExecuteMethod<AUDIT.RegistroResponse>(strIdSession, strTransaction, () =>
                {
                    return Configuration.WebServiceConfiguration.GRABARAUDIT.registroAuditoria(objAudiRequest);

                });

                objAuditResp.vResultado = objAudiResponse.resultado;

                objAuditResp.vTransaccionResp = objAudiResponse.transaccion;
                objAuditResp.vestado = objAudiResponse.estado;

                if (objAudiResponse.estado == "0")
                {
                    objAuditResp.respuesta = false;
                }
                else
                {
                    objAuditResp.respuesta = true;
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "ERROR AUDITORIA : " + ex.Message);
                //throw;
            }
            

            return objAuditResp;
        }


        public static SECURITY.GetSaveAuditM.SaveAuditMResponse SaveAuditM(Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM.SaveAuditMRequest objRegistAuditoria)
        {

            SECURITY.GetSaveAuditM.SaveAuditMResponse oRegAuditoria = new SECURITY.GetSaveAuditM.SaveAuditMResponse();

            REGISTAUDIT.RegistroRequest oRegistroRequest = new REGISTAUDIT.RegistroRequest();
            REGISTAUDIT.registroAuditoriaResponse oRegistroAuditoriaResponse = new REGISTAUDIT.registroAuditoriaResponse();
            REGISTAUDIT.registroAuditoria oRegistroAuditoria = new REGISTAUDIT.registroAuditoria();

            REGISTAUDIT.RequestOpcionalComplexType[] listOptional = new REGISTAUDIT.RequestOpcionalComplexType[0];

            REGISTAUDIT.AuditRequest oAuditRequest = new REGISTAUDIT.AuditRequest();
            oAuditRequest.idTransaccion = objRegistAuditoria.Audit.Transaction;
            oAuditRequest.ipAplicacion = objRegistAuditoria.Audit.IPAddress;
            oAuditRequest.aplicacion = objRegistAuditoria.Audit.ApplicationName;
            oAuditRequest.usrAplicacion = objRegistAuditoria.Audit.UserName;

            oRegistroAuditoria.Audit = oAuditRequest;
            //oRegistroAuditoria.ListaOpcionalRequest = listOptional;
            oRegistroRequest.transaccion = objRegistAuditoria.vTransaccion;
            oRegistroRequest.servicio = objRegistAuditoria.vServicio;
            oRegistroRequest.ipCliente = objRegistAuditoria.vIpCliente;
            oRegistroRequest.nombreCliente = objRegistAuditoria.vNombreCliente;
            oRegistroRequest.ipServidor = objRegistAuditoria.vIpServidor;
            oRegistroRequest.nombreServidor = objRegistAuditoria.vNombreServidor;
            oRegistroRequest.cuentaUsuario = objRegistAuditoria.vCuentaUsuario;
            oRegistroRequest.telefono = objRegistAuditoria.vTelefono;
            oRegistroRequest.monto = objRegistAuditoria.vMonto;
            oRegistroRequest.texto = objRegistAuditoria.vTexto;
            oRegistroAuditoria.RegistroRequest = oRegistroRequest;


            oRegistroAuditoriaResponse = Claro.Web.Logging.ExecuteMethod(objRegistAuditoria.Audit.Session, objRegistAuditoria.Audit.Transaction, () =>
            {
                return WebServiceConfiguration.REGISTRARAUDIT.registroAuditoria(oRegistroAuditoria);

            });

            oRegAuditoria.vResultado = oRegistroAuditoriaResponse.AuditResponse.mensajeRespuesta;
            oRegAuditoria.vTransaccionResp = oRegistroAuditoriaResponse.AuditResponse.idTransaccion;

            if (oRegistroAuditoriaResponse.AuditResponse.codigoRespuesta == "0")
            {
                oRegAuditoria.respuesta = false;
            }
            else
            {
                oRegAuditoria.respuesta = true;
            }

            return oRegAuditoria;
        }
        #endregion

        public static bool GetInsertInteractHFC(string strIdSession, string strTransaction, EntitiesFixed.Interaction item,
            ref string rInteraccionId,
            ref string rFlagInsercion,
            ref string rMsgText)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input),
                new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_HECHO_EN_UNO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_SERVAFECT_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_INCONVEN_CODE", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_CO_ID", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_COD_PLANO", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR1", DbType.String,255,ParameterDirection.Input),
                new DbParameter("P_VALOR2", DbType.String,255,ParameterDirection.Input),
                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),				
                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)			
            };

            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (item.OBJID_CONTACTO != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_CONTACTO);

            i++;
            if (item.OBJID_SITE != null)
                parameters[i].Value = Convert.ToInt64(item.OBJID_SITE);

            i++;
            if (item.CUENTA != null)
                parameters[i].Value = item.CUENTA;

            i++;
            if (item.TELEFONO != null)
                parameters[i].Value = item.TELEFONO;
            i++;
            if (item.TIPO != null)
                parameters[i].Value = item.TIPO;

            i++;
            if (item.CLASE != null)
                parameters[i].Value = item.CLASE;

            i++;
            if (item.SUBCLASE != null)
                parameters[i].Value = item.SUBCLASE;

            i++;
            if (item.METODO != null)
                parameters[i].Value = item.METODO;

            i++;
            if (item.TIPO_INTER != null)
                parameters[i].Value = item.TIPO_INTER;

            i++;
            if (item.AGENTE != null)
                parameters[i].Value = item.AGENTE;

            i++;
            if (item.USUARIO_PROCESO != null)
                parameters[i].Value = item.USUARIO_PROCESO;

            i++;
            if (item.HECHO_EN_UNO != null)
                parameters[i].Value = item.HECHO_EN_UNO;

            i++;
            if (item.NOTAS != null)
                parameters[i].Value = item.NOTAS;

            i++;
            if (item.FLAG_CASO != null)
                parameters[i].Value = item.FLAG_CASO;

            i++;
            if (item.RESULTADO != null)
                parameters[i].Value = item.RESULTADO;

            i++;
            if (item.SERVICIO != null)
                parameters[i].Value = item.SERVICIO;

            i++;
            if (item.INCONVENIENTE != null)
                parameters[i].Value = item.INCONVENIENTE;

            i++;
            if (item.SERVICIO_CODE != null)
                parameters[i].Value = item.SERVICIO_CODE;

            i++;
            if (item.INCONVENIENTE_CODE != null)
                parameters[i].Value = item.INCONVENIENTE_CODE;

            i++;
            if (item.CONTRATO != null)
                parameters[i].Value = item.CONTRATO;

            i++;
            if (item.PLANO != null)
                parameters[i].Value = item.PLANO;

            i++;
            if (item.VALOR_1 != null)
                parameters[i].Value = item.VALOR_1;

            i++;
            if (item.VALOR_2 != null)
                parameters[i].Value = item.VALOR_2;



            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC, parameters);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rInteraccionId = parameters[parameters.Length - 3].Value.ToString();
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

                rInteraccionId = Functions.CheckStr(rInteraccionId);
                rFlagInsercion = Functions.CheckStr(rFlagInsercion);
                rMsgText = Functions.CheckStr(rMsgText);

                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rInteraccionId);
                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rFlagInsercion);
                Logging.Info(strIdSession, strTransaction, "INTERACCIÓN FRONT END: " + rMsgText);
            }

            return true;
        }

        public static SECURITY.InteractionTemplate GetDatTempInteraction(string strIdSession, string strTransaction, string vInteraccionID, ref string vFLAG_CONSULTA, ref string vMSG_TEXT)
        {
            DbParameter[] parameters = {
                new DbParameter("P_NRO_INTERACCION", DbType.String,255,ParameterDirection.Input, vInteraccionID),
                new DbParameter("FLAG_CONSULTA", DbType.String,255,ParameterDirection.Output, vFLAG_CONSULTA),
                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output,vMSG_TEXT),
                new DbParameter("OUT_CURSOR", DbType.Object,ParameterDirection.Output)
            };

            var item = new SECURITY.InteractionTemplate();
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY,
                        DbCommandConfiguration.SIACU_POST_CLARIFY_SP_QUERY_PLUS_INTER, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                item.TIENE_DATOS = "S";
                                item.ID_INTERACCION = reader["NRO_INTERACCION"].ToString();
                                item.X_INTER_1 = reader["X_INTER_1"].ToString();
                                item.X_INTER_2 = reader["X_INTER_2"].ToString();
                                item.X_INTER_3 = reader["X_INTER_3"].ToString();
                                item.X_INTER_4 = reader["X_INTER_4"].ToString();
                                item.X_INTER_5 = reader["X_INTER_5"].ToString();
                                item.X_INTER_6 = reader["X_INTER_6"].ToString();
                                item.X_INTER_7 = reader["X_INTER_7"].ToString();
                                item.X_INTER_8 = Convert.ToDouble(reader["X_INTER_8"]);
                                item.X_INTER_9 = Convert.ToDouble(reader["X_INTER_9"]);
                                item.X_INTER_10 = Convert.ToDouble(reader["X_INTER_10"]);
                                item.X_INTER_11 = Convert.ToDouble(reader["X_INTER_11"]);
                                item.X_INTER_12 = Convert.ToDouble(reader["X_INTER_12"]);
                                item.X_INTER_13 = Convert.ToDouble(reader["X_INTER_13"]);
                                item.X_INTER_14 = Convert.ToDouble(reader["X_INTER_14"]);
                                item.X_INTER_15 = reader["X_INTER_15"].ToString();
                                item.X_INTER_16 = reader["X_INTER_16"].ToString();
                                item.X_INTER_17 = reader["X_INTER_17"].ToString();
                                item.X_INTER_18 = reader["X_INTER_18"].ToString();
                                item.X_INTER_19 = reader["X_INTER_19"].ToString();
                                item.X_INTER_20 = reader["X_INTER_20"].ToString();
                                item.X_INTER_21 = reader["X_INTER_21"].ToString();
                                item.X_INTER_22 = Convert.ToDouble(reader["X_INTER_22"]);
                                item.X_INTER_23 = Convert.ToDouble(reader["X_INTER_23"]);
                                item.X_INTER_24 = Convert.ToDouble(reader["X_INTER_24"]);
                                item.X_INTER_25 = Convert.ToDouble(reader["X_INTER_25"]);
                                item.X_INTER_26 = Convert.ToDouble(reader["X_INTER_26"]);
                                item.X_INTER_27 = Convert.ToDouble(reader["X_INTER_27"]);
                                item.X_INTER_28 = Convert.ToDouble(reader["X_INTER_28"]);
                                item.X_INTER_29 = reader["X_INTER_29"].ToString();
                                item.X_INTER_30 = reader["X_INTER_30"].ToString();
                                item.X_PLUS_INTER2INTERACT = Convert.ToDouble(reader["X_PLUS_INTER2INTERACT"]);
                                item.X_ADJUSTMENT_AMOUNT = Convert.ToDouble(reader["X_ADJUSTMENT_AMOUNT"]);
                                item.X_ADJUSTMENT_REASON = reader["X_ADJUSTMENT_REASON"].ToString();
                                item.X_ADDRESS = reader["X_ADDRESS"].ToString();
                                item.X_AMOUNT_UNIT = reader["X_AMOUNT_UNIT"].ToString();

                                if (reader["X_BIRTHDAY"] != null && reader["X_BIRTHDAY"] != DBNull.Value)
                                    item.X_BIRTHDAY = Convert.ToDate(reader["X_BIRTHDAY"]);

                                item.X_CLARIFY_INTERACTION = reader["X_CLARIFY_INTERACTION"].ToString();
                                item.X_CLARO_LDN1 = reader["X_CLARO_LDN1"].ToString();
                                item.X_CLARO_LDN2 = reader["X_CLARO_LDN2"].ToString();
                                item.X_CLARO_LDN3 = reader["X_CLARO_LDN3"].ToString();
                                item.X_CLARO_LDN4 = reader["X_CLARO_LDN4"].ToString();
                                item.X_CLAROLOCAL1 = reader["X_CLAROLOCAL1"].ToString();
                                item.X_CLAROLOCAL2 = reader["X_CLAROLOCAL2"].ToString();
                                item.X_CLAROLOCAL3 = reader["X_CLAROLOCAL3"].ToString();
                                item.X_CLAROLOCAL4 = reader["X_CLAROLOCAL4"].ToString();
                                item.X_CLAROLOCAL5 = reader["X_CLAROLOCAL5"].ToString();
                                item.X_CLAROLOCAL6 = reader["X_CLAROLOCAL6"].ToString();
                                item.X_CONTACT_PHONE = reader["X_CONTACT_PHONE"].ToString();
                                item.X_DNI_LEGAL_REP = reader["X_DNI_LEGAL_REP"].ToString();
                                item.X_DOCUMENT_NUMBER = reader["X_DOCUMENT_NUMBER"].ToString();
                                item.X_EMAIL = reader["X_EMAIL"].ToString();
                                item.X_FIRST_NAME = reader["X_FIRST_NAME"].ToString();
                                item.X_FIXED_NUMBER = reader["X_FIXED_NUMBER"].ToString();
                                item.X_FLAG_CHANGE_USER = reader["X_FLAG_CHANGE_USER"].ToString();
                                item.X_FLAG_LEGAL_REP = reader["X_FLAG_LEGAL_REP"].ToString();
                                item.X_FLAG_OTHER = reader["X_FLAG_OTHER"].ToString();
                                item.X_FLAG_TITULAR = reader["X_FLAG_TITULAR"].ToString();
                                item.X_IMEI = reader["X_IMEI"].ToString();
                                item.X_LAST_NAME = reader["X_LAST_NAME"].ToString();
                                item.X_LASTNAME_REP = reader["X_LASTNAME_REP"].ToString();
                                item.X_LDI_NUMBER = reader["X_LDI_NUMBER"].ToString();
                                item.X_NAME_LEGAL_REP = reader["X_NAME_LEGAL_REP"].ToString();
                                item.X_OLD_CLARO_LDN1 = reader["X_OLD_CLARO_LDN1"].ToString();
                                item.X_OLD_CLARO_LDN2 = reader["X_OLD_CLARO_LDN2"].ToString();
                                item.X_OLD_CLARO_LDN3 = reader["X_OLD_CLARO_LDN3"].ToString();
                                item.X_OLD_CLARO_LDN4 = reader["X_OLD_CLARO_LDN4"].ToString();
                                item.X_OLD_CLAROLOCAL1 = reader["X_OLD_CLAROLOCAL1"].ToString();
                                item.X_OLD_CLAROLOCAL2 = reader["X_OLD_CLAROLOCAL2"].ToString();
                                item.X_OLD_CLAROLOCAL3 = reader["X_OLD_CLAROLOCAL3"].ToString();
                                item.X_OLD_CLAROLOCAL4 = reader["X_OLD_CLAROLOCAL4"].ToString();
                                item.X_OLD_CLAROLOCAL5 = reader["X_OLD_CLAROLOCAL5"].ToString();
                                item.X_OLD_CLAROLOCAL6 = reader["X_OLD_CLAROLOCAL6"].ToString();
                                item.X_OLD_DOC_NUMBER = reader["X_OLD_DOC_NUMBER"].ToString();
                                item.X_OLD_FIRST_NAME = reader["X_OLD_FIRST_NAME"].ToString();
                                item.X_OLD_FIXED_PHONE = reader["X_OLD_FIXED_PHONE"].ToString();
                                item.X_OLD_LAST_NAME = reader["X_OLD_LAST_NAME"].ToString();
                                item.X_OLD_LDI_NUMBER = reader["X_OLD_LDI_NUMBER"].ToString();
                                item.X_OLD_FIXED_NUMBER = reader["X_OLD_FIXED_NUMBER"].ToString();
                                item.X_OPERATION_TYPE = reader["X_OPERATION_TYPE"].ToString();
                                item.X_OTHER_DOC_NUMBER = reader["X_OTHER_DOC_NUMBER"].ToString();
                                item.X_OTHER_FIRST_NAME = reader["X_OTHER_FIRST_NAME"].ToString();
                                item.X_OTHER_LAST_NAME = reader["X_OTHER_LAST_NAME"].ToString();
                                item.X_OTHER_PHONE = reader["X_OTHER_PHONE"].ToString();
                                item.X_PHONE_LEGAL_REP = reader["X_PHONE_LEGAL_REP"].ToString();
                                item.X_REFERENCE_PHONE = reader["X_REFERENCE_PHONE"].ToString();
                                item.X_REASON = reader["X_REASON"].ToString();
                                item.X_MODEL = reader["X_MODEL"].ToString();
                                item.X_LOT_CODE = reader["X_LOT_CODE"].ToString();
                                item.X_FLAG_REGISTERED = reader["X_FLAG_REGISTERED"].ToString();
                                item.X_REGISTRATION_REASON = reader["X_REGISTRATION_REASON"].ToString();
                                item.X_CLARO_NUMBER = reader["X_CLARO_NUMBER"].ToString();
                                item.X_MONTH = reader["X_MONTH"].ToString();
                                item.X_OST_NUMBER = reader["X_OST_NUMBER"].ToString();
                                item.X_BASKET = reader["X_BASKET"].ToString();
                                if (reader["X_EXPIRE_DATE"] != null && reader["X_EXPIRE_DATE"] != DBNull.Value)
                                    item.X_EXPIRE_DATE = Convert.ToDate(reader["X_EXPIRE_DATE"]);
                                item.X_ADDRESS5 = reader["X_ADDRESS5"].ToString();
                                item.X_CHARGE_AMOUNT = Convert.ToDouble(reader["X_CHARGE_AMOUNT"]);
                                item.X_CITY = reader["X_CITY"].ToString();
                                item.X_CONTACT_SEX = reader["X_CONTACT_SEX"].ToString();
                                item.X_DEPARTMENT = reader["X_DEPARTMENT"].ToString();
                                item.X_DISTRICT = reader["X_DISTRICT"].ToString();
                                item.X_EMAIL_CONFIRMATION = reader["X_EMAIL_CONFIRMATION"].ToString();
                                item.X_FAX = reader["X_FAX"].ToString();
                                item.X_FLAG_CHARGE = reader["X_FLAG_CHARGE"].ToString();
                                item.X_MARITAL_STATUS = reader["X_MARITAL_STATUS"].ToString();
                                item.X_OCCUPATION = reader["X_OCCUPATION"].ToString();
                                item.X_POSITION = reader["X_POSITION"].ToString();
                                item.X_REFERENCE_ADDRESS = reader["X_REFERENCE_ADDRESS"].ToString();
                                item.X_TYPE_DOCUMENT = reader["X_TYPE_DOCUMENT"].ToString();
                                item.X_ZIPCODE = reader["X_ZIPCODE"].ToString();
                                item.X_ICCID = reader["X_ICCID"].ToString();
                            }
                        });
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                vFLAG_CONSULTA = parameters[parameters.Length - 2].Value.ToString();
                vMSG_TEXT = parameters[parameters.Length - 1].ToString();
            }

            return item;
        }

        #region Redirect
        public static List<Claro.SIACU.Entity.Transac.Service.Common.Redirect> GetRedirectSession(Claro.Entity.AuditRequest audit, string strApplication, string strOption, out string errorMsg, out string codError)
        {
            List<Claro.SIACU.Entity.Transac.Service.Common.Redirect> listRedirect = null;

            AUDIT_CREDENTIALS.ListaSesionesObtenidas objSesiones = new AUDIT_CREDENTIALS.ListaSesionesObtenidas();
            AUDIT_CREDENTIALS.ListaResponseOpcional listResponseOpc = new AUDIT_CREDENTIALS.ListaResponseOpcional();
            AUDIT_CREDENTIALS.parametrosAuditRequest request = new AUDIT_CREDENTIALS.parametrosAuditRequest()
            {
                idTransaccion = audit.Transaction,
                ipAplicacion = audit.IPAddress,
                nombreAplicacion = audit.ApplicationName,
                usuarioAplicacion = audit.UserName
            };
            AUDIT_CREDENTIALS.ListaRequestOpcional OptionRequest = new AUDIT_CREDENTIALS.ListaRequestOpcional();

            try
            {
                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse = Claro.Web.Logging.ExecuteMethod<AUDIT_CREDENTIALS.parametrosAuditResponse>(audit.Session, audit.Transaction, Configuration.ServiceConfiguration.AUDIT_CREDENTIALS, () =>
                {
                    return Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.obtenerSesiones(request, strOption, strApplication, OptionRequest, out objSesiones, out listResponseOpc);
                }
                    );

                codError = objAuditResponse.codigoRespuesta;
                errorMsg = objAuditResponse.mensajeRespuesta;

                if (codError == Claro.Constants.NumberZeroString && objSesiones.Count > 0)
                {
                    Claro.SIACU.Entity.Transac.Service.Common.Redirect redirect;
                    listRedirect = new List<Claro.SIACU.Entity.Transac.Service.Common.Redirect>();
                    for (int i = 0; i < objSesiones.Count; i++)
                    {
                        redirect = new Claro.SIACU.Entity.Transac.Service.Common.Redirect();
                        redirect.option_key = objSesiones[i].opcionParametroId.ToString();
                        redirect.session_name = objSesiones[i].nombreSesion.ToString();
                        redirect.option_type = objSesiones[i].tipoOpcion.ToString();
                        if (!String.IsNullOrEmpty(objSesiones[i].propSession))
                        {
                            redirect.prop_session = objSesiones[i].propSession.ToString();
                        }
                        if (!String.IsNullOrEmpty(objSesiones[i].valorSession))
                        {
                            redirect.value_Session = objSesiones[i].valorSession.ToString();
                        }
                        listRedirect.Add(redirect);
                    }
                }
            }
            catch (TimeoutException ex)
            {
                codError = "";
                errorMsg = "Limite de tiempo";
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (WebException ex)
            {
                codError = "";
                errorMsg = "Error";
                throw new Claro.MessageException(ex.Message.ToString());

            }

            catch (Exception ex)
            {
                codError = "";
                errorMsg = ex.Message;
            }

            return listRedirect;
        }

        /// <summary>
        /// Método que se genera con el servicio y registra la comunicación entre las páginas Redireccionadas, para ello se le agregará un parámetro de entrada que permita saber el nombre de la aplicación de destino.
        /// </summary>
        /// <param name="audit">Objeto auditoría</param>
        /// <param name="appDest">Aplicación destino</param>
        /// <param name="option">Opción</param>
        /// <param name="ipClient">Ip de cliente</param>
        /// <param name="ipServer">Ip de servidor</param>
        /// <param name="jsonParameters">Parámetros Json</param>
        /// <param name="Sequence">Secuencia</param>
        /// <param name="Url">Url</param>
        /// <returns>Devuelve secuencia y url para realizar la redirección.</returns>
        public static string InsertRedirectCommunication(Claro.Entity.AuditRequest audit, string appDest, string option, string ipClient, string ipServer, string jsonParameters, out string Sequence, out string Url)
        {
            string message = "";

            try
            {
                AUDIT_CREDENTIALS.ListaResponseOpcional objResponseOpc;
                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse =
                    Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.registrarComunicacion(new AUDIT_CREDENTIALS.parametrosAuditRequest()
                    {
                        idTransaccion = audit.Transaction,
                        ipAplicacion = audit.IPAddress,
                        nombreAplicacion = audit.ApplicationName,
                        usuarioAplicacion = audit.UserName,
                    },
                option,
                appDest,
                ipClient,
                ipServer,
                jsonParameters,
                new AUDIT_CREDENTIALS.ListaRequestOpcional(), out Sequence, out Url, out objResponseOpc);
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, string.Format("Metodo: {0} , Sequence: {1} , Url:{2}, option: {3}", "InsertRedirectCommunication", Sequence, Url, option));
                string codError = objAuditResponse.codigoRespuesta;
                string errorMsg = objAuditResponse.mensajeRespuesta;

                if (codError == Claro.Constants.NumberZeroString)
                {
                    if (Sequence != "" && Url != "")
                    {
                        message = "ok";
                    }
                    else
                    {
                        message = errorMsg;
                    }
                }
                else
                {
                    message = errorMsg;
                }
            }
            catch (TimeoutException ex)
            {
                message = "Time out";
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (WebException ex)
            {
                message = "Error";
                throw new Claro.MessageException(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                message = ex.Message;
                throw;
            }

            return message;
        }

        public static Boolean ValidateRedirectCommunication(Claro.Entity.AuditRequest audit, string sequence, out string errorMsg, string ipServer, out string urlDest, out string availability, out string jsonParameters)
        {
            Claro.Web.Logging.Info("1234566666", "1234566666", "sequence1 " + sequence + "ipServer1" + ipServer);
            errorMsg = "";
            urlDest = "";
            availability = "";
            jsonParameters = "";

            try
            {
                AUDIT_CREDENTIALS.ListaResponseOpcional objResponseOpc;

                AUDIT_CREDENTIALS.parametrosAuditResponse objAuditResponse;

                Claro.Web.Logging.Info("1234566666", "1234566666", "sequence2 " + sequence + "ipServer2" + ipServer );

                objAuditResponse = Configuration.ServiceConfiguration.AUDIT_CREDENTIALS.validarComunicacion(new AUDIT_CREDENTIALS.parametrosAuditRequest()
                {
                    idTransaccion = audit.Transaction,
                    ipAplicacion = audit.IPAddress,
                    nombreAplicacion = audit.ApplicationName,
                    usuarioAplicacion = audit.UserName,
                },
                sequence,
                ipServer,
                new AUDIT_CREDENTIALS.ListaRequestOpcional(),
                out urlDest,
                out availability,
                out jsonParameters,
                out objResponseOpc);



                string codError = objAuditResponse.codigoRespuesta;

                if (codError == Claro.Constants.NumberZeroString)
                {
                    if (jsonParameters != "" && urlDest != "")
                    {
                        //errorMsg = Claro.SIACU.Constants.ok;
                        errorMsg = "OK";
                    }
                    else
                    {
                        errorMsg = objAuditResponse.mensajeRespuesta;
                    }
                }
                else
                {
                    errorMsg = objAuditResponse.mensajeRespuesta;
                }
            }
            catch (TimeoutException ex)
            {
                Claro.Web.Logging.Info("1234566666", "1234566666", "TimeoutException_ex_: " + ex + "TimeoutException_ex.iner" + ex.InnerException);
                // errorMsg = Claro.SIACU.Transac.Service.Constants.MessageNotServicesLimitWait;
                errorMsg = "No se ha podido completar la operación debido a que el servicio web ha superado el tiempo límite de espera.";
                throw ex;
            }
            catch (WebException ex)
            {
                Claro.Web.Logging.Info("1234566666", "1234566666", "WebException_ex_: " + ex + "WebException_ex.iner" + ex.InnerException);
                //errorMsg = Claro.SIACU.Transac.Service.Constants.MessageNotComunicationServerRemote;
                errorMsg = "No se ha podido establecer comunicación con el servidor remoto.";
                throw ex;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info("1234566666", "1234566666", "Exception_ex_: " + ex + "Exception_ex.iner" + ex.InnerException);
                errorMsg = ex.Message;
                throw;
            }

            return true;
        }
        #endregion
       
        //Verificar Usuario SIAPO_Validar.aspx
        public static List<ConsultSecurity> VerifyUser(string sessionId, string transactionId, string appId, string appName, string username, string appCode)
        {
            string errorMsgTemp;
            var objResponse = new List<ConsultSecurity>();
            EntitiesConsultaSeguridad.seguridadType[] objLstService = { };
            var transactionIdTemp = string.Empty;
            var messageService = Web.Logging.ExecuteMethod(sessionId, transactionId, ServiceConfiguration.SIACU_ConsultaSeguridad, () =>
            {
                return ServiceConfiguration.SIACU_ConsultaSeguridad.verificaUsuario(ref transactionIdTemp, appId, appName, username, Convert.ToInt64(appCode), out errorMsgTemp, out objLstService);
            });

            if (objLstService.Length > 0 && string.IsNullOrEmpty(messageService))
            {
                foreach (var item in objLstService)
                {
                    var obj = new ConsultSecurity
                    {
                        Perfccod = item.PerfcCod,
                        Usuaccod = item.UsuacCod,
                        Usuaccodvensap = item.UsuacCodVenSap
                    };
                    objResponse.Add(obj);
                }
            }

            return objResponse;
        }



        public static string GetNumberEAI(string strIdSession, string strTransaction, string vMsisdn, ref string vNumber)
        {
            //string vNumber = "";
            DbParameter[] parameters = {
                new  DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input),
                new  DbParameter("P_NUMERO", DbType.String,255, ParameterDirection.Output)
            };
            for (int i = 0; i < parameters.Length; i++)
                parameters[i].Value = DBNull.Value;

            parameters[0].Value = vMsisdn;
            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO, parameters);
            vNumber = Functions.CheckStr(parameters[1].Value);
            return vNumber;
        }

        public static string GetNumberGWP(string strIdSession, string strTransaction, string vMsisdn, ref string vNumber)
        {
            //string vNumber = "";
            DbParameter[] parameters = {
                new  DbParameter("P_MSISDN", DbType.String, ParameterDirection.Input),
                new  DbParameter("P_NUMERO", DbType.String,255, ParameterDirection.Output)
            };

            for (int i = 0; i < parameters.Length; i++)
                parameters[i].Value = DBNull.Value;

            parameters[0].Value = vMsisdn;

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_GWP, DbCommandConfiguration.SIACU_SP_OBTENER_NUMERO_PORT, parameters);
            vNumber = Functions.CheckStr(parameters[1].Value);
            return vNumber;
        }

        //EvaluarMontoAutorizarDCM
        public static bool GetEvaluateAmount_DCM(string strIdSession, string strTransaction, string vListaPerfil, double vMonto, string vUnidad, string vModalidad, string vTipoTelefono)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_LISTA_PERFIL", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("P_MONTO", DbType.Double, ParameterDirection.Input),
                new DbParameter("P_UNIDAD", DbType.String, 255, ParameterDirection.Input, vUnidad),
                new DbParameter("P_MODALIDAD", DbType.String, 255, ParameterDirection.Input, vModalidad),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, 255,ParameterDirection.Input, vTipoTelefono),
                new DbParameter("P_VALIDACION", DbType.String, 255, ParameterDirection.Output)
            };

            string[] arrLista = System.Text.RegularExpressions.Regex.Split(vListaPerfil, ",");
            var perfiles = string.Empty;
            var perfil = string.Empty;
            var resultado = false;

            for (int i = 0; i < arrLista.Length; i++)
            {
                perfil = "*" + arrLista[i] + "*";

                if (perfiles == "")
                    perfiles = perfil;
                else
                    perfiles += "," + perfil;
            }

            parameters[0].Value = perfiles;
            parameters[1].Value = vMonto;

            var objectTemp = DbFactory.ExecuteScalar(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_EVALUAR_MONTO_AUTORIZAR_DCM, parameters);
            var validacion = Functions.CheckStr(parameters[parameters.Length - 1].Value);

            if (validacion == "1")
                resultado = true;

            return resultado;
        }

        //EvaluarMontoAutorizar
        public static bool GetEvaluateAmount(string strIdSession, string strTransaction, string vListaPerfil, double vMonto, string vUnidad, string vModalidad, string vTipoTelefono)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("P_LISTA_PERFIL", DbType.String, 255, ParameterDirection.Input),
                new DbParameter("P_MONTO", DbType.Double, ParameterDirection.Input),
                new DbParameter("P_UNIDAD", DbType.String, 255, ParameterDirection.Input, vUnidad),
                new DbParameter("P_MODALIDAD", DbType.String, 255, ParameterDirection.Input, vModalidad),
                new DbParameter("P_TIPO_TELEFONO", DbType.String, 255,ParameterDirection.Input, vTipoTelefono),
                new DbParameter("P_VALIDACION", DbType.String, 255, ParameterDirection.Output)
            };

            string[] arrLista = System.Text.RegularExpressions.Regex.Split(vListaPerfil, ",");
            var perfiles = string.Empty;
            var perfil = string.Empty;
            var resultado = false;

            for (int i = 0; i < arrLista.Length; i++)
                {
                perfil = "*" + arrLista[i] + "*";

                if (perfiles == "")
                    perfiles = perfil;
                else
                    perfiles += "," + perfil;
            }

            parameters[0].Value = perfiles;
            parameters[1].Value = vMonto;

            var objectTemp = DbFactory.ExecuteScalar(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_EVALUAR_MONTO_AUTORIZAR, parameters);
            var validacion = Functions.CheckStr(parameters[parameters.Length - 1].Value);

            if (validacion == "1")
                resultado = true;

            return resultado;
        }

        public static string UpdateXinter29(string strIdSession, string strTransaction, string P_INTERACT_ID, string P_TEXTO, string P_ORDEN, out string message)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_INTERACT_ID", DbType.String,256, ParameterDirection.Input,P_INTERACT_ID),
                new DbParameter("P_TEXTO", DbType.String,256, ParameterDirection.Input,P_TEXTO),
                new DbParameter("P_ORDEN", DbType.String,256, ParameterDirection.Input,P_ORDEN),
                new DbParameter("P_FLAG_INSERCION", DbType.String,256, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,256, ParameterDirection.Output)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_UPDATE_INTERACT_X_INTER29, parameters);

            string Flag = Convert.ToString(parameters[3].Value);
            message = Convert.ToString(parameters[4].Value);
            return Flag;
        }

        #region External/ Internal Transfer

        public static List<ListItem> GetMzBloEdiTypePVU(string strIdSession, string strTransaction)
        {

            DbParameter[] parameters = new DbParameter[] {

                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTIENE_TIPO_MANZANA, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ListItem>();
                    int intCount = 0;
                    while (reader.Read())
                    {
                        intCount++;
                        string strID = intCount.ToString();
                        listItem.Add(new ListItem()
                        {
                            //idReason = strID,
                            //strnumber = strID,
                            Code = Convert.ToString(reader["ID_TIPO_MANZANA"]),
                            Description = Convert.ToString(reader["TIPO_MANZANA_DESC"]),
                        });

                    }

                });
            }
             catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }

        public static List<ListItem> GetTipDptIntCOBS(string strIdSession, string strTransaction)
        {

            DbParameter[] parameters = new DbParameter[] {

                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTIENE_TIPOS_INTERIOR, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ListItem>();
                    int intCount = 0;
                    while (reader.Read())
                        {
                        intCount++;
                        string strID = intCount.ToString();
                        listItem.Add(new ListItem()
                            {
                            //idReason = strID,
                            //strnumber = strID,
                            Code = Convert.ToString(reader["ID_TIPO_INTERIOR"]),
                            Description = Convert.ToString(reader["DESC_TIPO_INTERIOR"]),
                        });

                    }

                });
            }
             catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
                        }

        public static List<ListItem> GetDepartmentsPVU(string strIdSession, string strTransaction, string vstrDep, string vstrEst)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("k_cod_departamento", DbType.String,100, ParameterDirection.Input,vstrDep),
                 new DbParameter("K_ESTADO", DbType.String,100, ParameterDirection.Input,vstrEst), 
                new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SECSS_CON_DEPARTAMENTO, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ListItem>();
                    int intCount = 0;
                    while (reader.Read())
                    {
                        intCount++;
                        string strID = intCount.ToString();
                        listItem.Add(new ListItem()
                        {
                            //idReason = strID,
                            //strnumber = strID,
                            Code = Convert.ToString(reader["DEPAC_CODIGO"]),
                            Description = Convert.ToString(reader["DEPAV_DESCRIPCION"]),
                        });

                        }

                });
            }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);
                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Info(strIdSession, strIdSession, ex.InnerException.Message);

            }
            return listItem;
                }

        public static List<ListItem> GetProvincesPVU(string strIdSession, string strTransaction, string vstrProv, string vstrDep, string vstrState)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_COD_PROVINCIA", DbType.String,100, ParameterDirection.Input,vstrProv),
                new DbParameter("K_COD_DEPARTAMENTO", DbType.String,100, ParameterDirection.Input,vstrDep ), 
                 new DbParameter("K_ESTADO", DbType.String,100, ParameterDirection.Input, vstrState),
                new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };
           
            List<ListItem> listItem = null;
            try{
                Claro.Web.Logging.Info(strIdSession, strTransaction, "vstrProv: " + vstrProv + "vstrDep: " + vstrDep);
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SECSS_CON_PROVINCIA, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();
                int intCount = 0;
                while (reader.Read())
                {
                    intCount++;
                    string strID = intCount.ToString();
                    listItem.Add(new ListItem()
                {
                        //idReason = strID,
                        //strnumber = strID,
                        Code = Convert.ToString(reader["PROVC_CODIGO"]),
                        Description = Convert.ToString(reader["PROVV_DESCRIPCION"]),
                    });

                }

            });
                 }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

                if (ex.InnerException.Message != null)
                    Claro.Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);

            }  
            return listItem;
                }

        public static List<ListItem> GetDistrictsPVU(string strIdSession, string strTransaction, string vstrDist, string vstrProv, string vstrDep, string vstrState)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_COD_DISTRITO", DbType.String,100, ParameterDirection.Input,vstrDist),
                new DbParameter("K_COD_PROVINCIA", DbType.String,100, ParameterDirection.Input,vstrProv),
                new DbParameter("K_COD_DEPARTAMENTO", DbType.String,100, ParameterDirection.Input,vstrDep),
                new DbParameter("K_ESTADO", DbType.String,100, ParameterDirection.Input,vstrState),
                new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try{

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SECSS_CON_DISTRITO, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();
                int intCount = 0;
                while (reader.Read())
                {
                    intCount++;
                    string strID = intCount.ToString();
                    listItem.Add(new ListItem()
                    {
                        //idReason = strID,
                        //strnumber = strID,
                        Code = Convert.ToString(reader["DISTC_CODIGO"]),
                        Description = Convert.ToString(reader["DISTV_DESCRIPCION"]),
                    });

                    }

            });
             }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

                  if(ex.InnerException.Message!=null)
                      Claro.Web.Logging.Error(strIdSession, strTransaction, ex.InnerException.Message);
            }
            return listItem;
                }
                
        public static string GetCenterPopulatPVU(string strIdSession, string strTransaction, string vstrDep, string vstrPro, string vstrDis, string vstrState)
        {
            
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("K_COD_DISTRITO", DbType.String,100, ParameterDirection.Input,vstrDis),
                new DbParameter("K_COD_PROVINCIA", DbType.String,100, ParameterDirection.Input,vstrPro),
                new DbParameter("K_COD_DEPARTAMENTO", DbType.String,100, ParameterDirection.Input,vstrDep),
                new DbParameter("K_ESTADO", DbType.String,100, ParameterDirection.Input,vstrState), //"A"
                new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)
            };

            string idubigeo = string.Empty;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_SECSS_CON_DISTRITO, parameters, (IDataReader reader) =>
                {

                    while (reader.Read())
                    {
                        idubigeo = Convert.ToString(reader["UBIGEO"]);
                }
                });
             }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return idubigeo;
        }

        public static List<ListItem> GetZoneTypeCOBS(string strIdSession, string strTransaction)
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try{
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_OBTIENE_TIPOS_ZONA, parameters, (IDataReader reader) =>
        {
                listItem = new List<ListItem>();
                int intCount = 0;
                while (reader.Read())
            {
                    intCount++;
                    string strID = intCount.ToString();
                    listItem.Add(new ListItem()
                {
                        Code = Convert.ToString(reader["ID_TIPO_ZONA"]),
                        Description = Convert.ToString(reader["DESC_TIPO_ZONA"])

                    });

                }

            });

               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
                }

        public static List<ListItem> GetBuildingsPVU(string strIdSession, string strTransaction, string vstrCodFlant, string vstrCodBuilding)//??
        {

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("P_COD_PLANO", DbType.String,100, ParameterDirection.Input,vstrCodFlant),
                new DbParameter("P_COD_EDIFICIO", DbType.String,100, ParameterDirection.Input, vstrCodBuilding) //??
                
            };

            List<ListItem> listItem = null;
            try{
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.PVU_MANTSS_LISTA_EDIFICIOHFC, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();
                int intCount = 0;
                while (reader.Read())
                {
                    intCount++;
                    string strID = intCount.ToString();
                    listItem.Add(new ListItem()
                    {
                        Code = Convert.ToString(reader["DEPAV_DESCRIPCION"]),
                        Code2 = Convert.ToString(reader["PROVV_DESCRIPCION"]),
                        //Code3 = Convert.ToString(reader["DISTV_DESCRIPCION"]),
                        Description = Convert.ToString(reader["EDIFV_DIRECCION"]),
                        //Description2 = Convert.ToString(reader["EDIFV_DIRECCION"]),//
                        //Description3 = Convert.ToString(reader["EDIFV_DESCRIPCION"]),
                        //Date = Convert.ToString(reader["EDIFV_CODIGO"])
                    });

            }

            });

               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }

            return listItem;
        }

        public static List<ListItem> GetWorkType(string strIdSession, string strTransaction, int type)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_tipo", DbType.Int64, ParameterDirection.Input, type),
                new DbParameter("srv_tipra", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;
            try{
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_CONSULTA_TIPTRA, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new ListItem()
                    {
                        Code = Convert.ToString(reader["TIPTRA"]),
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                        Code2 = Convert.ToString(reader["FLAG_FRANJA"])
                    });
        }

            });


               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }
        public static List<ListItem> GetWorkSubType(string strIdSession, string strTransaction, string type)
        {
           // <add name="SALES_P_CONSULTA_SUBTIPTRA" procedureName="SALES.PKG_ETADIRECT.OBTIENE_TIPO_ORDEN_TIPTRA" /> - Antes
            //  Validar - GetOrderSubType - Plan Migration
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("vIdtiptra", DbType.String, ParameterDirection.Input, type),
                new DbParameter("cur_tipo", DbType.Object, ParameterDirection.Output)
            };

            List<ListItem> listItem = null;

            try{
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_SUBTIPORD, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new ListItem()
                    {
                        Code = Convert.ToString(reader["VALOR"]),
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                    });
                }
            });

               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }

        public static List<ListItem> GetDocumentTypeCOBS(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_lista", DbType.String,100, ParameterDirection.Input, strCodCargaDdl),
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)
                
            };

            List<ListItem> listItem = null;
            try{
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS, parameters, (IDataReader reader) =>
            {
                listItem = new List<ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new ListItem()
                    {
                        Description = Convert.ToString(reader["DESCRIPCION"]),
                        Code = Convert.ToString(reader["VALOR"])
                    });
                }
            });
               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listItem;
        }


    #endregion

        #region Audit Nueva version y version anterior
        public static SECURITY.GetPagOptionXuserNV.PagOptionXuserNVResponse GetPagOptionXuserVN(SECURITY.GetPagOptionXuserNV.PagOptionXuserNVRequest objPagOptionXuserMRequest)
        {

            SECURITY.GetPagOptionXuserNV.PagOptionXuserNVResponse model = new SECURITY.GetPagOptionXuserNV.PagOptionXuserNVResponse();
            model.ListConsultSecurity = new List<ConsultSecurity>();

            var opcionesPorUsuario = new OPTIONAUDIT.leerPaginaOpcionesPorUsuario();
            var opcionesUsuarioReques = new OPTIONAUDIT.PaginaOpcionesUsuarioRequest();
            var oAuditRequest = new OPTIONAUDIT.AuditRequest();

            oAuditRequest.idTransaccion = objPagOptionXuserMRequest.Audit.Transaction;
            oAuditRequest.ipAplicacion = objPagOptionXuserMRequest.Audit.IPAddress;
            oAuditRequest.aplicacion = objPagOptionXuserMRequest.Audit.ApplicationName;
            oAuditRequest.usrAplicacion = objPagOptionXuserMRequest.Audit.UserName;
            opcionesPorUsuario.audit = oAuditRequest;

            opcionesUsuarioReques.aplicCod = objPagOptionXuserMRequest.IntAplicationCode;
            opcionesUsuarioReques.user = objPagOptionXuserMRequest.IntUser;
            opcionesPorUsuario.PaginaOpcionesUsuarioRequest = opcionesUsuarioReques;

            OPTIONAUDIT.PaginaOpcionType[] objSeg;

            var opcionesPorUsuarioResponse = new OPTIONAUDIT.leerPaginaOpcionesPorUsuarioResponse();

            opcionesPorUsuarioResponse = Claro.Web.Logging.ExecuteMethod(objPagOptionXuserMRequest.Audit.Session, objPagOptionXuserMRequest.Audit.Transaction, () =>
            {
                return WebServiceConfiguration.OpcionesAuditoria.leerPaginaOpcionesPorUsuario(opcionesPorUsuario);

            });

            model.ErrMessage = opcionesPorUsuarioResponse.audit.mensajeRespuesta;
            model.CodeErr = opcionesPorUsuarioResponse.audit.codigoRespuesta;
            objSeg = opcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse != null ? opcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse.listaOpciones : null;

            if (model.CodeErr == Claro.SIACU.Transac.Service.Constants.strUno)
            {
                if (objSeg != null)
                {
                    objSeg.ToList().ForEach(x =>
                    {
                        model.ListConsultSecurity.Add(new ConsultSecurity
                        {
                            Opciccod = x.opcicCod,
                            Opcicabrev = x.clave,
                            Opcicdes = x.opcicDes
                        });
                    });
                }
            }

            return model;
        }

        public static SECURITY.GetPagOptionXuser.PagOptionXuserResponse GetPagOptionXuser(SECURITY.GetPagOptionXuser.PagOptionXuserRequest objPagOptionXuserMRequest)
        {

            SECURITY.GetPagOptionXuser.PagOptionXuserResponse model = new SECURITY.GetPagOptionXuser.PagOptionXuserResponse();
            model.ListConsultSecurity = new List<ConsultSecurity>();

            var opcionesUsuarioReques = new AUDIT.PaginaOpcionesUsuarioRequest();
            var opcionesPorUsuario = new AUDIT.PaginaOpcionesUsuarioResponse();

            opcionesUsuarioReques.aplicCod = objPagOptionXuserMRequest.IntAplicationCode;
            opcionesUsuarioReques.user = objPagOptionXuserMRequest.IntUser;
            AUDIT.PaginaOpcionType[] objSeg;
             
            opcionesPorUsuario = Claro.Web.Logging.ExecuteMethod(() =>
            {
                return WebServiceConfiguration.GRABARAUDIT.leerPaginaOpcionesPorUsuario(opcionesUsuarioReques);

            });

            model.ErrMessage = opcionesPorUsuario.mensaje;
            model.CodeErr = opcionesPorUsuario.resultado;
            objSeg = opcionesPorUsuario.listaOpciones;

            if (model.CodeErr == Claro.SIACU.Transac.Service.Constants.strCero)
            {
                if (objSeg != null)
                {
                    objSeg.ToList().ForEach(x =>
                    {
                        model.ListConsultSecurity.Add(new ConsultSecurity
                        {
                            Opciccod = x.opcicCod,
                            Opcicabrev = x.clave,
                            Opcicdes = x.opcicDes
                        });
                    });
                }
            }

            return model;
        }


        #endregion

        public static User GetUser(string strIdSession, string strTransaction, string strCodeUser, string strCodeRol, string strCodeCac, string strState)
        {


            Claro.Web.Logging.Info("1010101010", "1010101010", "strCodeUser: " + strCodeUser + "strCodeRol: " + strCodeRol + "strCodeCac: " + strCodeCac + "strState: " + strState);


            User userModel = new User();
            try{
            DbParameter[] parameters = {
                new DbParameter("P_CODIGOUSUARIO", DbType.String, ParameterDirection.Input,strCodeUser), 
                new DbParameter("P_CODIGOROL", DbType.Int32,ParameterDirection.Input, strCodeRol), 
                new DbParameter("P_CODIGOCAC", DbType.String, ParameterDirection.Input,strCodeCac), 
                new DbParameter("P_ESTADO", DbType.Int32, ParameterDirection.Input,strState ), 
                new DbParameter("P_RESULTADO", DbType.Object,ParameterDirection.Output)
            };

            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SP_ST_CONSULTAS_USUARIO, parameters,
                (IDataReader dr) =>
                {
                    while (dr.Read())
                    {
                        userModel = new User();
                        userModel.CodeUser = Functions.CheckStr(dr["ID_USUARIO"]);
                        userModel.Name = Functions.CheckStr(dr["USU_NOMBRES"]);
                        userModel.LatName = Functions.CheckStr(dr["USU_APELLIDOS"]);
                        userModel.Dni = Functions.CheckStr(dr["USU_DNI"]);
                        userModel.Password = Functions.CheckStr(dr["USU_PASS"]);
                        userModel.CodeRol = Functions.CheckInt(dr["USU_ROL"]);
                        userModel.Rol = Functions.CheckStr(dr["ROL"]);
                        userModel.CodeState = Functions.CheckInt(dr["USU_ESTADO"]);
                        userModel.State = Functions.CheckStr(dr["Estado"]);
                        userModel.CodeEmpress = Functions.CheckInt(dr["ID_EMPRESA"]);
                        userModel.Empress = Functions.CheckStr(dr["Empresa"]);
                        userModel.CodeCac = Functions.CheckStr(dr["ID_CAC"]);
                        userModel.Cac = Functions.CheckStr(dr["CAC"]);
                        userModel.CodeTypeCac = Functions.CheckInt(dr["ID_TIPOCAC"]);
                        userModel.TypeCac = Functions.CheckStr(dr["TIPOCAC"]);
                    }
                });

               }
              catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return userModel;
        }

        public static bool GetDatBscsExt(string strIdSession, string strTransaction, string strNroTelephone, double dblCodeNewPlan,
                                         ref double dblNroFacture, ref double dblCargCurrentFixed, ref double dblCargFixedNewPlan)
        {
            DbParameter[] parameters =
            {    
                new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,strNroTelephone),
                new DbParameter("p_tmcode_men", DbType.Double,10,ParameterDirection.Input,dblCodeNewPlan),
                new DbParameter("p_num_fact", DbType.Double,10,ParameterDirection.Output,dblNroFacture),
                new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Output,dblCargCurrentFixed),
                new DbParameter("p_cargo_fijo_men", DbType.Double,10,ParameterDirection.Output,dblCargFixedNewPlan),
            };
            try
            {

                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_POST_COBS_SSSIGA_OBTENER_DATOS_BSCS_EXT, parameters);

                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
                return false;
            }

            dblNroFacture = Convert.ToDouble(parameters[2].Value.ToString());
            dblCargCurrentFixed = Convert.ToDouble(parameters[3].Value.ToString());
            dblCargFixedNewPlan = Convert.ToDouble(parameters[4].Value.ToString());

            return true;
        }

        public static bool GetPenaltyExt(string strIdSession, string strTransaction, string strNroTelephone, 
                                                DateTime dtDatePenalty,double dblNroFacture, double dblCargFixedCurrent, 
                                                double dblCargFixedNewPlan,double dblDayXMonth, double dblCodeNewPlan,
                                                ref double dblAgreementIdExit, ref double dblDayPendient, ref double dblCargFiexdDiar, 
                                                ref double dblPrecList, ref double dblPrecVent, ref double dblPenaltyPcs, ref double dblPenaltyApadece)
        {

            DbParameter[] parameters =
            {    
                new DbParameter("p_acuerdo_id", DbType.Double,10,ParameterDirection.Input,"0"),
                new DbParameter("p_nro_telefono", DbType.String,50,ParameterDirection.Input,strNroTelephone),
                new DbParameter("p_fecha_penalidad", DbType.DateTime,10,ParameterDirection.Input,dtDatePenalty),
                new DbParameter("p_numero_facturas", DbType.Double,10,ParameterDirection.Input,dblNroFacture),
                new DbParameter("p_cargo_fijo_act", DbType.Double,10,ParameterDirection.Input,dblCargFixedCurrent),
                new DbParameter("p_cargo_fijo_inf", DbType.Double,10,ParameterDirection.Input,dblCargFixedNewPlan),
                new DbParameter("p_diasxmes", DbType.Double,10,ParameterDirection.Input,dblDayXMonth),
                new DbParameter("p_codigo_plan_nuevo", DbType.Double,10,ParameterDirection.Input,dblCodeNewPlan),
                new DbParameter("p_acuerdo_id_salida", DbType.Double,22,ParameterDirection.Output,dblAgreementIdExit),
                new DbParameter("p_dias_pendientes", DbType.Double,22,ParameterDirection.Output,dblDayPendient),
                new DbParameter("p_cargo_fijo_diario", DbType.Double,22,ParameterDirection.Output,dblCargFiexdDiar),
                new DbParameter("p_precio_lista", DbType.Double,22,ParameterDirection.Output,dblPrecList),
                new DbParameter("p_precio_venta", DbType.Double,22,ParameterDirection.Output,dblPrecVent),
                new DbParameter("p_monto_pcs", DbType.Double,10,ParameterDirection.Output,dblPenaltyPcs),
                new DbParameter("p_monto_apadece", DbType.Double,10,ParameterDirection.Output,dblPenaltyApadece),

                
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SIGA,
                        DbCommandConfiguration.SIACU_POST_SIGA_SSSIGA_OBTENER_PENALIDAD_EXT, parameters);


                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession.ToString(), strTransaction, ex.Message);
                return false;
            }
            dblAgreementIdExit = (parameters[8].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[8].Value.ToString()));
            dblDayPendient = (parameters[9].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));
            dblCargFiexdDiar = (parameters[10].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));
            dblPrecList = (parameters[11].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));
            dblPrecVent = (parameters[12].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));
            dblPenaltyPcs = (parameters[13].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));
            dblPenaltyApadece = (parameters[14].Value.ToString() == "null" ? 0.00 : Convert.ToDouble(parameters[9].Value.ToString()));

            return true;

        }



        public static SECURITY.GetEmployerDate.GetEmployerDateResponse GetEmployerDate(SECURITY.GetEmployerDate.GetEmployerDateRequest objDatosEmpleadoRequest)
        {


            SECURITY.GetEmployerDate.GetEmployerDateResponse objEmpleado = new SECURITY.GetEmployerDate.GetEmployerDateResponse();


            var opcionesUsuarioReques = new AUDIT.DatosEmpleadoRequest();
            var objResponseAuditoria = new AUDIT.EmpleadoResponse();

            objResponseAuditoria = Claro.Web.Logging.ExecuteMethod<AUDIT.EmpleadoResponse>(objDatosEmpleadoRequest.Audit.Session, objDatosEmpleadoRequest.Audit.Transaction, () =>
            {
                return Configuration.WebServiceConfiguration.GRABARAUDIT.leerDatosEmpleado(opcionesUsuarioReques);

            });

            if (objResponseAuditoria.resultado.estado.Equals("1"))
            {
                objEmpleado.strCode = objResponseAuditoria.empleados.item[0].codigo;
                objEmpleado.strCodeArea = objResponseAuditoria.empleados.item[0].codigoArea;
                objEmpleado.strCodeCargo = objResponseAuditoria.empleados.item[0].codigoCargo;
                objEmpleado.strDesAddress = objResponseAuditoria.empleados.item[0].codigoDireccion;
                objEmpleado.strDesBoss = objResponseAuditoria.empleados.item[0].codigoJefe;
                objEmpleado.strEmail = objResponseAuditoria.empleados.item[0].correo;
                objEmpleado.strDesArea = objResponseAuditoria.empleados.item[0].descripcionArea;
                objEmpleado.strDesCargo = objResponseAuditoria.empleados.item[0].descripcionCargo;
                objEmpleado.strDesAddress = objResponseAuditoria.empleados.item[0].descripcionDireccion;
                objEmpleado.strDesBoss = objResponseAuditoria.empleados.item[0].descripcionJefe;
                objEmpleado.strlogin = objResponseAuditoria.empleados.item[0].login;
                objEmpleado.strNomb = objResponseAuditoria.empleados.item[0].nombres;
                objEmpleado.strApPat = objResponseAuditoria.empleados.item[0].paterno;
                objEmpleado.strApMat = objResponseAuditoria.empleados.item[0].materno;
            }
            else
            {
                objEmpleado = null;
            }

            return objEmpleado;
        }


        public static bool InsertBusinessInteractionFixed(string strSesion, string strTransaction, Iteraction objItem, ref string strInteractionId, ref string strFlagInsertion, ref string strMessage)
        {
            DbParameter[] parameters = new DbParameter[] 
            {
                                new DbParameter("P_CONTACTOBJID_1", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_SITEOBJID_1", DbType.Int64,ParameterDirection.Input), 
								new DbParameter("P_ACCOUNT", DbType.String,255,ParameterDirection.Input), 
                                new DbParameter("P_PHONE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_TIPO", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_CLASE", DbType.String,255,ParameterDirection.Input),
								new DbParameter("P_SUBCLASE", DbType.String,255,ParameterDirection.Input),												   
								new DbParameter("P_METODO_CONTACTO", DbType.String,255,ParameterDirection.Input),													   
								new DbParameter("P_TIPO_INTER", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_AGENTE", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_USR_PROCESO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_HECHO_EN_UNO", DbType.Int64,ParameterDirection.Input),
                                new DbParameter("P_NOTAS", DbType.String,4000,ParameterDirection.Input),
                                new DbParameter("P_FLAG_CASO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("P_RESULTADO", DbType.String,255,ParameterDirection.Input),
                                new DbParameter("ID_INTERACCION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("FLAG_CREACION", DbType.String,255,ParameterDirection.Output),
                                new DbParameter("MSG_TEXT", DbType.String,255,ParameterDirection.Output)
            };

            for (int j = 0; j < parameters.Length; j++)
            {
                parameters[j].Value = System.DBNull.Value;
            }
            parameters[0].Value = objItem.OBJID_CONTACTO;
            parameters[1].Value = objItem.OBJID_SITE;
            parameters[2].Value = objItem.CUENTA;
            parameters[3].Value = objItem.TELEFONO;
            parameters[4].Value = objItem.TIPO;
            parameters[5].Value = objItem.CLASE;
            parameters[6].Value = objItem.SUBCLASE;
            parameters[7].Value = objItem.METODO;
            parameters[8].Value = objItem.TIPO_INTER;
            parameters[9].Value = objItem.AGENTE;
            parameters[10].Value = objItem.USUARIO_PROCESO;
            parameters[11].Value = objItem.HECHO_EN_UNO;
            parameters[12].Value = objItem.NOTAS;
            parameters[13].Value = objItem.FLAG_CASO;
            parameters[14].Value = objItem.RESULTADO;

            int result =
            DbFactory.ExecuteNonQuery(strSesion, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_CREATE_INTERACT, parameters);

            strInteractionId = parameters[15].Value.ToString() == "null" ? string.Empty : parameters[15].Value.ToString();
            strFlagInsertion = parameters[16].Value.ToString();
            strMessage = parameters[17].Value.ToString();

            if (String.IsNullOrEmpty(strMessage)) strMessage = "OK";

            return (strFlagInsertion == CONSTANTS.Message_OK);
        }

        public static List<ConsultIGV> GetConsultIGV(string strIdSession, string strIdTransaction, string strAppID, string strAppName, string strAppUser)
        {
            List<ConsultIGV> listConsultIGV = null;
            ConsultIGV oConsultIGV = null;
            COMMON_IGV.consultarIGVRequest objConsultarIGVRequest = new COMMON_IGV.consultarIGVRequest();
            COMMON_IGV.AuditRequestType audi = new COMMON_IGV.AuditRequestType();
            audi.idTransaccion = strIdTransaction;
            audi.ipAplicacion = strAppID;
            audi.nombreAplicacion = strAppName;
            audi.usuarioAplicacion = strAppUser;

            objConsultarIGVRequest.auditoria = audi;

            var lista = Web.Logging.ExecuteMethod(strIdSession, strIdTransaction, ServiceConfiguration.SIACUConsultaIGV, () => {
                return Configuration.ServiceConfiguration.SIACUConsultaIGV.consultarIGV(objConsultarIGVRequest);
            });

            if (lista != null && lista.listaIGVS != null)
            {
                if (lista.listaIGVS.Length > 0)
                {
                    listConsultIGV = new List<ConsultIGV>();
                    foreach (var item in lista.listaIGVS)
                    {
                        oConsultIGV = new ConsultIGV();
                        oConsultIGV.igv = item.igv;
                        oConsultIGV.igvD = item.igvD;
                        oConsultIGV.impudFecFinVigencia = item.impudFecFinVigencia;
                        oConsultIGV.impudFecIniVigencia = item.impudFecIniVigencia;
                        oConsultIGV.impudFecRegistro = item.impudFecRegistro;
                        oConsultIGV.impunTipDoc = item.impunTipDoc;
                        oConsultIGV.imputId = item.imputId;
                        oConsultIGV.impuvDes = item.impuvDes;
                        listConsultIGV.Add(oConsultIGV);
                    }
                }
            }

            return listConsultIGV;
        }
        public static Claro.SIACU.Entity.Transac.Service.Common.GetSendEmailSB.SendEmailSBResponse GetSendEmailSB(
                                                      string strIdSession, string strIdTransaction, string strAppID, string strAppCode, string strAppUser,
                                                      string strRemitente, string strDestinatario, string strAsunto, string strMensaje, string strHTMLFlag,
                                                      byte[] Archivo,string strNomFile)
        {
            Claro.SIACU.Entity.Transac.Service.Common.GetSendEmailSB.SendEmailSBResponse objSendEmailSBResponse = new Claro.SIACU.Entity.Transac.Service.Common.GetSendEmailSB.SendEmailSBResponse();
            COMMON_ENVIOCORREOSB.AuditTypeRequest audi = new COMMON_ENVIOCORREOSB.AuditTypeRequest();
            audi.idTransaccion = strIdTransaction;
            audi.codigoAplicacion = strAppCode;
            audi.ipAplicacion = strAppID;
            audi.usrAplicacion = strAppUser;

            COMMON_ENVIOCORREOSB.ArchivoAdjunto[] listArchivoAdjunto = new COMMON_ENVIOCORREOSB.ArchivoAdjunto[1];
            COMMON_ENVIOCORREOSB.ArchivoAdjunto oArchivoAdjunto = new COMMON_ENVIOCORREOSB.ArchivoAdjunto();
            oArchivoAdjunto.nombre = strNomFile;
            oArchivoAdjunto.cabecera = strNomFile;
            oArchivoAdjunto.archivo = Archivo;
            listArchivoAdjunto[0] = oArchivoAdjunto;

            COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[] listParametroObcionalRequest = new COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[1];
            COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[] listParametroObcionalResponse = new COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType[1];
            COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType oParametroOpcionalComplexType = new COMMON_ENVIOCORREOSB.ParametroOpcionalComplexType();
            oParametroOpcionalComplexType.clave = string.Empty;
            oParametroOpcionalComplexType.valor = string.Empty;
            listParametroObcionalRequest[0] = oParametroOpcionalComplexType;
            listParametroObcionalResponse[0] = oParametroOpcionalComplexType;

            COMMON_ENVIOCORREOSB.AuditTypeResponse auditResponse = new COMMON_ENVIOCORREOSB.AuditTypeResponse();

            auditResponse = Web.Logging.ExecuteMethod(strIdSession, strIdTransaction, WebServiceConfiguration.SIACUEnvioCorreoSB, () =>
            {
                return WebServiceConfiguration.SIACUEnvioCorreoSB.enviarCorreo(
                    audi,
                    strRemitente,
                    strDestinatario,
                    strAsunto,
                    strMensaje,
                    strHTMLFlag,
                    listArchivoAdjunto,
                    listParametroObcionalRequest,
                    out listParametroObcionalResponse
                    );
            });

            if (auditResponse != null)
            {
                objSendEmailSBResponse.idTransaccion = auditResponse.idTransaccion;
                objSendEmailSBResponse.codigoRespuesta = auditResponse.codigoRespuesta;
                objSendEmailSBResponse.mensajeRespuesta = auditResponse.mensajeRespuesta;
            }

            return objSendEmailSBResponse;
        }

        public static ContractByPhoneNumberResponse GetContractByPhoneNumber(ContractByPhoneNumberRequest objRequest)
        {
            ContractByPhoneNumberResponse objResponse = new ContractByPhoneNumberResponse();


            List<ContractByPhoneNumber> ListContractByPhoneNumber = new List<ContractByPhoneNumber>();

            contratosTelefonoRequest objContractRequest = new contratosTelefonoRequest();
            contratosTelefonoResponse objContractResponse = new contratosTelefonoResponse();

            objContractRequest.login = objRequest.User;
            objContractRequest.msisdn = objRequest.PhoneNumber;
            objContractRequest.sistema = objRequest.System;

            objContractResponse = Claro.Web.Logging.ExecuteMethod<contratosTelefonoResponse>(objRequest.Audit.Session,
                objRequest.Audit.Transaction,
                () =>
                {
                    return  Configuration.WebServiceConfiguration.ActDesactServiciosComerciales.contratosTelefono(objContractRequest);
                });

            objResponse.Result = objContractResponse.errNum;
            objResponse.Message = objContractResponse.errMsj;

            if (Functions.CheckInt(objResponse.Result) == CONSTANTS.zero)
            {
                int intQuantity = objContractResponse.contrato.Length;
                for (int i = 0; i < intQuantity; i++)
                {
                    ContractByPhoneNumber obj = new ContractByPhoneNumber();
                    obj.CustCod = objContractResponse.contrato[i].custCode;
                    obj.Name = objContractResponse.contrato[i].nombre;
                    obj.CodId = objContractResponse.contrato[i].coId;
                    obj.State = objContractResponse.contrato[i].estado;
                    obj.Date = objContractResponse.contrato[i].fecha;
                    obj.Reason = objContractResponse.contrato[i].razon;
                    ListContractByPhoneNumber.Add(obj);
                     
                }
            }
            objResponse.ListContByPhone = ListContractByPhoneNumber;
            return objResponse;
        }

        public static string GetNoteInteraction(string strIdSession, string strTransaction, string vInteraccionId, ref string rFlagInsercion, ref string rMsgText)
        {
            string strNotas = string.Empty;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_IDINTERACT", DbType.String,255, ParameterDirection.Input, vInteraccionId),
                new DbParameter("FLAG_CREACION", DbType.String,255, ParameterDirection.Output),
                new DbParameter("MSG_TEXT", DbType.String,255, ParameterDirection.Output),
                new DbParameter("NOTES_INTERACT", DbType.Object, ParameterDirection.Output)
            };
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_NOTES_INTERACT, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        strNotas = Convert.ToString(reader["NOTAS"]);
                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return strNotas;
        }

        public static CaseTemplate GetDynamicCaseTemplateData(string strIdSession, string strTransaction, string vCasoID, ref string vFLAG_CONSULTA, ref string vMSG_TEXT)
        {
            CaseTemplate objCaseTemplate = null;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_NRO_CASO", DbType.String,255, ParameterDirection.Input, vCasoID),
                new DbParameter("P_FLAG_CONSULTA", DbType.String,255, ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String,255, ParameterDirection.Output),
                new DbParameter("OUT_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_QUERY_PLUS_CASE, parameters, (IDataReader reader) =>
                {
                    while (reader.Read())
                    {
                        objCaseTemplate = new CaseTemplate();

                        objCaseTemplate.ID_CASO = vCasoID;
                        objCaseTemplate.X_CAS_1 = Functions.CheckStr(reader["X_CAS_1"]);
                        objCaseTemplate.X_CAS_2 = Functions.CheckStr(reader["X_CAS_2"]);
                        objCaseTemplate.X_CAS_3 = Functions.CheckStr(reader["X_CAS_3"]);
                        objCaseTemplate.X_CAS_4 = Functions.CheckStr(reader["X_CAS_4"]);
                        objCaseTemplate.X_CAS_5 = Functions.CheckStr(reader["X_CAS_5"]);
                        objCaseTemplate.X_CAS_6 = Functions.CheckStr(reader["X_CAS_6"]);
                        objCaseTemplate.X_CAS_7 = Functions.CheckStr(reader["X_CAS_7"]);
                        objCaseTemplate.X_CAS_8 = Functions.CheckDbl(reader["X_CAS_8"]);
                        objCaseTemplate.X_CAS_9 = Functions.CheckDbl(reader["X_CAS_9"]);
                        objCaseTemplate.X_CAS_10 = Functions.CheckDbl(reader["X_CAS_10"]);
                        objCaseTemplate.X_CAS_11 = Functions.CheckDbl(reader["X_CAS_11"]);
                        objCaseTemplate.X_CAS_12 = Functions.CheckDbl(reader["X_CAS_12"]);
                        objCaseTemplate.X_CAS_13 = Functions.CheckDbl(reader["X_CAS_13"]);
                        objCaseTemplate.X_CAS_14 = Functions.CheckDbl(reader["X_CAS_14"]);
                        objCaseTemplate.X_CAS_15 = Functions.CheckStr(reader["X_CAS_15"]);
                        objCaseTemplate.X_CAS_16 = Functions.CheckStr(reader["X_CAS_16"]);
                        objCaseTemplate.X_CAS_17 = Functions.CheckStr(reader["X_CAS_17"]);
                        objCaseTemplate.X_CAS_18 = Functions.CheckStr(reader["X_CAS_18"]);
                        objCaseTemplate.X_CAS_19 = Functions.CheckStr(reader["X_CAS_19"]);
                        objCaseTemplate.X_CAS_20 = Functions.CheckStr(reader["X_CAS_20"]);
                        objCaseTemplate.X_CAS_21 = Functions.CheckStr(reader["X_CAS_21"]);
                        objCaseTemplate.X_CAS_22 = Functions.CheckDbl(reader["X_CAS_22"]);
                        objCaseTemplate.X_CAS_23 = Functions.CheckDbl(reader["X_CAS_23"]);
                        objCaseTemplate.X_CAS_24 = Functions.CheckDbl(reader["X_CAS_24"]);
                        objCaseTemplate.X_CAS_25 = Functions.CheckDbl(reader["X_CAS_25"]);
                        objCaseTemplate.X_CAS_26 = Functions.CheckDbl(reader["X_CAS_26"]);
                        objCaseTemplate.X_CAS_27 = Functions.CheckDbl(reader["X_CAS_27"]);
                        objCaseTemplate.X_CAS_28 = Functions.CheckDbl(reader["X_CAS_28"]);
                        objCaseTemplate.X_CAS_29 = Functions.CheckStr(reader["X_CAS_29"]);
                        objCaseTemplate.X_CAS_30 = Functions.CheckStr(reader["X_CAS_30"]);
                        if (reader["X_SUSPENSION_DATE"] != null && reader["X_SUSPENSION_DATE"] != DBNull.Value)
                            objCaseTemplate.X_SUSPENSION_DATE = Functions.CheckDate(reader["X_SUSPENSION_DATE"]);
                        if (reader["X_REACTIVATION_DATE"] != null && reader["X_REACTIVATION_DATE"] != DBNull.Value)
                            objCaseTemplate.X_REACTIVATION_DATE = Functions.CheckDate(reader["X_REACTIVATION_DATE"]);
                        objCaseTemplate.X_SUSPENSION_QT = Functions.CheckDbl(reader["X_SUSPENSION_QT"]);
                        objCaseTemplate.X_CONCLUSIONS = Functions.CheckStr(reader["X_CONCLUSIONS"]);
                        objCaseTemplate.X_TEST_MADE = Functions.CheckStr(reader["X_TEST_MADE"]);
                        objCaseTemplate.X_PROBLEM_DESCRIPTION = Functions.CheckStr(reader["X_PROBLEM_DESCRIPTION"]);
                        objCaseTemplate.X_ADDRESS = Functions.CheckStr(reader["X_ADDRESS"]);
                        objCaseTemplate.X_DOCUMENT_NUMBER = Functions.CheckStr(reader["X_DOCUMENT_NUMBER"]);
                        objCaseTemplate.X_CALL_DURATION = Functions.CheckStr(reader["X_CALL_DURATION"]);
                        objCaseTemplate.X_CALL_COST = Functions.CheckDbl(reader["X_CALL_COST"]);
                        objCaseTemplate.X_SYSTEM_STATUS = Functions.CheckStr(reader["X_SYSTEM_STATUS"]);
                        objCaseTemplate.X_FLAG_VARIATION = Functions.CheckStr(reader["X_FLAG_VARIATION"]);

                        if (reader["X_SEARCH_DATE"] != null && reader["X_SEARCH_DATE"] != DBNull.Value)
                            objCaseTemplate.X_SEARCH_DATE = Functions.CheckDate(reader["X_SEARCH_DATE"]);

                        if (reader["X_VARIATION_DATE"] != null && reader["X_VARIATION_DATE"] != DBNull.Value)
                            objCaseTemplate.X_VARIATION_DATE = Functions.CheckDate(reader["X_VARIATION_DATE"]);

                        if (reader["X_LAST_QUERY"] != null && reader["X_LAST_QUERY"] != System.DBNull.Value)
                            objCaseTemplate.X_LAST_QUERY = Functions.CheckDate(reader["X_LAST_QUERY"]);

                        if (reader["X_PROBLEM_DATE"] != null && reader["X_PROBLEM_DATE"] != System.DBNull.Value)
                            objCaseTemplate.X_PROBLEM_DATE = Functions.CheckDate(reader["X_PROBLEM_DATE"]);

                        if (reader["X_PURCHASE_DATE"] != null && reader["X_PURCHASE_DATE"] != System.DBNull.Value)
                            objCaseTemplate.X_PURCHASE_DATE = Functions.CheckDate(reader["X_PURCHASE_DATE"]);

                        if (reader["X_RECHARGE_DATE"] != null && reader["X_RECHARGE_DATE"] != System.DBNull.Value)
                            objCaseTemplate.X_RECHARGE_DATE = Functions.CheckDate(reader["X_RECHARGE_DATE"]);

                        if (reader["X_REQUEST_DATE"] != null && reader["X_REQUEST_DATE"] != System.DBNull.Value)
                            objCaseTemplate.X_REQUEST_DATE = Functions.CheckDate(reader["X_REQUEST_DATE"]);

                        objCaseTemplate.X_REQUEST_PLACE = Functions.CheckStr(reader["X_REQUEST_PLACE"]);
                        objCaseTemplate.X_FLAG_GPRS = Functions.CheckStr(reader["X_FLAG_GPRS"]);
                        objCaseTemplate.X_COMPLAINT_AMOUNT = Functions.CheckDbl(reader["X_COMPLAINT_AMOUNT"]);
                        objCaseTemplate.X_LINES = Functions.CheckStr(reader["X_LINES"]);
                        objCaseTemplate.X_ERROR_MESSAGE = Functions.CheckStr(reader["X_ERROR_MESSAGE"]);
                        objCaseTemplate.X_MODEL = Functions.CheckStr(reader["X_MODEL"]);
                        objCaseTemplate.X_MARK = Functions.CheckStr(reader["X_MARK"]);
                        objCaseTemplate.X_BAND = Functions.CheckStr(reader["X_BAND"]);
                        objCaseTemplate.X_REPOSITION_REASON = Functions.CheckStr(reader["X_REPOSITION_REASON"]);
                        objCaseTemplate.X_CELLULAR_NUMBER = Functions.CheckStr(reader["X_CELLULAR_NUMBER"]);
                        objCaseTemplate.X_IDCLARIFY_VARIATION = Functions.CheckStr(reader["X_IDCLARIFY_VARIATION"]);
                        objCaseTemplate.X_FLAG_SEND_RECEIVE = Functions.CheckStr(reader["X_FLAG_SEND_RECEIVE"]);
                        objCaseTemplate.X_CUSTOMER_NAME = Functions.CheckStr(reader["X_CUSTOMER_NAME"]);
                        objCaseTemplate.X_PREPAID_CARD_NUMBER = Functions.CheckStr(reader["X_PREPAID_CARD_NUMBER"]);
                        objCaseTemplate.X_NUMBERS_COMMUNICATION = Functions.CheckStr(reader["X_NUMBERS_COMMUNICATION"]);
                        objCaseTemplate.X_REFERENCE_NUMBER = Functions.CheckStr(reader["X_REFERENCE_NUMBER"]);
                        objCaseTemplate.X_OST_NUMBER = Functions.CheckStr(reader["X_OST_NUMBER"]);
                        objCaseTemplate.X_FRIENDS_FAMILY = Functions.CheckStr(reader["X_FRIENDS_FAMILY"]);
                        objCaseTemplate.X_COUNTRY_OPERATOR = Functions.CheckStr(reader["X_COUNTRY_OPERATOR"]);
                        objCaseTemplate.X_OLD_PLAN = Functions.CheckStr(reader["X_OLD_PLAN"]);
                        objCaseTemplate.X_NEW_PLAN = Functions.CheckStr(reader["X_NEW_PLAN"]);
                        objCaseTemplate.X_CURRENT_PLAN = Functions.CheckStr(reader["X_CURRENT_PLAN"]);
                        objCaseTemplate.X_CAMPAIGN = Functions.CheckStr(reader["X_CAMPAIGN"]);
                        objCaseTemplate.X_BILL_NUMBER_COMPLAINT = Functions.CheckStr(reader["X_BILL_NUMBER_COMPLAINT"]);
                        objCaseTemplate.X_REFERENCE_ADDRESS = Functions.CheckStr(reader["X_REFERENCE_ADDRESS"]);
                        objCaseTemplate.X_CURRENT_BALANCE = Functions.CheckDbl(reader["X_CURRENT_BALANCE"]);
                        objCaseTemplate.X_LAST_BALANCE = Functions.CheckDbl(reader["X_LAST_BALANCE"]);
                        objCaseTemplate.X_BALANCE_REQUESTED = Functions.CheckDbl(reader["X_BALANCE_REQUESTED"]);
                        objCaseTemplate.X_CUSTOMER_SEGMENT = Functions.CheckStr(reader["X_CUSTOMER_SEGMENT"]);
                        objCaseTemplate.X_SERVICE_PROBLEM = Functions.CheckStr(reader["X_SERVICE_PROBLEM"]);
                        objCaseTemplate.X_FLAG_OTHER_PROBLEMS = Functions.CheckStr(reader["X_FLAG_OTHER_PROBLEMS"]);
                        objCaseTemplate.X_OPERATOR_PROBLEM = Functions.CheckStr(reader["X_OPERATOR_PROBLEM"]);
                        objCaseTemplate.X_CONTACT_TIME_TERM = Functions.CheckStr(reader["X_CONTACT_TIME_TERM"]);
                        objCaseTemplate.X_STORE = Functions.CheckStr(reader["X_STORE"]);
                        objCaseTemplate.X_DIAL_TYPE = Functions.CheckStr(reader["X_DIAL_TYPE"]);
                        objCaseTemplate.X_PROBLEM_LOCATION = Functions.CheckStr(reader["X_PROBLEM_LOCATION"]);
                        objCaseTemplate.X_FLAG_ADDITIONAL_SERVICES = Functions.CheckStr(reader["X_FLAG_ADDITIONAL_SERVICES"]);
                        objCaseTemplate.X_FLAG_WAP = Functions.CheckStr(reader["X_FLAG_WAP"]);
                        objCaseTemplate.X_DEACTIVATION_REASON = Functions.CheckStr(reader["X_DEACTIVATION_REASON"]);
                        objCaseTemplate.X_CLARO_LDN1 = Functions.CheckStr(reader["X_CLARO_LDN1"]);
                        objCaseTemplate.X_CLARO_LDN2 = Functions.CheckStr(reader["X_CLARO_LDN2"]);
                        objCaseTemplate.X_CLARO_LDN3 = Functions.CheckStr(reader["X_CLARO_LDN3"]);
                        objCaseTemplate.X_CLARO_LDN4 = Functions.CheckStr(reader["X_CLARO_LDN4"]);
                        objCaseTemplate.X_CLAROLOCAL1 = Functions.CheckStr(reader["X_CLAROLOCAL1"]);
                        objCaseTemplate.X_CLAROLOCAL2 = Functions.CheckStr(reader["X_CLAROLOCAL2"]);
                        objCaseTemplate.X_CLAROLOCAL3 = Functions.CheckStr(reader["X_CLAROLOCAL3"]);
                        objCaseTemplate.X_CLAROLOCAL4 = Functions.CheckStr(reader["X_CLAROLOCAL4"]);
                        objCaseTemplate.X_CLAROLOCAL5 = Functions.CheckStr(reader["X_CLAROLOCAL5"]);
                        objCaseTemplate.X_CLAROLOCAL6 = Functions.CheckStr(reader["X_CLAROLOCAL6"]);
                        objCaseTemplate.X_FIXED_NUMBER = Functions.CheckStr(reader["X_FIXED_NUMBER"]);
                        objCaseTemplate.X_LDI_NUMBER = Functions.CheckStr(reader["X_LDI_NUMBER"]);

                        #region MyRegion
                        //if (reader["X_SUSPENSION_DATE"] != null && reader["X_SUSPENSION_DATE"] != DBNull.Value)
                        //    objCaseTemplate.X_SUSPENSION_DATE = Convert.ToDate(reader["X_SUSPENSION_DATE"]);

                        //objCaseTemplate.X_CAS_1 = Convert.ToString(reader["X_CAS_1"]);
                        //objCaseTemplate.X_OPERATOR_PROBLEM = Convert.ToString(reader["X_OPERATOR_PROBLEM"]);
                        //objCaseTemplate.X_CAS_16 = Convert.ToString(reader["X_CAS_16"]);
                        //objCaseTemplate.X_CAS_15 = Convert.ToString(reader["X_CAS_15"]);
                        #endregion

                    }
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }

            return objCaseTemplate;
        }

        public static RegisterServiceCommercialResponse GetRegisterServiceCommercial(RegisterServiceCommercialRequest objRequest)
        {
            RegisterServiceCommercialResponse objResponse = new RegisterServiceCommercialResponse();

            
            registraServicioComercialRequest objServiceRequest = new registraServicioComercialRequest();
            objServiceRequest.login = objRequest.StrUser;
            objServiceRequest.sistema = objRequest.StrSystem;
            objServiceRequest.idTransaccion = objRequest.StrIdTransaction;
            objServiceRequest.coId = objRequest.StrCoId;
            objServiceRequest.coSer = objRequest.StrCodServ;
            objServiceRequest.tipReg = objRequest.StrTypeActivation;

            registraServicioComercialResponse objServiceResponse = new registraServicioComercialResponse();
            objServiceResponse = Claro.Web.Logging.ExecuteMethod<registraServicioComercialResponse>(
            objRequest.Audit.Session, objRequest.Audit.Transaction,
            () =>
            {
                return Configuration.WebServiceConfiguration.ActDesactServiciosComerciales
                    .registraServicioComercial(objServiceRequest);
            });

            objResponse.StrMessage = objServiceResponse.errMsj;
            if (objResponse.StrMessage == "")
            {
                objResponse.StrMessage = "No hay Errores";
            }
            objResponse.StrResult = objServiceResponse.errNum;

            return objResponse;
        }

        public static bool UpdatexInter30(string strIdSession, string strTransaction, string p_objid, string p_texto, ref string rFlagInsercion, ref string rMsgText)
        {
            var salida = false;
            DbParameter[] parameters = {
                new DbParameter("P_INTERACT_ID", DbType. Int64,ParameterDirection.Input),
                new DbParameter("P_TEXTO", DbType.String, 1000,ParameterDirection.Input),
                new DbParameter("P_FLAG_INSERCION", DbType.String, 255,ParameterDirection.Output),
                new DbParameter("P_MSG_TEXT", DbType.String, 255,ParameterDirection.Output)
            };

            foreach (var t in parameters)
                t.Value = DBNull.Value;

            var i = 0;
            if (!string.IsNullOrEmpty(p_objid))
                parameters[i].Value = Convert.ToInt64(p_objid);

            if (!string.IsNullOrEmpty(p_texto))
                parameters[1].Value = p_texto;
            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_POST_CLARIFY_SP_UPDATE_X_INTER_30, parameters);
                    salida = true;
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rFlagInsercion = parameters[parameters.Length - 2].Value.ToString();
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
            }
            return salida;
        }

        public static List<EntitiesFixed.GenericItem> GetProgramTask(string strIdSession, string strTransaction, string strIdLista)
        {
            var salida = new List<EntitiesFixed.GenericItem>();
            var cantidadRegistros = 0;
            DbParameter[] parameters = 
            {
                new DbParameter("P_ID_LISTA", DbType.String, 255, ParameterDirection.Input, strIdLista),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)										   
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB,
                        DbCommandConfiguration.SIACU_SP_OBTIENE_LISTAS_COBSDB, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                cantidadRegistros++;
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Codigo = Functions.CheckStr(reader["VALOR"]),
                                    Descripcion = Functions.CheckStr(reader["DESCRIPCION"]),
                                };
                                salida.Add(item);
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                Web.Logging.Info(strIdSession, strTransaction, "Parametros de Salida-> Cantidad Registros: " + cantidadRegistros);
            }

            return salida;
        }

        public static List<EntitiesFixed.GenericItem> GetTypeTransaction(string strIdSession, string strTransaction)
        {
            var salida = new List<EntitiesFixed.GenericItem>();
            var cantidadRegistros = 0;
            DbParameter[] parameters = 
            {
                new DbParameter("p_cursor", DbType.Object, ParameterDirection.Output)										   
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_EAIAVM,
                        DbCommandConfiguration.SIACU_SP_CONSULTA_OBT_SERVICIO_FIJA, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                cantidadRegistros++;
                                var item = new EntitiesFixed.GenericItem
                                {
                                    Codigo = Functions.CheckStr(reader["CODIGO"]),
                                    Descripcion = Functions.CheckStr(reader["DESCRIPCION"]),
                                };
                                salida.Add(item);
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                Web.Logging.Info(strIdSession, strTransaction, "Parametros de Salida-> Cantidad Registros: " + cantidadRegistros);
            }

            return salida;
        }
        #region JCAA
        public static ETAFlow ETAFlowValidate(string strIdSession, string strTransaction, string as_origen, string av_idplano, string av_ubigeo, int an_tiptra,
                             string an_tipsrv)
        {
            ETAFlow oEtaFlow;
            string as_codzona = "";
            int an_indica = 1;
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("as_origen", DbType.String, ParameterDirection.Input,as_origen),
                new DbParameter("av_idplano", DbType.String,255, ParameterDirection.Input, av_idplano),
                new DbParameter("av_ubigeo", DbType.String,255, ParameterDirection.Input, av_ubigeo),
                new DbParameter("an_tiptra", DbType.Int32,20, ParameterDirection.Input, an_tiptra),
                new DbParameter("an_tipsrv", DbType.String,255, ParameterDirection.Input, an_tipsrv),
                new DbParameter("as_codzona", DbType.String,255, ParameterDirection.Output,as_codzona),
                new DbParameter("an_indica", DbType.Int32,20, ParameterDirection.Output,an_indica)
            };

            DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_VALIDA_FLUJO_ZONA_ADC, parameters);

            return new ETAFlow
            {
                an_indica = Convert.ToInt(parameters[6].Value.ToString()),
                as_codzona = Convert.ToString(parameters[5].Value)
            };
        }
        public static List<BEHub> GetHubs(string strIdSession, string strTransaction, string v_customerid, string v_contrato)
        {
            List<BEHub> list = new List<BEHub>();
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("av_customer_id", DbType.String, ParameterDirection.Input,v_customerid),
                new DbParameter("av_cod_id", DbType.String,255, ParameterDirection.Input, v_contrato),
                new DbParameter("ac_equ_cur", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int32,20, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String,255, ParameterDirection.Output)
            };
            list = DbFactory.ExecuteReader<List<BEHub>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_CONSULTA_HUB, parameters);

            return list;

        }
#endregion

        public static List<ParameterTerminalTPI> GetParameterTerminalTPI(string strIdSession, string strTransaction, int ParameterID, ref string Message)
        {
            var msg = string.Format("Metodo: {0}, Request: {1}", "GetParameterTerminalTPI", ParameterID);
            Claro.Web.Logging.Info("Session: " + strIdSession, "GetParameterTerminalTPI ", "ParameterID" + ParameterID);
            List<ParameterTerminalTPI> listParameterTerminalTPI = new List<ParameterTerminalTPI>();


            try
            {
                
                DbParameter[] parameters = {
                new DbParameter("P_PARAMETRO_ID", DbType.Int32, ParameterDirection.Input, ParameterID),
                new DbParameter("P_MENSAJE", DbType.Int32, ParameterDirection.Output),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
            };


                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_POST_DB_SP_OBTENER_PARAMETRO, parameters, (IDataReader reader) =>
                {
                    ParameterTerminalTPI entity = null;
                    while (reader.Read())
                    {
                        entity = new ParameterTerminalTPI();
                        entity.ParameterID = Functions.CheckStr(reader["PARAMETRO_ID"]);
                        entity.Name = Functions.CheckStr(reader["NOMBRE"]);
                        entity.Description = Functions.CheckStr(reader["DESCRIPCION"]);
                        entity.Type = Functions.CheckStr(reader["TIPO"]);
                        entity.ValorC = Functions.CheckStr(reader["VALOR_C"]);
                        entity.ValorN = Functions.CheckDbl(reader["VALOR_N"]);
                        entity.ValorL = Functions.CheckStr(reader["VALOR_L"]);

                        listParameterTerminalTPI.Add(entity);

                    }

                });

                Message = parameters[1].Value.ToString();
                Claro.Web.Logging.Info("Session: " + strIdSession, "GetParameterTerminalTPI ", "Message" + Message);


            }
            catch (Exception ex)
            {

                Web.Logging.Error(strIdSession, strTransaction,"Error GetParameterTerminalTPI : " + ex.Message);
            }


    


            return listParameterTerminalTPI;

        }

        public static SECURITY.GetInsertEvidence.InsertEvidenceResponse GetInsertEvidence(SECURITY.GetInsertEvidence.InsertEvidenceRequest request)
        {
            SECURITY.GetInsertEvidence.InsertEvidenceResponse model = new SECURITY.GetInsertEvidence.InsertEvidenceResponse();

            DbParameter[] parameters = {
                new DbParameter("V_TRANSACTION_TYPE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TRANSACTION_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_CUSTOMER_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_PHONE_NUMBER", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TYPIFICATION_CODE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TYPIFICATION_DESC", DbType.String,200,ParameterDirection.Input),
                new DbParameter("V_COMMERCIAL_DESC", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_PRODUCT_TYPE", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_SERVICE_CHANNEL", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_TRANSACTION_DATE", DbType.Date,ParameterDirection.Input),
                new DbParameter("D_ACTIVATION_DATE", DbType.Date,ParameterDirection.Input),
                new DbParameter("D_SUSPENSION_DATE", DbType.Date,ParameterDirection.Input),												   
                new DbParameter("V_SERVICE_STATUS", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_USER_NAME", DbType.String,50,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_NAME",DbType.String,1000,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_TYPE",DbType.String,250,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_EXTENSION",DbType.String,250,ParameterDirection.Input),
                new DbParameter("V_DOCUMENT_PATH",DbType.String,250,ParameterDirection.Input),				
                new DbParameter("P_RESULTADO", DbType.Int32,ParameterDirection.Output),
                new DbParameter("P_MSG", DbType.String,255,ParameterDirection.Output)
            };

            bool salida = false;
            for (int j = 0; j < parameters.Length; j++)
                parameters[j].Value = System.DBNull.Value;

            int i = 0;
            if (request.Evidence.StrDocumentType != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrTransactionType);
            i++;
            if (request.Evidence.StrTransactionCode != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrTransactionCode);

            i++;
            if (request.Evidence.StrCustomerCode != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrCustomerCode);
            i++;
            if (request.Evidence.StrPhoneNumber != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrPhoneNumber);
            i++;

            if (request.Evidence.StrTypificationCode != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrTypificationCode);
            i++;

            if (request.Evidence.StrTypificationDesc != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrTypificationDesc);
            i++;

            if (request.Evidence.StrCommercialDesc != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrCommercialDesc);
            i++;

            if (request.Evidence.StrProductType != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrProductType);
            i++;

            if (request.Evidence.StrServiceChannel != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrServiceChannel);
            i++;

            if (request.Evidence.StrTransactionDate != null)
                parameters[i].Value = Functions.CheckDate(request.Evidence.StrTransactionDate);
            i++;

            if (request.Evidence.StrActivationDate != null)
                parameters[i].Value = Functions.CheckDate(request.Evidence.StrActivationDate);
            i++;

            if (request.Evidence.StrSuspensionDate != null)
                parameters[i].Value = Functions.CheckDate(request.Evidence.StrSuspensionDate);
            i++;

            if (request.Evidence.StrServiceStatus != null)
                parameters[i].Value = request.Evidence.StrServiceStatus;
            i++;

            if (request.Evidence.StrUserName != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrUserName);
            i++;

            if (request.Evidence.StrDocumentName != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrDocumentName);
            i++;

            if (request.Evidence.StrDocumentType != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrDocumentType);
            i++;

            if (request.Evidence.StrDocumentExtension != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrDocumentExtension);
            i++;

            if (request.Evidence.StrDocumentPath != null)
                parameters[i].Value = Functions.CheckStr(request.Evidence.StrDocumentPath);
            i++;

            try
            {
                Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(request.Audit.Session, request.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU,
                        DbCommandConfiguration.SIACU_POST_PVU_SP_INSERTAR_EVIDENCIA_A, parameters);


                });
                model.BoolResult = true;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                model.BoolResult = false;
            }


            var parSalida1 = string.Empty;
            var parSalida2 = string.Empty;

            parSalida1 = parameters[parameters.Length - 2].Value.ToString();
            parSalida2 = parameters[parameters.Length - 1].Value.ToString();

            model.StrFlagInsertion  = Functions.CheckStr(parSalida1);
            model.StrMsgText = Functions.CheckStr(parSalida2);

            return model;
        }

    public static List<Entity.Transac.Service.Common.ListItem> GetSotMtto(string strIdSession, string strTransaction, Int64 customer_id, Int64 cod_id)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("av_customer_id", DbType.Int64, ParameterDirection.Input, customer_id),
                new DbParameter("av_cod_id", DbType.Int64, ParameterDirection.Input, cod_id),
                new DbParameter("ac_list_solot_mtto", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_resultado", DbType.Int64, ParameterDirection.Output),
                new DbParameter("av_mensaje", DbType.String, 225,ParameterDirection.Output)
            };

            List<Entity.Transac.Service.Common.ListItem> listItem = null;
            DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_GET_SOT_MTTO, parameters, (IDataReader reader) =>
            {
                listItem = new List<Entity.Transac.Service.Common.ListItem>();

                while (reader.Read())
                {
                    listItem.Add(new Entity.Transac.Service.Common.ListItem
                    {
                        Code = Convert.ToString(reader["codsolot"]),
                        Description = Convert.ToString(reader["codsolot"])
                    });
                }
            });

            return listItem;
        }

        public static List<ConsultImei> GetConsultImei(string strIdSession, string strIdTransaction, string nrophone)
        {
            List<ConsultImei> listConsultaImei = null;
            ConsultImei oConsultImei = null;

            COMMON_IMEI.ImeiRequest obRequest = new COMMON_IMEI.ImeiRequest();
            COMMON_IMEI.ImeiResponse obResponse = new COMMON_IMEI.ImeiResponse();
            try
            {
                Claro.Web.Logging.Info(strIdSession, strIdTransaction, "Parametros Entrada WS: BusquedaIMEI Método: buscarIMEIs ::: vtelefono: " + nrophone);
               
                obRequest.numeroTelefono = nrophone;

                var lista = Web.Logging.ExecuteMethod(strIdSession, strIdTransaction, ServiceConfiguration.SIACUConsultaIMEI, () =>
                {
                    return Configuration.ServiceConfiguration.SIACUConsultaIMEI.buscarIMEIs(obRequest);
                });
                string strRespuesta = lista.respuesta;
                string strMensaje = lista.mensaje;
                if (strRespuesta == "0")
                {
                    if (lista != null && lista.imeiLista != null)
                    {
                        if (lista.imeiLista.imeiItem != null)
                        {
                            listConsultaImei = new List<ConsultImei>();
                            foreach (var item in lista.imeiLista.imeiItem)
                            {
                                oConsultImei = new ConsultImei();
                                oConsultImei.Nro_phone = item.telefono;
                                oConsultImei.Nro_imei = item.imei;
                                oConsultImei.Date_hour_start = item.fechaInicio;
                                oConsultImei.Date_hour_end = item.fechaFin;
                                oConsultImei.mark = item.marca;
                                oConsultImei.model = item.modelo;
                                listConsultaImei.Add(oConsultImei);
                            }
                        }
                    }
                    
                }
                Claro.Web.Logging.Info(strIdSession, strIdTransaction, "Parametros Salida::: Respuesta:" + strRespuesta);
                Claro.Web.Logging.Info(strIdSession, strIdTransaction, "Parametros Salida::: Mensaje:" + strMensaje);
               
            }
            catch (Exception ex)
            {                
                Claro.Web.Logging.Error(strIdSession, strIdTransaction,"(Exception) Error :" + ex.Message);
            }           
            return listConsultaImei;
        }
        public static string GetConsultMarkModel(string strIdSession, string strIdTransaction, string IMEI)
        {
            COMMON_MARKMODEL.EbsSWSAPClient objEbsSWSAPClient = new COMMON_MARKMODEL.EbsSWSAPClient();
            COMMON_MARKMODEL.consultaEquipoXIMEIRequest obRequest = new COMMON_MARKMODEL.consultaEquipoXIMEIRequest();
            COMMON_MARKMODEL.consultaEquipoXIMEIResponse obResponse = new COMMON_MARKMODEL.consultaEquipoXIMEIResponse();

            obRequest.imei = IMEI;

            var description = Web.Logging.ExecuteMethod(strIdSession, strIdTransaction, ServiceConfiguration.SIACUConsultMarkModel, () =>
            {
                return Configuration.ServiceConfiguration.SIACUConsultMarkModel.consultaEquipoXIMEI(obRequest.ToString());
            });
            return description.ToString();
        } 

        public static List<QuestionsSecurity> GetQuestionsSecurity(string strIdSession, string strTransaction, string typeclient, string grupclient)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPOCLIENTE", DbType.String, ParameterDirection.Input, typeclient),
                new DbParameter("P_GRUPOCLIENTE", DbType.String, ParameterDirection.Input, grupclient),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_CODE_RESULT", DbType.Int64, 255, ParameterDirection.Output),
	            new DbParameter("PO_MESSAGE_RESULT", DbType.String, 255, ParameterDirection.Output)   
                
            };

            List<QuestionsSecurity> listQuestionsSecurity = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACSS_SP_PREGUNTAS_SEGURIDAD, parameters, (IDataReader reader) =>
                {
                    listQuestionsSecurity = new List<QuestionsSecurity>();

                    while (reader.Read())
                    {
                        listQuestionsSecurity.Add(new QuestionsSecurity()
                        {
                            Description = Convert.ToString(reader["PEGEV_DESCPREGUNTA"]),
                            IdQuestions = Convert.ToInt(reader["PEGEN_OBJID"])
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listQuestionsSecurity;
        }

        public static List<AnswerSecurity> GetAnswerSecurity(string strIdSession, string strTransaction, string typeclient, string grupclient)
        {
            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("P_TIPOCLIENTE", DbType.String, ParameterDirection.Input, typeclient),
                new DbParameter("P_GRUPOCLIENTE", DbType.String, ParameterDirection.Input, grupclient),
                new DbParameter("P_CURSOR", DbType.Object, ParameterDirection.Output)
                
            };

            List<AnswerSecurity> listQuestionsSecurity = null;
            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACSS_SP_RESPUESTAS_SEGURIDAD, parameters, (IDataReader reader) =>
                {
                    listQuestionsSecurity = new List<AnswerSecurity>();

                    while (reader.Read())
                    {
                        listQuestionsSecurity.Add(new AnswerSecurity()
                        {
                            Description = Convert.ToString(reader["REGEV_DESCRESPUESTA"]),
                            IdAnswer = Convert.ToInt(reader["REGEN_OBJIDPREGUNTA"])
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, ex.Message);

            }
            return listQuestionsSecurity;
        }


        public static List<Entity.Transac.Service.Common.EquipmentForeign> GetEquipmentForeign(string strIdSession, string strTransaction, Int64 strCustomerId, string strEstado, ref int rCodeResult, ref string rMsgText)
        {
            Web.Logging.Error(strIdSession, strTransaction, "Entrada data GetEquipmentForeign");
            DbParameter[] parameters = 
            {
                new DbParameter("PI_CUSTOMERID", DbType.Int64, ParameterDirection.Input, strCustomerId),
                new DbParameter("PI_ESTADO", DbType.String, ParameterDirection.Input, strEstado),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_CODE_RESULT", DbType.String, 255, ParameterDirection.Output),
	            new DbParameter("PO_MESSAGE_RESULT", DbType.String, 255, ParameterDirection.Output)               
            };

            List<Entity.Transac.Service.Common.EquipmentForeign> listItem = null;
            try
            {
                Web.Logging.Info(strIdSession, strTransaction, "Entrada data GetEquipmentForeign try");
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SIACSS_LISTAEXTRANJERO, parameters, (IDataReader reader) =>
                {
                    listItem = new List<Entity.Transac.Service.Common.EquipmentForeign>();
                    Web.Logging.Info(strIdSession, strTransaction, "Entrada data GetEquipmentForeign while");
                    while (reader.Read())
                    {
                        listItem.Add(new Entity.Transac.Service.Common.EquipmentForeign
                        {
                            redevNumeroLinea = Convert.ToString(reader["REDEV_NUMEROLINEA"]),
                            rededFechaCrea = Convert.ToString(reader["REDED_FECHACREA"]),
                            redevNumeroImei = Convert.ToString(reader["REDEV_NUMERO_IMEI"]),
                            redevMarcaModelo = Convert.ToString(reader["REDEV_MARCA_MODELO"]),
                            estadoEquipo = Convert.ToString(reader["ESTADO_EQUIPO"]),
                            tipoMotivoBloqueo = Convert.ToString(reader["TIPO_MOTIVO_BLOQUEO"]),
                            fechaRegistroBloqueo = Convert.ToString(reader["FECHA_REGISTRO_BLOQUEO"])
                        });
                    }
                    Web.Logging.Info(strIdSession, strTransaction, "Termina data GetEquipmentForeign while");
                });
               
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, "GetEquipmentForeign Error: " + ex.Message);
            }
            finally
            {
                rCodeResult = Convert.ToInt(parameters[parameters.Length - 2].Value.ToString());
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

                Logging.Info(strIdSession, strTransaction, "GetEquipmentForeign rCodeResult: " + rCodeResult);
                Logging.Info(strIdSession, strTransaction, "GetEquipmentForeign rMsgText: " + rMsgText);
            }
            return listItem;
        }


        public static bool GetInteraction(string idTransaccion, Typification objTypification, Claro.Entity.AuditRequest objAuditRequest)
        {
            bool strResult = false;

            try
            {

                SIACU_INTERAC.InteraccionType objInteraccionType = new SIACU_INTERAC.InteraccionType()
                {
                    clase = objTypification.CLASE,
                    codigoEmpleado = objTypification.CODIGOEMPLEADO,
                    codigoSistema = objTypification.CODIGOSISTEMA,
                    flagCaso = objTypification.FLAGCASO,
                    hechoEnUno = objTypification.HECHOENUNO,
                    metodoContacto = objTypification.METODOCONTACTO,
                    notas = objTypification.NOTAS,
                    subClase = objTypification.SUBCLASE,
                    telefono = objTypification.TELEFONO,
                    textResultado = objTypification.TEXTRESULTADO,
                    tipo = objTypification.TIPO,
                    tipoInteraccion = objTypification.TIPOINTERACCION

                };
                SIACU_INTERAC.TransaccionInteraccionesAsync objTransaccionInteraccionesAsync = new SIACU_INTERAC.TransaccionInteraccionesAsync()
                {
                    Url = KEY.AppSettings("gConstUrlTipificaHistPaquetes"),
                    Credentials = System.Net.CredentialCache.DefaultCredentials,
                    Timeout = 10000
                };
                SIACU_INTERAC.AuditType objAuditType = Claro.Web.Logging.ExecuteMethod<SIACU_INTERAC.AuditType>(objAuditRequest.Session, objAuditRequest.Transaction, () =>
                {
                    return objTransaccionInteraccionesAsync.crearInteraccion(idTransaccion, objInteraccionType, null, null, "");
                });

                
                if (objAuditType.errorCode.Equals("0"))
                {
                    strResult = true;
                }

            }
            catch (Exception ex)
            {


                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }


            return strResult;
        }


        public static bool GetInsertEquipmentForeign(string strIdSession, string strTransaction, EquipmentForeignInsert item, ref int rCodeResult, ref string rMsgText)
        {
            Web.Logging.Info(strIdSession, strTransaction, "Inicio GetInsertEquipmentForeign");
            DbParameter[] parameters = 
            {
                new DbParameter("PI_CUSTOMERID", DbType.Int64,ParameterDirection.Input, item.REDEN_CUSTOMERID),
                new DbParameter("PI_IMEI", DbType.String,255,ParameterDirection.Input, item.REDEV_NUMERO_IMEI),
                new DbParameter("PI_IMEI_FISICO", DbType.String,255,ParameterDirection.Input, item.REDEV_NUMERO_IMEI_FISICO),
                new DbParameter("PI_NUMLINEA", DbType.String,255,ParameterDirection.Input, item.REDEV_NUMEROLINEA),
                new DbParameter("PI_ESTADO", DbType.String,255,ParameterDirection.Input, item.REDEV_ESTADO),
                new DbParameter("PI_MARCA_MODELO", DbType.String,255,ParameterDirection.Input, item.REDEV_MARCA_MODELO),
                new DbParameter("PI_USUARIO", DbType.String,255,ParameterDirection.Input, item.REDEV_USUARIOCREA),
                new DbParameter("PI_MAXIMO", DbType.String,255,ParameterDirection.Input, item.MAXIMO),
                new DbParameter("PO_CODE_RESULT", DbType.String,255,ParameterDirection.Output),
			    new DbParameter("PO_MESSAGE_RESULT", DbType.String,255,ParameterDirection.Output)	
            };

            try
            {
                Web.Logging.Info(strIdSession, strTransaction, "Inicio GetInsertEquipmentForeign try");
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SIACSI_REG_DESVIN_EQUIPO_MOV, parameters);
                });
                Web.Logging.Info(strIdSession, strTransaction, "Fin GetInsertEquipmentForeign try");
            }
            catch (Exception ex)
            {
                Web.Logging.Info(strIdSession, strTransaction, "GetInsertEquipmentForeign catch");
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                rCodeResult = Convert.ToInt(parameters[parameters.Length - 2].Value.ToString());
                rMsgText = parameters[parameters.Length - 1].Value.ToString();

                Web.Logging.Info(strIdSession, strTransaction, "GetInsertEquipmentForeign rCodeResult: " + rCodeResult);
                Web.Logging.Info(strIdSession, strTransaction, "GetInsertEquipmentForeign rMsgText: " + rMsgText);
            }
            Web.Logging.Info(strIdSession, strTransaction, "Fin GetInsertEquipmentForeign");
            return true;
        }

        public static List<Entity.Transac.Service.Common.ListEquipmentRegistered> GetListEquipmentRegistered(string strIdSession, string strTransaction, Int64 strCustomerId, string strImei, string strEstado, int strCantidadMaxima, ref int rCodeResult, ref string rMsgText)
        {
            Web.Logging.Info(strIdSession, strTransaction, "entro Data GetListEquipmentRegistered  ");
            DbParameter[] parameters = 
            {
                new DbParameter("PI_CUSTOMERID", DbType.Int64, ParameterDirection.Input, strCustomerId),
                new DbParameter("PI_IMEI", DbType.String,255,ParameterDirection.Input, strImei),
                new DbParameter("PI_ESTADO", DbType.String, ParameterDirection.Input, strEstado),
                new DbParameter("PI_MAXIMO", DbType.String, ParameterDirection.Input, strCantidadMaxima),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output),
                new DbParameter("PO_CODE_RESULT", DbType.String, 255, ParameterDirection.Output),
	            new DbParameter("PO_MESSAGE_RESULT", DbType.String, 255, ParameterDirection.Output)               
            };

            List<Entity.Transac.Service.Common.ListEquipmentRegistered> listItem = null;
            try
            {

                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_DB, DbCommandConfiguration.SIACU_SIACSS_LISTAEQUIPOREGISTRADO, parameters, (IDataReader reader) =>
                {
                    listItem = new List<Entity.Transac.Service.Common.ListEquipmentRegistered>();
                    Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered entrando while");
                    while (reader.Read())
                    {
                        Entity.Transac.Service.Common.ListEquipmentRegistered objListEquipmentRegistered = new Entity.Transac.Service.Common.ListEquipmentRegistered();
                        Web.Logging.Info(strIdSession, strTransaction, "proceso : ");


                        objListEquipmentRegistered.numeroImei = Convert.ToString(reader["REDEV_NUMERO_IMEI"]);
                        Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered REDEV_NUMERO_IMEI: " + objListEquipmentRegistered.numeroImei);
                        objListEquipmentRegistered.numLinea = Convert.ToString(reader["REDEV_NUMEROLINEA"]);
                        Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered REDEV_NUMEROLINEA: " + objListEquipmentRegistered.numLinea);
                        objListEquipmentRegistered.estado = Convert.ToString(reader["REDEV_ESTADO"]);
                        Web.Logging.Info(strIdSession, strTransaction, "Data GetListEquipmentRegistered REDEV_ESTADO: " + objListEquipmentRegistered.estado);
                        objListEquipmentRegistered.marcaModelo = Convert.ToString(reader["REDEV_MARCA_MODELO"]);
                        Web.Logging.Info(strIdSession, strTransaction, "Data GetListEquipmentRegistered REDEV_MARCA_MODELO: " + objListEquipmentRegistered.marcaModelo);
                        objListEquipmentRegistered.fechaRegistro = Convert.ToDate(reader["REDED_FECHACREA"]);
                        Web.Logging.Info(strIdSession, strTransaction, "Data GetListEquipmentRegistered REDED_FECHACREA: " + objListEquipmentRegistered.fechaRegistro);

                        listItem.Add(objListEquipmentRegistered);     
                    }
                    Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered salio while");
                   
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered entro catch 1");
                Web.Logging.Error(strIdSession, strTransaction, "Error: " + ex.Message);

            }
            finally
            {
                Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered entro finally");
                rCodeResult = Convert.ToInt(parameters[parameters.Length - 2].Value.ToString());
                Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered entro finally"+ rCodeResult);
                rMsgText = parameters[parameters.Length - 1].Value.ToString();
                Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered entro finally" + rMsgText);

                Web.Logging.Info(strIdSession, strTransaction, "Listado Registros: " + rMsgText);
            }
            Web.Logging.Info(strIdSession, strTransaction, " Data GetListEquipmentRegistered retorna lista");
            return listItem;
        }

        public static List<ConsultByGroupParameter> GetConsultByGroupParameter(string strIdSession, string strTransaction, Int64 intCodGrupo)
        {
            Web.Logging.Info(strIdSession, strTransaction, "Inicio GetConsultByGroupParameter");
            DbParameter[] parameters = 
            {
                new DbParameter("P_PARAN_GRUPO", DbType.Int64, ParameterDirection.Input, intCodGrupo),
                new DbParameter("K_CUR_SALIDA", DbType.Object, ParameterDirection.Output)               
            };

            List<ConsultByGroupParameter> listItem = null;
            try
            {                
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SECSS_CON_PARAMETRO_GP, parameters, (IDataReader reader) =>
                {
                    listItem = new List<ConsultByGroupParameter>();
                    Web.Logging.Info(strIdSession, strTransaction, "Inicio while");
                    while (reader.Read())
                    {
                        listItem.Add(new ConsultByGroupParameter
                        {
                            ID_PARAMETRO = Convert.ToInt64(reader["PARAN_CODIGO"]),
                            DESCRIPCION = Convert.ToString(reader["PARAV_DESCRIPCION"]),
                            VALOR = Convert.ToString(reader["PARAV_VALOR"]),
                            VALOR1 = Convert.ToString(reader["PARAV_VALOR1"]),
                        });
                    }
                    Web.Logging.Info(strIdSession, strTransaction, "Fin while");
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Info(strIdSession, strTransaction, "GetConsultByGroupParameter catch");
                Claro.Web.Logging.Error(strIdSession, strTransaction, "GetConsultByGroupParameter Error: " + ex.Message);
                throw;
            }
            Web.Logging.Info(strIdSession, strTransaction, "Fin GetConsultByGroupParameter");
            return listItem;
        }

        public static string GetIdTrazabilidad(string strIdSession, string strTransaction, Int32 intCodGrupo)
        {
            string resp = string.Empty;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("p_id_trx_bio", DbType.String, 16, ParameterDirection.ReturnValue),
                new DbParameter("p_tipo_id", DbType.Int32, ParameterDirection.Input, intCodGrupo)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteNonQuery(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_PVU,
                        DbCommandConfiguration.GET_ID_TRAZABILIDAD_BIOMETRIA, parameters);
                });
                resp = Convert.ToString(parameters[0].Value);
     
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "Error: " + ex.Message);
                resp = string.Empty;
            }
            return resp;
          
        }

        public static Entity.Transac.Service.Common.GetBlackList.BlackListResponse GetBlackListOsiptel(Entity.Transac.Service.Common.GetBlackList.BlackListRequest objBlackListRequest)
        {
            Claro.Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Session, "Entrada: Data Método: GetBlackListOsiptel");
            Claro.Web.Logging.Info(objBlackListRequest.Audit.Session, objBlackListRequest.Audit.Session, "Parametros Entrada: Data Método: GetBlackList ::: strImei: " + objBlackListRequest.MessageRequest.Body.pi_imei);
            return RestService.PostInvoque<Entity.Transac.Service.Common.GetBlackList.BlackListResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.BLACKLIST_OSIPTEL, objBlackListRequest.Audit, objBlackListRequest, true);  
                       
        }

        #region TOA

        public static List<Entity.Transac.Service.Common.TimeZone> GetTimeZones(string strIdSession, string strTransaction, string vstrCoUbi, string vstrTipTra, string vstrFecha)
        {
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCPlanMigration", "Entro GetTimeZones");
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCPlanMigration", "los parametros que recibe el metodo son: strIdSession:" + strIdSession + ";strTransaction:" + strTransaction + ";vstrCoUbi:" + vstrCoUbi + ";vstrTipTra:" + vstrTipTra + ";vstrFecha:" + vstrFecha);
            List<Entity.Transac.Service.Common.TimeZone> list = null;

            DbParameter[] parameters = new DbParameter[] {
                new DbParameter("programaciones", DbType.Object, ParameterDirection.Output),
                new DbParameter("an_ubigeo", DbType.String,255, ParameterDirection.Input, vstrCoUbi),
                new DbParameter("an_tiptra", DbType.String,255, ParameterDirection.Input,vstrTipTra),
                new DbParameter("ad_fecagenda",DbType.Date,30,ParameterDirection.Input,vstrFecha)
            };
            try
            {
                list = DbFactory.ExecuteReader<List<Entity.Transac.Service.Common.TimeZone>>(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_P_GENERA_HORARIO_SIAC, parameters);
            }
            catch (Exception ex)
            {
                list = null;
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));

            }
            Claro.Web.Logging.Info(strIdSession, strTransaction, JsonConvert.SerializeObject(list));
            Claro.Web.Logging.Info("HFCGetTimeZones", "HFCGetTimeZones", "Finalizó GetTimeZones");
            return list;
        }

        public static EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditResponseCapacity ConsultarCapacidadCuadrillas(EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditRequestCapacity objETAAuditRequestCapacity)
        {

            string vDurActivity = string.Empty;
            string vTiempoViajeActivity = string.Empty;
            EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditResponseCapacity Resp = new EntitiesCommon.GetETAAuditRequestCapacity.ETAAuditResponseCapacity();

            ADMCU.AuditResponse objResponseCuadrillas = new ADMCU.AuditResponse();
            try
            {
                ADMCU.AuditRequest AuditRequestCuadrillas = new ADMCU.AuditRequest();
                AuditRequestCuadrillas.idTransaccion = objETAAuditRequestCapacity.pIdTrasaccion;
                AuditRequestCuadrillas.ipAplicacion = objETAAuditRequestCapacity.pIP_APP;
                AuditRequestCuadrillas.nombreAplicacion = objETAAuditRequestCapacity.pAPP;
                AuditRequestCuadrillas.usuarioAplicacion = objETAAuditRequestCapacity.pUsuario;

                ADMCU.campoActividadType[] ListaCapActiRequestCuadrillas = new ADMCU.campoActividadType[objETAAuditRequestCapacity.vCampoActividad.Length];


                String CantidadFechas = String.Empty;
                foreach (DateTime vF in objETAAuditRequestCapacity.vFechas)
                {
                    CantidadFechas = CantidadFechas + ";" + vF.ToString();
                }

                if (objETAAuditRequestCapacity.vCampoActividad.Length > 0)
                {
                    int i = 0;
                    ADMCU.campoActividadType CampoActividadRequestCuadrillas = null;
                    foreach (Claro.SIACU.Entity.Transac.Service.Common.ETAFieldActivity oCampAct in objETAAuditRequestCapacity.vCampoActividad)
                    {
                        CampoActividadRequestCuadrillas = new ADMCU.campoActividadType();
                        CampoActividadRequestCuadrillas.nombre = oCampAct.Nombre;
                        CampoActividadRequestCuadrillas.valor = oCampAct.Valor;
                        ListaCapActiRequestCuadrillas[i] = CampoActividadRequestCuadrillas;
                        i++;
                    }
                }
                else
                {
                    ListaCapActiRequestCuadrillas[0].nombre = String.Empty;
                    ListaCapActiRequestCuadrillas[0].valor = String.Empty;
                }

                ADMCU.parametrosRequest oIniParamRequestCuadrillas = new ADMCU.parametrosRequest();
                ADMCU.parametrosRequest[] oParamRequestCuadrillas = new ADMCU.parametrosRequest[] { oIniParamRequestCuadrillas };

                ADMCU.parametrosRequestObjetoRequestOpcional[] ListaParamReqOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional[objETAAuditRequestCapacity.vListaCapReqOpc.Length];

                if (objETAAuditRequestCapacity.vListaCapReqOpc.Length > 0)
                {
                    int j = 0, k = 0;
                    foreach (EntitiesCommon.ETAListParametersRequestOptionalCapacity oListaParReq in objETAAuditRequestCapacity.vListaCapReqOpc)
                    {

                        foreach (EntitiesCommon.ETAParametersRequestCapacity oParamReqCapacity in oListaParReq.ParamRequestCapacities)
                        {
                            ADMCU.parametrosRequestObjetoRequestOpcional oParamRequestOpcionalCuadrillas = new ADMCU.parametrosRequestObjetoRequestOpcional();
                            oParamRequestOpcionalCuadrillas.campo = oParamReqCapacity.Campo;
                            oParamRequestOpcionalCuadrillas.valor = oParamReqCapacity.Valor;
                            ListaParamReqOpcionalCuadrillas[j] = oParamRequestOpcionalCuadrillas;
                            j++;
                        }
                        oParamRequestCuadrillas[k].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                        k++;
                    }
                }
                else
                {
                    ListaParamReqOpcionalCuadrillas[0].campo = string.Empty;
                    ListaParamReqOpcionalCuadrillas[0].valor = string.Empty;
                    oParamRequestCuadrillas[0].objetoRequestOpcional = ListaParamReqOpcionalCuadrillas;
                }

                ADMCU.capacidadType[] ListaCapacidadTypeCuadrillas = new ADMCU.capacidadType[0];

                ADMCU.parametrosResponse[] ListaParamResponseCuadrillas = new ADMCU.parametrosResponse[0];


                objResponseCuadrillas = Claro.Web.Logging.ExecuteMethod<ADMCU.AuditResponse>(objETAAuditRequestCapacity.Audit.Session, objETAAuditRequestCapacity.Audit.Transaction, () =>
                {

                    return Configuration.WebServiceConfiguration.ADMCUAD_CapacityService.consultarCapacidad(AuditRequestCuadrillas,
                        objETAAuditRequestCapacity.vFechas,
                        null,
                        objETAAuditRequestCapacity.vCalcDur,
                        objETAAuditRequestCapacity.vCalcDurEspec,
                        objETAAuditRequestCapacity.vCalcTiempoViaje,
                        objETAAuditRequestCapacity.vCalcTiempoViajeEspec,
                        objETAAuditRequestCapacity.vCalcHabTrabajo,
                        objETAAuditRequestCapacity.vCalcHabTrabajoEspec,
                        objETAAuditRequestCapacity.vObtenerUbiZona,
                        objETAAuditRequestCapacity.vObtenerUbiZonaEspec,
                        objETAAuditRequestCapacity.vEspacioTiempo,
                        objETAAuditRequestCapacity.vHabilidadTrabajo,
                        ListaCapActiRequestCuadrillas,
                        oParamRequestCuadrillas,
                        out vDurActivity,
                        out vTiempoViajeActivity,
                        out ListaCapacidadTypeCuadrillas,
                        out ListaParamResponseCuadrillas);
                });

                Resp.CodigoRespuesta = objResponseCuadrillas.codigoRespuesta;
                Resp.IdTransaccion = objResponseCuadrillas.idTransaccion;
                Resp.MensajeRespuesta = objResponseCuadrillas.mensajeRespuesta;
                string OutDurActivity = vDurActivity;
                string OutTiempoViajeActivity = vTiempoViajeActivity;

                Resp.DuraActivity = vDurActivity;
                Resp.TiempoViajeActivity = vTiempoViajeActivity;

                EntitiesCommon.ETAEntityCapacityType[] oCapacidadTypeM = new EntitiesCommon.ETAEntityCapacityType[ListaCapacidadTypeCuadrillas.Length];
                int l = 0;
                foreach (ADMCU.capacidadType oEntCapacidadType in ListaCapacidadTypeCuadrillas)
                {
                    EntitiesCommon.ETAEntityCapacityType oCapacidadType = new EntitiesCommon.ETAEntityCapacityType();
                    oCapacidadType.strLocation = oEntCapacidadType.ubicacion;
                    oCapacidadType.dtmDate = oEntCapacidadType.fecha;
                    oCapacidadType.strSpaceTime = oEntCapacidadType.espacioTiempo;
                    oCapacidadType.strSkillWork = oEntCapacidadType.habilidadTrabajo;
                    oCapacidadType.lngQuota = oEntCapacidadType.cuota;
                    oCapacidadType.lngAvailable = oEntCapacidadType.disponible;
                    oCapacidadTypeM[l] = oCapacidadType;
                    l++;
                }
                Resp.ObjetoCapacity = oCapacidadTypeM;



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objETAAuditRequestCapacity.Audit.Session, objETAAuditRequestCapacity.Audit.Transaction, ex.Message);
            }
            return Resp;
        }

        public static int RegisterEtaRequest(EntitiesCommon.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {

            DbParameter[] arrParam = {
                new DbParameter("vnumslc", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vnumslc),
                new DbParameter("vfecha_venta", DbType.Date,20,ParameterDirection.Input,Convert.ToDate( objRegisterEtaRequest.vfecha_venta) ),
                new DbParameter("vcod_zona", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaRequest.vcod_zona),
                new DbParameter("vcod_plano", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vcod_plano),
                new DbParameter("vtipo_orden", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vtipo_orden),
                new DbParameter("vsubtipo_orden", DbType.String,20,ParameterDirection.Input,objRegisterEtaRequest.vsubtipo_orden),
                new DbParameter("vidPoblado", DbType.String,20 ,ParameterDirection.Input,String.Empty),
                new DbParameter("vtiempo_trabajo", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaRequest.vtiempo_trabajo),
                new DbParameter("vidreturn", DbType.Int32,20,ParameterDirection.Output,objRegisterEtaRequest.vidreturn)
            };

            try
            {
                Claro.Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_ETA_REQ, arrParam);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, ex.Message);
            }
            finally
            {
                objRegisterEtaRequest.vidreturn = Convert.ToInt(arrParam[arrParam.Length - 1].Value.ToString());
            }

            return objRegisterEtaRequest.vidreturn;
        }

        public static string RegisterEtaResponse(EntitiesCommon.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            DbParameter[] arrParam = {
                new DbParameter("vidconsulta", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vidconsulta),
                new DbParameter("vdia", DbType.Date,20,ParameterDirection.Input,objRegisterEtaResponse.vdia),
                new DbParameter("vfranja", DbType.String,20,ParameterDirection.Input,objRegisterEtaResponse.vfranja),
                new DbParameter("vestado", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vestado),
                new DbParameter("vquota", DbType.Int32,20,ParameterDirection.Input,objRegisterEtaResponse.vquota),
                new DbParameter("vid_bucket", DbType.String,50,ParameterDirection.Input,objRegisterEtaResponse.vid_bucket),
                new DbParameter("vresp", DbType.String,20,ParameterDirection.Output,objRegisterEtaResponse.vresp)
            };

            try
            {
                Claro.Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, () =>
                {
                    DbFactory.ExecuteNonQuery(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_SP_REGISTRA_ETA_RESP, arrParam);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, ex.Message);
            }
            finally
            {
                objRegisterEtaResponse.vresp = arrParam[arrParam.Length - 1].Value.ToString();
            }

            return objRegisterEtaResponse.vresp;
        }

        public static List<EntitiesCommon.TransactionScheduled> GetSchedulingRule(string strIdSession, string strTransaction, string p_idparametro)
        {
            DbParameter[] parameters = 
            {
                new DbParameter("p_idparametro", DbType.String,255, ParameterDirection.Input, p_idparametro),
                new DbParameter("po_dato", DbType.Object, ParameterDirection.Output),
                new DbParameter("po_cod_error", DbType.String,255, ParameterDirection.Output),
                new DbParameter("po_des_error", DbType.String,255, ParameterDirection.Output)
            };

            List<EntitiesCommon.TransactionScheduled> listItem = new List<EntitiesCommon.TransactionScheduled>();
            EntitiesCommon.TransactionScheduled item = null;

            try
            {
                DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_SGA, DbCommandConfiguration.SIACU_CONSULTA_REGLAS_AGENDAMIENTO, parameters, dr =>
                {
                    while (dr.Read())
                    {
                        item = new EntitiesCommon.TransactionScheduled();
                        item.ID_PARAMETRO = Functions.CheckStr(dr["ID_PARAMETRO"]);
                        item.CODIGOC = Functions.CheckStr(dr["CODIGOC"]);
                        item.CODIGONPAGE = Functions.CheckStr(dr["CODIGON"].ToString());
                        item.DESCRIPCION = Functions.CheckStr(dr["cab_desc"]);
                        item.ABREVIATURA = Functions.CheckStr(dr["cab_desc"]);
                        item.ESTADO = Functions.CheckStr(dr["cab_desc"]);
                        item.ID_DETALLE = Functions.CheckStr(dr["ID_DETALLE"]);
                        item.DESCRIPCION_DET = Functions.CheckStr(dr["deta_desc"]);
                        item.ABREVIATURA_DET = Functions.CheckStr(dr["deta_abrev"]);
                        item.ESTADO_DET = Functions.CheckStr(dr["deta_estado"]);

                        listItem.Add(item);
                    }
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, Claro.SIACU.Transac.Service.Functions.GetExceptionMessage(ex));
            }

            Claro.Web.Logging.Info(strIdSession, strTransaction, "GetSchedulingRule Lista Resultado: " + listItem.Count);

            return listItem;
        }

        #endregion

        public static Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost GetPuntosAtencion(Entity.Transac.Service.Common.GetPuntosAtencion.PuntosAtencionRequest objPuntosAtencionRequest)
        {
            Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost objListTabPuntosAtencionPost = new Entity.Transac.Service.Common.GetPuntosAtencion.responseDataObtenerTabPuntosAtencionPost();
            Entity.Transac.Service.Common.GetPuntosAtencion.PuntosAtencionResponse objPuntosAtenciontResponse = null;

            try
            {
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = KEY.AppSettings("strRestricTetheVelocWS");
                string cryptoUser = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("User", "").ToString();
                string cryptoPass = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("Password", "").ToString();
                desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                    {
                        idTransaccion = objPuntosAtencionRequest.Audit.Transaction,
                        ipAplicacion = objPuntosAtencionRequest.Audit.IPAddress,
                        ipTransicion = objPuntosAtencionRequest.Audit.IPAddress,
                        usrAplicacion = objPuntosAtencionRequest.Audit.UserName,
                        idAplicacion = objPuntosAtencionRequest.Audit.ApplicationName,
                        codigoAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                        usuarioAplicacionEncriptado = cryptoUser,
                        claveEncriptado = cryptoPass,
                    };
               
                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                Claro.Web.Logging.Error(objPuntosAtencionRequest.Audit.Session, objPuntosAtencionRequest.Audit.Transaction, "CONSULTA_CLAVES.desencriptar codigoResultado: " + objdesencriptarResponse.codigoResultado);
               
                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    PuntosAtencionWS.HeaderRequestType objHeaderRequest = new PuntosAtencionWS.HeaderRequestType()
                    {
                        country = objPuntosAtencionRequest.Header.country,
                        language = objPuntosAtencionRequest.Header.language,
                        consumer = objPuntosAtencionRequest.Header.consumer,
                        system = objPuntosAtencionRequest.Header.system,
                        modulo = KEY.AppSettings("puntosAtencion"),
                        pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        userId = objPuntosAtencionRequest.Audit.UserName,
                        dispositivo = objPuntosAtencionRequest.Audit.IPAddress,
                        wsIp = objPuntosAtencionRequest.Audit.IPAddress,
                        operation = KEY.AppSettings("obtenerTabPuntosAtencionPost"),
                        timestamp = DateTime.Now,
                        msgType = objPuntosAtencionRequest.Header.msgType,
                        VarArg = new PuntosAtencionWS.ArgType[1]
                        {
                            new PuntosAtencionWS.ArgType(){
                                key = String.Empty,
                                value = String.Empty
                             }
                        }
                    };

                    PuntosAtencionWS.HeaderRequestType1 objHeaderRequest1 = new PuntosAtencionWS.HeaderRequestType1()
                    {
                        canal = objPuntosAtencionRequest.Audit.ApplicationName,
                        idAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                        usuarioAplicacion = objPuntosAtencionRequest.Audit.UserName,
                        usuarioSesion = objPuntosAtencionRequest.Audit.UserName,
                        idTransaccionESB = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                        idTransaccionNegocio = objPuntosAtencionRequest.Audit.Transaction,
                        fechaInicio = DateTime.Now,
                        nodoAdicional = new object()
                    };
                    PuntosAtencionWS.obtenerTabPuntosAtencionPostRequestType objobtenerTabPuntosAtencionPostRequest = new PuntosAtencionWS.obtenerTabPuntosAtencionPostRequestType()
                    {
                        title = objPuntosAtencionRequest.obtenerTabPuntosAtencionPostRequest.title,
                    };
                    PuntosAtencionWS.obtenerTabPuntosAtencionPostResponseType objobtenerTabPuntosAtencionPostResponseType = new PuntosAtencionWS.obtenerTabPuntosAtencionPostResponseType();
                    PuntosAtencionWS.HeaderResponseType1 objHeaderResponseType1 = new PuntosAtencionWS.HeaderResponseType1();
                    PuntosAtencionWS.HeaderResponseType objHeaderResponseType = null;
                    objPuntosAtenciontResponse = new Entity.Transac.Service.Common.GetPuntosAtencion.PuntosAtencionResponse();
                    try
                    {
                        using (new ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.SIACU_PUNTOSATENCION.InnerChannel))
                        {
                            ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                          (
                              new Claro.Entity.SecurityHeader(objPuntosAtencionRequest.Audit.Transaction, objdesencriptarResponse.usuarioAplicacion, objdesencriptarResponse.clave)
                          );
                            objHeaderResponseType = Claro.Web.Logging.ExecuteMethod<PuntosAtencionWS.HeaderResponseType>
                           (objPuntosAtencionRequest.Audit.Session, objPuntosAtencionRequest.Audit.Transaction, Configuration.ServiceConfiguration.SIACU_PUNTOSATENCION, () =>
                           {
                               return Configuration.ServiceConfiguration.SIACU_PUNTOSATENCION.obtenerTabPuntosAtencionPost(objHeaderRequest, objHeaderRequest1, objobtenerTabPuntosAtencionPostRequest, out objHeaderResponseType1, out objobtenerTabPuntosAtencionPostResponseType);
                           });
                        }

                        if (objHeaderResponseType != null && objobtenerTabPuntosAtencionPostResponseType != null)
                        {
                            List<Entity.Transac.Service.Common.GetPuntosAtencion.tabPuntosAtencionPost> listaTabPuntosAtencionPost = new List<Entity.Transac.Service.Common.GetPuntosAtencion.tabPuntosAtencionPost>();
                            if (objobtenerTabPuntosAtencionPostResponseType.responseStatus.codigoRespuesta == Constant.strCero
                                && objobtenerTabPuntosAtencionPostResponseType.responseDataObtenerTabPuntosAtencionPost.listaTabPuntosAtencionPost != null)
                            {
                                foreach (var item in objobtenerTabPuntosAtencionPostResponseType.responseDataObtenerTabPuntosAtencionPost.listaTabPuntosAtencionPost)
                                {
                                    Entity.Transac.Service.Common.GetPuntosAtencion.tabPuntosAtencionPost obj = new SECURITY.GetPuntosAtencion.tabPuntosAtencionPost();

                                    obj.codele = item.codele;
                                    obj.nombre = item.nombre;
                                    obj.tipo = item.tipo;
                                    obj.rank = item.rank;
                                    obj.cacTypeCodele = item.cacTypeCodele;
                                    obj.cacTypeTitle = item.cacTypeTitle;
                                    listaTabPuntosAtencionPost.Add(obj);
                                }
                                objListTabPuntosAtencionPost.listaTabPuntosAtencionPost = listaTabPuntosAtencionPost;
                            }

                        }
                    }


                    catch (Exception ex)
                    {
                        Claro.Web.Logging.Error(objPuntosAtencionRequest.Audit.Session, objPuntosAtencionRequest.Audit.Transaction, ex.Message);
                        throw new Claro.MessageException(ex.Message.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPuntosAtencionRequest.Audit.Session, objPuntosAtencionRequest.Audit.Transaction, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }

            return objListTabPuntosAtencionPost;
        }

        public static Entity.Transac.Service.Common.GetOffice.OfficeResponse GetOffice(Entity.Transac.Service.Common.GetOffice.OfficeRequest objOfficeRequest)
        {
            var objOfficeResponse = new OfficeResponse();
            var objOffice = new Office();
            try
            {
               DbParameter[] parameters = {
                    new DbParameter("P_CTA_RED", DbType.String, ParameterDirection.Input,objOfficeRequest.strCodeUser),  
                    new DbParameter("P_CURSOR", DbType.Object,ParameterDirection.Output)
                };
                DbFactory.ExecuteReader(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, DbConnectionConfiguration.SIAC_POST_PVU, DbCommandConfiguration.SIACU_SP_CONSULTA_PDV_USUARIO, parameters,
                    (IDataReader dr) =>
                    {
                        while (dr.Read())
                        {
                            objOffice.strCodeUserSisac = Functions.CheckStr(dr["USUAN_CODIGO"]);
                            objOffice.strCodeClasificationOffice = Functions.CheckStr(dr["TOFIC_CODIGO"]);
                            objOffice.strCodeOffice = Functions.CheckStr(dr["OVENC_CODIGO"]);
                        }
                    });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objOfficeRequest.Audit.Session, objOfficeRequest.Audit.Transaction, ex.Message);
                objOffice = null;
            }
            objOfficeResponse.objOffice = objOffice;
            return objOfficeResponse;
        }

        public static List<ListItem> GetNationalityList(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_CLARIFY, DbCommandConfiguration.SIACU_SP_BIRTHPLACE, "RESULT", "CODIGO", "NACIONALIDAD");
        }

        public static List<ListItem> GetCivilStatusList(string strIdSession, string strTransaction)
        {
            return ListItems(DbConnectionConfiguration.SIAC_POST_BSCS, DbCommandConfiguration.SIACU_BSCS_SP_CONSULTAS_MARITAL_STATUS, "RESULT", "MAS_ID", "MAS_DES");
        }

//PROY-32650
        public static Entity.Transac.Service.Common.CheckingUser.CheckingUserResponse CheckingUser(string sessionId, string IdTransaction, string IpAplication, string Aplication, string user, long AppCode)
        {

            Claro.Web.Logging.Info(sessionId, IdTransaction, string.Format("entrada al metodo CheckingUser: sessionId {0}, IdTransaction {1}, IpAplication {2}, Aplication {3}, user {4}, AppCode {5}",sessionId, IdTransaction, IpAplication, Aplication, user, AppCode));

            Entity.Transac.Service.Common.CheckingUser.CheckingUserResponse objCheckingUserResponse = new Entity.Transac.Service.Common.CheckingUser.CheckingUserResponse();
            try
            {
            List<Entity.Transac.Service.Common.ConsultSecurity> list = new List<Entity.Transac.Service.Common.ConsultSecurity>();
            CONSULTA_SECURITY.ConsultaSeguridad consulta = Configuration.ServiceConfiguration.SIACU_ConsultaSeguridad;
            CONSULTA_SECURITY.verificaUsuarioRequest request = new CONSULTA_SECURITY.verificaUsuarioRequest();
            CONSULTA_SECURITY.verificaUsuarioResponse response;

            request.idTransaccion = IdTransaction;
            request.ipAplicacion = IpAplication;
            request.aplicacion = Aplication;
            request.usuarioLogin = user;
            request.appCod = AppCode;

            CONSULTA_SECURITY.seguridadType[] seguridadType;

            response = consulta.verificaUsuario(request);
            seguridadType = response.cursorSeguridad;


                Claro.Web.Logging.Info(sessionId, IdTransaction, string.Format("El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario Parametros de Entrada: idTransaccion={0} ,IpAplication={1}, Aplication={2}, usuarioLogin={3} , AppCode={4}", IdTransaction, IpAplication, Aplication, user, AppCode));
            if (seguridadType != null && seguridadType.Length > 0)
            {
                int seguridadTypeCount = seguridadType.Length;
                for (int i = 0; i < seguridadTypeCount; i++)
                {
                    Entity.Transac.Service.Common.ConsultSecurity item = new Entity.Transac.Service.Common.ConsultSecurity();

                    item.Usuaccod = seguridadType[i].UsuacCod;
                    item.Perfccod = seguridadType[i].PerfcCod;
                    item.Usuaccodvensap = seguridadType[i].UsuacCodVenSap;
                    list.Add(item);
                        Claro.Web.Logging.Info(sessionId, IdTransaction, string.Format("elementos del objeto seguridadType: Usuaccod {0}, Perfccod {1},Usuaccodvensap {2}", seguridadType[i].UsuacCod, seguridadType[i].PerfcCod, seguridadType[i].UsuacCodVenSap));
                }

            }
            else
            {
                    Claro.Web.Logging.Error(sessionId, IdTransaction, "El objeto seguridadType esta vacio, (bloque else)");
                    //Claro.Web.Logging.Error(sessionId, IdTransaction, "El servicio ConsultaSeguridad(strWebServiceDBAUDIT) Metodo: verificaUsuario , No devuelve resultados");
            }

            objCheckingUserResponse.consultasSeguridad = list;
            objCheckingUserResponse.IdTransaccion = response.idTransaccion;
            objCheckingUserResponse.CodeErr = response.errorCode;
            objCheckingUserResponse.MsgErr = response.errorMsg;
                Claro.Web.Logging.Info(sessionId, IdTransaction, string.Format("el metodo CheckingUser, devuelve: IdTransaccion {0}, CodeErr {1}, MsgErr{2}, consultasSeguridad.count {3}", response.idTransaccion, response.errorCode, response.errorMsg, list.Count.ToString()));
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error( sessionId,  IdTransaction, "metodo CheckingUser, error: " + ex.Message + ", error largo" + ex.ToString());
            }
            

            return objCheckingUserResponse;
        }

        public static List<Entity.Transac.Service.Common.Employee> GetEmployeByUser(string IdSession, string Transaction, string UserName)
        {

            List<Entity.Transac.Service.Common.Employee> lstEmploye = null;
            try
            {
            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.DatosEmpleadoRequest objRequest = new AUDIT_SECURITY.DatosEmpleadoRequest()
            {
                login = UserName
            };

            AUDIT_SECURITY.EmpleadoResponse objEmpleadoResponse = null;
            Claro.Web.Logging.Info(IdSession, Transaction,
            string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado, Parametros de Entrada: login={0}", UserName));
            objEmpleadoResponse = objEbsAuditoria.leerDatosEmpleado(objRequest);

            if (objEmpleadoResponse != null && (objEmpleadoResponse.empleados != null && objEmpleadoResponse.empleados.item != null && objEmpleadoResponse.empleados.item.Length > 0))
            {
                AUDIT_SECURITY.EmpleadoType[] objEmpleadoType = objEmpleadoResponse.empleados.item;
                lstEmploye = new List<Entity.Transac.Service.Common.Employee>();
                foreach (AUDIT_SECURITY.EmpleadoType item in objEmpleadoType)
                {
                    lstEmploye.Add(new Employee()
                    {
                        Login = item.login,
                        UserID = int.Parse(item.codigo),
                        FullName = string.Format("{0} {1} {2}", item.nombres, item.paterno, item.materno),
                        FirstName = item.nombres,
                        LastName1 = item.paterno,
                        LastName2 = item.materno,
                        SearchUser = "0"
                    });
                        Claro.Web.Logging.Info(IdSession, Transaction, string.Format( "elementos del objeto GetEmployeByUser, items(foreach): Login {0},UserID {1},FirstName {2},LastName1 {3},LastName2 {4},SearchUser {5}", item.login, item.codigo, item.nombres, item.paterno, item.materno, "0"));
                }
            }
            else
            {
                    Claro.Web.Logging.Info(IdSession, Transaction, string.Format( "el objEmpleadoResponse esta vacio, probablemente a un error"));
                    //Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerDatosEmpleado , No devuelve resultados");
                }

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(IdSession, Transaction, "metodo GetEmployeByUser, error: " + ex.Message + ", error largo" + ex.ToString());
            }

            return lstEmploye;
        }

        public static List<Entity.Transac.Service.Common.PaginaOption> ReadOptionsByUser(string IdSession, string Transaction, int IdAplication, int IdUser)
        {
            Claro.Web.Logging.Info(IdSession, Transaction, string.Format("entrada al metodo ReadOptionsByUser: IdSession {0},  Transaction {1},  IdAplication {2},  IdUser {3}",IdSession, Transaction, IdAplication, IdUser));
            List<Entity.Transac.Service.Common.PaginaOption> lstPaginaOption = null;
            try
            {
            AUDIT_SECURITY.EbsAuditoriaClient objEbsAuditoria = Configuration.ServiceConfiguration.SECURITY_PERMISSIONS;

            AUDIT_SECURITY.PaginaOpcionesUsuarioRequest objRequest = new AUDIT_SECURITY.PaginaOpcionesUsuarioRequest();
            objRequest.aplicCod = IdAplication;
            objRequest.user = IdUser;

            AUDIT_SECURITY.PaginaOpcionesUsuarioResponse objleerOpcionesPorUsuarioResponse = null;
                Claro.Web.Logging.Info(IdSession, Transaction, string.Format("El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario, Parametros de Entrada: aplicCod={0} ,user={1}", IdAplication, IdUser));
            objleerOpcionesPorUsuarioResponse = objEbsAuditoria.leerPaginaOpcionesPorUsuario(objRequest);

            if (objleerOpcionesPorUsuarioResponse != null && objleerOpcionesPorUsuarioResponse.listaOpciones.Length > 0)
            {
                lstPaginaOption = new List<Entity.Transac.Service.Common.PaginaOption>();
                AUDIT_SECURITY.PaginaOpcionType[] arrPaginaOpcionType = objleerOpcionesPorUsuarioResponse.listaOpciones;
                foreach (AUDIT_SECURITY.PaginaOpcionType item in arrPaginaOpcionType)
                {
                    lstPaginaOption.Add(new Entity.Transac.Service.Common.PaginaOption()
                    {
                        OptionCode = item.opcicCod,
                        OptionDescription = item.opcicDes,
                        Clave = item.clave
                    });
                        Claro.Web.Logging.Info(IdSession, Transaction, string.Format(" elementos del objeto arrPaginaOpcionType, OptionCode {0}, OptionDescription {1} , Clave {2}", item.opcicCod, item.opcicDes, item.clave));
                }
            }
            else
            {
                    Claro.Web.Logging.Error(IdSession, Transaction, "El objeto objleerOpcionesPorUsuarioResponse esta vacio, probablemente a un error controlado");
                    //Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario , No devuelve resultados");
            }
            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(IdSession, Transaction, "el metodo ReadOptionsByUser, provoco un error: NET:"+ ex.Message+ ", NET largo:"+ ex.ToString());
            }


            Claro.Web.Logging.Info(IdSession, Transaction, "Sale del metodo ReadOptionsByUser y devuelve el metodo lstPaginaOption.");
            return lstPaginaOption;
        }

        /*PROY-32650 F2*/
        public static List<EntitiesFixed.GenericItem> GetParamsBSCS(string strIdSession, string strTransaction, string vCODCLIENTE, ref string MSG_ERROR)
        {

            var salida = new List<EntitiesFixed.GenericItem>();
            DbParameter[] parameters = 
            {
                new DbParameter("PI_CAMPO", DbType.String, 24, ParameterDirection.Input, vCODCLIENTE),
                new DbParameter("PO_CODE_RESULT", DbType.Int32, ParameterDirection.Output),												   
                new DbParameter("PO_MESSAGE_RESULT", DbType.String,255, ParameterDirection.Output),
                new DbParameter("PO_CURSOR", DbType.Object, ParameterDirection.Output)
            };

            try
            {
                Web.Logging.ExecuteMethod(strIdSession, strTransaction, () =>
                {
                    DbFactory.ExecuteReader(strIdSession, strTransaction, DbConnectionConfiguration.SIAC_POST_BSCS,
                        DbCommandConfiguration.SIACU_SIACSS_PARAMETROSBSCS, parameters, reader =>
                        {
                            while (reader.Read())
                            {
                                var item = new EntitiesFixed.GenericItem
                                {

                                    Descripcion = Functions.CheckStr(reader["campo"]),
                                    Descripcion2 = Functions.CheckStr(reader["descripcion"]),
                                    Codigo = Functions.CheckStr(reader["valor1"]),
                                    Codigo2 = Functions.CheckStr(reader["valor2"]),
                                    Codigo3 = Functions.CheckStr(reader["valor3"]),
                                    Tipo = Functions.CheckStr(reader["valor4"]),
                                    Nombre = Functions.CheckStr(reader["valor5"]),
                                    Agrupador = Functions.CheckStr(reader["valor6"])
                                };

                                salida.Add(item);
                            }
                        });
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(strIdSession, strTransaction, ex.Message);
            }
            finally
            {
                MSG_ERROR = parameters[parameters.Length - 1].Value.ToString();
            }

            return salida;
        }


        public static List<Entity.Transac.Service.Common.Employee> GetEmployeByUserwithDP(string IdSession, string Transaction, string UserName, string IPAddress, string aplicacion, string user, string pass)
        {
            Claro.Web.Logging.Info(IdSession, Transaction, string.Format("metodo GetEmployeByUserwithDP, parametros de entrada  IdSession {0},  Transaction {1},  UserName {2},  IPAddress {3},  aplicacion {4},  user {5},  pass {6}", IdSession,  Transaction,  UserName,  IPAddress,  aplicacion,  user,  pass));
            RequestOpcionalComplexType[] listaRequestOpcional = new RequestOpcionalComplexType[1];

            listaRequestOpcional[0] = new RequestOpcionalComplexType() { clave = "", valor = "" };

            List<Entity.Transac.Service.Common.Employee> lstEmploye = null;

            try
            {
                HeaderRequestType objHeaderRequest = new HeaderRequestType()
                {
                    country = Claro.ConfigurationManager.AppSettings("consCountry"),//"PE" VERIFICAR SI EXISTE ESTA LLAVE EN PRODUCCION
                    language = Claro.ConfigurationManager.AppSettings("consLanguage"),//"ES",
                    consumer = Claro.ConfigurationManager.AppSettings("NombreAplicacion"),//"TCRM"
                    system = string.Empty,
                    modulo = string.Empty,
                    pid = Transaction,
                    userId = UserName,
                    dispositivo = "MOVIL",
                    wsIp = IPAddress, 
                    operation = "desbloqueoLinea",
                    timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                    msgType = "Request",
                    VarArg = new ArgType[1]
                            {
                                new ArgType()
                                {
                                    key = String.Empty,
                                    value = String.Empty,
                                }
                            },

                };
                leerDatosEmpleado objleerDatosEmpleado = new leerDatosEmpleado()
                {
                    audit = new AuditRequest()
                    {
                        idTransaccion = Transaction,
                        ipAplicacion = IPAddress,
                        usrAplicacion = UserName,
                        aplicacion = aplicacion


                    },
                    DatosEmpleadoRequest = new DatosEmpleadoRequest()
                    {
                        login = UserName
                    },
                    listaOpcionalRequest = listaRequestOpcional
                };


                leerDatosEmpleadoResponse objleerDatosEmpleadoResponse1 = null;
                HeaderResponseType objHeaderResponseType = null;




                using (new System.ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.LDE.InnerChannel))
                {
                    System.ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                  (
                      new Claro.Entity.SecurityHeader(Transaction, user, pass)
                  );
                    objHeaderResponseType = Web.Logging.ExecuteMethod<HeaderResponseType>(IdSession, Transaction, () =>
                    {
                        return ServiceConfiguration.LDE.leerDatosEmpleado(objHeaderRequest, objleerDatosEmpleado, out objleerDatosEmpleadoResponse1);
                    });
                }
                if (objHeaderResponseType.Status.code == Claro.Constants.NumberZeroString)
                {
                    if (objleerDatosEmpleadoResponse1.audit.codigoRespuesta == Claro.Constants.NumberZeroString)
                    {

                        if (objleerDatosEmpleadoResponse1.EmpleadoResponse != null && (objleerDatosEmpleadoResponse1.EmpleadoResponse.empleados != null))
                        {
                            LDEDP.EmpleadoType[] objEmpleadoType = objleerDatosEmpleadoResponse1.EmpleadoResponse.empleados;
                            lstEmploye = new List<Entity.Transac.Service.Common.Employee>();
                            foreach (LDEDP.EmpleadoType item in objEmpleadoType)
                            {
                                lstEmploye.Add(new Employee()
                                {
                                    Login = item.login,
                                    UserID = int.Parse(item.codigo),
                                    FullName = string.Format("{0} {1} {2}", item.nombres, item.paterno, item.materno),
                                    FirstName = item.nombres,
                                    LastName1 = item.paterno,
                                    LastName2 = item.materno,
                                    SearchUser = "0"
                                });
                                Claro.Web.Logging.Info(IdSession, Transaction, string.Format("metodo GetEmployeByUserwithDP, elementos del objeto: objEmpleadoType(foreach, al final es lo que devuelve), Login {0},UserID {1},FullName {2},FirstName {3},LastName1 {4},LastName2 {5},SearchUser {6}", item.login, item.codigo, item.nombres + item.paterno + item.materno, item.nombres, item.paterno, item.materno, "0"));
                            }
                        }
                    }

                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "metodo GetEmployeByUserwithDP, error: "+ex.Message + ", error largo"+ ex.ToString());
                

            }




            return lstEmploye;
        }
        


        public static List<Entity.Transac.Service.Common.PaginaOption> ReadOptionsByUserwithDP(string IdSession, string Transaction, string UserName, string IPAddress, string aplicacion, int idAplicacion, int IdUser, string user, string pass)
        {
            Claro.Web.Logging.Info(IdSession, Transaction, string.Format("Entrada al metodo: ReadOptionsByUserwithDP en la clase Common del backend:  IdSession {0},  Transaction {1},  UserName {2},  IPAddress {3},  aplicacion {4},  idAplicacion {5},  IdUser {6},  user {7},  pass {8} ", IdSession,  Transaction,  UserName,  IPAddress,  aplicacion,  idAplicacion,  IdUser,  user,  pass));
            LPOPU.RequestOpcionalComplexType[] listaRequestOpcional = new LPOPU.RequestOpcionalComplexType[1];

            listaRequestOpcional[0] = new LPOPU.RequestOpcionalComplexType() { clave = "", valor = "" };
            List<Entity.Transac.Service.Common.PaginaOption> lstPaginaOption = null;




            #region RequesetDP
            var headerRequestType = new LPOPU.HeaderRequestType()
            {
                country = Claro.ConfigurationManager.AppSettings("consCountry"),//"PE" VERIFICAR SI EXISTE ESTA LLAVE EN PRODUCCION
                language = Claro.ConfigurationManager.AppSettings("consLanguage"),//"ES",
                consumer = Claro.ConfigurationManager.AppSettings("NombreAplicacion"),//"TCRM"
                system = string.Empty,
                modulo = string.Empty,
                pid = Transaction,
                userId = UserName,
                dispositivo = "MOVIL",
                wsIp = IPAddress,
                operation = "leer opciones",
                timestamp = DateTime.Parse(string.Format("{0:u}", DateTime.UtcNow)),
                msgType = "Request",
                VarArg = new LPOPU.ArgType[1]
                            {
                                new LPOPU.ArgType()
                                {
                                    key = String.Empty,
                                    value = String.Empty,
                                }
                            },
            };
           
            var leerPaginaOpcionesPorUsuario = new LPOPU.leerPaginaOpcionesPorUsuario()
            {

                audit = new LPOPU.AuditRequest()
                {
                    idTransaccion = Transaction,
                    ipAplicacion = IPAddress,
                    usrAplicacion = UserName,
                    aplicacion = aplicacion
                },
                PaginaOpcionesUsuarioRequest = new LPOPU.PaginaOpcionesUsuarioRequest()
                {
                    user = IdUser,
                    aplicCod = idAplicacion
                },
                listaOpcionalRequest = listaRequestOpcional
            };
            #endregion

            #region ResponseDP
            var HeaderResponseType = new LPOPU.HeaderResponseType();
            var leerPaginaOpcionesPorUsuarioResponse = new LPOPU.leerPaginaOpcionesPorUsuarioResponse();
            #endregion
            try
            {
                using (new ServiceModel.OperationContextScope(Configuration.ServiceConfiguration.SECURITY_PERMISSIONSDP.InnerChannel))
                {
                    ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add(
                        new Claro.Entity.SecurityHeader(Transaction, user, pass));

                    HeaderResponseType = Claro.Web.Logging.ExecuteMethod<LPOPU.HeaderResponseType>("Session", "Transaction", Configuration.ServiceConfiguration.SECURITY_PERMISSIONSDP, () =>
                    {
                        return Configuration.ServiceConfiguration.SECURITY_PERMISSIONSDP.leerPaginaOpcionesPorUsuario
                            (headerRequestType, leerPaginaOpcionesPorUsuario, out leerPaginaOpcionesPorUsuarioResponse);
                    });
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, Transaction, "El servicio EbsAuditoriaClient(strWebServiceSecurityPermissions) Metodo: leerPaginaOpcionesPorUsuario , error de .NET:" + ex.Message + ", NET error largo: "+ ex.ToString());
            }

            if (leerPaginaOpcionesPorUsuarioResponse != null && leerPaginaOpcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse.listaOpciones.Length > 0)
            {
                Claro.Web.Logging.Info(IdSession, Transaction, "El objeto leerPaginaOpcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse.listaOpciones.Length contiene: " + leerPaginaOpcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse.listaOpciones.Length.ToString()+ " registros");
                lstPaginaOption = new List<Entity.Transac.Service.Common.PaginaOption>();
                foreach (var item in leerPaginaOpcionesPorUsuarioResponse.PaginaOpcionesUsuarioResponse.listaOpciones)
                {
                    lstPaginaOption.Add(new Entity.Transac.Service.Common.PaginaOption()
                    {
                        Clave = item.clave,
                        OptionDescription = item.opcicDes,
                        OptionCode = item.opcicCod
                    });
                }
            }

            return lstPaginaOption;
        }

        public static bool IsOkGetKey(string idSession, string idTransaccion, string ipAplicacion, string ipTransicion, string usrAplicacion, string idAplicacion, out string User, out string Pass)
        {

            try
            {
                Claro.Web.Logging.Info(idSession, idTransaccion, string.Format("entrada al metodo: public static bool IsOkGetKey(),  idSession {0},  idTransaccion {1},  ipAplicacion {2},  ipTransicion {3},  usrAplicacion {4}, idAplicacion {5}, out  User {6}, out  Pass {7}.", idSession, idTransaccion, ipAplicacion, ipTransicion, usrAplicacion, idAplicacion,"es valor de salida","es valor de salida" ));
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = ConfigurationManager.AppSettings("strRestricTetheVelocWS");
                string cryptoUser = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("User", "").ToString();
                string cryptoPass = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("Password", "").ToString();
                desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                {
                    idTransaccion = idTransaccion,
                    ipAplicacion = ipAplicacion,
                    ipTransicion = ipTransicion,
                    usrAplicacion = usrAplicacion,
                    idAplicacion = idAplicacion,
                    codigoAplicacion = ConfigurationManager.AppSettings("strCodigoAplicacion"),
                    usuarioAplicacionEncriptado = cryptoUser,
                    claveEncriptado = cryptoPass,
                };
                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );
                User = objdesencriptarResponse.usuarioAplicacion;
                Pass = objdesencriptarResponse.clave;
                Claro.Web.Logging.Info(idSession, idTransaccion, string.Format("metodo IsOkGetKey, variables internas: strKey {0},cryptoUser {1},cryptoPass {2},codigoAplicacion(llave de config) {3},User {4},Pass {5}", strKey, cryptoUser, cryptoPass, objdesencriptarRequest.codigoAplicacion, User, Pass));
                Claro.Web.Logging.Info(idSession, idTransaccion, string.Format("metodo IsOkGetKey, valores del objeto objdesencriptarResponse: mensajeResultado{0},usuarioAplicacion{1},clave{2}", objdesencriptarResponse.mensajeResultado, objdesencriptarResponse.usuarioAplicacion, objdesencriptarResponse.clave));
                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(idSession, idTransaccion, "metodo IsOkGetKey, retorna true.");
                    return true;

                }
            }

            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, idTransaccion, "error del metodo IsOkGetKey: "+ ex.Message);
                User = "";
                Pass = "";
                return false;
            }
            Claro.Web.Logging.Info(idSession, idTransaccion, "metodo IsOkGetKey, retorna false.");
            return false;

        }

        #region  PROY-140245-IDEA140240
        /// <summary>
        /// Metodo que valida el colaborador.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>GetValidateCollaboratorResponse</returns>
        /// <remarks>GetValidateCollaborator</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>20/05/2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static Entity.Transac.Service.Common.GetValidateCollaborator.GetValidateCollaboratorResponse GetValidateCollaborator(GetValidateCollaboratorRequest objRequest, string strIdSession)
        {
            GetValidateCollaboratorResponse response = null;
           try
           {
               Claro.Web.Logging.Info(strIdSession, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.ValidateCollaboratorAuditRequest.IdTransaction, String.Format("[GetValidateCollaborator] Parametros Entrada: tipoDocumento:{0}, numeroDocumento:{1}, casoEspecial:{2}", objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.KindDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.NumberDocument, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.CaseSpecial));

               response = RestService.PostInvoque<Entity.Transac.Service.Common.GetValidateCollaborator.GetValidateCollaboratorResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.VALIDAR_USUARIO, objRequest, objRequest, true);

               Claro.Web.Logging.Info(strIdSession, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.ValidateCollaboratorAuditRequest.IdTransaction, String.Format("[GetValidateCollaborator] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.ValidateCollaboratorMessageResponse.ValidateCollaboratorBodyResponse.ValidateCollaboratorResponse.ValidateCollaboratorAuditResponse.IdTransaction, response.ValidateCollaboratorMessageResponse.ValidateCollaboratorBodyResponse.ValidateCollaboratorResponse.ValidateCollaboratorAuditResponse.CodeResponse, response.ValidateCollaboratorMessageResponse.ValidateCollaboratorBodyResponse.ValidateCollaboratorResponse.ValidateCollaboratorAuditResponse.MessageResponse));
           }
           catch (Exception ex)
           {
               Claro.Web.Logging.Error(strIdSession, objRequest.ValidateCollaboratorMessageRequest.ValidateCollaboratorBodyRequest.ValidateCollaboratorRequest.ValidateCollaboratorAuditRequest.IdTransaction, ex.Message);
               string sep = " - ";
               int posResponse = ex.Message.IndexOf(sep);
               string result = ex.Message.Substring(posResponse + sep.Length);
               response = JsonConvert.DeserializeObject<GetValidateCollaboratorResponse>(result);
           }
            return response;
        }
        /// <summary>
        /// Metodo que consulta los bonos de incremento de velocidad
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        /// <item><mot>PROY-140319 IDEA-140331 Bono aumenta velocidad Internet fijo</mot></item>
        public static Entity.Transac.Service.Common.GetConsultServiceBono.GetConsultServiceBonoResponse GetConsultServiceBono(GetConsultServiceBonoRequest objRequest, string strIdSession)
        {


            GetConsultServiceBonoResponse response = null;
            try
            {
                string[] parametro = { objRequest.CoId };

                response = RestService.GetInvoque<Entity.Transac.Service.Common.GetConsultServiceBono.GetConsultServiceBonoResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CONSULTAR_SERVICIO_BONO, objRequest.Audit, null, parametro);
                var responseLog = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                Claro.Web.Logging.Info(strIdSession, objRequest.Audit.Transaction, string.Format("Response GetConsultServiceBono : {0} ", responseLog));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.Audit.Transaction, ex.Message);

            }

            return response;
        }
        /// <summary>
        /// Metodo que Registra el Bono Incremento de velocidad
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns></returns>
        public static Entity.Transac.Service.Common.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse GetRegisterBonoSpeed(GetRegisterBonoSpeedRequest objRequest, string strIdSession)
        {
            GetRegisterBonoSpeedResponse objResponse = null;
            try
            {
                string.Format("[GetRegisterBonoSpeed] Parametros Entrada: bonoId:{0}, coId:{1}, periodo:{2}",

                    objRequest.RegisterBonoSpeedMessageRequest.RegisterBonoSpeedBodyRequest.BonoId,
                    objRequest.RegisterBonoSpeedMessageRequest.RegisterBonoSpeedBodyRequest.CoId,
                    objRequest.RegisterBonoSpeedMessageRequest.RegisterBonoSpeedBodyRequest.Periodo);

                objResponse = RestService.PostInvoque<Entity.Transac.Service.Common.GetRegisterBonoSpeed.GetRegisterBonoSpeedResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.REGISTRAR_BONO_INCR_VELOCIDAD, objRequest.Audit, objRequest, true);

            }
            catch (Exception ex)
            {
               // Claro.Web.Logging.Error(strIdSession, objRequest.CoId, ex.Message);
                string sep = "-";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<GetRegisterBonoSpeedResponse>(result);

            }
            return objResponse;
        }
        /// <summary>
        /// [PostValidarEntregaBAV] Método para consultar a los clientes que cuentan con Bono Aumenta Velocidad por retención y fidelización.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>PostValidarEntregaBAVResponse</returns>
        /// <remarks>GetValidateCollaborator</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>/03/2020.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>05/03/2020.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-""- Convivencia de bono</Mot></item></list>
        public static Entity.Transac.Service.Common.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse PostValidateDeliveryBAV(PostValidateDeliveryBAVRequest objRequest, string strIdSession)
        {
            PostValidateDeliveryBAVResponse objResponse = null;
            try
            {
                string.Format("[PostValidateDeliveryBAV] Parametros de Entrada: coId:{0}, meses:{1}, codSubClase:{2}",
                    objRequest.validateDeliveryBAVMessageRequest.validateDeliveryBAVBodyRequest.CoId,
                    objRequest.validateDeliveryBAVMessageRequest.validateDeliveryBAVBodyRequest.Meses,
                    objRequest.validateDeliveryBAVMessageRequest.validateDeliveryBAVBodyRequest.CodSubClase);

                objResponse = RestService.PostInvoque<Entity.Transac.Service.Common.PostValidateDeliveryBAV.PostValidateDeliveryBAVResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.VALIDATE_DELIVERY_BAV, objRequest.Audit, objRequest, true);
                var responseLog = Newtonsoft.Json.JsonConvert.SerializeObject(objResponse);
                Claro.Web.Logging.Info(strIdSession, objRequest.Audit.Transaction, string.Format("Response Metodo PostValidateDeliveryBAV : {0}", responseLog));
            }
            catch (Exception ex)
            {
                string sep = "-";
                int postResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(postResponse + sep.Length);
                objResponse = JsonConvert.DeserializeObject<PostValidateDeliveryBAVResponse>(result);
                Claro.Web.Logging.Error(strIdSession, objRequest.Audit.Transaction, string.Format("Metodo PostValidateDeliveryBAV, Error: ", objResponse));
            }

            return objResponse;
        }
        /// <summary>
        /// Metodo que consulta las campanias.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>GetConsultCampaignResponse</returns>
        /// <remarks>GetConsultCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static Entity.Transac.Service.Common.GetConsultCampaign.GetConsultCampaignResponse GetConsultCampaign(GetConsultCampaignRequest objRequest, string strIdSession)
        {
            GetConsultCampaignResponse response = null;
            try
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaignAuditRequest.idTransaction, String.Format("[GetConsultCampaign] Parametros Entrada: numLinea:{0}, tipoDoc:{1}, nroDoc:{2}, coId:{3}, tipoPrdCod:{4}, nroPed:{5}, nroPedDet:{6}, nroCont:{7}, nroContDet:{8}",
                    objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NumLine, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.KindDoc, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NroDoc,
                    objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.CoId, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.KindPrdCod, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NroPed,
                    objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NroPedDet, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NroCont, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaign.NroContDet));

                response = RestService.PostInvoque<Entity.Transac.Service.Common.GetConsultCampaign.GetConsultCampaignResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.CONSULTAR_CAMPANIA, objRequest, objRequest, true);

                Claro.Web.Logging.Info(strIdSession, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaignAuditRequest.idTransaction, String.Format("[GetConsultCampaign] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.ConsultCampaignMessageResponse.ConsultCampaignBodyResponse.ConsultCampaignResponse.ConsultCampaignAuditResponse.IdTransaction,
                    response.ConsultCampaignMessageResponse.ConsultCampaignBodyResponse.ConsultCampaignResponse.ConsultCampaignAuditResponse.CodeResponse, response.ConsultCampaignMessageResponse.ConsultCampaignBodyResponse.ConsultCampaignResponse.ConsultCampaignAuditResponse.MessageResponse));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.ConsultCampaignMessageRequest.ConsultCampaignBodyRequest.ConsultCampaignRequest.ConsultCampaignAuditRequest.idTransaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<GetConsultCampaignResponse>(result);
            }
            return response;
        }
        /// <summary>
        /// Metodo que registra la campania.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>GetRegisterCampaignResponse</returns>
        /// <remarks>GetRegisterCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static Entity.Transac.Service.Common.GetRegisterCampaign.GetRegisterCampaignResponse GetRegisterCampaign(GetRegisterCampaignRequest objRequest, string strIdSession)
        {
            GetRegisterCampaignResponse response = null;
            try
            {
                Claro.Web.Logging.Info(strIdSession, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaignAuditRequest.IdTransaction, String.Format("[GetRegisterCampaign] Parametros Entrada: tipoDocumento:{0}, nroDocumento:{1}, nroLinea:{2}, nroSec:{3}, nroPed:{4}, nroPedDet:{5}, nroCont:{6}, nroContDet:{7}, planCodigo:{8}, planDescripcion:{9}, tipoPrdCodigo:{10}, tipoPrdDescripcion:{11}, campaniaCodigo:{12}, campaniaDescripcion:{13}, coId:{14},  tipoOpeCodigo:{15}, tipoOpeDescripcion:{16}, estado:{17}, usuarioCrea:{18}, fechaCrea:{19}, usuarioModifica:{20}, fechaModifica:{21}",
                objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.KindDocument, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroDocument, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroLine, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroSec, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroPed,
                objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroPedDet, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroCont, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.NroContDet, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.PlanCode, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.PlanDescription,
                objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.KindPrdCode, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.KindPrdDescription, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.CampaignCode, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.CampaignDescription, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.CoId,
                objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.KindOpeCode, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.KindOpeDescription, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.estado, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.UserCreates, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.DateCreates,
                objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.UserModify, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaign.DateModify));

                response = RestService.PostInvoque<Entity.Transac.Service.Common.GetRegisterCampaign.GetRegisterCampaignResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.REGISTRAR_CAMPANIA, objRequest, objRequest, true);

                Claro.Web.Logging.Info(strIdSession, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaignAuditRequest.IdTransaction, String.Format("[GetRegisterCampaign] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.RegisterCampaignMessageResponse.RegisterCampaignBodyResponse.RegisterCampaignResponse.RegisterCampaignAuditResponse.IdTransaction, response.RegisterCampaignMessageResponse.RegisterCampaignBodyResponse.RegisterCampaignResponse.RegisterCampaignAuditResponse.CodeResponse, response.RegisterCampaignMessageResponse.RegisterCampaignBodyResponse.RegisterCampaignResponse.RegisterCampaignAuditResponse.MessageResponse));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.RegisterCampaignMessageRequest.RegisterCampaignBodyRequest.RegisterCampaignRequest.RegisterCampaignAuditRequest.IdTransaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<GetRegisterCampaignResponse>(result);
            }
            return response;
        }
        /// <summary>
        /// Obtiene datos del usuario
        /// </summary>
        /// <param name="objReadDataUserRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>GetReadDataUserResponse</returns>
        /// <remarks>GetReadDataUser</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static GetReadDataUserResponse GetReadDataUser(GetReadDataUserRequest objReadDataUserRequest, string strIdSession)
        {
            SECURITY.GetReadDataUser.GetReadDataUserResponse model = new SECURITY.GetReadDataUser.GetReadDataUserResponse();
            try
            {
                EntitiesOpcionesAuditoria.RequestOpcionalComplexType[] arrRequestOpcionalComplexType = new EntitiesOpcionesAuditoria.RequestOpcionalComplexType[1];
                arrRequestOpcionalComplexType[0] = new EntitiesOpcionesAuditoria.RequestOpcionalComplexType() { clave = "", valor = "" };

                OPTIONAUDIT.leerDatosUsuario leerDatosUsuario = new OPTIONAUDIT.leerDatosUsuario()
                {
                    audit = new EntitiesOpcionesAuditoria.AuditRequest()
                    {
                        idTransaccion = objReadDataUserRequest.Audit.Transaction,
                        ipAplicacion = objReadDataUserRequest.Audit.IPAddress,
                        aplicacion = objReadDataUserRequest.Audit.ApplicationName,
                        usrAplicacion = objReadDataUserRequest.Audit.UserName
                    },
                    AccesoRequest = new EntitiesOpcionesAuditoria.AccesoRequest()
                    {
                        aplicacion = objReadDataUserRequest.aplicacion,
                        usuario = objReadDataUserRequest.Audit.UserName
                    },
                    listaOpcionalRequest = new EntitiesOpcionesAuditoria.RequestOpcionalComplexType[1]{
                        new EntitiesOpcionesAuditoria.RequestOpcionalComplexType() { clave = string.Empty, valor = string.Empty },
                    }
                };

                OPTIONAUDIT.leerDatosUsuarioResponse datosUsuarioResponse = null;
                try
                {

                    Claro.Web.Logging.Info(strIdSession, leerDatosUsuario.audit.idTransaccion, String.Format("[IsUserLogin] Parametros Entrada: Aplicacion:{0}, Usuario:{1}", leerDatosUsuario.AccesoRequest.aplicacion, leerDatosUsuario.AccesoRequest.usuario));

                    datosUsuarioResponse = Claro.Web.Logging.ExecuteMethod(objReadDataUserRequest.Audit.Session, objReadDataUserRequest.Audit.Transaction,WebServiceConfiguration.OpcionesAuditoria, () =>
                    {
                        return WebServiceConfiguration.OpcionesAuditoria.leerDatosUsuario(leerDatosUsuario);

                    });

                    Claro.Web.Logging.Info(strIdSession, leerDatosUsuario.audit.idTransaccion, String.Format("[IsUserLogin] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", datosUsuarioResponse.audit.idTransaccion, datosUsuarioResponse.audit.codigoRespuesta, datosUsuarioResponse.audit.mensajeRespuesta));
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(strIdSession, leerDatosUsuario.audit.idTransaccion, ex.Message);
                    throw new Claro.MessageException(ex.Message.ToString());
                }

                model.auditResponse = new SECURITY.GetReadDataUser.AuditResponse();

                model.auditResponse.codigoRespuesta = datosUsuarioResponse.audit.codigoRespuesta;
                model.auditResponse.idTransaccion = datosUsuarioResponse.audit.idTransaccion;
                model.auditResponse.mensajeRespuesta = datosUsuarioResponse.audit.mensajeRespuesta;


                if (model.auditResponse.codigoRespuesta == Claro.SIACU.Transac.Service.Constants.strUno)
                {
                    model.accessResponse = new SECURITY.GetReadDataUser.AccessResponse();
                    model.accessResponse.auditoria = new SECURITY.Auditoria();
                    model.accessResponse.employee = new SECURITY.Employee();

                    for (int i = 0; i < datosUsuarioResponse.AccesoResponse.auditoria.AuditoriaItem.Length; i++)
                    {
                        model.accessResponse.auditoria.codigo = datosUsuarioResponse.AccesoResponse.auditoria.AuditoriaItem[i].codigo;
                        model.accessResponse.auditoria.perfil = datosUsuarioResponse.AccesoResponse.auditoria.AuditoriaItem[i].perfil;
                    }
                    model.accessResponse.employee.nombres = datosUsuarioResponse.AccesoResponse.empleado.nombres;
                    model.accessResponse.employee.paterno = datosUsuarioResponse.AccesoResponse.empleado.paterno;
                    model.accessResponse.employee.materno = datosUsuarioResponse.AccesoResponse.empleado.materno;
                    model.accessResponse.employee.correo = datosUsuarioResponse.AccesoResponse.empleado.correo;
                    model.accessResponse.employee.codigoArea = datosUsuarioResponse.AccesoResponse.empleado.codigoArea;
                    model.accessResponse.employee.descripcionArea = datosUsuarioResponse.AccesoResponse.empleado.descripcionArea;
                    model.accessResponse.employee.telefono = datosUsuarioResponse.AccesoResponse.empleado.telefono;
                    model.accessResponse.employee.dni = datosUsuarioResponse.AccesoResponse.empleado.dni;

                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdSession, ex.Message);
                throw new Claro.MessageException(ex.Message.ToString());
            }
            return model;
        }
        /// <summary>
        /// Metodo que valida cantidad campania.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <param name="strIdSession"></param>
        /// <returns>GetValidateQuantityCampaignResponse</returns>
        /// <remarks>GetValidateQuantityCampaign</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static Entity.Transac.Service.Common.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse GetValidateQuantityCampaign(GetValidateQuantityCampaignRequest objRequest, string strIdSession)
        {
            GetValidateQuantityCampaignResponse response = null;
            try
            {
               

                Claro.Web.Logging.Info(strIdSession, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.ValidateQuantityCampaignAuditRequest.IdTransaction, String.Format("[GetValidateQuantityCampaign] Parametros Entrada: tipoDocumento:{0}, numeroDocumento:{1}, Campania{2}, TipoOperacion{3}, CodAplicativo{4}, CodTipoProducto{5}, DescTipoProducto{6}",
                    objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.KindDocument, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.NumDocument, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.CaseSpecial, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.KindOperation,
                    objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.CodAplica, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.CodKindProduc, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.DescKindProduc));

                response = RestService.PostInvoque<Entity.Transac.Service.Common.GetValidateQuantityCampaign.GetValidateQuantityCampaignResponse>(Claro.SIACU.Data.Transac.Service.Configuration.RestServiceConfiguration.VALIDAR_CANTIDAD_MAXIMA, objRequest, objRequest, true);

                Claro.Web.Logging.Info(strIdSession, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.ValidateQuantityCampaignAuditRequest.IdTransaction, String.Format("[GetValidateQuantityCampaign] Parametros Salida: IdTransaccion:{0}, CodigoRespuesta:{1}, MensajeRespuesta:{2}", response.ValidateQuantityCampaignMessageResponse.ValidateQuantityCampaignBodyResponse.ValidateQuantityCampaignResponse.ValidateQuantityCampaignAuditResponse.IdTransaction, response.ValidateQuantityCampaignMessageResponse.ValidateQuantityCampaignBodyResponse.ValidateQuantityCampaignResponse.ValidateQuantityCampaignAuditResponse.CodeResponse, response.ValidateQuantityCampaignMessageResponse.ValidateQuantityCampaignBodyResponse.ValidateQuantityCampaignResponse.ValidateQuantityCampaignAuditResponse.MessageResponse));
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.ValidateQuantityCampaignMessageRequest.ValidateQuantityCampaignBodyRequest.ValidateQuantityCampaignRequest.ValidateQuantityCampaignAuditRequest.IdTransaction, ex.Message);
                string sep = " - ";
                int posResponse = ex.Message.IndexOf(sep);
                string result = ex.Message.Substring(posResponse + sep.Length);
                response = JsonConvert.DeserializeObject<GetValidateQuantityCampaignResponse>(result);
            }
            return response;
        }
        /// <summary>
        /// Metodo que envia la notificación.
        /// </summary>
        /// <param name="objRequest"></param>
        /// <returns>EmailSubmitResponse</returns>
        /// <remarks>EmailSubmit</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static Entity.Transac.Service.Common.GetSubmitNotificationEmail.EmailSubmitResponse EmailSubmit(Entity.Transac.Service.Common.GetSubmitNotificationEmail.EmailSubmitRequest objRequest)
        {
            Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "Begin a EmailSubmitResponse()");
            EntitiesCommon.GetSubmitNotificationEmail.EmailSubmitResponse objResponse = null;
            COMMON_EnviarNotificacion.emailSubmitResponseType objEmailSubmitResponse = new COMMON_EnviarNotificacion.emailSubmitResponseType();
            COMMON_EnviarNotificacion.HeaderResponseType objHeaderResponse = null;
            try
            {
                string idTransaction = objRequest.Audit.Session;
                desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
                
                objdesencriptarResponse = GetDecryptCredentialDPSIACU(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Audit.IPAddress, objRequest.Audit.UserName, objRequest.Audit.ApplicationName);

                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    COMMON_EnviarNotificacion.HeaderRequestType objHeaderRequestType = new COMMON_EnviarNotificacion.HeaderRequestType()
                    {
                        country = objRequest.Header.HeaderRequest.country,
                        language = objRequest.Header.HeaderRequest.language,
                        consumer = objRequest.Header.HeaderRequest.consumer,
                        system = objRequest.Header.HeaderRequest.system,
                        modulo = objRequest.Header.HeaderRequest.modulo,
                        pid = objRequest.Header.HeaderRequest.pid,
                        userId = objRequest.Header.HeaderRequest.userId,
                        dispositivo = objRequest.Header.HeaderRequest.dispositivo,
                        wsIp = KEY.AppSettings("strWsIpEnviarNotificacion"),
                        operation = objRequest.Header.HeaderRequest.operation,
                        timestamp = DateTime.Now,
                        msgType = objRequest.Header.HeaderRequest.msgType,
                        VarArg = new COMMON_EnviarNotificacion.ArgType[]
                        {
                            new COMMON_EnviarNotificacion.ArgType()
                            {
                                key = string.Empty,
                                value = string.Empty
                            }
                        }
                    };

                    COMMON_EnviarNotificacion.emailSubmitRequestType objEmailSubmitRequest = new COMMON_EnviarNotificacion.emailSubmitRequestType()
                    {
                        CampaignName = objRequest.CampaignName,
                        CampaignDesc = objRequest.CampaignDesc,
                        CampaignCategory = objRequest.CampaignCategory,
                        PromotionalCategory = objRequest.PromotionalCategory,
                        CallBackURL = objRequest.CallBackURL,
                        ScheduledDateTime = Convert.ToDate(objRequest.ScheduledDateTime),
                        ExpiryDateTime = Convert.ToDate(objRequest.ExpiryDateTime)
                    };
                    if (objRequest.SenderRequest != null)
                    {
                        objEmailSubmitRequest.Sender = new COMMON_EnviarNotificacion.emailSubmitRequestTypeSender()
                        {
                            Username = objRequest.SenderRequest.Username,
                            Password = objRequest.SenderRequest.Password,
                            FromAddress = objRequest.SenderRequest.FromAddress,
                            DisplayName = objRequest.SenderRequest.DisplayName
                        };
                    }
                    if (objRequest.MsgDetailsResquest != null)
                    {
                        objEmailSubmitRequest.MsgDetails = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetails()
                        {
                            Subject = objRequest.MsgDetailsResquest.Subject,
                            ReadReply = objRequest.MsgDetailsResquest.ReadReply,
                            DeliveryReport = objRequest.MsgDetailsResquest.DeliveryReport
                        };
                        if (objRequest.MsgDetailsResquest.SimpleRequest != null)
                        {
                            objEmailSubmitRequest.MsgDetails.Simple = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetailsSimple()
                            {
                                Content = objRequest.MsgDetailsResquest.SimpleRequest.Content,
                                Attachments = objRequest.MsgDetailsResquest.SimpleRequest.Attachments
                            };
                            if (objRequest.MsgDetailsResquest.SimpleRequest.RecipientsRequest != null)
                            {
                                objEmailSubmitRequest.MsgDetails.Simple.Recipients = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetailsSimpleRecipients()
                                {
                                    Contact = objRequest.MsgDetailsResquest.SimpleRequest.RecipientsRequest.Contact,
                                    ContactGroup = objRequest.MsgDetailsResquest.SimpleRequest.RecipientsRequest.ContactGroup,
                                    Recipient = objRequest.MsgDetailsResquest.SimpleRequest.RecipientsRequest.Recipient,
                                    FileURL = objRequest.MsgDetailsResquest.SimpleRequest.RecipientsRequest.FileURL
                                };
                            }
                        }
                        if (objRequest.MsgDetailsResquest.TemplateRequest != null)
                        {
                            objEmailSubmitRequest.MsgDetails.Template = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetailsTemplate()
                            {
                                Name = objRequest.MsgDetailsResquest.TemplateRequest.Name,
                                DataFileURL = objRequest.MsgDetailsResquest.TemplateRequest.DataFileURL
                            };
                            if (objRequest.MsgDetailsResquest.TemplateRequest.SFTPFileURLRequest != null)
                            {
                                objEmailSubmitRequest.MsgDetails.Template.SFTPFileURL = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetailsTemplateSFTPFileURL()
                                {
                                    SFTPDomain = objRequest.MsgDetailsResquest.TemplateRequest.SFTPFileURLRequest.SFTPDomain,
                                    SFTPUsername = objRequest.MsgDetailsResquest.TemplateRequest.SFTPFileURLRequest.SFTPUsername,
                                    SFTPPassword = objRequest.MsgDetailsResquest.TemplateRequest.SFTPFileURLRequest.SFTPPassword,
                                    SFTPPath = objRequest.MsgDetailsResquest.TemplateRequest.SFTPFileURLRequest.SFTPPath
                                };
                            }
                        }
                        if (objRequest.MsgDetailsResquest.FallbackRequest != null)
                        {
                            objEmailSubmitRequest.MsgDetails.Fallback = new COMMON_EnviarNotificacion.emailSubmitRequestTypeMsgDetailsFallback()
                            {
                                FallbackType = Convert.ToInt(objRequest.MsgDetailsResquest.FallbackRequest.FallbackType),
                                SenderAddress = objRequest.MsgDetailsResquest.FallbackRequest.SenderAddress,
                                Text = objRequest.MsgDetailsResquest.FallbackRequest.Text
                            };
                        }
                    }

                    using (new ServiceModel.OperationContextScope(ServiceConfiguration.SIACU_SubmitNotificationEmail.InnerChannel))
                    {
                        ServiceModel.OperationContext.Current.OutgoingMessageHeaders.Add
                         (
                             new Claro.Entity.SecurityHeader(objRequest.Audit.Transaction, objdesencriptarResponse.usuarioAplicacion, objdesencriptarResponse.clave)
                         );
                        objHeaderResponse = Web.Logging.ExecuteMethod<COMMON_EnviarNotificacion.HeaderResponseType>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                        {
                            return Configuration.ServiceConfiguration.SIACU_SubmitNotificationEmail.emailSubmitRequest(ref idTransaction, objHeaderRequestType, objEmailSubmitRequest, out objEmailSubmitResponse);
                        });
                    }
                    if (objEmailSubmitResponse != null)
                    {
                        objResponse = new EntitiesCommon.GetSubmitNotificationEmail.EmailSubmitResponse();
                        objResponse.CampaignID = objEmailSubmitResponse.CampaignID;
                        objResponse.jobCost = objEmailSubmitResponse.jobCost;
                        objResponse.RecipientsCount = objEmailSubmitResponse.RecipientsCount.ToString();
                        if (objEmailSubmitResponse.Status != null)
                        {
                            objResponse.StatusResponse = new EntitiesCommon.GetSubmitNotificationEmail.StatusResponse();
                            objResponse.StatusResponse.StatusCode = objEmailSubmitResponse.Status.StatusCode;
                            objResponse.StatusResponse.StatusText = objEmailSubmitResponse.Status.StatusText;
                        }
                    }
                }
                Claro.Web.Logging.Info("IdSession: " + objRequest.Audit.Session, "Transaccion: " + objRequest.Audit.Transaction, "End a EmailSubmitResponse()");
            }
            catch (Exception ex)
            {
                objResponse = null;
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objResponse;
        }
        /// <summary>
        /// Funcion que devuelve usuario y contraseña DP desencriptado .
        /// </summary>
        /// <param name="strIdSession"></param>
        /// <param name="strTransaction"></param>
        /// <param name="ipAdress"></param>
        /// <param name="strUsername"></param>
        /// <param name="strApplicationName"></param>
        /// <returns>desencriptarResponseBody</returns>
        /// <remarks>GetDecryptCredentialDPSIACU</remarks>
        /// <list type="bullet">
        /// <item><CreadoPor>Everis</CreadoPor></item>
        /// <item><FecCrea>2019.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>20/05/2019.</FecActu></item>
        /// <item><Resp>Everis</Resp></item>
        /// <item><Mot>PROY-140245-Oferta Colaborador</Mot></item></list>
        public static desencriptarResponseBody GetDecryptCredentialDPSIACU(string strIdSession, string strTransaction, string ipAdress, string strUsername, string strApplicationName)
        {
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Transaccion: " + strTransaction, "Begin a GetDecryptCredentialDPSIACU()");

            desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();

            try
            {
                const string TIMRootRegistry = @"SOFTWARE\TIM";
                string strKey = KEY.AppSettings("strRestricTetheVelocWS");
                string cryptoUser = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("User", "").ToString();
                string cryptoPass = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + strKey).GetValue("Password", "").ToString();

                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                {
                    idTransaccion = strTransaction,
                    ipAplicacion = ipAdress,
                    ipTransicion = ipAdress,
                    usrAplicacion = strUsername,
                    idAplicacion = strApplicationName,
                    codigoAplicacion = KEY.AppSettings("strCodigoAplicacion"),
                    usuarioAplicacionEncriptado = cryptoUser,
                    claveEncriptado = cryptoPass,
                };

                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("[GetDecryptCredentialDPSIACU][Parametro Entrada] => idTransaccion: {0}, codigoAplicacion: {1}, usuarioAplicacionEncriptado: {2}, claveEncriptado: {3}",
                                                                          objdesencriptarRequest.idTransaccion,
                                                                          objdesencriptarRequest.codigoAplicacion,
                                                                          objdesencriptarRequest.usuarioAplicacionEncriptado,
                                                                          objdesencriptarRequest.claveEncriptado));

                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar
                (
                            ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave
                );

                Claro.Web.Logging.Info(strIdSession, strTransaction, string.Format("[GetDecryptCredentialDPSIACU][Parametros Salida] => idTransaccion: {0}, mensajeResultado: {1}, usuarioAplicacion: {2}, clave: {3}",
                                                                          objdesencriptarRequest.idTransaccion,
                                                                          objdesencriptarResponse.mensajeResultado,
                                                                          objdesencriptarResponse.usuarioAplicacion,
                                                                          objdesencriptarResponse.clave));
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, "(Exception) Error :" + ex.Message);
                objdesencriptarResponse = null;
            }

            return objdesencriptarResponse;
        }

        #endregion

/// <summary>
        /// Método que genera Constancia PDF
        /// </summary>
        /// <param name="oParametersGeneratePDF"></param>
        /// <returns></returns>
        public static bool GenerateContancyWithXml(string idSession, string transaction, ParametersGeneratePDF parameters, ref string strErroMsg, string xml)
        {
            bool blnResult;

            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateContancyWithXml()");
                //string xml = BuildXML_Constancy(parameters);
                //Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, parameters.StrNombreArchivoTransaccion.Replace("/", "_"));

                if (String.IsNullOrEmpty(parameters.StrCasoInter))
                {
                    parameters.StrCasoInter = "X";
                }
                Claro.Web.Logging.Info(idSession, transaction, "After strNamePDF:  " + strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();

                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;

                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;

                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;

                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;

                string strInputTrama = Fun.Functions.CreateXML(objGenerarPDFRequest);

                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });

                string strOutputTrama = Fun.Functions.CreateXML(objGenerarPDFResponse);

                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    blnResult = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    blnResult = true;
                }

                GenerateTrazability(idSession, transaction, parameters.StrNombreArchivoTransaccion, strInputTrama, strOutputTrama, parameters.StrCasoInter);

            }
            catch (Exception ex)
            {
                strErroMsg = SIACU.Transac.Service.Functions.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                blnResult = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateContancyWithXml()");
            return blnResult;
        }

        #region OnBase
        public static OnBaseCargaResponse TargetDocumentoOnBase(OnBaseCargaRequest objRequest, System.Collections.Hashtable objCredentials)
        {
            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, "Inicio: TargetDocumentoOnBase");

            CargaDocumentoOnBase.cargaDocumentOnBase objPortTypeClient = Configuration.WebServiceConfiguration.CargaDocumentoOnBase;
            OnBaseCargaResponse objResponse = new OnBaseCargaResponse();
            CargaDocumentoOnBase.Request request = new CargaDocumentoOnBase.Request();
            CargaDocumentoOnBase.HeaderRequestType HeaderRequest;
            CargaDocumentoOnBase.HeaderRequest headerRequest1;
            CargaDocumentoOnBase.CargarDocumentoOnBaseRequestType CargarDocumentoOnBaseRequestMessage = new CargaDocumentoOnBase.CargarDocumentoOnBaseRequestType();
            CargaDocumentoOnBase.HeaderResponse headerResponse1 = new CargaDocumentoOnBase.HeaderResponse();
            CargaDocumentoOnBase.CargarDocumentoOnBaseResponseType CargarDocumentoOnBaseResponseMessage = new CargaDocumentoOnBase.CargarDocumentoOnBaseResponseType();
            CargaDocumentoOnBase.HeaderResponseType HeaderResponseType = new CargaDocumentoOnBase.HeaderResponseType();

            CargarDocumentoOnBaseRequestMessage._entitySpecificationAttachment = new CargaDocumentoOnBase.EntitySpecificationAttachment();
            CargarDocumentoOnBaseRequestMessage._entitySpecificationAttachment._entitySpecAttachExtension = new CargaDocumentoOnBase.EntitySpecAttachExtension();
            CargarDocumentoOnBaseRequestMessage._entitySpecificationAttachment._entitySpecAttachExtension.user = objRequest.user;

            CargarDocumentoOnBaseRequestMessage.listaDocumentos = new CargaDocumentoOnBase.DocumentoType[1].Select(h => new CargaDocumentoOnBase.DocumentoType()).ToArray();
            int countDoc = objRequest.metadatosOnBase.Count;
            int countSec = 0;
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0].metadatos = new CargaDocumentoOnBase.MetadatosType[countDoc].Select(h => new CargaDocumentoOnBase.MetadatosType()).ToArray();

            string strKeyWorks = "";

            foreach (metadatosOnBase item in objRequest.metadatosOnBase)
            {
                strKeyWorks = strKeyWorks + string.Format(" [ID] - {0} ; [attributeName] - {1} ; [attributeValue] - {2} ; ",
                    countSec,
                    item.attributeName,
                    item.attributeValue);

                CargarDocumentoOnBaseRequestMessage.listaDocumentos[0].metadatos[countSec]._attributeValuePair = new CargaDocumentoOnBase.AttributeValuePair();
                CargarDocumentoOnBaseRequestMessage.listaDocumentos[0].metadatos[countSec]._attributeValuePair.attributeName = item.attributeName;
                CargarDocumentoOnBaseRequestMessage.listaDocumentos[0].metadatos[countSec]._attributeValuePair.attributeValue = item.attributeValue;
                countSec++;
            }

            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment = new CargaDocumentoOnBase.EntitySpecificationAttachment();
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment.name = objRequest.SpecificationAttachmentOnBase.name;
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment.type = objRequest.SpecificationAttachmentOnBase.type;
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment._entitySpecAttachExtension = new CargaDocumentoOnBase.EntitySpecAttachExtension();
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment._entitySpecAttachExtension.ID = objRequest.SpecificationAttachmentOnBase.listEntitySpectAttach.ID;
            CargarDocumentoOnBaseRequestMessage.listaDocumentos[0]._entitySpecificationAttachment._entitySpecAttachExtension.fileBase64 = objRequest.SpecificationAttachmentOnBase.listEntitySpectAttach.fileBase64;

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
               string.Format("TargetDocumentoOnBase - Parámetros de entrada objRequest: [user] - {0} ; [KeyWorks] - [{1}]  ", objRequest.user, strKeyWorks));

            Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    string.Format("TargetDocumentoOnBase - Parámetros de entrada request: [consumer] - {0} ; [country] - {1} ; [dispositivo] - {2} ; " +
                    "[language] - {3} ; [modulo] - {4} ; [msgType] - {5} ; " +
                    "[operation] - {6} ; [pid] - {7}; [system] - {8};[timestamp] - {9}; [userId] - {10}; [wsIp] - {11};",
                    objRequest.HeaderDPRequest.consumer,
                    objRequest.HeaderDPRequest.country,
                    objRequest.HeaderDPRequest.dispositivo,
                    objRequest.HeaderDPRequest.language,
                    objRequest.HeaderDPRequest.modulo,
                    objRequest.HeaderDPRequest.msgType,
                    objRequest.HeaderDPRequest.operation,
                    objRequest.HeaderDPRequest.pid,
                    objRequest.HeaderDPRequest.system,
                    objRequest.HeaderDPRequest.timestamp,
                    objRequest.HeaderDPRequest.userId,
                    objRequest.HeaderDPRequest.wsIp));

            HeaderRequest = new CargaDocumentoOnBase.HeaderRequestType()
            {
                consumer = objRequest.HeaderDPRequest.consumer,
                country = objRequest.HeaderDPRequest.country,
                dispositivo = objRequest.HeaderDPRequest.dispositivo,
                language = objRequest.HeaderDPRequest.language,
                modulo = objRequest.HeaderDPRequest.modulo,
                msgType = objRequest.HeaderDPRequest.msgType,
                operation = objRequest.HeaderDPRequest.operation,
                pid = objRequest.HeaderDPRequest.pid,
                system = objRequest.HeaderDPRequest.system,
                timestamp = objRequest.HeaderDPRequest.timestamp,
                userId = objRequest.HeaderDPRequest.userId,
                wsIp = objRequest.HeaderDPRequest.wsIp,
                VarArg = new List<CargaDocumentoOnBase.ArgType>().ToArray()
            };

            headerRequest1 = new CargaDocumentoOnBase.HeaderRequest()
            {
                additionalNode = (objRequest.Audit.additionalNode == null ? "" : objRequest.Audit.additionalNode),
                channel = (objRequest.Audit.chanel == null ? "" : objRequest.Audit.chanel),
                idApplication = (objRequest.Audit.idApplication == null ? "" : objRequest.Audit.idApplication),
                idBusinessTransaction = objRequest.Audit.Session,
                idESBTransaction = objRequest.Audit.Session,
                startDate = DateTime.Now,
                userApplication = objRequest.Audit.UserName,
                userSession = objRequest.Audit.UserName
            };

            if (objRequest.Audit != null)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
               string.Format("TargetDocumentoOnBase - Parámetros de entrada audit: [idTransaccion] - {0} ; [ipAplicacion] - {1} ; [nombreAplicacion] - {2} ; " +
               "[usuarioAplicacion] - {3};",
                objRequest.Audit.Transaction,
                objRequest.Audit.IPAddress,
                objRequest.Audit.ApplicationName,
                objRequest.Audit.UserName));
    }


            try
            {

                Tools.Entity.Credentials credentials = Tools.Connections.Security.Cryptography.DES.decryptUserAndPass(objCredentials, objRequest.Audit);

                objPortTypeClient.Security = new CargaDocumentoOnBase.SecurityType()
                {
                    UsernameToken = new CargaDocumentoOnBase.UsernameTokenType()
                    {
                        Username = credentials.User,
                        Password = credentials.Password
                    }
                };

                objPortTypeClient.headerRequest = headerRequest1;
                objPortTypeClient.HeaderRequest = HeaderRequest;


                try
                {
                    string data = Newtonsoft.Json.JsonConvert.SerializeObject(CargarDocumentoOnBaseRequestMessage);
                    Claro.Web.Logging.Info("TargetDocumentoOnBase json", "Parámetros de entrada JSON Rest", data);
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                }            

                CargarDocumentoOnBaseResponseMessage = objPortTypeClient.cargarDocumentoOnBase(CargarDocumentoOnBaseRequestMessage);

                if (CargarDocumentoOnBaseResponseMessage != null)
                {
                    objResponse = new OnBaseCargaResponse();

                    if (CargarDocumentoOnBaseResponseMessage.responseData != null)
                    {
                        if ((CargarDocumentoOnBaseResponseMessage.responseData.idDocumento == null ? false : CargarDocumentoOnBaseResponseMessage.responseData.idDocumento.Length > 0) ? (CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment != null ? true : false) : false)
                        {
                            if (CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment._entitySpecAttachExtension != null)
                            {
                                objResponse.codeOnBase = CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment._entitySpecAttachExtension.idDocRepository;
                                objResponse.status = CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment._entitySpecAttachExtension.status;
                                objResponse.id = CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment._entitySpecAttachExtension.ID;

                                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                                string.Format("TargetDocumentoOnBase - Parámetros de salida _entitySpecAttachExtension: [codeOnBase] - {0} ; [status] - {1} ; [id] - {2} ;",
                                objResponse.codeOnBase,
                                objResponse.status,
                                objResponse.id));
                            }
                            else
                            {
                                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("TargetDocumentoOnBase - Error Parámetros de salida CargarDocumentoOnBaseResponseMessage.responseData.idDocumento[0]._entitySpecificationAttachment._entitySpecAttachExtension : {0}", "Null"));
                            }
                        }
                        else
                        {
                            Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("TargetDocumentoOnBase - Error Parámetros de salida CargarDocumentoOnBaseResponseMessage.responseData.idDocumento : {0}", "Null"));
                        }
    }
                    else
                    {
                        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("TargetDocumentoOnBase - Error Parámetros de salida CargarDocumentoOnBaseResponseMessage.responseData : {0}", "Null"));
                    }

                    if (CargarDocumentoOnBaseResponseMessage.responseStatus != null)
                    {
                        objResponse.codeResponse = CargarDocumentoOnBaseResponseMessage.responseStatus.codeResponse;
                        objResponse.descriptionResponse = CargarDocumentoOnBaseResponseMessage.responseStatus.descriptionResponse + ", origin=" + (CargarDocumentoOnBaseResponseMessage.responseStatus.origin == null ? "null" : CargarDocumentoOnBaseResponseMessage.responseStatus.origin);
                        objResponse.date = CargarDocumentoOnBaseResponseMessage.responseStatus.date;

                        Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        string.Format("TargetDocumentoOnBase - Parámetros de salida responseStatus: [codeResponse] - {0} ; [descriptionResponse] - {1} ; [date] - {2} ;",
                        objResponse.codeResponse,
                        objResponse.descriptionResponse,
                        objResponse.date));
                    }
                    else
                    {
                        Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("TargetDocumentoOnBase - Error Parámetros de salida CargarDocumentoOnBaseResponseMessage.responseStatus : {0}", "Null"));
                    }
                }
                else
                {
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, string.Format("TargetDocumentoOnBase - Error Parámetros de salida CargarDocumentoOnBaseResponseMessage : {0}", "Null"));
                }



            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                objResponse.codeResponse = "1";
                objResponse.descriptionResponse = ex.Message;
            }

            return objResponse;

        }

        //CAYCHOJJ
        public static UploadDocumentOnBaseResponse GetUploadDocumentOnBase(UploadDocumentOnBaseRequest UploadDocumentOnBaseRequest)
        {
            EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseResponse objUploadDocumentOnBaseResponse = new EntitiesCommon.GetUploadDocumentOnBase.UploadDocumentOnBaseResponse();
            StringBuilder xml = new StringBuilder();
            xml.Append("<ArrayOfE_Documento xmlns:xsd='" + "http://www.w3.org/2001/XMLSchema'" + " xmlns:xsi='" + "http://www.w3.org/2001/XMLSchema-instance'" + ">");
            xml.Append("<E_Documento>");
            xml.Append("<seq>" + UploadDocumentOnBaseRequest.listaDocumentos.Seq + "</seq>");
            xml.Append("<CodigoTCRM>" + UploadDocumentOnBaseRequest.listaDocumentos.CodigoTCRM + "</CodigoTCRM>");
            xml.Append("<TipoDocumentoOnBase>" + UploadDocumentOnBaseRequest.listaDocumentos.TipoDocumentoOnBase + "</TipoDocumentoOnBase>");
            xml.Append("<ListaMetadatos>");
            foreach (var itemMetadatos in UploadDocumentOnBaseRequest.listaDocumentos.ListaMetadatos)
            {
                xml.Append("<E_Keyword>");
                xml.Append("<codigoCampo>" + itemMetadatos.codigoCampo + "</codigoCampo>");
                xml.Append("<Campo>" + itemMetadatos.Campo + "</Campo>");
                xml.Append("<Valor>" + itemMetadatos.Valor + "</Valor>");
                xml.Append("<longitud>" + itemMetadatos.longitud + "</longitud>");
                xml.Append("</E_Keyword>");
            }
            xml.Append("</ListaMetadatos>");
            xml.Append("<abytArchivo>" + UploadDocumentOnBaseRequest.listaDocumentos.abytArchivo + "</abytArchivo>");
            xml.Append("<CodigoOnBase>" + UploadDocumentOnBaseRequest.listaDocumentos.CodigoOnBase + "</CodigoOnBase>");
            xml.Append("<strExtension>" + UploadDocumentOnBaseRequest.listaDocumentos.strExtension + "</strExtension>");
            xml.Append("</E_Documento>");
            xml.Append("</ArrayOfE_Documento>");

            onBaseLoad.auditRequest oAuditRequest = new onBaseLoad.auditRequest()
            {
                idTransaccion = UploadDocumentOnBaseRequest.Audit.Transaction,
                ipAplicacion = UploadDocumentOnBaseRequest.Audit.IPAddress,
                nombreAplicacion = UploadDocumentOnBaseRequest.Audit.ApplicationName,
                usuarioAplicacion = UploadDocumentOnBaseRequest.Audit.UserName,

            };

            onBaseLoad.ArrayOfObjetoRequestOpcional listOptionalRequest = new onBaseLoad.ArrayOfObjetoRequestOpcional();

            if (UploadDocumentOnBaseRequest.parametrosRequest != null)
            {
                foreach (var item in UploadDocumentOnBaseRequest.parametrosRequest.ListaRequestOpcional.objetoRequestOpcional)
                {
                    onBaseLoad.objetoRequestOpcional OptionalObject = new onBaseLoad.objetoRequestOpcional()
                    {
                        campo = item.campo,
                        valor = item.valor
                    };
                    listOptionalRequest.Add(OptionalObject);
                }
            }
            try
            {
                onBaseLoad.CargarDocumentoOnBaseRequestBody objOnBaseRequestBody = new onBaseLoad.CargarDocumentoOnBaseRequestBody()
                {

                    auditRequest = oAuditRequest,
                    idInterfazTCRM = UploadDocumentOnBaseRequest.idInterfazTCRM,
                    usuario = UploadDocumentOnBaseRequest.usuario,
                    fecha = UploadDocumentOnBaseRequest.fecha,
                    hora = UploadDocumentOnBaseRequest.hora,
                    listaDocumentos = xml.ToString(),
                    parametrosRequest = new onBaseLoad.parametrosRequest()
                    {
                        ListaRequestOpcional = listOptionalRequest
                    }
                };
                var objHeaderResponse = Web.Logging.ExecuteMethod(UploadDocumentOnBaseRequest.Audit.Session, UploadDocumentOnBaseRequest.Audit.Transaction, () =>
                {
                    return Configuration.ServiceConfiguration.onBaseLoad.CargarDocumentoOnBase(oAuditRequest, objOnBaseRequestBody.idInterfazTCRM, objOnBaseRequestBody.usuario, objOnBaseRequestBody.fecha, objOnBaseRequestBody.hora, objOnBaseRequestBody.listaDocumentos, objOnBaseRequestBody.parametrosRequest);
                });
                if (objHeaderResponse != null)
                {
                    XmlDocument xmlDoc = new XmlDocument();

                    xmlDoc.LoadXml(objHeaderResponse.listaDocumentos);
                    var objEd = xmlDoc.SelectSingleNode("ArrayOfE_Documento").SelectSingleNode("E_Documento");

                    objUploadDocumentOnBaseResponse.idTransaccion = objHeaderResponse.idTransaccion;
                    objUploadDocumentOnBaseResponse.idInterfazTCRM = objHeaderResponse.idInterfazTCRM;
                    objUploadDocumentOnBaseResponse.ipServerResponse = objHeaderResponse.ipServerResponse;
                    objUploadDocumentOnBaseResponse.fecha = objHeaderResponse.fecha;
                    objUploadDocumentOnBaseResponse.hora = objHeaderResponse.hora;
                    objUploadDocumentOnBaseResponse.E_Document = new EntitiesCommon.GetUploadDocumentOnBase.E_DocumentResponse()
                    {
                        CodigoTCRM = objEd.SelectSingleNode("CodigoTCRM").InnerText,
                        Estado = objEd.SelectSingleNode("Estado").InnerText,
                        Seq = objEd.SelectSingleNode("Seq").InnerText,
                        Fecha = objEd.SelectSingleNode("Fecha").InnerText,
                        CodigoOnBase = objEd.SelectSingleNode("CodigoOnBase").InnerText,
                        Hora = objEd.SelectSingleNode("Hora").InnerText,
                    };
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(UploadDocumentOnBaseRequest.Audit.Session, UploadDocumentOnBaseRequest.Audit.Transaction, "(Exception) Error :" + ex.Message);
            }
            return objUploadDocumentOnBaseResponse;
        }

        #endregion



        public static Boolean UploadSftp(Tools.Entity.AuditRequest objAuditRequest, ConnectionSFTP objConnectionSFTP, string fileName, byte[] objFile)
            {
            Boolean status = false;

            Tools.Entity.Credentials credentials = Tools.Connections.Security.Cryptography.DES.decryptUserAndPass(GetCredentials(objConnectionSFTP.KeyUser, objConnectionSFTP.KeyPassword), objAuditRequest);

            string strUsuari = credentials.User;
            string strPasswd = credentials.Password;

            try
            {
                string strServer = objConnectionSFTP.server;
                string workingdirectory = objConnectionSFTP.path_Destination;
                int port = Convert.ToInt(objConnectionSFTP.port);
                using (var client = new SftpClient(strServer, port, strUsuari, strPasswd))
                {
                    client.Connect();
                    client.ChangeDirectory(workingdirectory);

                    using (MemoryStream fileStream = new MemoryStream(objFile))
                    {
                        client.BufferSize = 8 * 1024; // bypass Payload error large files 
                        client.UploadFile(fileStream, fileName);
                    }

                }
                status = true;
            }
            catch (WebException ex)
            {
                Claro.Web.Logging.Error("1", objAuditRequest.Transaction, "error UploadSftp: " + ex);
                throw new ArgumentException("No se pudo conectar al servidor SFTP de Imagenes de Equipos -" + ex);
            }
            return status;
    }


        public static System.Collections.Hashtable GetCredentials(string usuario, string clave)
        {
            System.Collections.Hashtable objCredentials = new System.Collections.Hashtable();

            objCredentials.Add("idAplicacion", KEY.AppSettings("idAplicacion"));
            objCredentials.Add("codigoAplicacion", KEY.AppSettings("codigoAplicacion"));
            objCredentials.Add("usuarioAplicacionEncriptado", KEY.AppSettings(usuario));
            objCredentials.Add("claveEncriptado", KEY.AppSettings(clave));
            return objCredentials;
        }
        public static bool GenerateConstancyNamePDF(string idSession, string transaction, ParametersGeneratePDF parameters, ref string strErroMsg,string NombrePDF)
        {
            bool blnResult;

            try
            {
                Claro.Web.Logging.Info(idSession, transaction, "IN GenerateConstancyPDF()");
                string xml = BuildXML_Constancy(parameters);
                Claro.Web.Logging.Info(idSession, transaction, string.Format("ConstruyeXML: {0}", xml.ToString()));
                Claro.Web.Logging.Info(idSession, transaction, "Before StrNombreArchivoTransaccion:  " + parameters.StrNombreArchivoTransaccion);
                string strDateTransaction = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strPathPDF = string.Format("{0}{1}", parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strNamePDF = string.Format("{0}_{1}_{2}.pdf", parameters.StrCasoInter, strDateTransaction, NombrePDF.Replace("/", "_"));

                if (String.IsNullOrEmpty(parameters.StrCasoInter))
                {
                    parameters.StrCasoInter = "X";
                }
                Claro.Web.Logging.Info(idSession, transaction, "After strNamePDF:  " + strNamePDF);
                string pubFile = "claro-postventa.pub", strDriver = "Driver";
                GENERATEPDF.ewsComposeResponse objGenerarPDFResponse = new GENERATEPDF.ewsComposeResponse();
                GENERATEPDF.EngineService objGenerarPDF = new GENERATEPDF.EngineService();

                GENERATEPDF.output objGenerarPDFRequestOutput = new GENERATEPDF.output();
                objGenerarPDFRequestOutput.directory = strPathPDF;
                objGenerarPDFRequestOutput.fileName = strNamePDF;

                GENERATEPDF.driverFile objGenerarPDFRequestDriver = new GENERATEPDF.driverFile();
                objGenerarPDFRequestDriver.driver = ASCIIEncoding.UTF8.GetBytes(xml);
                objGenerarPDFRequestDriver.fileName = strDriver;

                objGenerarPDF.Url = ConfigurationManager.AppSettings("strServidorGenerarPDF");
                objGenerarPDF.Credentials = CredentialCache.DefaultCredentials;

                GENERATEPDF.ewsComposeRequest objGenerarPDFRequest = new GENERATEPDF.ewsComposeRequest();
                objGenerarPDFRequest.driver = objGenerarPDFRequestDriver;
                objGenerarPDFRequest.fileReturnRegEx = ".*.(pdf)";
                objGenerarPDFRequest.includeHeader = false;
                objGenerarPDFRequest.includeMessageFile = true;
                objGenerarPDFRequest.outputFile = objGenerarPDFRequestOutput;
                objGenerarPDFRequest.pubFile = pubFile;
                
                string strInputTrama = Fun.Functions.CreateXML(objGenerarPDFRequest);
                Claro.Web.Logging.Info(idSession, transaction, strInputTrama);

                objGenerarPDFResponse = Claro.Web.Logging.ExecuteMethod<GENERATEPDF.ewsComposeResponse>(() =>
                {
                    return objGenerarPDF.Compose(objGenerarPDFRequest);
                });

                string strOutputTrama = Fun.Functions.CreateXML(objGenerarPDFResponse);

                if (objGenerarPDFResponse.statusMessage.Contains(Claro.Constants.NumberTwelveString) || objGenerarPDFResponse.files == null)
                {
                    strErroMsg = "Ha ocurrido un problema en el servicio que genera PDF, por favor intentar en otro momento.";
                    blnResult = false;
                    Claro.Web.Logging.Error(idSession, transaction, strErroMsg);
                    Claro.Web.Logging.Error(idSession, transaction, objGenerarPDFResponse.statusMessage);
                }
                else
                {
                    strErroMsg = "Se generó correctamente el archivo: " + objGenerarPDFResponse.files[0].fileName;
                    Claro.Web.Logging.Info(idSession, transaction, strErroMsg);
                    blnResult = true;
                }

                GenerateTrazability(idSession, transaction, parameters.StrNombreArchivoTransaccion, strInputTrama, strOutputTrama, parameters.StrCasoInter);

            }
            catch (Exception ex)
            {
                strErroMsg = SIACU.Transac.Service.Functions.GetExceptionMessage(ex);
                Claro.Web.Logging.Error(idSession, transaction, "strErroMsg: " + strErroMsg);
                blnResult = false;
            }
            Claro.Web.Logging.Info(idSession, transaction, "OUT GenerateConstancyPDF()");
            return blnResult;
        }

        #region "DataPower - Credenciales"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAuditRequest"></param>
        /// <param name="keyUser"></param>
        /// <param name="keyPass"></param>
        /// <returns></returns>
        public static Claro.Entity.UsernameToken getCredentials(Claro.Entity.AuditRequest objAuditRequest, string keyUser, string keyPass)
        {
            Claro.Entity.UsernameToken objCrendentials = null;
            string User = KEY.AppSettings(keyUser);
            string pass = KEY.AppSettings(keyPass);
            objCrendentials = Decrypt(objAuditRequest, User, pass);
            return objCrendentials;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objAuditRequest"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        private static Claro.Entity.UsernameToken Decrypt(Claro.Entity.AuditRequest objAuditRequest, string user, string pass)
        {
            Claro.Entity.UsernameToken objCrendentials;
            desencriptarResponseBody objdesencriptarResponse = new desencriptarResponseBody();
            string User = string.Empty;
            string Password = string.Empty;
            string resStatus = string.Empty;
            Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Inicio de Ejecución del WS ConsultaClaves - Método Desencriptar");
            try
            {
                desencriptarRequestBody objdesencriptarRequest = new desencriptarRequestBody()
                {
                    idTransaccion = objAuditRequest.Transaction,
                    ipAplicacion = objAuditRequest.IPAddress,
                    ipTransicion = objAuditRequest.IPAddress,
                    usrAplicacion = objAuditRequest.UserName,
                    idAplicacion = objAuditRequest.ApplicationName,
                    codigoAplicacion = KEY.AppSettings("codigoAplicacion"),
                    usuarioAplicacionEncriptado = user,
                    claveEncriptado = pass,
                };

                objdesencriptarResponse.codigoResultado = Configuration.ServiceConfiguration.CONSULTA_CLAVES.desencriptar(ref objdesencriptarRequest.idTransaccion,
                            objdesencriptarRequest.ipAplicacion,
                            objdesencriptarRequest.ipTransicion,
                            objdesencriptarRequest.usrAplicacion,
                            objdesencriptarRequest.idAplicacion,
                            objdesencriptarRequest.codigoAplicacion,
                            objdesencriptarRequest.usuarioAplicacionEncriptado,
                            objdesencriptarRequest.claveEncriptado,
                            out objdesencriptarResponse.mensajeResultado,
                            out objdesencriptarResponse.usuarioAplicacion,
                            out objdesencriptarResponse.clave);

                if (objdesencriptarResponse.codigoResultado == "0")
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado  0, desencriptado con exito - Método Desencriptar");
                }
                else
                {
                    Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Codigo resultado diferente de 0, error  al desencriptar - Método Desencriptar");
                }
                Claro.Web.Logging.Info(objAuditRequest.ApplicationName, objAuditRequest.Transaction, "Fin de Ejecución del WS ConsultaClaves - Método Desencriptar");
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAuditRequest.Session, objAuditRequest.Transaction, ex.Message);
            }
            objCrendentials = new Claro.Entity.UsernameToken()
            {
                Username = objdesencriptarResponse.usuarioAplicacion,
                Password = new Claro.Entity.Password()
                {
                    Value = objdesencriptarResponse.clave
                }
            };
            return objCrendentials;
        }
        #endregion
    }
}
