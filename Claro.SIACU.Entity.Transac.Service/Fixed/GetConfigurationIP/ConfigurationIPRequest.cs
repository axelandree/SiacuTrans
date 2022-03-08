using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetConfigurationIP
{
    [DataContract]
    public class ConfigurationIPRequest : Claro.Entity.Request
    {
       
        [DataMember]
        public InsertInteractHFCRequest oInsertInteractHFCRequest { get; set; }
        [DataMember]
        public InsertTemplateInteraction oInsertTemplateInteraction { get; set; }
        [DataMember]
        public Customer oCustomer { get; set; }
        [DataMember]
        public string strMsisdn { get; set; }
        [DataMember]
        public string strFlagContingencia { get; set; }
        [DataMember]
        public string strTipoTransaccion { get; set; }
        [DataMember]
        public string strCorreoDestinatario {get; set;}
        [DataMember]
        public string strFormatoConstancia {get; set;}

        // ParamSotHFC
        [DataMember]
        public string strId {get; set;}
        [DataMember]
        public string strTipoTrans {get;set;}
        [DataMember]
        public string strCodInterCaso  {get; set;}
        [DataMember]
        public string strTipoVia {get; set;}
        [DataMember]
        public string strNombreVia {get; set;}
        [DataMember]
        public string strNumeroVia {get; set;}
        [DataMember]
        public string strTipoUrbanizacion {get; set;}
        [DataMember]
        public string strNombreUrbanizacion {get; set;}
        [DataMember]
        public string strManzana {get; set;}
        [DataMember]
        public string strLote {get; set;}
        [DataMember]
        public string strCodUbigeo {get; set;}
        [DataMember]
        public string strCodZona {get; set;}
        [DataMember]
        public string strIdPlano {get;set;}
        [DataMember]
        public string strCodeDif {get; set;}
        [DataMember]
        public string strReferencia {get; set;}
        [DataMember]
        public string strObservacion {get; set;}
        [DataMember]
        public string strFecProg {get; set;}
        [DataMember]
        public string strFranjaHorario {get; set;}
        [DataMember]
        public string strNumCarta {get; set;}
        [DataMember]
        public string strOperador {get; set;}
        [DataMember]
        public string strPresuscrito {get; set;}
        [DataMember]
        public string strPublicar {get; set;}
        [DataMember]
        public string stradTmCode {get; set;}
        [DataMember]
        public string strListaCose {get; set;}
        [DataMember]
        public string strListaSpCode {get; set;}
        [DataMember]
        public string strUsuarioReg {get; set;}

        [DataMember]
        public string strJobType { get; set; }
        [DataMember]
        public string strCodSolot { get; set; }
        [DataMember]
        public string strTypeServices { get; set; }
        [DataMember]
        public string strCodinssrv { get; set; }
        [DataMember]
        public string strWeb { get; set; }
        [DataMember]
        public string strApplicationName { get; set; }
        [DataMember]
        public string strIpCliente { get; set; }

        public ConfigurationIPRequest()
        {
            oInsertInteractHFCRequest = new InsertInteractHFCRequest();
            oInsertTemplateInteraction = new InsertTemplateInteraction();
            oCustomer = new Customer();
        }

    }
}
