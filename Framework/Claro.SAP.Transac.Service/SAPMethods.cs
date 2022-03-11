using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP.Middleware.Connector;
using System.Data;
using System.Configuration;


namespace Claro.SAP.Transac.Service
{
    public class SAPMethods
    {
        private static RfcDestination oRfcDestination;
        private static RfcRepository oRfcRepository;
        private static IRfcFunction oIRfcFunction;

        protected SAPMethods()
        {
            string strDestinationName = (ConfigurationManager.AppSettings["SAP_VERSION_4"] == "1") ? ConfigurationManager.AppSettings["SAP_NAME_4"] : ConfigurationManager.AppSettings["SAP_NAME_6"];
            string strPlanContrato = ConfigurationManager.AppSettings["SAP_Plan_Contrato"];
            oRfcDestination = RfcDestinationManager.GetDestination(strDestinationName);
            oRfcRepository = oRfcDestination.Repository;
            oIRfcFunction = oRfcRepository.CreateFunction(strPlanContrato);
        }


        public static DataSet GetFixedCharge(string strTelephone, string strContract)
        {
            DataSet dsDatos;


            string strDestinationName = (ConfigurationManager.AppSettings["SAP_VERSION_4"] == "1") ? ConfigurationManager.AppSettings["SAP_NAME_4"] : ConfigurationManager.AppSettings["SAP_NAME_6"];

            SAPConnectorInitialise objSAPConnectorInitialise = new SAPConnectorInitialise();
            objSAPConnectorInitialise.Initialize(strDestinationName);

            oRfcDestination = RfcDestinationManager.GetDestination(strDestinationName);

            if (oRfcDestination != null)
            {
                string strTelDebt = ConfigurationManager.AppSettings["SAP_Plan_Contrato"];
                oRfcRepository = oRfcDestination.Repository;
                oIRfcFunction = oRfcRepository.CreateFunction(strTelDebt);
                oIRfcFunction.SetValue("PNUMERO_CONTRATO", strContract);
                oIRfcFunction.SetValue("PNUMERO_TELEFONO", strTelephone);
                oIRfcFunction.Invoke(oRfcDestination);

                dsDatos = new DataSet();

                dsDatos.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)oIRfcFunction.GetTable("TI_RESULTADO")));
            }
            else
            {
                dsDatos = null;
            }


            return dsDatos;
        }
    }
}
