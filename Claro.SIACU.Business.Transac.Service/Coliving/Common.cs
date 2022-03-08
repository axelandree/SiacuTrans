using Claro.SIACU.Entity.Transac.Service.Coliving;
using Claro.SIACU.Entity.Transac.Service.Coliving.GetParameter;
using System.Collections.Generic;

namespace Claro.SIACU.Business.Transac.Service.Coliving
{
    public class Common
    {
        #region [INICIATIVA 217]
        public static GetParameterResponse GetParameter(string name, string strIdSession, string strTransaction)
        {
            string Message = string.Empty;
            GetParameterResponse objResponse = new GetParameterResponse();

            objResponse = Claro.Web.Logging.ExecuteMethod<GetParameterResponse>(strIdSession, strTransaction, () =>
                 {
                     return Data.Transac.Service.Coliving.Common.GetParameter(name, strIdSession, strTransaction);
                 });
            return objResponse;
        }

        public static List<ListItems> GetDocumentTypeTOBE(string strIdSession, string strTransaction, string strCodCargaDdl)
        {
            List<ListItems> listItem = null;

            listItem = Claro.Web.Logging.ExecuteMethod<List<ListItems>>(strIdSession, strTransaction, () =>
            {
                return Data.Transac.Service.Coliving.Common.GetDocumentTypeTOBE(strIdSession, strTransaction, strCodCargaDdl);
            });
            return listItem;
        }
        #endregion
    }
}

