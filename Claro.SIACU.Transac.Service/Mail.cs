using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Web;
using System.Security;

using System.Web;  
using System.Security.Principal; 


namespace Claro.SIACU.Transac.Service
{
    public class Mail
    {

       
       public bool EnviarEmailMej(string vRemitente,string vPara, string vCC,string vBCC,string vAsunto, string vMensaje, List<string> ArchivoPedido_ = null)
        {

            MailMessage oMail = new MailMessage();
            string error = string.Empty;
            List<string> Archivo = new List<string>(); 
            Archivo = ArchivoPedido_;

            StringBuilder str = new StringBuilder();    

            if (vPara.Trim().Equals("") || vMensaje.Trim().Equals("") || vAsunto.Trim().Equals(""))
            {
                return false;
            }

             try
             {
                 if (Archivo != null)
                 {
                     foreach (string archivo in Archivo)
                     {
                         if (File.Exists(@archivo))
                         {
                             oMail.Attachments.Add(new Attachment(@archivo));
                         }
                     }
                 }

                
                 oMail.IsBodyHtml = false;   
                 oMail.From = new MailAddress(vRemitente);
                 oMail.Bcc.Add(vPara);
                 oMail.Subject = vAsunto;
                 oMail.Body = vMensaje;
                 oMail.BodyEncoding = Encoding.UTF8;
                 oMail.Priority = MailPriority.Normal;
                 str.Append(AppDomain.CurrentDomain.BaseDirectory);
                 str.Append("Web.Config");
             
                 SmtpClient smtp = new SmtpClient();

                 smtp.EnableSsl = false;              
                 smtp.UseDefaultCredentials = false;  
                 smtp.Host = str.ToString();
                 smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                 smtp.Send(oMail);
                 oMail.Dispose();

                 return true;
             }
             catch (Exception ex)
             {
                return false;
              }
              finally
             {
                  oMail = null;
              }
      }
       public string EnviarEmail(string vRemitente, string vPara, string vCC, string vBCC, string vAsunto, string vMensaje, string vAdjunto)
       {
           string salida = "";
           MailMessage oMail = new MailMessage();
           string error = string.Empty;
           string[] Archivo = vAdjunto.Split('|');

           StringBuilder str = new StringBuilder();

           if (vPara.Trim().Equals("") || vMensaje.Trim().Equals("") || vAsunto.Trim().Equals(""))
           {
               return "";
           }

           try
           {
               if (Archivo != null)
               {
                   foreach (string archivo in Archivo)
                   {
                       if (File.Exists(@archivo))
                       {
                           oMail.Attachments.Add(new Attachment(@archivo));
                       }
                   }
               }

               oMail.IsBodyHtml = true;
               oMail.From = new MailAddress(vRemitente);
               oMail.To.Add(new MailAddress(vPara));
               oMail.Bcc.Add(vPara);
               oMail.CC.Add(new MailAddress(vCC));
               oMail.Subject = vAsunto;
               oMail.Body = vMensaje;
               oMail.BodyEncoding = Encoding.UTF8;
               oMail.Priority = MailPriority.Normal;
               str.Append(AppDomain.CurrentDomain.BaseDirectory);
               str.Append("Web.Config");

               SmtpClient smtp = new SmtpClient();
       
               smtp.EnableSsl = false;
               smtp.UseDefaultCredentials = false;
               smtp.Host = str.ToString();
               smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
               smtp.Send(oMail);
               oMail.Dispose();

               salida = "OK";
           }
           catch (Exception ex)
           {
               salida = ex.Message;
           }
           finally
           {
               oMail = null;
           }
           return salida;
       }
       public string EnviarEmailAttach(string vRemitente, string vPara, string vCC, string vBCC, string vAsunto, string vMensaje, string vAdjunto, string vAdjuntoArchivo)
       {
           string salida = "";
           bool blnExisteArchivo = false;
           string blnArchivoCopiado = "1";
           string error = string.Empty;
           string[] arrAdjuntos = vAdjunto.Split('|');
           string[] arrAdjuntosArchivo = vAdjuntoArchivo.Split('|');

           string fileName ="test.txt";
           string strRutaOrigen = @"d:\siac_postpago\Origen";    //RHJ: Leer el web.config
           string strRutaDestino = @"d:\siac_postpago\Destino"; //RHJ: Leer el web.config
           
           string sourceFile = System.IO.Path.Combine(strRutaOrigen,vAdjunto);
           string destFile = System.IO.Path.Combine(strRutaDestino,vAdjuntoArchivo);
           string str = String.Empty;
           StringBuilder strA = new StringBuilder();
           StringBuilder strB = new StringBuilder();
           string rol = String.Empty;

           MailMessage oMail = new MailMessage();

           oMail.IsBodyHtml = true;
           oMail.From = new MailAddress(vRemitente);
           oMail.To.Add(new MailAddress(vPara));
           oMail.Bcc.Add(vPara);
           oMail.CC.Add(new MailAddress(vCC));
           oMail.Subject = vAsunto;
           oMail.Body = vMensaje;
           oMail.BodyEncoding = Encoding.UTF8;
           oMail.Priority = MailPriority.Normal;

           try 
           {
               //WindowsImpersonationContext impersonationContext = new WindowsImpersonationContext();
               //WindowsPrincipal principal;
               //WindowsIdentity identity = principal.Identity as WindowsIdentity;
               //impersonationContext = identity.Impersonate();

               //rol = principal.IsInRole(WindowsBuiltInRole.PowerUser).ToString();

               //strB.Append("Token: ");
               //strB.Append(identity.Token.ToString());
               //strB.Append("IsGuest: ");
               //strB.Append(identity.IsGuest.ToString());
               //strB.Append("IsSystem: ");
               //strB.Append(identity.IsSystem.ToString());
               //strB.Append("Rol: ");
               //strB.Append(rol);

               foreach (string sArchivo in arrAdjuntos)
               {
                   blnExisteArchivo = false;
                   blnExisteArchivo = File.Exists(@sArchivo);
                   Console.WriteLine(String.Format("blnExisteArchivo: {0}", blnExisteArchivo));
                   Console.WriteLine(String.Format("strRutaDestino: {0}", strRutaDestino));
                   Console.WriteLine(String.Format("sArchivo: {0}", sArchivo));

                   if (blnExisteArchivo)
                   {
                       File.Copy(sourceFile, destFile);
                       Console.WriteLine(String.Format("sArchivo: {0}", sArchivo));

                       fileName = Path.GetFileName(sArchivo);
                       destFile = Path.Combine(strRutaDestino, fileName);
                       File.Copy(sArchivo, destFile, true);

                       strA.Append("Ruta Attach:").Append(strRutaDestino).Append(sArchivo);
                       Console.WriteLine(String.Format("blnArchivoCopiado: {0}", sArchivo));
                       Console.Write(strA.ToString());

                       if (blnArchivoCopiado == "1")
                       {
                           oMail.Attachments.Add(new Attachment(@sArchivo));
                       }
                   }
               }

               //impersonationContext.Undo();   

               SmtpClient smtp = new SmtpClient();

               smtp.EnableSsl = false;
               smtp.UseDefaultCredentials = false;
               smtp.Host = strRutaDestino; 
               smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
               smtp.Send(oMail);
               oMail.Dispose();

               salida = "OK";
           }
           catch (Exception ex)
           {
               Console.WriteLine(String.Format("Error: {0}", ex.Message));
               salida = ex.Message;
               blnArchivoCopiado = "0";
           }
           finally
           {
               oMail = null;
           }
           Console.WriteLine(String.Format("Salida: {0}", salida));
           return salida;
       }

       public string EnviarEmailAlt(string vRemitente, string vPara, string vCC, string vBCC, string vAsunto, string vMensaje, string vAdjunto, string vAdjuntoArchivo, ref string vOperacion)
       {
           string salida = "";
           int i = 0;
           bool blnExisteArchivo = false;
           string blnArchivoCopiado = "1";
           string error = string.Empty;
          

           string fileName = "test.txt";
           string strRutaOrigen = @"d:\siac_postpago\Origen";   //RHJ: Pendiente leer Key de Web.Config
           string strRutaDestino = @"d:\siac_postpago\Destino"; //RHJ: Pendiente leer Key de Web.Config

           string sourceFile = Path.Combine(strRutaOrigen, vAdjunto);
           string destFile = Path.Combine(strRutaDestino, vAdjuntoArchivo);
           MailMessage oMail = new MailMessage();
           string str = String.Empty;
           StringBuilder strB = new StringBuilder();

           string rol = String.Empty;

           if (vCC.Trim().Equals("") || vBCC.Trim().Equals("") )
           {
               return "0";
           }
           oMail.Subject = vAsunto;
           oMail.IsBodyHtml = true;
           oMail.From = new MailAddress(vRemitente);
           oMail.To.Add(new MailAddress(vPara));
           oMail.Bcc.Add(vPara);
           oMail.CC.Add(new MailAddress(vCC));
           oMail.Body = vMensaje;
           oMail.BodyEncoding = Encoding.UTF8;
           oMail.Priority = MailPriority.Normal;

           try
           {

               //WindowsImpersonationContext impersonationContext = new WindowsImpersonationContext();
               //WindowsPrincipal principal ;
               //WindowsIdentity identity = principal.Identity as WindowsIdentity;
               //impersonationContext = identity.Impersonate();
               
               //rol = principal.IsInRole(WindowsBuiltInRole.PowerUser).ToString();

               //strB.Append("Token: ");
               //strB.Append(identity.Token.ToString());
               //strB.Append("IsGuest: ");
               //strB.Append(identity.IsGuest.ToString());
               //strB.Append("IsSystem: ");
               //strB.Append(identity.IsSystem.ToString());
               //strB.Append("Rol: ");
               //strB.Append(rol);

               string[] arrAdjuntos = vAdjunto.Split('|');
               string[] arrAdjuntosArchivo = vAdjuntoArchivo.Split('|');

               //File.Copy(sourceFile, destFile);
               //FileStream oFileStream = new FileStream();
               //byte[] arrBuffer = null;
               //arrBuffer = Encoding.ASCII.GetBytes("");


                   string[] file = System.IO.Directory.GetFiles(sourceFile);
                   StringBuilder st = new StringBuilder();

                   foreach (string sArchivo in arrAdjuntos)
                   {
                       if (arrAdjuntos != null)
                       {
                           blnExisteArchivo = false;
                           blnExisteArchivo = File.Exists(@sArchivo);
                           Console.WriteLine(String.Format("blnExisteArchivo: {0}", blnExisteArchivo));
                           Console.WriteLine(String.Format("strRutaDestino: {0}", strRutaDestino));
                           Console.WriteLine(String.Format("sArchivo: {0}", sArchivo));

                           if (blnExisteArchivo)
                           {

                               Console.WriteLine(String.Format("sArchivo: {0}", sArchivo));
                               //fileName = System.IO.Path.GetFileName(sArchivo);
                               //destFile = System.IO.Path.Combine(strRutaDestino, fileName);
                               //System.IO.File.Copy(sArchivo, destFile, true);

                               st.Append("Ruta Attach:").Append(strRutaDestino).Append(sArchivo);
                               Console.WriteLine(String.Format("blnArchivoCopiado: {0}", sArchivo));
                               Console.Write(st.ToString());

                               if (blnArchivoCopiado == "1")
                               {
                                   oMail.Attachments.Add(new Attachment(@sArchivo));
                               }
                           }
                       }
                       i++;
                   }
              

               SmtpClient smtp = new SmtpClient();

               smtp.EnableSsl = false;
               smtp.UseDefaultCredentials = false;
               smtp.Host = strRutaDestino; 
               smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
               smtp.Send(oMail);
               oMail.Dispose();

               salida = "OK";
           }
           catch (Exception ex)
           {
               Console.WriteLine(String.Format("Error: {0}", ex.Message));
               salida = ex.Message;
               blnArchivoCopiado = "0";
           }
           finally
           {
               oMail = null;
           }
           Console.WriteLine(String.Format("Salida: {0}", salida));
           return salida;
       }
    
    }
}
