using System;
using System.Collections.Generic;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class ChangeEquipment
    {
        public static Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteResponse GetCustomerEquipments(Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteResponse();
            try
            {
                var listServicesLte = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ChangeEquipment.GetEquipments(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCustomerId, objRequest.strCoid);

                    });
                objResponse.ListServicesLte = listServicesLte;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetDispEquipment.DispEquipmentResponse GetValidateEquipment(Entity.Transac.Service.Fixed.GetDispEquipment.DispEquipmentRequest objDispEquipmentRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetDispEquipment.DispEquipmentResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objDispEquipmentRequest.Audit.Session, objDispEquipmentRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ChangeEquipment.GetValidateEquipment(objDispEquipmentRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objDispEquipmentRequest.Audit.Session, objDispEquipmentRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardBSCS(Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session,objRequest.Audit.Transaction,() =>
                  {
                        return Data.Transac.Service.Fixed.ChangeEquipment.GetAvailabilitySimcardBSCS(objRequest);
                  });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }

        public static Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetAvailabilitySimcardSANS(Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ChangeEquipment.GetAvailabilitySimcardSANS(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipment(Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ChangeEquipment.GetExecuteChangeEquipment(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            } 

            return objResponse;
        }
        public static Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse GetValidateSimcardBSCS_HLCODE(Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetAvailabilitySimcard.AvailabilitySimcardResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ChangeEquipment.GetValidateSimcardBSCS_HLCODE(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        //PROY140315 - Inicio
        public static Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteResponse GetCustomerEquipmentsDTH(Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetServicesLte.ServicesLteResponse();
            try
            {
                var listServicesLte = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                    {
                        return Data.Transac.Service.Fixed.ChangeEquipment.GetEquipmentsDTH(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCustomerId, objRequest.strCoid);

                    });
                objResponse.ListServicesLte = listServicesLte;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
        }
        public static Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentResponse GetExecuteChangeEquipmentDTH(Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentRequest objRequest)
        {
            var objResponse = new Entity.Transac.Service.Fixed.GetChangeEquipment.ChangeEquipmentResponse();
            try
            {
                objResponse = Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ChangeEquipment.GetExecuteChangeEquipmentDTH(objRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }
        //PROY140315 - Fin
    }
}
