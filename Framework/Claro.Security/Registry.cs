using Claro.Security.Cryptography;
using System.Text;

namespace Claro.Security
{
    public class Registry
    {
        public static readonly DES Crypto = CreateCrypto();
        public const string TIMRootRegistry = @"SOFTWARE\TIM";

        public Registry()
        {
            
        }

        #region CreateCrypto

        private static DES CreateCrypto()
        {
            string strSecretKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry).GetValue("key").ToString();

            DES crypto = new Cryptography.DES(strSecretKey);

            return crypto;
        }

        #endregion

        #region RegistryReadValue

        public static string RegistryReadValue(string key, string value)
        {
            return Microsoft.Win32.Registry.LocalMachine.OpenSubKey(TIMRootRegistry + @"\" + key).GetValue(value, "").ToString();
        }

        #endregion

        #region RegistryReadDecryptValue

        public string RegistryReadDecryptValue(string key, string value)
        {
            string strPassword = RegistryReadValue(key, value);

            byte[] ByteArray = Encoding.Default.GetBytes(strPassword);

            Crypto.DecryptByte(ref ByteArray);

            return Encoding.Default.GetString(ByteArray);
        }

        #endregion
    }
}
