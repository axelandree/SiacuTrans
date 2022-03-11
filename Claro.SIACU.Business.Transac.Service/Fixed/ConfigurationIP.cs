using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntitiesFixed = Claro.SIACU.Entity.Transac.Service.Fixed;
using EntityCommon = Claro.SIACU.Entity.Transac.Service.Common;
using KEY = Claro.ConfigurationManager;
using CONSTANTS = Claro.SIACU.Transac.Service.Constants;

namespace Claro.SIACU.Business.Transac.Service.Fixed
{
    public class ConfigurationIP
    {
        public static List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> GetJobTypesConfIP(EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPRequest objJobTypesRequest)
        {
            List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse> objJobTypesResponse = new List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse>();
           
            try
            {
                objJobTypesResponse = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GetJobTypeConfigIP.JobTypesConfigIPResponse>>(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.GetJobTypesConfIP(objJobTypesRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objJobTypesRequest.Audit.Session, objJobTypesRequest.Audit.Transaction, ex.Message);
            }
            return objJobTypesResponse;
        }

        public static List<EntitiesFixed.GetDataLine.DataLineResponse> GetDataLine(EntitiesFixed.GetDataLine.DataLineRequest oBE)
        {
            List<EntitiesFixed.GetDataLine.DataLineResponse> LstDataLine = new List<EntitiesFixed.GetDataLine.DataLineResponse>();

            try
            {
                LstDataLine = Web.Logging.ExecuteMethod<List<EntitiesFixed.GetDataLine.DataLineResponse>>(oBE.Audit.Session, oBE.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.GetDataLine(oBE);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oBE.Audit.Session, oBE.Audit.Transaction, ex.Message);
            }
            return LstDataLine;
        }


        public static List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse> GetTypeConfIP(EntitiesFixed.GetTypeConfip.TypeConfigIpRequest objTypeConfigIpRequest)
        {

            List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse> LstTypeConfigIpResponse = new List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse>();
            try
            {
                LstTypeConfigIpResponse = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GetTypeConfip.TypeConfigIpResponse>>(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.GetTypeConfig(objTypeConfigIpRequest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objTypeConfigIpRequest.Audit.Session, objTypeConfigIpRequest.Audit.Transaction, ex.Message);
                throw;
            }
            return LstTypeConfigIpResponse;
        }
        public static List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse> GetBranchCustomer(EntitiesFixed.GetBranchCustomer.BranchCustomerResquest objBranchCustomerResquest)
        {

            List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse> LstBranchCustomerResponse = new List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse>();
            try
            {
                LstBranchCustomerResponse = Claro.Web.Logging.ExecuteMethod<List<EntitiesFixed.GetBranchCustomer.BranchCustomerResponse>>(objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.GetBranchCustomer(objBranchCustomerResquest);
                });
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(objBranchCustomerResquest.Audit.Session, objBranchCustomerResquest.Audit.Transaction, ex.Message);
                throw;
            }
            return LstBranchCustomerResponse;
        }

        public static EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse ConfigurationServicesSave(EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest oConfigurationIPRequest)
        {
            EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPResponse = new EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse();
            try
            {
                oConfigurationIPResponse = Claro.Web.Logging.ExecuteMethod(oConfigurationIPRequest.Audit.Session, oConfigurationIPRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.ConfigurationServicesSave(oConfigurationIPRequest);
                });
            }
            catch (Exception ex)
            {
                Web.Logging.Error(oConfigurationIPRequest.Audit.Session, oConfigurationIPRequest.Audit.Transaction, ex.Message);
                throw;
            }
           return oConfigurationIPResponse;
        }

        public static EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse GetConfigurationIPMegas(EntitiesFixed.GetConfigurationIP.ConfigurationIPRequest  oConfigurationIPMegasRequest) 
        {
            EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse oConfigurationIPMegasResponse = new EntitiesFixed.GetConfigurationIP.ConfigurationIPResponse();
            try
            {
                oConfigurationIPMegasResponse = Claro.Web.Logging.ExecuteMethod(oConfigurationIPMegasRequest.Audit.Session, oConfigurationIPMegasRequest.Audit.Transaction, () =>
                {
                    return Data.Transac.Service.Fixed.ConfigurationIP.GetConfigurationIPMegas(oConfigurationIPMegasRequest.Audit.Session, oConfigurationIPMegasRequest.Audit.Transaction, oConfigurationIPMegasRequest.strId);
                });

            }
            catch (Exception ex)
            {
                Web.Logging.Error(oConfigurationIPMegasRequest.Audit.Session, oConfigurationIPMegasRequest.Audit.Transaction, ex.Message);
                throw;
            }
            return oConfigurationIPMegasResponse;
        }
    }
}
