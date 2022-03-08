sessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
//if (sessionTransac.UrlParams.IDSESSION == null || sessionTransac.UrlParams.IDSESSION == undefined) {
//    sessionTransac.UrlParams.IDSESSION = '0';
//}
var userValidatorAuth = "";
var modelTempSave;
var routeConstancy = "";
var dataSearch;

var HiddenPageHtml = {};

function CloseValidation(obj, pag, controls) {
    //obj.hidAccion = 'G';
    userValidatorAuth = obj.EmailUserValidator;

    if (obj.hidAccion === 'G') {
        FC_GrabarCommit(pag, controls);
    }

    var mensaje;

    if (obj.hidAccion == 'F') {
        var descripcion = HiddenPageAuth.hidDescripcionProceso_Validar;
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

function FC_GrabarCommit(pag, controls) {
    HiddenPageAuth.hidAccion = '';
    if (HiddenPageAuth.hidOpcion === 'EXP') {
        DowloadDetailLlamadaHFC(pag, controls);
    } else if (HiddenPageAuth.hidOpcion === 'IMP') {
        PrintDetailLlamadaHFC(pag, controls);
    } else if (HiddenPageAuth.hidOpcion === 'BUS') {
        SearchCustomer(pag, controls);
    }

    SendMail(pag, controls);
    HiddenPageAuth.hidOpcion = "";
}

function DowloadDetailLlamadaHFC(that, controls) {
    var objParameter = {};
    var date = new Date();
    objParameter.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
    objParameter.strStarDate = $('#cboMes option:selected').text();
    objParameter.strEndDate = $('#cboYear').val();
    objParameter.strNameUser = sessionTransac.SessionParams.USERACCESS.fullName;
    objParameter.strTypeProduct = sessionTransac.UrlParams.SUREDIRECT;
    objParameter.strCustomer = sessionTransac.SessionParams.DATACUSTOMER.BusinessName;
    var myUrlDowload = '/Transactions/CommonServices/DownloadExcel';

    $.app.ajax({
        type: 'POST',
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: 'JSON',
        url: '/Transactions/HFC/CallDetails/GetExportExcel',
        data: JSON.stringify(objParameter),
        success: function (path) {
            window.location = myUrlDowload + '?strPath=' + path + "&strNewfileName=ExportHfcBilledCallDetails.xlsx";
        }
    });
}

function PrintDetailLlamadaHFC(that, controls) {
    var textDefault = $('#tblDetalleFacturacionHFC tbody tr :first').text();
    if (textDefault == "") {
        alert("No hay datos para imprimir.", "Alerta");
        return false;
    } else {

        var prm = '?strIdSession=' +
            sessionTransac.SessionParams.USERACCESS.userId +
            '&strCustomer=' +
            sessionTransac.SessionParams.DATACUSTOMER.FullName +
            '&strCuenta=' +
            sessionTransac.SessionParams.DATACUSTOMER.Account +
            '&strPlan=' +
            sessionTransac.SessionParams.DATASERVICE.Plan.trim() +
            '&strTypeCustomer=' +
            sessionTransac.SessionParams.DATACUSTOMER.CustomerType +
            '&strRazonSocial=' +
            sessionTransac.SessionParams.DATACUSTOMER.BusinessName +
            '&strTelephone=' +
            Session.SessionParams.HIDDEN.hdntelephone +
            '&strNameUser=' +
            sessionTransac.SessionParams.USERACCESS.fullName +
            '&strSn=' +
            Session.SessionParams.HIDDEN.hdnServName +
            '&strIpServidor=' +
            Session.SessionParams.HIDDEN.hdnLocalAdd +
            '&strInvoiceNumber=' +
            "" +
            '&strFuentDat=' +
            "1";

        var view = '/Transactions/HFC/CallDetails/HfcBilledCallsDetailPrint' + prm;
        window.open(view, '_blank', 'directories=no, location=no, menubar=no, scrollbars=yes, statusbar=no, tittlebar=no, width=750, height=600');
    }
    return false;
}

function SearchCustomer(that, controls) {

    var oMessage = Session.SessionParams.MESSAGEHFC, oHidden = Session.SessionParams.HIDDEN, oCustomer = sessionTransac.SessionParams.DATACUSTOMER, oSession = sessionTransac.SessionParams.USERACCESS;

    var strMesSel = "";
    var strValue = true;
    var strMonth = "";

    if ($("#cboMes").val() == "") {
        alert(oMessage.Message1, "Alerta");
        strValue = false;
        return false;
    } else {
        strMonth = $("#cboMes").val().length;
        if (strMonth == 1) {
            strMonth = "0" + $("#cboMes").val();
        }
        else {
            strMonth = $("#cboMes").val();
        }
    }

    if ($("#cboYear").val() == "") {
        alert(oMessage.Message10, "Alerta");
        strValue = false;
        return false;
    }

    if ($("#cboCacDac").val() == "") {
        alert(oMessage.Message17, "Alerta");
        strValue = false;
        return false;
    }

    strMesSel = $("#cboYear").val() + strMonth;

    if (strMesSel > $("#hidFechaActual").val()) {
        alert(oMessage.Message2, "Alerta");
        strValue = false;
        return false;
    }

    var email = "";
    if ($("#chkSentEmail").is(':checked')) {
        email = $("#txtSendforEmail").val();
    }

    if (strValue) {
        //var strNota = oMessage.hidMsgConsBusca + " " + $('#cboMes option:selected').text() + " del " + $('#cboYear').val();
        var strNota = oMessage.hidMsgConsBusca + " " + $('#cboMes option:selected').text() + " del " + $('#cboYear').val() + ' - ' + ($("#txtNote").val() || '');
        alert("Se generará tipificación.", "Informativo", function () {

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
            model = {};
            model.HeaderRequestTypeBpelModel = {};
            model.DetailCallRequestBpelModel = {};
            model.DetailCallRequestBpelModel.ContactUserBpelModel = {};
            model.DetailCallRequestBpelModel.CustomerClfyBpelModel = {};
            model.DetailCallRequestBpelModel.InteractionBpelModel = {};
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel = {};


            //headerRequest
            model.StrIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            model.StrTelephone = oHidden.hdntelephone;
            model.StrTransaction = oHidden.hidCodOpcion;
            model.StrMonthEmision = strMonth;
            model.StrYearEmision = $("#cboYear").val();
            if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                model.StrCodeTipification = oHidden.hdnTipiBuscarHFC;
            } else {
                model.StrCodeTipification = oHidden.hdnTipiBuscarLTE;
            }

            model.StrHdnType = oHidden.hdnType;
            model.StrHdnClase = oHidden.hdnClase;
            model.StrHdnSubClass = oHidden.hdnSubClass;

            //ContactUser
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Usuario = oSession.login;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Nombres = oCustomer.Name;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Apellidos = oCustomer.LastName;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.RazonSocial = oCustomer.BusinessName;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.TipoDoc = oCustomer.DocumentType;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.NumDoc = oCustomer.DocumentNumber;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Domicilio = oCustomer.Address;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Distrito = oCustomer.District;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Departamento = oCustomer.Departament;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Provincia = oCustomer.Province;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.Modalidad = oCustomer.Modality;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.CustomerId = oCustomer.CustomerID;
            model.DetailCallRequestBpelModel.ContactUserBpelModel.ContractId = oCustomer.ContractID;
            //model.DetailCallRequestBpelModel.ContactUserBpelModel.Nota = nota;

            //CustomerClfy
            model.DetailCallRequestBpelModel.CustomerClfyBpelModel.FlagReg = "1";
            model.DetailCallRequestBpelModel.CustomerClfyBpelModel.Account = oCustomer.Account;
            //Interaction
            model.DetailCallRequestBpelModel.InteractionBpelModel.Phone = "";
            model.DetailCallRequestBpelModel.InteractionBpelModel.Notas = strNota;
            model.DetailCallRequestBpelModel.InteractionBpelModel.CoId = oCustomer.ContractID;
            model.DetailCallRequestBpelModel.InteractionBpelModel.CodPlano = oCustomer.PlaneCodeBilling;
            model.DetailCallRequestBpelModel.InteractionBpelModel.Agente = oSession.login;

            //InteractPlus
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.ClaroNumber = oHidden.hdntelephone;//Linea
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.DniLegalRep = oCustomer.DocumentNumber;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.DocumentNumber = oCustomer.DocumentNumber;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.FirstName = oCustomer.Name;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.LastName = oCustomer.LastName;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.NameLegalRep = oCustomer.LegalAgent;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.FlagRegistered = "";
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Email = email;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter30 = strNota;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter29 = strMonth + "-" + $("#cboYear").val(); //Mes+Año
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter15 = $('#cboCacDac option:selected').text();
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter16 = "";
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Inter18 = oCustomer.ContractID;
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.Birthday = "";
            model.DetailCallRequestBpelModel.InteractionPlusBpelModel.ExpireDate = "";

            //DetalleLlamadasRequest
            model.DetailCallRequestBpelModel.TipoConsulta = "F";
            model.DetailCallRequestBpelModel.Msisdn = oHidden.hdntelephone; //Linea
            model.DetailCallRequestBpelModel.FlagConstancia = "";
            model.DetailCallRequestBpelModel.IpCliente = oHidden.hdnLocalAdd;
            model.DetailCallRequestBpelModel.TipoConsultaContrato = "T";
            model.DetailCallRequestBpelModel.ValorContrato = oHidden.hdntelephone;
            model.DetailCallRequestBpelModel.FlagContingencia = "";
            model.DetailCallRequestBpelModel.CodigoCliente = oCustomer.CustomerID;
            model.DetailCallRequestBpelModel.FlagEnvioCorreo = "";
            model.DetailCallRequestBpelModel.FlagGenerarOcc = "";
            model.DetailCallRequestBpelModel.InvoiceNumber = "";
            model.DetailCallRequestBpelModel.Periodo = $("#cboYear").val() + "" + strMonth; //Año+Mes
            model.DetailCallRequestBpelModel.TipoProducto = sessionTransac.UrlParams.SUREDIRECT;

            //Header
            model.HeaderRequestTypeBpelModel.UsuarioAplicacion = sessionTransac.SessionParams.USERACCESS.login;

            $.app.ajax({
                url: '/Transactions/HFC/CallDetails/GetSearch',
                type: 'POST',
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                cache: true,
                success: function (response) {
                    var registros = response.data.ListExportExcel;
                    Session.SessionParams.ListModels = registros;//por pendiente
                    $('#btnGuardar').attr('disabled', false);

                    if (registros.length > 0) {
                        var pagingBoolean = !controls.chkAllRecords.is(':checked');
                        dataSearch = registros;

                        HiddenPageHtml.FechaCicloIni = response.data.FechaCicloIni;
                        HiddenPageHtml.FechaCicloFin = response.data.FechaCicloFin;

                        that.InitLoadDetalleFacturacionHFC(registros, pagingBoolean, false);
                        $('#idTotal').text(response.data.Total);
                        //$('#idTotalSms').text(response.data.TotalSms);
                        //$('#idTotalMms').text(response.data.TotalMms);
                        $('#idTotalRegistro').text(response.data.TotalRegistro);
                        //$('#idCargoFinal').text(response.data.CargoFinal);
                        //$('#idTotalGprs').text(response.data.TotalGprs);
                    } else {
                        alert("No se encontro registro.", "Alerta");
                        //console.logresponse);
                    }
                },
                error: function () {
                    alert("Ocurrió un error interno, intente mas tarde.", "Alerta");
                }
            });
        });
    }
    return false;
};

function SendMail(pag, controls) {
    if (userValidatorAuth != "") {
        var objParameter = {
            strIdSession: sessionTransac.UrlParams.IDSESSION,
            codeUser: sessionTransac.SessionParams.USERACCESS.login,
            phonfNroGener: sessionTransac.SessionParams.DATACUSTOMER.Telephone,
            cuenta: sessionTransac.SessionParams.DATACUSTOMER.Account,
            currentUser: userValidatorAuth,
            currentTerminal: Session.SessionParams.HIDDEN.hdnLocalAdd
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

(function ($, undefined) {

 

    var Form = function ($element, options) {

        $.extend(this, $.fn.HFCBilledCallsDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
            //ComboBox

            , cboYear: $('#cboYear', $element)
            , cboMes: $('#cboMes', $element)

            , cboCacDac: $('#cboCacDac', $element)
            , tblDetalleFacturacionHFC: $('#tblDetalleFacturacionHFC', $element)

            //Button
            , btnSearch: $('#btnSearch', $element)
            , btnExport: $('#btnExport', $element)
            , btnPrint: $('#btnPrint', $element)
            , btnGuardar: $('#btnGuardar', $element)
            , btnConstancia: $('#btnConstancia', $element)
            , btnCerrar: $('#btnCerrar', $element)
            //Check box
            , chkSentEmail: $('#chkSentEmail', $element)
            , chkAllRecords: $('#chkAllRecords', $element)

            //loadSessionData
            , lblHeaderTelephone: $('#lblHeaderTelephone', $element)
            , lblHeaderTypeCustomer: $('#lblHeaderTypeCustomer', $element)
            //, lblHeaderContact: $('#lblHeaderContact', $element)
            , lblContactCustomer: $('#lblContactCustomer', $element)
            , lbContact: $('#lbContact', $element)
            , lblRepreLegal: $('#lblRepreLegal', $element)
            , lblDocument: $('#lblDocument', $element)
            //, lblDocRepresLegal: $('#lblDocRepresLegal', $element)
            , lblDateActivation: $('#lblDateActivation', $element)
            , lblCustomer: $('#lblCustomer', $element)
            , lblContract: $('#lblContract', $element)
            , lblCycleFacture: $('#lblCycleFacture', $element)
            , lblLimitCredit: $('#lblLimitCredit', $element)
            , txtDireccion: $('#txtDireccion', $element)
            , txtNotaDireccion: $('#txtNotaDireccion', $element)
            , txtDepartamento: $('#txtDepartamento', $element)
            , txtPais: $('#txtPais', $element)
            , txtDistrito: $('#txtDistrito', $element)
            , txtProvincia: $('#txtProvincia', $element)
            , txtCodigoPlano: $('#txtCodigoPlano', $element)
            , txtCodigoUbigeo: $('#txtCodigoUbigeo', $element)
            , myModalLoad: $('#myModalLoad', $element)
            , lblCustomerId: $('#lblCustomerId', $element)
            //, myModalLoad: $('#lblPlan', $element)

            , spnMainTitle: $('#spnMainTitle')
            , IdlblCodPlano: $('#IdlblCodPlano', $element)
            , IdlblCodUbigeo: $('#IdlblCodUbigeo', $element)

        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this, controls = this.getControls();
            controls.btnSearch.addEvent(that, 'click', that.btnSearch_Click);
            controls.btnExport.addEvent(that, 'click', that.btnExport_Click);
            controls.chkSentEmail.addEvent(that, 'click', that.chkSentEmail_Click);
            controls.btnPrint.addEvent(that, 'click', that.btnPrint_Click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_Click);
            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            controls.chkAllRecords.addEvent(that, 'change', that.refreshRecords_change);
            controls.cboCacDac.addEvent(that, 'change', that.cboCacDac_change);
            controls.IdlblCodPlano.hide();
            controls.IdlblCodUbigeo.hide();
            that.windowAutoSize();
            that.maximizarWindow();
            that.render();
        },

        render: function () {
            var that = this, controls = this.getControls();
            that.InitLoadDetalleFacturacionHFC();
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
            that.loadSessionData();
        },

        loadSessionData: function () {
            var that = this, oHidden = Session.SessionParams.HIDDEN, objModel = {};
            objModel.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/Transactions/HFC/CallDetails/AppConfig',
                success: function (response) {
                    oHidden.strVariableT = response.strVariableT;
                    oHidden.strMsgDatosLinea = response.strMsgDatosLinea;
                    oHidden.hdnSiteUrl = response.hdnSiteUrl;
                    oHidden.hdnLocalAdd = response.hdnLocalAdd;
                    oHidden.hdnServName = response.hdnServName;
                    oHidden.strTransactionLTEDetCallFac = response.strTransactionLTEDetCallFac;
                    oHidden.strConstOpcDetailCallsFact = response.strConstOpcDetailCallsFact;
                    //Buscar
                    oHidden.hdnTipiBuscarLTE = response.hdnTipificationBuscarLte;
                    oHidden.hdnTipiBuscarHFC = response.hdnTipificationBuscarHfc;
                    //Guardar 
                    oHidden.hdnTipiSaveLTE = response.hdnTipificationSaveLte;
                    oHidden.hdnTipiSaveHFC = response.hdnTipificationSaveHfc;
                    //Nuevo AppConfig
                    oHidden.hdnConstTransaction = response.hdnConstTransaction;
                    oHidden.hdnConststrService = response.hdnConststrService;
                    oHidden.hdnConstDescription = response.hdnConstDescription;

                    var pageIsPostBack = true;

                    if (sessionTransac.SessionParams.DATASERVICE.TelephonyValue != oHidden.strVariableT) {
                        alert(oHidden.strMsgDatosLinea, "Alerta", function () {
                            parent.window.close();
                        });

                        pageIsPostBack = false;
                        return false;
                    }else if (sessionTransac.SessionParams.USERACCESS == {} || sessionTransac.SessionParams.DATACUSTOMER == {}) {
                        alert("Se cancelo la sessión.", "Alerta", function () {
                            location.href = oHidden.hdnSiteUrl;
                        });
                        pageIsPostBack = false;
                        return false;
                    }

                    if (pageIsPostBack) {
                        that.Page_Load();
                        that.InitGetMessage();
                        that.InitEnabledPermission();
                        that.InitCustomerPhone();
                        that.InitCacDat();
                        that.InitGetMonthYearLimit();
                        that.InitTypification();
                        that.InitLoadYear();
                        that.InitMonth();
                    }

                    return false;
                }
            });

            return false;
        },

        Page_Load: function () {

            var that = this,
                controls = that.getControls(),
                oCustomer = sessionTransac.SessionParams.DATACUSTOMER,
                oHidden = Session.SessionParams.HIDDEN,
                oDatLine = sessionTransac.SessionParams.DATASERVICE;

            //Cabezera
            controls.lblHeaderTelephone.text(oCustomer.Telephone);
            controls.lblHeaderTypeCustomer.text(oCustomer.CustomerType);
            //controls.lblHeaderContact.text(oCustomer.CustomerID);

            //Contenido
            controls.lblContract.text(oCustomer.ContractID);
            controls.lblContactCustomer.text(oCustomer.CustomerContact);
            controls.lbContact.text(oCustomer.FullName);
            controls.lblRepreLegal.text(oCustomer.LegalAgent);
            controls.lblDocument.text(oCustomer.DNIRUC);
            //controls.lblDocRepresLegal.text(oCustomer.DocumentNumber);
            controls.lblCustomer.text(oCustomer.BusinessName);
            controls.lblCycleFacture.text(oCustomer.BillingCycle);
            controls.lblLimitCredit.text(oCustomer.objPostDataAccount.CreditLimit);
            controls.lblDateActivation.text(oCustomer.ActivationDate);
            $('#lblPlanL').text(oDatLine.Plan);
            //controls.lblPlan.text(oDatLine.Plan);

            //Direccion
            controls.txtDireccion.text(oCustomer.InvoiceAddress);
            controls.txtNotaDireccion.text(oCustomer.Reference);
            controls.txtDepartamento.text(oCustomer.Departament);
            controls.txtPais.text(oCustomer.LegalCountry);
            controls.txtDistrito.text(oCustomer.District);
            controls.txtProvincia.text(oCustomer.Province);
            controls.txtCodigoPlano.text(oCustomer.PlaneCodeBilling);
            controls.txtCodigoUbigeo.text(oCustomer.InvoiceUbigeo);
            controls.lblCustomerId.text(oCustomer.CustomerID);

            //SESSION.HIDDEN
            var d = new Date();
            oHidden.hidTransaccion = "TRANSACCION_DETALLE_LLAMADAS";
            oHidden.hidCodCliente = oCustomer.CustomerID;
            oHidden.hidInvoiceNumber = "";
            oHidden.hidFechaActual = d.getFullYear() + "" + "00";
            oHidden.hidTransaccionDetalleLlamada = "TRANSACCION_DETALLE_LLAMADAS";
            oHidden.hidFlagPlataforma = oDatLine.FlagPlatform;
            oHidden.hidContador = 0;

            //Validacion
            if (oHidden.hidFlagPlataforma == "C") {
                controls.lblDateActivation.text(oCustomer.ActivationDate);
            } else if (oHidden.hidFlagPlataforma == "P") {
                controls.lblDateActivation.text(oDatLine.ActivationDate);
            }

            //if (oCustomer.Modality != null) {
            //    if (oCustomer.Modality.toUpperCase() == "PARTICULAR") {
            //        $('#txtSendforEmail').attr('disabled', false);
            //    } else {
            //        $('#txtSendforEmail').attr('disabled', true);
            //    }
            //} else {
            //    $('#txtSendforEmail').attr('disabled', true);
            //}
            
            $('#txtSendforEmail').attr('disabled', false);
            $('#btnGuardar').attr('disabled', true); 
            $('#btnPrint').attr('disabled', true);
            $('#btnExport').attr('disabled', true);
            $('#btnConstancia').attr('disabled', true);
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        getControls: function () {
            return this.m_controls || {};
        },

        chkSentEmail_Click: function () {
            var oCustomer = sessionTransac.SessionParams.DATACUSTOMER;
            var value = $("#chkSentEmail").is(':checked');
            if (value == true) {
                $("#txtSendforEmail").show();
                $("#txtSendforEmail").val(oCustomer.Email);
            } else {
                $("#txtSendforEmail").hide();
                $("#txtSendforEmail").val("");
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

        InitCacDat: function () {
            var userId = sessionTransac.SessionParams.USERACCESS.userId;
            var login = sessionTransac.SessionParams.USERACCESS.login;
            var idDiv = "cboCacDac";
            LoadCacDac(userId, login, idDiv);
        },

        InitGetMonthYearLimit: function () {
            var that = this;
            var objModel = {};
            objModel.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/Transactions/HFC/CallDetails/GetMonthYearLimit',
                success: function (response) {
                    Session.SessionParams.HIDDEN.hiddenMonthYearLimit = response; //otras funciones no lo utilize
                }
            });
        },

        InitGetMessage: function () {
            var that = this,
                controls = that.getControls(),
                objModel = {};
            objModel.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            $.app.ajax({
                async: true,
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: '/Transactions/HFC/CallDetails/GetMessage',
                success: function (response) {
                    Session.SessionParams.MESSAGEHFC.tituloPagina = response[0];
                    Session.SessionParams.MESSAGEHFC.Message1 = response[1];
                    Session.SessionParams.MESSAGEHFC.Message2 = response[2];
                    Session.SessionParams.MESSAGEHFC.Message3 = response[3];
                    Session.SessionParams.MESSAGEHFC.Message4 = response[4];
                    Session.SessionParams.MESSAGEHFC.Message5 = response[5];
                    Session.SessionParams.MESSAGEHFC.Message6 = response[6];
                    Session.SessionParams.MESSAGEHFC.Message7 = response[7];
                    Session.SessionParams.MESSAGEHFC.Message8 = response[8];
                    Session.SessionParams.MESSAGEHFC.Message9 = response[9];
                    Session.SessionParams.MESSAGEHFC.Message10 = response[10];
                    Session.SessionParams.MESSAGEHFC.Message11 = response[11];
                    Session.SessionParams.MESSAGEHFC.Message12 = response[12];
                    Session.SessionParams.MESSAGEHFC.Message13 = response[13];
                    Session.SessionParams.MESSAGEHFC.Message14 = response[14];
                    Session.SessionParams.MESSAGEHFC.Message15 = response[15];
                    Session.SessionParams.MESSAGEHFC.Message16 = response[16];
                    Session.SessionParams.MESSAGEHFC.Message17 = response[17];
                    Session.SessionParams.MESSAGEHFC.Message18 = response[18];
                    Session.SessionParams.MESSAGEHFC.Message19 = response[19];
                    Session.SessionParams.MESSAGEHFC.hidMsgConsBusca = response[20];
                    Session.SessionParams.HIDDEN.hdnServName = response[21];
                    Session.SessionParams.HIDDEN.hdnLocalAdd = response[22];
                    Session.SessionParams.HIDDEN.UserHostName = response[22];
                    Session.SessionParams.HIDDEN.hidCodOpcion = response[24];

                    if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                        controls.spnMainTitle.text("DETALLE DE LLAMADAS SALIENTES FACTURADAS");
                        $('#header-text').text('Detalle De Llamadas Salientes Facturadas HfC');
                        controls.IdlblCodPlano.show();
                        
                    } else {
                        controls.spnMainTitle.text("DETALLE DE LLAMADAS SALIENTES FACTURADAS");
                        $('#header-text').text('Detalle De Llamadas Salientes Facturadas LTE');
                        controls.IdlblCodUbigeo.show();
                    }

                    HiddenPageAuth.hidOpcion = response[24];
                    //$('#hidOpcion').val(response[24]);
                }
            });
        },

        InitEnabledPermission: function () {
            var that = this;
            var model = {};
            model.StrCadOption = sessionTransac.SessionParams.USERACCESS.optionPermissions;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/HFC/CallDetails/EnabledPermission',
                success: function (response) {
                    if (response[0].split('/')[0] === "chkSentEmail") {
                        if (response[0].split('/')[1] === "False") {
                            $("#idHdnSendEmail").hide();
                        } else {
                            $("#chkSentEmail").attr('disabled', false);
                        }
                    }
                    if (response[1].split('/')[0] === "btnSecurity") {
                        Session.SessionParams.ENABLEDPERMISION.btnSecurity = response[1].split('/')[1];
                    }
                    if (response[2].split('/')[0] === "hdnPermission") {
                        Session.SessionParams.ENABLEDPERMISION.hdnPermission = response[2].split('/')[1];
                    }
                    if (response[3].split('/')[0] === "hdnPermisionExport") {
                        Session.SessionParams.ENABLEDPERMISION.hdnPermisionExport = response[3].split('/')[1];
                    }
                    //if (response[4].split('/')[0] === "btnExport") {
                    //    if (response[4].split('/')[1] === "False") {
                    //        $("#btnExport").hide();
                    //    } else {
                    //        $("#btnExport").attr('disabled', true);
                    //    }
                    //}
                    if (response[5].split('/')[0] === "btnPrint") {
                        if (response[5].split('/')[1] === "False") {
                            $("#btnPrint").hide();
                        } else {
                            $("#btnPrint").attr('disabled', true);
                        }
                    }
                    if (response[6].split('/')[0] === "hdnPermissionBus") {
                        Session.SessionParams.ENABLEDPERMISION.hdnPermissionBus = response[6].split('/')[1];
                    }
                }
            });
        },

        InitCustomerPhone: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            model.intIdContract = sessionTransac.SessionParams.DATACUSTOMER.ContractID;
            model.strTypeProduct = sessionTransac.UrlParams.SUREDIRECT;

            $.ajax({
                url: '/Transactions/HFC/CallDetails/GetCustomerPhone',
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    if (response != null) {
                        Session.SessionParams.HIDDEN.hdntelephone = response;
                    } else {
                        alert("No existe una linea de Telefono.", "Alerta");
                    }
                },
                error: function () {
                    alert("Ocurrió un error interno, intente mas tarde.", "Alerta");
                }
            });
        },

        InitTypification: function () {
            var that = this;
            var parameters = {};
            var oHidden = Session.SessionParams.HIDDEN;
            parameters.strIdSession = sessionTransac.SessionParams.USERACCESS.userId;
            if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                parameters.strTransactionName = oHidden.hdnTipiBuscarHFC;
            } else {
                parameters.strTransactionName = oHidden.hdnTipiBuscarLTE;
            }

            parameters.strType = sessionTransac.UrlParams.SUREDIRECT;

            $.app.ajax({
                url: '/Transactions/CommonServices/GetTypification',
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    $.each(response.ListTypification, function (index, value) {
                        if (value.CLASE != null) {
                            oHidden.hdnClaseCode = value.CLASE_CODE;
                            oHidden.hdnSubClassCode = value.SUBCLASE_CODE;
                            oHidden.hdnType = value.TIPO;
                            oHidden.hdnSubClass = value.SUBCLASE;
                            oHidden.hdnInteractionCode = value.INTERACCION_CODE;
                            oHidden.hdnClase = value.CLASE;
                            oHidden.HdnTypeCode = value.TIPO_CODE;
                        } else {
                            alert("No se encontro la tipificacion.", "Alerta");
                        }
                    });
                }
            });
        },

        InitLoadYear: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            model.strNameFunction = "ListaAnnosLlamada";
            model.strFlagCode = "";
            model.fileName = "Data.xml";

            var fullDate = new Date();
            var strYear = fullDate.getFullYear();
            var yearExistent = false;
            var myUrl = "/Transactions/CommonServices/GetListValueXmlMethod";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    if (response.data != null) {
                        var item = "";
                        $('#cboYear').html("");
                        item += "<option value=''>Seleccionar</option>";
                        $.each(response.data, function (index, value) {
                            if (value.Code == strYear) {
                                item += "<option selected value='" + value.Code + "'>" + value.Description + "</option>";
                                yearExistent = true;
                            } else {
                                item += "<option value='" + value.Code + "'>" + value.Description + "</option>";
                            }

                        });
                        if (!yearExistent) {
                            item += "<option selected value='" + strYear + "'>" + strYear + "</option>";
                        }

                        $('#cboYear').html(item);
                    }
                },
                error: function () {
                    alert("Ocurrió un error interno, intente mas tarde.", "Alerta");
                }
            });
        },

        InitMonth: function () {
            var that = this, controls = that.getControls(), model = {};
            model.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            model.strNameFunction = "ListaMeses";
            model.strFlagCode = "";
            model.fileName = "Data.xml";

            var fullDate = new Date();
            var strMonth = fullDate.getMonth();

            var myUrl = "/Transactions/CommonServices/GetListValueXmlMethod";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    var item = "";
                    $('#cboMes').html("");
                    if (response.data != null) {
                        item += "<option value=''>Seleccionar</option>";
                        $.each(response.data, function (index, value) {
                            if (value.Code == strMonth) {
                                item += "<option selected value='" + value.Code + "'>" + value.Description + "</option>";
                            } else {
                                item += "<option value='" + value.Code + "'>" + value.Description + "</option>";
                            }
                        });
                        $('#cboMes').html(item);
                    }
                },
                error: function () {
                    alert("Ocurrió un error interno, intente mas tarde.", "Alerta");
                }
            });
        },
        //Acciones
        refreshRecords_change: function () {
            var that = this, controls = that.getControls();
            var pagingBoolean = !controls.chkAllRecords.is(':checked');
            that.InitLoadDetalleFacturacionHFC(dataSearch, pagingBoolean, false);
        },

        btnSearch_Click: function () {
            var that = this, controls = that.getControls();
            confirm("¿Está seguro de realizar la consulta?", "Confirmar", function (result) {
                if (result) {
                    controls = that.getControls();
                    //if (Session.SessionParams.ENABLEDPERMISION.hdnPermissionBus === 'SI') {
                        if ($("#cboCacDac").val() == "") {
                            alert(Session.SessionParams.MESSAGEHFC.Message17, "Alerta");
                            return;
                        } else {
                            SearchCustomer(that, controls);
                        }

                    //} else {
                    //    if ($("#cboCacDac").val() == "") {
                    //        alert(Session.SessionParams.MESSAGEHFC.Message17, "Alerta");
                    //        return;
                    //    } else {
                    //        //SearchCustomer(that, controls);
                    //        var co = Session.SessionParams.HIDDEN.hidCodOpcion;
                    //        var param =
                    //        {
                    //            "strIdSession": sessionTransac.SessionParams.USERACCESS.userId,
                    //            'pag': '1',
                    //            'opcion': 'BUS',
                    //            'co': co,
                    //            'telefono': Session.SessionParams.HIDDEN.hdntelephone
                    //        };
                    //        ValidateAccess(that, controls, 'BUS', 'gConstEvtBuscarDetaLlamadaLin', '1', param, 'Fixed');
                    //    }

                    }                    
            });
        },

        InitLoadDetalleFacturacionHFC: function (registros, pagingBoolean, collapse) {
            var that = this,
            controls = that.getControls();
            controls.tblDetalleFacturacionHFC.DataTable({
                info: false
                ,scrollY: 300
                , select: "single"
                , paging: pagingBoolean || false
                , scrollCollapse: collapse == null ? true : collapse
                , searching: false
                , scrollX: true
                , destroy: true
                , sort: false
                , data: registros
                , language: {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                },
                columns: [
                    { "data": "CurrentNumber" },
                    { "data": "StrDate" },
                    { "data": "StrHour" },
                    { "data": "DestinationPhone" },
                    { "data": "NroCustomer" },
                    { "data": "Consumption", "sClass": "text-right" },
                    { "data": "CargOriginal", "sClass": "text-right" },
                    { "data": "TypeCalls" },
                    { "data": "Destination" },
                    { "data": "Operator" }
                ]
            });
        },

        btnExport_Click: function () {
            var that = this, controls = this.getControls();
            var textDefault = $('#tblDetalleFacturacionHFC tbody tr :first').text();
            if (textDefault == "No existen datos") {
                alert("No hay datos para exportar.", "Alerta");
                return false;
            } else {
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
                if (Session.SessionParams.ENABLEDPERMISION.hdnPermisionExport == "SI") {
                    DowloadDetailLlamadaHFC(that, controls);
                } else {
                    //DowloadDetailLlamadaHFC(that, controls);
                    var co = Session.SessionParams.HIDDEN.hidCodOpcion;
                    var param = {
                        "strIdSession": sessionTransac.SessionParams.USERACCESS.userId,
                        'pag': '1',
                        'opcion': 'EXP',
                        'co': co,
                        'telefono': Session.SessionParams.HIDDEN.hdntelephone
                    };
                    ValidateAccess(that, controls, 'EXP', 'gConstEvtExportarDetalleLlamada', '1', param, 'Fixed');
                }
                return false;
            }

        },

        btnPrint_Click: function () {
            var that = this, controls = this.getControls();
            var textDefault = $('#tblDetalleFacturacionHFC tbody tr :first').text();
            if (textDefault == "No existen datos") {
                alert("No hay datos para Inprimir.", "Alerta");
                return false;
            } else {
                if (Session.SessionParams.ENABLEDPERMISION.hdnPermisionExport == "SI") {
                    PrintDetailLlamadaHFC(that, controls);
                } else {
                    //PrintDetailLlamadaHFC(that, controls);
                    var co = Session.SessionParams.HIDDEN.hidCodOpcion;
                    var param = {
                        "strIdSession": sessionTransac.SessionParams.USERACCESS.userId,
                        'pag': '1',
                        'opcion': 'IMP',
                        'co': co,
                        'telefono': Session.SessionParams.HIDDEN.hdntelephone
                    };
                    ValidateAccess(that, controls, 'IMP', 'gConstEvtImprimirDetalleLlamada', '1', param, 'Fixed');
                }

                return false;
            }
        },
        btnConstanciaSendMail_Click: function () {
            var that = this, controls = that.getControls(), oCustomer = sessionTransac.SessionParams.DATACUSTOMER;
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

            if (sessionTransac.SessionParams.USERACCESS == {} || sessionTransac.SessionParams.DATACUSTOMER == {}) {
                parent.window.close();
                opener.parent.top.location.href = Session.SessionParams.HIDDEN.hdnSiteUrl;
                return false;
            }
            else
            {
                var model = {};
                model.strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
                model.strTitle = oCustomer.FullName;
                model.strRepresentant = oCustomer.LegalAgent;
                model.strTypeDoc = oCustomer.DocumentType;
                model.strNroDoc = oCustomer.DocumentNumber;
                model.strCustomerId = oCustomer.CustomerID;
                model.strCacDac = $('#cboCacDac option:selected').text();

                model.strInteraccionId = HiddenPageHtml.hidCasoId;

                if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                    model.strTypeTransaction = "DetalleLlamada_SalienteFacturado_HFC";
                } else {
                    model.strTypeTransaction = "DetalleLlamada_SalienteFacturado_LTE";
                }

                model.strTypeProduct = sessionTransac.UrlParams.SUREDIRECT;
                model.modelSave = modelTempSave;
                model.fechaCicloIni = HiddenPageHtml.FechaCicloIni;
                model.fechaCicloFin = HiddenPageHtml.FechaCicloFin;

                $.ajax({
                    url: '/Transactions/HFC/CallDetails/HfcBilledCallsDetailConstancy',
                    type: 'POST',
                    data: JSON.stringify(model),
                    contentType: 'application/json charset=utf-8',
                    dataType: 'json',
                    success: function (response)
                    {
                        if (response.Generated) {
                            var param = {};
                            param.FlagBill = "1";
                            routeConstancy = response.FullPathPDF;
                            alert("La transacción de realizó correctamente.", "Informativo");
                            //ReadRecordSharedFile(sessionTransac.SessionParams.USERACCESS.userId, that.strPDFRoute);
                        }
                        else
                        {
                            alert("Ocurrió un error generando la constancia.", "Alerta");
                        } 
                    },
                    error: function () {
                        alert("Ocurrió un error interno, intente mas tarde.", "Alerta");
                    }
                });
            }
            return false;
        },

        btnConstancia_Click: function () {
            var that = this, controls = that.getControls(), oCustomer = sessionTransac.SessionParams.DATACUSTOMER;
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
            if (routeConstancy !== "") {
                ReadRecordSharedFile(sessionTransac.SessionParams.USERACCESS.userId, routeConstancy);
            }
        },

        btnGuardar_Click: function () {
            var strMonth = "";
            var that = this,
                controls = that.getControls(),
                oHidden = Session.SessionParams.HIDDEN,
                oCustomer = sessionTransac.SessionParams.DATACUSTOMER,
                oMessage = Session.SessionParams.MESSAGEHFC,
                model = {};

            var textDefault = $('#tblDetalleFacturacionHFC tbody tr :first').text();
            if (textDefault == "No existen datos") {
                alert("No existe Datos en la tabla de la busqueda.", "Alerta");
                return false;
            } else {
                if ($("#cboMes").val() == "") {
                    alert(oMessage.Message1, "Alerta");
                    return false;
                } else {
                    strMonth = $("#cboMes").val().length;
                    if (strMonth == 1) {
                        strMonth = "0" + $("#cboMes").val();
                    } else {
                        strMonth = $("#cboMes").val();
                    }
                }
                if ($("#cboYear").val() == "") {
                    alert(oMessage.Message10, "Alerta");
                    return false;
                }

                if ($("#cboCacDac").val() == "") {
                    alert(oMessage.Message17, "Alerta");
                    return false;
                }
                //if ($("#txtNote").val().length > 3800) {
                //    alert(oMessage.Message19);
                //    return false;
                //}
                var email = "";
                if ($("#chkSentEmail").is(':checked')) {
                    email = $("#txtSendforEmail").val();
                    if ($("#txtSendforEmail").val() == "") {
                        alert(oMessage.Message5, "Alerta");
                        return false;
                    }

                    var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                    var blvalidate = regx.test($("#txtSendforEmail").val());
                    if (blvalidate == false) {
                        alert(oMessage.Message6, "Alerta");
                        return false;
                    }
                }

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

                model.Transaction = oHidden.hidTransaccion;
                model.Telephone = oHidden.hdntelephone;
                model.Note = $("#txtNote").val();
                model.Email = email;

                if (sessionTransac.UrlParams.SUREDIRECT == "HFC") {
                    model.CodeTipification = oHidden.hdnTipiSaveHFC;
                } else {
                    model.CodeTipification = oHidden.hdnTipiSaveLTE;
                }
                model.CodeTipification = oHidden.hdnTipiGuardar;
                model.MonthEmision = strMonth;
                model.YearEmision = $("#cboYear").val();
                model.StardDate = "";
                model.EndDate = "";
                model.CacDac = $("#cboCacDac").val();
                model.Sn = oHidden.hdnServName;
                model.IpServidor = oHidden.hdnLocalAdd;
                model.CustomerId = oCustomer.CustomerID;
                model.ContractId = oCustomer.ContractID;
                model.IdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
                model.CodePlanInst = oCustomer.CodeCenterPopulate;
                model.Plan = oCustomer.Plan;
                model.NameComplet = oCustomer.FullName;
                model.TypeClient = oCustomer.CustomerType;
                model.RazonSocial = oCustomer.BusinessName;//RAZON_SOCIAL
                model.Periodo = strMonth + "" + $("#cboYear").val();

                model.Cuenta = oCustomer.Account;
                model.TypeDoc = oCustomer.DocumentType;
                model.NroDoc = oCustomer.DocumentNumber;
                model.RepresentLegal = oCustomer.LegalAgent;//REPRESENTANTE_LEGAL
                model.DescCacDac = $("#cboCacDac option:selected").text();

                model.StrDistrict = oCustomer.District;
                model.StrDepartament = oCustomer.Departament;
                model.StrProvince = oCustomer.Province;
                model.StrModality = oCustomer.Modality;
                model.CurrentUser = sessionTransac.SessionParams.USERACCESS.login;
                model.LastName = oCustomer.LastName;

                model.ListExportExcel = "";
                model.product = sessionTransac.UrlParams.SUREDIRECT;                

                $.app.ajax({
                    url: '/Transactions/HFC/CallDetails/Save',
                    type: 'POST',
                    data: JSON.stringify(model),
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    success: function (response) {
                        //console.logresponse);
                        if (response[0] == "NO OK") {
                            alert(oMessage.Message15, "Alerta");
                        } else
                        {
                            modelTempSave = model;
                            HiddenPageHtml.hidCasoId = response[3];
                            //$("#hidCasoId").val(response[3]);

                            $("#btnGuardar").prop("disabled", true);
                            $("#btnSearch").prop("disabled", true);
                            $("#cboMes").prop("disabled", true);
                            $("#cboYear").prop("disabled", true);

                            $("#cboCacDac").prop("disabled", true);
                            $("#btnConstancia").prop("disabled", false);
                            $("#btnPrint").prop("disabled", false);
                            $("#btnExport").prop("disabled", false);

                            that.btnConstanciaSendMail_Click();
                        }
                    },
                    error: function () {
                        alert("Ocurrió un error al realizar la petición.", "Alerta");
                    }
                });
                return false;
            }
        },

        btnCerrar_Click: function () {
            parent.window.close();
        },

        cboCacDac_change: function () {
            var that = this, controls = this.getControls(), oHidden = Session.SessionParams.HIDDEN, oCustomer = sessionTransac.SessionParams.DATACUSTOMER;

            var strTransaction = oHidden.hdnConstTransaction;
            var strService = oHidden.hdnConststrService;
            var strText = $('#cboCacDac option:selected').text() + " " + oHidden.hdnConstDescription;
            var strTelephone = Session.SessionParams.HIDDEN.hdntelephone;
            var strNameCustomer = oCustomer.FullName;
            var strIdSession = "50548795"; //sessionTransac.UrlParams.IDSESSION;
            var strIpCustomer = "";
            var strCuentUser = sessionTransac.SessionParams.USERACCESS.login;
            var strMontoInput = "0";
            SaveAudtiCacDac(strTransaction, strService, strText, strTelephone, strNameCustomer, strIdSession, strIpCustomer, strCuentUser, strMontoInput);
        },

        strUrl: '/Transactions/HFC/'
    };

    $.fn.HFCBilledCallsDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCBilledCallsDetail'),
                options = $.extend({}, $.fn.HFCBilledCallsDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCBilledCallsDetail', data);
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

    $.fn.HFCBilledCallsDetail.defaults = {
    }

    $('#divBody').HFCBilledCallsDetail();
})(jQuery);