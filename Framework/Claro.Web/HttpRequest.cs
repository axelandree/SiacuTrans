using System.Web;


namespace Claro.Web
{
    public static class HttpRequest
    {

        public static string UserHostAddress(string[] names)
        {
            string userHostAddress = null;

            foreach (string name in names)
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables[name];

                if (!string.IsNullOrEmpty(userHostAddress))
                {
                    break;
                }

            }

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;

                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = HttpContext.Current.Request.UserHostName;
                }
            }

            return userHostAddress;
        }


    }
}
