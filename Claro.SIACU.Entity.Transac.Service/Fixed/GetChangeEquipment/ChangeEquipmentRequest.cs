using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetChangeEquipment
{
    public class ChangeEquipmentRequest : Claro.Entity.Request
    {

        [DataMember]
        public string channel { get; set; }

        [DataMember]
        public string idApplication { get; set; }

        [DataMember]
        public string userApplication { get; set; }

        [DataMember]
        public string userSession { get; set; }

        [DataMember]
        public string idESBTransaction { get; set; }

        [DataMember]
        public string idBusinessTransaction { get; set; }

        [DataMember]
        public string startDate { get; set; }

        [DataMember]
        public string additionalNode { get; set; }


        [DataMember]
        public string usuario { get; set; }

        [DataMember]
        public string nombres { get; set; }

        [DataMember]
        public string apellidos { get; set; }

        [DataMember]
        public string razonSocial { get; set; }

        [DataMember]
        public string tipoDoc { get; set; }

        [DataMember]
        public string numDoc { get; set; }

        [DataMember]
        public string domicilio { get; set; }

        [DataMember]
        public string distrito { get; set; }

        [DataMember]
        public string departamento { get; set; }

        [DataMember]
        public string provincia { get; set; }

        [DataMember]
        public string modalidad { get; set; }


        [DataMember]
        public string account { get; set; } 

        [DataMember]
        public string contactObjId { get; set; }

        [DataMember]
        public string flagReg { get; set; }


        [DataMember]
        public string contactobjid { get; set; }

        [DataMember]
        public string siteobjid { get; set; }

        [DataMember]
        public string phone { get; set; }
        
        [DataMember]
        public string tipo { get; set; }

        [DataMember]
        public string clase { get; set; }

        [DataMember]
        public string subClase { get; set; }

        [DataMember]
        public string metodoContacto { get; set; }

        [DataMember]
        public string tipoInter { get; set; }

        [DataMember]
        public string agente { get; set; }

        [DataMember]
        public string usrProceso { get; set; } 

        [DataMember]
        public string hechoEnUno { get; set; }

        [DataMember]
        public string notas { get; set; } 

        [DataMember]
        public string flagCaso { get; set; }

        [DataMember]
        public string resultado { get; set; }

        [DataMember]
        public string servaFect { get; set; }

        [DataMember]
        public string inconven { get; set; }

        [DataMember]
        public string servaFectCode { get; set; }

        [DataMember]
        public string inconvenCode { get; set; }

        [DataMember]
        public string cold { get; set; }

        [DataMember]
        public string codPlano { get; set; }  

        [DataMember]
        public string valor1 { get; set; }

        [DataMember]
        public string valor2 { get; set; }


        [DataMember]
        public string inter1 { get; set; }

        [DataMember]
        public string inter2 { get; set; }

        [DataMember]
        public string inter3 { get; set; }

        [DataMember]
        public string inter4 { get; set; }

        [DataMember]
        public string inter5 { get; set; }

        [DataMember]
        public string inter6 { get; set; }

        [DataMember]
        public string inter7 { get; set; }

        [DataMember]
        public string inter8 { get; set; }

        [DataMember]
        public string inter9 { get; set; }

        [DataMember]
        public string inter10 { get; set; }

        [DataMember]
        public string inter11 { get; set; }

        [DataMember]
        public string inter12 { get; set; }

        [DataMember]
        public string inter13 { get; set; }

        [DataMember]
        public string inter14 { get; set; }

        [DataMember]
        public string inter15 { get; set; }

        [DataMember]
        public string inter16 { get; set; }

        [DataMember]
        public string inter17 { get; set; }

        [DataMember]
        public string inter18 { get; set; }

        [DataMember]
        public string inter19 { get; set; }

        [DataMember]
        public string inter20 { get; set; }

        [DataMember]
        public string inter21 { get; set; }

        [DataMember]
        public string inter22 { get; set; }

        [DataMember]
        public string inter23 { get; set; }

        [DataMember]
        public string inter24 { get; set; }

        [DataMember]
        public string inter25 { get; set; }

        [DataMember]
        public string inter26 { get; set; }

        [DataMember]
        public string inter27 { get; set; }

        [DataMember]
        public string inter28 { get; set; }

        [DataMember]
        public string inter29 { get; set; }

        [DataMember]
        public string inter30 { get; set; }

        [DataMember]
        public string plusInter2Interact { get; set; }

        [DataMember]
        public string adjustmentAmount { get; set; }

        [DataMember]
        public string adjustmentReason { get; set; }

        [DataMember]
        public string address { get; set; }

        [DataMember]
        public string amountUnit { get; set; }

        [DataMember]
        public string birthday { get; set; }

        [DataMember]
        public string clarifyInteraction { get; set; } 

        [DataMember]
        public string claroLdn1 { get; set; }

        [DataMember]
        public string claroLdn2 { get; set; }

        [DataMember]
        public string claroLdn3 { get; set; }

        [DataMember]
        public string claroLdn4 { get; set; }

        [DataMember]
        public string claroLocal1 { get; set; }

        [DataMember]
        public string claroLocal2 { get; set; }

        [DataMember]
        public string claroLocal3 { get; set; }

        [DataMember]
        public string claroLocal4 { get; set; }

        [DataMember]
        public string claroLocal5 { get; set; }

        [DataMember]
        public string claroLocal6 { get; set; }

        [DataMember]
        public string contactPhone { get; set; }

        [DataMember]
        public string dniLegalRep { get; set; }

        [DataMember]
        public string documentNumber { get; set; }
         
        [DataMember]
        public string email { get; set; }

        [DataMember]
        public string firstName { get; set; }

        [DataMember]
        public string fixedNumber { get; set; }

        [DataMember]
        public string flagChangeUser { get; set; }

        [DataMember]
        public string flagLegalRep { get; set; }

        [DataMember]
        public string flagOther { get; set; }

        [DataMember]
        public string flagTitular { get; set; }

        [DataMember]
        public string imei { get; set; } 

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string LastnameRep { get; set; }

        [DataMember]
        public string LdiNumber { get; set; }

        [DataMember]
        public string NameLegalRep { get; set; }

        [DataMember]
        public string oldClaroLdn1 { get; set; }

        [DataMember]
        public string oldClaroLdn2 { get; set; }

        [DataMember]
        public string oldClaroLdn3 { get; set; }

        [DataMember]
        public string oldClaroLdn4 { get; set; }

        [DataMember]
        public string oldclaroLocal1 { get; set; }

        [DataMember]
        public string oldclaroLocal2 { get; set; }

        [DataMember]
        public string oldclaroLocal3 { get; set; }

        [DataMember]
        public string oldclaroLocal4 { get; set; }

        [DataMember]
        public string oldclaroLocal5 { get; set; }

        [DataMember]
        public string oldclaroLocal6 { get; set; }

        [DataMember]
        public string oldDocNumber { get; set; }
    
        [DataMember]
        public string oldFirstName { get; set; }

        [DataMember]
        public string oldFixedPhone { get; set; }

        [DataMember]
        public string oldLastName { get; set; }

        [DataMember]
        public string oldLdiNumber { get; set; }

        [DataMember]
        public string oldFixedNumber { get; set; }

        [DataMember]
        public string OperationType { get; set; }

        [DataMember]
        public string OtherDocNumber { get; set; }

        [DataMember]
        public string OtherFirstName { get; set; }

        [DataMember]
        public string OtherLastName { get; set; }

        [DataMember]
        public string OtherPhone { get; set; }

        [DataMember]
        public string PhoneLegalRep { get; set; }

        [DataMember]
        public string ReferencePhone { get; set; }

        [DataMember]
        public string Reason { get; set; }

        [DataMember]
        public string Model { get; set; }

        [DataMember]
        public string LotCode { get; set; }

        [DataMember]
        public string flagRegistered { get; set; }

        [DataMember]
        public string RegistrationReason { get; set; }

        [DataMember]
        public string claroNumber { get; set; } 

        [DataMember]
        public string Month { get; set; }

        [DataMember]
        public string OstNumber { get; set; }

        [DataMember]
        public string basket { get; set; } 

        [DataMember]
        public string expireDate { get; set; }

        [DataMember]
        public string address5 { get; set; } 

        [DataMember]
        public string chargeAmount { get; set; }

        [DataMember]
        public string city { get; set; }

        [DataMember]
        public string contactSex { get; set; }

        [DataMember]
        public string department { get; set; }

        [DataMember]
        public string district { get; set; } 

        [DataMember]
        public string emailConfirmation { get; set; }

        [DataMember]
        public string fax { get; set; }

        [DataMember]
        public string flagCharge { get; set; }

        [DataMember]
        public string MaritalStatus { get; set; }

        [DataMember]
        public string Occupation { get; set; }

        [DataMember]
        public string Position { get; set; }

        [DataMember]
        public string ReferenceAddress { get; set; }

        [DataMember]
        public string TypeDocument { get; set; }

        [DataMember]
        public string Zipcode { get; set; }

        [DataMember]
        public string iccid { get; set; }


        [DataMember]
        public string codId { get; set; }

        [DataMember]
        public string customerId  { get; set; }

        [DataMember]
        public string tipoTrans { get; set; }
    
        [DataMember]
        public string codIntercaso { get; set; }

        [DataMember]
        public string tipoVia { get; set; }

        [DataMember]
        public string nomVia { get; set; }

        [DataMember]
        public string numVia { get; set; }

        [DataMember]
        public string tipUrb { get; set; }

        [DataMember]
        public string nomUrb { get; set; }

        [DataMember]
        public string manzana { get; set; }

        [DataMember]
        public string lote { get; set; }

        [DataMember]
        public string ubigeo { get; set; }

        [DataMember]
        public string codZona { get; set; }

        [DataMember]
        public string codeDif { get; set; } 

        [DataMember]
        public string referencia { get; set; }

        [DataMember]
        public string observacion { get; set; }

        [DataMember]
        public string fecProg { get; set; }

        [DataMember]
        public string franjaHor { get; set; }

        [DataMember]
        public string numCarta { get; set; }

        [DataMember]
        public string operador { get; set; }

        [DataMember]
        public string preSuscrito { get; set; }

        [DataMember]
        public string publicar { get; set; }

        [DataMember]
        public string tmCode { get; set; }

        [DataMember]
        public string lstTipEqu { get; set; }

        [DataMember]
        public string lstCoser { get; set; }

        [DataMember]
        public string lstSncode { get; set; }

        [DataMember]
        public string lstSpcode { get; set; }

        [DataMember]
        public string usuReg { get; set; }

        [DataMember]
        public string fecReg { get; set; }

        [DataMember]
        public string cargo { get; set; }

        [DataMember]
        public string codCaso { get; set; }

        [DataMember]
        public string tipTra { get; set; }

        [DataMember]
        public string tipoServicio { get; set; }

        [DataMember]
        public string idInteraccion { get; set; }

        [DataMember]
        public string flagActDirFact { get; set; }

        [DataMember]
        public string codMotot { get; set; }

        [DataMember]
        public string tipoProducto { get; set; }


        [DataMember]
        public string trama { get; set; }


        [DataMember]
        public string idProcess { get; set; }

        [DataMember]
        public string coId { get; set; }

        [DataMember]
        public string flagContingencia { get; set; }

        [DataMember]
        public string formatoConstancia { get; set; }

        [DataMember]
        public string correoDestinatario { get; set; } 

        [DataMember]
        public string directory { get; set; }
        
        [DataMember]
        public string fileName { get; set; }

        [DataMember]
        public string asunto { get; set; }
            
        [DataMember]
        public string mensaje { get; set; }

        [DataMember]
        public List<Decoder> ListDetService{ get; set; } 

        [DataMember]
        public string strTransactionAudit { get; set; }

        [DataMember]
        public string strService { get; set; }
        
        [DataMember]
        public string strIpClient { get; set; }
        
        [DataMember]
        public string strIpServer { get; set; }
        
        [DataMember]
        public string strNameServer { get; set; }
        
        [DataMember]
        public string strAmount { get; set; }
        
        [DataMember]
        public string strText { get; set; }

        [DataMember]
        public string strNameClient { get; set; }

        [DataMember]
        public string strSOTAssociate { get; set; }
    }
}