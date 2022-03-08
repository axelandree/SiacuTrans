using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.ChangeEquipment
{
    public class ChangeEquip
    {
        public object Servicios { get; set; }
        public string ConID { get; set; }
        public string CustomerID { get; set; }
        public string CurrentUser { get; set; }
        public string TransTipo { get; set; }
        public string InterCasoID { get; set; }
        public int MotivoID { get; set; }
        public string TrabajoID { get; set; }
        public string TipoVia { get; set; }
        public string NomVia { get; set; }
        public string NroVia { get; set; }

        public string hdnTipoVia { get; set; }
        //public string ddlTipMzBloEdi { get; set; }
        //public string hdnTipMzBloEdi { get; set; }
        //public string ddlDepartment { get; set; }
        public string hdnDepartment { get; set; }
        //public string hdnNumberDepartment { get; set; }

        public string NumLote { get; set; }
        public string TipoUrb { get; set; }
        //public string hdnTipoUrb { get; set; }
        public string NomUrb { get; set; }
        public string Ubigeo { get; set; }
        public string ZonaID { get; set; }
        public string PlanoID { get; set; }
        public string EdificioID { get; set; }
        public string Referencia { get; set; }
        public string Observacion { get; set; }
        public string FranjaHora { get; set; }
        public string FechaProgramada { get; set; }
        public string Cargo { get; set; }

        public string strIdSession { get; set; }
        public bool chkUseChangeBillingChecked { get; set; }
        public bool chkEmailChecked { get; set; }

        public bool chkLoyalty { get; set; }


        public string strtypetransaction { get; set; }

        public bool chkSN { get; set; }

        public string txtNumDir { get; set; }

        public string txtNotText { get; set; }

        public string hdnCodigoRequestAct { get; set; }


        public string nameCustomer { get; set; }
        public string RepreCustomer { get; set; }
        public string TypDocRepreCustomer { get; set; }

        public string NumbDocRepreCustomer { get; set; }
        public string cuenta { get; set; }
        public string AddressCustomer { get; set; }
        public string NotAddressCustomer { get; set; }

        public string CountryCustomer { get; set; }
        public string CountryCustomerFac { get; set; }
        public string DepCustomer { get; set; }
        public string ProvCustomer { get; set; }
        public string DistCustomer { get; set; }
        public string IdEdifCustomer { get; set; }
        public string EmailCustomer { get; set; }

        public string PlanoIDCustomer { get; set; }
        public string urbLegalCustomer { get; set; }
        public string DirecDespachoCustomer { get; set; }

        public string RefAddressCustomer { get; set; }
        public string hdnUbiAct { get; set; }
        public string DescripCADDAC { get; set; }

        public string strIgv { get; set; }

        //Direccion

        public string Email { get; set; }
        public string DOMICILIO { get; set; }
        public string strRef { get; set; }
        public string RefNoteDirec { get; set; } //GenerarNotasDireccion
        public string DISTRITO { get; set; }
        public string PROVINCIA { get; set; }
        public string ZIPCODE { get; set; }
        public string DEPARTAMENTO { get; set; }

        public string PAIS_LEGAL { get; set; }

        public string agendaGetValidaEta { get; set; }

        public string ObtenerHoraAgendaETA { get; set; }

        public string agendaGetCodigoFranja { get; set; }

        public string agendaGetFecha { get; set; }
        public string agendaGetTipoTrabajo { get; set; }
        public string CodPos { get; set; }
        public string hdnCodPos { get; set; }
        public string codCenPob { get; set; }
        public string hdnCenPobDes { get; set; }
        public string hdnCodPla { get; set; }
        public string hdnUbiID { get; set; }


        public string Telephone { get; set; }
        //LTE
        public string strtypeCustomer { get; set; }
        public string strCodOCC { get; set; }
        public string CicloFact { get; set; }

        public string strNroSOT { get; set; }

        public string strtypeCliente { get; set; }
        public string strSubTypeWork { get; set; }
        public string CodSot { get; set; }
    }
}