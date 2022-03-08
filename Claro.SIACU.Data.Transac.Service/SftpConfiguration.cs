using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Data.Transac.Service
{
    internal struct SftpConfiguration : ISftpConfiguration
    {
        public static readonly ISftpConfiguration SICES_FtpEquipment = SftpConfiguration.Create("SICES_SFTP_Equipment");
        private string m_name;

        public string Name
        {
            get
            {
                return this.m_name;
            }
        }

        private void SetName(string name)
        {
            this.m_name = name;
        }

        private static ISftpConfiguration Create(string name)
        {
            SftpConfiguration helper = new SftpConfiguration();

            helper.SetName(name);

            return helper;
        }
    }
}
