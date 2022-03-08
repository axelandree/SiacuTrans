using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Claro.SIACU.Data.Transac.Service.Configuration;
using FIXED_BPEL = Claro.SIACU.ProxyService.Transac.Service.SIACUBPEL.ChangeNumber;
using FIXED = Claro.SIACU.Entity.Transac.Service.Fixed;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using KEY = Claro.ConfigurationManager;

namespace Claro.SIACU.Data.Transac.Service.Fixed
{
    public class ChangePhoneNumber
    {
        public static string GetExecuteChangeNumber(FIXED.GetPhoneNumber.PhoneNumberRequest objRequest, COMMON.InsertInteract oInteract,
            ref string strPDFRoute, ref string strResponseCode, ref string strResponseMessage)
        {
            string strNewPhone = string.Empty;
            string strFaultMsg = string.Empty;
            FIXED_BPEL.ejecutarCambioNumeroFijaResponseType oResponse = new FIXED_BPEL.ejecutarCambioNumeroFijaResponseType();
            FIXED_BPEL.parametrosPlusType oPlusType = new FIXED_BPEL.parametrosPlusType();
            oPlusType.registrationReason = objRequest.CONTRACT_ID;
            oPlusType.typeDocument = objRequest.CUSTOMER_TYPE;
            oPlusType.firstName = objRequest.CONTACT;
            oPlusType.reason = Claro.Constants.NumberZeroString;
            oPlusType.claroLdn1 = objRequest.DOCUMENT;
            oPlusType.inter22 = objRequest.NRO_TELEF;
            oPlusType.flagOther = objRequest.FLAG_FIDEL_TRANS;
            //oPlusType.inter19 = objRequest.COST_TRANS;
            oPlusType.inter19 = objRequest.COST_TRANS_IGV;
            oPlusType.flagRegistered = objRequest.FLAG_LOCU;
            oPlusType.flagCharge = objRequest.FLAG_FIDEL_LOCU;
            //oPlusType.adjustmentAmount = objRequest.COST_LOCU;
            oPlusType.adjustmentAmount = objRequest.COST_LOCU_IGV;
            oPlusType.emailConfirmation = objRequest.FLAG_SEND_EMAIL;
            oPlusType.email = objRequest.EMAIL;
            oPlusType.inter15 = objRequest.POINT_ATTENTION;
            oPlusType.inter30 = objRequest.NOTES;
            oPlusType.address = Claro.Constants.NumberZeroString;
            oPlusType.address5 = Claro.Constants.NumberZeroString;
            oPlusType.adjustmentReason = Claro.Constants.NumberZeroString;
            oPlusType.amountUnit = Claro.Constants.NumberZeroString;
            oPlusType.basket = Claro.Constants.NumberZeroString;
            oPlusType.birthday = "03/03/2000";
            oPlusType.chargeAmount = Claro.Constants.NumberZeroString;
            oPlusType.city = Claro.Constants.NumberZeroString;
            oPlusType.clarifyInteraction = Claro.Constants.NumberZeroString;
            oPlusType.claroLdn2 = Claro.Constants.NumberZeroString;
            oPlusType.claroLdn3 = Claro.Constants.NumberZeroString;
            oPlusType.claroLdn4 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal1 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal2 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal3 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal4 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal5 = Claro.Constants.NumberZeroString;
            oPlusType.claroLocal6 = Claro.Constants.NumberZeroString;
            oPlusType.claroNumber = Claro.Constants.NumberZeroString;
            oPlusType.contactPhone = Claro.Constants.NumberZeroString;
            oPlusType.contactSex = Claro.Constants.NumberZeroString;
            oPlusType.department = Claro.Constants.NumberZeroString;
            oPlusType.district = Claro.Constants.NumberZeroString;
            oPlusType.dniLegalRep = Claro.Constants.NumberZeroString;
            oPlusType.documentNumber = Claro.Constants.NumberZeroString;
            oPlusType.expireDate = "03/03/2000";
            oPlusType.fax = Claro.Constants.NumberZeroString;
            oPlusType.fixedNumber = Claro.Constants.NumberZeroString;
            oPlusType.flagChangeUser = Claro.Constants.NumberZeroString;
            oPlusType.flagLegalRep = Claro.Constants.NumberZeroString;
            oPlusType.flagTitular = Claro.Constants.NumberZeroString;
            oPlusType.iccid = Claro.Constants.NumberZeroString;
            oPlusType.imei = Claro.Constants.NumberZeroString;
            oPlusType.inter1 = Claro.Constants.NumberZeroString;
            oPlusType.inter10 = Claro.Constants.NumberZeroString;
            oPlusType.inter11 = Claro.Constants.NumberZeroString;
            oPlusType.inter12 = Claro.Constants.NumberZeroString;
            oPlusType.inter13 = Claro.Constants.NumberZeroString;
            oPlusType.inter14 = Claro.Constants.NumberZeroString;
            oPlusType.inter16 = Claro.Constants.NumberZeroString;
            oPlusType.inter17 = Claro.Constants.NumberZeroString;
            oPlusType.inter18 = Claro.Constants.NumberZeroString;
            oPlusType.inter2 = Claro.Constants.NumberZeroString;

            // CAMBIO
            if (objRequest.FULL_NAME.Length >= 40)
            {
                oPlusType.inter20 = objRequest.FULL_NAME.Substring(Claro.Constants.NumberZero, Claro.Constants.NumberForty-1);
            }
            else
            {
                oPlusType.inter20 = objRequest.FULL_NAME; 
            }

            oPlusType.inter21 = Claro.Constants.NumberZeroString;
            oPlusType.inter23 = Claro.Constants.NumberZeroString;
            oPlusType.inter24 = Claro.Constants.NumberZeroString;
            oPlusType.inter25 = Claro.Constants.NumberZeroString;
            oPlusType.inter26 = Claro.Constants.NumberZeroString;
            oPlusType.inter27 = Claro.Constants.NumberZeroString;
            oPlusType.inter28 = Claro.Constants.NumberZeroString;
            oPlusType.inter29 = string.Empty;
            oPlusType.inter3 = Claro.Constants.NumberZeroString;
            oPlusType.inter4 = Claro.Constants.NumberZeroString;
            oPlusType.inter5 = Claro.Constants.NumberZeroString;
            oPlusType.inter6 = Claro.Constants.NumberZeroString;
            oPlusType.inter7 = DateTime.Today.ToShortDateString();
            oPlusType.inter8 = Claro.Constants.NumberZeroString;
            oPlusType.inter9 = Claro.Constants.NumberZeroString;
            oPlusType.lastName = Claro.Constants.NumberZeroString;
            oPlusType.lastNameRep = Claro.Constants.NumberZeroString;
            oPlusType.ldiNumber = Claro.Constants.NumberZeroString;
            oPlusType.lotCode = Claro.Constants.NumberZeroString;
            oPlusType.maritalStatus = Claro.Constants.NumberZeroString;
            oPlusType.model = Claro.Constants.NumberZeroString;
            oPlusType.month = Claro.Constants.NumberZeroString;
            oPlusType.nameLegalRep = Claro.Constants.NumberZeroString;
            oPlusType.occupation = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLdn1 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLdn2 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLdn3 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLdn4 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal1 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal2 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal3 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal4 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal5 = Claro.Constants.NumberZeroString;
            oPlusType.oldClaroLocal6 = Claro.Constants.NumberZeroString;
            oPlusType.oldDocNumber = Claro.Constants.NumberZeroString;
            oPlusType.oldFirstName = Claro.Constants.NumberZeroString;
            oPlusType.oldFixedNumber = Claro.Constants.NumberZeroString;
            oPlusType.oldFixedPhone = Claro.Constants.NumberZeroString;
            oPlusType.oldLastName = Claro.Constants.NumberZeroString;
            oPlusType.oldLdiNumber = Claro.Constants.NumberZeroString;
            oPlusType.operationType = Claro.Constants.NumberZeroString;
            oPlusType.ostNumber = Claro.Constants.NumberZeroString;
            oPlusType.otherDocNumber = Claro.Constants.NumberZeroString;
            oPlusType.otherFirstName = Claro.Constants.NumberZeroString;
            oPlusType.otherLastName = Claro.Constants.NumberZeroString;
            oPlusType.otherPhone = Claro.Constants.NumberZeroString;
            oPlusType.phoneLegalRep = Claro.Constants.NumberZeroString;
            oPlusType.plusInter2Interact = Claro.Constants.NumberZeroString;
            oPlusType.position = Claro.Constants.NumberZeroString;
            oPlusType.referenceAddress = Claro.Constants.NumberZeroString;
            oPlusType.referencePhone = Claro.Constants.NumberZeroString;
            oPlusType.zipCode = Claro.Constants.NumberZeroString;


            FIXED_BPEL.parametrosPrincipalType oPrincipalType = new FIXED_BPEL.parametrosPrincipalType();
            oPrincipalType.agente = objRequest.Audit.UserName;
            oPrincipalType.codPlano = string.Empty;
            oPrincipalType.coId = objRequest.CONTRACT_ID;
            oPrincipalType.flagCaso = Claro.Constants.NumberOneString;
            oPrincipalType.hechoEnUno = Claro.Constants.NumberZeroString;
            oPrincipalType.inconven = string.Empty;
            oPrincipalType.inconvenCode = string.Empty;
            oPrincipalType.metodoContacto = objRequest.METHOD;
            oPrincipalType.notas = objRequest.NOTES;
            oPrincipalType.resultado = objRequest.RESULT;
            oPrincipalType.servaFect = string.Empty;
            oPrincipalType.servaFectCode = string.Empty;
            oPrincipalType.tipo = objRequest.TYPE;
            oPrincipalType.clase = objRequest.CLASS;
            oPrincipalType.subClase = objRequest.SUBCLASS;
            oPrincipalType.tipoInter = objRequest.TIPO_INTER;
            oPrincipalType.usrProceso = objRequest.USER_PROCESS;
            oPrincipalType.valor1 = Claro.Constants.NumberThreeString;
            oPrincipalType.valor2 = Claro.Constants.NumberThreeString;

            FIXED_BPEL.obtenerClientesType oGetCustomer = new FIXED_BPEL.obtenerClientesType();
            oGetCustomer.msisdn = objRequest.CUSTIDOBJID;
            oGetCustomer.contactObjId = string.Empty;
            oGetCustomer.flagReg = Claro.Constants.NumberOneString;
            oGetCustomer.account = string.Empty;

            FIXED_BPEL.HeaderResponseType oHeaderResponse =
            Claro.Web.Logging.ExecuteMethod<FIXED_BPEL.HeaderResponseType>
            (objRequest.Audit.Session, objRequest.Audit.Transaction, Configuration.ServiceConfiguration.FIXED_CHANGE_NUMBER, () =>
            {
                try
                {
                    return Configuration.ServiceConfiguration.FIXED_CHANGE_NUMBER.ejecutarCambioNumeroFija(
                    new FIXED_BPEL.HeaderRequestType()
                    {
                        canal = "WEB",
                        idAplicacion = "1234",
                        usuarioAplicacion = objRequest.Audit.UserName,
                        usuarioSesion = objRequest.Audit.UserName,
                        idTransaccionESB = objRequest.Audit.Transaction,
                        idTransaccionNegocio = objRequest.Audit.Transaction,
                        fechaInicio = DateTime.Today,
                        nodoAdicional = "1"
                    },
                    new FIXED_BPEL.ejecutarCambioNumeroFijaRequestType()
                    {
                        cantidadTelef = objRequest.NUMBER_PHONES,
                        clasifRed = objRequest.CLASIF_RED,
                        tipoCliente = objRequest.CUSTOMER_TYPE,
                        codigoNacional = objRequest.NATIONAL_CODE,
                        tipoNroTelef = objRequest.TYPE_NUMBER,
                        codigoHlr = objRequest.HLR_CODE,
                        nroTelef = objRequest.NRO_TELEF,
                        tipoNumero = objRequest.TYPE_PHONE,
                        contratID = objRequest.CONTRACT_ID,
                        customerID = objRequest.CUSTOMER_ID,
                        flagOccFidelTrans = objRequest.FLAG_FIDEL_TRANS,
                        importeOccTrans = objRequest.COST_TRANS,
                        flagLocu = objRequest.FLAG_LOCU,
                        flagOccFidelLocu = objRequest.FLAG_FIDEL_LOCU,
                        importeOccLocu = objRequest.COST_LOCU,
                        flagPlantTipi = objRequest.FLAG_PLAN_TIPI,
                        flagEnvioCorreo = objRequest.FLAG_SEND_EMAIL,
                        correo = objRequest.EMAIL,
                        flagContingenciaTipi = ConfigurationManager.AppSettings("gConstContingenciaClarify_SIACU"),
                        parametrosPlus = oPlusType,
                        parametrosPrincipal = oPrincipalType,
                        parametrosCliente = oGetCustomer,
                        auditRequest = new FIXED_BPEL.AuditRequest()
                        {
                            idTransaccion = objRequest.Audit.Transaction,
                            ipAplicacion = objRequest.Audit.IPAddress,
                            nombreAplicacion = objRequest.Audit.ApplicationName,
                            usuarioAplicacion = objRequest.Audit.UserName
                        }
                    },
                    out oResponse);
                }
                catch (FaultException<FIXED_BPEL.ClaroFault> ex)
                {
                    strFaultMsg = ex.Detail.descripcionError;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, strFaultMsg);
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (FaultException<ExceptionDetail> ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, strFaultMsg);
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (FaultException ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, strFaultMsg);
                    return null;
                    throw new Exception(strFaultMsg);
                }
                catch (Exception ex)
                {
                    strFaultMsg = ex.Message;
                    Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, strFaultMsg);
                    return null;
                    throw new Exception(ex.Message);
                }
                    
            });

            if (strFaultMsg.Equals(string.Empty))
            {
                strResponseCode = oResponse.auditResponse.codigoRespuesta;
                strResponseMessage = oResponse.auditResponse.mensajeRespuesta;
            }
            else
            {
                strResponseCode = Claro.Constants.NumberOneNegativeString;
                strResponseMessage = KEY.AppSettings("strMsgErrorFaultBPel");
            }
            

            if (oResponse != null && oResponse.nroTelefNuev != null)
            {
                strNewPhone = oResponse.nroTelefNuev;
                strPDFRoute = oResponse.rutaConstancia;
            }

            return strNewPhone;
        }
    }
}
