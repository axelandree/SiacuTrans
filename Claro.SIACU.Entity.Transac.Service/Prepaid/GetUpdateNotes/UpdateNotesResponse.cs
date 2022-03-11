using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetUpdateNotes
{
    public class UpdateNotesResponse
    {
        [DataMember]
        public bool Salida { get; set; }
    }
}
