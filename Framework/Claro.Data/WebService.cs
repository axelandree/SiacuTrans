using Claro.Data.Configuration;
using System;

namespace Claro.Data
{
    public static class WebService
    {
        public static TChannel Create<TChannel>(string name) where TChannel : class
        {
            if (name == null)
            {
                throw new ArgumentNullException("Name");
            }

            if (name.Length == 0)
            {
                throw new ArgumentException("Name", "Size non-zero.");
            }

            WebServiceConfigurationElement cfgSservice = DataSettings.WebServices[name];

            if (cfgSservice == null)
            {
                throw new ArgumentException(string.Format("The key \"{0}\" not found in webservice section config.", name));
            }

            TChannel webservice = Activator.CreateInstance<TChannel>();

            Type webserviceType = typeof(TChannel);

            webserviceType.GetProperty("Url").SetValue(webservice, cfgSservice.Url);
            webserviceType.GetProperty("Timeout").SetValue(webservice, cfgSservice.Timeout);

            return webservice;
        }
    }
}
