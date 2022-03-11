using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common
{
    //se registra interaccion subclase detalle
   public class InteractionSubClasDetail
    {
       public string CASOID { get; set; }
       public string INTERACT_ID { get; set; }
       public string TIPO { get; set; }
       public string CLASE { get; set; }
       public string SUBCLASE { get; set; }
       public string SUBCLASEDET { get; set; }
       public string TIPO_CODIGO { get; set; }
       public string CLASE_CODIGO { get; set; }
       public string SUBCLASE_CODIGO { get; set; }
       public string SUBCLASEDET_CODIGO { get; set; }
       public string USUARIO_PROCESO { get; set; }
       public string SERVAFECT_CODE { get; set; }
       public string SERVAFECT { get; set; }
       public string INCONVEN_CODE { get; set; }
       public string INCONVEN { get; set; }

    }
}
