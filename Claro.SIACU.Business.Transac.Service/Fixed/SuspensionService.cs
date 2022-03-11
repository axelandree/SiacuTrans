using System;
using System.Collections.Generic;
using Claro.SIACU.Transac.Service;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using KEY = Claro.ConfigurationManager;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class SuspensionService
    {
        public static EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse EjecutaSuspensionDeServicioCodRes(EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostExecuteSuspension.ExecuteSuspensionResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.SuspensionService.EjecutaSuspensionDeServicioCodRes(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.GetReconeService.ReconeServiceResponse GetReconectionService(EntitiesFixed.GetReconeService.ReconeServiceRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetReconeService.ReconeServiceResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.SuspensionService.GetReconectionService(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse EjecutaSuspensionDeServicioLte(EntitiesFixed.PostSuspensionLte.PostSuspensionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostSuspensionLte.PostSuspensionLteResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.SuspensionService.EjecutaSuspensionDeServicioLte(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        public static EntitiesFixed.PostReconexionLte.ReconexionLteResponse EjecutaReconexionDeServicioLte(EntitiesFixed.PostReconexionLte.ReconexionLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.PostReconexionLte.ReconexionLteResponse();

            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.SuspensionService.EjecutaReconexionDeServicioLte(objRequest);
                    });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
    }
}
