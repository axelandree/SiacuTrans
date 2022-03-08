using AutoMapper;
using iTextSharp.awt.geom;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Linq;
using System.Security.Principal;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using System.Web.Mvc;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using CommonService = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using AuditRequestCommon = Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest;
using AuditRequestFixed = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest;
using KEY = Claro.ConfigurationManager;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Code;
using Model = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.SIACU.Transac.Service;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using ConstantsSIACPO = Claro.SIACU.Transac.Service.ConstantsSiacpo;
using CSTS = Claro.SIACU.Transac.Service;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models;
using Claro.Web;
using HelperCommon = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid.PostpaidIncomingCall;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;
using Constant = Claro.SIACU.Transac.Service.Constants;
using HELPERS = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;
using CONSTANTADDITIONALSERVICEPOSTPAID = Claro.SIACU.Transac.Service.Constants.ADDITIONALSERVICESPOSTPAID;
using FunctionsSIACU = Claro.SIACU.Transac.Service.Functions;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR;
using FixedService = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;
using RegisterActiDesaBonoDesc = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers
{
    public class CommonServicesController : Controller
    {
        private readonly CommonTransacServiceClient _oServiceCommon = new CommonTransacServiceClient();
        private readonly FixedTransacServiceClient _oServiceFixed = new FixedTransacServiceClient();

        private readonly PostTransacServiceClient _oServicePostpaid =
            new PostTransacServiceClient();

        //PROY-32650-Retencion/Fidelización II
        private static FixedTransacService.BEETAAuditoriaResponseCapacityHFC _BEETAAuditoriaResponseCapacityHFC = new BEETAAuditoriaResponseCapacityHFC();
        //PROY-32650-Retencion/Fidelización II

        public CommonServicesController()
        {
            Mappings.AutoMapperConfig.RegisterMappings();
        }




        public string CurrentTerminal()
        {
            return System.Web.HttpContext.Current.Request.UserHostAddress;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIpClient"></param>
        /// <returns></returns>
        public string ClientHostname(string strIpClient)
        {
            string hostName = "";
            try
            {
                hostName = Dns.GetHostByAddress(strIpClient).HostName;
            }
            catch
            {
                //Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format(Constant.Message_ErrorGetIpClient_LogTrx,
                //    DateTime.Now.ToString("yyyyMMdd"), strIpCliente));
                hostName = strIpClient;
            }
            return hostName;
        }

        public string CurrentUser(string idSession)
        {
            return idSession;
            //var strUser = string.Empty;
            //try
            //{
            //    var strDomainUser = HttpContext.Request.ServerVariables["LOGON_USER"];
            //    //strUser = ConfigurationManager.AppSettings("UsuarioIdPrueba");
            //    if (string.IsNullOrEmpty(strUser))
            //    {
            //        strUser = strDomainUser.Substring(strDomainUser.IndexOf("\\", StringComparison.Ordinal) + 1);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logging.Error(idSession, idSession, ex.Message);
            //}

            ////string user = KEY.AppSettings("TestUser");
            ////string user = string.Empty;
            ////if (!String.IsNullOrEmpty(idSession)) user = idSession;
            ////if (!String.IsNullOrEmpty(user)) user = user.ToUpper();
            //return strUser.ToUpper();
        }

        public GenerateConstancyResponseCommon GenerateContancyPDF(string idSession, ParametersGeneratePDF parameters)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            //ADICIÓN DE S/. AL CARGO FIJO CON IGV
            parameters.StrCargoFijoConIGV = "S/. " + parameters.StrCargoFijoConIGV;
            parameters.StrCostoTransaccion = "S/. " + parameters.StrCostoTransaccion;
            parameters.StrCargoFijo = "S/. " + parameters.StrCargoFijo;
            if (parameters.flagCargFijoServAdic != "")
                parameters.StrCostoInstalacion = "S/. " + parameters.StrCostoInstalacion;

            var strTerminacionPDF = ConfigurationManager.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession)
            };
            Logging.Info(idSession, "", "Begin GenerateContancyPDF");
            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return _oServiceCommon.GetGenerateContancyPDF(request);
            });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());
            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", string.Format("End GenerateContancyPDF result: {0}, FullPathPDF: {1}", objResponse.Generated.ToString(), objResponse.FullPathPDF));
            return objResponse;
        }

        public void InsertEvidence(string strIdSesison, string strID, string strCustomerID, string strSubClass, string strSubClassCode,
            string strInteraction, string strTransactionName, string strPath, string strDocument, string strUser)
        {
            InsertEvidenceResponse oResponse = new InsertEvidenceResponse();
            InsertEvidenceRequest oRequest = new InsertEvidenceRequest();
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSesison);
            oRequest.audit = audit;
            oRequest.Evidence = new Evidence();
            oRequest.Evidence.StrTransactionType = KEY.AppSettings("strTransactionType");
            oRequest.Evidence.StrTransactionCode = strInteraction;
            oRequest.Evidence.StrCustomerCode = strCustomerID;
            oRequest.Evidence.StrPhoneNumber = strID; //PRE-POST: MovilNumber HFC-LTE: Contract
            oRequest.Evidence.StrTypificationCode = strSubClassCode;
            oRequest.Evidence.StrTypificationDesc = strSubClass;
            oRequest.Evidence.StrCommercialDesc = string.Empty;
            oRequest.Evidence.StrProductType = string.Empty;
            oRequest.Evidence.StrServiceChannel = string.Empty;
            oRequest.Evidence.StrTransactionDate = DateTime.Today.ToShortDateString();
            oRequest.Evidence.StrActivationDate = string.Empty;
            oRequest.Evidence.StrSuspensionDate = string.Empty;
            oRequest.Evidence.StrServiceStatus = string.Empty;

            oRequest.Evidence.StrDocumentName = strDocument;
            oRequest.Evidence.StrDocumentType = strTransactionName;
            oRequest.Evidence.StrDocumentExtension = KEY.AppSettings("strDocumentoExtension");
            oRequest.Evidence.StrDocumentPath = strPath;
            oRequest.Evidence.StrUserName = strUser;

            try
            {
                oResponse = Logging.ExecuteMethod<InsertEvidenceResponse>(() =>
                    {
                        return _oServiceCommon.GetInsertEvidence(oRequest);
                    });
                Logging.Info(audit.Session, audit.transaction, "Evidencia registra correctamente : " + oResponse.StrMsgText);
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, Functions.GetExceptionMessage(ex));
            }
        }

        public List<ItemGeneric> GetListCacDac(string strIdSession)
        {
            List<ItemGeneric> list = new List<ItemGeneric>();

            CacDacTypeResponseCommon objCacDacTypeResponseCommon;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            CacDacTypeRequestCommon objCacDacTypeRequestCommon = new CacDacTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objCacDacTypeResponseCommon = Logging.ExecuteMethod<CacDacTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetCacDacType(objCacDacTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCacDacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }


            if (objCacDacTypeResponseCommon != null && objCacDacTypeResponseCommon.CacDacTypes != null)
            {
                foreach (CommonTransacService.ListItem item in objCacDacTypeResponseCommon.CacDacTypes)
                {
                    list.Add(new ItemGeneric()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
            }

            return list;
        }

        public CommonTransacService.Iteraction InteractionData(string idSession, string idContact, string strPhone,
            string strNotes, string strType,
            string strClass, string strSubClass, string isTFI)
        {

            CommonTransacService.Iteraction entity = new CommonTransacService.Iteraction();
            try
            {
                entity.OBJID_CONTACTO = idContact;
                entity.FECHA_CREACION = DateTime.Now.ToShortDateString();
                entity.TELEFONO = strPhone;
                entity.TIPO = strType;
                entity.CLASE = strClass;
                entity.SUBCLASE = strSubClass;
                entity.TIPO_INTER = ConfigurationManager.AppSettings("AtencionDefault");
                entity.METODO = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                entity.RESULTADO = ConfigurationManager.AppSettings("Ninguno");
                entity.HECHO_EN_UNO = Constant.strCero;
                entity.NOTAS = strNotes;
                entity.FLAG_CASO = Constant.strCero;
                entity.USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU");
                entity.AGENTE = CurrentUser(idSession);
                entity.ES_TFI = isTFI;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public CommonTransacService.InsertTemplateInteraction InteractionTemplateData(string idInteraction,
            string nameTransaction, string strPhoneNumber,
            string strReason, string firstName, string lastName, string strLegalRepresentative, string strDocumentType,
            string strNumberDocument,
            string strStartDate, string strEndDate, string flagGenerateOCC, double dblMonto, string idCACDAC)
        {
            CommonTransacService.InsertTemplateInteraction entity =
                new CommonTransacService.InsertTemplateInteraction();
            try
            {
                entity._NOMBRE_TRANSACCION = nameTransaction;
                entity._ID_INTERACCION = idInteraction;
                entity._X_CLARO_NUMBER = strPhoneNumber;
                entity._X_ADJUSTMENT_REASON = strReason;
                entity._X_TYPE_DOCUMENT = strDocumentType;
                entity._X_DOCUMENT_NUMBER = strNumberDocument;
                entity._X_FIRST_NAME = firstName;
                entity._X_LAST_NAME = lastName;
                entity._X_NAME_LEGAL_REP = strLegalRepresentative;
                entity._X_DNI_LEGAL_REP = strLegalRepresentative;
                entity._X_INTER_1 = DateTime.Now.ToString("dd/MM/yyyy");
                entity._X_INTER_20 = strStartDate;
                entity._X_INTER_21 = strEndDate;
                entity._X_FLAG_REGISTERED = (flagGenerateOCC == "T") ? Constant.strUno : Constant.strCero;// X_INTER_3 => Fidelidad
                entity._X_INTER_22 = (flagGenerateOCC == "T") ? dblMonto : 0;// X_INTER_1 => Monto
                entity._X_INTER_15 = idCACDAC;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public void InsertBusinessInteraction2(CommonTransacService.Iteraction item, string idSession,
            out string rInteractionId, out string rFlagInsertion, out string rMsgText)
        {
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            BusinessInteraction2RequestCommon request =
                new BusinessInteraction2RequestCommon()
                {
                    Item = item,
                    audit = audit
                };

            BusinessInteraction2ResponseCommon objResponse =
                Logging.ExecuteMethod<BusinessInteraction2ResponseCommon>(() =>
                {
                    return _oServiceCommon.GetInsertBusinnesInteraction2(request);
                });
            rInteractionId = objResponse.InteractionId;
            rFlagInsertion = objResponse.FlagInsertion;
            rMsgText = objResponse.MsgText;
        }

        public string GetNumber(string idSession, bool flagCountry, string strNumber)
        {
            DateTime dateAVM;
            DateTime dateCurrent;
            string numberGenerated = String.Empty, numberTelehone = String.Empty;
            string strInternationalCode = KEY.AppSettings("gInternationalCode");
            int maximumLenghNumberTelephone = Int32.Parse(KEY.AppSettings("gMaximumLengthPhone"));
            Logging.Info(idSession, "Transaction: ", "IN GetNumber()");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);
            dateAVM = DateTime.Parse(KEY.AppSettings("strFechaAVM"), culture,
                System.Globalization.DateTimeStyles.AssumeLocal);
            dateCurrent = DateTime.Now;
            Logging.Info(idSession, "Transaction: ", "GetNumber() - strInternationalCode: " + strInternationalCode);

            if (string.IsNullOrEmpty(strNumber))
            {
                numberTelehone = string.Empty;
                Logging.Info(idSession, "Transaction: ", "GetNumber() - numberTelehone: " + numberTelehone);
            }
            else if (strNumber.Length == maximumLenghNumberTelephone)
            {
                if ((dateCurrent - dateAVM).Days < 0)
                {
                    Logging.Info(idSession, "Transaction: ", "GetNumber() - numberTelehone: " + numberTelehone);
                    numberGenerated = GetNumberGenerated(idSession, strNumber);
                    if (!String.IsNullOrEmpty(numberGenerated))
                    {
                        if (flagCountry == true)
                            numberTelehone = strInternationalCode + numberGenerated.Trim();
                        else
                            numberTelehone = numberGenerated.Trim();
                    }
                    else
                        numberTelehone = string.Empty;
                }
                else
                {
                    if (flagCountry == true)
                        numberTelehone = strInternationalCode + strNumber;
                    else
                        numberTelehone = strNumber;
                }
            }
            else
            {
                if (flagCountry == true)
                    numberTelehone = strInternationalCode + strNumber;
                else
                    numberTelehone = strNumber;
            }
            Logging.Info(idSession, "Transaction: ", "OUT GetNumber()");
            return numberTelehone;
        }

        public string GetValueFromListValues(string idSession, string value, string nameList)
        {
            string description = "";

            List<HelperCommon.GenericItem> list = GetListValues(idSession, nameList);
            HelperCommon.GenericItem item = list.Where(x => x.Code.Equals(value)).FirstOrDefault();
            if (item != null)
                description = item.Description;

            return description;
        }

        public HelperCommon.Typification LoadTypification(string idSession, string transactionName, string strType)
        {
            HelperCommon.Typification typification = new HelperCommon.Typification();

            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            try
            {
                TypificationRequest request = new TypificationRequest()
                {
                    TRANSACTION_NAME = transactionName,
                    audit = audit
                };
                TypificationResponse response = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return _oServiceCommon.GetTypification(request);
                });

                if (response.ListTypification.Count > 0)
                {
                    if (response.ListTypification.Count == 1)
                    {
                        typification = MappingTypification(response.ListTypification[0]);
                    }
                    else
                    {
                        for (int i = 0; i < response.ListTypification.Count; i++)
                        {
                            if (response.ListTypification[i].TIPO.ToUpper().Equals(strType.ToUpper()))
                            {
                                typification = MappingTypification(response.ListTypification[i]);
                                break;
                            }
                        }
                    }
                }
                //Inicio Bloque Temporal 
                if (String.IsNullOrEmpty(typification.Type))
                {
                    Logging.Info("Persquash", "LoadTypification", String.Format("ListTypification.Count: {0}", response.ListTypification.Count)); // Temporal 
                    for (int i = 0; i < response.ListTypification.Count; i++)
                    {
                        Logging.Info("Persquash", "LoadTypification", String.Format("TIPO: {0};CLASE: {1};SUBCLASE:{2}", response.ListTypification[i].TIPO,
                            response.ListTypification[i].CLASE, response.ListTypification[i].SUBCLASE)); // Temporal 
                    }
                }
                //Fin Bloque Temporal
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }
            return typification;
        }

        public bool RegisterAuditGeneral(string idSession, string strPhone, string strAmount, string strText,
            string strService, string strTransaction)
        {
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);
            if (strText.Length > 255) strText = strText.Substring(0, 255);

            string strIpClient = CurrentTerminal();
            string strClientName = ClientHostname(strIpClient);
            string strUserAccount = CurrentUser(idSession);

            SaveAuditRequestCommon request = new SaveAuditRequestCommon()
            {
                vCuentaUsuario = strUserAccount,
                vIpCliente = strIpClient,
                vIpServidor = audit.ipAddress,
                vMonto = strAmount,
                vNombreCliente = strClientName,
                vNombreServidor = audit.applicationName,
                vServicio = strService,
                vTelefono = strPhone,
                vTexto = strText,
                vTransaccion = strTransaction,
                audit = audit
            };

            SaveAuditResponseCommon objResponse =
                Logging.ExecuteMethod<SaveAuditResponseCommon>(() =>
                {
                    return _oServiceCommon.SaveAudit(request);
                });

            return objResponse.respuesta;
        }

        public bool RegisterLogTrx(string idSession, string strPhone, string strInteraction, string strTypification,
            string strParamIN, string strParamOUT, string strOpcionCode, string strAccion, string strAccionEvento,
            string nameTransaction)
        {
            bool salida = false;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            string strIpCliente = CurrentTerminal();
            string strCliente = ClientHostname(strIpCliente);
            //try
            //{
            //    strCliente = System.Net.Dns.GetHostByAddress(strIpCliente).HostName;
            //}
            //catch
            //{
            //    Claro.Web.Logging.Error(audit.Session, audit.transaction, String.Format(Constant.Message_ErrorGetIpClient_LogTrx,
            //        DateTime.Now.ToString("yyyyMMdd"), strIpCliente));
            //    strCliente = strIpCliente;
            //} 
            string strMsg = "";

            try
            {
                InsertLogTrxRequestCommon request =
                    new InsertLogTrxRequestCommon()
                    {
                        Accion = String.Format("{0} - {1}", strAccion, strAccionEvento),
                        Aplicacion = Constant.NameApplication,
                        audit = audit,
                        IdInteraction = strInteraction,
                        IdTypification = strTypification,
                        InputParameters = strParamIN,
                        OutpuParameters = strParamOUT,
                        IPPCClient = strIpCliente,
                        PCClient = strCliente,
                        IPServer = audit.ipAddress,
                        NameServer = audit.applicationName,
                        Opcion = strOpcionCode,
                        Phone = strPhone,
                        Transaccion = nameTransaction,
                        User = audit.userName
                    };
                string flagInsertion = InsertLogTrx(request);
                if (flagInsertion.Equals(Constant.Message_OK)) salida = true;
            }
            catch (Exception ex)
            {
                Logging.Error(audit.Session, audit.transaction,
                    String.Format(Constant.Message_ErrorGeneral, ex.Message));
                salida = false;
                strMsg = ex.Message;
            }
            return salida;
        }



        #region Funciones Privadas

        private List<HelperCommon.GenericItem> GetListValues(string idSession, string nameList)
        {
            List<HelperCommon.GenericItem> list = new List<HelperCommon.GenericItem>();
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            if (String.IsNullOrEmpty(nameList)) return list;
            if (Functions.IsNumeric(nameList))
            {
                List<CommonTransacService.ListItem> listDB = GetListMigratedElementsBD(audit, nameList);
                return MappingListItemGeneric(listDB);
            }

            if (nameList.Equals("ListaTipoTriacion") || nameList.Equals("ListaPlanDisponible") ||
                nameList.Equals("ListaPromocionDisponible"))
            {
                List<ItemGeneric> listaItem = Functions.GetListValuesXML(nameList, "2", Constant.SiacutDataXML);
                list = MappingListItemGeneric(listaItem);
            }

            else
            {
                List<ItemGeneric> listaItem = Functions.GetListValuesXML(nameList, "0", Constant.SiacutDataXML);
                list = MappingListItemGeneric(listaItem);
            }


            if (list != null && list.Count > 0) return list;

            List<CommonTransacService.ListItem> listDBMigEl;
            if (list.Count == 0)
            {
                nameList = nameList.ToUpper();
                if (nameList.Equals(Constant.Option_CAC))
                {
                    listDBMigEl = GetListMigratedElements2BD(audit, KEY.AppSettings("strListadoDAC_CAC"));
                    list = MappingListItemGeneric(listDBMigEl.ToList());
                }
                else if (nameList.Equals(Constant.Option_TypeDocId))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_CivilStatus))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_CAS16))
                {
                    listDBMigEl = GetListMigratedElements2BD(audit, nameList.ToUpper());
                    list = MappingListItemGeneric(listDBMigEl.ToList());
                }
                else if (nameList.Equals(Constant.Option_Occupation))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_RegistrationReason))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_Brand))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_Model))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_Departamento))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_Provincia))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_Distrito))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_BirthPlace))
                {
                    //Por Implementar
                }
                else if (nameList.Equals(Constant.Option_ServiceVAS))
                {
                    //Por Implementar
                }
                else
                {
                    listDBMigEl = GetListMigratedElementsBD(audit, nameList.ToUpper());
                    list = MappingListItemGeneric(listDBMigEl.ToList());
                }
            }

            return list;
        }

        private List<CommonTransacService.ListItem> GetListMigratedElementsBD(CommonTransacService.AuditRequest audit,
            string strId)
        {
            MigratedElementsRequest request =
                new MigratedElementsRequest()
                {
                    audit = audit,
                    Id = Int32.Parse(strId)
                };
            MigratedElementsResponse objResponse =
                Logging.ExecuteMethod<MigratedElementsResponse>(() =>
                {
                    return _oServiceCommon.GetMigratedElements(request);
                });
            return objResponse.ListMigratedElements;
        }

        private List<CommonTransacService.ListItem> GetListMigratedElements2BD(CommonTransacService.AuditRequest audit,
            string nameFunction)
        {
            MigratedElementsRequest request =
                new MigratedElementsRequest()
                {
                    audit = audit,
                    NameFunction = nameFunction
                };
            MigratedElementsResponse objResponse =
                Logging.ExecuteMethod<MigratedElementsResponse>(() =>
                {
                    return _oServiceCommon.GetMigratedElements2(request);
                });
            return objResponse.ListMigratedElements;
        }

        private string GetNumberGenerated(string idSession, string msisdn)
        {
            string strNumberGenerated = String.Empty;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            NumberEAIResponse objNumberEAI = GetNumberEAI(msisdn, audit, ref strNumberGenerated);
            if (string.IsNullOrEmpty(strNumberGenerated))
            {
                NumberGWPResponse objNumberGWP = GetNumberGWP(msisdn, audit, ref strNumberGenerated);
            }
            return strNumberGenerated;
        }

        private NumberEAIResponse GetNumberEAI(string msisdn, CommonTransacService.AuditRequest audit,
            ref string strNumberGenerated)
        {
            NumberEAIRequest objRequest = new NumberEAIRequest()
            {
                Msisdn = msisdn,
                audit = audit
            };

            NumberEAIResponse objResponse = Logging.ExecuteMethod<NumberEAIResponse>(() =>
            {
                return _oServiceCommon.GetNumberEAI(objRequest);
            });
            strNumberGenerated = objResponse.Number;
            return objResponse;
        }

        private NumberGWPResponse GetNumberGWP(string msisdn, CommonTransacService.AuditRequest audit,
            ref string strNumberGenerated)
        {
            NumberGWPRequest objRequest = new NumberGWPRequest()
            {
                Msisdn = msisdn,
                audit = audit
            };
            NumberGWPResponse objResponse = Logging.ExecuteMethod<NumberGWPResponse>(() =>
            {
                return _oServiceCommon.GetNumberGWP(objRequest);
            });
            strNumberGenerated = objResponse.Number;
            return objResponse;
        }

        private string InsertLogTrx(InsertLogTrxRequestCommon request)
        {
            InsertLogTrxResponseCommon objResponse =
                Logging.ExecuteMethod<InsertLogTrxResponseCommon>(() =>
                {
                    return _oServiceCommon.InsertLogTrx(request);
                });
            return objResponse.FlagInsertion;
        }

        private List<HelperCommon.GenericItem> MappingListItemGeneric(List<CommonTransacService.ListItem> listDB)
        {
            List<HelperCommon.GenericItem> list = new List<HelperCommon.GenericItem>();
            HelperCommon.GenericItem entity = null;
            foreach (var item in listDB)
            {
                entity = new HelperCommon.GenericItem()
                {
                    Code = item.Code,
                    Description = item.Description
                };
                list.Add(entity);
            }
            return list;
        }

        private List<HelperCommon.GenericItem> MappingListItemGeneric(List<ItemGeneric> listTrans)
        {
            List<HelperCommon.GenericItem> list = new List<HelperCommon.GenericItem>();
            HelperCommon.GenericItem entity = null;
            foreach (var item in listTrans)
            {
                entity = new HelperCommon.GenericItem()
                {
                    Code = item.Code,
                    Description = item.Description,
                    Code2 = item.Code2
                };
                list.Add(entity);
            }
            return list;
        }

        private HelperCommon.Typification MappingTypification(CommonTransacService.Typification typifDB)
        {
            HelperCommon.Typification entity = null;
            entity = new HelperCommon.Typification()
            {
                IdClass = typifDB.CLASE_CODE,
                Class = typifDB.CLASE,
                IdSubClass = typifDB.SUBCLASE_CODE,
                SubClass = typifDB.SUBCLASE,
                Type = typifDB.TIPO
            };
            return entity;
        }
        #endregion



        public JsonResult GetCacDacType(string strIdSession)
        {

            CacDacTypeResponseCommon objCacDacTypeResponseCommon;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            CacDacTypeRequestCommon objCacDacTypeRequestCommon =
                new CacDacTypeRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objCacDacTypeResponseCommon =
                    Logging.ExecuteMethod<CacDacTypeResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetCacDacType(objCacDacTypeRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCacDacTypeRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objCacDacTypeResponseCommon != null && objCacDacTypeResponseCommon.CacDacTypes != null)
            {
                objCommonServices = new CommonServices();
                List<CacDacTypeVM> listCacDacTypes =
                    new List<CacDacTypeVM>();

                foreach (CommonTransacService.ListItem item in objCacDacTypeResponseCommon.CacDacTypes)
                {
                    listCacDacTypes.Add(new CacDacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objCommonServices });
        }


        public JsonResult GetPuntosAtencion(string strIdSession)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            Logging.Info(audit.Session, audit.transaction, "Entra a GetPuntosAtencion");
            responseDataObtenerTabPuntosAtencionPost objPuntosAtencionResponse;

            PuntosAtencionRequest objPuntosAtencionRequest = new PuntosAtencionRequest()
            {
                audit = audit,

                Header = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.HeaderRequest()
                {
                    country = KEY.AppSettings("country"),
                    language = KEY.AppSettings("language"),
                    consumer = KEY.AppSettings("consumer"),
                    system = KEY.AppSettings("strConstUsrAplicacion"),
                    msgType = KEY.AppSettings("msgType"),
                },

                TabPuntosAtencion = new obtenerTabPuntosAtencionPostRequest()
                {
                    title = Constants.DAC,
                },
            };

            try
            {
                objPuntosAtencionResponse = Logging.ExecuteMethod<responseDataObtenerTabPuntosAtencionPost>(() =>
                {
                    return _oServiceCommon.GetPuntosAtencion(objPuntosAtencionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objPuntosAtencionRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            List<CacDacTypeVM> listPuntosAtencion = new List<CacDacTypeVM>();

            if (objPuntosAtencionResponse.listaTabPuntosAtencionPost != null && objPuntosAtencionResponse.listaTabPuntosAtencionPost.Count > 0)
            {
                objPuntosAtencionResponse.listaTabPuntosAtencionPost.ToList().ForEach(item =>
                {
                    var objItem = new CacDacTypeVM
                    {
                        Code = item.codele,
                        Description = item.nombre,
                    };
                    listPuntosAtencion.Add(objItem);
                });
            }
            Logging.Info(audit.Session, audit.transaction, "Sale de GetPuntosAtencion");
            return Json(listPuntosAtencion, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBillingCycle(string strTypeCustomer, string strIdSession)
        {
            BillingCycleResponse objBillingCycleResponse = null;
            PostTransacService.AuditRequest audit =
                Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            BillingCycleRequest objBillingCycleRequest = new BillingCycleRequest()
            {
                audit = audit,
                strTypeCustomer = strTypeCustomer
            };

            try
            {
                objBillingCycleResponse = Logging.ExecuteMethod<BillingCycleResponse>(() =>
                {
                    return _oServicePostpaid.GetBillingCycle(objBillingCycleRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objBillingCycleResponse.LstBillingCycleResponse, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetArea(string strIdSession)
        {
            AreaResponse objAreaResponse = null;
            PostTransacService.AuditRequest audit =
                Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            AreaRequest objRequest = new AreaRequest()
            {
                audit = audit,
            };

            try
            {
                objAreaResponse = Logging.ExecuteMethod<AreaResponse>(() =>
                {
                    return _oServicePostpaid.GetArea(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objAreaResponse.lstArea, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMotive(string strIdSession, string strIdArea)
        {
            try
            {
                PostTransacService.AuditRequest audit =
                    Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
                MotiveByAreaRequest model =
                    new MotiveByAreaRequest();

                #region Momentaneo

                model.audit = audit;

                #endregion

                model.strIdArea = strIdArea;
                PostTransacServiceClient Service =
                    new PostTransacServiceClient();
                var result = Service.GetMotiveByArea(model);
                return Json(result.lstReasonByArea, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetSubMotive(string strIdSession, string strIdArea, string strIdMotive)
        {
            try
            {
                PostTransacService.AuditRequest audit =
                    Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
                SubMotiveRequest model =
                    new SubMotiveRequest();

                #region Momentaneo

                model.audit = audit;

                #endregion

                model.strIdArea = strIdArea;
                model.strIdMotive = strIdMotive;
                PostTransacServiceClient Service =
                    new PostTransacServiceClient();
                var result = Service.GetSubMotive(model);
                return Json(result.lstSubMotive, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetListValueXmlMethod(string strIdSession, string strNameFunction, string strFlagCode = "", string fileName = "")
        {
            List<ListItemVM> lstListItemVm = new List<ListItemVM>();

            List<ItemGeneric> listaItem = Functions.GetListValuesXML(strNameFunction, strFlagCode, fileName);
            listaItem.ForEach(item =>
            {
                lstListItemVm.Add(new ListItemVM()
                {
                    Code = item.Code,
                    Code2 = item.Code2,
                    Description = item.Description
                });
            });

            lstListItemVm = lstListItemVm.Where(x => x.Code2 != "CO").ToList();

            return Json(new { data = lstListItemVm });
        }

        public List<ListItemVM> ListValueXmlMethod(string keyAppSettings, string strFlagCode = "", string fileName = "")
        {
            List<ListItemVM> lstListItemVm = new List<ListItemVM>();

            List<ItemGeneric> listaItem = Functions.GetListValuesXML(keyAppSettings, strFlagCode, fileName);
            listaItem.ForEach(item =>
            {
                lstListItemVm.Add(new ListItemVM()
            {
                Code = item.Code,
                Code2 = item.Code2,
                Description = item.Description
            });
            });

            lstListItemVm = lstListItemVm.Where(x => x.Code2 != "CO").ToList();
            return lstListItemVm;

            #region MyRegion
            //List<ListItemVM> lstListItemVM = new List<ListItemVM>();
            //var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestCommon>(strIdSession);
            //var objListItemRequest = new ListItemRequest
            //{
            //    audit = objAuditRequest,
            //    strFlagCode = "1",
            //    fileName = fileName
            //};

            //string strTypeLineAct = string.Empty;
            //strTypeLineAct = "CO";

            //if (ConfigurationManager.AppSettings("gConstTipoLineaActual").Split('|')[0].Trim() == keyAppSettings)
            //{
            //    objListItemRequest.strNameFunction = ConfigurationManager.AppSettings("strObtenerRubroTipCliMOV");
            //}
            //else if (ConfigurationManager.AppSettings("gConstTipoLineaActual").Split('|')[1].Trim() == keyAppSettings)
            //{
            //    objListItemRequest.strNameFunction = ConfigurationManager.AppSettings("strObtenerRubroTipCliTFI");
            //}
            //else
            //{
            //    objListItemRequest.strNameFunction = keyAppSettings;
            //    objListItemRequest.strFlagCode = "";
            //}
            //try
            //{
            //    var objListItemResponse = Logging.ExecuteMethod<ListItemResponse>(() =>
            //    {
            //        return _oServiceCommon.GetListValueXML(objListItemRequest);
            //    });
            //    objListItemResponse.lstListItem.ToList().ForEach(item =>
            //    {
            //        lstListItemVM.Add(new ListItemVM()
            //        {
            //            Code = item.Code,
            //            Code2 = item.Code2,
            //            Description = item.Description
            //        });
            //    });

            //    lstListItemVM = lstListItemVM.Where(x => x.Code2 != strTypeLineAct).ToList();
            //}
            //catch (Exception ex)
            //{
            //    Logging.Error(objAuditRequest.Session, objListItemRequest.audit.transaction, ex.Message);
            //    throw new Exception(objAuditRequest.transaction);
            //}
            //return lstListItemVM;


            #endregion

        }


        public JsonResult GetCustomerPhone(string strIdSession, int intIdContract)
        {
            var objListItemVM = new List<ListItemVM>();
            var objAuditRequest = Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objCustomerPhoneRequest = new CustomerPhoneRequest()
            {
                audit = objAuditRequest,
                IdContract = intIdContract
            };

            try
            {
                var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
                {
                    return _oServiceFixed.GetCustomerPhone(objCustomerPhoneRequest);
                });

                objCustomerPhoneResponse.LstCustomerPhone.ToList().ForEach(item =>
                {
                    var objItemVM = new ListItemVM
                    {
                        Description = item.Descripcion ?? "",
                    };
                    objListItemVM.Add(objItemVM);
                });

            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objCustomerPhoneRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return Json(objListItemVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListGeneric(string strIdSession, string strClave)
        {
            ParameterBusinnesResponse objResponse = null;
            PostTransacService.AuditRequest audit =
                Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            ParameterBusinnesRequest objRequest = new ParameterBusinnesRequest()
            {
                audit = audit,
                strIdList = strClave
            };

            try
            {
                objResponse = Logging.ExecuteMethod<ParameterBusinnesResponse>(() =>
                {
                    return _oServicePostpaid.GetPlanModel(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objResponse.lstParameterBusinnes });
        }
        public List<ItemGeneric> GetServiceTypesList(string strIdSession, string strClave)
        {
            List<ItemGeneric> lstServiceTypes = new List<ItemGeneric>();
            ParameterBusinnesResponse objResponse = null;
            PostTransacService.AuditRequest audit =
                Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            ParameterBusinnesRequest objRequest = new ParameterBusinnesRequest()
            {
                audit = audit,
                strIdList = strClave
            };

            try
            {
                objResponse = Logging.ExecuteMethod<ParameterBusinnesResponse>(() =>
                {
                    return _oServicePostpaid.GetPlanModel(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            if (objResponse != null && objResponse.lstParameterBusinnes != null)
            {
                foreach (ParameterBusinnes item in objResponse.lstParameterBusinnes)
                {
                    lstServiceTypes.Add(new ItemGeneric()
                    {
                        Code = item.strCode,
                        Description = item.strDescription
                    });
                }
            }
            return lstServiceTypes;
        }

        public JsonResult GetConfig(string strIdSession, string Key)
        {
            string llave;
            try
            {
                llave = KEY.AppSettings(Key);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, Common.GetTransactionID(), ex.Message);
                throw new Exception(ex.Message);
            }
            return Json(new { data = llave });
        }

        //Ruta de Descarga del Excel 
        public FileResult DownloadExcel(string strPath, string strNewfileName)
        {
            return File(strPath, "application/vnd.ms-excel", strNewfileName);
        }

        public JsonResult GetTypification(string strIdSession, string strTransactionName, string strType = "")
        {
            TypificationResponse objTypificationResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest =
                new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;

            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(
                () =>
                {
                    return _oServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (!string.IsNullOrEmpty(strType))
            {
                objTypificationResponse.ListTypification = objTypificationResponse.ListTypification
                    .Where(y => y.TIPO == strType).ToList();
            }
            return Json(objTypificationResponse, JsonRequestBehavior.AllowGet);
        }

        public List<TypificationModel> GetTypificationHFC(string strIdSession, string strTransactionName)
        {
            var response = new List<TypificationModel>();
            TypificationResponse objTypificationResponse = null;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            TypificationRequest objTypificationRequest = new TypificationRequest();
            objTypificationRequest.audit = audit;
            objTypificationRequest.TRANSACTION_NAME = strTransactionName;
            var msg = string.Format("Controlador: {0},Metodo: {1}, WebConfig: {2}", "CallDetailController", "GetTypificationHFC", "SIACU_SP_OBTENER_TIPIFICACION");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            try
            {
                objTypificationResponse = Logging.ExecuteMethod<TypificationResponse>(() =>
                {
                    return _oServiceCommon.GetTypification(objTypificationRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, "Error GetTypificationHFC : " + ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objTypificationResponse.ListTypification;

            if (objTypificationResponse.ListTypification != null)
                Logging.Info("IdSession: " + strIdSession, " objTypificationResponse.ListTypification: Total Reg : ", objTypificationResponse.ListTypification.Count().ToString());
            else
                Logging.Info("IdSession: " + strIdSession, " objTypificationResponse.ListTypification: Total Reg : ", "0 o null");

            response = Mapper.Map<List<TypificationModel>>(tempLst);
            return response;
        }

        public JsonResult GetBusinessRules(string strIdSession, string strSubClase)
        {
            BusinessRulesResponse objBusinessRulesResponse = new BusinessRulesResponse();
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            BusinessRulesRequest objBusinessRulesRequest = new BusinessRulesRequest();
            objBusinessRulesRequest.audit = audit;
            objBusinessRulesRequest.SUB_CLASE = strSubClase;

            Logging.Info("IdSession: " + strIdSession, "Inicio Método : GetBusinessRules", "strSubClase : " + strSubClase);
            try
            {
                objBusinessRulesResponse = Logging.ExecuteMethod<BusinessRulesResponse>(() =>
                {
                    return _oServiceCommon.GetBusinessRules(objBusinessRulesRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Logging.Info("IdSession: " + strIdSession, "Método : GetBusinessRules", "Fín");
            return Json(new { data = objBusinessRulesResponse });
        }

        public JsonResult GetRegions(string strIdSession)
        {
            RegionResponse objRegionResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            RegionRequest objRegionRequest = new RegionRequest();
            objRegionRequest.audit = audit;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<RegionResponse>(() =>
                {
                    return _oServiceCommon.GetRegions(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(new { data = objRegionResponse });
        }

        public bool RestrictPlan(string sCodPlan, string sPlanesRestingidos, string estadoAcceso)
        {
            var blnRetorno = false;
            var blnPerfilAutorizado = false;
            var blnPlanRestringido = false;

            var strkeyRestringirDetalleLlamada =
                ConfigurationManager.AppSettings("gConstkeyRestringirConsultaDetalleLlamadaHFC");
            var strPermisos = estadoAcceso;
            if (strkeyRestringirDetalleLlamada.IndexOf(strPermisos, StringComparison.OrdinalIgnoreCase) + 1 > 0)
            {
                blnPerfilAutorizado = true;
            }

            if (sCodPlan.Length > 0)
            {
                if (sPlanesRestingidos.Length > 0)
                {
                    var sArrayPlanesRestringidos = sPlanesRestingidos.Split(',');
                    foreach (var sPlanRestringido in sArrayPlanesRestringidos)
                    {
                        if (sCodPlan.Trim() == sPlanRestringido.Trim())
                        {
                            blnPlanRestringido = true;
                        }
                    }
                }
            }

            if (blnPlanRestringido)
            {
                if (!blnPerfilAutorizado)
                {
                    blnRetorno = true;
                }
            }

            return blnRetorno;
        }

        public string GetTotalTR_Detail_Calls(List<Helpers.HFC.CallDetails.BilledCallsDetail> lista)
        {
            double Total = 0;
            double TotalMIN = 0;
            double TotalSMS = 0;
            double TotalMMS = 0;
            double TotalGPRS = 0;
            string[] Cantidad;
            double Consumo = 0;

            try
            {
                foreach (Helpers.HFC.CallDetails.BilledCallsDetail item in lista)
                {
                    Cantidad = item.Consumption.Split(char.Parse(":"));

                    if (Cantidad.Length.Equals(1))
                    {
                        Consumo = Functions.CheckDbl(Cantidad[0]);
                    }
                    else
                    {
                        if (Cantidad.Length.Equals(2))
                            Consumo = (Functions.CheckDbl(Cantidad[0]) * 60) + Functions.CheckDbl(Cantidad[1]);
                        else
                            Consumo = (Functions.CheckDbl(Cantidad[0]) * 3600) +
                                      (Functions.CheckDbl(Cantidad[1]) * 60) + Functions.CheckDbl(Cantidad[2]);
                    }
                    if (item.TypeCalls != null)
                    {
                        if (item.TypeCalls.ToUpper().IndexOf("LLAMADA") != -1 ||
                            item.TypeCalls.ToUpper().IndexOf("MOC") != -1)
                        {
                            TotalMIN += Consumo;
                        }
                        else
                        {
                            if (item.TypeCalls.ToUpper().IndexOf("SMS") != -1)
                            {
                                TotalSMS += Consumo;
                            }
                            else
                            {
                                if (item.TypeCalls.ToUpper().IndexOf("MMS") != -1)
                                {
                                    TotalMMS += Consumo;
                                }
                                else
                                {
                                    if (item.TypeCalls.ToUpper().IndexOf("GPRS") != -1)
                                    {

                                        TotalGPRS += Consumo;
                                    }
                                }
                            }
                        }

                        Total += Functions.CheckDbl(item.CargOriginal);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return Functions.CheckStr(Total) + ";" + Functions.CheckStr(TotalMIN) + ";" + Functions.CheckStr(TotalSMS) +
                   ";" + Functions.CheckStr(TotalMMS) + ";" + Functions.CheckStr(TotalGPRS);
        }

        public TemplateInteractionModel GetDatTemplateInteraction(string transaction, string telephone,
            string nroDoc,
            string email, string monthEmition, string yearEmition, string cacDac,
            string invoceNumber, string note, string name, string lastName,
            string representLegal, string strIdSession, string contratoId)
        {
            var oPlantCampDat = new TemplateInteractionModel();

            oPlantCampDat.NOMBRE_TRANSACCION = transaction;
            oPlantCampDat.X_CLARO_NUMBER = telephone;
            oPlantCampDat.X_DOCUMENT_NUMBER = nroDoc;
            oPlantCampDat.X_FIRST_NAME = name;
            oPlantCampDat.X_LAST_NAME = lastName;
            oPlantCampDat.X_NAME_LEGAL_REP = representLegal;

            if (!string.IsNullOrEmpty(email))
            {
                oPlantCampDat.X_FLAG_REGISTERED = ConstantsHFC.strUno;
            }
            else
            {
                oPlantCampDat.X_FLAG_REGISTERED = ConstantsHFC.strCero;
            }

            oPlantCampDat.X_EMAIL = email ?? string.Empty;
            oPlantCampDat.X_INTER_30 = note;
            var keyAppSettings = "ListaMeses";
            var fileName = "Data.xml";

            var listMonth = ListValueXmlMethod(keyAppSettings, "", fileName);

            var stMonth = monthEmition;
            for (int i = 0; i < listMonth.Count; i++)
            {
                var codeInt = Convert.ToInt(listMonth[i].Code);
                if (codeInt.Equals(Convert.ToInt(monthEmition)))
                {
                    stMonth = listMonth[i].Description;
                }
            }


            oPlantCampDat.X_INTER_29 = stMonth + "-" + yearEmition;
            oPlantCampDat.X_INTER_15 = cacDac;
            oPlantCampDat.X_INTER_16 = invoceNumber;
            oPlantCampDat.X_INTER_18 = contratoId;


            return oPlantCampDat;
        }

        //Insertar Interaccion CLFY
        public Dictionary<string, string> GetInsertInteractionCLFY(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonTransacService.Interaction>(objInteractionModel);
            CommonTransacService.InsertInteractHFCResponse objInteractHFCResponse = null;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertInteractionCLFY", "SIACU_POST_CLARIFY_SP_CREATE_INTERACT_HFC");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            CommonTransacService.InsertInteractHFCRequest objInteractHFCRequest = new CommonTransacService.InsertInteractHFCRequest()
                {
                    audit = audit,
                    Interaction = serviceModelInteraction
                };
            try
            {
                objInteractHFCResponse = Logging.ExecuteMethod<CommonTransacService.InsertInteractHFCResponse>(() =>
                {
                    return _oServiceCommon.GetInsertInteractHFC(objInteractHFCRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var dictionaryResponse = new Dictionary<string, string>
            {
                {"rInteraccionId", objInteractHFCResponse.rInteraccionId},
                {"rFlagInsercion", objInteractHFCResponse.rFlagInsercion},
                {"rMsgText", objInteractHFCResponse.rMsgText},
                {"rResult", objInteractHFCResponse.rResult.ToString()},
            };

            Logging.Info(strIdSession, strIdSession, "strInteractionId Common: " + objInteractHFCResponse.rInteraccionId);
            Logging.Info(strIdSession, strIdSession, "strFlagInsert Common: " + objInteractHFCResponse.rFlagInsercion);
            Logging.Info(strIdSession, strIdSession, "strMsgText Common: " + objInteractHFCResponse.rMsgText);
            Logging.Info(strIdSession, strIdSession, "rResult Common: " + objInteractHFCResponse.rResult);

            return dictionaryResponse;
        }



        //Insertar Interaccion de Contingencia
        public Dictionary<string, string> GetInsertContingencyInteraction(InteractionModel objInteractionModel, string strIdSession)
        {
            var serviceModelInteraction = Mapper.Map<CommonTransacService.Iteraction>(objInteractionModel);
            InsertInteractResponseCommon objInsertInteractResponse = null;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var msg = string.Format("Controlador: {0}, Metodo: {1}, WebConfig: {2}", "CommonServiceController", "GetInsertContingencyInteraction", "SIACU_POST_DB_SP_INSERTAR_INTERACT");
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, msg);
            InsertInteractRequestCommon objInsertInteractRequest = new InsertInteractRequestCommon()
                {
                    audit = audit,
                    item = serviceModelInteraction
                };


            try
            {
                objInsertInteractResponse =
                    Logging.ExecuteMethod<InsertInteractResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetInsertInteract(objInsertInteractRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }


            var dictionaryResponse = new Dictionary<string, string>
            {
                {"rInteraccionId", objInsertInteractResponse.Interactionid},
                {"rFlagInsercion", objInsertInteractResponse.FlagInsercion},
                {"rMsgText", objInsertInteractResponse.MsgText},
                {"rResult", objInsertInteractResponse.ProcesSucess.ToString()},
            };

            return dictionaryResponse;
        }

        //Insertar Plantilla de Interaccion
        public Dictionary<string, object> InsertPlantInteraction(TemplateInteractionModel objInteractionTemplateModel, string rInteraccionId, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession)
        {
            string strTransaccion = Functions.CheckStr(objInteractionTemplateModel.NOMBRE_TRANSACCION);
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");
            Dictionary<string, object> dictionaryResponse;
            if (contingenciaClarify != ConstantsHFC.blcasosVariableSI)
            {
                var serviceModelInteraction = Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objInteractionTemplateModel);
                InsertTemplateInteractionResponseCommon objInsertTemplInteractResponse = null;
                CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                InsertTemplateInteractionRequestCommon objInsertTempInteractRequest = new InsertTemplateInteractionRequestCommon()
                    {
                        audit = audit,
                        item = serviceModelInteraction,
                        IdInteraction = rInteraccionId
                    };
                try
                {
                    objInsertTemplInteractResponse = Logging.ExecuteMethod<InsertTemplateInteractionResponseCommon>(() => { return _oServiceCommon.GetInsertInteractionTemplate(objInsertTempInteractRequest); });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
                dictionaryResponse = new Dictionary<string, object>
                {
                    {"FlagInsercion", objInsertTemplInteractResponse.FlagInsercion},
                    {"MsgText", objInsertTemplInteractResponse.MsgText},
                    {"ProcesSucess", objInsertTemplInteractResponse.ProcesSucess},
                };

                Logging.Info(strIdSession, strIdSession, "FlagInsercion Common: " + objInsertTemplInteractResponse.FlagInsercion);
                Logging.Info(strIdSession, strIdSession, "MsgText Common: " + objInsertTemplInteractResponse.MsgText);
                Logging.Info(strIdSession, strIdSession, "ProcesSucess Common: " + objInsertTemplInteractResponse.ProcesSucess);
            }
            else
            {
                //Contigencia
                var serviceModelInteraction = Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objInteractionTemplateModel);
                InsTemplateInteractionResponseCommon objInsertTemplInteractResponse = null;
                CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                InsTemplateInteractionRequestCommon objInsertTempInteractRequest = new InsTemplateInteractionRequestCommon()
                    {
                        audit = audit,
                        item = serviceModelInteraction,
                        IdInteraction = rInteraccionId
                    };
                try
                {
                    objInsertTemplInteractResponse = Logging.ExecuteMethod<InsTemplateInteractionResponseCommon>(() => { return _oServiceCommon.GetInsInteractionTemplate(objInsertTempInteractRequest); });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }

                dictionaryResponse = new Dictionary<string, object>
                {
                    {"FlagInsercion", objInsertTemplInteractResponse.FlagInsercion},
                    {"MsgText", objInsertTemplInteractResponse.MsgText},
                    {"ProcessSucess", objInsertTemplInteractResponse.ProcessSucess},
                };

            }
            return dictionaryResponse;
        }

        //Obtner Datos Plantilla de Interaccion
        public TemplateInteractionModel GetInfoInteractionTemplate(string strIdSession, string strInteraccionId)
        {
            var model = new TemplateInteractionModel();

            DatTempInteractionResponse objDatTempInteractionResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            DatTempInteractionRequest objInteractHFCRequest =
                new DatTempInteractionRequest()
                {
                    audit = audit,
                    vInteraccionID = strInteraccionId
                };

            try
            {
                objDatTempInteractionResponse = Logging.ExecuteMethod<DatTempInteractionResponse>(
                    () =>
                    {
                        return _oServiceCommon.GetInfoInteractionTemplate(objInteractHFCRequest);
                    });
                var tempLst = objDatTempInteractionResponse.InteractionTemplate;
                model = Mapper.Map<TemplateInteractionModel>(tempLst);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return model;
        }

        public string CreateArcchivePdf(string html, string Name)
        {
            var msg1 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "CreateArcchivePdf", "Llamando la ruta para el archivo ");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
            var strRutaGeneradorPdf = ConfigurationManager.AppSettings("constRutaArchivosPdf");
            var strRutArchivePdf = strRutaGeneradorPdf + Name + SIACU.Transac.Service.Constants.PresentationLayer.gstrExtensionPDF;
            return GeneratePdf(html, strRutArchivePdf);
        }

        public string GeneratePdf(string html, string strRutArchivePdf)
        {
            var msg1 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "GeneratePdf", "Generando el Archivo");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg1);
            var document = new Document();
            PdfWriter.GetInstance(document, new FileStream(strRutArchivePdf, FileMode.Create));
            document.Open();
            var hw = new iTextSharp.text.html.simpleparser.HTMLWorker(document);
            hw.Parse(new StringReader(html));
            document.Close();

            var msg2 = string.Format("CONTROLLER: {0},METODO: {1}, RESULTADO: {2}", "CommonServiceController", "GeneratePdf", "Guardando el Archivo");
            Logging.Info("IdSession: " + "", "Transaccion: " + "", msg2);

            return strRutArchivePdf;
        }

        public string CreateHeaderEmail(string pstrTitle, string pstrCAC, string pstrDate,
            string pstrTitular, string pstrCaseInteraction, string pstrRepresent,
            string NroClaro, string pstrTypeDocument, string pstrNroDoc, string pstrTelephone)
        {

            string strReturn = string.Empty;

            strReturn = "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strReturn += "<tr><td width='100%' class='Estilo1'>Estimado Cliente:</td></tr>";
            strReturn += "<tr><td height='10'></td>";
            if (pstrTitle == "Servicio de Variación de Débito / Crédito Manual Corporativo")
            {
                strReturn +=
                    "<tr><td class='Estilo1'>Por la presente queremos informarle que se realizo un ajuste al saldo de su linea corporativa</td></tr>";
            }
            else
            {
                strReturn += "<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " +
                             pstrTitle + " fue atendida.</td></tr>";
            }

            strReturn += "</table>";
            return strReturn;
        }

        public bool GetSendEmail(SendEmailModel model)
        {
            SendEmailResponseCommon objGetSendEmailResponse = new SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.strIdSession);
            SendEmailRequestCommon objGetSendEmailRequest;
            objGetSendEmailRequest = new SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = model.strTo,
                    strTo = model.strSender,
                    strMessage = model.strMessage,
                    strAttached = model.strAttached,
                    strSubject = model.strSubject
                };

            try
            {
                objGetSendEmailResponse = Logging.ExecuteMethod<SendEmailResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetSendEmail(objGetSendEmailRequest);
                    });
                var result = objGetSendEmailResponse.ExtensionData;
            }
            catch (Exception ex)
            {
                Logging.Error(model.strIdSession, AuditRequest.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return true;
        }

        public bool SaveAudit(string strIdSession, string strCuentaUsuario, string strIpCliente, string strIpServidor,
            string strMonto, string strNombreCliente, string strNombreServidor,
            string strServicio, string strTelefono, string strTexto, string strTransaccion)
        {
            SaveAuditResponseCommon objRegAuditResponseCommon = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SaveAuditRequestCommon objRegAuditRequestCommon =
                new SaveAuditRequestCommon()
                {
                    audit = audit,
                    vCuentaUsuario = strCuentaUsuario,
                    vIpCliente = strIpCliente,
                    vIpServidor = strIpServidor,
                    vMonto = strMonto,
                    vNombreCliente = strNombreCliente,
                    vNombreServidor = strNombreServidor,
                    vServicio = strServicio,
                    vTelefono = strTelefono,
                    vTexto = strTexto,
                    vTransaccion = strTransaccion
                };

            try
            {
                objRegAuditResponseCommon = Logging.ExecuteMethod<SaveAuditResponseCommon>(
                    () =>
                    {
                        return _oServiceCommon.SaveAudit(objRegAuditRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var result = objRegAuditResponseCommon.respuesta;

            return result;
        }

        public bool SaveAuditM(string strTransaction, string strService, string strText, string strTelephone, string strNameCustomer, string strIdSession, string strNameServidor, string strIpServidor, string strIpCustomer = "", string strCuentUser = "", string strMontoInput = "0")
        {
            var strMonto = strMontoInput == "0" ? ConstantsHFC.strCero : strMontoInput;

            SaveAuditMResponseCommon objRegAuditResponseMCommon = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SaveAuditMRequestCommon objRegAuditRequestMCommon =
                new SaveAuditMRequestCommon()
                {
                    audit = audit,
                    vTransaccion = strTransaction,
                    vServicio = strService,
                    vIpCliente = strIpCustomer,
                    vNombreCliente = strNameCustomer,
                    vIpServidor = strIpServidor,
                    vNombreServidor = strNameServidor,
                    vCuentaUsuario = strCuentUser,
                    vTelefono = strTelephone,
                    vMonto = strMonto,
                    vTexto = strText
                };

            try
            {
                objRegAuditResponseMCommon = Logging.ExecuteMethod<SaveAuditMResponseCommon>(
                    () =>
                    {
                        return _oServiceCommon.SaveAuditM(objRegAuditRequestMCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var result = objRegAuditResponseMCommon.respuesta;

            return result;
        }

        public string GetAccessOfPage(string strIdSession)
        {
            var model = new ConsultSecurityModel();
            model.ListConsultSecurity = new List<SecurityModel>();
            var strKey = string.Empty;

            int strCodeApp = Convert.ToInt(ConfigurationManager.AppSettings("CodAplicacion"));

            PagOptionXuserResponse objPagOptionXuserResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            PagOptionXuserRequest objPagOptionXuserRequest = new PagOptionXuserRequest()
            {
                audit = audit,
                IntUser = Convert.ToInt(strIdSession),
                IntAplicationCode = strCodeApp
            };

            try
            {
                objPagOptionXuserResponse = Logging.ExecuteMethod<PagOptionXuserResponse>(() =>
                {
                    return _oServiceCommon.GetPagOptionXuser(objPagOptionXuserRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var tempLst = objPagOptionXuserResponse.ListConsultSecurity;
            model.CodeErr = objPagOptionXuserResponse.CodeErr;
            model.ErrMessage = objPagOptionXuserResponse.ErrMessage;
            model.ListConsultSecurity = Mapper.Map<List<SecurityModel>>(tempLst);

            model.ListConsultSecurity.ForEach(x =>
            {
                strKey = strKey + x.Opcicabrev + ",";
            });

            return strKey.ToUpper();
        }

        public bool LocalAuthorization(string strIdSession, string pUsuario, string strKey)
        {
            var resultado = false;
            var usuario = string.Empty;
            //AUDITORIA

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objRequest = new VerifyUserRequest
            {
                audit = audit,
                AppName = nomApp,
                TransactionId = audit.transaction,
                AppCode = codApp,
                SessionId = strIdSession,
                AppId = ipApp,
                Username = pUsuario.Trim()
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var item = objResponse.LstConsultSecurities[0];
                    usuario = item.Usuaccod;
                }
                else
                {
                    usuario = string.Empty;
                }

                //var strPermisos = "PERMISOS"; //Auditoria ListarAccesosPagina(usuario)
                var strPermisos = GetAccessOfPage(strIdSession);
                if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return resultado;
        }

        public bool IsAuthenticated_LDAP(string strIdSession, string transaction, string vUsuario, string vClave)
        {
            var resultado = false;
            var strDominio = KEY.AppSettings("DominioLDAP");
            var entry = new DirectoryEntry(strDominio, vUsuario, vClave);
            try
            {
                var obj = entry.NativeObject;
                var search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + vUsuario + ")" };
                search.PropertiesToLoad.Add("cn");
                var result = search.FindOne();

                if (result != null)
                {
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
            }

            return resultado;
        }

        public bool Authorize_TX(string strIdSession, string transaction, string pUsuario, string pClave, string strKey)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var response = false;
            try
            {
                if (resultado)
                {
                    var usuario = string.Empty;

                    var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
                    var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
                    var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

                    CommonTransacService.AuditRequest audit =
                        Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                    var objRequest = new VerifyUserRequest
                    {
                        audit = audit,
                        AppName = nomApp,
                        TransactionId = audit.transaction,
                        AppCode = codApp,
                        SessionId = strIdSession,
                        AppId = ipApp,
                        Username = pUsuario.Trim()
                    };

                    var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));

                    if (objResponse.LstConsultSecurities.Count > 0)
                    {
                        var item = objResponse.LstConsultSecurities[0];
                        usuario = item.Usuaccod;

                        //var strPermisos = "PERMISOS"; //Auditoria ListarAccesosPagina(usuario)
                        var strPermisos = GetAccessOfPage(strIdSession);
                        if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            response = true;
                            Session["UsuarioValidador"] = pUsuario.Trim();
                        }
                        else
                        {
                            Session["UsuarioValidador"] = string.Empty;
                        }
                    }
                    else
                    {
                        usuario = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
                throw;
            }

            return response;
        }

        public string ConsultProfile_DCM(string strIdSession, string transaction, string pUsuario, string pClave)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var strCodPerfil = string.Empty;

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            if (resultado)
            {
                resultado = false;
                var currentWindowsIdentity = (WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                var objRequest = new VerifyUserRequest
                {
                    audit = audit,
                    AppName = nomApp,
                    TransactionId = audit.transaction,
                    AppCode = codApp,
                    SessionId = strIdSession,
                    AppId = ipApp,
                    Username = pUsuario.Trim()
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var objLst = objResponse.LstConsultSecurities;
                    foreach (var obj in objLst)
                    {
                        strCodPerfil = strCodPerfil + obj.Perfccod + ",";
                    }
                }
                else
                {
                    strCodPerfil = "";
                }

                strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1);

                var sPerfilAutorizado = strCodPerfil.Length > 0 ? strCodPerfil : "0";

                return sPerfilAutorizado;
            }

            return "0";
        }

        public string ConsultProfile(string strIdSession, string transaction, string pUsuario, string pClave,
            string pCadenaPerfiles)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var strCodPerfil = string.Empty;
            var arrPerfiles = pCadenaPerfiles.Split('|');

            var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
            var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
            var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

            var sPerfilAutorizado = string.Empty;

            if (resultado)
            {
                resultado = false;
                var currentWindowsIdentity = (WindowsIdentity)System.Web.HttpContext.Current.User.Identity;
                var impersonationContext = currentWindowsIdentity.Impersonate();
                impersonationContext.Undo();

                var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                var objRequest = new VerifyUserRequest
                {
                    audit = audit,
                    AppName = nomApp,
                    TransactionId = audit.transaction,
                    AppCode = codApp,
                    SessionId = strIdSession,
                    AppId = ipApp,
                    Username = pUsuario.Trim()
                };

                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));
                if (objResponse.LstConsultSecurities.Count > 0)
                {
                    var objLst = objResponse.LstConsultSecurities;
                    foreach (var obj in objLst)
                    {
                        strCodPerfil = strCodPerfil + obj.Perfccod + ",";
                    }
                }
                else
                {
                    strCodPerfil = "";
                }

                strCodPerfil = strCodPerfil.Substring(0, strCodPerfil.Length - 1);

                if (strCodPerfil.Length > 0)
                {
                    var sArrayPerfiles = strCodPerfil.Split(',');
                    foreach (var itemOne in arrPerfiles)
                    {
                        foreach (var itemTwo in sArrayPerfiles)
                        {
                            if (itemTwo == itemOne)
                            {
                                sPerfilAutorizado = itemOne;
                            }
                        }

                        if (!string.IsNullOrEmpty(sPerfilAutorizado))
                        {
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(sPerfilAutorizado))
                    {
                        sPerfilAutorizado = "0";
                    }
                }
                else
                {
                    sPerfilAutorizado = "0";
                }

                return sPerfilAutorizado;
            }

            return "0";
        }

        public void RecoverAccessPage(string strIdSession, string transaction)
        {
            try
            {
                //Servicio Auditoria
                //Seteo de permisos a session.
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
                throw;
            }
        }



        private ArrayList ObtieneFranjasHorarias(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        {
            ArrayList Items = new ArrayList();
            string idTran, ipApp, nomAp, usrAp;
            try
            {
                idTran = Common.GetTransactionID();
                ipApp = Common.GetApplicationIp();
                nomAp = KEY.AppSettings("NombreAplicacion");
                usrAp = Common.CurrentUser;

                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

                int fID = Convert.ToInt(Functions.GetValueFromConfigFile("strDiasConsultaCapacidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    ));
                DateTime[] dDate = new DateTime[fID];

                dDate[0] = dInitialDate;

                for (int i = 1; i <= fID; i++)
                {
                    dInitialDate = dInitialDate.AddDays(1);
                    dDate[i] = dInitialDate;
                }

                Boolean vExistSesion = false;
                string strUbicacion = Functions.GetValueFromConfigFile("strCodigoUbicacion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    );
                string[] vUbicaciones = { strUbicacion };
                Boolean v1, v2, v3, v4, v5, v6, v7, v8;

                v1 = Boolean.Parse(Functions.GetValueFromConfigFile("strCalcDuracion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v2 = Boolean.Parse(Functions.GetValueFromConfigFile("strCalcDuracionEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v3 = Boolean.Parse(
                    Functions.GetValueFromConfigFile("strCalcTiempoViaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v4 = Boolean.Parse(Functions.GetValueFromConfigFile("strCalcTiempoViajeEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v5 = Boolean.Parse(Functions.GetValueFromConfigFile("strCalcHabTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v6 = Boolean.Parse(Functions.GetValueFromConfigFile("strCalcHabTrabajoEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));
                v7 = Boolean.Parse(Functions.GetValueFromConfigFile("strObtenerZonaUbi", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    ));
                v8 = Boolean.Parse(Functions.GetValueFromConfigFile("strObtenerZonaUbiEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")));


                String vHabTra = String.Empty;
                vHabTra = Functions.GetValueFromConfigFile("strCodigoHabilidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

                string[] vEspacioTiempo = { string.Empty };
                string[] HabilidadTrabajo = { vHabTra };

                //    Dim vespacioTiempo As String() = {String.Empty}
                //Dim vhabilidtrab As String() = {vstrHabTra}


            }
            catch (Exception)
            {

                throw;
            }



            return Items;
        }

        public static string ValidatePlanTFI(string strType, string flagTFI)
        {
            Logging.Info("Session: en ValidatePlanTFI", "Transaction: Entra a ValidatePlanTFI", "Message" + strType + "/" + flagTFI);

            string strTFIPostpaid = "";
            if (flagTFI == "SI")
            {
                strTFIPostpaid = KEY.AppSettings("gConstProductoTFIPOSTPAGO");
                strType = strTFIPostpaid + strType;
            }

            Logging.Info("Session: en ValidatePlanTFI", "Transaction: Sale de ValidatePlanTFI", "Message" + strTFIPostpaid + "/" + strType);

            return strType;
        }

        public JsonResult ValidateUser(string strIdSession, string transaction, string txtUsuario, string txtPass,
            string hidPagina, string hidMonto, string hidUnidad, string hidModalidad, string hidDescripcionProceso,
            string hidTipoA, string hidCo, string hidMotivoA, string hidTelefono, string hidAccion, string hidVeces,
            string hidOpcion, string hidPagDCM, string hidConcepto, string transaccion, string tecnologia)
        {
            var dictionaryValidateUser = new Dictionary<string, object>();
            var blnCorrecto = false;
            var blnRetorno = false;

            var sUsuario = txtUsuario.Trim();
            var sContrasena = txtPass.Trim();
            var sPagina = hidPagina.Trim();
            var sMonto = hidMonto.Trim();
            var sUnidad = hidUnidad.Trim();
            var strmodalidad = hidModalidad.Trim();
            var sDescripcionProceso = hidDescripcionProceso.Trim();

            dictionaryValidateUser.Add("hidTelefono", hidTelefono);

            var resultado = string.Empty;
            var sCodPerfil = string.Empty;
            var sCadenaPerfilesAutorizar =
                Functions.GetValueFromConfigFile("JerarquiaPerfiles", ConfigurationManager.AppSettings("strConstArchivoSIACPOConfigMsg"));
            var vTipoTelefono = string.Empty;


            dictionaryValidateUser.Add("hidUserValidator", txtUsuario);
            dictionaryValidateUser.Add("gConstKeyStrResultValLogOK",
                ConfigurationManager.AppSettings("gConstKeyStrResultValLogOK"));
            dictionaryValidateUser.Add("gConstKeyStrResultValLogCANCEL",
                ConfigurationManager.AppSettings("gConstKeyStrResultValLogCANCEL"));
            dictionaryValidateUser.Add("gConstkeyTransaccionConsultaBiometria",
                ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaBiometria"));

            var hidUserValidator = txtUsuario; // Enviar como resultado.

            //INICIALIZACION DE AUDITORIA :(

            var dblRpta = 0.00;
            var dblCodUsuario = 0.00;
            var strIP = string.Empty;
            var dblCodOpcion = 0.00;
            var strDescripcion = string.Empty;
            var dblCodEvento = 0.00;
            var dblCodPerfil = 0.00;
            var strLogin = string.Empty;
            var intCodEstado = 1;
            var intResultado = 0;

            var detalle = new string[8, 3];
            var strFecha = DateTime.UtcNow.ToShortDateString();
            var strSeparador = string.Empty;
            var arrPerfil =
                "ESPERANDO A SESSION"; //Session["codPerfil"].ToString().Split(System.Convert.ToChar(strSeparador));

            if (!string.IsNullOrEmpty(hidTipoA) && !string.IsNullOrEmpty(hidCo))
            {
                dblCodUsuario = System.Convert.ToDouble(Session["codUsuario"]);
                strIP = Request.ServerVariables["REMOTE_ADDR"];
                dblCodEvento =
                    System.Convert.ToDouble(ConfigurationManager.AppSettings("gConstEvtFallasValidacionUyP"));
                dblCodOpcion = System.Convert.ToDouble(hidCo);
                strDescripcion =
                    "DataThroughWebServicesServiceReference.DataThroughWebServicesServiceClient de Fallas de Validacion de User y Password";

                dblCodPerfil = arrPerfil.Length > 0 ? System.Convert.ToInt64(arrPerfil[0]) : 0;

                var userValidator = string.IsNullOrEmpty(hidUserValidator) ? "CURRENTUSER" : hidUserValidator;

                strLogin = "CurrentUser"; //CurrentUser()

                detalle[1, 1] = "aUsuario";
                detalle[1, 2] = strLogin;
                detalle[1, 3] = "Usuario Login";

                detalle[2, 1] = "bNombrePC";
                detalle[2, 2] = strIP;
                detalle[2, 3] = "IP de PC";

                detalle[3, 1] = "cFecha";
                detalle[3, 2] = strFecha;
                detalle[3, 3] = "Fecha";

                detalle[4, 1] = "dTipoAjuste";
                detalle[4, 2] = hidTipoA;
                detalle[4, 3] = "Tipo de Ajuste";

                detalle[5, 1] = "eMotivoAjuste";
                detalle[5, 2] = hidMotivoA;
                detalle[5, 3] = "Motivo de ajuste";

                detalle[6, 1] = "fImporte";
                detalle[6, 2] = hidMonto;
                detalle[6, 3] = "Importe";

                detalle[7, 1] = "gTelefono";
                detalle[7, 2] = hidTelefono;
                detalle[7, 3] = "Telefono Ajustado";

                detalle[8, 1] = "hUsuarioAprobador";
                detalle[8, 2] = userValidator;
                detalle[8, 3] = "Usuario que Intenta Autorizar";

                try
                {
                    if (Convert.ToInt(hidVeces) > 1)
                    {
                        dblRpta = 0;
                        RegisterAudit(strDescripcion, dblCodEvento, detalle, strIdSession);

                    }
                }
                catch (Exception e)
                {
                    Logging.Info(strIdSession, transaction, e.Message);
                    dictionaryValidateUser.Add("hidAccion", "E");
                }
            }
            if (tecnologia.Equals("Prepaid"))
            {
                if (sPagina == "4" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }
                else if (sPagina == "5")
                {
                    blnCorrecto = Authorize_TX_Search(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    dictionaryValidateUser.Add("Reseteo", hidPagina == "6");
                }
                else if (sPagina == "4" || sPagina == "6" || sPagina == "5")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "10")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValSG");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValF");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "16")
                {
                    var strRes = string.Empty;
                    var strPerfiles = string.Empty;
                    var item = string.Empty;
                    var blResultado = false;

                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "V";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "17")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "V";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }
            }
            else if (tecnologia.Equals("Postpaid"))
            {
                if (sPagina == "1" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    if (hidPagina == "6")
                    {
                        dictionaryValidateUser.Add("Reseteo", true);
                    }
                    else
                    {
                        dictionaryValidateUser.Add("Reseteo", false);
                    }
                }
                else if (sPagina == "1" || sPagina == "6")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "2")
                {
                    blnCorrecto = false;
                    strmodalidad = "";
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }

                    if (hidPagDCM == "DEBITO_CREDITO_MANUAL")
                    {
                        sCodPerfil = ConsultProfile_DCM(strIdSession, transaction, sUsuario, sContrasena);
                        resultado = EvaluateAmount_DCM(strIdSession, transaction, sCodPerfil, sMonto, sUnidad,
                            strmodalidad, hidConcepto);
                    }
                    else
                    {
                        sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                            sCadenaPerfilesAutorizar);
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                    }

                    if (resultado == "1")
                    {
                        blnCorrecto = true;
                        Session["UsuarioValidador"] = txtUsuario.Trim();
                    }

                    dictionaryValidateUser.Add("hidAccion", blnCorrecto ? "G" : "F");
                }

                if (sPagina == "3")
                {
                    var strConcepto = strmodalidad;
                    dictionaryValidateUser.Add("hidConcepto",
                        ListValueXmlMethod_Auth(strIdSession, transaccion, "SIACPODatosPos.xml",
                            "ListaValidaMontoXTransaccion", "1") + strConcepto);

                    blnRetorno = true;
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }
                    sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                        sCadenaPerfilesAutorizar);
                    if (sCodPerfil == "0")
                    {
                        dictionaryValidateUser.Add("hidAccion", "L");
                    }
                    else
                    {
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                        if (resultado == "1")
                        {
                            blnRetorno = true;
                        }
                        else
                        {
                            blnRetorno = false;
                        }

                        if (blnRetorno)
                        {
                            dictionaryValidateUser.Add("hidAccion", "G");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                        else
                        {
                            dictionaryValidateUser.Add("hidAccion", "M");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                    }
                }

                if (sPagina == "4")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        dictionaryValidateUser.Add("hidAccion", "D");
                    }
                    else
                    {
                        blnRetorno = false;
                        dictionaryValidateUser.Add("hidAccion", "F");
                    }
                }

                if (sPagina == "5")
                {
                    blnRetorno = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    if (blnRetorno)
                    {
                        dictionaryValidateUser.Add("hidAccion", "ADS");
                        if (hidOpcion == "gConstValidaFidelizaApadece")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "FA");
                        }
                        else if (hidOpcion == "gConstFechaProgMigracion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstValidaProgramarFecha")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaMigracion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConstActivRetencion")
                        {
                            hidUserValidator = txtUsuario.Trim() + "_" + "ACTRETCHK";
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "ACTRETCHK");
                        }

                        else if (hidOpcion == "gConstModFechaProgMigraPlan")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaPenalidadMigraPlan")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConsAccesoFidelizaApalece")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "FIDEAPALECECHK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "FIDEAPALECECHK";
                        }

                        else if (hidOpcion == "gConstModFechaProgCamTipClient")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                        }

                        else if (hidOpcion == "gConstFidelizaPenalidadCamTipClient")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PCHECK");
                            hidUserValidator = txtUsuario.Trim() + "_" + "PCHECK";
                        }

                        else if (hidOpcion == "gConstModFechaProgCamCicloFact")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" + "PF";
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "PF");
                        }
                        else if (hidOpcion == "gConstSinTopeConsAutorizacion")
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            dictionaryValidateUser.Add("hidUserValidator", txtUsuario.Trim() + "_" + "SinTopeConsAut");
                            hidUserValidator = txtUsuario.Trim() + "_" + "SinTopeConsAut";
                        }
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "99")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "H";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = "TN";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "10")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValSG");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = ConfigurationManager.AppSettings("strCCTxtValF");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "16")
                {
                    var strRes = string.Empty;
                    var strPerfiles = string.Empty;
                    var item = string.Empty;
                    var blResultado = false;

                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = ConstantsSIACPO.ConstUno;
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstAutorizado;
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = ConstantsSIACPO.ConstTN;
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstNoAutorizado;
                    }
                }

                if (sPagina == "17")
                {
                    var strRes = string.Empty; //GetTypificationByTransaction
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "AS4G";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstNoAutorizado;
                    }
                }

                if (sPagina == "18")
                {
                    var strRes = string.Empty;
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = "DS4G";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstAutorizado;
                    }
                }

                if (sPagina == "19")
                {
                    var strRes = string.Empty;
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);

                    if (blnCorrecto)
                    {
                        hidAccion = ConfigurationManager.AppSettings("gConstkeyTransaccionConsultaBiometria");
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstAutorizado;
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                        strRes = ConstantsSIACPO.ConstNoAutorizado;
                    }
                }

                var strPaginaVal = ConfigurationManager.AppSettings("gConstKeyStrEnvioLog");
                var strEstOkVal = ConfigurationManager.AppSettings("gConstKeyStrResultValLogOK");
                var strEstCancelVal = ConfigurationManager.AppSettings("gConstKeyStrResultValLogCANCEL");
                if (sPagina == strPaginaVal)
                {
                    var objEstado = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    hidAccion = objEstado ? strEstOkVal : strEstCancelVal;
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                }

            }
            else if (tecnologia.Equals("Fixed"))
            {
                if (sPagina == "1" || sPagina == "6")
                {
                    blnCorrecto = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                }

                if (blnCorrecto)
                {
                    hidAccion = "G";
                    dictionaryValidateUser.Add("hidAccion", hidAccion);
                    Session["UsuarioValidador"] = txtUsuario.Trim();
                    if (hidPagina == "6")
                    {
                        dictionaryValidateUser.Add("Reseteo", true);
                    }
                    else
                    {
                        dictionaryValidateUser.Add("Reseteo", false);
                    }
                }
                else if (sPagina == "1" || sPagina == "6")
                {
                    dictionaryValidateUser.Add("hidAccion", "F");
                }

                if (sPagina == "2")
                {
                    blnCorrecto = false;
                    strmodalidad = "";
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }

                    if (hidPagDCM == Functions.GetValueFromConfigFile("strConstDebCrdManual",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")))
                    {
                        sCodPerfil = ConsultProfile_DCM(strIdSession, transaction, sUsuario, sContrasena);
                        resultado = EvaluateAmount_DCM(strIdSession, transaction, sCodPerfil, sMonto, sUnidad,
                            strmodalidad, hidConcepto);
                    }
                    else
                    {
                        sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                            sCadenaPerfilesAutorizar);
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                    }

                    if (resultado == "1")
                    {
                        blnCorrecto = true;
                        Session["UsuarioValidador"] = txtUsuario.Trim();
                    }

                    dictionaryValidateUser.Add("hidAccion", blnCorrecto ? "G" : "F");
                }

                if (sPagina == "3")
                {
                    var strConcepto = strmodalidad;
                    dictionaryValidateUser.Add("hidConcepto",
                        ListValueXmlMethod_Auth(strIdSession, transaccion, "SIACPODatosPos.xml",
                            "ListaValidaMontoXTransaccion", "1") + strConcepto);

                    blnRetorno = true;
                    if (sMonto == "")
                    {
                        sMonto = "0";
                    }
                    sCodPerfil = ConsultProfile(strIdSession, transaction, sUsuario, sContrasena,
                        sCadenaPerfilesAutorizar);
                    if (sCodPerfil == "0")
                    {
                        dictionaryValidateUser.Add("hidAccion", "L");
                    }
                    else
                    {
                        resultado = EvaluateAmount(strIdSession, transaction, sCodPerfil, sMonto, sUnidad, strmodalidad,
                            hidConcepto);

                        blnRetorno = resultado == "1";

                        if (blnRetorno)
                        {
                            dictionaryValidateUser.Add("hidAccion", "G");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                        else
                        {
                            dictionaryValidateUser.Add("hidAccion", "M");
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                        }
                    }
                }

                if (sPagina == "4")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        dictionaryValidateUser.Add("hidAccion", "D");
                    }
                    else
                    {
                        blnRetorno = false;
                        dictionaryValidateUser.Add("hidAccion", "F");
                    }
                }

                if (sPagina == "5")
                {
                    blnRetorno = Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion);
                    if (blnRetorno)
                    {
                        hidAccion = Functions.GetValueFromConfigFile("strConstActDesServicios",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);

                        if (hidOpcion == Functions.GetValueFromConfigFile("strConstValidaFidelizaApadece",
                                ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValFA",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstFechaProgMigracion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstValidaProgramarFecha",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstFidelizaMigracion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValPCHECK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstActivRetencion",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValACTRETCHK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstModFechaProgMigraPlan",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValPF",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }

                        else if (hidOpcion == Functions.GetValueFromConfigFile("strConstFidelizaPenalidadMigraPlan",
                                     ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")))
                        {
                            Session["UsuarioValidador"] = txtUsuario.Trim();
                            hidUserValidator = txtUsuario.Trim() + "_" +
                                               Functions.GetValueFromConfigFile("strConstUsrValPCHECK",
                                                   ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                            dictionaryValidateUser.Add("hidUserValidator", hidUserValidator);
                        }
                    }
                    else
                    {
                        hidAccion = "F";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }

                if (sPagina == "99")
                {
                    if (Authorize_TX(strIdSession, transaction, sUsuario, sContrasena, hidOpcion))
                    {
                        blnRetorno = true;
                        hidAccion = "H";
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                    else
                    {
                        blnRetorno = false;
                        hidAccion = Functions.GetValueFromConfigFile("strConstUsrValTN",
                            ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                        dictionaryValidateUser.Add("hidAccion", hidAccion);
                    }
                }
            }

            var userValidatorLog = GetEmployerDate(strIdSession, sUsuario);
            dictionaryValidateUser.Add("NamesUserValidator",
                userValidatorLog.strNomb + " " + userValidatorLog.strApPat + ' ' + userValidatorLog.strApMat);
            dictionaryValidateUser.Add("EmailUserValidator", userValidatorLog.strEmail);

            return new JsonResult
            {
                Data = dictionaryValidateUser,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string EvaluateAmount(string strIdSession, string transaction, string vModalidad, string vMonto,
            string vUnidad, string vListaPerfil, string vTipoTelefono)
        {
            var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objRequest = new EvaluateAmountRequest
            {
                audit = audit,
                StrIdSession = strIdSession,
                VModalidad = vModalidad,
                StrTransaction = transaction,
                VMonto = System.Convert.ToDouble(vMonto),
                VUnidad = vUnidad,
                VListaPerfil = vListaPerfil,
                VTipoTelefono = vTipoTelefono
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetEvaluateAmount(objRequest));
            var resultado = objResponse.Resultado ? "1" : "0";
            return resultado;
        }

        public string EvaluateAmount_DCM(string strIdSession, string transaction, string vModalidad, string vMonto,
            string vUnidad, string vListaPerfil, string vTipoTelefono)
        {
            var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objRequest = new EvaluateAmountRequest
            {
                audit = audit,
                StrIdSession = strIdSession,
                VModalidad = vModalidad,
                StrTransaction = transaction,
                VMonto = System.Convert.ToDouble(vMonto),
                VUnidad = vUnidad,
                VListaPerfil = vListaPerfil,
                VTipoTelefono = vTipoTelefono
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetEvaluateAmount_DCM(objRequest));
            var resultado = objResponse.Resultado ? "1" : "0";
            return resultado;
        }

        public JsonResult UserValidate_PageLoad(string strIdSession, string pag = "", string paginadcm = "",
            string monto = "", string opcion = "", string modalidad = "", string tipo = "", string motivo = "",
            string telefono = "", string loginS = "", string co = "", string migracion = "", string descripcion = "",
            string transaccion = "", string detEntAccion = "", string tipotx = "", string unidad = "",
            string hidOpcion = "", string hidAccion = "")
        {
            var hidTelefono = GetNumber(strIdSession, false, telefono);
            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - hidTelefono: " + hidTelefono);
            var dictionaryPageLoad = new Dictionary<string, object>
            {
                {"hidPagina", pag},
                {"hidPagDCM", paginadcm},
                {"hidMonto", monto},
                {"hidUnidad", unidad},
                {"hidOpcion", opcion},
                {"hidModalidad", modalidad},
                {"hidTipoA", tipo},
                {"hidMotivoA", motivo},
                {"hidTelefono", hidTelefono},
                {"hidLogin", loginS},
                {"hidCO", co},
                {"hidMigracion", migracion},
                {"hidDescripcionProceso", descripcion},
                {
                    "hidConcepto",
                    ListValueXmlMethod_Auth(strIdSession, transaccion, "SiacutData.xml", "ListaValidaMontoXTransaccion",
                        "1")
                },
                {"hidAccionDetEnt", detEntAccion}
            };

            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - tipotx: " + tipotx);
            switch (tipotx)
            {
                case "L":
                    dictionaryPageLoad.Add("lblTitulo", ConstantsSIACPO.ConstAutoLinea);
                    break;
                case "E":
                    dictionaryPageLoad.Add("lblTitulo", ConstantsSIACPO.ConstAutoEquipo);
                    break;
                case "M":
                    dictionaryPageLoad.Add("lblTitulo", ConstantsSIACPO.ConstAutorizacion);
                    break;
            }

            Logging.Info(strIdSession, "Transaction: ", "UserValidate_PageLoad() - dictionaryPageLoad: " + dictionaryPageLoad.Count().ToString());
            if (!pag.Equals("15"))
            {
                if (pag.Equals("6") && LocalAuthorization(strIdSession, "CurrentUser", hidOpcion))
                {
                    dictionaryPageLoad.Add("ReseteoLinea", true);
                }
                else
                {
                    dictionaryPageLoad.Add("ReseteoLinea", false);
                }
            }

            return new JsonResult
            {
                Data = dictionaryPageLoad,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public string ListValueXmlMethod_Auth(string strIdSession, string transaccion, string fileName,
            string strNameFunction, string strFlagCode)
        {
            var lstItemVm = new List<ListItemVM>();
            var responseMethod = string.Empty;
            var objAuditRequest = Common.CreateAuditRequest<AuditRequestCommon>(strIdSession);

            try
            {

                var objListItemResponse = Functions.GetListValuesXML(strNameFunction, strFlagCode, fileName);

                foreach (var item in objListItemResponse)
                {
                    if (item.Code2 == transaccion)
                    {
                        responseMethod = item.Code;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error(objAuditRequest.Session, objAuditRequest.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return responseMethod;
        }

        public string ValidatePermissionPost(string idSession, string contractID)
        {
            string strNameTypePhone = "";
            Logging.Info("Session: " + idSession, "Transaction: Start ", "contractID: " + contractID);

            if (!string.IsNullOrEmpty(contractID))
            {
                string var = "";
                string[] var2;
                string[] var3;
                string[] var4;
                string varTFI = "";
                string[] varTFI2;
                string[] varTFI3;
                string message = "";
                string message2 = "";
                string strCodResult = "";
                int intCodPlanTariff = 0;
                int flagFound = 0;

                List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
                List<ParameterTerminalTPIHelper> list2 = new List<ParameterTerminalTPIHelper>();
                CommonService.AuditRequest auditC =
                    Common.CreateAuditRequest<CommonService.AuditRequest>(idSession);
                PostTransacService.AuditRequest audit =
                    Common.CreateAuditRequest<PostTransacService.AuditRequest>(idSession);

                try
                {


                    int intCodParam = Convert.ToInt(KEY.AppSettings("gObtenerParametroTerminalTPI"));
                    int intCodParam2 = Convert.ToInt(KEY.AppSettings("gObtenerParametroSoloTFIPostpago"));


                    ParameterTerminalTPIResponse objResponse = GetParameterTerminalTPI(intCodParam, auditC);
                    list = MappingParameterTerminalTPI(objResponse);
                    message = objResponse.Message;

                    objResponse = GetParameterTerminalTPI(intCodParam2, auditC);
                    list2 = MappingParameterTerminalTPI(objResponse);
                    message2 = objResponse.Message;

                    var = list[0].ValorC.ToString();
                    varTFI = list2[0].ValorC.ToString();

                    DataLineResponsePostPaid objData = GetDataLine(contractID, audit);

                    strCodResult = objData.StrResponse;

                    intCodPlanTariff = Convert.ToInt(objData.DataLine.CodPlanTariff);

                    if (!string.IsNullOrEmpty(var))
                    {
                        var2 = var.Split(';');
                        for (int i = 0; i < var2.Length - 1; i++)
                        {
                            var3 = var2[i].Split(':');
                            if (var3.Length > 0)
                            {
                                if (!string.IsNullOrEmpty(var3[1]))
                                {
                                    var4 = var3[1].Split(',');
                                    for (int j = 0; j < var4.Length; j++)
                                    {
                                        if (intCodPlanTariff == Functions.CheckInt(var4[j].Trim()))
                                        {
                                            strNameTypePhone = var3[0].Trim();
                                            flagFound = 1;
                                            break;
                                        }
                                        else
                                        {
                                            strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
                                            flagFound = 0;
                                        }
                                    }
                                }
                            }
                            if (flagFound == 1)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
                    }

                    if (flagFound == 0)
                    {
                        if (!string.IsNullOrEmpty(varTFI))
                        {
                            varTFI2 = varTFI.Split(';');
                            for (int x = 0; x < varTFI2.Length - 1; x++)
                            {
                                varTFI3 = varTFI2[x].Split(',');
                                for (int y = 0; y < varTFI3.Length; y++)
                                {
                                    if (intCodPlanTariff == Functions.CheckInt(varTFI3[y].Trim()))
                                    {
                                        strNameTypePhone = ConstantsHFC.strTipoLinea_FIJO_POST;
                                        flagFound = 1;
                                        break;
                                    }
                                    else
                                    {
                                        strNameTypePhone = ConstantsHFC.strTipoLinea_POSTPAGO;
                                        flagFound = 0;
                                    }
                                }
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    Logging.Error(idSession, audit.transaction, ex.Message);
                    Logging.Info("Session: " + idSession, "Transaction: Start ", "Error: " + ex.Message);

                }


            }
            else
            {
                strNameTypePhone = string.Empty;
            }
            Logging.Info("Session: " + idSession, "Transaction: Start ", "salida: " + strNameTypePhone.ToUpper());

            return strNameTypePhone.ToUpper();
        }

        private ParameterTerminalTPIResponse GetParameterTerminalTPI(int ParameterID,
            CommonService.AuditRequest audit)
        {
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest()
            {
                ParameterID = ParameterID,
                audit = audit
            };
            ParameterTerminalTPIResponse objResponse =
                Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
                {
                    return _oServiceCommon.GetParameterTerminalTPI(objRequest);
                });
            return objResponse;
        }

        private List<ParameterTerminalTPIHelper> MappingParameterTerminalTPI(ParameterTerminalTPIResponse objResponse)
        {
            List<ParameterTerminalTPIHelper> list = new List<ParameterTerminalTPIHelper>();
            ParameterTerminalTPIHelper entity;
            foreach (var item in objResponse.ListParameterTeminalTPI)
            {
                entity = new ParameterTerminalTPIHelper()
                {
                    ParameterID = item.ParameterID,
                    Name = item.Name,
                    Description = item.Description,
                    Type = item.Type,
                    ValorC = item.ValorC,
                    ValorN = item.ValorN,
                    ValorL = item.ValorL
                };
                list.Add(entity);
            }
            return list;
        }

        private DataLineResponsePostPaid GetDataLine(string contractID, PostTransacService.AuditRequest audit)
        {
            DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
            {
                ContractID = contractID,
                audit = audit
            };
            DataLineResponsePostPaid objResponse =
                Logging.ExecuteMethod<DataLineResponsePostPaid>(() =>
                {
                    return _oServicePostpaid.GetDataLine(objRequest);
                });

            return objResponse;
        }

        public void RegisterAudit(string strDescription, double dblCodeEvent, object[,] objDetailAuditory,
            string strIdSession)
        {

            //var strIdTransaccion = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstIDTransaccionAplicacionPostpago"), Functions.CadenaAleatoria());
            string strTransaction = Convert.ToString(dblCodeEvent);
            string strIPCustomer = ""; // Convert.ToString(HttpContext.Current.Request.UserHostAddress);
            string strIpServi = Request.ServerVariables["LOCAL_ADDR"];
            string strNameCustomer = ""; //Convert.ToString(HttpContext.Current.Session("NombreUsuario"));
            strNameCustomer = string.IsNullOrEmpty(strNameCustomer) ? strIPCustomer : strNameCustomer;
            string strNameServidor = Request.ServerVariables["SERVER_NAME"];
            string strService = ConfigurationManager.AppSettings("gConstEvtServicio_ModCP");

            var strTelephone = string.Empty;
            var sbTexto = new StringBuilder();
            for (int i = 0; i < ((objDetailAuditory.Length / 4) - 1); i++)
            {
                if (objDetailAuditory.GetValue(i, 1) != null && objDetailAuditory.GetValue(i, 2) != null)
                {
                    sbTexto.Append(" " + objDetailAuditory.GetValue(i, 1) + " : ");
                    sbTexto.Append(objDetailAuditory.GetValue(i, 2));
                    if (objDetailAuditory.GetValue(i, 1) ==
                        SIACU.Transac.Service.Constants.PresentationLayer.gstrTelefono)
                    {
                        strTelephone = (string)objDetailAuditory.GetValue(i, 2);
                    }
                }
            }
            string strTexto = !string.IsNullOrEmpty(strDescription)
                ? string.Format("{0}{1}", strDescription, sbTexto.ToString())
                : string.Empty;
            strTexto = (strTexto.Length - 1 > 255) ? strTexto.Substring(0, 255) : strTexto;
            SaveAuditM(strTransaction, strService, strTexto, strTelephone, strNameCustomer, strIdSession,
                strNameServidor, strIpServi);
        }

        public string StatusLineValidate(string strIdSession, int vintTipoValidacion, string statusLinea)
        {
            var respuesta = string.Empty;
            if (statusLinea == null)
            {
                Logging.Info(strIdSession, "CommonServices-StatusLineValidate", "StatusLine:null-Respuesta:");//Temporal
                return respuesta;
            }
            switch (vintTipoValidacion)
            {
                case 1:
                    respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    break;
                case 2:
                    if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoSuspendido", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    break;
                case 3:
                    if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoReservado", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    break;
                case 4:

                    if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoInactivo", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    else if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoReservado", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    break;
                default:
                    if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoInactivo", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoInactivo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    else if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoSuspendido", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoSuspendido", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    else if (statusLinea.Trim().ToUpper() == GetValueConfig("strEstadoContratoReservado", strIdSession, "StatusLineValidate:").Trim().ToUpper())
                    {
                        respuesta = Functions.GetValueFromConfigFile("strMsgValidacionContratoReservado", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    }
                    break;
            }
            Logging.Info(strIdSession, "CommonServices-StatusLineValidate", "StatusLine:" + statusLinea + "-Respuesta:" + respuesta);//Temporal
            return respuesta;
        }

        public JsonResult ValidateCustomerJanus(string strIdSession)
        {
            var strResult = "T";
            if (strResult != ConstantsHFC.strLetraT)
            {
                strResult = "k";
            }
            return Json(strResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers(string strIdSession, string strCodeUser)
        {

            Logging.Info("1010101010", "1010101010", "strCodeUser: " + strCodeUser);

            UserResponse objRegionResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            UserRequest objRegionRequest = new UserRequest();
            objRegionRequest.audit = audit;

            objRegionRequest.CodeUser = strCodeUser;
            objRegionRequest.CodeRol = ConstantsHFC.strMenosUno;
            objRegionRequest.CodeCac = ConstantsHFC.strMenosUno;
            objRegionRequest.State = ConstantsHFC.strMenosUno;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<UserResponse>(() =>
                {
                    return _oServiceCommon.GetUser(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return Json(objRegionResponse.UserModel, JsonRequestBehavior.AllowGet);
        }

        //Cargo apadece (ObtenerPenalidadCambioPlan - ObtenerApadeceCancelacionRet)
        public JsonResult GetApadece(string strIdSession, string strTypeCustomer, string appTypeCustomer)
        {
            Boolean booSalida;
            var dtApadece = "";
            var intCodError = "";
            var strDesError = "";

            double dblPenalidadPCS;
            double dblPenalidadAPADECE;
            double dblMontoFidelizar;
            double dblNroFacuras;
            double dblCargoFijoActual;
            double dblCargoFijoNuevoPlan;
            double dblcuerdoIdSalida;
            double dblDiasPendientes;
            double dblCargoFijoDiario;
            double dblPrecioLista;
            double dblPrecioVenta;

            //hidValidaCargoFijoSAP.Value = ""

            //booSalida = False

            PenaltyChangePlanResponse objPenaltyChangePlanResponse = null;
            if (strTypeCustomer == appTypeCustomer)
            {
                CommonTransacService.AuditRequest audit =
                    Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                PenaltyChangePlanRequest objPenaltyChangePlanRequest = new PenaltyChangePlanRequest()
                {
                    audit = audit
                };

                try
                {
                    objPenaltyChangePlanResponse = Logging.ExecuteMethod<PenaltyChangePlanResponse>(
                        () =>
                        {
                            return _oServiceCommon.GetPenaltyChangePlan(objPenaltyChangePlanRequest);
                        });
                }
                catch (Exception ex)
                {
                    Logging.Error(strIdSession, audit.transaction, ex.Message);
                    throw new Exception(ex.Message);
                }
            }


            if (objPenaltyChangePlanResponse.Result)
            {
                dblPenalidadAPADECE = 0;
            }
            //if (dblPenalidadAPADECE = 0)
            //{

            //}
            //hidValidaCargoFijoSAP.Value = ConstantesWeb.NumeracionUNO

            return Json("", JsonRequestBehavior.AllowGet);
        }

        public GetEmployerDateResponseCommon GetEmployerDate(string strIdSession, string strCodeUser)
        {
            GetEmployerDateResponseCommon objRegionResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            GetEmployerDateRequestCommon objRegionRequest = new GetEmployerDateRequestCommon();
            objRegionRequest.audit = audit;
            objRegionRequest.strCurrentUser = strCodeUser;
            try
            {
                objRegionResponse = Logging.ExecuteMethod<GetEmployerDateResponseCommon>(() =>
                {
                    return _oServiceCommon.GetEmployerDate(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            return objRegionResponse;
        }

        public bool Authorize_TX_Search(string strIdSession, string transaction, string pUsuario, string pClave,
            string strKey)
        {
            var resultado = IsAuthenticated_LDAP(strIdSession, transaction, pUsuario, pClave);
            var response = false;
            try
            {
                if (resultado)
                {
                    var usuario = string.Empty;

                    var codApp = KEY.AppSettings("CodAplicacion_SIACPO");
                    var ipApp = KEY.AppSettings("strWebIpCod_SIACPO");
                    var nomApp = KEY.AppSettings("NombreAplicacion_SIACPO");

                    CommonTransacService.AuditRequest audit =
                        Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
                    var objRequest = new VerifyUserRequest
                    {
                        audit = audit,
                        AppName = nomApp,
                        TransactionId = audit.transaction,
                        AppCode = codApp,
                        SessionId = strIdSession,
                        AppId = ipApp,
                        Username = pUsuario.Trim()
                    };

                    var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetVerifyUser(objRequest));

                    if (objResponse.LstConsultSecurities.Count > 0)
                    {
                        var item = objResponse.LstConsultSecurities[0];
                        usuario = item.Usuaccod;

                        var strPermisos = GetAccessOfPage(strIdSession);
                        if (strPermisos.IndexOf(KEY.AppSettings(strKey), StringComparison.OrdinalIgnoreCase) != -1)
                        {
                            Session["UsuarioValidador"] = string.Empty;
                        }
                        else
                        {
                            response = true;
                            Session["UsuarioValidador"] = pUsuario.Trim();
                        }
                    }
                    else
                    {
                        usuario = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, transaction, ex.Message);
                throw;
            }

            return response;
        }

        public ParameterDataResponseCommon GetParameterData(string strIdSession, string name)
        {

            ParameterDataResponseCommon objResponse = new ParameterDataResponseCommon();
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ParameterDataRequestCommon objRequest = new ParameterDataRequestCommon();

            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Inicio Metodo : GetParameterData");
            objRequest.audit = audit;
            objRequest.Name = name;
            try
            {
                objResponse = Logging.ExecuteMethod<ParameterDataResponseCommon>(() =>
                {
                    return _oServiceCommon.GetParameterData(objRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(objRequest.audit.Session, objRequest.audit.transaction, ex.Message);

            }
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + audit.transaction, "Fín Metodo : GetParameterData");
            return objResponse;

        }

        public bool GetSendEmailCommon(GetEmployerDateResponseCommon item, string strIdSession, string phonfNroGener,
            string cuenta, string currentUser, string currentTerminal)
        {
            string strTemplateEmail = TemplateEmailCommon(item, phonfNroGener, cuenta, currentUser, currentTerminal);
            string strDestinatarios = item.strEmail;
            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

            SendEmailResponseCommon objGetSendEmailResponse =
                new SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SendEmailRequestCommon objGetSendEmailRequest =
                new SendEmailRequestCommon()
                {
                    audit = AuditRequest,
                    strSender = strRemitente,
                    strTo = strDestinatarios,
                    strMessage = strTemplateEmail,
                    strSubject = ConfigurationManager.AppSettings("gConstAsuntoDetLlamadaSaliente")
                };
            try
            {
                objGetSendEmailResponse =
                    Logging.ExecuteMethod<SendEmailResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetSendEmail(objGetSendEmailRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objGetSendEmailRequest.audit.transaction, ex.Message);
                throw new Exception(AuditRequest.transaction);
            }

            return true;
        }

        public string TemplateEmailCommon(GetEmployerDateResponseCommon item, string phonfNroGener, string cuenta,
            string currentUser, string currentTerminal)
        {
            string strmessage = string.Empty;
            strmessage = "<html>";
            strmessage += "<head>";
            strmessage += "<style type='text/css'>";
            strmessage += "<!--";
            strmessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strmessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strmessage += "-->";
            strmessage += "</style>";
            strmessage += "<body>";
            strmessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage += string.Format("<tr><td width='180' class='Estilo2' height='22'>Estimado(a) {0} {1} {2} </td>",
                item.strNomb, item.strApPat, item.strApMat);
            strmessage += "<tr><td height='10'></td>";
            strmessage +=
                "<tr><td class='Estilo1'>Se le informa que su Código y Password de Autorización ha sido utilizado para realizar una Transacción relacionada a Detalle de Llamadas desde las siguientes entradas</td></tr>";
            strmessage += "<tr><td height='10'></td>";

            strmessage += "<tr>";
            strmessage += "<td align='center'>";
            strmessage += "<Table width='90%' border='0' cellpadding='0' cellspacing='0'>";
            strmessage +=
                string.Format(
                    "<tr><td width='180' class='Estilo2' height='22'>Nro. Telefónico :</td><td class='Estilo1'> {0} </td></tr>",
                    phonfNroGener);
            strmessage +=
                string.Format(
                    "<tr><td width='180' class='Estilo2' height='22'>Cuenta :</td><td class='Estilo1'>{0}</td></tr>",
                    cuenta);
            strmessage +=
                string.Format(
                    "<tr><td width='180' class='Estilo2' height='22'>Usuario Logueado:</td><td class='Estilo1'>{0}</td></tr>",
                    currentUser);
            strmessage +=
                string.Format(
                    "<tr><td width='180' class='Estilo2' height='22'>Terminal o Computador :</td><td class='Estilo1'>{0}</td></tr>",
                    currentTerminal);
            strmessage +=
                string.Format(
                    "<tr><td width='180' class='Estilo2' height='22'>Fecha y Hora :</td><td class='Estilo1'>{0}</td></tr>",
                    DateTime.UtcNow.AddHours(-5));
            strmessage += "</Table>";
            strmessage += "</td></tr>";

            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td class='Estilo1'>Saludos Cordiales,</td></tr>";
            strmessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strmessage += "<tr><td height='10'></td>";
            strmessage += "<tr><td height='10'></td>";
            strmessage +=
                "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local)</td></tr>";
            strmessage += "</table>";
            strmessage += "</body>";
            strmessage += "</html>";

            return strmessage;
        }

        public JsonResult SendMail(string strIdSession, string codeUser, string phonfNroGener, string cuenta)
        {
            var receiverUser = GetEmployerDate(strIdSession, codeUser);
            var resultado = false;
            if (!string.IsNullOrEmpty(receiverUser.strEmail))
            {
                string currentUser = CurrentUser(strIdSession);
                string currentTerminal = CurrentTerminal();
                resultado = GetSendEmailCommon(receiverUser, strIdSession, phonfNroGener, cuenta, currentUser, currentTerminal);
            }

            return new JsonResult
            {
                Data = resultado,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult SendMail(string strIdSession, string codeUser, string phonfNroGener, string cuenta,
            string currentUser, string currentTerminal)
        {
            var receiverUser = GetEmployerDate(strIdSession, codeUser);
            var resultado = false;
            if (!string.IsNullOrEmpty(receiverUser.strEmail))
            {
                resultado = GetSendEmailCommon(receiverUser, strIdSession, phonfNroGener, cuenta, currentUser,
                    currentTerminal);
            }

            return new JsonResult
            {
                Data = resultado,
                ContentType = "application/json",
                ContentEncoding = Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetConsultIGV(string strIdSession)
        {
            ConsultIGVModel oConsultIGV = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            var objRequest = new ConsultIGVRequest
            {
                audit = audit,
                SessionId = strIdSession,
                TransactionId = audit.transaction,
                AppId = audit.ipAddress,
                AppName = audit.applicationName,
                Username = audit.userName
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetConsultIGV(objRequest));

                if (objResponse != null && objResponse.ListConsultIGV.Count > 0)
                {
                    DateTime currentDate = DateTime.Now;
                    foreach (var item in objResponse.ListConsultIGV)
                    {
                        if ((Convert.ToDate(item.impudFecIniVigencia) <= currentDate &&
                             Convert.ToDate(item.impudFecFinVigencia) >= currentDate) &&
                            item.impunTipDoc == Constant.strCero)
                        {
                            oConsultIGV = new ConsultIGVModel();
                            oConsultIGV.igv = item.igv;
                            oConsultIGV.igvD = item.igvD;
                            oConsultIGV.impudFecFinVigencia = item.impudFecFinVigencia;
                            oConsultIGV.impudFecIniVigencia = item.impudFecIniVigencia;
                            oConsultIGV.impudFecRegistro = item.impudFecRegistro;
                            oConsultIGV.impunTipDoc = item.impunTipDoc;
                            oConsultIGV.imputId = item.imputId;
                            oConsultIGV.impuvDes = item.impuvDes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }
            string strIGV = oConsultIGV == null ? string.Empty : Functions.CheckStr(oConsultIGV.igv);
            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objRequest.audit.transaction, "End a GetConsultIGV Controller => oConsultIGV.igv: " + strIGV); // Temporal
            return Json(new { data = oConsultIGV });
        }

        public JsonResult GetMotiveSot(string strIdSession)
        {

            MotiveSotResponseCommon objMotiveSotResponseCommon;

            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            MotiveSotRequestCommon objMotiveSotRequestCommon =
                new MotiveSotRequestCommon()
                {
                    audit = audit
                };

            try
            {
                objMotiveSotResponseCommon =
                    Logging.ExecuteMethod<MotiveSotResponseCommon>(() =>
                    {
                        return _oServiceCommon.GetMotiveSot(objMotiveSotRequestCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objMotiveSotRequestCommon.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            CommonServices objCommonServices = null;
            if (objMotiveSotResponseCommon != null && objMotiveSotResponseCommon.getMotiveSot != null)
            {
                objCommonServices = new CommonServices();
                List<CacDacTypeVM> listCacDacTypes =
                    new List<CacDacTypeVM>();

                foreach (CommonTransacService.ListItem item in objMotiveSotResponseCommon.getMotiveSot)
                {
                    listCacDacTypes.Add(new CacDacTypeVM()
                    {
                        Code = item.Code,
                        Description = item.Description
                    });
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            return Json(new { data = objCommonServices });
        }

        public List<Helpers.CommonServices.GenericItem> GetTypeWorkLteList(string strIdSession, int strTypeWork)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            JobTypesResponseHfc objJobTypeResponse;
            JobTypesRequestHfc objJobTypesRequest = new JobTypesRequestHfc()
            {
                audit = audit,
                p_tipo = strTypeWork,
            };

            try
            {
                objJobTypeResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                {
                    return _oServiceFixed.GetJobTypeLte(objJobTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            List<Helpers.CommonServices.GenericItem> lstTypeWork = null;
            if (objJobTypeResponse != null && objJobTypeResponse.JobTypes != null)
            {
                lstTypeWork = new List<Helpers.CommonServices.GenericItem>();
                for (int i = 0; i < objJobTypeResponse.JobTypes.Count; i++)
                {
                    lstTypeWork.Add(new Helpers.CommonServices.GenericItem()
                    {
                        Code = objJobTypeResponse.JobTypes[i].FLAG_FRANJA.Equals(Claro.SIACU.Transac.Service.Constants.strUno) ? objJobTypeResponse.JobTypes[i].tiptra + ".|" : objJobTypeResponse.JobTypes[i].tiptra,
                        Code2 = objJobTypeResponse.JobTypes[i].FLAG_FRANJA,
                        Description = objJobTypeResponse.JobTypes[i].descripcion
                    });
                }
            }
            return lstTypeWork;
        }
        public List<ItemGeneric> GetMotiveSotByTypeJob(string strIdSession, int idTipoTrabajo)
        {
            List<ItemGeneric> lstMotiveSotByTypeJob = new List<ItemGeneric>();
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.MotiveSOTByTypeJobResponse objResponse = null;
            FixedTransacService.MotiveSOTByTypeJobRequest objRequest = new MotiveSOTByTypeJobRequest()
            {
                audit = audit,
                tipTra = idTipoTrabajo
            };
            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Inicio Método : GetMotiveSOTByTypeJob");

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.MotiveSOTByTypeJobResponse>(() =>
                {
                    return _oServiceFixed.GetMotiveSOTByTypeJob(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            if (objResponse != null && objResponse.List != null)
            {
                for (int i = 0; i < objResponse.List.Count; i++)
                {
                    lstMotiveSotByTypeJob.Add(new ItemGeneric()
                    {
                        Code = objResponse.List[i].Codigo,
                        Description = objResponse.List[i].Descripcion
                    });
                }
            }
            return lstMotiveSotByTypeJob;

        }
        //PROY140315 - Inicio
        public List<Helpers.CommonServices.GenericItem> GetTypeWorkListDTH(string strIdSession, int strTypeWork)
        {
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            JobTypesResponseHfc objJobTypeResponse;
            JobTypesRequestHfc objJobTypesRequest = new JobTypesRequestHfc()
            {
                audit = audit,
                p_tipo = strTypeWork,
            };

            try
            {
                objJobTypeResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.JobTypesResponseHfc>(() =>
                {
                    return _oServiceFixed.GetJobTypeDTH(objJobTypesRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objJobTypesRequest.audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            List<Helpers.CommonServices.GenericItem> lstTypeWork = null;
            if (objJobTypeResponse != null && objJobTypeResponse.JobTypes != null)
            {
                lstTypeWork = new List<Helpers.CommonServices.GenericItem>();
                for (int i = 0; i < objJobTypeResponse.JobTypes.Count; i++)
                {
                    lstTypeWork.Add(new Helpers.CommonServices.GenericItem()
                    {
                        Code = objJobTypeResponse.JobTypes[i].FLAG_FRANJA.Equals(Claro.SIACU.Transac.Service.Constants.strUno) ? objJobTypeResponse.JobTypes[i].tiptra + ".|" : objJobTypeResponse.JobTypes[i].tiptra,
                        Code2 = objJobTypeResponse.JobTypes[i].FLAG_FRANJA,
                        Description = objJobTypeResponse.JobTypes[i].descripcion
                    });
                }
            }
            return lstTypeWork;
        }
        //PROY140315 - Fin
        #region Servicios adicionales LTE/HFC

        public void SendEmailAdditionalService(string strAddressee, string strInteractionId, string strNameCustomer,
            string strLegalRepresent, string strCustomerId, string strTypeDoc, string strNroDoc,
           string strTypeTransaction, string strIdSession, string strAdjunto, byte[] attachFile, string strAccion = "")
        {
            var objDatInteraction = GetInfoInteractionTemplate(strIdSession, strInteractionId);


            if (strAccion == "Activación")
            {
                strAccion = "Activación de Servicios";
            }
            else
            {
                strAccion = "Desactivación de Servicios";
            }

            var strSubject = "Variación de Servicio";
            var strMessage = "<html>";
            strMessage += "<head>";
            strMessage += "<style type='text/css'>";
            strMessage += "<!--";
            strMessage += ".Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
            strMessage += ".Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
            strMessage += "-->";
            strMessage += "</style>";
            strMessage += "<body>";
            strMessage += "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strMessage += "<tr><td>";
            strMessage += MailHeader(strAccion, objDatInteraction.X_INTER_15, DateTime.Now.ToShortDateString(), strNameCustomer, strInteractionId, strLegalRepresent, strCustomerId, strTypeDoc, strNroDoc);
            strMessage += "</td></tr>";

            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Cordialmente</td></tr>";
            strMessage += "<tr><td class='Estilo1'>Atención al Cliente</td></tr>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td height='10'></td>";
            strMessage += "<tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
            strMessage += "</table>";
            strMessage += "</body>";
            strMessage += "</html>";

            string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");

            SendEmailResponseCommon objGetSendEmailResponse = new SendEmailResponseCommon();
            CommonTransacService.AuditRequest AuditRequest = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SendEmailRequestCommon objGetSendEmailRequest =
            new SendEmailRequestCommon()
            {
                audit = AuditRequest,
                strSender = strRemitente,
                strTo = strAddressee,
                strMessage = strMessage,
                strAttached = strAdjunto,
                strSubject = strSubject,
                AttachedByte = attachFile
            };
            try
            {
                objGetSendEmailResponse = Logging.ExecuteMethod<SendEmailResponseCommon>
                (
                    () => { return _oServiceCommon.GetSendEmailFixed(objGetSendEmailRequest); }
                );

                Logging.Info(AuditRequest.Session, AuditRequest.transaction, "INFO EMAIL CONTROLLER ACTIVACION DESACTIVACION : " + objGetSendEmailResponse.Exit);
            }
            catch (Exception ex)
            {
                Logging.Error(objGetSendEmailRequest.audit.Session, objGetSendEmailRequest.audit.transaction, "Error EMAIL : " + ex.Message);
            }
        }

        public string MailHeader(string strTitle, string strCacDac, string strDate, string strHeadline,
            string strCaseInteraction, string strRepresentative, string strNroClaro, string strTypeDocument,
            string strNroDocument)
        {
            string strRetorno;
            strRetorno = "<table width='100%' border='0' cellpadding='0' cellspacing='0'>";
            strRetorno += "\t\t\t\t<tr><td width='100%' class='Estilo1'>Estimado Cliente, </td></tr>";
            strRetorno += "\t\t\t\t<tr><td height='10'></td>";
            if (strTitle == "Servicio de Variación de Débito / Crédito Manual Corporativo")
            {
                strRetorno +=
                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que se realizo un ajuste al saldo de su linea corporativa</td></tr>";
            }
            else if (strTitle == "Activación de Servicios")
            {
                strRetorno +=
                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " + strTitle +
               " Adicionales fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
            }
            else if (strTitle == "Desactivación de Servicios")
            {
                strRetorno +=
                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " + strTitle +
               " Adicionales fue registrada y estará siendo atendida en el plazo establecido.</td></tr>";
            }
            else
            {
                strRetorno +=
                    "\t\t\t\t<tr><td class='Estilo1'>Por la presente queremos informarle que su solicitud de " +
                    strTitle + " fue atendida.</td></tr>";
            }

            strRetorno += "\t\t\t</table>";

            return strRetorno;
        }

        #endregion

        public bool GetValidateCustomerId(CustomersDataModel model, string strCustomerId, string strIdSession)
        {
            ValidateCustomerIdResponse objRegionResponse = null;
            FixedTransacService.AuditRequest audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            ValidateCustomerIdRequest objRegionRequest = new ValidateCustomerIdRequest();
            objRegionRequest.audit = audit;
            objRegionRequest.Phone = strCustomerId;
            bool valueCustomer = true;

            try
            {
                objRegionResponse = Logging.ExecuteMethod<ValidateCustomerIdResponse>(() =>
                {
                    return _oServiceFixed.GetValidateCustomerId(objRegionRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            if (objRegionResponse.FlgResult != ConstantsHFC.strCero)
            {
                model = LoadDatosCustomerId(model, strIdSession);
                CustomerResponse objCustomerResponse = null;
                FixedTransacService.AuditRequest auditCustomer = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
                Customer objCustomerRequest = new Customer();
                objCustomerRequest.audit = auditCustomer;
                objCustomerRequest.Telephone = model.StrPhone;
                objCustomerRequest.User = model.StrUser;
                objCustomerRequest.Name = model.StrName;
                objCustomerRequest.LastName = model.StrLastName;
                objCustomerRequest.BusinessName = model.StrBusinessName;
                objCustomerRequest.DocumentType = model.StrDocumentType;
                objCustomerRequest.DocumentNumber = model.StrDocumentNumber;
                objCustomerRequest.Address = model.StrAccount;
                objCustomerRequest.District = model.StrDistrict;
                objCustomerRequest.Departament = model.StrDepartament;
                objCustomerRequest.Province = model.StrProvince;
                objCustomerRequest.Modality = model.StrModality;

                try
                {
                    objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
                    {
                        return _oServiceFixed.GetRegisterCustomerId(objCustomerRequest);
                    });
                    if (objCustomerResponse.Resultado)
                    {
                        if (objCustomerResponse.vFlagConsulta.Trim() == ConfigurationManager.AppSettings("gConstKeyStrResultInsertCusID"))
                        {
                            valueCustomer = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    Logging.Error(strIdSession, strIdSession, e.Message);
                }
            }
            return valueCustomer;
        }

        public CustomersDataModel LoadDatosCustomerId(CustomersDataModel model, string strIdSession)
        {
            model.StrPhone = string.Format("{0}{1}", ConfigurationManager.AppSettings("gConstKeyCustomerInteract"), model.StrCustomerId);
            model.StrModality = ConfigurationManager.AppSettings("gConstKeyStrModalidad");
            return model;
        }

        public string GetCustomer(string strCustomerId, string strIdSession)
        {
            string strObjId;
            var strFlgRegistrado = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO;
            CustomerResponse objCustomerResponse = null;
            FixedTransacService.AuditRequest audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            GetCustomerRequest objCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strCustomerId,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            try
            {
                objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
                {
                    return _oServiceFixed.GetCustomer(objCustomerRequest);
                });
                strObjId = objCustomerResponse.Customer.ContactCode;

            }
            catch (Exception e)
            {
                throw;
            }
            return strObjId;
        }

        public CustomerResponse GetCustomerData(GetCustomerRequest oCustomer, string strIdSession)
        {
            var strFlgRegistrado = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO;
            CustomerResponse objCustomerResponse = new CustomerResponse();
            FixedTransacService.AuditRequest audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            GetCustomerRequest objCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = oCustomer.vPhone,
                vAccount = oCustomer.vAccount,
                vContactobjid1 = oCustomer.vContactobjid1,
                vFlagReg = strFlgRegistrado
            };
            try
            {
                objCustomerResponse = Logging.ExecuteMethod<CustomerResponse>(() =>
                {
                    return _oServiceFixed.GetCustomer(objCustomerRequest);
                });


            }
            catch (Exception e)
            {
                throw;
            }
            return objCustomerResponse;
        }

        public string ValidarPermiso(string strIdSession, string strContratoID, string hidListNumImportar)
        {
            string vContratoID2 = string.Empty;
            string strNombreTipoTelef = string.Empty;
            string TerminalTPI = string.Empty;
            string[] arrTerminalTPI;
            string TerminalTPI_Post = string.Empty;
            string intCodPlanTarif = string.Empty;
            string[] arrTerminalTPI_2;
            string[] arrTerminalTPI_3;
            string[] TerminalTPI_Post_2;
            string[] TerminalTPI_Post_3;

            string cadena4 = string.Empty;
            int flagEncontro = 0;
            //int parametroID =0;

            Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "strContratoID : " + strContratoID);

            int intCodParam = Convert.ToInt(ConfigurationManager.AppSettings("gObtenerParametroTerminalTPI"));
            int intCodParam2 = Convert.ToInt(ConfigurationManager.AppSettings("gObtenerParametroSoloTFIPostpago"));

            ParameterTerminalTPIResponse objTerTPI = new ParameterTerminalTPIResponse();
            ParameterTerminalTPIResponse objTFI_Post = new ParameterTerminalTPIResponse();

            PostTransacService.AuditRequest audit =
                Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            CommonService.AuditRequest auditC =
                Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            if (strContratoID != string.Empty)
            {
                if (strContratoID.Length > 0)
                {
                    Logging.Info("IdSession: " + strIdSession, " Método: ValidarPermiso", "Inicio GetParameterTerminalTPI - intCodParam :" + intCodParam);
                    objTerTPI = GetParameterTerminalTPI(intCodParam, auditC);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio GetSiacParameterTFIPostpago : strIdSession " + strIdSession + "intCodParam2: " + intCodParam2);
                    objTFI_Post = GetSiacParameterTFIPostpago(strIdSession, intCodParam2);

                    foreach (ParameterTerminalTPI ItemTPI_Response in objTerTPI.ListParameterTeminalTPI)
                    {
                        TerminalTPI = ItemTPI_Response.ValorC.ToString();
                        break;
                    }

                    Logging.Info("IdSession: " + strIdSession, " Método: ValidarPermiso", " GetParameterTerminalTPI - TerminalTPI :" + TerminalTPI);

                    foreach (ParameterTerminalTPI ItemTFI_Post in objTFI_Post.ListParameterTeminalTPI)
                    {
                        TerminalTPI_Post = ItemTFI_Post.ValorC.ToString();
                        break;
                    }
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio GetSiacParameterTFIPostpago : TerminalTPI_Post " + TerminalTPI_Post);

                    DataLineResponsePostPaid oDataLinea = new DataLineResponsePostPaid();

                    //PostTransacService.AuditRequest audit =
                    //App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);

                    DataLineRequestPostPaid objRequest = new DataLineRequestPostPaid()
                    {
                        ContractID = strContratoID,
                        audit = audit
                    };

                    oDataLinea = _oServicePostpaid.GetDataLine(objRequest);
                    intCodPlanTarif = oDataLinea.DataLine.CodPlanTariff;
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "Captura strNombreTipoTelef"

                    if (TerminalTPI != string.Empty)
                    {
                        arrTerminalTPI = TerminalTPI.Split(';');

                        for (int i = 0; i < arrTerminalTPI.Length - 2; i++)
                        {
                            arrTerminalTPI_2 = arrTerminalTPI[i].ToString().Split(':');
                            if (arrTerminalTPI_2 != null)
                            {
                                if (arrTerminalTPI_2[1].ToString() != null)
                                {
                                    arrTerminalTPI_3 = arrTerminalTPI_2[1].ToString().Split(',');

                                    for (int a = 0; a < arrTerminalTPI_3.Length; a++)
                                    {

                                        if (arrTerminalTPI_3[a].ToString().Trim() == intCodPlanTarif)
                                        {
                                            strNombreTipoTelef = arrTerminalTPI_2[0].ToString().Trim();
                                            flagEncontro = 1;

                                        }
                                        else
                                        {
                                            strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");
                                            flagEncontro = 0;
                                        }

                                    }

                                }
                            }
                        }

                    }
                    else
                    {
                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");

                    }

                    #endregion
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  : strNombreTipoTelef " + strNombreTipoTelef);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef 2 : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "Captura strNombreTipoTelef"
                    if (flagEncontro == 0)
                    {
                        if (TerminalTPI_Post.ToString() != null)
                        {
                            TerminalTPI_Post_2 = TerminalTPI_Post.ToString().Split(';');

                            for (int n = 0; n < TerminalTPI_Post_2.Length; n++)
                            {
                                TerminalTPI_Post_3 = TerminalTPI_Post_2[n].ToString().Split(',');

                                for (int i = 0; i < TerminalTPI_Post_3.Length; i++)
                                {
                                    if (intCodPlanTarif == TerminalTPI_Post_3[i].ToString())
                                    {
                                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadFijoPost");
                                        flagEncontro = 1;
                                        break;
                                    }
                                    else
                                    {
                                        strNombreTipoTelef = ConfigurationManager.AppSettings("strModalidadPostpago");
                                        flagEncontro = 0;
                                    }

                                }


                            }

                        }

                    }
                    # endregion
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  2: strNombreTipoTelef " + strNombreTipoTelef);

                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Inicio Captura strNombreTipoTelef 3 : strNombreTipoTelef " + strNombreTipoTelef);
                    #region "strNombreTipoTelef"
                    string strgConsHabilitaCambioTrasladoNumeroSoloTFI = ConfigurationManager.AppSettings("gConsHabilitaCambTrasladoNumeroCambPlanSoloTFI");
                    string[] arrConsHabilitaCambioTrasladoNumeroSoloTFI = strgConsHabilitaCambioTrasladoNumeroSoloTFI.Split(';');
                    string[] arrCHCTNSoloTFI;
                    string[] arrCHCTNSoloTFI2;

                    if (TerminalTPI_Post != string.Empty)
                    {
                        TerminalTPI_Post_2 = TerminalTPI_Post.ToString().Split(';');

                        for (int x = 0; x < TerminalTPI_Post_2.Length; x++)
                        {
                            TerminalTPI_Post_3 = TerminalTPI_Post_2[x].ToString().Split(',');

                            for (int v = 0; v < TerminalTPI_Post_3.Length; v++)
                            {

                                if (intCodPlanTarif == TerminalTPI_Post_3[v].ToString())
                                {
                                    arrCHCTNSoloTFI = arrConsHabilitaCambioTrasladoNumeroSoloTFI.ToString().Split(';');
                                    arrCHCTNSoloTFI2 = arrCHCTNSoloTFI['0'].ToString().Split(',');

                                    for (int j = 0; j < arrCHCTNSoloTFI2.Length - 1; j++)
                                    {
                                        if (intCodPlanTarif == arrCHCTNSoloTFI2[j].ToString())
                                        {
                                            strNombreTipoTelef = "HCTNumeroCPlanSoloTFI";
                                            break;
                                        }
                                    }

                                }

                            }

                        }

                    }
                    #endregion;
                    Logging.Info("IdSession: " + strIdSession, "Método: ValidarPermiso", "Fín Captura strNombreTipoTelef  3: strNombreTipoTelef " + strNombreTipoTelef);

                }
                else
                {
                    strNombreTipoTelef = String.Empty;
                }
            }
            else
            {
                strNombreTipoTelef = String.Empty;
            }

            return strNombreTipoTelef;

        }

        public ParameterTerminalTPIResponse GetSiacParameterTFIPostpago(string strIdSession, int parametroID)
        {
            ParameterTerminalTPIResponse objResponse = new ParameterTerminalTPIResponse();

            CommonService.AuditRequest audit =
                 Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);

            //SiacParametroRequestPostPaid objRequest = new SiacParametroRequestPostPaid();
            ParameterTerminalTPIRequest objRequest = new ParameterTerminalTPIRequest();
            objRequest.audit = audit;
            objRequest.ParameterID = parametroID;


            try
            {
                objResponse =
                    Logging.ExecuteMethod<ParameterTerminalTPIResponse>(() =>
                    {
                        return _oServiceCommon.GetParameterTerminalTPI(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(objRequest.audit.transaction);
            }


            return objResponse;
        }

        public SendEmailSBModel GetSendEmailSB(string strIdSession, string strRemitente, string strDestinatario,
                                 string strAsunto, string strMensaje, string strHTMLFlag, byte[] Archivo)
        {
            SendEmailSBModel objSendEmailSBModel = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            var objRequest = new SendEmailSBRequest
            {
                audit = audit,
                SessionId = strIdSession,
                TransactionId = audit.transaction,
                strAppID = audit.ipAddress,
                strAppCode = audit.applicationName,
                strAppUser = audit.userName,
                strRemitente = strRemitente,
                strDestinatario = strDestinatario,
                strAsunto = strAsunto,
                strMensaje = strMensaje,
                strHTMLFlag = strHTMLFlag,
                Archivo = Archivo
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetSendEmailSB(objRequest));

                if (objResponse != null)
                {
                    objSendEmailSBModel = new SendEmailSBModel()
                    {
                        idTransaccion = objResponse.idTransaccion,
                        codigoRespuesta = objResponse.codigoRespuesta,
                        mensajeRespuesta = objResponse.mensajeRespuesta
                    };
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return objSendEmailSBModel;
        }

        public Dictionary<string, object> GetNoteInteraction(string strIdSession, string strIdInteraction)
        {
            var dictionaryResponse = new Dictionary<string, object>();
            NoteInteractionResponse objNoteInteractionResponse;
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            NoteInteractionRequest objNoteInteractionRequest = new NoteInteractionRequest()
            {
                audit = audit,
                vInteraccionId = strIdInteraction
            };

            try
            {
                objNoteInteractionResponse =
                Logging.ExecuteMethod<NoteInteractionResponse>(() =>
                {
                    return _oServiceCommon.GetNoteInteraction(objNoteInteractionRequest);
                });

                if (objNoteInteractionResponse != null)
                {
                    dictionaryResponse.Add("FlagInsercion", objNoteInteractionResponse.rFlagInsercion);
                    dictionaryResponse.Add("MsgText", objNoteInteractionResponse.rMsgText);
                    dictionaryResponse.Add("Nota", objNoteInteractionResponse.strNota);
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objNoteInteractionRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return dictionaryResponse;
        }


        public FixedTransacService.Interaction GetCreateCase(FixedTransacService.Interaction oRequest)
        {

            FixedTransacService.Interaction oRequestNew = new FixedTransacService.Interaction();
            FixedTransacService.Interaction oResponse = new FixedTransacService.Interaction();


            try
            {

                oResponse = Logging.ExecuteMethod<FixedTransacService.Interaction>(() =>
                {
                    return _oServiceFixed.GetCreateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        public CaseInsertResponse GetCaseInsert(CaseInsertRequest oRequest)
        {
            CaseInsertResponse oResponse = new CaseInsertResponse();
            try
            {
                oResponse = Logging.ExecuteMethod<CaseInsertResponse>(() =>
                {
                    return _oServiceFixed.GetCaseInsert(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }



        public FixedTransacService.Interaction GetInsertCase(FixedTransacService.Interaction oRequest)
        {
            FixedTransacService.Interaction oResponse = new FixedTransacService.Interaction();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedTransacService.Interaction>(() =>
                {
                    return _oServiceFixed.GetInsertCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        public FixedTransacService.CaseTemplate GetInsertTemplateCase(FixedTransacService.CaseTemplate oRequest)
        {
            FixedTransacService.CaseTemplate oResponse = new FixedTransacService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedTransacService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.GetInsertTemplateCase(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }


        public FixedTransacService.CaseTemplate GetInsertTemplateCaseContingent(FixedTransacService.CaseTemplate oRequest)
        {
            FixedTransacService.CaseTemplate oResponse = new FixedTransacService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedTransacService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.GetInsertTemplateCaseContingent(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        public CaseTemplateModel GetDynamicCaseTemplateData(string strIdSession, string strIdInteraccion)
        {
            CaseTemplateModel oCaseTemplate = null;
            var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objResponse = new DynamicCaseTemplateDataResponse();
            var objRequest = new DynamicCaseTemplateDataRequest()
            {
                audit = audit,
                vCasoID = strIdInteraccion
            };

            try
            {
                objResponse = Logging.ExecuteMethod<DynamicCaseTemplateDataResponse>(() =>
                {
                    return _oServiceCommon.GetDynamicCaseTemplateData(objRequest);
                });

                if (objResponse != null && objResponse.oCaseTemplate != null)
                {
                    oCaseTemplate = new CaseTemplateModel();
                    oCaseTemplate.X_SUSPENSION_DATE = objResponse.oCaseTemplate.X_SUSPENSION_DATE;
                    oCaseTemplate.X_CAS_1 = objResponse.oCaseTemplate.X_CAS_1;
                    oCaseTemplate.X_OPERATOR_PROBLEM = objResponse.oCaseTemplate.X_OPERATOR_PROBLEM;
                    oCaseTemplate.X_CAS_16 = objResponse.oCaseTemplate.X_CAS_16;
                    oCaseTemplate.X_CAS_15 = objResponse.oCaseTemplate.X_CAS_15;
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return oCaseTemplate;
        }

        public FixedTransacService.CaseTemplate ActualizaPlantillaCaso(FixedTransacService.CaseTemplate oRequest)
        {
            FixedTransacService.CaseTemplate oResponse = new FixedTransacService.CaseTemplate();

            try
            {
                oResponse = Logging.ExecuteMethod<FixedTransacService.CaseTemplate>(() =>
                {
                    return _oServiceFixed.ActualizaPlantillaCaso(oRequest);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oResponse;
        }

        #region JCAA
        public string GetOBJID(string strIdSession, string strCustomerID)
        {
            string strObjId = String.Empty;
            string strFlgRegistrado = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionUNO;
            string strFlagConsulta = string.Empty;
            string strMsgResultado = string.Empty;
            GetClientRequestCommon oClienteRequest = new GetClientRequestCommon()
            {
                strflagreg = strFlgRegistrado,
                strContactobjid = strObjId,
                straccount = string.Empty,
                strphone = strCustomerID,
                audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession)
            };
            GetClientResponseComon oClienteResponse = Logging.ExecuteMethod<GetClientResponseComon>(() =>
            {
                return _oServiceCommon.GetObClient(oClienteRequest);
            });
            return oClienteResponse.listClient.OBJID_CONTACTO;
        }

        public static List<string> GetXmlToString(string filename)
        {

            try
            {
                XDocument xdoc = XDocument.Load(filename);
                List<string> listadios = (from o in xdoc.Root.Descendants()
                                          select o.Name.ToString()).ToList();

                return listadios;

            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }
        public JsonResult ETAValidation(string strIdSession, string vstrTipTra, string vstrIdPLano)
        {
            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_Parameters strIdSession: " + strIdSession + " vstrTipTra: " + vstrTipTra + " vstrIdPLano: " + vstrIdPLano);
            string v_TipoOrden;
            char[] constantToSplit = new char[] { '.', '|' };
            Helpers.CommonServices.GenericItem objGenericItem = new Helpers.CommonServices.GenericItem();
            OrderTypesRequestHfc objOrderTypesRequest = new OrderTypesRequestHfc()
    {
        audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession),
        vIdtiptra = (vstrTipTra.IndexOf(".|") == Claro.Constants.NumberOneNegative ? vstrTipTra : vstrTipTra.Split(constantToSplit)[0])
    };
            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objOrderTypesRequest : " + Newtonsoft.Json.JsonConvert.SerializeObject(objOrderTypesRequest));
            try
            {
                OrderTypesResponseHfc objOrderTypesResponse = Logging.ExecuteMethod<OrderTypesResponseHfc>(() => { return _oServiceFixed.GetOrderType(objOrderTypesRequest); });

                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objOrderTypesResponse : " + Newtonsoft.Json.JsonConvert.SerializeObject(objOrderTypesResponse));


                if (objOrderTypesResponse.ordertypes.Count == 0)
                    v_TipoOrden = SIACU.Transac.Service.Constants.strMenosUno;
                else
                    v_TipoOrden = objOrderTypesResponse.ordertypes[0].VALOR;

                ETAFlowRequestHfc oEtaRequest = new ETAFlowRequestHfc()
                {
                    audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession),
                    an_tipsrv = ConfigurationManager.AppSettings("gConstHFCTipoServicio"),
                    an_tiptra = (vstrTipTra.IndexOf(".|") == Claro.Constants.NumberOneNegative ? Int32.Parse(vstrTipTra) : Int32.Parse(vstrTipTra.Split(constantToSplit)[0])),
                    as_origen = ConfigurationManager.AppSettings("gConstHFCOrigen"),
                    av_idplano = vstrIdPLano,
                    av_ubigeo = String.Empty
                };
                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_oEtaRequest : " + Newtonsoft.Json.JsonConvert.SerializeObject(oEtaRequest));

                ETAFlowResponseHfc oEtaResponse = new ETAFlowResponseHfc();
                oEtaResponse = Logging.ExecuteMethod<ETAFlowResponseHfc>(() =>
                {
                    return new FixedTransacServiceClient().ETAFlowValidate(oEtaRequest);
                });
                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_oEtaResponse : " + Newtonsoft.Json.JsonConvert.SerializeObject(oEtaResponse));

                //objGenericItem.Condition = objETAFlowResponse.rResult;
                objGenericItem.Code = Functions.CheckStr(oEtaResponse.ETAFlow.an_indica);
                objGenericItem.Code2 = oEtaResponse.ETAFlow.as_codzona + "|" + vstrIdPLano + "|" + v_TipoOrden;
                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_apuntodeentraralswitch");

                switch (oEtaResponse.ETAFlow.an_indica)
                {
                    case -1:
                        objGenericItem.Description = Functions.GetValueFromConfigFile("strMsgNoExistePlano", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -2:
                        objGenericItem.Description = Functions.GetValueFromConfigFile("strMsgNoExisteUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -3:
                        objGenericItem.Description = Functions.GetValueFromConfigFile("strMsgNoExistePlanoUbigeo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -4:
                        objGenericItem.Description = Functions.GetValueFromConfigFile("strMsgNoExisteTipoTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case -5:
                        objGenericItem.Description = Functions.GetValueFromConfigFile("strMsgNoExisteTipoServicio", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg")); break;
                    case 1:
                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariable_OK; break;
                    case 0:
                        objGenericItem.Description = CSTS.Constants.DAReclamDatosVariableNO_OK; break;
                }

            }
            catch (Exception ex)
            {
                Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_Exception: " + Newtonsoft.Json.JsonConvert.SerializeObject(ex));

                Logging.Error(strIdSession, objOrderTypesRequest.audit.transaction, ex.Message);
                throw new Exception(objOrderTypesRequest.audit.transaction);
            }
            Logging.Info("ETAValidation", "CommonServicesControllers", "HFCPlanMigration_ETAValidation_objGenericItem: " + Newtonsoft.Json.JsonConvert.SerializeObject(objGenericItem));

            return Json(new { data = objGenericItem });
        }
        public JsonResult GetTimeZones(string strIdSession, TimeZoneVM objTimeZoneVM)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            ArrayList lstGenericItem = new ArrayList();
            FixedTransacService.GenericItem objGenericItem = new FixedTransacService.GenericItem();

            objGenericItem.Descripcion = Functions.GetValueFromConfigFile("strSeleccionar", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
            objGenericItem.Codigo = SIACU.Transac.Service.Constants.PresentationLayer.NumeracionMENOSUNO;

            try
            {
                if (objTimeZoneVM.vValidateETA == SIACU.Transac.Service.Constants.strUno)
                {
                    lstGenericItem = GetTimeZones(objTimeZoneVM, strIdSession);
                }
                else
                {
                    if (objTimeZoneVM.vJobTypes != null)
                    {
                        if (objTimeZoneVM.vJobTypes.Contains(".|"))
                        {
                            if (objTimeZoneVM.vJobTypes != KEY.AppSettings("TipoTrabajo_HFC_RETENCION"))
                            {

                                TimeZonesResponseHfc objFranjasHorariasResponseHfc = new TimeZonesResponseHfc();
                                TimeZonesRequestHfc objFranjasHorariasRequestHfc = new TimeZonesRequestHfc();

                                FixedTransacService.AuditRequest auditFixed = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

                                try
                                {
                                    objFranjasHorariasRequestHfc.audit = auditFixed;
                                    objFranjasHorariasRequestHfc.strAnTiptra = objTimeZoneVM.vJobTypes.Substring(0, objTimeZoneVM.vJobTypes.Length - 2);
                                    objFranjasHorariasRequestHfc.strAnUbigeo = objTimeZoneVM.vUbigeo;
                                    objFranjasHorariasRequestHfc.strAdFecagenda = objTimeZoneVM.vCommitmentDate;

                                    objFranjasHorariasResponseHfc = Logging.ExecuteMethod<TimeZonesResponseHfc>(() =>
                                    {
                                        return _oServiceFixed.GetTimeZones(objFranjasHorariasRequestHfc);
                                    });



                                    foreach (FixedTransacService.TimeZone _item in objFranjasHorariasResponseHfc.TimeZones)
                                    {
                                        objGenericItem = new FixedTransacService.GenericItem();

                                        objGenericItem.Agrupador = _item.TIPTRA;
                                        objGenericItem.Codigo = _item.CODCON;
                                        objGenericItem.Codigo2 = _item.CODCUADRILLA;
                                        objGenericItem.Descripcion = _item.HORA;
                                        objGenericItem.Estado = _item.COLOR;

                                        lstGenericItem.Add(objGenericItem);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logging.Error(objFranjasHorariasRequestHfc.audit.Session, objFranjasHorariasRequestHfc.audit.transaction, ex.Message);
                                    throw new Exception(auditFixed.transaction);
                                }
                            }
                            else
                            {
                                lstGenericItem = Common.GetXMLList("FranjasHorariasXML");
                            }
                        }
                        else
                        {
                            lstGenericItem = Common.GetXMLList("FranjasHorariasXML");

                        }
                    }
                    else
                    {
                        lstGenericItem = Common.GetXMLList("FranjasHorariasXML");
                    }

                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);

            }

            return Json(new { data = lstGenericItem });
        }

        private ArrayList GetTimeZones(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        {
            ArrayList Items = new ArrayList();
            string idTran, ipApp, nomAp, usrAp;
            try
            {
                idTran = Common.GetTransactionID();
                ipApp = Common.GetApplicationIp();
                nomAp = KEY.AppSettings("NombreAplicacion");
                usrAp = Common.CurrentUser;

                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

                int fID = Convert.ToInt(Functions.GetValueFromConfigFile("strDiasConsultaCapacidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    ));
                DateTime[] dDate = new DateTime[fID];

                dDate[0] = dInitialDate;

                for (int i = 0; i < fID; i++)
                {
                    dInitialDate = dInitialDate.AddDays(1);
                    dDate[i] = dInitialDate;
                }

                Boolean vExistSesion = false;
                string strUbicacion = Functions.GetValueFromConfigFile("strCodigoUbicacion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    );
                string[] vUbicaciones = { strUbicacion };
                Boolean v1, v2, v3, v4, v5, v6, v7, v8;

                v1 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strCalcDuracion", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v2 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strCalcDuracionEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v3 = System.Convert.ToBoolean(Int32.Parse(
                    Functions.GetValueFromConfigFile("strCalcTiempoViaje", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v4 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strCalcTiempoViajeEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v5 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strCalcHabTrabajo", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v6 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strCalcHabTrabajoEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));
                v7 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strObtenerZonaUbi", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig")
                    )));
                v8 = System.Convert.ToBoolean(Int32.Parse(Functions.GetValueFromConfigFile("strObtenerZonaUbiEspec", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"))));


                String vHabTra = String.Empty;
                vHabTra = Functions.GetValueFromConfigFile("strCodigoHabilidad", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));

                string[] vEspacioTiempo = { string.Empty };
                string[] HabilidadTrabajo = { vHabTra };

                //    Dim vespacioTiempo As String() = {String.Empty}
                //Dim vhabilidtrab As String() = {vstrHabTra}


            }
            catch (Exception)
            {

                throw;
            }



            return Items;
        }

        [HttpPost]
        public JsonResult GetCustomerPhoneCommon(string strIdSession, string intIdContract, string strTypeProduct)
        {
            var objAuditRequest = Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var objRequest = new ConsultationServiceByContractRequest()
            {
                audit = objAuditRequest,
                strCodContrato = intIdContract,
                typeProduct = strTypeProduct
            };

            var objCustomerPhoneResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomerLineNumber(objRequest);
            });
            Logging.Info("IdSession: " + strIdSession, "Metodo: " + "GetCustomerPhone", objCustomerPhoneResponse.msisdn);
            var phone = objCustomerPhoneResponse.msisdn;
            return Json(phone, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public List<Helpers.CommonServices.GenericItem> GetProgramTask(string sessionId, string transactionId, string strIdLista)
        {
            var objListViewModel = new List<Helpers.CommonServices.GenericItem>();
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(sessionId);

            var objRequest = new ProgramTaskRequest
            {
                audit = audit,
                SessionId = sessionId,
                TransactionId = transactionId,
                StrIdLista = strIdLista
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetProgramTask(objRequest));

            if (objResponse != null)
            {
                var lstTemp = objResponse.ListProgramTask;
                if (lstTemp.Count > 0)
                {
                    foreach (var item in lstTemp)
                    {
                        var objViewModel = new Helpers.CommonServices.GenericItem
                        {
                            Code = item.Codigo,
                            Description = item.Descripcion
                        };
                        objListViewModel.Add(objViewModel);
                    }
                }
            }

            return objListViewModel;
        }

        public List<Helpers.CommonServices.GenericItem> GetTypeTransaction(string sessionId, string transactionId)
        {
            var objListViewModel = new List<Helpers.CommonServices.GenericItem>();
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(sessionId);

            var objRequest = new TypeTransactionRequest
            {
                audit = audit,
                SessionId = sessionId,
                TransactionId = transactionId
            };

            var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetTypeTransaction(objRequest));

            if (objResponse != null)
            {
                var lstTemp = objResponse.TypeTransaction;
                if (lstTemp.Count > 0)
                {
                    foreach (var item in lstTemp)
                    {
                        var objViewModel = new Helpers.CommonServices.GenericItem
                        {
                            Code = item.Codigo,
                            Description = item.Descripcion
                        };
                        objListViewModel.Add(objViewModel);
                    }
                }
            }

            return objListViewModel;
        }

        public Dictionary<string, object> InsertInteraction(InteractionModel objInteractionModel, TemplateInteractionModel oPlantillaDat, string strNroTelephone, string strUserSession, string strUserAplication, string strPassUser, bool boolEjecutTransaction, string strIdSession, string strCustomerId)
        {
            string contingenciaClarify = ConfigurationManager.AppSettings("gConstContingenciaClarify");

            var strTelefono = strNroTelephone == objInteractionModel.Telephone ? strNroTelephone : objInteractionModel.Telephone;

            //Obtener Cliente
            string strFlgRegistrado = ConstantsHFC.strUno;
            CustomerResponse objCustomerResponse;
            AuditRequestFixed audit = Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest()
            {
                audit = audit,
                vPhone = strTelefono,
                vAccount = string.Empty,
                vContactobjid1 = string.Empty,
                vFlagReg = strFlgRegistrado
            };
            objCustomerResponse = Logging.ExecuteMethod(() =>
            {
                return _oServiceFixed.GetCustomer(objGetCustomerRequest);
            });

            if (objCustomerResponse.Customer != null)
            {
                objInteractionModel.ObjidContacto = objCustomerResponse.Customer.ContactCode;
                objInteractionModel.ObjidSite = objCustomerResponse.Customer.SiteCode;
            }

            //Validacion de Contingencia
            Dictionary<string, string> result;
            if (contingenciaClarify != ConstantsHFC.blcasosVariableSI)
            {
                result = GetInsertInteractionCLFY(objInteractionModel, strIdSession);
            }
            else
            {
                result = GetInsertContingencyInteraction(objInteractionModel, strIdSession);
            }


            var rInteraccionId = result.FirstOrDefault().Value;

            var dictionaryResponse = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(rInteraccionId))
            {
                if (oPlantillaDat != null)
                {
                    dictionaryResponse = InsertPlantInteraction(oPlantillaDat, rInteraccionId, strNroTelephone, strUserSession, strUserAplication, strPassUser, boolEjecutTransaction, strIdSession);
                }
            }
            dictionaryResponse["rInteraccionId"] = rInteraccionId;
            dictionaryResponse["strFlagInsertion"] = result.SingleOrDefault(y => y.Key.Equals("rFlagInsercion")).Value;
            dictionaryResponse["strFlagInsertionInteraction"] = dictionaryResponse.SingleOrDefault(y => y.Key.Equals("FlagInsercion")).Value;
            return dictionaryResponse;
        }

        public void ExecuteUpdateInter30(string strIdSession, string strTransaction, string strInteractionId, string pText)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            UpdatexInter30Request objUpdateInter30Request = new UpdatexInter30Request
            {
                audit = audit,
                p_objid = strInteractionId,
                p_texto = pText
            };

            Logging.ExecuteMethod(() => { return _oServiceCommon.GetUpdatexInter30(objUpdateInter30Request); });
        }


        #region metodos para leer constancia via ftp
        public JsonResult ExistFile(string strFilePath, string strIdSession)
        {
            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            bool ExistFile = false;
            byte[] byteDataFile;
            Logging.Info(objAudit.Session, objAudit.transaction, "ExistFile: Ruta = " + strFilePath);
            try
            {
                ExistFile = DisplayFileFromServer(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);
                Logging.Info(objAudit.Session, objAudit.transaction, "ExistFile: Salida de Existencia = " + ExistFile.ToString());
                byteDataFile = null;
            }
            catch (Exception ex)
            {
                ExistFile = false;

                Logging.Error(strIdSession, objAudit.transaction, Functions.GetExceptionMessage(ex));
            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult ShowRecord(string strIdSession, string strFilePath)
        {
            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            byte[] databytes;
            string strContenType = string.Empty;
            try
            {
                string strExtentionFile = Path.GetExtension(strFilePath);
                strContenType = Functions.f_obtieneContentType(strExtentionFile);
                bool dt = false;
                dt = DisplayFileFromServer(strIdSession, objAudit.transaction, strFilePath, out databytes);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, Functions.GetExceptionMessage(ex));
                databytes = null;
            }

            return File(databytes, strContenType);

        }


        public bool DisplayFileFromServer(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
        {
            mydata = null;
            try
            {
                string doc = "//172.19.112.75/";
                string strFullPath = "ftp:" + doc;
                string user = ConfigurationManager.AppSettings("userftp");
                string pass = ConfigurationManager.AppSettings("passftp");
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: User: " + user);
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Pass: " + pass);
                FtpWebRequest sftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(strFullPath));
                ServicePointManager.ServerCertificateValidationCallback = (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;
                sftpRequest.Credentials = new NetworkCredential(user, pass);
                sftpRequest.KeepAlive = false;
                sftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                sftpRequest.UseBinary = true;
                sftpRequest.Proxy = null;
                sftpRequest.UsePassive = false;
                sftpRequest.EnableSsl = true;
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Lectura de path: " + sftpRequest.ToString());
                FtpWebResponse response = (FtpWebResponse)sftpRequest.GetResponse();
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response: " + sftpRequest.ToString());
                Stream responseStream = response.GetResponseStream();
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response Stream: " + responseStream.ToString());
                StreamReader reader = new StreamReader(responseStream);
                BinaryReader br = new BinaryReader(responseStream);
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response BinaryReader: " + br.ToString());
                FileInfo fiInfo = new FileInfo("/SIACUNICO/HFC/DETALLE_LLAMADAS_ENTRANTES/1508709011_04_08_2017_DETALLE_LLAMADAS_ENTRANTES_0.pdf");
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response FileInfo: " + fiInfo.ToString());
                FileStream fs = new FileStream(fiInfo.ToString(), FileMode.Open);
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response FileStream: " + fs.ToString());
                mydata = br.ReadBytes(Convert.ToInt((fs.Length - 1)));
                Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response mydata: " + mydata.ToString());
                reader.Close();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, strTransaction, Functions.GetExceptionMessage(ex));
                return false;
            }



            //Uri myurl = new Uri(strFullPath);
            //if (myurl.Scheme != Uri.UriSchemeFtp)
            //{
            //    mydata = null;
            //    return false;
            //}

            //try
            //{


            //    WebClient request = new WebClient();
            //    request.Credentials = new NetworkCredential(user, pass);
            //    mydata = request.DownloadData(myurl.ToString());
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Claro.Web.Logging.Error(strIdSession, strTransaction, CSTS.Functions.GetExceptionMessage(ex));

            //    mydata = null;
            //    return false;
            //}
        }

        #endregion

        #region metodos para leer constancia via carpeta compartida

        public JsonResult ExistFileSharedFile(string strFilePath, string strIdSession)
        {
            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            bool ExistFile = false;
            byte[] byteDataFile;
            Logging.Info(objAudit.Session, objAudit.transaction, "ExistFileSharedFile: Ruta = " + strFilePath);
            try
            {
                ExistFile = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strFilePath, out byteDataFile);
                Logging.Info(objAudit.Session, objAudit.transaction, "ExistFileSharedFile: Salida de Existencia = " + ExistFile.ToString());
                byteDataFile = null;
            }
            catch (Exception ex)
            {
                ExistFile = false;

                Logging.Error(strIdSession, objAudit.transaction, Functions.GetExceptionMessage(ex));
            }
            return Json(new { Exist = ExistFile }, JsonRequestBehavior.AllowGet);
        }

        public FileContentResult ShowRecordSharedFile(string strIdSession, string strFilePath)
        {
            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            byte[] databytes;
            string strContenType = string.Empty;
            try
            {
                string strExtentionFile = Path.GetExtension(strFilePath);
                strContenType = Functions.f_obtieneContentType(strExtentionFile);
                bool dt = false;
                dt = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strFilePath, out databytes);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, Functions.GetExceptionMessage(ex));
                databytes = null;
            }

            return File(databytes, strContenType);
        }

        public bool DisplayFileFromServerSharedFile(string strIdSession, string strTransaction, string strPath, out byte[] mydata)
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
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Response mydata: " + mydata.ToString());
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Suplantación OK");
                    blnResult = true;
                }
                else
                {
                    Logging.Info(strIdSession, strTransaction, "DisplayFileFromServer: Error de Suplantación ");
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
                Logging.Error(strIdSession, strTransaction, Functions.GetExceptionMessage(ex));

                mydata = null;
                blnResult = false;
            }

            obj.undoImpersonatiom();

            return blnResult;
        }

        public bool Get_Credentials(string strIdSession, string strTransaction, string strConexion, out string struser, out string strpass, out string strdomain)
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
                Logging.Error(strIdSession, strTransaction, Functions.GetExceptionMessage(ex));

                struser = string.Empty;
                strpass = string.Empty;
                strdomain = string.Empty;
                return false;
            }
        }

        #endregion
        public JsonResult SaveAuditMJson(string strTransaction, string strService, string strText, string strTelephone, string strNameCustomer, string strIdSession, string strIpCustomer = "", string strCuentUser = "", string strMontoInput = "0")
        {
            var strMonto = strMontoInput == "0" ? ConstantsHFC.strCero : strMontoInput;
            var strNameServidor = Request.ServerVariables["SERVER_NAME"];
            var strIpServidor = Request.ServerVariables["LOCAL_ADDR"];

            SaveAuditMResponseCommon objRegAuditResponseMCommon = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            SaveAuditMRequestCommon objRegAuditRequestMCommon =
                new SaveAuditMRequestCommon()
                {
                    audit = audit,
                    vTransaccion = strTransaction,
                    vServicio = strService,
                    vIpCliente = strIpCustomer,
                    vNombreCliente = strNameCustomer,
                    vIpServidor = strIpServidor,
                    vNombreServidor = strNameServidor,
                    vCuentaUsuario = strCuentUser,
                    vTelefono = strTelephone,
                    vMonto = strMonto,
                    vTexto = strText
                };

            try
            {
                objRegAuditResponseMCommon = Logging.ExecuteMethod<SaveAuditMResponseCommon>(
                    () =>
                    {
                        return _oServiceCommon.SaveAuditM(objRegAuditRequestMCommon);
                    });
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }

            var result = objRegAuditResponseMCommon.respuesta;

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public void UpdateXInter29(string strIdSession, string strInteractionId, string strText, string strOrder)
        {
            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            UpdateXInter29RequestCommon objUpdateXInter29Request = new UpdateXInter29RequestCommon
            {
                audit = audit,
                IdInteract = strInteractionId,
                Text = strText,
                Order = strOrder
            };

            Logging.ExecuteMethod(() => { return _oServiceCommon.UpdateXInter29(objUpdateXInter29Request); });
        }

        public static void LogException(string strIdSession, string strTransaction, string text, Exception error)
        {
            Exception realerror = error;
            while (realerror.InnerException != null)
                realerror = realerror.InnerException;
            Logging.Error(strIdSession, strTransaction, text + realerror.Message);
        }

        public string DownloandFilePDF(string strIdSession, string strPath, string strNameFile)
        {
            byte[] buffer;
            bool ExistFile = false;
            var RutaArchivoConstPDF = ConfigurationManager.AppSettings("strRutaArchivoConstPDF");
            var PathServer = System.Web.HttpContext.Current.Server.MapPath(RutaArchivoConstPDF);
            string PathFileEmail = PathServer + "\\" + strNameFile;
            DirectoryInfo di = null;

            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            Logging.Info(strIdSession, objAudit.transaction, "Begin a DownloandFilePDF"); // Temporal
            try
            {
                ExistFile = DisplayFileFromServerSharedFile(strIdSession, objAudit.transaction, strPath, out buffer);
                Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Obtiene en byte: " + ExistFile.ToString()); // Temporal
                if (ExistFile)
                {
                    //Verifica si existe la carpeta FileEmail
                    Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Creando la carpeta: " + PathServer); // Temporal
                    if (!Directory.Exists(PathServer))
                        di = Directory.CreateDirectory(PathServer);

                    System.IO.File.WriteAllBytes(PathFileEmail, buffer);
                }
                else
                {
                    PathFileEmail = string.Empty;
                }
                Logging.Info(strIdSession, objAudit.transaction, "DownloandFilePDF -> Descargado el archivo: " + PathFileEmail); // Temporal
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, ex.Message);
                PathFileEmail = string.Empty;
                return PathFileEmail;
            }
            Logging.Info(strIdSession, objAudit.transaction, "End a DownloandFilePDF"); // Temporal
            return PathFileEmail;
        }
        public bool DeleteFilePDF(string strIdSession, string strPathFileLocal)
        {
            bool respuesta = false;
            CommonService.AuditRequest objAudit = Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            Logging.Info(strIdSession, objAudit.transaction, "Begin a DeleteFilePDF"); // Temporal
            try
            {
                Logging.Info(strIdSession, objAudit.transaction, "DeleteFilePDF -> Parametro entrada strPathFileLocal: " + strPathFileLocal); // Temporal
                if (System.IO.File.Exists(strPathFileLocal))
                {
                    System.IO.File.Delete(strPathFileLocal);
                    respuesta = true;
                    Logging.Info(strIdSession, objAudit.transaction, "DeleteFilePDF -> Archivo Eliminado respuesta: " + respuesta.ToString()); // Temporal
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objAudit.transaction, ex.Message);
                throw new Exception(objAudit.transaction);
            }
            Logging.Info(strIdSession, objAudit.transaction, "End a DeleteFilePDF"); // Temporal
            return respuesta;
        }

        public ConsultIGVModel GetCommonConsultIgv(string strIdSession)
        {
            var oConsultIgv = new ConsultIGVModel();

            CommonTransacService.AuditRequest audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);

            var objRequest = new ConsultIGVRequest
            {
                audit = audit,
                SessionId = strIdSession,
                TransactionId = audit.transaction,
                AppId = audit.ipAddress,
                AppName = audit.applicationName,
                Username = audit.userName
            };

            try
            {
                var objResponse = Logging.ExecuteMethod(() => _oServiceCommon.GetConsultIGV(objRequest));

                if (objResponse != null && objResponse.ListConsultIGV.Count > 0)
                {
                    DateTime currentDate = DateTime.Now;
                    foreach (var item in objResponse.ListConsultIGV)
                    {
                        if (Convert.ToDate(item.impudFecIniVigencia) <= currentDate &&
                            Convert.ToDate(item.impudFecFinVigencia) >= currentDate &&
                            item.impunTipDoc == Constant.strCero)
                        {
                            oConsultIgv = new ConsultIGVModel
                            {
                                igv = item.igv,
                                igvD = item.igvD,
                                impudFecFinVigencia = item.impudFecFinVigencia,
                                impudFecIniVigencia = item.impudFecIniVigencia,
                                impudFecRegistro = item.impudFecRegistro,
                                impunTipDoc = item.impunTipDoc,
                                imputId = item.imputId,
                                impuvDes = item.impuvDes
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            Logging.Info("IdSession: " + strIdSession, "Transaccion: " + objRequest.audit.transaction, "End a GetConsultIGV Controller => oConsultIGV.igv: " + oConsultIgv.igv.ToString()); // Temporal
            return oConsultIgv;
        }


        public ItemGeneric GetInteractIDforCaseID(string strIdSession, string strCasoID)
        {
            ItemGeneric oItem = new ItemGeneric();
            CaseInsertResponse objResponse = new CaseInsertResponse();
            FixedTransacService.AuditRequest audit = Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            Logging.Info(audit.Session, audit.transaction, "GetInteractIDforCaseID IN | strCasoID: " + strCasoID);
            var objRequest = new CaseInsertRequest
            {
                audit = audit,
                ID_CASO = strCasoID
            };
            try
            {
                objResponse = Logging.ExecuteMethod(() => _oServiceFixed.GetInteractIDforCaseID(objRequest));
                oItem.Code = objResponse.rCasoId;
                oItem.Code2 = objResponse.rFlagInsercion;
                oItem.Description = objResponse.rMsgText;

                Logging.Info(audit.Session, audit.transaction, "GetInteractIDforCaseID OUT | Interaccio: " + oItem.Code);
            }
            catch (Exception ex)
            {
                Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }

            return oItem;
        }
        public List<ItemGeneric> GetBusinessRulesLst(string strIdSession, string strSubClase)
        {
            var lstBussinesRules = new List<ItemGeneric>();

            var objBusinessRulesResponse = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.BusinessRulesResponse();
            var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objBusinessRulesRequest = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.BusinessRulesRequest();
            objBusinessRulesRequest.audit = audit;
            objBusinessRulesRequest.SUB_CLASE = strSubClase;
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Inicio Método : GetBusinessRules", "strSubClase : " + strSubClase);
            try
            {
                objBusinessRulesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.BusinessRulesResponse>(() =>
                {
                    return _oServiceCommon.GetBusinessRules(objBusinessRulesRequest);
                });
                if (objBusinessRulesResponse != null && objBusinessRulesResponse.ListBusinessRules != null)
                {
                    foreach (var item in objBusinessRulesResponse.ListBusinessRules)
                    {
                        lstBussinesRules.Add(new ItemGeneric()
                        {
                            Description = item.REGLA
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            Claro.Web.Logging.Info("IdSession: " + strIdSession, "Método : GetBusinessRules", "Fín");
            return lstBussinesRules;
        }
        public string GetValueConfig(string strvalor, string strIdSession, string strTransaccion)
        {
            try
            {
                return KEY.AppSettings(strvalor);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strTransaccion, ex.Message);
                return "";
            }
        }
        public CommonService.Typification LoadTypifications(string strIdSession, string typeProduct, ref string lblMensaje)
        {
            Claro.Web.Logging.Configure();

            CommonService.Typification oTypification = new CommonService.Typification();
            CommonService.AuditRequest audit = App_Code.Common.CreateAuditRequest<CommonService.AuditRequest>(strIdSession);
            Claro.Web.Logging.Info(audit.Session, audit.transaction, "Metodo LoadTypification");

            try
            {
                CommonService.TypificationRequest objTypificationRequest = new CommonTransacService.TypificationRequest();
                objTypificationRequest.TRANSACTION_NAME = typeProduct;
                objTypificationRequest.audit = audit;

                var objResponse =
                Claro.Web.Logging.ExecuteMethod<CommonService.TypificationResponse>(
                    () => { return _oServiceCommon.GetTypification(objTypificationRequest); });

                oTypification = objResponse.ListTypification.First();

                if (oTypification == null)
                {
                    lblMensaje = FunctionsSIACU.GetValueFromConfigFile("strAjusteNoRecon", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);
                lblMensaje = FunctionsSIACU.GetValueFromConfigFile("strMensajeDeError", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return oTypification;
        }
        public string GetUsersStr(string strIdSession, string strCodeUser)
        {

            var strCac = string.Empty;
            Claro.Web.Logging.Info("Session: " + strIdSession, string.Empty, "strCodeUser: " + strCodeUser);

            Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UserResponse objRegionResponse = null;
            CommonTransacService.AuditRequest audit =
                Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objRegionRequest = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UserRequest();
            objRegionRequest.audit = audit;

            objRegionRequest.CodeUser = strCodeUser;
            objRegionRequest.CodeRol = ConstantsHFC.strMenosUno;
            objRegionRequest.CodeCac = ConstantsHFC.strMenosUno;
            objRegionRequest.State = ConstantsHFC.strMenosUno;
            try
            {
                objRegionResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.UserResponse>(() =>
                {
                    return _oServiceCommon.GetUser(objRegionRequest);
                });
                strCac = objRegionResponse.UserModel.Cac;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, audit.transaction, ex.Message);
                throw new Exception(ex.Message);
            }
            return strCac;
        }
        public JsonResult GetListDocument(string sessionId)
        {
            ProgramTaskResponse objProgramTaskResponse = null;
            CommonTransacService.AuditRequest audit =
            Common.CreateAuditRequest<CommonTransacService.AuditRequest>(sessionId);
            ProgramTaskRequest objProgramTaskRequest = new ProgramTaskRequest();
            string strIdLista = ConfigurationManager.AppSettings("strTypeListDocument");

            objProgramTaskRequest.audit = audit;
            objProgramTaskRequest.SessionId = sessionId;
            objProgramTaskRequest.TransactionId = audit.transaction;
            objProgramTaskRequest.StrIdLista = strIdLista;
            try
            {
                Logging.Info(objProgramTaskRequest.SessionId, objProgramTaskRequest.TransactionId, "IN Common GetListDocument > Entrando a GetProgramTask()");
                objProgramTaskResponse = Logging.ExecuteMethod<ProgramTaskResponse>(() =>
                {
                    return _oServiceCommon.GetProgramTask(objProgramTaskRequest);
                });
                Logging.Info(objProgramTaskRequest.SessionId, objProgramTaskRequest.TransactionId, "OUT Common GetListDocument > GetProgramTask()");
            }
            catch (Exception ex)
            {
                Logging.Info(objProgramTaskRequest.SessionId, objProgramTaskRequest.TransactionId, "ERROR Common GetListDocument >  Entrando a GetProgramTask()");
                Logging.Error(sessionId, audit.transaction, ex.Message);
                throw new Exception(audit.transaction);
            }

            return Json(new { data = objProgramTaskResponse });
        }


        public Office GetOffice(string strIdSession, string strCodeUser)
        {
            var audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            var objResponse = new OfficeResponseCommon();
            var objRequest = new OfficeRequestCommon();
            objRequest.audit = audit;
            objRequest.strCodeUser = strCodeUser;
            try
            {
                objResponse = Logging.ExecuteMethod<OfficeResponseCommon>(() =>
                {
                    return _oServiceCommon.GetOffice(objRequest);
                });
                Logging.Info("Ejecutó" + strCodeUser, "GetOffice", "Fin de GetOffice");

            }
            catch (Exception ex)
            {
                Logging.Info("Entro a catch" + strCodeUser, "GetOffice", "Fin de catch GetOffice" + ex.Message);
            }
            if (objResponse.objOffice == null)
            {
                objResponse.objOffice = new Office();
            }
            if (objResponse.objOffice.strCodeOffice == null)
            {
                Logging.Info("EjecutoPMHFC" + strCodeUser, "GetOffice", "strCodeOffice nulo");
                objResponse.objOffice.strCodeOffice = GetValueConfig("strPuntoVentaDefault", strIdSession, "");
            }
            else if (objResponse.objOffice.strCodeOffice == string.Empty)
            {
                Logging.Info("EjecutoPMHFC" + strCodeUser, "GetOffice", "strCodeOffice vacio");
                objResponse.objOffice.strCodeOffice = GetValueConfig("strPuntoVentaDefault", strIdSession, ""); ; ;
            }
            return objResponse.objOffice;
        }

        // PROY140141-MIGRACION KV FASE 2       
        public GenerateConstancyResponseCommon GenerateContancyWithXml(string idSession, ParametersGeneratePDF parameters, string xml)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");

            ////ADICIÓN DE S/. AL CARGO FIJO CON IGV
            //parameters.StrCargoFijoConIGV = "S/. " + parameters.StrCargoFijoConIGV;
            //parameters.StrCostoTransaccion = "S/. " + parameters.StrCostoTransaccion;
            //parameters.StrCargoFijo = "S/. " + parameters.StrCargoFijo;

            var strTerminacionPDF = ConfigurationManager.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession)
            };
            Logging.Info(idSession, "", "IN GenerateContancyWithXml()");
            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return _oServiceCommon.GenerateContancyWithXml(request, xml);
            });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());
            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", "OUT GenerateContancyWithXml()");
            return objResponse;
        }
        // FIN PROY140141-MIGRACIONKV FASE 2   


        public GenerateConstancyResponseCommon GenerateContancyWithXml(string idSession, string xml, ParametersGeneratePDF parameters)
        {
            var strTerminacionPDF = ConfigurationManager.AppSettings("strTerminacionPDF");

            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession)
            };
            Logging.Info(idSession, "", "IN GenerateContancyWithXml()");
            GenerateConstancyResponseCommon objResponse =
            Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() =>
            {
                return _oServiceCommon.GenerateContancyWithXml(request, xml);
            });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());
            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");

                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion.Replace("/", "_"), strTerminacionPDF);

                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);

                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, parameters.StrNombreArchivoTransaccion, strTerminacionPDF);

                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", "OUT GenerateContancyWithXml()");
            return objResponse;
        }

        #region Proy-32650
        /// <summary>
        /// Obtiene el listado de los descuentos configurados.
        /// </summary>
        /// <param name="strIdTipo">Obtiene el tipo de campaña</param>
        /// <returns>Listado de los descuentos configurados</returns>
        public JsonResult GetMonths(string strIdSession, string strIdTipo)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaMonthsResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
            App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objGetMonthsRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    Tipo = strIdTipo
                };

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin GetMonths");
            try
            {
                objlistaMonthsResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetMonths(objGetMonthsRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objGetMonthsRequest.audit.transaction, ex.Message);

            }


            Models.CommonServices objCommonServices = null;

            if (objlistaMonthsResponse != null && objlistaMonthsResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objlistaMonthsResponse.AccionTypes)
                {
                    Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                        new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                    oCacDacTypeVM.Code = /*item.Codigo + "|"+*/ item.Codigo2; //VALOR
                    oCacDacTypeVM.Description = item.Descripcion;
                    listCacDacTypes.Add(oCacDacTypeVM);
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "End GetMonths - Total Registros : " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        /// <summary>
        /// Obtiene el listado de los descuentos configurados.
        /// </summary>
        /// <returns>Listado de los descuentos configurados</returns>
        public JsonResult GetListDiscount(string strIdSession)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaDescuentoCFResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objlistaDescuentoCFRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit
                };

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin GetListDiscount");
            try
            {
                objlistaDescuentoCFResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetListDiscount(objlistaDescuentoCFRequest); //codigo es el IdPorcentaje, no el valor del %
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objlistaDescuentoCFRequest.audit.transaction, ex.Message);

            }


            Claro.Web.Logging.Info(strIdSession, audit.transaction, "End GetListDiscount - Total Registros : " + objlistaDescuentoCFResponse.AccionTypes.Count);
            return Json(new { data = objlistaDescuentoCFResponse.AccionTypes });


        }

        public JsonResult GetListarAccionesRC(string strIdSession)
        {
            FixedTransacService.RetentionCancelServicesResponse objlistaAccionesResponse = new FixedTransacService.RetentionCancelServicesResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.RetentionCancelServicesRequest objlistaAccionesRequest =
                new FixedTransacService.RetentionCancelServicesRequest()
                {
                    audit = audit,
                    vNivel = Convert.ToInt(ConfigurationManager.AppSettings("gConstPerfil_AsesorCAC"))
                };

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin method");
            try
            {
                objlistaAccionesResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                    {
                        return _oServiceFixed.GetListarAccionesRC(objlistaAccionesRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objlistaAccionesRequest.audit.transaction, ex.Message);

            }


            Models.CommonServices objCommonServices = null;

            if (objlistaAccionesResponse != null && objlistaAccionesResponse.AccionTypes != null)
            {
                objCommonServices = new Models.CommonServices();
                List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM> listCacDacTypes =
                    new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM>();

                foreach (Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenericItem item in objlistaAccionesResponse.AccionTypes)
                {
                    Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM oCacDacTypeVM =
                        new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.CommonServices.CacDacTypeVM();

                    if (item.Cod_tipo_servicio == Claro.SIACU.Transac.Service.Constants.numeroTres)
                    {
                        oCacDacTypeVM.Code = item.Codigo;
                        oCacDacTypeVM.Description = item.Descripcion;
                        listCacDacTypes.Add(oCacDacTypeVM);
                    }
                }
                objCommonServices.CacDacTypes = listCacDacTypes;
            }

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "End method - Total Registros: " + objCommonServices.CacDacTypes.Count);
            return Json(new { data = objCommonServices.CacDacTypes });
        }

        /// <summary>
        /// Obtiene los servicios adicionales disponibles a contratar - HFC
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Listado de los servicios adicionales disponibles a contratar</returns>
        public JsonResult HfcGetAdditionalServices(string strCoId)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var lstFinal = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.CommercialServiceHP>();
            StringBuilder sb = new StringBuilder();
            List<PlanService> lstRetentionRate = new List<PlanService>();
            try
            {
                var lstCommertialServices = GetAdditionalServices(strCoId);//Lista de Servicios comerciales bscs
                var strCommertialPlan = GetCommertialPlan(strCoId);//plan comercial del contrato bscs
                var lstRetentionServices = GetRetServicesByPlan(strCommertialPlan, Models.HFC.typeHFCLTE.HFC.ToString());//servicios de retención desde pvu



                for (int i = 0; i < lstCommertialServices.Count; i++)
                {
                    for (int j = 0; j < lstRetentionServices.Count; j++)
                    {
                        if ((lstCommertialServices[i].SNCODE.Equals(lstRetentionServices[j].SNCode) && lstCommertialServices[i].SPCODE.Equals(lstRetentionServices[j].SPCode)) && lstCommertialServices[i].ESTADO != "A")
                        {
                            lstCommertialServices[i].COSTOPVU = String.Format("{0:0.00}", Double.Parse(lstRetentionServices[j].CF));
                            lstCommertialServices[i].VALORPVU = lstRetentionServices[j].DesCodigoExterno;
                            lstCommertialServices[i].DESCOSER = lstRetentionServices[j].DesServSisact;
                            lstCommertialServices[i].TIPO_SERVICIO = lstRetentionServices[j].TipoServ;
                            lstCommertialServices[i].CODGRUPOSERV = lstRetentionServices[j].CodGrupoServ;

                            lstCommertialServices[i].CANTEQUIPO = lstRetentionServices[j].CantidadEquipo;
                            lstCommertialServices[i].IDEQUIPO = lstRetentionServices[j].TipoEquipo; //IdEquipo; //TIPEQU
                            lstCommertialServices[i].CODTIPOEQUIPO = lstRetentionServices[j].MatvIdSap; //TipoEquipo; //CODTIPEQU

                            lstCommertialServices[i].CODSERPVU = lstRetentionServices[j].CodServSisact;
                            lstCommertialServices[i].TMCODE = lstRetentionServices[j].TmCode;

                            sb.Append(lstRetentionServices[j].TmCode + ConstantsHFC.PresentationLayer.gstrVariablePipeline + lstRetentionServices[j].SNCode + ConstantsHFC.PresentationLayer.gstrVariablePipeline + strCommertialPlan + ConstantsHFC.PresentationLayer.gstrPuntoyComa);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    lstRetentionRate = GetRetentionRate(sb.ToString());//costo de los servicios bscs
                    if (lstRetentionRate.Count > 0)
                    {
                        for (int i = 0; i < lstCommertialServices.Count; i++)
                        {
                            for (int j = 0; j < lstRetentionRate.Count; j++)
                            {
                                if (lstCommertialServices[i].SNCODE.Equals(lstRetentionRate[j].SNCode) && lstRetentionRate[j].CF != ConstantsHFC.PresentationLayer.NumeracionMENOSUNO)
                                {
                                    lstCommertialServices[i].CARGOFIJO = String.Format("{0:0.00}", Double.Parse(lstRetentionRate[j].CF)); //Tarifa de retencion
                                    if (!lstCommertialServices[i].COSTOPVU.Equals(ConstantsHFC.PresentationLayer.gstrNoInfo))
                                    {
                                        lstFinal.Add(lstCommertialServices[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error(objAuditRequest.Session, objAuditRequest.Session, ex.Message);
            }


            return new JsonResult
            {
                Data = lstFinal,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Obtiene los servicios adicionales disponibles a contratar - LTE
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Listado de los servicios adicionales disponibles a contratar</returns>
        public JsonResult LTEGetAdditionalServices(string strIdSession, string strCoId)
        {

            var audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);
            var lstFinal = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.CommercialServiceHP>();
            List<PlanService> lstRetentionRate = new List<PlanService>();
            StringBuilder sb = new StringBuilder();
            var objCommercialPlanRequest = new CommertialPlanRequest
            {
                audit = audit,
                StrCoId = strCoId
            };
            try
            {
                var lstCommertialServices = GetAdditionalServices(strCoId);//Lista de Servicios comerciales bscs
                var strCommertialPlan = GetCommertialPlan(strCoId);//plan comercial del contrato bscs
                var lstRetentionServices = GetRetServicesByPlan(strCommertialPlan, Models.HFC.typeHFCLTE.LTE.ToString());//servicios de retención desde pvu


                for (int i = 0; i < lstCommertialServices.Count; i++)
                {
                    for (int j = 0; j < lstRetentionServices.Count; j++)
                    {
                        var tipoEquTemp = "|" + lstRetentionServices[j].TipoEquipo.Replace(" ", string.Empty) + "|";

                        if ((lstCommertialServices[i].SNCODE.Equals(lstRetentionServices[j].SNCode) && lstCommertialServices[i].SPCODE.Equals(lstRetentionServices[j].SPCode)) && lstCommertialServices[i].ESTADO != "A" && !ConfigurationManager.AppSettings("gConstFilterOnlyDecos").Contains(tipoEquTemp))
                        {
                            lstCommertialServices[i].COSTOPVU = String.Format("{0:0.00}", Double.Parse(lstRetentionServices[j].CF));
                            lstCommertialServices[i].VALORPVU = lstRetentionServices[j].DesCodigoExterno;
                            lstCommertialServices[i].DESCOSER = lstRetentionServices[j].DesServSisact;
                            lstCommertialServices[i].TIPO_SERVICIO = lstRetentionServices[j].TipoServ;
                            lstCommertialServices[i].CODGRUPOSERV = lstRetentionServices[j].CodGrupoServ;
                            lstCommertialServices[i].TMCODE = lstRetentionServices[j].TmCode;

                            lstCommertialServices[i].CANTEQUIPO = lstRetentionServices[j].CantidadEquipo;
                            lstCommertialServices[i].IDEQUIPO = lstRetentionServices[j].TipoEquipo; //IdEquipo; //TIPEQU
                            lstCommertialServices[i].CODTIPOEQUIPO = lstRetentionServices[j].MatvIdSap; //TipoEquipo; //CODTIPEQU

							lstCommertialServices[i].CODSERPVU = lstRetentionServices[j].CodServSisact;
                            sb.Append(lstRetentionServices[j].TmCode + ConstantsHFC.PresentationLayer.gstrVariablePipeline + lstRetentionServices[j].SNCode + ConstantsHFC.PresentationLayer.gstrVariablePipeline + strCommertialPlan + ConstantsHFC.PresentationLayer.gstrPuntoyComa);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    lstRetentionRate = GetRetentionRate(sb.ToString());//costo de los servicios bscs
                    if (lstRetentionRate.Count > 0)
                    {
                        for (int i = 0; i < lstCommertialServices.Count; i++)
                        {
                            for (int j = 0; j < lstRetentionRate.Count; j++)
                            {
                                if (lstCommertialServices[i].SNCODE.Equals(lstRetentionRate[j].SNCode) && lstRetentionRate[j].CF != ConstantsHFC.PresentationLayer.NumeracionMENOSUNO)
                                {
                                    lstCommertialServices[i].CARGOFIJO = String.Format("{0:0.00}", Double.Parse(lstRetentionRate[j].CF)); //Tarifa de retencion
                                    if (!lstCommertialServices[i].COSTOPVU.Equals(ConstantsHFC.PresentationLayer.gstrNoInfo))
                                    {
                                        lstFinal.Add(lstCommertialServices[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objCommercialPlanRequest.audit.transaction, Functions.GetExceptionMessage(ex));
                throw new Exception(audit.transaction);
            }

            return new JsonResult
            {
                Data = lstFinal,
                ContentType = "application/json",
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// Obtiene los servicios asociados al plan comercial en PVU.
        /// </summary>
        /// <param name="strCodePlan">Plan comercial del contrato</param>
        /// <param name="strTypeProduct">Codigo del producto</param>
        /// <returns>Listado de servicios adicionales disponibles a contratar en PVU</returns>
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.PlanServiceHP> GetRetServicesByPlan(string strCodePlan, string strTypeProduct)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var objLstPlanService = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.PlanServiceHP>();
            var objCommercialServicesRequest = new PlanServicesRequest
            {
                audit = objAuditRequest,
                IdPlan = strCodePlan,
                TypeProduct = ConfigurationManager.AppSettings("strProductoHFC"),
                CodeProduct = ConfigurationManager.AppSettings("strProductoLTE")
            };
            try
            {
                var objPlanServiceResponse = Logging.ExecuteMethod(() => { return (strTypeProduct == Models.HFC.typeHFCLTE.HFC.ToString() ? _oServiceFixed.GetPlanServices(objCommercialServicesRequest) : _oServiceFixed.GetPlanServicesLte(objCommercialServicesRequest)); });
                if (objPlanServiceResponse.LstPlanServices.Count > 0)
                {
                    var lstTemp = objPlanServiceResponse.LstPlanServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.PlanServiceHP
                        {
                            CF = item.CF ?? "",
                            CantidadEquipo = item.CantidadEquipo ?? "",
                            CodGrupoServ = item.CodGrupoServ ?? "",
                            CodServSisact = item.CodServSisact ?? "",
                            CodTipoServ = item.CodTipoServ ?? "",
                            CodigoExterno = item.CodigoExterno ?? "",
                            CodigoPlan = item.CodigoPlan ?? "",
                            DesCodigoExterno = item.DesCodigoExterno ?? "",
                            DesServSisact = item.DesServSisact ?? "",
                            DescPlan = item.DescPlan ?? "",
                            Equipo = item.Equipo ?? "",
                            GrupoServ = item.GrupoServ ?? "",
                            IdEquipo = item.IdEquipo ?? "",
                            MatvDesSap = item.MatvDesSap ?? "",
                            MatvIdSap = item.MatvIdSap ?? "",
                            SNCode = item.SNCode ?? "",
                            SPCode = item.SPCode ?? "",
                            ServvUsuarioCrea = item.ServvUsuarioCrea ?? "",
                            Solucion = item.Solucion ?? "",
                            TipoEquipo = item.TipoEquipo ?? "",
                            TipoServ = item.TipoServ ?? "",
                            TmCode = item.TmCode ?? ""
                        };

                        objLstPlanService.Add(objTemp);

                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return objLstPlanService;
        }

        /// <summary>
        /// Obtiene los servicios adicionales disponibles a contratar/contratados del cliente.
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Servicios Adicionales disponibles para contratar.</returns>
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.CommercialServiceHP> GetAdditionalServices(string strCoId)
        {
            var objLstCommercialService = new List<Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.CommercialServiceHP>();
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");

            var objCommercialServicesRequest = new CommercialServicesRequest
            {
                audit = objAuditRequest,
                StrCoId = strCoId
            };
            try
            {
                var objCommercialServicesResponse = Logging.ExecuteMethod(() => { return _oServiceFixed.GetCommercialService(objCommercialServicesRequest); });
                if (objCommercialServicesResponse.LstCommercialServices.Count > 0)
                {
                    var lstTemp = objCommercialServicesResponse.LstCommercialServices;
                    foreach (var item in lstTemp)
                    {
                        var objTemp = new Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices.CommercialServiceHP
                        {
                            DE_SER = item.DE_SER ?? "",
                            DE_EXCL = item.DE_EXCL ?? "",
                            BLOQ_ACT = item.BLOQ_ACT ?? "",
                            BLOQ_DES = item.BLOQ_DES ?? "",
                            CARGOFIJO = item.CARGOFIJO ?? "",
                            CODSERPVU = item.CODSERPVU ?? "",
                            COSTOPVU = item.COSTOPVU ?? "",
                            CO_EXCL = item.CO_EXCL ?? "",
                            CO_SER = item.CO_SER ?? "",
                            CUOTA = item.CUOTA ?? "",
                            DESCOSER = item.DESCOSER ?? "",
                            DESCUENTO = item.DESCUENTO,
                            DE_GRP = item.DE_GRP ?? "",
                            ESTADO = item.ESTADO ?? "",
                            NO_GRP = item.NO_GRP ?? "",
                            NO_SER = item.NO_SER ?? "",
                            PERIODOS = item.PERIODOS ?? "",
                            SNCODE = item.SNCODE ?? "",
                            SPCODE = item.SPCODE ?? "",
                            SUSCRIP = item.SUSCRIP ?? "",
                            TIPOSERVICIO = item.TIPOSERVICIO ?? "",
                            TIPO_SERVICIO = item.TIPO_SERVICIO ?? "",
                            VALIDO_DESDE = item.VALIDO_DESDE ?? "",
                            VALORPVU = item.VALORPVU ?? ""
                        };

                        objLstCommercialService.Add(objTemp);
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objLstCommercialService;
        }

        /// <summary>
        /// Obtiene el plan comercial asociado al contrato.
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Plan comercial del contrato</returns>
        public string GetCommertialPlan(string strCoId)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var objComertialPlan = new CommertialPlanResponse();
            var objCommercialPlanRequest = new CommertialPlanRequest
            {
                audit = objAuditRequest,
                StrCoId = strCoId
            };

            try
            {
                objComertialPlan = Logging.ExecuteMethod(() =>
                { return _oServiceFixed.GetCommertialPlan(objCommercialPlanRequest); });
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCommercialPlanRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objComertialPlan.rCodigoPlan;
        }

        /// <summary>
        /// Obtiene los montos configurados de los servicios adicionales disponibles a contratar.
        /// </summary>
        /// <param name="strArrServices">Concatenacion por palotes del TMCODE,Plan comercial y sncode</param>
        /// <returns>el monto de los sncode configurados en BSCS</returns>
        public List<Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.PlanService> GetRetentionRate(string strArrServices)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var objServicesResponse = new PlanServicesResponse();
            FixedTransacService.PlanServicesRequest objServicesRequest = new FixedTransacService.PlanServicesRequest
            {
                ArrServices = strArrServices,
                audit = objAuditRequest
            };
            try
            {
                objServicesResponse = Logging.ExecuteMethod<FixedTransacService.PlanServicesResponse>(() =>
                {
                    return new FixedTransacService.FixedTransacServiceClient().GetRetentionRate(objServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return objServicesResponse.LstPlanServices;
        }

        /// <summary>
        /// Obtiene el monto total de inversion
        /// </summary>
        /// <param name="strCoId">codigo de contrato</param>
        /// <returns>El monto total de la inversion del cliente</returns>
        public JsonResult GetTotalInversion(string strCoId)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            var objServicesResponse = new RetentionCancelServicesResponse();
            var objRetServicesRequest = new RetentionCancelServicesRequest
            {
                audit = objAuditRequest,
                CodId = int.Parse(strCoId)
            };
            try
            {
                objServicesResponse = Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                {
                    return new FixedTransacService.FixedTransacServiceClient().GetTotalInversion(objRetServicesRequest);
                });
            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objRetServicesRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }

            return Json(new { data = String.Format("{0:0.00}", objServicesResponse.CargoFijoActual) });
        }

        /// <summary>
        /// Validacion si tiene bono actual
        /// </summary>
        /// <param name="strCoId">codigo de contrado</param>
        /// <returns></returns>
        public JsonResult GetCurrentDiscountFixedCharge(string strCoId)
        {
            var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
            RetentionCancelServicesRequest objCurrentDiscountRequest = new RetentionCancelServicesRequest();
            objCurrentDiscountRequest.audit = objAuditRequest;
            objCurrentDiscountRequest.CodId = Convert.ToInt(strCoId);
            var objCurrentDiscountResponse = new RetentionCancelServicesResponse();
            string strValidaDescuentoActivo = "";
            try
            {
                objCurrentDiscountResponse = Logging.ExecuteMethod<FixedTransacService.RetentionCancelServicesResponse>(() =>
                {
                    return new FixedTransacService.FixedTransacServiceClient().GetCurrentDiscountFixedCharge(objCurrentDiscountRequest);
                });

                if (objCurrentDiscountResponse.CurrentDiscounts.Count > 0)
                {
                    if (Convert.ToDouble(objCurrentDiscountResponse.CurrentDiscounts[0].TOTAL_DESCUENTO) > 0
                           && objCurrentDiscountResponse.CurrentDiscounts[0].FLAG == "0"
                           && DateTime.Parse(objCurrentDiscountResponse.CurrentDiscounts[0].FEC_FIN) > DateTime.Today)
                    {
                        strValidaDescuentoActivo = string.Format(ConfigurationManager.AppSettings("gConstMsgDescuentoActivoCF"), objCurrentDiscountResponse.CurrentDiscounts[0].PORCENTAJE, objCurrentDiscountResponse.CurrentDiscounts[0].PERIODO, Convert.ToDate(objCurrentDiscountResponse.CurrentDiscounts[0].FEC_INICIO).ToShortDateString());
                    }
                    else if (Convert.ToDouble(objCurrentDiscountResponse.CurrentDiscounts[0].TOTAL_DESCUENTO) > 0
                           && objCurrentDiscountResponse.CurrentDiscounts[0].FLAG == "1"
                           && DateTime.Parse(objCurrentDiscountResponse.CurrentDiscounts[0].FEC_FIN) > DateTime.Today)
                    {
                        strValidaDescuentoActivo = string.Format(ConfigurationManager.AppSettings("gConstMsgDescuentoActivoSA"), objCurrentDiscountResponse.CurrentDiscounts[0].PERIODO, Convert.ToDate(objCurrentDiscountResponse.CurrentDiscounts[0].FEC_INICIO).ToShortDateString());
                    }
                }

            }
            catch (Exception ex)
            {
                Logging.Error("SESSION", objCurrentDiscountRequest.audit.transaction, ex.Message);
                throw new Exception(objAuditRequest.transaction);
            }
            return Json(new { data = strValidaDescuentoActivo });
        }


        /// <summary>
        /// Validacion si aplica a descuento de promoción a factura vigente de pago N/C check
        /// </summary>
        /// <param name="strCoId">codigo de contrato</param>
        /// <param name="strTantoPorciento">Porcentaje para formula</param>
        /// <param name="strCliNroCuenta">Numero de cuenta del cliente</param>
        /// <param name="strNroTelefono">Numero de telefono</param>
        /// <param name="strCustomerId">CustomerId del cliente</param>
        /// <param name="BillingCycle">Ciclo de facturacion del cliente</param>
        /// <returns>Si aplica la promocion a la factura del clietne</returns>
        public JsonResult GetValidationOfPromotionToCurrentInvoice(string strIdSession, string strCoId, string strTantoPorciento, string strCliNroCuenta, string strNroTelefono, string strCustomerId, string BillingCycle)
        {
            if (string.IsNullOrEmpty(BillingCycle)) { BillingCycle = "01"; }
            Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", "metodo para el validar check");
            Claro.Web.Logging.Info(strIdSession, "Método : GetValidationOfPromotionToCurrentInvoice INI", " strCoId : " + strCoId + ", strTantoPorciento : " + strTantoPorciento + ", strCliNroCuenta: " + strCliNroCuenta + ", strNroTelefono:" + strNroTelefono + ", strCustomerId:" + strCustomerId + ", (ciclodefacturacion) BillingCycle:" + BillingCycle);

            if (string.IsNullOrEmpty(strTantoPorciento)) { strTantoPorciento = "0 %"; }
            strTantoPorciento = strTantoPorciento.Substring(0, strTantoPorciento.IndexOf('%')).Trim();

            bool PermiteCheck = true;
            string strMensajeValidador = string.Empty;
            string strTransaccion = string.Empty;
            try
            {
                var objAuditRequest = App_Code.Common.CreateAuditRequest<AuditRequestFixed>("SESSION");
                RetentionCancelServicesRequest objCurrentDiscountRequest = new RetentionCancelServicesRequest();
                objCurrentDiscountRequest.audit = objAuditRequest;
                objCurrentDiscountRequest.CodId = Convert.ToInt(strCoId);
                strTransaccion = objAuditRequest.transaction;

                bool validateService = false;

                //VALIDACION 01
                Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("entra a llamar al ws de consultapagos en el metodo GetVerifyPaymentes(strIdSession: {0}, strCoId: {1})", strIdSession, strCoId));
                Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("GetVerifyPaymentes(strIdSession:{0}, strCustomerId:{1})", strIdSession, strCustomerId));
                QueryDebtResponse objConsultaPagosWS = GetVerifyPaymentes(strIdSession, strCustomerId); // ws ConsultaPagos

                if (objConsultaPagosWS != null)
                {
                    if (objConsultaPagosWS.audit.errorCode == "2" || objConsultaPagosWS.audit.errorCode == "1")
                    {
                        strMensajeValidador = ConfigurationManager.AppSettings("strClienteSinFactPendtePago");
                        PermiteCheck = false;
                        Claro.Web.Logging.Error(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("objConsultaPagosWS: Code: {0}, Msg: {1}", objConsultaPagosWS.audit.errorCode, objConsultaPagosWS.audit.errorMsg));
                    }
                    else if (objConsultaPagosWS.audit.errorCode == Constant.strCero)
                    {
                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("objConsultaPagosWS: Code: {0}, Msg: {1}", objConsultaPagosWS.audit.errorCode, objConsultaPagosWS.audit.errorMsg));
                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", "validara el IF,a la regla de validacion 01: objConsultaPagosWS != null && objConsultaPagosWS.xNroDocsDeuda < 1");
                        if (objConsultaPagosWS.xNroDocsDeuda < 1)//El cliente no cuenta con factura pendiente de pago
                        {
                            strMensajeValidador = ConfigurationManager.AppSettings("strClienteSinFactPendtePago");
                            PermiteCheck = false;
                            Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format(" entró a validacion 1:, valores: strMensajeValidador {0}, PermiteCheck {1}", ConfigurationManager.AppSettings("strClienteSinFactPendtePago"), "false"));
                        }
                        else
                        {
                            validateService = true;
                        }
                    }
                    else
                    {
                        strMensajeValidador = ConfigurationManager.AppSettings("strErrorConsulPagCliente");
                        PermiteCheck = false;
                        Claro.Web.Logging.Error(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("objConsultaPagosWS: Code: {0}, Msg: {1}", objConsultaPagosWS.audit.errorCode, objConsultaPagosWS.audit.errorMsg));

                    }
                }
                else
                {
                    strMensajeValidador = ConfigurationManager.AppSettings("strErrorConsulPagCliente");
                    PermiteCheck = false;
                    Claro.Web.Logging.Error(strIdSession, "GetValidationOfPromotionToCurrentInvoice", "Error en la devolucion de datos por parte del Servicio, objConsultaPagosWS nulo");
                }

                if (validateService)
                {
                    Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("entrara a llamar al ws consultaEstadoCuenta en el metodo GetVerificationOfAccountStatus(strIdSession {0}, strCliNroCuenta {1}, strNroTelefono {2}, BillingCycle:{3})", strIdSession, strCliNroCuenta, strNroTelefono, BillingCycle));
                    //VALIDACION 02
                    AccountStatusResponse objConsultaEstadoCuenta = GetVerificationOfAccountStatus(strIdSession, strCliNroCuenta, strNroTelefono, BillingCycle); // ws ConsultaEstadoCuenta
                    if (objConsultaEstadoCuenta != null)
                    {
                        if (objConsultaEstadoCuenta.audit.errorCode == Constant.strCero)
                        {
                            Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("objConsultaEstadoCuenta: Code: {0}, Msg: {1}", objConsultaEstadoCuenta.audit.errorCode, objConsultaEstadoCuenta.audit.errorMsg));

                            if (objConsultaEstadoCuenta.xEstadoCuenta != null && objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab.Count > 0 && objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx != null && objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta.Count > 0)
                            {
                                Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("obtener la posicion/ indice del ultimo recibo y ver si tiene monto reclamado es: {0}", (objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta.Count - 1).ToString()));
                                int ultimaPosicion = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta.Count - 1;

                                if (ultimaPosicion > -1)
                                {

                                    string strMontoPagadoParcialmente = ConfigurationManager.AppSettings("strMontoPagadoParcialmente");
                                    string strEstadoDocumentoEmitido = ConfigurationManager.AppSettings("strEstadoDocumentoEmitido");
                                    
                                    //verifica si el ultimo recibo tiene monto reclamado
                                    if (Convert.ToDecimal(objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xMontoReclamado) > 0)
                                    {
                                        strMensajeValidador = ConfigurationManager.AppSettings("strReciboConMontoReclamado");
                                        PermiteCheck = false;
                                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format(" entró a validacion 2:, valores: strMensajeValidador {0}, PermiteCheck {1}", ConfigurationManager.AppSettings("strReciboConMontoReclamado"), "false"));
                                    }
                                    /*fecha de vencimien < que la fecha de emsion, no debe permitir si el recibo esta vencido.*/
                                    /*else if ( Convert.ToDate(Convert.ToDate(objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xFechaVencimiento).ToShortDateString()) < Convert.ToDate( DateTime.Now.ToShortDateString())) {
                                        strMensajeValidador = ConfigurationManager.AppSettings("strClienteSinFactPendtePago");
                                        PermiteCheck = false;
                                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format(" entró a validacion 3:, valores: strMensajeValidador {0}, PermiteCheck {1}", ConfigurationManager.AppSettings("strClienteSinFactPendtePago"), "false"));
                                    }*/

                                    //si documento esta pagado PARCIALMENTE o EMITIDO
                                    else if ((objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xEstadoDocumento == strMontoPagadoParcialmente) || (objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xEstadoDocumento==strEstadoDocumentoEmitido))
                                    {
                                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("evaluar la condicion para la regla si tiene monto pagado parcialmente: if (objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xEstadoDocumento: {0} == strMontoPagadoParcialmente {1})", objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xEstadoDocumento, strMontoPagadoParcialmente));
                                        double montoPendiente = Convert.ToDouble(objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosicion].xSaldoDocumento);
                                        double  dIgv = Convert.ToDouble(GetCommonConsultIgv(strIdSession).igvD.ToString(System.Globalization.CultureInfo.InvariantCulture));
                                        double cargoFijoMensual = obtenerCargoFijo(strIdSession, strCustomerId);
                                        cargoFijoMensual = cargoFijoMensual + (cargoFijoMensual * dIgv);
                                        Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("el montoPendiente: {0}, el cargoFijoMensual: {1} ", montoPendiente, cargoFijoMensual));
                                        //ha pagado parcialmente por eso hay un saldo pendiente
                                        if (montoPendiente > 0 && (((Convert.ToDouble(strTantoPorciento) / 100) * cargoFijoMensual) > montoPendiente))
                                        {
                                            //aqui validar si el monto pendiente es > al monto porcentaje de descuento
                                            strMensajeValidador = ConfigurationManager.AppSettings("strMontoPendteMenorADescnto");
                                            PermiteCheck = false;
                                            Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("entro al if strMensajeValidador: {0}, PermiteCheck {1} ", strMensajeValidador, "false"));
                                        }
                                    }
                                }
                            }
                            else if (objConsultaEstadoCuenta.xEstadoCuenta != null && objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab.Count > 0 && (objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx == null || objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta.Count == 0))
                            {
                                strMensajeValidador = ConfigurationManager.AppSettings("strClienteSinFactPendtePago");
                                PermiteCheck = false;
                                Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("entro al if strMensajeValidador: {0}, PermiteCheck {1} ", strMensajeValidador, "false"));

                            }
                            else
                            {
                                strMensajeValidador = ConfigurationManager.AppSettings("strErrorObtenerDetalleEC");
                                PermiteCheck = false;
                                Claro.Web.Logging.Info(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("entro al if strMensajeValidador: {0}, PermiteCheck {1} ", strMensajeValidador, "false"));
                            }
                        }
                        else
                        {
                            strMensajeValidador = ConfigurationManager.AppSettings("strErrorConsulEstCuenta");
                            PermiteCheck = false;
                            Claro.Web.Logging.Error(strIdSession, "GetValidationOfPromotionToCurrentInvoice", string.Format("objConsultaEstadoCuenta: Code: {0}, Msg: {1}", objConsultaEstadoCuenta.audit.errorCode, objConsultaEstadoCuenta.audit.errorMsg));
                        }

                    }
                    else
                    {
                        strMensajeValidador = ConfigurationManager.AppSettings("strErrorConsulEstCuenta");
                        PermiteCheck = false;
                        Claro.Web.Logging.Error(strIdSession, "GetValidationOfPromotionToCurrentInvoice", "Error en la devolucion de datos por parte del Servicio, objConsultaEstadoCuenta nulo");
                    }
                }

            }
            catch (Exception ex)
            {
                PermiteCheck = false; strMensajeValidador = ConfigurationManager.AppSettings("strErrorWS");
                Claro.Web.Logging.Error(strIdSession, strTransaccion, "Método : GetValidationOfPromotionToCurrentInvoice FIN, " + " PermiteCheck : " + PermiteCheck.ToString() + ", strMensajeValidador : " + strMensajeValidador + ", Error: " + ex.Message + ", Error largo:" + ex.ToString());
            }

            var data = new { validacion = PermiteCheck, mensaje = strMensajeValidador };

            Claro.Web.Logging.Info(strIdSession, "Método : GetValidationOfPromotionToCurrentInvoice FIN", string.Format(" los valores del json result son: data.validacion {0}, data.mensaje {1}", PermiteCheck.ToString(), strMensajeValidador));
            return Json(data);
        }

        public JsonResult getCurrentDiscount(string strIdSession)
        {
            return Json(new { data = "WS: El cliente ya cuenta con el descuento de Tipo de descuento (cargo fijo/Servicio Adicional) % por Nro. meses desde el día dd/mm/AA" });
        }

        /// <summary>
        /// Metodo que obtiene el objsiteid del cliente
        /// </summary>
        /// <returns>Metodo que obtiene el objsiteid del cliente</returns>
        public void setIdContactoSide(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var oCustomer = new GetCustomerRequest()
            {
                audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession),
                vPhone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId,
            };
            var customerResp = GetCustomerData(oCustomer, oModel.IdSession);
            if (customerResp.Customer != null)
            {
                oModel.objIdContacto = customerResp.Customer.ContactCode;
                oModel.objIdSite = customerResp.Customer.SiteCode;
            }
        }

        public int ToInt(bool value)
        {
            return value ? 1 : 0;
        }


        public Model.InteractionModel CargarTificacion(string IdSession, string CodeTipification)
        {

            var objInteraction = new Model.InteractionModel();

            Claro.Web.Logging.Info(IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification);
            try
            {

                var tipification = GetTypificationHFC(IdSession, CodeTipification);

                if (tipification != null)
                {
                    tipification.ToList().ForEach(x =>
                    {
                        objInteraction.Type = x.Type;
                        objInteraction.Class = x.Class;
                        objInteraction.SubClass = x.SubClass;
                        objInteraction.InteractionCode = x.InteractionCode;
                        objInteraction.TypeCode = x.TypeCode;
                        objInteraction.ClassCode = x.ClassCode;
                        objInteraction.SubClassCode = x.SubClassCode;
                        objInteraction.FlagCase = Claro.SIACU.Transac.Service.Constants.CriterioMensajeOK;
                        Claro.Web.Logging.Info(IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "FlagCase : " + objInteraction.FlagCase);
                    });
                }
                else
                {
                    objInteraction.Result = Claro.SIACU.Transac.Service.Constants.ADDITIONALSERVICESPOSTPAID.strNotTypification;
                    objInteraction.FlagCase = Claro.SIACU.Transac.Service.Constants.DAReclamDatosVariableNO_OK;
                    Claro.Web.Logging.Info(IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "Result : " + objInteraction.Result);
                    Claro.Web.Logging.Info(IdSession, "Método : CargarTificacion Inicio ", "CodeTipification : " + CodeTipification + "FlagCase : " + objInteraction.FlagCase);

                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(IdSession, "Método : CargarTificacion ", ex.Message);
            }

            Claro.Web.Logging.Info(IdSession, "Método : CargarTificacion Fín", "CodeTipification : " + CodeTipification + "FlagCase :" + objInteraction.FlagCase);
            return objInteraction;
        }

        public Model.InteractionModel DatosInteraccion(string codeTipification, Model.HFC.RetentionCancelServicesModel oModel) // Retenido & No Retenido
        {
            var oInteraccion = new Model.InteractionModel();
            var objInteraction = new Model.InteractionModel();
            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(oModel.IdSession);
            GetCustomerRequest objGetCustomerRequest = new GetCustomerRequest();

            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin DatosInteraccion");
            try
            {
                // Get Datos de la Tipificacion
                objInteraction = CargarTificacion(oModel.IdSession, codeTipification);
                var strNroTelephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;

                oInteraccion.ObjidContacto = oModel.objIdContacto; // GetCustomer(strNroTelephone, oModel.IdSession);  //Get Customer = strObjId
                oInteraccion.DateCreaction = Convert.ToString(DateTime.Now);
                oInteraccion.Telephone = ConfigurationManager.AppSettings("gConstKeyCustomerInteract") + oModel.CustomerId;
                oInteraccion.Type = objInteraction.Type;
                oInteraccion.Class = objInteraction.Class;
                oInteraccion.SubClass = objInteraction.SubClass;
                oInteraccion.TypeInter = ConfigurationManager.AppSettings("AtencionDefault");
                oInteraccion.Method = ConfigurationManager.AppSettings("MetodoContactoTelefonoDefault");
                oInteraccion.Result = ConfigurationManager.AppSettings("Ninguno");
                oInteraccion.MadeOne = Claro.SIACU.Transac.Service.Constants.strCero;
                oInteraccion.Note = oModel.Note;
                oInteraccion.Contract = oModel.ContractId;
                oInteraccion.Plan = oModel.Plan;
                oInteraccion.FlagCase = Claro.SIACU.Transac.Service.Constants.strCero;
                oInteraccion.UserProces = ConfigurationManager.AppSettings("USRProcesoSU");
                oInteraccion.Agenth = oModel.CurrentUser;
                oInteraccion.ObjidSite = oModel.objIdSite;
                oInteraccion.Cuenta = oModel.Cuenta;
            }
            catch (Exception ex)
            {
                Logging.Error(oModel.IdSession, audit.transaction, ex.Message);

            }

            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "End DatosInteraccion");
            return oInteraccion;
        }

        /// <summary>
        /// Metodo que realiza el registro de los bonos para Cargo Fijo y Servicios Adicionales
        /// </summary>
        /// <returns>Metodo que realiza el registro de los bonos para Cargo Fijo y Servicios Adicionales</returns>
        public bool RegisterBonoDiscount(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var result = false;
            RegisterBonoDiscountRequest request = new RegisterBonoDiscountRequest();
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            try
            {
                request = new RegisterBonoDiscountRequest()
                {
                    pi_co_id = Convert.ToInt(oModel.idContrato),
                    pi_id_campana = Convert.ToInt(oModel.idCampana),
                    pi_porcentaje = Convert.ToInt(oModel.idPorcentaje),
                    //pi_monto = Convert.ToDouble(oModel.montoTotalSA),
                    pi_monto = Convert.ToDouble(oModel.MontoDescuento),
                    pi_periodo = Convert.ToInt(oModel.mesVal),
                    pi_sncode = Convert.ToInt(oModel.snCode),
                    pi_costo_inst = (!String.IsNullOrEmpty(oModel.costoWSInst) ? Convert.ToDouble(String.Format("{0:0.00}", Double.Parse(oModel.costoWSInst))) : 0),
                    pi_flag = Convert.ToInt(oModel.flagCargFijoServAdic),
                };

                request.audit = audit;
                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin RegisterBonoDiscount");

                result = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.RegisterBonoDiscount(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, request.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format("End RegisterBonoDiscount result: {0}", result));
            return result;
        }

        /// <summary>
        /// Metodo que realiza el registro de los bonos para Cargo Fijo y Servicios Adicionales
        /// </summary>
        /// <returns>Metodo que realiza el registro de los bonos para Cargo Fijo y Servicios Adicionales</returns>
        public string RegisterActiDesaBonoDesc(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var response = new RegisterActiDesaBonoDesc.BodyResponse();
            var result = "";
            var codigoRespuesta = "";
            var mensajeRespuesta = "";

            RegisterActiDesaBonoDesc.Request1 request = new RegisterActiDesaBonoDesc.Request1();
            Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest>(oModel.IdSession);
            try
            {
                RegisterActiDesaBonoDesc.registrarDescHFCRequest HFCRequest = null;
                RegisterActiDesaBonoDesc.registrarDescLTERequest LTERequest = null;
                if (oModel.typeHFCLTE == Model.HFC.typeHFCLTE.HFC)
                {
                    Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin ObtenerRequestHFC");
                    HFCRequest = ObtenerRequestHFC(oModel, audit);
                }
                else
                {
                    Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin ObtenerRequestLTE");
                    LTERequest = ObtenerRequestLTE(oModel, audit);
                }


                request = new RegisterActiDesaBonoDesc.Request1()
                {
                    audit = audit,
                    MessageRequest = new RegisterActiDesaBonoDesc.MessageRequest()
                    {
                        Header = new RegisterActiDesaBonoDesc.HeadersRequest()
                        {
                            HeaderRequest = new RegisterActiDesaBonoDesc.HeaderRequestADB()
                            {
                                country = KEY.AppSettings("country"),
                                language = KEY.AppSettings("country"),
                                consumer = KEY.AppSettings("consumer"),
                                system = KEY.AppSettings("system"),
                                modulo = KEY.AppSettings("modulo"),
                                pid = DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                                userId = App_Code.Common.CurrentUser,
                                dispositivo = App_Code.Common.GetClientIP(),
                                wsIp = App_Code.Common.GetApplicationIp(),
                                operation = KEY.AppSettings("ValidaEstadoIMEI"),
                                timestamp = DateTime.Now.ToString(),
                                msgType = KEY.AppSettings("msgType"),
                                VarArg = KEY.AppSettings("VarArg")
                            }
                        },
                        Body = new RegisterActiDesaBonoDesc.BodyRequest()
                        {
                            registrarDescHFCRequest = HFCRequest,
                            registrarDescLTERequest = LTERequest
                        }
                    }
                };

                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction,
                                oModel.typeHFCLTE == Model.HFC.typeHFCLTE.HFC ? "Begin RegisterActiDesaBonoDescHFC" : "Begin RegisterActiDesaBonoDescLTE");
                response = Claro.Web.Logging.ExecuteMethod<RegisterActiDesaBonoDesc.BodyResponse>(() =>
                {
                    return oModel.typeHFCLTE == Model.HFC.typeHFCLTE.HFC ?
                                _oServiceFixed.RegisterActiDesaBonoDescHFC(request)
                                : _oServiceFixed.registrarDescLTE(request);
                });

                if (oModel.typeHFCLTE == Model.HFC.typeHFCLTE.HFC)
                {
                    if (response.registrarDescHFCResponse.auditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                    {
                        codigoRespuesta = Claro.Constants.NumberZeroString;
                        result = codigoRespuesta;
                    }
                    else
                    {
                        mensajeRespuesta = response.registrarDescHFCResponse.auditResponse.mensajeRespuesta;
                        result = mensajeRespuesta;
                    }
                }
                else
                {
                    if (response.registrarDescLTEResponse.auditResponse.codigoRespuesta == Claro.Constants.NumberZeroString)
                    {
                        codigoRespuesta = Claro.Constants.NumberZeroString;
                        result = codigoRespuesta;
                    }
                    else
                    {
                        mensajeRespuesta = response.registrarDescLTEResponse.auditResponse.mensajeRespuesta;
                        result = mensajeRespuesta;
                    }
                }

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format(oModel.typeHFCLTE == Model.HFC.typeHFCLTE.HFC ?
                "End RegisterActiDesaBonoDescHFC - result: {0}, Descrip:{1}" : "End RegisterActiDesaBonoDescLTE result: {0}, Descrip: {1}", codigoRespuesta, mensajeRespuesta));
            return result;
        }

        public RegisterActiDesaBonoDesc.registrarDescHFCRequest ObtenerRequestHFC(Model.HFC.RetentionCancelServicesModel oModel, Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest audit)
        {

            List<RegisterActiDesaBonoDesc.listaRequestOpcional1> lista = new List<RegisterActiDesaBonoDesc.listaRequestOpcional1>();

            if (!string.IsNullOrEmpty(oModel.RazonSocial) && oModel.RazonSocial.Length > 20)
            {
                oModel.RazonSocial = oModel.RazonSocial.Substring(0, 20);
            }

            RegisterActiDesaBonoDesc.listaRequestOpcional1 obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "dniRuc",
                valor = oModel.DNI_RUC
            };
            lista.Add(obj);
            obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "razonSocial",
                valor = oModel.RazonSocial
            };
            lista.Add(obj);

            obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "EnvioCorreo",
                valor = (oModel.Flag_Email) ? "SI" : "NO"
            };
            lista.Add(obj);

            obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "COD_PLANO",
                valor = oModel.Plan
            };
            lista.Add(obj);

            RegisterActiDesaBonoDesc.registrarDescHFCRequest result;
            result = new RegisterActiDesaBonoDesc.registrarDescHFCRequest()
            {
                auditRequest = new RegisterActiDesaBonoDesc.auditRequest2()
                {
                    idTransaccion = audit.transaction,
                    ipAplicacion = audit.ipAddress,
                    nombreAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                    usuarioAplicacion = ConfigurationManager.AppSettings("USRProcesoSU")
                },
                codigoAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                fechaProgramacion = DateTime.Now.AddMonths(Convert.ToInt(oModel.mesVal)).ToString("yyyy-MM-dd"),
                coId = oModel.idContrato,
                codCliente = oModel.CustomerId,
                DesServicioPVU = oModel.desServicioPVU,
                telefono = (oModel.Telephone == null) ? "" : oModel.Telephone,
                coSer = oModel.codServAdic,
                emailUsuarioApp = oModel.emailUsuario,
                desCoSer = oModel.descServAdic,
                nroCuenta = oModel.Cuenta,
                cicloFac = oModel.BillingCycle,
                aplicaDesc = "1",
                objId = oModel.objIdContacto,
                siteObjId = oModel.objIdSite,
                idCampana = oModel.idCampana.ToString(),
                //MontoDesc = Convert.ToDouble(oModel.montTariRete).ToString(),
                MontoDesc = Convert.ToDouble(oModel.MontoDescuento).ToString(),
                PeriodoDesc = Convert.ToDouble(oModel.mesVal).ToString(),
                FlagDesc = "1",
                SnCode = Convert.ToInt(oModel.snCode).ToString(),
                notas = (oModel.Note == null) ? "" : oModel.Note,
                destinatarioCorreo = oModel.Destinatarios,
                codigoEmpleado = oModel.CurrentUser,
                codigoSistema = ConfigurationManager.AppSettings("USRProcesoSU"),
                NroDocumento = oModel.NroDoc,
                PlanCliente = oModel.planContract,
                NomCliente = oModel.name,
                ApeCliente = oModel.LastName,
                DescDAC = oModel.DescCacDac,
                DescTipoDoc = oModel.TypeDoc,
                TipoCliente = oModel.TypeClient,
                NomClienteCompleto = oModel.NameComplet,
                NroCliente = oModel.TelefonoReferencia,
                NomResponsableLegal = oModel.RepresentLegal,
                CostoServiciosinIGV = oModel.costoServiciosinIGV, /*string.Format("{0:0.00}", Double.Parse(oModel.costoServiciosinIGV)).Replace(".", ","),*/
                CostoServicioconIGV = oModel.costoServicioconIGV,
                listaRequestOpcional = lista

            };
            return result;
        }


        public RegisterActiDesaBonoDesc.registrarDescLTERequest ObtenerRequestLTE(Model.HFC.RetentionCancelServicesModel oModel, Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRequest audit)
        {

            List<RegisterActiDesaBonoDesc.listaRequestOpcional1> lista = new List<RegisterActiDesaBonoDesc.listaRequestOpcional1>();

            if (!string.IsNullOrEmpty(oModel.RazonSocial) && oModel.RazonSocial.Length > 20)
            {
                oModel.RazonSocial = oModel.RazonSocial.Substring(0, 20);
            }

            RegisterActiDesaBonoDesc.listaRequestOpcional1 obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "dniRuc",
                valor = oModel.DNI_RUC
            };
            lista.Add(obj);
            obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "razonSocial",
                valor = oModel.RazonSocial
            };
            lista.Add(obj);

            obj = new RegisterActiDesaBonoDesc.listaRequestOpcional1()
            {
                campo = "EnvioCorreo",
                valor = (oModel.Flag_Email) ? "SI" : "NO"
            };
            lista.Add(obj);

            RegisterActiDesaBonoDesc.registrarDescLTERequest result;
            result = new RegisterActiDesaBonoDesc.registrarDescLTERequest()
            {
                auditRequest = new RegisterActiDesaBonoDesc.auditRequest2()
                {
                    idTransaccion = audit.transaction,
                    ipAplicacion = audit.ipAddress,
                    nombreAplicacion = ConfigurationManager.AppSettings("ApplicationName"),
                    usuarioAplicacion = ConfigurationManager.AppSettings("USRProcesoSU")
                },
                fechaProgramacion = DateTime.Now.AddMonths(Convert.ToInt(oModel.mesVal)).ToString("yyyy-MM-dd"),
                customerId = oModel.CustomerId,
                coId = oModel.idContrato,
                msisdn = oModel.Msisdn,
                coSer = oModel.codServAdic,
                desCoSer = oModel.descServAdic,
                nroCuenta = oModel.Cuenta,
                cicloFac = oModel.BillingCycle,
                aplicaDesc = "1",
                objId = oModel.objIdContacto,
                siteObjId = oModel.objIdSite,
                idCampana = oModel.idCampana.ToString(),
                //MontoDesc = Convert.ToDouble(oModel.montTariRete).ToString(),
                MontoDesc = Convert.ToDouble(oModel.MontoDescuento).ToString(),
                PeriodoDesc = Convert.ToDouble(oModel.mesVal).ToString(),
                FlagDesc = "1",
                SnCode = Convert.ToInt(oModel.snCode).ToString(),
                notas = (oModel.Note == null) ? "" : oModel.Note,
                destinatarioCorreo = oModel.Destinatarios,
                codigoEmpleado = oModel.CurrentUser,
                codigoSistema = ConfigurationManager.AppSettings("USRProcesoSU"),
                NroDocumento = oModel.NroDoc,
                PlanCliente = oModel.planContract == null ? "" : oModel.planContract,
                NomCliente = oModel.name,
                ApeCliente = oModel.LastName,
                DescDAC = oModel.DescCacDac,
                DescTipoDoc = oModel.TypeDoc,
                TipoCliente = oModel.TypeClient,
                NomClienteCompleto = oModel.NameComplet,
                NroCliente = oModel.TelefonoReferencia,
                NomResponsableLegal = oModel.RepresentLegal,
                CostoServiciosinIGV = oModel.costoServiciosinIGV, /*string.Format("{0:0.00}", Double.Parse(oModel.costoServiciosinIGV)).Replace(".", ","),*/
                CostoServicioconIGV = oModel.costoServicioconIGV,
                listaRequestOpcional = lista

            };
            return result;
        }
        /// <summary>
        /// Metodo que realiza la actualizacion en BSCS y Clarify
        /// </summary>
        /// <returns>Metodo que realiza la actualizacion en BSCS y Clarify</returns>
        public bool ActualizarDatosMenores(Model.HFC.RetentionCancelServicesModel oModel, out string message)
        {
            message = "Error no registro: ";
            var bDM = UpdateDatosMenores(oModel);
            var bDC = ActualizarDatosClarify(oModel);
            if (!bDM)
                message += "UpdateDatosMenores, ";
            if (!bDC)
                message += "ActualizarDatosClarify, ";


            var resul = ToInt(bDM) + ToInt(bDC);
            if (resul > 0)
            {
                message = (resul == 2) ? "" : message;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Tipifica la Retencion y la fidelizacion de las nuevas acciones
        /// </summary>
        /// <returns>Tipifica la Retencion y la fidelizacion de las nuevas acciones</returns>
        public string TipificarRetencionFidelizacion(string idSession, Model.InteractionModel oInteraccion, Model.HFC.RetentionCancelServicesModel oModel)
        {
            var result = "";
            var idInteraction = RegisterNuevaInteraccion(idSession, oInteraccion, oModel.typeRETEFIDE.ToString());
            if (!string.IsNullOrEmpty(idInteraction) && !string.IsNullOrEmpty(RegisterNuevaInteraccionPlus(oModel, idInteraction)))
            {
                result = idInteraction;
            }
            return result;
        }

        /// <summary>
        /// Tipifica la actualización de datos menores del cliente
        /// </summary>
        /// <param name="codeTipification">Codigo de la interaccion de datos menores</param>
        /// <returns>Tipifica la actualización de datos menores del cliente</returns>
        public bool TipificarDatosMenores(string codeTipification, Model.HFC.RetentionCancelServicesModel oModel)
        {
            var result = false;
            var oInteraccion = DatosInteraccion(codeTipification, oModel);
            var idInteraction = RegisterNuevaInteraccion(oModel.IdSession, oInteraccion, "MENO");
            if (!string.IsNullOrEmpty(idInteraction) && !string.IsNullOrEmpty(RegisterNuevaInteraccionPlus(oModel, idInteraction)))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Actualiza datos menores del cliente
        /// </summary>
        /// <returns>Actualiza datos menores del cliente</returns>
        public bool UpdateDatosMenores(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var result = false;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            var item = new Customer()
            {
                audit = audit,
                CustomerID = oModel.CustomerId,
                Position = oModel.cargo,
                PhoneReference = oModel.TelefonoReferencia,
                Telephone = oModel.Telephone,
                Fax = oModel.fax,
                Email = oModel.Email,
                Tradename = oModel.RazonSocial,
                CustomerContact = oModel.contactoCliente,
                BirthDate = oModel.fechaNac,
                BirthPlaceID = oModel.idLugarNac,
                Sex = oModel.sexo,
                CivilStatusID = oModel.idEstadoCivil
            };
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin UpdateDatosMenores");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.ActualizarDatosMenores(item);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format("End UpdateDatosMenores result: {0}", result));
            return result;
        }

        /// <summary>
        /// Actualiza datos menores del cliente
        /// </summary>
        /// <returns>Actualiza datos menores del cliente</returns>
        public bool ActualizarDatosClarify(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var result = false;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            var item = new Customer()
            {
                audit = audit,
                ContactCode = oModel.objIdContacto,
                PhoneReference = oModel.TelefonoReferencia,
                Fax = oModel.fax,
                Email = oModel.Email,
                BirthDate = oModel.fechaNac,
                Sex = oModel.sexo,
                CivilStatus = oModel.estadoCivil,
                Position = oModel.cargo,
                Tradename = oModel.RazonSocial,
                CustomerContact = oModel.contactoCliente,
                BirthPlace = oModel.lugarNac
            };

            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin ActualizarDatosClarify");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return _oServiceFixed.ActualizarDatosClarify(item);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format("End ActualizarDatosClarify result: {0}", result));
            return result;
        }

        /// <summary>
        /// Registra la interaccion de las nuevas acciones
        /// </summary>
        /// <param name="tipo">Numero de telefono del cliente</param>
        /// <returns>Registra la interaccion de las nuevas acciones</returns>
        public string RegisterNuevaInteraccion(string idSession, Model.InteractionModel oInteraccion, string tipo)
        {
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession);

            Claro.Web.Logging.Info(idSession, audit.transaction, "Begin RegisterNuevaInteraccion " + tipo);

            CommonTransacService.InsertInteractHFCRequest objInteractHFCRequest = new CommonTransacService.InsertInteractHFCRequest()
            {
                audit = audit,
                Interaction = new CommonService.Interaction()
            {
                    OBJID_CONTACTO = oInteraccion.ObjidContacto,
                    OBJID_SITE = string.IsNullOrEmpty(oInteraccion.ObjidSite) ? "" : oInteraccion.ObjidSite,
                    CUENTA = "",
                    FECHA_CREACION = DateTime.Now.ToString("MM/dd/yyyy"),
                    TELEFONO = oInteraccion.Telephone,
                    TIPO = oInteraccion.Type,
                    CLASE = oInteraccion.Class,
                    SUBCLASE = oInteraccion.SubClass,
                    TIPO_INTER = oInteraccion.TypeInter,
                    METODO = oInteraccion.Method,
                    RESULTADO = oInteraccion.Result, 
                    HECHO_EN_UNO = oInteraccion.MadeOne,
                    AGENTE = oInteraccion.Agenth,
                    NOTAS = (string.IsNullOrEmpty(oInteraccion.Note) ? "" : oInteraccion.Note),
                    FLAG_CASO = oInteraccion.FlagCase,
                    USUARIO_PROCESO = ConfigurationManager.AppSettings("USRProcesoSU"),               
                    TIPO_INTERACCION = oInteraccion.TypeInter,
                    CONTRATO = oInteraccion.Contract,
                    PLANO = oInteraccion.Plan
                }
            };

            CommonTransacService.InsertInteractHFCResponse objInteractHFCResponse = null;

            try
            {
                objInteractHFCResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.InsertInteractHFCResponse>(() =>
                {
                    return _oServiceCommon.GetInsertInteractHFC(objInteractHFCRequest);
                });
                result = objInteractHFCResponse.rInteraccionId;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(idSession, objInteractHFCRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(idSession, audit.transaction, string.Format("End RegisterNuevaInteraccion {0} - result: {1}", tipo, result));
            return result;
        }

        /// <summary>
        /// Verifica el estado de la cuenta
        /// </summary>
        /// <param name="idInteraction">Numero de telefono del cliente</param>
        /// <returns>Verifica el estado de la cuenta</returns>
        public string RegisterNuevaInteraccionPlus(Model.HFC.RetentionCancelServicesModel oModel, string idInteraction)
        {
            if (string.IsNullOrEmpty(oModel.RegularBonusServAdic))
            {
                oModel.RegularBonusServAdic = "0";
            }


            var template = generateTemplateInteractionCFSA(oModel);
            var result = "";
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);
            var txId = audit.transaction;
            var dateBegin = new DateTime(1, 1, 1);
            var interaccionPlus = new InteraccionPlus()
            {
                p_nro_interaccion = idInteraction,
                p_inter_1 = template._X_INTER_1,
                p_inter_2 = template._X_INTER_2,
                p_inter_3 = template._X_INTER_3,
                p_inter_4 = oModel.BonoRetentionFidelizacion,
                p_inter_5 = oModel.VigenciaRetFid,
                p_inter_6 = oModel.DiscountDescription,
                p_inter_7 = oModel.mesDesc,
                p_inter_8 = oModel.bonoId,//template._X_INTER_8.ToString(),
                p_inter_9 = template._X_INTER_9.ToString(),
                p_inter_10 = template._X_INTER_10.ToString(),
                p_inter_11 = template._X_INTER_11.ToString(),
                p_inter_12 = template._X_INTER_12.ToString(),
                p_inter_13 = template._X_INTER_13.ToString(),
                p_inter_14 = template._X_INTER_14.ToString(),
                p_inter_15 = template._X_INTER_15,
                p_inter_16 = template._X_INTER_16,
                p_inter_17 = oModel.descServAdic,
                p_inter_18 = template._X_INTER_18,
                p_inter_19 = oModel.vMotiveSot, //aqui va el motivo sot.
                p_inter_20 = template._X_INTER_20,
                p_inter_21 = template._X_INTER_21,
                p_inter_22 = template._X_INTER_22.ToString(),
                p_inter_23 = template._X_INTER_23.ToString(),
                p_inter_24 = template._X_INTER_24.ToString(),
                p_inter_25 = template._X_INTER_25.ToString(),
                p_inter_26 = template._X_INTER_26.ToString(),
                p_inter_27 = template._X_INTER_27.ToString(),
                p_inter_28 = template._X_INTER_28.ToString(),
                p_inter_29 = template._X_INTER_29,
                p_inter_30 = template._X_INTER_30,
                p_plus_inter2interact = template._X_PLUS_INTER2INTERACT.ToString(),
                p_adjustment_amount = template._X_ADJUSTMENT_AMOUNT.ToString(),
                p_adjustment_reason = template._X_ADJUSTMENT_REASON,
                p_address = template._X_ADDRESS,
                p_amount_unit = template._X_AMOUNT_UNIT,
                p_birthday = template._X_BIRTHDAY != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_BIRTHDAY)) : "",
                p_clarify_interaction = template._X_CLARIFY_INTERACTION,
                p_claro_ldn1 = template._X_CLARO_LDN1,
                p_claro_ldn2 = template._X_CLARO_LDN2,
                p_claro_ldn3 = template._X_CLARO_LDN3,
                p_claro_ldn4 = template._X_CLARO_LDN4,
                p_clarolocal1 = template._X_CLAROLOCAL1,
                p_clarolocal2 = ((!String.IsNullOrEmpty(oModel.costInst)) ? String.Format("{0:0.00}", Double.Parse(oModel.costInst)) : template._X_CLAROLOCAL2),
                p_clarolocal3 = oModel.SOT, //oModel.sot, aqui va el numero de sot devuelto por el  ws
                p_clarolocal4 = oModel.DiscountDescription,
                p_clarolocal5 = template._X_CLAROLOCAL5,
                p_clarolocal6 = template._X_CLAROLOCAL6,
                p_contact_phone = template._X_CONTACT_PHONE,
                p_dni_legal_rep = template._X_DNI_LEGAL_REP,
                p_document_number = template._X_DOCUMENT_NUMBER,
                p_email = template._X_EMAIL,
                p_first_name = template._X_FIRST_NAME,
                p_fixed_number = template._X_FIXED_NUMBER,
                p_flag_change_user = template._X_FLAG_CHANGE_USER,
                p_flag_legal_rep = oModel.CodTypeClient,
                p_flag_other = template._X_FLAG_OTHER,
                p_flag_titular = template._X_FLAG_TITULAR,
                p_imei = template._X_IMEI,
                p_last_name = template._X_LAST_NAME,
                p_lastname_rep = template._X_LASTNAME_REP,
                p_ldi_number = template._X_LDI_NUMBER,
                p_name_legal_rep = template._X_NAME_LEGAL_REP,
                p_old_claro_ldn1 = template._X_OLD_CLARO_LDN1,
                p_old_claro_ldn2 = template._X_OLD_CLARO_LDN2,
                p_old_claro_ldn3 = template._X_OLD_CLARO_LDN3,
                p_old_claro_ldn4 = template._X_OLD_CLARO_LDN4,
                p_old_clarolocal1 = template._X_OLD_CLAROLOCAL1,
                p_old_clarolocal2 = template._X_OLD_CLAROLOCAL2,
                p_old_clarolocal3 = template._X_OLD_CLAROLOCAL3,
                p_old_clarolocal4 = template._X_OLD_CLAROLOCAL4,
                p_old_clarolocal5 = template._X_OLD_CLAROLOCAL5,
                p_old_clarolocal6 = template._X_OLD_CLAROLOCAL6,
                p_old_doc_number = template._X_OLD_DOC_NUMBER,
                p_old_first_name = template._X_OLD_FIRST_NAME,
                p_old_fixed_phone = template._X_OLD_FIXED_PHONE,
                p_old_last_name = template._X_OLD_LAST_NAME,
                p_old_ldi_number = template._X_OLD_LDI_NUMBER,
                p_old_fixed_number = template._X_OLD_FIXED_NUMBER,
                p_operation_type = template._X_OPERATION_TYPE,
                p_other_doc_number = template._X_OTHER_DOC_NUMBER,
                p_other_first_name = template._X_OTHER_FIRST_NAME,
                p_other_last_name = template._X_OTHER_LAST_NAME,
                p_other_phone = template._X_OTHER_PHONE,
                p_phone_legal_rep = template._X_PHONE_LEGAL_REP,
                p_reference_phone = template._X_REFERENCE_PHONE,
                p_reason = template._X_REASON,
                p_model = template._X_MODEL,
                p_lot_code = template._X_LOT_CODE,
                p_flag_registered = template._X_FLAG_REGISTERED,
                p_registration_reason = template._X_REGISTRATION_REASON,
                p_claro_number = template._X_CLARO_NUMBER,
                p_month = template._X_MONTH,
                p_ost_number = template._X_OST_NUMBER,
                p_basket = template._X_BASKET,
                p_expire_date = template._X_EXPIRE_DATE != dateBegin ? String.Format("{0:MM-dd-yyyy}", Functions.CheckDate(template._X_EXPIRE_DATE)) : "",
                p_ADDRESS5 = template._X_ADDRESS5,
                p_CHARGE_AMOUNT = oModel.RetentionBonusServAdic,/// oModel.RegularBonusServAdic,
                p_CITY = template._X_CITY,
                p_CONTACT_SEX = template._X_CONTACT_SEX,
                p_DEPARTMENT = template._X_DEPARTMENT,
                p_DISTRICT = template._X_DISTRICT,
                p_EMAIL_CONFIRMATION = template._X_EMAIL_CONFIRMATION,
                p_FAX = template._X_FAX,
                p_FLAG_CHARGE = template._X_FLAG_CHARGE,
                p_MARITAL_STATUS = template._X_MARITAL_STATUS,
                p_OCCUPATION = template._X_OCCUPATION,
                p_POSITION = template._X_POSITION,
                p_REFERENCE_ADDRESS = template._X_REFERENCE_ADDRESS,
                p_TYPE_DOCUMENT = template._X_TYPE_DOCUMENT,
                p_ZIPCODE = template._X_ZIPCODE,
                p_iccid = template._X_ICCID,
            };
            var request = new RegisterNuevaInteraccionPlusRequest()
            {
                interaccionPlus = interaccionPlus,
                txId = txId,
                audit = audit,
            };
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin RegisterNuevaInteraccionPlus");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<string>(() =>
                {
                    return _oServiceFixed.RegisterNuevaInteraccionPlus(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, request.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, string.Format("End RegisterNuevaInteraccionPlus result: {0}", result));
            return result;
        }

        /// <summary>
        /// Genera plantilla de las interacciones para las nuvas acciones
        /// </summary>
        /// <returns>Genera plantilla de las interacciones para las nuvas acciones</returns>
        public CommonTransacService.InsertTemplateInteraction generateTemplateInteractionCFSA(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var template = new CommonTransacService.InsertTemplateInteraction();
            try
            {
                template = InsertPlantInteractionCFSA(GetDataTemplateInteractionCFSA(oModel));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, "generateTemplateInteractionCFSA", ex.Message);
            }
            return template;
        }

        /// <summary>
        /// Devuelve la plantilla de la interaccion de las nuevas acciones
        /// </summary>
        /// <returns>Devuelve la plantilla de la interaccion de las nuevas acciones</returns>
        public CommonTransacService.InsertTemplateInteraction InsertPlantInteractionCFSA(TemplateInteractionModel objInteractionTemplateModel)
        {
            var serviceModelInteraction = Mapper.Map<CommonTransacService.InsertTemplateInteraction>(objInteractionTemplateModel);
            return serviceModelInteraction;
        }

        /// <summary>
        /// Genera la plantilla de la interaccion para las nuevas acciones
        /// </summary>
        /// <returns>Genera la plantilla de la interaccion para las nuevas acciones</returns>
        public Model.TemplateInteractionModel GetDataTemplateInteractionCFSA(Model.HFC.RetentionCancelServicesModel oModel)
        {
            var oPlantCampDat = new Model.TemplateInteractionModel();
            FixedTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(oModel.IdSession);

            try
            {
                Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "Begin GetDataTemplateInteraction");

                oPlantCampDat.X_INTER_8 = (oModel.Reintegro == string.Empty ? CONSTANTS.numeroCero : Convert.ToDouble(oModel.Reintegro));
                oPlantCampDat.X_INTER_9 = Convert.ToDouble(CONSTANTS.strCero);
                oPlantCampDat.X_CHARGE_AMOUNT = oPlantCampDat.X_INTER_8;
                oPlantCampDat.X_OPERATION_TYPE = oModel.DesMotivos;
                oPlantCampDat.X_REGISTRATION_REASON = oModel.DesAccion;
                oPlantCampDat.X_FLAG_OTHER = (oModel.hidSupJef == CONSTANTS.gstrVariableS ? CONSTANTS.numeroUno.ToString() : CONSTANTS.numeroCero.ToString());
                oPlantCampDat.X_EXPIRE_DATE = Convert.ToDate(oModel.FechaCompromiso);
                oPlantCampDat.X_FIXED_NUMBER = string.Empty;
                oPlantCampDat.X_CLARO_NUMBER = oModel.NroCelular;
                oPlantCampDat.X_REASON = oModel.Accion;
                // INICIO - PROY-140319
                oPlantCampDat.X_INTER_4 = oModel.BonoRetentionFidelizacion;
                oPlantCampDat.X_INTER_5 = oModel.VigenciaRetFid;
                // FIN - PROY-140319
                oPlantCampDat.X_INTER_16 = oModel.DesSubMotivo;
                oPlantCampDat.X_INTER_15 = oModel.DescCacDac;
                oPlantCampDat.X_ADJUSTMENT_AMOUNT = (oModel.TotalInversion == string.Empty ? CONSTANTS.numeroCero : Convert.ToDouble(oModel.TotalInversion));

                if (oPlantCampDat.X_REASON == CONSTANTS.gstrNoRetenido)
                {
                    oPlantCampDat.X_FLAG_REGISTERED = oModel.PagoAPADECE;
                    oPlantCampDat.X_MODEL = string.Empty;

                }

                oPlantCampDat.X_ZIPCODE = oModel.NroCelular;
                oPlantCampDat.X_INTER_18 = string.Empty;


                if (oModel.TypeClient.ToUpper().Equals("CONSUMER"))
                {
                    oPlantCampDat.X_NAME_LEGAL_REP = string.Empty;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.NroDoc;

                }
                else
                {
                    oPlantCampDat.X_NAME_LEGAL_REP = oModel.RepresentLegal;
                    oPlantCampDat.X_OLD_LAST_NAME = oModel.DNI_RUC;

                }

                oPlantCampDat.X_LASTNAME_REP = oModel.TypeDoc.ToUpper();
                oPlantCampDat.X_PHONE_LEGAL_REP = oModel.TelefonoReferencia;
                oPlantCampDat.X_FLAG_LEGAL_REP = oModel.TypeClient;
                oPlantCampDat.X_ADDRESS = oModel.AdressDespatch;
                oPlantCampDat.X_INTER_1 = oModel.Reference;
                oPlantCampDat.X_DEPARTMENT = oModel.Departament_Fact;
                oPlantCampDat.X_DISTRICT = oModel.District_Fac;
                oPlantCampDat.X_INTER_2 = oModel.Pais_Fac;
                oPlantCampDat.X_INTER_3 = oModel.Provincia_Fac;
                oPlantCampDat.X_INTER_20 = CONSTANTS.strCero;


                oPlantCampDat.X_CLARO_LDN1 = oModel.Flag_Email == true ? CONSTANTS.strUno : CONSTANTS.strCero; //Validar

                if (oPlantCampDat.X_CLARO_LDN1 == CONSTANTS.strUno)
                {
                    oPlantCampDat.X_INTER_29 = oModel.Email;
                }
                else
                {
                    oPlantCampDat.X_INTER_29 = string.Empty;
                }

                oPlantCampDat.X_CLAROLOCAL1 = oModel.PaqueteODeco;

            }
            catch (Exception ex)
            {

                Claro.Web.Logging.Error(oModel.IdSession, audit.transaction, ex.Message);
            }

            Claro.Web.Logging.Info(oModel.IdSession, audit.transaction, "End GetDataTemplateInteraction");
            return oPlantCampDat;

        }


        /// <summary>
        /// Verifica el estado de la cuenta
        /// </summary>
        /// <param name="strCliNroCuenta">numero de cuenta del cliente</param>
        /// <param name="strNroTelefono">Numero de telefono del cliente</param>
        /// <returns>Verifica el estado de la cuenta</returns>
        public AccountStatusResponse GetVerificationOfAccountStatus(string strIdSession, string strCliNroCuenta, string strNroTelefono, string BillingCycle)
        {
            if (string.IsNullOrEmpty(BillingCycle)) { BillingCycle = "01"; }

            Claro.Web.Logging.Info(strIdSession, DateTime.Now.ToString("yyyyMMddhhmmss"), string.Format("entrada llamada al ws de ConsultaEstadoCuenta mediante el metodo GetVerificationOfAccountStatus ( strIdSession:{0}, strCliNroCuenta:{1},  strNroTelefono:{2}, (ciclodefacturacion) BillingCycle:{3})", strIdSession, strCliNroCuenta, strNroTelefono, BillingCycle));
            int DiaDeFactutacion = Convert.ToInt(BillingCycle);
            DateTime oFechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            if (DateTime.Now.Day < DiaDeFactutacion)
            {
                oFechaDesde = oFechaDesde.AddMonths(-1);
                oFechaDesde = oFechaDesde.AddDays(DiaDeFactutacion - 1);
            }
            else if (DateTime.Now.Day == DiaDeFactutacion)
            {
                oFechaDesde = oFechaDesde.AddDays(DiaDeFactutacion - 2);
            }
            else
            {
                oFechaDesde = oFechaDesde.AddDays(DiaDeFactutacion - 1);
            }

            AccountStatusResponse result = new AccountStatusResponse();
            var _audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            string formatoFecha = ConfigurationManager.AppSettings("strASRpFormatoFechaYMD");
            Claro.Web.Logging.Info(strIdSession, DateTime.Now.ToString("yyyyMMddhhmmss"), string.Format(" El formato en la KEY que se usa para envio de las fechas de strASRpFormatoFechaYMD:{0}", formatoFecha));
            AccountStatusRequest objRequest = new AccountStatusRequest()
            {
                audit = _audit,
                txId = "",
                pCodAplicacion = ConfigurationManager.AppSettings("strASRpCodAplicacion"),
                pUsuarioAplic = Common.CurrentUser,
                pTipoConsulta = "REC",///
                pTipoServicio = ConfigurationManager.AppSettings("strASRpTipoServicio"),
                pCliNroCuenta = strCliNroCuenta,
                pNroTelefono = strNroTelefono,
                pFlagSoloSaldo = "1",///
                pFlagSoloDisputa = "",
                pFechaDesde = oFechaDesde.ToString(formatoFecha),
                pFechaHasta = DateTime.Now.ToString(formatoFecha),
                pTamanoPagina = ConfigurationManager.AppSettings("strASRpTamanoPagina"),
                pNroPagina = ConfigurationManager.AppSettings("strASRpNroPagina")

            };

            Claro.Web.Logging.Info(strIdSession, DateTime.Now.ToString("yyyyMMddhhmmss"),
                string.Format("Metodo GetVerificationOfAccountStatus, AccountStatusRequest para el ws de ConsultaEstadoCuenta-> pCodAplicacion:{0}, pUsuarioAplic:{1}, pTipoServicio:{2}, pCliNroCuenta:{3}, pNroTelefono:{4}, pFechaDesde:{5}, pFechaHasta:{6}, pTamanoPagina:{7}, pNroPagina:{8} ",
                objRequest.pCodAplicacion, objRequest.pUsuarioAplic, objRequest.pTipoServicio, objRequest.pCliNroCuenta, objRequest.pNroTelefono, objRequest.pFechaDesde, objRequest.pFechaHasta, objRequest.pTamanoPagina, objRequest.pNroPagina));

            Claro.Web.Logging.Info(strIdSession, _audit.transaction, "Begin GetStatusAccountFixedAOC");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<AccountStatusResponse>(() =>
                {
                    return _oServiceFixed.GetStatusAccountFixedAOC(objRequest);

                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, _audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, DateTime.Now.ToString("yyyyMMddhhmmss"), "salida GetVerificationOfAccountStatus");

            return result;
        }
       /// <summary>
       /// Funcion para obtener el tipo de documento fiscal.
       /// </summary>
       /// <param name="td">tipo de documento de la persona</param>
       /// <returns></returns>
        public static string getTypeDocumentFisc(string td)
        {
            string ctd = Claro.Constants.NumberOneNegativeString;
            switch (td)
            {
                case Claro.SIACU.Constants.DNI: ctd = Claro.Constants.NumberZeroOneString; break;
                case Claro.SIACU.Constants.RUC: ctd = Claro.Constants.NumberZeroSixString; break;
                case Claro.SIACU.Constants.Passport: ctd = Claro.Constants.NumberZeroSevenString; break;
                case Claro.SIACU.Constants.CardForeign_: ctd = Claro.Constants.NumberZeroFourString; break;


            }

            return ctd;

        }
        /// <summary>
        /// Funcion para obtener el nombre de la BANDEJA DE COLAS, verifica si la accion ingresada y la envia a la BANDEJA DE COLAS segun corresponda. se verifica en una key.
        /// </summary>
        /// <param name="IdAccion">accion</param>
        /// <param name="Hfc_Lte">identifica el producto HFC=1, LTE=2</param>
        /// <returns></returns>
        public static string getNombreEnviaCola(string IdAccion, int Hfc_Lte)
        {
            string strBandejaCola = string.Empty;
            try
            {

                //la cola por defecto que estaba ya establecido en el código
                if (Hfc_Lte == 1)
                {
                    strBandejaCola = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("PColaRetenCanceServ", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfig"));
                }
                else if (Hfc_Lte == 2)
                {
                    strBandejaCola = Claro.SIACU.Transac.Service.Functions.GetValueFromConfigFile("PColaRetenCanceServLTE", ConfigurationManager.AppSettings("strConstArchivoHFCPOSTConfig"));
                }

                string strFlagBandejaNR = ConfigurationManager.AppSettings("strFlagBandejaNR");
                if (strFlagBandejaNR == "0")
                {
                    string strColasParaBandejaRetenciones = ConfigurationManager.AppSettings("strAccionesParaBandejaRetencionesFija");
                    if (!string.IsNullOrEmpty(strColasParaBandejaRetenciones))
                    {
                        string[] ColasParaBandejaRetenciones = strColasParaBandejaRetenciones.Split('|');

                        for (int i = 0; i <= ColasParaBandejaRetenciones.Length - 1; i++)
                        {
                            if (IdAccion == ColasParaBandejaRetenciones[i])
                            {
                                strBandejaCola = ConfigurationManager.AppSettings("strColaParaBandejaRetencionesFija");
                                break;
                            }
                        }
                    }
                }
                else if (strFlagBandejaNR == "1")
                {
                    strBandejaCola = ConfigurationManager.AppSettings("strColaParaBandejaRetencionesFija");

                }

                
                Claro.Web.Logging.Error(DateTime.Now.ToString("ddMMyyyyhhmmss"), DateTime.Now.ToString("ddMMyyyyhhmmss"), string.Format("metodo actual getNombreEnviaCola que es llamado desde el metodo DatosCaso, El No retenido se enviara a la cola:{0}.", strBandejaCola));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(DateTime.Now.ToString("ddMMyyyyhhmmss"), DateTime.Now.ToString("ddMMyyyyhhmmss"), ex.Message + " nombre de cola:" + strBandejaCola);

            }
            return strBandejaCola;
        }
        /// <summary>
        /// Genera el ajuste a la ultima factura pendiente de pago
        /// </summary>
        /// <param name="strTantoPorciento">Porcentaje para realizar el ajuste a la ultima factura</param>
        /// <returns>Genera el ajuste a la ultima factura pendiente de pago</returns>
        public bool RegisterInteractionAdjust(string strIdSession, RegisterInteractionAdjustRequest objRegisterInteractionAdjustRequest, string strTantoPorciento, ref string strIdInteractOUT, ref string strIdDocAutOUT, string BillingCycle, string strNroTelefono)
        {

            if (string.IsNullOrEmpty(strTantoPorciento)) { strTantoPorciento = "0 %"; }
            strTantoPorciento = strTantoPorciento.Substring(0, strTantoPorciento.IndexOf('%')).Trim();
            string sFechaVencDoc = null, sNombreDocumento = string.Empty, sFechaDocRef = string.Empty, strOhxAct = string.Empty, strFechaDesde = string.Empty, strFechaHasta = string.Empty, strFechaHasta2 = string.Empty;
            double montoAjusteSinIGV = 0, montoAjusteConIGV = 0, cargoFijoIGV=0, porcentajeDescuento = 0, cargoFijo = 0;
            double dIgv = Convert.ToDouble(GetCommonConsultIgv(strIdSession).igvD.ToString(System.Globalization.CultureInfo.InvariantCulture));
            porcentajeDescuento = (Convert.ToDouble(strTantoPorciento) / 100);

            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piChargeAmount)) { objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piChargeAmount = "0"; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piInter21)) { objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piInter21 = ""; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piBirthday)) { objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piBirthday = ""; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piExpireDate)) { objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piExpireDate = ""; }

            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piErrorWsSap)) { objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piErrorWsSap = ""; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piNombreCliente)) { objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piNombreCliente = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piFirstName + " " + objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piLastName; }
            //mg13
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piTipoIdentFiscal)) { objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piTipoIdentFiscal = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdTipoDoc; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piResponsabCrm)) { objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piResponsabCrm=" "; }
            if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarAjusteOAC.piIdReclamoOrigen)) { objRegisterInteractionAdjustRequest.RegistrarAjusteOAC.piIdReclamoOrigen = ""; }
            //if (string.IsNullOrEmpty(objRegisterInteractionAdjustRequest.RegistrarDetalleDoc.ListaDetDocAdicional[0].pNombreResponsable)) { objRegisterInteractionAdjustRequest.RegistrarDetalleDoc.ListaDetDocAdicional[0].pNombreResponsable = ""; }
            #region "WS_y_Sp"
            //servicio de CONSULTA ESTADO CUENTA WS
            AccountStatusResponse objConsultaEstadoCuenta = new AccountStatusResponse();
            try
            {
                objConsultaEstadoCuenta = GetVerificationOfAccountStatus(strIdSession, objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdCliente, strNroTelefono, BillingCycle); // ws ConsultaEstadoCuenta
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "RegisterInteractionAdjust", ex.Message);
            }

            // obtener el Cargo Fijo.
            try
            {
                if (objConsultaEstadoCuenta != null)
                {
                    int ultimaPosi = 0;
                    ultimaPosi = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta.Count - 1;
                    sFechaVencDoc = System.DateTime.Now.ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]);
                    sNombreDocumento = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosi].xNroDocumento;
                    sFechaDocRef = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosi].xFechaRegistro;
                    //sFechaDocRef = Convert.ToDate(sFechaDocRef).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]);
                    strOhxAct = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosi].xIdDocOrigen;
                    strFechaDesde = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosi].xFechaEmision;
                    //strFechaDesde = Convert.ToDate(strFechaDesde).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]);
                    strFechaHasta = objConsultaEstadoCuenta.xEstadoCuenta.xDetalleEstadoCuentaCab[0].xDetalleTrx.xDetalleEstadoCuenta[ultimaPosi].xFechaVencimiento;
                    //strFechaHasta = Convert.ToDate(strFechaHasta).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]);

                    DateTime fecha1 = Convert.ToDate(strFechaDesde);
                    fecha1 = fecha1.AddMonths(1);

                    DateTime fecha2 = fecha1;
                    fecha2 = fecha2.AddDays(-1);

                    strFechaHasta2 = fecha2.ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]);
                }
                cargoFijo = obtenerCargoFijo(strIdSession, objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdCliente);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "RegisterInteractionAdjust", ex.Message);
            }

            #endregion
            cargoFijoIGV = Math.Round((cargoFijo + (cargoFijo * dIgv)), 2);
            montoAjusteConIGV = Math.Round((cargoFijoIGV * porcentajeDescuento), 2);
            montoAjusteSinIGV = Math.Round((montoAjusteConIGV / (1 + dIgv)), 2);

            RegisterInteractionAdjustResponse result = null;
            var audit = App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);

            string[] strRegistrarInteraccion = null, strRegistrarDetalleInteraccion = null, strRegistrarCabeceraDoc = null, strRegistroDetDocum = null, strDetDocAdicional = null, strAreaImputable = null, strRegistrarAjusteOAC = null, strRegistrarExportarSap = null, strActualizarCamposSap = null;
            strRegistrarInteraccion = ConfigurationManager.AppSettings("strRegistrarInteraccion").Split('|');
            strRegistrarDetalleInteraccion = ConfigurationManager.AppSettings("strRegistrarDetalleInteraccion").Split('|');
            strRegistrarCabeceraDoc = ConfigurationManager.AppSettings("strRegistrarCabeceraDoc").Split('|');
            strRegistroDetDocum = ConfigurationManager.AppSettings("strRegistroDetDocum").Split('|');
            strDetDocAdicional = ConfigurationManager.AppSettings("strDetDocAdicional").Split('|');
            strAreaImputable = ConfigurationManager.AppSettings("strAreaImputable").Split('|');
            strRegistrarAjusteOAC = ConfigurationManager.AppSettings("strRegistrarAjusteOAC").Split('|');
            strRegistrarExportarSap = ConfigurationManager.AppSettings("strRegistrarExportarSap").Split('|');
            strActualizarCamposSap = ConfigurationManager.AppSettings("strActualizarCamposSap").Split('|');

            RegisterInteractionAdjustRequest objRequest = new RegisterInteractionAdjustRequest()
            {
                #region Request

                audit = new AuditRequestFixed()
                {
                    transaction = audit.transaction,
                    Session = audit.Session,
                    ipAddress = audit.ipAddress,
                    applicationName = ConfigurationManager.AppSettings("ApplicationName"),
                    userName = App_Code.Common.CurrentUser
                },

                ContingClafifyAct = ConfigurationManager.AppSettings("strContingClafifyAct"),
                pLTE = objRegisterInteractionAdjustRequest.pLTE,

                RegistrarInteraccion = new RegistrarInteraccion()
                {
                    piContactObjId1 = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piContactObjId1,
                    piSiteObjId1 = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piSiteObjId1,
                    piPhone = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piPhone,
                    piTipo = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piTipo,
                    piClase = strRegistrarInteraccion[0],
                    piSubClase = strRegistrarInteraccion[1],
                    piMetodoContacto = strRegistrarInteraccion[2],
                    piTipoInter = strRegistrarInteraccion[3],
                    piAgente = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piAgente,
                    piUsrProceso = strRegistrarInteraccion[4],
                    piHechoEnUno = strRegistrarInteraccion[5],
                    piNotas = strRegistrarInteraccion[6],
                    piFlagCaso = strRegistrarInteraccion[7],
                    piResultado = strRegistrarInteraccion[8],
                    piCodPlano = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piCodPlano
                },
                RegistrarDetalleInteraccion = new RegistrarDetalleInteraccion()
                {
                    piInter8 = strRegistrarDetalleInteraccion[0],
                    piInter9 = strRegistrarDetalleInteraccion[1],
                    piInter10 = strRegistrarDetalleInteraccion[2],
                    piInter11 = strRegistrarDetalleInteraccion[3],
                    piInter12 = strRegistrarDetalleInteraccion[4],
                    piInter13 = strRegistrarDetalleInteraccion[5],
                    piInter14 = strRegistrarDetalleInteraccion[6],
                    piInter15 = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piInter15,
                    piInter20 = strRegistrarDetalleInteraccion[9],
                    piInter22 = strRegistrarDetalleInteraccion[10],
                    piInter23 = strRegistrarDetalleInteraccion[11],
                    piInter24 = strRegistrarDetalleInteraccion[12],
                    piInter25 = strRegistrarDetalleInteraccion[13],
                    piInter26 = strRegistrarDetalleInteraccion[14],
                    piInter27 = strRegistrarDetalleInteraccion[15],
                    piInter28 = strRegistrarDetalleInteraccion[16],
                    piPlusInter2interact = strRegistrarDetalleInteraccion[17],
                    piAdjustmentAmount = Convert.ToString(montoAjusteSinIGV),
                    piAdjustmentReason = strRegistrarDetalleInteraccion[18],
                    piDocumentNumber = strRegistrarDetalleInteraccion[19],
                    piFirstName = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piFirstName,
                    piFlagOther = strRegistrarDetalleInteraccion[20],
                    piLastName = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piLastName,
                    piClaroNumber = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piPhone,
                    piClarolocal1 = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piClarolocal1,
                    piChargeAmount = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piChargeAmount //ultimo cambio
                        /*validar con */
                    ,
                    piInter21 = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piInter21,
                    piBirthday = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piBirthday,
                    piExpireDate = objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piExpireDate

                },
                RegistrarCabeceraDoc = new RegistrarCabeceraDoc()
                {
                    piSubTotalAfecto = strRegistrarCabeceraDoc[0],
                    piSubTotalNoAfecto = Convert.ToString(montoAjusteSinIGV),
                    piMontoIgv = Convert.ToString(montoAjusteConIGV-montoAjusteSinIGV),
                    //piFechaVenc =Convert.ToDate(strFechaHasta).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]), /*sFechaVencDoc*/
                    piFechaVenc = DateTime.Now.ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]), /*sFechaVencDoc*/ // ANTES Convert.ToDate(strFechaHasta)
                    piUsuRegistro = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piAgente,
                    piCicloFact = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piCicloFact,
                    piDocRef = sNombreDocumento,
                    piFechaDocRef = Convert.ToDate(sFechaDocRef).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]),
                    piIdTipoDoc = strRegistrarCabeceraDoc[1],
                    piIdResponsabCrm = strRegistrarCabeceraDoc[2],
                    piIdCliente = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdCliente,
                    piOhxAct = strOhxAct,
                    piIdTipCliente = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdTipCliente,
                    piNumDoc = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piNumDoc,
                    piClienteCta = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piClienteCta,
                    piReintentosWsSap = strRegistrarCabeceraDoc[3],
                    piCiudad = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piCiudad,
                    piCodigoPais = strRegistrarCabeceraDoc[4],
                    piDireccion = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piDireccion,
                    piNumIdentFiscal = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piNumIdentFiscal,
                    piSubjetoImp = strRegistrarCabeceraDoc[5],
                    piVersionSap6 = strRegistrarCabeceraDoc[6]
                        /*validacion*/
                    ,
                    piErrorWsSap = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piErrorWsSap,
                    piNombreCliente = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piNombreCliente, ///poner el nombre del cliente,
                    //Made13
                    piTipoIdentFiscal = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piTipoIdentFiscal// en los log apare 01, que confirme el AF
                    ,
                    piResponsabCrm = " "//*-*************************************************************************************************************
                },
                RegistrarDetalleDoc = new RegistrarDetalleDoc()
                {
                    piNumDocAjuste = sNombreDocumento,
                    ListaRegistroDetDocum = new List<RegistroDetDocum>(){ 
                        new RegistroDetDocum(){
                            pImporte = Convert.ToString(montoAjusteSinIGV),
                            pMontoSinIgv = Convert.ToString(montoAjusteSinIGV),
                            pIgv= Convert.ToString(montoAjusteConIGV-montoAjusteSinIGV),
                            pTotal= Convert.ToString(montoAjusteConIGV),
                            pTelefono=objRegisterInteractionAdjustRequest.RegistrarDetalleDoc.ListaRegistroDetDocum[0].pTelefono, /*strNroTelefono*/
                            pFechaDesde= Convert.ToDate(strFechaDesde).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]),
                            //pFechaHasta= Convert.ToDate(strFechaHasta).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]),
                            pFechaHasta= Convert.ToDate(strFechaHasta2).ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaDMY"]),
                            pIdCategoria=strRegistroDetDocum[0],
                            pSubCategoria=strRegistroDetDocum[1],
                            pTipoTran=strRegistroDetDocum[2]
                        }  
                    },
                    ListaDetDocAdicional = new List<DetDocAdicional>() { new DetDocAdicional(){ 
                                pIdCategoria = strDetDocAdicional[0],
                                pNombreCategoria = strDetDocAdicional[1],
                                pImporte = Convert.ToString(montoAjusteSinIGV),
                                pImporteAjustar = Convert.ToString(montoAjusteSinIGV),  
                                pIgvImporteAjustarIgv = Convert.ToString(montoAjusteConIGV-montoAjusteSinIGV),
                                pImporteAjustarIgv = Convert.ToString((montoAjusteConIGV)),
                                pIdAreaImputar = strDetDocAdicional[2],
                                pNombreAreaImputar = strDetDocAdicional[3],
                                pIdMotivo = strDetDocAdicional[4],
                                pNombreMotivo = strDetDocAdicional[5],
                                pIdResponsable = strDetDocAdicional[6],
                                pNombreResponsable=" - "///******************************************
                                //pNombreResponsable =objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piFirstName + " " + objRegisterInteractionAdjustRequest.RegistrarDetalleInteraccion.piLastName
                                
                      } 
                    }
                },
                RegistrarAreaImputable = new RegistrarAreaImputable()
                {
                    ListadoAreaImputable = new List<AreaImputable>() {  new AreaImputable(){
                        piArimIdCategoria = strAreaImputable[0],
                        piArimIdArea = strAreaImputable[1],
                        piArimIdMotivo = strAreaImputable[2],
                        piArimTotalImputable = Convert.ToString(montoAjusteSinIGV)
                     }
                    }
                },
                AprobarDocumento = new AprobarDocumento()//
                {
                    piIdTipoDoc = ConfigurationManager.AppSettings("strpiIdTipoDoc"),
                    piUsuAprob = objRegisterInteractionAdjustRequest.RegistrarInteraccion.piAgente
                },
                RegistrarAjusteOAC = new RegistrarAjusteOAC()
                {
                    piCodAplicacionOAC = strRegistrarAjusteOAC[0],
                    piTipoServicio = strRegistrarAjusteOAC[1],
                    piCodCuenta = objRegisterInteractionAdjustRequest.RegistrarCabeceraDoc.piIdCliente,
                    piTipoOperacion = strRegistrarAjusteOAC[2],
                    piTipoAjuste = strRegistrarAjusteOAC[3],
                    piEstado = strRegistrarAjusteOAC[4],
                    piMonedaOrigen = strRegistrarAjusteOAC[5],
                    piNSaldoAjuste = strRegistrarAjusteOAC[6],
                    piCodMotivoAjuste = strRegistrarAjusteOAC[7],
                    piFechaAjuste = DateTime.Now.ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaYMD"]),
                    piFechaCancelacion = new DateTime().ToString(System.Configuration.ConfigurationManager.AppSettings["strRCFormatoFechaYMD"])
                        /*validaciones*/
                   ,
                    piIdReclamoOrigen = objRegisterInteractionAdjustRequest.RegistrarAjusteOAC.piIdReclamoOrigen, 
                    docsRef=null
                },
                RegistrarExportarSap = new RegistrarExportarSap()
                {
                    piCuentaCab = strRegistrarExportarSap[0],
                    piTipoDocCab = strRegistrarExportarSap[1],
                    piClaseDocumentoCab = strRegistrarExportarSap[2],
                    piSociedadCab = strRegistrarExportarSap[3],
                    piMonedaCab = strRegistrarExportarSap[4],
                    piTextoCab = objRegisterInteractionAdjustRequest.RegistrarExportarSap.piTextoCab,
                    piClavePosCab = strRegistrarExportarSap[7],
                    piClaveNegCab = strRegistrarExportarSap[8],
                    piTipoDocDet = strRegistrarExportarSap[9],
                    piIndivaDet = strRegistrarExportarSap[10],
                    piClavePosDet = strRegistrarExportarSap[11],
                    piClaveNegDet = strRegistrarExportarSap[12],
                    piTipoDocIgv = strRegistrarExportarSap[13],
                    piCuentaIgv = strRegistrarExportarSap[14],
                    piIndicadorSap = strRegistrarExportarSap[15],
                    piFlagEliminarAnterior = strRegistrarExportarSap[16]
                },
                ActualizarCamposSap = new ActualizarCamposSap()
                {
                    piFlagEnvioSap = strActualizarCamposSap[0],
                    piErrorWsSap = strActualizarCamposSap[1],
                    piReintentosWsSap = strActualizarCamposSap[2],
                },
            };


                #endregion

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin RegisterInteractionAdjust");
            try
            {
                result = Claro.Web.Logging.ExecuteMethod<RegisterInteractionAdjustResponse>(() =>
                {
                    return _oServiceFixed.GetRegisterInteractionAdjust(objRequest);

                });
                strIdInteractOUT = result.idInteract;
                strIdDocAutOUT = result.idDocAut;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objRequest.audit.transaction, ex.Message);
            }
            Claro.Web.Logging.Info(strIdSession, audit.transaction, string.Format("End RegisterInteractionAdjust result: {0}", result));

            if (result.auditResponse.codigoRespuesta == "0") { return true; } else { return false; }

        }

        /// <summary>
        /// Envio de correo con el formato de las nuevas acciones
        /// </summary>
        /// <param name="strIdInteraction">numero de interaccion generado en la tipificacion</param>
        /// <returns>Envio de correo con el formato de las nuevas acciones</returns>
        public string GetSendEmailCFSA(string strInteraccionId, string strAdjunto, Model.HFC.RetentionCancelServicesModel model, string strNombreArchivoPDF, byte[] attachFile, string strNomFile)
        {
            string strResul = string.Empty;
            CommonTransacService.AuditRequest AuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(model.IdSession);
            try
            {
                string strMessage = string.Empty;
                string strDestinatarios = model.Destinatarios;
                string strAsunto = (model.typeRETEFIDE == Model.HFC.typeRETEFIDE.RETE ?
                    ConfigurationManager.AppSettings("strAsuntoRete") : ConfigurationManager.AppSettings("strAsuntoFide"));
                string strRemitente = ConfigurationManager.AppSettings("CorreoServicioAlCliente");
                string[] cuerpo = ConfigurationManager.AppSettings("strCuerpoCorreoReteFide").Split('|');

                Claro.Web.Logging.Info(model.IdSession, strInteraccionId, "Begin GetSendEmailCFSA");

                #region "Body Email"

                strMessage = "<html>";
                strMessage += " <head>";
                strMessage += "     <style type='text/css'>";
                strMessage += "     <!--";
                strMessage += "         .Estilo1 {font-family: Arial, Helvetica, sans-serif;font-size:12px;}";
                strMessage += "         .Estilo2 {font-family: Arial, Helvetica, sans-serif;font-weight:bold;font-size:12px;}";
                strMessage += "      -->";
                strMessage += "      </style>";
                strMessage += " </head>";
                strMessage += "<body>";
                strMessage += "     <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                strMessage += "         <tr><td width='180' class='Estilo1' height='22'>Estimado Cliente, </td></tr>";
                if (model.Accion == "R")
                {
                    if (model.typeRETEFIDE == Models.HFC.typeRETEFIDE.RETE)
                    { strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + cuerpo[0] + "</td></tr>"; }
                    else
                    {
                        string[] cuerpoFide = cuerpo[2].Split('#');
                        string strBodyMessage = string.Empty;
                        if (model.flagCargFijoServAdic == Claro.Constants.NumberZeroString) //Cargo fijo xDescx   xmesex
                        {
                            strBodyMessage = cuerpoFide[0] + cuerpoFide[2].Replace("xDescx", model.DiscountDescription).Replace("xmesex", model.mesDesc);
                        }
                        else if (model.flagCargFijoServAdic == Claro.Constants.NumberOneString) //Servicio adicional xtypex xServiceAdix xmesesx
                        {
                            strBodyMessage = cuerpoFide[0] + cuerpoFide[1].Replace("xtypex", model.PaqueteODeco.ToLower()).Replace("xServiceAdix", model.descServAdic).Replace("xmesesx", model.mesDesc);
                        }
                        if (model.flagCargFijoServAdic == Claro.Constants.NumberTwoString) // Incremento de velocidad 
                            strBodyMessage = cuerpoFide[0] + cuerpoFide[2].Replace("xBonox", model.BonoRetentionFidelizacion).Replace("xMesesBonox", model.VigenciaRetFid);
                        {

                        }

                        strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + strBodyMessage + "</td></tr>"; 
                    }

                }
                else
                {
                    strMessage += "         <tr><td width='180' class='Estilo1' height='22'>" + cuerpo[1] + "</td></tr>";
                }

                strMessage += "<tr>";
                strMessage += " <td align='center'>";
                strMessage += " </td></tr>";

                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>&nbsp;</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>Cordialmente</td></tr>";
                strMessage += "         <tr><td class='Estilo1'>Atención al Cliente</td></tr>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td height='10'></td>";
                strMessage += "         <tr><td class='Estilo1'>Consultas, llame gratis desde su celular Claro al 123 o al 0801-123-23 (costo de llamada local).</td></tr>";
                strMessage += "    </table>";
                strMessage += "  </body>";
                strMessage += "</html>";
                #endregion

                SendEmailSBRequest objSendEmailSBReq = new SendEmailSBRequest();
                SendEmailSBResponse objSendEmailSBRes = new SendEmailSBResponse();

                objSendEmailSBReq = new SendEmailSBRequest()
                {
                    audit = AuditRequest,
                    TransactionId = model.Transaction,
                    SessionId = model.IdSession,
                    strRemitente = strRemitente,
                    strDestinatario = strDestinatarios,
                    strMensaje = strMessage,
                    strHTMLFlag = "1",
                    strAsunto = strAsunto,
                    Archivo = attachFile,
                    strNomFile = strNomFile

                };

                objSendEmailSBRes = Claro.Web.Logging.ExecuteMethod<CommonTransacService.SendEmailSBResponse>(() => { return _oServiceCommon.GetSendEmailSB(objSendEmailSBReq); });
                if (objSendEmailSBRes.codigoRespuesta == "0")
                {
                    strResul = Functions.GetValueFromConfigFile("strMensajeEnvioOK", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                else
                {
                    strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                }
                Claro.Web.Logging.Info(model.IdSession, strInteraccionId, "End GetSendEmailCFSA result: " + strResul);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(model.IdSession, AuditRequest.transaction, ex.Message);
                Claro.Web.Logging.Info(model.IdSession, AuditRequest.transaction, "Retencion Cancelación Services_HFC  ERROR - GetSendEmail");
                strResul = Functions.GetValueFromConfigFile("strMsgNoSeEnvioMailNotif", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
            }
            return strResul;
        }

        /// <summary>
        /// Genera la constancia para las nuevas acciones
        /// </summary>
        /// <param name="strIdInteraction">numero de interaccion generado en la tipificacion</param>
        /// <returns>Genera la constancia para las nuevas acciones</returns>
        public Dictionary<string, object> GetConstancyPDFCFSA(string strIdSession, string strIdInteraction, Model.HFC.RetentionCancelServicesModel oModel)
        {
            var listResponse = new Dictionary<string, object>();
            string nombrepath = string.Empty;
            bool generado = false;
            string strTypeTransaction = string.Empty;
            string strNombreArchivo = string.Empty;
            string strTexto = string.Empty;

            InteractionServiceRequestHfc objInteractionServiceRequest = null;
            ParametersGeneratePDF parameters = new ParametersGeneratePDF();

            if (oModel.typeRETEFIDE == Models.HFC.typeRETEFIDE.RETE)
            { strNombreArchivo = System.Configuration.ConfigurationManager.AppSettings["gConstRETE"]; }
            else
            { strNombreArchivo = System.Configuration.ConfigurationManager.AppSettings["gConstFIDE"]; }


            try
            {
                strTexto = CSTS.Functions.GetValueFromConfigFile("strMsgRetencionCancelConstanciaTexto", ConfigurationManager.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));

                if (oModel.Accion == "R") // Retenido
                {
                    strTypeTransaction = oModel.typeRETEFIDE == Models.HFC.typeRETEFIDE.RETE ?
                        ConfigurationManager.AppSettings("strNombreArchivo_Retencion") : ConfigurationManager.AppSettings("strNombreArchivo_Fidelizacion");
                    parameters = new ParametersGeneratePDF();
                    parameters.StrCustomerId = oModel.CustomerId;
                    parameters.StrFechaTransaccionProgram = Convert.ToDate(oModel.fechaActual).ToString("dd/MM/yyyy");
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.StrAccion = oModel.DesAccion;
                    parameters.StrTipoTransaccion = strTypeTransaction;
                    parameters.strInternetActual = oModel.InternetActual;
                    parameters.strBonoRetencionFidelizacion = oModel.BonoRetentionFidelizacion;
                    parameters.Vigencia = oModel.VigenciaRetFid;

                    if (oModel.typeRETEFIDE == Models.HFC.typeRETEFIDE.RETE)
                    {
                        if (oModel.typeHFCLTE == Models.HFC.typeHFCLTE.HFC)
                            parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoHFC");
                        else
                            parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionRetenidoLTE");
                    }
                    else
                    {
                        if (oModel.typeHFCLTE == Models.HFC.typeHFCLTE.HFC)
                            parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionFidelizacionHFC");
                        else
                            parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionFidelizacionLTE");
                    }
                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;
                    parameters.StrContenidoComercial2 = strTexto;
                }
                else // No Retenido(NR)
                {
                    strTypeTransaction = ConfigurationManager.AppSettings("strNombreArchivo_Cancelacion"); //SOLICITUD DE CANCELACION
                    parameters = new ParametersGeneratePDF();
                    parameters.StrProductos = oModel.ProductType;
                    parameters.StrTipoDocIdentidad = oModel.TypeDoc;
                    parameters.strRepLegNroDocumento = oModel.DNI_RUC;
                    parameters.StrNroDocIdentidad = oModel.DNI_RUC;
                    parameters.strDireccionInstalcion = oModel.AdressDespatch;
                    parameters.strDireccionInstalac = oModel.AdressDespatch;
                    parameters.StrFechaTransaccionProgram = Convert.ToDate(oModel.DateProgrammingSot).ToString("dd/MM/yyyy hh:mm");
                    parameters.StrTipoTransaccion = strTypeTransaction;

                    if (oModel.typeHFCLTE == Models.HFC.typeHFCLTE.HFC)
                        parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionNoRetenidoHFC");
                    else
                        parameters.StrCarpetaTransaccion = ConfigurationManager.AppSettings("strCarpetaTransaccionNoRetenidoLTE");

                    parameters.StrNombreArchivoTransaccion = strNombreArchivo;
                    parameters.StrFechaCancel = Convert.ToDate(oModel.DateProgrammingSot).ToString("dd/MM/yyyy hh:mm");
                    parameters.StrContenidoComercial2 = strTexto;
                }
                Claro.Web.Logging.Info(strIdSession, "transaccion", string.Format("Begin GetConstancyPDFCFSA - nombreArchivo: {0}, tipoTransaccion: {1}", strNombreArchivo, strTypeTransaction));

                parameters.StrSubMotivoCancel = oModel.DesSubMotivo;
                parameters.StrMotivoCancelacion = oModel.DesMotivos;
                parameters.Vigencia = oModel.flagCargFijoServAdic == "2" ? oModel.VigenciaRetFid : oModel.mesDesc;
                parameters.Descuento = oModel.DiscountDescription;
                parameters.StrNroServicio = oModel.Msisdn;
                parameters.NroTelefono = oModel.Telephone;
                parameters.StrCentroAtencionArea = oModel.DescCacDac;
                parameters.StrCasoInter = oModel.CodigoInteraction;
                parameters.StrSegmento = oModel.Segmento;
                parameters.strContrato = oModel.ContractId;
                parameters.strFechaHoraAtención = Convert.ToString(DateTime.Now);
                parameters.StrTitularCliente = oModel.NameComplet;
                parameters.StrRepresLegal = oModel.RepresentLegal;
                parameters.StrDescripTransaccion = (oModel.typeRETEFIDE == Models.HFC.typeRETEFIDE.RETE ? ConfigurationManager.AppSettings("strNombreTransaccionRete") : ConfigurationManager.AppSettings("strNombreTransaccionFide"));
                parameters.StrReferenciaActual = oModel.Reference;
                parameters.StrReferenciaDestino = oModel.ReferenceOfTransaction;
                parameters.StrEmail = oModel.Email;
                parameters.StrEnviarEmail = oModel.Flag_Email == true ? "SI" : "NO";
                parameters.strEnvioCorreo = oModel.Flag_Email == true ? "SI" : "NO";
                parameters.strContratoCliente = oModel.ContractId;
                parameters.StrNumeroContrato = oModel.ContractId;
                parameters.strFechaTransaccion = Convert.ToDate(oModel.fechaActual).ToString("dd/MM/yyyy");
                parameters.StrNombreServicio = oModel.descServAdic;
                parameters.RegularBonusServAdic = oModel.RegularBonusServAdic;
                parameters.RetentionBonusServAdic = oModel.RetentionBonusServAdic;
                parameters.StrFechaCompromiso = oModel.FechaCompromiso;
                parameters.strMontoReintegro = oModel.Reintegro;
                parameters.Constancia = oModel.Constancia;
                parameters.flagCargFijoServAdic = oModel.flagCargFijoServAdic;
                parameters.flagServDeco = oModel.flagServDeco;
                parameters.StrNombreAgenteUsuario = oModel.NombreAsesor;
                parameters.StrCodigoAsesor = oModel.CodigoAsesor;
                parameters.StrNombreAsesor = parameters.StrNombreAgenteUsuario;
                parameters.StrTelfReferencia = oModel.TelefonoReferencia;
                parameters.StrCostoTransaccion = oModel.RetentionBonusServAdic;
                parameters.StrCostoInstalacion = oModel.costInst;
                parameters.StrCasoInter = strIdInteraction;//esto nunca lo seteaba
                parameters.strCasoInteraccion = strIdInteraction;//esto nunca lo seteaba
                GenerateConstancyResponseCommon response = GenerateContancyPDF(strIdSession, parameters);
                nombrepath = response.FullPathPDF;
                generado = response.Generated;
                oModel.bGeneratedPDF = response.Generated;
                oModel.strFullPathPDF = response.FullPathPDF;
                listResponse.Add("respuesta", generado);
                listResponse.Add("ruta", nombrepath);
                listResponse.Add("nombreArchivo", strNombreArchivo);

                Claro.Web.Logging.Info(strIdSession, "transaccion", string.Format("End GetConstancyPDFCFSA - nombreArchivo: {0}, generado: {1}, nombrepath: {2}", strNombreArchivo, generado, nombrepath));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oModel.IdSession, objInteractionServiceRequest.audit.transaction, ex.Message);
            }
            return listResponse;
        }

        /// <summary>
        /// Verifica los pagos del cliente
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Obtiene la deuda actual del cliente</returns>
        public QueryDebtResponse GetVerifyPaymentes(string strIdSession, string strCoId)
        {
            FixedTransacService.QueryDebtResponse objDeudaResponse = new FixedTransacService.QueryDebtResponse();
            FixedTransacService.AuditRequest audit =
                App_Code.Common.CreateAuditRequest<FixedTransacService.AuditRequest>(strIdSession);
            FixedTransacService.QueryDebtRequest objDeudaRequest =
                new FixedTransacService.QueryDebtRequest()
                {
                    audit = audit,
                    pTxId = "",
                    pCodAplicacion = ConfigurationManager.AppSettings("strCodAplicacion"),
                    pCodBanco = ConfigurationManager.AppSettings("strCodBanco"),
                    pCodReenvia = ConfigurationManager.AppSettings("strCodReenvia"),
                    pCodMoneda = "",
                    pCodTipoServicio = ConfigurationManager.AppSettings("strCodTipoServicio"),
                    pPosUltDocumento = Decimal.Parse(ConfigurationManager.AppSettings("strPosUltDocumento")),
                    pTipoIdentific = ConfigurationManager.AppSettings("strTipoIdentific"),
                    pDatoIdentific = strCoId,
                    pNombreComercio = ConfigurationManager.AppSettings("strNombreComercio"),
                    pNumeroComercio = "",
                    pCodAgencia = "",
                    pCodCanal = "",
                    pCodCiudad = "",
                    pNroTerminal = "",
                    pPlaza = "",
                    pNroReferencia = ""
                };

            Claro.Web.Logging.Info(strIdSession, audit.transaction, "Begin GetVerifyPaymentes");
            try
            {
                objDeudaResponse =
                    Claro.Web.Logging.ExecuteMethod<FixedTransacService.QueryDebtResponse>(() =>
                    {
                        return _oServiceFixed.GetDebtQuery(objDeudaRequest);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objDeudaRequest.audit.transaction, ex.Message);

            }


            Claro.Web.Logging.Info(strIdSession, audit.transaction, "End GetVerifyPaymentes");
            return objDeudaResponse;




        }

        /// <summary>
        /// Valida la deuda pendiente del cliente
        /// </summary>
        /// <param name="strCoId">Contrato del cliente</param>
        /// <returns>Obtiene la deuda actual del cliente</returns>
        public JsonResult GetDebtQuery(string strIdSession, string strCoId)
        {
            FixedTransacService.QueryDebtResponse objDeudaResponse = new FixedTransacService.QueryDebtResponse();
            try
            {
                objDeudaResponse = GetVerifyPaymentes(strIdSession, strCoId);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "GetDebtQuery", ex.Message);

            }
            return Json(new { data = objDeudaResponse });
        }

        /// <summary>
        /// Obtiene el monto actual de la factura
        /// </summary>
        /// <param name="strCustomerID">CustomerID del cliente</param>
        /// <returns>Obtiene el monto actual de la factura</returns>
        public double obtenerCargoFijo(string strIdSession, string strCustomerID)
        {
            Claro.Web.Logging.Info(strIdSession, "obtenerCargoFijo", string.Format("entrada a metodo obtenerCargoFijo(strIdSession {0}, strCustomerID {1}) ", strIdSession, strCustomerID));
            double montoPendiente = 0;

            PostTransacService.PostTransacServiceClient oPostServiceT = new PostTransacService.PostTransacServiceClient();
            PostTransacService.ReceiptResponseTransactions oResponse = null;
            PostTransacService.ReceiptRequestTransactions oRequest = new PostTransacService.ReceiptRequestTransactions();
            PostTransacService.AuditRequest audit = App_Code.Common.CreateAuditRequest<PostTransacService.AuditRequest>(strIdSession);
            oRequest.audit = audit;
            oRequest.CustomerCode = strCustomerID;
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<PostTransacService.ReceiptResponseTransactions>(() => { return oPostServiceT.GetDataInvoice(oRequest); });
                montoPendiente = Convert.ToDouble(oResponse.ObjReceipt.RECIBO_DETALLADO.TOTALACCESS);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.transaction, ex.Message);

            }
            Claro.Web.Logging.Info(strIdSession, "obtenerCargoFijo", string.Format("salida del motodo obtenerCargoFijo({0},{1}) con resultado:{2}", strIdSession, strCustomerID, montoPendiente.ToString()));
            return montoPendiente;
        }

        /// <summary>
        /// Valida las credenciales del usuario
        /// </summary>
        /// <param name="user">Usuario ingresado</param>
        /// <param name="pass">Clave del usuario</param>
        /// <returns>Valida las credenciales del usuario</returns>
        private bool IsAuthenticated(string strIdSession, string user, string pass)
        {
            Claro.Web.Logging.Info(strIdSession, "IsAuthenticated", string.Format("entrada al metodo que valida si existe o no en el Active Directory: IsAuthenticated(strIdSession, user, pass)", strIdSession, user, pass));
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            string strDominio = ConfigurationManager.AppSettings("DominioLDAP");
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(strDominio, user, pass);
            try
            {
                var obj = entry.NativeObject;
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + user + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult resul = search.FindOne();

                if (resul == null)
                {
                    return false;
                }
                Claro.Web.Logging.Info(strIdSession, "IsAuthenticated", "salida del metodo que valida si existe o no en el Active Directory con resultado true");
                return true;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, "Metodo IsAuthenticated Error: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Valida las credenciales del usuario
        /// </summary>
        /// <param name="user">Usuario ingresado</param>
        /// <param name="pass">Clave del usuario</param>
        /// <param name="option">perfil del usuario</param>
        /// <returns>Valida las credenciales del usuario</returns>
        public JsonResult CheckingUser(string strIdSession, string user, string pass, string option)
        {
            Claro.Web.Logging.Info(strIdSession, "CheckingUser", "metodo CheckingUser cuando es NO RETENIDO");
            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("entrada al metodo de CheckingUser, con los parametros de entrada: strIdSession: {0}, user {1},  pass: {2},  option(PERFIL del usuario): {3}", strIdSession, user, pass, option));
            CommonTransacService.CheckingUserResponse objCheckingUserResponse = null;
            CommonTransacService.ReadOptionsByUserResponse objReadOptionsByUserResponse = null;
            CommonTransacService.EmployeeResponse objEmployeeResponse = null;
            CommonTransacService.AuditRequest objAuditRequest = App_Code.Common.CreateAuditRequest<CommonTransacService.AuditRequest>(strIdSession);
            bool result = false;
            int responseCode = 0;
            string UserValidator = "";
            string usu = "";
            try
            {
                Claro.Web.Logging.Info(strIdSession, "CheckingUser", "Entra al metodo de IsAuthenticated (por Active Directory)");
                result = IsAuthenticated(strIdSession, user, pass);
                Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("salio del metodo de IsAuthenticated (por Active Directory) con resultado true/false: {0} ", result.ToString()));
                if (result == true)
                {
                    result = false;

                    CommonTransacService.CheckingUserRequest objCheckingUserRequest;
                    objCheckingUserRequest = new CommonTransacService.CheckingUserRequest()
                    {
                        audit = objAuditRequest,
                        Usuario = user,
                        AppCod = int.Parse(KEY.AppSettings("ApplicationCode"))
                    };
                    Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("el codigo de aplicacion ApplicationCode(webconfig) es: " + KEY.AppSettings("ApplicationCode")));
                    CommonTransacService.CommonTransacServiceClient OCLI = new CommonTransacServiceClient();
                    Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format(" entra a OCLI.CheckingUser(objCheckingUserRequest) y guarda en objCheckingUserResponse"));
                    objCheckingUserResponse = OCLI.CheckingUser(objCheckingUserRequest);
                    Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format(" sale de OCLI.CheckingUser(objCheckingUserRequest) y lo guardo en objCheckingUserResponse "));
                    if (objCheckingUserResponse != null)
                    {
                        Claro.Web.Logging.Info(strIdSession, "CheckingUser", "objCheckingUserResponse no es null");
                        if (objCheckingUserResponse.consultasSeguridad != null && (objCheckingUserResponse.consultasSeguridad != null && objCheckingUserResponse.consultasSeguridad.Count > 0))
                        {
                            usu = objCheckingUserResponse.consultasSeguridad[0].Usuaccod;
                            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("el usuario en el indice objCheckingUserResponse.consultasSeguridad[0].Usuaccod : {0}", objCheckingUserResponse.consultasSeguridad[0].Usuaccod));
                        }
                        int IdUser = 0;
                        bool est = int.TryParse(usu, out IdUser);
                        if (!est)
                        {
                            CommonTransacService.EmployeeRequest objEmployeeRequestEmployee = new CommonTransacService.EmployeeRequest()
                            {
                                audit = objAuditRequest,
                                UserName = user

                            };
                            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("entrará a buscar .GetEmployeByUserwithDP(objEmployeeRequestEmployee"));
                            objEmployeeResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.EmployeeResponse>(() =>
                            {
                                return new CommonTransacService.CommonTransacServiceClient().GetEmployeByUserwithDP(objEmployeeRequestEmployee);
                            });
                            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("salió de buscar .GetEmployeByUserwithDP(objEmployeeRequestEmployee"));
                            if (objEmployeeResponse != null && (objEmployeeResponse.lstEmployee != null && objEmployeeResponse.lstEmployee.Count > 0))
                            {
                                usu = objEmployeeResponse.lstEmployee[0].userId.ToString();
                                Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format(" el objeto objEmployeeResponse no es null, y el usuario en el indice objEmployeeResponse.lstEmployee[0].userId es:", objEmployeeResponse.lstEmployee[0].userId.ToString()));
                            }
                            est = int.TryParse(usu, out IdUser);
                        }
                        if (est)
                        {
                            CommonTransacService.ReadOptionsByUserRequest objReadOptionsByUserRequest = new CommonTransacService.ReadOptionsByUserRequest()
                            {
                                audit = objAuditRequest,
                                IdUser = IdUser,
                                IdAplication = int.Parse(ConfigurationManager.AppSettings("ApplicationCode"))
                            };
                            objReadOptionsByUserResponse = Claro.Web.Logging.ExecuteMethod<CommonTransacService.ReadOptionsByUserResponse>(() =>
                            {
                                return new CommonTransacService.CommonTransacServiceClient().ReadOptionsByUserwithDP(objReadOptionsByUserRequest);
                            });
                        }
                        StringBuilder strPermission = null;
                        if (objReadOptionsByUserResponse != null && (objReadOptionsByUserResponse.ListOption != null && objReadOptionsByUserResponse.ListOption.Count > 0))
                        {
                            strPermission = new StringBuilder();
                            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("se buscará este valor ConfigurationManager.AppSettings(option): {0} en la siguiente lista de permisos ", ConfigurationManager.AppSettings(option)));
                            foreach (CommonTransacService.PaginaOption item in objReadOptionsByUserResponse.ListOption)
                            {
                                strPermission.Append(item.Clave);
                                strPermission.Append(",");
                                Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format(" los permisos cargagos son item.Clave {0}:", item.Clave));
                            }
                        }
                        if (strPermission != null)
                        {
                            if (strPermission.ToString().IndexOf(ConfigurationManager.AppSettings(option)) != -1)
                            {
                                responseCode = 1;
                                UserValidator = user;
                                Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("se encontró el perfil ConfigurationManager.AppSettings(option) {0} en la lista de perfiles con permiso", ConfigurationManager.AppSettings(option)));
                            }
                            else
                            {
                                responseCode = 2;
                                UserValidator = "";
                            }
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                objCheckingUserResponse = null;
                objReadOptionsByUserResponse = null;
                Claro.Web.Logging.Info(strIdSession, "CheckingUser", "Error en catch, " + ex.Message + "NET largo: " + ex.ToString());
                Claro.Web.Logging.Error(strIdSession, objAuditRequest.transaction, ex.Message);
                responseCode = 3;

            }
            Claro.Web.Logging.Info(strIdSession, "CheckingUser", string.Format("return con los valores UserValidator {0}, result {1}, para que sea ok debe ser 1.", UserValidator, responseCode.ToString()));
            return Json(new { UserValidator = UserValidator, result = responseCode });

        }

        //PROY-32650-Retencion/Fidelización II

        public ArrayList ObtieneFranjasHorariasAdicional(TimeZoneVM objTimeZoneVM, string strIdSession = "")
        {
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ObtieneFranjasHorarias ", "entra a ObtieneFranjasHorarias");

            ArrayList listaHorarios = new ArrayList();
            ArrayList listaHorariosAux = new ArrayList();
            FixedService.GenericItem objHorarioAux = new FixedService.GenericItem();


            FixedTransacService.AuditRequest Audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(strIdSession);

            string idTran, ipApp, nomAp, usrAp;
            try
            {
                idTran = App_Code.Common.GetTransactionID();
                ipApp = App_Code.Common.GetApplicationIp();
                nomAp = KEY.AppSettings("NombreAplicacion");
                usrAp = App_Code.Common.CurrentUser;

                String objDisponibilidad = objTimeZoneVM.vSubJobTypes.Split('|')[1];

                DateTime dInitialDate = Convert.ToDate(objTimeZoneVM.vCommitmentDate);

                int fID = Convert.ToInt(Functions.GetValueFromConfigFile("strDiasConsultaCapacidad", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));
                DateTime[] dDate = new DateTime[fID];

                dDate[0] = dInitialDate;

                for (int i = 1; i < fID; i++)
                {
                    dInitialDate = dInitialDate.AddDays(1);
                    dDate[i] = dInitialDate;
                }

                Boolean vExistSesion = false;
                string strUbicacion = Functions.GetValueFromConfigFile("strConstArchivoSIACUTHFCConfig", "strCodigoUbicacion");
                string[] vUbicaciones = { strUbicacion };
                string v1, v2, v3, v4, v5, v6, v7, v8;

                v1 = Functions.GetValueFromConfigFile("strCalcDuracion", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v2 = Functions.GetValueFromConfigFile("strCalcDuracionEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v3 = Functions.GetValueFromConfigFile("strCalcTiempoViaje", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v4 = Functions.GetValueFromConfigFile("strCalcTiempoViajeEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v5 = Functions.GetValueFromConfigFile("strCalcHabTrabajo", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v6 = Functions.GetValueFromConfigFile("strCalcHabTrabajoEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v7 = Functions.GetValueFromConfigFile("strObtenerZonaUbi", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                v8 = Functions.GetValueFromConfigFile("strObtenerZonaUbiEspec", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));


                String vHabTra = String.Empty;
                vHabTra = Functions.GetValueFromConfigFile("strCodigoHabilidad", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));

                string[] vEspacioTiempo = { string.Empty };
                string[] HabilidadTrabajo = { vHabTra };


                FixedTransacService.BEETACampoActivityHfc oObj1 = new BEETACampoActivityHfc();
                FixedTransacService.BEETACampoActivityHfc oObj2 = new BEETACampoActivityHfc();
                FixedTransacService.BEETACampoActivityHfc oObj3 = new BEETACampoActivityHfc();

                string Aux = "0000000000";

                oObj1.Nombre = Functions.GetValueFromConfigFile("strCampActZonaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj1.Valor = objTimeZoneVM.vHistoryETA.Split('|')[0];

                oObj2.Nombre = Functions.GetValueFromConfigFile("strCampActMapaCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj2.Valor = objTimeZoneVM.vHistoryETA.Split('|')[0].PadLeft(10, '0') + objTimeZoneVM.vHistoryETA.Split('|')[1];

                oObj3.Nombre = Functions.GetValueFromConfigFile("strCampActSubtipoCode", KEY.AppSettings("strConstArchivoSIACUTHFCConfig"));
                oObj3.Valor = objTimeZoneVM.vSubJobTypes.Split('|')[0];


                FixedTransacService.BEETACampoActivityHfc[] oCampoactivity = { oObj1, oObj2, oObj3 };

                FixedTransacService.BEETAParamRequestCapacityHfc oObj2P = new FixedTransacService.BEETAParamRequestCapacityHfc();
                oObj2P.Campo = string.Empty;
                oObj2P.Valor = string.Empty;

                List<BEETAParamRequestCapacityHfc> lst_BEETAParamRequestCapacityHfc = new List<BEETAParamRequestCapacityHfc>();
                lst_BEETAParamRequestCapacityHfc.Add(oObj2P);

                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc oListaPRQ = new BEETAListaParamRequestOpcionalCapacityHfc
                {
                    ParamRequestCapacities = lst_BEETAParamRequestCapacityHfc
                };

                FixedTransacService.BEETAListaParamRequestOpcionalCapacityHfc[] oListaCapcReq = { oListaPRQ };
                FixedTransacService.BEETAAuditoriaResponseCapacityHFC oCapacityResponse = new BEETAAuditoriaResponseCapacityHFC();

                Boolean vOut = false;

                BEETAAuditoriaRequestCapacityHFC objBEETAAuditoriaRequestCapacityHFC = new BEETAAuditoriaRequestCapacityHFC();
                objBEETAAuditoriaRequestCapacityHFC.pIdTrasaccion = idTran;
                objBEETAAuditoriaRequestCapacityHFC.pIP_APP = ipApp;
                objBEETAAuditoriaRequestCapacityHFC.pAPP = nomAp;
                objBEETAAuditoriaRequestCapacityHFC.pUsuario = usrAp;
                objBEETAAuditoriaRequestCapacityHFC.vFechas = dDate.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vUbicacion = vUbicaciones.ToList();

                if (v1 == "1")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDur = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDur = false;

                if (v1 == "2")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcDurEspec = false;

                if (v1 == "3")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViaje = false;

                if (v1 == "4")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcTiempoViajeEspec = false;

                if (v1 == "5")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajo = false;

                if (v1 == "6")
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vCalcHabTrabajoEspec = false;

                if (v1 == "7")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZona = false;

                if (v1 == "8")
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = true;
                else
                    objBEETAAuditoriaRequestCapacityHFC.vObtenerUbiZonaEspec = false;


                objBEETAAuditoriaRequestCapacityHFC.vEspacioTiempo = vEspacioTiempo.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vHabilidadTrabajo = HabilidadTrabajo.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vCampoActividad = oCampoactivity.ToList();
                objBEETAAuditoriaRequestCapacityHFC.vListaCapReqOpc = oListaCapcReq.ToList();

                objBEETAAuditoriaRequestCapacityHFC.audit = Audit;
                if (_BEETAAuditoriaResponseCapacityHFC != null)
                {
                    if (_BEETAAuditoriaResponseCapacityHFC.ObjetoCapacity != null)
                    {

                        foreach (BEETAEntidadcapacidadType objBEETAEntidadcapacidadType in _BEETAAuditoriaResponseCapacityHFC.ObjetoCapacity)
                        {
                            if (objBEETAEntidadcapacidadType.Fecha == Convert.ToDate(objTimeZoneVM.vCommitmentDate))
                            {
                                oCapacityResponse = _BEETAAuditoriaResponseCapacityHFC;
                                vOut = true;
                                break;
                            }
                        }
                        if (!vOut)
                        {
                            oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                            {
                                return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                            });
                        }
                    }
                    else
                    {

                        oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                        {
                            return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                        });

                        _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;

                    }

                }
                else
                {
                    oCapacityResponse = Claro.Web.Logging.ExecuteMethod<FixedTransacService.BEETAAuditoriaResponseCapacityHFC>(() =>
                    {
                        return _oServiceFixed.GetETAAuditoriaRequestCapacity(objBEETAAuditoriaRequestCapacityHFC);
                    });

                    _BEETAAuditoriaResponseCapacityHFC = oCapacityResponse;
                }

                var ocap = oCapacityResponse.ObjetoCapacity;
                int vstrDispoMin = Convert.ToInt(Functions.GetValueFromConfigFile("strDisponMinima", KEY.AppSettings("strConstArchivoSIACUTHFCConfig")));

                string sCadAux = string.Empty;


                var listaHorariosAux_ = Functions.GetListValuesXML("ListaFranjasHorariasETA", "", "HFCDatos.xml");
                foreach (var item in listaHorariosAux_)
                {
                    objHorarioAux = new FixedService.GenericItem();
                    objHorarioAux.Codigo = item.Code;
                    objHorarioAux.Descripcion = item.Description;
                    objHorarioAux.Codigo3 = string.Empty;
                    objHorarioAux.Codigo2 = string.Empty;
                    listaHorarios.Add(objHorarioAux);
                }

                int vstrDispoAuxMax;
                string vstrUbicaMax;

                foreach (FixedService.GenericItem item in listaHorariosAux)
                {
                    objHorarioAux = new FixedService.GenericItem();

                    if (ocap != null)
                    {
                        if (ocap.Count > 0)
                        {
                            vstrDispoAuxMax = 0;
                            vstrUbicaMax = string.Empty;

                            objHorarioAux.Descripcion = item.Descripcion;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Codigo;
                            objHorarioAux.Codigo3 = String.Empty;
                            objHorarioAux.Estado = "RED";


                            foreach (BEETAEntidadcapacidadType oent in ocap)
                            {
                                objHorarioAux.Descripcion2 = oent.Disponible.ToString();
                                if (item.Codigo == oent.EspacioTiempo && dDate[0] == oent.Fecha)
                                {
                                    if (vstrDispoMin <= Convert.ToInt(oent.Disponible))
                                    {
                                        if (Convert.ToInt(objDisponibilidad) < Convert.ToInt(oent.Disponible))
                                        {

                                            if (Convert.ToInt(vstrDispoAuxMax) < Convert.ToInt(oent.Disponible))
                                            {
                                                vstrDispoAuxMax = Convert.ToInt(oent.Disponible);
                                                vstrUbicaMax = oent.Ubicacion;
                                                objHorarioAux.Estado = "WHITE";
                                                objHorarioAux.Codigo3 = oent.Ubicacion;
                                            }
                                        }
                                    }
                                }
                            }

                            listaHorarios.Add(objHorarioAux);
                        }
                        else
                        {
                            objHorarioAux.Descripcion = item.Descripcion;
                            objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                            objHorarioAux.Codigo = item.Codigo;
                            objHorarioAux.Codigo3 = String.Empty;
                            objHorarioAux.Estado = "RED";
                            listaHorarios.Add(objHorarioAux);
                        }
                    }
                    else
                    {
                        objHorarioAux.Descripcion = item.Descripcion;
                        objHorarioAux.Descripcion2 = ConstantsHFC.numeroCero.ToString();
                        objHorarioAux.Codigo = item.Codigo;
                        objHorarioAux.Codigo3 = String.Empty;
                        objHorarioAux.Estado = "RED";
                        listaHorarios.Add(objHorarioAux);
                    }
                }

                if (listaHorarios.Count > 0)
                {
                    string idReq = string.Empty;

                    RegistrarHistorialConsultasEta(
                                                    strIdSession,
                                                    ocap,
                                                    Convert.ToString(dDate[0]),
                                                    Convert.ToInt(objTimeZoneVM.vHistoryETA.Split('|')[0]),
                                                    objTimeZoneVM.vHistoryETA.Split('|')[1],
                                                    objTimeZoneVM.vHistoryETA.Split('|')[2],
                                                    objTimeZoneVM.vSubJobTypes.Split('|')[0],
                                                    objDisponibilidad,
                                                    strUbicacion,
                                                    ref idReq
                                                    );

                    foreach (FixedService.GenericItem item in listaHorarios)
                    {
                        item.Codigo2 = idReq;
                    }

                }
                else
                {
                    FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                    objHorAuxError.Codigo = "-1";
                    objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrCarFraHor", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                    listaHorarios.Add(objHorAuxError);
                }
            }
            catch (Exception)
            {

                FixedService.GenericItem objHorAuxError = new FixedService.GenericItem();
                objHorAuxError.Codigo = "-1";
                objHorAuxError.Descripcion = Functions.GetValueFromConfigFile("strMensajeErrorWs", KEY.AppSettings("strConstArchivoSIACUTHFCConfigMsg"));
                listaHorarios.Add(objHorAuxError);
                return listaHorarios;

            }
            Claro.Web.Logging.Info("Session: " + strIdSession, "Transaction: ObtieneFranjasHorarias ", "sale de ObtieneFranjasHorarias");

            return listaHorarios;
        }

        public static string RegistrarHistorialConsultasEta(string IdSession, List<BEETAEntidadcapacidadType> ocapacity, string fecha, int cod_zona, string cod_plano, string codTipoOrden, string codSubTipoOrden, string vTiempoTrabajo, string strUbicacion, ref string idRequest)
        {
            Claro.Web.Logging.Info("Session: " + IdSession, "Transaction: RegistrarHistorialConsultasEta ", "entra a RegistrarHistorialConsultasEta");

            string strResp = string.Empty;
            string strMsg = string.Empty;
            int vIdReq = 0;

            AuditRequestFixed audit = App_Code.Common.CreateAuditRequest<AuditRequestFixed>(IdSession);
            FixedTransacService.RegisterEtaRequest objRegisterEtaRequest = new FixedTransacService.RegisterEtaRequest();
            FixedTransacService.RegisterEtaResponse objRegisterEtaResponse = new FixedTransacService.RegisterEtaResponse();
            objRegisterEtaRequest.vfecha_venta = fecha;
            objRegisterEtaRequest.vcod_zona = cod_zona;
            objRegisterEtaRequest.vcod_plano = cod_plano;
            objRegisterEtaRequest.vtipo_orden = codTipoOrden;
            objRegisterEtaRequest.vsubtipo_orden = codSubTipoOrden;
            objRegisterEtaRequest.vtiempo_trabajo = Convert.ToInt(vTiempoTrabajo);
            objRegisterEtaRequest.vidreturn = vIdReq;
            objRegisterEtaRequest.audit = audit;


            try
            {
                vIdReq = Claro.Web.Logging.ExecuteMethod<int>(() =>
                {
                    return new FixedTransacServiceClient().registraEtaRequest(objRegisterEtaRequest);
                });


                try
                {
                    foreach (BEETAEntidadcapacidadType item in ocapacity)
                    {
                        objRegisterEtaResponse.vidconsulta = vIdReq;
                        objRegisterEtaResponse.vdia = item.Fecha;
                        objRegisterEtaResponse.vfranja = item.EspacioTiempo;
                        objRegisterEtaResponse.vestado = Convert.ToInt(item.Disponible);
                        objRegisterEtaResponse.vquota = Convert.ToInt(item.Cuota);
                        objRegisterEtaResponse.vid_bucket = (item.Ubicacion == null ? string.Empty : item.Ubicacion);
                        objRegisterEtaResponse.vresp = string.Empty;

                        strResp = Claro.Web.Logging.ExecuteMethod<string>(() =>
                        {
                            return new FixedTransacServiceClient().registraEtaResponse(objRegisterEtaResponse);
                        });

                    }
                }
                catch (Exception ex)
                {
                    Claro.Web.Logging.Error(objRegisterEtaRequest.audit.Session, objRegisterEtaRequest.audit.transaction, ex.Message);
                }


                idRequest = vIdReq.ToString();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRegisterEtaRequest.audit.Session, objRegisterEtaRequest.audit.transaction, ex.Message);
                throw new Exception(objRegisterEtaRequest.audit.transaction);
            }

            Claro.Web.Logging.Info("Session: " + IdSession, "Transaction: RegistrarHistorialConsultasEta ", "sale de RegistrarHistorialConsultasEta, mensaje: " + strMsg);

            return strMsg;
        }

        //PROY-32650-Retencion/Fidelización II
        public JsonResult GetInstallationCost(string strIdSession, int hfc_lte)
        {
            string strCostoInstalacion = string.Empty;
            string strMessageOutput = string.Empty;
            string strSalida = string.Empty;

            Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ReceiptsResponse objGenericRes = new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ReceiptsResponse();
            Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest objaudit =
                App_Code.Common.CreateAuditRequest<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.AuditRequest>(strIdSession);

            Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ReceiptsRequest objGenericReq =
                new Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ReceiptsRequest()
                {
                    audit = objaudit,
                    vCODCLIENTE = ConfigurationManager.AppSettings("strCostoInstalacionSA").Split('|')[hfc_lte].ToString()
                };

            Claro.Web.Logging.Info(strIdSession, objaudit.transaction, "Begin GetInstallationCost");
            try
            {
                objGenericRes =
                    Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService.ReceiptsResponse>(() =>
                    {
                        return _oServiceCommon.GetParamsBSCS(objGenericReq);
                    });

                if (objGenericRes != null && objGenericRes.LstReceipts.Count > 0)
                {
                    strCostoInstalacion = objGenericRes.LstReceipts.Single(x => x.Codigo == ConfigurationManager.AppSettings("strCostoInstalacionSA").Split('|')[1].ToString()).Codigo2;
                    strMessageOutput = Claro.SIACU.Transac.Service.Constants.strCero;
                    Claro.Web.Logging.Info(strIdSession, objaudit.transaction, "Total Registros : " + objGenericRes.LstReceipts.Count);
                }
                else if (objGenericRes.LstReceipts == null || objGenericRes.LstReceipts.Count == 0)
                {
                    strMessageOutput = ConfigurationManager.AppSettings("strMessageErrorCostoInst").Split('|')[0];
                }
                else
                {
                    strMessageOutput = ConfigurationManager.AppSettings("strMessageErrorCostoInst").Split('|')[1];
                }
            }
            catch (Exception ex)
            {
                strMessageOutput = ConfigurationManager.AppSettings("strMessageErrorCostoInst").Split('|')[1];

                Claro.Web.Logging.Error(strIdSession, objaudit.transaction, ex.Message);
            }


            Claro.Web.Logging.Info(strIdSession, objaudit.transaction, "End GetInstallationCost - Costo de instalacion: " + strCostoInstalacion);
            return Json(new { data = strCostoInstalacion + "|" + strMessageOutput });

        }

        public JsonResult GetAmountInstall(string strIdSession, string strTantoPorciento, string strCostoInst)
        {
            string strCostoInstal = "";
            double porcentajeDescuento = 0;

            double dblNuevoCostoInstal = 0;
            double dblCostoDescuento = 0;
            try
            {
                strTantoPorciento = strTantoPorciento.Substring(0, strTantoPorciento.IndexOf('%')).Trim();
                porcentajeDescuento = (Convert.ToDouble(strTantoPorciento) / 100);

                dblNuevoCostoInstal = Double.Parse(strCostoInst) - (Double.Parse(strCostoInst) * porcentajeDescuento);

                dblCostoDescuento = (Double.Parse(strCostoInst) * porcentajeDescuento);//Porcentaje del costo de instalacion que se envia al servicio.

                //strCostoInstal = String.Format("{0:0.00}", dblCostoInstal.ToString());

                Claro.Web.Logging.Info(strIdSession, "GetAmountInstall", "Costo de Instalacion con descuento: " + strCostoInstal);
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, "GetAmountInstall", "Error al obtener monto de instalación. Error: " + ex.Message);

            }

            return Json(new { data = new { nuevoCostoInstal = dblNuevoCostoInstal.ToString("N2"), costoDescuentoInstal = dblCostoDescuento.ToString("N2") } });
        }
        #endregion

        public CommonTransacService.FileDefaultImpersonationResponseCommon GetfileDefaultImpersonation(FileDefaultImpersonationRequestCommon objRequest)
        {
            return _oServiceCommon.GetfileDefaultImpersonation(objRequest);
        }
        public GenerateConstancyResponseCommon GetGenerateContancyNamePDF(string idSession, ParametersGeneratePDF parameters, string StrNombreArchivoPDF)
        {
            parameters.StrServidorGenerarPDF = KEY.AppSettings("strServidorGenerarPDF");
            parameters.StrServidorLeerPDF = KEY.AppSettings("strServidorLeerPDF");
            parameters.StrCarpetaPDFs = KEY.AppSettings("strCarpetaPDFs");
            
            GenerateConstancyRequestCommon request = new GenerateConstancyRequestCommon()
            {
                ParametersGeneratePDFGeneric = parameters,
                audit = Common.CreateAuditRequest<CommonTransacService.AuditRequest>(idSession)
            };
            
            Logging.Info(idSession, "", "Begin GetGenerateContancyPDF");
            GenerateConstancyResponseCommon objResponse = Logging.ExecuteMethod<GenerateConstancyResponseCommon>(() => { return _oServiceCommon.GetGenerateContancyNamePDF(request, StrNombreArchivoPDF); });
            Logging.Info(idSession, "", " Generated:  " + objResponse.Generated.ToString());

            string strTerminacionPDF = KEY.AppSettings("strTerminacionPDF");  
            if (objResponse.Generated)
            {
                string strFechaTransaccion = DateTime.Today.ToShortDateString().Replace("/", "_");
                string strNamePDF = string.Format("{0}{1}{2}{3}_{4}_{5}_{6}.pdf", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion, parameters.StrCasoInter, strFechaTransaccion, StrNombreArchivoPDF, strTerminacionPDF);
                string strNamePath = string.Format("{0}{1}{2}", parameters.StrServidorLeerPDF, parameters.StrCarpetaPDFs, parameters.StrCarpetaTransaccion);
                string strDocumentName = string.Format("{0}_{1}_{2}_{3}", parameters.StrCasoInter, strFechaTransaccion, StrNombreArchivoPDF, strTerminacionPDF);
                objResponse.FullPathPDF = strNamePDF;
                objResponse.Path = strNamePath;
                objResponse.Document = strDocumentName;
                Logging.Info(idSession, "", " FullPathPDF:    " + objResponse.FullPathPDF);
            }
            else
            {
                objResponse.FullPathPDF = string.Empty;
                objResponse.Path = string.Empty;
                objResponse.Document = string.Empty;
            }

            Logging.Info(idSession, "", string.Format("End GetGenerateContancyPDF result: {0}, FullPathPDF: {1}", objResponse.Generated.ToString(), objResponse.FullPathPDF));
            return objResponse;
        }
    }
}
