function CloseValidation(obj, pag, controls) {
    //obj.hidAccion = 'G';
    if (obj.hidAccion === 'G') {
        var sUser = obj.hidUserValidator;
        FC_GrabarCommit(pag, controls, obj.NamesUserValidator, obj.EmailUserValidator);
    }
    var mensaje;
    if (obj.hidAccion == 'F') {
        var descripcion = $("#hidDescripcionProceso_Validar").val();
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '') {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }
        alert(mensaje,"Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
};

function FC_GrabarCommit(pag, controls, NamesUserValidator, EmailUserValidator) {
    document.getElementById('hidAccion').value = '';

    pag.GetCallOutDetailsGetUserValidator(NamesUserValidator, EmailUserValidator)
    if (document.getElementById("hidOpcion").value == 'EXP') {
        pag.GetCallOutDetailsGetExport();
    } else if (document.getElementById("hidOpcion").value == 'BUS') {
        pag.GetCallOutDetailsGetSearch();
    }
    document.getElementById("hidOpcion").value = "";
};

function FC_Fallo() {
    $("#lblErrorMessage").html("ERROR: Ocurrió un problema al validar las credenciales. Vuelva a intentarlo.");
    $("#divErrorAlert").show();
    alert("ERROR: Ocurrió un problema al validar las credenciales. Vuelva a intentarlo.","Alerta");
};

function FormatDateToString(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
};

(function ($, undefined) {
 
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

    var SessionBilledCall = function () { };
    SessionBilledCall.strIdSession = "145452862";
    SessionBilledCall.strPhone = SessionTransac.SessionParams.DATACUSTOMER.TelephoneCustomer;
    Session.TELEPHONE =SessionBilledCall.strPhone;
    SessionBilledCall.CurrentUser = SessionTransac.SessionParams.USERACCESS.login;//
    SessionBilledCall.Names = SessionTransac.SessionParams.DATACUSTOMER.FullName;// 
    SessionBilledCall.ApPart = SessionTransac.SessionParams.DATACUSTOMER.Lastname;//
    SessionBilledCall.Cuenta =  SessionTransac.SessionParams.DATACUSTOMER.Account;
    SessionBilledCall.CurrentTerminal = null;
    SessionBilledCall.hidBolsa = null;
    SessionBilledCall.ObjidContact = SessionTransac.SessionParams.DATACUSTOMER.ContactCode;
    SessionBilledCall.IsTFI =SessionTransac.SessionParams.DATASERVICE.IsTFI.toString() == "true" ? 'SI' : 'NO';
    SessionBilledCall.hidPerfilBuscar = null;
    SessionBilledCall.hidPerfilExportar = null;
    SessionBilledCall.AccesPage = SessionTransac.SessionParams.USERACCESS.optionPermissions;
    SessionBilledCall.gConstkeyExpDetCallBot = null;
    SessionBilledCall.gConstkeyExpDetCallAut = null;
    SessionBilledCall.gConstkeySearchDetCallAut = null;
    SessionBilledCall.ddlTraficType = "TODOS";
    SessionBilledCall.EmpyEmail = SessionTransac.SessionParams.DATACUSTOMER.Email;
    SessionBilledCall.ClaseId = null;
    SessionBilledCall.SubClaseId = null;
    SessionBilledCall.TipoDes = null;
    SessionBilledCall.ClaseDes = null;
    SessionBilledCall.SubClaseDes = null;
    SessionBilledCall.PhonfNroGener = null;
    SessionBilledCall.colummvis = null;
    SessionBilledCall.ObjectData = null;
    SessionBilledCall.NamesUserValidator = null;
    SessionBilledCall.EmailUserValidator = null;

    //console.log"Redireccionó a la Transacion - PrepaidBilledOutCallDetail");
    //console.logSessionTransac);

    var Form = function ($element, options) {
        $.extend(this, $.fn.BilledOutCallDetails.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
          , lblCustomer: $('#lblCustomerName', $element)
          , lblContactCustomer: $('#lblContactCustomer', $element)
          , lblContact: $('#lblContact', $element)
          , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
          , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
          , lblIdentificationDocumentoLR: $('#lblIdentificationDocumentoLR', $element)
          , lblActivationDate: $('#lblActivationDate', $element)
          , txtStartDate: $('#txtStartDate', $element),
            lblPhone: $('#lblPhone', $element),
            txtEndDate: $('#txtEndDate', $element),
            btnSearch: $('#btnSearch', $element),
            btnClear: $('#btnClear', $element),
            btnClose: $('#btnClose', $element),
            btnExport: $('#btnExport', $element),
            ddlTraficType: $('#ddlTraficType', $element),
            tblBilledOutCalls: $('#tblBilledOutCalls', $element),
            lblResultMessag: $('#lblResultMessag', $element),

            lblErrorMessage: $('#lblErrorMessage', $element),
            divErrorAlert: $('#divErrorAlert', $element),

            lblTitle: $('#lblTitle', $element),
            chkAllRecords: $('#chkAllRecords', $element),
            myModalLoad: $('#myModalLoad', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_click);
            controls.btnClear.addEvent(that, 'click', that.btnClear_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_click);

            controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '+0d' });
            controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '+0d' });
            $("#ddlTraficType > option[value='" + SessionBilledCall.ddlTraficType + "']").attr('selected', 'select');
            controls.chkAllRecords.addEvent(that, 'change', that.refreshRecords_change);
            controls.divErrorAlert.prop("style").display = "none"

            var dateEnd = new Date();
            var dateString = FormatDateToString(dateEnd);

            controls.txtStartDate.val(dateString);
            controls.txtEndDate.val(dateString);
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
        render: function () {
            var that = this, controls = this.getControls();
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

            that.Loading();
            controls.lblTitle.text("DETALLE DE LLAMADAS SALIENTES");

            that.InitGetMessage();
            that.GetCallOutDetailsLoad();

        },

        InitGetMessage: function () {
            var that = this,
                objModel = {};
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrl = urlBase + "/GetMessage";
            objModel.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url:   "/transactions/Prepaid/BilledOutCallDetails/GetMessage",
                success: function (response) {
                    SessionBilledCall.gConstkeyExpDetCallBot = response[0];
                    SessionBilledCall.gConstkeyExpDetCallAut = response[1];
                    SessionBilledCall.gConstkeySearchDetCallAut = response[2];
                    that.EnablePermission();
                }
            });
        },

        EnablePermission: function () {
            var that = this
            var controls = this.getControls();
            var AccessPageProfile = SessionBilledCall.AccesPage;
            var PerfAccExp = 1;//AccessPageProfile.substr(0, AccessPageProfile.length).indexOf(SessionBilledCall.gConstkeyExpDetCallBot)// QUITAR
            if (PerfAccExp > -1) {
                controls.btnExport.show();
            }
            else
            {
                controls.btnExport.hide();
            }

            var hidPerfilBuscar = 1;//AccessPageProfile.indexOf(SessionBilledCall.gConstkeySearchDetCallAut);//QUITAR
            if (hidPerfilBuscar > 0) {
                SessionBilledCall.hidPerfilBuscar = "1";
            }
            else
            {
                SessionBilledCall.hidPerfilBuscar = "0";
            }
            var hidPerfilExportar = 1;//AccessPageProfile.indexOf(SessionBilledCall.gConstkeyExpDetCallAut) ;//QUITAR
            if (hidPerfilExportar > 0) {
                SessionBilledCall.hidPerfilExportar = "1";
            }
            else
            {
                SessionBilledCall.hidPerfilExportar = "0";
            }
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
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

        btnSearch_click: function () {
            var that = this, controls = this.getControls();
            //if (SessionBilledCall.hidPerfilBuscar == "1") {
            //    // var co = document.getElementById("hidCodOpcion").value;
            //    var param = {
            //        "strIdSession": "1234567",
            //        'pag': '5',
            //        'opcion': 'BUS',
            //        //'co': co,
            //        'telefono': SessionBilledCall.PhonfNroGener
            //    };

            //    ValidateAccess(that, controls, 'BUS', 'gConstkeyBuscarDetalleLLamadaAutorizada', '5', param, 'Prepaid');
            //    return;
            //}
            //else
            //{
            //    if (!that.ValidateDate()) {
            //        return;
            //    }
            //}



            that.GetCallOutDetailsGetSearch();
        },

        refreshRecords_change: function () {
            var that = this;
            that.tblBilledOutCalls_Load(SessionBilledCall.ObjectData);
        },

        GetCallOutDetailsGetUserValidator: function (NamesUserValidator, EmailUserValidator) {
            SessionBilledCall.NamesUserValidator = NamesUserValidator;
            SessionBilledCall.EmailUserValidator = EmailUserValidator;
        },

        GetCallOutDetailsGetSearch: function () {
            var that = this, controls = that.getControls();
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
            SessionBilledCall.hidBolsa = "0";
            var that = this,
            controls = that.getControls();
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var oBilledCallPerftipificacion = {};
          
            oBilledCallPerftipificacion.strIdSession = SessionBilledCall.strIdSession;
            oBilledCallPerftipificacion.Phone = SessionBilledCall.strPhone;
            oBilledCallPerftipificacion.StartDate = controls.txtStartDate.val();
            oBilledCallPerftipificacion.EndDate = controls.txtEndDate.val();
            oBilledCallPerftipificacion.PerfSearch = SessionBilledCall.hidPerfilBuscar;
            oBilledCallPerftipificacion.TrafType = controls.ddlTraficType.val();
            oBilledCallPerftipificacion.ClaseId = SessionBilledCall.ClaseId;
            oBilledCallPerftipificacion.SubClaseId = SessionBilledCall.SubClaseId;
            oBilledCallPerftipificacion.TipoDes = SessionBilledCall.TipoDes;
            oBilledCallPerftipificacion.ClaseDes = SessionBilledCall.ClaseDes;
            oBilledCallPerftipificacion.SubClaseDes = SessionBilledCall.SubClaseDes;
            oBilledCallPerftipificacion.PhonfNroGener = SessionBilledCall.PhonfNroGener;
            oBilledCallPerftipificacion.ObjidContact = SessionBilledCall.ObjidContact;
            oBilledCallPerftipificacion.EmpyEmail = SessionBilledCall.EmpyEmail;
            oBilledCallPerftipificacion.CurrentUser = SessionBilledCall.CurrentUser;
            oBilledCallPerftipificacion.IsTFI = SessionBilledCall.IsTFI;

            oBilledCallPerftipificacion.Name = SessionBilledCall.Names;
            oBilledCallPerftipificacion.ApPat = SessionBilledCall.ApPart;
            oBilledCallPerftipificacion.ApMat = SessionBilledCall.ApMat;
            oBilledCallPerftipificacion.Cuenta = SessionBilledCall.Cuenta;
            oBilledCallPerftipificacion.CurrentTerminal = SessionBilledCall.CurrentTerminal;

            oBilledCallPerftipificacion.NamesUserValidator = SessionBilledCall.NamesUserValidator;
            oBilledCallPerftipificacion.EmailUserValidator = SessionBilledCall.EmailUserValidator;

            //console.log"Print Object");
            //console.logoBilledCallPerftipificacion);
        
            $.app.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(oBilledCallPerftipificacion),
                url:   "/transactions/Prepaid/BilledOutCallDetails/GetCallOutDetailsSearch",
                success: function (response) {
                    //console.log"Obteniendo el Response de GetCallOutDetailsSearch");
                    //console.logresponse);

                    if (response.data != null) {
                        var registros = response.data.ListBilledOutCallDetails;
                        SessionBilledCall.colummvis = response.data.BilledCalltipificacion.columnvisib;
                        that.formatListBilledOutCallDetails(registros);
                        controls.lblErrorMessage.html('');
                        controls.divErrorAlert.hide();
                        SessionBilledCall.ObjectData = registros;
                        that.tblBilledOutCalls_Load(registros);

                        //console.logregistros);
                    }
                }
            });
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

        tblBilledOutCalls_Load: function (data) {
            var that = this,
            controls = that.getControls();

            if (SessionBilledCall.colummvis != null)
                var arr = SessionBilledCall.colummvis.split(',')

            var valueSelect = ($('input:radio[name="optradLoadTab"]:checked').val())

            controls.tblBilledOutCalls.DataTable({
                select: "single"
                , scrollCollapse: true
                , paging: !controls.chkAllRecords.is(':checked')
                , searching: false
                , scrollX: true
                , scrollY: "200px"
                , destroy: true
                , data: data
                , language: {
                    "lengthMenu": "Display _MENU_ registros por pagina",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "NumOrden" },
                    { "data": "FechaHora" },
                    { "data": "TelefonoDestino" },
                    { "data": "TipoTrafico" },
                    { "data": "Duracion" },
                    { "data": "Consumo" },
                    { "data": "CompradoRegalado" },
                    { "data": "Saldo" },
                    { "data": "Bolsa" },
                    { "data": "Descripcion" },
                    { "data": "Plan" },
                    { "data": "Promoción" },
                    { "data": "Destino" },
                    { "data": "Operador" },
                    { "data": "GrupoCobro" },
                    { "data": "TipoRed" },
                    { "data": "IMEI" },
                    { "data": "Roaming" },
                    { "data": "ZonaTarifaria" }
                ],
                columnDefs: [
                    { "targets": 1, "visible": Boolean(arr[0].toLowerCase() == 'true') },
                    { "targets": 2, "visible": Boolean(arr[1].toLowerCase() == 'true') },
                    { "targets": 3, "visible": Boolean(arr[2].toLowerCase() == 'true') },
                    { "targets": 4, "visible": Boolean(arr[3].toLowerCase() == 'true') },
                    { "targets": 5, "visible": Boolean(arr[4].toLowerCase() == 'true') },
                    { "targets": 6, "visible": Boolean(arr[5].toLowerCase() == 'true') },
                    { "targets": 7, "visible": Boolean(arr[6].toLowerCase() == 'true') },
                    { "targets": 8, "visible": Boolean(arr[7].toLowerCase() == 'true') },
                    { "targets": 9, "visible": Boolean(arr[8].toLowerCase() == 'true') },
                    { "targets": 10, "visible": Boolean(arr[9].toLowerCase() == 'true') },
                    { "targets": 11, "visible": Boolean(arr[10].toLowerCase() == 'true') },
                    { "targets": 12, "visible": Boolean(arr[11].toLowerCase() == 'true') },
                    { "targets": 13, "visible": Boolean(arr[12].toLowerCase() == 'true') },
                    { "targets": 14, "visible": Boolean(arr[13].toLowerCase() == 'true') },
                    { "targets": 15, "visible": Boolean(arr[14].toLowerCase() == 'true') },
                    { "targets": 16, "visible": Boolean(arr[15].toLowerCase() == 'true') },
                    { "targets": 17, "visible": Boolean(arr[16].toLowerCase() == 'true') },
                    { "targets": 18, "visible": Boolean(arr[17].toLowerCase() == 'true') }
                ]
              
            });
        },

        GetCallOutDetailsLoad: function () {

            var that = this,
            controls = that.getControls();
            var oBilledCalltipificacion = {};
            oBilledCalltipificacion.strIdSession = SessionBilledCall.strIdSession;
            oBilledCalltipificacion.Phone = SessionBilledCall.strPhone;
            oBilledCalltipificacion.StartDate = controls.txtStartDate.val();
            oBilledCalltipificacion.EndDate = controls.txtEndDate.val();
            oBilledCalltipificacion.TrafType = controls.ddlTraficType.val();
            oBilledCalltipificacion.IsTFI = SessionBilledCall.IsTFI;
            oBilledCalltipificacion.CurrentUser = SessionBilledCall.CurrentUser;
            oBilledCalltipificacion.EmpyEmail = SessionBilledCall.EmpyEmail;

            oBilledCalltipificacion.Name = SessionBilledCall.Names;
            oBilledCalltipificacion.ApPat = SessionBilledCall.ApPart;
            oBilledCalltipificacion.ApMat = SessionBilledCall.ApMat;
            oBilledCalltipificacion.Cuenta = SessionBilledCall.Cuenta;
            oBilledCalltipificacion.CurrentTerminal = SessionBilledCall.CurrentTerminal;

            //cargar datos
            controls.lblCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblContactCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerContact);
            controls.lblContact.text(SessionTransac.SessionParams.DATACUSTOMER.Name);
            controls.lblLegalRepresentative.text(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblIdentificationDocument.text(SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            controls.lblIdentificationDocumentoLR.text(SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
            controls.lblActivationDate.html(Session.DatosLinea_ActivationDate);
            controls.lblPhone.text(SessionTransac.SessionParams.DATACUSTOMER.TelephoneCustomer);
            $('#lblDateActivation').html(SessionTransac.SessionParams.DATASERVICE.DateActivation);
            $('#lblCustomerName').html(SessionTransac.SessionParams.DATACUSTOMER.FullName);
            $('#lblCustomerCode').html(SessionTransac.SessionParams.DATACUSTOMER.Modality);
            //controls.lblCustomerName.text(SessionTransac.SessionParams.DATACUSTOMER.Name);
            $('#lblContactCode').html(SessionTransac.SessionParams.DATACUSTOMER.ContactCode);
            $('#lblPlan').html(SessionTransac.SessionParams.DATASERVICE.Plan);
            


            $.app.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(oBilledCalltipificacion),
                url:   "/transactions/Prepaid/BilledOutCallDetails/GetCallOutDetailsLoad",
                success: function (response) {
                    var registros = response.data.ListBilledOutCallDetails;
                    SessionBilledCall.colummvis = response.data.BilledCalltipificacion.columnvisib;
                    SessionBilledCall.ClaseId = response.data.BilledCalltipificacion.ClaseId;
                    SessionBilledCall.SubClaseId = response.data.BilledCalltipificacion.SubClaseId;
                    SessionBilledCall.TipoDes = response.data.BilledCalltipificacion.TipoDes;
                    SessionBilledCall.ClaseDes = response.data.BilledCalltipificacion.ClaseDes;
                    SessionBilledCall.SubClaseDes = response.data.BilledCalltipificacion.SubClaseDes;
                    SessionBilledCall.PhonfNroGener = response.data.BilledCalltipificacion.PhonfNroGener;
                    if (response.data.BilledCalltipificacion.RespTipif == "false") {

                        controls.lblErrorMessage.html(response.data.BilledCalltipificacion.DescTipif);
                        controls.divErrorAlert.show();
                    }
                    SessionBilledCall.ObjectData = registros;
                    that.formatListBilledOutCallDetails(registros);
                    that.tblBilledOutCalls_Load(registros);
                }
            });
        },

        formatListBilledOutCallDetails: function (registros) {
            var horas = 0;
            var minutos = 0;
            var segundo = 0;
            var cantm = 0.0;
            var SegundosTotal = 0;
            var cantvoz = 0;
            var SconsumoVoz = 0;
            var cantsms = 0;
            var ConsumoSMS = 0
            var SConsumoSMS = 0;
            var cantmms = 0;
            var ConsumoMMS = 0;
            var SConsumoMMS = 0;
            var cantdatos = 0;
            var ConsumoDatos = 0;
            var SconsumoDatos = 0;
            var SconsumoIP = 0;
            var contEND = 0;
            $.each(registros, function (i, r) {
                contEND++;
                switch (r.TipoTrafico) {
                    case 'VOZ':
                        var duracionT;
                        duracionT = r.Duracion;
                        var arrDuracion = duracionT.split(':');
                        SegundosTotal = parseInt(arrDuracion[0] * 3600) + parseInt(arrDuracion[1] * 60) + parseInt(arrDuracion[2]);
                        cantvoz = cantvoz + SegundosTotal;
                        var ConsumoVoz = r.Consumo.toString().indexOf(".");
                        if (ConsumoVoz > 0) {
                            SconsumoVoz = SconsumoVoz + parseFloat(r.Consumo)
                        }
                        break;
                    case 'SMS':
                        cantsms += parseInt(r.Consumo)
                        var ConsumoSMS = r.Consumo.toString().indexOf('.');
                        if (ConsumoSMS > 0) {
                            SConsumoSMS = SConsumoSMS + parseFloat(r.Consumo)
                        }
                        break;
                    case 'MMS':
                        cantmms += parseInt(r.Consumo)
                        var ConsumoMMS = r.Consumo.toString().indexOf(".");
                        if (ConsumoMMS > 0) {
                            SConsumoMMS = SConsumoMMS + parseFloat(r.Consumo)
                        }
                        break;
                    case 'DATOS':
                        cantdatos += parseFloat(r.Consumo)
                        var ConsumoDatos = r.Consumo.toString().indexOf("MB");
                        if (ConsumoDatos > 0) {
                            SconsumoDatos = SconsumoDatos + parseFloat(r.Consumo)
                        }
                        break;
                    case 'IP':
                        SconsumoIP = SconsumoIP + parseFloat(r.Consumo)
                        break;
                }
            });
            horas = parseInt(cantvoz / 3600);
            minutos = parseInt(parseInt((cantvoz - (horas * 3600)) / 60));
            segundo = cantvoz - ((horas * 3600) + (minutos * 60));
            var strHorasCad, strminutusCad, strsegundoCad;
            strHorasCad = horas.toString()
            strminutusCad = minutos.toString()
            strsegundoCad = segundo.toString()
            if (horas.toString().length < 2) {
                strHorasCad = "0" + horas.toString();
            }
            if (minutos.toString().length < 2) {
                strminutusCad = "0" + minutos.toString();
            }
            if (segundo.toString().length < 2) {
                strsegundoCad = "0" + segundo.toString();
            }
            cantm = SconsumoVoz + SconsumoIP + SConsumoSMS + SConsumoMMS + SconsumoDatos;
            $("#lblTotalVoz").html(strHorasCad + ":" + strminutusCad + ":" + strsegundoCad);
            $("#lblTotalSMS").html(cantsms);
            $("#lblTotalMMS").html(cantmms);
            $("#lblTotalGPRS").html(cantdatos);
            $("#lblTotalRegistros").html(contEND);
            $("#lblConsumo").html(cantm);
        },

        btnExport_click: function () {

            var that = this, controls = that.getControls();


            var textDefault = $('#tblBilledOutCalls tbody tr :first').text();
            if (textDefault == "No existen datos") {
                alert("No hay datos para exportar.", "Alerta");
                return;
            }

            if (SessionBilledCall.hidPerfilExportar != "1") 
            {
                //co=document.getElementById("hidCodOpcion").value;
                var param = {
                    "strIdSession": SessionBilledCall.strIdSession,
                    'pag': '4',
                    'opcion': 'BUS',
                    //'co': co,
                    'telefono': SessionBilledCall.PhonfNroGener
                };
                ValidateAccess(that, controls, 'EXP', 'gConstkeyExportacionDetalleLLamadaAutorizada', '4', param);
                return;
            }
            else {
                if (!that.ValidateDate()) {
                    return
                }



                that.GetCallOutDetailsGetExport();
            }
        },



        GetCallOutDetailsGetExport: function () {

            var that = this, controls = that.getControls();
            that.Loading();
        
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'))
            var myUrlDowload =   "/transactions/Prepaid/BilledOutCallDetails/DownloadExcel";
            var oBilledOutCallExport = {};
            oBilledOutCallExport.strIdSession = SessionBilledCall.strIdSession;
            oBilledOutCallExport.PerfExcel = SessionBilledCall.hidPerfilExportar;

            oBilledOutCallExport.Names = SessionBilledCall.Names;
            oBilledOutCallExport.ApPart = SessionBilledCall.ApPart;
            oBilledOutCallExport.ApMat = SessionBilledCall.ApMat;
            oBilledOutCallExport.Cuenta = SessionBilledCall.Cuenta;
            oBilledOutCallExport.CurrentUser = SessionBilledCall.CurrentUser;
            oBilledOutCallExport.CurrentTerminal = SessionBilledCall.CurrentTerminal;

            oBilledOutCallExport.EmpyEmail = SessionBilledCall.EmpyEmail;
            oBilledOutCallExport.Phone = SessionBilledCall.strPhone;
            oBilledOutCallExport.PhonfNroGener = SessionBilledCall.PhonfNroGener;
            oBilledOutCallExport.TrafType = controls.ddlTraficType.val() + '|' + $("#ddlTraficType option:selected").html()
            oBilledOutCallExport.StartDate = controls.txtStartDate.val();
            oBilledOutCallExport.EndDate = controls.txtEndDate.val();

            oBilledOutCallExport.NamesUserValidator = SessionBilledCall.NamesUserValidator;
            oBilledOutCallExport.EmailUserValidator = SessionBilledCall.EmailUserValidator;
            $.app.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(oBilledOutCallExport),
                url:   "/transactions/Prepaid/BilledOutCallDetails/GetBilledOutCallExport",
                success: function (response) {
                    window.location = myUrlDowload + '?strPath=' + response + "&strNewfileName=ExportExcelCallOutDetailsPrep.xlsx";
                }
            });
        },

        btnClear_click: function () {
            var that = this, controls = this.getControls();
         
            $("#txtStartDate").val("");
            $("#txtEndDate").val("");
            //$("#ddlTraficType > option[value='DSS']").attr('selected', 'select');
            $('#ddlTraficType').val("");
            $("#lblTotalVoz").html("");
            $("#lblTotalSMS").html("");
            $("#lblTotalMMS").html("");
            $("#lblTotalGPRS").html("");
            $("#lblTotalRegistros").html("");
            $("#lblConsumo").html("");
            //$("#tbody").empty();

        },

        btnClose_click: function () {
            parent.window.close();
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


        ValidateDate: function () {

            var that = this, controls = this.getControls();

            if (controls.txtStartDate.val() == '') {
                alert("Debe Ingresar una Fecha Inicio.","Alerta");
                controls.txtStartDate.focus();
                return false;
            }
            if (controls.txtEndDate.val() == '') {
                alert("Debe Ingresar la Fecha Fin.", "Alerta");
                controls.txtEndDate.focus();
                return false;
            }
            if (!that.ComparaFecha(controls.txtStartDate.val(), controls.txtEndDate.val(), '0')) {/// > ) {
                alert("Fecha Inicio debe ser menor o igual que la Fecha final.", "Alerta");
                return false;
            }

            return true;
        },

        ComparaFecha: function (fechainicio, fechafin, flag) {
            var comp1, comp2;
            comp1 = fechainicio.substr(6, 4) + '' + fechainicio.substr(3, 2) + '' + fechainicio.substr(0, 2);
            comp2 = fechafin.substr(6, 4) + '' + fechafin.substr(3, 2) + '' + fechafin.substr(0, 2);
            if (flag == '0') {
                if ((comp1) > (comp2)) {
                    return false;
                }
            }
            if (flag == '1') {
                if ((comp1) >= (comp2)) {
                    return false;
                }
            }


            return true;
        }

    };

    $.fn.BilledOutCallDetails = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('BilledOutCallDetails'),
                options = $.extend({}, $.fn.BilledOutCallDetails.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('BilledOutCallDetails', data);
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

    $.fn.BilledOutCallDetails.defaults = {
    }

    $('#divBody').BilledOutCallDetails();

})(jQuery);

