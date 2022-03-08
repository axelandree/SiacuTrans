using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    public class ItemMigratedListsDB
    {
        [Data.DbColumn("CODIGO")]
        public int CODIGO { get; set; }

        [Data.DbColumn("NOMBRE")]
        public string NOMBRE { get; set; }

        [Data.DbColumn("TIPO")]
        public string TIPO { get; set; }

        [Data.DbColumn("RANK")]
        public int RANK { get; set; }

        [Data.DbColumn("CAC_TYPE_CODELE")]
        public int CAC_TYPE_CODELE { get; set; }

        [Data.DbColumn("CAC_TYPE_TITLE")]
        public string CAC_TYPE_TITLE { get; set; }
  
    }
}
