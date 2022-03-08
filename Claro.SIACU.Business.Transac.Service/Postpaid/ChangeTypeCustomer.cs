using System;
using System.Collections.Generic;
using Claro.SIACU.Entity;
using KEY = Claro.ConfigurationManager;
using COMMON = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class ChangeTypeCustomer
    {
        public static COMMON.GetBillingCycle.BillingCycleResponse GetBillingCycle(COMMON.GetBillingCycle.BillingCycleRequest objBillingCycleRequest)
        {
            COMMON.GetBillingCycle.BillingCycleResponse objBillingCycleResponse = new COMMON.GetBillingCycle.BillingCycleResponse();
            objBillingCycleResponse.LstBillingCycleResponse = new List<COMMON.BillingCycle>();
            List<COMMON.BillingCycle> lstobjBillingCycle = new List<COMMON.BillingCycle>();
            try
            {
                lstobjBillingCycle = Claro.Web.Logging.ExecuteMethod<List<COMMON.BillingCycle>>(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetBillingCycle(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, objBillingCycleRequest.strTypeCustomer);
                });

                objBillingCycleResponse.LstBillingCycleResponse = lstobjBillingCycle;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBillingCycleRequest.Audit.Session, objBillingCycleRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objBillingCycleResponse;
        }

        public static COMMON.GetParameterBusinnes.ParameterBusinnesResponse GetPlanModel(COMMON.GetParameterBusinnes.ParameterBusinnesRequest objPlanModelRequest)
        {
            COMMON.GetParameterBusinnes.ParameterBusinnesResponse objPlanModelResponse = new COMMON.GetParameterBusinnes.ParameterBusinnesResponse();
            objPlanModelResponse.lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstParameterBusinnes = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objPlanModelRequest.Audit.Session, objPlanModelRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetParameterBusinnes(objPlanModelRequest.Audit.Session, objPlanModelRequest.Audit.Transaction, objPlanModelRequest.strIdList);
                });
                objPlanModelResponse.lstParameterBusinnes = lstParameterBusinnes;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objPlanModelRequest.Audit.Session, objPlanModelRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objPlanModelResponse;
        }

        public static COMMON.GetNewPlan.NewPlanResponse GetNewPlan(COMMON.GetNewPlan.NewPlanRequest objNewPlanRequest)
        {
            COMMON.GetNewPlan.NewPlanResponse objNewPlanResponse = new COMMON.GetNewPlan.NewPlanResponse();
            objNewPlanResponse.lstNewPlan = new List<COMMON.NewPlan>();
            List<COMMON.NewPlan> lstNewPlan = new List<COMMON.NewPlan>();
            try
            {
                lstNewPlan = Claro.Web.Logging.ExecuteMethod<List<COMMON.NewPlan>>(objNewPlanRequest.Audit.Session, objNewPlanRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetNewPlan(objNewPlanRequest.Audit.Session, objNewPlanRequest.Audit.Transaction, objNewPlanRequest.ValorTipoProducto, objNewPlanRequest.CategoriaProducto, objNewPlanRequest.PlanActual);
                });
                objNewPlanResponse.lstNewPlan = lstNewPlan;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objNewPlanRequest.Audit.Session, objNewPlanRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return objNewPlanResponse;
        }

        public static COMMON.GetTarifePlan.TarifePlanResponse GetTarifePlan(COMMON.GetTarifePlan.TarifePlanRequest objTarifePlanRequest)
        {
            COMMON.GetTarifePlan.TarifePlanResponse objTarifePlanResponse = new COMMON.GetTarifePlan.TarifePlanResponse();
            objTarifePlanResponse.lstTarifePlan = new List<COMMON.TarifePlan>();
            List<COMMON.TarifePlan> lstTarifePlan = new List<COMMON.TarifePlan>();
            try
            {
                lstTarifePlan = Claro.Web.Logging.ExecuteMethod<List<COMMON.TarifePlan>>(objTarifePlanRequest.Audit.Session, objTarifePlanRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetTarifePlan(objTarifePlanRequest.Audit.Session, objTarifePlanRequest.Audit.Transaction, objTarifePlanRequest.strTPROCCode, objTarifePlanRequest.strPRDCCode, objTarifePlanRequest.strModalVent, objTarifePlanRequest.strPLNCFamilly);
                });
                objTarifePlanResponse.lstTarifePlan = lstTarifePlan;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTarifePlanRequest.Audit.Session, objTarifePlanRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objTarifePlanResponse;
        }

        public static COMMON.GetElementMigration.ElementMigrationResponse GetElementMigration(COMMON.GetElementMigration.ElementMigrationRequest objElementMigrationRequest)
        {
            COMMON.GetElementMigration.ElementMigrationResponse objElementMigrationResponse = new COMMON.GetElementMigration.ElementMigrationResponse();
            objElementMigrationResponse.lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstElementMigration = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstElementMigration = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objElementMigrationRequest.Audit.Session, objElementMigrationRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetElementMigration(objElementMigrationRequest.Audit.Session, objElementMigrationRequest.Audit.Transaction, objElementMigrationRequest.intCode);
                });
                objElementMigrationResponse.lstParameterBusinnes = lstElementMigration;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objElementMigrationRequest.Audit.Session, objElementMigrationRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objElementMigrationResponse;
        }

        public static COMMON.GetCodeChangeCustomer.CodeChangeCustomerResponse GetCodeChangeCustomer(COMMON.GetCodeChangeCustomer.CodeChangeCustomerRequest objCodeChangeCustomerRequest)
        {

            COMMON.GetCodeChangeCustomer.CodeChangeCustomerResponse objCodeChangeCustomerResponse = new COMMON.GetCodeChangeCustomer.CodeChangeCustomerResponse();
            try
            {
                objCodeChangeCustomerResponse.strCodeChangeCustomer = Claro.Web.Logging.ExecuteMethod<string>(objCodeChangeCustomerRequest.Audit.Session, objCodeChangeCustomerRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetCodeChangeCustomer(objCodeChangeCustomerRequest.Audit.Session, objCodeChangeCustomerRequest.Audit.Transaction, objCodeChangeCustomerRequest.strName);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objCodeChangeCustomerRequest.Audit.Session, objCodeChangeCustomerRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objCodeChangeCustomerResponse;
        }

        public static COMMON.GetArea.AreaResponse GetArea(COMMON.GetArea.AreaRequest objGetAreaRequest)
        {
            COMMON.GetArea.AreaResponse objAreaResponse = new COMMON.GetArea.AreaResponse();
            objAreaResponse.lstArea = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstParameterBusinnes = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objGetAreaRequest.Audit.Session, objGetAreaRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetArea(objGetAreaRequest.Audit.Session, objGetAreaRequest.Audit.Transaction);
                });
                objAreaResponse.lstArea = lstParameterBusinnes;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetAreaRequest.Audit.Session, objGetAreaRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objAreaResponse;
        }

        public static COMMON.GetMotiveByArea.MotiveByAreaResponse GetMotiveByArea(COMMON.GetMotiveByArea.MotiveByAreaRequest objMotiveByAreaRequest)
        {
            COMMON.GetMotiveByArea.MotiveByAreaResponse objReasonByAreaResponse = new COMMON.GetMotiveByArea.MotiveByAreaResponse();
            objReasonByAreaResponse.lstReasonByArea = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstParameterBusinnes = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objMotiveByAreaRequest.Audit.Session, objMotiveByAreaRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetMotiveByArea(objMotiveByAreaRequest.Audit.Session, objMotiveByAreaRequest.Audit.Transaction, objMotiveByAreaRequest.strIdArea);
                });
                objReasonByAreaResponse.lstReasonByArea = lstParameterBusinnes;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objMotiveByAreaRequest.Audit.Session, objMotiveByAreaRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objReasonByAreaResponse;
        }

        public static COMMON.GetSubMotive.SubMotiveResponse GetSubMotive(COMMON.GetSubMotive.SubMotiveRequest objGetSubMotiveRequest)
        {
            COMMON.GetSubMotive.SubMotiveResponse objSubMotiveResponse = new COMMON.GetSubMotive.SubMotiveResponse();
            objSubMotiveResponse.lstSubMotive = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstParameterBusinnes = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objGetSubMotiveRequest.Audit.Session, objGetSubMotiveRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetSubMotive(objGetSubMotiveRequest.Audit.Session, objGetSubMotiveRequest.Audit.Transaction, objGetSubMotiveRequest.strIdArea, objGetSubMotiveRequest.strIdMotive);
                });
                objSubMotiveResponse.lstSubMotive = lstParameterBusinnes;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objGetSubMotiveRequest.Audit.Session, objGetSubMotiveRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objSubMotiveResponse;
        }

        public static COMMON.GetConsumptionStop.ConsumptionStopResponse GetConsumptionStop(COMMON.GetConsumptionStop.ConsumptionStopRequest objConsumptionStopRequest)
        {
            COMMON.GetConsumptionStop.ConsumptionStopResponse objConsumptionStopResponse = new COMMON.GetConsumptionStop.ConsumptionStopResponse();
            objConsumptionStopResponse.lstConsumptionStop = new List<COMMON.ParameterBusinnes>();
            List<COMMON.ParameterBusinnes> lstParameterBusinnes = new List<COMMON.ParameterBusinnes>();
            try
            {
                lstParameterBusinnes = Claro.Web.Logging.ExecuteMethod<List<COMMON.ParameterBusinnes>>(objConsumptionStopRequest.Audit.Session, objConsumptionStopRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Postpaid.ChangeTypeCustomer.GetConsumptionStop(objConsumptionStopRequest.Audit.Session, objConsumptionStopRequest.Audit.Transaction, objConsumptionStopRequest.strCode);
                });
                objConsumptionStopResponse.lstConsumptionStop = lstParameterBusinnes;
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objConsumptionStopRequest.Audit.Session, objConsumptionStopRequest.Audit.Transaction, ex.Message);
                throw ex;
            }
            return objConsumptionStopResponse;
        }

        //public static COMMON.GetSiacParameter.SiacParameterResponse
        //    GetParamTerminalSoloTFIPostpago(COMMON.GetSiacParameter.SiacParameterRequest objSiacParametroRequest)
        //{
        //    string p_msg_text = "";
        //    COMMON.GetSiacParameter.SiacParameterResponse objSiacParametroResponse =
        //        new COMMON.GetSiacParameter.SiacParameterResponse()
        //        {
        //            ListSiacParametro = Claro.Web.Logging.ExecuteMethod<List<COMMON.SiacParameter>>
        //            (objSiacParametroRequest.Audit.Session, objSiacParametroRequest.Audit.Transaction,
        //                () =>
        //                {
        //                    return Data.Transac.Service.Postpaid.Postpaid.GetParamTerminalSoloTFIPostpago(objSiacParametroRequest.Audit.Session,
        //                        objSiacParametroRequest.Audit.Transaction, objSiacParametroRequest.p_ParametroID, ref p_msg_text);
        //                })

        //        };
        //    objSiacParametroResponse.p_msg_text = p_msg_text;
        //    return objSiacParametroResponse;
        //}
    }
}
