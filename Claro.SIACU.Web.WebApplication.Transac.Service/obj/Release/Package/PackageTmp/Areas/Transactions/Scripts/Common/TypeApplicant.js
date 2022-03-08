var cantidad = 0;
var objInfoBioTrazabilidad = window.opener.Session.Persons;
var mywin = null;
var solicitante = new Object();
var strOpcion;
var popup = null;
var lstsolicitantes = [];
var Cantidad = 0;
var arrDatosValidacion = null;
var Persons = [];

(function ($, undefined) {
    var maximunrecords;
    var Form = function ($element, options) {
        $.extend(this, $.fn.TypeApplicant.defaults, $element.data(), typeof options === 'object' && options);
        this.setControls({
            form: $element
            , btnGrabar: $('#btnGrabar', $element)
            , btnCerrar: $('#btnCerrar', $element)
            , rdoTipoSolicitante03: $('#rdoTipoSolicitante03', $element)
            , rdoTipoSolicitante01: $('#rdoTipoSolicitante01', $element)
            , rdoTipoSolicitante02: $('#rdoTipoSolicitante02', $element)
            , txtNombreTitular: $('#txtNombreTitular', $element)
            , txtApellidosTitular: $('#txtApellidosTitular', $element)
            , ddlTipoDocumentoTitular: $('#ddlTipoDocumentoTitular', $element)
            , txtNumeroDocumentoTitular: $('#txtNumeroDocumentoTitular', $element)
            , hdnTipoDocumentValidation: $('#hdnTipoDocumentValidation', $element)
            , lblrdoTipoSolicitante01: $('#lblrdoTipoSolicitante01', $element)
            , lblrdoTipoSolicitante02: $('#lblrdoTipoSolicitante02', $element)
            , lblrdoTipoSolicitante03: $('#lblrdoTipoSolicitante03', $element)
            , lblMensaje: $('#lblMensaje', $element)
                       
                    });
                }

        Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                controls = this.getControls();

            controls.btnGrabar.addEvent(that, "click", that.btnSave_click);
            controls.rdoTipoSolicitante03.addEvent(that, 'click', that.OpenOptionApplicant);
            controls.btnCerrar.addEvent(that, 'click', that.btnCerrar_click);
            that.loadSessionData();
            that.render();
        },
        loadSessionData: function () {
            var controls = this.getControls();
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        btnCerrar_click: function () {
            window.opener.Session.TypeApplicant.returnvalue = null;
            setInterval(function () { window.close() }, 1000);
        },
        render: function () {
            var that = this,
            control = that.getControls();
            that.loadConfigBio();
            that.ValidateLengthInput();
            that.onChangeCboDocument();
            that.getListDocument();
            that.loadData();            

            $('#loading').hide();
            if (window.opener.Session.InitValidation == '2') {
                popup = 1;
                return false;
            }

            if (control.rdoTipoSolicitante03.is(':checked')) {
                that.OpenOptionApplicant();
            }
        },
        onChangeCboDocument: function () {
            var that = this,
                controls = that.getControls();

            controls.ddlTipoDocumentoTitular.change(function () {
            controls.txtNumeroDocumentoTitular.val("");
            });
        },
        SendData: function () {
            var controls = this.getControls();
            if (arrDatosValidacion.valida == '4') {
                Cantidad = Cantidad - 1;
                window.opener.Session.TypeApplicant.returnvalue = JSON.stringify(arrDatosValidacion);
                return;
            }
            else if (arrDatosValidacion.valida == '5') {
                lstsolicitantes.push(solicitante);
            }
            else {
                Cantidad = Cantidad - 1;
                return;
            }

            if (Cantidad == popup) {
                controls.lblMensaje.html('Se completó el registro correctamente.','Mensaje de Registro de Equipos');
                strOpcion = JSON.stringify(lstsolicitantes);
                window.opener.Session.TypeApplicant.returnvalue = strOpcion;
                //podria retornar algun valor diferente de Q, para indicar que salio bien.
                setInterval(function () { window.close(); }, 1000);
            }
            else {
                controls.txtNombreTitular.val('');
                controls.txtApellidosTitular.val('');
                controls.txtNumeroDocumentoTitular.val('');
                controls.ddlTipoDocumentoTitular.val('');
                controls.txtNombreTitular.focus();
                controls.lblMensaje.html('Ingrese el representante legal N°: ' + (Cantidad + 1));
            }
        },
        winClose: function () {
            var controls = this.getControls();

            strOpcion = 'Q';
            window.opener.Session.TypeApplicant.returnvalue = strOpcion;
            setInterval(function () { window.close(); }, 1000);
            return;
        },
        disabledControls: function () {
            var controls = this.getControls();
            $("input,button,textarea,select").attr("disabled", "disabled");
            controls.btnGrabar.removeAttr('disabled');
            controls.btnCerrar.removeAttr('disabled');
        },
        enabledControls: function () {
            var that = this,
                controls = that.getControls();
            $("input,button,textarea,select").removeAttr("disabled");
            controls.rdoTipoSolicitante01.attr('disabled', 'disabled');
            controls.rdoTipoSolicitante02.attr('disabled', 'disabled');
            controls.rdoTipoSolicitante03.attr('disabled', 'disabled');
        },
        OpenOptionApplicant: function () {
            var that = this;

        var modalDialogtop = (screen.height - 66) / 2;
        var modalDialogLeft = (screen.width - 66) / 2;
            
        popup = 'Q';
        var strurl = window.location.protocol + '//' + window.location.host;
        mywin = window.open(strurl + '/Transactions/Common/CantApplicant', '_win2', 'location=si,menubar=no,titlebar=no,resizable=si,toolbar=no, menubar=no,width=220,height=200, top=280,left=600');
        $(mywin).on('blur', function (e) { mywin.focus(); });

        var timer = setInterval(function () {
            try {
                if (typeof mywin != 'undefined' && mywin != null) {
                    if (mywin.closed) {
                        clearInterval(timer);
                        if (typeof cantidad != 'undefined' && cantidad != null) {
                            if (parseInt(cantidad) > 0) {
                                popup = cantidad;
                            }

                            if (popup == 'Q') {
                                window.opener.Session.TypeApplicant.returnvalue = popup;
                                setInterval(function () { window.close(); }, 1000);
                            }
                            else {
                                if (objInfoBioTrazabilidad.TransaccionOrigen == 'FIRMA_DIGITAL') {
                                    if (popup > 1) {//cantidad de RRLL mayor a 1, se cancela la firma digital.
                                        window.opener.Session.TypeApplicant.returnvalue = popup;
                                        setInterval(function () { window.close(); }, 1000);
                                    }
                                }
                                that.enabledControls();
                            }
                        }
                    }
                }
            } catch (ex) {
            }

        }, 1000);
        },
        loadData: function () {
          
            var controls = this.getControls();
            var TipoDocumento;
            var persona = window.opener.Session.Persons;
            Session.objValoresBiometria = (typeof window.opener.Session.Persons.objValoresBiometria == 'undefined' || window.opener.Session.Persons.objValoresBiometria == null ? null : window.opener.Session.Persons.objValoresBiometria);
            TipoDocumento = persona.TipoDocumento;
            var OpcionSolicitada = persona.OpcionSolicitada;
            objInfoBioTrazabilidad = persona;
            if (typeof Session.objBiometria.objDatosSolicitante == 'undefined') Session.objBiometria.objDatosSolicitante = new Object();
            if (OpcionSolicitada == 1) {
                controls.rdoTipoSolicitante03.prop('checked', true);
                Session.objBiometria.objDatosSolicitante.Tipo = "RRLL";
            } else {
                controls.rdoTipoSolicitante01.prop('checked', true);
                Session.objBiometria.objDatosSolicitante.Tipo = "Carta Poder";
            }
            controls.txtNombreTitular.val(persona.Nombres);
            controls.txtApellidosTitular.val(persona.Apellidos);

            this.disabledControls();
            controls.ddlTipoDocumentoTitular.val(TipoDocumento);
            controls.txtNumeroDocumentoTitular.val(persona.NumeroDocumento);

            solicitante = new Object();
        },
        btnSave_click: function () {
            debugger;
            var that = this;
            var controls;
            try {
                controls = this.getControls();
            } catch (ex) {
                console.log(ex);
                controls = this.m_controls;
            }

            var strTipDocAutorizado = controls.hdnTipoDocumentValidation.val();
          
            var pregunta = "¿Está seguro que desea registrar el solicitante?";
            var tiposolicitante = "";
            if (controls.rdoTipoSolicitante01.is(':checked')) {
                    tiposolicitante = controls.lblrdoTipoSolicitante01.html();
            }
                else if (controls.rdoTipoSolicitante02.is(':checked')) {
                    tiposolicitante = controls.lblrdoTipoSolicitante02.html();
            }
                else if (controls.rdoTipoSolicitante03.is(':checked')) {
                    tiposolicitante = controls.lblrdoTipoSolicitante03.html();
            }

            solicitante = {
                Nombres: controls.txtNombreTitular.val(),
                Apellidos: controls.txtApellidosTitular.val(),
                TipoDocumento: $('#ddlTipoDocumentoTitular option:selected').text(),
                TipoDoc: controls.ddlTipoDocumentoTitular.val(),
                NroDocumento: controls.txtNumeroDocumentoTitular.val(),
                TipoSolicitante: tiposolicitante
            };
            if (typeof Session.objBiometria == 'undefined' || Session.objBiometria == null) Session.objBiometria = new Object();
       
            Session.objBiometria.solicitante = solicitante;
            Session.objBiometria.objValoresBiometria = (Session.objValoresBiometria == null ? null : Session.objValoresBiometria);

        
            if (typeof Session.objBiometria.objDatosSolicitante == 'undefined' || Session.objBiometria.objDatosSolicitante == null) {
                Session.objBiometria.objDatosSolicitante = new Object();
            }

            Session.objBiometria.objDatosSolicitante.Nombres = solicitante.Nombres;
            Session.objBiometria.objDatosSolicitante.Apellidos = solicitante.Apellidos;
            Session.objBiometria.objDatosSolicitante.Documento = solicitante.NroDocumento;
            Session.objBiometria.objDatosSolicitante.Tipo = (window.opener.Session.Persons.OpcionSolicitada == 1 ? 'RRLL' : 'Carta Poder');

            if ((controls.ddlTipoDocumentoTitular.val() == strTipDocAutorizado && solicitante.NroDocumento.length != 8)) {
                controls.lblMensaje.html('Debe ingresar un DNI válido.');
                controls.txtNumeroDocumentoTitular.focus();
                return;
            }
            if ($.trim(controls.txtNumeroDocumentoTitular.val()).length < 4) {
                controls.lblMensaje.html('Ingrese como mínimo 4 dígitos para el tipo de documento.');
                return false;
            }
            if ((solicitante.Nombres == "") || (solicitante.Apellidos == "") || (solicitante.NroDocumento == "") || (solicitante.TipoDocumento == "") || ($('#ddlTipoDocumentoTitular option:selected').val() =="-1")) {
                controls.lblMensaje.html('Debe completar todos los campos de forma obligatoria.');
                return;
            }

            Cantidad = Cantidad + 1;
            if (popup == "Q" || popup == null || popup == "" || popup == undefined) {
                popup = 1;                    
            }

            var fn_ok = function () {

                Session.objBiometria.TipoDocumento = controls.ddlTipoDocumentoTitular.val();
                Session.objBiometria.NumeroDocumento = controls.txtNumeroDocumentoTitular.val();
                Session.objBiometria.FlagOtraPersona = "4";
                Session.objBiometria.PaginaOrigen = 'DESBLOQUEO_SOLICITANTES';

                var continua = false;
                solicitante.CantidadRRLL = (controls.rdoTipoSolicitante03.is(':checked') ? popup : 0);
                if (typeof Session.objBiometria.ConfiguracionBiometrica == 'undefined' || Session.objBiometria.ConfiguracionBiometrica == null) $.ValidationBiometrica.getConfigBiometria();
                
                /*Solo si el DOC es DNI debe pasar por Val Biometrica */
                if (controls.ddlTipoDocumentoTitular.val() == strTipDocAutorizado) { 
                    if (Session.objBiometria.ConfiguracionBiometrica.soxpnFlagBiometria == '1') {
                        $.ValidationBiometrica.init(function (response) {
                            arrDatosValidacion = JSON.parse(response);

                            var JsonRequest = JSON.parse(arrDatosValidacion.request);
                            objInfoBioTrazabilidad.JsonRequest = JsonRequest;
                            Session.objInfoBioTrazabilidad = objInfoBioTrazabilidad;
                            if (arrDatosValidacion != null) {
                                var ValOk = arrDatosValidacion.valida;

                                solicitante.ValidacionBioOK = (ValOk == '0') ? true : false;
                                if (Session.objBiometria.TransaccionOrigen == 'FIRMA_DIGITAL') {
                                    if (ValOk != '0') {
                                        strOpcion = '-1';
                                        window.opener.Session.TypeApplicant.returnvalue = strOpcion;
                                        setInterval(function () { window.close(); }, 1000);
                                        return;
                                    }
                                    else {
                                    alert('Se validó correctamente.', 'Mensaje de Registro de Vinculación');
                                        lstsolicitantes.push(solicitante);
                                        strOpcion = JSON.stringify(lstsolicitantes);
                                        window.opener.Session.TypeApplicant.returnvalue = strOpcion;
                                        setInterval(function () { window.close(); }, 1000);
                                    }
                                }
                                else {
                                    that.f_MostrarAlertasOContingencia(arrDatosValidacion);
                                    return;
                                }
                            }

                        }, function () {
                            arrDatosValidacion.valida = 3;
                            that.SendData();
                        });
                       } else if (Session.objBiometria.ConfiguracionBiometrica.soxpnFlagIdValidator == '1') {
                            if ((typeof arrDatosValidacion == "undefined") || (arrDatosValidacion == null)) {
                                arrDatosValidacion = new Object();
                            }
                        
                            if (Session.objBiometria.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == "1") {
                                that.fnRedirectNoBiometria();
                                return
                            }
                            else {
                                arrDatosValidacion.valida = '4';                                
                            }
                            that.SendData();
                            
                        }
                        else {
                            if (Session.objBiometria.objBiometriaTrazabilidad.ConfiguracionBiometrica.soxpvMensaje.trim() != '') {
                                alert(Session.objBiometria.ConfiguracionBiometrica.soxpvMensaje);
                                    }
                            if (Session.objBiometria.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                                        arrDatosValidacion.valida = '5';
                                    } else {
                                        arrDatosValidacion.valida = '4';
                                        return;
                                    }
                            that.SendData();
                        }

                } else {
                    if ((arrDatosValidacion == null) || (arrDatosValidacion == undefined)) {
                        arrDatosValidacion = new Object();
                        arrDatosValidacion.valida = '';
                    }
                    arrDatosValidacion.valida = '5';
                    that.SendData();
                }
            };
            var fn_cancel = function () {
                            Cantidad = Cantidad - 1;
                            return;
            };
            var fn_close = function () {
            };
            confirm(pregunta, 'Confirmar', fn_ok, fn_cancel, fn_close);
        },
        f_MostrarAlertasOContingencia: function (resp) {
            var that = this,
                controls = that.getControls();
            var strDatosNoBio = false;

            var ValOk = resp.valida;
            var objInfoBioTrazabilidad = Session.objInfoBioTrazabilidad.JsonRequest;
            
                if (ValOk == '0') {
                    alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica1);
                    solicitante.flagFirmaDigital = "1";
                    solicitante.strHuellaMinucia = resp.huellaTemplate; // huellaDerechaWSQ 
                    solicitante.strHuellaEncode = resp.varHuellaIndiceDerImagen;
                    arrDatosValidacion.valida = '5';
                    that.SendData();
                    return;
                }
                else if (ValOk == '3') {
                    
                    alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica3);
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {

                        that.fnRedirectNoBiometria();
                        return;
                    }
                    else {
                        if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica1);

                        arrDatosValidacion.valida = '5';
                        that.SendData();
                        return;
                    }
                    else {
                        return;
                    }
                }
                else if (ValOk == '4') {
                    alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica0);
                    return;
                }
                else if (ValOk == '-1') { /*ha cancelado en biometria*/
                    return;

                }
                else if (ValOk == '-4') {//discapacidad
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {                      
                        that.fnRedirectNoBiometria();
                        return;
                    }
                    else {
                        if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica1);
                    
                        arrDatosValidacion.valida = '5';
                        that.SendData();
                        return;
                    }
                    else {
                        return;
                    }
                }
                else if (ValOk == '-2') {//Error de WebMethod
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagNoBiometriaReniec == '1') {
                        that.fnRedirectNoBiometria();
                        return;
                    }
                    else {
                        if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje != '') {
                            alert(objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpvMensaje);
                        }
                    }
                    if (objInfoBioTrazabilidad.ConfiguracionBiometrica.soxpnFlagFinVenta == '1') {
                        alert(objInfoBioTrazabilidad.strMensajeValidacionBiometrica1);

                        arrDatosValidacion.valida = '5';
                        that.SendData();
                        return;
                    }
                    else {
                        return;
                    }
                }
                else if (ValOk == '-5') { /*superó el intento de huellas con mala calidad. en biometria*/
                    return;
                }
                else {
                    alert(objInfoBioTrazabilidad.strMensajeValidacionBiometricaOtros);
                    return;
                }
        },
        fnRedirectNoBiometria: function () {
            var that = this,
                controls = that.getControls();

            if (typeof Session.objBiometria == 'undefined') Session.objBiometria = new Object();
            if (typeof Session.objBiometria.objDatosSolicitante == 'undefined') {
                Session.objBiometria.objDatosSolicitante = new Object();
            }
            Session.objBiometria.TransaccionOrigen = (typeof window.opener.Session.Persons.TransaccionOrigen == 'undefined' ? '' : window.opener.Session.Persons.TransaccionOrigen);
            Session.objBiometria.Telefono = (typeof window.opener.Session.Persons.Telefono == 'undefined' ? '' : window.opener.Session.Persons.Telefono);
            Session.objBiometria.MotivoVal = (typeof window.opener.Session.Persons.MotivoVal == 'undefined' ? '' : window.opener.Session.Persons.MotivoVal);

            Session.objBiometria.TipoDocumento = controls.ddlTipoDocumentoTitular.val();
            Session.objBiometria.NumeroDocumento = controls.txtNumeroDocumentoTitular.val();
            Session.objBiometria.FlagOtraPersona = "4";
            Session.objBiometria.PaginaOrigen = 'DESBLOQUEO_SOLICITANTES';

            $.ValidacionNoBiometria.init({ TransaccionOrigen: Session.objBiometria.TransaccionOrigen, MotivoVal: Session.objBiometria.MotivoVal }, function (response) {
       
                var strDatosNoBio = JSON.parse(response);
                if (strDatosNoBio.valida == '1') {
                    arrDatosValidacion.valida = '5';
                    solicitante.flagFirmaDigital = "0";
                    solicitante.rp1 = strDatosNoBio.Questions[0]['ResponseUsuario'];
                    solicitante.rp2 = strDatosNoBio.Questions[1]['ResponseUsuario'];
                    solicitante.rp3 = strDatosNoBio.Questions[2]['ResponseUsuario'];

                }
                else if (strDatosNoBio.valida == '2') {
                    arrDatosValidacion.valida = '4';
                }
                else if (strDatosNoBio.valida == '3') {                    
                    arrDatosValidacion.valida = '4';
                }
                else {
                    arrDatosValidacion.valida = '4';
                }
                that.SendData();
            }, function () {
                arrDatosValidacion.valida = '3';
                that.SendData();
            });
        },
        loadConfigBio: function () {
            $('#loading').show();
            $.ValidationBiometrica.getConfigBiometria();
            $('#loading').hide();
        },
        ValidateLengthInput: function () {
            var that = this,
                controls = that.getControls();
            $("#txtNombreTitular,#txtApellidosTitular").keypress(function (key) {
                if ((key.charCode < 97 || key.charCode > 122)
                    && (key.charCode < 65 || key.charCode > 90) && (key.charCode != 211) && (key.charCode != 218)
                    && (key.charCode != 45) && (key.charCode != 241) && (key.charCode != 209) && (key.charCode != 32)
                     && (key.charCode != 225) && (key.charCode != 233) && (key.charCode != 237) && (key.charCode != 243)
                     && (key.charCode != 250) && (key.charCode != 193) && (key.charCode != 201) && (key.charCode != 205)
                    )
                { return false; }
            });
        },
        getListDocument: function () {
            var strurl = window.location.protocol + '//' + window.location.host;
            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                async: false,
                dataType: 'json',
                url: strurl + '/Transactions/CommonServices/GetListDocument',
                data: JSON.stringify({ sessionId: window.opener.Session.IDSESSION }),
                success: function (result) {
                    console.log(result)
                    $('#ddlTipoDocumentoTitular').append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (result.data != null) {
                        $.each(result.data.ListProgramTask, function (index, value) {
                            //if (value.Codigo != '0') {
                                $('#ddlTipoDocumentoTitular').append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                            //}
                        });
                        }
                    },
            });
        }

        };

    $.fn.TypeApplicant = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('TypeApplicant'),
                options = $.extend({}, $.fn.TypeApplicant.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('TypeApplicant', data);
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

    $.fn.TypeApplicant.defaults = {
    }
    $(document).ready(function () {
        $('#loading').show();
        var timer = setInterval(function () {
            clearInterval(timer);
            $(document).off("ajaxStop");
            $(document).unbind("ajaxStop");
            $('#divRepreLegal').TypeApplicant();
        }, 1500);

    });
})(jQuery);

