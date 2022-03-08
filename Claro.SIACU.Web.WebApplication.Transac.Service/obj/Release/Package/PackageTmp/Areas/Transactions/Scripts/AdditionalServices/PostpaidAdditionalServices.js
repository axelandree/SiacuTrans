
var Desactivo = 'Desactivo';
var Activo = 'Activo';
var S = 'S';
var N = 'N';


// URL
var URL_GetServivesContract = "/Transactions/Postpaid/AdditionalServices/GetServivesContract";
var URL_ProgActDesact = "/Transactions/Postpaid/AdditionalServices/ProgActDesact";
var URL_ProgRoamming = '/Transactions/Postpaid/AdditionalServices/ProgRoamming';
var URL_SaveProgramming = '/Transactions/Postpaid/AdditionalServices/SaveProgramming';
var URL_PostBack = '/Transactions/Postpaid/AdditionalServices/PostBack';
var URL_SIACPO_Validate = '/Transactions/Postpaid/Auth/AuthUserHtml';

var URL_GetBDBridge = '';
var URL_Active = '/Transactions/Postpaid/AdditionalServices/Active';
var URL_Desactive = "/Transactions/Postpaid/AdditionalServices/Desactive";
var URL_AdditionalServicesConstModCP = '/Transactions/Postpaid/AdditionalServicesConstModCP/AdditionalServicesConstModCP';
var URL_AdditionalServicesDetailCommercial = '/Transactions/Postpaid/AdditionalServicesDetailCommercial/AdditionalServicesDetailCommercial';
// GRID
var Grid_ServicesContract;


//Sessiones
var AdditionalServices = {};
 
AdditionalServices.HidStatus_Conctract = "";

//Hidden
AdditionalServices.HidTransactionDTH = "";
AdditionalServices.HidTransaction = "";
AdditionalServices.HidClassId = "";
AdditionalServices.HidSubClassId = "";
AdditionalServices.HidType = "";
AdditionalServices.HidClassDes = "";
AdditionalServices.HidSubClassDes = "";
AdditionalServices.HidNumberPhone = "";
AdditionalServices.HidCodId = "";
AdditionalServices.HidListTypeSolRoaming = "";
AdditionalServices.HidProfileSolRoaming = "";
AdditionalServices.HidCodServRoaming = "";
AdditionalServices.HidMinDateDeactivation = "";
AdditionalServices.HidMaxDateDeactivacion = "";
AdditionalServices.HidTotalFixedCharge = "";
AdditionalServices.HidCodServ4G = "";
AdditionalServices.HidCodOptAuthorized = "";
AdditionalServices.HidAccess = "";
AdditionalServices.HidAccessWC = "";
AdditionalServices.HidAccessMCP = "";
AdditionalServices.HidStateUserBSCS = "";
AdditionalServices.HidClassIdMCP = "";
AdditionalServices.HidSubClassIdMCP = "";
AdditionalServices.HidTypeMCP = "";
AdditionalServices.HidClassDesMCP = "";
AdditionalServices.HidSubClassDesMCP = "";
AdditionalServices.HidNameService = "";
AdditionalServices.HidCodService = "";
AdditionalServices.HidFixedCharge = "";
AdditionalServices.HidFixedChargeM = "";
AdditionalServices.HidNumberPeriod = "";
AdditionalServices.HidEstGraMCP = "";
AdditionalServices.HidQoutMod = "";
AdditionalServices.HidPeriodMod = "";
AdditionalServices.HidCarFixed = "";
AdditionalServices.HidPeriodAnt = "";
AdditionalServices.HidQuotAnt = "";
AdditionalServices.HidCaseId = "";
AdditionalServices.HidCodId_Contract = "";
AdditionalServices.HidSnCode = "";
AdditionalServices.HidStateMod = "";
AdditionalServices.HidProgramingRoamming = "";
AdditionalServices.HidAction = "";
AdditionalServices.HidDateFrom = "";
AdditionalServices.HidTypeRequest = "";
AdditionalServices.HidStatePrograming = "";
AdditionalServices.HidStateContract = "";
AdditionalServices.HidState = "";
AdditionalServices.HidBloqDes = "";
AdditionalServices.HidRecord = "";
AdditionalServices.HidCodPackage = "";
AdditionalServices.HidBloqAct = "";
AdditionalServices.HidCodExclusive = "";
AdditionalServices.HidValidateInitial = "";
AdditionalServices.HidStatusDisabledApa = "";
AdditionalServices.HidStateService = "";
AdditionalServices.HidNameObj = "";
AdditionalServices.HidQuotAntControl = "";
AdditionalServices.HidPeriodAntControl = "";
AdditionalServices.HidStateActiveCC = "";
AdditionalServices.strMaxQuota = "";
AdditionalServices.strMinQuota = "";
AdditionalServices.strMaxPeriod = "";
AdditionalServices.strMinPeriod = "";
AdditionalServices.strPeriod = "";
AdditionalServices.strModQuotaPer = "";
AdditionalServices.strEnvioLog = "";
AdditionalServices.strEstOk = "";
AdditionalServices.strEstCancel = "";
AdditionalServices.strlblPhoneNumber = "";
AdditionalServices.UserLogin = "";
AdditionalServices.FirstName = "";

AdditionalServices.HidConstancy = "";
AdditionalServices.ContactCode = "";
AdditionalServices.HidRoutePdf = "";

AdditionalServices.gConstResultadoErrorBSCS = "";

function Loading() { 
    $.blockUI({
        message: $("#ModalLoading").html(),
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000000',
            '-webkit-border-radius': '50px',
            '-moz-border-radius': '50px',
            opacity: .7,
            color: '#fff'
        }
    });
}

function PopupDetailCommer() {
    var url = location.protocol + "//" + location.host + URL_AdditionalServicesDetailCommercial;
    var ventana = window.open(url, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=778, height=640');
    ventana.focus();
}

function LoadContract() {
    $('#tblContracts tr').each(function() {
        var state = $(this).find('#rdbContract').is(':checked');
        if (state == true) {
            var codId = $(this).find('td').eq(2).text();
            var status = $(this).find('td').eq(4).text();
            AdditionalServices.HidCodId_Contract = codId;
            AdditionalServices.HidStateContract = status;
        }
    });
   
    LoadServicesContract(AdditionalServices.HidCodId_Contract);
    
}

function LoadServicesContract (ContractId){
    var model = {};

    //$('#dataTables_scrollHead').css("width", "100%");
    //$('#dataTables_scrollHeadInner').css("width", "100%");

    model.ContractId = ContractId;
    model.IdSession = AdditionalServices.IdSession;
    model.HidContract = ContractId;
    model.UserLogin = AdditionalServices.UserLogin;
    $.ajax({
        url: URL_GetServivesContract,
        data: JSON.stringify(model),
        type: 'POST',
        contentType: "application/json charset=utf-8;",
        dataType: "json",
        success: function (response) {
            
            if (response.data != null) {
                if (response.data.length > 0) {

                    var List = response.data;

                    //Loading();
                    $.each(response.data, function (i, r) {
                        var vMontoFinal = (r._monto_final == null) ? '' : r._monto_final;
                        var dFecha = (r._fecha_validez == '') ? '' : moment(r._fecha_validez).format('DD/MM/YYYY');
                       
                        var strHtml = '';
                       
                        if(ValidarServicioActivado(List,r._cod_excluyente,r._estado)){
                            strHtml = "Select"; //' onclick="Seleccionar(); "';
                        }
                        else{
                            strHtml = "";//' onclick="BloqueaRadio();"';
                        }

                        var vRadioId = "rdb_" + strHtml + "_" + r._cod_serv + "_" + r._des_serv + "_" + r._pos_serv + "_" + r._cod_excluyente + "_" + r._des_excluyente + "_" + r._estado + "_" + dFecha + "_" + r._monto_cargo_sus + "_" + r._monto_cargo_fijo + "_" + vMontoFinal + "_" + r._bloqueo_desact + "_" + r._bloqueo_act + "_" + r._des_grupo + "_" + r._pos_grupo + "_" + r._cuota_modif + "_" + r._periodos_validos;

                        Grid_ServicesContract.row.add([
                          '<input id="' + vRadioId + '" type="radio" class="rdbServicebyContract"  name="rdbServicebyContract">'
                          , r._cod_serv
                          , '<a href="javascript:void(0);" onclick="PopupDetailCommer();">' + r._des_serv + '</a>'
                          , r._pos_serv
                          , r._cod_excluyente
                          , r._des_excluyente
                          , r._estado
                          , dFecha
                          , r._monto_cargo_sus
                          , r._monto_cargo_fijo
                          , '<input class="txtQuot" name="txtQuot" type="text" style="width: 50px; text-align:right;" maxLength="7" disabled  value="' + r._cuota_modif + '"  />'
                          , vMontoFinal
                          , '<input class="txtPeriod" name="txtPeriod" type="text" style="width: 25px; text-align:right"  MaxLength="3" disabled value="' + r._periodos_validos + '" />'
                          , r._bloqueo_desact
                          , r._bloqueo_act
                          , r._des_grupo
                          , r._pos_grupo
                        ]).draw(true);
                    });
                }
            }
            
        }
    });

}
 

function ValidarServicioActivado(List, codigo_excluyente, estado){
    $.each(List, function (i, r) {
        if(codigo_excluyente==r._cod_excluyente){
            if(r._estado=='Activo' && estado != "Activo"){
                return false;
            }
        }
       
    });
    return true;
}

var sessionTransacPost = {};
sessionTransacPost = JSON.parse(sessionStorage.getItem("SessionTransac"));
//console.log"Imprimiendo Session");
//console.logsessionTransacPost);
(function ($, undefined) {
    
   
    // VARIABLES
    

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPaidAdditionalServices.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            // Button
            , btnActive             : $("#btnActive", $element)
            , btnProActive          : $('#btnProActive', $element)
            , btnConstancy          : $('#btnConstancy', $element)
            , btnProDesactive       : $("#btnProDesactive", $element)
            , btnDesactive          : $("#btnDesactive",$element)
            , btnKeepActive         : $("#btnKeepActive",$element)
            , btnProRoamming        : $("#btnProRoamming",$element)
            , btnConsultEquipment   : $("#btnConsultEquipment", $element)
            , btnModifyQuota        : $("#btnModifyQuota", $element)
            , btnCloseError         : $('#btnCloseError', $element)

            //TextBox
            , txtDateApp            : $('#txtDateApp', $element)
            , txtDateAct            : $('#txtDateAct', $element)
            , txtDateDeact          : $('#txtDateDeact', $element)
            , txtEmail              : $('#txtEmail', $element)
            , txtNotes              : $('#txtNotes', $element)
            , txtQuot               : $('#txtQuot', $element)
            , txtPeriod             : $('#txtPeriod', $element)
            //ComboBox

            , cboCACDAC             : $('#cboCACDAC', $element)
            , cboCareChannel        : $("#cboCareChannel", $element)

            //Label
            , lblPhoneNumber        : $('#lblPhoneNumber', $element)
            , lblCustomerType       : $('#lblCustomerType', $element)
            , lblCustomerName       : $('#lblCustomerName', $element)
            , lblPlanName           : $('#lblPlanName', $element)
            , lblCycleFact          : $('#lblCycleFacture', $element)
            , lblCldFI              : $("#lblCldFI",$element)
            , lblCldFF              : $("#lblCldFF", $element)
            , lblLegendProg         : $("#lblLegendProg",$element)
            , lblErrorMessage       : $('#lblErrorMessage', $element)
           
            // RadioButton
            , rdbDetermined         : $('#rdbDetermined', $element)
            , rdbIndeterminate      : $('#rdbIndeterminate', $element)
           
            //CheckBox
            , chkModifyQuota        : $("#chkModifyQuota", $element)
            , chckContract          : $('#chckContract', $element)
            , chkSendMail           : $('#chkSendMail', $element)
            , chkProgram            : $('#chkProgram', $element)

            // Table
            , tblServicesAdd        : $("#tblServicesAdd", $element)
            , tblContracts          : $('#tblContracts', $element)

            // DIV
            , DivIdServRoaming      : $("#DivIdServRoaming",$element)
            , DivFechApli           : $("#DivFechApli", $element)
            , divShowMessage        : $('#divShowMessage', $element)

            , spnMainTitle          : $('#spnMainTitle')

             , ModalLoading         : $('#ModalLoading', $element)

        //    ,ClsRdbContract          : $("body .ClsRdbContract", $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
          
            controls.txtDateApp.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateAct.datepicker({ format: 'dd/mm/yyyy' });
            controls.txtDateDeact.datepicker({ format: 'dd/mm/yyyy' });

            //controls.btnConsult.addEvent(that, 'click', that.btnConsult_click);
            controls.btnProActive.addEvent(that, 'click', that.btnProActive_click);
            controls.btnProDesactive.addEvent(that, 'click', that.btnProDesactive_click);

            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.chkProgram.addEvent(that, 'click', that.chkProgram_click);
            controls.rdbDetermined.addEvent(that, 'click', that.rdbDetermined_click);
            controls.rdbIndeterminate.addEvent(that, 'click', that.rdbIndeterminate_click);
            controls.btnConsultEquipment.addEvent(that, 'click', that.btnConsultEquipment_click);
            controls.btnProRoamming.addEvent(that, 'click', that.f_ValidateDateRoamming);
            controls.btnActive.addEvent(that, 'click', that.btnActive_click);
            controls.btnDesactive.addEvent(that, 'click', that.btnDesactive_click);
            controls.btnKeepActive.addEvent(that, 'click', that.btnKeepActive_click);
            controls.chkModifyQuota.addEvent(that, 'change', that.f_ReviewGrid);


            controls.btnModifyQuota.addEvent(that, 'click', that.f_Modify_click);
            
            controls.chkSendMail.addEvent(that, 'click', that.f_EnableMail);

            controls.txtDateApp.addEvent(that, 'change', that.f_PermissionsValidate);
            controls.btnCloseError.addEvent(that, 'click', that.btnCloseError);
          
            //controls.btnProActive.addEvent(that,'click',that.f_ValidateDateApp("A"));
            that.maximizarWindow();
            that.windowAutoSize();
            Loading();
            that.loadSessionData();
            that.render();

            

            $(document).on('click', '.rdbServicebyContract', function (e) {
                var vRadioId = $(this).attr("id");
                var array = vRadioId.split('_');

                var vCodSer = array[2];
                var vDescSer = array[3];
                var vPosSer = array[4];
                var vCodExcluyente = array[5];
                var vDescExcluyente = array[6];
                var vState = array[7];
                var vFecha = array[8];
                var vCargoSus = array[9];
                var vMonto_cargo_fijo = array[10];
                var vMontoFinal = array[11];

                var vBloqueoDesact = array[12];
                var vBloqueoAct = array[13];
                var vDescGroup = array[14];
                var vPostGroup = array[15];
                var vNumPeriodo = array[17];
                if (array[1] == 'Select') {
                    that.f_Select(vCodSer, vPostGroup, vBloqueoDesact, vBloqueoAct, vState, vDescGroup, vCargoSus, vMonto_cargo_fijo, vNumPeriodo, vCodExcluyente, vFecha)
                    that.f_SelecRdb(vState, vBloqueoAct);
                    
                }
                else {
                    var gridName = "tblServicesAdd";
                    that.f_RadioBlock(gridName);
                }
              

            });

            //$(document).on('click', '.clsrdbContract', function (e) {
            //    LoadContract();
            //});

            
            
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
            that.getCACDAC();
        },

        maximizarWindow: function () {
            top.window.moveTo(0, 0);
            if (document.all) {
                top.window.resizeTo(screen.availWidth, screen.availHeight);
            } else if (document.layers || document.getElementById) {
                if (top.window.outerHeight < screen.availHeight || top.window.outerWidth < screen.availWidth) {
                    top.window.outerHeight = screen.availHeight;
                    top.window.outerWidth = screen.availWidth;
                }
            }
        },


        loadSessionData: function () {
            var that = this;
            var controls = this.getControls();

            controls.spnMainTitle.text("ACTIVAR O DESACTIVAR SERVICIOS ADICIONALES POSTPAGO");

            Session.IDSESSION = '20170518150515737831';
            //Session.IDSESSION = sessionTransacPost.UrlParams.IDSESSION;
			Session.DATACUSTOMER = sessionTransacPost.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = sessionTransacPost.SessionParams.DATASERVICE;
            Session.USERACCESS = sessionTransacPost.SessionParams.USERACCESS;

            //DATOS CLIENTEConsole
            AdditionalServices.IdSession = Session.IDSESSION;
            AdditionalServices.ContractId = Session.DATACUSTOMER.ContractID;
            AdditionalServices.CustomerId = Session.DATACUSTOMER.CustomerID;
            AdditionalServices.lblPhoneNumber = Session.DATACUSTOMER.Telephone;//lblNroTelefono EN SIACPO
            AdditionalServices.lblCustomerType = Session.DATACUSTOMER.CustomerType;//lblTipoCliente EN SIACPO
            AdditionalServices.lblCustomerName = Session.DATACUSTOMER.BusinessName; //lblCliente EN SIACPO
            AdditionalServices.lblCycleFact = Session.DATACUSTOMER.objPostDataAccount.BillingCycle;//lblCicloFacturación EN SIACPO
            AdditionalServices.txtEmail = Session.DATACUSTOMER.Email; //txtEmail EN SIACPO
            AdditionalServices.FirstName = Session.DATACUSTOMER.Name;
            AdditionalServices.LastName = Session.DATACUSTOMER.LastName;
            AdditionalServices.DniRuc = Session.DATACUSTOMER.DNIRUC;
            AdditionalServices.PhoneReference = Session.DATACUSTOMER.PhoneReference;
            AdditionalServices.CustomerContact = Session.DATACUSTOMER.CustomerContact;
            AdditionalServices.TypeDocument = Session.DATACUSTOMER.DocumentType;
            AdditionalServices.LegalAgent = Session.DATACUSTOMER.LegalAgent;
            AdditionalServices.Name = Session.DATACUSTOMER.Name;
            AdditionalServices.Account = Session.DATACUSTOMER.Account;
            AdditionalServices.ContactCode = Session.DATACUSTOMER.ContactCode;



            //DATOS DE LA LINEA
            AdditionalServices.lblPlanName = Session.DATASERVICE.Plan; //lblPlan EN SIACPO

            //DETOS DEL USUARIO QUE ESTA LOGUEADO
            AdditionalServices.UserLogin = Session.USERACCESS.login; //USERACCESS.login es el valo que viene de la redireccion
            AdditionalServices.FullName = Session.DATACUSTOMER.FullName;

            AdditionalServices.strOpcActivaCheckProgramarDTH = "";
            AdditionalServices.SessionProfile = Session.USERACCESS.profiles;
            AdditionalServices.AccesPage = Session.USERACCESS.optionPermissions; //"ACP_APD,ACP_AA4G,ACP_AMCP";
            AdditionalServices.HidPermissions = "";
            AdditionalServices.Transaction = "TRANSACCION_ACT_DES_SER";


            //ASIGNACION DE VALORES A MOSTRAR EN HTML
            //controls.lblPhoneNumber.text(AdditionalServices.lblPhoneNumber);
            //controls.lblCustomerType.text(AdditionalServices.lblCustomerType);
            //controls.lblCustomerName.text(AdditionalServices.lblCustomerName);
            //controls.lblPlanName.text(AdditionalServices.lblPlanName);
            //controls.lblCycleFact.text(AdditionalServices.lblCycleFact);
            controls.txtEmail.val(AdditionalServices.txtEmail);
            controls.txtEmail.hide();
            controls.divShowMessage.hide();
            that.Page_Load();
            that.AccesPage();


            that.f_GetSolRoammingTypeList();
          
          //  that.f_InputDisabled(true);
          
        },
        Page_Load: function () {
            var that = this,
               controls = that.getControls(),
               oCustomer = Session.DATACUSTOMER,
               oUserAccess = Session.USERACCESS,
               oDataService = Session.DATASERVICE;



            if (oUserAccess == null || oCustomer == null) {
                window.close();
                //opener.parent.top.location.href = response.hdnSiteUrl;
                return;
            }
            if (oCustomer.ContractID == null || oCustomer.ContractID == '0' || oCustomer.ContractID == '' || oCustomer.ContractID == '&nbsp;') {
                alert("No se cargó datos generales de la linea, no se podra continuar con la transacción.", "Alerta");
                window.close();
            }
            if (oUserAccess.userId == null || oUserAccess.userId == '0' || oUserAccess.userId == '' || oUserAccess.userId == '&nbsp;') {
                window.close();
                //opener.parent.top.location.href = response.hdnSiteUrl;
                return;
            }
            controls.btnConstancy.prop("disabled",true);

            var txtNotes = $('textarea[id = txtNotes]').val();

            var strUrlModal = that.strUrl + '/Postpaid/AdditionalServices/Page_Load';

            $('#btnKeepActive').css("display", "none");
            $('#btnProActive').attr("disabled", true);
            $('#btnProDesactive').attr("disabled", true);
            //$('#divCareChannel').css("display", "none");
			$('#divCareChannel').hide();
            $('#DivFechApli').css("display", "none");
            $('#DivIdServRoaming').css("display", "none");
            $('#btnModifyQuota').attr("disabled", true);
            $('#chkModifyQuota').attr("disabled", true);

            var objAdditionalServices = {
                IdSession: AdditionalServices.IdSession,
                ContractId: AdditionalServices.ContractId,
                Transaction: AdditionalServices.Transaction,
                lblPhoneNumber: oDataService.CellPhone,
                lblCustomerType: AdditionalServices.lblCustomerType,
                lblCustomerName: AdditionalServices.lblCustomerName,
                lblPlanName: AdditionalServices.lblPlanName,
                lblCycleFact: AdditionalServices.lblCycleFact,
                txtEmail: AdditionalServices.txtEmail,
                UserLogin: AdditionalServices.UserLogin,
                SessionProfile: AdditionalServices.SessionProfile,
                TxtNote: txtNotes
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                data: JSON.stringify(objAdditionalServices),
                url: strUrlModal,
                success: function (result) {
                    //alert('Validacion de Permiso Correcto');
                    AdditionalServices.HidTransactionDTH = result.HidTransactionDTH;
                    AdditionalServices.HidCodServ4G = result.HidCodServ4G;
                    AdditionalServices.HidCodOptAuthorized = result.HidCodOptAuthorized;
                    AdditionalServices.HidAccessWC = result.HidAccessWC;
                    AdditionalServices.HidAccessMCP = result.HidAccessMCP;
                    AdditionalServices.HidCodId = result.HidCodId;
                    AdditionalServices.HidListTypeSolRoaming = result.HidListTypeSolRoaming;
                    AdditionalServices.HidCodServRoaming = result.HidCodServRoaming;
                    AdditionalServices.HidTransaction = result.HidTransaction;
                    AdditionalServices.HidClassId = result.HidClassId;
                    AdditionalServices.HidSubClassId = result.HidSubClassId;
                    AdditionalServices.HidType = result.HidType;
                    AdditionalServices.HidClassDes = result.HidClassDes;
                    AdditionalServices.HidSubClassDes = result.HidSubClassDes;
                    AdditionalServices.HidMinDateDeactivation = result.HidMinDateDeactivation;
                    AdditionalServices.HidMaxDateDeactivacion = result.HidMaxDateDeactivacion;
                    AdditionalServices.HidProfileSolRoaming = result.HidProfileSolRoaming;
                    that.f_EnableTelephonyDTH(result.EnableTelephonyDTH);
                    AdditionalServices.HidAccess = AdditionalServices.AccesPage;
                    AdditionalServices.HidStateActiveCC = result.HidStateActiveCC;

                    AdditionalServices.strMaxQuota = result.strMaxQuota;
                    AdditionalServices.strMinQuota = result.strMaxQuota;
                    AdditionalServices.strMaxPeriod = result.strMaxQuota;
                    AdditionalServices.strMinPeriod = result.strMaxQuota;
                    AdditionalServices.strPeriod = result.strMaxQuota;
                    AdditionalServices.strModQuotaPer = result.strMaxQuota;
                    AdditionalServices.strEnvioLog = result.strEnvioLog;
                    AdditionalServices.strlblPhoneNumber = result.lblPhoneNumber;
                    AdditionalServices.gConstResultadoErrorBSCS = result.gConstResultadoErrorBSCS;
                    AdditionalServices.HidStateUserBSCS = result.HidStateUserBSCS;


                    if (result.MessageCode == 'E') {
                        //result.Message;
                    }else if (result.MessageCode == 'E_A') {
                        alert(result.Message,"Alerta");
                        return;
                    }
                    else
                    {
                        controls.tblContracts.find('tbody').html('');
                        controls.tblContracts.DataTable({
                            "pagingType": "full_numbers",
                           // "scrollY": "300px",
                            "scrollCollapse": true,
                            "info": false,
                         //   "select": 'single',
                            "ordering": false,
                            "paging": false,
                            "pageLength": false,
                            "searching": false,
                            "destroy": true,
                            "data": result.ListContractByPhoneNumber,
                            "columns": [
                                {
                                    "render": function () {
                                        return "<input type='radio'  name='rdbContract' id='rdbContract' class='clsrdbContract'/>";
                                    }
                                },
                                { "data": "CustCod" },
                                { "data": "CodId" },
                                { "data": "Name" },
                                { "data": "State" },
                                { "data": "Date" },
                                { "data": "Reason" }
                            ],
                            "language": {
                                "lengthMenu": "Mostrar _MENU_ registros por página.",
                                "zeroRecords": "No existen datos",
                                "info": " ",
                                "infoEmpty": " ",
                                "infoFiltered": "(filtered from _MAX_ total records)",
                            }
                        });
                        //that.
                        //Loading(controls);
                        $('#rdbContract').prop("checked", true);
                        LoadContract();
                        //$(document).on('checked', '.clsrdbContract', function (e) {
                        //    LoadContract();
                        //});
                        controls.tblServicesAdd.find('tbody').html('');
                        Grid_ServicesContract = controls.tblServicesAdd.DataTable({
                            //pagingType: "full_numbers",
                            info: false,
                            select: 'single',
                            paging: false,
                            searching: false,
                            scrollX: false,
                            scrollY: 300,
                            scrollCollapse: true,
                            destroy: true,                         
                            //ordering: true,                           
                            //pageLength: false,                           
                            columnDefs: [
                                { targets: [0], width: '20px' },
                                { targets: [1], visible: false },
                                { targets: [2], width: '170px' },
                                { targets: [3], visible: false },
                                { targets: [4], visible: false },
                                { targets: [5], width: '150px' },
                                { targets: [6], width: '70px' },
                                { targets: [7], width: '70px' },
                                { targets: [8], width: '130px' },
                                { targets: [9], width: '130px' },
                                { targets: [10], width: '80px' },
                                { targets: [11], visible: false },
                                { targets: [12], width: '80px' },
                                { targets: [13], visible: false },
                                { targets: [14], visible: false },
                                { targets: [15], visible: false },
                                { targets: [16], visible: false }
                            ],
                            //fixedColumns : true,
                            drawCallback: function (settings) {
                                var api = this.api();
                                var rows = api.rows({ page: 'current' }).nodes();
                                var last = null;                       
                                api.column(15, { page: 'current' }).data().each(function (group, i) {
                                    if (last !== group) {
                                        $(rows).eq(i).before(
                                            '<tr class="group"><td colspan="9" class="TablaTitulos">' + group + '</td></tr>'
                                        );
                                        last = group;
                                    }
                                });
                            },
                            createdRow: function (row, data, index) {
                               
                                var color = "black";
                                var fondo = "white";

                                var flagEstado = "";
                                var valorAux = 0;
                                if (data[6] == "Activo") {
                                    color = "green";
                                }
                                if (data[6] == "Desactivo") {
                                    color = "red";
                                }
                                
                                //if (data[14] == "S" && data[13] == "S" || data[2] == "CORE ADICIONAL" || data[2] == "CORE") {
                                //    fondo = "#FAFAB1";
                                //} else {
                                //    if (data[14] == "S") {
                                //        fondo = "#E9E9E7"; //plomo
                                //    } else {
                                //        if (data[13] == "S") {
                                //            fondo = "Fuchsia";
                                //        }
                                //    }
                                //}


                                if (data[14] == "S") {
                                    fondo = "#E9E9E7"; //plomo
                                } else {
                                    if (data[13] == "S") {
                                        fondo = "#FAFAB1";
                                    }
                                }
                                $("td", row).css({ "color": color , "background-color": fondo });
                            }
                        });
                    }

                    that.f_GetSolRoammingTypeList();
                    if (AdditionalServices.HidRecord != "") {
                        that.f_InputDisabled(true);
                    }

                }

                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });

        },
        PostBack: function() {
            var that = this,
                controls = this.getControls();

            var strUrlModal = that.strUrl + '/Postpaid/AdditionalServices/PostBack';

            var flagchkSendMail;
            if (controls.chkSendMail.prop("checked")) {
                flagchkSendMail = "T";
            } else {
                flagchkSendMail = "F";
            }

            var flagchkProgram;
            if (controls.chkProgram.prop("checked")) {
                flagchkProgram = "T";
            } else {
                flagchkProgram = "F";
            }


            var flagrdbIndeterminateSelected;
            if (controls.rdbIndeterminate.prop("checked")) {
                flagrdbIndeterminateSelected = "T";
            }

            var falgrdbDeterminedSelected;
            if (controls.rdbDetermined.prop("checked")) {
                falgrdbDeterminedSelected = "T";
            }


            var cboCACDACSelected = $('select[id = cboCACDAC] option:selected').text();
            var txtNotes = $('textarea[id = txtNotes]').val();
            var txtQuot = $('input[id = txtQuot]').val();
            var txtPeriod = $('input[id = txtPeriod]').val();
            var txtDateDeact = $('input[id = txtDateDeact]').val();
            var txtDateAct = $('input[id = txtDateAct]').val();
            var txtDateApp = $('input[id = txtDateApp]').val();

            var objAdditionalServices = {
                IdSession: AdditionalServices.IdSession,
                ContractId: AdditionalServices.ContractId,
                Transaction: AdditionalServices.Transaction,
                lblPhoneNumber: AdditionalServices.lblPhoneNumber,
                lblCustomerType: AdditionalServices.lblCustomerType,
                lblCustomerName: AdditionalServices.lblCustomerName,
                lblPlanName: AdditionalServices.lblPlanName,
                lblCycleFact: AdditionalServices.lblCycleFact,
                UserLogin: AdditionalServices.UserLogin,
                SessionProfile: AdditionalServices.SessionProfile,
                TxtNote: txtNotes,
                chkSendMail_IsCheched: flagchkSendMail,
                txtEmail: AdditionalServices.txtEmail,
                cboCACDACValue: cboCACDACSelected,
                HidQoutMod: txtQuot,
                HidPeriodMod: txtPeriod,
                TypeDocument: AdditionalServices.TypeDocument,
                HidCodId_Contract: AdditionalServices.HidCodId_Contract,
                FullName: AdditionalServices.FullName,
                LegalAgent: AdditionalServices.LegalAgent,
                FirstName: AdditionalServices.Name,
                LastName: AdditionalServices.LastName,
                DniRuc: AdditionalServices.DniRuc,
                PhoneReference: AdditionalServices.PhoneReference,
                CustomerContact: AdditionalServices.CustomerContact,
                txtDateAct: txtDateAct,
                txtDateDeact: txtDateDeact,
                Plan: AdditionalServices.lblPlanName,
                chkProgram_IsChecked: flagchkProgram,
                txtDateApp: txtDateApp,
                rdbIndeterminate_IsChecked: flagrdbIndeterminateSelected,
                rdbDetermined_IsChecked: falgrdbDeterminedSelected

        };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                data: JSON.stringify(objAdditionalServices),
                url: strUrlModal,
                success: function(result) {
                    if (result.MessageCode == "E") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.MessageLabel);
                        return;
                    } else if (result.MessageCode == "A") {
                        alert(result.Message,"Informativo");
                        return;
                    }
                    else if (result.MessageCode == "OK") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.MessageLabel);
                        return;
                    } else if (result.MessageCode == "E_A") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.MessageLabel);
                        alert(result.Message,"Informativo");
                        return;
                    }else{
                        controls.divShowMessage.hide();
                    }
                }
                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });

        },
        AccesPage: function () {
            var that = this, controls = this.getControls();
            var strUrlModal = that.strUrl + '/Postpaid/AdditionalServices/AccesPAge';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: {},
                url: strUrlModal,
                success: function (response) {
                    AdditionalServices.strOpcActivaCheckProgramarDTH = response[0];

                    that.EnablePermission();
                    controls.btnConstancy.prop("disabled", true);
                }
            });
        },
        EnablePermission: function () {
            var that = this,
                controls = this.getControls();

            var accessPageProfile = AdditionalServices.AccesPage;
            if (accessPageProfile.indexOf(AdditionalServices.strOpcActivaCheckProgramarDTH) !== 1) {
                AdditionalServices.HidPermissions = "1" & "|";
            } else {
                AdditionalServices.HidPermissions = "0" & "|";
            }

        },
        btnConsultEquipment_click: function () {
            var that = this,
                controls = this.getControls();
            that.f_GetTeam();
        },
        btnProActive_click: function () {
            var that = this,
                controls = this.getControls();
            
            that.f_ValidateDateApp("A");
        },

        btnProDesactive_click: function () {
            var that = this,
               controls = this.getControls();
            that.f_ValidateDateApp("D");
        },
        btnActive_click: function () {
            var that = this,
        controls = that.getControls();

            that.f_ValidateActive();
        },
        btnConstancy_click: function() {
            var that = this, controls = this.getControls();
            //that.strPDFRoute = "home//weblogicap";
            var strPDFRoute = AdditionalServices.HidRoutePdf;
            var filetransaction = "Record";
            var Filename = "Record.pdf";

            ReadRecord(Session.IDSESSION, strPDFRoute, filetransaction, Filename);
        },
        btnDesactive_click: function () {
            var that = this, controls = this.getControls();
            that.f_ValidateInactive("D");
        },
        btnKeepActive_click: function () {
            var that = this,
            controls = that.getControls();
            that.f_MaintainActivation();
        },


        btnCloseError: function () {
            var that = this,
                controls = this.getControls();

            //$('#divErrorAlert').hide();
            controls.divShowMessage.hide();
        },

        getCACDAC: function () {
            var that = this,
                objCacDacType = {};

            objCacDacType.strIdSession = AdditionalServices.IdSession;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: window.location.protocol + '//' + window.location.host + '/Transactions/CommonServices/GetCacDacType',

                success: function (response) {
                    that.createDropdownCACDAC(response);
                }
            });

        },

        //Event
        chkProgram_click : function(){
            var that = this,
          controls = that.getControls();
            var CodServ = AdditionalServices.HidCodService; // $("#hidCodigoServicio").val();
            if (controls.chkProgram.is(':checked')) { that.f_CheckProgram(CodServ);  }
            else { that.f_UnCheckProgram(CodServ); }
            controls.txtDateAct.val("");
            controls.txtDateDeact.val("");
        },
        rdbDetermined_click: function () {
            var that = this,
          controls = that.getControls();
            if (controls.rdbDetermined.is(':checked')) {
                if (AdditionalServices.HidState == Desactivo) {
                    controls.txtDateAct.css("display", "inline");
                    controls.lblCldFI.css("display", "inline");
                    controls.txtDateDeact.css("display", "inline");
                    controls.lblCldFF.css("display", "inline");

                    //$("#tdCldFI").css({ "width": "50%", "padding-left": "0px" });
                    //$("#tdCldFF").css({ "width": "50%", "padding-left": "0px" });
                     
                }
            }
        },
        rdbIndeterminate_click: function () {
            var that = this,
         controls = that.getControls();
            if (controls.rdbIndeterminate.is(':checked')) {
                if (AdditionalServices.HidState == Desactivo) {
                    controls.txtDateAct.css("display", "inline");
                    controls.lblCldFI.css("display", "inline");
                    //controls.txtDateDeact.css("display", "none");
					$('#divDateDesact').css("display", "none");
                    controls.lblCldFF.css("display", "none");

                    //$("#tdCldFI").css({ "width": "100%", "padding-left": "400px" });
                    //$("#tdCldFF").css({ "width": "0%", "padding-left": "0px" });

                }
            }
        },
        f_Modify_click: function () {
        var that = this,
        controls = that.getControls();
      
            $('#tblContracts tr').each(function () {
                var state = $(this).find('#rdbContract').is(':checked');
                if (state == true) {
                    var codId = $(this).find('td').eq(2).text();
                    var status = $(this).find('td').eq(4).text();
                    AdditionalServices.HidCodId_Contract = codId;
                    AdditionalServices.HidStateContract = status;
                }
            });

            var nombre = AdditionalServices.HidNameObj; //$('#hidNameObj').val();
            $("#tblServicesAdd tr").each(function () {
                var state = $(this).find(".rdbServicebyContract").is(":checked");
                if (state == true) {
                    var Quota = parseFloat($(this).find(controls.txtQuot.val()).toFixed(2));
                    var Period = parseInt($(this).find(controls.txtPeriod.val()));

                    if ((isNaN(Quota)) && (isNaN(Period))) {
                        alert('Debe ingresar cuota y periodo.',"Alerta");
                        return false;
                    }
                    if (isNaN(Quota)) {
                        alert('Debe ingresar cuota.',"Alerta");
                        return false;
                    }
                    if ((parseFloat(Quota) > parseFloat(AdditionalServices.strMaxQuota) || parseFloat(Quota) < parseFloat(AdditionalServices.strMinQuota)) && isNaN(Period)) {
                        alert('El monto es incorrecto y se debe ingresar un periodo.',"Alerta");
                        return false;
                    }
                    if (isNaN(Period)) {
                        alert('Debe ingresar periodo.',"Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) < parseFloat(AdditionalServices.strMinQuota) && parseInt(Period) == parseInt(AdditionalServices.strPeriod)) {
                        alert('La cuota es menor que la configuración mínima, y el periodo 0 no es permitido.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) < parseFloat(AdditionalServices.strMinQuota) && parseInt(Period) < parseInt(AdditionalServices.strMinPeriod)) {
                        alert('La cuota y el periodo es menor que la configuración mínima.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) < parseFloat(AdditionalServices.strMinQuota) && parseInt(Period) > parseInt(AdditionalServices.strMaxPeriod)) {
                        alert('La cuota es menor que la configuración mínima, y el periodo es mayor que la configuración máxima.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) > parseFloat(AdditionalServices.strMaxQuota) && parseInt(Period) == parseInt(AdditionalServices.strPeriod)) {
                        alert('La cuota es mayor que la configuración máxima, y el periodo 0 no es permitido.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) > parseFloat(AdditionalServices.strMaxQuota) && parseInt(Period) < parseInt(AdditionalServices.strMinPeriod)) {
                        alert('La cuota es mayor que la configuración máxima, y el periodo es menor que la configuración mínima.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) > parseFloat(AdditionalServices.strMaxQuota) && parseInt(periodo) > parseInt(AdditionalServices.strMaxPeriod)) {
                        alert('La cuota y el periodo es mayor que la configuración máxima.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) < parseFloat(AdditionalServices.strMinQuota)) {
                        alert('La cuota es menor que la configuración mínima.', "Alerta");
                        return false;
                    }
                    if (parseFloat(Quota) > parseFloat(AdditionalServices.strMaxQuota)) {
                        alert('La cuota es mayor que la configuración máxima.', "Alerta");
                        return false;
                    }
                    if (parseInt(Period) == parseInt(AdditionalServices.strPeriod)) {
                        alert('El periodo 0 no es permitido.', "Alerta");
                        return false;
                    }
                    if (parseInt(Period) < parseInt(AdditionalServices.strMinPeriod)) {
                        alert('El periodo es menor que la configuración mínima.', "Alerta");
                        return false;
                    }
                    if (parseInt(Period) > parseInt(AdditionalServices.strMaxPeriod)) {
                        alert('El periodo es mayor que la configuración máxima.', "Alerta");
                        return false;
                    }
                    if (controls.cboCACDAC.val() == '-1' || controls.cboCACDAC.val() == '') {
                        alert('Debe seleccionar un punto de atención.', "Alerta");
                        return false;
                    }

                    AdditionalServices.HidQoutMod = Quota;
                    AdditionalServices.HidPeriodMod = Period;

                    if (AdditionalServices.HidAccess.indexOf(AdditionalServices.HidAccessMCP) == -1) {
                        alert('Usted No tiene Autorización para modificar cuota y periodo, por favor comuníquese con su supervisor.', "Alerta");
                        //Show1();
                        strModCuoPer = "gConstkeyModCuoPer";
                        that.f_ValidateLogin(strModCuoPer);

                    } else {
                        //Hiden2();
                        var oBE = {};
                        oBE.HidEstGraMCP = "GCP";
                        oBE.IdSession = AdditionalServices.IdSession;
                        oBE.ContractId = AdditionalServices.ContractId;
                        oBE.HidContract = AdditionalServices.ContractId;
                        oBE.CustomerId = AdditionalServices.CustomerId;

                        oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                        oBE.HidType = AdditionalServices.HidType;
                        oBE.HidClassDes = AdditionalServices.HidClassDes;
                        oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                        oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                        oBE.TxtNote = controls.txtNotes.val();
                        oBE.UserLogin = AdditionalServices.UserLogin;
                        oBE.txtDateAct = controls.txtDateAct.val();
                        oBE.txtDateDeact = controls.txtDateDeact.val();
                        oBE.HidTransaction = AdditionalServices.HidTransaction;
                        oBE.FirstName = AdditionalServices.FirstName;
                        oBE.LastName = AdditionalServices.LastName;
                        oBE.DniRuc = AdditionalServices.DniRuc;
                        oBE.PhoneReference = AdditionalServices.PhoneReference;
                        oBE.HidNameService = AdditionalServices.HidNameService;
                        oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                        oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                        oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;

                        oBE.HidDateFrom = AdditionalServices.HidDateFrom;

                        if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                            oBE.chkSendMail_IsCheched = "T";
                            oBE.txtEmail = controls.txtEmail.val();
                        }
                        else {
                            oBE.chkSendMail_IsCheched = "F";
                            oBE.txtEmail = "";
                        }
                        oBE.cboCACDACValue = controls.cboCACDAC.val();
                        oBE.Plan = AdditionalServices.lblPlanName;
                        if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                            oBE.chkProgram_IsChecked = "T";
                        }
                        else {
                            oBE.chkProgram_IsChecked = "F";
                        }

                        oBE.txtDateApp = controls.txtDateApp.val();

                        oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                        oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                        oBE.HidProgramingRoamming = controls.txtDateApp.val();


                        if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                            oBE.rdbDetermined_IsChecked = "T";
                        }
                        else {
                            oBE.rdbDetermined_IsChecked = "F";
                        }

                        if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                            oBE.rdbIndeterminate_IsChecked = "T";
                        }
                        else {
                            oBE.rdbIndeterminate_IsChecked = "F";
                        }
                        oBE.LegalAgent = AdditionalServices.LegalAgent;
                        oBE.HidCodId = AdditionalServices.HidCodId;
                        oBE.CustomerId = AdditionalServices.CustomerId;
                        oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                        oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                        oBE.Account = AdditionalServices.Account;
                        oBE.HidState = AdditionalServices.HidState;
                        oBE.HidBloqDes = AdditionalServices.HidBloqDes;
                        //oBE.HidContract = AdditionalServices.HidContract;
                        oBE.HidCodService = AdditionalServices.HidCodService;
                        oBE.HidStateContract = AdditionalServices.HidStateContract;
                        oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                        oBE.HidCodPackage = AdditionalServices.HidCodPackage;
                        oBE.HidCodExclusive = AdditionalServices.HidCodExclusive;
                        oBE.ContactCode = AdditionalServices.ContactCode;
                        
                        $.ajax({
                            url: URL_PostBack,
                            data: JSON.stringify(oBE),
                            type: 'POST',
                            contentType: "application/json charset=utf-8;",
                            dataType: "json",
                            //error: function (request, status, error) {
                            //    //console.logerror);
                            //    alert(error);
                            //},
                            success: function (response) {

                                if (response.Message != "") {
                                    alert(response.Message,"Informativo")
								}
								if (response.Message != "") {
								    alert(response.MessageLabel, "Informativo");
                                }
                                if (response.HidCaseId != "") {
                                    AdditionalServices.HidCaseId = response.HidCaseId;
                                    LoadServicesContract(AdditionalServices.ContractId);
                                    AdditionalServices.HidAction = response.HidAction;
                                    AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                }
                                else {
                                    AdditionalServices.HidCaseId = "";
                                }

                                
                            }
                        });
                    }
                }
            });
        },
        //Method


        //Functions
            
        f_ContractBlock: function (gridName, pos) {
            var that = this,
           controls = that.getControls();

            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "radio") {
                    var count = all[i].name.indexOf(gridName + '$ctl');
                    if (count != -1) {
                        all[i].checked = false;
                        all[document.getElementById(controls.hdnPosActiva.attr("id")).value].checked = true;
                    }
                }
            }
            alert('Solo puede consultar contratos activos.',"Alerta");

        },
        f_RadioBlock: function (gridName) {
            var that = this,
            controls = that.getControls();
            all = document.getElementsByTagName("input");
            for (i = 0; i < all.length; i++) {
                if (all[i].type == "radio") {
                    var count = all[i].name.indexOf(gridName + '$ctl');
                    if (count != -1) {
                        all[i].checked = false;
                    }
                }
            }
            alert('Tiene un servicio activo con el mismo motivo de servicio excluyente.',"Alerta");
            controls.btnProActive.prop("disabled", true);
            controls.btnProDesactive.prop("disabled", true);
            
            //document.getElementById(controls.btnProActive.attr("id")).disabled = true;
            //document.getElementById(controls.btnProDesactive.attr("id")).disabled = true;
            
        },
        
        f_EnableMail: function () {
            var that = this,
           controls = that.getControls();

            var objCheck = document.getElementById(controls.chkSendMail.attr("id"));
            var objEmail = document.getElementById(controls.txtEmail.attr("id"));
            if (objCheck.checked == true) {
                objEmail.style.display = "";
            }
            else {
                objEmail.style.display = "none";
            }
        },
        f_Select: function (codServ, codPaquete, bloqDesact, bloqAct, estado, nomServ, cargoF, cargoFM, nroPeriodo, codExcluyente, fechadesde) {
            var that = this,
            controls = that.getControls();

            //all=document.getElementsByTagName("input");
            //for (i = 0; i < all.length; i++) 
            //{
            //    if (all[i].type == "radio") 
            //    {
            //        var count=all[i].name.indexOf(gridName+'$ctl'); 
	   
            //        if (count != -1) 
            //        {
            //            all[i].checked=false;
            //        }
            //    }
            //}
            //rdo.checked=true;
            var vRecord = AdditionalServices.HidRecord;

           
            if (vRecord != "") { return false; }

            AdditionalServices.HidCodService = (codServ == null) ? "" : codServ; 

            AdditionalServices.HidCodPackage = (codPaquete == null) ? "" : codPaquete;

            AdditionalServices.HidBloqDes = (bloqDesact == null) ? "" : bloqDesact;
           
            AdditionalServices.HidBloqAct = (bloqAct == null) ? "" : bloqAct;

            AdditionalServices.HidState =  (estado==null)? "" : estado;

            AdditionalServices.HidNameService = nomServ;
            AdditionalServices.HidFixedChargeM = cargoFM;
            AdditionalServices.HidFixedCharge = cargoF;
             
            AdditionalServices.HidNumberPeriod = nroPeriodo;
            AdditionalServices.HidCodExclusive = codExcluyente;
       
            if((fechadesde=="A") || ($.trim(fechadesde)=="&nbsp;")){
                var d = new Date();
                var FechadeHoy = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
                AdditionalServices.HidDateFrom = FechadeHoy;
            }else{
                fechadesde=fechadesde.substring(0,11);
                AdditionalServices.HidDateFrom = fechadesde;
            }

            var objCheck = document.getElementById(controls.chkProgram.attr("id"));
            var objProgramAct = document.getElementById(controls.btnProActive.attr("id"));
            var objProgramDesact = document.getElementById(controls.btnProDesactive.attr("id"));
            var objCodSer4g = AdditionalServices.HidCodServ4G; 
		
            if(codServ==objCodSer4g){
                controls.chkProgram.prop("disabled",true)
            }
            else{
                controls.chkProgram.prop("disabled", false)
            }
	   
            var CodServRoamming = AdditionalServices.HidCodServRoaming; 
            if (parseInt(codServ) == parseInt(CodServRoamming)) {

                controls.btnProActive.css("display", "none");
                controls.btnProDesactive.css("display", "none");
                controls.btnKeepActive.css("display", "inline");
                controls.lblLegendProg.text("Programar Activación/Desactivación Roamming");
                //$("#MostrarTipoSolicitud").css("display","inline");
   				//$('#divCareChannel').css("display", "inline");
                $('#divCareChannel').show();
            }
            else{
				$('#divCareChannel').hide();
                //$('#divCareChannel').css("display", "none");
                //$("#MostrarTipoSolicitud").css("display","none");
                controls.btnProActive.css("display", "inline");
                controls.btnProDesactive.css("display", "inline");
                controls.btnKeepActive.css("display", "none");
            }
			
            if(objCheck.checked == true){
                switch(bloqAct){					
                    case S:
                        switch(bloqDesact){
                            case S:
                                objProgramAct.disabled=true;									
                                objProgramDesact.disabled=true;
                                break;
                            case N:	
                                objProgramAct.disabled=true;
                                objProgramDesact.disabled=false;
                                break;
                        }
                        break;
                    case N:
                        switch(bloqDesact){
                            case S:
                                objProgramAct.disabled=false;
                                objProgramDesact.disabled=true;
                                break;
                            case N:	
                                objProgramAct.disabled=false;
                                objProgramDesact.disabled=false;
                                break;
                        }
                        break;
                }
                that.f_CheckProgram(codServ);
            }
            else
            {
                that.f_UnCheckProgram(codServ);
            }	
		
			
            if (estado == Activo)
            {
                controls.btnActive.prop("disabled", true);
                controls.btnDesactive.prop("disabled", false);
                controls.btnKeepActive.prop("disabled",false);
            }
            else if ((estado == Desactivo) || (estado == '') || (estado == '&nbsp;'))
            {
                controls.btnActive.prop("disabled", false);
                controls.btnDesactive.prop("disabled", true);
                controls.btnKeepActive.prop("disabled", true);
            }
        },
        f_CheckProgram: function (codServ) {
            var that = this,
            controls = that.getControls();

            var objCodServ = "";
            if (($.type(codServ) === "null") || ($.trim(codServ) === "")) {
                objCodServ = AdditionalServices.HidCodService; 
            }
            else  { objCodServ = codServ; }

            var CodServRoamming = AdditionalServices.HidCodServRoaming; 
            var Estado = AdditionalServices.HidState;

            if ((Estado == '') || (Estado == '&nbsp;') || ($.type(Estado) === "null")) {
                AdditionalServices.HidState = Desactivo;
                Estado = Desactivo;
            }

            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {
                controls.DivIdServRoaming.css('display', 'inline');
                controls.DivFechApli.css('display', 'none');

                if ($.trim(Estado) == Activo) {
                    controls.rdbIndeterminate.prop("disabled", true);
                    controls.rdbDetermined.prop("checked", true);
                    controls.txtDateAct.css("display", "none");
                    controls.lblCldFI.css("display", "none");
                    // $("#imgFechIniRoaming").css("display", "none");
                    controls.txtDateDeact.css("display", "inline");
                    controls.lblCldFF.css("display", "inline");
                    //$("#imgFechFinRoaming").css("display", "inline");
                
                }
                else if (($.trim(Estado) == Desactivo) || (Estado == '') || (Estado == '&nbsp;') || (Estado === "null")) {
                    controls.rdbDetermined.prop("checked", true);
                    controls.txtDateAct.css("display", "inline");
                    controls.lblCldFI.css("display", "inline");
                    controls.txtDateDeact.css("display", "inline");
                    controls.lblCldFF.css("display", "inline");
                }

                controls.btnProRoamming.css('display', 'inline');
                controls.btnProActive.css('display', 'none');
                controls.btnProDesactive.css('display', 'none');
                controls.btnDesactive.css('display', 'none');
                controls.btnActive.css("display", "none");
                controls.btnKeepActive.css("display", "none");
            } else {
           
                controls.DivIdServRoaming.css('display', 'none');
                controls.btnProRoamming.css('display', 'none');
                controls.btnProActive.css('display', 'inline');
                controls.btnProDesactive.css('display', 'inline');
                controls.btnDesactive.css('display', 'inline');
                controls.btnActive.css("display", "inline");
                controls.btnKeepActive.css("display", "none");
                that.f_ProgramActive();
            }


        },
        f_ProgramActive: function () {

            var that = this,
           controls = that.getControls();

            var objCheck = document.getElementById(controls.chkProgram.attr("id"));
            var objActivar = document.getElementById(controls.btnActive.attr("id"));
            var objDesactivar = document.getElementById(controls.btnDesactive.attr("id"));
            var objProgramAct = document.getElementById(controls.btnProActive.attr("id"));
            var objProgramDesact = document.getElementById(controls.btnProDesactive.attr("id"));
            var objCeldaFechaApli = document.getElementById(controls.DivFechApli.attr("id"));
            var objFechFacturacion = document.getElementById(controls.lblCycleFact.attr("id"));
            var objFechAplica = document.getElementById(controls.txtDateApp.attr("id"));

            var mydate = new Date();
            var objDia = mydate.getDate();
            var objMes = mydate.getMonth() + 1;
            var objAnio = mydate.getFullYear();
            var objFechaHoy = '';
            if (objMes < 10) {
                objFechaHoy = objDia + "/" + "0" + objMes + "/" + objAnio;
            } else {
                objFechaHoy = objDia + "/" + "0" + objMes + "/" + objAnio;
            }

            var objCicloFactAnterio = '';

            if (objCheck.checked == true) {
                objCeldaFechaApli.style.display = "";
                objActivar.disabled = true;
                objDesactivar.disabled = true;

                var strBloqueoActivo = AdditionalServices.HidBloqDes; 
                var strBloqueoDesactivo = AdditionalServices.HidBloqAct; 


                switch (strBloqueoActivo) {
                    case S:
                        switch (strBloqueoDesactivo) {
                            case S:
                                objProgramAct.disabled = true;
                                objProgramDesact.disabled = true;
                                break;
                            case N:
                                objProgramAct.disabled = true;
                                objProgramDesact.disabled = false;
                                break;
                        }
                        break;
                    case N:
                        switch (strBloqueoDesactivo) {
                            case S:
                                objProgramAct.disabled = false;
                                objProgramDesact.disabled = true;
                                break;
                            case N:
                                objProgramAct.disabled = false;
                                objProgramDesact.disabled = false;
                                break;
                        }
                        break;
                }

                if (objDia >= objFechFacturacion.innerHTML) {
                    objMes = objMes + 1;
                    if (objMes > 12) {
                        objMes = 1;
                        objAnio = objAnio + 1;
                    }
                    var objNuevoDia = objFechFacturacion.innerHTML - 1;
                    if (objNuevoDia < 10) {
                        objFechAplica.value = "0" + objNuevoDia + "/";
                        if (objMes < 10) {
                            objFechAplica.value = objFechAplica.value + "0" + objMes + "/" + objAnio;
                        }
                        else {
                            objFechAplica.value = objFechAplica.value + objMes + "/" + objAnio;
                        }
                    }
                    else {
                        objFechAplica.value = objNuevoDia + "/";
                        if (objMes < 10) {
                            objFechAplica.value = objFechAplica.value + "0" + objMes + "/" + objAnio;
                        }
                        else {
                            objFechAplica.value = objFechAplica.value + objMes + "/" + objAnio;
                        }
                    }

                } else {
                    var objNuevoDia = objFechFacturacion.innerHTML - 1;
                    if (objNuevoDia < 10) {
                        objFechAplica.value = "0" + objNuevoDia + "/";
                        if (objMes < 10) {
                            objFechAplica.value = objFechAplica.value + "0" + objMes + "/" + objAnio;
                        }
                        else {
                            objFechAplica.value = objFechAplica.value + objMes + "/" + objAnio;
                        }
                    }
                    else {
                        objFechAplica.value = objNuevoDia + "/";
                        if (objMes < 10) {
                            objFechAplica.value = objFechAplica.value + "0" + objMes + "/" + objAnio;
                        }
                        else {
                            objFechAplica.value = objFechAplica.value + objMes + "/" + objAnio;
                        }
                    }
                }

                if (objFechAplica.value == objFechaHoy) {
                    if (parseInt(objFechAplica.value.split('/')[1]) < 12) {
                        if (parseInt(objFechAplica.value.split('/')[1]) < 10) {
                            objFechAplica.value = objFechAplica.value.split('/')[0] + "/" + '0' + (parseInt(objFechAplica.value.split('/')[1]) + 1) + "/" + objFechAplica.value.split('/')[2];
                        } else {
                            objFechAplica.value = objFechAplica.value.split('/')[0] + "/" + (parseInt(objFechAplica.value.split('/')[1]) + 1) + "/" + objFechAplica.value.split('/')[2];
                        }
                    } else {
                        objFechAplica.value = objFechAplica.value.split('/')[0] + "/" + "01" + "/" + (parseInt(objFechAplica.value.split('/')[2]) + 1);
                    }
                }
            }
            else {
                objCeldaFechaApli.style.display = "none";
                objActivar.disabled = false;
                objDesactivar.disabled = false;
                objProgramAct.disabled = true;
                objProgramDesact.disabled = true;
            }

        },
        f_UnCheckProgram: function (codServ) {
            var that = this,
          controls = that.getControls();

            var objCodServ = "";
            var Estado = AdditionalServices.HidState; 
            var CodServRoamming = AdditionalServices.HidCodServRoaming; 

            if (($.type(codServ) === "null") || ($.trim(codServ) === ""))
            {
                objCodServ = AdditionalServices.HidCodService; 
            }
            else
            {
                objCodServ = codServ;
            }

            
            controls.rdbDetermined.prop("checked", false);
            controls.rdbIndeterminate.prop("disabled", false);
            controls.rdbIndeterminate.prop("checked", false);

            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {

                controls.DivIdServRoaming.css('display', 'none');
                controls.DivFechApli.css('display', 'none');
                
                controls.btnProRoamming.css('display', 'none');
                controls.btnProActive.css("display", "none");
                controls.btnProDesactive.css("display", "none");
                controls.btnKeepActive.css("display", "inline");
               
                if ($.trim(Estado != "")) {
                    if ($.trim(Estado) == Desactivo) {
                        controls.btnDesactive.css('display', 'inline');
                        controls.btnDesactive.prop("disabled", true);

                        controls.btnActive.css("display", "inline");
                        controls.btnActive.prop("disabled", false);
                         
                    } else if ($.trim(Estado) == Activo) {
                        controls.btnDesactive.css('display', 'inline');
                        controls.btnDesactive.prop("disabled", false);

                        controls.btnActive.css("display", "inline");
                        controls.btnActive.prop("disabled", true);

                    }
                }
            }
            else {

                controls.DivIdServRoaming.css('display', 'none');
                controls.btnKeepActive.css("display", "none");
                controls.btnDesactive.css('display', 'inline');
                controls.btnActive.css("display", "inline");
                controls.btnProRoamming.css('display', 'none');
                controls.btnProActive.css("display", "inline");
                controls.btnProDesactive.css("display", "inline");
                 
            }

        },
        f_GetSolRoammingTypeList: function () {
            var that = this,
         controls = that.getControls();
            
            var strList = AdditionalServices.HidListTypeSolRoaming;

            controls.cboCareChannel.html("");
            if (strList != null) {
                for (var i = 0; i <= strList.split(';').length - 1; i++) {
                    var strCod = strList.split(';')[i].split(',')[0];
                    var strDes = strList.split(';')[i].split(',')[1];
                    controls.cboCareChannel.append($('<option>', { value: strCod, html: strDes }));
                }
                controls.cboCareChannel.prop("selectedIndex", parseInt(AdditionalServices.HidProfileSolRoaming));

            } else {
                //console.log"lista f_GetSolRoammingTypeList : strList = null");
            }
        },
        
        f_InputDisabled: function (Enable) {
            var that = this,
            controls = that.getControls();


            controls.btnDesactive.prop("disabled", Enable);
            controls.btnActive.prop("disabled", Enable);
            controls.btnKeepActive.prop("disabled", Enable);
            controls.chkProgram.prop("disabled", Enable);
            controls.btnProRoamming.prop("disabled",Enable);
            controls.chkSendMail.prop("disabled", Enable);
            controls.cboCareChannel.prop("disabled", Enable);
            controls.cboCACDAC.prop("disabled", Enable);
            controls.txtNotes.prop("disabled", Enable);
        },
        f_EnableTelephonyDTH: function (type) {
            var that = this,
            controls = that.getControls();
            if (type == 'Telef') {
                controls.btnConsultEquipment.css("display", "none");
            } else if (type == 'DTH') {
                controls.btnConsultEquipment.css("display", "");
            }
        },
        f_ValidateDateApp: function (Status) {
            var that = this,
            controls = that.getControls(),
            model = {};

            var CodServices = AdditionalServices.HidCodService;
            if ((CodServices == '') || (CodServices == null)) {
                alert('Seleccione un Servicio.',"Alerta");
                return false;
            }

            AdditionalServices.HidValidateInitial = 'VF';

           

            var strTelef = AdditionalServices.strlblPhoneNumber;//controls.lblPhoneNumber.text();// document.getElementById('hidTelef').value;
            var strCodId = AdditionalServices.HidCodId;//  document.getElementById('hidCodID').value;
            var strCodSer = AdditionalServices.HidCodService;// document.getElementById('hidCodigoServicio').value;
            var strStatus = AdditionalServices.HidState;

            var objFecha = document.getElementById(controls.txtDateApp.attr("id"));
            if ((objFecha.value == null) || ($.trim(objFecha.value) == '')) {
                alert('Porfavor ingrese una fecha no menor o igual a la Fecha Actual.', "Alerta");
                return false;
            }

            var objFechActual = new Date()
            var objDiaActual = objFechActual.getDate();
            var objMesActual = objFechActual.getMonth() + 1;
            var objAnioActual = objFechActual.getFullYear();
            var fecha1 = new  that.f_Date(objFecha.value);
            var cont = 0;


            if (Status == 'D') {
                AdditionalServices.HidAction = "0";
                if ((strStatus == Desactivo) || (strStatus == '') || (strStatus == null) || (strStatus == '&nbsp;')) {
                    alert('Usted No Puede Programar una Desactivación de un Servicio ya Desactivo', "Alerta");
                    return false;
                }
            }

            if (Status == 'A') {
                AdditionalServices.HidAction = "1";
                if (strStatus == Activo) {
                    alert('Usted No Puede Programar una Activación de un Servicio ya Activo', "Alerta");
                    return false;
                }
            }

            if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {

                if ((Trim(document.getElementById(controls.txtEmail.attr("id")).value) == null) || (Trim(document.getElementById(controls.txtEmail.attr("id")).value) == '')) {
                    alert('Debe ingresar el correo electrónico del Cliente por que usted ha seleccionado la opción ENVIAR POR CORREO.', "Alerta");
                    return false;
                }
            }

            if ((fecha1.dia <= objDiaActual) && (fecha1.mes <= objMesActual) && (fecha1.anio <= objAnioActual)) {
                alert('La Fecha de Aplicación no puede ser menor o igual a la Fecha Actual.', "Alerta");
            } else {


                model.IdSession = AdditionalServices.IdSession;
                model.StrStatus = Status;

                model.StrPhone = strTelef;
                model.StrCodId = strCodId;
                model.StrCodSer = strCodSer;
                
                $.ajax({
                    url: URL_ProgActDesact,
                    data: JSON.stringify(model),
                    type: 'POST',
                    contentType: "application/json charset=utf-8;",
                    dataType: "json",
                    //error: function (request, status, error) {
                    //    //console.logerror);
                    //    alert(error);
                    //},
                    success: function (response) {
                        if (response.MessageLabel.split('|').length > 1) {
                            if (response.MessageLabel.split('|')[0] == 1) {
                                var oBE = {};
                                oBE.IdSession = AdditionalServices.IdSession;
                                oBE.ContractId  = AdditionalServices.ContractId;
                                oBE.HidContract = AdditionalServices.ContractId;
                                oBE.CustomerId = AdditionalServices.CustomerId
                                oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                                oBE.HidType = AdditionalServices.HidType;
                                oBE.HidClassDes = AdditionalServices.HidClassDes;
                                oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                                oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                                oBE.TxtNote = controls.txtNotes.val();
                                oBE.UserLogin = AdditionalServices.UserLogin;
                                oBE.txtDateAct = controls.txtDateAct.val();
                                oBE.txtDateDeact = controls.txtDateDeact.val();
                                oBE.HidTransaction = AdditionalServices.HidTransaction;
                                oBE.FirstName = AdditionalServices.FirstName;
                                oBE.LastName = AdditionalServices.LastName;
                                oBE.DniRuc = AdditionalServices.DniRuc;
                                oBE.PhoneReference = AdditionalServices.PhoneReference;
                                oBE.HidNameService = AdditionalServices.HidNameService;
                                oBE.HidCodService = AdditionalServices.HidCodService;
                                oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                                oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                                oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;
                                oBE.HidAction = AdditionalServices.HidAction;
                                if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                                    oBE.chkSendMail_IsCheched = "T";
                                    oBE.txtEmail = controls.txtEmail.val();
                                }
                                else {
                                    oBE.chkSendMail_IsCheched = "F";
                                    oBE.txtEmail = "";
                                }
                                oBE.cboCACDACValue =controls.cboCACDAC.val();
                                oBE.Plan = AdditionalServices.lblPlanName;
                                if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                                    oBE.chkProgram_IsChecked = "T";
                                }
                                else {
                                    oBE.chkProgram_IsChecked = "F";
                                }

                                oBE.txtDateApp = controls.txtDateApp.val();

                                oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                                oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                                oBE.HidProgramingRoamming = AdditionalServices.HidProgramingRoamming;

                                if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                                    oBE.rdbDetermined_IsChecked = "T";
                                }
                                else {
                                    oBE.rdbDetermined_IsChecked = "F";
                                }

                                if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                                    oBE.rdbIndeterminate_IsChecked = "T";
                                }
                                else {
                                    oBE.rdbIndeterminate_IsChecked = "F";
                                }
                                oBE.LegalAgent = AdditionalServices.LegalAgent;
                                oBE.HidCodId = AdditionalServices.HidCodId;
                                oBE.CustomerId = AdditionalServices.CustomerId;
                                oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                                oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                                oBE.Account = AdditionalServices.Account;

                                oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                                oBE.HidBloqDes = AdditionalServices.HidBloqDes;

                                oBE.HidState = AdditionalServices.HidState;
                                 
                               
                                oBE.HidCodService = AdditionalServices.HidCodService;
                                oBE.HidStateContract = AdditionalServices.HidStateContract;
                
                                AdditionalServices.ContactCode = Session.DATACUSTOMER.ContactCode;
								oBE.TypeDocument = AdditionalServices.TypeDocument;
                                oBE.FullName = AdditionalServices.FullName;

                                $.ajax({
                                    url: URL_SaveProgramming,
                                    data: JSON.stringify(oBE),
                                    type: 'POST',
                                    contentType: "application/json charset=utf-8;",
                                    dataType: "json",
                                    //error: function (request, status, error) {
                                    //    //console.logerror);
                                    //    alert(error);
                                    //},
                                    success: function (response) {
                                        
                                        if (response.Message != "") {
                                            alert(response.MessageLabel,"Informativo")
                                        }
                                        if (response.HidCaseId != "") {
                                            AdditionalServices.HidCaseId = response.HidCaseId;
                                            LoadServicesContract(AdditionalServices.ContractId);
                                            AdditionalServices.HidAction = response.HidAction;
                                            AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                        }
                                        

                                    }
                                });

                                
                            }
                            else {
                                alert(response.split('|')[1],"Alerta");
                                return false;
                            }
                        }
 						return true;
                    }
                });

 
            }

        },
        f_Date: function (strChain) {

            var separador = "/"


            if (strChain.indexOf(separador) != -1) {
                var posi1 = 0
                var posi2 = strChain.indexOf(separador, posi1 + 1);
                var posi3 = strChain.indexOf(separador, posi2 + 1);
                this.dia = strChain.substring(posi1, posi2);
                this.mes = strChain.substring(posi2 + 1, posi3);
                this.anio = strChain.substring(posi3 + 1, strChain.length);
            } else {
                this.dia = 0
                this.mes = 0
                this.anio = 0
            }

        },
        f_GetTeam: function () {
            //var w = 778;
            //var h = 500;
            //var leftScreen = (screen.width - w) / 2;
            //var topScreen = (screen.height - h) / 2;
            //var opciones = "directories=no,menubar=no,scrollbars=no,status=yes,resizable=no,width=" + w + ",height=" + h + ",left=" + leftScreen + ",top=" + topScreen;
            //ventana = window.open(URL_GetTeam, 'Consultar Equipo', opciones);
            
            var urlCheckDevices = location.protocol + "//" + location.host + "/Transactions/Postpaid/CheckDevices/PostpaidCheckDevices";
            var ventana = window.open(urlCheckDevices, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=778, height=640');
            ventana.focus();
        },
        f_PermissionsValidate: function () {
            var objAccesoWC = AdditionalServices.HidAccessWC;
            var objAcceso = AdditionalServices.HidAccess;
            if (objAcceso.indexOf(objAccesoWC == -1)) {
                alert('Usted No tiene Autorización para modificar la Fecha de Aplicación, por favor comuníquese con su supervisor.', "Alerta");
                var URL = location.protocol + "//" + location.host + URL_SIACPO_Validate;
                window.open(URL + "?pag=5&opcion=gConstValidaProgramarFecha&tipotx=NA", window, 'dialogTop:200;status:no;edge:sunken;dialogHide:false;help:no;dialogWidth:283px;dialogHeight:153px');
            }
            else {
               // show_calendar('Form1.txtFechAplicacion', '../../../');
            }
        },
        f_PermissionsResponse: function (User) {
            if (User != '') {
                if (User.indexOf('PF') > -1) {
                 //   show_calendar('Form1.txtFechAplicacion', '../');
                }
                if (User.indexOf('FA') > -1) {

                    //document.getElementById('hidEstadoDesactApadece').value = 'FA';
                    //document.Form1.submit();
                    var oBE = {};
                    oBE.blnNoTipi = false; 

                    $.ajax({
                        url: URL_Desactivar,
                        data: JSON.stringify(oBE),
                        type: 'POST',
                        contentType: "application/json charset=utf-8;",
                        dataType: "json",
                        //error: function (request, status, error) {
                        //    //console.logerror);
                        //    alert(error);
                        //},
                        success: function (response) {
                            //Falta que hacer
                        }
                    });

                }

            }
        },
        f_DisabledMessage: function (bStatus, bError, Mensaje) {
            var that = this,
          controls = that.getControls();

            if (bStatus == null) { bStatus = true; }
            if (bError == null) { bError = false; }

            controls.btnProActive.prop("disabled", bStatus);
            controls.btnProDesactive.prop("disabled", bStatus);
            controls.btnActive.prop("disabled", bStatus);
            controls.btnDesactive.prop("disabled", bStatus);
            controls.chkProgram.prop("disabled", bStatus);
            controls.chkProgram.prop("checked", !bStatus);
            controls.chkSendMail.prop("disabled", bStatus);
            controls.chkSendMail.prop("checked", !bStatus);
            controls.btnConstancy.prop("disabled", !bStatus);
            alert(Mensaje, "Alerta");

            if (bError) { parent.window.close(); }
        },
        f_Disabled: function (Action) {
            var that = this,
            controls = that.getControls();

            if (AdditionalServices.HidCodService == '' || AdditionalServices.HidCodService == null) {
                alert('Seleccione un Servicio.', "Alerta");
                return false;
            }
            if (Action == 'D') {
                if (AdditionalServices.HidState == Desactivo || AdditionalServices.HidState == '' || AdditionalServices.HidState == null || AdditionalServices.HidState == '&nbsp;') {
                    alert('Usted No Puede Desactivar un Servicio ya Desactivo.', "Alerta");
                    return false;
                }
            }
            
            AdditionalServices.HidStatePrograming = Action;
            AdditionalServices.HidStatusDisabledApa = 'FA';

             
            var objCodServ = AdditionalServices.HidCodService; 
            var CodServRoamming = AdditionalServices.HidCodServRoaming; 
            var ddlTipoSol = controls.cboCareChannel.val(); 

            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {
                if (parseInt(ddlTipoSol) == parseInt("-1")) { alert('Seleccione Canal de Atención.', "Alerta"); return false; }
                else {
                    var str = "";
                    $("#"+ controls.cboCareChannel.attr("id") +" option:selected").each(function () { str += $(this).text(); });
                    //$("#hidTipoSolicitud").val(str);

                    AdditionalServices.HidTypeRequest = str;
                }
            }

            var strTelef = controls.lblPhoneNumber.text(); // $('#hidTelef').val();
            var strCodId = AdditionalServices.HidCodId;// $('#hidCodID').val();
            var strCodSer = AdditionalServices.HidCodService; // $('#hidCodigoServicio').val();
           // var Puente = URL_GetBDBridge; // "../../../paginas/interaccion/SIACPO_ConsultaBD_Puente.aspx";
            var A_o_D = "D";
            var AccionPuente = 'ProgramarActiDesacti';

 

            var oBE = {};
            oBE.IdSession = AdditionalServices.IdSession;
            oBE.StrStatus = A_o_D;
            oBE.StrPhone = AdditionalServices.strlblPhoneNumber;
            oBE.StrCodId = AdditionalServices.HidCodId;
            oBE.StrCodSer = AdditionalServices.HidCodService;

            Loading();
            $.ajax({
                url: URL_ProgActDesact,
                data: JSON.stringify(oBE),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                //error: function (request, status, error) {
                //    //console.logerror);
                //    alert(error);
                //},
                success: function (response) {
                    var oBE = {};

                    oBE.IdSession = AdditionalServices.IdSession;
                    oBE.ContractId = AdditionalServices.ContractId;
                    oBE.HidContract = AdditionalServices.ContractId;
                    oBE.CustomerId = AdditionalServices.CustomerId

                    oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                    oBE.HidType = AdditionalServices.HidType;
                    oBE.HidClassDes = AdditionalServices.HidClassDes;
                    oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                    oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                    oBE.TxtNote = controls.txtNotes.val();
                    oBE.UserLogin = AdditionalServices.UserLogin;
                    oBE.txtDateAct = controls.txtDateAct.val();
                    oBE.txtDateDeact = controls.txtDateDeact.val();
                    oBE.HidTransaction = AdditionalServices.HidTransaction;
                    oBE.FirstName = AdditionalServices.FirstName;
                    oBE.LastName = AdditionalServices.LastName;
                    oBE.DniRuc = AdditionalServices.DniRuc;
                    oBE.PhoneReference = AdditionalServices.PhoneReference;
                    oBE.HidNameService = AdditionalServices.HidNameService;
                    oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                    oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                    oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;
                    oBE.HidAction = "1";
                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;




                    if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                        oBE.chkSendMail_IsCheched = "T";
                        oBE.txtEmail = controls.txtEmail.val();
                    }
                    else {
                        oBE.chkSendMail_IsCheched = "F";
                        oBE.txtEmail = "";
                    }
                    oBE.cboCACDACValue = controls.cboCACDAC.val();
                    oBE.Plan = AdditionalServices.lblPlanName;
                    if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                        oBE.chkProgram_IsChecked = "T";
                    }
                    else {
                        oBE.chkProgram_IsChecked = "F";
                    }

                    oBE.txtDateApp = controls.txtDateApp.val();

                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                    oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                    oBE.HidProgramingRoamming = controls.txtDateApp.val();


                    if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                        oBE.rdbDetermined_IsChecked = "T";
                    }
                    else {
                        oBE.rdbDetermined_IsChecked = "F";
                    }

                    if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                        oBE.rdbIndeterminate_IsChecked = "T";
                    }
                    else {
                        oBE.rdbIndeterminate_IsChecked = "F";
                    }
                    oBE.LegalAgent = AdditionalServices.LegalAgent;
                    oBE.HidCodId = AdditionalServices.HidCodId;
                    oBE.CustomerId = AdditionalServices.CustomerId;
                    oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                    oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                    oBE.Account = AdditionalServices.Account;
                    oBE.HidState = AdditionalServices.HidState;
                    oBE.HidBloqDes = AdditionalServices.HidBloqDes;
                    //oBE.HidContract = AdditionalServices.HidContract;
                    oBE.HidCodService = AdditionalServices.HidCodService;
                    oBE.HidStateContract = AdditionalServices.HidStateContract;
                    oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                    oBE.HidCodPackage = AdditionalServices.HidCodPackage;
                    oBE.HidCodExclusive = AdditionalServices.HidCodExclusive;
                    oBE.ContactCode = AdditionalServices.ContactCode;

                    if (response.MessageLabel.split('|').length > 1) {
                        if (response.MessageLabel.split('|')[0] == 1) {
                            Loading();

                            $.ajax({
                                url: URL_Desactive,
                                data: JSON.stringify(oBE),
                                type: 'POST',
                                contentType: "application/json charset=utf-8;",
                                dataType: "json",
                                //error: function (request, status, error) {
                                //    //console.logerror);
                                //    alert(error);
                                //},
                                success: function (response) {

                                    if (response.Message != "") {
                                        alert(response.Message,"Informativo");
                                    }
									if (response.Message != "") {
									    alert(response.MessageLabel,"Informativo");
                                    }
                                    if (response.HidCaseId != "") {
                                        AdditionalServices.HidCaseId = response.HidCaseId;
                                        LoadServicesContract(AdditionalServices.ContractId);
                                        AdditionalServices.HidAction = response.HidAction;
                                        AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                    }
                                    else {
                                        AdditionalServices.HidCaseId = "";
                                    }


                                }
                            });


                        }
                        else if (response.MessageLabel.split('|')[0] == "-1") {

                            confirm(response.MessageLabel.split('|')[1], 'Confirmar', function (result) {

                                if (result == true) {

                                    Loading();
                                    $.ajax({
                                        url: URL_Desactive,
                                        data: JSON.stringify(oBE),
                                        type: 'POST',
                                        contentType: "application/json charset=utf-8;",
                                        dataType: "json",
                                        //error: function (request, status, error) {
                                        //    //console.logerror);
                                        //    alert(error);
                                        //},
                                        success: function (response) {
                                            if (response.Message != "") {
                                                alert(response.Message,"Informativo");
                                            }
											if (response.Message != "") {
                                                alert(response.MessageLabel,"Informativo");
                                            }
                                            if (response.HidCaseId != "") {
                                                AdditionalServices.HidCaseId = response.HidCaseId;
                                                LoadServicesContract(AdditionalServices.ContractId);
                                                AdditionalServices.HidAction = response.HidAction;
                                                AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                            }
                                            else {
                                                AdditionalServices.HidCaseId = "";
                                            }

                                        }
                                    });

                                }
                                else {
                                    return false;
                                }
                            });


                        }
                        else {
                            alert(response.MessageLabel.split('|')[1],"Alerta");
                            return false;
                        }
                    }

                }
            });

             

             
        },
        f_MaintainActivation: function () {
            var that = this,
          controls = that.getControls();

            
            if ($.trim(AdditionalServices.HidCodService) == '' || AdditionalServices.HidCodService == null) {
                alert('Seleccione un Servicio.',"Alerta");
                return false;
            }

            if (AdditionalServices.HidState == Desactivo || AdditionalServices.HidState == '' || AdditionalServices.HidState == null || AdditionalServices.HidState == '&nbsp;') {
                alert('Usted No Puede Mantener Activo un Servicio Desactivado.', "Alerta");
                return false;
            }

            AdditionalServices.HidStatusDisabledApa = 'MA';
             
            var objCodServ = AdditionalServices.HidCodService; // $("#hidCodigoServicio").val();
            var CodServRoamming = AdditionalServices.HidCodServRoaming; // $("#hidCodServRoamming").val();
            var ddlTipoSol = controls.cboCareChannel.val();// $('#ddlTipoSolRoaming').val();
            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {
                if (parseInt(ddlTipoSol) == parseInt("-1")) { alert('Seleccione Canal de Atención.', "Alerta"); return false; }
                else {
                    var str = "";
                    $("#"+ controls.cboCareChannel.attr("id")+" option:selected").each(function () { str += $(this).text(); });
                    AdditionalServices.HidTypeRequest = str; // $("#hidTipoSolicitud").val(str);
                }
            }

 
            var A_o_D = "MA";
            var oBE = {};
            oBE.IdSession = AdditionalServices.IdSession;
            oBE.StrStatus = A_o_D;
            oBE.StrPhone = AdditionalServices.strlblPhoneNumber;
            oBE.StrCodId = AdditionalServices.HidCodId;
            oBE.StrCodSer = AdditionalServices.HidCodService;

            Loading();
            $.ajax({
                url: URL_ProgActDesact,
                data: JSON.stringify(oBE),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                //error: function (request, status, error) {
                //    //console.logerror);
                //    alert(error);
                //},
                success: function (response) {
                    var oBE = {};

                    oBE.IdSession = AdditionalServices.IdSession;
                    oBE.ContractId = AdditionalServices.ContractId;
                    oBE.HidContract = AdditionalServices.ContractId;
                    oBE.CustomerId = AdditionalServices.CustomerId

                    oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                    oBE.HidType = AdditionalServices.HidType;
                    oBE.HidClassDes = AdditionalServices.HidClassDes;
                    oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                    oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                    oBE.TxtNote = controls.txtNotes.val();
                    oBE.UserLogin = AdditionalServices.UserLogin;
                    oBE.txtDateAct = controls.txtDateAct.val();
                    oBE.txtDateDeact = controls.txtDateDeact.val();
                    oBE.HidTransaction = AdditionalServices.HidTransaction;
                    oBE.FirstName = AdditionalServices.FirstName;
                    oBE.LastName = AdditionalServices.LastName;
                    oBE.DniRuc = AdditionalServices.DniRuc;
                    oBE.PhoneReference = AdditionalServices.PhoneReference;
                    oBE.HidNameService = AdditionalServices.HidNameService;
                    oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                    oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                    oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;
                    oBE.HidAction = "1";
                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;


                    if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                        oBE.chkSendMail_IsCheched = "T";
                        oBE.txtEmail = controls.txtEmail.val();
                    }
                    else {
                        oBE.chkSendMail_IsCheched = "F";
                        oBE.txtEmail = "";
                    }
                    oBE.cboCACDACValue = controls.cboCACDAC.val();
                    oBE.Plan = AdditionalServices.lblPlanName;
                    if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                        oBE.chkProgram_IsChecked = "T";
                    }
                    else {
                        oBE.chkProgram_IsChecked = "F";
                    }

                    oBE.txtDateApp = controls.txtDateApp.val();

                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                    oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                    oBE.HidProgramingRoamming = controls.txtDateApp.val();


                    if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                        oBE.rdbDetermined_IsChecked = "T";
                    }
                    else {
                        oBE.rdbDetermined_IsChecked = "F";
                    }

                    if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                        oBE.rdbIndeterminate_IsChecked = "T";
                    }
                    else {
                        oBE.rdbIndeterminate_IsChecked = "F";
                    }
                    oBE.LegalAgent = AdditionalServices.LegalAgent;
                    oBE.HidCodId = AdditionalServices.HidCodId;
                    oBE.CustomerId = AdditionalServices.CustomerId;
                    oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                    oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                    oBE.Account = AdditionalServices.Account;
                    oBE.HidState = AdditionalServices.HidState;
                    oBE.HidBloqDes = AdditionalServices.HidBloqDes;
                    //oBE.HidContract = AdditionalServices.HidContract;
                    oBE.HidCodService = AdditionalServices.HidCodService;
                    oBE.HidStateContract = AdditionalServices.HidStateContract;
                    oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                    oBE.HidCodPackage = AdditionalServices.HidCodPackage;
                    oBE.HidCodExclusive = AdditionalServices.HidCodExclusive;
                    oBE.ContactCode = AdditionalServices.ContactCode;

                    if (response.MessageLabel.split('|').length > 1) {
                        if (response.MessageLabel.split('|')[0] == 1) {
                            Loading();

                            $.ajax({
                                url: URL_Active,
                                data: JSON.stringify(oBE),
                                type: 'POST',
                                contentType: "application/json charset=utf-8;",
                                dataType: "json",
                                //error: function (request, status, error) {
                                //    //console.logerror);
                                //    alert(error);
                                //},
                                success: function (response) {

                                    if (response.Message != "") {
                                        alert(response.Message,"Informativo");
                                    }
                                    if (response.Message != "") {
                                        alert(response.MessageLabel,"Informativo");
                                    }
                                    if (response.HidCaseId != "") {
                                        AdditionalServices.HidCaseId = response.HidCaseId;
                                        LoadServicesContract(AdditionalServices.ContractId);
                                        AdditionalServices.HidAction = response.HidAction;
                                        AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                    }
                                    else {
                                        AdditionalServices.HidCaseId = "";
                                    }


                                }
                            });


                        }
                        else if (response.MessageLabel.split('|')[0] == "-1") {

                            confirm(response.MessageLabel.split('|')[1], 'Confirmar', function (result) {

                                if (result == true) {

                                    Loading();
                                    $.ajax({
                                        url: URL_Active,
                                        data: JSON.stringify(oBE),
                                        type: 'POST',
                                        contentType: "application/json charset=utf-8;",
                                        dataType: "json",
                                        //error: function (request, status, error) {
                                        //    //console.logerror);
                                        //    alert(error);
                                        //},
                                        success: function (response) {
                                            if (response.Message != "") {
                                                alert(response.Message, "Informativo");
                                            }
                                            if (response.Message != "") {
                                                alert(response.MessageLabel, "Informativo");
                                            }
                                            if (response.HidCaseId != "") {
                                                AdditionalServices.HidCaseId = response.HidCaseId;
                                                LoadServicesContract(AdditionalServices.ContractId);
                                                AdditionalServices.HidAction = response.HidAction;
                                                AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                            }
                                            else {
                                                AdditionalServices.HidCaseId = "";
                                            }

                                        }
                                    });

                                }
                                else {
                                    return false;
                                }
                            });


                        }
                        else {
                            alert(response.MessageLabel.split('|')[1], "Informativo");
                            return false;
                        }
                    }

                }
            });

           
        },
        f_ValidateActive: function () {
            var that = this,
            controls = that.getControls();
            if (AdditionalServices.HidCodService == '' || AdditionalServices.HidCodService == null) {
                alert('Seleccione un Servicio.',"Alerta");
                return false;
            }
            var strCodSer = AdditionalServices.HidCodService; //$('#hidCodigoServicio').val();
            var strCodSer4g = AdditionalServices.HidCodServ4G; // document.getElementById('hidCodSer4g').value;
            if (strCodSer == strCodSer4g) {
                var strCadenaPerfil = AdditionalServices.HidAccess; // document.getElementById('hidAcceso').value;
                var strValidaPerfil = AdditionalServices.HidCodOptAuthorized;  //document.getElementById('hidCodOpcAutorizado').value;
                if (strCadenaPerfil.indexOf(strValidaPerfil) == -1) {

                    confirm('Se requiere autorización del Jefe/Supervisor.', 'Confirmar', function (result) {

                        if (result == true) {
                            var varOpcion = 'strConstkeyAct4G';
                            var URL = location.protocol + "//" + location.host + URL_SIACPO_Validate;
                            window.open(URL + '?pag=17&opcion=' + varOpcion, window, 'dialogTop:200;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:283px;dialogHeight:153px');

                        }
                        else {
                            return false;
                        }
                    });
                }
                else {
                   
                    that.f_Active();
                }
            }
            else {
                that.f_Active();
            }
        },
        f_ValidateInactive: function (Action) {
            var that = this,
           controls = that.getControls();
            if (AdditionalServices.HidCodService == '' || AdditionalServices.HidCodService == null) {
                alert('Seleccione un Servicio.', "Alerta");
                return false;
            }
            
            if (Action == 'D') {
                if (AdditionalServices.HidState == Desactivo || AdditionalServices.HidState == '' || AdditionalServices.HidState == null || AdditionalServices.HidState == '&nbsp;') {
                    alert('Usted No Puede Desactivar un Servicio ya Desactivo.', "Alerta");
                    return false;
                }
            }

            var strCodSer = AdditionalServices.HidCodService; // $('#hidCodigoServicio').val();
            var strCodSer4g = AdditionalServices.HidCodServ4G;// document.getElementById('hidCodSer4g').value;
            if (strCodSer == strCodSer4g) {
                var strCadenaPerfil = AdditionalServices.HidAccess; // document.getElementById('hidAcceso').value;
                var strValidaPerfil = AdditionalServices.HidCodOptAuthorized;// document.getElementById('hidCodOpcAutorizado').value;
                if (strCadenaPerfil.indexOf(strValidaPerfil) == -1) {

                    confirm('Se requiere autorización del Jefe/Supervisor', 'Confirmar', function (result) {

                        if (result == true) {
                            var varOpcion = 'strConstkeyAct4G';
                            var URL = location.protocol + "//" + location.host + URL_SIACPO_Validate;
                            window.open(URL + '?pag=1&opcion=' + varOpcion, window, 'dialogTop:200;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:283px;dialogHeight:153px');

                        }
                        else {
                            return false;
                        }
                    });
                }
                else {
                    that.f_Disabled('D');
                }
            }
            else {
                that.f_Disabled('D');
            }
        },
        f_Active: function () {
            var that = this,
            controls = that.getControls();

            if (AdditionalServices.HidCodService == '' || AdditionalServices.HidCodService == null) {
                alert('Seleccione un Servicio.', "Alerta");
                return false;
            }

            AdditionalServices.HidStatusDisabledApa = 'A';


            var objCodServ = AdditionalServices.HidCodService;// $("#hidCodigoServicio").val();
            var CodServRoamming = AdditionalServices.HidCodServRoaming;// $("#hidCodServRoamming").val();
            var ddlTipoSol = controls.cboCareChannel.val();// $('#ddlTipoSolRoaming').val();
            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {

                if (AdditionalServices.HidState == '' || AdditionalServices.HidState == '&nbsp;' || AdditionalServices.HidState == null) {
                    AdditionalServices.HidState = Desactivo;
                }
                 

                if (parseInt(ddlTipoSol) == parseInt("-1")) { alert('Seleccione Canal de Atención.', "Alerta"); return false; }
                else {
                    var str = "";
                    $("#" + controls.cboCareChannel.attr("id") + " option:selected").each(function () { str += $(this).text(); });
                    AdditionalServices.HidTypeRequest = str;
                }
            }

            var oBE = {};
            oBE.IdSession = AdditionalServices.IdSession;
            oBE.StrStatus = "A";
            oBE.StrPhone = AdditionalServices.strlblPhoneNumber;
            oBE.StrCodId = AdditionalServices.HidCodId;
            oBE.StrCodSer = AdditionalServices.HidCodService;

            Loading();
            $.ajax({
                url: URL_ProgActDesact,
                data: JSON.stringify(oBE),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                //error: function (request, status, error) {
                //    //console.logerror);
                //    alert(error);
                //},
                success: function (response) {
                    var oBE = {};
                    
                    oBE.IdSession = AdditionalServices.IdSession;
                    oBE.ContractId = AdditionalServices.ContractId;
                    oBE.HidContract = AdditionalServices.ContractId;
                    oBE.CustomerId = AdditionalServices.CustomerId
                   
                    oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                    oBE.HidType = AdditionalServices.HidType;
                    oBE.HidClassDes = AdditionalServices.HidClassDes;
                    oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                    oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                    oBE.TxtNote = controls.txtNotes.val();
                    oBE.UserLogin = AdditionalServices.UserLogin;
                    oBE.txtDateAct = controls.txtDateAct.val();
                    oBE.txtDateDeact = controls.txtDateDeact.val();
                    oBE.HidTransaction = AdditionalServices.HidTransaction;
                    oBE.FirstName = AdditionalServices.FirstName;
                    oBE.LastName = AdditionalServices.LastName;
                    oBE.DniRuc = AdditionalServices.DniRuc;
                    oBE.PhoneReference = AdditionalServices.PhoneReference;
                    oBE.HidNameService = AdditionalServices.HidNameService;
                    oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                    oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                    oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;
                    oBE.HidAction = "1";
                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                 

                  

                    if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                        oBE.chkSendMail_IsCheched = "T";
                        oBE.txtEmail = controls.txtEmail.val();
                    }
                    else {
                        oBE.chkSendMail_IsCheched = "F";
                        oBE.txtEmail = "";
                    }
                    oBE.cboCACDACValue = controls.cboCACDAC.val();
                    oBE.Plan = AdditionalServices.lblPlanName;
                    if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                        oBE.chkProgram_IsChecked = "T";
                    }
                    else {
                        oBE.chkProgram_IsChecked = "F";
                    }

                    oBE.txtDateApp = controls.txtDateApp.val();

                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                    oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                    oBE.HidProgramingRoamming = controls.txtDateApp.val();
                    

                    if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                        oBE.rdbDetermined_IsChecked = "T";
                    }
                    else {
                        oBE.rdbDetermined_IsChecked = "F";
                    }

                    if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                        oBE.rdbIndeterminate_IsChecked = "T";
                    }
                    else {
                        oBE.rdbIndeterminate_IsChecked = "F";
                    }
                    oBE.LegalAgent = AdditionalServices.LegalAgent;
                    oBE.HidCodId = AdditionalServices.HidCodId;
                    oBE.CustomerId = AdditionalServices.CustomerId;
                    oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                    oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                    oBE.Account = AdditionalServices.Account;
                    oBE.HidState = AdditionalServices.HidState;
                    oBE.HidBloqDes = AdditionalServices.HidBloqDes;
                    //oBE.HidContract = AdditionalServices.HidContract;
                    oBE.HidCodService = AdditionalServices.HidCodService;
                    oBE.HidStateContract = AdditionalServices.HidStateContract;
                    oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                    oBE.HidCodPackage = AdditionalServices.HidCodPackage;
                    oBE.HidCodExclusive = AdditionalServices.HidCodExclusive;
                    oBE.ContactCode = AdditionalServices.ContactCode;
                     
                    if (response.MessageLabel.split('|').length > 1) {
                        if (response.MessageLabel.split('|')[0] == 1) {
                            Loading();
                           
                            $.ajax({
                                url: URL_Active,
                                data: JSON.stringify(oBE),
                                type: 'POST',
                                contentType: "application/json charset=utf-8;",
                                dataType: "json",
                                //error: function (request, status, error) {
                                //    //console.logerror);
                                //    alert(error);
                                //},
                                success: function (response) {

                                    if (response.Message != "") {
                                        alert(response.Message, "Informativo");
                                    }
                                    if (response.Message != "") {
                                        alert(response.MessageLabel, "Informativo");
                                    }
                                    if (response.HidCaseId != "") {
                                        AdditionalServices.HidCaseId = response.HidCaseId;
                                        LoadServicesContract(AdditionalServices.ContractId);
                                        AdditionalServices.HidAction = response.HidAction;
                                        AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                    }
                                    else {
                                        AdditionalServices.HidCaseId = "";
                                    }
                                  
                                   
                                }
                            });


                        }
                        else if (response.MessageLabel.split('|')[0] == "-1") {

                            confirm(response.MessageLabel.split('|')[1], 'Confirmar', function (result) {

                               if (result == true) {

                                   Loading();
                                    $.ajax({
                                        url: URL_Active,
                                        data: JSON.stringify(oBE),
                                        type: 'POST',
                                        contentType: "application/json charset=utf-8;",
                                        dataType: "json",
                                        //error: function (request, status, error) {
                                        //    //console.logerror);
                                        //    alert(error);
                                        //},
                                        success: function (response) {
                                            if (response.Message != "") {
                                                alert(response.Message, "Informativo");
                                            }
                                            if (response.Message != "") {
                                                alert(response.MessageLabel, "Informativo");
                                            }
                                            if (response.HidCaseId != "") {
                                                AdditionalServices.HidCaseId = response.HidCaseId;
                                                LoadServicesContract(AdditionalServices.ContractId);
                                                AdditionalServices.HidAction = response.HidAction;
                                                AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                            }
                                            else {
                                                AdditionalServices.HidCaseId = "";
                                            }

                                        }
                                    });

                                }
                                else {
                                    return false;
                                }
                            });

                            
                        }
                        else {
                            alert(response.MessageLabel.split('|')[1], "Alerta");
                            return false;
                        }
                    }

                }
            });

        },
        f_DateInitial: function () {
            var that = this,
           controls = that.getControls();

            var fechainicio = "";
            if ($.trim(controls.txtDateAct.val()) == "") {
                alert("Por favor agregue la fecha Activación.","Alerta");
            }
            else {
                var fechaini = controls.txtDateAct.val(); 
                var dia = fechaini.substring(0, 2);
                var mes = fechaini.substring(3, 5);
                var anio = fechaini.substring(6, 10);
                fechainicio = new Date(anio, mes, dia, 0, 0, 0, 0);
            }
            return fechainicio;
        },
        f_DateEnd: function () {
            var that = this,
            controls = that.getControls();

            var fechafin = "";

            if ($.trim(controls.txtDateDeact.val()) != "") {
                var fecha = controls.txtDateDeact.val();
                var dias = fecha.substring(0, 2);
                var meses = fecha.substring(3, 5);
                var anios = fecha.substring(6, 10);
                var fechafin = new Date(anios, meses, dias, 0, 0, 0, 0);
            }
            return fechafin;
        },
        f_ValidateDateRoamming: function () {
            var that = this,
            controls = that.getControls();

            var Determinado = controls.rdbDetermined.is(':checked'); // $('#radBtnDeterminado').is(':checked');
            var Indeterminado = controls.rdbIndeterminate.is(':checked'); //$('#radBtnIndeterminado').is(':checked');
            var d = new Date();
            var FechadeHoy = new Date(d.getFullYear(), (d.getMonth() + 1), d.getDate(), 0, 0, 0, 0);
            var ActivoDesactivo = AdditionalServices.HidState;// $("#hidEstado").val();
            var salida = "";
            var A_o_D = "";
            var fechafin = "";
          
            var FechaMin = "";
            var FechaMax = "";
            var fechainicio = "";
            var diaMilisegundos = 60 * 60 * 24 * 1000;
            var objCodServ = AdditionalServices.HidCodService; // $("#hidCodigoServicio").val();
            var CodServRoamming = AdditionalServices.HidCodServRoaming;// $("#hidCodServRoamming").val();
            var ddlTipoSol = controls.cboCareChannel.val();// $('#ddlTipoSolRoaming').val();
             
            if (parseInt(objCodServ) == parseInt(CodServRoamming)) {
                if (parseInt(ddlTipoSol) == parseInt("-1")) { alert('Seleccione Canal de Atención',"Alerta"); return false; }
                else {
                    var str = "";
                    $("#" + controls.cboCareChannel.attr("id") + " option:selected").each(function () { str += $(this).text(); });
                    AdditionalServices.HidTypeRequest = str;
                }
            }


            if ((Determinado == false) && (Indeterminado == false)) {
                alert("Por favor determine la Programación Determinado o Interminado.","Alerta");
            }

            if (($.trim(AdditionalServices.HidMinDateDeactivation) != "") || ($.trim(AdditionalServices.HidMaxDateDeactivacion) != "")) {
                FechaMin = AdditionalServices.HidMinDateDeactivation;
                FechaMax = AdditionalServices.HidMaxDateDeactivacion;
            }


            if ((ActivoDesactivo == Activo) && (Determinado == true)) {
                fechafin =   that.f_DateInitial();
                if (fechafin != "") {
                    if (fechafin < FechadeHoy) { alert("La fechas de Desactivación no puede ser menor a la fecha de Hoy.","Alerta"); return false; }
                } else {
                    alert('Por favor ingrese una fecha para la Desactivación.',"Alerta");
                    return;
                }
                salida = "AD";
                A_o_D = "D";
            }

            if ((ActivoDesactivo == Desactivo) && (Indeterminado === true)) {
                fechainicio =   that.f_DateInitial();
                if (fechainicio != "") {
                    if (fechainicio < FechadeHoy) { alert("La fechas de Activación no puede ser menor a la fecha de Hoy.","Alerta"); return false; }
                }
                salida = "AI";
                A_o_D = "A";
            }

            if ((ActivoDesactivo == Desactivo) && (Determinado === true)) {
                fechainicio =   that.f_DateInitial();
                fechafin = that.f_DateEnd();
                if (fechafin != "") {
                    if ((fechafin < FechadeHoy) || (fechainicio < FechadeHoy)) { alert("La fechas de Activación o Desactivación no puede ser menor a la fecha de Hoy.","Alerta"); return false; }

                    if (fechainicio > fechafin) {
                        alert("La fecha de Desactivación no puede ser menor a la de Activación.","Alerta"); return false;
                    }
                    else {
                        var diff = Math.abs(fechainicio - fechafin);
                        var diasDiferencia = Math.round(diff / diaMilisegundos)
                        if (parseInt(diasDiferencia) > parseInt(FechaMax)) {
                            alert("La Fecha Desactivación no puede ser mayor de " + FechaMax + " dias respecto a la Fecha Activación.", "Alerta"); return false;
                        }
                        if (parseInt(diasDiferencia) < parseInt(FechaMin)) {
                            alert("La Fecha Desactivación no puede ser menor de " + FechaMin + " dias respecto a la Fecha Activación.", "Alerta"); return false;
                        }

                        salida = "DD"; A_o_D = "A";
                    }
                }
                else {
                    alert("Por favor ingrese la fecha Desactivación.","Alerta");
                }
            }
           
            var oBE = {};
            oBE.IdSession = AdditionalServices.IdSession;
            oBE.StrStatus = A_o_D;
            oBE.StrPhone = AdditionalServices.strlblPhoneNumber;
            oBE.StrCodId = AdditionalServices.HidCodId;
            oBE.StrCodSer = AdditionalServices.HidCodService;

            Loading();
            $.ajax({
                url: URL_ProgActDesact,
                data: JSON.stringify(oBE),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                //error: function (request, status, error) {
                //    //console.logerror);
                //    alert(error);
                //},
                success: function (response) {
                    var oBE = {};

                    oBE.IdSession = AdditionalServices.IdSession;
                    oBE.ContractId = AdditionalServices.ContractId;
                    oBE.HidContract = AdditionalServices.ContractId;
                    oBE.CustomerId = AdditionalServices.CustomerId

                    oBE.lblPhoneNumber = AdditionalServices.lblPhoneNumber;
                    oBE.HidType = AdditionalServices.HidType;
                    oBE.HidClassDes = AdditionalServices.HidClassDes;
                    oBE.HidSubClassDes = AdditionalServices.HidSubClassDes;
                    oBE.HidSubClassId = AdditionalServices.HidSubClassId;
                    oBE.TxtNote = controls.txtNotes.val();
                    oBE.UserLogin = AdditionalServices.UserLogin;
                    oBE.txtDateAct = controls.txtDateAct.val();
                    oBE.txtDateDeact = controls.txtDateDeact.val();
                    oBE.HidTransaction = AdditionalServices.HidTransaction;
                    oBE.FirstName = AdditionalServices.FirstName;
                    oBE.LastName = AdditionalServices.LastName;
                    oBE.DniRuc = AdditionalServices.DniRuc;
                    oBE.PhoneReference = AdditionalServices.PhoneReference;
                    oBE.HidNameService = AdditionalServices.HidNameService;
                    oBE.HidFixedCharge = AdditionalServices.HidFixedCharge;
                    oBE.HidFixedChargeM = AdditionalServices.HidFixedChargeM;
                    oBE.HidNumberPeriod = AdditionalServices.HidNumberPeriod;
                   
                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;

                    if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                        oBE.chkSendMail_IsCheched = "T";
                        oBE.txtEmail = controls.txtEmail.val();
                    }
                    else {
                        oBE.chkSendMail_IsCheched = "F";
                        oBE.txtEmail = "";
                    }
                    oBE.cboCACDACValue = controls.cboCACDAC.val();
                    oBE.Plan = AdditionalServices.lblPlanName;
                    if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                        oBE.chkProgram_IsChecked = "T";
                    }
                    else {
                        oBE.chkProgram_IsChecked = "F";
                    }

                    oBE.txtDateApp = controls.txtDateApp.val();

                    oBE.HidDateFrom = AdditionalServices.HidDateFrom;
                    oBE.HidTypeRequest = AdditionalServices.HidTypeRequest;
                    oBE.HidProgramingRoamming = controls.txtDateApp.val();


                    if (document.getElementById(controls.rdbDetermined.attr("id")).checked == true) {
                        oBE.rdbDetermined_IsChecked = "T";
                    }
                    else {
                        oBE.rdbDetermined_IsChecked = "F";
                    }

                    if (document.getElementById(controls.rdbIndeterminate.attr("id")).checked == true) {
                        oBE.rdbIndeterminate_IsChecked = "T";
                    }
                    else {
                        oBE.rdbIndeterminate_IsChecked = "F";
                    }
                    oBE.LegalAgent = AdditionalServices.LegalAgent;
                    oBE.HidCodId = AdditionalServices.HidCodId;
                    oBE.CustomerId = AdditionalServices.CustomerId;
                    oBE.HidDiffCFixedTotWithCFixed = AdditionalServices.HidDiffCFixedTotWithCFixed;
                    oBE.lblCycleFact = AdditionalServices.lblCycleFact; //Falta
                    oBE.Account = AdditionalServices.Account;
                    oBE.HidState = AdditionalServices.HidState;
                    oBE.HidBloqDes = AdditionalServices.HidBloqDes;
                    //oBE.HidContract = AdditionalServices.HidContract;
                    oBE.HidCodService = AdditionalServices.HidCodService;
                    oBE.HidStateContract = AdditionalServices.HidStateContract;
                    oBE.HidBloqAct = AdditionalServices.HidBloqAct;
                    oBE.HidCodPackage = AdditionalServices.HidCodPackage;
                    oBE.HidCodExclusive = AdditionalServices.HidCodExclusive;
                    oBE.ContactCode = AdditionalServices.ContactCode;
                    oBE.HidEstGraMCP = "";
                    if (response.MessageLabel.split('|').length > 1) {
                        if (response.MessageLabel.split('|')[0] == 1) {
                            Loading();

                            $.ajax({
                                url: URL_PostBack,
                                data: JSON.stringify(oBE),
                                type: 'POST',
                                contentType: "application/json charset=utf-8;",
                                dataType: "json",
                                //error: function (request, status, error) {
                                //    //console.logerror);
                                //    alert(error);
                                //},
                                success: function (response) {

                                    if (response.Message != "") {
                                        alert(response.Message,"Informativo");
                                    }
                                    if (response.Message != "") {
                                        alert(response.MessageLabel,"Informativo");
                                    }
                                    if (response.HidCaseId != "") {
                                        AdditionalServices.HidCaseId = response.HidCaseId;
                                        LoadServicesContract(AdditionalServices.ContractId);
                                        AdditionalServices.HidAction = response.HidAction;
                                        AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                    }
                                    else {
                                        AdditionalServices.HidCaseId = "";
                                    }


                                }
                            });


                        }
                        else if (response.MessageLabel.split('|')[0] == "-1") {

                            confirm(response.MessageLabel.split('|')[1], 'Confirmar', function (result) {

                                if (result == true) {

                                    Loading();
                                    $.ajax({
                                        url: URL_Active,
                                        data: JSON.stringify(oBE),
                                        type: 'POST',
                                        contentType: "application/json charset=utf-8;",
                                        dataType: "json",
                                        //error: function (request, status, error) {
                                        //    //console.logerror);
                                        //    alert(error);
                                        //},
                                        success: function (response) {
                                            if (response.Message != "") {
                                                alert(response.Message,"Informativo");
                                            }
                                            if (response.Message != "") {
                                                alert(response.MessageLabel,"Informativo");
                                            }
                                            if (response.HidCaseId != "") {
                                                AdditionalServices.HidCaseId = response.HidCaseId;
                                                LoadServicesContract(AdditionalServices.ContractId);
                                                AdditionalServices.HidAction = response.HidAction;
                                                AdditionalServices.HidRoutePdf = response.HidRoutePdf;
                                            }
                                            else {
                                                AdditionalServices.HidCaseId = "";
                                            }

                                        }
                                    });

                                }
                                else {
                                    return false;
                                }
                            });


                        }
                        else {
                            alert(response.MessageLabel.split('|')[1],"Alerta");
                            return false;
                        }
                    }

                }
            });
        },

        
        f_ReviewGrid: function (sender, arg) {
            var that = this,
             controls = that.getControls();
            if (controls.chkModifyQuota.prop('checked')) {
                
                var CantRbSel = $("input:radio[name*='rdbServicebyContract']:checked").length;
                var estado = $('[name="chkModifyQuota"]').is(':checked');
                
                if (estado == true) {
                    
                    var estError = "OK";
                    if (AdditionalServices.HidStateUserBSCS == estError) {
                        alert('No puede realizar la transacción debido a que no se encuentra registrado en BSCS.', "Alerta");
                        return false;
                    }
                    if (CantRbSel == false) {
                        alert('Debe seleccionar un servicio.', "Alerta");
                        return false;
                    }


                    $('#tblServicesAdd tr').each(function () {
                        var state = $(this).find("[name*='rdbServicebyContract']").is(':checked');
                        var estConfig = "Activo";
                        if (state == true && AdditionalServices.HidStateService == estConfig) {
                            
                            $(this).find("[name*='txtQuot']").attr("disabled", false);
                            $(this).find("[name*='txtQuot']").focus();
                            $(this).find("[name*='txtPeriod']").attr("disabled", false);

                            // AdditionalServices.HidFixedCharge = $(this).find("td").eq(6).text();
                            var cuota = $(this).find("[name*='txtQuot']").val();
                            if (cuota != '') {
                                AdditionalServices.HidQuotAnt = cuota;
                            }
                            else {
                                AdditionalServices.HidQuotAnt = "";
                            }

                            var Control1 = $(this).find("[name*='txtQuot']").attr("name");
                            AdditionalServices.HidQuotAntControl = Control1;
                            var perio = $(this).find("[name*='txtPeriod']").val();
                            if (perio != '') {
                                AdditionalServices.HidPeriodAnt = perio;
                            }
                            else {
                                AdditionalServices.HidPeriodAnt = "";
                            }

                            var Control2 = $(this).find("[name*='txtPeriod']").attr("name");
                            AdditionalServices.HidPeriodAntControl = Control2;

                            controls.btnModifyQuota.prop("disabled", false);
                            controls.chkProgram.prop("disabled", true);
                            controls.btnActive.prop("disabled", true);
                            controls.btnDesactive.prop("disabled", true);

                        }
                    });



                }
                else {

                    controls.btnModifyQuota.prop("disabled", true);
                    controls.chkProgram.prop("disabled", false);
                    controls.btnActive.prop("disabled", false);
                    controls.btnDesactive.prop("disabled", false);


                    var control1 = AdditionalServices.HidQuotAntControl; //$('#hidCuotaAntControl').val();
                    var control2 = AdditionalServices.HidPeriodAntControl; //$('#hidPerioAntControl').val();
                    if (control1 != "") {
                        $('#' + control1 + '').attr("disabled", true);
                        $('#' + control2 + '').attr("disabled", true);

                        $('#' + control1 + '').val(AdditionalServices.HidQuotAnt);
                        $('#' + control2 + '').val(AdditionalServices.HidPeriodAnt);

                        AdditionalServices.HidQuotAnt = '';
                        AdditionalServices.HidPeriodAnt = '';
                        AdditionalServices.HidQuotAntControl = ''; //$('#hidCuotaAntControl').val();
                        AdditionalServices.HidPeriodAntControl = ''; //$('#hidPerioAntControl').val();
                    }

                }
            }
            return true;
        },
        f_SelecRdb: function (Estado, BloqueoAct) {
            var that = this,
            controls = that.getControls();
            AdditionalServices.HidStateService = Estado;
            AdditionalServices.HidBloqAct = BloqueoAct;
            

            //var num = $(InputRadio).attr("id").indexOf('tblServicesAdd');
            //var cadena = $(InputRadio).attr("id").substring(0, num + 9);

            AdditionalServices.HidNameObj = "tblServicesAdd";
            controls.chkModifyQuota.prop('checked', false);

             

            var control1 = AdditionalServices.HidQuotAntControl; //$('#hidCuotaAntControl').val();
            var control2 = AdditionalServices.HidPeriodAntControl; //$('#hidPerioAntControl').val();
            

            if (control1 != "") {
                $("[name*=" + control1 + "]").attr("disabled", true);
                $(this).find("[name*='txtQuot']").focus();
                $("[name*=" + control2 + "]").attr("disabled", true);
                //$(this).find("[name*='txtQuot']").attr("disabled", true);
                //$(this).find("[name*='txtPeriod']").attr("disabled", true);


                $('#' + control1 + '').val(AdditionalServices.HidQuotAnt);
                $('#' + control2 + '').val(AdditionalServices.HidPeriodAnt);

                AdditionalServices.HidQuotAnt = '';
                AdditionalServices.HidPeriodAnt = '';
                AdditionalServices.HidQuotAntControl = ''; //$('#hidCuotaAntControl').val();
                AdditionalServices.HidPeriodAntControl = ''; //$('#hidPerioAntControl').val();
            }

            var estConfig = "Activo";
            if (AdditionalServices.HidStateService == estConfig) {
                controls.chkModifyQuota.prop("disabled", false);
            }
            else {
                controls.chkModifyQuota.prop("disabled", true);
            }

            controls.btnModifyQuota.prop("disabled",true);

        },
        f_ValidateLogin: function (spOpcion) {
            var varOpcion = spOpcion;
            var strEnvioLog = AdditionalServices.strEnvioLog;
            window.open(URL_SIACPO_Validate + '?pag=' + strEnvioLog + '&opcion=' + varOpcion, window, 'dialogTop:200;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:283px;dialogHeight:153px');
        },
        f_PermissionsResponseFC: function(sStatePermissions, sOption) {
       //     HidenBloq();
            if (sEstadoPermiso == AdditionalServices.strEstOk){
                //Hiden2();
                //document.getElementById('hidEstGraMCP').value = 'GCP';
                //document.Form1.submit();
                var oBE = {};
                $.ajax({
                    url: URL_Save,
                    data: JSON.stringify(oBE),
                    type: 'POST',
                    contentType: "application/json charset=utf-8;",
                    dataType: "json",
                    //error: function (request, status, error) {
                    //    //console.logerror);
                    //    alert(error);
                    //},
                    success: function (response) {
                         //Falta
                    }
                });

            }
            else if (sEstadoPermiso ==AdditionalServices.strEstCancel){
              //  Hiden2();
            }	

        },
        f_WindowsConstancyMCP: function () {
           var that = this,
           controls = that.getControls();

           controls.chkSendMail.prop('checked', false);
           controls.txtNotes.val("");
           controls.chkModifyQuota.prop('checked', false);
             
           var strTran = AdditionalServices.HidTransaction;
           var strCodigoInter = AdditionalServices.HidCaseId; // $('#hidCasoId').val();
           window.showModalDialog(URL_AdditionalServicesConstModCP + '?Transaccion=' + strTran + '&CasoInteraccionId=' + strCodigoInter, window, 'dialogTop:100;status:no;edge:sunken;dialogHide:true;help:no;dialogWidth:920px;dialogHeight:400px');

        },
        createDropdownCACDAC: function (response) {
            var that = this,
                controls = that.getControls();

            controls.cboCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.CacDacTypes, function (index, value) {
                    controls.cboCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }


        },
        windowAutoSize: function () {
            var hsize = Math.max(
                    document.documentElement.clientHeight,
                    document.body.scrollHeight,
                    document.documentElement.scrollHeight,
                    document.body.offsetHeight,
                    document.documentElement.offsetHeight
                );
            hsize = hsize - 72;
            $('#content').css({ 'height': hsize + 'px' });
        },
        //Loading: function () {
        //    var that = this,
        //     controls = that.getControls();
        //    $.blockUI({
        //        message: controls.ModalLoading,
        //        css: {
        //            border: 'none',
        //            padding: '15px',
        //            backgroundColor: '#000000',
        //            '-webkit-border-radius': '50px',
        //            '-moz-border-radius': '50px',
        //            opacity: .7,
        //            color: '#fff'
        //        }
        //    });
        //},
        
       
        
           
     
        

        strUrl: window.location.protocol + '//' + window.location.host + '/Transactions'
    }

    $.fn.PostPaidAdditionalServices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPaidAdditionalServices'),
                options = $.extend({}, $.fn.PostPaidAdditionalServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPaidAdditionalServices', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.PostPaidAdditionalServices.defaults = {
    };

    $('#divBody').PostPaidAdditionalServices();
})(jQuery);
    