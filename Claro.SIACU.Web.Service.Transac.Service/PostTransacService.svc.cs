using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt;
using POSTPAID = Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsultService;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyServiceQuotAmount;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetTypeTransactionBRMS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetUserExistsBSCS;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateActDesServProg;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetInsertTraceability;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration;
using Claro.SIACU.Entity.Transac.Service.Postpaid.GetSignDocument;
using FUNCTIONS = Claro.SIACU.Transac.Service;
using Claro.SIACU.Entity.Transac.Service.Postpaid.PostUpdateChangeData;

namespace Claro.SIACU.Web.Service.Transac.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostTransacService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostTransacService.svc or PostTransacService.svc.cs at the Solution Explorer and start debugging.
    public class PostTransacService : IPostTransacService
    {
        public PostTransacService()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt.ReceiptResponse GetDataInvoice(ReceiptRequest request)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt.ReceiptResponse objDataInvoiceResponse;

            try
            {
                objDataInvoiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt.ReceiptResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetDataInvoice(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objDataInvoiceResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit.ConsumeLimitResponse GETConsumeLimit(ConsumeLimitRequest objConsumeLimitRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit.ConsumeLimitResponse objConsumeLimitResponse;
            try
            {
                objConsumeLimitResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit.ConsumeLimitResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetConsumeLimit(objConsumeLimitRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumeLimitRequest.Audit.Session, objConsumeLimitRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objConsumeLimitResponse;
        }
        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeResponse ConsultFixedCharge(Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeResponse objFixedChargeResponse;
            try
            {
                objFixedChargeResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge.FixedChargeResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.ConsultFixedCharge(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objFixedChargeResponse;
        }
        public Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone GetStatusPhone(string strIdSession, string strTransactionID, string strApplicationID, string strApplicationName, string strApplicationUser, string strPhoneNumber)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone objNumberPhoneStatus = new Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone();
            Claro.Web.Logging.Info(strIdSession, strTransactionID, strPhoneNumber);

            try
            {
                objNumberPhoneStatus = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.SimCardPhone>(() => { return Business.Transac.Service.Postpaid.Postpaid.GetStatusPhone(strIdSession, strTransactionID, strApplicationID, strApplicationName, strApplicationUser, strPhoneNumber); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objNumberPhoneStatus;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR.HLRResponse GetHLRLocation(Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR.HLRRequest objHLRRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR.HLRResponse objHLRResponse = null;

            try
            {
                objHLRResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR.HLRResponse>(() => { return Business.Transac.Service.Postpaid.ChangePhoneNumber.GetHLRLocation(objHLRRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objHLRRequest.Audit.Session, objHLRRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objHLRResponse;
        }

        public Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleResponse GetBillingCycle(Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleResponse objBillingCycleResponse = new Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleResponse();

            try
            {
                objBillingCycleResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillingCycle.BillingCycleResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetBillingCycle(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objBillingCycleResponse;
        }

        public Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesResponse GetPlanModel(Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesResponse objParameterBusinnesResponse = null;

            try
            {
                objParameterBusinnesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes.ParameterBusinnesResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetPlanModel(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objParameterBusinnesResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanResponse GetNewPlan(Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanResponse objNewPlanResponse;
            try
            {
                objNewPlanResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetNewPlan.NewPlanResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetNewPlan(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objNewPlanResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanResponse GetPlansMigrations(Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanResponse objMigrationPlanResponse;
            try
            {
                objMigrationPlanResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetMigrationPlans.MigrationPlanResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetPlansMigrations(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objMigrationPlanResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanResponse GetFixedCostBasePlan(Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanResponse objFixedCostBasePlanRespons = null;
            try
            {
                objFixedCostBasePlanRespons = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCostBasePlan.FixedCostBasePlanResponse>(() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetFixedCostBasePlan(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objFixedCostBasePlanRespons;
        }
        public Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanResponse GetTarifePlan(Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanResponse objTarifePlanResponse = null;

            try
            {
                objTarifePlanResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan.TarifePlanResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetTarifePlan(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objTarifePlanResponse;
        }

        public Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationResponse GetElementMigration(Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationResponse objElementMigrationResponse = null;

            try
            {
                objElementMigrationResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration.ElementMigrationResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetElementMigration(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objElementMigrationResponse;
        }

        public Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerResponse GetCodeChangeCustomer(Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerResponse objCodeChangeCustomerResponse = null;

            try
            {
                objCodeChangeCustomerResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetCodeChangeCustomer.CodeChangeCustomerResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetCodeChangeCustomer(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objCodeChangeCustomerResponse;
        }

        public Entity.Transac.Service.Postpaid.GetArea.AreaResponse GetArea(Entity.Transac.Service.Postpaid.GetArea.AreaRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea.AreaResponse objAreaResponse = null;

            try
            {
                objAreaResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea.AreaResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetArea(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objAreaResponse;
        }

        public Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaResponse GetMotiveByArea(Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaResponse objMotiveByAreaResponse = null;

            try
            {
                objMotiveByAreaResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea.MotiveByAreaResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetMotiveByArea(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objMotiveByAreaResponse;
        }

        public Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveResponse GetSubMotive(Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveResponse objSubMotiveResponse = null;

            try
            {
                objSubMotiveResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive.SubMotiveResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetSubMotive(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objSubMotiveResponse;
        }
       
        public Claro.SIACU.Entity.Transac.Service.Postpaid.ChangePhoneNumber ValidateChangeNumberTransaction(string strIdSession, string strTransactionID, string strContract, string strFlagFidelize)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.ChangePhoneNumber objChangePhoneNumber = null;

            try
            {
                objChangePhoneNumber = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.ChangePhoneNumber>(() => { return Business.Transac.Service.Postpaid.ChangePhoneNumber.ValidateChangeNumberTransaction(strIdSession, strTransactionID, strContract, strFlagFidelize); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objChangePhoneNumber;
        }

        public ChangePhoneNumberResponse GetAvailableLines(ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            ChangePhoneNumberResponse objChangePhoneNumberResponse = null;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<ChangePhoneNumberResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.ChangePhoneNumber.GetAvailableLines(objChangePhoneNumberRequest); 
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objChangePhoneNumberResponse;
        }

        public bool ValidateChangeNumberBSCS(string strIdSession, string strTransactionID, string strSerialNum, string strDnNum, int intEstado)
        {
            bool Response = false;

            try
            {
                Response = Claro.Web.Logging.ExecuteMethod<bool>(strIdSession, strTransactionID, () =>
                {
                    return Business.Transac.Service.Postpaid.ChangePhoneNumber.ValidateChangeNumberBSCS(strIdSession, strTransactionID, strSerialNum, strDnNum, intEstado);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return Response;
        }

        public ChangePhoneNumberResponse ExecuteChangeNumber(ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            ChangePhoneNumberResponse objChangePhoneNumberResponse = null;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<ChangePhoneNumberResponse>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Postpaid.ChangePhoneNumber.ExecuteChangeNumber(objChangePhoneNumberRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objChangePhoneNumberResponse;
        }
        public Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopResponse GetConsumptionStop(Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopRequest objRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopResponse objConsumptionStopResponse = null;

            try
            {
                objConsumptionStopResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop.ConsumptionStopResponse>(() => { return Business.Transac.Service.Postpaid.ChangeTypeCustomer.GetConsumptionStop(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objConsumptionStopResponse;
        }

        public Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse RollbackChangeNumber(ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = null;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse>( () => { return Business.Transac.Service.Postpaid.ChangePhoneNumber.RollbackChangeNumber(objChangePhoneNumberRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objChangePhoneNumberResponse;
        }

        public Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse UpdatePhoneNumber(Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberRequest objChangePhoneNumberRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse objChangePhoneNumberResponse = null;

            try
            {
                objChangePhoneNumberResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetChangePhoneNumber.ChangePhoneNumberResponse>(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, () =>
                {
                    return Business.Transac.Service.Postpaid.ChangePhoneNumber.UpdatePhoneNumber(objChangePhoneNumberRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objChangePhoneNumberRequest.Audit.Session, objChangePhoneNumberRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objChangePhoneNumberResponse;
        }

        public string DeleteUserHistory(string strIdSession, string strTransactionID, string strPhone, string strMotive)
        {
            string strResponse = "";

            try
            {
                strResponse = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransactionID, () =>
                {
                    return Business.Transac.Service.Postpaid.ChangePhoneNumber.DeleteUserHistory(strIdSession, strTransactionID, strPhone, strMotive);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransactionID, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return strResponse;
        }

        public Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse GetPortability(Entity.Transac.Service.Postpaid.GetPortability.PortabilityRequest objPortabilityRequest)
        {
            Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse objPortabilityTypeResponse;

            try
            {
                objPortabilityTypeResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Postpaid.GetPortability.PortabilityResponse>(() => { return Business.Transac.Service.Postpaid.Postpaid.GetPortability(objPortabilityRequest); });
            }
            catch (Exception ex)
            {
                objPortabilityTypeResponse = null;
                Claro.Web.Logging.Error(objPortabilityRequest.Audit.Session, objPortabilityRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objPortabilityTypeResponse;
        }

        public Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse GetTriaciones(Entity.Transac.Service.Postpaid.GetTriations.StriationsRequest objTriacionRequest)
        {
            Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse objTriacionesResponse = null;

            try
            {
                objTriacionesResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Postpaid.GetTriations.StriationsResponse>(() => { return Business.Transac.Service.Postpaid.Postpaid.GetTriaciones(objTriacionRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTriacionRequest.Audit.Session, objTriacionRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objTriacionesResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesResponse GetListQueryAssociatedLines
            (Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesRequest objQueryAssociatedLinesRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesResponse objQueryAssociatedLinesResponse;
            try
            {
                objQueryAssociatedLinesResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines.QueryAssociatedLinesResponse>
                    (() => { return Business.Transac.Service.Postpaid.Postpaid.GetListQueryAssociatedLines(objQueryAssociatedLinesRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objQueryAssociatedLinesRequest.Audit.Session, objQueryAssociatedLinesRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objQueryAssociatedLinesResponse;
        }

        public Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationProgDeudaBloqSuspResponse
            (Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest)
        {
            Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse objValidationProgDeudaBloqSuspResponse;
            try
            {
                objValidationProgDeudaBloqSuspResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetValidationProgDeudaBloqSusp(objRequest); });
           
            }
            catch (Exception ex)
            {
              Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objValidationProgDeudaBloqSuspResponse;
        }

        public Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse GetValidationMigration
        (Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspRequest objRequest)
        {
            Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse objValidationProgDeudaBloqSuspResponse;
            try
            {
                objValidationProgDeudaBloqSuspResponse = Claro.Web.Logging.ExecuteMethod<Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp.ValidationProgDeudaBloqSuspResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetValidationMigration(objRequest); });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objValidationProgDeudaBloqSuspResponse;
        }

        public POSTPAID.GetBillData.BillDataResponse GetListBillSummary(POSTPAID.GetBillData.BillDataRequest request) {
            POSTPAID.GetBillData.BillDataResponse response;
            try
            {
                response = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetBillData.BillDataResponse>
                    (() => { return Business.Transac.Service.Postpaid.Postpaid.GetListBillSummary(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return response;
        }

        public POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response GetCallDetailNB_DB1(POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Request request)
        {
            POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetCallDetailNBDB1.CallDetailNBDB1Response>
                    (() => { return Business.Transac.Service.Postpaid.CallsDetail.GetCallDetailNB_DB1(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse GetCallDetailNB_DB1_BSCS(POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSRequest request)
        {
            POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse obj;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetCallDetailNBDB1BSCS.CallDetailNBDB1BSCSResponse>
                    (() => { return Business.Transac.Service.Postpaid.CallsDetail.GetCallDetailNB_DB1_BSCS(request); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(request.Audit.Session, request.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetDataLine.DataLineResponse GetDataLine(POSTPAID.GetDataLine.DataLineRequest objRequest)
        {
            POSTPAID.GetDataLine.DataLineResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetDataLine.DataLineResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetDataLine(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        public POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse GetAmountIncomingCall(POSTPAID.GetAmountIncomingCall.AmountIncomingCallRequest objRequest)
        {
            POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetAmountIncomingCall.AmountIncomingCallResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetAmountIncomingCall(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public List<ListItem> GetDocumentType(string strIdSession, string strTransaction, string strCodCargaDdl) {
            List<ListItem> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetDocumentType(strIdSession, strTransaction, strCodCargaDdl);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return listItem;

        }

        public POSTPAID.GetUpdateInteraction.UpdateInteractionResponse GetUpdateInteraction(POSTPAID.GetUpdateInteraction.UpdateInteractionRequest objRequest)
        {
            POSTPAID.GetUpdateInteraction.UpdateInteractionResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetUpdateInteraction.UpdateInteractionResponse>(() => 
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetUpdateInteraction(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse GetAdjustForClaims(POSTPAID.GetAdjustForClaims.AdjustForClaimsRequest objRequest)
        {
            POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetAdjustForClaims.AdjustForClaimsResponse>(() => 
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetAdjustForClaims(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }
        

        public POSTPAID.GetAgreement.AgreementResponse GetReinstatementEquipment(POSTPAID.GetAgreement.AgreementResquest objRequest)
        {
            POSTPAID.GetAgreement.AgreementResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetAgreement.AgreementResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetReinstatementEquipment(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductResponse GetServByTransCodeProductResponse(POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductRequest objRequest)
        {
            POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetServByTransCodeProduct.ServByTransCodeProductResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetServByTransCodeProductResponse(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetMaintenancePlan.MaintenancePlanResponse GetPlansServices(POSTPAID.GetMaintenancePlan.MaintenancePlanRequest objRequest)
        {
            POSTPAID.GetMaintenancePlan.MaintenancePlanResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetMaintenancePlan.MaintenancePlanResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetPlansServices(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
          }
        public POSTPAID.GetValidateBagShare.ValidateBagShareResponse GetValidateBagShare(POSTPAID.GetValidateBagShare.ValidateBagShareRequest objRequest)
        {
            POSTPAID.GetValidateBagShare.ValidateBagShareResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetValidateBagShare.ValidateBagShareResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetValidateBagShare(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
          }

        public POSTPAID.GetValidateProgByProduct.ValidateProgByProductResponse GetValidateProgByProduct(POSTPAID.GetValidateProgByProduct.ValidateProgByProductRequest objRequest)
        {
            POSTPAID.GetValidateProgByProduct.ValidateProgByProductResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetValidateProgByProduct.ValidateProgByProductResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetValidateProgByProduct(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetRegisterPlanService.RegisterPlanResponse RegisterPlanService(POSTPAID.GetRegisterPlanService.RegisterPlanRequest objRequest)
        {
            POSTPAID.GetRegisterPlanService.RegisterPlanResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetRegisterPlanService.RegisterPlanResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.RegisterPlanService(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
        }
            return obj;
        }

        public POSTPAID.GetProgramerMigration.ProgramerMigrationResponse ProgramerMigrationControlPostPago(POSTPAID.GetProgramerMigration.ProgramerMigrationRequest objRequest)
        {
            POSTPAID.GetProgramerMigration.ProgramerMigrationResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetProgramerMigration.ProgramerMigrationResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.ProgramerMigrationControlPostPago(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        
        public POSTPAID.GetDataByContract.DataByContractResponse GetDataByContract(POSTPAID.GetDataByContract.DataByContractRequest objRequest)
        {
            POSTPAID.GetDataByContract.DataByContractResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetDataByContract.DataByContractResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetDataByContract(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetDataByCount.DataByCountResponse GetDataByCount(POSTPAID.GetDataByCount.DataByCountRequest objRequest)
        {
            POSTPAID.GetDataByCount.DataByCountResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetDataByCount.DataByCountResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.GetDataByCount(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }

        public POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse MigrationPlans(POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanRequest objRequest)
        {
            POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse obj = null;
            try
            {
                obj = Claro.Web.Logging.ExecuteMethod<POSTPAID.GetExecuteMigrationPlan.ExecuteMigrationPlanResponse>
                    (() => { return Business.Transac.Service.Postpaid.MigrationPlan.MigrationPlans(objRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return obj;
        }
        public bool AlignTransactionService(string strIdSession, string strTransaction, string strContractID)
        {
            bool blnResponse;
            try
            {
                blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(() => 
                {
                    return Business.Transac.Service.Postpaid.Postpaid.AlignTransactionService(strIdSession,strTransaction,strContractID);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return blnResponse;
        }

        public bool AlignCodID(string strIdSession, string strTransaction, string strContractID)
        {
            bool blnResponse;
            try
            {
                blnResponse = Claro.Web.Logging.ExecuteMethod<bool>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.AlignCodID(strIdSession, strTransaction, strContractID);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return blnResponse;
        }
        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailResponse GetBillPostDetail(Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailRequest objBillPostRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailResponse objBillPostResponse;
            try
            {
                objBillPostResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail.BillPostDetailResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetBillPostDetail(objBillPostRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBillPostRequest.Audit.Session, objBillPostRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objBillPostResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIResponse GetListInvoicePDI(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIRequest objListInvoicePdiRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIResponse ObjListInvoicePdiResponse;
            try
            {
                ObjListInvoicePdiResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI.ListInvoice_PDIResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetListInvoicePDI(objListInvoicePdiRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListInvoicePdiRequest.Audit.Session, objListInvoicePdiRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return ObjListInvoicePdiResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceResponse GetListInvoice(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceRequest objListInvoiceRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceResponse ObjListInvoiceResponse;
            try
            {
                ObjListInvoiceResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice.ListInvoiceResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetListInvoice(objListInvoiceRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListInvoiceRequest.Audit.Session, objListInvoiceRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return ObjListInvoiceResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailResponse GetListCallDetail(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailRequest objListCallDetailRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailResponse ObjListCallDetailResponse;
            try
            {
                ObjListCallDetailResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail.ListCallDetailResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetListCallDetail(objListCallDetailRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListCallDetailRequest.Audit.Session, objListCallDetailRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return ObjListCallDetailResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIResponse GetListCallDetailPDI(Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIRequest objListCallDetailPDIRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIResponse ObjListCallDetailPDIResponse;
            try
            {
                ObjListCallDetailPDIResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI.ListCallDetailPDIResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetListCallDetailPDI(objListCallDetailPDIRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objListCallDetailPDIRequest.Audit.Session, objListCallDetailPDIRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return ObjListCallDetailPDIResponse;
        }

        public Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListResponse GetRechargeList(Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListRequest objRechargeListRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListResponse ObjRechargeListResponse;
            try
            {
                ObjRechargeListResponse = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList.RechargeListResponse>(() => { return Business.Transac.Service.Postpaid.PostBilledOutCallDetails.GetRechargeList(objRechargeListRequest); });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRechargeListRequest.Audit.Session, objRechargeListRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return ObjRechargeListResponse;
        }

        public ApprovalBusinessCreditLimitResponse GetApprovalBusinessCreditLimitBusinessAccount(
            ApprovalBusinessCreditLimitRequest objRequest)
        {
            ApprovalBusinessCreditLimitResponse objResponse = new ApprovalBusinessCreditLimitResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ApprovalBusinessCreditLimitResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid
                        .GetApprovalBusinessCreditLimitBusinessAccount(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public UserExistsBSCSResponse GetUserExistsBSCS(UserExistsBSCSRequest objRequest)
        {
            UserExistsBSCSResponse objResponse = new UserExistsBSCSResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UserExistsBSCSResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GEtUserExistsBSCS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ConsultServiceResponse GetConsultService(ConsultServiceRequest objRequest)
        {
            ConsultServiceResponse objResponse = new ConsultServiceResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ConsultServiceResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetConsultService(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ModifyServiceQuotAmountResponse GetModifyServiceQuotAmount(ModifyServiceQuotAmountRequest objRequest)
        {
            ModifyServiceQuotAmountResponse objResponse = new ModifyServiceQuotAmountResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<ModifyServiceQuotAmountResponse>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetModifyServiceQuotAmount(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public TypeTransactionBRMSResponse GetTypeTransactionBRMS(TypeTransactionBRMSRequest objRequest)
        {
            TypeTransactionBRMSResponse objResponse = new TypeTransactionBRMSResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetTypeTransactionBRMS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ActDesServProgResponse GetActDesServProg(ActDesServProgRequest objRequest)
        {
            ActDesServProgResponse objResponse = new ActDesServProgResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetActDesServProg(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ServiceByContractResponse GetServiceByContract(ServiceByContractRequest objRequest)
        {
            ServiceByContractResponse objResponse = new ServiceByContractResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetServiceByContract(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public POSTPAID.GetServicesDTH.ServicesDTHResponse GetServicesDTH(
            POSTPAID.GetServicesDTH.ServicesDTHRequest objRequest)
        {
            POSTPAID.GetServicesDTH.ServicesDTHResponse objResponse = new POSTPAID.GetServicesDTH.ServicesDTHResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetServicesDTH(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return objResponse;
        }

        public POSTPAID.GetValidateActDesServProg.ValidateActDesServProgResponse GetValidateActDesServProg(POSTPAID.GetValidateActDesServProg.ValidateActDesServProgRequest objRequest)
        {
            POSTPAID.GetValidateActDesServProg.ValidateActDesServProgResponse objResponse = new ValidateActDesServProgResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetValidateActDesServProg(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public ServiceBSCSResponse GetServiceBSCS(ServiceBSCSRequest objRequest)
        {
            ServiceBSCSResponse objResponse = new ServiceBSCSResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetServiceBSCS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }


        #region RetenciónCancelación
        public RetentionCancel GetDataAccord(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.RetentionCancelServ.GetDataAccord(oRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return oResponse;

        }

        public RetentionCancel GetLoadStaidTotal(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();
            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.RetentionCancelServ.GetLoadStaidTotal(oRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session, oRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return oResponse;

        }
        #endregion

        public InsertTraceabilityResponse GetInsertTraceability(InsertTraceabilityRequest objRequest)
        {
            InsertTraceabilityResponse objResponse = new InsertTraceabilityResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetInsertTraceability(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public BiometricConfigurationResponse GetBiometricConfiguration(BiometricConfigurationRequest objRequest)
        {
            BiometricConfigurationResponse objResponse = new BiometricConfigurationResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetBiometricConfiguration(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public SignDocumentResponse GetSignDocument(SignDocumentRequest objRequest)
        {
            SignDocumentResponse objResponse = new SignDocumentResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetSignDocument(objRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public POSTPAID.GetDataCustomer.DataCustomerResponse GetDataCustomer(POSTPAID.GetDataCustomer.DataCustomerRequest objRequest,int flag)
        {
            POSTPAID.GetDataCustomer.DataCustomerResponse objResponse;
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetDataCustomer(objRequest,flag);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public List<ListItem> GetMotivoCambio(string strIdSession, string strTransaction, string pid_parametro,  string rMsgText)
        {
            List<ListItem> listItem = null;

            try
            {
                listItem = Claro.Web.Logging.ExecuteMethod<List<ListItem>>(() =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.GetMotivoCambio(strIdSession, strTransaction, pid_parametro,  rMsgText);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strIdSession, strTransaction, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }

            return listItem;

        }
           
        public string ValidateEnvioxMail(string strIdSession, string strTransaction, string strCustomerID)
        {
            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strIdSession, strTransaction, () =>
            {
                return Business.Transac.Service.Postpaid.Postpaid.ValidateEnvioxMail(strIdSession, strTransaction, strCustomerID);
            });
            return strRespuesta;
        }

        public UpdateChangeDataResponse UpdateNameCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {           
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UpdateChangeDataResponse>(strUsrApp, strIdTransaccion, () =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.UpdateNameCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strUsrApp, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            return objResponse;
        }

        public UpdateChangeDataResponse UpdateDataMinorCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, int intSeq_in)
        {            
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();
            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UpdateChangeDataResponse>(strUsrApp, strIdTransaccion, () =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.UpdateDataMinorCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente, intSeq_in);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strUsrApp, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            
            return objResponse;
        }

        public string UpdateDataCustomerCLF(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {

            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Business.Transac.Service.Postpaid.Postpaid.UpdateDataCustomerCLF(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente);
            });
            return strRespuesta;
        }

        public UpdateChangeDataResponse UpdateAddressCustomer(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente, string tipoDireccion, int intSeq_in)
        {

            string strRespuesta = "";
            UpdateChangeDataResponse objResponse = new UpdateChangeDataResponse();

            try
            {
                objResponse = Claro.Web.Logging.ExecuteMethod<UpdateChangeDataResponse>(strUsrApp, strIdTransaccion, () =>
                {
                    return Business.Transac.Service.Postpaid.Postpaid.UpdateAddressCustomer(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente, tipoDireccion, intSeq_in);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(strUsrApp, strIdTransaccion, FUNCTIONS.Functions.GetExceptionMessage(ex));
                throw new FaultException(FUNCTIONS.Functions.GetExceptionMessage(ex));
            }
            
            return objResponse;
        }

        public string UpdateDataCustomerPClub(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Common.Client oCliente)
        {

            string strRespuesta = "";
            strRespuesta = Claro.Web.Logging.ExecuteMethod<string>(strUsrApp, strIdTransaccion, () =>
            {
                return Business.Transac.Service.Postpaid.Postpaid.UpdateDataCustomerPClub(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oCliente);
            });
            return strRespuesta;
        }

        public int registrarTransaccionSiga(string strIdTransaccion, string strIpAplicacion, string strAplicacion, string strUsrApp, Entity.Transac.Service.Fixed.TransactionSiga oTransaction)
        {

            int strRespuesta = 0;
            strRespuesta = Claro.Web.Logging.ExecuteMethod<int>(strUsrApp, strIdTransaccion, () =>
            {
                return Business.Transac.Service.Postpaid.Postpaid.registrarTransaccionSiga(strIdTransaccion, strIpAplicacion, strAplicacion, strUsrApp, oTransaction);
            });
            return strRespuesta;
        }



    }
}
