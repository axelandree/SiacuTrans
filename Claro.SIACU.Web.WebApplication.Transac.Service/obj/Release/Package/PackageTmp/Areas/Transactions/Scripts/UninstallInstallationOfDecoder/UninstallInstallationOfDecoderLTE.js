(function ($) {
    var Form = function ($element, options) {
        $.extend(this, $.fn.UninstallInstallationOfDecoder.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,

            //Label
            lblIDContrato: $('#lblIDContrato', $element),
            lblIDCustomer: $('#lblIDCustomer', $element),
            lblTipoCliente: $('#lblTipoCliente', $element),
            lblCliente: $('#lblCliente', $element),
            lblContacto: $('#lblContacto', $element),
            lblDNIRUC: $('#lblDNIRUC', $element),
            lblRepresentanteLegal: $('#lblRepresentanteLegal', $element),
            lblIdentDocLegalRepresent: $('#lblIdentDocLegalRepresent', $element),
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
            lblCentroP: $('#lblCentroP', $element),
            lblDireccionInst: $('#lblDireccionInst', $element),
            lblReferenciaInst: $('#lblReferenciaInst', $element),
            lblPaisInst: $('#lblPaisInst', $element),
            lblDepartamentoInst: $('#lblDepartamentoInst', $element),
            lblProvinciaInst: $('#lblProvinciaInst', $element),
            lblDistritoInst: $('#lblDistritoInst', $element),
            lblCentroPInst: $('#lblCentroPInst', $element),
            lblCantidad: $('#lblCantidad', $element),
            lblCargoFijoTotalPlanSIGV: $('#lblCargoFijoTotalPlanSIGV', $element),
            lblCargoFijoTotalPlanCIGV: $('#lblCargoFijoTotalPlanCIGV', $element),
            lblMontoFidelizacion: $('#lblMontoFidelizacion', $element),
            lblCosto: $('#lblCosto', $element),
            lblTitle: $('#lblTitle', $element),
            lblTitle2: $('#lblTitle2', $element),
            lblNode: $("#lblNode", $element),
            lblNodeNew: $("#lblNodeNew", $element),
            lblErrorMessage: $('#lblErrorMessage', $element),
            lblCentroPoblado: $('#lblCentroPoblado', $element),
            lblCodUbigeo: $('#lblCodUbigeo', $element),
            lblPtosMax: $('#lblPtosMax', $element),
            lblPtosFree: $('#lblPtosFree', $element),
            lblPtosUse: $('#lblPtosUse',$element),

            lblCargoFijoPlanBaseSIGV: $('#lblCargoFijoPlanBaseSIGV', $element),
            lblCargoFijoPlanBaseCIGV: $('#lblCargoFijoPlanBaseCIGV', $element),
            lblCargoFijoAdicionalSIGV: $('#lblCargoFijoAdicionalSIGV', $element),
            lblCargoFijoAdicionalCIGV: $('#lblCargoFijoAdicionalCIGV', $element),

            //text
            txtNota: $('#txtNota', $element),
            txtFechaCompromiso: $("#txtFechaCompromiso", $element),
            txtEnviarporEmail: $("#txtEnviarporEmail", $element),

            //Botones
            btnConsultarEquipos: $('#btnConsultarEquipos', $element),
            btnGuardar: $('#btnGuardar', $element),
            btnCerrar01: $('#btnCerrar01', $element),
            btnCerrar02: $('#btnCerrar02', $element),
            btnConstancia: $('#btnConstancia', $element),
            btnValidarHorario: $('#btnValidarHorario', $element),
            btnSiguiente01: $('#btnSiguiente01', $element),
            btnSummary: $('#btnSummary', $element),

            //ComboBox
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

            //DIVS
            divRules: $('#divRules', $element),
            myModalLoad: $('#myModalLoad', $element)
        });
    }
    Form.prototype = {
        constructor: Form,
        init: function () {
            $('#content').attr('style', 'margin-top: 25px;');
            var that = this, controls = that.getControls();
            controls.chkEnviarPorEmail.addEvent(that, 'click', that.chkEnviarPorEmail_Click);
            controls.btnSiguiente01.addEvent(that, 'click', that.btnSummary_click);
            controls.btnSummary.addEvent(that, 'click', that.btnSummary_click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_Click);
            controls.btnConstancia.addEvent(that, 'click', that.btnConstancia_Click);
            controls.cboTipoTrabajo.addEvent(that, 'change', that.cboTipoTrabajo_Change);
            controls.cboSubTipoTrabajo.addEvent(that, 'change', that.cboSubTipoTrabajo_Change);
            controls.chkFidelizacion.addEvent(that, 'click', that.chkFidelizacion_Click);
            controls.btnCerrar01.addEvent(that, 'click', that.btnCerrar01_Click);
            controls.btnCerrar02.addEvent(that, 'click', that.btnCerrar02_Click);
            that.getLoadingPage();
            that.render();
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
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
        render: function () {
            var that = this;
            that.windowAutoSize();
            that.getValidateTransac();
        },
        getValidateTransac: function () {
            var that = this, controls = that.getControls();
            var param = {
                strIdSession: that.SessionTransac.UrlParams.IDSESSION
            }
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetValidationMessages',
                success: function (response) {
                    if (response != null) {
                        var data = response;
                        if (that.SessionTransac.SessionParams.DATACUSTOMER.ContractID === '') {
                            controls.btnSiguiente01.prop("disabled", true);
                            alert(data.strMsgConsultaCustomerContratoVacio,
                                'Alerta',
                                function () {
                                    window.close();
                                });
                        }

                        if (that.SessionTransac.SessionParams.DATASERVICE.CableValue === '') {
                            controls.btnSiguiente01.prop("disabled", true);
                            alert(data.strTextoDecoNoTieneCable,
                                'Alerta',
                                function () {
                                    window.close();
                                });
                        } else if (that.SessionTransac.SessionParams.DATASERVICE.CableValue === 'F') {
                            controls.btnSiguiente01.prop("disabled", true);
                            alert(data.strTextoDecoNoTieneCable,
                                'Alerta',
                                function () {
                                    window.close();
                                });
                        }

                        if (that.SessionTransac.SessionParams.DATASERVICE.StateLine === '') {
                            controls.btnSiguiente01.prop("disabled", true);
                            alert(data.strTextoEstadoInactivo,
                                'Alerta',
                                function () {
                                    window.close();
                                });
                        } else if (that.SessionTransac.SessionParams.DATASERVICE.StateLine !== data.strDescActivo) {
                            controls.btnSiguiente01.prop("disabled", true);
                            alert(data.strTextoEstadoInactivo,
                                'Alerta',
                                function () {
                                    window.close();
                                });
                        }
                    }
                },               
                complete: function() {
                    that.getDefaultVariables();
                }
            });
        },
        getDefaultVariables: function () {
            var that = this, controls = that.getControls();
            var param = {
                strIdSession: that.SessionTransac.UrlParams.IDSESSION
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetDefaultVariables',
                success: function (response) {
                    if (response != null) {
                        var data = response;
                        controls.lblTitle.text(data.hdnTituloPagina);
                        that.objLteUninstallInstallDeco.hdnmensajeConfirmacion = data.hdnmensajeConfirmacion;
                        that.objLteUninstallInstallDeco.hdnMensaje1 = data.hdnMensaje1;
                        that.objLteUninstallInstallDeco.hdnMensaje2 = data.hdnMensaje2;
                        that.objLteUninstallInstallDeco.hdnMensaje8 = data.hdnMensaje8;
                        that.objLteUninstallInstallDeco.hdnMensaje9 = data.hdnMensaje9;
                        that.objLteUninstallInstallDeco.hdnMensaje10 = data.hdnMensaje10;
                        that.objLteUninstallInstallDeco.hdnMensaje11 = data.hdnMensaje11;
                        that.objLteUninstallInstallDeco.hdnFechaActualServidor = data.hdnFechaActualServidor;
                        that.objLteUninstallInstallDeco.hdnTipoTrabajo = data.hdnTipoTrabajo;
                        that.objLteUninstallInstallDeco.hdnErrValidarAge = data.hdnErrValidarAge;
                        that.objLteUninstallInstallDeco.hdnListaGrupoCableLTE = data.hdnListaGrupoCableLTE;
                        that.objLteUninstallInstallDeco.hdnListaGrupoEquiposLTE = data.hdnListaGrupoEquiposLTE;
                        that.objLteUninstallInstallDeco.strMensajeValidaPlanComercial = data.strMensajeValidaPlanComercialLTE;
                        that.objLteUninstallInstallDeco.strMsgNoExisteDeco = data.strMsgNoExisteDeco;
                        that.objLteUninstallInstallDeco.strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE = data.strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE;
                        that.objLteUninstallInstallDeco.strMensajeNoExistenReglasDeNegocio = data.strMensajeNoExistenReglasDeNegocio;
                        that.objLteUninstallInstallDeco.strMensajeErrorConsultaIGV = data.strMensajeErrorConsultaIGV;
                        that.objLteUninstallInstallDeco.strMensajeValidationETA = data.strMensajeValidationETA;
                        that.objLteUninstallInstallDeco.strMensajeConfirmacionDeco = data.strMensajeConfirmacionDeco;
                        that.objLteUninstallInstallDeco.strMsgLimiteSdHd = data.strMsgLimiteSdHd;
                        that.objLteUninstallInstallDeco.strMsgLimiteDVR = data.strMsgLimiteDVR;
                        that.objLteUninstallInstallDeco.TypeLoyalty = data.intTypeLoyalty;
                        that.objLteUninstallInstallDeco.MotSotCode = data.strCodigoMotivoSot;
                        that.objLteUninstallInstallDeco.CodTipServLte = data.strCodTipServLte;
                        Session.ServerDate = data.hdnFechaActualServidor;
                    }
                },
                complete: function () {
                    that.getLoadDataSession();
                    that.getTypificationTransaction();
                }
            });
        },
        getLoadingPage: function () {
            var that = this;
            var controls = that.getControls();
            $.blockUI({
                message: controls.myModalLoad,
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
        getLoadDataSession: function () {
            var that = this, controls = that.getControls();

            controls.lblCantidad.text("0");
            controls.lblCargoFijoTotalPlanSIGV.text("0.00");
            controls.lblCargoFijoTotalPlanCIGV.text("0.00");

            //Datos del Cliente
            controls.lblIDContrato.text(that.SessionTransac.SessionParams.DATACUSTOMER.ContractID);
            controls.lblIDCustomer.text(that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID);
            controls.lblTipoCliente.text(that.SessionTransac.SessionParams.DATACUSTOMER.CustomerType);
            controls.lblCliente.text(that.SessionTransac.SessionParams.DATACUSTOMER.BusinessName);
            controls.lblContacto.text(that.SessionTransac.SessionParams.DATACUSTOMER.FullName);
            controls.lblDNIRUC.text(that.SessionTransac.SessionParams.DATACUSTOMER.DNIRUC);
            controls.lblRepresentanteLegal.text(that.SessionTransac.SessionParams.DATACUSTOMER.LegalAgent);
            controls.lblIdentDocLegalRepresent.text(that.SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber);
            controls.lblPlanActual.text(that.SessionTransac.SessionParams.DATASERVICE.Plan);
            controls.lblFechaActivacion.text(that.SessionTransac.SessionParams.DATASERVICE.ActivationDate == null ? '' : that.SessionTransac.SessionParams.DATASERVICE.ActivationDate.substring(0, 10));
            controls.lblCicloFacturacion.text(that.SessionTransac.SessionParams.DATACUSTOMER.BillingCycle);
            controls.lblLimiteCredito.text('S/ ' + (that.getRound(that.SessionTransac.SessionParams.DATACUSTOMER.objPostDataAccount.CreditLimit, 2)).toFixed(2));
            controls.lblDireccion.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress);
            controls.lblReferencia.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization);
            controls.lblPais.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry);
            controls.lblDepartamento.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament);
            controls.lblProvincia.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince);
            controls.lblDistrito.text(that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict);
            controls.lblCentroPoblado.text(that.SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate);
            controls.lblCodUbigeo.text(that.SessionTransac.SessionParams.DATACUSTOMER.InstallUbigeo);
            if(that.SessionTransac.SessionParams.DATACUSTOMER.Email != null)
            {
                if (that.SessionTransac.SessionParams.DATACUSTOMER.Email != "") {
                controls.chkEnviarPorEmail.attr('checked', true);
                controls.txtEnviarporEmail.css("display", "block");
                controls.txtEnviarporEmail.val(that.SessionTransac.SessionParams.DATACUSTOMER.Email);
            }
            }
        },
        getTypificationTransaction: function () {
            var that = this;

            var param = {
                strIdSession: that.SessionTransac.UrlParams.IDSESSION,
                strTransactionName: that.objLteUninstallInstallDeco.strTRANSACCION_INSTALACION_DECO_ADICIONAL_LTE
            }

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(param),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetTypificationTransaction',
                success: function (response) {
                    if (response != null) {
                        var data = response;
                        that.objLteUninstallInstallDeco.Typification.Type = data.Type;
                        that.objLteUninstallInstallDeco.Typification.Class = data.Class;
                        that.objLteUninstallInstallDeco.Typification.SubClass = data.SubClass;
                        that.objLteUninstallInstallDeco.Typification.InteractionCode = data.InteractionCode;
                        that.objLteUninstallInstallDeco.Typification.TypeCode = data.TypeCode;
                        that.objLteUninstallInstallDeco.Typification.ClassCode = data.ClassCode;
                        that.objLteUninstallInstallDeco.Typification.SubClassCode = data.SubClassCode;
                    }
                },
                complete: function () {
                    that.getDecosMatriz();
                    that.getCommertialPlan();
                    that.getBusinessRules();
                    that.getWorkType();
                }
            });
        },
        getBusinessRules: function () {
            var that = this, controls = that.getControls(), objRules = {};

            objRules.strIdSession = that.SessionTransac.UrlParams.IDSESSION,
                objRules.strSubClase = that.objLteUninstallInstallDeco.Typification.SubClassCode;

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/CommonServices/GetBusinessRules',
                data: JSON.stringify(objRules),
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divRules.append(list[0].REGLA);
                        }
                    } else {
                        controls.divRules.append(that.objLteUninstallInstallDeco
                            .strMensajeNoExistenReglasDeNegocio);
                    }
                }
            });
        },
        getCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            objCacDacType.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            var parameters = {};
            parameters.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            parameters.strCodeUser = that.SessionTransac.SessionParams.USERACCESS.login;

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
                            controls.ddlCACDAC.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                            if (response.data != null) {
                            }
                            var itemSelect;
                            $.each(response.data.CacDacTypes,
                                function (index, value) {

                                    if (cacdac === value.Description) {
                                        controls.ddlCACDAC.append($('<option>',
                                            { value: value.Code, html: value.Description }));
                                        itemSelect = value.Code;

                                    } else {
                                        controls.ddlCACDAC.append($('<option>',
                                            { value: value.Code, html: value.Description }));
                                    }
                                });
                            if (itemSelect != null && itemSelect.toString != "undefined") {
                                $("#ddlCACDAC option[value=" + itemSelect + "]").attr("selected", true);
                            }
                        }
                    });
                }
            });
        },
        getInitDatatableDetalleProducto: function () {
            var that = this,
                controls = that.getControls();

            var tblDetalleProducto = controls.tblDetalleProducto.dataTable({
                "scrollY": 150,
                "scrollCollapse": true,
                "searching": false,
                "bProcessing": true,
                "bDestroy": true,
                "bPaginate": false,
                /*"sPaginationType": "full_numbers",*/
                "oLanguage": {
                    "sProcessing": "Procesando...",
                    "sSearch": "Buscar: ",
                    "sLengthMenu": "",
                    "sZeroRecords": "No existen datos.",
                    "sInfo": "",
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
            that.getConsultIgv(tblDetalleProducto);
            that.getCacDat();
        },
        getProductDetail: function (tabla) {
            var cantidad = 0, cantDecos = 0,
                that = this,
                param = {},
                controls = that.getControls();

            that.objLteUninstallInstallDeco.CantidadListaEquipos = cantidad;
            tabla.fnClearTable();

            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.strContratoID = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.strCustomerID = that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID;

            $.ajax({
                type: 'POST',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetListDataProducts',
                data: JSON.stringify(param),
                contentType: 'application/json; charset=utf-8',
                datatype: 'json',
                async: true,
                cache: false,
                error: function () {
                    $.unblockUI();
                },
                success: function (response) {
                    var registros = response;
                    var contador = 0;
                    $.each(registros,
                        function (i, r) {
                            var descripcion40 = "";
                            if (r.tipoServicio === "TV SATELITAL") {
                                var contenido;
                                if (r.tipo_equipo === 'DECO') {
                                    var iconDeco = "";
                                    var txtTipo = "";
                                    if (r.tipo_deco === 'HD') {
                                        iconDeco = 'glyphicon-hd-video';
                                        txtTipo = "HD";
                                    } else if (r.tipo_deco.toUpperCase() === 'SD' || r.tipo_deco.toUpperCase() === 'REGULAR') {
                                        iconDeco = 'glyphicon-sd-video';
                                        txtTipo = "SD";
                                    } else {
                                        iconDeco = 'glyphicon-hdd';
                                        txtTipo = "DVR";
                                    }
                                    var peso = that.getWeightDeco(txtTipo);
                                    if (r.descripcion_material.length > 40) {
                                        descripcion40 = r.descripcion_material.substring(0, 37) + '...';
                                    } else {
                                        descripcion40 = r.descripcion_material;
                                    }

                                    if (r.oc_equipo.toUpperCase() === 'BÁSICO' ||
                                        r.oc_equipo.toUpperCase() === 'BASICO') {
                                        var tipodeco = r.tipo_deco;
                                        contador++;
                                        tabla.fnAddData([
                                            contador, r.descripcion_material, tipodeco, "INCLUIDO"
                                        ]);

                                        contenido =
                                            "<a href='javascript:void(0);' class='list-group-item' data-costo='0'  data-tipodeco='" +
                                            r.tipo_deco +
                                            "'  title='" +
                                            r.descripcion_material +
                                            /*"'>&nbsp;<span class='glyphicon " +
                                            iconDeco +
                                            "'>&nbsp;</span>" +*/
                                            "'>&nbsp;<label class='badge-dec' style='width: 26px; font-size: 11px;'>" +
                                            txtTipo +
                                            "</label>&nbsp;DECODIFICADOR " +
                                            /*descripcion40 +*/
                                            txtTipo +
                                            "</a>";
                                        $('#mis-decos').append(contenido);
                                        cantDecos = cantDecos + parseInt(peso);

                                    } else {
                                        var tipodeco2 = r.tipo_deco;
                                        var costo = that.getRound((parseFloat(r.precio_almacen) *
                                                parseFloat(that.objLteUninstallInstallDeco.igv)),
                                            2).toFixed(2);
                                        contador++;
                                        tabla.fnAddData([
                                            contador, r.descripcion_material, tipodeco2, r.oc_equipo
                                        ]);

                                        if (r.descripcion_material.length > 40) {
                                            descripcion40 = r.descripcion_material.substring(0, 37) + '...';
                                        } else {
                                            descripcion40 = r.descripcion_material;
                                        }

                                        contenido =
                                            "<a href='javascript:void(0);' style ='background-color: #fbffbb; font-size: 12px;' class='list-group-item' " +
                                            "data-id='" +
                                            r.Codigo +
                                            "'data-codeservice='" +
                                            "" +
                                            "' data-sncode='" +
                                            r.codtipequ +
                                            "' data-cf='" +
                                            r.precio_almacen +
                                            "' data-spcode='" +
                                            r.tipequ +
                                            "' data-servicename='" +
                                            r.descripcion_material +
                                            "' data-servicetype='" +
                                            r.tipoServicio +
                                            "' data-servicegroup='" +
                                            "" +
                                            "' data-equipment='" +
                                            '' +
                                            "' data-quantity='" +
                                            '' +

                                            "' data-associated='" +
                                            r.asociado +
                                            "' data-codinssrv='" +
                                            r.codinssrv +
                                            "' data-serialnumber='" +
                                            r.numero_serie +
                                            "' data-costo='" +
                                            r.precio_almacen +
                                            "' data-tipodeco='" +
                                            r.tipo_deco +
                                            "' data-decotype='ALQUILADO'" +
                                            /*
                                            " data-des40='" +
                                            descripcion40 +
                                            "' data-desc='" +
                                            r.descripcion_material +
                                            "' title='" +
                                            r.descripcion_material +
                                            */

                                            " data-des40='DECODIFICADOR " +
                                            txtTipo +
                                            "' data-desc='DECODIFICADOR " +
                                            txtTipo +
                                            "' title='DECODIFICADOR " +
                                            txtTipo +

                                            "' data-peso='" +
                                            peso +
                                            "'>&nbsp;&nbsp;<label class='badge-dec' style='width: 26px; font-size: 11px;'>" +
                                            txtTipo +
                                            "</label>&nbsp;DECODIFICADOR " +
                                            /* "'>&nbsp;&nbsp;<span class='glyphicon " +
                                            iconDeco +
                                            "'>&nbsp;</span>" +
                                             
                                            descripcion40 +*/
                                            txtTipo +
                                            "<span style='float: right'> S/." +
                                            that.getRoundDecimal(costo) +
                                            "</span><input type='checkbox' class='pull-left'/></a>";
                                        $('#mis-decos').append(contenido);
                                        cantDecos = cantDecos + parseInt(peso);
                                    }
                                }
                                cantidad++;

                            }
                        });
                   
                    that.objLteUninstallInstallDeco.intCantidadDecos = cantidad;
                    that.objLteUninstallInstallDeco.intCantidadDecosTemp = cantDecos;
                    
                    
                    var free = parseInt(that.objLteUninstallInstallDeco.CantPuntosMax) - parseInt(that.objLteUninstallInstallDeco.intCantidadDecosTemp);
                    controls.lblPtosUse.text(parseInt(that.objLteUninstallInstallDeco.intCantidadDecosTemp));
                    controls.lblPtosFree.text(free);
                    tabla.fnDraw();
                },
                complete: function () {
                    that.getAddtionalEquipment();
                }
            });
        },
        getCommertialPlan: function () {
            var that = this, controls = that.getControls(), param = {};
            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.strContratoId = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetCommertialPlan',
                success: function (response) {
                    if (response != null) {
                        if (response.rResult === true &
                            response.rintCodigoError === that.objLteUninstallInstallDeco.intNumeroCero) {
                            that.objLteUninstallInstallDeco.hdnCodigoPlan = response.rCodigoPlan;

                        } else {
                            alert(that.objLteUninstallInstallDeco.strMensajeValidaPlanComercial,
                                'Alerta',
                                function () { window.close(); });
                        }
                    }
                },
                complete: function() {
                    that.getInitDatatableDetalleProducto();
                },
                error: function () {
                }
            });
        },
        chkEnviarPorEmail_Click: function () {
            var that = this,
                controls = that.getControls(),
                chkEnviarPorEmail = controls.chkEnviarPorEmail;

            if (chkEnviarPorEmail[0].checked == true) {
                controls.txtEnviarporEmail.css("display", "block");
                controls.txtEnviarporEmail.val(that.SessionTransac.SessionParams.DATACUSTOMER.Email);
            } else {
                controls.txtEnviarporEmail.css("display", "none");
            }
        },
        validateEmail: function () {
            var that = this, controls = that.getControls();

            if (controls.chkEnviarPorEmail[0].checked === true) {
                if ($.trim(controls.txtEnviarporEmail.val()) === that.objLteUninstallInstallDeco.strVariableEmpty) {
                    alert(that.objLteUninstallInstallDeco.hdnMensaje1,
                        'Alerta',
                        function () {
                            controls.txtEnviarporEmail.focus();
                        });
                    return false;
                } else {
                    var regx =
                        /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                    var blvalidar = regx.test(controls.txtEnviarporEmail.val());

                    if (blvalidar === false) {
                        alert(that.objLteUninstallInstallDeco.hdnMensaje2,
                            'Alerta',
                            function () {
                                controls.txtEnviarporEmail.select();
                            });
                        return false;
                    }
                }
            }

            return true;
        },
        getRound: function (cantidad, decimales) {
            //donde: decimales > 2
            cantidad = parseFloat(cantidad);
            decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },
        getRoundDecimal: function (x) {
            return parseFloat(x).toFixed(2);
        },
        getAddtionalEquipment: function () {
            var that = this, controls = that.getControls(), param = {};
            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.idplan = that.objLteUninstallInstallDeco.hdnCodigoPlan;//1659
            param.coid = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;

            $.ajax({
                type: "POST",
                url: "/Transactions/LTE/UninstallInstallationOfDecoder/GetListEquipment",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                },
                success: function (response) {
                    var filas = response,
                        index = 0;

                    that.objLteUninstallInstallDeco.ListaEquiposAdicionalesServer = filas;

                    if (filas != null) {
                        $('#default-instalar').hide();
                        $.each(filas,
                            function(key, value) {
                                var grupo = filas[index].CodGrupoServ;
                                var tipoServicio = filas[index].CodTipoServ;
                                var id = filas[index].CodServSisact;
                                var descripcion = filas[index].DesServSisact;
                                var solucion = filas[index].Solucion;
                                var equipo = filas[index].Equipo;
                                var costo = that.getRound((parseFloat(filas[index].CF) *
                                        parseFloat(that.objLteUninstallInstallDeco.igv)),
                                    2).toFixed(2);
                                var cantidad = filas[index].CantidadEquipo;
                                var identificador = descripcion + id + equipo;
                                var sncod = filas[index].SNCode;
                                var spcod = filas[index].SPCode;
                                var tipoequi = filas[index].TipoEquipo;
                                var tmcode = filas[index].TmCode;
                                var mdsap = "0";
                                var tipoequitxt = filas[index].MatvDesSap;

                                that.objLteUninstallInstallDeco.TmCode = tmcode;

                                var deshabilitar = "";
                                var descripcion40 = "";

                                var lstGrupoEquipos = that.objLteUninstallInstallDeco.hdnListaGrupoEquiposLTE.split(';');
                                var iconDeco = "";
                                var txtTipo = "";
                                if (that.getValidateGroup(grupo, lstGrupoEquipos)) {
                                    if (descripcion.indexOf('HD') !== -1) {
                                        iconDeco = 'glyphicon-hd-video';
                                        txtTipo = "HD";
                                    } else if (descripcion.indexOf('SD') !== -1) {
                                        iconDeco = 'glyphicon-sd-video';
                                        txtTipo = "SD";
                                    } else {
                                        iconDeco = 'glyphicon-hdd';
                                        txtTipo = "DVR";
                                    }
                                    if (descripcion.length > 40) {
                                        descripcion40 = descripcion.substring(0, 37) + '...';
                                    } else {
                                        descripcion40 = descripcion;
                                    }
                                    var peso = that.getWeightDeco(txtTipo);
                                    var contenido =
                                        "<a href='javascript:void(0);' style='background-color:#ccffc8;' class='list-group-item' " +
                                            "data-id='" +
                                            id +
                                            "'data-codeservice='" +
                                            id +
                                            "' data-sncode='" +
                                            sncod +
                                            "' data-cf='" +
                                            filas[index].CF +
                                            "' data-spcode='" +
                                            spcod +
                                            "' data-servicename='" +
                                            descripcion +
                                            "' data-servicetype='" +
                                            filas[index].TipoServ +
                                            "' data-servicegroup='" +
                                            filas[index].GrupoServ +
                                            "' data-equipment='" +
                                            '' +
                                            "' data-quantity='" +
                                            '' +
                                            "' data-associated='" +
                                            '' +
                                            "' data-codinssrv='" +
                                            '' +
                                            "' data-serialnumber='" +
                                            '' +
                                            "' data-costo='" +
                                            filas[index].CF +
                                            "' data-tipodeco='" +
                                            tipoequi +
                                            "' data-decotype='ADICIONAL' data-des40='" +
                                            descripcion40 +
                                            "' data-desc='" +
                                            descripcion +
                                            "' title='" +
                                            descripcion +
                                            "' data-peso='" +
                                            peso +
                                            "'>&nbsp;&nbsp;<label class='badge-dec' style='width: 26px; font-size: 11px;'>" +
                                            txtTipo +
                                            "</label>&nbsp;" +
                                            /* "'>&nbsp;&nbsp;<span class='glyphicon " +
                                            iconDeco +
                                            "'>&nbsp;</span>" +
                                             */
                                            descripcion40 +
                                            " <span style='float: right'> S/." +
                                            costo +
                                            "</span><input type='checkbox' class='pull-left'/></a>";

                                    $('#decos-instalar').append(contenido);
                                }

                                index++;
                            });
                    } else {
                        $('#default-instalar').show();
                    }
                },
                complete: function() {
                        that.initPickList();
                }
            });
        },
        getValidateGroup: function (grupo, listaGrupo) {
            var flagValid = false;
            var i = 0;
            for (i = 0; i < listaGrupo.length; i++) {
                if (grupo == listaGrupo[i]) {
                    flagValid = true;
                }
            }
            return flagValid;
        },
        getQuantity: function (objDeco) {
            var that = this,
                controls = that.getControls();

            that.getLoadingPage();

            if (objDeco.tipo === "INSTALAR") {
                if (objDeco.decotype === "ADICIONAL") {

                    that.objLteUninstallInstallDeco.CostoAdicional = parseFloat(that.objLteUninstallInstallDeco.CostoAdicional) + parseFloat(objDeco.costo);
                    that.objLteUninstallInstallDeco.CostoAdicionalCIGV = parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV) +
                        that.getRound((parseFloat(objDeco.costo) * parseFloat(that.objLteUninstallInstallDeco.igv)), 2);
                    that.objLteUninstallInstallDeco.CantAdicionalesInst = parseInt(that.objLteUninstallInstallDeco.CantAdicionalesInst) + 1;

                    var decoIns = {
                        id: objDeco.id,
                        desc: objDeco.description,
                        desc40: objDeco.description40,
                        tipodeco: objDeco.tipodeco,
                        peso: objDeco.peso,
                        costosing: that.getRoundDecimal(objDeco.costo),
                        costocigv: that.getRoundDecimal((parseFloat(objDeco.costo) * parseFloat(that.objLteUninstallInstallDeco.igv))),

                        CodeService: objDeco.CodeService,
                        SnCode: objDeco.SnCode,
                        Cf: objDeco.Cf,
                        SpCode: objDeco.SpCode,
                        ServiceName: objDeco.ServiceName,
                        ServiceType: objDeco.ServiceType,
                        ServiceGroup: objDeco.ServiceGroup,
                        Equipment: objDeco.Equipment,
                        Quantity: objDeco.Quantity,

                        Associated: objDeco.Associated,
                        CodeInsSrv: objDeco.CodeInsSrv,
                        SerialNumber: objDeco.SerialNumber,
                        Flag: 'A'
                    }

                    that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.push(decoIns);

                } else {
                    that.objLteUninstallInstallDeco.CantAdicionalesDesint =
                        parseInt(that.objLteUninstallInstallDeco.CantAdicionalesDesint) - 1;


                    if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {
                        var temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst;
                        temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.filter(function (el) {
                            return el.id !== objDeco.id;
                        });

                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst = temp;

                    }
                }

            }
            if (objDeco.tipo === "REMOVER") {
                if (objDeco.decotype === "ADICIONAL") {

                    that.objLteUninstallInstallDeco.CostoAdicional = parseFloat(that.objLteUninstallInstallDeco.CostoAdicional) - parseFloat(objDeco.costo);
                    that.objLteUninstallInstallDeco.CostoAdicionalCIGV = parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV) -
                        that.getRoundDecimal((parseFloat(objDeco.costo) * parseFloat(that.objLteUninstallInstallDeco.igv)));
                    that.objLteUninstallInstallDeco.CantAdicionalesInst = parseInt(that.objLteUninstallInstallDeco.CantAdicionalesInst) - 1;

                    if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0) {

                        var temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst;
                        temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.filter(function (el) {
                            return el.id != objDeco.id;
                        });

                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst = temp;
                    }


                } else {
                    that.objLteUninstallInstallDeco.CantAdicionalesDesint = parseInt(that.objLteUninstallInstallDeco.CantAdicionalesDesint) - 1;

                    that.objLteUninstallInstallDeco.CostoAdicional = parseFloat(that.objLteUninstallInstallDeco.CostoAdicional) + parseFloat(objDeco.costo);
                    that.objLteUninstallInstallDeco.CostoAdicionalCIGV = parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV) +
                        that.getRound((parseFloat(objDeco.costo) * parseFloat(that.objLteUninstallInstallDeco.igv)), 2);

                    if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {

                        var temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst;
                        temp = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.filter(function (el) {
                            return el.id != objDeco.id;
                        });


                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst = temp;
                    }
                }

            }


            if (objDeco.tipo === "DESINSTALAR") {


                that.objLteUninstallInstallDeco.CostoAdicional = parseFloat(that.objLteUninstallInstallDeco.CostoAdicional) - parseFloat(objDeco.costo);
                that.objLteUninstallInstallDeco.CostoAdicionalCIGV = parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV) -
                    that.getRoundDecimal((parseFloat(objDeco.costo) * parseFloat(that.objLteUninstallInstallDeco.igv)));


                that.objLteUninstallInstallDeco.CantAdicionalesDesint = parseInt(that.objLteUninstallInstallDeco.CantAdicionalesDesint) + 1;

                var decoDes = {
                    id: objDeco.id,
                    desc: objDeco.description,
                    desc40: objDeco.description40,
                    tipodeco: objDeco.tipodeco,
                    peso: objDeco.peso,
                    costosing: "0.00",
                    costocigv: "0.00",

                    CodeService: objDeco.CodeService,
                    SnCode: objDeco.SnCode,
                    Cf: objDeco.Cf,
                    SpCode: objDeco.SpCode,
                    ServiceName: objDeco.ServiceName,
                    ServiceType: objDeco.ServiceType,
                    ServiceGroup: objDeco.ServiceGroup,
                    Equipment: objDeco.Equipment,
                    Quantity: objDeco.Quantity,

                    Associated: objDeco.Associated,
                    CodeInsSrv: objDeco.CodeInsSrv,
                    SerialNumber: objDeco.SerialNumber,
                    Flag: 'B'
                }

                that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.push(decoDes);

            }

            controls.lblCargoFijoTotalPlanCIGV.text(that.getRound(that.objLteUninstallInstallDeco.CostoAdicionalCIGV, 2).toFixed(2));
            controls.lblCargoFijoTotalPlanSIGV.text(that.getRound(that.objLteUninstallInstallDeco.CostoAdicional, 2).toFixed(2));
            controls.lblCantidad.text(that.objLteUninstallInstallDeco.CantAdicionalesInst);

            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0 &&
                that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {

                if (controls.cboTipoTrabajo.val() != that.objLteUninstallInstallDeco.opcAct) {
                    controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcAct);
                    that.validationEta();
                    that.getLoyaltyAmount();
                } else {
                    controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcAct);
                }

                that.objLteUninstallInstallDeco.FlajInstDesins = 2;
                controls.chkFidelizacion.attr('disabled', false);
            } else {
                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0) {

                    if (controls.cboTipoTrabajo.val() != that.objLteUninstallInstallDeco.opcInst) {
                        controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcInst);
                        that.validationEta();
                        that.getLoyaltyAmount();
                    } else {
                        controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcInst);
                    }
                    that.objLteUninstallInstallDeco.FlajInstDesins = 1;
                    controls.chkFidelizacion.attr('disabled', false);
                }
                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {

                    if (controls.cboTipoTrabajo.val() != that.objLteUninstallInstallDeco.opcDesinst) {

                        controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcDesinst);
                        that.validationEta();
                        controls.lblMontoFidelizacion.text('0.00');
                    } else {
                        controls.cboTipoTrabajo.val(that.objLteUninstallInstallDeco.opcDesinst);
                    }
                    that.objLteUninstallInstallDeco.FlajInstDesins = 0;
                    
                    controls.chkFidelizacion.attr('disabled', true);
                }
            }

            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length === 0 &&
                that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length === 0) {
                controls.cboTipoTrabajo.val('-1');
                controls.cboSubTipoTrabajo.html("");
                controls.cboSubTipoTrabajo.attr('disabled', true);
            }

            that.setTotal();
            setTimeout(function () { $.unblockUI(); }, 1500);
        },
        btnSummary_click: function (e) {
            var that = this;

            if ($(e).attr('id') == 'btnSiguiente01') {

                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length < 1 &&
                    that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length < 1) {
                    alert(that.objLteUninstallInstallDeco.strMsgNoExisteDeco, 'Alerta');
                    return false;
                }
                if (that.objLteUninstallInstallDeco.hndValidateETA == that.objLteUninstallInstallDeco.strNumeroDos) {
                    if ($("#cboFranjaHoraria").val() == "-1") {
                        alert(that.objLteUninstallInstallDeco.hdnMensaje10);
                        return false;
                    }
                }
                

                if (!that.validateEmail())
                    return false;

                navigateTabs(e);
                $('#idtab2').focus();
                that.setSummary();
               
            }
        },
        getConsultIgv: function (tblDetalleProducto) {
            var that = this, controls = that.getControls(),
                param = {};

            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/CommonServices/GetConsultIGV',
                success: function (response) {
                    if (response.data != null) {
                        var igv = response.data.igvD;
                        that.objLteUninstallInstallDeco.igv = 1 + igv;
                    }
                    else
                    {
                        alert(that.objLteUninstallInstallDeco.strMensajeErrorConsultaIGV,
                            'Alerta',
                            function () { window.close(); });
                    }
                },
                complete: function() {
                    that.getProductDetail(tblDetalleProducto);
                    that.getAmountsCurrentPlan();
                },
                error: function () {
                    alert(that.objLteUninstallInstallDeco.strMensajeErrorConsultaIGV,
                        'Alerta',
                        function () { window.close(); });
                }
            });
        },
        getWorkType: function () {
            var that = this, controls = that.getControls(), param = {};

            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.strTransacType = that.objLteUninstallInstallDeco.hdnTipoTrabajo;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetWorkType',
                success: function (response) {
                    controls.cboTipoTrabajo.empty();
                    controls.cboTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.objFinalResponse != null && response.objFinalResponse.length > 0) {
                        $.each(response.objFinalResponse,
                            function (index, value) {
                                controls.cboTipoTrabajo.append($('<option>',
                                    { value: value.Code, html: value.Description }));
                                if (value.Code == response.strTypeJobMix) {
                                    that.objLteUninstallInstallDeco.opcAct = value.Code;
                                } else {
                                    if (value.Code == response.strTypeJobDesinst) {
                                        that.objLteUninstallInstallDeco.opcDesinst = value.Code;
                                    } else {
                                        that.objLteUninstallInstallDeco.opcInst = value.Code;
                                    }
                                }
                            });
                    } else {
                        alert("No existe ningún tipo de trabajo para esta transacción.");
                    }
                }
            });
        },
        initPickList: function () {

            var that = this, controls = that.getControls();

            $('.arrowRight').click(function () {
                var items = $("#mis-decos input:checked");
                var n = items.length;
                if (n > 0) {
                    items.each(function (idx, item) {
                        var decotype = $(item).parent().data('decotype');
                        var id = $(item).parent().data('id');
                        var description = $(item).parent().data('desc');
                        var description40 = $(item).parent().data('des40');
                        var tipodeco = $(item).parent().data('tipodeco');
                        var costo = $(item).parent().data('costo');
                        var peso = $(item).parent().data('peso');

                        var codeservice = $(item).parent().data('codeservice');
                        var sncode = $(item).parent().data('sncode');
                        var cf = $(item).parent().data('cf');
                        var spcode = $(item).parent().data('spcode');
                        var servicename = $(item).parent().data('servicename');
                        var servicetype = $(item).parent().data('servicetype');
                        var servicegroup = $(item).parent().data('servicegroup');
                        var equipment = $(item).parent().data('equipment');
                        var quantity = $(item).parent().data('quantity');

                        var associated = $(item).parent().data('associated');
                        var codinssrv = $(item).parent().data('codinssrv');
                        var serialnumber = $(item).parent().data('serialnumber');

                        if (decotype === 'ADICIONAL') {

                            var choice = $(item);
                            choice.prop("checked", false);
                            choice.parent().appendTo("#decos-instalar");

                            var objDecoRem = {
                                id: id,
                                tipodeco: tipodeco,
                                description: description,
                                description40: description40,
                                costo: costo,
                                decotype: decotype,
                                peso: peso,
                                tipo: 'REMOVER',

                                CodeService: codeservice,
                                SnCode: sncode,
                                Cf: cf,
                                SpCode: spcode,
                                ServiceName: servicename,
                                ServiceType: servicetype,
                                ServiceGroup: servicegroup,
                                Equipment: equipment,
                                Quantity: quantity,

                                Associated: associated,
                                CodeInsSrv: codinssrv,
                                SerialNumber: serialnumber
                            }

                            that.getQuantity(objDecoRem);
                        } else {
                            $(item).prop("checked", false);
                            $(item).parent().appendTo("#decos-desinstalar");

                            var objDecoDes = {
                                id: id,
                                tipodeco: tipodeco,
                                description: description,
                                description40: description40,
                                costo: costo,
                                decotype: decotype,
                                peso: peso,
                                tipo: 'DESINSTALAR',

                                CodeService: codeservice,
                                SnCode: sncode,
                                Cf: cf,
                                SpCode: spcode,
                                ServiceName: servicename,
                                ServiceType: servicetype,
                                ServiceGroup: servicegroup,
                                Equipment: equipment,
                                Quantity: quantity,

                                Associated: associated,
                                CodeInsSrv: codinssrv,
                                SerialNumber: serialnumber
                            }

                            that.getQuantity(objDecoDes);

                        }

                    });
                } else {
                    alert("Debes seleccionar por lo menos un deco.");
                }

                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {
                    $('#default-desinstalar').hide();
                } else {
                    $('#default-desinstalar').show();
                }
            });

            $('.arrowLeft').click(function () {


                var items = $("#decos-instalar input:checked");

                var items2 = $("#decos-desinstalar input:checked");

                var n1 = items.length;
                var n2 = items2.length;

                if (n1 > 0 || n2 > 0) {
                    if (n1 > 0) {

                        var listMisDecos = document.querySelector("#mis-decos").querySelectorAll("a");

                        var invalid = true;

                        items.each(function (idx, item) {

                            if (invalid) {
                                var decotype = $(item).parent().data('decotype');
                                var costo = $(item).parent().data('costo');
                                var id = $(item).parent().data('id');
                                var description = $(item).parent().data('desc');
                                var description40 = $(item).parent().data('des40');
                                var tipodeco = $(item).parent().data('tipodeco');
                                var peso = $(item).parent().data('peso');

                                var codeservice = $(item).parent().data('codeservice');
                                var sncode = $(item).parent().data('sncode');
                                var cf = $(item).parent().data('cf');
                                var spcode = $(item).parent().data('spcode');
                                var servicename = $(item).parent().data('servicename');
                                var servicetype = $(item).parent().data('servicetype');
                                var servicegroup = $(item).parent().data('servicegroup');
                                var equipment = $(item).parent().data('equipment');
                                var quantity = $(item).parent().data('quantity');

                                var associated = $(item).parent().data('associated');
                                var codinssrv = $(item).parent().data('codinssrv');
                                var serialnumber = $(item).parent().data('serialnumber');

                                if (that.validateMatriz(listMisDecos, tipodeco)) {
                                    var choice = $(item);
                                    choice.prop("checked", false);
                                    choice.parent().appendTo("#mis-decos");

                                    var objDecoIns = {
                                        id: id,
                                        tipodeco: tipodeco,
                                        description: description,
                                        description40: description40,
                                        costo: costo,
                                        decotype: decotype,
                                        peso: peso,
                                        tipo: 'INSTALAR',

                                        CodeService: codeservice,
                                        SnCode: sncode,
                                        Cf: cf,
                                        SpCode: spcode,
                                        ServiceName: servicename,
                                        ServiceType: servicetype,
                                        ServiceGroup: servicegroup,
                                        Equipment: equipment,
                                        Quantity: quantity,

                                        Associated: associated,
                                        CodeInsSrv: codinssrv,
                                        SerialNumber: serialnumber
                                    }

                                    that.getQuantity(objDecoIns);
                                } else {
                                    invalid = false;

                                    var mensaje = that.getMessageDeco(tipodeco);
                                    alert(mensaje);
                                }
                            }

                        });
                    }
                    if (n2 > 0) {
                        var listMisDecos2 = document.querySelector("#mis-decos").querySelectorAll("a");

                        var invalid2 = true;

                        items2.each(function (idx, item) {
                            if (invalid2) {
                                var decotype = $(item).parent().data('decotype');
                                var costo2 = $(item).parent().data('costo');
                                var id = $(item).parent().data('id');
                                var description = $(item).parent().data('desc');
                                var description40 = $(item).parent().data('des40');
                                var peso = $(item).parent().data('peso');
                                var tipodeco = $(item).parent().data('tipodeco');

                                var codeservice = $(item).parent().data('codeservice');
                                var sncode = $(item).parent().data('sncode');
                                var cf = $(item).parent().data('cf');
                                var spcode = $(item).parent().data('spcode');
                                var servicename = $(item).parent().data('servicename');
                                var servicetype = $(item).parent().data('servicetype');
                                var servicegroup = $(item).parent().data('servicegroup');
                                var equipment = $(item).parent().data('equipment');
                                var quantity = $(item).parent().data('quantity');

                                var associated = $(item).parent().data('associated');
                                var codinssrv = $(item).parent().data('codinssrv');
                                var serialnumber = $(item).parent().data('serialnumber');

                                if (that.validateMatriz(listMisDecos2, tipodeco)) {
                                    var choice = $(item);
                                    choice.prop("checked", false);
                                    choice.parent().appendTo("#mis-decos");

                                    var objDecoRem = {
                                        id: id,
                                        tipodeco: tipodeco,
                                        description: description,
                                        description40: description40,
                                        costo: costo2,
                                        decotype: decotype,
                                        peso: peso,
                                        tipo: 'REMOVER',
                                        CodeService: codeservice,
                                        SnCode: sncode,
                                        Cf: cf,
                                        SpCode: spcode,
                                        ServiceName: servicename,
                                        ServiceType: servicetype,
                                        ServiceGroup: servicegroup,
                                        Equipment: equipment,
                                        Quantity: quantity,

                                        Associated: associated,
                                        CodeInsSrv: codinssrv,
                                        SerialNumber: serialnumber
                                    }

                                    that.getQuantity(objDecoRem);
                                } else {
                                    invalid2 = false;
                                    var mensaje = that.getMessageDeco(tipodeco);
                                    alert(mensaje);
                                }
                            }
                        });
                    }
                } else {
                    alert("Debes seleccionar por lo menos un deco.");
                }


                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {
                    $('#default-desinstalar').hide();
                } else {
                    $('#default-desinstalar').show();
                }


            });

            $('[type=checkbox]').click(function (e) {
                e.stopPropagation();
            });

            /* toggle checkbox when list group item is clicked */
            $('.list-group a').click(function (e) {

                e.stopPropagation();

                var $this = $(this).find("[type=checkbox]");
                if ($this.is(":checked")) {
                    $this.prop("checked", false);
                } else {
                    $this.prop("checked", true);
                }
            });
        },
        setSummary: function () {
            var that = this,
                controls = that.getControls();

            that.smmry.set('CargoFijoSIGV', 'S/ ' + controls.lblCargoFijoTotalPlanSIGV.text());
            that.smmry.set('CargoFijoCIGV', 'S/ ' + controls.lblCargoFijoTotalPlanCIGV.text());
            that.smmry.set('MontoFidelizar', 'S/ ' + controls.lblMontoFidelizacion.text());

            if (controls.cboTipoTrabajo.val() != "-1") {
                that.smmry.set('TipoTrabajo', $('#cboTipoTrabajo option:selected').html());
            } else {
                that.smmry.set('TipoTrabajo', '');
            }

                if (controls.cboSubTipoTrabajo.val() != "-1") {
                    that.smmry.set('SubTipoTrabajo', $('#cboSubTipoTrabajo option:selected').html());
                } else {
                    that.smmry.set('SubTipoTrabajo', '');
                }
                
            if ($.trim(controls.txtFechaCompromiso.val()) != "") {
                that.smmry.set('FechaCompromiso', controls.txtFechaCompromiso.val());
            } else {
                that.smmry.set('FechaCompromiso', '');
            }
            if (controls.cboFranjaHoraria.val() !== "-1") {
                that.smmry.set('Horario', $('#cboFranjaHoraria option:selected').html());
            } else {
                that.smmry.set('Horario', '');
            }
            if (controls.chkFidelizacion[0].checked == true) {
                that.smmry.set('Fidelizar', 'SI');
            } else {
                that.smmry.set('Fidelizar', 'NO');
            }
            if ($.trim(controls.txtNota.val()) != "") {
                that.smmry.set('Nota', controls.txtNota.val());
            } else {
                that.smmry.set('Nota', '');
            }
            if (controls.chkEnviarPorEmail[0].checked == true) {
                that.smmry.set('Correo', controls.txtEnviarporEmail.val());
            } else {
                that.smmry.set('Correo', '');
            }
            if (controls.ddlCACDAC.val() != "") {
                that.smmry.set('PuntoVenta', $('#ddlCACDAC option:selected').html());
            } else {
                that.smmry.set('PuntoVenta', '');
            }

            var tr = "";
            tr = tr +
                '<tr><td colspan="3" style="text-align: left;padding: 0px;background-color: #d9534f;color: #ffffff;">&nbsp;<span class="glyphicon glyphicon-plus">&nbsp;</span>Equipos a instalar</td></tr>';
            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0) {


                for (var x = 0; x < that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length; x++) {
                    tr = tr +
                        '<tr>' +
                        '<td title="' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst[x].desc +
                        '">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst[x].desc40 +
                        '</td>' +
                        '<td style="text-align: right;">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst[x].costosing +
                        '</td>' +
                        '<td style="text-align: right;">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst[x].costocigv +
                        '</td>' +
                        '</tr>'
                }
            } else {
                tr = tr + '<tr><td colspan="3" style="text-align: center;">No existen datos</td></tr>';
            }


            tr = tr +
                '<tr><td colspan="3" style="text-align: left;padding: 0px;background-color: #d9534f;color: #ffffff;">&nbsp;<span class="glyphicon glyphicon-minus">&nbsp;</span>Equipos a desinstalar</td></tr>';

            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {


                for (var x = 0; x < that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length; x++) {
                    tr = tr +
                        '<tr>' +
                        '<td title="' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].desc +
                        '">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].desc40 +
                        '</td>' +
                        '<td style="text-align: right;">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].costosing +
                        '</td>' +
                        '<td style="text-align: right;">' +
                        that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].costocigv +
                        '</td>' +
                        '</tr>'
                }
            } else {
                tr = tr + '<tr><td colspan="3" style="text-align: center;">No existen datos</td></tr>';
            }
            that.smmry.set('ListEqInstBaja', tr);

            var txtopcion = "";
            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0 &&
                that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {
                txtopcion = "INSTALACIÓN Y DESINSTALACIÓN";
            } else {
                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0) {
                    txtopcion = "INSTALACIÓN";
                }
                if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {
                    txtopcion = "DESINSTALACIÓN";
                }
            }
            that.smmry.set('Opcion', txtopcion);


        },
        getDecosMatriz: function () {
            var that = this, controls = that.getControls(), param = {};
            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;

            $.ajax({
                type: "POST",
                url: "/Transactions/LTE/UninstallInstallationOfDecoder/GetDecoMatriz",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                },
                success: function (response) {
                    var filas = response.ListDecos, index = 0;
                    that.objLteUninstallInstallDeco.CantPuntosMax = response.numDecosMax;
                    controls.lblPtosMax.text(parseInt(that.objLteUninstallInstallDeco.CantPuntosMax));

                    if (filas != null) {
                        $.each(filas,
                            function () {
                                var mat = {
                                    id: filas[index].TipoDeco,
                                    descripcion: filas[index].Descripcion,
                                    valor: filas[index].Valor
                                }
                                that.objLteUninstallInstallDeco.MatrizDecos.push(mat);
                                index++;
                            });
                    }
                }
            });
        },
        getMessageDeco: function (tipodeco) {
            var that = this;
            var result = "";
            var addpoint = 0;
            for (var z = 0; z < that.objLteUninstallInstallDeco.MatrizDecos.length; z++) {
                if (tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[z].descripcion ||
                    tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[z].id) {

                    addpoint = parseInt(that.objLteUninstallInstallDeco.MatrizDecos[z].valor);
                }
            }
            if (addpoint > 1) {
                result = that.objLteUninstallInstallDeco.strMsgLimiteDVR;
            } else {
                result = that.objLteUninstallInstallDeco.strMsgLimiteSdHd;
            }
            return result;

        },
        validateMatriz: function (listMisDecos, tipodeco) {
            var that = this;
            var result = false;
            var points = 0;
            var addpoint = 0;
            
            var listadecos = document.querySelector("#mis-decos").querySelectorAll("a");

            for (var x = 1; x < listadecos.length; x++) {

                if (that.objLteUninstallInstallDeco.MatrizDecos.length > 0) {
                    for (var y = 0; y < that.objLteUninstallInstallDeco.MatrizDecos.length; y++) {
                        if (listadecos[x].attributes['data-tipodeco'].nodeValue == that.objLteUninstallInstallDeco.MatrizDecos[y].descripcion ||
                            listadecos[x].attributes['data-tipodeco'].nodeValue == that.objLteUninstallInstallDeco.MatrizDecos[y].id) {
                            points = points + parseInt(that.objLteUninstallInstallDeco.MatrizDecos[y].valor);
                        }
                    }
                }
            }
            for (var z = 0; z < that.objLteUninstallInstallDeco.MatrizDecos.length; z++) {
                if (tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[z].descripcion ||
                    tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[z].id) {
                   
                    addpoint = parseInt(that.objLteUninstallInstallDeco.MatrizDecos[z].valor);
                }
            }
            if (parseInt(that.objLteUninstallInstallDeco.CantPuntosMax) < (points + addpoint)) {
                result = false;
            } else {
                result = true;
            }

            return result;

        },
        cboTipoTrabajo_Change: function () {
            var that = this,
                controls = that.getControls();
            that.validationEta();

        },
        cboSubTipoTrabajo_Change: function () {
            var that = this,
                controls = that.getControls();

            that.objLteUninstallInstallDeco.SubTipOrdCU = controls.cboSubTipoTrabajo.val();
            if (controls.cboSubTipoTrabajo.val() == "-1") {
                return false;
            }

            if (that.objLteUninstallInstallDeco.hndValidateETA == '1') {
                InitFranjasHorario();
                
            }
        },
        validationEta: function () {
            var that = this, controls = that.getControls(), param = {};
       
            param.IdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.strJobTypes = controls.cboTipoTrabajo.val();
            param.StrCodeUbigeo = that.SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
            param.StrTypeService = that.objLteUninstallInstallDeco.CodTipServLte;

            that.getLoadingPage();
            $.ajax({
                type: "POST",
                url: "/Transactions/SchedulingToa/GetValidateETA",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {
                },
                success: function (response) {
                    var oItem = response.data;
                    that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;
                    var fechaServidor = new Date(Session.ServerDate);
                    $('#txtFechaCompromiso').val([that.f_pad(fechaServidor.getDate()), that.f_pad(fechaServidor.getMonth() + 1), fechaServidor.getFullYear()].join("/"));

                    if (oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroUno || oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroCero) {
                        that.objLteUninstallInstallDeco.hndValidateETA = oItem.Codigo;
                        that.objLteUninstallInstallDeco.hndHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = oItem.Codigo;
                        Session.History = oItem.Codigo2;

                        if (oItem.Codigo == that.objLteUninstallInstallDeco.strNumeroUno) {
                            controls.cboSubTipoTrabajo.attr('disabled', false);
                             that.CargarSubTipoOrden();
                        
                        }
                        else {
                            alert("No aplica agendamiento en línea, favor de continuar con la operación.", "Alerta");
                            that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;
                            controls.cboSubTipoTrabajo.attr('disabled', true);
                            Session.ValidateETA = "0";
                            Session.History = "";
                            InitFranjasHorario();
                        }
                    } else {
                        alert(that.objLteUninstallInstallDeco.strMensajeValidationETA, "Alerta");
                        that.objLteUninstallInstallDeco.hndValidateETA = that.objLteUninstallInstallDeco.strNumeroCero;

                        InitFranjasHorario();
                        that.objLteUninstallInstallDeco.hndHistoryETA = oItem.Codigo2;
                        Session.ValidateETA = "0";
                        Session.History = oItem.Codigo2;
                    }
                }
            });

        },
        f_pad: function (s) { return (s < 10) ? '0' + s : s; },
        CargarSubTipoOrden: function () {
            var that = this,
                controls = that.getControls(),
                objSubTypeWork = {};

            objSubTypeWork.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            objSubTypeWork.strTipoTrabajo = controls.cboTipoTrabajo.val();

            $.ajax({
                type: "POST",
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetJobSubType',
                data: JSON.stringify(objSubTypeWork),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (data) {
                   
                },
                success: function (data) {
                    var filas = data.data, that = this,
                        intIndex = 0;
                    controls.cboSubTipoTrabajo.html("");
                 
                        controls.cboSubTipoTrabajo.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    

                    $.each(filas, function (key, value) {
                        intIndex++;
                        controls.cboSubTipoTrabajo.append($('<option>', { value: value.Code, html: value.Description }));
                    });

                    if (intIndex == 0 && that.objLteUninstallInstallDeco.hndValidateETA == "1") {
                        alert("No se encontraron subtipos de trabajo disponibles.", "Alerta");
                    }

                    controls.cboSubTipoTrabajo.attr('disabled', false);
                    controls.cboSubTipoTrabajo.change();
                }
            });
        },
        chkFidelizacion_Click: function () {
            var that = this,
                controls = that.getControls(),
                chkFidelizacion = controls.chkFidelizacion;


            if (chkFidelizacion[0].checked == true) {
            controls.lblMontoFidelizacion.text('0.00');
            that.objLteUninstallInstallDeco.LoyaltyAmount = '0.00';
            that.objLteUninstallInstallDeco.LoyaltyFlag = 1;
            } else {
                that.objLteUninstallInstallDeco.LoyaltyAmount = that.objLteUninstallInstallDeco.LoyaltyAmountTemp;
                controls.lblMontoFidelizacion.text(that.objLteUninstallInstallDeco.LoyaltyAmountTemp);
                that.objLteUninstallInstallDeco.LoyaltyFlag = 0;
            }

        },
        setTotal: function() {
            var that = this, controls = that.getControls();
           
            that.objLteUninstallInstallDeco.TotalAmountsIgv = parseFloat(parseFloat(that.objLteUninstallInstallDeco.BaseCharge) + parseFloat(that.objLteUninstallInstallDeco.CostoAdicional)).toFixed(2);
            that.objLteUninstallInstallDeco.TotalAmountcIgv = parseFloat(that.getRound((parseFloat(that.objLteUninstallInstallDeco.BaseCharge) * parseFloat(that.objLteUninstallInstallDeco.igv)), 2).toFixed(2)) +
                parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV);
            
            controls.lblCargoFijoAdicionalSIGV.text(parseFloat(that.objLteUninstallInstallDeco.CostoAdicional).toFixed(2));
            controls.lblCargoFijoAdicionalCIGV.text(parseFloat(that.objLteUninstallInstallDeco.CostoAdicionalCIGV).toFixed(2));
            
            controls.lblCargoFijoTotalPlanSIGV.text(parseFloat(that.objLteUninstallInstallDeco.TotalAmountsIgv).toFixed(2));
            controls.lblCargoFijoTotalPlanCIGV.text(parseFloat(that.objLteUninstallInstallDeco.TotalAmountcIgv).toFixed(2));
            
            var use = parseInt(that.objLteUninstallInstallDeco.intCantidadDecosTemp);
           
            for (var x = 0; x < that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length; x++) {
                use = parseInt(use) + parseInt(that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst[x].peso);
            }
            for (var y = 0; y < that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length; y++) {
                use = parseInt(use) - parseInt(that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[y].peso);
            }
            
            var libre = parseInt(that.objLteUninstallInstallDeco.CantPuntosMax) - parseInt(use);
            controls.lblPtosUse.text(use);
            controls.lblPtosFree.text(libre);
        },
        getWeightDeco: function (tipodeco) {
            var that = this, valretorno = 0;
            for (var x = 0; x < that.objLteUninstallInstallDeco.MatrizDecos.length; x++) {
                if (tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[x].descripcion ||
                    tipodeco == that.objLteUninstallInstallDeco.MatrizDecos[x].id) {

                    valretorno = parseInt(that.objLteUninstallInstallDeco.MatrizDecos[x].valor);
                }
            }
            return valretorno;
        },
        objLteUninstallInstallDeco: {
            ListaEquiposAdicionalesServer: {},
            ListaEquiposBajaServer: {},
            Typification: {},
            hndHistoryETA: "",
            hndValidateETA: "0",
            hidAgregaAsociar: 0,
            hidAgregaDesaso: 0,
            hidCerrar: 0,
            igv: 0,
            CodEquipAlSelec: "",
            strLetraF: "F",
            Slash: "/",
            strVariableEmpty: "",
            intNumeroCero: 0,
            intNumeroUno: 1,
            strNumeroCeroDecimal: "0.00",
            strNumeroMenosUno: "-1",
            strNumeroCero: "0",
            strNumeroUno: "1",
            strNumeroDos: "2",
            InstDesins: "0",
            hdnCodigoPlan: "",
            CostoAdicional: 0,
            CostoAdicionalCIGV: 0,
            CantAdicionalesInst: 0,
            CantAdicionalesDesint: 0,
            intCantidadDecos: 0,
            CantPuntosMax: 4,
            opcAct: 0,
            opcDesinst: 0,
            opcInst: 0,
            RequestActId: 0,
            FlajInstDesins: 0,
            ListaEquiposAdicionalesInst: [],
            ListaEquiposAdicionalesDesinst: [],
            MatrizDecos: [],
            TmCode: '',
            LoyaltyAmount: '0.00',
            strRutaPDF: "",
            LoyaltyFlag: 0,
            BaseCharge: 0,
            AdditionalCharge: 0,
            ServicesNumber: 0,
            LoyaltyAmountTemp: '',
            TotalAmountsIgv: 0,
            TotalAmountcIgv: 0,
            TypeLoyalty: 0,
            MotSotCode: '',
            intCantidadDecosTemp: 0,
            SubTipOrdCU: "",
            CodTipServLte: '',
            IdConsulta: '',
            IdInteraccion: '',
            FechaCompromiso: '',
            Franja: '',
            Idbucket: ''
        },
        SessionTransac: JSON.parse(sessionStorage.getItem("SessionTransac")),
        smmry: new Summary('transfer'),
        btnGuardar_Click: function () {
            var that = this, controls = that.getControls();

            that.getLoadingPage();

            var listDecos = [];

            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst.length > 0) {
                listDecos = that.objLteUninstallInstallDeco.ListaEquiposAdicionalesInst;
            }
            if (that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length > 0) {

                for (var x = 0; x < that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst.length; x++) {

                    var decoDes = {
                        id: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].id,
                        desc: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].description,
                        desc40: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].description40,
                        tipodeco: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].tipodeco,
                        costosing: 0,
                        costocigv: 0,

                        CodeService: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].CodeService,
                        SnCode: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].SnCode,
                        Cf: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].Cf,
                        SpCode: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].SpCode,
                        ServiceName: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].ServiceName,
                        ServiceType: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].ServiceType,
                        ServiceGroup: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].ServiceGroup,
                        Equipment: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].Equipment,
                        Quantity: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].Quantity,

                        Associated: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].Associated,
                        CodeInsSrv: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].CodeInsSrv,
                        SerialNumber: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].SerialNumber,
                        Flag: that.objLteUninstallInstallDeco.ListaEquiposAdicionalesDesinst[x].Flag
                    }

                    listDecos.push(decoDes);
                }



            }


            var param =
            {
                StrIdSession: that.SessionTransac.UrlParams.IDSESSION,
                StrContractId: that.SessionTransac.SessionParams.DATACUSTOMER.ContractID,
                strCustomerId: that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID,
                Decos: listDecos,
                LoyaltyAmount: that.objLteUninstallInstallDeco.LoyaltyAmount,
                LoyaltyFlag: that.objLteUninstallInstallDeco.LoyaltyFlag,
                EtaValidation: that.objLteUninstallInstallDeco.hndValidateETA
            };

            param.InsInteractionPlusModel = {};
            param.InsInteractionPlusModel.NameLegalRep = that.SessionTransac.SessionParams.DATACUSTOMER.LegalAgent;
            param.InsInteractionPlusModel.Basket = that.SessionTransac.SessionParams.DATASERVICE.Plan;
            param.InsInteractionPlusModel.Inter1 = that.SessionTransac.SessionParams.DATACUSTOMER.BillingCycle;
            param.InsInteractionPlusModel.ClaroLdn1 = that.SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
            param.InsInteractionPlusModel.Inter3 = that.SessionTransac.SessionParams.DATASERVICE.ActivationDate;
            param.InsInteractionPlusModel.Inter5 = that.SessionTransac.SessionParams.DATASERVICE.StateLine;
            param.InsInteractionPlusModel.Inter7 = that.SessionTransac.SessionParams.DATACUSTOMER.Address;
            param.InsInteractionPlusModel.Inter15 = $("#ddlCACDAC option:selected").text();
            param.InsInteractionPlusModel.Inter16 = that.SessionTransac.SessionParams.DATACUSTOMER.LegalDepartament;
            param.InsInteractionPlusModel.Inter17 = that.SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict;
            param.InsInteractionPlusModel.Inter18 = that.SessionTransac.SessionParams.DATACUSTOMER.LegalCountry;
            param.InsInteractionPlusModel.Inter19 = that.SessionTransac.SessionParams.DATACUSTOMER.LegalProvince;
            param.InsInteractionPlusModel.Inter20 = that.SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;//.PlaneCodeInstallation;
            param.InsInteractionPlusModel.Inter21 = listDecos.length;//cantidad
            param.InsInteractionPlusModel.Inter22 = that.getRoundDecimal(that.objLteUninstallInstallDeco.CostoAdicional);
            param.InsInteractionPlusModel.Inter23 = that.getRoundDecimal(controls.lblCargoFijoTotalPlanCIGV.text());
            param.InsInteractionPlusModel.Inter24 = that.getRoundDecimal(that.objLteUninstallInstallDeco.CostoAdicionalCIGV);
            param.InsInteractionPlusModel.ClaroLdn2 = (controls.chkEnviarPorEmail[0].checked == true ? '1' : '0');
            param.InsInteractionPlusModel.Email = (controls.chkEnviarPorEmail[0].checked == true ? controls.txtEnviarporEmail.val() : '');
            param.InsInteractionPlusModel.ClaroLdn4 = (controls.chkFidelizacion[0].checked == true ? '1' : '0');
            param.InsInteractionPlusModel.ClaroLocal1 = (controls.chkFidelizacion[0].checked == true ? '0.00' : that.objLteUninstallInstallDeco.LoyaltyAmount);///CAMBIAR COSTO
            param.InsInteractionPlusModel.ClaroLocal2 = that.objLteUninstallInstallDeco.FlajInstDesins;// 0 = Desinstalacion , 1 = Instalacion y 2 = Actualizacion(Inst + Desint)
            param.InsInteractionPlusModel.Inter25 = parseFloat((that.objLteUninstallInstallDeco.igv).toFixed(2));
            param.InsInteractionPlusModel.Inter29 = "";
            param.InsInteractionPlusModel.Inter6 = "0";
            param.InsInteractionPlusModel.District = that.SessionTransac.SessionParams.DATACUSTOMER.LegalDistrict;
            param.InsInteractionPlusModel.Inter30 = controls.txtNota.val();
            param.InsInteractionPlusModel.FirstName = that.SessionTransac.SessionParams.DATACUSTOMER.Name;
            param.InsInteractionPlusModel.LastName = that.SessionTransac.SessionParams.DATACUSTOMER.LastName;
            param.InsInteractionPlusModel.DocumentNumber = that.SessionTransac.SessionParams.DATACUSTOMER.DocumentNumber;
            param.InsInteractionPlusModel.RegistrationReason = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.InsInteractionPlusModel.ClaroNumber = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.InsInteractionPlusModel.TypeDocument = that.SessionTransac.SessionParams.DATACUSTOMER.DocumentType;
            param.InsInteractionPlusModel.Address = that.SessionTransac.SessionParams.DATACUSTOMER.Address;
            param.InsInteractionPlusModel.City = that.SessionTransac.SessionParams.DATACUSTOMER.InstallUbigeo;
            param.InsInteractionPlusModel.Reason = that.SessionTransac.SessionParams.DATACUSTOMER.BusinessName;
            param.InsInteractionPlusModel.Position = controls.txtFechaCompromiso.val();
            param.InsInteractionPlusModel.Address = that.SessionTransac.SessionParams.DATACUSTOMER.Reference;

            param.InteractionModel = {};
            param.InteractionModel.Tipo = that.objLteUninstallInstallDeco.Typification.Type;
            param.InteractionModel.Clase = that.objLteUninstallInstallDeco.Typification.Class;
            param.InteractionModel.Subclase = that.objLteUninstallInstallDeco.Typification.SubClass;
            param.InteractionModel.Agente = that.SessionTransac.SessionParams.USERACCESS.login;
            param.InteractionModel.AgenteName = that.SessionTransac.SessionParams.USERACCESS.fullName;
            param.InteractionModel.Notas = controls.txtNota.val();

            param.SotPending = {};
            param.SotPending.StrCoId = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.SotPending.StrTipTra = controls.cboTipoTrabajo.val();

            param.AuditRegister = {};
            param.AuditRegister.strNombreCliente = that.SessionTransac.SessionParams.DATACUSTOMER.FullName;
            param.AuditRegister.strTelefono = that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID;

            param.RegistrarProcesoPostventa = {};
            param.RegistrarProcesoPostventa.PiCodId = that.SessionTransac.SessionParams.DATACUSTOMER.ContractID;
            param.RegistrarProcesoPostventa.PiCustomerId = that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            param.RegistrarProcesoPostventa.PiCodplano = that.SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
            param.RegistrarProcesoPostventa.PiObservacion = controls.txtNota.val();
            param.RegistrarProcesoPostventa.PiFecProg = controls.txtFechaCompromiso.val();
            param.RegistrarProcesoPostventa.PiFranjaHor = '';
            param.RegistrarProcesoPostventa.PiTiptra = controls.cboTipoTrabajo.val();
            param.RegistrarProcesoPostventa.PiTmcode = that.objLteUninstallInstallDeco.TmCode;
            param.RegistrarProcesoPostventa.PiCodmotot = that.objLteUninstallInstallDeco.MotSotCode;
            param.RegistrarProcesoPostventa.PiTipoProducto = that.SessionTransac.UrlParams.SUREDIRECT;

            param.ImplementLoyalty = {};
            param.ImplementLoyalty.CustomerId = that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            param.ImplementLoyalty.DireccionFacturacion = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceAddress;
            param.ImplementLoyalty.NotasDireccion = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceUrbanization;
            param.ImplementLoyalty.Distrito = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceDistrict;
            param.ImplementLoyalty.Provincia = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceProvince;
            param.ImplementLoyalty.CodigoPostal = that.SessionTransac.SessionParams.DATACUSTOMER.LegalPostal;
            param.ImplementLoyalty.Departamento = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceDepartament;
            param.ImplementLoyalty.Pais = that.SessionTransac.SessionParams.DATACUSTOMER.InvoiceCountry;

            param.ImplementOcc = {};
            param.ImplementOcc.CustomerId = that.SessionTransac.SessionParams.DATACUSTOMER.CustomerID;
            param.ImplementOcc.Monto = that.objLteUninstallInstallDeco.LoyaltyAmount;
            param.ImplementOcc.FlagCobroOcc = that.objLteUninstallInstallDeco.LoyaltyFlag;

            if (param.EtaValidation === "0" || $("#cboFranjaHoraria option:selected").val() == "-1") {
                param.EtaValidation = "0";
            }

            if (param.EtaValidation !== "0") {
                var subtipoOrden = $("#cboSubTipoTrabajo option:selected").val();
                var franjaHoraria = $('#cboFranjaHoraria').val();

                var valuesFranjaHoraria = franjaHoraria.split('+');
                var valuesSubTipoOrden = subtipoOrden.split('|');

                param.RegistrarProcesoPostventa.PiFranjaHor = $("#cboFranjaHoraria option:selected").attr("idhorario");

                param.RegistrarEtaSeleccion = {};
                param.RegistrarEtaSeleccion.IdConsulta = Session.RequestActId;
                param.RegistrarEtaSeleccion.IdInteraccion = that.PadWithZeroes(Session.RequestActId);//"$CodigoInteraccion";
                param.RegistrarEtaSeleccion.FechaCompromiso = controls.txtFechaCompromiso.val();
                param.RegistrarEtaSeleccion.Franja = valuesFranjaHoraria[0];
                param.RegistrarEtaSeleccion.Idbucket = valuesFranjaHoraria[1];

                param.RegistrarEta = {};
                param.RegistrarEta.IdPoblado = that.SessionTransac.SessionParams.DATACUSTOMER.CodeCenterPopulate;
                param.RegistrarEta.DniTecnico = '';
                param.RegistrarEta.Franja = valuesFranjaHoraria[0];
                param.RegistrarEta.Idbucket = valuesFranjaHoraria[1];
                param.RegistrarEta.IpCreacion = '';
                param.RegistrarEta.SubTipoOrden = valuesSubTipoOrden[0];
                param.RegistrarEta.UsrCrea = that.SessionTransac.SessionParams.USERACCESS.login;
                param.RegistrarEta.FechaProg = controls.txtFechaCompromiso.val();
            }


            $.ajax({
                type: "POST",
                url: "/Transactions/LTE/UninstallInstallationOfDecoder/ExecuteTransaction",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function () {

                },
                success: function (response) {
                    if (response.ResponseCode === "0") {
                    that.objLteUninstallInstallDeco.strRutaPDF = response.UrlConstancy;
                        alert('<span>Se ejecutó correctamente la transacción.<br />La SOT generada es: ' +
                            response.SotNumber +
                            '</span><br /><span> El codigo de interaccion es: ' +
                            response.CodeInteraction +
                            '</span>',
                            'Mensaje');
                        controls.btnConstancia.attr('disabled', false);
                        controls.btnGuardar.attr('disabled', true);
                    } else {
                        if (response.ResponseCode === "1") {

                            if (response.SotNumber !== "") {
                                var sotPendientes = response.SotNumber.split(",");
                                response.ResponseMessage = response.ResponseMessage + ' ' + '(SOT: ' + sotPendientes[0] + ')';
                            }


                        }
                        controls.btnGuardar.attr('disabled', true);
                        alert(response.ResponseMessage, 'Alerta', function() {
                            location.reload();
                        });
                    }                    
                }
            });
        },
        getLoyaltyAmount: function () {
            var that = this, control = that.getControls(), param = {};
            param.strIdSession = that.SessionTransac.UrlParams.IDSESSION;
            param.iTipo = that.objLteUninstallInstallDeco.TypeLoyalty;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(param),
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetLoyaltyAmountLte',
                success: function (response) {
                    if (response != null) {
                        that.objLteUninstallInstallDeco.LoyaltyAmount = that.getRound(parseFloat(response) * parseFloat(that.objLteUninstallInstallDeco.igv), 2).toFixed(2);
                        that.objLteUninstallInstallDeco.LoyaltyAmountTemp = that.objLteUninstallInstallDeco.LoyaltyAmount;
                        control.lblMontoFidelizacion.text(that.objLteUninstallInstallDeco.LoyaltyAmount);
                        control.chkFidelizacion.prop("checked", false);
                    }
                },
                error: function () {
                }
            });

        },
        btnConstancia_Click: function () {
            var that = this;
        var pdfRoute = that.objLteUninstallInstallDeco.strRutaPDF;
        var idSession = that.SessionTransac.UrlParams.IDSESSION;
            ReadRecordSharedFile(idSession, pdfRoute);
        },
        getAmountsCurrentPlan: function () {
            var that = this, controls = that.getControls();
            var parametros = {
                strIdSession: that.SessionTransac.UrlParams.IDSESSION,
                strContractId: that.SessionTransac.SessionParams.DATACUSTOMER.ContractID
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(parametros),
                dataType: 'json',
                url: '/Transactions/LTE/UninstallInstallationOfDecoder/GetAmountCurrentPlan',
                success: function (response) {
                    if (response != null) {
                        that.objLteUninstallInstallDeco.ServicesNumber = response.CantidadServicios;
                        that.objLteUninstallInstallDeco.BaseCharge = response.MontoActualBase;
                        that.objLteUninstallInstallDeco.AdditionalCharge = response.MontoActualAdicional;

                        controls.lblCargoFijoPlanBaseSIGV.text(that.objLteUninstallInstallDeco.BaseCharge);
                        controls.lblCargoFijoPlanBaseCIGV.text(that.getRound((parseFloat(that.objLteUninstallInstallDeco.BaseCharge) * parseFloat(that.objLteUninstallInstallDeco.igv)), 2).toFixed(2));

                        controls.lblCargoFijoAdicionalSIGV.text(that.objLteUninstallInstallDeco.AdditionalCharge);
                        controls.lblCargoFijoAdicionalCIGV.text(that.getRound((parseFloat(that.objLteUninstallInstallDeco.AdditionalCharge) * parseFloat(that.objLteUninstallInstallDeco.igv)), 2).toFixed(2));

                        that.objLteUninstallInstallDeco.CostoAdicional = parseFloat(that.objLteUninstallInstallDeco.AdditionalCharge);
                        that.objLteUninstallInstallDeco.CostoAdicionalCIGV = parseFloat((that.objLteUninstallInstallDeco.AdditionalCharge) * parseFloat(that.objLteUninstallInstallDeco.igv)).toFixed(2);

                        var totalconIGV = that.getRound(parseFloat(that.objLteUninstallInstallDeco.BaseCharge) * parseFloat(that.objLteUninstallInstallDeco.igv) + parseFloat(that.objLteUninstallInstallDeco.AdditionalCharge) * parseFloat(that.objLteUninstallInstallDeco.igv)).toFixed(2);
                        controls.lblCargoFijoTotalPlanSIGV.text(that.getRound((parseFloat(that.objLteUninstallInstallDeco.BaseCharge) + parseFloat(that.objLteUninstallInstallDeco.AdditionalCharge)), 2).toFixed(2));
                        controls.lblCargoFijoTotalPlanCIGV.text(totalconIGV);
                        
                    }
                }
            });
        },
        btnCerrar01_Click: function() {
            window.close();
        },
        btnCerrar02_Click: function () {
            window.close();
        },
        PadWithZeroes: function(number) {
            var myString = '' + number;
            while (myString.length < 10) {
                myString = '0' + myString;
            }
            return myString;
        }
    };

    $.fn.UninstallInstallationOfDecoder = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('UninstallInstallationOfDecoder'),
                options = $.extend({}, $.fn.UninstallInstallationOfDecoder.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('UninstallInstallationOfDecoder', data);
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

    $.fn.UninstallInstallationOfDecoder.defaults = {
    }

    $('#divBody').UninstallInstallationOfDecoder();

})(jQuery);