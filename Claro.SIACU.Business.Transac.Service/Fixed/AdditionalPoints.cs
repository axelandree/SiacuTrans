using Claro.SIACU.Entity.Transac.Service.Fixed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE = Claro.SIACU.Entity.Transac.Service.Fixed;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
  public  class AdditionalPoints
    {
        public static BE.GetGenericSot.GenericSotResponse GetGenericSot(BE.GetGenericSot.GenericSotRequest objGenericSotRequest)
        {

            var ObjGenericSotResponse = new BE.GetGenericSot.GenericSotResponse();

            ObjGenericSotResponse = Claro.Web.Logging.ExecuteMethod<BE.GetGenericSot.GenericSotResponse>(
            objGenericSotRequest.Audit.Session, objGenericSotRequest.Audit.Transaction,
            () =>
            {
                return Data.Transac.Service.Fixed.AdditionalPoints.GenericSOT(objGenericSotRequest);
            });
            return ObjGenericSotResponse;
        }


        public static BE.GetUpdateInter29.UpdateInter29Response GetUpdateInter29(BE.GetUpdateInter29.UpdateInter29Request objRequest)
        {
            var objResponse = new BE.GetUpdateInter29.UpdateInter29Response();

            try
            {
                var rFlagInsercion = string.Empty;
                var rMsgText = string.Empty;

                objResponse.rResult = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.AdditionalPoints.UpdateInter29(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_objid, objRequest.p_texto, objRequest.p_orden, ref rFlagInsercion, ref rMsgText);
                    });

                objResponse.rFlagInsercion = rFlagInsercion;
                objResponse.rMsgText = rMsgText;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }


        public static BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity GetETAAuditoriaRequestCapacity(BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaRequestCapacity objBEETAAuditoriaRequestCapacity)
        {

            var objResponse = new BE.GetETAAuditoriaRequestCapacity.BEETAAuditoriaResponseCapacity();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction,
                () =>
                {

                    return Data.Transac.Service.Fixed.AdditionalPoints.ConsultarCapacidadCuadrillas(objBEETAAuditoriaRequestCapacity);
                });
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objBEETAAuditoriaRequestCapacity.Audit.Session, objBEETAAuditoriaRequestCapacity.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static int registraEtaRequest(BE.GetRegisterEta.RegisterEtaRequest objRegisterEtaRequest)
        {
            int vidreturn = 0;
            try
            {
                vidreturn = Web.Logging.ExecuteMethod(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalPoints.registraEtaRequest(objRegisterEtaRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRegisterEtaRequest.Audit.Session, objRegisterEtaRequest.Audit.Transaction, ex.Message);
            }
            return vidreturn;
        }

        public static string RegisterEtaResponse(BE.GetRegisterEta.RegisterEtaResponse objRegisterEtaResponse)
        {
            string vidreturn = string.Empty;
            try
            {
                vidreturn = Web.Logging.ExecuteMethod(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalPoints.registraEtaResponse(objRegisterEtaResponse);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRegisterEtaResponse.Audit.Session, objRegisterEtaResponse.Audit.Transaction, ex.Message);
            }
            return vidreturn;
        }

        public static BE.GetDetailTransExtra.DetailTransExtraResponse REGISTRA_COSTO_PA(BE.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {

            BE.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse = new BE.GetDetailTransExtra.DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalPoints.REGISTRA_COSTO_PA(objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, ex.Message);
            }
            return objDetailTransExtraResponse;
        }

        public static BE.GetDetailTransExtra.DetailTransExtraResponse ACTUALIZAR_COSTO_PA(BE.GetDetailTransExtra.DetailTransExtraRequest objDetailTransExtraRequest)
        {

            BE.GetDetailTransExtra.DetailTransExtraResponse objDetailTransExtraResponse = new BE.GetDetailTransExtra.DetailTransExtraResponse();
            try
            {
                objDetailTransExtraResponse = Web.Logging.ExecuteMethod(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.AdditionalPoints.ACTUALIZAR_COSTO_PA(objDetailTransExtraRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDetailTransExtraRequest.Audit.Session, objDetailTransExtraRequest.Audit.Transaction, ex.Message);
            }
            return objDetailTransExtraResponse;
        }


        public static BE.GetRegisterTransaction.RegisterTransactionResponse LTERegisterTransaction(BE.GetRegisterTransaction.RegisterTransactionRequest objRequest)
        {
            var objResponse = new BE.GetRegisterTransaction.RegisterTransactionResponse();
            try
            {
                int intRescod = 0;
                string strResDes = string.Empty;
                objResponse.intNumSot = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.AdditionalPoints.LTERegisterTransaction(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.objRegisterTransaction,
                            out intRescod,
                            out strResDes);
                    });

                objResponse.intResCod = intRescod;
                objResponse.strResDes = strResDes;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw;
            }
            return objResponse;

        }

    }
}
