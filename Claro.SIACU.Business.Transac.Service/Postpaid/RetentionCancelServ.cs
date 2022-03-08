using Claro.SIACU.Entity.Transac.Service.Postpaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Business.Transac.Service.Postpaid
{
    public class RetentionCancelServ
    {
        public static RetentionCancel GetDataAccord(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancel>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Postpaid.RetentionCancelServ.GetDataAccord(oRequest);
                });
            
            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }

        public static RetentionCancel GetLoadStaidTotal(RetentionCancel oRequest)
        {
            RetentionCancel oResponse = new RetentionCancel();

            try
            {
                oResponse = Claro.Web.Logging.ExecuteMethod<RetentionCancel>(oRequest.Audit.Session, oRequest.Audit.Transaction, () =>
                {
                    return Claro.SIACU.Data.Transac.Service.Postpaid.RetentionCancelServ.GetLoadStaidTotal(oRequest);
                });

            }
            catch (Exception ex)
            {
                Claro.Web.Logging.Error(oRequest.Audit.Session.ToString(), oRequest.Audit.Transaction, ex.Message);
                throw ex;
            }

            return oResponse;
        }
    }
}
