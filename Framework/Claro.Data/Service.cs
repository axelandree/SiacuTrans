using Claro.Data.Configuration;
using System;
using System.ServiceModel;

namespace Claro.Data
{
    public static class Service
    {
        public static TChannel Create<TChannel>(string name) where TChannel : ICommunicationObject
        {
            if (name == null)
            {
                throw new ArgumentNullException("Name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Name", "Size non-zero.");
            }

            ServiceConfigurationElement cfgSservice = DataSettings.Services[name];

            if (cfgSservice == null)
            {
                throw new ArgumentException(string.Format("The key \"{0}\" not found in service section config.", name));
            }

            EndpointAddress endPoint = new EndpointAddress(cfgSservice.Url);


            BasicHttpBinding binding = new BasicHttpBinding()
            {
                Name = cfgSservice.BindingName,
                MaxBufferPoolSize = cfgSservice.MaxBufferPoolSize,
                MaxBufferSize = cfgSservice.MaxBufferSize,
                MaxReceivedMessageSize = cfgSservice.MaxReceivedMessageSize,
                CloseTimeout = cfgSservice.CloseTimeout,
                OpenTimeout = cfgSservice.OpenTimeout,
                ReceiveTimeout = cfgSservice.ReceiveTimeout,
                SendTimeout = cfgSservice.SendTimeout,
            };
            //service webconfig
            if (cfgSservice.ClientCredentialType.Equals("Windows", StringComparison.CurrentCultureIgnoreCase))
            {
                //autenticacion windows
                binding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport = new HttpTransportSecurity()
                {
                    ClientCredentialType = HttpClientCredentialType.Windows
                };
            }


            TChannel service = (TChannel)Activator.CreateInstance(typeof(TChannel), new object[] { binding, endPoint });

            return service;
        }
    }
}
