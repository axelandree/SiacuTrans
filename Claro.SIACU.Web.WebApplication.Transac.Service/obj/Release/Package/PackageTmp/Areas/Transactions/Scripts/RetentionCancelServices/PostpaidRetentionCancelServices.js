(function ($, undefined) {


    var Smmry = new Summary('transfer');
    var Form = function ($element, options) {
        $.extend(this, $.fn.PostpaidRetentionCancelServices.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,
            //div
            divErrorAlert: $('#divErrorAlert', $element),
            divFidelizacion: $('#divFidelizacion', $element),
            divDescuento: $('#divDescuento', $element),
            divDescuentoETI: $('#divDescuentoETI', $element),
            divFidelizaDTH: $('#divFidelizaDTH', $element),
            divNoRetenido: $('#divNoRetenido', $element),
            divSeccionBonoDTH1: $('#divSeccionBonoDTH1', $element),
            divSeccionBonoDTH2: $('#divSeccionBonoDTH2', $element),
            divSeccionDstoBonoDTH: $('#divSeccionDstoBonoDTH', $element),
            divAccion: $('#divAccion', $element),
            divSupevJefe: $('#divSupevJefe', $element),
            divDescuentoSJ: $('#divDescuentoSJ', $element),
            divDescuentoEtiSJ: $('#divDescuentoEtiSJ', $element),

            //Label
            lblNroTelefono: $('#lblNroTelefono', $element),
            lblTipoCliente: $('#lblTipoCliente', $element),
            lblContacto: $('#lblContacto', $element),
            lblCliente: $('#lblCliente', $element),
            lblPlan: $('#lblPlan', $element),
            lblFecActivacion: $('#lblFecActivacion', $element),
            lblTipAcuerdo: $('#lblTipAcuerdo', $element),
            lblEstadoLinea: $('#lblEstadoLinea', $element),
            lblEstadoAcuerdo: $('#lblEstadoAcuerdo', $element),
            lblFecActivacionLinea: $('#lblFecActivacionLinea', $element),
            lblFecFinAcuerdo: $('#lblFecFinAcuerdo', $element),
            lblDstoBonoDTH: $('#lblDstoBonoDTH', $element),
            lblTotalClienteCuenta: $('#lblTotalClienteCuenta', $element),

            //text
            txtReintegro: $('#txtReintegro', $element),
            txtPenalidadPCS: $("#txtPenalidadPCS", $element),
            txtTotalPenalidad: $("#txtTotalPenalidad", $element),
            txtTotalInversion: $("#txtTotalInversion", $element),
            txtFCancelacion: $("#txtFCancelacion", $element),
            txtNumBoleta: $("#txtNumBoleta", $element),
            txtNota: $("#txtNota", $element),

            //Botones
            btnImportar: $('#btnImportar', $element),
            btnValidarBoleta: $('#btnValidarBoleta', $element),
            btnGuardar: $('#btnGuardar', $element),
            btnCerrar01: $('#btnCerrar01', $element),
            btnCerrar02: $('#btnCerrar02', $element),
            btnCerrar03: $('#btnCerrar03', $element),
            btnCerrar04: $('#btnCerrar04', $element),
            btnConstancia: $('#btnConstancia', $element),
            btnSiguiente02: $('#btnSiguiente02', $element),
            btnSiguiente03: $('#btnSiguiente03', $element),
            btnSummary: $('#btnSummary', $element),
            btnQuitar: $('#btnQuitar', $element),
            btnAgregar: $('#btnAgregar', $element),
            btnBorrarPedido: $('#btnBorrarPedido', $element),

            //ConboBox
            cboMotCancelacion: $('#cboMotCancelacion', $element),
            cboAccion: $('#cboAccion', $element),
            cboArea: $('#cboArea', $element),
            cboMotivoNR: $('#cboMotivoNR', $element),
            cboSubMotivo: $('#cboSubMotivo', $element),
            cboPuntoDeAtencion: $('#cboPuntoDeAtencion', $element),
            cboPeriodosDHT: $('#cboPeriodosDHT', $element),
            cboSupervJefe: $('#cboSupervJefe', $element),

            //Radio-Check
            rdbRetenido: $('#rdbRetenido', $element),
            rdbNoRetenido: $('#rdbNoRetenido', $element),
            rbtReintegroEfectivo: $('#rbtReintegroEfectivo', $element),
            rbtReintegroOCC: $('#rbtReintegroOCC', $element),
            rbtPenalidadEfectivo: $('#rbtPenalidadEfectivo', $element),
            rbtPenalidadDebito: $('#rbtPenalidadDebito', $element),
            rbtAplicaTodoServ: $('#rbtAplicaTodoServ', $element),
            rbtAplicaAlgunosServ: $('#rbtAplicaAlgunosServ', $element),
            chkAccion: $('#chkAccion', $element),
            chkSuspReacXCuenta: $('#chkSuspReacXCuenta', $element),

            //Tabla
            tblServicios: $('#tblServicios', $element),
            tblListaServResumen: $('#tblListaServResumen', $element),
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                control = this.getControls();

            control.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            control.btnCerrar01.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar02.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar03.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnCerrar04.addEvent(that, 'click', that.btnCerrar_Click);
            control.btnConstancia.addEvent(that, 'click', that.btnConstancia_Click);
            //control.btnSiguiente02.addEvent(that, 'click', that.btnSiguiente02_click);
            control.btnSiguiente03.addEvent(that, 'click', that.btnSummary_click);
            control.btnSummary.addEvent(that, 'click', that.btnSummary_click);
            that.maximizarWindow();
            that.render();
        },

        render: function () {
            var that = this,
                control = that.getControls();

            control.divErrorAlert.hide();
            control.btnConstancia.prop('disabled', true);


            //that.getMotCancel();
            //that.getCACDAC();
            //that.getAccion();

            that.IniConfigurationPage();
            //that.loadPage();
            //that.InitGetMessage();
        },

        InitGetMessage: function () {
            var that = this,
                control = that.getControls(),
                param = {};

            param.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/Postpaid/PostpaidRetentionCancelServices/GetMessage',
                success: function (response) {
                    if (response.data != null) {
                        var data = response.data;
                        //HFCPOST_Session.FechaActualServidor = data.strFechaActualServidor;
                        //HFCPOST_Session.MensajeConfirmacion = data.gConstKeyPreguntaDeco;
                        //HFCPOST_Session.TipoTrabajo = data.strCodTipoTrabajoCodificador;
                        //HFCPOST_Session.ErrorProcesoEquiposAsociado = data.gConstMensajeErrorEquiposAsociado;
                        //HFCPOST_Session.MensajeEquiposAsociado = data.gConstMensajeEquiposAsociado;
                        //HFCPOST_Session.Mensaje1 = data.gConstKeyIngreseCorreo;
                        //HFCPOST_Session.Mensaje2 = data.gConstKeyCorreoIncorrecto;
                        //HFCPOST_Session.Mensaje8 = data.gConstMsgSelCacDac;
                        //HFCPOST_Session.Mensaje10 = data.gConstMsgNSFranjaHor;
                        //HFCPOST_Session.strMsgConsultaCustomerContratoVacio = data.strMsgConsultaCustomerContratoVacio;
                        //HFCPOST_Session.strTextoDecoNoTieneCable = data.strTextoDecoNoTieneCable;
                        //HFCPOST_Session.strTextoEstadoInactivo = data.strTextoEstadoInactivo;
                        //HFCPOST_Session.ErrValidarAge = data.strMensajeErrValAge;
                        //HFCPOST_Session.MensajeProblemaLoad = data.strMensajeProblemaLoad;
                        //HFCPOST_Session.strDescActivo = data.strDescActivo;
                        //HFCPOST_Session.MensajeCantidadLimiteDeEquipos = data.strMensajeCantidadLimiteDeEquipos;
                        //HFCPOST_Session.CantidadLimiteDeEquipos = data.gCantidadLimiteDeEquipos;
                        //HFCPOST_Session.gConstMensajeNoTieneEquiposAdicionales = data.gConstMensajeNoTieneEquiposAdicionales;
                        //HFCPOST_Session.strMensajeValidaPlanComercial = data.strMensajeValidaPlanComercial;
                        //HFCPOST_Session.gAccesoInstalacionIDD = data.gAccesoInstalacionIDD;
                        //HFCPOST_Session.gAccesoDesinstalacionIDD = data.gAccesoDesinstalacionIDD;
                        //HFCPOST_Session.gAccesoFidelizaCostoIDD = data.gAccesoFidelizaCostoIDD;
                        //HFCPOST_Session.gAccesoGuardarIDD = data.gAccesoGuardarIDD;

                        //that.loadSessionData();

                        //var fechaServidor = new Date(HFCPOST_Session.FechaActualServidor);
                        //var fechaServidorMas7Dias = new Date(fechaServidor.setDate(fechaServidor.getDate() + 7));
                        //control.txtFProgramacion.val([that.pad(fechaServidorMas7Dias.getDate()), that.pad(fechaServidorMas7Dias.getMonth() + 1), fechaServidorMas7Dias.getFullYear()].join("/"));

                        if (!that.IniValidateLoadPage())
                            return false;





                        //Accesos
                        //that.ValidateAccess();

                        //that.IniGetJobType();

                        //that.IniLoadProductDetail();
                        //that.IniLoadAggregatedEquipment();
                        //that.IniGetCacDat();
                        //that.IniGetConsultIGV();
                    }
                }
            });
        },

        loadSessionData: function () {
            var that = this,
                controls = that.getControls(),
                oCliente = HFCPOST_Session.DatosCliente;

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

            /**/
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
            HFCPOST_Session.DatosCliente.TIPO_CLIENTE = Session.DATACUSTOMER.CustomerType;
            HFCPOST_Session.DatosCliente.CONTACTO_CLIENTE = Session.DATACUSTOMER.CustomerContact;
            HFCPOST_Session.DatosCliente.REFERENCIA = Session.DATACUSTOMER.Reference;
            HFCPOST_Session.DatosCliente.CUSTOMER_ID = Session.DATACUSTOMER.CustomerID;

            HFCPOST_Session.DatosLinea.cableTv = Session.DATASERVICE.CableValue;
            HFCPOST_Session.DatosLinea.StatusLinea = Session.DATASERVICE.StateLine;
            HFCPOST_Session.DatosLinea.Plan = Session.DATASERVICE.Plan;
            HFCPOST_Session.DatosLinea.FecActivacion = Session.DATASERVICE.ActivationDate;
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

            //Listado De Decos Adicionales o Baja
            controls.lblCantidad.text(HFCPOST_Session.strNumeroCero);
            controls.lblCargoFijoTotalPlanSIGV.text(HFCPOST_Session.strNumeroCeroDecimal);
            controls.lblCargoFijoTotalPlanCIGV.text(HFCPOST_Session.strNumeroCeroDecimal);
            //controls.lblMontoFidelizacion.text(HFCPOST_Session.MontoFidelizacionInstalacion);

            //Datos del Cliente
            controls.lblCodCliente.text(oCliente.CONTRATO_ID);
            controls.lblContacto.text(oCliente.NOMBRE_COMPLETO);

            controls.lblCliente.text(oCliente.RAZON_SOCIAL);
            controls.lblRepresentLegal.text(oCliente.REPRESENTANTE_LEGAL);
            controls.lblContactoCliente.text(oCliente.NOMBRE_COMPLETO);
            controls.lblDocRepLegal.text(oCliente.NRO_DOC);
            controls.lblDNIRuc.text(oCliente.DNI_RUC);
            controls.lblFechActivacion.text(oCliente.FECHA_ACT.substring(0, 10));

            controls.lblDireccionInstalacion.text(oCliente.DIRECCION_DESPACHO);
            controls.lblPais.text(oCliente.PAIS_LEGAL);
            controls.lblNotasDirec.text(oCliente.URBANIZACION_LEGAL);
            controls.lblProvincia.text(oCliente.PROVINCIA_LEGAL);
            controls.lblDepartamento.text(oCliente.DEPARTEMENTO_LEGAL);
            controls.lblCodPlano.text(oCliente.CODIGO_PLANO_INST);
            controls.lblDistrito.text(oCliente.DISTRITO_LEGAL);
            controls.lblCodUbigeo.text(oCliente.UBIGEO_INST);
        },

        IniConfigurationPage: function () {
            var that = this,
                control = that.getControls();

            control.divFidelizacion.hide();
            control.divFidelizaDTH.hide();
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

        getMotCancel: function () {
            var that = this,
               controls = that.getControls(),
               objMotiveCancel = {};

            objMotiveCancel.strIdSession = Session.IDSESSION;

            //
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objMotiveCancel),
                url: '/Transactions/Postpaid/RetentionCancelServices/GetMotCancelacion',
                asyn: false,
                success: function (response) {
                    //
                    controls.cboMotCancelacion.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboMotCancelacion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });
        },

        getCACDAC: function () {

            var that = this,
                controls = that.getControls(),

            objCacDacType = {};

            objCacDacType.strIdSession = Session.IDSESSION;
            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = 'C17730'; //SessionTransac.SessionParams.USERACCESS.login;
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

        getAccion: function () {

            var that = this,
           controls = that.getControls(),
           objLstAccionType = {};

            objLstAccionType.strIdSession = Session.IDSESSION;
            objLstAccionType.ServDTH_Movil = '0'; // validar
            
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLstAccionType),
                url: '/Transactions/Postpaid/RetentionCancelServices/GetListarAcciones',
                success: function (response) {
                    
                    controls.cboAccion.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboAccion.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                }
            });

        },

        SaveTransaction: function () {
            var that = this,
                control = that.getControls(),
                model = {};
            
            model.SotDeBaja = HFCPOST_Session.SotDeBaja;
            model.IdSession = Session.IDSESSION;
            model.PuntoAtencion = $("#cboCACDAC option:selected").html();// control.cboCACDAC.val();
            model.Cantidad = HFCPOST_Session.Cantidad;
            model.CargoFijoTotalPlanSIGV = HFCPOST_Session.CargoFijoTotalPlanSIGV;
            model.CargoFijoTotalPlanCIGV = HFCPOST_Session.CargoFijoTotalPlanCIGV;
            model.CargoFijoTotal = HFCPOST_Session.CargoFijoTotal;
            model.FechaProgramacion = control.txtFProgramacion.val();
            model.TipoTrabajo = HFCPOST_Session.TipoTrabajo;
            model.Nota = control.txtNota.val();
            model.ValidaEta = HFCPOST_Session.ValidaEta;
            model.FranjaHorariaETA = HFCPOST_Session.FranjaHorariaETA;
            model.FranjaHorariaFinal = HFCPOST_Session.FranjaHorariaFinal;
            model.CodigoRequestAct = HFCPOST_Session.CodigoRequestAct;
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
            model.IGV = 1 - parseFloat(HFCPOST_Session.igv);

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: '/Transactions/HFC/PostpaidRetentionCancelServices/SaveTransaction',
                error: function (response) {
                    console.error(response);
                },
                success: function (response) {
                    if (response.data != null) {
                        var message = response.data.message;
                        if (response.data.name == 'Interaccion') {
                            that.ErrorInteraction(message);
                        } else if (response.data.name == 'Deco') {
                            alert(message,"Alerta");
                            controls.btnConstancia.prop("disabled", response.data.btnConstancia);
                        } else if (response.data.name == 'SOT') {
                            that.ErrorInteraction(message);
                            controls.btnConstancia.prop("disabled", response.data.btnConstancia);
                        } else if (response.data.name == 'Exito') {
                            alert(message,"Alerta");
                            control.btnGuardar.prop('disabled', true);
                            control.btnConstancia.prop('disabled', false);
                            HFCPOST_Session.idInteraccion = response.data.idInteraccion;
                            HFCPOST_Session.rutaArchivo = response.data.rutaArchivo;
                            HFCPOST_Session.nombreArchivo = response.data.nombreArchivo;
                        }

                        //control.divErrorAlert.show(); control.lblErrorMessage.text(message);
                    }
                }
            });
        },

        btnCerrar_Click: function () {
            window.close();
        },

        btnGuardar_Click: function () {
            var that = this,
                control = that.getControls();
        },

        btnConstancia_Click: function () {
            alert('Constancia',"Alerta");

            //var rutaArchivo = HFCPOST_Session.rutaArchivo;
            //var nombreArchivo = HFCPOST_Session.nombreArchivo;
            //var FlagBill = HFCPOST_Session.strNumeroUno;
            //var IdSession = Session.IDSESSION;

            //ReadRecordPdf(rutaArchivo, nombreArchivo, IdSession, FlagBill);
        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

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

        Show: function (NoneOrBlock01, NoneOrBlock02, ROrNR) {
            var that = this,
                control = that.getControls();

            var reintegro = control.txtReintegro.val();
            var penalidadPCS = control.txtPenalidadPCS.val();

            //f_BorrarPedido();
            //f_LimpiarListaLinea();
            //$('#tbListarServicios').attr("style", "display:none");
            //$('#chkSuspReacXCuenta').attr("checked", false);

            if (control.rdbNoRetenido.prop("checked") == true) {
                control.divFidelizacion.hide();
                control.divFidelizaDTH.hide();

                if (reintegro == SIACPO_Session.strCero) {
                    control.rbtReintegroEfectivo.prop("checked", false);
                    control.rbtReintegroEfectivo.prop("disabled", true);
                    control.rbtReintegroOCC.prop("checked", false);
                    control.rbtReintegroOCC.prop("disabled", true);
                } else {
                    control.rbtReintegroEfectivo.prop("checked", false);
                    control.rbtReintegroEfectivo.prop("disabled", false);
                    control.rbtReintegroOCC.prop("checked", false);
                    control.rbtReintegroOCC.prop("disabled", false);
                }

                if (penalidadPCS == SIACPO_Session.strCero) {
                    control.rbtPenalidadEfectivo.prop("checked", false);
                    control.rbtPenalidadEfectivo.prop("disabled", true);
                    control.rbtPenalidadDebito.prop("checked", false);
                    control.rbtPenalidadDebito.prop("disabled", true);
                } else {
                    control.rbtPenalidadEfectivo.prop("checked", false);
                    control.rbtPenalidadEfectivo.prop("disabled", false);
                    control.rbtPenalidadDebito.prop("checked", false);
                    control.rbtPenalidadDebito.prop("disabled", false);
                }
            }

            control.divDescuento.hide();
            control.divDescuentoETI.hide();
            control.divDescuentoSJ.hide();
            control.divDescuentoEtiSJ.hide();
            SIACPO_Session.hidAccionTra = ROrNR;
            control.divNoRetenido.prop("display", NoneOrBlock02);

            SIACPO_Session.hidFlagValMA = SIACPO_Session.Vacio;
            SIACPO_Session.hidFlagValMASJ = SIACPO_Session.Vacio;
            SIACPO_Session.hifFlagValMonto = SIACPO_Session.Vacio;

            if (ROrNR == SIACPO_Session.Retenido) {
                if (control.cboAccion.val() != SIACPO_Session.strMenosUno)
                    that.f_accion(
                        control.cboAccion.val(),
                        "cboAccion",
                        divDescuento,
                        divDescuentoEti,
                        txtCosto,
                        lblConcepto,
                        "lblOCC",
                        "txtDescuento");

                if (SIACPO_Session.hidSupJef == "S") {
                    if (control.cboAccion.val() != SIACPO_Session.strMenosUno)
                        that.f_accion(
                            control.cboSupervJefe.val(),
                            'cboSupervJefe',
                            divDescuentoSJ,
                            divDescuentoEtiSJ,
                            txtCostoSJ,
                            lblConceptoSJ,
                            'lblOCCSJ',
                            'txtDescuentoSJ');
                }

            }

            if (control.rdbNoRetenido.prop("checked") == true) {
                var conta = 0;
                //$('#idTRListaApadece').attr("style", "display:block");
                //$('#idTRListaReintegroApadece').attr("style", "display:block");
                $('#tblServicios tbody tr').each(function () {
                    conta += 1;
                });
                if (conta >= 3) {
                    control.chkSuspReacXCuenta.prop("disabled", false);
                }
            } else if (control.rdbRetenido.prop("checked") == true) {
                //$('#idTRListaApadece').attr("style", "display:block");
                control.chkSuspReacXCuenta.prop("checked", false);
                control.chkSuspReacXCuenta.prop("disabled", true);
                f_HabilitaListServ();
            }
        },
        // GetValidCommercialServiceActive Falta Implementar
        f_accion: function (val, ddl, div1, div2, txt1, lbl1, lbl2, txt2) {
            var that = this,
                control = that.getControls();

            if (SIACPO_Session.hidAccionTra == SIACPO_Session.Vacio) {
                $("#" + ddl).val(SIACPO_Session.strMenosUno);
                alert('Seleccione la Transacción.',"Alerta");
                return false;
            }

            var arrBonos;
            var idBonosFideliza;
            arrBonos = val.split('|');
            idBonosFideliza = arrBonos[0];
            if (idBonosFideliza == SIACPO_Session.hidIdBonoFidelizacion && control.rdbRetenido.prop("checked") == true) {
                control.divFidelizacion.show();
                SIACPO_Session.hidFlgFideliza = idBonosFideliza;
            } else {
                control.divFidelizacion.hide();
                SIACPO_Session.hidFlgFideliza = SIACPO_Session.Vacio;
            }

            var arrListaCodAccionTem = val.split('|');
            var tempFiltCodAccDth = SIACPO_Session.hidCodBonosDTH;
            var arrFiltraCodAccDth;
            var flgCount = 0;
            if (control.rdbRetenido.prop("checked") == true) {
                if (arrListaCodAccionTem[7] == SIACPO_Session.strDos) {
                    if (arrListaCodAccionTem[5] != SIACPO_Session.Vacio) {
                        if (parseFloat(arrListaCodAccionTem[5]) > 0) {
                            flgCount = 1;
                        }
                    }
                }
            } else {
                flgCount = 0;
            }

            if (flgCount > 0) {
                var coIDServAct = SIACPO_Session.hidCoIDLinea;
                var NroServBono = SIACPO_Session.hidTelReferencia;
                var param = {};

                param.strIdSession = Session.IDSESSION;
                param.CO_ID_Ser = coIDServAct;
                param.CodSerAct = val.split('|')[6];
                param.strNroServicio = NroServBono;
                param.strFlgServDsto = val.split('|')[4];

                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(param),
                    url: "/Transactions/Postpaid/PostpaidRetentionCancelServices/GetValidCommercialServiceActive",
                    success: function (response) {
                        if (response.data != null) {
                            var tmp = result.split("|");
                            if (tmp[0] == SIACPO_Session.strCero && tmp[1] == SIACPO_Session.strUno) {
                                var tmpMnj = SIACPO_Session.hidMensajeTieneBonoDTH.split("|");
                                if (SIACPO_Session.hidSoloConsultaDTH == SIACPO_Session.Vacio) {
                                    confirm(
                                        tmpMnj[1],
                                        'Confirmar',
                                        function () {
                                            //ShowGeneric();
                                            //abreValidacion(99, 'gConstkeyPermisoBonoDTH');
                                            //return;
                                        },
                                        function () {
                                            control.divFidelizaDTH.hide();
                                            control.cboAccion.val(SIACPO_Session.strMenosUno);
                                            $("#" + div1).hide();
                                            $("#" + div2).hide();
                                            $("#" + txt1).val(SIACPO_Session.Vacio);
                                            $("#" + lbl1).text(SIACPO_Session.Vacio);
                                        }
                                    );
                                }
                            } else if (tmp[0] == SIACPO_Session.strCero) {
                                control.divFidelizaDTH.show();
                                $("#" + div1).hide();
                                $("#" + div2).hide();
                                $("#" + txt1).val(SIACPO_Session.Vacio);
                                $("#" + lbl1).text(SIACPO_Session.Vacio);

                                that.f_ObtienePeriodoMaxDTH(val);
                            } else {
                                if (SIACPO_Session.hidSoloConsultaDTH == SIACPO_Session.Vacio) {
                                    var tmpMsjs = SIACPO_Session.hidMensajeTieneBonoDTH.split('|');
                                    alert(tmpMsjs[0],"Informativo");
                                }

                                control.divFidelizaDTH.hide();
                                control.divSeccionBonoDTH1.hide();
                                control.divSeccionBonoDTH2.hide();
                                control.divSeccionDstoBonoDTH.hide();
                                control.cboAccion.val(SIACPO_Session.strMenosUno);
                            }
                        }
                    },
                    error: function (response) {
                        console.error(response);
                    }
                });
            } else {
                control.divFidelizaDTH.hide();
                control.divSeccionBonoDTH1.hide();
                control.divSeccionBonoDTH2.hide();
                control.divSeccionDstoBonoDTH.hide();

                var arr;
                var flgDscto;
                arr = val.split('|');
                flgDscto = arr[1];

                if (flgDscto == 1) {
                    SIACPO_Session.hidDscto = SIACPO_Session.iUno;
                } else {
                    SIACPO_Session.hidDscto = SIACPO_Session.iCero;
                }

                $("#" + txt2).val(SIACPO_Session.Vacio);

                if (SIACPO_Session.hidAccionTra == SIACPO_Session.Retenido) {
                    var arrAccion;
                    arrAccion = val.split('|');
                    var arrValoresOCC;

                    if (arrAccion[1] == SIACPO_Session.strUno) {
                        $("#" + div1).show();
                        $("#" + div2).hide();

                        arrValoresOCC = arrAccion[2].split(';');
                        SIACPO_Session.hidCodigoOCC = arrValoresOCC[0];
                        $("#" + lbl2).text(arrValoresOCC[1]);
                    } else if (arrAccion[1] == SIACPO_Session.strCero) {
                        $("#" + div1).hide();
                        $("#" + div2).show();
                        $("#" + txt1).val(SIACPO_Session.strNumeroCeroDecimal);

                        if (arrAccion[0] == SIACPO_Session.hidCambNumSinCosto) {
                            $("#" + txt1).val(SIACPO_Session.hidVCambNumSinCosto);
                        };

                        if (arrAccion[0] == SIACPO_Session.hidSusTempSinCostRec) {
                            $("#" + txt1).val(SIACPO_Session.hidVSusTempSinCostRec);
                        };

                        $("#" + lbl1).text(arrAccion[3]);
                        SIACPO_Session.hidFlagValMA = SIACPO_Session.Vacio;
                        SIACPO_Session.hidFlagValMASJ = SIACPO_Session.Vacio;
                        SIACPO_Session.hifFlagValMonto = SIACPO_Session.Vacio;
                    } else {
                        $("#" + div1).hide();
                        $("#" + div2).hide();
                        $("#" + txt1).val(SIACPO_Session.Vacio);
                        $("#" + lbl1).text(SIACPO_Session.Vacio);
                    }
                }
            }
        },
        // GetListPeriodMaxDTH Falta Implementar
        f_ObtienePeriodoMaxDTH: function (objValorMax) {
            var that = this,
                control = that.getControls(),
                objValorMaxArr = objValorMax.split('|');

            if (objValorMax == SIACPO_Session.Vacio) {
                control.cboPeriodosDHT.val(SIACPO_Session.strMenosUno); return;
            }

            if (objValorMaxArr[4] == SIACPO_Session.strUno) {
                control.divSeccionDstoBonoDTH.hide();
                control.divSeccionBonoDTH1.show();
                control.divSeccionBonoDTH2.show();
            } else {
                control.divSeccionDstoBonoDTH.show();
                control.divSeccionBonoDTH1.hide();
                control.divSeccionBonoDTH2.hide();
                control.lblDstoBonoDTH.text(SIACPO_Session.strNumeroCeroDecimal + SIACPO_Session.Espacio + $("#cboAccion option:selected").html());
            }

            var param = {};

            param.strIdSession = Session.IDSESSION;
            param.idPerMaxDTH = objValorMaxArr[5];

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: "/Transactions/Postpaid/PostpaidRetentionCancelServices/GetListPeriodMaxDTH",
                success: function (response) {
                    if (response.data != null) {
                        var data = response.data;
                        var idSelectValor = 0;
                        var emp = data.split("|");
                        control.cboPeriodosDHT.html("");

                        if (emp[0] == '-1') {
                            for (var i = 0; i < emp.length - 1; i++) {
                                if (idSelectValor == 0) {
                                    control.cboPeriodosDHT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                                    idSelectValor = idSelectValor + 1;
                                }
                            }
                        } else {
                            for (var i = 0; i < emp.length - 1; i++) {
                                if (idSelectValor == 0) {
                                    control.cboPeriodosDHT.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                                    idSelectValor = idSelectValor + 1;
                                }
                                if (emp.length >= 2) {
                                    var value = emp[i];
                                    i = i + 1;
                                    var text = emp[i];
                                    control.cboPeriodosDHT.append($('<option>', { value: value, html: text }));
                                }
                            }
                        }
                    }
                },
                error: function (response) {
                    console.error(response);
                }
            });
        },

        btnSummary_click: function (e) {
            var that = this;

            if ($(e).attr('id') == 'btnSummary') {
                $('.btn-circle.transaction-button').removeClass('transaction-button').addClass('btn-default');
                $(e).addClass('transaction-button').removeClass('btn-default').blur();
                var percent = $(e).attr('percent');
                document.getElementById('prog').style.width = percent;
            } else {
                navigateTabs(e);
            }
        },

        IniPage: function () {
            var that = this,
           controls = that.getControls(),
           objLoad = {};

            objLoad.strIdSession = Session.IDSESSION;
            objload.ListNumImportar = '';

            //
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objLoad),
                url: '/Transactions/Postpaid/RetentionCancelServices/LoadPage',
                asyn: false,
                success: function (response) {
                    //

                }
            });
        },
    };

    $.fn.PostpaidRetentionCancelServices = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidRetentionCancelServices'),
                options = $.extend({}, $.fn.PostpaidRetentionCancelServices.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidRetentionCancelServices', data);
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

    $.fn.PostpaidRetentionCancelServices.defaults = {
    }

    $('#PostpaidRetentionCancelServices').PostpaidRetentionCancelServices();

})(jQuery);