using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard
{
    //INICIATIVA-871
    public class DatosSIMPrepago
    {
        public string idLote { get; set; }
        public string fechaReg { get; set; }
        public string iccid { get; set; }
        public string imsi { get; set; }
        public string pin { get; set; }
        public string puk { get; set; }
        public string pin2 { get; set; }
        public string puk2 { get; set; }
        public string adm { get; set; }
        public string cmdCrear { get; set; }
        public string cmdCreudb { get; set; }
        public string estadoCod { get; set; }
        public string estadoDes { get; set; }
        public string r { get; set; }
    }
}
