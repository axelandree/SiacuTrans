using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.IncomingCallDetail
{
    public class IncomingCall
    {
        public string strIdSession { get; set; }
        public string flagPlataforma { get; set; }
        public string fecActivacion { get; set; }
        public string fechaActivacionPrepago { get; set; }
        public string contratoId { get; set; }
        public string pageAccess { get; set; }
        public string fecStart { get; set; }
        public string fecEnd { get; set; }
        public string type { get; set; }
        public string note { get; set; }
        public string phone { get; set; }
        public string claseDes { get; set; }
        public string subClaseDes { get; set; }
        public string transactionName { get; set; }
        public bool chkGeneraOCC { get; set; }
        public bool cobroOCC { get; set; }
        public string cboCACDAC { get; set; }
        public string idMonto { get; set; }
        public string idCasoId { get; set; }
        public string ptexto { get; set; }
        public bool chkSendMail { get; set; }
        public string customerId { get; set; }
        public string flagTfi { get; set; }
        public string currentUser { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string documentNumber { get; set; }
        public string referencePhone { get; set; }
        public string objIdContacto { get; set; }

        public string SubClaseDesCode { get; set; }
        public CallTipificacion tipificacion { get; set; }

        //////Print
        public string fullName { get; set; }
        public string option { get; set; }

        public string codOpcion { get; set; }


        //TIPO DE TECNOLOGIOA
        public string typeProduct { get; set; }


        //Datos
        public string razonSocial { get; set; }
        public string domicilio { get; set; }
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string modalidad { get; set; }
        public string tipoDocumento { get; set; }

        public string LegalAgent { get; set; }

        public double idMontoConIGV { get; set; }

        public string idMontoSinIGV { get; set; }

        //Constancy
        public Constancy constancy { get; set; }
        
    }
}