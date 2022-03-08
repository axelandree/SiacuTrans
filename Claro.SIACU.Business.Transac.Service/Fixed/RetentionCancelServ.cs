using System;
using System.Collections.Generic;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetCaseInsert;
using ConsultClienteHFCWS = Claro.SIACU.ProxyService.Transac.Service.WSClienteHFC;
using System.Linq;
#region Proy-32650
using Claro.SIACU.Entity.Transac.Service.Fixed.GetStatusAccountAOC;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetQueryDebt;
using Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus;
using Claro.SIACU.Entity.Transac.Service.Fixed.GetRegistarInstaDecoAdiHFC;
#endregion

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class RetentionCancelServ
    {
        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetListarAcciones(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objAccionRequest)
        {
            var objAccionResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objAccionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstAcciones = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstAcciones = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetAcciones(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction,objAccionRequest.vNivel);
                });
                objAccionResponse.AccionTypes = lstAcciones;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vNivel.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objAccionResponse;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotCancelacion(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objMotCancelacionRequest)
        {
            var objMotCancelacionResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objMotCancelacionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstMotCancelacion = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstMotCancelacion = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objMotCancelacionRequest.Audit.Session, objMotCancelacionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetMotCancelacion(objMotCancelacionRequest);
                });
                objMotCancelacionResponse.AccionTypes = lstMotCancelacion;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotCancelacionRequest.vEstado.ToString(),objMotCancelacionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objMotCancelacionResponse;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetSubMotiveCancel(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objAccionRequest)
        {
            var objSubMotResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objSubMotResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstSubMotivo = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstSubMotivo = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetSubMotiveCancel(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, objAccionRequest.vIdMotive);
                });
                objSubMotResponse.AccionTypes = lstSubMotivo.Where(x => x.Estado == "1").ToList<EntitiesFixed.GenericItem>();
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vIdMotive.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objSubMotResponse;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetTypeWork(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objTypeRequest)
        {
            var objTypeWorkResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objTypeWorkResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstTypeWork = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstTypeWork = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetTypeWork(objTypeRequest.Audit.Session, objTypeRequest.vIdTypeWork, objTypeRequest.Audit.Transaction);
                });
                objTypeWorkResponse.AccionTypes = lstTypeWork;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objTypeWorkResponse;
        }

        public static RetentionCancelServicesResponse GetSubTypeWork(RetentionCancelServicesRequest objTypeRequest)
        {
            var objTypeWorkResponse = new RetentionCancelServicesResponse();


            objTypeWorkResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstTypeWork = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstTypeWork = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetSubTypeWork(objTypeRequest.Audit.Session, objTypeRequest.vIdTypeWork, objTypeRequest.Audit.Transaction);
                });
                objTypeWorkResponse.AccionTypes = lstTypeWork;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeRequest.Audit.Session, objTypeRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objTypeWorkResponse;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMotiveSOT(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objMotiveSOTRequest)
        {
            var objMotiveSOTResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objMotiveSOTResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstMotiveSOT = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstMotiveSOT = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetMotiveSOT(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction);
                });
                objMotiveSOTResponse.AccionTypes = lstMotiveSOT;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveSOTRequest.Audit.Session, objMotiveSOTRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objMotiveSOTResponse;
        }

        public static EntitiesFixed.GetCaseInsert.AddDayWorkResponse GetAddDayWork(EntitiesFixed.GetCaseInsert.AddDayWorkRequest objRequest)
        {
            var objResponse = new AddDayWorkResponse();


            //objResponse = new AddDayWorkResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<AddDayWorkResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetAddDayWork(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.FechaInicio, objRequest.NumeroDias, objRequest.FechaResultado, objRequest.CodError, objRequest.DesError);
                });
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        public static Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainParameterTerminalTPI(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();

            objResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstParameter = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstParameter = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetObtainParameterTerminalTPI(objRequest.Audit.Session.ToString(), objRequest.Audit.Transaction.ToString(),Convert.ToInt(objRequest.ParameterID), objRequest.Descripcion);
                });
                objResponse.AccionTypes = lstParameter;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetSoloTFIPostpago(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();

            objResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstParameter = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstParameter = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetObtainParameterTerminalTPI(objRequest.Audit.Session.ToString(), objRequest.Audit.Transaction.ToString(), Convert.ToInt(objRequest.ParameterID), objRequest.Descripcion);
                });
                objResponse.AccionTypes = lstParameter;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }
        public static Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse ObtenerDatosBSCSExt(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();

            try
            {
                double NroFacturas = 0;
                double CargoFijoActual = 0;
                double CargoFijoNuevoPlan = 0;

                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.ObtenerDatosBSCSExt(objRequest.Audit.Session, objRequest.Audit.Transaction,
                        objRequest.NroTelefono, objRequest.CodNuevoPlan, ref NroFacturas, ref CargoFijoActual, ref CargoFijoNuevoPlan);
                });


                objResponse.NroFacturas = NroFacturas;
                objResponse.CargoFijoActual = CargoFijoActual;
                objResponse.CargoFijoNuevoPlan = CargoFijoNuevoPlan;
   
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        public static Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesResponse GetObtainPenalidadExt(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();

            
            
            try
            {
                double AcuerdoIdSalida = 0;
                double DiasPendientes =0;
                double CargoFijoDiario = 0;
                double PrecioLista = 0;
                double PrecioVenta = 0;
                double PenalidadPCS = 0;
                double PenalidaAPADECE = 0;

                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {

                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetObtainPenalidadExt(objRequest.Audit.Session, objRequest.Audit.Transaction, 
                        objRequest.NroTelefono, objRequest.FechaPenalidad,objRequest.NroFacturas, 
                        objRequest.CargoFijoActual, objRequest.CargoFijoNuevoPlan, objRequest.DiasxMes, 
                        objRequest.CodNuevoPlan,ref AcuerdoIdSalida,ref DiasPendientes,ref CargoFijoDiario,
                        ref PrecioLista,ref PrecioVenta,ref PenalidadPCS,ref PenalidaAPADECE);
                });


                objResponse.AcuerdoIdSalida = AcuerdoIdSalida;
                objResponse.DiasPendientes = DiasPendientes;
                objResponse.CargoFijoDiario = CargoFijoDiario;
                objResponse.PrecioLista = PrecioLista;
                objResponse.PrecioVenta = PrecioVenta;
                objResponse.PenalidadPCS = PenalidadPCS;
                objResponse.PenalidaAPADECE = PenalidaAPADECE;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetValidateCustomerID.ValidateCustomerIdResponse GetValidateCustomerId(Entity.Transac.Service.Fixed.GetValidateCustomerID.ValidateCustomerIdRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse();


            objResponse = new EntitiesFixed.GetValidateCustomerID.ValidateCustomerIdResponse();


            try
            {
                
                int vCONTACTOBJID = 0;
                string  strflgResult = string.Empty;
                string strMSError = string.Empty;

                objResponse.resultado = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetValidateCustomerId(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.Phone, ref vCONTACTOBJID, ref strflgResult, ref strMSError);
                });

                objResponse.ContactObjID = vCONTACTOBJID;
                objResponse.FlgResult = strflgResult;
                objResponse.MsError = strMSError;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }


        public static EntitiesFixed.GetCustomer.CustomerResponse GetRegisterCustomerId(Entity.Transac.Service.Fixed.Customer objRequest)
        {
            var objResponse = new EntitiesFixed.GetCustomer.CustomerResponse();

            try
            {

                string strflgResult = string.Empty;
                string strMSError = string.Empty;

                objResponse.Resultado = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetRegisterCustomerId(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest, ref strflgResult, ref strMSError);
                });
                objResponse.vFlagConsulta = strflgResult;
                objResponse.rMsgText = strMSError;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }



        public static string GetRegisterEtaSelection(Entity.Transac.Service.Fixed.GetRegisterEtaSelection.RegisterEtaSelectionRequest objRequest)
        {
            var resultado = string.Empty;

            try
            {

                string strMSError = string.Empty;

                resultado = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetRegisterEtaSelection(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest, ref strMSError);
                });
                
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return resultado;
        }

        public static CaseInsertResponse GetCaseInsert(CaseInsertRequest objRequest)
        {
           CaseInsertResponse oResponse = new CaseInsertResponse();

            try
            {

                string rCasoId=string.Empty;
                string rFlagInsercion=string.Empty;
                string rMsgText = string.Empty;

                oResponse.rMsgText = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetCaseInsert(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest, ref rCasoId, ref rFlagInsercion, ref rMsgText);
                });
                oResponse.rCasoId = rCasoId;
                oResponse.rFlagInsercion = rFlagInsercion;

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }

        public static RetentionCancelServicesResponse GetApadeceCancelRet(RetentionCancelServicesRequest objRequest)
        {
            var objResponse = new RetentionCancelServicesResponse();

            try
            {
                double rdbValorApadece = 0;
                string rintCodError = string.Empty;
                string rp_msg_text = string.Empty;
                
                objResponse = Claro.Web.Logging.ExecuteMethod<EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetApadeceCancelRet(objRequest.Audit.Session, objRequest.Audit.Transaction, Convert.ToInt(objRequest.NroTelefono), objRequest.CodId, ref rdbValorApadece, ref rintCodError, ref rp_msg_text);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }

        public static bool GetDesactivatedContract( Customer objRequestCliente)
        {
            bool resultado = false;

            resultado = Claro.Web.Logging.ExecuteMethod<bool>(objRequestCliente.Audit.Session, objRequestCliente.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.RetentionCancelServ.GetDesactivatedContract(objRequestCliente);
            });

            return resultado;

        
        }

        public static EntitiesFixed.GetCaseInsert.CaseInsertResponse GetCreateCase(EntitiesFixed.GetCaseInsert.CaseInsertRequest oRequest)
        {
            CaseInsertResponse objResponse = new CaseInsertResponse();
            try
            {

                objResponse = Claro.Web.Logging.ExecuteMethod(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetCreateCase(oRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        
        }

        public static EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse GetMotiveSOTByTypeJob(EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobRequest objRequest)
        {
            EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse objResponse = new EntitiesFixed.GetMotiveSOTByTypeJob.MotiveSOTByTypeJobResponse();
            try
            {
                objResponse.List = Claro.Web.Logging.ExecuteMethod<List<Claro.SIACU.Entity.Transac.Service.Fixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetMotiveSOTByTypeJob(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.tipTra);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objResponse;
        }



        public static bool GetDesactivatedContract_LTE(Customer objRequest)
        {
            bool resultado = false;

            resultado = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
            {
                return Data.Transac.Service.Fixed.RetentionCancelServ.GetDesactivatedContract_LTE(objRequest);
            });

            return resultado;


        }

        #region Proy-32650
        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetMonths(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objAccionRequest)
        {
            var objAccionResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();


            objAccionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstAcciones = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstAcciones = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetMonths(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, objAccionRequest.vBonoID, objAccionRequest.Tipo);
                });
                objAccionResponse.AccionTypes = lstAcciones;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vNivel.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objAccionResponse;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetListDiscount(Entity.Transac.Service.Fixed.GetCaseInsert.RetentionCancelServicesRequest objAccionRequest)
        {
            var objAccionResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();

            string error1 = string.Empty, error2 = string.Empty;
            objAccionResponse.AccionTypes = new List<EntitiesFixed.GenericItem>();
            List<EntitiesFixed.GenericItem> lstAcciones = new List<EntitiesFixed.GenericItem>();
            try
            {
                lstAcciones = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.GetListDiscount(objAccionRequest.Audit.Session, objAccionRequest.Audit.Transaction, ref error1, ref error2);
                });
                objAccionResponse.AccionTypes = lstAcciones;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAccionRequest.vNivel.ToString(), objAccionRequest.vtransaction, ex.Message);
                throw ex;
            }

            return objAccionResponse;

        }


        public static EntitiesFixed.GetPlanServices.PlanServicesResponse GetRetentionRate(EntitiesFixed.GetPlanServices.PlanServicesRequest objRequest)
        {
            string estado = string.Empty;
            var objResponse = new EntitiesFixed.GetPlanServices.PlanServicesResponse();
            List<EntitiesFixed.PlanService> objListPlanService = null;

            try
            {
                objListPlanService = Web.Logging.ExecuteMethod<List<Entity.Transac.Service.Fixed.PlanService>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.RetentionCancelServ.GetRetentionRate(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.ArrServices);
                    });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.ArrServices.ToString(), objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            objResponse.LstPlanServices = objListPlanService;

            return objResponse;
        }

        public static bool RegisterBonoDiscount(Entity.Transac.Service.Fixed.RegisterBonoDiscount.RegisterBonoDiscountRequest request)
        {
            bool result = false;
            try
            {
                result = Web.Logging.ExecuteMethod(request.Audit.Session, request.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.RegisterBonoDiscount(request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, ex.Message);
                throw ex;
            }
            return result;
        }

        public static EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse GetTotalInversion(EntitiesFixed.GetCaseInsert.RetentionCancelServicesRequest objRequest)
        {
            string strCodError = string.Empty, strcodMsg = string.Empty, strTotalDesc = string.Empty;
            var objResponse = new EntitiesFixed.GetCaseInsert.RetentionCancelServicesResponse();
            try
            {
                objResponse.Resultado = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.RetentionCancelServ.GetTotalInversion(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.CodId.ToString(), ref strTotalDesc, ref strCodError, ref strcodMsg);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.CodId.ToString(), objRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            objResponse.Message = strcodMsg;
            objResponse.CodMessage = strCodError;
            objResponse.CargoFijoActual = Double.Parse(string.IsNullOrEmpty(strTotalDesc) ? "0" : strTotalDesc);

            return objResponse;
        }

        public static bool ActualizarDatosMenores(Entity.Transac.Service.Fixed.Customer request)
        {
            bool result = false;
            result = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.ActualizarDatosMenores(request);
            });
            return result;
        }

        public static bool ActualizarDatosClarify(Entity.Transac.Service.Fixed.Customer request)
        {
            bool result = false;
            result = Claro.Web.Logging.ExecuteMethod<bool>(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.ActualizarDatosClarify(request);
            });
            return result;
        }

        public static string RegisterNuevaInteraccion(Entity.Transac.Service.Fixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionRequest request)
        {
            string result = Claro.Web.Logging.ExecuteMethod<string>(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.RegisterNuevaInteraccion(request);
            });
            return result;
        }

        public static string RegisterNuevaInteraccionPlus(Entity.Transac.Service.Fixed.RegisterNuevaInteraccion.RegisterNuevaInteraccionPlusRequest request)
        {
            string result = Claro.Web.Logging.ExecuteMethod<string>(request.Audit.Session, request.Audit.Transaction, () =>
            {
                return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.RegisterNuevaInteraccionPlus(request);
            });
            return result;
        }

        public static QueryDebtResponse GetDebtQuery(QueryDebtRequest objDebtQueryRequest)
        {
            var objDebtQueryResponse = new QueryDebtResponse();

            objDebtQueryResponse = Claro.Web.Logging.ExecuteMethod<QueryDebtResponse>(objDebtQueryRequest.Audit.Session, objDebtQueryRequest.Audit.Transaction,
            () =>
            {
                return Data.Transac.Service.Fixed.RetentionCancelServ.GetDebtQuery(objDebtQueryRequest);
            });
            return objDebtQueryResponse;
        }

        public static AccountStatusResponse GetStatusAccountFixedAOC(AccountStatusRequest objAccountStatusRequest)
        {
            var objAccountStatusResponse = new AccountStatusResponse();

            objAccountStatusResponse = Claro.Web.Logging.ExecuteMethod<AccountStatusResponse>(objAccountStatusRequest.Audit.Session, objAccountStatusRequest.Audit.Transaction,
            () =>
            {
                return Data.Transac.Service.Fixed.RetentionCancelServ.GetStatusAccountFixedAOC(objAccountStatusRequest);
            });
            return objAccountStatusResponse;
        }

        public static RegisterInteractionAdjustResponse RegisterInteractionAdjust(string strIdSession, string strTransaction,RegisterInteractionAdjustRequest objRegisterInteractionAdjustRequest)
        {
            var objRegisterInteractionAdjustResponse = new RegisterInteractionAdjustResponse();

            objRegisterInteractionAdjustResponse = Claro.Web.Logging.ExecuteMethod<RegisterInteractionAdjustResponse>(objRegisterInteractionAdjustRequest.Audit.Session, objRegisterInteractionAdjustRequest.Audit.Transaction,
            () =>
            {
                return Data.Transac.Service.Fixed.RetentionCancelServ.RegisterInteractionAdjust(strIdSession, strTransaction, objRegisterInteractionAdjustRequest);
            });
            return objRegisterInteractionAdjustResponse;
        }

        public static RegistarInstaDecoAdiHFCResponse RegistarInstaDecoAdiHFC(RegistarInstaDecoAdiHFCRequest pRegistarInstaDecoAdiHFCRequest) 
        {
            var registarInstaDecoAdiHFCResponse = new RegistarInstaDecoAdiHFCResponse();
            try
            {
                registarInstaDecoAdiHFCResponse = Web.Logging.ExecuteMethod(pRegistarInstaDecoAdiHFCRequest.Audit.Session, pRegistarInstaDecoAdiHFCRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.RegistarInstaDecoAdiHFC(pRegistarInstaDecoAdiHFCRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(pRegistarInstaDecoAdiHFCRequest.Audit.Session, pRegistarInstaDecoAdiHFCRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return registarInstaDecoAdiHFCResponse;
        }


        public static Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse RegisterActiDesaBonoDescHFC(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request Request)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescHFCResponse = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse();
            try
            {
                registrarDescHFCResponse = Web.Logging.ExecuteMethod(Request.Audit.Session, Request.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.RegisterActiDesaBonoDescHFC(Request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw ex;
            }
            return registrarDescHFCResponse;
        }

        public static Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTE(Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.Request Request)
        {
            Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse registrarDescLTEResponse = new Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc.BodyResponse();
            try
            {
                registrarDescLTEResponse = Web.Logging.ExecuteMethod(Request.Audit.Session, Request.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Fixed.RetentionCancelServ.registrarDescLTE(Request);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(Request.Audit.Session, Request.Audit.Transaction, ex.Message);
                throw ex;
            }
            return registrarDescLTEResponse;
        }
        #endregion
    }
}
