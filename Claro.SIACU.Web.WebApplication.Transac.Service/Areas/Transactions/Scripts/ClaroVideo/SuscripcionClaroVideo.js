var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log(SessionTransac);

//DATOS DE PRUEBA
//SessionTransac.SessionParams.DATACUSTOMER.Application = 'HFC';
//SessionTransac.SessionParams.DATACUSTOMER.CustomerID = '29152861'//'25896423' 16426849 // '31933665' desarrollo;
//SessionTransac.SessionParams.DATACUSTOMER.Email = 'fotoya@claro.com.pe' // Tiffy.treot@gmail.com 'fiorella.otoya@gmail.com' //'Tiffany.trelles15a@gmail.com'; 
//SessionTransac.SessionParams.DATACUSTOMER.Telephone = '982206520' //'951253412'; //914697496 // 914669248 
//SessionTransac.SessionParams.DATACUSTOMER.Name = 'Paola';
//SessionTransac.SessionParams.DATACUSTOMER.LastName = 'Cerdan';
//var CodigoInteraccion = '0004';
var flagUnicoIPTV = 0;

(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.SuscripcionClaroVideo.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element,
            txtCorreoelectronico: $('#txtCorreoelectronico', $element),
            txtNombre: $('#txtNombre', $element),
            txtApellido: $('#txtApellido', $element),
            txtLinea: $('#txtLinea', $element),
            cboPuntoVenta: $('#cboPuntoVenta', $element),
            chkSentEmail: $('#chkSentEmail', $element),
            txtSendforEmail: $('#txtSendforEmail', $element),
            btnHistoryActivateService: $('#btnHistoryActivateService', $element),
            btnViewHistory: $('#btnViewHistory', $element),
            chkChangeEmail: $('#chkChangeEmail', $element),
            txtCambiarCorreo: $('#txtCambiarCorreo', $element),
            btnViewHistoryVisualization: $('#btnViewHistoryVisualization', $element),
            btnConstancia: $('#btnConstancia', $element),
            chkDesvincular: $('#chkDesvincular', $element),
            btnViewHistoryDevice: $('#btnViewHistoryDevice', $element),
            btnSave: $('#btnSave', $element),
            txtNote: $('#txtNote', $element),
            btnRegistrar: $('#btnRegistrar', $element),
            accordionExample: $('#accordionExample', $element),
            PanelDesplegables: $('#PanelDesplegables', $element),
            hdTmcode: $('#hdTmcode', $element),
            chkChangePassword: $('#chkChangePassword', $element),
            lblMessagePass: $('#lblMessagePass', $element),
            btnClose: $('#btnClose', $element),
            TabSuscripcion: $('#TabSuscripcion', $element),
            TabAdministracion: $('#TabAdministracion', $element),
            // [INICIO]PROY-140510 IDEA-141421 - AMCO
            txtEmailAMCO: $('#txtEmailAMCO', $element),
            txtCorreoAMCO: $('#txtCorreoAMCO', $element),
            txtEmailAMCO2: $('#txtEmailAMCO2', $element),
            txtNamesAMCO: $('#txtNamesAMCO', $element),
            txtLastNamesAMCO: $('#txtLastNamesAMCO', $element),
            txtLineAMCO: $('#txtLineAMCO', $element),
            btnConsultar: $('#btnConsultar', $element),
            btnEliminar: $('#btnEliminar', $element),
            TabConsulta: $('#TabConsulta', $element)
            // [FIN]PROY-140510 IDEA-141421 - AMCO

        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();

            controls.chkSentEmail.addEvent(that, 'click', that.chkSentEmail_click);
            controls.chkChangeEmail.addEvent(that, 'click', that.chkChangeEmail_click);
            controls.btnHistoryActivateService.addEvent(that, 'click', that.OpenHistoryActivateService);
            controls.btnViewHistory.addEvent(that, 'click', that.OpenHistoryRentalUser);
            controls.btnViewHistoryVisualization.addEvent(that, 'click', that.OpenHistoryVisualization);
            controls.chkDesvincular.addEvent(that, 'change', that.ChkDesvincular_change);
            controls.btnViewHistoryDevice.addEvent(that, 'click', that.ViewHistoryDevice);
            controls.btnSave.addEvent(that, 'click', that.SaveSubscription_click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_click);
            controls.btnRegistrar.addEvent(that, 'click', that.btnRegistrar_click);
            controls.chkChangePassword.addEvent(that, 'change', that.chkChangePass);
            controls.btnClose.addEvent(that, 'click', that.btnClose_click);
            controls.TabSuscripcion.addEvent(that, 'click', that.TabSuscripcion_click);
            controls.TabAdministracion.addEvent(that, 'click', that.TabAdministracion_click);
            controls.TabConsulta.addEvent(that, 'click', that.TabConsulta_click);
            controls.btnConsultar.addEvent(that, 'click', that.btnConsultar_click);
            controls.btnEliminar.addEvent(that, 'click', that.btnEliminar_click);
            controls.txtEmailAMCO.addEvent(that, 'blur', that.ValidateEmail);
            that.render();
            that.loadCustomerData();
        },
        render: function () {
            var that = this,
            controls = that.getControls();

            that.ValidarSuscripcion(function (callback) {

                if (callback) {
                    that.CargarDatosCliente();
                    that.getCACDAC();

                    
                    //INICIATIVA-794
                    var strOpc = '';
                    if (SessionTransac.SessionParams.DATACUSTOMER.Application == 'PREPAID' || SessionTransac.SessionParams.DATACUSTOMER.Application == 'POSTPAID') {
                        strOpc = '1';
                    } else {
                        strOpc = '2';
                    }
                    that.dataListClaroVideo.resultadoValidarIPTV = that.GetValidarServicioIPTV(controls.txtLinea.val().trim(), strOpc);
                    
                    
                    var ClienteClarovideo = that.flagIsClientClarovideo;
                    if (ClienteClarovideo) {

                        that.dataListClaroVideo.listaVisualizacionesRentas = [];

                        that.searhRentalUser(function (flagOk) {

                            if (flagOk) {
                                if (that.dataListClaroVideo.listaVisualizacionesRentas.length > 0) {

                                    $('#tbRentalUser').find('tbody').html('');

                                    console.log(that.dataListClaroVideo.listaVisualizacionesRentas);

                                    that.populateGridRentalUser(that.dataListClaroVideo.listaVisualizacionesRentas);

                                } else {
                                    $('#tbRentalUser').find('tbody').html('');
                                    that.populateGridRentalUser(null);
                                }
                            }

                        });

                        that.LoadSuscripciones(function (ListaSuscripciones) {
                            if (ListaSuscripciones.length > 0) {
                                that.ValidaPeriodoPromocional();
                            }
                        });
                    }
                }
            });
        },
        TabAdministracion_click: function (sender, args) {
            $("#divPrePostContacto").addClass("active in");
            $("#divpreposttitular").removeClass("active in");
            $("#divSectionEmail").addClass("active in");
            $("#content").show();
        },
        TabSuscripcion_click: function (sender, args) {
            $("#divPrePostContacto").removeClass("active in");
            $("#divpreposttitular").addClass("active in");
            $("#divSectionEmail").addClass("active in");
            $("#content").show();
        },
        // [INICIO]PROY-140510 IDEA-141421 - AMCO
        TabConsulta_click: function (sender, args) {
            var that = this,
            controls = that.getControls();
            var linea = controls.txtLinea.val()
            if (linea == "") {
                $('#divPrePostContactoConsulta').attr('style', 'display:none');
                $("#content").hide();
                $("#divSectionEmail").removeClass("active in");
                alert(GetKeyConfig("strMsjSelectServicio"));
            }
            else {
                $('#divPrePostContactoConsulta').addClass("active in");
                $("#divpreposttitular").removeClass("active in");
                $("#divPrePostContacto").removeClass("active in");
                $("#divSectionEmail").removeClass("active in");
                $("#content").hide();
            }
        },

        loadCustomerData: function () {
            var that = this;
            var controls = that.getControls();
            Session.CLIENTE = SessionTransac.SessionParams.DATACUSTOMER;
            controls.txtEmailAMCO.val((Session.DATACUSTOMER.Email == null) ? '' : Session.DATACUSTOMER.Email);

        },

        AplicationAmco: "",
        LineaAmco: "",
        NombreAmco: "",
        ApellidoAmco: "",
        EmailAmco: "",
        CustomerAmco: "",
        btnConsultar_click: function () {
            var that = this,
            controls = that.getControls();
            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;
            var strFechaInicio = "";
            var strFechaFin = "";
            var Email = controls.txtEmailAMCO.val().trim();
            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarrentascliente");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });
            var varqueryUserOttRequest = {
                invokeMethod: 'consultardatoscliente',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                startDate: strFechaInicio,
                endDate: strFechaFin,
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                extraData: { data: [{ key: "email", value: Email }] },
                serviceName: 'consultardatoscliente',
                providerId: StrPartnerID,
                iccidManager: 'AMCO'
            };

            var objqueryUserOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryUserOttRequest: varqueryUserOttRequest }
                }
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultClientSN',
                data: JSON.stringify(objqueryUserOttRequest),
                success: function (response) {
                    if (response.data.QueryUserOttResponse != null) {
                        var CustomerId = "";
                        var fecha = new Date();
                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();
                        var cuentaAmco = "";
                        var IdInteraccion = "";
                        var AddNote = "";
                        var metodo = "CONSULTA_CLARO_VIDEO";
                        var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
                        if (response.data.QueryUserOttResponse.resultCode == "0") {
                            document.getElementById("txtEmailAMCO").disabled = true;
                            if (response.data.QueryUserOttResponse.userData != null) {
                                if (response.data.QueryUserOttResponse.userData.ListUserData != null) {
                                    if (response.data.QueryUserOttResponse.userData.ListUserData.length > 0) {
                                        document.getElementById("btnEliminar").disabled = false;
                                        if (response.data.QueryUserOttResponse.CUSTOMERID != null && response.data.QueryUserOttResponse.CUSTOMERID != undefined && response.data.QueryUserOttResponse.CUSTOMERID != '') {
                                            that.CustomerAmco = response.data.QueryUserOttResponse.CUSTOMERID;
                                        } else if (Session.CLIENTE.CustomerID != null || Session.CLIENTE.CustomerID != undefined) {
                                            that.CustomerAmco = Session.CLIENTE.CustomerID;
                                        } else if (Session.CLIENTE.objPostDataAccount.CustomerId != null && Session.CLIENTE.objPostDataAccount.CustomerId != undefined) {
                                            that.CustomerAmco = Session.CLIENTE.objPostDataAccount.CustomerId;
                                        }
                                        if (response.data.QueryUserOttResponse.userData.ListUserData[0].nombre != null && response.data.QueryUserOttResponse.userData.ListUserData[0].nombre != '') {
                                            that.NombreAmco = response.data.QueryUserOttResponse.userData.ListUserData[0].nombre;
                                        }
                                        if (response.data.QueryUserOttResponse.userData.ListUserData[0].apellido != null && response.data.QueryUserOttResponse.userData.ListUserData[0].apellido != '') {
                                            that.ApellidoAmco = response.data.QueryUserOttResponse.userData.ListUserData[0].apellido;
                                        }
                                        if (response.data.QueryUserOttResponse.userData.ListUserData[0].email != null && response.data.QueryUserOttResponse.userData.ListUserData[0].email != '') {
                                            that.EmailAmco = response.data.QueryUserOttResponse.userData.ListUserData[0].email;
                                        }
                                        if (response.data.QueryUserOttResponse.userData.ListUserData[0].cuenta != null && response.data.QueryUserOttResponse.userData.ListUserData[0].cuenta != '' &&
                                            response.data.QueryUserOttResponse.userData.ListUserData[0].cuenta != "null") {
                                            cuentaAmco = response.data.QueryUserOttResponse.userData.ListUserData[0].cuenta;
                                            if (cuentaAmco.length == 11) {
                                                that.LineaAmco = cuentaAmco.substr(2);
                                                CustomerId = that.LineaAmco;
                                                var LineaAsistobe = that.ConsultarLineaCuenta();
                                                if (LineaAsistobe == "NO") {
                                                    that.AplicationAmco = that.GetConsultaDatosLinea();
                                                }
                                                else if (LineaAsistobe == "SI") {
                                                    that.AplicationAmco = that.ConsultarContrato();
                                                }
                                            } else {
                                                that.LineaAmco = cuentaAmco.substr(0, 8);
                                                that.AplicationAmco = "HFC";
                                                CustomerId = "H" + that.LineaAmco;
                                            }
                                        } else {
                                            that.LineaAmco = "";
                                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                                that.AplicationAmco = "HFC";
                                                if (Session.CLIENTE.CustomerID != null || Session.CLIENTE.CustomerID != undefined) {
                                                    CustomerId = "H" + Session.CLIENTE.CustomerID;
                                                } else if (Session.CLIENTE.objPostDataAccount.CustomerId != null && Session.CLIENTE.objPostDataAccount.CustomerId != undefined) {
                                                    CustomerId = "H" + Session.CLIENTE.objPostDataAccount.CustomerId;
                                                }
                                            } else if (oCustomer.Application == 'POSTPAID') {
                                                that.AplicationAmco = "POSTPAGO";
                                                CustomerId = Session.CLIENTE.Telephone;
                                            } else if (oCustomer.Application == 'PREPAID') {
                                                that.AplicationAmco = "PREPAGO";
                                                CustomerId = Session.CLIENTE.TelephoneCustomer;
                                            }

                                        }
                                        controls.txtLastNamesAMCO.val(that.ApellidoAmco);
                                        controls.txtEmailAMCO2.val(that.EmailAmco);
                                        controls.txtNamesAMCO.val(that.NombreAmco);
                                        controls.txtLineAMCO.val(that.LineaAmco);
                                        AddNote = SessionTransac.SessionParams.USERACCESS.fullName + ' ' + FechaRegistro + '\n' +
                                            '\n\n' + 'CONSULTA CUENTA CLARO VIDEO:' + '\n' + 'Correo Electrónico: ' + that.EmailAmco + '\n' + 'Nombres: ' + that.NombreAmco +
                                            '\n' + 'Apellidos: ' + that.ApellidoAmco + '\n' + 'Linea/contrato: ' + that.LineaAmco;
                                        IdInteraccion = that.GenerarInteraccionAmco(metodo, CustomerId, AddNote);
                                        if (IdInteraccion != "0") {
                                            that.GenerarInteraccionPlusAmco(metodo, AddNote, CustomerId, IdInteraccion);
                                        }

                                    }

                                }
                            }
                        }
                        else if (response.data.QueryUserOttResponse.resultCode == "6") {
                            document.getElementById("txtEmailAMCO").disabled = true;
                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                that.AplicationAmco = "HFC";
                                if (Session.CLIENTE.CustomerID != null || Session.CLIENTE.CustomerID != undefined) {
                                    CustomerId = "H" + Session.CLIENTE.CustomerID;
                                } else if (Session.CLIENTE.objPostDataAccount.CustomerId != null && Session.CLIENTE.objPostDataAccount.CustomerId != undefined) {
                                    CustomerId = "H" + Session.CLIENTE.objPostDataAccount.CustomerId;
                                }
                            } else if (oCustomer.Application == 'POSTPAID') {
                                that.AplicationAmco = "POSTPAGO";
                                CustomerId = Session.CLIENTE.Telephone;
                            } else {
                                that.AplicationAmco = "PREPAGO";
                                CustomerId = Session.CLIENTE.TelephoneCustomer;
                            }
                            AddNote = SessionTransac.SessionParams.USERACCESS.fullName + ' ' + FechaRegistro + '\n\n' + 'El correo ' + Email + ' no se encuentra registrado';
                            IdInteraccion = that.GenerarInteraccionAmco(metodo, CustomerId, AddNote);
                            if (IdInteraccion != "0") {
                                that.GenerarInteraccionPlusAmco(metodo, AddNote, CustomerId, IdInteraccion);
                            }
                            alert("Este correo " + Email + " no se encuentra registrado");
                        }
                        else if (response.data.QueryUserOttResponse.resultCode != "0" || response.data.QueryUserOttResponse.resultCode != "6") {
                            alert("Consulta no exitosa, reintentelo mas tarde");
                        }
                    }
                    else {
                        alert("Ocurrió un error, inténtelo nuevamente.");
                    }
                }
            });
        },

        ConsultarContrato: function () {
            var that = this,
            controls = that.getControls();
            var tipo = "";
            var GetTypeProductDatRequest = {
                strIdSession: Session.IDSESSION,
                strIdTransaction: Session.IDSESSION,
                strTelephone: that.LineaAmco
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultarContrato',
                data: JSON.stringify(GetTypeProductDatRequest),
                success: function (response) {
                    if (response.data.responseStatus.codigoRespuesta != null) {
                        console.log("consult 1");
                        if (response.data.responseStatus.codigoRespuesta == "0") {
                            console.log("consult 2");
                            tipo = response.data.responseData.contrato[0].ofertaProducto[0].producto.caracteristicaProducto.tipoServicio;
                            console.log(tipo)
                        } else if (response.data.responseStatus.codigoRespuesta == "2") {
                            alert("no se encontro al cliente");
                        }
                    }
                }
            });
            return tipo;
        },

        GetConsultaDatosLinea: function () {
            var that = this,
            controls = that.getControls();
            var tipo = "";
            var GetTypeProductDatRequest = {
                strIdSession: Session.IDSESSION,
                strTelephone: that.LineaAmco
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/GetConsultaDatosLinea',
                data: JSON.stringify(GetTypeProductDatRequest),
                success: function (response) {
                    if (response.data.auditResponseField.codigoRespuestaField != null) {
                        if (response.data.auditResponseField.codigoRespuestaField == "0") {
                            tipo = response.data.datoLineaField.tipoField;
                            if (tipo != "PREPAGO") {
                                tipo = "POSTPAGO";
                            }
                            else {
                                tipo = "PREPAGO";
                            }
                        } else if (response.data.auditResponseField.codigoRespuestaField == "2") {
                            alert("no se encontro al cliente");
                        }
                    }
                }
            });
            return tipo;
        },

        //INICIATIVA-794
        GetValidarServicioIPTV: function (strCodNum, strOpc) {
            var that = this,
            controls = that.getControls();
            var tipo = "";
            var GetTypeProductDatRequest = {
                strIdSession: Session.IDSESSION,
                strCodNum: strCodNum,
                strOpc: strOpc
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ValidarServicioIPTV',
                data: JSON.stringify(GetTypeProductDatRequest),
                success: function (response) {
                    if (response.data != null) {
                        tipo = response.data;
                    }
                }
            });
            return tipo;
        },
        //INICIATIVA-794
        GetConsultarServicioIPTV: function (Producto) {
            var that = this,
            controls = that.getControls();
            //var tipo;
            var GetTypeProductDatRequest = {
                strIdSession: Session.IDSESSION,
                strProducto: Producto
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultarServicioIPTV',
                data: JSON.stringify(GetTypeProductDatRequest),
                success: function (response) {
                    if (response.data != null) {
                        that.listaServicios = response.data;
                    }
                }
            });
            //return tipo;
        },
        ConsultarLineaCuenta: function () {
            var that = this,
            controls = that.getControls();
            var Description = "";
            var objqueryUserOttRequest = {
                strIdSession: Session.IDSESSION,
                Tipo: "1",
                Valor: that.LineaAmco
            }
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultarLineaCuenta',
                data: JSON.stringify(objqueryUserOttRequest),
                success: function (response) {
                    if (response.data.ResponseValue != null) {
                        Description = response.data.ResponseDescription;
                    }
                }
            });
            return Description;
        },

        GenerarInteraccionAmco: function (TipoInteraccion, CustomerId, AddNote) {
            var that = this,
            controls = that.getControls();
            var idInteraccion = '0';
            var strClaseInfo = that.getTipificacionInfo(TipoInteraccion);
            var oModel = {
                strIdSession: Session.IDSESSION,
                objIdContacto: '0',
                Type: strClaseInfo[0].split(",")[0],
                Class: strClaseInfo[0].split(",")[1],
                SubClass: strClaseInfo[0].split(",")[2],
                Note: AddNote,
                CustomerId: CustomerId,
                Plan: '',
                ContractId: '',
                CurrentUser: SessionTransac.SessionParams.USERACCESS.login == null ? GetKeyConfig('strLeyPromoDefaultCodigoEmpleadoInterac') : SessionTransac.SessionParams.USERACCESS.login,
                objIdSite: '0',
                Cuenta: ''
            };
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/ClaroVideo/GenerarTipificacion',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin GenerarTipificacion");
                },
                success: function (response) {
                    console.log('response GenerarTipificacion');
                    console.log(response);

                    if (response.codeResponse == "0") {
                        idInteraccion = response.idInteraction;

                    } else {
                        idInteraccion = "0";

                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });

            return idInteraccion;
        },

        GenerarInteraccionPlusAmco: function (metodo, AddNote, CustomerId, IdInteraccion) {
            var that = this,
                controls = that.getControls();
            var fecha = new Date();
            var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();
            var onRequest = null;
            var ListaServiciosAdicionales = '';
            switch (metodo) {
                case GetKeyConfig("strNomTransaConsultaClaroVideo"):
                    onRequest = {
                        strIdSession: Session.IDSESSION,
                        template: {
                            _ID_INTERACCION: IdInteraccion,
                            _X_EMAIL: that.EmailAmco,
                            _X_INTER_29: AddNote,
                            _X_INTER_6: new Date(),
                            _X_INTER_17: FechaRegistro,
                            _X_INTER_7: "0",
                            _X_ADDRESS: that.EmailAmco

                        }
                    };
                    break;
                case GetKeyConfig("strNomTransaEliminarClaroVideo"):
                    onRequest = {
                        strIdSession: Session.IDSESSION,
                        template: {
                            _ID_INTERACCION: IdInteraccion,
                            _X_EMAIL: that.EmailAmco,
                            _X_INTER_29: AddNote,
                            _X_INTER_6: new Date(),
                            _X_INTER_17: FechaRegistro,
                            _X_INTER_7: "0",
                            _X_ADDRESS: that.EmailAmco
                        }
                    };

                    break;
                default:
                    console.log('El tipo de interaccion no esta definido ' + TipoInteraccion);
            }

            if (onRequest != null) {
                $.app.ajax({
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: that.strUrl + '/Transactions/Fixed/ClaroVideo/GenerarTipificacionPlus',
                    data: JSON.stringify(onRequest),
                    complete: function () {

                    },
                    success: function (response) {

                        if (response.codeResponse == "0") {
                            if (response.interaccionPlusId != "" && response.interaccionPlusId != "0") {
                                console.log('Se registro el detalle de la interaccion');
                            } else {
                                console.log('No se genero detalle de la interaccion');
                                that.ControlError.StrFlagFailedDetalleInteraccion = true;
                            }

                        } else {
                            that.ControlError.StrFlagFailedDetalleInteraccion = true;
                            console.log('No se genero detalle de la interaccion');
                        }
                    },
                    error: function (msger) {
                        that.ControlError.StrFlagFailedDetalleInteraccion = true;
                        console.log('response error GenerarInteraccionPlus:' + msger);

                    }
                });
            }
        },

        deleteRecord: function () {
            var that = this;
            var controls = that.getControls();
            var IdInteraccion = "0"
            var metodo = "ELIMINAR_CLARO_VIDEO";
            var CustomerId;
            var fecha = new Date();
            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
            var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();
            that.ProcessDeleteClientSN(function (callbackOK) {
                if (callbackOK == true) {
                    if (that.LineaAmco == "") {
                        if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                            that.AplicationAmco = "HFC";
                            if (Session.CLIENTE.CustomerID != null || Session.CLIENTE.CustomerID != undefined) {
                                CustomerId = "H" + Session.CLIENTE.CustomerID;
                            } else if (Session.CLIENTE.objPostDataAccount.CustomerId != null && Session.CLIENTE.objPostDataAccount.CustomerId != undefined) {
                                CustomerId = "H" + Session.CLIENTE.objPostDataAccount.CustomerId;
                            }
                        } else if (oCustomer.Application == 'POSTPAID') {
                            that.AplicationAmco = "POSTPAGO";
                            CustomerId = Session.CLIENTE.Telephone;
                        } else if (oCustomer.Application == 'PREPAID') {
                            that.AplicationAmco = "PREPAGO";
                            CustomerId = Session.CLIENTE.TelephoneCustomer;
                        }
                    } else if (that.AplicationAmco == "HFC") {
                        CustomerId = "H" + that.LineaAmco;
                    } else {
                        CustomerId = that.LineaAmco;
                    }

                    var AddNote = that.NombreAmco + ' ' + that.ApellidoAmco + ' ' + FechaRegistro + '\n' + 'DNI: ' +
                        '\n\n' + 'ELIMINAR CUENTA CLARO VIDEO:' + '\n' + 'Correo Electrónico: ' + that.EmailAmco + '\n' + 'Nombres: ' + that.NombreAmco +
                        '\n' + 'Apellidos: ' + that.ApellidoAmco + '\n' + 'Linea/contrato: ' + that.LineaAmco;
                    IdInteraccion = that.GenerarInteraccionAmco(metodo, CustomerId, AddNote);
                    if (IdInteraccion != "0") {
                        that.GenerarInteraccionPlusAmco(metodo, AddNote, CustomerId, IdInteraccion);
                    }
                }
            });
        },
        btnEliminar_click: function () {
            var that = this;
            var controls = that.getControls();
            if (that.LineaAmco == "" || that.LineaAmco == null || that.LineaAmco == undefined) {
                that.deleteRecord();
            }
            else {
                var gconstEliminar = GetKeyConfig("strPermisoEliminarRegistro");
                if (SessionTransac.SessionParams.USERACCESS.optionPermissions.indexOf(gconstEliminar) > -1) {
                    that.deleteRecord();
                }
                else {
                    confirm("Se requiere autorización del Jefe/Supervisor.", 'Confirmar', function () {
                        that.ValidateUser('strPermisoEliminarRegistro', that.deleteRecord, null, null, null);
                    }
                    )
                }
            }

        },

        ValidateUser: function (option, fn_success, fn_failled, fn_cancel, fn_error) {
            var xthat = this;
            $.window.open({
                autoSize: true,
                url: '/Transactions/AuthUser/Auth/AuthUserHtml',
                type: 'POST',
                title: 'SIACUNICO - Autenticación',
                modal: true,
                width: 360,
                height: 320,
                buttons: {
                    Aceptar: {
                        click: function (sender, args) {
                            var usu = $('#txtUsernameAuth').val();
                            var pass = $('#txtPasswordAuth').val();
                            var $this = this;
                            $.ajax({
                                type: "POST",
                                cache: false,
                                dataType: "json",
                                url: '/Transactions/CommonServices/CheckingUser',
                                data: { strIdSession: Session.IDSESSION, user: usu, pass: pass, option: option },
                                error: function (ex) {
                                    if (fn_error != null) {
                                        fn_error.call(xthat, true);
                                    }
                                },

                                beforeSend: function () {
                                    $.blockUI({
                                        message: '<div align="center"><img src="../../../../../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
                                        baseZ: $.app.getMaxZIndex() + 1,
                                        css: {
                                            border: 'none',
                                            padding: '15px',
                                            backgroundColor: '#000',
                                            '-webkit-border-radius': '10px',
                                            '-moz-border-radius': '10px',
                                            opacity: .5,
                                            color: '#fff'
                                        }
                                    });

                                },

                                complete: function () {
                                    $.unblockUI();
                                },
                                success: function (response) {

                                    if (response.result && response.result == 1) {
                                        if (fn_success != null) {
                                            $.unblockUI();
                                            fn_success.call(xthat, true);
                                        }
                                        $this.close();
                                        $.unblockUI();


                                    } else if (response.result == 2 || response.result == 0) {
                                        $.unblockUI();
                                        alert('La validacion del usuario ingresado es incorrecto o no tiene permisos para continuar con el proceso, por favor verifiquelo.');
                                        if (fn_failled != null) {
                                            fn_failled.call(xthat, true);
                                        }
                                    } else if (response.result == 3) {
                                        $.unblockUI();
                                        alert('Ocurrio un error al Validar el Usuario.');
                                        if (fn_error != null) {
                                            fn_error.call(xthat, true);
                                        }
                                    }
                                }
                            });
                        }
                    },
                    Cancelar: {

                        click: function (sender, args) {
                            var $that = this;
                            if (fn_cancel != null) {
                                fn_cancel.call(xthat, false);
                            }
                            $that.close();
                        }
                    }
                }
            });
        },

        ValidateEmail: function () {
            var that = this;
            var controls = that.getControls();
            controls = that.getControls();
            var validateEmailANCO = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
            var bValidate = validateEmailANCO.test($('#txtEmailAMCO').val());
            if (!bValidate) {
                alert(GetKeyConfig("strCorpMensajeEmailValido"));
                controls.txtEmailAMCO.val(''); return false;
            }

        },

        // [FIN]PROY-140510 IDEA-141421 - AMCO

        btnRegistrar_click: function (sender, args) {
            var that = this,
           controls = that.getControls();
            $(controls.PanelDesplegables).removeClass("hide");
            $(controls.PanelDesplegables).show();
            controls.btnRegistrar.prop('disabled', true);


            var oCustomer = null;
            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
                if (oCustomer.Application != undefined) {
                    if (oCustomer.Application == 'FTTH' || oCustomer.Application == 'HFC') { //VALIDAR TMDOC
                        that.ValidarServiciosAdicionalesFija();
                    }
                }
            }


            that.searhRentalUser(function (flagOk) {

                if (flagOk) {
                    if (that.dataListClaroVideo.listaVisualizacionesRentas.length > 0) {
                        $('#tbRentalUser').find('tbody').html('');
                        console.log(that.dataListClaroVideo.listaVisualizacionesRentas);

                        that.populateGridRentalUser(that.dataListClaroVideo.listaVisualizacionesRentas);

                    } else {
                        $('#tbRentalUser').find('tbody').html('');
                        that.populateGridRentalUser(null);
                    }
                }
            });

            that.LoadSuscripciones(function (ListaSuscripciones) {
                if (ListaSuscripciones.length > 0) {
                    that.ValidaPeriodoPromocional();
                }
            });

        },

        flagIsClientClarovideo: false,
        MostrarPromocionClaroVideo: function (Mensaje) {
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            var titleAlertaPromocional = GetKeyConfig("strtitleAlertaPromocional");
            alertPromocionClaroVideo(Mensaje, titleAlertaPromocional);
        },
        OpenServiciosAdicionalesFija: function (Mensaje) {

            var that = this,
            controls = that.getControls();

            $.window.open({
                modal: true,
                title: "",
                id: 'divmodalOpenServiciosAdicionalesFija',
                url: '/Transactions/Fixed/ClaroVideo/ViewServiceAdditional',
                type: 'POST',
                width: '600px',
                height: '450px',
                minimizeBox: false,
                maximizeBox: false,
                buttons: {
                    Cerrar: {
                        id: 'btnCerrarOpenHistoryVisualization',
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        ValidarSuscripcion: function (callback) {
            var that = this,
           controls = that.getControls();

            console.log("Se realiza la validacion si el cliente se encuentra registrado en claro video");
            that.VerificarClienteSuscrito(function (flag) {

                if (flag == 0 || flag == 2) {

                    var AccesoProductoPermitido = false;
                    var MensajeAccesoPermitido = 'El cliente no cuenta con una suscripción de Claro Video.';
                    var TitleAccesoPermitido = 'Confirmar';


                    if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                        var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                        console.log('Application: ' + oCustomer.Application);
                        console.log('El cliente si se encuentra registrado en claro video');

                        if (flag == 0) {

                            if (oCustomer.Application == 'PREPAID' || oCustomer.Application == 'POSTPAID' || oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {

                                AccesoProductoPermitido = true;
                                console.log('Acceso permitido');

                            } else {
                                AccesoProductoPermitido = false;
                                console.log('Acceso no permitido');
                            }
                        } else if (flag == 2) {
                            AccesoProductoPermitido = false;
                            TitleAccesoPermitido = 'Alerta';
                            MensajeAccesoPermitido = 'Acceso no permitido - Hubo un error en consultar los datos del cliente.';
                            console.log('Acceso no permitido - Hubo un error en consultar los datos del cliente.');
                        }


                    }

                    confirmSuscripcion(AccesoProductoPermitido, MensajeAccesoPermitido, TitleAccesoPermitido, function (result) {
                        if (result == true) {
                            //$('#btnconfirmYesCallCut').attr('data-loading-text', "<i class='fa fa-spinner fa-spin '></i> Cargando");
                            //$('#btnconfirmYesCallCut').button('loading');
                            //showLoading('Cargando información CLARO VIDEO.');
                            that.flagIsClientClarovideo = false;
                            callback(true);
                        } else {
                            that.flagIsClientClarovideo = false;
                            console.log("Cerrar");
                            callback(false);
                            that.btnClose_click();
                        }

                    });

                } else if (flag == 1) {

                    if (that.CustomerClaroVideo == '0' || that.CustomerClaroVideo == undefined || that.CustomerClaroVideo == '') {
                        AccesoProductoPermitido = false;
                        var MensajeIngresoClaroVideo = 'El cliente se encuentra registrado , no se pudo obtener el CustomerID de ClaroVideo';
                        console.log('Acceso no permitido el CustomerClarovideo es vacio');

                        confirmSuscripcion(AccesoProductoPermitido, MensajeIngresoClaroVideo, 'Confirmar', function (result) {
                        });

                    } else {
                        that.flagIsClientClarovideo = true;
                        callback(true);
                    }
                }

            });


        },
        ClearDatosGenerales: function () {
            var that = this,
            controls = that.getControls();
            controls.txtApellido.val("");
            controls.txtLinea.val("");
            controls.txtCorreoelectronico.val("");
            controls.txtNombre.val("");
            controls.txtNote.val("");
        },
        strCorreoClaro: "",
        VerificarClienteSuscrito: function (callback) {
            var that = this,
           controls = that.getControls();
            that.ClearDatosGenerales();
            var FlagExistCliente = 0;

            var oCustomer = null;
            var Email = '';
            that.dataListClaroVideo.listaSuscripcionCliente = [];

            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                if (that.strCorreoClaro != "") {
                    Email = that.strCorreoClaro;
                } else {
                    Email = oCustomer.Email;
                }

            }

            console.log('Se consulta la informacion del cliente mediante el siguiente email: ' + Email);

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarClienteSN");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });


            var Linea = that.ObtenerLineaCliente('Linea');

            var varqueryUserOttRequest = {
                invokeMethod: 'consultardatoscliente',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                startDate: strFechaInicio,
                endDate: strFechaFin,
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                extraData: {
                    data: [
                        {
                            key: "email",
                            value: Email
                        },
                        {
                            key: "medioPago",
                            value: Linea
                        }
                    ]
                },
                serviceName: 'consultardatoscliente',
                providerId: StrPartnerID,
                iccidManager: 'AMCO'
            };

            var objqueryUserOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryUserOttRequest: varqueryUserOttRequest }
                }
            }

            var LoadingPrincipal = GetKeyConfig("strLoadingPrincipal");
            showLoading(LoadingPrincipal);

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultClientSN',
                data: JSON.stringify(objqueryUserOttRequest),
                complete: function () {

                    hideLoading();//controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    if (response.data.QueryUserOttResponse != null) {
                        if (response.data.QueryUserOttResponse.resultCode == "0") {//cambia a 3                         

                            if (response.data.QueryUserOttResponse.userData != null) {
                                if (response.data.QueryUserOttResponse.userData.ListUserData != null) {
                                    if (response.data.QueryUserOttResponse.userData.ListUserData.length > 0) {

                                        FlagExistCliente = 1;

                                        controls.txtApellido.val(response.data.QueryUserOttResponse.userData.ListUserData[0].apellido);
                                        controls.txtCorreoelectronico.val(response.data.QueryUserOttResponse.userData.ListUserData[0].email);
                                        if (controls.txtCorreoelectronico.isEmptyObject != true) {
                                            controls.txtSendforEmail.val(response.data.QueryUserOttResponse.userData.ListUserData[0].email);
                                            $(controls.chkSentEmail).prop('checked', true);
                                        }

                                        controls.txtNombre.val(response.data.QueryUserOttResponse.userData.ListUserData[0].nombre);

                                    }
                                }
                            }

                            var ListaInactivos = [];

                            if (response.data.QueryUserOttResponse.ListUserSubscription != null) {
                                if (response.data.QueryUserOttResponse.ListUserSubscription.length > 0) {
                                    $.each(response.data.QueryUserOttResponse.ListUserSubscription, function (index, value) {

                                        if (value.activo == "0") { // inactivo

                                            var List = {
                                                idSubscription: ""
                                            }

                                            List.idSubscription = value.idSuscripcion;
                                            ListaInactivos.push(List);

                                        }

                                        if (value.activo == "1") { // activo

                                            var listaSuscripcion = {
                                                Metodo: "",
                                                precio: "",
                                                estadoPago: "",
                                                fechaAlta: "",
                                                origen: "",
                                                idSubscription: "",
                                                descripcion: "",
                                                promocion: "",
                                                accion: "",
                                                ProductID: "",
                                                idRefSuscripcion: "",
                                                fechaExpiracion: "",
                                                FlagVer: ""
                                            }

                                            listaSuscripcion.Metodo = "listaSuscripcionCliente";
                                            listaSuscripcion.precio = (value.precio == null ? '0' : value.precio);
                                            listaSuscripcion.estadoPago = value.estadoPago;
                                            listaSuscripcion.fechaAlta = value.fechaAlta;
                                            listaSuscripcion.origen = value.origen;
                                            listaSuscripcion.idSubscription = value.idSuscripcion;
                                            listaSuscripcion.descripcion = value.descripcion;
                                            listaSuscripcion.promocion = "";
                                            listaSuscripcion.accion = "Activado";
                                            listaSuscripcion.ProductID = value.productId;
                                            listaSuscripcion.idRefSuscripcion = value.idRefSuscripcion;
                                            listaSuscripcion.fechaExpiracion = value.fechaExpiracion;
                                            listaSuscripcion.FlagVer = '0';
                                            that.dataListClaroVideo.listaSuscripcionCliente.push(listaSuscripcion);
                                        }
                                    });


                                    $.grep(ListaInactivos, function (dataInactivo) {
                                        $.grep(that.dataListClaroVideo.listaSuscripcionCliente, function (data) {

                                            if (data.idSubscription == dataInactivo.idSubscription) {
                                                data.FlagVer = "1";
                                                return true;
                                            }
                                        });
                                    });

                                    console.log('ListaInactivos');
                                    console.log(ListaInactivos);

                                }
                            }
                        } else if (response.data.QueryUserOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.QueryUserOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {
                                if (flag != '') {
                                    alert(flag, 'Alerta');
                                }
                            });
                        }

                        if (FlagExistCliente == 1) {
                            if (response.data.QueryUserOttResponse.CUSTOMERID != null && response.data.QueryUserOttResponse.CUSTOMERID != undefined && response.data.QueryUserOttResponse.CUSTOMERID != '') {
                                that.CustomerClaroVideo = response.data.QueryUserOttResponse.CUSTOMERID; // ID DEL USUARIO DE CLARO VIDEO
                                console.log('Customer de ClaroVideo :' + that.CustomerClaroVideo);
                            }
                        }

                        callback(FlagExistCliente);

                    } else {
                        callback(2); // error
                    }
                },
                error: function (msger) {
                    console.log(msger);
                    callback(2); // error

                }
            });

        },
        GenerarProcesoClaroVideo: function (objParametersContancy, callback) {

            var that = this,
          controls = that.getControls();

            that.ConstanciaClaroVideo(objParametersContancy, function (Onbase) {

                console.log('ConstanciaClaroVideo: ' + Onbase);

                if (Onbase == '0') {
                    Onbase = '';
                }

                that.GenerarInteraccionPlus(objParametersContancy, Onbase);

                if ($(controls.chkSentEmail).is(':checked')) {

                    var Asunto = GetKeyConfig("strClaroVideoAsuntoCorreo");
                    var Motivo = GetKeyConfig("strClaroVideoMotivoCorreo");
                    var Remitente = GetKeyConfig("strClaroVideoCorreoRemitente");
                    var FlagHtml = GetKeyConfig("strClaroVideoFlagHtmlCorreo");
                    var BodyCorreo = GetKeyConfig("strClaroVideoCuerpoCorreo");

                    var Nombre = controls.txtNombre.val().trim();
                    var Apellido = controls.txtApellido.val().trim();

                    var NombreCompleto = Nombre + ' ' + Apellido;
                    var CorreoFinal = BodyCorreo.replace('@Nombre', NombreCompleto);
                    CorreoFinal = CorreoFinal.replace('@accion', objParametersContancy.strAccionBodyEmail);

                    console.log('****************************');
                    console.log('Parametros de correo');
                    console.log('Asunto: ' + Asunto);
                    console.log('Motivo: ' + Motivo);
                    console.log('Remitente: ' + Remitente);
                    console.log('FlagHtml: ' + FlagHtml);
                    console.log('BodyCorreo: ' + BodyCorreo);
                    console.log('BodyCorreo Final:' + CorreoFinal);
                    console.log('****************************');

                    that.EnvioCorreoClaroVideo(Remitente, controls.txtSendforEmail.val(), Asunto, CorreoFinal, FlagHtml, that.strRutaPDF);
                }

                callback();
            });

        },
        validateRegisterForm: function (callback) {

            var that = this,
          controls = this.getControls();
            var Mensaje = '';
            var flag = false;


            var Correo = controls.txtCorreoelectronico.val().trim();
            var CorreoNuevo = controls.txtCambiarCorreo.val().trim();
            var CorreoEnvio = controls.txtSendforEmail.val().trim();
            var Nombre = controls.txtNombre.val().trim();
            var Apellido = controls.txtApellido.val().trim();
            var Linea = controls.txtLinea.val().trim();

            var flagIsClientClarovideo = that.flagIsClientClarovideo;
            if (!flagIsClientClarovideo) {


                if (Correo == '') {
                    Mensaje = GetKeyConfig("strValidCorreoVacio");
                } else {
                    if (Correo.indexOf('@', 0) == -1 || Correo.indexOf('.', 0) == -1) {
                        Mensaje = 'El correo electrónico introducido no es correcto.';
                    }
                }

                if ($.isEmptyObject(that.dataListClaroVideo.listaSuscripciones) == false) {
                    if (that.dataListClaroVideo.listaSuscripciones.length == 0) {
                        Mensaje = GetKeyConfig("strValidUnaAccion");
                    }
                } else {
                    Mensaje = GetKeyConfig("strValidUnaAccion");
                }
            } else {

                var FlagAccionSuscripcion = true;
                var FlagAccionCancelacion = true;
                var FlagAccionRentas = true;
                var FlagAccionDesvinculacion = true;
                var FlagAccionCambioCorreo = true;
                var FlagAccionCambioClave = true;

                if ($.isEmptyObject(that.dataListClaroVideo.listaSuscripciones) == false) {
                    if (that.dataListClaroVideo.listaSuscripciones.length == 0) {
                        FlagAccionSuscripcion = false;
                    }
                } else {
                    FlagAccionSuscripcion = false
                }

                if ($.isEmptyObject(that.dataListClaroVideo.listaCancelaciones) == false) {
                    if (that.dataListClaroVideo.listaCancelaciones.length == 0) {
                        FlagAccionCancelacion = false;
                    }
                } else {
                    FlagAccionCancelacion = false
                }

                if ($.isEmptyObject(that.dataListClaroVideo.listaRentas) == false) {
                    if (that.dataListClaroVideo.listaRentas.length == 0) {
                        FlagAccionRentas = false;
                    }
                } else {
                    FlagAccionRentas = false
                }

                if ($(controls.chkChangeEmail).is(':checked')) {

                } else {
                    FlagAccionCambioCorreo = false
                }


                if ($(controls.chkChangePassword).is(':checked')) {

                } else {
                    FlagAccionCambioClave = false
                }


                if ($(controls.chkDesvincular).is(':checked')) {

                    if ($.isEmptyObject(that.dataListClaroVideo.listaDispositivos) == false) {
                        if (that.dataListClaroVideo.listaDispositivos.length == 0) {
                            FlagAccionDesvinculacion = false;
                        }
                    } else {
                        FlagAccionDesvinculacion = false
                    }

                } else {
                    FlagAccionDesvinculacion = false
                }

                if (FlagAccionSuscripcion == false && FlagAccionCancelacion == false && FlagAccionRentas == false && FlagAccionDesvinculacion == false && FlagAccionCambioCorreo == false && FlagAccionCambioClave == false) {
                    Mensaje = 'Debe realizar alguna acción';
                }

            }

            if (Nombre == '') {
                Mensaje = 'Debe ingresar el nombre';
            }

            if (Apellido == '') {
                Mensaje = 'Debe ingresar el apellido';
            }

            if (Linea == '') {
                Mensaje = 'Debe ingresar la linea';
            }


            if ($(controls.chkChangeEmail).is(':checked')) {
                if (CorreoNuevo == '') {
                    Mensaje = 'Debe ingresar el nuevo correo.';
                } else {
                    if (CorreoNuevo.indexOf('@', 0) == -1 || CorreoNuevo.indexOf('.', 0) == -1) {
                        Mensaje = 'El correo electrónico introducido no es correcto.';
                    }
                }
            }

            if ($(controls.chkSentEmail).is(':checked')) {
                if (CorreoEnvio == '') {
                    Mensaje = 'Debe ingresar el correo electronico de destino.';
                } else {
                    if (CorreoEnvio.indexOf('@', 0) == -1 || CorreoEnvio.indexOf('.', 0) == -1) {
                        Mensaje = 'El correo electrónico de destino no es correcto.';
                    }
                }
            }

            if (Mensaje != '') {
                flag = true;
                alert(Mensaje, 'Guardar');
            }

            callback(flag);
        },
        SaveSubscription_click: function (sender, args) {

            var that = this,
            controls = that.getControls();

            $('html, body').animate({ scrollTop: 0 }, 'slow');
            that.validateRegisterForm(function (flag) {

                if (flag) {

                    return false;

                } else {

                    var MensajeGrabar = GetKeyConfig("strMensajeGrabar");

                    confirm(MensajeGrabar, 'Confirmar', function (result) {

                        if (result == true) {

                            showLoading('Procesando..');

                            that.dataListClaroVideo.listaAcciones = [];

                            that.dataListClaroVideo.listaProcesosConstancias = [];


                            var oCustomer = null;
                            var TipoDocumento = '';
                            var NumeroDocumento = '';

                            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                                if (oCustomer.DocumentType != undefined && oCustomer.DocumentType != null) {
                                    TipoDocumento = oCustomer.DocumentType;
                                } else if (oCustomer.TypeDocument != undefined && oCustomer.TypeDocument != null) {
                                    TipoDocumento = oCustomer.TypeDocument;
                                } else {
                                    TipoDocumento = 'DNI';
                                }

                                if (oCustomer.DocumentNumber != undefined && oCustomer.DocumentNumber != null) {
                                    NumeroDocumento = oCustomer.DocumentNumber;
                                } else if (oCustomer.DNIRUC != undefined && oCustomer.DNIRUC != null) {
                                    NumeroDocumento = oCustomer.DNIRUC;
                                }
                            }

                            var listaAcciones = {
                                Metodo: "",
                                TipoConstancia: "",
                                Path: "",
                                GenerarSuscripcion: "",
                                GenerarCancelarSuscripcion: "",
                                GenerarActualizacionEmail: "",
                                IsProcesadoHPXTREAM: "",
                                IsGeneraConstancia: "",
                                GenerarActualizacionPassword: "",
                                GenerarActualizacionDispositivo: "",
                                GenerarSuscripcionRentas: "",
                                GenerarSuscripcionAdicionales: "",
                                ListDevice: [],
                                ListSuscriptcion: [],
                                ListService: [],
                                ListSuscriptcionRentas: [],
                                ListSuscriptcionAdicionales: [],
                                objParametersGenerateContancy: {
                                    strTipoConstanciaAMCO: "",
                                    StrNombreArchivoTransaccion: "",
                                    StrNombreArchivoPDF: "",
                                    strPuntoAtencion: "",
                                    strTitular: "",
                                    strRepresentante: "",
                                    strTipoDoc: "",
                                    strFechaAct: "",
                                    strNroCaso: "",
                                    strNroServicio: "",
                                    strNroDoc: "",
                                    strEmail: "",
                                    strKeyName: "",
                                    strKeyValue: "",
                                    strAccionBodyEmail: "",
                                    ListService: [],
                                    ListDevice: [],
                                    ListSuscriptcion: [],
                                    ListSuscriptcionRentas: [],
                                    ListSuscriptcionAdicionales: []

                                }
                            }

                            if ($(controls.chkChangeEmail).is(':checked')) {

                                listaAcciones = {
                                    Metodo: "",
                                    TipoConstancia: "",
                                    Path: "",
                                    GenerarSuscripcion: "",
                                    GenerarCancelarSuscripcion: "",
                                    GenerarActualizacionEmail: "",
                                    IsProcesadoHPXTREAM: "",
                                    IsGeneraConstancia: "",
                                    GenerarActualizacionPassword: "",
                                    GenerarActualizacionDispositivo: "",
                                    GenerarSuscripcionRentas: "",
                                    GenerarSuscripcionAdicionales: "",
                                    ListDevice: [],
                                    ListSuscriptcion: [],
                                    ListService: [],
                                    ListSuscriptcionRentas: [],
                                    ListSuscriptcionAdicionales: [],
                                    objParametersGenerateContancy: {
                                        strTipoConstanciaAMCO: "",
                                        StrNombreArchivoTransaccion: "",
                                        StrNombreArchivoPDF: "",
                                        strPuntoAtencion: "",
                                        strTitular: "",
                                        strRepresentante: "",
                                        strTipoDoc: "",
                                        strFechaAct: "",
                                        strNroCaso: "",
                                        strNroServicio: "",
                                        strNroDoc: "",
                                        strEmailAntiguo: "",
                                        strEmail: "",
                                        strKeyName: "",
                                        strKeyValue: "",
                                        strAccionBodyEmail: "",
                                        ListService: [],
                                        ListDevice: [],
                                        ListSuscriptcion: [],
                                        ListSuscriptcionRentas: [],
                                        ListSuscriptcionAdicionales: []
                                    }
                                }

                                listaAcciones.Metodo = GetKeyConfig("strNomTransaSusCambioCorreo"); // "CONSTANCIA_CAMBIO_CORREO_CLARO_VIDEO";
                                listaAcciones.TipoConstancia = "2";
                                listaAcciones.Path = "";
                                listaAcciones.GenerarSuscripcion = "0"; // metodo suscribir
                                listaAcciones.GenerarCancelarSuscripcion = "0";
                                listaAcciones.GenerarActualizacionEmail = "1"; // Metodo actualizar 
                                listaAcciones.IsProcesadoHPXTREAM = "0";  // flag de procesado     
                                listaAcciones.GenerarActualizacionPassword = "0";
                                listaAcciones.GenerarActualizacionDispositivo = "0";
                                listaAcciones.GenerarSuscripcionRentas = "0";
                                listaAcciones.GenerarSuscripcionAdicionales = "0";
                                listaAcciones.IsGeneraConstancia = "1",
                                listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "2";
                                listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSusCambioCorreo");
                                listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                listaAcciones.objParametersGenerateContancy.strEmailAntiguo = controls.txtCorreoelectronico.val();;
                                listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCambiarCorreo.val();
                                listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSusCambioCorreoName");
                                listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSusCambioCorreoValue");
                                listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailCambioCorreo");


                                that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);
                            }

                            if ($(controls.chkChangePassword).is(':checked')) {

                                listaAcciones = {
                                    Metodo: "",
                                    TipoConstancia: "",
                                    Path: "",
                                    GenerarSuscripcion: "",
                                    GenerarCancelarSuscripcion: "",
                                    GenerarActualizacionEmail: "",
                                    IsProcesadoHPXTREAM: "",
                                    IsGeneraConstancia: "",
                                    GenerarActualizacionPassword: "",
                                    GenerarActualizacionDispositivo: "",
                                    GenerarSuscripcionRentas: "",
                                    GenerarSuscripcionAdicionales: "",
                                    ListDevice: [],
                                    ListSuscriptcion: [],
                                    ListService: [],
                                    ListSuscriptcionRentas: [],
                                    ListSuscriptcionAdicionales: [],
                                    objParametersGenerateContancy: {
                                        strTipoConstanciaAMCO: "",
                                        StrNombreArchivoTransaccion: "",
                                        StrNombreArchivoPDF: "",
                                        strPuntoAtencion: "",
                                        strTitular: "",
                                        strRepresentante: "",
                                        strTipoDoc: "",
                                        strFechaAct: "",
                                        strNroCaso: "",
                                        strNroServicio: "",
                                        strNroDoc: "",
                                        strEmail: "",
                                        strKeyName: "",
                                        strKeyValue: "",
                                        strAccionBodyEmail: "",
                                        ListService: [],
                                        ListDevice: [],
                                        ListSuscriptcion: [],
                                        ListSuscriptcionRentas: [],
                                        ListSuscriptcionAdicionales: []
                                    }
                                }

                                listaAcciones.Metodo = GetKeyConfig("strNomTransaSusCambioPass"); // "CONSTANCIA_CAMBIO_CONTRASENA_CLARO_VIDEO";
                                listaAcciones.TipoConstancia = "1";
                                listaAcciones.Path = "";
                                listaAcciones.GenerarSuscripcion = "0"; // metodo suscribir 
                                listaAcciones.GenerarCancelarSuscripcion = "0";
                                listaAcciones.GenerarActualizacionEmail = "0"; // Metodo actualizar 
                                listaAcciones.IsProcesadoHPXTREAM = "0";  // flag de procesado      
                                listaAcciones.GenerarActualizacionPassword = "1";
                                listaAcciones.GenerarActualizacionDispositivo = "0";
                                listaAcciones.GenerarSuscripcionRentas = "0";
                                listaAcciones.GenerarSuscripcionAdicionales = "0",
                                listaAcciones.IsGeneraConstancia = "1",
                                listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "1";
                                listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSusCambioPass");
                                listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSusCambioPassName");
                                listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSusCambioPassValue");
                                listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailCambioPass");

                                that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);
                            }

                            if ($.isEmptyObject(that.dataListClaroVideo.listaDispositivos) == false) {

                                if (that.dataListClaroVideo.listaDispositivos.length > 0) {

                                    var listaAcciones = {
                                        Metodo: "",
                                        TipoConstancia: "",
                                        Path: "",
                                        GenerarActualizacionEmail: "",
                                        GenerarSuscripcion: "",
                                        GenerarCancelarSuscripcion: "",
                                        IsProcesadoHPXTREAM: "",
                                        IsGeneraConstancia: "",
                                        GenerarActualizacionPassword: "",
                                        GenerarActualizacionDispositivo: "",
                                        GenerarSuscripcionRentas: "",
                                        GenerarSuscripcionAdicionales: "",
                                        ListDevice: [],
                                        ListSuscriptcion: [],
                                        ListService: [],
                                        ListSuscriptcionRentas: [],
                                        ListSuscriptcionAdicionales: [],
                                        objParametersGenerateContancy: {
                                            strTipoConstanciaAMCO: "",
                                            StrNombreArchivoTransaccion: "",
                                            StrNombreArchivoPDF: "",
                                            strPuntoAtencion: "",
                                            strTitular: "",
                                            strRepresentante: "",
                                            strTipoDoc: "",
                                            strFechaAct: "",
                                            strNroCaso: "",
                                            strNroServicio: "",
                                            strNroDoc: "",
                                            strEmail: "",
                                            strKeyName: "",
                                            strKeyValue: "",
                                            strAccionBodyEmail: "",
                                            ListService: [],
                                            ListDevice: [],
                                            ListSuscriptcion: [],
                                            ListSuscriptcionRentas: [],
                                            ListSuscriptcionAdicionales: []
                                        }
                                    }

                                    listaAcciones.Metodo = GetKeyConfig("strNomTransaSusDesvDispositivo"); //"CONSTANCIA_DESVINCULAR_DISPOSITIVO_CLARO_VIDEO";
                                    listaAcciones.TipoConstancia = "4";
                                    listaAcciones.Path = "";
                                    listaAcciones.GenerarSuscripcion = "0";
                                    listaAcciones.GenerarCancelarSuscripcion = "0";
                                    listaAcciones.GenerarActualizacionEmail = "0"
                                    listaAcciones.IsProcesadoHPXTREAM = "0";
                                    listaAcciones.GenerarActualizacionPassword = "0";
                                    listaAcciones.GenerarActualizacionDispositivo = "1";
                                    listaAcciones.GenerarSuscripcionRentas = "0";
                                    listaAcciones.GenerarSuscripcionAdicionales = "0";
                                    listaAcciones.IsGeneraConstancia = "1",
                                    listaAcciones.ListDevice.push(that.dataListClaroVideo.listaDispositivos);
                                    listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "4";
                                    listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                    listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSusDesvDispositivo");
                                    listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                    listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                    listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                    listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                    listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                    listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                    listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                    listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSusDesvDispositivoName");
                                    listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSusDesvDispositivoValue");
                                    listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailCambioPassDesvDispositivo");


                                    that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                    that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);
                                }
                            }


                            if ($.isEmptyObject(that.dataListClaroVideo.listaSuscripciones) == false) {
                                if (that.dataListClaroVideo.listaSuscripciones.length > 0) {


                                    var ListaServiciosAdicionales = $.grep(that.dataListClaroVideo.listaSuscripciones, function (data) {
                                        return data.IsClaroVideo == "0";
                                    });


                                    if (ListaServiciosAdicionales != undefined && ListaServiciosAdicionales.length > 0) {

                                        listaAcciones = {
                                            Metodo: "",
                                            TipoConstancia: "",
                                            Path: "",
                                            GenerarSuscripcion: "",
                                            GenerarCancelarSuscripcion: "",
                                            GenerarActualizacion: "",
                                            IsProcesadoHPXTREAM: "",
                                            GenerarActualizacionPassword: "",
                                            GenerarActualizacionDispositivo: "",
                                            GenerarSuscripcionRentas: "",
                                            GenerarSuscripcionAdicionales: "",
                                            ListDevice: [],
                                            ListSuscriptcion: [],
                                            ListService: [],
                                            ListSuscriptcionRentas: [],
                                            ListSuscriptcionAdicionales: [],
                                            objParametersGenerateContancy: {
                                                strTipoConstanciaAMCO: "",
                                                StrNombreArchivoTransaccion: "",
                                                StrNombreArchivoPDF: "",
                                                strPuntoAtencion: "",
                                                strTitular: "",
                                                strRepresentante: "",
                                                strTipoDoc: "",
                                                strFechaAct: "",
                                                strNroCaso: "",
                                                strNroServicio: "",
                                                strNroDoc: "",
                                                strEmail: "",
                                                strKeyName: "",
                                                strKeyValue: "",
                                                strAccionBodyEmail: "",
                                                ListService: [],
                                                ListDevice: [],
                                                ListSuscriptcion: [],
                                                ListSuscriptcionRentas: [],
                                                ListSuscriptcionAdicionales: [],
                                            }
                                        }

                                        listaAcciones.Metodo = GetKeyConfig("strNomTransaSusAdicional");  //"CONSTANCIA_SUSCRIPCION_ADICIONALES_CLARO_VIDEO";
                                        listaAcciones.TipoConstancia = "5";
                                        listaAcciones.Path = "";
                                        listaAcciones.GenerarSuscripcion = "0";
                                        listaAcciones.GenerarCancelarSuscripcion = "0";
                                        listaAcciones.GenerarActualizacionEmail = "0";
                                        listaAcciones.IsProcesadoHPXTREAM = "0";
                                        listaAcciones.GenerarActualizacionPassword = "0";
                                        listaAcciones.GenerarActualizacionDispositivo = "0";
                                        listaAcciones.GenerarSuscripcionRentas = "0";
                                        listaAcciones.GenerarSuscripcionAdicionales = "1";
                                        listaAcciones.IsGeneraConstancia = "1",
                                        listaAcciones.ListSuscriptcionAdicionales.push(ListaServiciosAdicionales);
                                        listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "5";
                                        listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                        listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSusAdicional");
                                        listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                        listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                        listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                        listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                        listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                        listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                        listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                        listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                        listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                        listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSusAdicionalName");
                                        listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSusAdicionalValue");
                                        listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailSusAdicional");



                                        that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                        that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);

                                    }

                                    var ListaClaroVideo = $.grep(that.dataListClaroVideo.listaSuscripciones, function (data) {
                                        return data.IsClaroVideo == "1";
                                    });

                                    if (ListaClaroVideo != undefined && ListaClaroVideo.length > 0) {

                                        listaAcciones = {
                                            Metodo: "",
                                            TipoConstancia: "",
                                            Path: "",
                                            GenerarSuscripcion: "",
                                            GenerarCancelarSuscripcion: "",
                                            GenerarActualizacion: "",
                                            IsProcesadoHPXTREAM: "",
                                            GenerarActualizacionPassword: "",
                                            GenerarActualizacionDispositivo: "",
                                            GenerarSuscripcionRentas: "",
                                            GenerarSuscripcionAdicionales: "",
                                            ListDevice: [],
                                            ListSuscriptcion: [],
                                            ListService: [],
                                            ListSuscriptcionRentas: [],
                                            ListSuscriptcionAdicionales: [],
                                            objParametersGenerateContancy: {
                                                strTipoConstanciaAMCO: "",
                                                StrNombreArchivoTransaccion: "",
                                                StrNombreArchivoPDF: "",
                                                strPuntoAtencion: "",
                                                strTitular: "",
                                                strRepresentante: "",
                                                strTipoDoc: "",
                                                strFechaAct: "",
                                                strNroCaso: "",
                                                strNroServicio: "",
                                                strNroDoc: "",
                                                strEmail: "",
                                                strKeyName: "",
                                                strKeyValue: "",
                                                strAccionBodyEmail: "",
                                                ListService: [],
                                                ListDevice: [],
                                                ListSuscriptcion: [],
                                                ListSuscriptcionRentas: [],
                                                ListSuscriptcionAdicionales: [],
                                            }
                                        }

                                        listaAcciones.Metodo = GetKeyConfig("strNomTransaSuscripcion");  //"CONSTANCIA_SUSCRIPCION_CLARO_VIDEO";
                                        listaAcciones.TipoConstancia = "6";
                                        listaAcciones.Path = "";
                                        listaAcciones.GenerarSuscripcion = "1";
                                        listaAcciones.GenerarCancelarSuscripcion = "0";
                                        listaAcciones.GenerarActualizacionEmail = "0";
                                        listaAcciones.IsProcesadoHPXTREAM = "0";
                                        listaAcciones.GenerarActualizacionPassword = "0";
                                        listaAcciones.GenerarActualizacionDispositivo = "0";
                                        listaAcciones.GenerarSuscripcionRentas = "0";
                                        listaAcciones.GenerarSuscripcionAdicionales = "0";
                                        listaAcciones.IsGeneraConstancia = "1",
                                        listaAcciones.ListSuscriptcion.push(ListaClaroVideo);
                                        listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "6";
                                        listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                        listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSuscripcion");
                                        listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                        listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                        listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                        listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                        listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                        listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                        listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                        listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                        listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                        listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSuscripcionName");
                                        listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSuscripcionValue");
                                        listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailSuscripcion");

                                        that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                        that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);

                                    }
                                }
                            }

                            if ($.isEmptyObject(that.dataListClaroVideo.listaCancelaciones) == false) {
                                if (that.dataListClaroVideo.listaCancelaciones.length > 0) {

                                    listaAcciones = {
                                        Metodo: "",
                                        TipoConstancia: "",
                                        Path: "",
                                        GenerarSuscripcion: "",
                                        GenerarCancelarSuscripcion: "",
                                        GenerarActualizacion: "",
                                        IsProcesadoHPXTREAM: "",
                                        GenerarActualizacionPassword: "",
                                        GenerarActualizacionDispositivo: "",
                                        GenerarSuscripcionRentas: "",
                                        GenerarSuscripcionAdicionales: "",
                                        ListDevice: [],
                                        ListService: [],
                                        ListSuscriptcion: [],
                                        ListSuscriptcionRentas: [],
                                        ListSuscriptcionAdicionales: [],
                                        objParametersGenerateContancy: {
                                            strTipoConstanciaAMCO: "",
                                            StrNombreArchivoTransaccion: "",
                                            StrNombreArchivoPDF: "",
                                            strPuntoAtencion: "",
                                            strTitular: "",
                                            strRepresentante: "",
                                            strTipoDoc: "",
                                            strFechaAct: "",
                                            strNroCaso: "",
                                            strNroServicio: "",
                                            strNroDoc: "",
                                            strEmail: "",
                                            strKeyName: "",
                                            strKeyValue: "",
                                            strAccionBodyEmail: "",
                                            ListService: [],
                                            ListDevice: [],
                                            ListSuscriptcion: [],
                                            ListSuscriptcionRentas: [],
                                            ListSuscriptcionAdicionales: [],
                                        }
                                    }

                                    listaAcciones.Metodo = GetKeyConfig("strNomTransaSusCancelacion"); // CONSTANCIA_CANCELACION_CLARO_VIDEO";
                                    listaAcciones.TipoConstancia = "3";
                                    listaAcciones.Path = "";
                                    listaAcciones.GenerarSuscripcion = "0";
                                    listaAcciones.GenerarCancelarSuscripcion = "1";
                                    listaAcciones.GenerarActualizacionEmail = "0";
                                    listaAcciones.IsProcesadoHPXTREAM = "0";
                                    listaAcciones.GenerarActualizacionPassword = "0";
                                    listaAcciones.GenerarActualizacionDispositivo = "0";
                                    listaAcciones.GenerarSuscripcionRentas = "0";
                                    listaAcciones.GenerarSuscripcionAdicionales = "0";
                                    listaAcciones.IsGeneraConstancia = "1",
                                    listaAcciones.ListService.push(that.dataListClaroVideo.listaCancelaciones);
                                    listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "3";
                                    listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = listaAcciones.Metodo;
                                    listaAcciones.objParametersGenerateContancy.StrNombreArchivoPDF = GetKeyConfig("strNombrePDFTransaSusCancelacion");
                                    listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                    listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                    listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                    listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                    listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                    listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                    listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                    listaAcciones.objParametersGenerateContancy.strKeyName = GetKeyConfig("strOnBaseKeyTransaSusCancelacionName");
                                    listaAcciones.objParametersGenerateContancy.strKeyValue = GetKeyConfig("strOnBaseKeyTransaSusCancelacionValue");
                                    listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailCancelacion");

                                    that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                    that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);
                                }
                            }


                            if ($.isEmptyObject(that.dataListClaroVideo.listaRentas) == false) {
                                if (that.dataListClaroVideo.listaRentas.length > 0) {

                                    listaAcciones = {
                                        Metodo: "",
                                        TipoConstancia: "",
                                        Path: "",
                                        GenerarSuscripcion: "",
                                        GenerarCancelarSuscripcion: "",
                                        GenerarActualizacion: "",
                                        IsProcesadoHPXTREAM: "",
                                        GenerarActualizacionPassword: "",
                                        GenerarActualizacionDispositivo: "",
                                        GenerarSuscripcionRentas: "",
                                        GenerarSuscripcionAdicionales: "",
                                        ListDevice: [],
                                        ListService: [],
                                        ListSuscriptcion: [],
                                        ListSuscriptcionRentas: [],
                                        ListSuscriptcionAdicionales: [],
                                        objParametersGenerateContancy: {
                                            strTipoConstanciaAMCO: "",
                                            StrNombreArchivoTransaccion: "",
                                            StrNombreArchivoPDF: "",
                                            strPuntoAtencion: "",
                                            strTitular: "",
                                            strRepresentante: "",
                                            strTipoDoc: "",
                                            strFechaAct: "",
                                            strNroCaso: "",
                                            strNroServicio: "",
                                            strNroDoc: "",
                                            strEmail: "",
                                            strKeyName: "",
                                            strKeyValue: "",
                                            strAccionBodyEmail: "",
                                            ListService: [],
                                            ListDevice: [],
                                            ListSuscriptcion: [],
                                            ListSuscriptcionRentas: [],
                                            ListSuscriptcionAdicionales: [],
                                        }
                                    }

                                    listaAcciones.Metodo = "CONSTANCIA_SUSCRIPCION_RENTAS_CLARO_VIDEO";
                                    listaAcciones.TipoConstancia = "";
                                    listaAcciones.Path = "";
                                    listaAcciones.GenerarSuscripcion = "0";
                                    listaAcciones.GenerarCancelarSuscripcion = "0";
                                    listaAcciones.GenerarActualizacionEmail = "0";
                                    listaAcciones.IsProcesadoHPXTREAM = "0";
                                    listaAcciones.GenerarActualizacionPassword = "0";
                                    listaAcciones.GenerarActualizacionDispositivo = "0";
                                    listaAcciones.GenerarSuscripcionRentas = "1";
                                    listaAcciones.GenerarSuscripcionAdicionales = "0",
                                    listaAcciones.IsGeneraConstancia = "0",
                                    listaAcciones.ListSuscriptcionRentas.push(that.dataListClaroVideo.listaRentas);
                                    listaAcciones.objParametersGenerateContancy.strTipoConstanciaAMCO = "";
                                    listaAcciones.objParametersGenerateContancy.StrNombreArchivoTransaccion = "CONSTANCIA_SUSCRIPCION_RENTAS_CLARO_VIDEO";
                                    listaAcciones.objParametersGenerateContancy.strPuntoAtencion = controls.cboPuntoVenta.val();
                                    listaAcciones.objParametersGenerateContancy.strTitular = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strRepresentante = controls.txtNombre.val() + ' ' + controls.txtApellido.val();
                                    listaAcciones.objParametersGenerateContancy.strTipoDoc = TipoDocumento;
                                    listaAcciones.objParametersGenerateContancy.strFechaAct = "";
                                    listaAcciones.objParametersGenerateContancy.strNroCaso = "";
                                    listaAcciones.objParametersGenerateContancy.strNroServicio = controls.txtLinea.val();
                                    listaAcciones.objParametersGenerateContancy.strNroDoc = NumeroDocumento;
                                    listaAcciones.objParametersGenerateContancy.strEmail = controls.txtCorreoelectronico.val();
                                    listaAcciones.objParametersGenerateContancy.strAccionBodyEmail = GetKeyConfig("strAccionBodyEmailSuscripcionPelicula");

                                    that.dataListClaroVideo.listaAcciones.push(listaAcciones);
                                    that.dataListClaroVideo.listaProcesosConstancias.push(listaAcciones);
                                }
                            }

                            that.StrMensajeConstancia.MensajeError = '';
                            that.StrMensajeHPXtream.MensajeError = '';
                            that.StrMensajePersonalizado.MensajeError = '';

                            that.ControlError.StrFlagOKSuscripcion = false;
                            that.ControlError.StrFlagOKCancelacion = false;
                            that.ControlError.StrFlagOKAcciones = false;
                            that.ControlError.StrFlagFailedSuscripcion = false;
                            that.ControlError.StrFlagFailedCancelacion = false;
                            that.ControlError.StrFlagFailedAcciones = false;
                            that.ControlError.StrFlagFailedDetalleInteraccion = false;
                            var MensajeFailedInteraccion = '';

                            that.GenerarRegistroUsuario(function (flagOK_RegistroUsuario) {
                                console.log('flagOK_RegistroUsuario');

                                if (flagOK_RegistroUsuario) {

                                    $.each(that.dataListClaroVideo.listaAcciones, function (index, value) {

                                        if (value.IsProcesadoHPXTREAM == "0") { // SI NO ESTA PROCESADO

                                            that.GenerarSuscripcion(value.GenerarSuscripcion, value.ListSuscriptcion, function (flagOK_GenerarSuscripcion) {

                                                if ((flagOK_GenerarSuscripcion == true && value.GenerarSuscripcion == "1") || (flagOK_GenerarSuscripcion == false && value.GenerarSuscripcion == "0")) {

                                                    that.GenerarSuscripcionAdicionales(value.GenerarSuscripcionAdicionales, value.ListSuscriptcionAdicionales, function (flagOK_GenerarSuscripcionAdicionales) {

                                                        if ((flagOK_GenerarSuscripcionAdicionales == true && value.GenerarSuscripcionAdicionales == "1") || (flagOK_GenerarSuscripcionAdicionales == false && value.GenerarSuscripcionAdicionales == "0")) {

                                                            that.GenerarSuscripcionRentas(value.GenerarSuscripcionRentas, value.ListSuscriptcionRentas, function (flagOK_GenerarSuscripcionRentail) {

                                                                if ((flagOK_GenerarSuscripcionRentail == true && value.GenerarSuscripcionRentas == "1") || (flagOK_GenerarSuscripcionRentail == false && value.GenerarSuscripcionRentas == "0")) {

                                                                    that.GenerarCancelacionSuscripcion(value.GenerarCancelarSuscripcion, value.ListService, function (flagOK_GenerarCancelarSuscripcion) {

                                                                        if ((flagOK_GenerarCancelarSuscripcion == true && value.GenerarCancelarSuscripcion == "1") || (flagOK_GenerarCancelarSuscripcion == false && value.GenerarCancelarSuscripcion == "0")) {

                                                                            that.GenerarActualizacionEmail(value.GenerarActualizacionEmail, function (flagOK_GenerarActualizacionEmail) {

                                                                                if ((flagOK_GenerarActualizacionEmail == true && value.GenerarActualizacionEmail == "1") || (flagOK_GenerarActualizacionEmail == false && value.GenerarActualizacionEmail == "0")) {

                                                                                    that.GenerarActualizacionPassword(value.GenerarActualizacionPassword, function (flagOK_GenerarActualizacionPassword) {

                                                                                        if ((flagOK_GenerarActualizacionPassword == true && value.GenerarActualizacionPassword == "1") || (flagOK_GenerarActualizacionPassword == false && value.GenerarActualizacionPassword == "0")) {

                                                                                            that.GenerarDesvinculacionDispositivo(value.GenerarActualizacionDispositivo, value.ListDevice, function (flagOK_GenerarActualizacionDispositivo) {

                                                                                                if ((flagOK_GenerarActualizacionDispositivo == true && value.GenerarActualizacionDispositivo == "1") || (flagOK_GenerarActualizacionDispositivo == false && value.GenerarActualizacionDispositivo == "0")) {

                                                                                                    var IdInteraccion = that.GenerarInteraccion(value.Metodo); //CodigoInteraccion;  PARA PRUEBA

                                                                                                    if (IdInteraccion != "0" && IdInteraccion != "" && IdInteraccion != null) {

                                                                                                        value.objParametersGenerateContancy.strNroCaso = IdInteraccion;

                                                                                                        if (value.IsGeneraConstancia == "1") {

                                                                                                            that.GenerarProcesoClaroVideo(value.objParametersGenerateContancy, function () {
                                                                                                                console.log('Fin del proceso');
                                                                                                            });

                                                                                                        } else {

                                                                                                            that.GenerarInteraccionPlus(value.objParametersGenerateContancy, "");
                                                                                                        }

                                                                                                    } else {
                                                                                                        MensajeFailedInteraccion = GetKeyConfig("StrFlagFailedInteraccion");
                                                                                                        console.log('No se generó la interacción: ' + value.Metodo);
                                                                                                    }
                                                                                                } else {
                                                                                                    console.log('No se genero la desvinculacion del dispositivo');
                                                                                                }

                                                                                            });


                                                                                        } else {
                                                                                            console.log('No se genero la actualizacion de la contraseña');
                                                                                        }
                                                                                    });

                                                                                } else {
                                                                                    console.log('No se genero la actualizacion del email');
                                                                                }

                                                                            });

                                                                        } else {
                                                                            console.log('No se genero la cancelacion de la suscripcion');
                                                                        }
                                                                    });


                                                                } else {
                                                                    console.log('No se genero suscripcion de alquiler');
                                                                }

                                                            });


                                                        } else {
                                                            console.log('No se genero suscripcion de servicios adicionales');
                                                        }

                                                    });

                                                } else {
                                                    console.log('No se genero suscripcion');
                                                }

                                            });


                                        }

                                    });



                                    var MensajeFront = '';
                                    var MensajeAccion = '';
                                    var MensajeFailed = '';
                                    var MensajeDetalleInteraccion = '';

                                    if (that.ControlError.StrFlagOKAcciones == true) {
                                        MensajeAccion = GetKeyConfig("StrFlagOKAcciones");
                                    } else {

                                        if (that.ControlError.StrFlagOKSuscripcion == true && that.ControlError.StrFlagOKCancelacion == false) {
                                            MensajeAccion = GetKeyConfig("StrFlagOKSuscripcion");
                                        }
                                        if (that.ControlError.StrFlagOKSuscripcion == false && that.ControlError.StrFlagOKCancelacion == true) {
                                            MensajeAccion = GetKeyConfig("StrFlagOKCancelacion");
                                        }
                                        if (that.ControlError.StrFlagOKSuscripcion == true && that.ControlError.StrFlagOKCancelacion == true) {
                                            MensajeAccion = GetKeyConfig("StrFlagOKSuscripcionCancelacion");
                                        }
                                    }

                                    if (that.ControlError.StrFlagFailedSuscripcion == true || that.ControlError.StrFlagFailedCancelacion == true || that.ControlError.StrFlagFailedAcciones == true) {
                                        if (that.StrMensajePersonalizado.MensajeError != '') {
                                            MensajeFailed = that.StrMensajePersonalizado.MensajeError;
                                        } else {
                                            MensajeFailed = GetKeyConfig("StrFlagFailed");
                                        }
                                    }

                                    if (that.ControlError.StrFlagFailedDetalleInteraccion == true) {
                                        MensajeDetalleInteraccion = GetKeyConfig("StrFlagFailedDetalleInteraccion");
                                    }

                                    if (that.StrMensajeConstancia.MensajeError != '' || that.StrMensajeHPXtream.MensajeError != '') {

                                        if (that.StrMensajeConstancia.MensajeError != '' && that.StrMensajeHPXtream.MensajeError != '') {
                                            MensajeFront = ', ' + that.StrMensajeHPXtream.MensajeError + '; ' + that.StrMensajeConstancia.MensajeError;
                                        }
                                        if (that.StrMensajeConstancia.MensajeError != '' && that.StrMensajeHPXtream.MensajeError == '') {
                                            MensajeFront = ', ' + that.StrMensajeConstancia.MensajeError;
                                        }
                                        if (that.StrMensajeConstancia.MensajeError == '' && that.StrMensajeHPXtream.MensajeError != '') {
                                            MensajeFront = ', ' + that.StrMensajeHPXtream.MensajeError;
                                        }

                                    }


                                    alert(MensajeAccion + (MensajeAccion != '' && MensajeFailed != '' ? ', ' + MensajeFailed : MensajeFailed) + '' + MensajeFront + ((MensajeAccion != '' || MensajeFailed != '' || MensajeFront != '') && MensajeFailedInteraccion != '' ? ', ' + MensajeFailedInteraccion : MensajeFailedInteraccion) + ((MensajeAccion != '' || MensajeFailed != '' || MensajeFront != '' || MensajeFailedInteraccion != '') && MensajeDetalleInteraccion != '' ? ', ' + MensajeDetalleInteraccion : MensajeDetalleInteraccion));

                                    hideLoading();


                                    if ((that.ControlError.StrFlagOKSuscripcion == false && that.ControlError.StrFlagOKCancelacion == false && that.ControlError.StrFlagOKAcciones == false) && (that.ControlError.StrFlagFailedSuscripcion == true || that.ControlError.StrFlagFailedCancelacion == true || that.ControlError.StrFlagFailedAcciones == true)) {
                                        // no refresca la informacion
                                    }
                                    else {

                                        that.ClearList();
                                        that.render();

                                        that.populateGridDeviceUser(null);
                                        $('#accordionExample1').attr('style', 'display:none');
                                        $(controls.chkDesvincular).prop('checked', false);
                                        that.TabSuscripcion_click();

                                        var strFlagDisabledSave = GetKeyConfig("strFlagDisabledSave");
                                        if (strFlagDisabledSave == '1') {
                                            controls.btnSave.prop("disabled", true);
                                        }
                                    }
                                }
                                else {

                                    var MensajeFailedPersonalizado = '';

                                    if (that.StrMensajePersonalizado.MensajeError != '') {
                                        MensajeFailedPersonalizado = that.StrMensajePersonalizado.MensajeError;
                                    } else {
                                        MensajeFailedPersonalizado = GetKeyConfig("StrFlagFailed");
                                    }

                                    alert(MensajeFailedPersonalizado);
                                    hideLoading();
                                }

                            });

                        }

                    });

                }
            });
        },
        GenerarSuscripcion: function (isGenerarSuscripcion, ListSuscriptcion, callback) {

            var that = this;


            if (isGenerarSuscripcion == '1') {

                var VarSoles = GetKeyConfig("strKeySoles");

                var CountSuscripcion = 0;
                var CantidadProcesados = 0;
                var cantOK = 0;

                if (ListSuscriptcion.length > 0) {
                    CountSuscripcion = ListSuscriptcion[0].length
                }

                var DeviceProcesado = 0;

                if (ListSuscriptcion != null && ListSuscriptcion != undefined) {

                    if (CountSuscripcion > 0) {

                        var Proceso = GetKeyConfig("strNomTransaSuscripcion");

                        $.each(ListSuscriptcion[0], function (index, value) {

                            that.ProcesoSuscripcion(value, function (Flag_CantidadProcesados) {

                                CantidadProcesados = CantidadProcesados + 1;
                                cantOK = cantOK + Flag_CantidadProcesados;

                                if (Flag_CantidadProcesados > 0) {

                                    that.ControlError.StrFlagOKSuscripcion = true;

                                    $.grep(that.dataListClaroVideo.listaAcciones, function (data) {
                                        if (data.Metodo == Proceso) {
                                            $.grep(data.ListSuscriptcion, function (dataListSuscriptcion) {
                                                for (var i = 0; i < dataListSuscriptcion.length; i++) {

                                                    if (dataListSuscriptcion[i].idSubscription == value.idSubscription && dataListSuscriptcion[i].flagProcesado == "0") {
                                                        dataListSuscriptcion[i].flagProcesado = "1";

                                                        var ListSuscripcion = {
                                                            idSubscription: "",
                                                            descripcion: "",
                                                            fechaAlta: "",
                                                            precio: "",
                                                            estadoPago: "",
                                                            strSuscFechReg: ""
                                                        }

                                                        var fecha = new Date();
                                                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();

                                                        console.log('FechaRegistro de la suscripcion')
                                                        console.log(FechaRegistro);

                                                        ListSuscripcion.idSubscription = value.idSubscription;
                                                        ListSuscripcion.strSuscTitulo = value.descripcion;
                                                        ListSuscripcion.strSuscPeriodo = value.promocion;
                                                        ListSuscripcion.strSuscPrecio = (VarSoles + '' + value.precio);
                                                        ListSuscripcion.strSuscEstado = value.estado;
                                                        ListSuscripcion.strSuscServicio = value.origen;
                                                        ListSuscripcion.strSuscFechReg = FechaRegistro;
                                                        ListSuscripcion.strProductID = value.ProductID;

                                                        data.objParametersGenerateContancy.ListSuscriptcion.push(ListSuscripcion);

                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    that.ControlError.StrFlagFailedSuscripcion = true;
                                }


                                if (CantidadProcesados == CountSuscripcion)
                                    if (cantOK > 0) {
                                        callback(true);
                                    } else {
                                        callback(false);
                                    }
                            });

                        });

                    } else {
                        callback(false);
                    }
                } else {
                    callback(false);
                }


            } else {
                callback(false);
            }

        },
        GenerarSuscripcionAdicionales: function (isGenerarSuscripcion, ListSuscriptcion, callback) {

            var that = this,
                controls = that.getControls();

            var VarSoles = GetKeyConfig("strKeySoles");

            if (isGenerarSuscripcion == '1') {

                var CountSuscripcionAdicionales = 0;
                var CantidadProcesados = 0;
                var cantOK = 0;

                if (ListSuscriptcion.length > 0) {
                    CountSuscripcionAdicionales = ListSuscriptcion[0].length
                }

                if (ListSuscriptcion != null && ListSuscriptcion != undefined) {

                    if (CountSuscripcionAdicionales > 0) {

                        var Proceso = GetKeyConfig("strNomTransaSusAdicional");

                        $.each(ListSuscriptcion[0], function (index, value) {

                            that.ProcesoSuscripcion(value, function (Flag_CantidadProcesados) {

                                CantidadProcesados = CantidadProcesados + 1;
                                cantOK = cantOK + Flag_CantidadProcesados;

                                if (Flag_CantidadProcesados > 0) {

                                    that.ControlError.StrFlagOKSuscripcion = true;

                                    $.grep(that.dataListClaroVideo.listaAcciones, function (data) {

                                        if (data.Metodo == Proceso) {

                                            $.grep(data.ListSuscriptcionAdicionales, function (dataListSuscriptcionAdicionales) {

                                                for (var i = 0; i < dataListSuscriptcionAdicionales.length; i++) {

                                                    if (dataListSuscriptcionAdicionales[i].idSubscription == value.idSubscription && dataListSuscriptcionAdicionales[i].flagProcesado == "0") {
                                                        dataListSuscriptcionAdicionales[i].flagProcesado = "1";

                                                        var fecha = new Date();
                                                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();

                                                        var ListSuscriptcionAdicionales = {
                                                            idSubscription: "",
                                                            strSuscTitulo: "",
                                                            strSuscPeriodo: "",
                                                            strSuscPrecio: "",
                                                            strSuscEstado: "",
                                                            strSuscServicio: "",
                                                            strSuscFechReg: ""
                                                        }

                                                        ListSuscriptcionAdicionales.idSubscription = value.idSubscription;
                                                        ListSuscriptcionAdicionales.strSuscTitulo = value.descripcion;
                                                        ListSuscriptcionAdicionales.strSuscPeriodo = value.promocion;
                                                        ListSuscriptcionAdicionales.strSuscPrecio = (VarSoles + '' + value.precio);
                                                        ListSuscriptcionAdicionales.strSuscEstado = value.estado;
                                                        ListSuscriptcionAdicionales.strSuscServicio = value.origen;
                                                        ListSuscriptcionAdicionales.strSuscFechReg = FechaRegistro;
                                                        ListSuscriptcionAdicionales.strProductID = value.ProductID;
                                                        data.objParametersGenerateContancy.ListSuscriptcionAdicionales.push(ListSuscriptcionAdicionales);

                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    that.ControlError.StrFlagFailedSuscripcion = true;
                                }

                                if (CantidadProcesados == CountSuscripcionAdicionales) {
                                    if (cantOK > 0) {
                                        callback(true);
                                    } else {
                                        callback(false);
                                    }
                                }

                            });
                        });
                    } else {
                        callback(false);
                    }
                } else {
                    callback(false);
                }

            } else {
                callback(false);
            }

        },
        GenerarSuscripcionRentas: function (isGenerarSuscripcion, ListSuscriptcion, callback) {

            var that = this;

            if (isGenerarSuscripcion == '1') {

                var CountSuscripcion = 0;
                var CantidadProcesados = 0;
                var cantOK = 0;

                if (ListSuscriptcion.length > 0) {
                    CountSuscripcion = ListSuscriptcion[0].length
                }

                var DeviceProcesado = 0;

                if (ListSuscriptcion != null && ListSuscriptcion != undefined) {

                    if (CountSuscripcion > 0) {

                        $.each(ListSuscriptcion[0], function (index, value) {

                            that.ProcesoSuscripcionRenta(value, function (Flag_CantidadProcesados) {

                                CantidadProcesados = CantidadProcesados + 1;
                                cantOK = cantOK + Flag_CantidadProcesados;

                                if (Flag_CantidadProcesados > 0) {

                                    that.ControlError.StrFlagOKSuscripcion = true;

                                    $.grep(that.dataListClaroVideo.listaAcciones, function (data) {
                                        if (data.Metodo == "CONSTANCIA_SUSCRIPCION_RENTAS_CLARO_VIDEO") {
                                            $.grep(data.ListSuscriptcionRentas, function (dataListSuscriptcion) {
                                                for (var i = 0; i < dataListSuscriptcion.length; i++) {

                                                    if (dataListSuscriptcion[i].strRentailID == value.strRentailID && dataListSuscriptcion[i].flagProcesado == "0") {
                                                        dataListSuscriptcion[i].flagProcesado = "1";

                                                        var ListSuscripcionRentas = {
                                                            strRentailID: "",
                                                            strRentailNom: "",
                                                            strSuscFechReg: "",
                                                            strSuscPrecio: "",
                                                            strSuscEstado: "",
                                                            strSuscServicio: ""
                                                        }

                                                        var fecha = new Date();
                                                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();

                                                        console.log('FechaRegistro de la renta')
                                                        console.log(FechaRegistro);

                                                        ListSuscripcionRentas.strRentailID = value.strRentailID;
                                                        ListSuscripcionRentas.strRentailNom = value.strRentailNom;
                                                        ListSuscripcionRentas.strSuscFechReg = FechaRegistro
                                                        ListSuscripcionRentas.strSuscPrecio = value.strRentaPrecio;
                                                        ListSuscripcionRentas.strSuscEstado = GetKeyConfig("strSuscEstadoRentaPelicula");
                                                        ListSuscripcionRentas.strSuscServicio = GetKeyConfig("strSuscServicioRentaPelicula");
                                                        data.objParametersGenerateContancy.ListSuscriptcionRentas.push(ListSuscripcionRentas);

                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    that.ControlError.StrFlagFailedSuscripcion = true;
                                }

                                // valida si se ha realizado al menos 1 suscripcion
                                if (CantidadProcesados == CountSuscripcion) {
                                    if (cantOK > 0) {
                                        callback(true);
                                    } else {
                                        callback(false);
                                    }
                                }

                            });

                        });

                    } else {
                        callback(false);
                    }
                } else {
                    callback(false);
                }

            } else {
                callback(false);
            }

        },
        GenerarCancelacionSuscripcion: function (isCancelarSuscripcion, ListService, callback) {

            var that = this;
            if (isCancelarSuscripcion == '1') {

                var CountCancelSuscripcion = 0;
                var CantidadProcesados = 0;
                var cantOK = 0;

                if (ListService.length > 0) {
                    CountCancelSuscripcion = ListService[0].length
                }

                var DeviceProcesado = 0;

                if (ListService != null && ListService != undefined) {

                    if (CountCancelSuscripcion > 0) {

                        var Proceso = GetKeyConfig("strNomTransaSusCancelacion");

                        $.each(ListService[0], function (index, value) {

                            that.ProcesoCancelacionSuscripcion(value, function (Flag_CantidadProcesados) {

                                CantidadProcesados = CantidadProcesados + 1;
                                cantOK = cantOK + Flag_CantidadProcesados;
                                if (Flag_CantidadProcesados > 0) {

                                    that.ControlError.StrFlagOKCancelacion = true;

                                    $.grep(that.dataListClaroVideo.listaAcciones, function (data) {
                                        if (data.Metodo == Proceso) {
                                            $.grep(data.ListService, function (dataListService) {
                                                for (var i = 0; i < dataListService.length; i++) {

                                                    if (dataListService[i].idSubscription == value.idSubscription) {
                                                        dataListService[i].flagProcesado = "1";

                                                        var fecha = new Date();
                                                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();

                                                        var ListService = {
                                                            idSubscription: "",
                                                            strBajaServicios: "",
                                                            strSuscPeriodo: "",
                                                            strSuscPrecio: "",
                                                            strSuscEstado: "",
                                                            strSuscServicio: "",
                                                            strSuscFechReg: "",
                                                            strProductID: ""
                                                        }

                                                        ListService.idSubscription = value.idSubscription;
                                                        ListService.strBajaServicios = value.descripcion;
                                                        ListService.strSuscPeriodo = value.promocion;
                                                        ListService.strSuscPrecio = value.precio;
                                                        ListService.strSuscEstado = value.estado;
                                                        ListService.strSuscServicio = value.origen;
                                                        ListService.strSuscFechReg = FechaRegistro;
                                                        ListService.strProductID = value.ProductID;

                                                        data.objParametersGenerateContancy.ListService.push(ListService);

                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    that.ControlError.StrFlagFailedCancelacion = true;
                                }

                                // valida si se ha realizado al menos 1 suscripcion
                                if (CantidadProcesados == CountCancelSuscripcion) {
                                    if (cantOK > 0) {

                                        callback(true);
                                    } else {
                                        callback(false);
                                    }
                                }
                            });

                        });
                    } else {
                        callback(false);
                    }
                } else {
                    callback(false);
                }

            } else {
                callback(false);
            }

        },
        ProcesoCancelacionSuscripcion: function (data, callback) {

            var that = this,
                  controls = that.getControls();

            var Linea = that.ObtenerLineaCliente('Linea');
            var operatorUserID = that.ObtenerLineaCliente('operatorUserID');

            var StrPartnerID = GetKeyConfig("strPartnerIDcancelarSuscripcionSN");

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var varcancelarSuscripcionSNRequest = {
                cancelAccountRequest: {
                    partnerID: StrPartnerID,
                    productID: data.ProductID,
                    level: '1',
                    operatorUser: {
                        operatorUserID: Linea,
                        providerUserID: that.CustomerClaroVideo,
                        subProductID: " ",
                        description: data.descripcion
                    },
                    countryID: "PER",
                    extensionInfo: [
           {
               key: "CUSTOMERID",
               value: that.CustomerClaroVideo
           },
           {
               key: "EXECUTIONTYPE",
               value: "3"
           },
           {
               key: "CONTENTID",
               value: "2"
           },
           {
               key: "region",
               value: "PE"
           },
           {
               key: "user_type",
               value: GetKeyConfig("strUsertype")
           },
           {
               key: "payment_method",
               value: that.strPaymentMethod
           },
          {
              key: "operatorUserID",
              value: operatorUserID
          }

                    ]

                }
            };

            var oCancelSubscriptionSNRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { cancelarSuscripcionSNRequest: varcancelarSuscripcionSNRequest }
                }
            }

            console.log('Request CancelSubscriptionSN');
            console.log(oCancelSubscriptionSNRequest);


            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Fixed/ClaroVideo/CancelSubscriptionSN',
                data: JSON.stringify(oCancelSubscriptionSNRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    //falta implementar si existe o no
                    console.log('Response CancelSubscriptionSN');
                    console.log(response);

                    if (response.data.cancelAccountResponse != null) {
                        if (response.data.cancelAccountResponse.result != null) {
                            if (response.data.cancelAccountResponse.result.resultCode == "0") {
                                console.log('exito');

                                if (response.Transaccion != null) {

                                    var dataControl = {
                                        transaccionId: response.Transaccion,
                                        tipoTransaccion: GetKeyConfig("strControltipoTransaccionCancelacion"),
                                        operacionSuscripcion: GetKeyConfig("strControlOperacionCancelacion") + data.descripcion,
                                        nombreServicio: data.descripcion, // poner variable del servicio
                                        nombrePdv: controls.cboPuntoVenta.val(),
                                        custormerId: that.CustomerClaroVideo,
                                        linea: Linea,
                                        estadoTransaccion: 'Exito',
                                        mensajeTransaccion: response.data.cancelAccountResponse.result.resultMessage
                                    };

                                    // llamada al servicio de controles.
                                    that.RegisterControlsClaroVideo(dataControl, function (flag) {

                                        if (flag) {
                                            console.log('se registro el control');
                                        } else {
                                            console.log('No se registro el control');
                                        }

                                        callback(1);
                                    })
                                }

                            } else if (response.data.cancelAccountResponse.result.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                var Mensaje = response.data.cancelAccountResponse.result.resultMessage;

                                that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                    if (flag != '') {
                                        if (that.StrMensajePersonalizado.MensajeError != '') {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                        } else {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                        }
                                    }

                                    callback(0);

                                });
                            }
                            else {
                                callback(0);
                            }
                        } else {
                            callback(0);
                        }
                    } else {
                        callback(0);
                    }
                },
                error: function (msger) {
                    callback(0);
                }
            });
        },
        ObtenerLineaCliente: function (key) {
            var that = this,
                controls = that.getControls();

            var oCustomer = null;
            var Linea = '';

            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                if (oCustomer.Application != undefined) {


                    if (oCustomer.Application == 'PREPAID' || oCustomer.Application == 'POSTPAID') {

                        if (key == 'operatorUserID') {

                            Linea = '51' + controls.txtLinea.val().trim();

                        } else {

                            Linea = controls.txtLinea.val().trim();

                            if (Linea == null || Linea == '') {
                                Linea = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                                      (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                                      : oCustomer.Telephone);
                            }
                        }

                    } else {
                        //ContractID

                        var strTokenFija = GetKeyConfig("strTokenFija");

                        if (key == 'operatorUserID') {

                            if (oCustomer.CustomerID != undefined) {
                                Linea = oCustomer.CustomerID + strTokenFija;
                            } else {
                                console.log('CustomerID es undefined');
                            }

                        } else if (key == 'ContractID') {

                            if (oCustomer.ContractID != undefined) {
                                Linea = oCustomer.ContractID;
                            } else {
                                console.log('ContractID es undefined');
                            }

                        } else if (key == 'CustomerID') {

                            if (oCustomer.CustomerID != undefined) {
                                Linea = oCustomer.CustomerID;
                            } else {
                                console.log('CustomerID es undefined');
                            }

                        } else if (key == 'Linea') {

                            if (oCustomer.CustomerID != undefined) {
                                Linea = oCustomer.CustomerID + strTokenFija;
                            } else {
                                console.log('CustomerID es undefined');
                            }
                        }

                    }
                } else {
                    console.log('Aplication no definido');
                }
            }

            return Linea;
        },
        GenerarRegistroUsuario: function (callback) {

            var that = this,
                controls = that.getControls();

            var flagIsClientClarovideo = that.flagIsClientClarovideo;

            if (!flagIsClientClarovideo) {

                var StrPartnerID = GetKeyConfig("strPartnerIDregistrarClienteSN");
                var CorrelatorId = that.GeneratedCorrelatorId();
                var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;
                var Email = controls.txtCorreoelectronico.val().trim();
                var Nombre = controls.txtNombre.val().trim();
                var Apellido = controls.txtApellido.val().trim();

                var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
                var IdPersonalizado = that.GeneratedCorrelatorId();
                var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;


                var varcreateUserOttRequest = {

                    invokeMethod: 'registrarusuario',
                    countryId: 'PE',
                    employeeId: GetKeyConfig("strEmployeeId"),
                    correlatorId: providerCorrelatorId,
                    origin: 'SIAC',
                    name: Nombre,
                    lastName: Apellido,
                    email: Email,
                    motherLastName: 'Prueba',
                    serviceName: 'registrarClienteSN',
                    providerId: StrPartnerID,
                    iccidManager: 'AMCO',
                    extensionInfo: [
                      { key: "1", value: "1" }
                    ]
                };

                var objcreateUserOttRequest = {
                    strIdSession: Session.IDSESSION,
                    MessageRequest: {
                        Body: { createUserOttRequest: varcreateUserOttRequest }
                    }
                };
                console.log('Request RegisterClientSN');
                console.log(JSON.stringify(objcreateUserOttRequest));
                // controls.btnbuscar.button('loading');
                $.app.ajax({
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Fixed/ClaroVideo/RegisterClientSN',
                    data: JSON.stringify(objcreateUserOttRequest),
                    complete: function () {
                        //controls.btnbuscar.button('reset');
                    },
                    success: function (response) {

                        //falta implementar si existe o no
                        console.log('Response RegisterClientSN');
                        console.log(response.data);

                        if (response.data.createUserOttResponse != null) {
                            if (response.data.createUserOttResponse.resultCode == "0") {
                                // se activa el flag de cliente claro video // validar
                                if (response.data.createUserOttResponse.CUSTOMERID != null && response.data.createUserOttResponse.CUSTOMERID != undefined) {

                                    that.CustomerClaroVideo = response.data.createUserOttResponse.CUSTOMERID;
                                    that.flagIsClientClarovideo = true;
                                    callback(true);
                                    console.log('RegisterClientSN exito');

                                } else {

                                    callback(false);
                                }

                            } else if (response.data.createUserOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                var Mensaje = response.data.createUserOttResponse.resultMessage;

                                that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                    if (flag != '') {
                                        if (that.StrMensajePersonalizado.MensajeError != '') {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                        } else {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                        }
                                    }

                                    callback(false);

                                });
                            } else {
                                callback(false);
                            }
                        } else {
                            callback(false);
                        }


                    },
                    error: function (msger) {
                        callback(false);
                    }
                });

            } else {
                callback(true);
            }


        },
        ConstanciaClaroVideo: function (objParametersContancy, callback) {

            var that = this, controls = this.getControls();
            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
            var dateEnd = "", dateIni = "";
            var codeOnBase = '0';

            console.log('Request Constancia - ConstanciaClaroVideo');
            console.log(objParametersContancy);

            that.strRutaPDF = '';

            var objReq = {
                strIdSession: Session.IDSESSION,
                objParametersGenerateContancy: objParametersContancy
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Fixed/ClaroVideo/GetGenerateContancy',
                data: JSON.stringify(objReq),
                success: function (response) {
                    console.log('Response Constancia - ConstanciaClaroVideo');
                    console.log(response);

                    if (response.codeResponse == "0" && response.Constancia.Generated == true) {

                        that.strRutaPDF = response.Constancia.FullPathPDF;
                        var strTipoConstancia = response.TipoConstancia;

                        $.grep(that.dataListClaroVideo.listaAcciones, function (data) {
                            //console.log(data.TipoConstancia);
                            //console.log(strTipoConstancia);
                            if (data.TipoConstancia == strTipoConstancia) {
                                data.Path = response.Constancia.FullPathPDF;
                                data.IsProcesadoHPXTREAM = "1";
                                return true;
                            }
                        });

                        $.grep(that.dataListClaroVideo.listaProcesosConstancias, function (data) {
                            //console.log(data.TipoConstancia);
                            //console.log(strTipoConstancia);
                            if (data.TipoConstancia == strTipoConstancia) {
                                data.Path = response.Constancia.FullPathPDF;
                                data.IsProcesadoHPXTREAM = "1";
                                return true;
                            }
                        });


                        console.log('CONSTANCIAS GENERADAS');
                        console.log(that.dataListClaroVideo.listaAcciones);
                        console.log(that.dataListClaroVideo.listaProcesosConstancias);


                        objParametersContancy.strIdSession = Session.IDSESSION;
                        objParametersContancy.FullPathPDF = response.Constancia.FullPathPDF;
                        objParametersContancy.Document = response.Constancia.Document;


                        that.ConstanciaClaroVideoOnBase(objParametersContancy, function (CodigoOnbase) {

                            codeOnBase = CodigoOnbase;
                            console.log('ConstanciaClaroVideoOnBase');
                            console.log('Codigo Onbase Generado:' + codeOnBase)
                        });


                        controls.btnConstancia.prop("disabled", false);
                        console.log(response);

                        callback(codeOnBase);
                    } else {
                        that.StrMensajeHPXtream.MensajeError = 'no se genero constancia hpxtream';
                        callback(codeOnBase);
                    }


                }, error: function (msger) {
                    that.StrMensajeHPXtream.MensajeError = 'no se genero constancia hpxtream';
                    console.log(msger);
                    callback(codeOnBase);
                }
            });

        },
        ControlError: {
            StrFlagOKSuscripcion: false,
            StrFlagOKCancelacion: false,
            StrFlagOKAcciones: false,
            StrFlagFailedSuscripcion: false,
            StrFlagFailedCancelacion: false,
            StrFlagFailedAcciones: false,
            StrFlagFailedDetalleInteraccion: false
        },
        StrMensajeConstancia: {
            MensajeError: "",
        },
        StrMensajeHPXtream: {
            MensajeError: "",
        },
        StrMensajePersonalizado: {
            MensajeError: "",
        },
        ConstanciaClaroVideoOnBase: function (objParametersContancy, callback) {

            var that = this, controls = that.getControls();


            console.log("ConstanciaClaroVideoOnBase");
            var strKeyName = objParametersContancy != null ? (objParametersContancy.strKeyName != null ? objParametersContancy.strKeyName : objParametersContancy.strKeyName) : "";
            var strKeyValue = objParametersContancy != null ? (objParametersContancy.strKeyValue != null ? objParametersContancy.strKeyValue : objParametersContancy.strKeyValue) : "";
            var strKeyLen = GetKeyConfig("strClaroVideoMetadatosLength");

            var d = new Date();
            var FechaActualizacion = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1)) + "/" + d.getFullYear();

            var oOnBaseCargaModel = {
                IdSession: objParametersContancy.strIdSession,
                strKeyWorkName: strKeyName,
                strKeyWorkValue: strKeyValue,
                strKeyWorkLeng: strKeyLen,
                FullPathPDF: objParametersContancy.FullPathPDF, //'D:\\CONTANCIA\\100013424_12_12_2019_CONSTANCIA_SUSCRIPCION_CLARO_VIDEO_0.pdf',//objParametersContancy.FullPathPDF,
                FormatoTransaccion: objParametersContancy.StrNombreArchivoTransaccion,
                Document: objParametersContancy.Document, //'100013424_12_12_2019_CONSTANCIA_SUSCRIPCION_CLARO_VIDEO_0',//objParametersContancy.Document,
                Modulo: GetKeyConfig("moduloClaroVideo"),
                CodigoAsesor: SessionTransac.SessionParams.USERACCESS.login,
                Constancia: {
                    /** VALORES DE PRUEBA DE FULL CLARO  **/
                    CodigoCliente: SessionTransac.SessionParams.USERACCESS.login,
                    FORMATO_TRANSACCION: objParametersContancy.StrNombreArchivoTransaccion,
                    FechaActualizacion: FechaActualizacion,
                    NRO_CASO_INTERACCION: objParametersContancy.strNroCaso,
                    CASO_INTER: objParametersContancy.strNroCaso,
                    NumeroServicio: objParametersContancy.strNroServicio,
                    PuntoAtencion: objParametersContancy.strPuntoAtencion,
                    RepresentanteLegal: objParametersContancy.strRepresentante,
                    TipoDocumento: objParametersContancy.strTipoDoc,
                    Titular: objParametersContancy.strTitular,
                    NumeroLinea: objParametersContancy.strNroServicio,
                    NumeroDocumento: objParametersContancy.strNroDoc,
                    Email: objParametersContancy.strEmail,
                    FormatoTransaccion: objParametersContancy.StrNombreArchivoTransaccion
                }
            };

            var strStatusOkOnbase = GetKeyConfig("strStatusOkOnbase");

            var codeOnBase = '0';
            console.log("Request GenerarConstanciaOnBase");
            console.log(oOnBaseCargaModel);
            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/GenerarConstanciaOnBase',
                data: JSON.stringify(oOnBaseCargaModel),
                success: function (response) {
                    console.log('Response GenerarConstanciaOnBase');
                    console.log(response);
                    if (response.codeResponse == "0") {
                        if (response.OnBase != null) {
                            if (response.OnBase.codeResponse == '0') {
                                if (response.OnBase.status == strStatusOkOnbase) {
                                    codeOnBase = response.OnBase.codeOnBase;
                                    controls.btnConstancia.prop("disabled", false);
                                }
                            } else {
                                that.StrMensajeConstancia.MensajeError = "no se genero la constancia OnBase";
                            }
                        } else {
                            that.StrMensajeConstancia.MensajeError = "no se genero la constancia OnBase";
                        }

                    } else {
                        that.StrMensajeConstancia.MensajeError = "no se genero la constancia OnBase";
                    }
                },
                error: function (msger) {
                    that.StrMensajeConstancia.MensajeError = "no se cargo la constancia a OnBase";
                    console.log(msger);
                    callback(codeOnBase);
                }
            });


            callback(codeOnBase);

        },
        btnConstancia_click: function () {
            var that = this;
            $('html, body').animate({ scrollTop: 0 }, 'slow');

            console.log("btnConstancia_click : Lista de constancias:");
            console.log(that.dataListClaroVideo.listaProcesosConstancias);

            $.each(that.dataListClaroVideo.listaProcesosConstancias, function (index, value) {

                if (value.Path != "") {
                    var params = ['height=600',
                                  'width=750',
                                  'resizable=yes',
                                  'location=yes'
                    ].join(',');
                    window.open('/Transactions/Fixed/ClaroVideo/DownloadFileServer' + "?strPath=" + value.Path + "&strIdSession=" + Session.IDSESSION, "_blank", params);
                } else {
                    alert("El archivo no existe");
                }
            });
        },
        ChkDesvincular_change: function () {
            console.log('change desvincular');
            var that = this, controls = that.getControls();

            if ($(controls.chkDesvincular).is(':checked')) {
                $('#accordionExample1').attr('style', 'display:block');
                var loadTable = '';
                loadTable = loadTable + '<tr>';
                loadTable = loadTable + '<td colspan="5" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
                loadTable = loadTable + '</tr>';
                $('#tbDeviceUserBodyList').html(loadTable);
                that.searhDeviceUser();
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', false);
                //INICIATIVA-794
                controls.btnSave.attr('class', 'btn btn-info active');
            } else {
                that.populateGridDeviceUser(null);
                $('#accordionExample1').attr('style', 'display:none');
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', true);
            }
        },
        ViewHistoryDevice: function (sender, args) {
            var that = this,
                controls = that.getControls(),
                type = that.TypeLine,
                phone = that.ObtenerLineaCliente('Linea'); // cambio mejora

            console.log('type' + type);

            $('html, body').animate({ scrollTop: 0 }, 'slow');

            $.window.open({
                modal: true,
                title: "Historial de Dispositivos",
                id: 'divModalHistoDevice',
                url: '/Transactions/Fixed/ClaroVideo/ViewHistoryDevice',
                data: { strIdSession: Session.IDSESSION, strMobile: that.CustomerClaroVideo, strType: type },
                type: 'POST',
                width: '950px',
                height: '900px',
                minimizeBox: false,
                maximizeBox: false,
                buttons: {
                    Cerrar: {
                        id: 'btnCloseHistoryDevice',
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            })
        },
        chkSentEmail_click: function () {
            console.log('click');
            var that = this,
            controls = that.getControls();
            if ($(controls.chkSentEmail).is(':checked')) {
                controls.txtSendforEmail.attr('disabled', false);
                controls.txtSendforEmail.val('');
            } else {
                controls.txtSendforEmail.attr('disabled', true);
                controls.txtSendforEmail.val('');
            }
        },
        chkChangeEmail_click: function () {
            console.log('click');
            var that = this,
            controls = that.getControls();

            if ($(controls.chkChangeEmail).is(':checked')) {
                controls.txtCambiarCorreo.attr('disabled', false);
                controls.txtCambiarCorreo.val('');
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', false);
                //INICIATIVA-794
                controls.btnSave.attr('class', 'btn btn-info active');
            } else {
                controls.txtCambiarCorreo.attr('disabled', true);
                controls.txtCambiarCorreo.val('');
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', true);
            }
        },
        chkChangePass: function () {
            var that = this,
                controls = that.getControls();

            if ($(controls.chkChangePassword).is(':checked')) {

                $('#lblMessagePass').attr('style', 'color: red');
                $(controls.lblMessagePass)[0].innerHTML = GetKeyConfig('strCheckChangePassword');
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', false);
                //INICIATIVA-794
                controls.btnSave.attr('class', 'btn btn-info active');
            }
            else {

                $(controls.lblMessagePass)[0].innerHTML = "";
                //MODIFICADO IPTV 1 PLAY
                controls.btnSave.attr('disabled', true);
            }

        },
        ValidarServiciosAdicionalesFija: function () {

            var that = this;
            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                oCustomer.Telephone = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                 (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                 : oCustomer.Telephone);

                // oCustomer.ContractID = '18039423';
                that.LoadServiceAdicional(oCustomer.ContractID, oCustomer.Application, Session.IDSESSION, oCustomer.Telephone, function (callback) {

                    if (callback != '') {
                        that.OpenServiciosAdicionalesFija(callback);
                    }

                });

            }

        },
        LoadServiceAdicional: function (strIdcontract, strApplication, strIdSession, strTelephone, callback) {
            var that = this,
             oContractServices = {};
            var ListServices = '';
            oContractServices.ContractId = strIdcontract;
            oContractServices.Application = strApplication;
            oContractServices.strIdSession = strIdSession;
            oContractServices.Telephone = strTelephone;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/GetContractServices',
                data: JSON.stringify(oContractServices),
                complete: function () {
                    // that.serviceDescription_click();
                },
                success: function (response) {
                    console.log('GetContractServices')
                    console.log(response);

                    if (response.data.ContractServices != null) {
                        if (response.data.ContractServices.length > 0) {

                            var CodigoGrupoServiciosAdicionalesCable = GetKeyConfig('strGroupPosServicioAdicionalCable');


                            var filter = $.grep(response.data.ContractServices, function (data) {
                                return data.GroupPos == CodigoGrupoServiciosAdicionalesCable
                            });

                            if (filter != undefined) {
                                if (filter.length > 0) {

                                    SessionTransac.ListServiciosAdicionalesFija = filter;

                                    console.log(SessionTransac.ListServiciosAdicionalesFija);

                                    for (var i = 0; i < filter.length; i++) {

                                        ListServices = ListServices + filter[i].ServiceDescription + "\n ";
                                    }

                                }
                            }

                        }
                    }


                    callback(ListServices);


                    //controls.tblDetail.find('tbody').html('');
                    //that.createTableServices(response);
                },
                error: function (msger) {

                    callback(ListServices);

                    //controls.tblDetail.find('tbody').html('');
                    //$.app.error({
                    //    id: 'errorDetailContract',
                    //    message: msger,
                    //    click: function () {
                    //        that.getContractServices();
                    //    }
                    //});
                }
            });
        },
        stridRefSuscripcionHistory: ""
        ,
        LoadSuscripciones: function (callback) {

            var that = this, controls = that.getControls();

            that.dataListClaroVideo.listaSuscripcion = [];

            //showLoading('Cargando...');
            var loadTable = '';
            loadTable = loadTable + '<tr>';
            loadTable = loadTable + '<td colspan="8" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
            loadTable = loadTable + '</tr>';
            $('#tbPackageBodyList').html(loadTable);

            //that.searhListarSuscripcionCliente(function () {
            that.searhListarSuscripcion(function (callbackOK) {

                if (callbackOK) {

                    $('#tbPackageList').find('tbody').html('');

                    $.grep(that.dataListClaroVideo.listaSuscripcion, function (dataCliente) {
                        $.grep(that.dataListClaroVideo.listaSuscripcionCliente, function (data) {

                            //if (data.idSubscription == dataCliente.idSubscription) {
                            if (data.descripcion == dataCliente.descripcion) { // valida que no haya duplicados
                                dataCliente.Metodo = "NoAplica";
                                return true;
                            }
                        });
                    });


                    that.dataListClaroVideo.listaSuscripcion = $.grep(that.dataListClaroVideo.listaSuscripcion, function (data) {
                        return data.Metodo != "NoAplica";
                    });

                    var idRefSuscripcionHistory = "";

                    if (that.dataListClaroVideo.listaSuscripcionCliente != null) {
                        if (that.dataListClaroVideo.listaSuscripcionCliente.length > 0) {

                            var loadTableHistory = '';
                            loadTableHistory = loadTableHistory + '<tr>';
                            loadTableHistory = loadTableHistory + '<td colspan="8" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
                            loadTableHistory = loadTableHistory + '</tr>';
                            $('#tbVisualizationBody').html(loadTableHistory);

                            //INICIATIVA-794
                            flagUnicoIPTV = that.dataListClaroVideo.resultadoValidarIPTV;
                            var resultado = that.dataListClaroVideo.resultadoValidarIPTV;

                            $.each(that.dataListClaroVideo.listaSuscripcionCliente, function (index, value) {

                                var listaSuscripcion = {
                                    Metodo: "",
                                    precio: "",
                                    estadoPago: "",
                                    fechaAlta: "",
                                    origen: "",
                                    idSubscription: "",
                                    descripcion: "",
                                    promocion: "",
                                    accion: "",
                                    ProductID: "",
                                    idRefSuscripcion: "",
                                    fechaExpiracion: "",
                                    FlagVer: ""
                                }

                                listaSuscripcion.Metodo = "listaSuscripcionCliente";


                                //INICIATIVA-794
                                //console.log("FLAG precio " + value.ProductID);
                                //var arrProductId = $("#LstProductId").val();
                                var LstValidacionIPTV = $("#LstValidacionIPTV").val();
                                //if (arrProductId.search(value.ProductID) != -1 && LstValidacionIPTV.search(SessionTransac.SessionParams.DATACUSTOMER.Application) != -1) {
                                //if (LstValidacionIPTV.search(resultado) != -1 && LstValidacionIPTV.search(SessionTransac.SessionParams.DATACUSTOMER.Application) != -1) {
                                if (resultado == '1' && LstValidacionIPTV.search(SessionTransac.SessionParams.DATACUSTOMER.Application) != -1) {
                                    listaSuscripcion.precio = "-";
                                } else {
                                listaSuscripcion.precio = value.precio;
                                }
                                listaSuscripcion.estadoPago = value.estadoPago;
                                listaSuscripcion.fechaAlta = value.fechaAlta;
                                listaSuscripcion.origen = value.origen;
                                listaSuscripcion.idSubscription = value.idSubscription;
                                listaSuscripcion.descripcion = value.descripcion;
                                listaSuscripcion.promocion = "";
                                listaSuscripcion.accion = value.accion;
                                listaSuscripcion.ProductID = value.ProductID;
                                listaSuscripcion.idRefSuscripcion = value.idRefSuscripcion;
                                listaSuscripcion.fechaExpiracion = value.fechaExpiracion;
                                listaSuscripcion.FlagVer = value.FlagVer;

                                that.dataListClaroVideo.listaSuscripcion.push(listaSuscripcion);

                                if (index == 0) {
                                    idRefSuscripcionHistory = idRefSuscripcionHistory + value.idRefSuscripcion;
                                } else {
                                    idRefSuscripcionHistory = idRefSuscripcionHistory + '|' + value.idRefSuscripcion;
                                }

                            });

                            that.stridRefSuscripcionHistory = idRefSuscripcionHistory;

                            console.log('stridRefSuscripcionHistory:' + that.stridRefSuscripcionHistory);
                            that.searhVisualizationClient(); // call visualizaciones

                        } else {
                            that.populateGridVisualizationClient(null);
                        }
                    } else {
                        that.populateGridVisualizationClient(null);
                    }

                    console.log('lista de suscripciones a cargar')
                    console.log(that.dataListClaroVideo.listaSuscripcion);
                    that.populateGridListarPaquete(that.dataListClaroVideo.listaSuscripcion);


                    //INICIATIVA-794
                    flagUnicoIPTV = that.dataListClaroVideo.resultadoValidarIPTV;
                    var resultado = that.dataListClaroVideo.resultadoValidarIPTV;
                       
                    var LstValidacionIPTV = $("#LstValidacionIPTV").val();
                    if (resultado == '1' && LstValidacionIPTV.search(SessionTransac.SessionParams.DATACUSTOMER.Application) != -1)
                    {
                        $("#contienepaqueteiptv").val(1);
                        $("#idConsultaCV").addClass("inactiveLink");
                        $("#TabConsulta").addClass("inactiveLink");
                        $("#divDashboardTabs").addClass("cursor_block_iptv");
                    } else {
                        $("#idConsultaCV").removeClass("inactiveLink");
                        $("#TabConsulta").removeClass("inactiveLink");
                        $("#divDashboardTabs").removeClass("cursor_block_iptv");
                    }

                    //MODIFICADO IPTV 1 PLAY                    
                    if ($("#contienepaqueteiptv").val() == 1) {
                        console.log('entro deshabilitar desvincular');
                        $("#chkDesvincular").prop('disabled', true);
                    } else {
                        $("#chkDesvincular").prop('disabled', false);
                    }
                    callback(that.dataListClaroVideo.listaSuscripcion);
                }
            });
            //});

        },
        ValidaPeriodoPromocional: function () {
            var that = this, controls = that.getControls();

            console.log('**********  INICIO VALIDACION PROMOCIONAL ***************')
            console.log('TMCOD - PROMOCIONAL:' + controls.hdTmcode.val());


            $.each(that.dataListClaroVideo.listaSuscripcion, function (index, value) {
                var code = '';

                if (value.Metodo == "listaSuscripcion") {
                    code = controls.hdTmcode.val();
                }
                if (value.Metodo == "listaSuscripcionCliente") {
                    code = '';
                }

                var phone = '', Type = '', formatoPromocion = '', tmcode = '0';

                phone = that.ObtenerLineaCliente('Linea'); // cambio mejora
                Type = that.TypeLine;

                if (phone == '') {

                    console.log('No se obtuvo: Phone');

                } else if (Type == '') {

                    console.log('No se obtuvo: tipo de linea');
                }

                var arrReq;

                if (code == '') {

                    arrReq = {
                        nombreServicio: value.descripcion,
                        linea: phone,
                        servicioName: "EstadoPagoServicio",
                        tipoLinea: Type
                    };
                } else {
                    tmcode = '1';

                    arrReq = {
                        nombreServicio: value.descripcion,
                        linea: phone,
                        tmcod: code,
                        servicioName: "EstadoPagoServicio",
                        tipoLinea: Type
                    };
                }

                var objNodeRequest = {
                    strIdSession: Session.IDSESSION,
                    flagIsTMCOD: tmcode,
                    MessageRequest: {
                        Body: { historialServDispCVRequest: arrReq }
                    }
                }

                console.log('Request Promocional');
                console.log(objNodeRequest);

                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    //async: false,
                    url: '/Transactions/Fixed/ClaroVideo/HistoryDeviceService',
                    data: JSON.stringify(objNodeRequest),
                    complete: function () {
                        //controls.btnbuscar.button('reset');
                    },
                    success: function (response) {
                        console.log('Response HistoryDeviceService - Lista de promociones');
                        console.log(response.data);

                        if (response.data.historialServDispCVResponse != null) {
                            if (response.data.historialServDispCVResponse.pEstadoPagoServ != null) {
                                that.MessageFormat(response.data.historialServDispCVResponse.pEstadoPagoServ, function (formato) {
                                    formatoPromocion = formato;
                                });
                            } else {
                                formatoPromocion = '';
                            }
                        } else {
                            formatoPromocion = '';
                        }



                        console.log(that.dataListClaroVideo.listaSuscripcion);
                        $.grep(that.dataListClaroVideo.listaSuscripcion, function (data) {

                            if (data.idSubscription == value.idSubscription) {

                                data.promocion = formatoPromocion;

                                var MensajePromocion = '';
                                var Precio = '';
                                var PrecioAccion = '';
                                var PromocionAccion = '';
                                var HtmlAccion = '';
                                var bloquearcheckCancelado = '0';
                                var htmlbloquearcheckCancelado = '';
                                var FlagPromocion = '0';


                                if (formatoPromocion == '') {
                                    MensajePromocion = '<label class="text-primary"> Pago</label>';
                                    Precio = '<p class="text-right"> S/.' + value.precio + '</p>';
                                    PrecioAccion = value.precio;
                                    PromocionAccion = 'Pago';
                                } else {
                                    Precio = '<p class="text-right"> S/.0 </p>';
                                    PrecioAccion = 0;
                                    MensajePromocion = '<button type="button" id=' + "btn" + value.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'MostrarPromocionClaroVideo\',\'' + formatoPromocion + '\')"' + 'class="btn btn-link" title="Ver promocion" <span class="glyphicon glyphicon-check"></span> Promocional </button>';
                                    PromocionAccion = formatoPromocion;
                                    FlagPromocion = '1';
                                }

                                //MODIFICADO IPTV 1 PLAY
                                var _productId = data.ProductID;
                                var _flagServIPTV = false;

                                //INICIATIVA-794
                                for (var i = 0; i < that.listaServicios.length; i++) {
                                    if (_productId !== null) {
                                        if (that.listaServicios[i].SERVV_CODIGO_EXT == _productId) {
                                            _flagServIPTV = true;
                                        }
                                    }
                                }

                                var ColorAccion = '';
                                if (value.accion.trim().toUpperCase() == 'DESACTIVADO') {
                                    //MODIFICADO IPTV 1 PLAY
                                    //INICIATIVA-794
                                    if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                        ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-success active ] btn-sm cursor_block_iptv" style="width:75px">' +
                                            '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                           '</label>';
                                    } else {

                                    ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-success active ] btn-sm" style="width:75px">' +
                                            '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                           '</label>';
                                    }


                                } else {

                                    if (value.estadoPago == 'CANCELADO') {
                                        //MODIFICADO IPTV 1 PLAY
                                        //INICIATIVA-794
                                        if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                            var title = 'Cancelado: Fecha de expiración: ' + value.fechaExpiracion;
                                            ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-default active ] btn-sm cursor_block_iptv" style="width:75px" title="' + title + '" disabled>' +
                                                    '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                                   '</label>';
                                            bloquearcheckCancelado = '1';
                                        } else {

                                        var title = 'Cancelado: Fecha de expiración: ' + value.fechaExpiracion;
                                        ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-default active ] btn-sm" style="width:75px" title="' + title + '" disabled>' +
                                                '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                               '</label>';
                                        bloquearcheckCancelado = '1';
                                        }

                                    } else {
                                        //MODIFICADO IPTV 1 PLAY
                                        //INICIATIVA-794
                                        if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                            ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-secondary active] btn-sm cursor_block_iptv">' +
                                                       '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                                      '</label>';

                                        } else {
                                            ColorAccion = '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-danger active ] btn-sm">' +
                                                       '' + (value.accion.trim().toUpperCase() == 'DESACTIVADO' ? "Activar" : "Desactivar") + '' +
                                                      '</label>';
                                        }
                                    }
                                }

                                var ProductID = "";

                                if (value.ProductID != null && value.ProductID != '') {
                                    ProductID = value.ProductID;
                                }                                

                                if (bloquearcheckCancelado == '1') {
                                    //MODIFICADO IPTV 1 PLAY
                                    if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {

                                        var title = 'Cancelado: Fecha de expiración:' + value.fechaExpiracion;

                                        htmlbloquearcheckCancelado = '<input disabled type="checkbox" name="' + "chk_" + value.idSubscription + '" style="display:none" title="' + title + '" id="' + "chk_" + value.idSubscription + '" onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'loadChangesSuscription\',\'' + value.idSubscription + ',' + value.accion + ',' + value.descripcion + ',' + PrecioAccion + ',' + value.fechaAlta + ',' + value.origen + ',' + PromocionAccion + ',' + ProductID + ',' + FlagPromocion + '\')"  autocomplete="off" disabled/>';
                                    } else {
                                        var title = 'Cancelado: Fecha de expiración:' + value.fechaExpiracion;

                                        htmlbloquearcheckCancelado = '<input type="checkbox" name="' + "chk_" + value.idSubscription + '" style="display:none" title="' + title + '" id="' + "chk_" + value.idSubscription + '" onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'loadChangesSuscription\',\'' + value.idSubscription + ',' + value.accion + ',' + value.descripcion + ',' + PrecioAccion + ',' + value.fechaAlta + ',' + value.origen + ',' + PromocionAccion + ',' + ProductID + ',' + FlagPromocion + '\')"  autocomplete="off" disabled/>';
                                    }
                                } else {
                                    //MODIFICADO IPTV 1 PLAY
                                    if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {

                                        htmlbloquearcheckCancelado = '<input disabled type="checkbox" name="' + "chk_" + value.idSubscription + '" style="display:none" id="' + "chk_" + value.idSubscription + '" onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'loadChangesSuscription\',\'' + value.idSubscription + ',' + value.accion + ',' + value.descripcion + ',' + PrecioAccion + ',' + value.fechaAlta + ',' + value.origen + ',' + PromocionAccion + ',' + ProductID + ',' + FlagPromocion + '\')"  autocomplete="off"/>';
                                    } else {
                                        htmlbloquearcheckCancelado = '<input type="checkbox" name="' + "chk_" + value.idSubscription + '" style="display:none" id="' + "chk_" + value.idSubscription + '" onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'loadChangesSuscription\',\'' + value.idSubscription + ',' + value.accion + ',' + value.descripcion + ',' + PrecioAccion + ',' + value.fechaAlta + ',' + value.origen + ',' + PromocionAccion + ',' + ProductID + ',' + FlagPromocion + '\')"  autocomplete="off"/>';
                                    }

                                }                                

                                //MODIFICADO IPTV 1 PLAY
                                //INICIATIVA-794
                                if (_flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                    HtmlAccion = '<center><div class="[ form-group ]">' + htmlbloquearcheckCancelado +
                                          '<div class="[ btn-group ]">' + ColorAccion +
                                               '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-secondary ] btn-sm cursor_block_iptv">' +
                                               '    <span class="[ glyphicon glyphicon-ok ]"></span>' +
                                               '    <span> </span>' +
                                               '</label>' +
                                           '</div>' +
                                      '</div></center>';
                                } else {
                                    HtmlAccion = '<center><div class="[ form-group ]">' + htmlbloquearcheckCancelado +
                                          '<div class="[ btn-group ]">' + ColorAccion +
                                               '<label for="' + "chk_" + value.idSubscription + '" class="[ btn btn-default ] btn-sm">' +
                                               '    <span class="[ glyphicon glyphicon-ok ]"></span>' +
                                               '    <span> </span>' +
                                               '</label>' +
                                           '</div>' +
                                      '</div></center>';
                                }                                

                                $('#divPromocion' + value.idSubscription).html('');
                                $('#divPromocion' + value.idSubscription).append(MensajePromocion);

                                $('#divPrecio' + value.idSubscription).html('');
                                $('#divPrecio' + value.idSubscription).append(Precio);

                                $('#divAccion' + value.idSubscription).html('');
                                $('#divAccion' + value.idSubscription).append(HtmlAccion);

                            }
                            return true;
                        });

                    },
                    error: function (msger) {
                        console.log('Error: Response HistoryDeviceService - Lista de promociones:' + msger);
                        formatoPromocion = '';
                    }

                });



            });



            console.log('**********  FIN VALIDACION PROMOCIONAL ***************')
            console.log('lista de suscripciones a cargar')
            console.log(that.dataListClaroVideo.listaSuscripcion);


        },
        // [INICIO] - AMCO - Proceso para eliminar cliente SN
        ProcessDeleteClientSN: function (callback) {
            var that = this;
            var controls = that.getControls();
            controls = that.getControls();

            var StrPartnerID = GetKeyConfig("strPartnerIDdeleteClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;
            var strUserId = SessionTransac.SessionParams.USERACCESS.userId;
            var strDeleteEmail = controls.txtEmailAMCO2.val().trim();
            if (strDeleteEmail != "" || strDeleteEmail != undefined) {

                var varDeleteUserOttRequest = {
                    invokeMethod: 'eliminarClienteSN',
                    correlatorId: providerCorrelatorId,
                    countryId: 'PE',
                    paymentMethod: '1',
                    userId: "AMCO",
                    account: strDeleteEmail,
                    employeeId: GetKeyConfig("strEmployeeId"),
                    origin: 'SIAC',
                    serviceName: 'eliminarClienteSN',
                    providerId: StrPartnerID,
                    iccidManager: 'AMCO',
                    extensionInfo: [
                        { key: "CUSTOMERID", value: that.CustomerAmco },
                        { key: "TRANSACTIONLEVELTYPE", value: "3" }
                    ]
                };

                var objDeleteUserOttRequest = {
                    strIdSession: Session.IDSESSION,
                    MessageRequest: {
                        Body: {
                            deleteUserOttRequest: varDeleteUserOttRequest
                        }

                    }

                }
                $.app.ajax({
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Fixed/ClaroVideo/DeleteClientSN',
                    data: JSON.stringify(objDeleteUserOttRequest),
                    complete: function () {

                    },
                    success: function (response) {
                        if (response.data.deleteUserOttResponse != null) {
                            if (response.data.deleteUserOttResponse.resultCode == '0') {
                                var strMsgExitoDeleteClientSN = GetKeyConfig("strMsgExitoDeleteClientSN");
                                if (strMsgExitoDeleteClientSN != '') {
                                    alert(strMsgExitoDeleteClientSN, 'Alert');
                                }
                                callback(true);

                            } else {
                                var strMsgErrorDeleteClientSN = GetKeyConfig("strMsgErrorDeleteClientSN")
                                if (strMsgErrorDeleteClientSN != '') {
                                    alert(strMsgErrorDeleteClientSN, 'Alert');
                                }
                                callback(false);
                            }

                        } else {
                            alert(GetKeyConfig("strMensajeDeError"), 'Alert');
                            callback(false);
                        }
                    }
                });
            } else {
                callback(false);
            }
        },
        // [FIN] - AMCO - Proceso para eliminar cliente SN

        searhListarSuscripcionCliente: function (callback) {

            var that = this,
           controls = this.getControls();
            var FlagExistCliente = false;
            that.dataListClaroVideo.listaSuscripcionCliente = [];

            var Email = controls.txtCorreoelectronico.val().trim();


            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarClienteSN");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });

            var varqueryUserOttRequest = {
                invokeMethod: 'consultardatoscliente',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                startDate: strFechaInicio,
                endDate: strFechaFin,
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                extraData: { data: [{ key: "email", value: Email }] },
                serviceName: 'consultardatoscliente',
                providerId: StrPartnerID,
                iccidManager: 'AMCO'
            };

            var objqueryUserOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryUserOttRequest: varqueryUserOttRequest }
                }
            }

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultClientSN',
                data: JSON.stringify(objqueryUserOttRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {

                    //falta implementar si existe o no
                    console.log('Response ConsultClientSN - ListaSuscripciones del cliente');
                    console.log(response.data);
                    if (response.data.QueryUserOttResponse != null) {
                        if (response.data.QueryUserOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas    
                            if (response.data.QueryUserOttResponse.ListUserSubscription != null) {
                                if (response.data.QueryUserOttResponse.ListUserSubscription.length > 0) {
                                    $.each(response.data.QueryUserOttResponse.ListUserSubscription, function (index, value) {

                                        if (value.activo == "1") { // activo

                                            var listaSuscripcion = {
                                                Metodo: "",
                                                precio: "",
                                                estadoPago: "",
                                                fechaAlta: "",
                                                origen: "",
                                                idSubscription: "",
                                                descripcion: "",
                                                promocion: "",
                                                accion: "",
                                                ProductID: "",
                                                idRefSuscripcion: "",
                                                fechaExpiracion: ""
                                            }

                                            listaSuscripcion.Metodo = "listaSuscripcionCliente";
                                            listaSuscripcion.precio = (value.precio == null ? '0' : value.precio);
                                            listaSuscripcion.estadoPago = value.estadoPago;
                                            listaSuscripcion.fechaAlta = value.fechaAlta;
                                            listaSuscripcion.origen = value.origen;
                                            listaSuscripcion.idSubscription = value.idSuscripcion;
                                            listaSuscripcion.descripcion = value.descripcion;
                                            listaSuscripcion.promocion = "";
                                            listaSuscripcion.accion = "Activado";
                                            listaSuscripcion.ProductID = value.productId;
                                            listaSuscripcion.idRefSuscripcion = value.idRefSuscripcion;
                                            listaSuscripcion.fechaExpiracion = value.fechaExpiracion;

                                            that.dataListClaroVideo.listaSuscripcionCliente.push(listaSuscripcion);
                                        }
                                    });
                                }
                            }

                        } else {

                            alert('Error en el servicio consultardatoscliente no se logro obtener la información de suscripciones del cliente');
                        }

                    } else {
                        alert('Error en el servicio consultardatoscliente no se logro obtener la información de suscripciones del cliente');
                    }

                    callback();

                },
                error: function (msger) {
                    console.log('Response ConsultClientSN :' + msger);
                    callback();
                }
            });
        },
        searhListarSuscripcion: function (callback) {

            var that = this,
            controls = this.getControls();

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthscListarsuscripcion");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });


            var varqueryOttRequest = {
                invokeMethod: 'listarsuscripcion',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'queryOtt',
                providerId: StrPartnerID,
                startDate: strFechaInicio,
                endDate: strFechaFin,
                iccidManager: 'AMCO',
                extraData: {
                    data: [
                        { key: "", value: "" }
                    ]
                }
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            }


            // controls.btnbuscar.button('loading');

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultSN',
                data: JSON.stringify(objqueryOttRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');

                },
                success: function (response) {
                    console.log('Response ConsultSN - Lista de suscripciones disponibles');
                    console.log(response.data);

                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.subscriptions != null) {
                                if (response.data.queryOttResponse.subscriptions.item != null) {

                                    $.each(response.data.queryOttResponse.subscriptions.item, function (index, value) {

                                        var listaSuscripcion = {
                                            Metodo: "",
                                            precio: "",
                                            estadoPago: "",
                                            fechaAlta: "",
                                            origen: "",
                                            idSubscription: "",
                                            descripcion: "",
                                            promocion: "",
                                            accion: "",
                                            ProductID: "",
                                            idRefSuscripcion: "",
                                            fechaExpiracion: "",
                                            flagVer: ""
                                        }

                                        listaSuscripcion.Metodo = "listaSuscripcion";
                                        listaSuscripcion.precio = (value.price == null ? '0' : value.price);
                                        listaSuscripcion.estadoPago = "",
                                        listaSuscripcion.fechaAlta = "",
                                        listaSuscripcion.origen = value.currency,
                                        listaSuscripcion.idSubscription = value.idSubscription,
                                        listaSuscripcion.descripcion = value.name,
                                        listaSuscripcion.promocion = "",
                                        listaSuscripcion.accion = "Desactivado",
                                        listaSuscripcion.ProductID = value.productId;
                                        listaSuscripcion.idRefSuscripcion = value.idRefSuscripcion;
                                        listaSuscripcion.fechaExpiracion = "";
                                        listaSuscripcion.flagVer = "0";

                                        that.dataListClaroVideo.listaSuscripcion.push(listaSuscripcion);

                                    });
                                }
                            }
                        } else if (response.data.queryOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.queryOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {
                                if (flag != '') {
                                    alert(flag, 'Alerta');
                                } else {
                                    alert('Error en el servicio listar suscripción, no se logró obtener la información de suscripciones disponibles');
                                }
                            });

                        } else {
                            alert('Error en el servicio listar suscripción, no se logró obtener la información de suscripciones disponibles');
                        }
                    } else {
                        alert('Error en el servicio listar suscripción, no se logró obtener la información de suscripciones disponibles');
                    }

                    callback(true);

                    //that.populateGridListarPaquete(that.dataListClaroVideo.listaSuscripcion);



                },
                error: function (msger) {
                    console.log(msger);
                    callback(true);

                    //var $tr = $('<tr>'),
                    //$td = $('<td>').prop("colspan", "8").prop('id', 'tdtbtbBlackList');
                    //$tr.append($td);
                    //$('#tbBlackList').find('tbody').append($tr);
                    //$('#tdtbtbBlackList').showMessageErrorLoading({
                    //    message: $.app.const.messageErrorLoading,
                    //    session: Session.IDSESSION,
                    //    buttonID: 'btn_buscarBlackList',
                    //    funct: function () { that.searhBlackList() },
                    //    transaction: msger,
                    //    that: that
                    //});
                }
            });
        },
        getValidateElegibility: function (data, callback) {

            var that = this,
            controls = that.getControls();

            var ListValidateElegibility = {
                medioPago: "",
                tipoLinea: "",
                listadoServicios: [],
            }

            var varvalidarElegibilidadRequest = {
                medioPago: that.ObtenerLineaCliente('CustomerID'),
                tipoLinea: that.strFlagTipoLinea,
                producto: " ",
                productoId: data.ProductID
            };

            var objValidateElegibilityRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { validarElegibilidadRequest: varvalidarElegibilidadRequest }
                }
            }

            console.log('Request getValidateElegibility');
            console.log(objValidateElegibilityRequest);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ValidateElegibility',
                async: false,
                data: JSON.stringify(objValidateElegibilityRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    console.log('Response ValidateElegibility - Lista de servicios');
                    console.log(response.data);
                    if (response.data.validateElegibilityResponse != null) {
                        if (response.data.validateElegibilityResponse.codError == "0") { //cambiar a 0 solo para pruebas                                                

                            ListValidateElegibility.medioPago = response.data.validateElegibilityResponse.medioPago;
                            ListValidateElegibility.tipoLinea = response.data.validateElegibilityResponse.tipoLinea;

                            if (response.data.validateElegibilityResponse.listadoServicios != null) {
                                if (response.data.validateElegibilityResponse.listadoServicios.length > 0) {

                                    $.each(response.data.validateElegibilityResponse.listadoServicios, function (index, value) {

                                        var listadoServicios = {
                                            nombre: "",
                                            productID: ""
                                        }

                                        listadoServicios.nombre = value.nombre;
                                        listadoServicios.productID = value.productID;
                                        ListValidateElegibility.listadoServicios.push(listadoServicios);
                                    });
                                }
                            }
                        }
                    }

                    console.log('ListValidateElegibility');
                    console.log(ListValidateElegibility);
                    callback(ListValidateElegibility);
                },
                error: function (msger) {
                    console.log(msger);
                    callback(ListValidateElegibility);
                }
            });
        },
        dataListClaroVideo: {
            listaSuscripcion: [],
            listaSuscripcionCliente: [],
            listaAcciones: [],
            listaSuscripciones: [],
            listaCancelaciones: [],
            listaCambios: [],
            listaDispositivos: [],
            listaRentas: [],
            listaVisualizacionesCliente: [],
            listaProcesosConstancias: [],
            listaVisualizacionesRentas: [],
            //INICIATIVA-794
            listaServicios: [],
        },
        ClearList: function () {
            var that = this,
                controls = that.getControls();


            that.dataListClaroVideo.listaSuscripcion = [];
            that.dataListClaroVideo.listaSuscripcionCliente = [];
            that.dataListClaroVideo.listaSuscripciones = [];
            that.dataListClaroVideo.listaCancelaciones = [];
            that.dataListClaroVideo.listaCambios = [];
            that.dataListClaroVideo.listaDispositivos = [];
            that.dataListClaroVideo.listaRentas = [];
            that.dataListClaroVideo.listaVisualizacionesCliente = [];
            that.dataListClaroVideo.listaVisualizacionesRentas = [],

            that.populateGridListarPaquete(null);
            var loadTable = '';
            loadTable = loadTable + '<tr>';
            loadTable = loadTable + '<td colspan="8" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
            loadTable = loadTable + '</tr>';
            $('#tbPackageBodyList').html(loadTable);


            if ($(controls.chkDesvincular).is(':checked')) {
                var loadTable = '';
                loadTable = loadTable + '<tr>';
                loadTable = loadTable + '<td colspan="5" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
                loadTable = loadTable + '</tr>';
                $('#tbDeviceUserBodyList').html(loadTable);
                that.searhDeviceUser();
            }
        },
        servicePaymentStatus: function (descripcion, code, callback) {

            var that = this,
                controls = that.getControls();
            var phone = '', Type = '', formato = '', tmcode = '0';

            phone = that.ObtenerLineaCliente('Linea'); // cambio mejora
            Type = that.TypeLine;

            if (phone == '') {

                console.log('No se obtuvo: Phone');

            } else if (Type == '') {

                console.log('No se obtuvo: tipo de linea');
            }

            var arrReq;

            if (code == '') {

                arrReq = {
                    nombreServicio: descripcion,
                    linea: phone,
                    servicioName: "EstadoPagoServicio",
                    tipoLinea: Type
                };
            } else {
                tmcode = '1';

                arrReq = {
                    nombreServicio: descripcion,
                    linea: phone,
                    tmcod: code,
                    servicioName: "EstadoPagoServicio",
                    tipoLinea: Type
                };
            }

            var objNodeRequest = {
                strIdSession: Session.IDSESSION,
                flagIsTMCOD: tmcode,
                MessageRequest: {
                    Body: { historialServDispCVRequest: arrReq }
                }
            }

            console.log('Request Promocional');
            console.log(objNodeRequest);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                //async: false,
                url: '/Transactions/Fixed/ClaroVideo/HistoryDeviceService',
                data: JSON.stringify(objNodeRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    console.log('Response HistoryDeviceService - Lista de promociones');
                    console.log(response.data);
                    if (response.data.historialServDispCVResponse != null) {
                        if (response.data.historialServDispCVResponse.pEstadoPagoServ != null) {

                            that.MessageFormat(response.data.historialServDispCVResponse.pEstadoPagoServ, function (formato) {
                                //console.log(formato);
                                callback(formato);
                                // console.log(response.data.historialServDispCVResponse.codError + ": " + response.data.historialServDispCVResponse.messageError);
                            });

                        } else {
                            // console.log(response.data.historialServDispCVResponse.codError + ": " + response.data.historialServDispCVResponse.messageError);
                            callback('');
                        }
                    } else {
                        callback('');
                    }
                },
                error: function (msger) {
                    console.log('Error: Response HistoryDeviceService - Lista de promociones:' + msger);
                    callback('');

                    //var $tr = $('<tr>'),
                    //$td = $('<td>').prop("colspan", "8").prop('id', 'tdtbtbBlackList');
                    //$tr.append($td);
                    //$('#tbBlackList').find('tbody').append($tr);
                    //$('#tdtbtbBlackList').showMessageErrorLoading({
                    //    message: $.app.const.messageErrorLoading,
                    //    session: Session.IDSESSION,
                    //    buttonID: 'btn_buscarBlackList',
                    //    funct: function () { that.searhBlackList() },
                    //    transaction: msger,
                    //    that: that
                    //});
                }
            });

        },
        MessageFormat: function (lista, callback) {

            var message = '';

            var d = new Date();
            var FechaHasta = new Date();

            if (lista.length > 0) {

                $.grep(lista, function (data) {

                    var state = GetKeyConfig('strEstadoPagoPromocional').toUpperCase();
                    var part = '';

                    if (data.estadoPago.toUpperCase() == state) {
                        if (data.fechaAct == null || data.fechaAct == '') {

                            //part = 'de ' + data.diasPromo + 'dias';
                            //var fecha = new Date($('#fech').val());
                            var dias = 0;

                            if (data.diasPromo != null) {
                                dias = parseInt(data.diasPromo);
                            }
                            // Número de días a agregar
                            var Desde = AboveZero(d.getDate()) + "/" + (AboveZero(d.getMonth() + 1)) + "/" + d.getFullYear();
                            FechaHasta.setDate(FechaHasta.getDate() + dias);

                            var FechaHastaFinal = AboveZero(FechaHasta.getDate()) + '/' + AboveZero(FechaHasta.getMonth() + 1) + '/' + FechaHasta.getFullYear();


                            part = 'del ' + Desde + ' hasta ' + FechaHastaFinal;

                        } else {

                            part = 'del ' + data.fechaAct + ' hasta ' + data.fehaExp;
                        }

                        message = 'El producto ' + data.nombreServicio + ' tiene un periodo promocional (Gratis) ' + part + ' despues de la fecha indicada  el costo sera de S/.' + data.servicioPrecio;

                    }

                    //} else {

                    //    message = 'El producto ' + data.nombreServicio + ' es de Pago';
                    //}
                    // console.log(message);
                    callback(message);
                });

            } else {

                //console.log(message);
                callback(message);
            }
            //'El producto ' + objRes.nombreServicio+' tiene un periodo promocional'+;
        },
        datatableBlackList: {},
        populateGridListarPaquete: function (Lista) {
            var that = this;
            var controls = that.getControls();

            //INICIATIVA-794
            that.GetConsultarServicioIPTV(SessionTransac.SessionParams.DATACUSTOMER.Application);

            var table = $('#tbPackageList').DataTable();
            table.clear().draw();

            $('#tbPackageList').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": false,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[1, "desc"]],
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [
                    {
                        "data": "descripcion"
                    },
                    {
                        "data": "accion",
                        "render":
                           function (data, type, row, meta) {
                               if (type === 'display') {

                                   if (row.accion.trim().toUpperCase() == 'DESACTIVADO') {
                                       data = '<b><p class="text-danger">' + row.accion + '</p></b>';
                                   } else {
                                       data = '<b><p class="text-success">' + row.accion + '</p></b>';
                                   }


                               }
                               return data;
                           }
                    },
                    {
                        "data": "fechaAlta"
                    },
                    {
                        "data": "idSubscription",
                        "render":
                        function (data, type, row, meta) {
                            if (type === 'display') {
                                //var MensajePromocion = '';
                                if (row.promocion == '') {
                                    //MensajePromocion = 'El producto ' + row.descripcion + ' es de Pago';
                                    data = '<center><div id="divPromocion' + row.idSubscription + '"><img src="/Images/loading.gif" height="20px" width="20px" /></div></center>';
                                    //data = '<center><label class="text-primary"> Pago</label></center>';
                                } else {
                                    //MensajePromocion = row.promocion;
                                    data = '<center><button type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'MostrarPromocionClaroVideo\',\'' + row.promocion + '\')"' + 'class="btn btn-link" title="Ver promocion" <span class="glyphicon glyphicon-check"></span> Promocional </button> </center>';
                                }

                                //data = '<center><button type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'MostrarPromocionClaroVideo\',\'' + MensajePromocion + '\')"' + 'class="btn btn-link" title="Ver promocion" <span class="glyphicon glyphicon-check"></span> Promocional </button> </center>';
                            }
                            return data;
                        }
                        // cambiar
                    },
                    {
                        "data": "precio",
                        "render":
                            function (data, type, row, meta) {
                                if (type === 'display') {

                                    data = '<div id="divPrecio' + row.idSubscription + '"><img src="/Images/loading.gif" height="20px" width="20px" /></div>';

                                    //if (row.promocion != '') {
                                    //    data = 'S/.0.00';
                                    //}
                                    //else {
                                    //    data = 'S/.' + row.precio;//quitar, ya que el servicio trae numeros decimales
                                    //}

                                }
                                return data;
                            }
                    },
                    {
                        "data": "origen" // preguntar
                    },
                    {
                        "data": "idSubscription",
                        "render":
                         function (data, type, row, meta) {
                             if (type === 'display') {

                                 //MODIFICADO IPTV 1 PLAY
                                 var productId = row.ProductID;
                                 var flagServIPTV = false;

                                 //INICIATIVA-794
                                 for (var i = 0; i < that.listaServicios.length; i++) {
                                    if (productId !== null) {
                                         if (that.listaServicios[i].SERVV_CODIGO_EXT == productId) {
                                            flagServIPTV = true;
                                        }
                                    }
                                 }

                                 if (flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                     data = '<div id="divAccion' + row.idSubscription + '" disabled><img src="/Images/loading.gif" height="20px" width="20px" /></div>';
                                } else {
                                    data = '<div id="divAccion' + row.idSubscription + '"><img src="/Images/loading.gif" height="20px" width="20px" /></div>';
                                }
                             }
                             return data;
                         }
                    },
                    {
                        "data": "idSubscription",
                        "render":
                         function (data, type, row, meta) {
                             if (type === 'display') {

                                 //MODIFICADO IPTV 1 PLAY
                                 var __productId = row.ProductID;
                                 var __flagServIPTV = false;

                                 //INICIATIVA-794
                                 for (var i = 0; i < that.listaServicios.length; i++) {
                                     if (__productId !== null) {
                                         if (that.listaServicios[i].SERVV_CODIGO_EXT == __productId) {
                                             __flagServIPTV = true;
                                         }
                                     }
                                 }

                                 //INICIATIVA-794
                                 if (row.FlagVer == '1') {
                                     if (__flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                         data = '<center><button disabled type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'OpenHistoryActivateService\',\'' + row.descripcion + '\')"' + 'class="btn claro-btn-info btn-sm" title="Ver"> <i class="glyphicon glyphicon-search"></i>  Ver </button> </center>';
                                     } else {
                                         data = '<center><button type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'OpenHistoryActivateService\',\'' + row.descripcion + '\')"' + 'class="btn claro-btn-info btn-sm" title="Ver"> <i class="glyphicon glyphicon-search"></i>  Ver </button> </center>';
                                     }
                                 } else {
                                     if (__flagServIPTV && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                                         data = '<center><button disabled type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'OpenHistoryActivateService\',\'' + row.descripcion + '\')"' + 'class="btn claro-btn-info btn-sm" title="Ver"> <i class="glyphicon glyphicon-search"></i>  Ver </button> </center>';
                                     } else {
                                         data = '<center><button type="button" id=' + "btn" + row.idSubscription + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'OpenHistoryActivateService\',\'' + row.descripcion + '\')"' + 'class="btn claro-btn-info btn-sm" title="Ver"> <i class="glyphicon glyphicon-search"></i>  Ver </button> </center>';
                                     }
                                 }
                             }
                             return data;
                         },
                        "visible": that.flagIsClientClarovideo
                    },
                ],
                createdRow: function (row, data, dataIndex, cells) {
                        //MODIFICADO IPTV 1 PLAY
                        var productId2 = data.ProductID;
                        var flagServIPTV2 = false;

                        //INICIATIVA-794
                        if (flagUnicoIPTV == 1) {
                            for (var i = 0; i < that.listaServicios.length; i++) {
                            if (productId2 !== null) {
                                    if (that.listaServicios[i].SERVV_CODIGO_EXT == productId2) {
                                    flagServIPTV2 = true;
                                }
                            }
                        }
                        }


                        if (flagServIPTV2 && that.dataListClaroVideo.resultadoValidarIPTV == '1') {
                            console.log("se cambio el estilo de la fila: ",data.ProductID);
                            $(row).addClass('gris_Class_Row cursor_block_iptv');
                        }
                },
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },

            });

        },
        searhRentalUser: function (callback) {
            var that = this,
            controls = this.getControls();

            var loadTable = '';
            loadTable = loadTable + '<tr>';
            loadTable = loadTable + '<td colspan="9" style="text-align: center;"><div style="padding: 30px 30px 10px 30px;"><img src="/Images/loading.gif" height="45" width="45" /></div></td>';
            loadTable = loadTable + '</tr>';
            $('#tbRentaPeliculaBodyList').html(loadTable);

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarrentascliente");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });

            var varqueryOttRequest = {
                invokeMethod: 'consultarrentascliente',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'queryOtt',
                providerId: StrPartnerID,
                startDate: strFechaInicio,
                endDate: strFechaFin,
                iccidManager: 'AMCO',
                extensionInfo: [
                    { key: "CUSTOMERID", value: that.CustomerClaroVideo }
                ]
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            };

            console.log('Request ConsultSN - Lista de rentas');
            console.log(objqueryOttRequest);

            // $('#tbRentalUser').find('tbody').html('');
            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultSN',
                data: JSON.stringify(objqueryOttRequest),
                complete: function () {

                },
                success: function (response) {
                    console.log('Response ConsultSN - Lista de rentas')
                    console.log(response.data);
                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.rentList.ListRentalUser != null) {
                                if (response.data.queryOttResponse.rentList.ListRentalUser.length > 0) {

                                    $.each(response.data.queryOttResponse.rentList.ListRentalUser, function (index, value) {

                                        var ListRentalUser = {
                                            descripcion: "",
                                            fechaAlta: "",
                                            fechaExpiracion: "",
                                            idRefRenta: "",
                                            idRenta: "",
                                            ipUsuario: "",
                                            medioPago: "",
                                            precio: "",
                                            tiempoMaximoVisualizacion: "",
                                            ultimaVisualizacion: ""
                                        }

                                        ListRentalUser.descripcion = value.descripcion;
                                        ListRentalUser.fechaAlta = value.fechaAlta;
                                        ListRentalUser.fechaExpiracion = value.fechaExpiracion;
                                        ListRentalUser.idRefRenta = value.idRefRenta;
                                        ListRentalUser.idRenta = value.idRenta;
                                        ListRentalUser.ipUsuario = (value.ipUsuario == 'Null' ? "" : value.ipUsuario);
                                        ListRentalUser.medioPago = value.medioPago;
                                        ListRentalUser.moneda = value.moneda;
                                        ListRentalUser.precio = value.precio;
                                        ListRentalUser.tiempoMaximoVisualizacion = (value.tiempoMaximoVisualizacion == 'Null' ? "" : value.tiempoMaximoVisualizacion);
                                        ListRentalUser.ultimaVisualizacion = (value.ultimaVisualizacion == 'Null' ? "" : value.ultimaVisualizacion);

                                        that.dataListClaroVideo.listaVisualizacionesRentas.push(ListRentalUser);
                                    });


                                    // that.populateGridRentalUser(response.data.queryOttResponse.rentList.ListRentalUser);
                                }
                            }
                        } else if (response.data.queryOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.queryOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {
                                if (flag != '') {
                                    alert(flag, 'Alerta');
                                }
                            });
                        }
                    }

                    console.log('Lista de rentas a cargar');
                    console.log(that.dataListClaroVideo.listaVisualizacionesRentas);
                    callback(true);

                },
                error: function (msger) {
                    console.log('Error: Response ConsultSN - Lista de rentas : ' + msger);
                    callback(true);
                }
            });
        },
        populateGridRentalUser: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbRentalUser').DataTable();
            table.clear().draw();

            $('#tbRentalUser').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[5, "desc"]],
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [
                    {
                        "data": "descripcion"
                    },
                    {
                        "data": "ipUsuario"
                    },
                    {
                        "data": "ultimaVisualizacion"
                    },
                    {
                        "data": "tiempoMaximoVisualizacion"
                    },
                    {
                        "data": "fechaAlta"
                    },
                    {
                        "data": "fechaExpiracion"
                    },
                    {
                        "data": "precio"
                    },
                    {
                        "data": "medioPago"
                    }
                    ,
                    {
                        "data": "idRenta",
                        "render":
                         function (data, type, row, meta) {
                             if (type === 'display') {
                                 if (row.idRenta != "") {
                                     data = '<center><button type="button" id=' + "btnRentail" + row.idRefRenta + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'UnlinkRentail\',\'' + row.idRenta + '|' + row.descripcion.replace(/['"]/gi, '§') + '|' + row.precio + '|' + row.idRefRenta + '\')"' + 'class="btn claro-btn-info btn-sm" title="Habilitar" <span class="glyphicon glyphicon-check"></span> Habilitar </button> </center>'; // INC000003840015 - ESCENARIO ADICIONAL
                                 }
                             }
                             return data;
                         }
                    }
                ],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },

            });

        },

        searhVisualizationClient: function () {
            var that = this,
            controls = this.getControls();

            if (that.dataListClaroVideo.listaSuscripcionCliente != null) {
                if (that.dataListClaroVideo.listaSuscripcionCliente.length > 0) {

                    that.dataListClaroVideo.listaVisualizacionesCliente = [];

                    $.each(that.dataListClaroVideo.listaSuscripcionCliente, function (index, value) {

                        that.SearchVisualization(value.idRefSuscripcion, function (flag) {

                            if (index == that.dataListClaroVideo.listaSuscripcionCliente.length - 1) {
                                if (that.dataListClaroVideo.listaVisualizacionesCliente.length > 0) {
                                    $('#tbVisualizationClient').find('tbody').html('');
                                    that.populateGridVisualizationClient(that.dataListClaroVideo.listaVisualizacionesCliente);

                                } else {
                                    $('#tbVisualizationClient').find('tbody').html('');
                                    that.populateGridVisualizationClient(null);
                                }
                            }

                        });
                    });
                } else {
                    $('#tbVisualizationClient').find('tbody').html('');
                    that.populateGridVisualizationClient(null);
                }
            } else {
                $('#tbVisualizationClient').find('tbody').html('');
                that.populateGridVisualizationClient(null);
            }
        },
        SearchVisualization: function (idRefSuscripcion, callback) {
            var that = this;

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultarvisualizacionessuscripcion");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });

            var varqueryOttRequest = {
                invokeMethod: 'consultarvisualizacionessuscripcion',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'queryOtt',
                providerId: StrPartnerID,
                startDate: strFechaInicio,
                endDate: strFechaFin,
                iccidManager: 'AMCO',
                extensionInfo: [
                    { key: "CUSTOMERID", value: that.CustomerClaroVideo },
                    { key: "idRefSuscripcion", value: idRefSuscripcion }
                ]
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            }

            console.log('Request Visualizacion');
            console.log(objqueryOttRequest);


            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultSN',
                data: JSON.stringify(objqueryOttRequest),
                complete: function () {

                },
                success: function (response) {
                    console.log('Response ConsultSN - Lista de visualizaciones');
                    console.log(response.data);
                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.visualizationsList.ListVisualizationUser != null) {
                                if (response.data.queryOttResponse.visualizationsList.ListVisualizationUser.length > 0) {
                                    $.each(response.data.queryOttResponse.visualizationsList.ListVisualizationUser, function (index, value) {

                                        var ListaVisualizationUser = {
                                            idContenido: "",
                                            titulo: "",
                                            ipUsuario: "",
                                            fechaTiempoVisualizacion: "",
                                            ultimoTiempoVisualizacion: "",
                                            fechaMaximoVisualizacion: "",
                                            tiempoMaximoVisualizacion: "",
                                            fechaTiempoVisualizacionConvert: ""
                                        }

                                        ListaVisualizationUser.idContenido = value.idContenido;
                                        ListaVisualizationUser.titulo = value.titulo;
                                        ListaVisualizationUser.ipUsuario = value.ipUsuario;
                                        ListaVisualizationUser.fechaTiempoVisualizacion = (value.fechaTiempoVisualizacion == 'null' ? "" : value.fechaTiempoVisualizacion);
                                        ListaVisualizationUser.fechaTiempoVisualizacionConvert = (value.fechaTiempoVisualizacion == 'null' ? "" : new Date(value.fechaTiempoVisualizacion));
                                        ListaVisualizationUser.ultimoTiempoVisualizacion = (value.ultimoTiempoVisualizacion == 'null' ? "" : value.ultimoTiempoVisualizacion);
                                        ListaVisualizationUser.fechaMaximoVisualizacion = (value.fechaMaximoVisualizacion == 'null' ? "" : value.fechaMaximoVisualizacion);
                                        ListaVisualizationUser.tiempoMaximoVisualizacion = (value.tiempoMaximoVisualizacion == 'null' ? "" : value.tiempoMaximoVisualizacion);


                                        that.dataListClaroVideo.listaVisualizacionesCliente.push(ListaVisualizationUser);


                                    });
                                }

                            }

                        } else if (response.data.queryOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.queryOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {
                                if (flag != '') {
                                    alert(flag, 'Alerta');
                                }
                            });
                        }
                    }

                    console.log(that.dataListClaroVideo.listaVisualizacionesCliente);
                    callback(true);

                    //console.log('lista visualizaciones');
                    //console.log(that.dataListClaroVideo.listaVisualizacionesCliente);

                },
                error: function (msger) {
                    console.log('Error: Response ConsultSN - Lista de visualizaciones: ' + msger);
                    callback(false);
                }
            });
        },
        populateGridVisualizationClient: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbVisualizationClient').DataTable();
            table.clear().draw();

            $('#tbVisualizationClient').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[7, "desc"]],
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [

                    {
                        "data": "idContenido"
                    },
                    {
                        "data": "titulo"
                    },
                    {
                        "data": "ipUsuario"
                    },
                    {
                        "data": "fechaTiempoVisualizacion"
                    },
                    {
                        "data": "ultimoTiempoVisualizacion",
                        "render":
                       function (data, type, row, meta) {
                           if (type === 'display') {

                               data = '<p class="text-right">' + row.ultimoTiempoVisualizacion + '</p>';

                           }
                           return data;
                       }
                    },
                    {
                        "data": "fechaMaximoVisualizacion"
                    },
                    {
                        "data": "tiempoMaximoVisualizacion",
                        "render":
                        function (data, type, row, meta) {
                            if (type === 'display') {

                                data = '<p class="text-right">' + row.tiempoMaximoVisualizacion + '</p>';

                            }
                            return data;
                        }
                    },
                    {
                        "data": "fechaTiempoVisualizacionConvert",
                        "visible": false

                    }

                ],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },

            });

        },
        searhDeviceUser: function () {
            var that = this,
            controls = this.getControls();

            var StrPartnerID = GetKeyConfig("strPartnerIDconsultarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var strFechaInicio = "";
            var strFechaFin = "";

            var strFilterMesesBusqueda = GetKeyConfig("strFilterMonthsconsultardispositivoscliente");
            var StrFilterDate = GetFilterDateMonth(strFilterMesesBusqueda, 1, "-", 1, function (Datos) {
                strFechaInicio = Datos.desde;
                strFechaFin = Datos.hasta;
            });


            var varqueryOttRequest = {
                invokeMethod: 'consultardispositivoscliente',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'queryOtt',
                providerId: StrPartnerID,
                startDate: strFechaInicio,
                endDate: strFechaFin,
                iccidManager: 'AMCO',
                extensionInfo: [
                    { key: "CUSTOMERID", value: that.CustomerClaroVideo }
                ],
                extraData: {
                    data: [
                        { key: "key11", value: "value11" },
						{
						    key: "key2",
						    value: "value22"
						}
                    ]
                }
            };

            var objqueryOttRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { queryOttRequest: varqueryOttRequest }
                }
            }

            console.log('Request ConsultSN - Lista de dispositivos');
            console.log(objqueryOttRequest);
            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/ConsultSN',
                data: JSON.stringify(objqueryOttRequest),
                complete: function () {

                },
                success: function (response) {
                    console.log('Response ConsultSN - Lista de dispositivos');
                    console.log(response.data);
                    if (response.data.queryOttResponse != null) {
                        if (response.data.queryOttResponse.resultCode == "0") { //cambiar a 0 solo para pruebas
                            if (response.data.queryOttResponse.deviceList.ListDeviceUser != null) {
                                if (response.data.queryOttResponse.deviceList.ListDeviceUser.length > 0) {
                                    $('#tbDeviceUser').find('tbody').html('');
                                    that.populateGridDeviceUser(response.data.queryOttResponse.deviceList.ListDeviceUser);
                                } else {
                                    $('#tbDeviceUser').find('tbody').html('');
                                    that.populateGridDeviceUser(null);
                                }
                            } else {
                                $('#tbDeviceUser').find('tbody').html('');
                                that.populateGridDeviceUser(null);
                            }
                        } else {
                            $('#tbDeviceUser').find('tbody').html('');
                            that.populateGridDeviceUser(null);
                        }
                    } else {
                        $('#tbDeviceUser').find('tbody').html('');
                        that.populateGridDeviceUser(null);
                    }
                },
                error: function (msger) {
                    console.log('Error: Response ConsultSN - Lista de dispositivos:' + msger);
                    $('#tbDeviceUser').find('tbody').html('');
                    that.populateGridDeviceUser(null);
                    //var $tr = $('<tr>'),
                    //$td = $('<td>').prop("colspan", "8").prop('id', 'tdtbtbBlackList');
                    //$tr.append($td);
                    //$('#tbBlackList').find('tbody').append($tr);
                    //$('#tdtbtbBlackList').showMessageErrorLoading({
                    //    message: $.app.const.messageErrorLoading,
                    //    session: Session.IDSESSION,
                    //    buttonID: 'btn_buscarBlackList',
                    //    funct: function () { that.searhBlackList() },
                    //    transaction: msger,
                    //    that: that
                    //});
                    that.populateGridDeviceUser(null);
                }
            });
        },
        populateGridDeviceUser: function (Lista) {
            var that = this;
            var controls = that.getControls();

            var table = $('#tbDeviceUser').DataTable();
            table.clear().draw();

            $('#tbDeviceUser').DataTable({
                "scrollY": "100%",
                "scrollCollapse": true,
                "paging": true,
                "searching": true,
                "destroy": true,
                "scrollX": true,
                "sScrollX": "100%",
                "sScrollXInner": "100%",
                "autoWidth": true,
                "order": [[1, "desc"]],
                "lengthMenu": [[10, 15, 20, 25, -1], [10, 15, 20, 25, "Todos"]],
                data: Lista,
                columns: [

                    {
                        "data": "tipoDispositivo"
                    },
                    {
                        "data": "nombreDispositivo"
                    },
                    {
                        "data": "idDispositivo"
                    },
                    {
                        "data": "fechaActivacion"
                    },
                    {
                        "data": "tipoDispositivo",
                        "render":
                         function (data, type, row, meta) {
                             if (type === 'display') {
                                 data = '<center><button type="button" id=' + "btnDesvincular" + row.idDispositivo + ' onclick="$(\'#ContentSuscripcionClaroVideo\').SuscripcionClaroVideo(\'UnlinkDevice\',\'' + row.idDispositivo + ',' + row.nombreDispositivo + ',' + row.fechaActivacion + ',' + row.tipoDispositivo + '\')"' + 'class="btn claro-btn-info btn-sm" title="Desvincular">  Desvincular </button> </center>';
                             }
                             return data;
                         }
                    }

                ],
                language: {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },

            });

        },
        UnlinkDevice: function (RowDispositivo) {


            var param = RowDispositivo.split(',');

            console.log('UnlinkDevice');
            var that = this,
                controls = that.getControls();

            var ListDevice = {
                strDispotisitivoID: "",
                strDispotisitivoNom: "",
                strFechaDesac: "",
                flagProcesado: "",
                strTipoDispositivo: ""
            }

            ListDevice.strDispotisitivoID = param[0];
            ListDevice.strDispotisitivoNom = param[1];
            ListDevice.strFechaDesac = param[2];
            ListDevice.flagProcesado = "0";
            ListDevice.strTipoDispositivo = param[3];

            that.dataListClaroVideo.listaDispositivos.push(ListDevice);
            console.log(that.dataListClaroVideo.listaDispositivos);

            $('#btnDesvincular' + param[0]).prop('disabled', true);


        },
        UnlinkRentail: function (RowSuscripcion) {

            var param = RowSuscripcion.split('|');

            console.log('UnlinkRentail');

            var that = this,
                controls = that.getControls();

            var ListRentail = {
                strRentailID: "",
                strRentailNom: "",
                flagProcesado: "",
                strPrecio: ""
            }

            ListRentail.strRentailID = param[0];
            ListRentail.strRentailNom = param[1].replace(/§/gi, "'"); // INC000003840015 - ESCENARIO ADICIONAL
            ListRentail.flagProcesado = "0";
            ListRentail.strRentaPrecio = param[2];


            that.dataListClaroVideo.listaRentas.push(ListRentail);
            console.log(ListRentail);
            console.log(that.dataListClaroVideo.listaRentas);

            //INC000003840015 - INICIO
            console.log('Cantidad de Peliculas : ', that.dataListClaroVideo.listaRentas.length)
            if (that.dataListClaroVideo.listaRentas.length > 0) {
                $("#btnSave").prop('disabled', false);
                //INICIATIVA-794
                $("#btnSave").addClass("btn btn-info active");
            } else if (that.dataListClaroVideo.listaRentas.length == 0) {
                $("#btnSave").prop('disabled', true);
            }
            //INC000003840015 - FIN
            
            $('#btnRentail' + param[3]).prop('disabled', true);

        },
        GenerarDesvinculacionDispositivo: function (isGenerarActualizacion, ListDevice, callback) {

            var that = this,
               controls = that.getControls();

            if (isGenerarActualizacion == "1") {

                var CountDevice = 0;
                var CantidadProcesados = 0;
                var cantOK = 0;

                if (ListDevice.length > 0) {
                    CountDevice = ListDevice[0].length
                }

                var DeviceProcesado = 0;

                if (ListDevice != null && ListDevice != undefined) {

                    if (CountDevice > 0) {

                        var Proceso = GetKeyConfig("strNomTransaSusDesvDispositivo");

                        $.each(ListDevice[0], function (index, value) {

                            that.ProcesoDesvinculacionDispotivo(value.strDispotisitivoID, function (Flag_CantidadProcesados) {

                                CantidadProcesados = CantidadProcesados + 1;
                                cantOK = cantOK + Flag_CantidadProcesados;

                                if (Flag_CantidadProcesados > 0) {

                                    that.ControlError.StrFlagOKAcciones = true;
                                    $.grep(that.dataListClaroVideo.listaAcciones, function (data) {
                                        if (data.Metodo == Proceso) {
                                            $.grep(data.ListDevice, function (dataListDevice) {
                                                for (var i = 0; i < dataListDevice.length; i++) {

                                                    if (dataListDevice[i].strDispotisitivoID == value.strDispotisitivoID) {
                                                        dataListDevice[i].flagProcesado = "1";

                                                        var fecha = new Date();
                                                        var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();

                                                        var ListDevice = {
                                                            strDispotisitivoID: "",
                                                            strDispotisitivoNom: "",
                                                            strFechaDesac: "",
                                                            flagProcesado: "",
                                                            strSuscFechReg: "",
                                                            strTipoDispositivo: ""
                                                        }

                                                        ListDevice.strDispotisitivoID = value.strDispotisitivoID;
                                                        ListDevice.strDispotisitivoNom = value.strDispotisitivoNom;
                                                        ListDevice.strFechaDesac = value.strFechaDesac;
                                                        ListDevice.strSuscFechReg = FechaRegistro;
                                                        ListDevice.strTipoDispositivo = value.strTipoDispositivo;
                                                        ListDevice.flagProcesado = "1";

                                                        data.objParametersGenerateContancy.ListDevice.push(ListDevice);

                                                        return true;
                                                    }
                                                }
                                            });
                                        }
                                    });
                                } else {
                                    that.ControlError.StrFlagFailedAcciones = true;
                                }

                                if (CantidadProcesados == CountDevice) {
                                    // valida si se ha realizado al menos 1 desvinculacion
                                    if (cantOK > 0) {
                                        callback(true);
                                    } else {
                                        callback(false);
                                    }
                                }

                            });

                        });
                    } else {
                        callback(false);
                    }
                } else {
                    callback(false);
                }

                //console.log(that.dataListClaroVideo.listaAcciones);

            } else {
                callback(false);
            }

        },
        ProcesoDesvinculacionDispotivo: function (IdEquipo, callback) {
            var that = this;

            var StrPartnerID = GetKeyConfig("strPartnerIDactualizarClienteSN");
            var CorrelatorId = that.GeneratedCorrelatorId();
            var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var arrReq = {

                invokeMethod: 'desvinculardispositivo',
                correlatorId: providerCorrelatorId,
                countryId: 'PE',
                userId: 'AMCO',
                deviceId: IdEquipo,
                newEmail: null,
                employeeId: GetKeyConfig("strEmployeeId"),
                origin: 'SIAC',
                serviceName: 'actualizarClienteSN',
                providerId: StrPartnerID,
                iccidManager: 'AMCO',
                extensionInfo: [
                    { key: "CUSTOMERID", value: that.CustomerClaroVideo }
                ]
            };
            var objUpdateUserOttRequest = {
                strIdSession: Session.IDSESSION,
                strEntityAddEmail: '0',
                strEntityAdddeviceId: '1',
                MessageRequest: {
                    Body: { updateUserOttRequest: arrReq }
                }
            };

            console.log('Request UpdateClientSN - Desvincular dispositivo');
            console.log(objUpdateUserOttRequest);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Fixed/ClaroVideo/UpdateClientSN',
                data: JSON.stringify(objUpdateUserOttRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    console.log('Response UpdateClientSN - Desvincular dispositivo');
                    console.log(response.data);
                    if (response.data.updateUserOttResponse != null) {
                        if (response.data.updateUserOttResponse.resultCode == "0") {
                            console.log('Desvincular con exito');
                            callback(1);
                        } else if (response.data.updateUserOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                            var Mensaje = response.data.updateUserOttResponse.resultMessage;

                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                if (flag != '') {
                                    if (that.StrMensajePersonalizado.MensajeError != '') {
                                        that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                    } else {
                                        that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                    }
                                }

                                callback(0);

                            });
                        }
                        else {
                            callback(0);
                        }
                    } else {
                        callback(0);
                    }
                },
                error: function (msger) {
                    console.log('Error: Response UpdateClientSN - Desvincular dispositivo: ' + smsger);
                    callback(0);
                }
            });

        },
        ProcesoSuscripcion: function (data, callback) {


            var that = this,
                controls = that.getControls();

            var CantOK = 0;

            that.getValidateElegibility(data, function (datos) {

                var medioPago = datos.medioPago;
                var tipoLinea = datos.tipoLinea;
                var listadoServicios = datos.listadoServicios;

                if (listadoServicios.length > 0) {

                    var Linea = that.ObtenerLineaCliente('Linea');
                    var StrPartnerID = GetKeyConfig("strPartnerIDprovisionarSuscripcionSN");
                    var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
                    var IdPersonalizado = that.GeneratedCorrelatorId();
                    var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

                    for (var i = 0; i < listadoServicios.length; i++) {

                        var varprovisionarSuscripcionSNRequest = {
                            operatorProvisioningProductRequest: {

                                partnerID: StrPartnerID,
                                productID: listadoServicios[i].productID,
                                level: '1',
                                operatorUser: {
                                    operatorUserID: Linea,
                                    providerUserID: that.CustomerClaroVideo,
                                    subProductID: " ",
                                    description: listadoServicios[i].nombre
                                },
                                countryID: "PER",
                                extensionInfo: [
                        {
                            key: "CUSTOMERID",
                            value: that.CustomerClaroVideo
                        },
                        {
                            key: "EXECUTIONTYPE",
                            value: that.strExecutiontype
                        },
                        {
                            key: "CONTENTID",
                            value: "2"
                        },
                        {
                            key: "region",
                            value: "PE"
                        },
                        {
                            key: "user_type",
                            value: GetKeyConfig("strUsertype")
                        },
                        {
                            key: "payment_method",
                            value: that.strPaymentMethod
                        }
                                ]

                            }
                        };

                        var objProvisionSubscripRequest = {
                            strIdSession: Session.IDSESSION,
                            MessageRequest: {
                                Body: { provisionarSuscripcionSNRequest: varprovisionarSuscripcionSNRequest }
                            }
                        }

                        console.log('Request ProvisionSubscription');
                        console.log(objProvisionSubscripRequest);

                        // controls.btnbuscar.button('loading');
                        $.app.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            async: false,
                            url: '/Transactions/Fixed/ClaroVideo/ProvisionSubscription',
                            data: JSON.stringify(objProvisionSubscripRequest),
                            complete: function () {
                                //controls.btnbuscar.button('reset');
                            },
                            success: function (response) {

                                //falta implementar si existe o no
                                console.log('Response ProvisionSubscription');
                                console.log(response.data);

                                if (response.data.operatorProvisioningProductResponse != null) {
                                    if (response.data.operatorProvisioningProductResponse.result != null) {
                                        if (response.data.operatorProvisioningProductResponse.result.resultCode == "0") {
                                            console.log('exito');

                                            if (response.Transaccion != null) {

                                                var dataControl = {
                                                    transaccionId: response.Transaccion,
                                                    tipoTransaccion: GetKeyConfig("strControltipoTransaccionSuscripcion"),
                                                    operacionSuscripcion: GetKeyConfig("strControlOperacionSuscripcion") + listadoServicios[i].nombre,
                                                    nombreServicio: listadoServicios[i].nombre, // poner variable del servicio
                                                    nombrePdv: controls.cboPuntoVenta.val(),
                                                    custormerId: that.CustomerIdBSCS,
                                                    linea: Linea,
                                                    estadoTransaccion: 'Exito',
                                                    mensajeTransaccion: response.data.operatorProvisioningProductResponse.result.resultMessage
                                                };

                                                // llamada al servicio de controles.
                                                that.RegisterControlsClaroVideo(dataControl, function (flag) {

                                                    if (flag) {
                                                        console.log('se registro el control');
                                                    } else {
                                                        console.log('No se registro el control');
                                                    }

                                                    CantOK = CantOK + 1;
                                                    // callback(1);
                                                });
                                            }

                                        } else if (response.data.operatorProvisioningProductResponse.result.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                            var Mensaje = response.data.operatorProvisioningProductResponse.result.resultMessage;

                                            that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                                if (flag != '') {
                                                    if (that.StrMensajePersonalizado.MensajeError != '') {
                                                        that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                                    } else {
                                                        that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                                    }
                                                }
                                            });

                                            //callback(0);
                                        }

                                    } else {

                                        //callback(0);
                                    }

                                } else {

                                    // callback(0);
                                }
                            },
                            error: function (msger) {
                                console.log(msger);
                                // callback(0);
                            }
                        });
                    }
                }
            });

            if (CantOK > 0) {
                callback(1);
            } else {
                callback(0);
            }
        },
        ProcesoSuscripcionRenta: function (data, callback) {


            var that = this,
                controls = that.getControls();

            var CantOK = 0;

            var Linea = that.ObtenerLineaCliente('Linea');
            var StrPartnerID = GetKeyConfig("strPartnerIDprovisionarSuscripcionSN");
            var StrProductIDRenta = GetKeyConfig("StrProductIDRenta");

            var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
            var IdPersonalizado = that.GeneratedCorrelatorId();
            var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

            var varprovisionarSuscripcionSNRequest = {
                operatorProvisioningProductRequest: {

                    partnerID: StrPartnerID,
                    productID: StrProductIDRenta,
                    level: '1',
                    operatorUser: {
                        operatorUserID: Linea,
                        providerUserID: that.CustomerClaroVideo,
                        subProductID: " ",
                        description: " "
                    },
                    countryID: "PER",
                    extensionInfo: [
            {
                key: "CUSTOMERID",
                value: that.CustomerClaroVideo
            },
            {
                key: "EXECUTIONTYPE",
                value: "3"
            },
            {
                key: "CONTENTID",
                value: data.strRentailID
            },
            {
                key: "region",
                value: "PE"
            },
            {
                key: "user_type",
                value: GetKeyConfig("strUsertype")
            },
            {
                key: "payment_method",
                value: that.strPaymentMethod
            }
                    ]

                }
            };

            var objProvisionSubscripRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { provisionarSuscripcionSNRequest: varprovisionarSuscripcionSNRequest }
                }
            }

            console.log('Request ProvisionSubscription');
            console.log(objProvisionSubscripRequest);

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Fixed/ClaroVideo/ProvisionSubscription',
                data: JSON.stringify(objProvisionSubscripRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {

                    //falta implementar si existe o no
                    console.log('Response ProvisionSubscription Renta');
                    console.log(response.data);

                    if (response.data.operatorProvisioningProductResponse != null) {
                        if (response.data.operatorProvisioningProductResponse.result != null) {
                            if (response.data.operatorProvisioningProductResponse.result.resultCode == "0") {
                                console.log('exito');
                                CantOK = CantOK + 1;

                            } else if (response.data.operatorProvisioningProductResponse.result.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                var Mensaje = response.data.operatorProvisioningProductResponse.result.resultMessage;

                                that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                    if (flag != '') {
                                        if (that.StrMensajePersonalizado.MensajeError != '') {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                        } else {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                        }
                                    }
                                });

                                //callback(0);
                            }
                        }
                    }
                },
                error: function (msger) {
                    console.log(msger);

                }
            });

            if (CantOK > 0) {
                callback(1);
            } else {
                callback(0);
            }

        },
        GenerarActualizacionEmail: function (isGenerarActualizacion, callback) {

            var that = this,
                 controls = that.getControls();

            var flagIsClientClarovideo = that.flagIsClientClarovideo;


            var NuevoCorreo = controls.txtCambiarCorreo.val().trim();
            if (NuevoCorreo != "" || NuevoCorreo != undefined) {

                if (isGenerarActualizacion == '1' && flagIsClientClarovideo == true) {

                    var StrPartnerID = GetKeyConfig("strPartnerIDactualizarClienteSN");
                    var CorrelatorId = that.GeneratedCorrelatorId();
                    var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

                    var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
                    var IdPersonalizado = that.GeneratedCorrelatorId();
                    var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;

                    var arrReq = {
                        invokeMethod: 'modificaremail',
                        correlatorId: providerCorrelatorId,
                        countryId: 'PE',
                        userId: 'AMCO',
                        deviceId: " ",
                        newEmail: NuevoCorreo,
                        employeeId: GetKeyConfig("strEmployeeId"),
                        origin: 'SIAC',
                        serviceName: 'updateUserOtt',
                        providerId: StrPartnerID,
                        iccidManager: 'AMCO',
                        extensionInfo: [
                            { key: "CUSTOMERID", value: that.CustomerClaroVideo }
                        ]
                    };
                    var objUpdateUserOttRequest = {
                        strIdSession: Session.IDSESSION,
                        strEntityAddEmail: '1',
                        strEntityAdddeviceId: '0',
                        MessageRequest: {
                            Body: { updateUserOttRequest: arrReq }
                        }
                    };

                    console.log('Request UpdateClientSN - Actualizar Email')
                    console.log(objUpdateUserOttRequest);

                    // controls.btnbuscar.button('loading');
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        async: false,
                        url: '/Transactions/Fixed/ClaroVideo/UpdateClientSN',
                        data: JSON.stringify(objUpdateUserOttRequest),
                        complete: function () {
                            //controls.btnbuscar.button('reset');
                        },
                        success: function (response) {

                            //falta implementar si existe o no
                            console.log('Response UpdateClientSN - Actualizar Email');
                            console.log(response.data);

                            if (response.data.updateUserOttResponse != null) {
                                if (response.data.updateUserOttResponse.resultCode == "0") {

                                    that.ControlError.StrFlagOKAcciones = true;

                                    controls.txtCambiarCorreo.attr('disabled', true);
                                    controls.txtCambiarCorreo.val('');
                                    $(controls.chkChangeEmail).prop('checked', false);

                                    console.log('GenerarActualizacionEmail exito');
                                    that.strCorreoClaro = NuevoCorreo;
                                    callback(true);

                                } else if (response.data.updateUserOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                    var Mensaje = response.data.updateUserOttResponse.resultMessage;

                                    that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                        if (flag != '') {
                                            if (that.StrMensajePersonalizado.MensajeError != '') {
                                                that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                            } else {
                                                that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                            }
                                        }
                                        that.ControlError.StrFlagFailedAcciones = true;
                                        callback(false);

                                    });
                                }
                                else {
                                    that.ControlError.StrFlagFailedAcciones = true;
                                    callback(false);
                                }
                            } else {
                                that.ControlError.StrFlagFailedAcciones = true;
                                callback(false);
                            }
                        },
                        error: function (msger) {
                            that.ControlError.StrFlagFailedAcciones = true;
                            console.log(msger);
                            callback(false);
                        }
                    });
                } else {
                    callback(false);
                }
            } else {
                callback(false);
            }
        },
        GeneratedCorrelatorId: function () {
            var that = this;

            var fecha = new Date();
            var yyyy = fecha.getFullYear().toString();
            var MM = that.pad(fecha.getMonth() + 1, 2);
            var dd = that.pad(fecha.getDate(), 2);
            var hh = that.pad(fecha.getHours(), 2);
            var mm = that.pad(fecha.getMinutes(), 2);
            var ss = that.pad(fecha.getSeconds(), 2);
            return yyyy + MM + dd + hh + mm + ss;
        },
        pad: function (number, length) {
            var str = '' + number;
            while (str.length < length) {
                str = '0' + str;
            }
            return str;
        },
        GenerarActualizacionPassword: function (isGenerarActualizacion, callback) {
            // falta  request 
            var that = this,
                controls = that.getControls();

            var flagIsClientClarovideo = that.flagIsClientClarovideo;

            if (isGenerarActualizacion == '1' && flagIsClientClarovideo == true) {

                var Email = controls.txtCorreoelectronico.val().trim();

                var StrPartnerID = GetKeyConfig("strPartnerIDactualizarClienteSN");
                var CorrelatorId = that.GeneratedCorrelatorId();
                var providerCorrelatorId = StrPartnerID + '' + CorrelatorId;

                var StrKeyPersonalizado = GetKeyConfig("strKeyPersonalizado");
                var IdPersonalizado = that.GeneratedCorrelatorId();
                var CorrelatorIdPersonalizado = StrKeyPersonalizado + '' + IdPersonalizado;


                var arrReq = {
                    invokeMethod: 'modificaremail',
                    correlatorId: providerCorrelatorId,
                    countryId: 'PE',
                    userId: 'AMCO',
                    deviceId: null,
                    newEmail: null,
                    employeeId: GetKeyConfig("strEmployeeId"),
                    origin: 'SIAC',
                    serviceName: 'updateUserOtt',
                    providerId: StrPartnerID,
                    iccidManager: 'AMCO',
                    extensionInfo: [
                        {
                            key: "CUSTOMERID",
                            value: that.CustomerClaroVideo
                        },
                         {
                             key: "RECOVERYPASSWORD",
                             value: "true"
                         }
                    ]
                };
                var objUpdateUserOttRequest = {
                    strIdSession: Session.IDSESSION,
                    strEntityAddEmail: '0',
                    strEntityAdddeviceId: '0',
                    MessageRequest: {
                        Body: { updateUserOttRequest: arrReq }
                    }
                };

                // controls.btnbuscar.button('loading');
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    url: '/Transactions/Fixed/ClaroVideo/UpdateClientSN',
                    data: JSON.stringify(objUpdateUserOttRequest),
                    complete: function () {
                        //controls.btnbuscar.button('reset');
                    },
                    success: function (response) {

                        //falta implementar si existe o no
                        console.log('GenerarActualizacionPassword');
                        console.log(response);

                        if (response.data.updateUserOttResponse != null) {
                            if (response.data.updateUserOttResponse.resultCode == "0") {

                                that.ControlError.StrFlagOKAcciones = true;

                                $(controls.chkChangePassword).prop('checked', false);
                                $(controls.lblMessagePass)[0].innerHTML = "";
                                callback(true);
                                console.log('GenerarActualizacionPassword exito');
                            } else if (response.data.updateUserOttResponse.resultCode == GetKeyConfig("strResultCodePersonalizado")) {

                                var Mensaje = response.data.updateUserOttResponse.resultMessage;

                                that.getPersonalizaMensajeOTT(CorrelatorIdPersonalizado, Mensaje, function (flag) {

                                    if (flag != '') {
                                        if (that.StrMensajePersonalizado.MensajeError != '') {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + ', ' + flag;
                                        } else {
                                            that.StrMensajePersonalizado.MensajeError = that.StrMensajePersonalizado.MensajeError + '' + flag;
                                        }
                                    }

                                    console.log('GenerarActualizacionPassword error');
                                    that.ControlError.StrFlagFailedAcciones = true;
                                    callback(false);

                                });
                            }
                            else {
                                console.log('GenerarActualizacionPassword error');
                                that.ControlError.StrFlagFailedAcciones = true;
                                callback(false);
                            }
                        } else {
                            that.ControlError.StrFlagFailedAcciones = true;
                            console.log('GenerarActualizacionPassword error');
                            callback(false);
                        }

                    },
                    error: function (msger) {
                        that.ControlError.StrFlagFailedAcciones = true;
                        console.log('GenerarActualizacionPassword error: ' + msger);
                        callback(false);
                    }
                });
            } else {
                callback(false);
            }
        },
        RegisterControlsClaroVideo: function (dataControl, callback) {

            var that = this;
            var arrReq = {
                transaccionId: dataControl.transaccionId,
                flagTransaccion: '1',
                tipoTransaccion: dataControl.tipoTransaccion,
                documentoVenta: dataControl.transaccionId,
                nombreAplicacion: "SIACUNICO",
                operacionSuscripcion: dataControl.operacionSuscripcion, //'Agregar Suscripcion',
                nombreServicio: dataControl.nombreServicio, //'HBO',
                nombrePdv: dataControl.nombrePdv,  //'CAC Begonias',
                custormerId: dataControl.custormerId, // '222567',
                linea: dataControl.linea, // '934567345',
                estadoTransaccion: dataControl.estadoTransaccion,//'exitoso',
                mensajeTransaccion: dataControl.mensajeTransaccion //'Mensaje de la Api servicio EAI'
            };

            var objRegistrarControlesRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { registrarControlesCvRequest: arrReq }
                }
            };

            console.log('Request RegisterControlesCV');
            console.log(objRegistrarControlesRequest);

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Fixed/ClaroVideo/RegisterControlesCV', // quitar el 1 
                data: JSON.stringify(objRegistrarControlesRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {

                    //falta implementar si existe o no
                    console.log('Response RegisterControlesCV');
                    console.log(response);

                    if (response.data.registrarcontrolescvresponse != null) {
                        if (response.data.registrarcontrolescvresponse.codRpta == "0") {
                            callback(true);
                            console.log('RegisterControlsClaroVideo exito');
                        }
                        else {
                            callback(false);
                        }
                    } else {

                        callback(false);
                    }

                },
                error: function (msger) {
                    console.log(msger);
                    callback(false);
                }
            });


        },
        loadChangesSuscription: function (suscription) {


            console.log('loadChangesSuscription');
            var that = this;
            var param = suscription.split(',');
            var isCheck = false;
            var newState = '', service = '', idSusc = '', precio = '', fechaalta = '', origen = '', Promocion = '', estado = '', IsClaroVideo = '0', ProductID = '', FlagPromocion = '0';

            isCheck = $('#chk_' + param[0]).is(':checked');
            console.log(param);
            console.log(isCheck);
            //that.dataListClaroVideo.listaCambios = [];

            if (isCheck) {
                //verificando estado inicial y obteniendo nuevo estado..
                if (param[1].toUpperCase() == 'DESACTIVADO') {
                    newState = 'ACTIVAR';
                }
                if (param[1].toUpperCase() == 'ACTIVADO') {
                    newState = 'DESACTIVAR';
                }

                service = param[2];
                idSusc = param[0];
                estado = param[1];
                precio = param[3];
                fechaalta = param[4];
                origen = param[5];
                Promocion = param[6];
                ProductID = param[7];
                FlagPromocion = param[8];

                var CodigoClaroVideo = GetKeyConfig("strCodigoSuscripcionClaroVideo");

                if (newState == 'ACTIVAR') {

                    var listaCambios = {
                        Metodo: "",
                        precio: "",
                        estadoPago: "",
                        estado: "",
                        fechaAlta: "",
                        origen: "",
                        idSubscription: "",
                        descripcion: "",
                        promocion: "",
                        flagProcesado: "",
                        IsClaroVideo: "",
                        ProductID: ""
                    }

                    //seteando array..
                    listaCambios.Metodo = "";
                    listaCambios.precio = precio;
                    listaCambios.estadoPago = newState;
                    listaCambios.estado = 'Activado';
                    listaCambios.fechaAlta = fechaalta;
                    listaCambios.origen = origen;
                    listaCambios.idSubscription = idSusc;
                    listaCambios.descripcion = service;
                    listaCambios.promocion = Promocion;
                    listaCambios.flagProcesado = "0";
                    listaCambios.ProductID = ProductID;

                    if (CodigoClaroVideo != null && CodigoClaroVideo != undefined) {
                        if (CodigoClaroVideo == idSusc) {
                            IsClaroVideo = "1";
                        }
                    }

                    listaCambios.IsClaroVideo = IsClaroVideo;

                    that.dataListClaroVideo.listaSuscripciones.push(listaCambios);

                    if (FlagPromocion == '1') {
                        var strFlagAlertPromocionCheck = GetKeyConfig("strFlagAlertPromocionCheck");
                        if (strFlagAlertPromocionCheck == '1') {

                            that.MostrarPromocionClaroVideo(Promocion);
                        }
                    }
                }

                if (newState == 'DESACTIVAR') {
                    var listaCambios = {
                        Metodo: "",
                        precio: "",
                        estadoPago: "",
                        estado: "",
                        fechaAlta: "",
                        origen: "",
                        idSubscription: "",
                        descripcion: "",
                        promocion: "",
                        flagProcesado: "",
                        IsClaroVideo: "",
                        ProductID: ""
                    }

                    listaCambios.Metodo = "";
                    listaCambios.precio = precio;
                    listaCambios.estadoPago = newState;
                    listaCambios.estado = 'Desactivado';
                    listaCambios.fechaAlta = fechaalta;
                    listaCambios.origen = origen;
                    listaCambios.idSubscription = idSusc;
                    listaCambios.descripcion = service;
                    listaCambios.promocion = Promocion;
                    listaCambios.flagProcesado = "0";
                    listaCambios.ProductID = ProductID;

                    if (CodigoClaroVideo != null && CodigoClaroVideo != undefined) {
                        if (CodigoClaroVideo == idSusc) {
                            IsClaroVideo = "1";
                        }
                    }

                    listaCambios.IsClaroVideo = IsClaroVideo;

                    that.dataListClaroVideo.listaCancelaciones.push(listaCambios);
                }

            } else {
                service = param[2];
                that.dataListClaroVideo.listaSuscripciones = $.grep(that.dataListClaroVideo.listaSuscripciones, function (data) {
                    if (data.descripcion != service) {
                        return true;

                    }

                });

                that.dataListClaroVideo.listaCancelaciones = $.grep(that.dataListClaroVideo.listaCancelaciones, function (data) {
                    if (data.descripcion != service) {
                        return true;

                    }

                });
            }
            var hash = {};
            that.dataListClaroVideo.listaSuscripciones = that.dataListClaroVideo.listaSuscripciones.filter(function (current) {
                let go = current.descripcion !== undefined ? String(current.Metodo) + String(current.precio) + String(current.estadoPago) + String(current.fechaAlta) + String(current.origen) + String(current.idSubscription) + String(current.descripcion) + String(current.promocion) : String(current.promocion);

                let exists = !hash[go] || false;

                hash[go] = true;

                return exists;
            });

            that.dataListClaroVideo.listaCancelaciones = that.dataListClaroVideo.listaCancelaciones.filter(function (current) {
                let go = current.descripcion !== undefined ? String(current.Metodo) + String(current.precio) + String(current.estadoPago) + String(current.fechaAlta) + String(current.origen) + String(current.idSubscription) + String(current.descripcion) + String(current.promocion) : String(current.promocion);

                let exists = !hash[go] || false;

                hash[go] = true;

                return exists;
            });

            //MODIFICADO IPTV 1 PLAY
            console.log('Cantidad de Suscripciones : ', that.dataListClaroVideo.listaSuscripciones.length)
            console.log('Cantidad de Cancelaciones : ', that.dataListClaroVideo.listaCancelaciones.length)
            if (that.dataListClaroVideo.listaSuscripciones.length > 0 || that.dataListClaroVideo.listaCancelaciones.length > 0) {
                $("#btnSave").prop('disabled', false);
                //INICIATIVA-794
                $("#btnSave").addClass("btn btn-info active");
            } else if (that.dataListClaroVideo.listaSuscripciones.length == 0 && that.dataListClaroVideo.listaCancelaciones.length == 0) {
                $("#btnSave").prop('disabled', true);
            }

            console.log('Acciones a suscribir');
            console.log(that.dataListClaroVideo.listaSuscripciones);
            console.log('Acciones a cancelar');
            console.log(that.dataListClaroVideo.listaCancelaciones);
        },
        CargarDatosCliente: function () {

            var that = this,
             controls = that.getControls();

            var oCustomer = null;
            var oService = null;
            that.TypeLine = "";
            that.strPaymentMethod = "";
            that.strExecutiontype = "";
            that.strFlagTipoLinea = "";

            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;


                oCustomer.Telephone = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                 (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                 : oCustomer.Telephone);

                var flagIsClientClarovideo = that.flagIsClientClarovideo;

                // SI NO ES CLIENTE CLARO VIDEO
                if (!flagIsClientClarovideo) {
                    controls.txtCorreoelectronico.prop('disabled', false);
                    controls.txtCorreoelectronico.val(oCustomer.Email);

                    const regex = /,/gi;

                    var strNombre = oCustomer.Name;

                    if (strNombre != null) {
                        strNombre = strNombre.replace(regex, '');
                    }

                    controls.txtNombre.val(strNombre);

                    var apellido = '';
                    if (oCustomer.LastName != null && oCustomer.LastName != undefined) {
                        apellido = oCustomer.LastName;
                    } else if (oCustomer.Lastname != null && oCustomer.Lastname != undefined) {
                        apellido = oCustomer.Lastname;
                    }

                    if (apellido != null) {
                        apellido = apellido.replace(regex, '');
                    }

                    controls.txtApellido.val(apellido);

                    $('#PanelRegistrar').show();
                    controls.btnRegistrar.prop('disabled', false);
                    $(controls.PanelDesplegables).hide();

                    if (controls.txtCorreoelectronico.isEmptyObject != true) {
                        controls.txtSendforEmail.val(oCustomer.Email);
                        $(controls.chkSentEmail).prop('checked', true);
                    }

                } else {

                    controls.txtCorreoelectronico.prop('disabled', true);
                    $('#PanelRegistrar').hide();
                    $(controls.PanelDesplegables).removeClass("hide");
                    $(controls.PanelDesplegables).show();
                }


                var strTMCOD = '0';
                var CustomerIdBSCS = '0';

                if (oCustomer.Application != undefined) {

                    oService = SessionTransac.SessionParams.DATASERVICE;


                    if (oCustomer.Application == 'PREPAID' || oCustomer.Application == 'POSTPAID') {
                        that.strFlagTipoLinea = "1";
                        that.strPaymentMethod = "1";
                        that.strExecutiontype = "2";
                        controls.txtLinea.val(oCustomer.Telephone); //controls.txtLinea.val('914669248');  
                    } else {
                        that.strPaymentMethod = "2";
                        that.strExecutiontype = "1";
                        that.strFlagTipoLinea = "2";
                        controls.txtLinea.val(oCustomer.ContractID);
                    }

                    if (oCustomer.Application == 'PREPAID') {
                        that.TypeLine = 'PREPAGO';

                        if (oService.PlanTariff != null && oService.PlanTariff != undefined) {
                            strTMCOD = oService.PlanTariff;
                        }

                    } else if (oCustomer.Application == 'POSTPAID') {
                        that.TypeLine = 'POSTPAGO';

                        if (oService.CodePlanTariff != null && oService.CodePlanTariff != undefined) {
                            strTMCOD = oService.CodePlanTariff;
                        }

                    } else if (oCustomer.Application == 'FTTH') { //VALIDAR TMDOC

                        if (oService.CodePlanTariff != null && oService.CodePlanTariff != undefined) {
                            strTMCOD = oService.CodePlanTariff;
                        }

                        that.TypeLine = 'FIJA';

                    } else if (oCustomer.Application == 'HFC') {

                        if (oService.CodePlanTariff != null && oService.CodePlanTariff != undefined) {
                            strTMCOD = oService.CodePlanTariff;
                        }

                        that.TypeLine = 'FIJA';
                    }
                }


                if (oCustomer.CustomerID != null && oCustomer.CustomerID != undefined) {
                    CustomerIdBSCS = oCustomer.CustomerID;
                } else if (oCustomer.CustomerId != null && oCustomer.CustomerId != undefined) {
                    CustomerIdBSCS = oCustomer.CustomerId;
                }

                that.CustomerIdBSCS = CustomerIdBSCS;
                console.log('customerID BSCS: ' + that.CustomerIdBSCS);

                controls.hdTmcode.val(strTMCOD);
            }

        },
        TypeLine: "",
        CustomerIdBSCS: "0",
        CustomerClaroVideo: "0", // Codigo del usuario de claro video
        getCACDAC: function () {

            var that = this,
            controls = that.getControls();

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = SessionTransac.SessionParams.USERACCESS.login;

            var objCacDacType = {
                strIdSession: Session.IDSESSION
            };

            console.log('Request getCACDAC');
            console.log(parameters);

            that.Array_ListaCAC.ListaCac = [];

            var cacdac = '';


            $(controls.cboPuntoVenta).empty();

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                async: false,
                url: '/Transactions/CommonServices/GetUsers',

                success: function (results) {

                    console.log('Response getCACDAC');
                    console.log(results);

                    if (($.isEmptyObject(results)) == false) {
                        if (results != null) {
                            cacdac = results.Cac;
                        }
                    }

                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        async: false,
                        data: JSON.stringify(objCacDacType),
                        url: '/Transactions/CommonServices/GetCacDacType',
                        success: function (response) {

                            console.log('Response GetCacDacType');
                            console.log(response);

                            //$("#cboPuntoVenta").append($('<option>', { value: '', text: 'Seleccionar' }));
                            if (response.data != null) { }
                            var itemSelect;
                            $.each(response.data.CacDacTypes, function (index, value) {
                                if (cacdac === value.Description) {

                                    controls.cboPuntoVenta.append($('<option>', { value: value.Description, text: value.Description }));
                                    itemSelect = value.Description;

                                } else {
                                    controls.cboPuntoVenta.append($('<option>', { value: value.Description, text: value.Description }));
                                }
                            });

                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                controls.cboPuntoVenta.val(itemSelect);
                            }
                        }
                    });

                },
                error: function (msger) {
                    console.log('Error getCACDAC ' + msger);
                }

            });

        },
        OpenHistoryActivateService: function (descripcion) {
            var that = this,
            controls = that.getControls();

            $('html, body').animate({ scrollTop: 0 }, 'slow');
            var Linea = that.ObtenerLineaCliente('Linea'); // CAMBIO PENDIENTE

            $.window.open({
                modal: true,
                title: "Historial de Activacion de " + descripcion,
                id: 'divmodalHistoryActivateService',
                url: '/Transactions/Fixed/ClaroVideo/HistoryActivateService',
                data: { strDescripcion: descripcion, strSession: Session.IDSESSION, strMobile: Linea },
                type: 'POST',
                width: '900px',
                height: '450px',
                minimizeBox: false,
                maximizeBox: false,
                buttons: {
                    Cerrar: {
                        id: 'btnCerrarHistoryActivateService',
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        OpenHistoryVisualization: function () {

            var that = this,
            controls = that.getControls();
            var idRefSuscripcionHistory = that.stridRefSuscripcionHistory;

            console.log('OpenHistoryVisualization: ' + idRefSuscripcionHistory);

            $('html, body').animate({ scrollTop: 0 }, 'slow');

            $.window.open({
                modal: true,
                title: "Historial de Visualización del Cliente",
                id: 'divmodalOpenHistoryVisualization',
                url: '/Transactions/Fixed/ClaroVideo/HistoryVisualizationClient',
                data: { stridRefSuscripcionHistory: idRefSuscripcionHistory, strCustomerID: that.CustomerClaroVideo },
                type: 'POST',
                width: '1100px',
                height: '700px',
                minimizeBox: false,
                maximizeBox: false,
                buttons: {
                    Cerrar: {
                        id: 'btnCerrarOpenHistoryVisualization',
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        OpenHistoryRentalUser: function (idPackage) {

            var that = this,
            controls = that.getControls();
            $('html, body').animate({ scrollTop: 0 }, 'slow');

            $.window.open({
                modal: true,
                title: "Historial de Alquileres del Usuario",
                id: 'divmodalOpenHistoryRentalUser',
                url: '/Transactions/Fixed/ClaroVideo/HistoryRentalUser',
                data: { strCustomerID: that.CustomerClaroVideo },
                type: 'POST',
                width: '1000px',
                height: '550px',
                minimizeBox: false,
                maximizeBox: false,
                buttons: {
                    Cerrar: {
                        id: 'btnCerrarOpenHistoryRentalUser',
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });

        },
        Array_ListaCAC:
            {
                ListaCac: []
            },
        getTipificacionInfo: function (TipoInteraccion) {
            var that = this,
            controls = that.getControls();
            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

            var tipoCliente = "";

            if (TipoInteraccion != '' && TipoInteraccion != null) {
                if (oCustomer.Application != undefined) {

                    console.log('Evaluando getTipificacionInfo: ' + TipoInteraccion);
                    console.log('Evaluando oCustomer.Application : ' + that.AplicationAmco);

                    switch (TipoInteraccion) {
                        case GetKeyConfig("strNomTransaSuscripcion"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCACTIVARSUSCRIPCION").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREACTIVARSUSCRIPCION").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTACTIVARSUSCRIPCION").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaSusAdicional"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCSUSCRIPCIONCANALADIC").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPRESUSCRIPCIONCANALADIC").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTSUSCRIPCIONCANALADIC").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaSusDesvDispositivo"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCDESVINCULARDISPOSI").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREDESVINCULARDISPOSI").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTDESVINCULARDISPOSI").split("|");
                            }


                            break;
                        case GetKeyConfig("strNomTransaSusCancelacion"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCDESACTIVARSUSCRIPC").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREDESACTIVARSUSCRIPC").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTDESACTIVARSUSCRIPC").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaSusCambioCorreo"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCACTUALIZACIONCORREO").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREACTUALIZACIONCORREO").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTACTUALIZACIONCORREO").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaSusCambioPass"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCACTUALIZACIONCONTRA").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREACTUALIZACIONCONTRA").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTACTUALIZACIONCONTRA").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaSuscripcionRentaPelicula"):

                            if (oCustomer.Application == 'HFC' || oCustomer.Application == 'FTTH') {
                                tipoCliente = GetKeyConfig("strHFCREACTIVACIONPELICULA").split("|");
                            } else if (oCustomer.Application == 'PREPAID') {
                                tipoCliente = GetKeyConfig("strPREREACTIVACIONPELICULA").split("|");
                            } else if (oCustomer.Application == 'POSTPAID') {
                                tipoCliente = GetKeyConfig("strPOSTREACTIVACIONPELICULA").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaConsultaClaroVideo"):
                            if (that.AplicationAmco.toUpperCase() == 'HFC') {
                                tipoCliente = GetKeyConfig("strHFCCONSULTACORREO").split("|");
                            } else if (that.AplicationAmco.toUpperCase() == 'PREPAGO') {//prepago
                                tipoCliente = GetKeyConfig("strPRECONSULTACORREO").split("|");
                            } else if (that.AplicationAmco.toUpperCase() == 'POSTPAGO') {//postpago
                                tipoCliente = GetKeyConfig("strPOSTCONSULTACORREO").split("|");
                            }

                            break;
                        case GetKeyConfig("strNomTransaEliminarClaroVideo"):
                            if (that.AplicationAmco.toUpperCase() == 'HFC') {
                                tipoCliente = GetKeyConfig("strHFCELIMINARCORREO").split("|");
                            } else if (that.AplicationAmco.toUpperCase() == 'PREPAGO') {//prepago
                                tipoCliente = GetKeyConfig("strPREELIMINARCORREO").split("|");
                            } else if (that.AplicationAmco.toUpperCase() == 'POSTPAGO') {//postpago
                                tipoCliente = GetKeyConfig("strPOSTELIMINARCORREO").split("|");
                            }

                            break;
                        default:
                            console.log('El tipo de interaccion no esta definido ' + TipoInteraccion);
                    }
                }
            }

            return tipoCliente;
        },

        GenerarInteraccion: function (TipoInteraccion) {
            var that = this,
            controls = that.getControls();

            var idInteraccion = '0';

            var strClaseInfo = that.getTipificacionInfo(TipoInteraccion);
            var strCustomerTelefono = '';

            var oCustomer = null;
            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {
                oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
            }

            if (SessionTransac.SessionParams.DATACUSTOMER.Application == 'HFC' || SessionTransac.SessionParams.DATACUSTOMER.Application == 'FTTH') {

                if (oCustomer.CustomerID != null && oCustomer.CustomerID != undefined) {
                    strCustomerTelefono = 'H' + SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
                } else if (oCustomer.CustomerId != null && oCustomer.CustomerId != undefined) {
                    strCustomerTelefono = 'H' + SessionTransac.SessionParams.DATACUSTOMER.CustomerId;
                }

            } else {
                strCustomerTelefono = controls.txtLinea.val().trim();
            }
            var fecha = new Date();
            var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();
            var AddNote = SessionTransac.SessionParams.USERACCESS.fullName + ' ' + FechaRegistro;

            //strCustomerTelefono = '987010501'; prueba

            var oModel = {
                strIdSession: Session.IDSESSION,
                objIdContacto: '0',
                Type: strClaseInfo[0].split(",")[0],
                Class: strClaseInfo[0].split(",")[1],
                SubClass: strClaseInfo[0].split(",")[2],
                Note: AddNote + ', ' + controls.txtNote.val(),
                CustomerId: strCustomerTelefono,
                Plan: '',
                ContractId: '',
                CurrentUser: SessionTransac.SessionParams.USERACCESS.login == null ? GetKeyConfig('strLeyPromoDefaultCodigoEmpleadoInterac') : SessionTransac.SessionParams.USERACCESS.login,
                objIdSite: '0',
                Cuenta: ''
            };

            console.log('Request GenerarTipificacion');
            console.log(oModel);

            $.app.ajax({
                type: 'POST',
                async: false,
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/ClaroVideo/GenerarTipificacion',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin GenerarTipificacion");
                },
                success: function (response) {
                    console.log('response GenerarTipificacion');
                    console.log(response);

                    if (response.codeResponse == "0") {
                        idInteraccion = response.idInteraction;

                    } else {
                        idInteraccion = "0";

                    }
                },
                error: function (msger) {
                    console.log(msger);
                }
            });

            return idInteraccion;
        },
        strRutaPDF: "",
        strPaymentMethod: "",
        strFlagTipoLinea: "",
        strExecutiontype: "",
        EnvioCorreoClaroVideo: function (Remitente, Destinatario, Asunto, Mensaje, HTMLFlag, FullPathPDF) {
            var that = this;
            console.log("Inicio GetSendEmailSBClaroVideo");

            var MensajeFormat = Mensaje;

            var oModel = {
                srtIdSession: Session.IDSESSION,
                strRemitente: Remitente,
                strDestinatario: Destinatario,
                strAsunto: Asunto,
                strMensaje: MensajeFormat,
                strHTMLFlag: HTMLFlag,
                strFullPathPDF: FullPathPDF
            };

            console.log('Request EnvioCorreoClaroVideo');
            console.log(oModel);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: that.strUrl + '/Transactions/Fixed/ClaroVideo/GetSendEmailSBClaroVideo',
                data: JSON.stringify(oModel),
                complete: function () {
                    console.log("fin GetSendEmailSBClaroVideo");
                },
                success: function (response) {

                    console.log('Response EnvioCorreoClaroVideo');
                    console.log(response);

                    if (response.codigoRespuesta != null) {
                        if (response.codigoRespuesta == 0) {
                            console.log("correo enviado");
                        } else {
                            console.log("correo no enviado");
                        }
                    }
                    else {
                        console.log("correo no enviado");
                    }
                },
                error: function (msger) {
                    console.log("correo no enviado");
                }
            });
        },
        btnClose_click: function () {
            window.close();
        },
        GenerarInteraccionPlus: function (objParametersGenerateContancy, codeOnBase) {

            var that = this,
                controls = that.getControls();
            var onRequest = null;
            var VarSoles = GetKeyConfig("strKeySoles");
            var fecha = new Date();
            var FechaRegistro = AboveZero(fecha.getDate()) + "/" + AboveZero(fecha.getMonth() + 1) + "/" + fecha.getFullYear() + " " + +fecha.getHours() + ':' + fecha.getMinutes() + ":" + fecha.getMilliseconds();
            var AddNote = SessionTransac.SessionParams.USERACCESS.fullName + ' ' + FechaRegistro;

            var Nota = AddNote + ', ' + controls.txtNote.val().trim();

            if (objParametersGenerateContancy.StrNombreArchivoTransaccion != '' && objParametersGenerateContancy.StrNombreArchivoTransaccion != null) {

                console.log('Evaluando GenerarInteraccionPlus: ' + objParametersGenerateContancy.StrNombreArchivoTransaccion);

                switch (objParametersGenerateContancy.StrNombreArchivoTransaccion) {
                    case GetKeyConfig("strNomTransaSuscripcion"):

                        var Precio = '';
                        var FechaRegistro = '';
                        var Promocion = '';
                        var Servicio = '';
                        var ProductID = '';
                        var Estado = '';

                        if ($.isEmptyObject(objParametersGenerateContancy.ListSuscriptcion) == false) {

                            if (objParametersGenerateContancy.ListSuscriptcion.length > 0) {
                                $.each(objParametersGenerateContancy.ListSuscriptcion, function (index, value) {

                                    Precio = value.strSuscPrecio;
                                    FechaRegistro = value.strSuscFechReg;
                                    Promocion = value.strSuscPeriodo;
                                    Servicio = value.strSuscTitulo;
                                    ProductID = value.strProductID;
                                    Estado = value.strSuscEstado

                                });
                            }

                            onRequest = {
                                strIdSession: Session.IDSESSION,
                                template: {
                                    _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                    _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                    _X_ADDRESS5: Promocion,
                                    _X_EMAIL: objParametersGenerateContancy.strEmail,
                                    _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                    _X_INTER_29: Nota,
                                    _X_INTER_3: Servicio,
                                    _X_INTER_6: new Date(),
                                    _X_INTER_16: Precio,
                                    _X_INTER_17: FechaRegistro,
                                    _X_INTER_7: codeOnBase,
                                    _X_ADDRESS: ProductID,
                                    _X_INTER_15: Estado

                                }
                            };
                        }


                        break;

                    case GetKeyConfig("strNomTransaSusAdicional"):


                        var ListaServiciosAdicionales = '';
                        var ProductID = '';
                        if ($.isEmptyObject(objParametersGenerateContancy.ListSuscriptcionAdicionales) == false) {

                            if (objParametersGenerateContancy.ListSuscriptcionAdicionales.length > 0) {
                                $.each(objParametersGenerateContancy.ListSuscriptcionAdicionales, function (index, value) {

                                    var trInicio = '<tr>';
                                    var trFin = '</tr>';

                                    var ColumIndex = '<span class=Arial11BV>' + (index + 1) + '</span>';
                                    var ColumstrSuscTitulo = '<span class=Arial11BV>' + value.strSuscTitulo + '</span>';
                                    var ColumstrSuscEstado = '<span class=Arial11BV>' + value.strSuscEstado + '</span>';
                                    var ColumstrSuscPrecio = '<span class=Arial11BV>' + value.strSuscPrecio + '</span>';
                                    var ColumstrSuscFechReg = '<span class=Arial11BV>' + value.strSuscFechReg + '</span>';
                                    var ColumstrSuscServicio = '<span class=Arial11BV>' + value.strSuscServicio + '</span>';


                                    var Columnas = '<td>' + ColumIndex + '</td><td>' + ColumstrSuscTitulo + '</td><td>' + ColumstrSuscEstado + '</td><td> ' + ColumstrSuscPrecio + ' </td><td>' + ColumstrSuscFechReg + '</td><td>' + ColumstrSuscServicio + '</td>';

                                    ListaServiciosAdicionales = ListaServiciosAdicionales + trInicio + '' + Columnas + '' + trFin;

                                    if (index == 0) {
                                        ProductID = value.strProductID;
                                    } else {
                                        ProductID = ProductID + '|' + value.strProductID;
                                    }

                                });
                            }

                            console.log('ListaServiciosAdicionales detalle interaccion');
                            console.log(ListaServiciosAdicionales);

                            onRequest = {
                                strIdSession: Session.IDSESSION,
                                template: {
                                    _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                    _X_INTER_5: ListaServiciosAdicionales,
                                    _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                    _X_EMAIL: objParametersGenerateContancy.strEmail,
                                    _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                    _X_INTER_29: Nota,
                                    _X_INTER_6: new Date(),
                                    _X_INTER_7: codeOnBase,
                                    _X_ADDRESS: ProductID
                                }
                            };
                        }

                        break;
                    case GetKeyConfig("strNomTransaSusDesvDispositivo"):



                        var ListaDispositivosDesvinculados = '';
                        if ($.isEmptyObject(objParametersGenerateContancy.ListDevice) == false) {

                            if (objParametersGenerateContancy.ListDevice.length > 0) {
                                $.each(objParametersGenerateContancy.ListDevice, function (index, value) {

                                    var trInicio = '<tr>';
                                    var trFin = '</tr>';

                                    var ColumIndex = '<span class=Arial11BV>' + (index + 1) + '</span>';
                                    var ColumstrTipoDispositivo = '<span class=Arial11BV>' + value.strTipoDispositivo + '</span>';
                                    var ColumstrDispotisitivoNom = '<span class=Arial11BV>' + value.strDispotisitivoNom + '</span>';
                                    var ColumstrDispotisitivoID = '<span class=Arial11BV>' + value.strDispotisitivoID + '</span>';
                                    var ColumstrSuscFechReg = '<span class=Arial11BV>' + value.strSuscFechReg + '</span>';


                                    var Columnas = '<td>' + ColumIndex + '</td><td>' + ColumstrTipoDispositivo + '</td><td>' + ColumstrDispotisitivoNom + '</td><td>' + ColumstrDispotisitivoID + '</td><td>' + ColumstrSuscFechReg + '</td>';

                                    ListaDispositivosDesvinculados = ListaDispositivosDesvinculados + trInicio + '' + Columnas + '' + trFin;
                                });
                            }

                            console.log('ListaDispositivosDesvinculados detalle interaccion');
                            console.log(ListaDispositivosDesvinculados);

                            onRequest = {
                                strIdSession: Session.IDSESSION,
                                template: {
                                    _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                    _X_INTER_5: ListaDispositivosDesvinculados,
                                    _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                    _X_EMAIL: objParametersGenerateContancy.strEmail,
                                    _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                    _X_INTER_29: Nota,
                                    _X_INTER_6: new Date(),
                                    _X_INTER_7: codeOnBase
                                }
                            };
                        }

                        break;
                    case GetKeyConfig("strNomTransaSusCancelacion"):

                        var ListaServiciosCancelados = '';
                        var ProductID = '';
                        if ($.isEmptyObject(objParametersGenerateContancy.ListService) == false) {

                            if (objParametersGenerateContancy.ListService.length > 0) {
                                $.each(objParametersGenerateContancy.ListService, function (index, value) {

                                    var trInicio = '<tr>';
                                    var trFin = '</tr>';

                                    var ColumIndex = '<span class=Arial11BV>' + (index + 1) + '</span>';
                                    var ColumstrBajaServicios = '<span class=Arial11BV>' + value.strBajaServicios + '</span>';
                                    var ColumstrSuscEstado = '<span class=Arial11BV>' + (value.strSuscEstado == '' ? '&nbsp;' : value.strSuscEstado) + '</span>';
                                    var ColumstrSuscPrecio = '<span class=Arial11BV>' + (VarSoles + value.strSuscPrecio) + '</span>';
                                    var ColumststrSuscFechReg = '<span class=Arial11BV>' + value.strSuscFechReg + '</span>';
                                    var ColumstrSuscServicio = '<span class=Arial11BV>' + (value.strSuscServicio == '' ? '&nbsp;' : value.strSuscServicio) + '</span>';


                                    var Columnas = '<td>' + ColumIndex + '</td><td>' + ColumstrBajaServicios + '</td><td>' + ColumstrSuscEstado + '</td><td> ' + ColumstrSuscPrecio + ' </td><td>' + ColumststrSuscFechReg + '</td><td>' + ColumstrSuscServicio + '</td>';

                                    ListaServiciosCancelados = ListaServiciosCancelados + trInicio + '' + Columnas + '' + trFin;

                                    if (index == 0) {
                                        ProductID = value.strProductID;
                                    } else {
                                        ProductID = ProductID + '|' + value.strProductID;
                                    }

                                });
                            }

                            console.log('ListaServiciosCancelados detalle interaccion');
                            console.log(ListaServiciosCancelados);

                            onRequest = {
                                strIdSession: Session.IDSESSION,
                                template: {
                                    _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                    _X_INTER_5: ListaServiciosCancelados,
                                    _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                    _X_EMAIL: objParametersGenerateContancy.strEmail,
                                    _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                    _X_INTER_29: Nota,
                                    _X_INTER_6: new Date(),
                                    _X_INTER_7: codeOnBase,
                                    _X_ADDRESS: ProductID
                                }
                            };
                        }

                        break;
                    case GetKeyConfig("strNomTransaSusCambioCorreo"):

                        console.log('Cambio de  detalle interaccion');
                        onRequest = {
                            strIdSession: Session.IDSESSION,
                            template: {
                                _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                _X_INTER_5: objParametersGenerateContancy.strEmailAntiguo,
                                _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                _X_EMAIL: objParametersGenerateContancy.strEmail,
                                _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                _X_INTER_29: controls.txtNote.val(),
                                _X_INTER_6: new Date(),
                                _X_INTER_7: codeOnBase,
                                _X_ADDRESS: ''
                            }
                        };

                        break;
                    case GetKeyConfig("strNomTransaSusCambioPass"):

                        console.log('Cambio de Password detalle interaccion');

                        onRequest = {
                            strIdSession: Session.IDSESSION,
                            template: {
                                _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                _X_INTER_5: '',
                                _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                _X_EMAIL: objParametersGenerateContancy.strEmail,
                                _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                _X_INTER_29: Nota,
                                _X_INTER_6: new Date(),
                                _X_INTER_7: codeOnBase,
                                _X_ADDRESS: ''
                            }
                        };

                        break;
                    case GetKeyConfig("strNomTransaSuscripcionRentaPelicula"):

                        var ListaRentaPelicula = '';
                        if ($.isEmptyObject(objParametersGenerateContancy.ListSuscriptcionRentas) == false) {

                            if (objParametersGenerateContancy.ListSuscriptcionRentas.length > 0) {
                                $.each(objParametersGenerateContancy.ListSuscriptcionRentas, function (index, value) {

                                    var trInicio = '<tr>';
                                    var trFin = '</tr>';

                                    var ColumIndex = '<span class=Arial11BV>' + (index + 1) + '</span>';
                                    var ColumstrRentailNom = '<span class=Arial11BV>' + value.strRentailNom + '</span>';
                                    var ColumstrSuscEstado = '<span class=Arial11BV>' + (value.strSuscEstado == '' ? '&nbsp;' : value.strSuscEstado) + '</span>';
                                    var ColumstrSuscPrecio = '<span class=Arial11BV>' + (VarSoles + value.strSuscPrecio) + '</span>';
                                    var ColumststrSuscFechReg = '<span class=Arial11BV>' + value.strSuscFechReg + '</span>';
                                    var ColumstrSuscServicio = '<span class=Arial11BV>' + (value.strSuscServicio == '' ? '&nbsp;' : value.strSuscServicio) + '</span>';

                                    var Columnas = '<td>' + ColumIndex + '</td><td>' + ColumstrRentailNom + '</td><td>' + ColumstrSuscEstado + '</td><td> ' + ColumstrSuscPrecio + ' </td><td>' + ColumststrSuscFechReg + '</td><td>' + ColumstrSuscServicio + '</td>';

                                    ListaRentaPelicula = ListaRentaPelicula + trInicio + '' + Columnas + '' + trFin;
                                });
                            }

                            console.log('ListaRentaPelicula detalle interaccion');
                            console.log(ListaRentaPelicula);

                            onRequest = {
                                strIdSession: Session.IDSESSION,
                                template: {
                                    _ID_INTERACCION: objParametersGenerateContancy.strNroCaso,
                                    _X_INTER_5: ListaRentaPelicula,
                                    _X_REGISTRATION_REASON: objParametersGenerateContancy.StrNombreArchivoTransaccion,
                                    _X_EMAIL: objParametersGenerateContancy.strEmail,
                                    _X_INTER_4: objParametersGenerateContancy.strPuntoAtencion,
                                    _X_INTER_29: Nota,
                                    _X_INTER_6: new Date(),
                                    _X_INTER_7: codeOnBase
                                }
                            };
                        }

                        break;
                    default:
                        console.log('El tipo de interaccion no esta definido ' + TipoInteraccion);
                }
            }

            console.log('request  GenerarInteraccionPlus');
            console.log(onRequest);

            if (onRequest != null) {
                $.app.ajax({
                    type: 'POST',
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: that.strUrl + '/Transactions/Fixed/ClaroVideo/GenerarTipificacionPlus',
                    data: JSON.stringify(onRequest),
                    complete: function () {

                    },
                    success: function (response) {
                        console.log('response GenerarInteraccionPlus');
                        console.log(response);

                        if (response.codeResponse == "0") {
                            if (response.interaccionPlusId != "" && response.interaccionPlusId != "0") {
                                console.log('Se registro el detalle de la interaccion');
                            } else {
                                console.log('No se genero detalle de la interaccion');
                                that.ControlError.StrFlagFailedDetalleInteraccion = true;
                            }

                        } else {
                            that.ControlError.StrFlagFailedDetalleInteraccion = true;
                            console.log('No se genero detalle de la interaccion');
                        }
                    },
                    error: function (msger) {
                        that.ControlError.StrFlagFailedDetalleInteraccion = true;
                        console.log('response error GenerarInteraccionPlus:' + msger);

                    }
                });
            }
        },
        getPersonalizaMensajeOTT: function (CorrelatorId, MensajeAmco, callback) {

            var that = this,
            controls = that.getControls();

            var MensajePersonalizado = '';

            var varPersonalizaMensajeOTTRequest = {
                correlatorId: CorrelatorId,
                employeeId: GetKeyConfig("strEmployeeId"),
                mensajeAmco: MensajeAmco
            };

            var objPersonalizaMensajeOTTRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: { personalizarMensajeRequest: varPersonalizaMensajeOTTRequest }
                }
            }

            console.log('Request getPersonalizaMensajeOTT');
            console.log(objPersonalizaMensajeOTTRequest);

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/ClaroVideo/PersonalizaMensajeOTT',
                async: false,
                data: JSON.stringify(objPersonalizaMensajeOTTRequest),
                complete: function () {
                    //controls.btnbuscar.button('reset');
                },
                success: function (response) {
                    console.log('Response getPersonalizaMensajeOTT');
                    console.log(response.data);
                    if (response.data.PersonalizarMensajeResponse != null) {
                        if (response.data.PersonalizarMensajeResponse.codRpta == "0" || response.data.PersonalizarMensajeResponse.codRpta == "2") { //cambiar a 0 solo para pruebas
                            //response.data.PersonalizarMensajeResponse.msjRpta;                     
                            MensajePersonalizado = response.data.PersonalizarMensajeResponse.mensajePersonalizado;
                        }
                    }

                    callback(MensajePersonalizado);
                },
                error: function (msger) {
                    console.log(msger);
                    callback(MensajePersonalizado);
                }
            });
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        strUrl: window.location.protocol + '//' + window.location.host,
    };

    $.fn.SuscripcionClaroVideo = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = ['OpenHistoryRentalUser', 'OpenHistoryActivateService', 'MostrarPromocionClaroVideo', 'UnlinkDevice', 'loadChangesSuscription', 'UnlinkRentail'];

        this.each(function () {

            var $this = $(this),
                data = $this.data('SuscripcionClaroVideo'),
                options = $.extend({}, $.fn.SuscripcionClaroVideo.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('RecordEquipment', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {

                var timeReady = setInterval(function () {
                    if (!!$.fn.addEvent) {
                        clearInterval(timeReady);
                        data.init();
                    }
                }, 100);

                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };
    $.fn.SuscripcionClaroVideo.defaults = {}
    $('#ContentSuscripcionClaroVideo').SuscripcionClaroVideo();

})(jQuery);