var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
var Session = {};

function CloseValidation(obj, pag, controls) {

    //obj.hidAccion = 'D';

    if (obj.hidAccion == 'D') {
        var opcionFinal = document.getElementById("hidOpcion").value;
        pag.GetCallOutDetailsGetUserValidator(obj.NamesUserValidator, obj.EmailUserValidator)
        validateInteractionCall(pag, controls, opcionFinal, '0', true);
        return;
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = $("#hidDescripcionProceso_Validar").val();
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '' && typeof (description) != typeof (undefined)) {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }

        alert(mensaje,"Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
}

function validateInteractionCall(pag, controls, action, state, result) {
    if (action == 'E') {
        ExportIncomingCallDetail(pag, controls);
        //console.log"Carga boton export despues de enviar datos al controlador");

    }

    if (action == 'I') {
        PrintIncomingCallDetail(pag, controls);
        //console.log"Carga boton print despues de enviar datos al controlador");
    }
}

function ExportIncomingCallDetail(pag, controls) {

    var strUrlModal = pag.strUrl + '/Postpaid/IncomingCallDetail/Export';
    var strUrlResult = '/Transactions/CommonServices/DownloadExcel';
    var flagChkGenerateOCC = "";

    if (controls.chkGenerateOCC.prop("checked")) {
        flagChkGenerateOCC = "T";
    } else {
        flagChkGenerateOCC = "F";
    }

    //Session.PaymentOCC = true;




    var txtStartDate = $('input[id = txtStartDate]').val();
    var txtEndDate = $('input[id = txtEndDate]').val();
    var taskNote = $('textarea[id = taskNote]').val();
    var ddlCACDACSelected = $('select[id=ddlCACDAC] option:selected').text();

    var NameClient = Session.Nombres + ' ' + Session.Apellidos;


    //console.log"Carga boton export");



    var objPost = {
        idSession: Session.IDSESSION,
        NameUserLoging: Session.NameUserLoging,
        CustomerId: Session.CustomerId,
        profileExp: Session.ProfileExport,
        EmailProfileAuthorized: Session.EmailProfileAuthorized,
        NameEmp: Session.NameEmp,
        SecondNameEmp: Session.SecondNameEmp,
        telephone: Session.TELEPHONE,
        codOption: Session.codOption,
        Account: Session.Account,
        ProfileAuthorized: Session.ProfileAuthorized,
        txtStartDate: txtStartDate,
        txtEndDate: txtEndDate,
        chkGenerateOCC_IsChecked: flagChkGenerateOCC,
        PaymentOCC: Session.PaymentOCC,
        ddlCACDACSelected: ddlCACDACSelected,
        NameClient: NameClient,
        UserLogin: Session.UserLogin
    };
    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        //data: {},
        data: JSON.stringify(objPost),

        success: function (result) {
            //console.log"Carga boton export esperando resultado de form a exportar");
            if (result.StatusCode == "OK") {
                controls.divShowMessage.show();
                controls.lblErrorMessage.text(result.StatusMessage);
                window.location = strUrlResult + '?strPath=' + result.Path + "&strNewfileName=ReporteDeLlamadas.xlsx";
                //console.log"fin de Carga boton export esperando resultado de form a exportar");
                if (result.PaymentOCC == false) {
                    Session.PaymentOCC = false;
                }
                return;
            } else if (result.StatusCode == "E") {
                controls.divShowMessage.show();
                controls.lblErrorMessage.text(result.StatusMessage);
            } else {
                controls.divShowMessage.hide();
                return;
            }
            //window.location = strUrlResult + '?strPath=' + result.Path + "&strNewfileName=ReporteDeLlamadas.xlsx";
            ////console.log"fin de Carga boton export esperando resultado de form a exportar");
        }
    });
}


function PrintIncomingCallDetail(pag, controls) {

    var strUrlModal = pag.strUrl + '/Postpaid/IncomingCallDetail/Print';

    var flagChkGenerateOCC = "";

    if (controls.chkGenerateOCC.prop("checked")) {
        flagChkGenerateOCC = "T";
    } else {
        flagChkGenerateOCC = "F";
    }

    //Session.PaymentOCC = true;


    var txtStartDate = $('input[id = txtStartDate]').val();
    var txtEndDate = $('input[id = txtEndDate]').val();
    var taskNote = $('textarea[id = taskNote]').val();
    var ddlCACDACSelected = $('select[id=ddlCACDAC] option:selected').text();

    //console.log"Carga boton print");

    var objPost = {
        idSession: Session.IDSESSION,
        NameUserLoging: Session.NameUserLoging,
        CustomerId: Session.CustomerId,
        profilePrint: Session.ProfilePrint,
        EmailProfileAuthorized: Session.EmailProfileAuthorized,
        NameEmp: Session.NameEmp,
        SecondNameEmp: Session.SecondNameEmp,
        telephone: Session.TELEPHONE,
        codOption: Session.codOption,
        Account: Session.Account,
        ProfileAuthorized: Session.ProfileAuthorized,
        txtStartDate: txtStartDate,
        txtEndDate: txtEndDate,
        chkGenerateOCC_IsChecked: flagChkGenerateOCC,
        PaymentOCC: Session.PaymentOCC,
        UserLogin: Session.UserLogin
    };

    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: strUrlModal,
        //data: {},
        data: JSON.stringify(objPost),

        success: function (result) {
            //console.log"Carga boton print esperando resultado de form a imprimir");
            if (result.StatusCode == "OK") {
                controls.divShowMessage.show();
                controls.lblErrorMessage.text(result.StatusMessage);

                var view = location.protocol + '//' + location.host + '/Transactions/Postpaid/IncomingCallDetail/PostpaidIncomingCallDetailPrint';


                var viewPrint = window.open(view, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=850, height=650');


                viewPrint.focus();
                $(viewPrint.document).ready(function () {
                    viewPrint.window.focus();
                    viewPrint.document.close();
                    viewPrint.window.print();
                });
                if (result.PaymentOCC == false) {
                    Session.PaymentOCC = false;
                }
            } else if (result.StatusCode == "E") {
                controls.divShowMessage.show();
                controls.lblErrorMessage.text(result.StatusMessage);
            } else {
                controls.divShowMessage.hide();
                return;
            }
            //console.log"fin de Carga boton print esperando resultado de form a imprimir");

        }
    });


}

function FormatDateToString(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}


function CompareDate(dateStart, dateEnd, flag) {
    comp1 = dateStart.substr(6, 4) + '' + dateStart.substr(3, 2) + '' + dateStart.substr(0, 2);
    comp2 = dateEnd.substr(6, 4) + '' + dateEnd.substr(3, 2) + '' + dateEnd.substr(0, 2);
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

(function ($, undefined) {

 

    var Form = function ($element, options) {

        $.extend(this, $.fn.PostpaidIncomingCallDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            , lblCustomer: $('#lblCustomerName', $element)
            , lblContactCustomer: $('#lblContactCustomer', $element)
            , lblContact: $('#lblContact', $element)
            , lblLegalRepresentative: $('#lblLegalRepresentative', $element)
            , lblIdentificationDocument: $('#lblIdentificationDocument', $element)
            , lblIdentificationDocumentoLR: $('#lblIdentificationDocumentoLR', $element)
            , lblActivationDate: $('#lblActivationDate', $element)
            , txtStartDate: $('#txtStartDate', $element)
            , txtEndDate: $('#txtEndDate', $element)
            , ddlCACDAC: $('#ddlCACDAC', $element)
            , chkGenerateOCC: $('#chkGeneraOCC', $element)
            , tblIncomingCalls: $('#tblIncomingCalls', $element)
            , btnConsult: $('#btnConsult', $element)
            , btnSave: $('#btnSave', $element)
            , btnPrint: $('#btnPrint', $element)
            , btnExport: $('#btnExport', $element)
            , btnConstancy: $('#btnConstancy', $element)
            , btnClose: $('#btnClose', $element)
            , dContenedorEmail: $('#dContenedorEmail', $element)
            , chkEnviarCorreo: $('#chkEnviarCorreo', $element)
            , txtEmail: $('#txtEmail', $element)
            , myModalLoad: $('#myModalLoad', $element)
            , txttaskNote: $('#taskNote', $element)
            , divShowMessage: $('#divShowMessage', $element)
            , lblTitle: $('#lblTitle', $element)
            , lblErrorMessage: $('#lblErrorMessage', $element)
            , btnCloseError: $('#btnCloseError', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.txtStartDate.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '0d' });
            controls.txtEndDate.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '0d' });

            controls.btnConsult.addEvent(that, 'click', that.btnConsult_click);
            controls.btnSave.addEvent(that, 'click', that.btnSave_click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_click);
            controls.btnConstancy.addEvent(that, 'click', that.btnConstancy_click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.btnCloseError.addEvent(that, 'click', that.btnCloseError);


            that.maximizarWindow();
            that.windowAutoSize();
            that.loadSessionData();
            that.AccessForm();
            that.render();

        },
        setControls: function (value) {
            this.m_controls = value
        },
        getControls: function () {
            return this.m_controls || {};
        },
        render: function () {
            var that = this;
          
            that.getCACDAC();

            that.CheckedOCC();
            that.LoadtblIncomingCalls();
            that.Loading();
            that.DisabledbtnConsult();
            that.DisabledbtnSave();
            that.DisabledbtnPrint();
            that.DisabledbtnExport();
            that.DisabledbtnConstancy();
            that.DisabledbtnClose();

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


        loadSessionData: function () {
            var that = this;
            var controls = this.getControls();


            controls.lblTitle.text("DETALLE DE LLAMADAS ENTRANTES");
            //console.log"Redireccionó a la Transacion");
            //var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);



            Session.IDSESSION = "2132131";
            Session.CustomerId = SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            Session.NameUserLoging = null; //SessionTransac.SessionParams.USERACCESS.fullName; //Agregar usuario logueado
            Session.UserLogin = SessionTransac.SessionParams.USERACCESS.login;
            //Session.TELEPHONE = "51900008889"; para consulta a base de datos.
            //Session.TELEPHONE = "997357786";
            //Session.TELEPHONE = "900008889";
            //Session.TELEPHONE = "900008889";
            Session.TELEPHONE = SessionTransac.SessionParams.DATACUSTOMER.Telephone;
            Session.CodUser = SessionTransac.SessionParams.USERACCESS.userId;
            Session.CONTRATOID = SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            Session.codOption = SessionTransac.UrlParams.CO;
           



            Session.FlagPlatform = SessionTransac.SessionParams.DATASERVICE.FlagPlatform;
            Session.ActivationDatePrepaid = SessionTransac.SessionParams.DATACUSTOMER.ActivationDate;
            Session.PAGEACCESS = SessionTransac.SessionParams.USERACCESS.optionPermissions;
            Session.TypeClient = "EMPLEADO CLARO";
            Session.DatosLinea_ActivationDate = SessionTransac.SessionParams.DATACUSTOMER.ActivationDate;
            Session.MinimunDate = new Date();
            Session.MaximunDate = new Date();

            Session.FlagTFI = SessionTransac.SessionParams.DATASERVICE.FlagTFI;

            Session.OBJID_CONTACTO = SessionTransac.SessionParams.DATACUSTOMER.ContactCode;
            Session.Nombres = SessionTransac.SessionParams.DATACUSTOMER.Name;
            Session.Apellidos = SessionTransac.SessionParams.DATACUSTOMER.LastName;
            Session.DocumentNumber = SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
            Session.PhoneReference = SessionTransac.SessionParams.DATACUSTOMER.PhoneReference;
            Session.Amount = 4.97;
            Session.ProfileAuthorized = null;

            Session.ProfileExport = null;
            Session.ProfilePrint = null;
            Session.gConstkeyExpDetCallAut = null;
            Session.gConstkeyPrintDetCallAut = null;
            Session.EmailProfileAuthorized = null; //"aguilarsd@globalhitss.com";
            Session.NameEmp = SessionTransac.SessionParams.USERACCESS.firstName;
            Session.SecondNameEmp = SessionTransac.SessionParams.USERACCESS.lastName1 + ' ' + SessionTransac.SessionParams.USERACCESS.lastName2;
            Session.Account = SessionTransac.SessionParams.DATACUSTOMER.Account;
            Session.PaymentOCC = true;


            //Carga de datos para datos del cliente
            controls.lblCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblContactCustomer.text(SessionTransac.SessionParams.DATACUSTOMER.CustomerContact);
            controls.lblContact.text(SessionTransac.SessionParams.DATACUSTOMER.FullName);
            controls.lblLegalRepresentative.text(SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblIdentificationDocument.text(SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            controls.lblIdentificationDocumentoLR.text(SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);

            controls.divShowMessage.hide();
            that.loadForm();

        },

        loadForm: function () {
            var that = this, controls = this.getControls();
            var strUrlModal = that.strUrl + '/Postpaid/IncomingCallDetail/Load';

            if (Session.CodUser == null || Session.CodUser == "") {
                alert("Codigo de Usuario Vacio","Alerta");
                window.close();
            }

            if (Session.FlagPlatform == "C") {
                controls.lblActivationDate.html(Session.ActivationDatePrepaid);
            } else if (Session.FlagPlatform == "P") {
                controls.lblActivationDate.html(Session.DatosLinea_ActivationDate);
            }

            var dateMin = new Date();
            dateMin.setMonth(dateMin.getMonth() - 3);

            var dateMax = new Date();

            var startDate = new Date();
            startDate.setMonth(startDate.getMonth() - 1);

            var dateEnd = new Date();

            var dateString = FormatDateToString(dateEnd);

            $('#txtStartDate').datepicker('setDate', startDate);
            $('#txtEndDate').datepicker('setDate', dateString);

            Session.MinimunDate = dateMin;
            Session.MaximunDate = dateMax;

            //console.log"Carga Load");

            var objPost = {
                idSession: Session.IDSESSION,
                contractID: Session.CONTRATOID,
                flagPlatform: Session.FlagPlatform,
                strTypeClient: Session.TypeClient
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPost),
                url: strUrlModal,

                success: function (result) {
                    //console.log"Parametros de salida" + result);
                    if (!result.StatusCode == "E") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.StatusMessage);
                        return;
                    }  else {
                        controls.divShowMessage.hide();
                    }
                        
                  

                },
                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });

        },

        AccessForm: function () {
            var that = this, controls = this.getControls();
            var strUrlModal = that.strUrl + '/Postpaid/IncomingCallDetail/AccesForm';
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: {},
                url: strUrlModal,
                success: function (response) {
                    Session.gConstkeyPrintDetCallAut = response[0];
                    Session.gConstkeyExpDetCallAut = response[1];
                    that.EnablePermission();

                }
            });
        },
         

        EnablePermission: function () {
            var that = this,
                controls = this.getControls();

            var AccessPageProfile = Session.PAGEACCESS;
           


            if (AccessPageProfile.indexOf(Session.gConstkeyPrintDetCallAut) > -1) {
                Session.ProfilePrint = "1";//cambiar a 1 para tener permiso por defecto!
                Session.ProfileAuthorized = Session.IDSESSION;
            }
            else {
                Session.ProfilePrint = "0";
            }

            if (AccessPageProfile.indexOf(Session.gConstkeyExpDetCallAut) > 0) {
                Session.ProfileExport = "1";//cambiar a 1 para tener permiso por defecto!
                Session.ProfileAuthorized = Session.IDSESSION;
            }
            else {
                Session.ProfileExport = "0";
            }

        },

        GetCallOutDetailsGetUserValidator: function (NamesUserValidator, EmailUserValidator) {
            Session.NameUserLoging = NamesUserValidator;
            Session.EmailProfileAuthorized = EmailUserValidator;
        },

        CheckedOCC: function () {
            var controls = this.getControls();
            controls.chkGenerateOCC.prop("checked", true);
        },


        LoadtblIncomingCalls: function () {
            var controls = this.getControls();

            controls.tblIncomingCalls.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "{filtered from  _MAX_ total records}"
                }
            });
        },

        DisabledbtnConsult: function () {
            var controls = this.getControls();
            controls.btnConsult.prop("disabled", false);
        },

        DisabledbtnSave: function () {
            var controls = this.getControls();
            controls.btnSave.prop("disabled" , true);
        },

        DisabledbtnPrint: function () {
            var controls = this.getControls();
            controls.btnPrint.prop("disabled", true);
        },

        DisabledbtnExport: function () {
            var controls = this.getControls();
            controls.btnExport.prop("disabled", true);
        },

        DisabledbtnConstancy: function () {
            var controls = this.getControls();
            controls.btnConstancy.prop("disabled", true);
        },

        DisabledbtnClose: function () {
            var controls = this.getControls();
            controls.btnClose.prop("disabled", false);
        },
        btnCloseError: function() {
            var that = this,
                controls = this.getControls();

            //$('#divErrorAlert').hide();
            controls.divShowMessage.hide();
        },

        btnConsult_click: function (sender, arg) {
        

            var that = this,
                controls = this.getControls();

            var strUrlModal = that.strUrl + '/Postpaid/IncomingCallDetail/Consult';

            


            var flagChkGenerateOCC = "";

            if (controls.chkGenerateOCC.prop("checked")) {
                flagChkGenerateOCC = "T";
            }

            var txtStartDate = $('input[id = txtStartDate]').val();
            var txtEndDate = $('input[id = txtEndDate]').val();
            var taskNote = $('textarea[id = taskNote]').val();
            var ddlCACDACSelected = $('select[id=ddlCACDAC] option:selected').text();


            if (txtStartDate == '' || txtEndDate == '') {
                alert("Ingrese el Periodo para realizar la consulta","Alerta");
                return false;
            }
            if (!CompareDate(txtStartDate,txtEndDate,'0')) {
                alert("La Fecha de Inicio debe ser menor que la Fecha Fin","Alerta")
                return false;
            }


            var NameClient = Session.Nombres + ' ' + Session.Apellidos;
            //console.log"Carga Consult");

            var objPost = {
                idSession: Session.IDSESSION,
                NameUserLoging: Session.NameUserLoging,
                telephone: Session.TELEPHONE,
                codOption: Session.codOption,
                txttaskNote: taskNote,
                txtStartDate: txtStartDate,
                txtEndDate: txtEndDate,
                flagTFI: Session.FlagTFI,
                ObjId_Contact: Session.OBJID_CONTACTO,
                strNombres: Session.Nombres,
                strApellidos: Session.Apellidos,
                strDocumentNumber: Session.DocumentNumber,
                strReferencePhone: Session.PhoneReference,
                chkGenerateOCC_IsChecked: flagChkGenerateOCC,
                Amount: Session.Amount,
                ProfileAuthorized: Session.ProfileAuthorized,
                ddlCACDACSelected: ddlCACDACSelected,
                NameClient: NameClient,
                UserLogin: Session.UserLogin
            };

            that.Loading();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(objPost),
                success: function (result) {
                    
                    //console.log"Carga tabla consult");
                    controls.tblIncomingCalls.find('tbody').html('');
                    controls.tblIncomingCalls.DataTable({
                        "pagingType": "full_numbers",
                        "scrollY": "300px",
                        "scrollCollapse": true,
                        "info": false,
                        "select": 'single',
                        "ordering": false,
                        "paging": false,
                        "pageLength": false,
                        "searching": false,
                        "destroy": true,
                        "data": result.lstQueryAssociatedLines,
                        "columns": [
                            { "data": "NroOrd" },
                            { "data": "MSISDN" },
                            { "data": "CallDate" },
                            { "data": "CallTime" },
                            { "data": "CallNumber" },
                            { "data": "CallDuration" },
                        ],
                        "language": {
                            "lengthMenu": "Mostrar _MENU_ registros por página.",
                            "zeroRecords": "No existen datos",
                            "info": " ",
                            "infoEmpty": " ",
                            "infoFiltered": "(filtered from _MAX_ total records)",
                        },
                    });


                    if (result.StatusCode == "OK_A") {
                        //controls.divShowMessage.show();
                        $('#divShowMessage').show();
                        controls.lblErrorMessage.text(result.DescriptionTotalRecords);
                        alert(result.StatusMessage,"Informativo");
                        controls.btnConsult.prop("disabled", true);
                        controls.btnSave.prop("disabled", false);
                    } else if (result.StatusCode == "E") {
                        //controls.divShowMessage.show();
                        $('#divShowMessage').show();
                        controls.lblErrorMessage.text(result.StatusMessage);
                    } else {
                        //controls.divShowMessage.hide();
                        $('#divShowMessage').hide();
                        return;
                    }


                    //console.log"fin Carga tabla consult");

                },
                //error: function (errormessage) {
                //    alert('Error: ' + errormessage);
                //}
            });
        },


        btnSave_click: function () {

            var that = this,
                controls = this.getControls();


            that.Loading();
            var strUrlModal = that.strUrl + '/Postpaid/IncomingCallDetail/Save';




            var flagChkGenerateOCC = "";

            if (controls.chkGenerateOCC.prop("checked")) {
                flagChkGenerateOCC = "T";
            } else {
                flagChkGenerateOCC = "F";
            }

            var txtStartDate = $('input[id = txtStartDate]').val();
            var txtEndDate = $('input[id = txtEndDate]').val();
            var taskNote = $('textarea[id = taskNote]').val();
            var ddlCACDACSelected = $('select[id=ddlCACDAC] option:selected').text();


            //console.log"Carga boton guardar");

            var objPost = {

                idSession: Session.IDSESSION,
                NameUserLoging: Session.NameUserLoging,
                telephone: Session.TELEPHONE,
                codOption: Session.codOption,
                txttaskNote: taskNote,
                txtStartDate: txtStartDate,
                txtEndDate: txtEndDate,
                flagTFI: Session.FlagTFI,
                ObjId_Contact: Session.OBJID_CONTACTO,
                strNombres: Session.Nombres,
                strApellidos: Session.Apellidos,
                strDocumentNumber: Session.DocumentNumber,
                strReferencePhone: Session.PhoneReference,
                chkGenerateOCC_IsChecked: flagChkGenerateOCC,
                Amount: Session.Amount,
                ProfileAuthorized: Session.ProfileAuthorized,
                ddlCACDACSelected: ddlCACDACSelected,
                UserLogin: Session.UserLogin
            };


            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlModal,
                data: JSON.stringify(objPost),
                success: function (result) {
                    //if (result.StatusCode == "OK") {
                    //    controls.divShowMessage.show();
                    //    controls.lblErrorMessage.text(result.DescriptionTotalRecords);
                    //    alert(result.StatusMessage);
                    //    controls.btnSave.prop("disabled", true);
                    //    controls.btnPrint.prop("disabled", false);
                    //    controls.btnExport.prop("disabled", false);
                    //    controls.btnConstancy.prop("disabled", false);
                    //    controls.btnClose.prop("disabled", false);
                    //} else

                    //console.log"Carga boton guardar despues de enviar datos al controlador");

                    if (result.StatusCode == "OK_A") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.StatusMessage);
                        //alert(result.StatusMessage);
                        controls.btnSave.prop("disabled", true);
                        controls.btnPrint.prop("disabled", false);
                        controls.btnExport.prop("disabled", false);
                        controls.btnConstancy.prop("disabled", false);
                        controls.btnClose.prop("disabled", false);
                    } else if (result.StatusCode == "E") {
                        controls.divShowMessage.show();
                        controls.lblErrorMessage.text(result.StatusMessage);
                    } else {
                        controls.divShowMessage.hide();
                        return;
                    }

                    //console.log"fin de Carga boton guardar despues de enviar datos al controlador");
                },
                //error: function (errormessage) { }
            });

        },
        btnPrint_click: function () {
            var controls = this.getControls();
            var that = this;
            that.Loading();
            //console.log"inicia Carga boton print");
            //var userV = '';

            if (Session.ProfilePrint == "1") {
                validateInteractionCall(that, controls, 'I', '1', false);
                return;
            }
            else if (Session.ProfilePrint != "1")
            {
                validateInteractionCall(that, controls, 'I', '1', false);
                //var param = {
                //    "strIdSession": Session.IDSESSION,
                //    'pag': '4',
                //    'opcion': 'strLlamadasEntImprimir',
                //    'telefono': Session.TELEPHONE,
                //    'transaccion': '',
                //    'DetEntAccion': 'I'
                //};
                //ValidateAccess(that, controls, "I", "strLlamadasEntImprimir", "4", param, "Postpaid");
                //return;
            }
                

        },
        btnExport_click: function () {
            var that = this,
                controls = this.getControls();
            that.Loading();
           
            //var userV = '';
            //console.log"inicia Carga boton Export");
            if (Session.ProfileExport == "1") {
                validateInteractionCall(that, controls, 'E', '1', false);
                return;
            }
            else if (Session.ProfileExport != "1") {
                validateInteractionCall(that, controls, 'E', '1', false);
                //var param = {
                //    "strIdSession": Session.IDSESSION,
                //    'pag': '4',
                //    'opcion': 'strLlamadasEntExportar',
                //    'telefono': Session.TELEPHONE,
                //    'transaccion': '',
                //    'DetEntAccion': 'E'
                //};
                //ValidateAccess(that, controls, "E", "strLlamadasEntExportar", "4", param, "Postpaid");
                //return;
            }
            
        },



        btnConstancy_click: function () {
            var that = this;
            that.Loading();
            //console.log"inicia Carga boton constancia");
            var url = that.strUrl.substring(0, that.strUrl.lastIndexOf('/'));
            $.unblockUI();
            var NameClient = Session.Nombres + ' ' + Session.Apellidos;

            var txtStartDate = $('input[id = txtStartDate]').val();
            var txtEndDate = $('input[id = txtEndDate]').val();
            var taskNote = $('textarea[id = taskNote]').val();
            var ddlCACDACSelected = $('select[id=ddlCACDAC] option:selected').text();

            var objPost = {
                idSession: Session.IDSESSION,
                ddlCACDACSelected: ddlCACDACSelected,
                NameClient: NameClient,
                LegalAgent: SessionTransac.SessionParams.DATACUSTOMER.LegalAgent,
                TypeDocument: SessionTransac.SessionParams.DATACUSTOMER.DocumentType,
                strDocumentNumber: Session.DocumentNumber,
                txtStartDate: txtStartDate,
                txtEndDate: txtEndDate,
                txttaskNote: taskNote,
                telephone: Session.TELEPHONE

            };
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objPost),
                url: that.strUrl + '/Postpaid/IncomingCallDetail/Constancy',
                success: function (results) {
                    alert(results.RoutePdf,"Informativo");
                    ReadRecordSharedFile(Session.IDSESSION, results.RoutePdf);
                    
                }
            });

        },
        btnClose_click: function () {
            parent.window.close();
        },


        getCACDAC: function () {

            var that = this,
                controls = that.getControls(),

            objCacDacType = {};

            objCacDacType.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            var parameters = {};
            parameters.strIdSession = SessionTransac.SessionParams.USERACCESS.userId;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;

            //console.log'userId: ' + SessionTransac.SessionParams.USERACCESS.userId + 'login: ' + SessionTransac.SessionParams.USERACCESS.login);
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
                            controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {

                                if (cacdac === value.Description) {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;

                                } else {
                                    controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                //console.log"valor itemSelect: " + itemSelect);
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });


        },
        strUrl: '/Transactions'
    }

    $.fn.PostpaidIncomingCallDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidIncomingCallDetail'),
                options = $.extend({}, $.fn.PostpaidIncomingCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidIncomingCallDetail', data);
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

    $.fn.PostpaidIncomingCallDetail.defaults = {
    }

    $('#divBody').PostpaidIncomingCallDetail();
})(jQuery);