using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.AdditionalServices
{
    public class BEInteraction
    {
        //public BEInteraccion() { }

        public string OBJID_CONTACTO { get; set; }
        public string OBJID_SITE { get; set; }
        public string CUENTA { get; set; }
        public string ID_INTERACCION { get; set; }
        public string FECHA_CREACION { get; set; }
        public string START_DATE { get; set; }
        public string TELEFONO { get; set; }
        public string TIPO { get; set; }
        public string CLASE { get; set; }
        public string SUBCLASE { get; set; }
        public string TIPIFICACION { get; set; }
        public string TIPO_CODIGO { get; set; }
        public string CLASE_CODIGO { get; set; }
        public string SUBCLASE_CODIGO { get; set; }
        public string INSERTADO_POR { get; set; }
        public string TIPO_INTER { get; set; }
        public string METODO { get; set; }
        public string RESULTADO { get; set; }
        public string HECHO_EN_UNO { get; set; }
        public string AGENTE { get; set; }
        public string NOMBRE_AGENTE { get; set; }
        public string APELLIDO_AGENTE { get; set; }
        public string ID_CASO { get; set; }
        public string NOTAS { get; set; }
        public string FLAG_CASO { get; set; }
        public string USUARIO_PROCESO { get; set; }
        public string SERVICIO { get; set; }
        public string INCONVENIENTE { get; set; }

        public string SERVICIO_CODE { get; set; }
        public string INCONVENIENTE_CODE { get; set; }
        public string CONTRATO { get; set; }
        public string PLANO { get; set; }
        public string VALOR_1 { get; set; }
        public string VALOR_2 { get; set; }
    }
}