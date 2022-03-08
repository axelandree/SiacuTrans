    var SessionBOCPostpaid = {};
// ==== Funciones Nuevas ==========
SessionBOCPostpaid.strinteractionId = "";
function ChanguedStatusButtons(controls, flagStatus) {
    if (flagStatus == "E") {
        controls.btnSave.removeAttr('disabled', 'disabled');
        controls.btnPrint.removeAttr('disabled', 'disabled');
        controls.btnExport.removeAttr('disabled', 'disabled');
        controls.btnConstancy.removeAttr('disabled', 'disabled');
        controls.btnSendEmail.removeAttr('disabled', 'disabled'); 
         
    } else {
        controls.btnSave.attr('disabled', 'disabled');
        controls.btnPrint.attr('disabled', 'disabled');
        controls.btnExport.attr('disabled', 'disabled');
        controls.btnConstancy.attr('disabled', 'disabled');
        controls.btnSendEmail.attr('disabled', 'disabled');
    }
}


function CloseValidation(obj, pag, controls) { 
    var option = controls.hidOption.val();
    //obj.hidAccion = 'G';//Temporal 
    if (obj.hidAccion == 'G') {
        if (option == "I") {
            Print(pag, controls, obj.hidUserValidator);
        } else if (option == "E") {
            ExportExcel(pag, controls, obj.hidUserValidator);
        } else if (option == "S") {
            Search_CallsDetail(pag, controls);
        }

    } else if (obj.hidAccion == 'F') {
        var mensaje = "La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.";
        alert(mensaje,"Alerta");
    } 
};

function ExportExcel(pag, controls, userAdmin) {
    var strUrlModal = pag.strUrl + '/GetPathExportExcel';
    var strUrlResult = '/Transactions/CommonServices/DownloadExcel';
    //var strUrlResult = window.location.protocol + '//' + window.location.host + '/CommonServices/DownloadExcel';

    var parameters = {
        idSession: SessionBOCPostpaid.IdSession,
        customer: SessionBOCPostpaid.Client,
        plan: SessionBOCPostpaid.Plan,
        profileCode: SessionBOCPostpaid.CodesProfiles, 
    }; 
    $.app.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify(parameters),
        success: function (result) {
            if (result.StatusCode == "OK") {
                controls.divErrorAlert.hide(); 
                window.location = strUrlResult + '?strPath=' + result.PathExcel + "&strNewfileName=ReporteDeLlamadas.xlsx";
            } else {
                controls.divErrorAlert.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                return;
            }
        }
    });
}

function FillData_Datatable(tblTable, list) {
    tblTable.find('tbody').html('');

    tblTable.DataTable({
        "scrollCollapse": true,
        "info": false,
        "select": 'single',
        "paging": false,
        "searching": false,
        "destroy": true,
        //"scrollX": true,
        "scrollY": 150,
        "data": list,
        "columns": [
               { "data": "Nro" },
               { "data": "StrDate" },
               { "data": "StrHour" },
               { "data": "DestinationPhone" },
               { "data": "Consumption" },
               { "data": "OriginalCharge" },
               { "data": "CallType" },
               { "data": "Destiny" },
               { "data": "Operator" },
        ],
        "language": {
            "lengthMenu": "Display _MENU_ records per page",
            "zeroRecords": "No existen datos",
            "info": " ",
            "infoEmpty": " ",
            "infoFiltered": "(filtered from _MAX_ total records)",
        },
    });
}

function Print(pag, controls, userAdmin) {
    var strUrlModal = pag.strUrl + '/GenerateDataForPrinting'; 
    var parameters = {
        idSession: SessionBOCPostpaid.IdSession,
        profileCode: SessionBOCPostpaid.CodesProfiles,
        customer: SessionBOCPostpaid.Client,
    };

    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify(parameters),
        success: function (result) {
            if (result.StatusCode == "OK") {
                controls.divErrorAlert.hide();  
                strUrlModal = pag.strUrl + '/PostpaidBilledOutCallDetailPrint?plan=' + SessionBOCPostpaid.Plan;
                var viewPrint = window.open(strUrlModal, '_blank', 'status=1,directories=no, location=no, menubar=no, scrollbars=1, statusbar=no, tittlebar=no, width=850, height=650');
                
            } else {
                controls.divErrorAlert.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                return;
            }
        },
        //error: function (errormessage) {
        //    alert('Error: ' + errormessage);
        //}

    });
}

function Save(pag, controls) {
    confirm("¿Está seguro de guardar los cambios?", "Confirmar", function () {
        ShowDataLoadingMessage(controls);

        var strUrlModal = pag.strUrl + '/GetSave';
        var flagSendEmail = "F",email='';
        if (controls.chkSendMail.prop('checked')) {
            flagSendEmail = 'T';
            email = controls.txtSendMail.val();
        } 
         

        var params = {
            idSession: SessionBOCPostpaid.IdSession,
            profileCode: SessionBOCPostpaid.CodesProfiles,
            flagSecurity: SessionBOCPostpaid.FlagSecurity,
            numberContract: SessionBOCPostpaid.NumberContract,
            customer: SessionBOCPostpaid.Client,
            flagSendEmail: flagSendEmail,
            email:email,
            codeMonth: controls.ddlMonths.val(),
            nameMonth: $('#ddlMonths option:selected').text(),
            codeYear: controls.ddlYears.val(),  
            hidTypePD:SessionBOCPostpaid.HidTypePD, 
            hidClassPD: SessionBOCPostpaid.HidClassPD,
            hidSubClassPD: SessionBOCPostpaid.HidSubClassPD,
            hidTransaction: SessionBOCPostpaid.Transaction,
            strinteractionId: SessionBOCPostpaid.strinteractionId,
            nameCACDAC: $('#ddlCacDac option:selected').text(),
            FechaInicio: SessionBOCPostpaid.FechaInicio,
            FechaFin: SessionBOCPostpaid.FechaFin,
            strFinalNotes: SessionBOCPostpaid.strFinalNotes,
        };
        //console.logparams);
        $.ajax({
            //async: false,
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            url: strUrlModal,
            data: JSON.stringify(params),
            success: function (result) {
                if (result.StatusCode == "OK") {
                    controls.divErrorAlert.hide();
                    controls.btnSearch.attr('disabled', 'disabled');
                    controls.btnSave.attr('disabled', 'disabled');

                    controls.btnExport.removeAttr('disabled', 'disabled');
                    controls.btnPrint.removeAttr('disabled', 'disabled');
                    controls.btnConstancy.removeAttr('disabled', 'disabled');
                    controls.btnSendEmail.removeAttr('disabled', 'disabled'); 
                    SessionBOCPostpaid.RutaPdf = result.RutaPdf;
                    if (result.StatusMessage!=null) {
                        alert(result.StatusMessage,"Informativo");
                    } 
                     
                } else {
                    controls.divErrorAlert.show();
                    controls.lblErrorMessage.text(result.StatusMessage);
                    ChanguedStatusButtons(controls, 'D');
                }
            },
            //error: function (errormessage) {
            //    alert('Error: ' + errormessage);
            //}
        });
    });
}

function Search_CallsDetail(pag,controls) {  
    var params = {
        CurrentId: SessionBOCPostpaid.CurrentId,
        idSession: SessionBOCPostpaid.IdSession,
        strPhone: SessionBOCPostpaid.Client.Telephone,
        idContact: SessionBOCPostpaid.Client.IdContact,
        idCustomer: SessionBOCPostpaid.Client.IdClient,
        profileCode: SessionBOCPostpaid.CodesProfiles,
        flagSecurity: SessionBOCPostpaid.FlagSecurity,
        flagPlatform: SessionBOCPostpaid.FlagPlatform,
        strTempTypePhone: SessionBOCPostpaid.HidTempTypePhone,
        codeMonth: controls.ddlMonths.val(),
        nameMonth: $('#ddlMonths option:selected').text(),
        codeYear: controls.ddlYears.val()
    };
    //console.logparams);
    var strUrlModal = pag.strUrl + '/GetSearch';
    //console.logstrUrlModal);
    alert('Se generará Tipificación por la información',"Informativo");
    $.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify(params),
        success: function (result) {
            //console.logresult);
            if (result.StatusCode == "OK") {
                if (SessionBOCPostpaid.FlagPlatform == 'C') {
                    //that.tblBilledOutCalls_Load_1(response.data.lista1);
                } else {
                    controls.tblBilledCallDetail.show();
                    FillData_Datatable(controls.tblBilledCallDetail, result.ListCallsDetail);
                } 
                SessionBOCPostpaid.HidTypePD = result.HidTypePD;
                SessionBOCPostpaid.HidClassPD = result.HidClassPD;
                SessionBOCPostpaid.HidSubClassPD = result.HidSubClassPD;
                SessionBOCPostpaid.strinteractionId = result.strinteractionId;
                SessionBOCPostpaid.FechaInicio = result.FechaInicio;
                SessionBOCPostpaid.FechaFin = result.FechaFin; 
                SessionBOCPostpaid.strFinalNotes = result.strFinalNotes;

                controls.lblTotal.html(result.StrTotal);
                controls.lblTotalSMS.html(result.StrTotalSMS);
                controls.lblTotalMMS.html(result.StrTotalMMS);
                controls.lblTotalGPRS.html(result.StrTotalGPRS);
                controls.lblTotalRegistro.html(result.StrTotalRegistration);
                controls.lblCargoFinal.html(result.StrFinalCharge);
                var numRows = Object.keys(result.ListCallsDetail).length;
                if (numRows > 0) {
                    controls.btnSave.removeAttr('disabled', 'disabled');
                }
                else { controls.btnSave.attr('disabled', 'disabled') }
            } else { 
                alert(result.StatusMessage,"Alerta");
                return;
            }
        },
        error: function (errormessage) {
            alert('Error: ' + errormessage,"Alerta");
        }
    });
}

function SendEmail(pag, controls) {
    ShowDataLoadingMessage(controls);

    var strUrlModal = pag.strUrl + '/GetSendEmail'; 

    var params = {
        idSession: SessionBOCPostpaid.IdSession,
        profileCode: SessionBOCPostpaid.CodesProfiles,
        flagSecurity: SessionBOCPostpaid.FlagSecurity,
        numberContract: SessionBOCPostpaid.NumberContract,
        customer: SessionBOCPostpaid.Client, 
        email: controls.txtSendMail.val(),
        codeMonth: controls.ddlMonths.val(),
        nameMonth: $('#ddlMonths option:selected').text(),
        codeYear: controls.ddlYears.val(),
        hidTypePD: SessionBOCPostpaid.HidTypePD,
        hidClassPD: SessionBOCPostpaid.HidClassPD,
        hidSubClassPD: SessionBOCPostpaid.HidSubClassPD,
        hidTransaction: SessionBOCPostpaid.Transaction,
    };

    $.ajax({
        //async: false,
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify(params),
        success: function (result) {
            if (result.StatusCode == "OK") {
                controls.divErrorAlert.hide();
                controls.btnSearch.attr('disabled', 'disabled');
                controls.btnSave.attr('disabled', 'disabled');

                controls.btnExport.removeAttr('disabled', 'disabled');
                controls.btnPrint.removeAttr('disabled', 'disabled');
                controls.btnConstancy.removeAttr('disabled', 'disabled');
                controls.btnSendEmail.removeAttr('disabled', 'disabled');

                if (result.StatusMessage != null) {
                    alert(result.StatusMessage,"Informativo");
                }

            } else {
                controls.divErrorAlert.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                ChanguedStatusButtons(controls, 'D');
            }
        },
        //error: function (errormessage) {
        //    alert('Error: ' + errormessage);
        //}
    });

}

//function SetInitialProperties_Datatable(tblTable) {
//    tblTable.DataTable({
//        info: false,
//        select: "single",
//        paging: false,
//        searching: false,
//        //scrollX: true,
//        scrollY: 300,
//        scrollCollapse: true,
//        destroy: true,
//        language: {
//            lengthMenu: "Display _MENU_ records per page",
//            zeroRecords: "No existen datos",
//            info: " ",
//            infoEmpty: " ",
//            infoFiltered: "(filtered from _MAX_ total records)"
//        }
//    });
//}

function ShowDataLoadingMessage(controls) {
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
}

function ValidateValues(controls) {
    if (controls.ddlMonths.val() == "") { 
        alert('Ingresar el mes.',"Alerta"); 
        return false;
    }
    if (controls.ddlYears.val() == "") { 
        alert('Ingresar el año.', "Alerta");
        return false;
    }
    if (controls.ddlCacDac.val() == "") { 
        alert('Ingresar el Punto de Atención.', "Alerta");
        return false;
    }
     
    //var selectedDate = controls.ddlYears.val() ;
    //if (controls.ddlMonths.val().length == 1)
    //    selectedDate = selectedDate + '0' +controls.ddlMonths.val();
    //else
    //    selectedDate = selectedDate + controls.ddlMonths.val();
    
    //if (selectedDate > SessionBOCPostpaid.HidCurrentDate) {
    //    alert('La fecha seleccionada no debe ser mayor a la actual.');
    //    return false;
    //}
    
    var currentDate = new Date(controls.ddlYears.val(),
        controls.ddlMonths.val()-1, 1);
    var minimumDate = new Date(SessionBOCPostpaid.HidCurrentDate.substring(0, 4),
        parseInt(SessionBOCPostpaid.HidCurrentDate.substring(4)) - 3, 1);
    var maximumDate = new Date(SessionBOCPostpaid.HidCurrentDate.substring(0, 4),
        parseInt(SessionBOCPostpaid.HidCurrentDate.substring(4)) - 1, 1);

    if (minimumDate.getTime() > currentDate.getTime()) {
        alert("La fecha seleccionada no debe ser menor de 3 meses", "Alerta");
        return false;
    }
     
    if (currentDate.getTime() > maximumDate.getTime()) {
        alert('La fecha seleccionada no debe ser mayor a la actual.', "Alerta");
        return false;
    }
    return true;
}

function ValidateEmail(email) {
    var s = email; 
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/

    if (s.length == 0) return true;
    if (filter.test(s))
        return true;
    else
        alert("Ingrese una direccion de correo valida","Alerta");
    return false;
}

// ================================
 
//tblBilledOutCalls_Load_1: function (data) {
//    var that = this,
//    controls = that.getControls();
//    controls.tblBilledCallDetail_1.show();
//    var numero = 0;
//    controls.tblBilledCallDetail_1.DataTable({
//        scrollY: "150px"
//        ,sccrollX: true
//        , select: "single"
//        , scrollCollapse: true
//        , paging: false
//        , searching: false
//        , destroy: true
//        , data: data
//        , language: {
//            "lengthMenu": "Display _MENU_ records per page",
//            "zeroRecords": "No existen datos",
//            "info": " ",
//            "infoEmpty": " ",
//            "infoFiltered": "(filtered from _MAX_ total records)"
//        },
//        "columns": [
//            {
//                "data": "TELEFONO", render: function (data, type, row) {
//                    numero++;
//                    return numero.toString();
//                }
//            },                    
//            { "data": "FECHA" },
//            { "data": "HORA" },
//            { "data": "TELEFONO_DESTINO" },
//            { "data": "CONSUMO" },
//            { "data": "CARGO_ORIGINAL" },
//            { "data": "Plan" },
//            { "data": "TIPO_LLAMADA" },
//            { "data": "DESTINO" },
//            { "data": "OPERADOR" },
//            { "data": "Horary" },
//            { "data": "BalanceSoles" },
//        ]
//    });
//}

(function ($, undefined) {
       

    
    var Form = function ($element, options) {

        $.extend(this, $.fn.BilledCallDetail.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element,
            lblCustomer: $('#lblCustomerName', $element),
            lblDateActivation: $('#lblDateActivation', $element),

            ddlMonths: $('#ddlMonths', $element),
            ddlYears: $('#ddlYears', $element),
            chkSendMail: $('#chkSendMail', $element),
            txtSendMail: $('#txtSendMail', $element),
            ddlCacDac: $('#ddlCacDac', $element),
            tblBilledCallDetail: $('#tblBilledCallDetail', $element),

            tblBilledCallDetail_1: $('#tblBilledCallDetail_1', $element),

            litTotal: $('#litTotal', $element),
            litTotalSMS: $('#litTotalSMS', $element),
            litTotalMMS: $('#litTotalMMS', $element),
            litTotalRegistro: $('#litTotalRegistro', $element),
            litCargoFinal: $('#litCargoFinal', $element),
            litGPRS: $('#lblTotalGeneralGPRS', $element),
            myModalLoad: $('#myModalLoad', $element),

            lblTotal: $('#lblTotal', $element),
            lblTotalSMS: $('#lblTotalSMS', $element),
            lblTotalMMS: $('#lblTotalMMS', $element),
            lblTotalRegistro: $('#lblTotalRegistro', $element),
            lblCargoFinal: $('#lblCargoFinal', $element),
            lblTotalGPRS: $('#lblTotalGPRS', $element),
            btnSearch: $('#btnSearch', $element),
            btnSave: $('#btnSave', $element),
            btnPrint: $('#btnPrint', $element),
            btnConstancy: $('#btnConstancy', $element),
            btnExport: $('#btnExport', $element),
            btnSendEmail: $('#btnSendEmail', $element),
            btnClose: $('#btnClose', $element),
            lblTitle: $('#lblTitle', $element), 

            //btnEnviar: $('#btnEnviar', $element), 
            lblResultMessag: $('#lblResultMessag', $element),
            divErrorAlert: $('#divErrorAlert', $element),
            lblErrorMessage: $('#lblResultMessag', $element),
            hidOption: $('#hidOpcion', $element),
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            ShowDataLoadingMessage(controls);

            controls.btnSearch.addEvent(that, 'click', that.btnSearch_click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_click);
            controls.btnSendEmail.addEvent(that, 'click', that.btnSendEmail_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_Click); 
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_Click); 
            controls.chkSendMail.addEvent(that, 'change', that.chkSendMail_Change);
            controls.tblBilledCallDetail_1.hide();
            ChanguedStatusButtons(controls, 'D');
            controls.btnSendEmail.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },


        render: function () {
            var that = this, controls = this.getControls();
            that.loadSessionData();
            that.loadForm(); 
        },
        loadSessionData: function () { 
            //Redirect ini  2.0
            var that = this, controls = this.getControls();

            //controls.lblTitle.text("DETALLE DE LLAMADAS FACTURADAS POR LÍNEA");
            controls.lblTitle.text("DETALLE DE LLAMADAS SALIENTES FACTURADAS - POSTPAGO");
            //console.log"Redireccionó a la Transacion");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);
            //Redirect fin

            SessionBOCPostpaid.CurrentId = SessionTransac.SessionParams.USERACCESS.login;
            SessionBOCPostpaid.IdSession = "12345654123";// SessionTransac.UrlParams.IDSESSION;

            //if (SessionTransac.UrlParams.IDSESSION == null || SessionTransac.UrlParams.IDSESSION == undefined) {
            //    Session.IDSESSION = '0';
            //} else {
            //    Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            //}

            SessionBOCPostpaid.PageAccess = SessionTransac.SessionParams.USERACCESS.optionPermissions;
            SessionBOCPostpaid.ArrayPageAccess = SessionBOCPostpaid.PageAccess.split(',');
            SessionBOCPostpaid.CodePlanTariff = SessionTransac.SessionParams.DATASERVICE.CodePlanTariff;
            SessionBOCPostpaid.CodesProfiles = SessionTransac.SessionParams.USERACCESS.profiles;
            SessionBOCPostpaid.FlagPlatform =  SessionTransac.SessionParams.DATASERVICE.FlagPlatform;
            SessionBOCPostpaid.NumberContract = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            SessionBOCPostpaid.Transaction = SessionTransac.UrlParams.TRANSACCION;
            SessionBOCPostpaid.Plan = SessionTransac.SessionParams.DATASERVICE.Plan;

            //SessionBOCPostpaid.FlagPlatform = 'Pers'//Temporal;

            SessionBOCPostpaid.Client = {}; 
            SessionBOCPostpaid.Client.Account = SessionTransac.SessionParams.DATACUSTOMER.Account;
            SessionBOCPostpaid.Client.Telephone = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
            SessionBOCPostpaid.Client.Type = SessionTransac.SessionParams.DATACUSTOMER.CustomerType;
            SessionBOCPostpaid.Client.IdContract = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            SessionBOCPostpaid.Client.IdContact = SessionTransac.SessionParams.DATACUSTOMER.ContactCode;
            SessionBOCPostpaid.Client.Email = SessionTransac.SessionParams.DATACUSTOMER.Email;
            SessionBOCPostpaid.Client.IdClient = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            SessionBOCPostpaid.Client.FirstName = SessionTransac.SessionParams.DATACUSTOMER.Name;
            SessionBOCPostpaid.Client.LastName = SessionTransac.SessionParams.DATACUSTOMER.LastName;
            SessionBOCPostpaid.Client.FullName = SessionTransac.SessionParams.DATACUSTOMER.FullName;
            SessionBOCPostpaid.Client.LegalRepresentative = SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
            SessionBOCPostpaid.Client.DocumentType = SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
            SessionBOCPostpaid.Client.NumberDocument = SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
            SessionBOCPostpaid.HidInteraction = "";
             
            //SessionBOCPostpaid.IdSession = 'user_adminca';
            //SessionBOCPostpaid.PageAccess = "ACP_CDL,ACP_CNC,ACP_IDL,ACP_RBI,ACP_EEL,ACP_RBE,ACP_BDM";
            //SessionBOCPostpaid.ArrayPageAccess = SessionBOCPostpaid.PageAccess.split(',');
            //SessionBOCPostpaid.CodePlanTariff = "1212",
            //SessionBOCPostpaid.CodesProfiles = "1,43,90,2,82,81";
            //SessionBOCPostpaid.FlagPlatform = "R";
            //SessionBOCPostpaid.NumberContract = "80";
            //SessionBOCPostpaid.Transaction = "TRANSACCION_DETALLE_LLAMADAS";
            //SessionBOCPostpaid.Plan = "Plan 180-I CDI";

            //SessionBOCPostpaid.Client = {};
            //SessionBOCPostpaid.Client.Account = "1.10839810";
            //SessionBOCPostpaid.Client.Telephone = "997500015",
            //SessionBOCPostpaid.Client.Type = "Consumer";
            //SessionBOCPostpaid.Client.IdContract = "80";
            //SessionBOCPostpaid.Client.IdContact = "268608511";
            //SessionBOCPostpaid.Client.Email = "cochachilj@globalhitss.com";
            //SessionBOCPostpaid.Client.IdClient = "13", 
            //SessionBOCPostpaid.Client.FirstName = '04GZ0PEW6M';
            //SessionBOCPostpaid.Client.LastName = 'OROZM9JLRT';
            //SessionBOCPostpaid.Client.FullName = '04GZ0PEW6M OROZM9JLRT';
            //SessionBOCPostpaid.Client.LegalRepresentative = 'W9QWBY5ZXW';
            //SessionBOCPostpaid.Client.DocumentType = 'Pasaporte';
            //SessionBOCPostpaid.Client.NumberDocument = '41880519';
              
            //console.logSession);

            $('#lblPlan').text(SessionTransac.SessionParams.DATASERVICE.Plan);
            $('#lblContract').text(SessionBOCPostpaid.Client.IdContract);
            $('#lblCustomerName').html(SessionBOCPostpaid.Client.FullName);
            $('#lblIdentificationDocument').html(SessionBOCPostpaid.Client.NumberDocument);
            $('#lblTypeCustomer').html(SessionBOCPostpaid.Client.Type);
            $('#lblCycleBilling').html(SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.BillingCycle);
            $('#lblReprLegal').html(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            $('#lblDateActivation').html(SessionTransac.SessionParams.DATACUSTOMER.ActivationDate);
            $('#lblContactCode').html(SessionTransac.SessionParams.DATACUSTOMER.ContactCode);
            $('#lblCustomerCode').html(SessionTransac.SessionParams.DATACUSTOMER.CustomerID);
            
            

            SessionBOCPostpaid.FlagEmail = '';
            SessionBOCPostpaid.FlagSecurity = '';
            SessionBOCPostpaid.FlagSearch = '';
            SessionBOCPostpaid.FlagPrint = '';
            SessionBOCPostpaid.FlagExport = '';
            SessionBOCPostpaid.HidCurrentDate = '';
            SessionBOCPostpaid.HidTempTypePhone = '';
            SessionBOCPostpaid.HidTypePD = '';
            SessionBOCPostpaid.HidClassPD = '';
            SessionBOCPostpaid.HidSubClassPD = '';
            SessionBOCPostpaid.RutaPdf = '';
        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        }, 
        loadForm: function () {
            var that = this, controls = this.getControls();
             
            var params = {
                idSession: SessionBOCPostpaid.IdSession,
                strPhone: SessionBOCPostpaid.Client.Telephone,
                idContract: SessionBOCPostpaid.Client.IdContract,
                arrPermissions: SessionBOCPostpaid.ArrayPageAccess,
                codePlanTariff: SessionBOCPostpaid.CodePlanTariff
            }; 
            var strUrlModal = that.strUrl + '/GetLoad';
            //console.logstrUrlModal);
            $.ajax({ 
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(params),
                success: function (result) {
                    if (result.StatusCode == "OK") {
                        controls.ddlMonths.append($('<option>', { value: '', html: 'Seleccionar' }));
                        if (result.ListMonths != null) {
                            $.each(result.ListMonths, function (index, value) {
                                controls.ddlMonths.append($('<option>', { value: value.Code, html: value.Description }));
                            });
                        }
                        controls.ddlYears.append($('<option>', { value: '', html: 'Seleccionar' }));
                        if (result.ListYears != null) {
                            $.each(result.ListYears, function (index, value) {
                                controls.ddlYears.append($('<option>', { value: value.Code, html: value.Description }));
                            });
                        }
                        controls.ddlCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                        if (result.ListCACDAC != null) {
                            $.each(result.ListCACDAC, function (index, value) {
                                controls.ddlCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                            });
                        }
                        SessionBOCPostpaid.FlagEmail = result.FlagEmail;
                        SessionBOCPostpaid.FlagSecurity = result.FlagSecurity;
                        SessionBOCPostpaid.FlagSearch = result.FlagSearch;
                        SessionBOCPostpaid.FlagPrint = result.FlagPrint;
                        SessionBOCPostpaid.FlagExport = result.FlagExport;
                        SessionBOCPostpaid.HidCurrentDate = result.HidCurrentDate;
                        SessionBOCPostpaid.HidTempTypePhone = result.HidTempTypePhone;

                        //SessionBOCPostpaid.FlagSearch = 'T';//Temporal
                        //SessionBOCPostpaid.FlagPrint = 'F';//Temporal
                        //SessionBOCPostpaid.FlagExport = 'F';//Temporal

                        if (SessionBOCPostpaid.FlagPrint == 'I') {
                            controls.btnPrint.hide();
                        }
                        if (SessionBOCPostpaid.FlagExport == 'I') {
                            controls.btnExport.hide();
                        }
                        if (SessionBOCPostpaid.FlagExport == 'C') { 
                            controls.chkSendMail.removeAttr('disabled', 'disabled');
                        } 
                    } else {
                        controls.btnSearch.attr('disabled', 'disabled');
                        alert(result.StatusMessage,"Alerta");
                        return;
                    }
                },
                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });
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
        btnPrint_click: function () {
            var that = this, controls = this.getControls();

            if (SessionBOCPostpaid.FlagPrint == "T") {
                ShowDataLoadingMessage(controls);
                Print(that, controls, '');
            } else {
                var param = {
                    "strIdSession": SessionBOCPostpaid.IdSession,
                    'pag': '1',
                    'opcion': 'gConstEvtImprimirDetalleLlamada',
                    'telefono': SessionBOCPostpaid.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'I'
                };
                ValidateAccess(that, controls, "I", '', '', param, "Postpaid");
                return;
            } 
        }, 
        btnSendEmail_click: function () {
            var that = this, controls = that.getControls();

            if (!ValidateValues(controls)) {
                return false;
            }

            var email = controls.txtSendMail.val();

            if (email == "") {
                alert('Debe indicar una dirección de correo, donde serán remitidas los datos procesados.',"Alerta");
                return false;
            }

            if (!ValidateEmail(email)) {
                return false;
            } 
            SendEmail(that,controls); 
        },
         
        chkSendMail_Change: function (sender, arg) { 
            var that = this;
            var controls = that.getControls();
            if (sender.prop("checked")) {
                controls.txtSendMail.val(SessionBOCPostpaid.Client.Email);
                controls.txtSendMail.prop("style").display = "block";
            } else {
                controls.txtSendMail.val("");
                controls.txtSendMail.prop("style").display = "none";
            }
        },
          
        btnSearch_click: function () {
            var that = this, controls = that.getControls();

            if (!ValidateValues(controls)) {
                return false;
            }

            if (SessionBOCPostpaid.FlagSearch == 'T') {
                ShowDataLoadingMessage(controls);
                Search_CallsDetail(that,controls);
            } else {
                Search_CallsDetail(that, controls);
                //var param = {
                //    "strIdSession": SessionBOCPostpaid.IdSession,
                //    'pag': '1',
                //    'opcion': 'gConstEvtBuscarDetalleLlamada',
                //    'telefono': SessionBOCPostpaid.Telephone,
                //    'transaccion': '',
                //    'DetEntAccion': 'S'
                //};
                //ValidateAccess(that, controls, "S", '', '', param, "Postpaid");
                //return;
            }
        },
  
        btnExport_click: function () {
            var that = this, controls = that.getControls();

            if (SessionBOCPostpaid.FlagExport == 'T') {
                ShowDataLoadingMessage(controls);
                ExportExcel(that, controls, '');
            } else {
                var param = {
                    "strIdSession": SessionBOCPostpaid.IdSession,
                    'pag': '1',
                    'opcion': 'gConstEvtExportarDetalleLlamada',
                    'telefono': SessionBOCPostpaid.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'E'
                };
                ValidateAccess(that, controls, "E", '', '', param, "Postpaid");
                return;
            }
        },
         
        btnSave_Click: function () {
            var that = this, controls = that.getControls();

            if (!ValidateValues(controls)) {
                return false;
            }

            var email = "";
            if (controls.chkSendMail.is(':checked')) {
                email = controls.txtSendMail.val();

                if (email == "") {
                    alert('Debe indicar una dirección de correo, donde serán remitidas los datos procesados.',"Alerta");
                    return false;
                }

                if (!ValidateEmail(email)) {
                    return false;
                }
            }

            Save(that, controls); 
        },
       
        btnClose_click: function () {
            parent.window.close();
        },

        btnConstancy_Click: function () {
            var that = this, controls = this.getControls();
            
            ReadRecordSharedFile(SessionBOCPostpaid.IdSession,SessionBOCPostpaid.RutaPdf);
        },
                strUrl: '/Transactions/Postpaid/BilledOutCallDetail' 
        //strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
        //    (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))

    }

    $.fn.BilledCallDetail = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('BilledCallDetail'),
                options = $.extend({}, $.fn.BilledCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('BilledCallDetail', data);
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

    $.fn.BilledCallDetail.defaults = {

    }

    $('#divBody').BilledCallDetail();
})(jQuery);