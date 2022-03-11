using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Constant = Claro.SIACU.Transac.Service;
using System.Threading.Tasks;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using FIXED = Claro.SIACU.Entity.Transac.Service.Fixed;
using COMMON = Claro.SIACU.Entity.Transac.Service.Common;
using DATA = Claro.SIACU.Data.Transac.Service.Fixed;
using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using ConstantsHFC = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class ExternalInternalTransfer
    {


        public static bool ActualizarDatosDireccionPostal(FIXED.GetRecordTransExtInt.RecordTranferExtIntRequest objGetRecordTransactionRequest)
        {
            bool Result = true;
            //{
            //    ItemGenerateSOT = Claro.Web.Logging.ExecuteMethod<ListItem>(
            //    () =>
            //    {
            //        return DATA.HfcTransfer.ActualizarDatosDireccionPostal(strIdSession,strTransaction, strCustomerCode)
            //            );

            //    })
            //};
            return Result;

        }


        public static FIXED.GetRecordTransExtInt.RecordTranferExtIntResponse GetRecordTransaction(FIXED.GetRecordTransExtInt.RecordTranferExtIntRequest objGetRecordTransactionRequest)
        {

            FIXED.GetRecordTransExtInt.RecordTranferExtIntResponse objGetRecordTransactionResponse =
            Claro.Web.Logging.ExecuteMethod<FIXED.GetRecordTransExtInt.RecordTranferExtIntResponse>
            (objGetRecordTransactionRequest.Audit.Session, objGetRecordTransactionRequest.Audit.Transaction, () =>
            {

                return DATA.ExternalInternalTransfer.GetRecordTransaction(objGetRecordTransactionRequest.Audit.Session,
                            objGetRecordTransactionRequest.Audit.Transaction, objGetRecordTransactionRequest);

            });


            return objGetRecordTransactionResponse;
        }


        public static FIXED.GetGenerateSOT.GenerateSOTResponse GetGenerateSOT(FIXED.GetGenerateSOT.GenerateSOTRequest objGetGenerateSOTRequest)
        {
            Claro.SIACU.Entity.Transac.Service.Common.ListItem ItemGenerateSOT = new Claro.SIACU.Entity.Transac.Service.Common.ListItem();
            ItemGenerateSOT = Claro.Web.Logging.ExecuteMethod<Claro.SIACU.Entity.Transac.Service.Common.ListItem>(
               objGetGenerateSOTRequest.Audit.Session, objGetGenerateSOTRequest.Audit.Transaction,
               () =>
               {
                   return DATA.ExternalInternalTransfer.GetGenerateSOT(objGetGenerateSOTRequest.Audit.Session,
                     objGetGenerateSOTRequest.Audit.Transaction
                     , objGetGenerateSOTRequest.vCusID
                     , objGetGenerateSOTRequest.vCoID
                     , objGetGenerateSOTRequest.vTipTra
                     , objGetGenerateSOTRequest.vFeProg
                     , objGetGenerateSOTRequest.vFranja
                     , objGetGenerateSOTRequest.vCodMotivo
                     , objGetGenerateSOTRequest.vObserv
                     , objGetGenerateSOTRequest.vPlano
                     , objGetGenerateSOTRequest.vUser
                     , objGetGenerateSOTRequest.idTipoServ
                     , objGetGenerateSOTRequest.Cargo
                       );

               });
            FIXED.GetGenerateSOT.GenerateSOTResponse objGetGenerateSOTResponse = new FIXED.GetGenerateSOT.GenerateSOTResponse()
            {
                IdGenerateSOT = ItemGenerateSOT.Code,
                CodMessaTransfer = ItemGenerateSOT.Code2,
                DescMessaTransfer = ItemGenerateSOT.Description
            };
            return objGetGenerateSOTResponse;
        }

        public static EntitiesFixed.GetDecoServices.BEDecoServicesResponse GetServicesDTH(EntitiesFixed.GetDecoServices.BEDecoServicesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetDecoServices.BEDecoServicesResponse();

            try
            {
                var ListDecoServices = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.BEDeco>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetServicesDTH(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.vCusID, objRequest.vCoID);
                    });

                objResponse.ListDecoServices = ListDecoServices;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;
        }

        //obtener lista de centro poblados
        public static EntitiesFixed.GetListTownCenter.ListTownCenterResponse GetListTownCenter(EntitiesFixed.GetListTownCenter.ListTownCenterRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetListTownCenter.ListTownCenterResponse();
            try
            {
                var  ListTownCenter=Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session,objRequest.Audit.Transaction,
                    ()=>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetListTownCenter(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strUbigeo);
                    });
                objResponse.ListItem = ListTownCenter;
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }
            return objResponse;
         
        }

        //obtener lista ubigeo desde ddp
        public static EntitiesFixed.GetIdUbigeo.IdUbigeoResponse GetIdUbigeo(EntitiesFixed.GetIdUbigeo.IdUbigeoRequest objrequest)
        {
            objrequest.strState = Constant.Constants.strLetraA;
            var objResponse = new EntitiesFixed.GetIdUbigeo.IdUbigeoResponse();

            try
            {
                var strIdUbigeo = Claro.Web.Logging.ExecuteMethod<string>(objrequest.Audit.Session, objrequest.Audit.Transaction,
                    () =>
                    {

                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetUbigeoId(objrequest.Audit.Session, objrequest.Audit.Transaction, objrequest.strDepartment, objrequest.strState, objrequest.strProvince, objrequest.strDistrict);
                    });
                objResponse.strIdUbigeo = strIdUbigeo;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objrequest.Audit.Session, objrequest.Audit.Transaction, ex.Message);
                
            }
            return objResponse;
        }

        //obtener lista de planos
        public static EntitiesFixed.GetListPlans.ListPlansResponse GetListPlans(EntitiesFixed.GetListPlans.ListPlansRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetListPlans.ListPlansResponse();
            try
            {
                var ListPlans = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetListPlans(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strIdUbigeo);
                    });
                objResponse.ListPlans = ListPlans;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                
            }
            return objResponse;
        
        }
        //obtner lista de edificaciones

        public static EntitiesFixed.GetListEbuildings.ListEbuildingsResponse GetListEBuildings(EntitiesFixed.GetListEbuildings.ListEbuildingsRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetListEbuildings.ListEbuildingsResponse();
            try
            {
                var ListEbuildings = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GenericItem>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetListEBuildings(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCodePlan);

                    });
                objResponse.ListEbuildings = ListEbuildings;

            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                
                
            }
            return objResponse;
        
        }

        //obtener cobertura

        public static EntitiesFixed.GetCoverage.CoverageResponse GetCoverage(EntitiesFixed.GetCoverage.CoverageRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetCoverage.CoverageResponse();
            try
            {
                var strIdCoverage = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetCoverage(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCob);
                    });
                objResponse.strCoverage = strIdCoverage;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
          
            }
            return objResponse;
        
        }

        public static EntitiesFixed.GetAddressUpdate.AddressUpdateResponse GetUpdateAddress(EntitiesFixed.GetAddressUpdate.AddressUpdateRequest objRequest)
        {
           bool Result = false;
           EntitiesFixed.GetAddressUpdate.AddressUpdateResponse objAddressUpdateResponse = new EntitiesFixed.GetAddressUpdate.AddressUpdateResponse();
            try
            {
                Result = Claro.Web.Logging.ExecuteMethod<bool>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {                     
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetUpdateAddress(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest);
                    });
                objAddressUpdateResponse.blnResult = Result;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);

            }
            return objAddressUpdateResponse;

        }
        #region External/Internal - LTE
        
        public static EntitiesFixed.GetMotiveSoft.MotiveSoftResponse GetMotiveSoftLte(EntitiesFixed.GetMotiveSoft.MotiveSoftRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetMotiveSoft.MotiveSoftResponse();
            try
            {
                var listMotiveSoft = Claro.Web.Logging.ExecuteMethod(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetMotiveSoftLte(objRequest.Audit.Session, objRequest.Audit.Transaction);
                    });
                objResponse.listMotiveSoft = listMotiveSoft;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw;
            }
            return objResponse;

        }
        //obtener tipo de trabajo

        public static EntitiesFixed.GetJobTypes.JobTypesResponse GetJobTypeLte(EntitiesFixed.GetJobTypes.JobTypesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetJobTypes.JobTypesResponse();
            try
            {
                var listJobTypes = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.JobType>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetJobTypeLte(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_tipo);
                    });
                objResponse.JobTypes = listJobTypes;
            }
            catch (Exception ex)
            {

                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
            }

            return objResponse;

        }

        //obtener servicios de lte
        public static EntitiesFixed.GetServicesLte.ServicesLteResponse GetServicesLte(EntitiesFixed.GetServicesLte.ServicesLteRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetServicesLte.ServicesLteResponse();
            try
            {
                var listServicesLte = Web.Logging.ExecuteMethod<List<EntitiesFixed.BEDeco>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetServicesLte(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.strCustomerId, objRequest.strCoid);

                    });
                objResponse.ListServicesLte = listServicesLte;
            }
            catch (Exception ex)
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);

            }
            return objResponse;
        }

        public static EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse RegisterTransactionLTE(EntitiesFixed.GetRegisterTransaction.RegisterTransactionRequest objRequest)
        {
            var objResponse= new EntitiesFixed.GetRegisterTransaction.RegisterTransactionResponse();

            try
            {
                int intRescod = 0;
                string strResDes = string.Empty;
                
                objResponse.intNumSot = Claro.Web.Logging.ExecuteMethod<string>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Claro.SIACU.Data.Transac.Service.Fixed.ExternalInternalTransfer.RegisterTransactionLTE(
                            objRequest.Audit.Session,
                            objRequest.Audit.Transaction,
                            objRequest.objRegisterTransaction,
                            out intRescod,
                            out strResDes);
                            
                           
                    });

                objResponse.intResCod = intRescod;
                objResponse.strResDes = strResDes;
            }
            catch (Exception ex )
            {
                Web.Logging.Error(objRequest.Audit.Session, objRequest.Audit.Transaction, ex.Message);
                throw;
            }
            return objResponse;
        
        }

        #endregion

        //PROY140315 - Inicio
        public static Claro.SIACU.Entity.Transac.Service.Fixed.GetJobTypes.JobTypesResponse GetJobTypeDTH(EntitiesFixed.GetJobTypes.JobTypesRequest objRequest)
        {
            var objResponse = new EntitiesFixed.GetJobTypes.JobTypesResponse();
            try
            {
                var listJobTypes = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.JobType>>(objRequest.Audit.Session, objRequest.Audit.Transaction,
                    () =>
                    {
                        return Data.Transac.Service.Fixed.ExternalInternalTransfer.GetJobTypeDTH(objRequest.Audit.Session, objRequest.Audit.Transaction, objRequest.p_tipo);
                    });
                objResponse.JobTypes = listJobTypes;
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