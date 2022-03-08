using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Web;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Configuration;

namespace Claro.SIACU{
    public class Siacu_Impersonation
    {

        public int LOGON32_LOGON_INTERACTIVE { get; set; }
        public int LOGON32_PROVIDER_DEFAULT { get; set; }
        WindowsImpersonationContext impersonationContext;
        [DllImport("advapi32.dll")]
        public static extern int LogonUserA(string lpszUsername,
                                            string lpszDomain,
                                            string lpszPassword,
                                            int dwLogonType,
                                            int dwLogonProvider,
                                            out IntPtr phToken);

        [DllImport("advapi32.dll")]
        public static extern int DuplicateToken(IntPtr ExistingTokenHandle,
                                               int ImpersonationLevel,
                                               out IntPtr DuplicateTokenHandle);


        [DllImport("advapi32.dll")]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll")]
        public static extern long CloseHandle(IntPtr handle);

        public bool impersonateValidUser(string userName, string domain, string password)
        {
            bool imper;
            try
            {
                string a = System.Configuration.ConfigurationManager.AppSettings["strLogonInteractive9"];
                LOGON32_LOGON_INTERACTIVE = Int32.Parse(a);
                string b = System.Configuration.ConfigurationManager.AppSettings["strProviderDefault"];
                LOGON32_PROVIDER_DEFAULT = Int32.Parse(b);
                WindowsIdentity tempWindowsIdentity;
                IntPtr token = IntPtr.Zero;
                IntPtr tokenDuplicate = IntPtr.Zero;
                imper = false;
                if (RevertToSelf())
                {
                    if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out token) != 0)
                    {
                        if (DuplicateToken(token, 2, out tokenDuplicate) != 0)
                        {
                            tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                            if (impersonationContext != null)
                            {
                                imper = true;
                            }
                        }
                    }
                }
                if (!(tokenDuplicate.Equals(IntPtr.Zero)))
                {
                    CloseHandle(tokenDuplicate);
                }
                if (!(token.Equals(IntPtr.Zero)))
                {
                    CloseHandle(token);
                }
            }
            catch (Exception ex)
            {
                imper = false;
            }

            return imper;
        }
        public void undoImpersonatiom()
        {

            if ((impersonationContext) != null)
            {
                impersonationContext.Undo();
            }
        }
        public string f_obtenerCredenciales(string strAplicacion)
        {
            string salida = "";
            try
            {
                ConfigurationRecord cfgBase = new ConfigurationRecord(strAplicacion);
                string user = cfgBase.LeerUsuario();
                string pwd = cfgBase.LeerContrasena();
                string domain = cfgBase.LeerBaseDatos();
                salida = user + "'|'" + domain + "'|'" + pwd;
            }
            catch (Exception)
            {

                salida = "'||'";
            }
            return salida;
        }
    }
}
