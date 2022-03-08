
// var SessionCommercialServices = {};
var hdnPermisoExp;
var hdnPermisoBus;
var sessionTransacHFC = {};
var userValidatorAuth = "";
var modelTempSave;
var routeConstancy = "";
var mensajeActivacionDesactivacion = "";

var HiddenPageHtml = {};

sessionTransacHFC = JSON.parse(sessionStorage.getItem("SessionTransac"));
//if (sessionTransacHFC.UrlParams.IDSESSION == null || sessionTransacHFC.UrlParams.IDSESSION == undefined) {
//    sessionTransacHFC.UrlParams.IDSESSION = '0';
//}

//console.log(sessionTransacHFC);


var AdditionalServicesModel = {};
AdditionalServicesModel.bErrorTransac = false;
AdditionalServicesModel.strMessageErrorTransac = "";

AdditionalServicesModel.strMessageEnterMail = "";
AdditionalServicesModel.strMessageValiateMail = "";

AdditionalServicesModel.strContractId = "";
AdditionalServicesModel.strCustomerId = "";
AdditionalServicesModel.strAmount = "";
AdditionalServicesModel.strDateProgramation = "";
AdditionalServicesModel.strBillingCycle = "";
AdditionalServicesModel.strPhoneInteraction = "";
AdditionalServicesModel.strTxtNote = "";
AdditionalServicesModel.strCodPlaneInst = "";
AdditionalServicesModel.strFirstName = "";
AdditionalServicesModel.strLastName = "";
AdditionalServicesModel.strNumberDocument = "";
AdditionalServicesModel.strReferencePhone = "";
AdditionalServicesModel.strReasonSocial = "";
AdditionalServicesModel.strContactClient = "";

AdditionalServicesModel.strPlan = "";
AdditionalServicesModel.strCacDacDescription = "";
AdditionalServicesModel.strFlagChkSendEmail = "";
AdditionalServicesModel.strTxtSendEmail = "";
AdditionalServicesModel.strFlagChkProgramming = "";
AdditionalServicesModel.strPhone = "";
AdditionalServicesModel.strTransaction = "";
AdditionalServicesModel.strNameClient = "";
AdditionalServicesModel.strAccountUser = "";
AdditionalServicesModel.strLegalRepresent = "";
AdditionalServicesModel.strFullNameCustomer = "";
AdditionalServicesModel.gstrTransaccionDTHTACTDESSER = "";

AdditionalServicesModel.strHdnReady = "";
AdditionalServicesModel.strHdnTipoTransaccion = "";
AdditionalServicesModel.strHdnTipiService = "";

AdditionalServicesModel.strHdnValuePVUMatch = "";
AdditionalServicesModel.strHdnDesCoSerSel = "";
AdditionalServicesModel.strHdnCostoPVUSel = "";
AdditionalServicesModel.strHdnCostoBSCS = "";
AdditionalServicesModel.strHdnCargoFijoSel = "";
AdditionalServicesModel.strHdnFechaProg = "";
AdditionalServicesModel.strHdnSNCodeSel = "";
AdditionalServicesModel.strHdnSPCodeSel = "";
AdditionalServicesModel.strHdnCoSerSel = "";
AdditionalServicesModel.strHdnSelectedIndex = "";


AdditionalServicesModel.strHdnClassCode = "";
AdditionalServicesModel.strHdnSubClassCode = "";
AdditionalServicesModel.strHdnSubClass = "";
AdditionalServicesModel.strHdnType = "";
AdditionalServicesModel.strHdnClass = "";


AdditionalServicesModel.strHdnCaseId = "";


AdditionalServicesModel.hdnValorIGV = "";

AdditionalServicesModel.HdnList = "0";

var URL_AdditionalServiceSaveTransaction = "/Transactions/HFC/AdditionalServices/SaveTransaction";
var URL_PageLoad_HFC = "/Transactions/HFC/AdditionalServices/PageLoad_HFC";
var URL_LoadTypification_HFC = "/Transactions/HFC/AdditionalServices/LoadTypification_HFC";
var URL_HfcGetCommercialSercices = "/Transactions/HFC/AdditionalServices/HfcGetCommercialSercices";
var URL_LogTotalFixedCharge = "/Transactions/HFC/AdditionalServices/LogTotalFixedCharge";
var URL_GetCamapaign = "/Transactions/HFC/AdditionalServices/GetCamapaign";


(function ($) {
    
    var Form = function ($element, options) {
        $.extend(this, $.fn.ActiveDesactiveServiceHFC.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,

            //DIVS
            divContentProgramDate           : $('#divContentProgramDate', $element),

            //LABELS
            lblCustomerId                   : $('#lblCustomerId', $element),
            lblContactName                  : $('#lblContactName', $element),
            lblCustomerName                 : $('#lblCustomerName', $element),
            lblPlanName                     : $('#lblPlanName', $element),
            lblCycleFacture                 : $('#lblCycleFacture', $element),
            lblDateActivation               : $('#lblDateActivation', $element),
            lblLteContract                  : $('#lblLteContract', $element),
            lblHeaderTypeCustomer           : $('#lblHeaderTypeCustomer', $element),
            lblDocument                     : $('#lblDocument', $element),
            lblLimitCredit                  : $('#lblLimitCredit', $element),
            lblRepreLegal                   : $('#lblRepreLegal', $element),
            lblDocRepresLegal               : $('#lblDocRepresLegal', $element),
             
            //TEXTS
            txtEmail                        : $('#txtEmail', $element),
            txtNotesTask                    : $('#txtNotesTask', $element),
            txtProgramDate                  : $('#txtProgramDate', $element),
            txtDireccion                    : $('#txtDireccion', $element),
            txtNotaDireccion                : $('#txtNotaDireccion', $element),
            txtDepartamento                 : $('#txtDepartamento', $element),
            txtPais                         : $('#txtPais', $element),
            txtDistrito                     : $('#txtDistrito', $element),
            txtProvincia                    : $('#txtProvincia', $element),
            txtCodigoPlano                  : $('#txtCodigoPlano', $element),
            txtCodigoUbigeo                 : $('#txtCodigoUbigeo', $element),

            //CHECKBOX
            //chkModificarCouta: $('#chkModificarCouta', $element),
            chkSendMail                     : $('#chkSendMail', $element),
            chkProgram                      : $('#chkProgram', $element),
            chkCampiagn                     : $('#chkCampiagn', $element),

            //SELECTS
            cboCACDAC                       : $('#cboCACDAC', $element),
            cboCamapaign                    : $("#cboCamapaign", $element),

            //BUTTONS
            btnModificarCuota               : $('#btnModificarCuota', $element),
            btnCheckDevices                 : $('#btnCheckDevices', $element),
            btnProgramTask                  : $('#btnProgramTask', $element),
            //btnProActive                    : $('#btnProActive', $element),
            //btnProDesactive                 : $('#btnProDesactive', $element),
            btnActive                       : $('#btnActive', $element),
            btnDesactive                    : $('#btnDesactive', $element),
            btnConstancy                    : $('#btnConstancy', $element),
            btnClose                        : $('#btnClose', $element),
            btnNextForm                     : $('#btnNextForm', $element),
            btnNexResumen                   : $('#btnNexResumen', $element),
            //MODAL LOAD
            myModalLoad                     : $('#myModalLoad', $element),

            //Table           
            lblCustomerIds                  : $('#lblCustomerIds', $element),
            lblContract                     : $('#lblContractPage', $element),
            lblName                         : $('#lblName', $element),
            lblState                        : $('#lblState', $element),
            lblDate                         : $('#lblDate', $element),
            lblMotive                       : $('#lblMotive', $element),

            divErrorAlertTable              : $('#divErrorAlertTable', $element),

            tblServBody                     : $("#tblServBody", $element),

            spnMainTitle                    : $('#spnMainTitle'),
            divRules                        : $('#divRules', $element)
            // rbServices                      : $("body .rbServices",$element)

        });
    };

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.txtProgramDate.datepicker({ format: 'dd/mm/yyyy', startDate: '+0d' });
            //Events Buttons
            controls.btnCheckDevices.addEvent(that, 'click', that.btnCheckDevices_click);
            controls.btnProgramTask.addEvent(that, 'click', that.btnProgramTask_click);
            controls.btnActive.addEvent(that, 'click', that.btnActive_click);
            controls.btnDesactive.addEvent(that, 'click', that.btnDesactive_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.chkSendMail.addEvent(that, 'change', that.chkSendMail_Change);
            controls.chkProgram.addEvent(that, 'change', that.chkProgram_Change);
            controls.chkCampiagn.addEvent(that, 'change', that.chkCampiagn_Change);
            controls.btnNextForm.addEvent(that, 'click', that.btnNextForm_click);
            controls.btnNexResumen.addEvent(that, 'click', that.btnNexResumen_click);
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();

        },
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function ()
        {
            var that = this, controls = that.getControls();
            that.Loading();
            that.getStartPage();           
        },
        GetDataSession: function () {
            var that = this, controls = this.getControls();
           
            Session.IDSESSION = "5051879654";//sessionTransacHFC.UrlParams.IDSESSION;
            Session.DATACUSTOMER = sessionTransacHFC.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = sessionTransacHFC.SessionParams.DATASERVICE;
            Session.USERACCESS = sessionTransacHFC.SessionParams.USERACCESS;
        },
        GetPageLoad_HFC: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oUserAccess = Session.USERACCESS,
                oDataService = Session.DATASERVICE,
                param = {};

            param.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            param.estadoLinea = sessionTransacHFC.SessionParams.DATASERVICE.StateLine;//Session.IDSESSION;          

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: URL_PageLoad_HFC,
                success: function (response) {
                    that.setMainTitle(response.hdnTituloPagina);

                    //EVALENZS - INICIO - SERVICIOS ADICIONALES                    
                    var strPlano = oCustomer.PlaneCodeInstallation;
                    var strPlanoFTTH = response.strPlanoFTTH;
                    if (strPlano.search(strPlanoFTTH) > 0 && response.strMensajeTransaccionFTTH != '') {
                        alert(response.strMensajeTransaccionFTTH.replace('{0}', response.hdnTituloPagina), "Alerta", function () {//Activación y Desactivación de Servicios Adicionales
                            parent.window.close();
                        });
                        return false;
                    }
                    //EVALENZS - FIN

                    if (oUserAccess == null || oCustomer == null) {
                        parent.window.close();
                        return;
                    }
                    if (response.EstadoLinea != "") {
                        alert(response.EstadoLinea, "Alerta", function () {
                        parent.window.close();
                        return;
                        });
                        
                    }

                    if (oUserAccess.userId == null || oUserAccess.userId == '0' || oUserAccess.userId == '' || oUserAccess.userId == '&nbsp;') {
                        parent.window.close();
                        return;
                    }

                    if (response.hdnValorIGV == "0") {
                        alert("El servicio de consulta IGV no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.", "Alerta", function () {
                            parent.window.close();
                        });

                        return;
                    }

                    AdditionalServicesModel.hdnValorIGV = response.hdnValorIGV;
                    AdditionalServicesModel.gstrTransaccionDTHTACTDESSER = response.gstrTransaccionDTHTACTDESSER;
                    
                    controls.btnConstancy.prop("disabled", true);
                    controls.btnActive.prop("disabled", true);
                    controls.btnDesactive.prop("disabled", true);
                    controls.divErrorAlertTable.hide();
                    that.getLoadTypification();

                }
            });

        },
        btnNexResumen_click: function (e) {
            if ($("#cboCACDAC").val() == "" || $("#cboCACDAC").val() == "-1")
            {
                alert("Debes seleccionar un punto de atención.", "Alerta");
                return;
            }

            if ($('#chkSendMail').is(':checked')) {
                var tempMail = $('#txtEmail').val();
                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test(tempMail);
                if (blvalidar === false) {
                    alert("Ingrese un email correcto.", "Alerta");
                    return;
                }            
            }

            if ($('#chkProgram').is(':checked')) {
                if ($("#txtProgramDate").val() == "") {
                    alert("Ingrese una fecha correcta.", "Alerta");
                    return;
                }                
            }


            navigateTabs(e);
            $('#tblResumen').html('');

            var that = this,
                controls = that.getControls();

            var costoPVUSel = "";

            if (HiddenPageHtml.hdnEstadoSerSel == 'A')
                costoPVUSel = AdditionalServicesModel.strHdnCargoFijoSel;
            else
                costoPVUSel = AdditionalServicesModel.strHdnCostoPVUSel;

            var item = "";
            item += "<tr><td colspan='9' style='color:white;background-color:#a7a8aa;'>" + AdditionalServicesModel.hdnDescrTypeService + "</td></tr>";
            item +=
                "<tr>" + 
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + AdditionalServicesModel.strHdnDesCoSerSel + "</td>" +
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + AdditionalServicesModel.hdnMotivoPorPaquete + "</td>" +
                "<td style='width:40px; text-align:center; color:black;background-color:white;'>" + HiddenPageHtml.hdnEstadoSerSel + "</td>" +
                "<td style='width:70px; text-align:center; color:black;background-color:white;'>" + AdditionalServicesModel.hdnValidoDesde + "</td>" +
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + "" + "</td>" +
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + AdditionalServicesModel.strHdnCargoFijoSel + "</td>" +
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + costoPVUSel + "</td>" +
                "<td style='width:150px; text-align:center; color:black;background-color:white;'>" + AdditionalServicesModel.hdnPeriodo + "</td>" +
                "</tr>";

            $('#tblResumen').html(item);

            //Campaña
            if ($("#chkCampiagn").is(':checked')) {
                $('#hdnCampañaResumen').show();
                $('#lblCampañaResumen').text($('#cboCamapaign option:selected').text());
            } else {
                $('#hdnCampañaResumen').hide();
            }

            //Email
            if ($('#chkSendMail').is(':checked')) {
                $('#lblEmailResumen').text($('#txtEmail').val());
                $('#hdnEmailResumen').show();
            } else {
                $('#hdnEmailResumen').hide();
            }

            //Fecha Programada
            if ($('#chkProgram').is(':checked')) {
                $('#lblProgramarResumen').text($('#txtProgramDate').val());
                $('#hdnProgramarResumen').show();
            } else {
                $('#hdnProgramarResumen').hide();
            }

            //Punto de atencion
            if ($("#cboCACDAC").val() == "") {
                $('#hdnPuntoAtencionResumen').hide();
            } else {
                $('#hdnPuntoAtencionResumen').show();
                $('#lblPuntoAtencioResumen').text($('#cboCACDAC option:selected').text());
            }

            //Nota
            if ($('#txtNotesTask').val() == "") {
                $('#hdnNotaResumen').hide();
            } else {
                $('#hdnNotaResumen').show();
                $('#lblNotaResumen').text($('#txtNotesTask').val());
            }
        },
        HideInputText: function () {
            var controls = this.getControls();
            controls.txtEmail.prop("style").display = "none";
            controls.divContentProgramDate.prop("style").display = "none";
        },
        btnCheckDevices_click: function() {
            var controls = this.getControls();
            var urlCheckDevices = location.protocol + "//" + location.host + "/Transactions/HFC/CheckDevices/HfcCheckDevices";
            window.open(urlCheckDevices, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1200, height=640');
        },
        SaveTransaction : function(){
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oUserAccess = Session.USERACCESS,
                oDataService = Session.DATASERVICE;
           
            if (AdditionalServicesModel.strHdnCoSerSel == '' || AdditionalServicesModel.strHdnCoSerSel == null || AdditionalServicesModel.strHdnCoSerSel=='&nbsp;') {
                alert('Seleccione un Servicio.', "Alerta");
                return false;
            }

            if (controls.chkCampiagn.prop("checked")) {
                if (controls.cboCamapaign.val() == "" || controls.cboCamapaign.val() == '-1') {
                    alert("Debe seleccionar una campaña.", "Alerta");
                    return false;
                }
            }

            if (!that.ValidateEmail()) { return false; }
            if (!that.ValidateProgram()) { return false; }
            if (AdditionalServicesModel.strHdnTipoTransaccion != 'A' && AdditionalServicesModel.strHdnTipoTransaccion != 'D') {
                //AdditionalServicesModel.strHdnTipoTransaccion = "A";
                alert("Debe seleccionar un servicio activo o desactivo.", "Alerta");
                return false;
            }

            confirm("¿Está seguro que desea grabar la transacción?", 'Confirmar', function (result) {
                if (result == true) {
                    var model = {};
                    that.Loading();
                    controls.btnActive.prop("disabled", false);

                    model.IdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
                    model.strContractId = oCustomer.ContractID;
                    model.strCustomerId = oCustomer.CustomerID;
                    model.strAmount = oCustomer.Account;
                    model.strCboCampaign = controls.cboCamapaign.val();
                   
                    model.strBillingCycle = oCustomer.BillingCycle;
                    model.strTxtNote = controls.txtNotesTask.val();
                    model.strCodPlaneInst = oCustomer.PlaneCodeInstallation;
                    model.strFirstName = oCustomer.Name;
                    model.strLastName = oCustomer.LastName;
                    model.strNumberDocument = oCustomer.DocumentNumber;
                    model.strReferencePhone = oCustomer.PhoneReference;
                    model.strReasonSocial = oCustomer.BusinessName;
                    model.strContactClient = oCustomer.CustomerContact;
                   
                    model.strPlan = oDataService.Plan;
                    model.strCacDacDescription = $('#' + controls.cboCACDAC.attr("id") + ' option:selected').text();

                    if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                        model.strFlagChkSendEmail = "T";
                        model.strTxtSendEmail = controls.txtEmail.val();
                    }
                    else {
                        model.strFlagChkSendEmail = "F";
                        model.strTxtSendEmail = "";
                    }

                    if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                        model.strFlagChkProgramming = "T";
                        model.strDateProgramation = controls.txtProgramDate.val();
                    }
                    else {
                        model.strFlagChkProgramming = "F";
                        model.strDateProgramation = "";
                    }

                    model.strPhone = oDataService.CellPhone;
                    model.strLegalRepresent = oCustomer.LegalAgent;
                    model.strDocumentType = oCustomer.DocumentType;
                    model.strFullNameCustomer = oCustomer.FullName;
                    model.strAccountUser = oUserAccess.login;
                   
                    
                    model.strHdnTipoTransaccion = AdditionalServicesModel.strHdnTipoTransaccion;
                    model.strHdnTipiService = AdditionalServicesModel.strHdnTipiService;
                    model.strHdnDesCoSerSel = AdditionalServicesModel.strHdnDesCoSerSel;
                    model.strHdnValuePVUMatch = AdditionalServicesModel.strHdnValuePVUMatch;
                    model.strHdnCoSerSel = AdditionalServicesModel.strHdnCoSerSel;
      
                    model.strHdnCostoPVUSel = AdditionalServicesModel.strHdnCostoPVUSel;
                    model.strHdnCostoBSCS = AdditionalServicesModel.strHdnCostoBSCS;
                    model.strHdnCargoFijoSel = AdditionalServicesModel.strHdnCargoFijoSel;

                    model.strDniRuc = oCustomer.DNIRUC;
                    model.strAddress = oCustomer.Address;
                    model.strDistrict = oCustomer.District;
                    model.strDepartment = oCustomer.Departament;
                    model.strProvince = oCustomer.Province;

                    model.strHdnClassCode = AdditionalServicesModel.strHdnClassCode;
                    model.strHdnSubClassCode = AdditionalServicesModel.strHdnSubClassCode;
                    model.strHdnSubClass = AdditionalServicesModel.strHdnSubClass;
                    model.strHdnType = AdditionalServicesModel.strHdnType;
                    model.strHdnClass = AdditionalServicesModel.strHdnClass;
                    model.strAccountNumber = oCustomer.Account;
                    model.strUsernameApp = sessionTransacHFC.SessionParams.USERACCESS.login;

                    model.strCustomerType = oCustomer.CustomerType;

                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(model),
                        url: URL_AdditionalServiceSaveTransaction,
                        error: function (request, status, error) {
                            alert("Ocurrió un error al realizar la operación.", "Alerta");
                        },
                        success: function (response) {
                            AdditionalServicesModel.HdnList = "1";
                            mensajeActivacionDesactivacion = response.strMessageErrorTransac;
                            AdditionalServicesModel.strHdnCaseId = response.strHdnCaseId;
                            controls.btnActive.prop("disabled", true);
                            if (response.bErrorTransac === false) {
                                modelTempSave = model;
                                that.btnConstancySendMail_click();                             
                            }
                            else {
                                alert(response.strMessageErrorTransac, "Alerta");
                                controls.btnConstancy.prop("disabled", true);
                            }
                        }
                    });
                }
            });
        },
        btnActive_click: function () {
            var that = this, controls = that.getControls();
            that.SaveTransaction();
        },
        btnDesactive_click: function () {
            var that = this, controls = that.getControls();
            that.SaveTransaction();
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
        btnConstancySendMail_click : function(){
            var that = this,
                  controls = that.getControls(),
                  oCustomer = Session.DATACUSTOMER,
                  oUserAccess = Session.USERACCESS,
                  oDataService = Session.DATASERVICE;

            $.blockUI({
                message: controls.myModalLoad,
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
            var model = {};
            model.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            model.strInteraccionId = AdditionalServicesModel.strHdnCaseId;
            model.strTypeTransaction = AdditionalServicesModel.gstrTransaccionDTHTACTDESSER;
            model.oModel = modelTempSave;
            $.ajax({
                url: '/Transactions/HFC/AdditionalServices/GenerateContancy',
                type: 'POST',
                data: JSON.stringify(model),
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (response.Generated) {
                        alert(mensajeActivacionDesactivacion, "Informativo");
                        routeConstancy = response.FullPathPDF;
                        controls.btnConstancy.prop("disabled", false);
                    }
                    else {
                        alert("Ocurrió un error generando la constancia.", "Alerta");
                    }
                },
                error: function () {
                    alert("Ocurrió un error al generar la constancia.", "Alerta");
                }
            });

        },
        btnConstancy_click : function(){
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oUserAccess = Session.USERACCESS,
                oDataService = Session.DATASERVICE;

            $.blockUI({
                message: controls.myModalLoad,
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

            if (routeConstancy !== "")
            {
                ReadRecordSharedFile(sessionTransacHFC.SessionParams.USERACCESS.userId, routeConstancy);
            }

        },
        chkSendMail_Change: function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            if (sender.prop("checked")) {
                controls.txtEmail.prop("style").display = "block";
            } else {
                controls.chkSendMail.html("");
                controls.txtEmail.prop("style").display = "none";
            }

        },
        chkProgram_Change : function (sender, arg) {
            var controls = this.getControls();
            var that = this;
            var tempData = controls.btnActive.text();
            var chkProgramar = $("#chkProgram").prop("checked");

            //that.StateButton(chkProgramar);

            if (chkProgramar) {

                controls.divContentProgramDate.prop("style").display = "block";
                if (controls.btnActive.text() === "Activar") {
                    controls.btnActive.html('<span class="glyphicon glyphicon-save"></span>Programar Activación');
                }
                if (controls.btnActive.text() === "Desactivar") {
                    controls.btnActive.html('<span class="glyphicon glyphicon-save"></span>Programar Desactivación');
                }

                var strCycleFacturation = sessionTransacHFC.SessionParams.DATACUSTOMER.BillingCycle;
                $("#txtProgramDate").attr("disabled", false);
                var dateCurrent = new Date();
                var dateFacturation = new Date(dateCurrent.getFullYear(), (dateCurrent.getMonth()), strCycleFacturation);
                var dateFactureComp = new Date(dateCurrent.getFullYear(), dateCurrent.getMonth(), (parseInt(strCycleFacturation, 10) - 1));
                if (dateCurrent >= dateFactureComp) {
                    dateFacturation.setMonth(dateFacturation.getMonth() + 1);
                }
                dateFacturation.setDate(dateFacturation.getDate() - 1);
                var month;
                if (parseInt(dateFacturation.getMonth(), 10) < 9) {
                    month = '0' + (parseInt(dateFacturation.getMonth(), 10) + 1);
                } else {
                    month = (parseInt(dateFacturation.getMonth(), 10) + 1);
                }
                var day;
                if (parseInt(dateFacturation.getDate(), 10) < 10) {
                    day = '0' + (parseInt(dateFacturation.getDate(), 10));
                } else {
                    day = (parseInt(dateFacturation.getDate(), 10));
                }

                $("#txtProgramDate").val(day + '/' + month + '/' + dateFacturation.getFullYear());

            } else {
                $('#txtProgramDate').datepicker({ format: 'dd/mm/yyyy' }).val("");

                $("#txtProgramDate").prop("disabled", true);

                controls.chkSendMail.html("");
                controls.divContentProgramDate.prop("style").display = "none";
                controls.txtProgramDate.val("");

                if (controls.btnActive.text() === "Programar Activación") {
                    controls.btnActive.html('<span class="glyphicon glyphicon-save"></span>Activar');
                }
                if (controls.btnActive.text() === "Programar Desactivación") {
                    controls.btnActive.html('<span class="glyphicon glyphicon-save"></span>Desactivar');
                }
            }
        },
        chkCampiagn_Change: function (sender, arg) {
          var that = this,
          controls = that.getControls();
            if (controls.chkCampiagn.prop("checked")) {
                controls.cboCamapaign.show("fade");
            }
            else {
                controls.cboCamapaign.hide("fade");
            }
            
        },

        btnProgramTask_click: function () {
            var urlProgramTask = location.protocol + "//" + location.host + "/Transactions/HFC/ProgramTask/HfcProgramTask?coid=" + sessionTransacHFC.SessionParams.DATACUSTOMER.ContractID;
            window.open(urlProgramTask, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=1200, height=640');
        },
        btnClose_click: function () {
            parent.window.close();
        },

	getCACDAC: function () {
            var that = this, objCacDacType = {
                strIdSession: "5051879654"
            };

            var parameters = {};
            parameters.strIdSession = "5051879654";
            parameters.strCodeUser = sessionTransacHFC.SessionParams.USERACCESS.login;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    //console.log"cacdac: " + cacdac);
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            $("#cboCACDAC").append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {
                                if (cacdac === value.Description) {
                                    $("#cboCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    $("#cboCACDAC").append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#cboCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            controls.spnMainTitle.html('<b>' + titlePage + '</b>');
        },
        getCustomerData: function () {
            var that = this, controls = that.getControls();
            controls.lblCustomerIds.text(sessionTransacHFC.SessionParams.DATACUSTOMER.CustomerID);
            controls.lblContract.text(sessionTransacHFC.SessionParams.DATACUSTOMER.ContractID);
            controls.lblName.text(sessionTransacHFC.SessionParams.DATACUSTOMER.FullName);
            controls.lblState.text(sessionTransacHFC.SessionParams.DATASERVICE.StateLine);
            controls.lblDate.text(sessionTransacHFC.SessionParams.DATASERVICE.ActivationDate);
            controls.lblMotive.text(sessionTransacHFC.SessionParams.DATASERVICE.StateLine);
            $("#txtEmail").val(sessionTransacHFC.SessionParams.DATACUSTOMER.Email);

        },           
        getStartPage: function () {
            var that = this, paramFinal = {}, paramtersOne = {};
            paramtersOne.strIdSession = sessionTransacHFC.SessionParams.USERACCESS.userId;
            paramtersOne.intIdContract = sessionTransacHFC.SessionParams.DATACUSTOMER.ContractID;
            paramtersOne.strTypeProduct = sessionTransacHFC.UrlParams.SUREDIRECT;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(paramtersOne),
                url: '/Transactions/LTE/AdditionalServices/GetCustomerPhone',
                success: function (responseOne) {
                    if (responseOne !== "") {
                        paramFinal.strIdSession = "5051879654";//sessionTransacHFC.UrlParams.IDSESSION;
                        paramFinal.nroCelular = responseOne;
                        //$('#').val();
                        paramFinal.cadenaOpciones = sessionTransacHFC.SessionParams.USERACCESS.optionPermissions;
                        AdditionalServicesModel.hdnTelefono = responseOne;
                        $.app.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            data: JSON.stringify(paramFinal),
                            url: location.protocol + "//" + location.host + "/Transactions/HFC/AdditionalServices/StartPage_HFC",
                            success: function (response) {                      
                                $('#chkCampiagn').attr('disabled', response.chkCampana);
                                AdditionalServicesModel.gstrTransaccionDTHTACTDESSER = paramFinal.gstrTransaccionDTHTACTDESSER;
                                AdditionalServicesModel.gConstTipoHFC = paramFinal.gConstTipoHFC;
                                that.GetDataSession();
                                that.GetPageLoad_HFC();
                            }
                        });
                    } else {
                        alert('Contrato inactivo o suspendido.', 'Alerta', function () {
                            parent.window.close();
                        });
                    }                 
                },
                error: function (e) {
                    alert('Contrato inactivo o suspendido.', 'Alerta', function () {
                        parent.window.close();
                    });
                }
            });   
        },
        getLoadTypification: function() {
            var that = this, param = {};
            param.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: URL_LoadTypification_HFC,
                success: function (response) {                 
                    if (response.lblMensajeVis) {
                        alert(response.lblMensajeTxt, "Alerta");
                        $('#btnActive').attr('disabled', response.btnGuardar);
                    }
                    else
                    {
                        AdditionalServicesModel.strHdnClassCode = response.hidClaseId;
                        AdditionalServicesModel.strHdnSubClassCode = response.hidSubClaseId;
                        AdditionalServicesModel.strHdnSubClass = response.hidClaseDes;
                        AdditionalServicesModel.strHdnType = response.hidTipo;
                        AdditionalServicesModel.strHdnClass = response.hidClass;
                    }

                    $('#btnConstancy').attr('disabled', true);

                    that.GetCommercialServices();
                    that.getCustomerData();
                    that.getCACDAC();
                    that.HideInputText();
                    that.getBusinessRules();
                }
            });
        },
        getBusinessRules: function () {
            var that = this,
                controls = that.getControls(),
                objRules = {};

            objRules.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            objRules.strSubClase = AdditionalServicesModel.strHdnSubClassCode;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    }
                }
            });
        },
        GetCommercialServices: function () {
            var that = this, objCacDacType = {}, controls = that.getControls();
            objCacDacType.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            objCacDacType.strCoId = sessionTransacHFC.SessionParams.DATACUSTOMER.ContractID; //CAMBIO

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: URL_HfcGetCommercialSercices,
                success: function (response) {
                    that.SetDataToTable(response);
                }
            });
        },
        SetDataToTable: function (data) {
            var that = this,
             controls = that.getControls(),
             oCustomer = Session.DATACUSTOMER,
             oUserAccess = Session.USERACCESS,
             oDataService = Session.DATASERVICE,
             model = {};

            var totalsin = 0;
            var totalcon = 0;
            var valorAux = 0;

            var filas = data;

            if (filas.length == 0) {

                model.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
                model.strIdTransaction = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
                model.cargoSinIgv = "00.00";
                model.cargoConIgv = "00.00";
                that.LogTotalFixedCharge(model);
            }

            var gruposdeservicio = new Array();
            var existe = 0;
            var indiceGrupos = 0;
            // SE ARMAN LOS GRUPOS DE SERVICIO EN gruposdeservicio
            for (var i = 0; i < filas.length; i++) {
                existe = 0;
                for (var e = 0; e < gruposdeservicio.length; e++) {
                    if (gruposdeservicio[e] == filas[i].DE_GRP) {
                        existe = 1;
                    }
                }
                if (existe == 0) {
                    gruposdeservicio[indiceGrupos] = filas[i].DE_GRP;
                    indiceGrupos++;
                }
            }

            var index = 0;
            if (filas.length == 0) {
                var htmlNoData = '<tr><td colspan="9" style="color:black; text-align: center;"> No existen servicios adicionales para este contrato.</td></tr>';
                controls.tblServBody.html(htmlNoData);
                alert("No hay servicios disponibles para el contrato.", "Alerta");
                return false;
            }

            if (filas[0].DE_SER == "Error") {
                alert("Ocurrió un error al cargar los servicios.", "Alerta");
                return false;
            }
            var html = "";
            var a = "";

            var htmlgrupos = new Array();
            var aux1 = "";
            for (var u = 0; u < gruposdeservicio.length; u++) {
                aux1 = "";
                aux1 = aux1 + "<tr>";
                aux1 = aux1 + "<td colspan='9' style='color:white;background-color:#a7a8aa;'>" + gruposdeservicio[u] + "</td>";
                aux1 = aux1 + "</tr>";
                htmlgrupos[u] = aux1;
            }

            var vacio = "";
            $.each(filas, function (key, value) {

                var gruposerv = filas[index].DE_GRP;
                var coSer = filas[index].CO_SER;
                var SNCODE = filas[index].SNCODE;
                var SPCODE = filas[index].SPCODE;
                var estado = filas[index].ESTADO;
                var bloqact = filas[index].BLOQ_ACT;
                var bloqdes = filas[index].BLOQ_DES;
                var valorpvu = filas[index].VALORPVU;
                var costopvu = filas[index].COSTOPVU;
                var descoser = filas[index].DESCOSER;
                var cargoFijo = filas[index].CUOTA;
                var tipoServicio = filas[index].TIPOSERVICIO;
                var tiposervpvu = filas[index].TIPO_SERVICIO;

                //Poryecto LTE: vmg
                var codservpvu = filas[index].CODSERPVU;
                var motivoporpquete = filas[index].DE_EXCL;
                var validodesde = filas[index].VALIDO_DESDE;
                var periodo = filas[index].PERIODOS;

                var color = "black";
                var fondo = "white";

                var flagEstado = "";
                valorAux = 0;
                if (estado === "A") {
                    color = "green";
                    flagEstado = "A";
                    valorAux = cargoFijo;
                }
                if (estado === "D") {
                    color = "red";
                    flagEstado = "D";
                }
                if ((bloqact === "S" && bloqdes === "S") || tiposervpvu === "CORE ADICIONAL" || tiposervpvu === "CORE") {
                    fondo = "#FAFAB1";
                    flagEstado = "N";
                }
                else {
                    if (bloqact === "S") {
                        fondo = "#E9E9E7"; //plomo
                        flagEstado = "N";
                    }
                    else
                    {
                        if (bloqdes === "S") {
                            fondo = "Fuchsia";
                            flagEstado = "N";
                        }
                    }
                }

                if (estado === "" && bloqact !== "S" && bloqdes !== "S") {
                    flagEstado = "D";
                }

                html = "";
                html = html + "<tr>";
                var idRadio = "rdb_" + coSer + "_" + SNCODE + "_" + SPCODE + "_" + index.toString() + "_" + valorpvu + "_" + costopvu + "_" + descoser + "_" + cargoFijo + "_" + flagEstado + "_" + tipoServicio + "_" + codservpvu + "_" + gruposerv + "_" + motivoporpquete + "_" + validodesde + "_" + periodo;
                
                html = html + "<td style='width:30px; text-align:center; color:" + color + ";'><input  onclick='getDataClickRow(this.value)'  id='" + idRadio + "'   name='group3' value='" + idRadio + "' type='radio' /></td>";

                a = descoser;
                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:150px; text-align:center; color:" + color + ";background-color:" + fondo + ";' >" + a + "</td>";
                a = filas[index].DE_EXCL;
                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = estado;
                if (a == "") {
                    a = vacio;
                }

                html = html + "<td style='width:40px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = filas[index].VALIDO_DESDE;
                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:70px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = "";
                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = cargoFijo;
                if (a == "") {
                    a = vacio;
                }

                html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = "";
                if (estado == "A") {
                    a = a + cargoFijo;
                    valorAux = a;
                } else {
                    a = a + costopvu;
                }

                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                a = filas[index].PERIODOS;
                if (a == "") {
                    a = vacio;
                }
                html = html + "<td style='width:150px; text-align:center; color:" + color + "; background-color:" + fondo + ";'>" + a + "</td>";
                html = html + "</tr>";

                if (valorAux != 0) {
                    totalsin = parseFloat(totalsin) + parseFloat(valorAux.replace(",", "."));
                }

                for (var t = 0; t < gruposdeservicio.length; t++) {
                    if (gruposerv == gruposdeservicio[t]) {
                        htmlgrupos[t] = htmlgrupos[t] + html;
                    }
                }
                index++;
            });

            var valorIgv = AdditionalServicesModel.hdnValorIGV;

            totalcon = parseFloat(totalsin) * parseFloat(1 + parseFloat(valorIgv));

            document.getElementById('lblBuyWithoutIGV').innerHTML = parseFloat(totalsin).toFixed(2);
            document.getElementById('lblBuyWithIGV').innerHTML = parseFloat(totalcon).toFixed(2);

            model = {};
            model.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            model.strIdTransaction = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
            model.cargoSinIgv = parseFloat(totalsin).toFixed(2),
            model.cargoConIgv = parseFloat(totalsin).toFixed(2),

            that.LogTotalFixedCharge(model);
 
            var htmlFinal = "";
            for (var k = 0; k < gruposdeservicio.length; k++) {
                htmlFinal = htmlFinal + htmlgrupos[k];
            }
            $("#tblServBody").html(htmlFinal);

            if (index == 0) {
                alert("No hay servicios disponibles para el contrato.", "Alerta");
            }

        },
        LogTotalFixedCharge: function (oModel) {
            $.ajax({
                type: "POST",
                url: URL_LogTotalFixedCharge,
                data: JSON.stringify(oModel),
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (data) {

                },
                success: function (data) {

                }
            });
        },
        Loading: function () {
            var that = this,
              controls = that.getControls();

            $.blockUI({
                message: controls.myModalLoad,
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
        },
        ValidateEmail: function () {
            var that = this,
             controls = that.getControls();
            if ($.trim(controls.txtEmail.val()) == '') {
                if (document.getElementById(controls.chkSendMail.attr("id")).checked == true) {
                    alert(AdditionalServicesModel.strMessageEnterMail, "Alerta");
                    return false;
                } else {
                    return true;
                }
            } else {
                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test(controls.txtEmail.val());
                if (blvalidar == false) {
                    alert(AdditionalServicesModel.strMessageValiateMail, "Alerta");
                    document.getElementById(controls.txtEmail.attr("id")).focus();
                    return false;
                } else { return true; }
            }

        },
        ValidateProgram: function () {
            var that = this,
             controls = that.getControls();
            if (document.getElementById(controls.chkProgram.attr("id")).checked == true) {
                if ($.trim(controls.txtProgramDate.val()) == "") {
                    alert("Debe seleccionar una fecha de programación.", "Alerta");
                    return false;
                } else {
                    return true;
                }
            }
            else {
                return true;
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
        btnNextForm_click: function (e) {
            var result = HiddenPageHtml.hidCheckTable;
            if (result == "1") {
                navigateTabs(e);
                $('#divErrorAlertTable').hide();
            } else {
                $('#divErrorAlertTable').show();
                $('#lblErrorMessageTable').text("Debes seleccionar al menos un servicio de la lista!");
            }
        }       
    };

    $.fn.ActiveDesactiveServiceHFC = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('ActiveDesactiveServiceHFC'),
                options = $.extend({}, $.fn.ActiveDesactiveServiceHFC.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('ActiveDesactiveServiceHFC', data);
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

    $.fn.ActiveDesactiveServiceHFC.defaults = {
    };

    $('#divBody').ActiveDesactiveServiceHFC();
 
})(jQuery);


function getDataClickRow (value){
    if (AdditionalServicesModel.HdnList == "1") { return; }
    HiddenPageHtml.hidCheckTable = "1";
    var vValues = value.split("_");
    AdditionalServicesModel.strHdnCoSerSel = vValues[1];
    AdditionalServicesModel.strHdnSNCodeSel = vValues[2];
    AdditionalServicesModel.strHdnSPCodeSel = vValues[3];
    AdditionalServicesModel.strHdnSelectedIndex = vValues[4];
    AdditionalServicesModel.strHdnValuePVUMatch = vValues[5];
    AdditionalServicesModel.strHdnCostoPVUSel = vValues[6];
    AdditionalServicesModel.strHdnDesCoSerSel = vValues[7];
    AdditionalServicesModel.strHdnCargoFijoSel = vValues[8];
    AdditionalServicesModel.strHdnCostoBSCS = vValues[8];
    HiddenPageHtml.hdnEstadoSerSel = vValues[9];
    AdditionalServicesModel.strHdnTipiService = vValues[10];

    AdditionalServicesModel.hdnDescrTypeService = vValues[12];
    AdditionalServicesModel.hdnMotivoPorPaquete = vValues[13];
    AdditionalServicesModel.hdnValidoDesde = vValues[14];
    AdditionalServicesModel.hdnPeriodo = vValues[15];
    //console.logvValues[9]);
    if (vValues[9] == "A") {
        AdditionalServicesModel.strHdnTipoTransaccion = "D";
        $("#btnActive").html('<span class="glyphicon glyphicon-save"></span>Desactivar');
        $("#btnActive").prop("disabled", false);
        $("#trCampanaTitle").hide();
    }
    else
    {
        if (vValues[9] == "D") {
            AdditionalServicesModel.strHdnTipoTransaccion = "A";
            $("#btnActive").html('<span class="glyphicon glyphicon-save"></span>Activar');
            $("#btnActive").prop("disabled", false);
            GetPromotionsList(AdditionalServicesModel.strHdnSNCodeSel);
            $("#trCampanaTitle").show();
        }
        else {
            AdditionalServicesModel.strHdnTipoTransaccion = "";
            $("#btnActive").html('<span class="glyphicon glyphicon-save"></span>Activar/Desact.');
            $("#btnActive").prop("disabled", true);
            $("#trCampanaTitle").hide();
        }
    }
}

function GetPromotionsList (vValue) {
    var oCustomer = sessionTransacHFC.SessionParams.DATACUSTOMER;
    $("#cboCamapaign").html("<option value='-1'>Cargando...</option>");

    var model = {};
    model.strIdSession = "5051879654"; //sessionTransacHFC.UrlParams.IDSESSION;
    model.strContractId = oCustomer.ContractID;
    model.strSncode = vValue;
    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: JSON.stringify(model),
        url: URL_GetCamapaign,
        success: function (response) {
            $("#cboCamapaign").html("");
            $("#cboCamapaign").append($('<option>', { value: '-1', html: 'Seleccionar' }));
            if (response != null) {
                if (response.length > 0) {
                    $.each(response,
                        function(index, value) {
                            $("#cboCamapaign").append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                        });

                } else {
                    $("#cboCamapaign").empty().html("<option value='-1'>No se encontró información.</option>");
                }
            }

        }
    });     
}