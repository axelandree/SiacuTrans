var hdnPermisoExp;
var hdnPermisoBus;
var sessionTransac = {};
var userValidatorAuth = "";
var dataSearch;

sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
//if (sessionTransac.UrlParams.IDSESSION == null || sessionTransac.UrlParams.IDSESSION == undefined) {
//    sessionTransac.UrlParams.IDSESSION = '0';
//}

function FC_GrabarCommit(pag, controls) {
    HiddenPageAuth.hidAccion = '';
    //document.getElementById('hidAccion').value = '';

    if (HiddenPageAuth.hidOpcion === 'EXP') {
        ExportCallDetail(pag, controls);
    } else if (HiddenPageAuth.hidOpcion === 'BUS') {
        SearchCall(pag, controls);
    }

    SendMail(pag, controls);
    HiddenPageAuth.hidOpcion = "";
}

function SearchCall(pag, controls) {
    var param =
    {
        strIdSession: "50548795",//sessionTransac.UrlParams.IDSESSION,
        tlf: Session.TELEPHONE,
        vContratoId: sessionTransac.SessionParams.DATACUSTOMER.ContractID,
        fInicio: $('#txtDateStart').val(),
        fFin: $('#txtDateEnd').val(),
        strLocalAd: HiddenPageAuth.hdnLocalAdd,
        strServName: HiddenPageAuth.hdnServName,
        product: sessionTransac.UrlParams.SUREDIRECT
    }

    //Validando Fechas
    if (!ValidarFechas(param.fInicio, param.fFin)) {
        alert("Debe ingresar las fechas de forma correcta.", "Alerta");
        return;
    }
    else {
        alert("Se generará tipificación por la información.", "Informativo ", function () {
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

            var paramFinal = {};
            paramFinal = {};
            paramFinal.HeaderRequestTypeBpel = {};
            paramFinal.DetailCallRequestBpelModel = {};
            paramFinal.HeaderRequestTypeBpelModel = {};

            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel = {};
            paramFinal.DetailCallRequestBpelModel.CustomerClfyBpelModel = {};
            paramFinal.DetailCallRequestBpelModel.InteractionBpelModel = {};
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel = {};

            //ContactUserBpelModel
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Usuario = sessionTransac.SessionParams.USERACCESS.login;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Nombres = sessionTransac.SessionParams.DATACUSTOMER.Name;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Apellidos = sessionTransac.SessionParams.DATACUSTOMER.LastName;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.RazonSocial = sessionTransac.SessionParams.DATACUSTOMER.BusinessName;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.TipoDoc = sessionTransac.SessionParams.DATACUSTOMER.DocumentType;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.NumDoc = sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Domicilio = sessionTransac.SessionParams.DATACUSTOMER.Address;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Distrito = sessionTransac.SessionParams.DATACUSTOMER.District;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Departamento = sessionTransac.SessionParams.DATACUSTOMER.Departament;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Provincia = sessionTransac.SessionParams.DATACUSTOMER.Province;
            //paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.Modalidad = sessionTransac.SessionParams.DATACUSTOMER.Modality;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.CustomerId = sessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            paramFinal.DetailCallRequestBpelModel.ContactUserBpelModel.ContractId = sessionTransac.SessionParams.DATACUSTOMER.ContractID;

            //CustomerClfyBpelModel
            paramFinal.DetailCallRequestBpelModel.CustomerClfyBpelModel.Account = sessionTransac.SessionParams.DATACUSTOMER.Account;
            paramFinal.DetailCallRequestBpelModel.CustomerClfyBpelModel.ContactObjId = "";
            paramFinal.DetailCallRequestBpelModel.CustomerClfyBpelModel.FlagReg = "1";

            //InteractionBpelModel
            paramFinal.DetailCallRequestBpelModel.InteractionBpelModel.Phone = Session.TELEPHONE;
            paramFinal.DetailCallRequestBpelModel.InteractionBpelModel.CoId = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
            paramFinal.DetailCallRequestBpelModel.InteractionBpelModel.CodPlano = sessionTransac.SessionParams.DATACUSTOMER.PlaneCodeBilling;
            paramFinal.DetailCallRequestBpelModel.InteractionBpelModel.Agente = sessionTransac.SessionParams.USERACCESS.login;

            //InteractionPlusBpelModel
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.ClaroNumber = Session.TELEPHONE;
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.DocumentNumber = sessionTransac.SessionParams.DATACUSTOMER.DNIRUC;
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.FirstName = sessionTransac.SessionParams.DATACUSTOMER.Name;
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.LastName = sessionTransac.SessionParams.DATACUSTOMER.LastName;
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.NameLegalRep = sessionTransac.SessionParams.DATACUSTOMER.LegalAgent;
            paramFinal.DetailCallRequestBpelModel.InteractionPlusBpelModel.DniLegalRep = sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;

            //Cabecera
            paramFinal.StrIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            paramFinal.StrTelephone = Session.TELEPHONE;
            paramFinal.StrTransaction = "13579";
            paramFinal.StrSecurity = "1";

            paramFinal.DetailCallRequestBpelModel.TipoConsulta = "N";
            paramFinal.DetailCallRequestBpelModel.Msisdn = Session.TELEPHONE;
            paramFinal.DetailCallRequestBpelModel.FechaInicio = $('#txtDateStart').val();
            paramFinal.DetailCallRequestBpelModel.FechaFin = $('#txtDateEnd').val();
            paramFinal.DetailCallRequestBpelModel.ValorContrato = Session.TELEPHONE;
            paramFinal.DetailCallRequestBpelModel.CodigoCliente = sessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            paramFinal.DetailCallRequestBpelModel.TipoProducto = sessionTransac.UrlParams.SUREDIRECT;
            paramFinal.DetailCallRequestBpelModel.TipoConsultaContrato = "C";

            //Header
            paramFinal.HeaderRequestTypeBpelModel.UsuarioAplicacion = sessionTransac.SessionParams.USERACCESS.login;

            var strUrl = pag.strUrl + '/CallDetails/GetUnBilledCallsDetail';

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrl,
                data: JSON.stringify(paramFinal),
                success: function (result) {
                    if (result.StrResponseCode == "1") {
                        var pagingBoolean = !controls.chkAllRecords.is(':checked');
                        dataSearch = result;
                        pag.InitDataTableUnBilledCallDetail(result.LstPhoneCall, pagingBoolean,false);

                    }
                    else
                    {
                        alert("Ocurrió un error al realizar la petición.", "Alerta");
                        return;
                    }
                    
                },
                error: function () {
                    alert('Error al consultar la información.', "Alerta");
                }
            });
        });
    }
}

function ValidarFechas(fechainicial, fechaFinal) {
    if (fechainicial == "" || fechaFinal == "") {
        return false;
    }
    return true;
}

function ExportCallDetail(pag, controls) {
    var textDefault = $('#tblUnbilledCallDetail tbody tr :first').text();
    if (textDefault == "No existen datos") {
        alert("No hay datos para exportar.", "Alerta");
        return;
    } else {
        var paramFinal = {};
        //paramFinal.lstTemp = dataSearch.LstPhoneCall;
        paramFinal.strCustomer = sessionTransac.SessionParams.DATACUSTOMER.BusinessName;
        paramFinal.tipoProducto = sessionTransac.UrlParams.SUREDIRECT;       
        paramFinal.fechaInicio = $('#txtDateStart').val();
        paramFinal.fechaFin = $('#txtDateEnd').val();

        var myUrlExport = '/Transactions/HFC/CallDetails/GetExportExcel_UnBilled';
        var myUrlDowload = '/Transactions/CommonServices/DownloadExcel';

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

        $.app.ajax({
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            url: myUrlExport,
            data: JSON.stringify(paramFinal),
            success: function (path) {
                if (path !== "") {
                    window.location = myUrlDowload + '?strPath=' + path + "&strNewfileName=ExportExcelUnBilledCallDetails.xlsx";
                }
            }
        });
    }
}

function CloseValidation(obj, pag, controls)
{
    userValidatorAuth = obj.EmailUserValidator;
    if (obj.hidAccion === 'G') {
        FC_GrabarCommit(pag, controls);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = $("#hidDescripcionProceso_Validar").val();
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        if (descripcion != '' && typeof (description) != typeof (undefined)) {
            mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para ' + descripcion + ', por favor verifiquelo.';
        }

        alert(mensaje, "Alerta");
        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");

        return;
    }
}

function SendMail(pag, controls) {
    if (userValidatorAuth != "") {
        var objParameter = {
            strIdSession: "50548795",//sessionTransac.UrlParams.IDSESSION,
            codeUser: sessionTransac.SessionParams.USERACCESS.login,
            phonfNroGener: sessionTransac.SessionParams.DATACUSTOMER.Telephone,
            cuenta: sessionTransac.SessionParams.DATACUSTOMER.Account,
            currentUser: userValidatorAuth,
            currentTerminal: HiddenPageAuth.hdnLocalAdd
        };

        var myUrlSendMail = "/Transactions/CommonServices/SendMail";

        $.app.ajax({
            type: 'POST',
            cache: false,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            url: myUrlSendMail,
            data: JSON.stringify(objParameter),
            success: function (data) {
            }
        });
    }
}

(function ($) {

    var Form = function ($element, options) {
        $.extend(this, $.fn.UnbilledCallDetail.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , txtDateStar: $('#txtDateStart', $element)
            , txtDateEnd: $('#txtDateEnd', $element)
            , btnSearch: $('#btnSearch', $element)
            , btnClose: $('#btnClose', $element)
            , btnExport: $('#btnExport', $element)
            , tblUnbilledCallDetail: $('#tblUnbilledCallDetail', $element)
            , spnMainTitle: $('#spnMainTitle')
            , lblTitle: $('#lblTitle', $element)
            , myModalLoad: $('#myModalLoad', $element)
            , chkAllRecords: $('#chkAllRecords', $element)
            , txtDireccion: $('#txtDireccion', $element)
            , txtNotaDireccion: $('#txtNotaDireccion', $element)
            , txtDepartamento: $('#txtDepartamento', $element)
            , txtPais: $('#txtPais', $element)
            , txtDistrito: $('#txtDistrito', $element)
            , txtProvincia: $('#txtProvincia', $element)
            , txtCodigoPlano: $('#txtCodigoPlano', $element)
            , txtCodigoUbigeo: $('#txtCodigoUbigeo', $element)
            , IdlblCodPlano: $('#IdlblCodPlano', $element)
            , IdlblCodUbigeo: $('#IdlblCodUbigeo', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_Click);
            controls.btnClose.addEvent(that, 'click', that.btnClose_Click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_Click);
            controls.chkAllRecords.addEvent(that, 'change', that.refreshRecords_change);
            //controls.txtDateStar.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '-5h' });
            //controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy', startDate: '-90d', endDate: '-5h' });

            $.datepicker.regional['10250'];
            controls.txtDateStar.datepicker({ format: 'dd/mm/yyyy', endDate: '0' });
            controls.txtDateEnd.datepicker({ format: 'dd/mm/yyyy', endDate: '0' });

            controls.IdlblCodPlano.hide();
            controls.IdlblCodUbigeo.hide();
            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
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

            that.InitDataTableUnBilledCallDetail();
            that.getLoadDetailCall();
        },
        btnSearch_Click: function () {
            var that = this, controls = this.getControls();
            //if (hdnPermisoBus == 'Si') {
                SearchCall(that, controls);
            //} else {
            //    //SearchCall(that, controls);
            //    var co = HiddenPageAuth.hidCodOpcion;
            //    var telefono = Session.TELEPHONE;
            //    var param =
            //    {
            //        "strIdSession": Session.IDSESSION,
            //        'pag': '1',
            //        'opcion': 'BUS',
            //        'co': co,
            //        'telefono': telefono
            //    };

            //    ValidateAccess(that, controls, 'BUS', 'gConstEvtBuscarDetaLlamadaLin', '1', param, 'Fixed');
            //}
        },
        btnExport_Click: function () {
            var that = this, controls = this.getControls();
            if (hdnPermisoExp == 'Si') {
                ExportCallDetail(that, controls);
            } else {
                //ExportCallDetail(that, controls);
                var co = HiddenPageAuth.hidCodOpcion;
                var telefono = Session.TELEPHONE;
                var param = {
                    "strIdSession": Session.IDSESSION,
                    'pag': '1',
                    'opcion': 'EXP',
                    'co': co,
                    'telefono': telefono
                };

                ValidateAccess(that, controls, 'EXP', 'gConstEvtExportarDetaLlamadaLin', '1', param, 'Fixed');
            }

        },
        btnClose_Click: function () {
            parent.window.close();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        setMainTitle: function (titlePage) {
            var that = this, controls = that.getControls();
            
            if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                controls.spnMainTitle.text("DETALLE DE LLAMADAS SALIENTES NO FACTURADAS");
                $('#header-text').text('Detalle De Llamadas Salientes No Facturadas HFC');
                controls.IdlblCodPlano.show();                
            } else {
                controls.spnMainTitle.text("DETALLE DE LLAMADAS SALIENTES NO FACTURADAS");
                $('#header-text').text('Detalle De Llamadas Salientes No Facturadas LTE');
                controls.IdlblCodUbigeo.show();
            }

        },
        maximizarWindow: function(){
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
        getCustomerPhone: function () {
            var that = this, controls = that.getControls(),
            model = {};
            model.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            model.intIdContract = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
            model.strTypeProduct = sessionTransac.UrlParams.SUREDIRECT;

            var myUrl = that.strUrl + '/CallDetails/GetCustomerPhone';
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        Session.TELEPHONE = response;
                    } else {
                        alert("No existe una linea de Teléfono.", "Alerta");
                    }
                },
                error: function (error) {
                    alert("Ocurrió un error al realizar la operación.", "Alerta");
                }
            });
        },
        getEnabledPermission: function () {
            var that = this;
            var strUrlController = that.strUrl + '/CallDetails/HFCUnbilledCallDetail_EnabledPermission';
            var param = { "strIdSession": "50548795", 'strPermisos': sessionTransac.SessionParams.USERACCESS.optionPermissions, 'product': sessionTransac.UrlParams.SUREDIRECT }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    hdnPermisoExp = response.hdnPermisoExp;
                    hdnPermisoBus = response.hdnPermisoBus;
                    HiddenPageAuth.hdnPermisoExp =response.hdnPermisoExp;
                    HiddenPageAuth.hdnPermisoBus = response.hdnPermisoBus;
                    //var boolExport = response.btnExportar;
                    //$('#btnExport').attr('disabled', !boolExport);
                    that.getLoadDates();
                },
                error: function (error) {
                    alert("Ocurrió un error al realizar la operación.", "Alerta");
                }
            });
        },
        getLoadDetailCall: function () {
            var controls = this.getControls(), that = this;
            if (Session.USERACCESS == {} && Session.USERACCESS.CODEUSER == "") {
                parent.window.close();
                opener.parent.top.location.href = Session.RouteSiteStart;
                return;
            }

            var strUrlController = that.strUrl + '/CallDetails/HFCUnbilledCallDetail_PageLoad';
            var param = { "strIdSession": "50548795", "codPlanTarifario": sessionTransac.SessionParams.DATASERVICE.CodePlanTariff, 'estadoAcceso': sessionTransac.SessionParams.USERACCESS.accessStatus }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    if (response.RestrictPlan == "True") {
                        alert(response.strMensajeInformacionRestrin, "Alerta", function () {
                            parent.window.close();
                            return false;
                        });
                    }

                    //if (sessionTransac.SessionParams.DATASERVICE.TelephonyValue != response.gstrVariableT) {
                    //    alert(response.strMsgDatosLinea, "Alerta", function () {
                    //        parent.window.close();
                    //        return false;
                    //    });
                    //}

                    $('#lblContract').text(sessionTransac.SessionParams.DATACUSTOMER.ContractID);
                    $('#lblCustomerId').text(sessionTransac.SessionParams.DATACUSTOMER.CustomerID);


                    $('#lblHeaderTypeCustomer').text(sessionTransac.SessionParams.DATACUSTOMER.CustomerType);
                    $('#lblCustomer').text(sessionTransac.SessionParams.DATACUSTOMER.BusinessName);
                    $('#lblPlan').text(sessionTransac.SessionParams.DATASERVICE.Plan);
                    $('#lblDocument').text(sessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
                    $('#lblDateActivation').text(sessionTransac.SessionParams.DATASERVICE.ActivationDate);
                    $('#lblCycleFacture').text(sessionTransac.SessionParams.DATACUSTOMER.BillingCycle);
                    $('#lblLimitCredit').text(sessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit);
                    $('#lblRepreLegal').text(sessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
                    //$('#lblHeaderContact').text(sessionTransac.SessionParams.DATACUSTOMER.CustomerID);
                    //$('#lblDocRepresLegal').text(sessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
                    $('#lblContact').text(sessionTransac.SessionParams.DATACUSTOMER.FullName);

        

                    controls.txtDateStar.datepicker('setStartDate', response.startDateConfig);
                    controls.txtDateEnd.datepicker('setStartDate', response.startDateConfig);


                    controls.txtDireccion.text(sessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
                    controls.txtNotaDireccion.text(sessionTransac.SessionParams.DATACUSTOMER.Reference);
                    controls.txtDepartamento.text(sessionTransac.SessionParams.DATACUSTOMER.Departament);
                    controls.txtPais.text(sessionTransac.SessionParams.DATACUSTOMER.LegalCountry);
                    controls.txtDistrito.text(sessionTransac.SessionParams.DATACUSTOMER.District);
                    controls.txtProvincia.text(sessionTransac.SessionParams.DATACUSTOMER.Province);
                    controls.txtCodigoPlano.text(sessionTransac.SessionParams.DATACUSTOMER.PlaneCodeInstallation);
                    controls.txtCodigoUbigeo.text(sessionTransac.SessionParams.DATACUSTOMER.InvoiceUbigeo);

                    that.setMainTitle(response.TituloPagina);

                    HiddenPageAuth.hdnMensaje1 = response.hdnMensaje1;
                    HiddenPageAuth.hdnMensaje2 = response.hdnMensaje2;
                    HiddenPageAuth.hdnMensaje3 = response.hdnMensaje3;
                    HiddenPageAuth.hdnMensaje4 = response.hdnMensaje4;
                    HiddenPageAuth.hdnMensaje5 = response.hdnMensaje5;
                    HiddenPageAuth.hdnSiteUrl = response.hdnSiteUrl;
                    HiddenPageAuth.hdnMensaje12 = response.hdnMensaje12;
                    HiddenPageAuth.hdnMensaje13 = response.hdnMensaje13;
                    HiddenPageAuth.hdnMensaje14 = response.hdnMensaje14;
                    HiddenPageAuth.hdnMensaje15 = response.hdnMensaje15;
                    HiddenPageAuth.hidContrId = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
                    HiddenPageAuth.hidTransaccion = response.hidTransaccion;
                    HiddenPageAuth.hdnLocalAdd = response.hdnLocalAdd;
                    HiddenPageAuth.hdnServName = response.hdnServName;
                    HiddenPageAuth.hidCodOpcion = response.hidCodOpcion;
                    that.getEnabledPermission();
                    that.getCustomerPhone();
                },
                error: function (error) {
                    alert("Ocurrió un error al realizar la operación.", "Alerta");
                }
            });
        },
        getLoadDates: function () {
            var that = this;
            var strUrlController = that.strUrl + '/CallDetails/LoadDates_HFC';
            var param = { "strIdSession": "50548795", 'cicloFacturacion': sessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.BillingCycle, 'cuenta': sessionTransac.SessionParams.DATACUSTOMER.Account }

            $.ajax({
                type: 'POST',
                cache: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'JSON',
                url: strUrlController,
                data: JSON.stringify(param),
                success: function (response) {
                    $('#txtDateStart').val(response.txtFechaInicio);
                    $('#txtDateEnd').val(response.txtFechaFin);
                    HiddenPageAuth.hidFechaIniTel = response.hidFechaIniTel;
                    HiddenPageAuth.hidFechaFinTel = response.hidFechaFinTel;
                },
                error: function () {
                    alert("Ocurrió un error al realizar la operación.", "Alerta");
                }
            });
        },
        InitDataTableUnBilledCallDetail: function (rowsData, pagingBool, collapse) {
            var that = this, controls = that.getControls();
            controls.tblUnbilledCallDetail.dataTable({
                info: false,
                select: "single",
                paging: pagingBool || false,
                searching: false,
                //scrollX: true,
                //scrollY: 300,
                scrollCollapse: collapse == null ? true : collapse,
                destroy: true,
                data: rowsData,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "NroRegistro" },
                    { "data": "Fecha" },
                    { "data": "Hora" },
                    { "data": "Telefono_Origen" },
                    //{ "data": "Telefono_Destino" },
                    {
                        "data": "Telefono_Destino", render: function (data) {
                            if (data.length - 4 >= 0) {
                                return data.substring(0, data.length - 4) + 'XXXX';
                            }
                            return data;
                        }
                    },
                    { "data": "Cantidad" },
                    { "data": "Cargo_Original" },
                    { "data": "Plan_Tarifario" },
                    { "data": "Tarifa" },
                    { "data": "Tipo" },
                    { "data": "Zona_Tarifaria" },
                    { "data": "Operador" },
                    { "data": "Horario" },
                    { "data": "Tipo_Llamada" },
                    { "data": "Cargo_Final" }
                ]
            });
        },
        refreshRecords_change: function () {
            var that = this, controls = that.getControls();
            var pagingBoolean = !controls.chkAllRecords.is(':checked');
            that.InitDataTableUnBilledCallDetail(dataSearch, pagingBoolean, false);
        },
        strUrl: window.location.protocol + '//' + window.location.host + '/Transactions/HFC/'
    };

    $.fn.UnbilledCallDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('UnbilledCallDetail'),
                options = $.extend({}, $.fn.UnbilledCallDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('UnbilledCallDetail', data);
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

    $.fn.UnbilledCallDetail.defaults = {
    }

    $('#divBody').UnbilledCallDetail();

})(jQuery);
