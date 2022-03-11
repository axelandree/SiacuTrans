var Session = {};
var SessionNX = {};
var objReg = {};
var TYPIFICATION = {
    ClaseId: "",
    SubClaseId: "",
    Tipo: "",
    ClaseDes: "",
    SubClaseDes: "",
    TipoId: "",
};

(function ($, undefined) {
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
    Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
    Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
    Session.LINEA = SessionTransac.SessionParams.DATASERVICE;
    Session.ACCESO = SessionTransac.SessionParams.USERACCESS;

    var Form = function ($element, options) {
        $.extend(this, $.fn.INTNetflixServiceHFC.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , lblTitle: $('#lblTitle', $element)
            , txtLinea: $('#txtLinea', $element)
            , txtNombres: $('#txtNombres', $element)
            , txtApellidos: $('#txtApellidos', $element)
            , txtEmail: $('#txtEmail', $element)
            , chkCorreo: $('#chkCorreo', $element)
            , chkLinea: $('#chkLinea', $element)
            , cboCacDac: $('#cboCacDac', $element)
            , txtNotas: $("#txtNotas", $element)
            , btnGuardar: $('#btnGuardar', $element)
            , btnConstancia: $('#btnConstancia', $element)
            , btnCerrar: $('#btnCerrar', $element)
            , hidIdInteraccion: $('#hidIdInteraccion', $element)
        });
    };

    Form.prototype = {
        constructor: Form,

        init: function () {
            var that = this,
            controls = this.getControls();
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_Click);
            
            SessionNX.strEstadoContratoInactivo = strEstadoContratoInactivo;
            SessionNX.strEstadoContratoSuspendido = strEstadoContratoSuspendido;
            SessionNX.strEstadoContratoReservado = strEstadoContratoReservado;
            SessionNX.strMsjEstadoContratoInactivo = strMsjEstadoContratoInactivo;
            SessionNX.strMsjServicioContrato = strMsjServicioContrato;
            SessionNX.strMsjEstadoContratoReservado = strMsjEstadoContratoReservado;

            that.maximizarWindow();
            that.windowAutoSize();
            that.loadSessionData();
            that.render();
        },
        changeDateChangeData: function () {
            var that = this,
                controls = this.getControls();
        },
        render: function () {
            var that = this,
            control = that.getControls();
            control.btnConstancia.prop('disabled', true);
            that.Loading();
            that.loadCustomerData();
            that.IniTypification();
            that.InitCacDac();
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
        IniBegin: function () {
            var that = this,
            controls = this.getControls();

            that.IniLoadPage();
        },
        loadSessionData: function () {
            var that = this,
                controls = this.getControls();

            if (Session.LINEA.StateLine == SessionNX.strEstadoContratoInactivo) {
                controls.btnGuardar.prop('disabled', true);
                alert(SessionNX.strMsjEstadoContratoInactivo, 'Alerta', function () {
                    parent.window.close();
                });
                that.BlockControl();
            }
        },
        loadCustomerData: function () {
            var that = this;
            var controls = this.getControls();
            controls = that.getControls();
            controls.lblTitle.text("REENVIO DE CORREO ELECTRONICO Y SMS PARA REGISTRO EN NETFLIX");
            //********** Datos del Cliente ***********/
            controls.txtLinea.val((Session.CLIENTE.CustomerID == null) ? '' : Session.CLIENTE.CustomerID);
            controls.txtNombres.val((Session.CLIENTE.Name == null) ? '' : Session.CLIENTE.Name);
            controls.txtApellidos.val((Session.CLIENTE.LastName == null) ? '' : Session.CLIENTE.LastName);
            controls.txtEmail.val((Session.CLIENTE.Email == null) ? '' : Session.CLIENTE.Email);

            var obj = {
                strIdSession: Session.IDSESSION,
                strIdTransaccion: Session.IDSESSION,
                intCoId: Session.CLIENTE.ContractID
            };
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(obj),
                url: '/Transactions/HFC/NetflixServices/validaServicioContratado',
                error: function (response) {
                    alert(response.data);
                },
                success: function (response) {
                    if (response.data != "OK") {
                        controls.btnGuardar.prop('disabled', true);
                        alert(SessionNX.strMsjServicioContrato, 'Alerta', function () {
                            parent.window.close();
                        });
                        that.BlockControl();
                    }
                }
            });
        },
        validaEnvioConstancia: function () {
            var that = this,
                controls = that.getControls();

            if ((!$('#chkCorreo').is(':checked')) && (!$('#chkLinea').is(':checked'))) {
                alert("Seleccione al menos uno (o ambos) de los medios para el reenvío del link.", 'Validación', function () {
                    controls.chkCorreo.focus();
                }); return false;
            }

            if ($('#cboCacDac option:selected').val() < 0) {
                alert("Seleccionar un centro de atención", 'Validación', function () {
                    controls.cboCacDac.focus();
                }); return false;
            }

            if ((controls.txtNotas.val() == null) || (controls.txtNotas.val() == undefined) || (controls.txtNotas.val() == "")) {
                alert("Ingresar un texto descriptivo de la transacción", 'Validación', function () {
                    controls.txtNotas.focus();
                });
                return false;
            }

            return true;
        },
        modelSendRegister: {},
        btnGuardar_Click: function () {
            var that = this,
                controls = this.getControls();

            if (!that.validaEnvioConstancia()) {
                return false;
            }

            confirm("¿Seguro que desea continuar?", 'Confirmar', function () {
                that.Loading();
                that.saveTransactionSendRegister();

            }, function () {
                //$("#hidAccion").val("");
                return false;
            });
        },
        btnCerrar_Click: function () {
            parent.window.close();
        },
        saveTransactionSendRegister: function () {
            var that = this,
                controls = that.getControls();

            that.modelSendRegister.strIdSession = Session.IDSESSION;
            that.modelSendRegister.strIdTransaccion = Session.IDSESSION;
            that.modelSendRegister.linea = controls.txtLinea.val();
            that.modelSendRegister.notificaEmail = $('#chkCorreo').is(':checked') == true ? 1 : 0;
            that.modelSendRegister.notificaSMS = $('#chkLinea').is(':checked') == true ? 1 : 0;
            that.modelSendRegister.notifica = ((that.modelSendRegister.notificaEmail == 1) || (that.modelSendRegister.notificaSMS == 1)) ? 1 : 0;
            that.modelSendRegister.email = controls.txtEmail.val();
            that.modelSendRegister.referencia = Session.CLIENTE.Reference;

            that.modelSendRegister.departamento = Session.CLIENTE.Departament;
            that.modelSendRegister.provincia = Session.CLIENTE.Province;
            that.modelSendRegister.distrito = Session.CLIENTE.District;

            that.modelSendRegister.tipo = TYPIFICATION.Tipo;
            that.modelSendRegister.claseDes = TYPIFICATION.ClaseDes;
            that.modelSendRegister.subClaseDes = TYPIFICATION.SubClaseDes;
            that.modelSendRegister.claseCode = TYPIFICATION.ClaseId;
            that.modelSendRegister.subClaseCode = TYPIFICATION.SubClaseId;
            that.modelSendRegister.tipoCode = TYPIFICATION.TipoId;

            that.modelSendRegister.strNombres = Session.CLIENTE.Name;
            that.modelSendRegister.strApellidos = Session.CLIENTE.FullName;
            that.modelSendRegister.strfullNameUser = Session.CLIENTE.LegalAgent;
            that.modelSendRegister.DNI_RUC = Session.CLIENTE.DocumentNumber;
            that.modelSendRegister.strTipoDocumento = Session.CLIENTE.DocumentType;
            that.modelSendRegister.strCacDac = $('#cboCacDac option:selected').text();
            that.modelSendRegister.currentUser = Session.ACCESO.login;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(that.modelSendRegister),
                url: '/Transactions/HFC/NetflixServices/saveTransactionNetflixServices',
                error: function (response) {
                    alert(response.data);
                },
                success: function (response) {
                    if (response.data !== null) {
                        if (response.flag == "0") {
                            if (response.id !== "") {
                                controls.hidIdInteraccion.value = response.id;
                                alert(response.data, 'Alerta');
                                objReg = {};
                                objReg.interaccion = response.id;
                                objReg.linea = that.modelSendRegister.linea;
                                objReg.cac = that.modelSendRegister.strCacDac;
                                objReg.legal = that.modelSendRegister.strfullNameUser;
                                objReg.cliente = that.modelSendRegister.strNombres + ' ' + that.modelSendRegister.strApellidos;
                                objReg.numero = that.modelSendRegister.DNI_RUC;
                                objReg.tipo = that.modelSendRegister.strTipoDocumento;
                                objReg.correo = that.modelSendRegister.correo;
                                objReg.email = that.modelSendRegister.notificaEmail == 1 ? that.modelSendRegister.email : "";
                                objReg.asesor = Session.ACCESO.fullName + ' - ' + Session.ACCESO.login;

                                if (that.modelSendRegister.notifica == "1") {
                                    objReg.notifica = "Si";
                                } else if (that.modelSendRegister.notifica == "0") {
                                    objReg.notifica = "No";
                                }
                                controls.txtNotas.prop('disabled', true);
                                controls.cboCacDac.prop('disabled', true);
                                controls.chkCorreo.prop('disabled', true);
                                controls.chkLinea.prop('disabled', true);
                                controls.btnGuardar.prop('disabled', true);
                                controls.btnConstancia.prop('disabled', false);
                            } else {
                                controls.btnGuardar.prop('disabled', true);
                                alert(response.data, 'Alerta');
                                that.BlockControl();
                            }
                        } else {
                            controls.btnGuardar.prop('disabled', true);
                            alert(response.data, 'Alerta');
                            that.BlockControl();
                        }
                    }
                }
            });
        },
        IniTypification: function () {
            var obj = { strIdSession: Session.IDSESSION };
            $.app.ajax({
                type: 'POST',
                cache: false,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(obj),
                url: '/Transactions/HFC/NetflixServices/PageLoad',
                success: function (response) {
                    if (response.data != null) {
                        TYPIFICATION.ClaseId = response.data.CLASE_CODE;
                        TYPIFICATION.SubClaseId = response.data.SUBCLASE_CODE;
                        TYPIFICATION.Tipo = response.data.TIPO;
                        TYPIFICATION.ClaseDes = response.data.CLASE;
                        TYPIFICATION.SubClaseDes = response.data.SUBCLASE;
                        TYPIFICATION.TipoId = response.data.TIPO_CODE;
                    }
                }
            });
        },
        InitCacDac: function () {
            var that = this,
                controls = that.getControls();
            var objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.ACCESO.login;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: '/Transactions/CommonServices/GetUsers',
                success: function (results) {
                    var cacdac = results.Cac;
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {
                            controls.cboCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {
                                if (cacdac === value.Description) {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                    itemSelect = value.Code;
                                } else {
                                    controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                                }
                            });

                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#cboCacDac option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                    if (cacdac != '') {
                        $("#cboCacDac option:contains(" + cacdac + ")").attr('selected', true);
                    }
                }
            });
        },
        getRulesControls: function () {
            $('#frmNetflixService').validate({
                rules: {
                    nmtxtEmailConstancia: {
                        required: true
                    }
                }, highlight: function (element) {
                    $(element).closest('.error-input').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.error-input').removeClass('has-error');
                },
                messages: {
                    nmtxtEmailConstancia: {
                        required: "* hjsdhkdj.",
                    },
                }

            });
        },
        LoadPag: function () {
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
        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        BlockControl: function () {
            var that = this,
            control = that.getControls();
            control.chkCorreo.prop('disabled', true);
            control.chkLinea.prop('disabled', true);
            control.txtNotas.prop('disabled', true);
            control.cboCacDac.prop('disabled', true);

        },
        Round: function (cantidad, decimales) {

            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },
        pad: function (s) { return (s < 10) ? '0' + s : s; },
        getUrl: window.location.protocol + '//' + window.location.host,
        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',
        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
        }

    };


    $.fn.INTNetflixServiceHFC = function () {

        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {
            var $this = $(this),
                data = $this.data('INTNetflixServiceHFC'),
                options = $.extend({}, $.fn.INTNetflixServiceHFC.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('INTNetflixServiceHFC', data);
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

    $.fn.INTNetflixServiceHFC.defaults = {
    }

    $('#divBody').INTNetflixServiceHFC();
})(jQuery);