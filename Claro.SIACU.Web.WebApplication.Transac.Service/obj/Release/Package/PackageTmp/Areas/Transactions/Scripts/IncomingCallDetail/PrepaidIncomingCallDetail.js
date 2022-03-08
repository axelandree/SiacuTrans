var SessionIncomingCallPre = {};

function ChanguedStatusButtons(controls, flagStatus) {
    if (flagStatus == "E") {
        controls.btnSave.removeAttr('disabled', 'disabled');
        controls.btnPrint.removeAttr('disabled', 'disabled');
        controls.btnExport.removeAttr('disabled', 'disabled');
        controls.btnConstancy.removeAttr('disabled', 'disabled');
    } else {
        controls.btnSave.attr('disabled', 'disabled');
        controls.btnPrint.attr('disabled', 'disabled');
        controls.btnExport.attr('disabled', 'disabled');
        controls.btnConstancy.attr('disabled', 'disabled');
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
        }
    } else if (obj.hidAccion == 'F') {
        var mensaje = "La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.";
        alert(mensaje,"Alerta");
    }
}

function ExportExcel(pag, controls, userAdmin) {
    var strUrlModal = pag.strUrl + '/GetPathExportExcel';
    var strUrlResult = '/Transactions/CommonServices/DownloadExcel'; 

    var nameCACDAC = $('#ddlCACDAC option:selected').text();

    var parameters = {
        idsession: SessionIncomingCallPre.IdSession,
        idContact: SessionIncomingCallPre.Client.IdContact, 
        nameClient: SessionIncomingCallPre.Client.FullName,
        nameCACDAC: nameCACDAC,
        flagLoadDataline: SessionIncomingCallPre.FlagLoadDataline,
        flagExport: SessionIncomingCallPre.FlagExport,
        userAdmin: userAdmin,
        clientAccount: SessionIncomingCallPre.Client.Account
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
                SessionIncomingCallPre.HidFlagCharge = result.HidFlagCharge;
                window.location = strUrlResult + '?strPath=' + result.PathExcel + "&strNewfileName=ReporteDeLlamadas.xlsx";
                if (result.AlertMessage != "") {
                    alert(result.AlertMessage,"Informativo");
                } 
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
        //"scrollCollapse": true,
        "info": false,
        "select": 'single',
        "paging": false,
        "searching": false,
        "destroy": true,
        "scrollY": 300,
        "data": list,
        "columns": [
               { "data": "NroOrd" },
               { "data": "NumberA" },
               { "data": "Date" },
               { "data": "StartHour" },
               { "data": "NumberB" },
               { "data": "Duration" },
        ],
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página.",
            "zeroRecords": "No existen datos",
            "info": " ",
            "infoEmpty": " ",
            //"infoFiltered": "(filtered from _MAX_ total records)",
        },
    });
}

//function GenerateConstancy(pag, controls) {
//    var strUrlModal = pag.strUrl + '/GenerateContancy';
    
//    var parameters = {
//        idSession: SessionIncomingCallPre.IdSession,
//        nameCACDAC: $('#ddlCACDAC option:selected').text(),
//        hidInteraction: SessionIncomingCallPre.HidInteraction,
//        client: SessionIncomingCallPre.Client,
//        strNotes: controls.txtaNotes.val()
//    };
//    //console.logstrUrlModal);
//    $.app.ajax({
//        type: 'POST',
//        contentType: "application/json; charset=utf-8",
//        dataType: 'JSON',
//        url: strUrlModal,
//        data: JSON.stringify(parameters),
//        success: function (result) {
//            if (result.StatusCode == "OK") {
//                ReadRecordSharedFile(Session.IdSession, result.FullPathPDF);
//                //controls.divErrorAlert.hide();
//                //var url = '/Transactions/GenerateRecord/showInvoice';
//                //var index = result.FullPathPDF.lastIndexOf("\\")+1;
//                //var filename = result.FullPathPDF.substring(index);
//                //window.open(url + "?strFilePath=" + result.FullPathPDF + "&strFileName=" + filename + "&strNameForm=" + "NO" + "&strIdSession="
//                //    + SessionIncomingCallPre.IdSession, "FACTURA ELECTRÓNICA", 'popimpr',
//                //        'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=800, height=600');
//            } else {
//                controls.divErrorAlert.show();
//                controls.lblErrorMessage.text(result.StatusMessage);
//                return;
//            }
//        } 

//    });
//}

function Print(pag, controls, userAdmin) { 
    pag.Loading();
    var strUrlModal = pag.strUrl + '/GenerateDataForPrinting';
    //ShowDataLoadingMessage(controls);
    var parameters = {
        idSession: SessionIncomingCallPre.IdSession,
        strTelephone: SessionIncomingCallPre.Client.Telephone,
        flagPrint: SessionIncomingCallPre.FlagPrint,
        flagLoadDataline: SessionIncomingCallPre.FlagLoadDataline,
        strStartDate: controls.txtStartDate.val(),
        strEndDate: controls.txtEndDate.val(),
        strSenderEmail: SessionIncomingCallPre.SenderEmail,
        userAdmin: userAdmin,
        clientAccount: SessionIncomingCallPre.Client.Account
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
                SessionIncomingCallPre.HidFlagCharge = result.HidFlagCharge;
                var nameCACDAC = $('#ddlCACDAC option:selected').text();
                ////console.lognameCACDAC);
                strUrlModal = pag.strUrl + '/PrepaidIncomingCallDetailPrint?nameClient=' + SessionIncomingCallPre.Client.FullName + '&nameCACDAC=' + nameCACDAC;
                var viewPrint = window.open(strUrlModal, '_blank', 'directories=no, location=no, menubar=no, scrollbars=no, statusbar=no, tittlebar=no, width=850, height=650');
                if (result.AlertMessage != "") {
                    alert(result.AlertMessage,"Informativo");
                }
            } else {
                controls.divErrorAlert.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                return;
            }
        }
        //,
        //error: function (errormessage) {
        //    alert('Error: ' + errormessage);
        //}

    });
}

function Save(pag, controls) {
    if (SessionIncomingCallPre.HidTotalRegistration == "0") {
        alert('No existen registros para grabar.',"Alerta");
        return;
    }

    if (ValidateDates(controls.txtStartDate.val(), controls.txtEndDate.val()) == false) {
        return;
    }

    if (controls.ddlCACDAC.val() == "") {
        alert('Ingresar el Punto de Atención.',"Alerta");
        return;
    }

    confirm("¿Está seguro de guardar los cambios?", "Confirmar", function () {
        pag.Loading();
         
        var strUrlModal = pag.strUrl + '/Save';
        var flagGenerateOCC = "F";
        if (controls.chkGenerateOCC.prop('checked')) {
            flagGenerateOCC = 'T';
        }
        var nameCACDAC = $('#ddlCACDAC option:selected').text();
        $.ajax({
            //async: false,
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            url: strUrlModal,
            data: JSON.stringify({
                idSession: SessionIncomingCallPre.IdSession, client: SessionIncomingCallPre.Client, flagLoadDataline: SessionIncomingCallPre.FlagLoadDataline,
                flagGenerateOCC: flagGenerateOCC, idCACDAC: controls.ddlCACDAC.val(),nameCACDAC:nameCACDAC, strNotes: controls.txtaNotes.val(),
                strStartDate: controls.txtStartDate.val(), strEndDate: controls.txtEndDate.val()
            }),
            success: function (result) {
                if (result.StatusCode == "OK") {
                    controls.divErrorAlert.hide(); 
                    SessionIncomingCallPre.HidInteraction = result.HidInteraction;
                    SessionIncomingCallPre.Amount = result.Amount;
                    SessionIncomingCallPre.MainBalance = result.MainBalance;
                    SessionIncomingCallPre.FullPathPDF = result.FullPathPDF;

                    controls.btnConsult.attr('disabled', 'disabled');
                    controls.btnSave.attr('disabled', 'disabled');
                    controls.chkGenerateOCC.attr('disabled', 'disabled');

                    controls.btnExport.removeAttr('disabled', 'disabled');
                    controls.btnPrint.removeAttr('disabled', 'disabled');
                    controls.btnConstancy.removeAttr('disabled', 'disabled');

                    ////console.logSessionIncomingCallPre.HidInteraction); 
                } else {
                    controls.divErrorAlert.show();
                    controls.lblErrorMessage.text(result.StatusMessage);
                    ChanguedStatusButtons(controls, 'D');
                }
            }
            //,
            //error: function (errormessage) {
            //    alert('Error: ' + errormessage);
            //}
        });
    });

}

function Search_CallsDetail(pag, controls) {
     
    if (ValidateDates(controls.txtStartDate.val(), controls.txtEndDate.val()) == false) {
        return;
    } 
    pag.Loading();

    var flagGenerateOCC = "F";
    if (controls.chkGenerateOCC.prop('checked')) {
        flagGenerateOCC = 'T';
    }

    var strUrlModal = pag.strUrl + '/Search';
    var params = {
        idSession: SessionIncomingCallPre.IdSession,
        idContact: SessionIncomingCallPre.Client.IdContact,
        strPhone: SessionIncomingCallPre.Client.Telephone,
        flagLoadDataline: SessionIncomingCallPre.FlagLoadDataline,
        strNotes: controls.txtaNotes.val(),
        flagGenerateOCC: flagGenerateOCC,
        flagPlatform: SessionIncomingCallPre.FlagPlatform,
        strStartDate: controls.txtStartDate.val(),
        strEndDate: controls.txtEndDate.val()
    };


    alert('Se generará Tipificación por la información',"Alerta");
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
                FillData_Datatable(controls.tblIncomingCalls, result.ListCallsDetail);
                controls.divErrorAlert.hide(); 
                controls.btnSave.removeAttr('disabled', 'disabled');
                controls.lblSummary.text('Total Registros: ' + Object.keys(result.ListCallsDetail).length);
                SessionIncomingCallPre.HidTotalRegistration = Object.keys(result.ListCallsDetail).length; 
                if (SessionIncomingCallPre.HidTotalRegistration > 0) { controls.btnSave.removeAttr('disabled', 'disabled'); }
                else { controls.btnSave.attr('disabled', 'disabled') }
            } else {
                controls.tblIncomingCalls.find('tbody').html('');
                controls.divErrorAlert.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                ChanguedStatusButtons(controls, 'D');
                controls.lblSummary.text('Total Registros: 0');
            }
        }
        //,
        //error: function (errormessage) {
        //    alert('Error: ' + errormessage);
        //}
    });
}

function SetInitialProperties_Datatable(tblTable) {
    tblTable.DataTable({
        info: false,
        select: "single",
        paging: false,
        searching: false,
        //scrollX: true,
        scrollY: 300,
        scrollCollapse: true,
        destroy: true,
        language: {
            lengthMenu: "Mostrar _MENU_ registros por página.",
            zeroRecords: "No existen datos",
            info: " ",
            infoEmpty: " ",
            infoFiltered: "(filtered from _MAX_ total records)"
        }
    });
}
 
function ValidateDates(strStartDate, strEndDate) {
    if (strStartDate == "") {
        alert("Debe Ingresar una Fecha Inicio valida.","Alerta");
        return false;
    }
    if (strEndDate == "") {
        alert("Debe Ingresar la Fecha Fin valida.","Alerta");
        return false;
    }

    var partsSSD = strStartDate.split("/");
    var partsSED = strEndDate.split("/");
    var startDate = new Date(partsSSD[2], partsSSD[1], partsSSD[0]);
    var endDate = new Date(partsSED[2], partsSED[1], partsSED[0]);

    if (startDate.getTime() > endDate.getTime()) {
        alert("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.","Alerta");
        return false;
    }
     
    if (SessionIncomingCallPre.MinimumDate.getTime() > startDate.getTime()) {
        alert("La Fecha de Inicio debe ser mayor o igual que " + SessionIncomingCallPre.StrMinimumDate, "Alerta");
        return false;
    }
    if (endDate.getTime() > SessionIncomingCallPre.MaximumDate.getTime()) {
        alert("La Fecha Fin debe ser menor o igual que " + SessionIncomingCallPre.StrMaximumDate,"Alerta");
        return false;
    }

    return true;
}

(function ($, undefined) { 
   
    var Form = function ($element, options) {

        $.extend(this, $.fn.IncomingCallDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblTitle: $('#lblTitle', $element)  
            , lblSummary: $('#lblSummary', $element)
            , txtStartDate: $('#txtStartDate', $element)
            , txtEndDate: $('#txtEndDate', $element)
            , hidOption: $('#hidOpcion', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , divErrorAlert: $('#divErrorAlert', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , chkGenerateOCC: $('#chkGenerateOCC', $element)
            , tblIncomingCalls: $('#tblIncomingCalls', $element)
            , txtaNotes: $('#txtaNotas', $element)
            , txtOCC: $('#txtOCC', $element)
            , myModalLoad: $('#myModalLoad', $element)
            , btnConsult: $('#btnConsult', $element)
            , btnSave: $('#btnSave', $element)
            , btnPrint: $('#btnPrint', $element)
            , btnExport: $('#btnExport', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            //controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy' });
            //controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnConsult.addEvent(that, 'click', that.btnConsult_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.chkGenerateOCC.addEvent(that, 'change', that.chkGenerateOCC_change);
            controls.txtOCC.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            controls.divErrorAlert.hide();
            ChanguedStatusButtons(controls, 'D');
           
            that.render(); 
        },
        loadSessionData: function () {
            //Redirect ini  2.0
            var that = this, controls = this.getControls();

            controls.lblTitle.text("DETALLE DE LLAMADAS ENTRANTES");
            //console.log"Redireccionó a la Transacion");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);
            //Redirect fin  

            SessionIncomingCallPre.IdSession = SessionTransac.SessionParams.USERACCESS.login;
            SessionIncomingCallPre.PageAccess = SessionTransac.SessionParams.USERACCESS.optionPermissions;
            SessionIncomingCallPre.ArrayPageAccess = SessionIncomingCallPre.PageAccess.split(',');
            SessionIncomingCallPre.Client = {};
            SessionIncomingCallPre.Client.FullName = SessionTransac.SessionParams.DATACUSTOMER.FullName;
            SessionIncomingCallPre.Client.FirstName = SessionTransac.SessionParams.DATACUSTOMER.Name;
            SessionIncomingCallPre.Client.LastName = SessionTransac.SessionParams.DATACUSTOMER.LastName;
            SessionIncomingCallPre.Client.DocumentType = SessionTransac.SessionParams.DATACUSTOMER.TypeDocument;
            SessionIncomingCallPre.Client.NumberDocument = SessionTransac.SessionParams.DATACUSTOMER.NumberDocument;
            SessionIncomingCallPre.Client.LegalRepresentative = SessionTransac.SessionParams.DATACUSTOMER.LegalAgent;
            SessionIncomingCallPre.Client.Telephone = SessionTransac.SessionParams.DATACUSTOMER.TelephoneCustomer;
            SessionIncomingCallPre.Client.IdContact = SessionTransac.SessionParams.DATACUSTOMER.ContactCode;
            SessionIncomingCallPre.Client.Account = SessionTransac.SessionParams.DATACUSTOMER.CustomerCode; 

            //SessionIncomingCallPre.IdSession = "user_adminca"; 
            //SessionIncomingCallPre.PageAccess = "SIACA_EXP|SIACA_IMP";
            //SessionIncomingCallPre.ArrayPageAccess = SessionIncomingCallPre.PageAccess.split(',');
            //SessionIncomingCallPre.Client = {};
            //SessionIncomingCallPre.Client.FullName = "Luis Cochachi";
            //SessionIncomingCallPre.Client.Telephone = "988202024";
            //SessionIncomingCallPre.Client.IdContact = "268582231";
            //SessionIncomingCallPre.Client.Account = "CuentaCliente";

           

            SessionIncomingCallPre.FlagExport = "R"; //Restricted
            SessionIncomingCallPre.FlagPrint = "R"; //Restricted
            SessionIncomingCallPre.StrMinimumDate = "";
            SessionIncomingCallPre.StrMaximumDate = "";
            SessionIncomingCallPre.MinimumDate = new Date();
            SessionIncomingCallPre.MaximumDate = new Date();
            SessionIncomingCallPre.FlagLoadDataline = "0";
            SessionIncomingCallPre.HidInteraction = "";
            SessionIncomingCallPre.HidFlagCharge = "T";
            SessionIncomingCallPre.HidTotalRegistration = "0";
            SessionIncomingCallPre.Amount = 0 ;
            SessionIncomingCallPre.MainBalance = 0;
            SessionIncomingCallPre.FlagPlatform = "";
            SessionIncomingCallPre.FullPathPDF = "";

            //SessionIncomingCallPre.FlagPlatform = "Pers";//Temporal  
             
            $('#lblCustomerName').text(SessionTransac.SessionParams.DATACUSTOMER.FullName);
            $('#lblContactCustomer').text(SessionTransac.SessionParams.DATACUSTOMER.CustomerCode);
            $('#lblContact').html(SessionTransac.SessionParams.DATACUSTOMER.ContactCode);
    
            $('#lblAddressCustomer').html(SessionTransac.SessionParams.DATACUSTOMER.Address);
            $('#lblEmailCustomer').html(SessionTransac.SessionParams.DATACUSTOMER.Email);
            $('#lblIdentificationDocument').html(SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            $('#lblIdentificationDocumentoLR').html(SessionTransac.SessionParams.DATACUSTOMER.NumberDocument);
            $('#lblActivationDate').html(SessionTransac.SessionParams.DATASERVICE.DateActivation);

            ////console.logSessionTransac);
        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this, controls = this.getControls();
             
            that.loadSessionData();
            that.loadForm();
            that.Loading();
            that.getCACDAC();
            SetInitialProperties_Datatable(controls.tblIncomingCalls);

            //that.getCustomerData(); 
        },
        loadForm: function () {
            var that = this, controls = this.getControls();
            //var fechahoy = new Date();
          
            //var day = fechahoy.getDate();
          
            //var month = fechahoy.getMonth() +1;
          
            //var year = fechahoy.getFullYear();
        
            //if (day < 10)
            //{
            //    day='0'+day
            //}
            //if (month < 10)
            //{
            //    month='0'+month
            //}
           
            //var fecha = day + '/' + month + '/' + year;
            //$('#txtStartDate').attr("placeholder",fecha);
            //$('#txtEndDate').attr("placeholder", fecha);
           
            var flagGenerateOCC = "F";
            if (controls.chkGenerateOCC.prop('checked')) {
                flagGenerateOCC = 'T';
            }
            var params = {
                idSession: SessionIncomingCallPre.IdSession,
                strPhone: SessionIncomingCallPre.Client.Telephone,
                clientFullName: SessionIncomingCallPre.Client.FullName,
                flagGenerateOCC: flagGenerateOCC,
                arrPermissions: SessionIncomingCallPre.ArrayPageAccess,
                flagPlatform: SessionIncomingCallPre.FlagPlatform,
            };

            var strUrlModal = that.strUrl + '/Load';
            //console.logstrUrlModal);
          
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
                        //controls.txtStartDate.datepicker('setDate', result.StrStartDate);
                        //controls.txtEndDate.datepicker('setDate', result.StrEndDate);
                        SessionIncomingCallPre.FlagExport = result.FlagAuthorization_Export;
                        SessionIncomingCallPre.FlagPrint = result.FlagAuthorization_Print;
                        SessionIncomingCallPre.StrMinimumDate = result.StrMinimumDate;
                        SessionIncomingCallPre.StrMaximumDate = result.StrMaximumDate;

                        SessionIncomingCallPre.FlagLoadDataLine = result.FlagLoadDataline; 
                        SessionIncomingCallPre.HidFlagCharge = result.HidFlagCharge;
                        //SessionIncomingCallPre.FlagPrint = 'F';//Temporal
                        //SessionIncomingCallPre.FlagExport = 'F';//Temporal

                        //Convirtiendo fechas usando cadenas con formatos dd/MM/yyy
                        var partsSMinD = SessionIncomingCallPre.StrMinimumDate.split("/");
                        var partsSMaxD = SessionIncomingCallPre.StrMaximumDate.split("/");
                        SessionIncomingCallPre.MinimumDate = new Date(partsSMinD[2], partsSMinD[1], partsSMinD[0]);
                        SessionIncomingCallPre.MaximumDate = new Date(partsSMaxD[2], partsSMaxD[1], partsSMaxD[0]);

                        var dateToday = new Date();
                        var diffDate = dateToday - SessionIncomingCallPre.MinimumDate;
                        var strDias = '90';//Math.floor(diffDate / (1000 * 60 * 60 * 24));
                        var blockedDays = '-' + strDias + 'd';
                        //console.logstrDias);
                        controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy', startDate: blockedDays, endDate: '0d' });
                        controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', startDate: blockedDays, endDate: '0d' });
                        controls.txtStartDate.datepicker('setDate', result.StrStartDate);
                        controls.txtEndDate.datepicker('setDate', result.StrEndDate);
                        //================= Llenando Controles ================= 

                    } else {
                        controls.btnConsult.attr('disabled', 'disabled');
                        alert(result.StatusMessage,"Alerta"); 
                        return;
                    }
                }
                //,
                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });
        },
        getCACDAC: function () {
            var that = this, controls = this.getControls();
            var objCacDacType = {
                strIdSession: SessionIncomingCallPre.IdSession
            };

            var strUrlModal = '/Transactions/CommonServices/GetCacDacType';

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: strUrlModal,
                success: function (response) {
                    controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));

                    if (response.data != null) {
                        $.each(response.data.CacDacTypes, function (index, value) {
                            controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },
        Loading: function () {
            var that = this;
            var controls = that.getControls();


            $.blockUI({
                message: '<div align="center"><img src="' + that.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
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


        btnConsult_click: function () {
            var that = this, controls = this.getControls();
            //that.Loading();
            Search_CallsDetail(that, controls);
        },
        chkGenerateOCC_change: function () {
            var that = this, controls = this.getControls();
             
            if (controls.chkGenerateOCC.prop('checked')) {
                controls.txtOCC.removeAttr('disabled', 'disabled'); 
            } else {
                controls.txtOCC.attr('disabled', 'disabled');
            }
        },
        btnSave_click: function () {
            var that = this, controls = this.getControls();

            Save(that, controls); 
        },

        btnPrint_click: function () {
            var that = this, controls = this.getControls(); 

            if ((SessionIncomingCallPre.MainBalance < SessionIncomingCallPre.Amount) &&
                SessionIncomingCallPre.HidFlagCharge == 'T') {
                alert('No se puede realizar la recarga, debido a que no tiene suficiente saldo.',"Alerta");
                return false;
            }

            if (SessionIncomingCallPre.FlagPrint == "T") {
       
                that.Loading();
                Print(that, controls, '');
            } else { 
                var param = {
                    "strIdSession": SessionIncomingCallPre.IdSession,
                    'pag': '4',
                    'opcion': 'gConstkeyImprimirLLamadaEntranteAutorizada',
                    'telefono': SessionIncomingCallPre.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'I'
                };
                ValidateAccess(that, controls, "I", '', '', param, "Prepaid");
                return;
            }
        },
        btnExport_click: function () {
            var that = this, controls = this.getControls();

            if ((SessionIncomingCallPre.MainBalance < SessionIncomingCallPre.Amount) &&
                SessionIncomingCallPre.HidFlagCharge =='T') {
                alert('No se puede realizar la recarga, debido a que no tiene suficiente saldo.',"Alerta");
                return false;
            } 

            if (SessionIncomingCallPre.FlagExport == "T") {
               
                that.Loading();
                ExportExcel(that, controls,'');
            } else {
                ////console.logSessionIncomingCallPre.IdSession);
                var param = {
                    "strIdSession": SessionIncomingCallPre.IdSession,
                    'pag': '4',
                    'opcion': 'gConstkeyExportarLLamadaEntranteAutorizada',
                    'telefono': SessionIncomingCallPre.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'E'
                };
                ValidateAccess(that, controls, "E", '', '', param, "Prepaid");
                return;
            }
        },
        btnConstancy_click: function () {
            var that = this, controls = this.getControls(); 
            that.Loading(); 
            ReadRecordSharedFile(SessionIncomingCallPre.IdSession, SessionIncomingCallPre.FullPathPDF);
        },
        btnClose_click: function () {
            parent.window.close();
        },
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        strUrl: '/Transactions/Prepaid/IncomingCallDetail'
        //strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
        //    (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))
    }

    $.fn.IncomingCallDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PrepaidIncomingCallDetail'),
                options = $.extend({}, $.fn.IncomingCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PrepaidIncomingCallDetail', data);
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

    $.fn.IncomingCallDetail.defaults = {
    }

    $('#divBody').IncomingCallDetail();
})(jQuery);