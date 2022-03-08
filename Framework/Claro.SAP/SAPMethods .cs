using SAP.Middleware.Connector;
using System;
using System.Data;

namespace Claro.SAP
{
    public class SAPMethods
    {
        private static RfcDestination oRfcDestination;
        private static RfcRepository oRfcRepository;
        private static IRfcFunction oIRfcFunction;

        protected SAPMethods()
        {

        }

        public static string GetKUNNR()
        {
            return GetValue("KUNNR").ToString();
        }
        public static Decimal GetMWSBK()
        {
            return Convert.ToDecimal(GetValue("MWSBK"));
        }
        public static string GetNAME1()
        {
            return GetValue("NAME1").ToString();
        }
        public static Decimal GetNETWR()
        {
            return Convert.ToDecimal(GetValue("NETWR"));
        }

        public static string GetSTCD1()
        {
            return GetValue("STCD1").ToString();
        }
        public static Decimal GetTOTAL()
        {
            return Convert.ToDecimal(GetValue("TOTAL"));
        }
        private static object GetValue(string value)
        {
            return oIRfcFunction.GetValue(value);
        }

        public static DataSet GetAddressOfficeRfc(string StrDocumentNumber)
        {
            DataSet dsDatos;
            string strDestinationName = (System.Configuration.ConfigurationManager.AppSettings["SAP_VERSION_4"] == "1") ? System.Configuration.ConfigurationManager.AppSettings["SAP_NAME_4"] : System.Configuration.ConfigurationManager.AppSettings["SAP_NAME_6"];

            SAPConnectorInitialise objSAPConnectorInitialise = new SAPConnectorInitialise();
            objSAPConnectorInitialise.Initialize(strDestinationName);
            if (oRfcDestination == null)
            {
                oRfcDestination = RfcDestinationManager.GetDestination(strDestinationName);

            }
            if (oRfcDestination != null)
            {
                string strAddressOffice = System.Configuration.ConfigurationManager.AppSettings["SAP_AddressOfficeRfc"];
                oRfcRepository = oRfcDestination.Repository;
                oIRfcFunction = oRfcRepository.CreateFunction(strAddressOffice);
                oIRfcFunction.SetValue("STCD1", StrDocumentNumber);
                oIRfcFunction.Invoke(oRfcDestination);
                dsDatos = new DataSet();

                dsDatos.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)oIRfcFunction.GetTable("TI_DIRECCIONES")));
            }
            else
            {
                dsDatos = null;
            }
            return dsDatos;
        }


        public static DataSet GetDebt(string strBukrs, string strTelephone)
        {
            DataSet dsDatos;


            string strDestinationName = (System.Configuration.ConfigurationManager.AppSettings["SAP_VERSION_4"] == "1") ? System.Configuration.ConfigurationManager.AppSettings["SAP_NAME_4"] : System.Configuration.ConfigurationManager.AppSettings["SAP_NAME_6"];

            SAPConnectorInitialise objSAPConnectorInitialise = new SAPConnectorInitialise();
            objSAPConnectorInitialise.Initialize(strDestinationName);

            oRfcDestination = RfcDestinationManager.GetDestination(strDestinationName);

            if (oRfcDestination != null)
            {
                string strTelDebt = System.Configuration.ConfigurationManager.AppSettings["SAP_Tel_Debt"];
                oRfcRepository = oRfcDestination.Repository;
                oIRfcFunction = oRfcRepository.CreateFunction(strTelDebt);
                oIRfcFunction.SetValue("BUKRS", strBukrs);
                oIRfcFunction.SetValue("NRO_TELEF", strTelephone);
                oIRfcFunction.Invoke(oRfcDestination);

                dsDatos = new DataSet();

                dsDatos.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)oIRfcFunction.GetTable("TI_CUOTAS")));
                dsDatos.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)oIRfcFunction.GetTable("TI_RETURN")));
            }
            else
            {
                dsDatos = null;
            }


            return dsDatos;
        }
        public static string GetmATNRField()
        {
            return GetValue("mATNRField").ToString();
        }
        public static string GetlGORTField()
        {
            return GetValue("lGORTField").ToString();
        }
        public static string GetlGOBEField()
        {
            return GetValue("lGOBEField").ToString();
        }
        public static string GetlABSTField()
        {
            return GetValue("lABSTField").ToString();
        }

        public static string GetmAKTXField()
        {
            return GetValue("mAKTXField").ToString();
        }
        /// <summary>
        /// Consulta stock almacen
        /// </summary>
        /// <param name="strSerie"></param>   
        /// <param name="strDescripcion"></param>   
        /// <param name="strRegion"></param>   
        /// <param name="errorMessage"></param>   
        /// <returns>DataSet</returns>
        /// <remarks><list type="bullet">
        /// <item><CreadoPor>Creado por.</CreadoPor></item>
        /// <item><FecCrea>XX/XX/XXXX.</FecCrea></item></list>
        /// <list type="bullet">
        /// <item><FecActu>15/09/2015.</FecActu></item>
        /// <item><Resp></Resp></item>
        /// <item><Mot>Evaluación del Control Interno de códigos fuente.</Mot></item></list></remarks>
        public static DataSet GetStockWarehouse(string strSerie, string strDescripcion, string strRegion)
        {

            string destinationName = System.Configuration.ConfigurationManager.AppSettings["SAP_NAME_6"].ToString();
            oRfcDestination = RfcDestinationManager.GetDestination(destinationName);
            SAPConnectorInitialise objSAPConnectorInitialise = new SAPConnectorInitialise();
            objSAPConnectorInitialise.Initialize(destinationName);
            DataSet resultado = new DataSet();
            if (oRfcDestination == null)
            {
                oRfcDestination = RfcDestinationManager.GetDestination(destinationName);
            }
            if (oRfcDestination != null)
            {
                RfcRepository rfcRepository = oRfcDestination.Repository;
                IRfcFunction rfcFunction = rfcRepository.CreateFunction("ZSIAC_CON_STOCK_X_ALMACEN");
                rfcFunction.SetValue("P_MATNR", strSerie);
                rfcFunction.SetValue("P_MAKTX", strDescripcion);
                rfcFunction.SetValue("P_REGION", strRegion);
                rfcFunction.Invoke(oRfcDestination);
                resultado.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)rfcFunction.GetTable("TI_RETURN")));
                resultado.Tables.Add(SAPConnectorInitialise.GetDataTableFromRFCTable((IRfcTable)rfcFunction.GetTable("TI_STOCK")));
            }


            return resultado;
        }


    }
}
