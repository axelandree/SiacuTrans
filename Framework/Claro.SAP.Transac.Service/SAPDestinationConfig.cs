﻿using System.Configuration;
using SAP.Middleware.Connector;

namespace Claro.SAP.Transac.Service
{
    public class SAPDestinationConfig : IDestinationConfiguration
    {
        public bool ChangeEventsSupported()
        {
            return false;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {
            Security.Registry registry = new Security.Registry();
            string StrNameRegeditSap = ConfigurationManager.AppSettings["KEYSAP"];
            string user = registry.RegistryReadDecryptValue(StrNameRegeditSap, "User");
            string password = registry.RegistryReadDecryptValue(StrNameRegeditSap, "Password");

            RfcConfigParameters parms = new RfcConfigParameters();

            if (ConfigurationManager.AppSettings["SAP_VERSION_4"] == "1")
            {
                parms.Add(RfcConfigParameters.Name, destinationName);
                parms.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["SERVIDOR_SAP"]);
                parms.Add(RfcConfigParameters.SystemNumber, ConfigurationManager.AppSettings["SISTEMA_SAP"]);
                parms.Add(RfcConfigParameters.SystemID, "200");
                parms.Add(RfcConfigParameters.User, user);
                parms.Add(RfcConfigParameters.Password, password);
                parms.Add(RfcConfigParameters.Client, "200");
                parms.Add(RfcConfigParameters.Language, ConfigurationManager.AppSettings["IDIOMA_SAP"]);
                parms.Add(RfcConfigParameters.PoolSize, ConfigurationManager.AppSettings["SAP_POOLSIZE_4"]);
            }
            else
            {
                parms.Add(RfcConfigParameters.Name, destinationName);
                parms.Add(RfcConfigParameters.AppServerHost, ConfigurationManager.AppSettings["SAP_APPSERVERHOST_6"]);
                parms.Add(RfcConfigParameters.SystemNumber, ConfigurationManager.AppSettings["SAP_SYSTEMNUM_6"]);
                parms.Add(RfcConfigParameters.SystemID, "200");
                parms.Add(RfcConfigParameters.User, user);
                parms.Add(RfcConfigParameters.Client, "200");
                parms.Add(RfcConfigParameters.Language, ConfigurationManager.AppSettings["SAP_LANGUAGE_6"]);
                parms.Add(RfcConfigParameters.PoolSize, ConfigurationManager.AppSettings["SAP_POOLSIZE_6"]);
            }

            return parms;
        }
    }
}

