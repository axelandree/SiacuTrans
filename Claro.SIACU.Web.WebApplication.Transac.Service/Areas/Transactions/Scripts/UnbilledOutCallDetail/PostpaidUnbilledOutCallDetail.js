var SessionPostpaidUOCD = {}; //SessionPostpaidUnbilledOutCallsDetail

function CloseValidation(obj, pag, controls) {
    var option = controls.hidOption.val();
    //obj.hidAccion = 'G';//Temporal
    ////console.logoption);
    if (obj.hidAccion == 'G') {
        ////console.logobj);
        if (option == "B") {
            Search_Calls(pag, controls);
        } else if (option == "E") {
            ExportExcel(pag, controls);
        }
    } else if (obj.hidAccion == 'F') {
        var mensaje = "La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.";
        alert(mensaje,"Alerta");
    }
}

function ExportExcel(pag, controls) {
    var strUrlModal = pag.strUrl + '/GetCallDetailExportExcel';
    var strUrlResult = '/Transactions/CommonServices/DownloadExcel';
    $.app.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify({
            idsession: SessionPostpaidUOCD.IdSession, phoneNumber: SessionPostpaidUOCD.Telephone,
            strStartDate: controls.txtStartDate.val(), strEndDate: controls.txtEndDate.val()
        }),
        success: function (path) {
            window.location = strUrlResult + '?strPath=' + path + "&strNewfileName=ReporteDeLlamadas.xlsx";
        }
    });
}

function Search_Calls(pag, controls) {

    var strUrlModal = pag.strUrl + '/Search';
    //ShowDataLoadingMessage(controls);
    //console.logstrUrlModal);
    alert('Se generará Tipificación por la información');
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        data: JSON.stringify({
            idSession: SessionPostpaidUOCD.IdSession, contactID: SessionPostpaidUOCD.ContactId, contractID: SessionPostpaidUOCD.ContractId,
            codesProfiles: SessionPostpaidUOCD.CodesProfiles, phone: SessionPostpaidUOCD.Telephone,
            strStartDate: controls.txtStartDate.val(), strEndDate: controls.txtEndDate.val(), security: SessionPostpaidUOCD.FlagSecurity,
            arrPermissions: SessionPostpaidUOCD.ArrayPageAccess, codePlanTariff: SessionPostpaidUOCD.CodePlanTariff, flagPlatform: SessionPostpaidUOCD.FlagPlatform
        }),
        success: function (response) {
            if (response.StatusCode == "OK") {
                controls.lblErrorMessage.hide();
            } else {
                controls.lblErrorMessage.show();
                controls.lblErrorMessage.html(response.StatusMessage);
                return;
            }

            var separador = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            var numRows = Object.keys(response.ListCallsDetail).length;
            if (numRows > 0) {
                controls.btnExport.removeAttr('disabled', 'disabled');
                controls.lblSummary.html("Total Registro : " + numRows + separador +
                    'Total General Minutos: ' + response.StrTotal + separador +
                    'Total General SMS:' + response.StrTotalSMS);
            } else {
                controls.btnExport.attr('disabled', 'disabled');
                controls.lblSummary.html('Total Registro: 0');
            }

            controls.tblDetailVisualizeCall.find('tbody').html('');

            controls.tblDetailVisualizeCall.DataTable({
                //"pagingType": "full_numbers", 
                //"ordering": false,
                //"pageLength": false,
                "scrollCollapse": true,
                "info": false,
                "select": 'single',
                "paging": false,
                "searching": false,
                "destroy": true,
                "scrollX": true,
                "scrollY": 300,
                "data": response.ListCallsDetail,
                "columns": [
                       { "data": "StrOrder" },
                       { "data": "StrDate" },
                       { "data": "StrHour" },
                       { "data": "DestinationPhone" },
                       { "data": "StrQuantity" },
                       { "data": "OriginalAmount" },
                       { "data": "TariffPlan" },
                       { "data": "Tariff" },
                       { "data": "Type" },
                       { "data": "Tariff_Zone" },
                       { "data": "Operator" },
                       { "data": "Horary" },
                       { "data": "CallType" },
                       { "data": "FinalAmount" },
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)",
                },
            });


        },
        error: function (errormessage) {
            alert('Error',"Alerta");
        }
    });
}

function ShowDataLoadingMessage(controls) {

    var strUrlLogo = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';

    $.blockUI({
       
        message: '<div align="center"><img src="' + strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
    var startDate = new Date(partsSSD[2], parseInt(partsSSD[1])-1, partsSSD[0]);
    var endDate = new Date(partsSED[2], parseInt(partsSED[1])-1, partsSED[0]);

    if (startDate.getTime() > endDate.getTime()) {
        alert("La Fecha de Inicio debe ser menor o igual que la Fecha Fin","Alerta");
        return false;
    }

    //if (SessionPostpaidUOCD.MinimumDate.getTime() > startDate.getTime()) {
    //    alert("La Fecha de Inicio debe ser mayor o igual que " + SessionPostpaidUOCD.StrMinimumDate)
    //    return false;
    //}
    if (endDate.getTime() > SessionPostpaidUOCD.MaximumDate.getTime()) {
        alert("La Fecha Fin debe ser menor o igual que " + SessionPostpaidUOCD.StrMaximumDate,"Alerta")
        return false;
    }

    return true;
}

(function ($, undefined) {

    var Form = function ($element, options) {

        $.extend(this, $.fn.OutgoingCallNBdDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , lblTitle: $('#lblTitle', $element)
          , txtStartDate: $('#txtDateStar', $element)
          , lblTelephone: $('#lblTelephone', $element)
          , lblSummary: $('#lblSummary', $element)
          , lblErrorMessage: $('#lblErrorMessage', $element)
          , txtEndDate: $('#txtDateEnd', $element)
          , btnSearch: $('#btnSearch', $element)
          , btnClose: $('#btnClose', $element)
          , btnExport: $('#btnExport', $element)
          , tblDetailVisualizeCall: $('#tblDetailVisualizeCall', $element) 
          , hidOption: $('#hidOpcion', $element)
          , spnMainTitle: $('#spnMainTitle')
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();

            controls.btnSearch.addEvent(that, 'click', that.btnSearch_Click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_Click);
            //controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy' });
            //controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy' });
            controls.btnExport.attr('disabled', 'disabled');
            controls.lblErrorMessage.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },
        render: function () {
            var that = this, controls = this.getControls();
            ShowDataLoadingMessage(controls);
            that.getLoadDetailVisualizeCall();
            that.loadSessionData();
            that.loadForm();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        loadSessionData: function () {
            //Redirect ini  2.0
            var that = this, controls = this.getControls();

            controls.lblTitle.text("Detalle de Llamadas Salientes No Facturado");
            //console.log"Redireccionó a la Transacion");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);
            //Redirect fin  

            SessionPostpaidUOCD.PageAccess = SessionTransac.SessionParams.USERACCESS.optionPermissions;
            SessionPostpaidUOCD.ArrayPageAccess = SessionPostpaidUOCD.PageAccess.split(',');
            SessionPostpaidUOCD.IdSession = SessionTransac.SessionParams.USERACCESS.login;
            SessionPostpaidUOCD.CodesProfiles = SessionTransac.SessionParams.USERACCESS.profiles;

            SessionPostpaidUOCD.ContractId = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            SessionPostpaidUOCD.ContactId = SessionTransac.SessionParams.DATACUSTOMER.ContactCode;
            SessionPostpaidUOCD.BillingCycle = SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
            SessionPostpaidUOCD.Telephone = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
            
            SessionPostpaidUOCD.FlagPlatform = SessionTransac.SessionParams.DATASERVICE.FlagPlatform;
            SessionPostpaidUOCD.CodePlanTariff = SessionTransac.SessionParams.DATASERVICE.CodePlanTariff;
            //SessionPostpaidUOCD.FlagPlatform = "Pers"; //Temporal

            $('#lblCustomerName').text(SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            $('#lblContactCustomer').text(SessionTransac.SessionParams.DATACUSTOMER.CustomerContact);
            $('#lblContact').html(SessionTransac.SessionParams.DATACUSTOMER.FullName);
            $('#lblLegalRepresentative').html(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            $('#lblIdentificationDocument').html(SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            $('#lblIdentificationDocumentoLR').html(SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
            $('#lblActivationDate').html(SessionTransac.SessionParams.DATACUSTOMER.ActivationDate);

            SessionPostpaidUOCD.FlagSecurity = "NF";
            SessionPostpaidUOCD.FlagExport = "R";//Restricted
            SessionPostpaidUOCD.FlagSearch = "R";//Restricted
            SessionPostpaidUOCD.StrMinimumDate = "";
            SessionPostpaidUOCD.StrMaximumDate = "";
            SessionPostpaidUOCD.MinimumDate = new Date();
            SessionPostpaidUOCD.MaximumDate = new Date();

            //console.logSessionPostpaidUOCD);

            //SessionPostpaidUOCD.PageAccess = "ACP_RDL|ACP_EJM|,ACP_VTD,|ACP_RBR|ACP_ECL|ACP_RBL";
            //SessionPostpaidUOCD.ArrayPageAccess = SessionPostpaidUOCD.PageAccess.split(',');
            //SessionPostpaidUOCD.IdSession = "user_adminca";
            //SessionPostpaidUOCD.CodesProfiles = "1,43,90,2,82,81";

            //SessionPostpaidUOCD.ContractId = "799786";
            //SessionPostpaidUOCD.ContactId = "127892";
            //SessionPostpaidUOCD.BillingCycle = "15";
            //SessionPostpaidUOCD.Telephone = "987654500";

            //SessionPostpaidUOCD.FlagPlatform = "P";
            //SessionPostpaidUOCD.CodePlanTariff = "";
        },
        loadForm: function () {
            var that = this, controls = this.getControls();
            var strUrlModal = that.strUrl + '/Load';
            controls.lblTelephone.html(SessionPostpaidUOCD.Telephone);
              
            if (SessionPostpaidUOCD.FlagPlatform == null || SessionPostpaidUOCD.CodePlanTariff == null) {
                controls.btnSearch.attr('disabled', 'disabled');
                alert('No se cargarón correctamente los datos para el servicio',"Alerta"); 
                return;
            }
            //console.log"URL: " + strUrlModal);//Temporal
            var params={
                idSession: SessionPostpaidUOCD.IdSession,
                arrPermissions: SessionPostpaidUOCD.ArrayPageAccess,
                flagPlatform: SessionPostpaidUOCD.FlagPlatform,
                billingCicle: SessionPostpaidUOCD.BillingCycle,
                codePlanTariff: SessionPostpaidUOCD.CodePlanTariff,
                clientAccount: SessionPostpaidUOCD.ContractId
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
                        //controls.txtStartDate.datepicker('setDate', result.StrStartDate);
                        //controls.txtEndDate.datepicker('setDate', result.StrEndDate);
                        SessionPostpaidUOCD.FlagSecurity = result.FlagSecurity;
                        SessionPostpaidUOCD.FlagExport = result.FlagAuthorization_Export;
                        SessionPostpaidUOCD.FlagSearch = result.FlagAuthorization_Search;
                        SessionPostpaidUOCD.StrMinimumDate = result.StrMinimumDate;
                        SessionPostpaidUOCD.StrMaximumDate = result.StrMaximumDate;

                        //SessionPostpaidUOCD.FlagSearch = "F";//Temporal
                        //SessionPostpaidUOCD.FlagExport = "F";//Temporal
                        // ===== Convirtiendo fechas usando cadenas con formatos dd/MM/yyy =====
                        var partsSMinD = SessionPostpaidUOCD.StrMinimumDate.split("/");
                        var partsSMaxD = SessionPostpaidUOCD.StrMaximumDate.split("/");
                        SessionPostpaidUOCD.MinimumDate = new Date(partsSMinD[2], parseInt(partsSMinD[1]) - 1, partsSMinD[0]);
                        SessionPostpaidUOCD.MaximumDate = new Date(partsSMaxD[2], parseInt(partsSMaxD[1]) - 1, partsSMaxD[0]);
                        var dateToday = new Date(); 
                        var diffDate = dateToday - SessionPostpaidUOCD.MinimumDate;
                        var strDias = 90;// Math.floor(diffDate / (1000 * 60 * 60 * 24));
                        var blockedDays = '-' + strDias + 'd';
                        //console.logstrDias);
                        controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', startDate: blockedDays, endDate: '0d' }); 
                        //if (controls.txtEndDate.val() == "") {
                        //    blockedDays = '-' + (strDias + 1) + 'd';  
                        //}
                        controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy', startDate: blockedDays, endDate: '0d' });
                        controls.txtStartDate.datepicker('setDate', result.StrStartDate);
                        controls.txtEndDate.datepicker('setDate', result.StrEndDate);  
                        // ========================= Fin de Conversion =========================
                        if (result.FlagShow_Export == "T") {
                            controls.btnExport.show();
                        }
                    }
                },
                error: function (errormessage) {
                    alert('Error: ' + errormessage,"Alerta");
                }
            });
        },
        getLoadDetailVisualizeCall: function () {
            var controls = this.getControls();

            controls.tblDetailVisualizeCall.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollX: true,
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

        btnClose_Click: function () {
            parent.window.close();
        }, 
        btnExport_Click: function () {
            var that = this, controls = this.getControls();

            if (SessionPostpaidUOCD.FlagExport == "T") {
                ShowDataLoadingMessage(controls);
                ExportExcel(that, controls)
            } else {
                var param = {
                    "strIdSession": SessionPostpaidUOCD.IdSession,
                    'pag': '1',
                    'opcion': 'gConstEvtExportarDetaLlamadaInt',
                    'telefono': SessionPostpaidUOCD.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'E'
                };
                ValidateAccess(that, controls, "E", '', '', param, "Postpaid");
                return;
            }
        },
        btnSearch_Click: function () {
            var that = this, controls = this.getControls();

            //Validando Fechas
            if (ValidateDates(controls.txtStartDate.val(), controls.txtEndDate.val()) == false) {
                return;
            }
            ////console.logSessionPostpaidUOCD.FlagSearch);
            if (SessionPostpaidUOCD.FlagSearch == "T") {
                ShowDataLoadingMessage(controls);
                Search_Calls(that, controls);
            } else {

                var param = {
                    "strIdSession": SessionPostpaidUOCD.IdSession,
                    'pag': '1',
                    'opcion': 'gConstEvtBuscarDetaLlamadaInt',
                    'telefono': SessionPostpaidUOCD.Telephone,
                    'transaccion': '',
                    'DetEntAccion': 'B'
                };
                ValidateAccess(that, controls, "B", '', '', param, "Postpaid");
                return;
            }
        }, 
        strUrl: '/Transactions/Postpaid/UnbilledOutCallDetail'
        //strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
        //    (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))
    };



    $.fn.OutgoingCallNBdDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('OutgoingCallNotBilledDetail'),
                options = $.extend({}, $.fn.OutgoingCallNBdDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('OutgoingCallNotBilledDetail', data);
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
    $.fn.OutgoingCallNBdDetail.defaults = {
    }

    $('#divBody').OutgoingCallNBdDetail();
})(jQuery);