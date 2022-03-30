(function ($, undefined) {



    var Form = function ($element, options) {
        $.extend(this, $.fn.PostpaidChangeTypeCustomer.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            //ConboBox
            , btnPopUpPlanDetailList: $("#btnPopUpPlanDetailList", $element)
            , chkDivPenalidadPCS: $(".chkDivPenalidadPCS", $element)
            , ddlArea: $("#ddlArea", $element)
            , ddlMotivo: $("#ddlMotivo", $element)
            , ddlLocalidad: $("#ddlLocalidad", $element)
            , rdbSinTopeConsumo: $("#rdbSinTopeConsumo", $element)
            , cboCACDAC: $('#ddlCACDAC', $element)
            , ddlNuevoCicloFact: $("#ddlNuevoCicloFact", $element)
            , ddlTopConsumtion: $("#ddlTopConsumtion", $element),

            //Label
            lblPost_BusinessName: $('#lblPost_BusinessName', $element),
            lblPost_DocumentNumber: $('#lblPost_DocumentNumber', $element),
            lblPost_ServiceTFI: $('#lblPost_ServiceTFI', $element),
            lblPost_ServiceActivationDate: $('#lblPost_ServiceActivationDate', $element),
            lblPost_BillingCycle: $('#lblPost_BillingCycle', $element),
            lblPost_IdentificationDocumentoLR: $('#lblPost_IdentificationDocumentoLR', $element),
            lblPost_CreditLimit: $('#lblPost_CreditLimit', $element),
            lblPost_AccountStatus: $('#lblPost_AccountStatus', $element),
            lblPost_LegalAgent: $('#lblPost_LegalAgent', $element),
            lblPost_DateExpiration: $('#lblPost_DateExpiration', $element),
            lblPost_LimitConsume: $('#lblPost_LimitConsume', $element),
            lblPost_Stop: $('#lblPost_Stop', $element),

            //Input

            //Hidden
            lblHeaderTelephone: $('#lblHeaderTelephone', $element),
            lblHeaderTypeCustomer: $('#lblHeaderTypeCustomer', $element),
            lblHeaderContact: $('#lblHeaderContact', $element),

            myModalLoad: $('#myModalLoad', $element)
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            control = this.getControls();

            //Initializing  controls
            control.btnPopUpPlanDetailList.addEvent(that, 'click', that.btnPopUpPlanDetailList_Click);
            control.chkDivPenalidadPCS.addEvent(that, 'click', that.chkDivPenalidadPCS_Click);
            control.ddlArea.addEvent(that, 'change', that.cboMotive_Click);
            control.ddlMotivo.addEvent(that, 'change', that.cboSubMotive_Click);
            control.ddlLocalidad.addEvent(that, 'change', that.cboBillingCycle_Click);
            control.rdbSinTopeConsumo.addEvent(that, 'click', that.rdbSinTopeConsumo_Click);
            that.maximizarWindow();
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
            that.loadSessionData();
        },

        loadSessionData: function () {
            var that = this,
                oHidden = Session.HIDDEN;
            var strIdSession = Session.IDSESSION;

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrlApp = urlBase + "/ValueAppSettings";

            //Traer los AppConfig
            $.app.ajax({
                url: myUrlApp,
                data: JSON.stringify(strIdSession),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (response) {
                    oHidden.gRouteSiteIni = response.strRutaSiteInicio;
                    oHidden.gConsTypeLineAct = response.gConstTipoLineaActual;
                    oHidden.gConstMsgNoEsPostNiFij = response.gConstMsjNoEsPostNiFijoPost;
                    oHidden.gConstTypeLineAbr = response.gConstTipoLineaAbrev;
                    oHidden.gConstMsgNoEsCob2Bu = response.gConstMsjNoEsCOB2BU;
                    oHidden.gFlagPlataformanceControl = response.strFlagPlataformaControl;
                    oHidden.gConstMsgFlgPlatafromaC = response.gConstMsjFlgPlataformaC;
                    oHidden.gConstCodePlanContNoApli = response.gConstCodPlanControlNoAplica;
                    oHidden.gStrTransAuditDate_ChangeCust = response.gStrTransAuditFechProg_CamTipClient;
                    oHidden.gTransAuditFide_ChangeCust = response.gStrTransAuditChckFideliza_CamTipClient;
                    oHidden.gTransAuditSinCost_ChangeCust = response.gStrTransAuditChckSinCosto_CamTipClient;
                    oHidden.gIgvConsumSol = response.IGVConsumosoles;
                    oHidden.gConstFidePenalChangeTypeCust = response.gConstFidelizaPenalidadCamTipClient;
                    oHidden.gConstModDateProgChangeType = response.gConstModFechaProgCamTipClient;
                    oHidden.gConstTopConSinCosttype = response.gConstTopeConsSinCostoCamTipClient;
                    oHidden.gConstDiferentMontCF = response.gConstDiferenciaMontoCF;
                    oHidden.gOpcTopConsAditional = response.OpcTopeConsumoAdicional;
                    oHidden.gConstChangeTitulari = response.gConstCambioTitularidad;
                    oHidden.gValidateCustomerJanus = response.gValidaClienteJanus;
                    oHidden.gMsgValidateJanus = response.gMensajeValidaJanus;
                    oHidden.gTopeConsumtionAuto = response.OpcTopeConsumoAutomatico;
                    oHidden.gListOpcTope = response.ListOpcTope;
                    oHidden.gOpcTopOrder = response.OpcTopeOrden;
                    oHidden.gOpcTopConsumtion5Sol = response.OpcTopeConsumo5soles;
                    oHidden.hidTransaccion = response.GstrTransaccionCambioTipoCliente;
                    oHidden.strCodeTypeCustomer = response.strCodTipoCli;

                    that.ValidatePermision();

                }
            });
        },

        //Validar Permiso
        ValidatePermision: function () {

            var that = this,
                oHidden = Session.HIDDEN,
                oCustomer = Session.DATACUSTOMER;

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strContractId = oCustomer.CONTRATO_ID;

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrlValidatePermision = urlBase + "/ValidatePermission";

            $.ajax({
                url: myUrlValidatePermision,
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    oHidden.strTempTypeTelephone = response;
                    if ((response.trim !== (oHidden.gConsTypeLineAct.split('|')[1].trim)) &&
                        (response !== (oHidden.gConsTypeLineAct.split('|')[0].trim))) {
                        alert(oHidden.gConstMsgNoEsPostNiFij,"Informativo");
                        parent.window.close();
                        return false;
                    } else {
                        that.ValidateCustomerJanus();
                    }
                    return false;
                }
            });
        },

        //Validar cliente Janus
        ValidateCustomerJanus: function () {
            var that = this,
                oHidden = Session.HIDDEN;

            var strIdSession = Session.IDSESSION;

            var janus = true;
            $.ajax({
                url: '/CommonServices/ValidateCustomerJanus',
                data: JSON.stringify(strIdSession),
                type: 'POST',
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    if (oHidden.gValidateCustomerJanus !== "0") {
                        janus = false;
                    }
                    if (janus) {
                        if (response !== "T") {
                            alert(oHidden.gMsgValidateJanus,"Alerta");
                            parent.window.close();
                        } else {
                            that.LoadPage();
                        }
                    } else {
                        that.LoadPage();
                    }
                }
            });
        },

        //Page_IsPostBack
        LoadPage: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oSessions = Session.USERACCESS,
                oHidden = Session.HIDDEN,
                oDatLine = Session.DATLINE;

            var pageIsPostBack = true;


            if (oCustomer.TELEFONO === "") {
                pageIsPostBack = false;
                parent.window.close();
                opener.parent.top.location.href = oHidden.gRouteSiteIni;
                return false;
            }
            if (oSessions.CODEUSER === "") {
                pageIsPostBack = false;
                parent.window.close();
                opener.parent.top.location.href = oHidden.gRouteSiteIni;
                return false;
            }

            var strTypeLineAct = oCustomer.TIPO_CLIENTE.substring(0, 2);
            if ((oHidden.gConstTypeLineAbr.indexOf(strTypeLineAct.toUpperCase())) === -1) {
                alert(oHidden.gConstMsgNoEsCob2Bu,"Alerta");
                pageIsPostBack = false;
                parent.window.close();
                return false;
            }

            if (oDatLine !== {}) {
                if (oDatLine.Flag_Plataforma.toUpperCase() === oHidden.gFlagPlataformanceControl) {
                    oHidden.hidConsumerControl = "SI";
                    alert(oHidden.gConstMsgFlgPlatafromaC,"Alerta");
                    pageIsPostBack = false;
                    parent.window.close();
                    return false;
                } else {
                    oHidden.hidConsumerControl = "NO";
                }

                //Cargar Tipificacion
                if (oHidden.hidConsumerControl === "SI") {
                    oHidden.hidCorporativo = "0";
                }
                else if (oCustomer.COD_TIPO_CLIENTE === oHidden.strCodeTypeCustomer.split('|')[1]) {
                    oHidden.hidCorporativo = "0";
                } else {
                    oHidden.hidCorporativo = "1";
                }
            }

            if (oDatLine !== {}) {
                var lstPlans = oHidden.gConstCodePlanContNoApli.split('|');
                if (lstPlans.length > 0) {
                    var codePlan;
                    var respta = false;
                    for (var index = 0; index < lstPlans.length; index++) {
                        codePlan = lstPlans[index];
                        if (codePlan.toUpperCase() === oDatLine.Cod_Plan_Tarifario) {
                            respta = true;
                            pageIsPostBack = false;
                        }
                    }
                    if (respta) {
                        alert(oHidden.gConstMsgFlgPlatafromaC,"Alerta");
                        pageIsPostBack = false;
                        parent.window.close();
                        return false;
                    }
                }
            }

            if (pageIsPostBack) {

                controls.lblPost_BusinessName.text(oCustomer.BusinessName);
                controls.lblPost_DocumentNumber.append(oCustomer.DNIRUC);
                controls.lblPost_ServiceTFI.append(oCustomer.ServiceTFI);
                controls.lblPost_ServiceActivationDate.append(oCustomer.ActivationDate);
                controls.lblPost_BillingCycle.append(oCustomer.BillingCycle);
                controls.lblPost_IdentificationDocumentoLR.append(oCustomer.IDENTIFICATIONDOCUMENTLR);
                controls.lblPost_CreditLimit.append(oCustomer.CreditLimit);
                controls.lblPost_AccountStatus.append(oCustomer.AccountStatus);
                controls.lblPost_LegalAgent.append(oCustomer.LegalAgent);
                controls.lblPost_DateExpiration.append(oCustomer.DATEEXPIRATION);
                controls.lblPost_LimitConsume.append(oCustomer.LimitConsume);
                controls.lblPost_Stop.append(oCustomer.STOP);
                controls.lblHeaderTelephone.append(oCustomer.TELEPHONE);
                controls.lblHeaderTypeCustomer.append(oCustomer.TYPECUSTOMER);
                controls.lblHeaderContact.append(oCustomer.BusinessName);

                var vCustCode = "";
                var vdnNum = "";
                var codeNroSerc = "";
                var strTipo = "";
                var intMontoTope = 0;

                var d = new Date();
                vCustCode = oCustomer.CUSTOMER_ID;
                vdnNum = oCustomer.TELEFONO;

                oHidden.hidDateAct = ((('0' + (d.getDate())).slice(-2)) + "/" + (('0' + (d.getMonth())).slice(-2)) + "/" + (d.getFullYear()));
                oHidden.hidDateLimit = ((('0' + (d.getDate())).slice(-2)) + "/" + (('0' + (d.getMonth() + 1)).slice(-2)) + "/" + (d.getFullYear()));
                oHidden.hidNroContract = oDatLine.CONTRATO_ID;
                oHidden.hidCicloFacturationCustomer = oCustomer.CICLO_FACTURACION;
                oHidden.hidTypeCustomerPost = oCustomer.TIPO_CLIENTE;
                oHidden.hidTelephone = oCustomer.TELEFONO;
                oHidden.hidMigration = Session.MIGRACION;
                oHidden.hidTypeCustomer = oCustomer.COD_TIPO_CLIENTE;
                oHidden.hidCodeOpcion = Session.CO;
                oHidden.hidCustCode = vCustCode;
                oHidden.hidContactId = oCustomer.OBJID_CONTACTO;
                oHidden.hidFlagPlataforma = oCustomer.Flag_Plataforma;
                oHidden.txtTotalPenalidadPcs = false;

                // ==== CargarDatosTransaccion =========
                this.InitDateAplication();
                this.InitConsumptionStop();
                // ==== Fin cargarDatosTransaccion =====

                this.InitNewTypeCustomer();
                this.InitCacDat();
                this.InitArea();
                this.InitGetTypification();
            }
            return false;
        },

        //combobox Nuevo Tipo de Cliente
        InitNewTypeCustomer: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = Session.IDSESSION;
            model.strNameFunction = "POSTPAGO";
            model.strFlagCode = "TipoDeClienteMOV";
            model.fileName = "Data.xml";


            var myUrl = "/CommonServices/GetListValueXmlMethod";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    controls.ddlLocalidad.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.ddlLocalidad.append($('<option>', { value: value.Code, html: value.Description }));
                        });
                    }
                },
                error: function (XError) {
                    //console.logXError);
                }
            });
        },

        //combobox Area
        InitArea: function () {
            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            $.ajax({
                url: "/CommonServices/GetArea",
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: "application/json charset=utf-8",
                dataType: "json",
                success: function (result) {
                    var content = "";
                    content += "<option value='0'>..Seleccione..</option>";
                    $.each(result, function (index, item) {
                        content += "<option value='" + item.strCode + "'>" + item.strDescription + "</option>";
                    });
                    $("#ddlArea").empty().html(content);
                    content = "";
                },
                error: function (XError) {
                    //console.logXError);
                }
            });
        },

        //combobox CacDac
        InitCacDat: function () {
            var that = this,
                controls = that.getControls();

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = "C12640";

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

        //Obtener fecha de aplicacion
        InitDateAplication: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                parameters = {};

            parameters.strCicleFacturation = oCustomer.CICLO_FACTURACION;

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrl = urlBase + "/GetDateApplication";

            $.ajax({
                url: myUrl,
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    $('#txtDateProgram').val(response);
                }
            });
        },

        //Obtener tope de consumo
        InitConsumptionStop: function () {
            var that = this,
                controls = that.getControls(),
                oHidden = Session.HIDDEN,
                parameters = {};

            parameters.strIdSession = Session.IDSESSION;
            parameters.strListTope = oHidden.gListOpcTope;

            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrl = urlBase + "/GetConsumptionStop";

            $.ajax({
                url: myUrl,
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    controls.ddlTopConsumtion.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response != null) {
                        $.each(response, function (index, value) {
                            if (value.strNumber === oHidden.gOpcTopOrder) {
                                oHidden.hidOpTopConsOrd = value.strNumber;
                                oHidden.hidOpTopConsCod = value.strCode;
                                oHidden.hidOpTopConsDesc = value.strDescription;
                            }
                            if (value.strCode === oHidden.gOpcTopConsumtion5Sol) {
                                oHidden.hidOpTopCons5Soles = value.strCode;
                            }

                            controls.ddlTopConsumtion.append($('<option>', { value: value.strCode, html: value.strNumber }));
                        });
                    }
                }
            });
        },

        //Obtener tipificacion
        InitGetTypification: function () {
            var that = this,
                controls = that.getControls(),
                oHidden = Session.HIDDEN,
                parameters = {};

            parameters.strIdSession = Session.IDSESSION;
            parameters.strTransactionName = oHidden.hidTransaccion;
            parameters.strType = oHidden.strTempTypeTelephone;

            $.ajax({
                url: '/CommonServices/GetTypification',
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: 'application/json charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var result = response.ListTypification;
                    if (result != null) {

                    }
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

        btnPopUpPlanDetailList_Click: function () {
            var that = this;
            var urlBase = window.location.href;
            urlBase = urlBase.substr(0, urlBase.lastIndexOf('/'));
            var myUrl = urlBase + "/PostpaidPlanDetail";

            $.window.open({
                modal: true,
                controlBox: true,
                maximizeBox: false,
                minimizeBox: false,
                title: 'Seleccione el Nuevo Plan',
                url: myUrl,
                data: {},
                width: 750,
                height: 500,
                buttons: {
                    Seleccionar: {
                        click: function () {
                            that.AddPlainDetail();
                        }
                    },
                    Cerrar: {
                        click: function () {
                            this.close();
                        }
                    }
                }
            });
        },

        chkDivPenalidadPCS_Click: function () {
            var value = $("#chkDivPenalidadPCS").is(':checked');
            if (value == true) {
                $("#divArea").show();
            } else {
                $("#divArea").hide();
            }

        },

        cboMotive_Click: function () {
            var idArea = $("#ddlArea").val();

            if (idArea == 0) {
                $("#ddlMotivo").empty().html('');
                $("#ddlSubMotivo").empty().html('');
                $("#ddlMotivo").attr('disabled', true);
                $("#ddlSubMotivo").attr('disabled', true);
                return false;
            }

            $.ajax({
                url: "/CommonServices/GetMotive",
                data: {
                    strIdSession: Session.IDSESSION,
                    strIdArea: idArea
                },
                type: 'get',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (result) {
                    $("#ddlMotivo").attr('disabled', false);
                    var content = "";
                    content += "<option value='0'>..Seleccione..</option>";
                    $.each(result, function (index, item) {
                        content += "<option value='" + item.strCode + "'>" + item.strDescription + "</option>";
                    });
                    $("#ddlMotivo").empty().html(content);
                    content = "";
                },
                error: function (XError) {
                    //console.logXError);
                }
            });
        },

        cboSubMotive_Click: function () {
            var idArea = $("#ddlArea").val();
            var idMotive = $("#ddlMotivo").val();

            if (idArea == 0 || idMotive == 0) {
                $("#ddlSubMotivo").empty().html('');
                $("#ddlSubMotivo").attr('disabled', true);
                return false;
            }

            $.ajax({
                url: "/CommonServices/GetSubMotive",
                data: {
                    strIdSession: Session.IDSESSION,
                    strIdArea: idArea,
                    strIdMotive: idMotive
                },
                type: 'get',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (result) {
                    $("#ddlSubMotivo").attr('disabled', false);
                    var content = "";
                    content += "<option value='0'>..Seleccione..</option>";
                    $.each(result, function (index, item) {
                        content += "<option value='" + item.strCode + "'>" + item.strDescription + "</option>";
                    });
                    $("#ddlSubMotivo").empty().html(content);
                    content = "";
                },
                error: function (XError) {
                    //console.logXError);
                }
            });
        },

        cboBillingCycle_Click: function () {

            var strTypeCustomer = $("#ddlLocalidad").val();
            if (strTypeCustomer == "") {
                $("#ddlNuevoCicloFact").attr('disabled', 'disabled');
                $("#ddlNuevoCicloFact").html('');
                return false;
            } else {
                $("#ddlNuevoCicloFact").html('');
            }

            var that = this,
                controls = that.getControls(),
                parameters = {};
            parameters.strTypeCustomer = strTypeCustomer;
            parameters.strIdSession = Session.IDSESSION;

            $.ajax({
                url: "/CommonServices/GetBillingCycle",
                data: JSON.stringify(parameters),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    $("#ddlNuevoCicloFact").attr('disabled', false);
                    controls.ddlNuevoCicloFact.append($('<option>', { value: '', html: 'Seleccionar' }));
                    if (response != null) {
                        $.each(response, function (index, value) {
                            controls.ddlNuevoCicloFact.append($('<option>', { value: value.strBicicle, html: value.strBicicle }));
                        });
                    }
                }
            });
            return false;
        },

        rdbSinTopeConsumo_Click: function () {
            alert("hello","Alerta");
        },

        AddPlainDetail: function () {

        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },

    };
    $.fn.PostpaidChangeTypeCustomer = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostpaidChangeTypeCustomer'),
                options = $.extend({}, $.fn.PostpaidChangeTypeCustomer.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostpaidChangeTypeCustomer', data);
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

    $.fn.PostpaidChangeTypeCustomer.defaults = {
    }

    $('#PostpaidChangeTypeCustomer').PostpaidChangeTypeCustomer();

})(jQuery);
