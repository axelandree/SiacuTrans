using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetDataHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PostHistoryClient;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutBillingAddress;
using Claro.SIACU.Entity.Transac.Service.Coliving.PutDataClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using COLIVING = Claro.SIACU.Entity.Transac.Service.Coliving;
using FUNCTIONS = Claro.SIACU.Transac.Service;
using System.Web;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    public class ColivingTransacService : IColivingTransacService
    {




        public COLIVING.GetDataBilling.GetDataBillingResponse GetDataBilling(COLIVING.GetDataBilling.DataBillingRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            COLIVING.GetDataBilling.GetDataBillingResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Coliving.ChangeData.GetDataBilling(objRequest, strIdSession, strIdTransaccion));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        //vtorremo

        public List<DataHistorical> HistoryDataClientTobe(Claro.Entity.AuditRequest audit, string strIdSession, string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, string strCustomerID, string flagconvivencia)
        {
            List<DataHistorical> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<DataHistorical>>(() =>
                {
                    return Business.Transac.Service.Coliving.ChangeData.HistoryDataClientTobe(audit, strIdSession, strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, strCustomerID, flagconvivencia);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(audit.Session, audit.Transaction, ex.Message);
            }

            return listItem;

        }

        //vtorremo



        public String UpdateDatosClienteMigrado(DataClientRequest objRequest, string strIdSession, string strIdTransaccion)
        {
            DataClientResponse objResponse = new DataClientResponse();
            String CodigoRespuesta = Constants.NumberZeroString;
            try
            {
                if (objRequest.GetDataClientRequest != null)
                {
                    //LHV
                    objResponse.GetDataClientResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.GetUpdateDataClient(objRequest, strIdSession, strIdTransaccion));
                    //LHV
                    CodigoRespuesta = objResponse.GetDataClientResponse.GetUpdateClientResponse.AuditResponse.CodeResponse;
                    //cmendez
                    if (CodigoRespuesta == Constants.NumberZeroString)
                    {
                        if (objRequest.HistoryClientRequest != null)
                        {
                            objResponse.HistoryClientResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, objRequest.HistoryClientRequest.HistoryClient, strIdSession, strIdTransaccion));

                        }
                        if (objRequest.HistoryClientDataRequest != null)
                        {
                            objResponse.HistoryClientDataResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, objRequest.HistoryClientDataRequest.HistoryClient, strIdSession, strIdTransaccion));
                        }
                        //Code New for LegalAgent
                        if (objRequest.ListRepresentanteLegal != null)
                        {
                            Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "No es nulo objRequest.ListRepresentanteLegal");
                            if (objRequest.ListRepresentanteLegal.Count > 0)
                            {
                                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Tiene mayor de 0 objRequest.ListRepresentanteLegal");
                                objRequest.ListRepresentanteLegal.ForEach(delegate(HistoryClientRequest legalAgent)
                                {
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "legalRep: " + legalAgent.HistoryClient.legalRep);
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "docRep: " + legalAgent.HistoryClient.docRep);
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "docTypePerep: " + legalAgent.HistoryClient.docTypePerep);
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "updateGrupo: " + legalAgent.HistoryClient.updateGrupo);
                                    Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "fecReg: " + legalAgent.HistoryClient.fecReg);
                                    objResponse.HistoryClientDataResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, legalAgent.HistoryClient, strIdSession, strIdTransaccion));
                                });                                
                            }
                        }

                        if (objRequest.HistoryClientLegalRequest != null)
                        {
                            objResponse.HistoryClientLegalResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, objRequest.HistoryClientLegalRequest.HistoryClient, strIdSession, strIdTransaccion));
                        }

                    }
                }
                if (objRequest.BillingAddressRequest != null && objRequest.HistoryClientFactRequest != null && CodigoRespuesta == Constants.NumberZeroString)
                {
                    objResponse.BillingAddressResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.UpdateDataBillingResponse(objRequest, strIdSession, strIdTransaccion));
                    CodigoRespuesta = objResponse.BillingAddressResponse.responseAudit.codigoRespuesta;
                    if (CodigoRespuesta == Constants.NumberZeroString)
                    {
                        objResponse.HistoryClientFactResponse = Claro.Web.Logging.ExecuteMethod(strIdSession, strIdTransaccion, () => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, objRequest.HistoryClientFactRequest.HistoryClient, strIdSession, strIdTransaccion));
                    }
                }
                //cmendez
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, "Respuesta de guardar cambio de datos: " + new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(objResponse));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return CodigoRespuesta;
        }

        //vtorremo
        public HistoryClientResponse PostDataHistoryClientResponse(DataClientRequest objRequest, HistoryClient objRequestType, string strIdSession, string strIdTransaccion)
        {
            COLIVING.PostHistoryClient.HistoryClientResponse objResponse;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Coliving.ChangeData.PostHistoryClientResponse(objRequest, objRequestType, strIdSession, strIdTransaccion));
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(strIdSession, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));

                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        public GetParameterResponse GetParameter(string name, string strIdSession, string strTransaction)
        {
            GetParameterResponse objResponse = new GetParameterResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() => Business.Transac.Service.Coliving.Common.GetParameter(name, strIdSession, strIdSession));
            }
            catch (Exception ex)
            {
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public List<ListItems> GetDocumentTypeTOBE(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            List<ListItems> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<ListItems>>(() =>
                {
                    return Business.Transac.Service.Coliving.Common.GetDocumentTypeTOBE(strIdSession, strTransaction, strCodCargaDdl);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return listItem;

        }



        public ResponseCUParticipante GetCuParticipante(RequestCUParticipante objRequest, HeaderToBe objAudit)
        {

            ResponseCUParticipante objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ResponseCUParticipante>(() =>
                {
                    return Business.Transac.Service.Coliving.ChangeData.GetCuParticipante(objRequest, objAudit);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objAudit.IdTransaccion, objAudit.IdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;

           
        }


        public Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Response.ResponseCBIO getDataCustomerCBIO(Claro.Entity.AuditRequest audit, Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request.request objRequest)
        {
            ResponseCBIO objResponse = null;

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ResponseCBIO>(audit.Session, audit.Transaction, () =>
                {
                    return Business.Transac.Service.Coliving.ChangeData.getDataCustomerCBIO(audit, objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Info(audit.Session, audit.Transaction, ex.Message);
            }
            return objResponse;
        }
    }
}
