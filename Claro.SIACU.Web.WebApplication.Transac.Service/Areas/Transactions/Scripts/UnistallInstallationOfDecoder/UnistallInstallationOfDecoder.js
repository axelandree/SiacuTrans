function CloseValidation(obj, pag, controls) {
    var mensaje;
    if (obj.hidAccion === 'G') {// Correcto
        if (Session.tipoAutorizacion != 1) {
            $("#chkFidelizacion").prop("checked", true);
            $("#lblMontoFidelizacion").text(HFCPOST_Session.strNumeroCeroDecimal);
        }        
    } else { //if (obj.hidAccion == 'F') {
        mensaje = 'La validación del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.';
        alert(mensaje, "Alerta");

        $("#txtUsernameAuth").val("");
        $("#txtPasswordAuth").val("");
        if (Session.tipoAutorizacion == 1) {
            $('#cboFranjaHoraria option[value="-1"]').prop('selected', true);
        }
        return;
    }
};

(function ($, undefined) {

    var loadSubTipoData = [];
    var Smmry = new Summary('transfer');
    var Form = function ($element, options) {
        $.extend(this, $.fn.UnistallInstallationOfDecoder.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            //div
            divErrorAlert: $('#divErrorAlert', $element),

            //Label
            lblIDContrato: $('#lblIDContrato', $element),
            lblIDCustomer: $('#lblIDCustomer', $element),
            lblTipoCliente: $('#lblTipoCliente', $element),
            lblCliente: $('#lblCliente', $element),
            lblContacto: $('#lblContacto', $element),
            lblDNIRUC: $('#lblDNIRUC', $element),
            lblRepresentanteLegal: $('#lblRepresentanteLegal', $element),
            lblPlanActual: $('#lblPlanActual', $element),
            lblFechaActivacion: $('#lblFechaActivacion', $element),
            lblCicloFacturacion: $('#lblCicloFacturacion', $element),
            lblLimiteCredito: $('#lblLimiteCredito', $element),

            lblDireccion: $('#lblDireccion', $element),
            lblReferencia: $('#lblReferencia', $element),
            lblPais: $('#lblPais', $element),
            lblDepartamento: $('#lblDepartamento', $element),
            lblProvincia: $('#lblProvincia', $element),
            lblDistrito: $('#lblDistrito', $element),
            lblCodigoPlano: $('#lblCodigoPlano', $element),

            lblCantidad: $('#lblCantidad', $element),
            lblCargoFijoTotalPlanSIGV: $('#lblCargoFijoTotalPlanSIGV', $element),
            lblCargoFijoTotalPlanCIGV: $('#lblCargoFijoTotalPlanCIGV', $element),
            lblMontoFidelizacion: $('#lblMontoFidelizacion', $element),
            lblCosto: $('#lblCosto', $element),
            lblTitle: $('#lblTitle', $element),
            lblErrorMessage: $('#lblErrorMessage', $element),

            //text
            txtNota: $('#txtNota', $element),
            txtFProgramacion: $("#txtFProgramacion", $element),
            txtEnviarporEmail: $("#txtEnviarporEmail", $element),

            //Botones
            btnConsultarEquipos: $('#btnConsultarEquipos', $element),
            btnGuardar: $('#btnGuardar', $element),
            btnCerrar01: $('#btnCerrar01', $element),
            btnCerrar02: $('#btnCerrar02', $element),
            btnCerrar03: $('#btnCerrar03', $element),
            btnCerrar04: $('#btnCerrar04', $element),
            btnConstancia: $('#btnConstancia', $element),
            btnValidarHorario: $('#btnValidarHorario', $element),
            btnSiguiente01: $('#btnSiguiente01', $element),
            btnSiguiente02: $('#btnSiguiente02', $element),
            btnSiguiente03: $('#btnSiguiente03', $element),
            btnSummary02: $('#btnSummary02', $element),

            //ConboBox
            cboTipoTrabajo: $('#cboTipoTrabajo', $element),
            cboSubTipoTrabajo: $('#cboSubTipoTrabajo', $element),
            ddlCACDAC: $('#ddlCACDAC', $element),
            cboFranjaHoraria: $('#cboFranjaHoraria', $element),

            //Radio-Check
            rbtInstalacion: $('#rbtInstalacion', $element),
            rbtDesinstalacion: $('#rbtDesinstalacion', $element),
            chkEnviarPorEmail: $('#chkEnviarPorEmail', $element),
            chkFidelizacion: $('#chkFidelizacion', $element),

            //Tablas
            tblDetalleProducto: $('#tblDetalleProducto', $element),
            tblEquipos: $('#tblEquipos', $element),

            //Hidden
            hndHistoryETA: $('#hndHistoryETA', $element),
            hndValidateETA: $('#hndValidateETA', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                control = this.getControls();
            //Initializing  controls
            //document.addEventListener('keyup', that.shortCutStep, false);
            control.btnConsultarEquipos.addEvent(that, 'click', that.btnConsultarEquipos_Click);
            control.cboSubTipoTrabajo.addEvent(that, 'change', that.cboSubTipoTrabajo_Click);
            control.cboTipoTrabajo.addEvent(that, 'change', that.cboTipoTrabajo_Click);
            control.chkEnviarPorEmail.addEvent(that, 'click', that.chkEnviarPorEmail_Click);
            control.chkFidelizacion.addEvent(that, 'change', that.chkFidelizacion_Click);
            control.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            control.btnCerrar01.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar02.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar03.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar04.addEvent(that, 'click', that.btnCerrar_Click);
            control.rbtInstalacion.addEvent(that, 'change', that.rbtInstDesinsDeco_Click);
            control.rbtDesinstalacion.addEvent(that, 'change', that.rbtInstDesinsDeco_Click);
            control.btnConstancia.addEvent(that, 'click', that.btnConstancia_Click);
            control.btnSiguiente02.addEvent(that, 'click', that.btnSiguiente02_click);
            control.btnSiguiente03.addEvent(that, 'click', that.btnSummary_click);
            control.btnSummary02.addEvent(that, 'click', that.btnSummary_click);

            that.maximizarWindow();
            that.windowAutoSize();
            that.render();
        },

        render: function () {
            var that = this,
                control = that.getControls();

            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);

            that.loadPage();
            that.InitGetMessage();
        },

        loadSessionData: function () {
            var that = this,
                controls = that.getControls(),
                oCliente = HFCPOST_Session.DatosCliente,
                oDatosLinea = HFCPOST_Session.DatosLinea;

            // Resumen ---
            Smmry.set('Opcion', '');
            Smmry.set('TipoTrabajo', '');
            Smmry.set('SubTipoTrabajo', '');
            Smmry.set('FechaCompromiso', '');
            Smmry.set('Horario', '');
            Smmry.set('ListEqInstBaja', '');
            Smmry.set('CargoFijoSIGV', '0.00');
            Smmry.set('CargoFijoCIGV', '0.00');
            Smmry.set('Fidelizar', 'NO');
            Smmry.set('MontoFidelizar', '0.00');
            Smmry.set('Nota', '');
            Smmry.set('Correo', '');
            Smmry.set('PuntoVenta', '');
            // -----------

            controls.lblTitle.text("INSTALACIÓN DESINSTALACIÓN DE DECODIFICADORES");
            //console.log"Redireccionó a la Transacion");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);

            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
            Session.URL = SessionTransac.UrlParams;

            /**/
            HFCPOST_Session.Url.IDSESSION = "20170814090833288270"; //Session.URL.IDSESSION;

            HFCPOST_Session.UsuarioAcceso.CodigoUsuario = Session.DATACUSTOMER.userId;
            HFCPOST_Session.UsuarioAcceso.Usuario = Session.USERACCESS.login;
            HFCPOST_Session.UsuarioAcceso.Accesos = Session.USERACCESS.optionPermissions;

            HFCPOST_Session.DatosCliente.CONTRATO_ID = Session.DATACUSTOMER.ContractID;
            HFCPOST_Session.DatosCliente.UBIGEO_INST = Session.DATACUSTOMER.InstallUbigeo;
            HFCPOST_Session.DatosCliente.NOMBRE_COMPLETO = Session.DATACUSTOMER.FullName;
            HFCPOST_Session.DatosCliente.EMAIL = Session.DATACUSTOMER.Email;
            HFCPOST_Session.DatosCliente.RAZON_SOCIAL = Session.DATACUSTOMER.BusinessName;
            HFCPOST_Session.DatosCliente.FECHA_ACT = Session.DATACUSTOMER.ActivationDate;
            HFCPOST_Session.DatosCliente.REPRESENTANTE_LEGAL = Session.DATACUSTOMER.LegalAgent;
            HFCPOST_Session.DatosCliente.DNI_RUC = Session.DATACUSTOMER.DNIRUC;
            HFCPOST_Session.DatosCliente.NRO_DOC = Session.DATACUSTOMER.DNIRUC;
            HFCPOST_Session.DatosCliente.CODIGO_PLANO_INST = Session.DATACUSTOMER.PlaneCodeInstallation;
            HFCPOST_Session.DatosCliente.DIRECCION_DESPACHO = Session.DATACUSTOMER.Address;
            HFCPOST_Session.DatosCliente.URBANIZACION_LEGAL = Session.DATACUSTOMER.LegalUrbanization;
            HFCPOST_Session.DatosCliente.DEPARTEMENTO_LEGAL = Session.DATACUSTOMER.LegalDepartament;
            HFCPOST_Session.DatosCliente.DISTRITO_LEGAL = Session.DATACUSTOMER.LegalDistrict;
            HFCPOST_Session.DatosCliente.PAIS_LEGAL = Session.DATACUSTOMER.LegalCountry;
            HFCPOST_Session.DatosCliente.PROVINCIA_LEGAL = Session.DATACUSTOMER.LegalProvince;
            HFCPOST_Session.DatosCliente.CICLO_FACTURACION = Session.DATACUSTOMER.BillingCycle;
            HFCPOST_Session.DatosCliente.NOMBRES = Session.DATACUSTOMER.Name;
            HFCPOST_Session.DatosCliente.APELLIDOS = Session.DATACUSTOMER.LastName;
            HFCPOST_Session.DatosCliente.TIPO_CLIENTE = Session.DATACUSTOMER.DocumentType;
            HFCPOST_Session.DatosCliente.CONTACTO_CLIENTE = Session.DATACUSTOMER.CustomerContact;
            HFCPOST_Session.DatosCliente.REFERENCIA = Session.DATACUSTOMER.Reference;
            HFCPOST_Session.DatosCliente.CUSTOMER_ID = Session.DATACUSTOMER.CustomerID;
            HFCPOST_Session.DatosCliente.LIMITE_CREDITO = Session.DATACUSTOMER.objPostDataAccount.CreditLimit;

            HFCPOST_Session.DatosCliente.DIRECCION_FAC = Session.DATACUSTOMER.InvoiceAddress;
            HFCPOST_Session.DatosCliente.URBANIZACION_FAC = Session.DATACUSTOMER.InvoiceUrbanization;
            HFCPOST_Session.DatosCliente.PAIS_FAC = Session.DATACUSTOMER.InvoiceCountry;
            HFCPOST_Session.DatosCliente.DEPARTEMENTO_FAC = Session.DATACUSTOMER.InvoiceDepartament;
            HFCPOST_Session.DatosCliente.PROVINCIA_FAC = Session.DATACUSTOMER.InvoiceProvince;
            HFCPOST_Session.DatosCliente.DISTRITO_FAC = Session.DATACUSTOMER.InvoiceDistrict;

            HFCPOST_Session.DatosLinea.cableTv = Session.DATASERVICE.CableValue;
            HFCPOST_Session.DatosLinea.StatusLinea = Session.DATASERVICE.StateLine;
            HFCPOST_Session.DatosLinea.Plan = Session.DATASERVICE.Plan;
            HFCPOST_Session.DatosLinea.FecActivacion = Session.DATASERVICE.ActivationDate;

            //console.log"HFCPOST_Session.DatosCliente");
            //console.logHFCPOST_Session.DatosCliente);
            /**/

            //Variables
            HFCPOST_Session.strCodError = HFCPOST_Session.strVariableEmpty;
            HFCPOST_Session.strMsgError = HFCPOST_Session.strVariableEmpty;
            HFCPOST_Session.intNroSec = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.intNroOST = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.intDesasociaDeco = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.resultAsociar = false;
            HFCPOST_Session.resultDesasocia = false;

            //Hidden
            HFCPOST_Session.AgregaAsociar = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.AgregaDesaso = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.Botonasociar = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.AsociaDeco = HFCPOST_Session.intNumeroCero;
            HFCPOST_Session.Cerrar = HFCPOST_Session.strNumeroCero;
            HFCPOST_Session.CoID = oCliente.CONTRATO_ID;
            HFCPOST_Session.CodigoUbi = oCliente.UBIGEO_INST;
            HFCPOST_Session.CantidadListaEquipos = HFCPOST_Session.strNumeroCero;

            HFCPOST_Session.Email = oCliente.EMAIL;
            HFCPOST_Session.IDPlano = oCliente.CODIGO_PLANO_INST;

            HFCPOST_Session.strSubTipoSeleccionado = "-1";
            HFCPOST_Session.codOpcionID = "srtCodOpcionInstalacionDecosAdicionalesHFC";
            HFCPOST_Session.codOpcionUD = "srtCodOpcionDesinstalacionDecosAdicionalesHFC";
            Session.codOpcion = HFCPOST_Session.codOpcionID;
            Session.tipoAutorizacion = 0;
            //Listado De Decos Adicionales o Baja
            controls.lblCantidad.text(HFCPOST_Session.strNumeroCero);
            controls.lblCargoFijoTotalPlanSIGV.text(HFCPOST_Session.strNumeroCeroDecimal);
            controls.lblCargoFijoTotalPlanCIGV.text(HFCPOST_Session.strNumeroCeroDecimal);
            //controls.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionInstalacion);

            //Datos del Cliente
            controls.lblIDContrato.text(oCliente.CONTRATO_ID);
            controls.lblIDCustomer.text(oCliente.CUSTOMER_ID);
            controls.lblTipoCliente.text(oCliente.TIPO_CLIENTE);
            controls.lblCliente.text(oCliente.RAZON_SOCIAL);
            controls.lblContacto.text(oCliente.NOMBRE_COMPLETO);
            controls.lblDNIRUC.text(oCliente.DNI_RUC);
            controls.lblRepresentanteLegal.text(oCliente.REPRESENTANTE_LEGAL);
            controls.lblPlanActual.text(oDatosLinea.Plan);
            controls.lblFechaActivacion.text(oCliente.FECHA_ACT.substring(0, 10));
            controls.lblCicloFacturacion.text(oCliente.CICLO_FACTURACION);
            controls.lblLimiteCredito.text('S/ ' + (that.Round(oCliente.LIMITE_CREDITO, 2)).toFixed(2));

            controls.lblDireccion.text(oCliente.DIRECCION_FAC);
            controls.lblReferencia.text(oCliente.URBANIZACION_FAC);
            controls.lblPais.text(oCliente.PAIS_FAC);
            controls.lblDepartamento.text(oCliente.DEPARTEMENTO_FAC);
            controls.lblProvincia.text(oCliente.PROVINCIA_FAC);
            controls.lblDistrito.text(oCliente.DISTRITO_FAC);
            controls.lblCodigoPlano.text(oCliente.CODIGO_PLANO_INST);

            Session.CodUbigeo = oCliente.UBIGEO_INST;
            Session.cantSubTipos = 0;
            Session.dataSubTipos = null;
            Session.contractID = Session.DATACUSTOMER.ContractID;
        },

        InitGetMessage: function () {
            var that = this,
                control = that.getControls();

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetMessage',
                success: function (response) {
                    if (response.data != null) {
                        var data = response.data;
                        HFCPOST_Session.FechaActualServidor = data.strFechaActualServidor;
                        HFCPOST_Session.MensajeConfirmacion = data.gConstKeyPreguntaDeco;
                        HFCPOST_Session.TipoTrabajo = data.strCodTipoTrabajoCodificador;
                        HFCPOST_Session.ErrorProcesoEquiposAsociado = data.gConstMensajeErrorEquiposAsociado;
                        HFCPOST_Session.MensajeEquiposAsociado = data.gConstMensajeEquiposAsociado;
                        HFCPOST_Session.Mensaje1 = data.gConstKeyIngreseCorreo;
                        HFCPOST_Session.Mensaje2 = data.gConstKeyCorreoIncorrecto;
                        HFCPOST_Session.Mensaje8 = data.gConstMsgSelCacDac;
                        HFCPOST_Session.Mensaje10 = data.gConstMsgNSFranjaHor;
                        HFCPOST_Session.strMsgConsultaCustomerContratoVacio = data.strMsgConsultaCustomerContratoVacio;
                        HFCPOST_Session.strTextoDecoNoTieneCable = data.strTextoDecoNoTieneCable;
                        HFCPOST_Session.strTextoEstadoInactivo = data.strTextoEstadoInactivo;
                        HFCPOST_Session.ErrValidarAge = data.strMensajeErrValAge;
                        HFCPOST_Session.MensajeProblemaLoad = data.strMensajeProblemaLoad;
                        HFCPOST_Session.strDescActivo = data.strDescActivo;
                        HFCPOST_Session.MensajeCantidadLimiteDeEquipos = data.strMensajeCantidadLimiteDeEquipos;
                        HFCPOST_Session.CantidadLimiteDeEquipos = data.gCantidadLimiteDeEquipos;
                        HFCPOST_Session.gConstMensajeNoTieneEquiposAdicionales = data.gConstMensajeNoTieneEquiposAdicionales;
                        HFCPOST_Session.strMensajeValidaPlanComercial = data.strMensajeValidaPlanComercial;
                        HFCPOST_Session.gAccesoInstalacionIDD = data.gAccesoInstalacionIDD;
                        HFCPOST_Session.gAccesoDesinstalacionIDD = data.gAccesoDesinstalacionIDD;
                        HFCPOST_Session.gAccesoFidelizaCostoIDD = data.gAccesoFidelizaCostoIDD;
                        HFCPOST_Session.gAccesoGuardarIDD = data.gAccesoGuardarIDD;
                        HFCPOST_Session.MensajeErrorConsultaIGV = data.strMensajeErrorConsultaIGV;
                        HFCPOST_Session.MsgErrorTrasaccion = data.strMsgErrorTrasaccion;
                        HFCPOST_Session.MsgETAValidation = data.strMessageETAValidation;
                        HFCPOST_Session.gSubTipoTrabajoDecoAdicional = data.gSubTipoTrabajoDecoAdicional;
                        HFCPOST_Session.gSubTipoTrabajoBajaDeco = data.gSubTipoTrabajoBajaDeco;
                        

                        that.loadSessionData();

                        //RONALDRR - INICIO - SERVICIOS ADICIONALES
                        var strPlano = Session.DATACUSTOMER.PlaneCodeInstallation;
                        var strPlanoFTTH = data.strPlanoFTTH;
                        if (strPlano.search(strPlanoFTTH) > 0 && data.strMensajeTransaccionFTTH != '') {
                            alert(data.strMensajeTransaccionFTTH.replace('{0}', "INSTALACIÓN DESINSTALACIÓN DE DECODIFICADORES"), "Alerta", function () {
                                parent.window.close();
                            });
                            return false;
                        }
                        //RONALDRR - FIN

                        if (!that.IniValidateLoadPage())
                            return false;

                        control.cboSubTipoTrabajo.html("");
                        control.cboSubTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));

                        //Accesos
                        that.ValidateAccess();
                        that.IniGetConsultIGV();
                    }
                }
            });
        },

        IniValidateLoadPage: function () {
            //Validacion al consultar por CustomerID
            if (HFCPOST_Session.DatosCliente.CONTRATO_ID == HFCPOST_Session.strVariableEmpty) {
                alert(HFCPOST_Session.strMsgConsultaCustomerContratoVacio, 'Alerta', function () { window.close(); }); return false;
            }

            //Validación que cuente con servicio de Cable --Cable y/o Internet
            if (HFCPOST_Session.DatosLinea.cableTv == HFCPOST_Session.strVariableEmpty) {
                alert(HFCPOST_Session.strTextoDecoNoTieneCable, 'Alerta', function () { window.close(); }); return false;
            } else if (HFCPOST_Session.DatosLinea.cableTv == HFCPOST_Session.strLetraF) {
                alert(HFCPOST_Session.strTextoDecoNoTieneCable, 'Alerta', function () { window.close(); }); return false;
            }

            //Validación  Linea Activa
            if (HFCPOST_Session.DatosLinea.StatusLinea == HFCPOST_Session.strVariableEmpty) {
                alert(HFCPOST_Session.strTextoEstadoInactivo, 'Alerta', function () { window.close(); }); return false;
            } else if (HFCPOST_Session.DatosLinea.StatusLinea != HFCPOST_Session.strDescActivo) {
                alert(HFCPOST_Session.strTextoEstadoInactivo, 'Alerta', function () { window.close(); }); return false;
            }

            return true;
        },

        IniGetLoyaltyAmount: function () {
            var that = this,
                control = that.getControls(),
                param = {};
            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.iTipo = (HFCPOST_Session.InstDesins == "0" ? 1 : 2);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetLoyaltyAmount',
                success: function (response) {
                    if (response.data != null) {
                        if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero) {// Instalacion
                            HFCPOST_Session.MontoFidelizacionInstalacion = that.Round(parseFloat(response.data) * parseFloat(HFCPOST_Session.igv), 2).toFixed(2);
                            control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionInstalacion);
                        }
                        else {
                            HFCPOST_Session.MontoFidelizacionDesinstalacion = that.Round(parseFloat(response.data) * parseFloat(HFCPOST_Session.igv), 2).toFixed(2);
                            control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionDesinstalacion);
                        }
                        control.chkFidelizacion.prop("checked", false);
                    }
                },
                error: function (response) {
                    //console.error(response);
                }
            });
        },

        IniGetCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = HFCPOST_Session.Url.IDSESSION;
            var parameters = {};
            parameters.strIdSession = HFCPOST_Session.Url.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.login;

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
                            controls.ddlCACDAC.append($('<option>', { value: '-1', html: 'Seleccionar' }));
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

            //$.app.ajax({
            //    type: 'POST',
            //    contentType: "application/json; charset=utf-8",
            //    dataType: 'json',
            //    data: JSON.stringify(objCacDacType),
            //    url: '/CommonServices/GetCacDacType',
            //    success: function (response) {
            //        controls.ddlCACDAC.append($('<option>', { value: '', html: 'Seleccionar' }));
            //        if (response.data != null) {
            //            $.each(response.data.CacDacTypes, function (index, value) {
            //                controls.ddlCACDAC.append($('<option>', { value: value.Code, html: value.Description }));
            //            });
            //        }
            //    }
            //});
        },

        IniGetJobType: function () {
            var that = this,
                controls = that.getControls(),
                param = {};

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strInstDesins = HFCPOST_Session.InstDesins;
            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetJobTypes',
                success: function (response) {
                    $('#cboTipoTrabajo option').remove();
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            if (value.Code == response.strTipoTrabajo)
                                controls.cboTipoTrabajo.append($('<option>', { value: value.Code, html: value.Description }));
                        });

                        that.GetCommertialPlan();
                    }
                    if (HFCPOST_Session.InstDesins == "1") {
                        controls.cboSubTipoTrabajo.val(HFCPOST_Session.gSubTipoTrabajoBajaDeco);
                    }
                },
                error: function (response) {
                    //console.error(response);
                }
            });
        },

        IniLoadProductDetail: function () {
            var that = this,
                control = that.getControls();

            var tblDetalleProducto = control.tblDetalleProducto.dataTable({
                "scrollY": 300,
                "scrollX": true,
                "scrollCollapse": false,
                //"bLengthChange": true,
                "searching": true,
                "bProcessing": true,
                //"bAutoWidth": false,
                "bDestroy": true,
                "sPaginationType": "full_numbers",
                "oLanguage": {
                    "sProcessing": "Procesando...",
                    "sSearch": "Buscar: ",
                    "sLengthMenu": "Mostrar _MENU_ Registros.",
                    "sZeroRecords": "No existen datos.",
                    "sInfo": "Mostrando _START_ hasta _END_ de _TOTAL_ registros.",
                    "sInfoEmpty": "No hay registros para mostrar.",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "sEmptyTable": "No existen datos"
                }
            });

            that.GetProductDetail(tblDetalleProducto);
        },

        IniLoadAggregatedEquipment: function () {
            var that = this,
                control = that.getControls();

            HFCPOST_Session.tblEquipos = control.tblEquipos.dataTable({
                "bLengthChange": false,
                "searching": false,
                "bProcessing": true,
                "bAutoWidth": false,
                "bPaginate": false,
                "bInfo": false,
                "bDestroy": true,
                "sPaginationType": "full_numbers",
                "oLanguage": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ Registros.",
                    "sZeroRecords": "No existen datos.",
                    "sInfo": "Mostrando _START_ hasta _END_ de _TOTAL_ registros.",
                    "sInfoEmpty": "No hay registros para mostrar.",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "sEmptyTable": "No existen datos"
                },
                'aoColumnDefs': [
                        { "aTargets": [0], "sClass": "text-center", "sDefaultContent": "", "bSortable": false }, //Selector
                        { "aTargets": [1] }, //Nombre del Equipo
                        { "aTargets": [2] }, //Tipo de Equipo
                        { "aTargets": [3], "bVisible": false }, //Grupo de Servicio
                        { "aTargets": [4], "sClass": "text-right" }, //Cargo Fijo Sin IGV
                        { "aTargets": [5], "sClass": "text-right" }, //Cargo Fijo Con IGV
                        { "aTargets": [6], "bVisible": false },
                        { "aTargets": [7], "bVisible": false },
                        { "aTargets": [8], "bVisible": false }, //SN Code
                        { "aTargets": [9], "bVisible": false }, //SP Code
                        { "aTargets": [10], "bVisible": false },
                        { "aTargets": [11], "bVisible": false }, //TM Code
                        { "aTargets": [12], "bVisible": false }, //Cod Serv
                ]
            });
        },

        // Decos asociados al cliente
        GetProductDetail: function (tabla) {
            var cantidad = 0,
                self = this,
                param = {};

            HFCPOST_Session.CantidadListaEquipos = cantidad;
            tabla.fnClearTable();

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strContratoID = HFCPOST_Session.DatosCliente.CONTRATO_ID;
            param.strCustomerID = HFCPOST_Session.DatosCliente.CUSTOMER_ID;

            $.ajax({
                type: 'Post',
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetProductDetail',
                data: JSON.stringify(param),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                async: true,
                cache: false,
                error: function (response) {
                    self.tabla.fnDraw();
                    alert(self.errCarDat, "Alerta");
                },
                success: function (response) {
                    var registros = response.data;
                    $.each(registros, function (i, r) {
                        // tipoServicio = 0 => Es un equipo ADICIONAL, caso contrario es un equipo INCLUIDO
                        if (r.tipoServicio == HFCPOST_Session.strNumeroCero)
                            tabla.fnAddData([r.codigo_material, r.codigo_sap, r.numero_serie, r.macadress, r.descripcion_material, r.tipo_equipo, r.id_producto, r.modelo, "Decodificador Adicional", /*r.convertertype,*/ r.servicio_principal/*, r.headend, r.ephomeexchange, r.numero*/]);
                        else
                            tabla.fnAddData([r.codigo_material, r.codigo_sap, r.numero_serie, r.macadress, r.descripcion_material, r.tipo_equipo, r.id_producto, r.modelo, "Decodificador Incluido", /*r.convertertype,*/ r.servicio_principal/*, r.headend, r.ephomeexchange, r.numero*/]);

                        cantidad ++;
                    });
                    
                    HFCPOST_Session.ListaEquiposAsociadosAlCliente = registros;

                    tabla.fnDraw();
                    HFCPOST_Session.CantidadListaEquipos = cantidad;
                }
            });
        },

        GetCommertialPlan: function () {
            var that = this,
                controls = that.getControls(),
                param = {};

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strContratoID = HFCPOST_Session.DatosCliente.CONTRATO_ID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/GetCommertialPlan',
                success: function (response) {
                    if (response.data != null) {
                        if (response.data.rResult == true & response.data.rintCodigoError == HFCPOST_Session.intNumeroCero) {
                            HFCPOST_Session.CodigoPlan = response.data.rCodigoPlan; //'1208'; // Temporal data en duro
                            that.ValidAutomatic();
                        } else {
                            alert(HFCPOST_Session.strMensajeValidaPlanComercial, 'Alerta', function () { window.close(); });
                        }
                    }
                },
                error: function (response) {
                    //console.error(response);
                }
            });
        },

        IniGetConsultIGV: function () {
            var that = this,
            controls = that.getControls(),
            param = {};

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (response) {
                    if (response.data != null) {
                        var igv = response.data.igvD;
                        HFCPOST_Session.igv = 1 + igv;

                        that.IniGetJobType();
                        that.IniLoadProductDetail();
                        that.IniLoadAggregatedEquipment();
                        that.IniGetCacDat();

                        that.IniGetLoyaltyAmount();
                    } else {
                        alert(HFCPOST_Session.MensajeErrorConsultaIGV, 'Alerta', function () { window.close(); });
                        controls.btnSiguiente01.prop("disabled", true);
                    }
                },
                error: function (response) {
                    alert(HFCPOST_Session.MensajeErrorConsultaIGV, 'Alerta', function () { window.close(); });
                    controls.btnSiguiente01.prop("disabled", true);
                }
            });
        },

        SaveTransaction: function () {
            var that = this,
                control = that.getControls(),
                model = {};
            model.SotDeBaja = HFCPOST_Session.SotDeBaja;
            model.IdSession = HFCPOST_Session.Url.IDSESSION;
            model.PuntoAtencion = $("#ddlCACDAC option:selected").text();// control.ddlCACDAC.val();
            model.Cantidad = HFCPOST_Session.Cantidad;
            model.CargoFijoTotalPlanSIGV = HFCPOST_Session.CargoFijoTotalPlanSIGV;
            model.CargoFijoTotalPlanCIGV = HFCPOST_Session.CargoFijoTotalPlanCIGV;
            model.CargoFijoTotal = HFCPOST_Session.CargoFijoTotal;
            model.FechaProgramacion = control.txtFProgramacion.val();
            model.TipoTrabajo = HFCPOST_Session.TipoTrabajo;
            model.Nota = control.txtNota.val();
            model.ValidaEta = HFCPOST_Session.ValidaEta;
            model.FranjaHorariaETA = $("#cboFranjaHoraria option:selected").html();
            model.FranjaHorariaFinal = $("#cboFranjaHoraria option:selected").val();
            model.FranjaHora = $("#cboFranjaHoraria option:selected").attr("idHorario");
            model.strSubTypeWork = $("#cboSubTipoTrabajo option:selected").val();
            model.CodigoRequestAct = Session.RequestActId;
            model.ContenidoEquipo = HFCPOST_Session.ContenidoEquipo;

            model.oDatosCliente = {};
            model.oDatosCliente.UBIGEO_INST = HFCPOST_Session.DatosCliente.UBIGEO_INST;
            model.oDatosCliente.CONTRATO_ID = HFCPOST_Session.DatosCliente.CONTRATO_ID;
            model.oDatosCliente.NOMBRE_COMPLETO = HFCPOST_Session.DatosCliente.NOMBRE_COMPLETO;
            model.oDatosCliente.EMAIL = HFCPOST_Session.DatosCliente.EMAIL;
            model.oDatosCliente.RAZON_SOCIAL = HFCPOST_Session.DatosCliente.RAZON_SOCIAL;
            model.oDatosCliente.FECHA_ACT = HFCPOST_Session.DatosCliente.FECHA_ACT;
            model.oDatosCliente.REPRESENTANTE_LEGAL = HFCPOST_Session.DatosCliente.REPRESENTANTE_LEGAL;
            model.oDatosCliente.DNI_RUC = HFCPOST_Session.DatosCliente.DNI_RUC;
            model.oDatosCliente.NRO_DOC = HFCPOST_Session.DatosCliente.NRO_DOC;
            model.oDatosCliente.CODIGO_PLANO_INST = HFCPOST_Session.DatosCliente.CODIGO_PLANO_INST;
            model.oDatosCliente.DIRECCION_DESPACHO = HFCPOST_Session.DatosCliente.DIRECCION_DESPACHO;
            model.oDatosCliente.URBANIZACION_LEGAL = HFCPOST_Session.DatosCliente.URBANIZACION_LEGAL;
            model.oDatosCliente.DEPARTEMENTO_LEGAL = HFCPOST_Session.DatosCliente.DEPARTEMENTO_LEGAL;
            model.oDatosCliente.DISTRITO_LEGAL = HFCPOST_Session.DatosCliente.DISTRITO_LEGAL;
            model.oDatosCliente.PAIS_LEGAL = HFCPOST_Session.DatosCliente.PAIS_LEGAL;
            model.oDatosCliente.PROVINCIA_LEGAL = HFCPOST_Session.DatosCliente.PROVINCIA_LEGAL;
            model.oDatosCliente.CICLO_FACTURACION = HFCPOST_Session.DatosCliente.CICLO_FACTURACION;
            model.oDatosCliente.NOMBRES = HFCPOST_Session.DatosCliente.NOMBRES;
            model.oDatosCliente.APELLIDOS = HFCPOST_Session.DatosCliente.APELLIDOS;
            model.oDatosCliente.TIPO_CLIENTE = HFCPOST_Session.DatosCliente.TIPO_CLIENTE;
            model.oDatosCliente.CONTACTO_CLIENTE = HFCPOST_Session.DatosCliente.CONTACTO_CLIENTE;
            model.oDatosCliente.REFERENCIA = HFCPOST_Session.DatosCliente.REFERENCIA;
            model.oDatosCliente.CUSTOMER_ID = HFCPOST_Session.DatosCliente.CUSTOMER_ID;
            model.oDatosLinea = {};
            model.oDatosLinea.cableTv = HFCPOST_Session.DatosLinea.cableTv;
            model.oDatosLinea.StatusLinea = HFCPOST_Session.DatosLinea.StatusLinea;
            model.oDatosLinea.Plan = HFCPOST_Session.DatosLinea.Plan;
            model.oDatosLinea.FecActivacion = HFCPOST_Session.DatosLinea.FecActivacion;
            model.oUsuarioAcceso = {};
            model.oUsuarioAcceso.USRREGIS = HFCPOST_Session.UsuarioAcceso.Usuario;

            model.FlajCorreo = (control.chkEnviarPorEmail[0].checked == true ? '1' : '0');
            model.Correo = (control.chkEnviarPorEmail[0].checked == true ? control.txtEnviarporEmail.val() : '');
            model.FlajFidelizacion = (control.chkFidelizacion[0].checked == true ? '1' : '0');

            if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero) //0 = Instalacion
                model.MontoFidelizacion = (control.chkFidelizacion[0].checked == true ? '0.00' : HFCPOST_Session.MontoFidelizacionInstalacion);
            else
                model.MontoFidelizacion = (control.chkFidelizacion[0].checked == true ? '0.00' : HFCPOST_Session.MontoFidelizacionDesinstalacion);

            model.FlajInstDesins = HFCPOST_Session.InstDesins; // 0 = Instalacion y 1 = Desinstalacion
            model.IGV = parseFloat((HFCPOST_Session.igv - 1).toFixed(2));

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/HFC/UnistallInstallationOfDecoder/SaveTransaction',
                error: function (response) {
                    control.btnGuardar.prop('disabled', true);
                    control.btnConstancia.prop('disabled', true);

                    alert(HFCPOST_Session.MsgErrorTrasaccion, 'Alerta');
                    control.divErrorAlert.show(); control.lblErrorMessage.text(HFCPOST_Session.MsgErrorTrasaccion);

                    //console.error(response);
                },
                success: function (response) {
                    if (response.data != null) {
                        var message = response.data.message;
                        if (response.data.name == 'Interaccion') {
                            alert(message, "Alerta");
                            control.divErrorAlert.show(); control.lblErrorMessage.text(message);
                            control.btnGuardar.prop('disabled', true);
                        } else if (response.data.name == 'Deco') {
                            alert(message, "Alerta");
                            control.divErrorAlert.show(); control.lblErrorMessage.text(message);
                            control.btnConstancia.prop('disabled', response.data.btnConstancia);
                        } else if (response.data.name == 'SOT') {
                            alert(message, "Alerta");
                            control.divErrorAlert.show(); control.lblErrorMessage.text(message);
                            control.btnGuardar.prop("disabled", true);
                            control.btnConstancia.prop("disabled", response.data.btnConstancia);
                        } else if (response.data.name == 'Exito') {
                            alert(message, "Informativo");
                            control.btnGuardar.prop('disabled', true);
                            control.btnConstancia.prop('disabled', false);
                            HFCPOST_Session.idInteraccion = response.data.idInteraccion;
                            HFCPOST_Session.rutaArchivo = response.data.rutaArchivo;
                            var codSot = response.data.codsot;
                            control.divErrorAlert.show(); control.lblErrorMessage.text(message + " Nro. SOT: " + codSot);
                            //var split = response.data.rutaArchivo.split('\\');
                            //for (var i = 0; i < split.length; i++) {
                            //    if (i === split.length - 1) {
                            //        HFCPOST_Session.rutaArchivo = HFCPOST_Session.rutaArchivo + split[i];
                            //    } else {
                            //        HFCPOST_Session.rutaArchivo = HFCPOST_Session.rutaArchivo + split[i] + '//';
                            //    }
                            //}

                            HFCPOST_Session.nombreArchivo = response.data.nombreArchivo;
                        }
                    }
                }
            });
        },

        btnConsultarEquipos_Click: function () {
            var that = this;
            var myUrl = location.protocol + '//' + location.host + '/Transactions/HFC/UnistallInstallationOfDecoder';

            //InstDesins = 0 Adicionar Equipo
            if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero)
                myUrl = myUrl + "/AdditionalDecoder";
            else
                myUrl = myUrl + "/UninstallDecoder";

            $.window.open({
                modal: true,
                controlBox: true,
                maximizeBox: false,
                minimizeBox: false,
                title: 'Seleccione un Equipo',
                url: myUrl,
                data: {},
                width: 750,
                height: 520,
                buttons: {
                    Aceptar: {
                        click: function () {
                            //InstDesins = 0 Adicionar Equipo
                            if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero)
                                that.AddDecoder(this);
                            else
                                that.DownDecoder(this);
                        }
                    },
                    Cancelar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },

        DeleteDeco: function () {
            var that = this,
                control = that.getControls(),
                id01 = HFCPOST_Session.IdEliminacion,
                tipo = HFCPOST_Session.TipoEliminacion,
                tr = $("#tblEquipos tbody tr"),
                tblEquipos = HFCPOST_Session.tblEquipos;

            if (id01 == HFCPOST_Session.strVariableEmpty || tipo == HFCPOST_Session.strVariableEmpty) {
                alert("Seleccione un servicio para eliminar.", "Alerta");
                return false;
            }

            confirm("Está seguro de quitar el servicio?", 'Confirmar', function () {
                for (var i = 0; i < tr.length; i++) {
                    var oCells = tr[i].cells;
                    var identificador = $(oCells[0]).find(":input[type=hidden]:eq(0)").val().split(',');
                    var id02 = identificador[identificador.length - 1];
                    var monto = oCells[3].innerHTML;

                    if (id01 == id02) {
                        var aTrs = tblEquipos.fnGetNodes();
                        tblEquipos.fnDeleteRow(aTrs[i]);
                        that.SubtractAmounts("Otro", monto);
                    }

                    HFCPOST_Session.IdEliminacion = HFCPOST_Session.strVariableEmpty;
                    HFCPOST_Session.TipoEliminacion = HFCPOST_Session.strVariableEmpty;
                }
            });

            //Ocultando div que contiene el botón "Agregar Equipo en Alquiler"
            //if (HFCPOST_Session.HayServicioCoreCable == HFCPOST_Session.strNumeroUno) {
            //    $("#SeleccionarEquiposEnAlquiler").show();
            //}
            //else {
            //    $("#SeleccionarEquiposEnAlquiler").hide();
            //}
        },
        
        cboTipoTrabajo_Click: function () {
            var that = this,
                controls = that.getControls();

            HFCPOST_Session.TipoTrabCU = controls.cboTipoTrabajo.val();
            HFCPOST_Session.TipoTrabajo = controls.cboTipoTrabajo.val();
            HFCPOST_Session.SubTipOrdCU = HFCPOST_Session.strVariableEmpty;

            if (controls.cboTipoTrabajo.val() != "-1") {
                that.ValidationETA();
            } else {
                controls.cboSubTipoTrabajo.html("");
                controls.cboSubTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                controls.cboFranjaHoraria.html("");
                controls.cboFranjaHoraria.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            }
        },

        cboSubTipoTrabajo_Click: function () {
            var that = this,
                controls = that.getControls();

            HFCPOST_Session.SubTipOrdCU = controls.cboSubTipoTrabajo.val();
            if (controls.cboSubTipoTrabajo.val() == "-1") {
                return false;
            }

            if (HFCPOST_Session.ValidaEta != '0') {
                if (HFCPOST_Session.SubTipOrdCU != "-1" || HFCPOST_Session.SubTipOrdCU != "") {
                    if (controls.txtFProgramacion.val() != "") {
                        InitFranjasHorario();//that.CargarFranjasHorarias();
                    }
                }
            }
        },

        chkEnviarPorEmail_Click: function () {
            var that = this,
                control = that.getControls(),
                chkEnviarPorEmail = control.chkEnviarPorEmail;

            if (chkEnviarPorEmail[0].checked == true) {
                control.txtEnviarporEmail.css("display", "block");
                control.txtEnviarporEmail.val(HFCPOST_Session.Email);
            } else {
                control.txtEnviarporEmail.css("display", "none");
            }
        },

        chkFidelizacion_Click: function () {
            var that = this,
                control = that.getControls(),
                chkFidelizacion = control.chkFidelizacion;
            Session.tipoAutorizacion = 0;
            if (chkFidelizacion[0].checked == true) {
                //if (HFCPOST_Session.UsuarioAcceso.Accesos.indexOf(HFCPOST_Session.gAccesoFidelizaCostoIDD) > -1) {
                //    control.lblMontoFidelizacion.text(HFCPOST_Session.strNumeroCeroDecimal);
                //} else {
                    var param = {
                        strIdSession: HFCPOST_Session.Url.IDSESSION,
                        pag: '1', // Pedir A Tito
                        opcion: 'BUS',
                        co: '',
                        telefono: Session.DATACUSTOMER.PhoneReference // Validar Numero
                    };
                    chkFidelizacion[0].checked = false;
                    ValidateAccess(that, control, 'BUS', '', '1', param, 'Fixed');
                //}
            } else {
                if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero) //0 = Instalacion
                    control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionInstalacion);
                else
                    control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionDesinstalacion);
            }
        },

        btnCerrar_Click: function () {
            window.close();
        },

        btnGuardar_Click: function () {
            var that = this,
                control = that.getControls();

            if (!this.ValidateDataTypeJob())
                return false;

            if (!that.ValidateTechnicalData())
                return false;

            if (parseInt(control.lblCantidad.text()) > 0) {
                that.SetContentTableInteraction();
            }

            confirm(HFCPOST_Session.MensajeConfirmacion, 'Confirmar', function () {
                //if (HFCPOST_Session.CantidadListaEquipos != HFCPOST_Session.strVariableEmpty) {
                //    if (parseInt(HFCPOST_Session.CantidadListaEquipos) > HFCPOST_Session.intNumeroCero)
                //        that.GetDataProductDetail();
                //} else { that.loadPage(); that.SaveTransaction(); }
                if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroUno) { // Desinstalacion
                    that.GetDataProductDetail();
                } else {
                    that.loadPage();
                    that.SaveTransaction();
                }
            });
        },

        btnConstancia_Click: function () {
            //alert('Constancia');

            var PDFRoute = HFCPOST_Session.rutaArchivo;
            var IdSession = HFCPOST_Session.Url.IDSESSION;

            ReadRecordSharedFile(IdSession, PDFRoute);
        },

        //Seleccionar Instalacion o Baja de Equipos
        rbtInstDesinsDeco_Click: function () {
            var that = this,
                cantDecoAdicional = 0,
                control = that.getControls(),
                ListaEquiposAsociadosAlCliente = {};

            ListaEquiposAsociadosAlCliente = HFCPOST_Session.ListaEquiposAsociadosAlCliente;

            //if (control.rbtInstalacion[0].checked)
            //{ HFCPOST_Session.InstDesins = "0"; that.loadPage(); that.IniGetJobType(); that.CleanTable(); }
            //else if (control.rbtDesinstalacion[0].checked)
            //{ HFCPOST_Session.InstDesins = "1"; that.loadPage(); that.IniGetJobType(); that.CleanTable(); }
            $('#cboFranjaHoraria option[value="' + HFCPOST_Session.strSubTipoSeleccionado + '"]').prop('selected', true);
            control.cboFranjaHoraria.html("");
            control.cboFranjaHoraria.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            if (control.rbtInstalacion[0].checked) {
                if (HFCPOST_Session.InstDesins == "1") // Si anteriormente estaba activo el check Desinstalacion
                    that.CleanTable();
                Session.codOpcion = HFCPOST_Session.codOpcionID;
                HFCPOST_Session.InstDesins = "0"; that.loadPage(); that.IniGetJobType(); that.IniGetLoyaltyAmount(); control.lblCosto.text("Costo de Instalación (Con IGV):");
                $('#lblInstalacion').addClass('active'); $('#lblDesinstalacion').removeClass('active');
            } else if (control.rbtDesinstalacion[0].checked) {
                Session.codOpcion = HFCPOST_Session.codOpcionUD;
                $.each(ListaEquiposAsociadosAlCliente, function (i, r) {
                    // tipoServicio = 0 => Es un equipo ADICIONAL, caso contrario es un equipo INCLUIDO
                    if (r.tipoServicio == HFCPOST_Session.strNumeroCero)
                        cantDecoAdicional++;
                });

                if (cantDecoAdicional == 0) {
                    alert(HFCPOST_Session.gConstMensajeNoTieneEquiposAdicionales, "Alerta");
                    control.rbtInstalacion[0].checked = true;
                    HFCPOST_Session.InstDesins = "0";
                    $('#lblInstalacion').addClass('active'); $('#lblDesinstalacion').removeClass('active');
                } else {
                    if (HFCPOST_Session.InstDesins == "0") // Si anteriormente estaba activo el check Instalacion
                        that.CleanTable();

                    HFCPOST_Session.InstDesins = "1"; that.loadPage(); that.IniGetJobType(); that.IniGetLoyaltyAmount(); control.lblCosto.text("Costo de Desinstalación (Con IGV):");
                    $('#lblDesinstalacion').addClass('active'); $('#lblInstalacion').removeClass('active');
                }
            }

            if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero) {//0 = Instalacion
                if (control.chkFidelizacion[0].checked == true)
                    control.lblMontoFidelizacion.text("0.00");
                else
                    control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionInstalacion);
            }
            else {
                if (control.chkFidelizacion[0].checked == true)
                    control.lblMontoFidelizacion.text("0.00");
                else
                    control.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionDesinstalacion);
            }
        },

        AddDecoder: function (modal) {
            var cantEquAsociados = parseInt(HFCPOST_Session.CantidadListaEquipos);
            var cantEquAdicionales01 = parseInt($("#tblEquipos tbody tr td a").length);
            var cantEquAdicionales02 = parseInt(HFCPOST_Session.CodEquipAlSelec.substring(1, HFCPOST_Session.CodEquipAlSelec.length).split('|').length);
            if (cantEquAsociados + cantEquAdicionales01 + cantEquAdicionales02 > parseInt(HFCPOST_Session.CantidadLimiteDeEquipos)) {
                var cantidadDisponible = parseInt(HFCPOST_Session.CantidadLimiteDeEquipos) - (cantEquAsociados + cantEquAdicionales01);
                var mensaje = HFCPOST_Session.MensajeCantidadLimiteDeEquipos.replace("_num_", cantidadDisponible);
                alert(mensaje, "Alerta");
                return false;
            }

            if (HFCPOST_Session.CodEquipAlSelec == HFCPOST_Session.strVariableEmpty) {
                alert("Necesita seleccionar un equipo.", "Alerta");
                return false;
            }

            this.loadPage();
            this.GetAddtionalEquipment();
            modal.close();
        },

        DownDecoder: function (modal) {
            if (HFCPOST_Session.CodEquipAlSelec == HFCPOST_Session.strVariableEmpty) {
                alert("Necesita seleccionar un equipo.", "Alerta");
                return false;
            }

            this.loadPage();
            this.GetUninstallEquipment();
            modal.close();
        },

        GetAddtionalEquipment: function () {
            var that = this,
                control = that.getControls(),
                tabla = control.tblEquipos,
                param = {};

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.idplan = HFCPOST_Session.CodigoPlan;
            param.coid = HFCPOST_Session.CoID;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/UnistallInstallationOfDecoder/GetAddtionalEquipment",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (data) {
                    alert("Error al recuperar datos.", "Alerta");
                },
                success: function (response) {
                    var filas = response.data,
                        index = 0;
                    HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec.substring(1, HFCPOST_Session.CodEquipAlSelec.length);
                    var CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec.split('|');

                    $.each(filas, function (key, value) {
                        var grupo = filas[index].CodGrupoServ;
                        var GrupoServ = filas[index].GrupoServ;
                        var tipoServicio = filas[index].CodTipoServ;
                        var id = filas[index].CodServSisact;
                        var descripcion = filas[index].DesServSisact;
                        var solucion = filas[index].Solucion;
                        var equipo = filas[index].Equipo;
                        var costo = filas[index].CF;
                        var cantidad = filas[index].CantidadEquipo;
                        var identificador = descripcion + id + equipo;
                        var sncod = filas[index].SNCode;
                        var spcod = filas[index].SPCode;
                        var tipoequi = filas[index].TipoEquipo;
                        var tmcode = filas[index].TmCode;

                        var iden = descripcion + id + equipo;

                        for (var i = 0; i < CodEquipAlSelec.length; i++) {
                            if (iden == CodEquipAlSelec[i]) {
                                var radio = "<a id='servEquipoAdiTabla" + "_" + id + "' style='cursor:pointer;color:#d9534f;'><span class='glyphicon glyphicon-remove'></span></a>" +
                                            "<input type='hidden' value='" + sncod + "," + spcod + "," + id + "," + GrupoServ + "," + identificador + "' />";;
                                tabla.fnAddData(
                                    [
                                        radio, //Selector
                                        descripcion, //Nombre del Equipo
                                        filas[index].TipoServ, //Tipo de Equipo
                                        filas[index].GrupoServ, //Grupo de Servicio
                                        that.Round(parseFloat(filas[index].CF), 2).toFixed(2), //Cargo Fijo Sin IGV
                                        that.Round((parseFloat(filas[index].CF) * parseFloat(HFCPOST_Session.igv)), 2).toFixed(2), //Cargo Fijo Con IGV
                                        "",
                                        filas[index].CantidadEquipo,
                                        sncod, //SN Code
                                        spcod, //SP Code
                                        tipoequi,
                                        tmcode, //TM Code
                                        id //Cod Serv
                                    ]);

                                that.SumAmounts("EQUIPO-ALQUILER", filas[index].CF);
                                $("#servEquipoAdiTabla" + "_" + id).click(function () {
                                    that.SetEquipmentRemove(identificador, "EquipoAlquiler", "Equipo", "servEquipoAdiTabla" + "_" + id);
                                });
                                //return false;
                            }
                        }

                        index++;
                    });
                }
            });
        },

        GetUninstallEquipment: function () {
            var that = this,
                control = that.getControls(),
                tabla = control.tblEquipos,
                param = {};

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strContratoID = HFCPOST_Session.DatosCliente.CONTRATO_ID;
            param.strCustomerID = HFCPOST_Session.DatosCliente.CUSTOMER_ID;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/UnistallInstallationOfDecoder/GetProductDown",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (data) {
                    alert("Error al recuperar datos.", "Alerta");
                },
                success: function (response) {
                    var filas = response.data,
                        index = 0;

                    HFCPOST_Session.ListaEquiposBajaServer = filas;

                    HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec.substring(1, HFCPOST_Session.CodEquipAlSelec.length);
                    var CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec.split('|');

                    $.each(filas, function (key, value) {
                        var identificador = value.codigo_material + value.codigo_sap + value.numero_serie;

                        // tipoServicio = 0 => Es un equipo ADICIONAL
                        if (value.tipoServicio == HFCPOST_Session.strNumeroCero) {
                            for (var i = 0; i < CodEquipAlSelec.length; i++) {
                                if (identificador == CodEquipAlSelec[i]) {
                                    var radio = "<a id='servEquipoBajaTabla" + "_" + identificador + "' style='cursor:pointer;color:#d9534f;'><span class='glyphicon glyphicon-remove'></span></a>" +
                                        "<input type='hidden' value='" + value.id_producto + "," + value.modelo + "," + identificador + "' />";

                                    tabla.fnAddData(
                                        [
                                            radio, //Selector
                                            value.TIPODECO, //Nombre del Equipo
                                            value.DesTipoServ, //Tipo de Equipo
                                            "", //Grupo de Servicio
                                            that.Round(parseFloat(value.CARGO_FIJO), 2).toFixed(2), //Cargo Fijo Sin IGV
                                            that.Round(parseFloat(value.CARGO_FIJO_IGV), 2).toFixed(2), //Cargo Fijo Con IGV
                                            "", //Equipo
                                            "",
                                            "", //SN Code
                                            "", //SP Code
                                            "",
                                            "", //TM Code
                                            ""  //Cod Serv
                                        ]);

                                    that.SumAmounts("EQUIPO-BAJA", value.CARGO_FIJO);
                                    $("#servEquipoBajaTabla" + "_" + identificador).click(function () {
                                        that.SetEquipmentRemove(identificador, "EquipoBaja", "Equipo", "servEquipoBajaTabla" + "_" + identificador);
                                    });
                                }
                            }
                        }
                    });
                }
            });
        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        CargarSubTipoTrabajo: function () {

            var that = this,
                controls = that.getControls();

            var pstrWorkType = controls.cboTipoTrabajo.val();
            var strIdSession = HFCPOST_Session.Url.IDSESSION;
            var strContractID = HFCPOST_Session.DatosCliente.CONTRATO_ID;

            var urlBase = '/Transactions/HFC/UnistallInstallationOfDecoder';
            $.app.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{strIdSession:'" + strIdSession + "',strTipoTrabajo:'" + pstrWorkType + "',strContractID:'" + strContractID + "'}",
                url: urlBase + '/GetJobSubType',
                success: function (response) {
                    that.createDropdownWorkSubType(response);
                }
            });
        },
        createDropdownWorkSubType: function (response) {

            var that = this,
                controls = that.getControls();
            var intIndex = 0;
            var orderSubtipo = [];
            controls.cboSubTipoTrabajo.empty();
            controls.cboSubTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            if (response.data != null) {
                Session.dataSubTipos = response.data;
                $.each(response.data, function (index, value) {
                    intIndex++;
                    var codTipoSubTrabajo = value.Code.split("|");
                    var cantidadDecos = parseInt(codTipoSubTrabajo[3]);
                    orderSubtipo.push(cantidadDecos);
                    if (response.typeValidate.COD_SP == "0" && codTipoSubTrabajo[0] == response.typeValidate.COD_SUBTIPO_ORDEN) {
                        controls.cboSubTipoTrabajo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2, selected: true }));
                        controls.cboSubTipoTrabajo.attr('disabled', true);
                        HFCPOST_Session.strSubTipoSeleccionado = response.typeValidate.COD_SUBTIPO_ORDEN;
                    }
                    else {
                        controls.cboSubTipoTrabajo.append($('<option>', { value: value.Code, html: value.Description, typeservice: value.Code2 }));
                    }


                });

                Session.cantSubTipos = intIndex;
                if (intIndex == 0 && HFCPOST_Session.ValidaEta != "0") {
                    alert("No se encontraron subtipos de trabajo disponibles.", "Alerta");
                }

                if (intIndex > 1) {
                    controls.cboSubTipoTrabajo.find('option:eq(1)').prop('selected', 'selected');
                    orderSubtipo.sort();
                    orderSubtipo.reverse();
                    var ini = 0;
                    var valAnt = 0;
                    $.each(orderSubtipo, function (indexP, valueP) {
                        ini++;
                        $.each(Session.dataSubTipos, function (index, value) {
                            var codTipoSubTrabajo = value.Code.split("|");
                            var cantidadDecos = parseInt(codTipoSubTrabajo[3]);
                            if (cantidadDecos == valueP) {
                                if (ini == 0) {
                                    loadSubTipoData.push({
                                        valorInicio: 0,
                                        valorFinal: cantidadDecos,
                                        valorSelect: value.Code
                                    });
                                } else {
                                    loadSubTipoData.push({
                                        valorInicio: valAnt,
                                        valorFinal: cantidadDecos,
                                        valorSelect: value.Code
                                    });
                                }
                                valAnt = cantidadDecos;
                            }
                        });
                    });
                }

                //if (HFCPOST_Session.InstDesins == "0") {
                //    controls.cboSubTipoTrabajo.val(HFCPOST_Session.gSubTipoTrabajoDecoAdicional);
                //}

            }

        },

        ValidAutomatic: function () {
            var that = this,
                controls = that.getControls();

            HFCPOST_Session.TipoTrabCU = controls.cboTipoTrabajo.val();
            HFCPOST_Session.TipoTrabajo = controls.cboTipoTrabajo.val();
            HFCPOST_Session.SubTipOrdCU = HFCPOST_Session.strVariableEmpty;

            if (controls.cboTipoTrabajo.val() != HFCPOST_Session.strNumeroMenosUno) {
                that.ValidationETA();
            } else {
                controls.cboSubTipoTrabajo.html("");
                controls.cboSubTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                controls.cboFranjaHoraria.html("");
                controls.cboFranjaHoraria.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            }
        },

        ValidationETA: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.IdSession = HFCPOST_Session.Url.IDSESSION;
            model.strJobTypes = controls.cboTipoTrabajo.val();
            model.strCodePlanInst = HFCPOST_Session.IDPlano;

            $.ajax({
                type: "POST",
                url: "/Transactions/SchedulingToa/GetValidateETA",
                //url: "/Transactions/HFC/UnistallInstallationOfDecoder/ValidationETA",
                data: JSON.stringify(model),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (response) {
                    //alert("ERROR JS : en llamar a ValidacionETA");
                    //console.logresponse);
                },
                success: function (response) {
                    var oItem = response.data;
                    var fechaServidor = new Date(Session.ServerDate);
                    HFCPOST_Session.ValidaEta = HFCPOST_Session.strNumeroCero;
                    controls.hndValidateETA.val(HFCPOST_Session.strNumeroCero);

                    $('#txtFProgramacion').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
                    if (oItem.Codigo == '1' || oItem.Codigo == '0' || oItem.Codigo == '2') {

                        HFCPOST_Session.ValidaEta = oItem.Codigo;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;
                        HFCPOST_Session.HistorialEta = oItem.Codigo2;
                        controls.hndValidateETA.val(oItem.Codigo);
                        controls.hndHistoryETA.val(oItem.Codigo2);

                        if (oItem.Codigo == '2') {
                            that.CargarSubTipoTrabajo();
                            that.ActivaDesactivaCamposAgendamiento(HFCPOST_Session.strNumeroUno);
                        }
                        else if (oItem.Codigo == '1') {
                            that.CargarSubTipoTrabajo();
                            that.ActivaDesactivaCamposAgendamiento(HFCPOST_Session.strNumeroUno);
                        }
                        else {
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                            HFCPOST_Session.ValidaEta = oItem.Codigo;
                            controls.hndValidateETA.val(oItem.Codigo);
                            controls.cboSubTipoTrabajo.prop('disabled', true);
                            InitFranjasHorario();// that.CargarFranjasHorarias();
                            that.ActivaDesactivaCamposAgendamiento(HFCPOST_Session.strNumeroUno);

                        }
                    }
                    else {
                        if (oItem.Descripcion == null)
                            oItem.Descripcion = " ";

                        alert(HFCPOST_Session.MsgETAValidation, "Alerta");
                        Session.ValidateETA = HFCPOST_Session.strNumeroCero;
                        Session.History = HFCPOST_Session.strNumeroCero;
                        HFCPOST_Session.ValidaEta = HFCPOST_Session.strNumeroCero;
                        controls.hndValidateETA.val(HFCPOST_Session.strNumeroCero);
                        controls.cboSubTipoTrabajo.prop('disabled', true);
                        that.ActivaDesactivaCamposAgendamiento(HFCPOST_Session.strNumeroUno);
                        InitFranjasHorario();// that.CargarFranjasHorarias();
                        HFCPOST_Session.HistorialEta = oItem.Code2;
                        controls.hndHistoryETA.val(oItem.Code2);

                    }

                }
            });
        },

        f_pad: function (s) { return (s < 10) ? '0' + s : s; },

        ActivaDesactivaCamposAgendamiento: function (varBool) {
            var that = this,
                controls = that.getControls();

            if (varBool == HFCPOST_Session.strNumeroUno) {
                controls.txtFProgramacion.prop("disabled", false);
                //controls.txtFProgramacion.data("kendoDatePicker").enable(true);
                controls.cboFranjaHoraria.prop("disabled", false);
                controls.btnValidarHorario.prop("disabled", true);
            } else {
                var fechaServidor = new Date(HFCPOST_Session.FechaActualServidor);
                controls.txtFProgramacion.val([that.pad(fechaServidor.getDate()), that.pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));
                //controls.txtFProgramacion.data("kendoDatePicker").enable(false);
                controls.txtFProgramacion.prop("disabled", true);
                controls.cboFranjaHoraria.prop("disabled", true);
                controls.btnValidarHorario.prop("disabled", true);
            }
        },

        //Función para devolver números de 1 sólo digito con el 0 por delante.
        pad: function (s) { return (s < 10) ? '0' + s : s; },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

        SumAmounts: function (tipo, monto) {
            var that = this,
                control = that.getControls(),
                montoSIGV = 0,
                montoCIGV = 0,
                montoVarConIGV = 0,
                cantidadS = 0;

            montoSIGV = parseFloat(control.lblCargoFijoTotalPlanSIGV.text());
            montoSIGV = montoSIGV + parseFloat(monto);
            control.lblCargoFijoTotalPlanSIGV.text(that.Round(montoSIGV, 2).toFixed(2));

            montoVarConIGV = parseFloat(monto) * parseFloat(HFCPOST_Session.igv);
            //montoVarConIGV = montoVarConIGV.toFixed(2);
            montoVarConIGV = parseFloat(montoVarConIGV);
            montoCIGV = parseFloat(control.lblCargoFijoTotalPlanCIGV.text());
            montoCIGV = montoCIGV + montoVarConIGV;
            control.lblCargoFijoTotalPlanCIGV.text(that.Round(montoCIGV, 2).toFixed(2));

            cantidadS = parseInt(control.lblCantidad.text());
            cantidadS = cantidadS + 1;
            control.lblCantidad.text(cantidadS);

            if (Session.ValidateETA != 0) {
                if (Session.cantSubTipos > 1) {
                    $.each(loadSubTipoData, function (index, value) {
                        var cantInicial = parseInt(value.valorInicio);
                        var cantFinal = parseInt(value.valorFinal);
                        if (cantInicial < cantidadS && cantFinal >= cantidadS) {
                            control.cboSubTipoTrabajo.val(value.valorSelect);
                            control.txtFProgramacion.val("");
                            control.cboFranjaHoraria.empty();
                        }
                    });
                }
            }

            HFCPOST_Session.Cantidad = control.lblCantidad.text();
            HFCPOST_Session.CargoFijoTotalPlanCIGV = control.lblCargoFijoTotalPlanCIGV.text();
            HFCPOST_Session.CargoFijoTotalPlanSIGV = control.lblCargoFijoTotalPlanSIGV.text();
        },

        SubtractAmounts: function (tipo, monto) {
            var that = this,
                control = that.getControls(),
                montoSIGV = 0,
                montoCIGV = 0,
                montoVarConIGV = 0,
                cantidadS = 0;

            montoSIGV = parseFloat(control.lblCargoFijoTotalPlanSIGV.text());
            montoSIGV = montoSIGV - parseFloat(monto);
            control.lblCargoFijoTotalPlanSIGV.text(that.Round(montoSIGV, 2).toFixed(2));

            montoVarConIGV = parseFloat(monto) * parseFloat(HFCPOST_Session.igv);
            //montoVarConIGV = montoVarConIGV.toFixed(2);
            montoVarConIGV = parseFloat(montoVarConIGV);
            montoCIGV = parseFloat(control.lblCargoFijoTotalPlanCIGV.text());
            montoCIGV = montoCIGV - montoVarConIGV;
            control.lblCargoFijoTotalPlanCIGV.text(that.Round(montoCIGV, 2).toFixed(2));

            cantidadS = parseInt(control.lblCantidad.text());
            cantidadS = cantidadS - 1;
            control.lblCantidad.text(cantidadS);

            if (Session.ValidateETA != 0) {
                if (Session.cantSubTipos > 1) {
                    $.each(loadSubTipoData, function (index, value) {
                        var cantInicial = parseInt(value.valorInicio);
                        var cantFinal = parseInt(value.valorFinal);
                        if (cantInicial < cantidadS && cantFinal >= cantidadS) {
                            control.cboSubTipoTrabajo.val(value.valorSelect);
                            control.txtFProgramacion.val("");
                            control.cboFranjaHoraria.empty();
                        }
                    });
                }
            }

            HFCPOST_Session.Cantidad = control.lblCantidad.text();
            HFCPOST_Session.CargoFijoTotalPlanCIGV = control.lblCargoFijoTotalPlanCIGV.text();
            HFCPOST_Session.CargoFijoTotalPlanSIGV = control.lblCargoFijoTotalPlanSIGV.text();
        },

        SetEquipmentRemove: function (id, tipo, servicio, id2) {
            var that = this;

            HFCPOST_Session.IdEliminacion = id;
            HFCPOST_Session.IdComponenteEliminacion = id2;
            HFCPOST_Session.TipoEliminacion = tipo;
            HFCPOST_Session.TipoServicioEliminar = servicio;

            that.DeleteDeco();
        },

        SetContentTableInteraction: function () {
            var tabla = document.getElementById('tblEquipos'),
                i,
                rowLength = tabla.rows.length;

            HFCPOST_Session.ContenidoEquipo = HFCPOST_Session.strVariableEmpty;
            //HFCPOST_Session.ContenidoMigracion = HFCPOST_Session.strVariableEmpty;// No se usa, revisar
            HFCPOST_Session.ContenidoTablaCons = HFCPOST_Session.strVariableEmpty;
            HFCPOST_Session.CargoFijoTotal = HFCPOST_Session.strNumeroCeroDecimal;

            for (i = 1; i < rowLength; i++) {
                var oCells = tabla.rows.item(i).cells;
                var SNSPCodServicioGrupoSer = $(oCells[0]).find(":input[type=hidden]").val().split(',');
                var count = SNSPCodServicioGrupoSer.length;
                var nombreServ = oCells[1].innerHTML;
                var tipoServ = oCells[2].innerHTML;
                var grupoServ = (count == 5 ? SNSPCodServicioGrupoSer[3] : "");
                var cf = oCells[4].innerHTML;
                var cfTotal = oCells[3].innerHTML;
                var equipo = HFCPOST_Session.strVariableEmpty;
                var cantidad = HFCPOST_Session.strVariableEmpty;
                var snCode = (count == 5 ? SNSPCodServicioGrupoSer[0] : ""); //oCells[5].innerHTML;
                var spCode = (count == 5 ? SNSPCodServicioGrupoSer[1] : ""); //oCells[6].innerHTML;
                var codigoServicio = (count == 5 ? SNSPCodServicioGrupoSer[2] : ""); //oCells[8].innerHTML;

                var contenFilaCons;
                var contenidoFila;

                ///////////////////LISTA DE EQUIPOS EN UN HIDDEN/////////////
                if (i == 1) { //NombreServicio + Tipo Servicio + Grupo de Servicio + CF sin  IGV
                    contenFilaCons = nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf;
                } else {
                    contenFilaCons = "|" + nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf;
                }
                HFCPOST_Session.ContenidoTablaCons += contenFilaCons;

                /////////PARA LOS CAMPOS FALTANTES EN EL REGISTRO DE LAS DECOS////////
                if (i == 1) {
                    contenidoFila = codigoServicio + "|" + snCode + "|" + cf + "|" + spCode + "|" + nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf + "|" + equipo + "|" + cantidad + "|" + codigoServicio + "|" + cfTotal;
                } else {
                    contenidoFila = ";" + codigoServicio + "|" + snCode + "|" + cf + "|" + spCode + "|" + nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf + "|" + equipo + "|" + cantidad + "|" + codigoServicio + "|" + cfTotal;
                }
                HFCPOST_Session.ContenidoEquipo += contenidoFila;

                //////////PARA QUE EQUIPOS SE MUESTREN EN CONSTANCIA ///////////////// Esta session no se usa, revisar
                //if (i == 1) {
                //    contenidoFilaMIgra = nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf + "|" + equipo + "|" + cantidad + "|" + codigoServicio;
                //} else {
                //    contenidoFilaMIgra = "|" + nombreServ + "|" + tipoServ + "|" + grupoServ + "|" + cf + "|" + equipo + "|" + cantidad + "|" + codigoServicio;
                //}
                //var conteMigraCons = $("#hdnContenidoMigracion").val();
                //$("#hdnContenidoMigracion").val(conteMigraCons + contenFilaCons);
                //////////////////////////////////////////////////////////////////
                HFCPOST_Session.CargoFijoTotal = this.Round(parseFloat(HFCPOST_Session.CargoFijoTotal) + parseFloat(cf), 2);
            }
        },

        // Obtener los equipos seleccionados a dar de BAJA
        GetDataProductDetail: function () {
            var that = this,
                control = that.getControls();
            if (HFCPOST_Session.InstDesins == HFCPOST_Session.strNumeroCero) { //0 = Instalacion
                this.SaveTransaction(); return;
            }
            var tabla = document.getElementById("tblEquipos");
            var identificador = HFCPOST_Session.strVariableEmpty,
                productoAndModelo = HFCPOST_Session.strVariableEmpty,
                cadena = HFCPOST_Session.strVariableEmpty;

            //if (parseInt(HFCPOST_Session.Cantidad) > $("#tblEquipos tbody tr td input[type=radio]").length) {
            //    alert(HFCPOST_Session.MensajeEquiposAsociado); return false;
            //}

            var idConsulta = "";
            if (Session.ValidateETA != 0) {
                if (control.txtFProgramacion.val() == HFCPOST_Session.strVariableEmpty) {
                    idConsulta = "";
                }
                else if (control.cboFranjaHoraria.val() == HFCPOST_Session.strNumeroMenosUno) {
                    idConsulta = "";
                } else{
                    idConsulta = "$" + Session.RequestActId;
                }

            }

            $("#tblEquipos tbody tr").each(function (index) {
                identificador = $(this).find(":input[type=hidden]:eq(0)").val().split(',');
                productoAndModelo = identificador[0] + idConsulta + "," + identificador[1];
                cadena = cadena + productoAndModelo + '|';
            });

            if (cadena != HFCPOST_Session.strVariableEmpty)
                this.ProcessingServices(cadena);
        },

        // Verifica que los equipos seleccionados a dar de baja no esten cancelados o que cuenten una SOT de desactivación en ejecución,
        // luego registra los equipos en la tabla SOLOTPTO
        ProcessingServices: function (cadena) {
            var that = this,
                control = that.getControls(),
                param = {},
                model = {};

            that.loadPage();

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.strContratoID = HFCPOST_Session.DatosCliente.CONTRATO_ID;
            param.strCustomerID = HFCPOST_Session.DatosCliente.CUSTOMER_ID;
            param.strCadena = cadena;
            param.strInteraccionId = Session.RequestActId;
            model.ValidaEta = HFCPOST_Session.ValidaEta;
            model.FechaProgramacion = control.txtFProgramacion.val();
            model.FranjaHora = $("#cboFranjaHoraria option:selected").attr("idHorario");
            model.FranjaHorariaFinal = $("#cboFranjaHoraria option:selected").val();
            model.CodigoRequestAct = Session.RequestActId;
            param.model = model;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/UnistallInstallationOfDecoder/GetProcessingServices",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                    control.btnGuardar.prop('disabled', true);
                    control.btnConstancia.prop('disabled', true);
                    alert("Ocurrió un problema al realizar la petición, intente nuevamente.", "Alerta");
                },
                success: function (respose) {
                    var datos = respose.data;
                    if (datos != null) {
                        if (datos.rResult == true && datos.rResultado == HFCPOST_Session.strNumeroCero) {
                            HFCPOST_Session.SotDeBaja = datos.rMensaje;
                            that.SaveTransaction();
                        }
                        else {
                            control.btnGuardar.prop('disabled', true);
                            control.btnConstancia.prop('disabled', true);
                            alert(datos.rMensaje, "Alerta"); return false;
                        }
                    } else {
                        control.btnGuardar.prop('disabled', true);
                        control.btnConstancia.prop('disabled', true);
                        alert(HFCPOST_Session.ErrorProcesoEquiposAsociado, "Alerta"); return false;
                    }

                    //that.SaveTransaction();
                    //alert('Datos respuesta: ' + datos);
                }
            });
        },

        Round: function (cantidad, decimales) {
            //donde: decimales > 2
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

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
        },

        shortCutStep: function (e) {
            if (e.ctrlkey && e.keyCode == 39) {
                var $nextBtn = $('.next-step');
                navigateTabs($nextBtn);
            }

            if (e.ctrlkey && e.keyCode == 37) {
                var $prevtBtn = $('.prev-step');
                navigateTabs($prevtBtn);
            }
        },

        ValidateEmail: function () {
            var that = this,
                control = that.getControls();

            if (control.chkEnviarPorEmail[0].checked == true) {
                if ($.trim(control.txtEnviarporEmail.val()) == HFCPOST_Session.strVariableEmpty) {
                    alert(HFCPOST_Session.Mensaje1, 'Alerta', function () {
                        control.txtEnviarporEmail.focus();
                    }); return false;
                } else {
                    var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                    var blvalidar = regx.test(control.txtEnviarporEmail.val());

                    if (blvalidar == false) {
                        alert(HFCPOST_Session.Mensaje2, 'Alerta', function () {
                            control.txtEnviarporEmail.select();
                        }); return false;
                    }
                }
            }

            return true;
        },

        // Limpia la tabla que lista los equipos ADICIONALES Y DE BAJA segun sea la opción
        CleanTable: function () {
            var that = this,
                controls = that.getControls();

            HFCPOST_Session.tblEquipos.fnClearTable();

            controls.lblCantidad.text(HFCPOST_Session.strNumeroCero);
            controls.lblCargoFijoTotalPlanSIGV.text(HFCPOST_Session.strNumeroCeroDecimal);
            controls.lblCargoFijoTotalPlanCIGV.text(HFCPOST_Session.strNumeroCeroDecimal);

            HFCPOST_Session.Cantidad = HFCPOST_Session.strNumeroCero;
            HFCPOST_Session.CargoFijoTotalPlanCIGV = HFCPOST_Session.strNumeroCeroDecimal;
            HFCPOST_Session.CargoFijoTotalPlanSIGV = HFCPOST_Session.strNumeroCeroDecimal;
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

        ValidateAccess: function () {
            var that = this,
                control = that.getControls();
            //btnGuardar
            //if (HFCPOST_Session.UsuarioAcceso.Accesos.indexOf(HFCPOST_Session.gAccesoGuardarIDD) == -1) {
            //    control.btnGuardar.prop("disabled", true);
            //    control.btnSiguiente02.prop("disabled", true);
            //    control.btnSiguiente03.prop("disabled", true);
            //}

            //rbtDesinstalacion
            if (HFCPOST_Session.UsuarioAcceso.Accesos.indexOf(HFCPOST_Session.gAccesoDesinstalacionIDD) == -1)
            { control.rbtDesinstalacion.prop("disabled", true); }
            else
            { control.rbtDesinstalacion.prop("checked", true); HFCPOST_Session.InstDesins = HFCPOST_Session.strNumeroUno; }

            //rbtInstalacion
            if (HFCPOST_Session.UsuarioAcceso.Accesos.indexOf(HFCPOST_Session.gAccesoInstalacionIDD) == -1)
            { control.rbtInstalacion.prop("disabled", true); }
            else
            { control.rbtInstalacion.prop("checked", true); HFCPOST_Session.InstDesins = HFCPOST_Session.strNumeroCero; }

            //Si los dos radios estan desabilitados, desabilitar el boton consultar y guardar
            //if (control.rbtInstalacion.prop("disabled") == true && control.rbtDesinstalacion.prop("disabled") == true) {
            //    control.btnConsultarEquipos.prop("disabled", true);
            //    control.btnGuardar.prop("disabled", true);
            //    control.btnSiguiente02.prop("disabled", true);
            //    control.btnSiguiente03.prop("disabled", true);
            //}
        },

        ValidateDataTypeJob: function () {
            var that = this,
                control = that.getControls();

            //if (control.rbtInstalacion.prop("disabled") == true && control.rbtDesinstalacion.prop("disabled") == true) {
            //    alert("Usted no tiene permiso para realizar esta acción.", "Alerta"); return false;
            //}

            if (control.cboTipoTrabajo.val() == HFCPOST_Session.strNumeroMenosUno || $("#cboTipoTrabajo option").length == HFCPOST_Session.intNumeroCero) {
                alert("Seleccione el Tipo de Trabajo.", 'Alerta', function () {
                    control.cboTipoTrabajo.focus();
                }); return false;
            }

            if (HFCPOST_Session.ValidaEta == '2') {
                if (control.txtFProgramacion.val() == HFCPOST_Session.strVariableEmpty) {
                    $.each(Session.vMessageValidationList, function (index, value) {
                        if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                           alert(value.CODIGOC, 'Alerta', function () {
                                control.txtFProgramacion.focus();
                            }); 
                        }
                    });
                    return false;
                }
                if (control.cboFranjaHoraria.val() == HFCPOST_Session.strNumeroMenosUno) {
                    $.each(Session.vMessageValidationList, function (index, value) {
                        if (value.ABREVIATURA_DET == "MSJ_OBLIG_ETA") {
                            alert(value.CODIGOC, 'Alerta', function () {
                                control.cboFranjaHoraria.focus();
                            }); 
                        }
                    });
                    return false;
                }
            }

            if (control.lblCantidad.text() == HFCPOST_Session.strNumeroCero) {
                alert("Necesita seleccionar por lo menos un equipo.", "Alerta"); return false;
            }

            return true;
        },

        ValidateTechnicalData: function () {
            var that = this,
                control = that.getControls();

            //if (control.rbtInstalacion.prop("disabled") == true && control.rbtDesinstalacion.prop("disabled") == true) {
            //    alert("Usted no tiene permiso para realizar esta acción.", "Alerta"); return false;
            //}

            if (!that.ValidateEmail())
                return false;

            if (control.ddlCACDAC.val() == HFCPOST_Session.strNumeroMenosUno) {
                alert(HFCPOST_Session.Mensaje8, "Alerta", function () {
                    control.ddlCACDAC.focus();
                }); return false;
            }

            return true;
        },

        btnSiguiente02_click: function (e) {
            if (!this.ValidateDataTypeJob())
                return false;

            navigateTabs(e);
        },

        btnSummary_click: function (e) {
            var that = this;

            //PARA CAMBIAR DESCRIPCION DEL LABEL TENIENDO EN CUENTA SI ES DESINTALACION O INSTALACION
            if ($("#rbtDesinstalacion").is(":checked")) {
                $('#lblSumaryTipoCosto').html('Costo de Desinstalación (Con IGV)');
            } else {
                $('#lblSumaryTipoCosto').html('Costo de Instalación (Con IGV)');
            }


            that.SummaryOpcion();
            that.SummaryTipoTrabajo();
            that.SummarySubTipoTrabajo();
            that.SummaryFechaCompromiso();
            that.SummaryHorario();
            that.SummaryListEqInstBaja();
            that.SummaryCargoFijoSIGV();
            that.SummaryCargoFijoCIGV();
            that.SummaryFidelizar();
            that.SummaryMontoFidelizar();
            that.SummaryNota();
            that.SummaryCorreo();
            that.SummaryPuntoVenta();

            if ($(e).attr('id') == 'btnSummary02') {
                $('.btn-circle.transaction-button').removeClass('transaction-button').addClass('btn-default');
                $(e).addClass('transaction-button').removeClass('btn-default').blur();
                var percent = $(e).attr('percent');
                document.getElementById('prog').style.width = percent;
            } else {
                if (!that.ValidateTechnicalData())
                    return false;

                navigateTabs(e);
            }
        },
        SummaryOpcion: function () {
            var that = this,
                controls = that.getControls();

            if (controls.rbtInstalacion[0].checked == true) {
                Smmry.set('Opcion', 'Instalación');
            }
            else {
                Smmry.set('Opcion', 'Desinstalación');
            }
        },
        SummaryTipoTrabajo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboTipoTrabajo.val() != "-1") {
                Smmry.set('TipoTrabajo', $('#cboTipoTrabajo option:selected').html());
            }
            else {
                Smmry.set('TipoTrabajo', '');
            }
        },
        SummarySubTipoTrabajo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboSubTipoTrabajo.val() != "-1") {
                Smmry.set('SubTipoTrabajo', $('#cboSubTipoTrabajo option:selected').html());
            }
            else {
                Smmry.set('SubTipoTrabajo', '');
            }
        },
        SummaryFechaCompromiso: function () {
            var that = this,
                controls = that.getControls();

            if ($.trim(controls.txtFProgramacion.val()) != "") {
                Smmry.set('FechaCompromiso', controls.txtFProgramacion.val());
            }
            else {
                Smmry.set('FechaCompromiso', '');
            }
        },
        SummaryHorario: function () {
            var that = this,
                controls = that.getControls();

            if (controls.cboFranjaHoraria.val() != "-1") {
                Smmry.set('Horario', $('#cboFranjaHoraria option:selected').html());
            }
            else {
                Smmry.set('Horario', '');
            }
        },
        SummaryListEqInstBaja: function () {
            var that = this,
                controls = that.getControls(),
                tr = "";

            $("#tblEquipos tbody tr").each(function (index) {
                if ($(this).find("td").length > 1) {
                    tr = tr + '<tr>' +
                                '<td>' + $(this).find("td").eq(1).text() + '</td>' +
                                '<td>' + $(this).find("td").eq(2).text() + '</td>' +
                                '<td class="text-right">' + that.Round(parseFloat($(this).find("td").eq(3).text()), 2).toFixed(2) + '</td>' +
                                '<td class="text-right">' + that.Round(parseFloat($(this).find("td").eq(4).text()), 2).toFixed(2) + '</td>' +
                              '</tr>';
                } else {
                    tr = '<tr><td colspan="5">No existen datos</td></tr>';
                }
            });

            Smmry.set('ListEqInstBaja', tr);
        },
        SummaryCargoFijoSIGV: function () {
            var that = this,
                controls = that.getControls();

            Smmry.set('CargoFijoSIGV', 'S/ ' + controls.lblCargoFijoTotalPlanSIGV.text());
        },
        SummaryCargoFijoCIGV: function () {
            var that = this,
                controls = that.getControls();

            Smmry.set('CargoFijoCIGV', 'S/ ' + controls.lblCargoFijoTotalPlanCIGV.text());
        },
        SummaryFidelizar: function () {
            var that = this,
                controls = that.getControls();

            if (controls.chkFidelizacion[0].checked == true) {
                Smmry.set('Fidelizar', 'SI');
            }
            else {
                Smmry.set('Fidelizar', 'NO');
            }
        },
        SummaryMontoFidelizar: function () {
            var that = this,
                controls = that.getControls();

            Smmry.set('MontoFidelizar', 'S/ ' + controls.lblMontoFidelizacion.text());
        },
        SummaryNota: function () {
            var that = this,
                controls = that.getControls();

            if ($.trim(controls.txtNota.val()) != "") {
                Smmry.set('Nota', controls.txtNota.val());
            }
            else {
                Smmry.set('Nota', '');
            }
        },
        SummaryCorreo: function () {
            var that = this,
                controls = that.getControls();

            if (controls.chkEnviarPorEmail[0].checked == true) {
                Smmry.set('Correo', controls.txtEnviarporEmail.val());
            }
            else {
                Smmry.set('Correo', '');
            }
        },
        SummaryPuntoVenta: function () {
            var that = this,
                controls = that.getControls();

            if (controls.ddlCACDAC.val() != "") {
                Smmry.set('PuntoVenta', $('#ddlCACDAC option:selected').html());
            }
            else {
                Smmry.set('PuntoVenta', '');
            }
        },
    };

    $.fn.UnistallInstallationOfDecoder = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('UnistallInstallationOfDecoder'),
                options = $.extend({}, $.fn.UnistallInstallationOfDecoder.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('UnistallInstallationOfDecoder', data);
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

    $.fn.UnistallInstallationOfDecoder.defaults = {
    }

    $('#divBody').UnistallInstallationOfDecoder();

})(jQuery);