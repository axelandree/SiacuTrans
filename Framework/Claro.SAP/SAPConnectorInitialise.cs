using SAP.Middleware.Connector;
using System;
using System.Data;

namespace Claro.SAP
{
    public class SAPConnectorInitialise
    {
        IDestinationConfiguration destinationConfig = null;
        bool destinationIsInitialised = false;

        public void Initialize(string destinationConfigName)
        {
            if (!destinationIsInitialised)
            {
                destinationConfig = new SAPDestinationConfig();

                destinationConfig.GetParameters(destinationConfigName);
                if (TryGetDestination(destinationConfigName) == null)
                {
                    RfcDestinationManager.RegisterDestinationConfiguration(destinationConfig);
                    destinationIsInitialised = true;
                }
            }
        }

        public RfcDestination TryGetDestination(string destinationConfigName)
        {
            RfcDestination oRfcDestination = null;
            try
            {
                oRfcDestination = RfcDestinationManager.GetDestination(destinationConfigName);
            }
            catch (Exception ex)
            {
                oRfcDestination = null;
            }
            return oRfcDestination;
        }

        public static DataTable GetDataTableFromRFCTable(IRfcTable lrfcTable)
        {
            DataTable loTable = new DataTable();

            try
            {
                for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
                {
                    RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
                    loTable.Columns.Add(metadata.Name);
                }

                foreach (IRfcStructure row in lrfcTable)
                {
                    DataRow ldr = loTable.NewRow();
                    for (int liElement = 0; liElement < lrfcTable.ElementCount; liElement++)
                    {
                        RfcElementMetadata metadata = lrfcTable.GetElementMetadata(liElement);
                        ldr[metadata.Name] = row.GetString(metadata.Name);
                    }
                    loTable.Rows.Add(ldr);
                }

                return loTable;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
