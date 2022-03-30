(function ($, undefined) {


    var Form = function ($element, options) {

        $.extend(this, $.fn.PostPlanMigrationDetail.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element
          , tblServiciosAsociadosPlan_Post: $('#tblServiciosAsociadosPlan_Post', $element)
          , tblServiciosAsociadoAgregadosPlan_Post: $('#tblServiciosAsociadoAgregadosPlan_Post', $element)
          , tblAgregarBoton: $("#tblAgregarBoton", $element)

          , btnCerrar: $('#btnCerrar', $element)
          , btn_PostNuevoPlan: $('#btn_PostNuevoPlan', $element)
          , btn_Post_ValidarBoleta: $("#btn_Post_ValidarBoleta", $element)
          , btnConstancia: $("#btnConstancia", $element)
          , btnGuardar: $("#btnGuardar", $element)
          , btnAgregarServicio_Post: $("#btnAgregarServicio_Post", $element)

          , txt_rdSinTopeConsumo: $('#txt_rdSinTopeConsumo', $element)
          , txtPost_SendforEmail: $("#txtPost_SendforEmail", $element)
          , txtPlanNuevoAgregado: $('#txtPlanNuevoAgregado', $element)
          , txt_Post_MontoFidelizaApadece: $('#txt_Post_MontoFidelizaApadece', $element)
          , txt_Post_TotalPenalidadCobrar: $("#txt_Post_TotalPenalidadCobrar", $element)
          , Post_txtFechaAplicacion: $("#Post_txtFechaAplicacion", $element)
          , txt_RegistroApadece_Post: $("#txt_RegistroApadece_Post", $element)//txtMontoSIGA
          , txt_CobroApadece_Post: $("#txt_CobroApadece_Post", $element)
          , txt_Post_PenalidadPCS: $("#txt_Post_PenalidadPCS", $element)
          , txt_Post_TotalFidelizacionPenalidadPCS: $("#txt_Post_TotalFidelizacionPenalidadPCS", $element)
          , txt_Post_TotalPenalidadPCSCobrar: $("#txt_Post_TotalPenalidadPCSCobrar", $element)
          , txtNota: $("#txtNota", $element)
          , txt_Post_NumeroBoleta: $("#txt_Post_NumeroBoleta", $element)

          , rdSinTopeConsumo: $('#rdSinTopeConsumo', $element)
          , rdConTopeConsumo: $('#rdConTopeConsumo', $element)
          , rdbPagoefectivo: $("#rdbPagoefectivo", $element)
          , rdbNotaDebito: $("#rdbNotaDebito", $element)

          , chkSinCostoTope: $('#chkSinCostoTope', $element)
          , chkMantenerTopeConsumo: $("#chkMantenerTopeConsumo", $element)
          , chkFidelizaPenalidad: $("#chkFidelizaPenalidad", $element)
          , chckApadeceNoAplica: $("#chckApadeceNoAplica", $element)
          , chk_Post_OCC: $("#chk_Post_OCC", $element)
          , chkFidelizaPenalidadPCS: $("#chkFidelizaPenalidadPCS", $element)
          , chk_Post_PenalidadPCS: $("#chk_Post_PenalidadPCS", $element)
          , chkFidelizaApadece: $("#chkFidelizaApadece", $element)
          , chkSentEmail: $("#chkSentEmail", $element)

          , lbl_PostMigracionPlanActual: $("#lbl_PostMigracionPlanActual", $element)
          , lblNuevoPlan_Post: $('#lblNuevoPlan_Post', $element)
          , lbl_PostControlMigration: $("#lbl_PostControlMigration", $element)
          , lblPost_CargoFijoTotalPlanSin: $("#lblPost_CargoFijoTotalPlanSin", $element)
          , lblPost_CargoFijoTotalPlanCon: $("#lblPost_CargoFijoTotalPlanCon", $element)
          , lblArea: $("#lblArea", $element)
          , lblMotivo: $("#lblMotivo", $element)
          , lblSubMotivo: $("#lblSubMotivo", $element)
          , lblPost_CargoFijoNuevoPlanBase: $("#lblPost_CargoFijoNuevoPlanBase", $element)
          , lblPost_CargoFijoDescNuevoPlanBase: $("#lblPost_CargoFijoDescNuevoPlanBase", $element)
          , lblPost_CargoTotalDelPlanSin: $("#lblPost_CargoTotalDelPlanSin", $element)
          , lblPost_CargoTotalDelPlanCon: $("#lblPost_CargoTotalDelPlanCon", $element)
          , lblMensajeCantidadServiciosAgr: $("#lblMensajeCantidadServiciosAgr", $element)
          , lblPost_Cliente: $("#lblPost_Cliente", $element)
          , lblPost_CicloFacturacion: $("#lblPost_CicloFacturacion", $element)
          , lblPost_LimiteCredito: $("#lblPost_LimiteCredito", $element)
          , lblPost_RepresentanteLegal: $("#lblPost_RepresentanteLegal", $element)
          , lblPost_Documento: $("#lblPost_Documento", $element)
          , lblPost_FechaActivacion: $("#lblPost_FechaActivacion", $element)
          , lblPost_TipoAcuerdo: $("#lblPost_TipoAcuerdo", $element)
          , lblPost_EstadoLinea: $("#lblPost_EstadoLinea", $element)
          , lblPost_TopeConsumoActual: $("#lblPost_TopeConsumoActual", $element)
          , lblPhoneNumber: $("#lblPhoneNumber", $element)
          , lblCustomerType: $("#lblCustomerType", $element)
          , lblCustomerContact: $("#lblCustomerContact", $element)
          , lblDueDate: $("#lblDueDate", $element)
          , lblAdditionalMoneyBag: $("#lblAdditionalMoneyBag", $element)

          , cbo_PostMigrationCacDac: $("#cbo_PostMigrationCacDac", $element)
          , ddlArea: $("#ddlArea", $element)
          , ddlMotivo: $("#ddlMotivo", $element)
          , ddlSubMotivo: $("#ddlSubMotivo", $element)
          , ddlTopesConsumo: $("#ddlTopesConsumo", $element)

          , trIDCantidadCFijo2: $("#trIDCantidadCFijo2", $element)
          , trIDCargoFijo2: $("#trIDCargoFijo2", $element)
          , trTopeConsumo: $("#trTopeConsumo", $element)
          , trBolsaSoles: $("#trBolsaSoles", $element)
          , idTRListaPlanCombo: $("#idTRListaPlanCombo", $element)//En STEP
          , idTrNuevoPlan: $("#idTrNuevoPlan", $element)
          , idtrLeyenda: $("#idtrLeyenda", $element)
          , IdTrArmaPlanesCombos: $("#IdTrArmaPlanesCombos", $element)
          , tbCargoFijoPlanBase: $("#tbCargoFijoPlanBase", $element)

          , divPCS: $("#divPCS", $element)
          , dTextCorreo: $("#dTextCorreo", $element)
          , dvValidacionBoleta: $("#dvValidacionBoleta", $element)
          , divReglasMigracionPlan: $("#divReglasMigracionPlan", $element)

          , lblTitle: $('#lblTitle', $element)   //Redirect 1.0
        });

    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this;
            var controls = this.getControls();
            controls.Post_txtFechaAplicacion.datepicker({format : 'dd/mm/yyyy'});
            controls.btn_PostNuevoPlan.addEvent(that, 'click', that.btn_PostNuevoPlan_click);
            controls.btnGuardar.addEvent(that, 'click', that.btnGuardar_click);
            controls.btnAgregarServicio_Post.addEvent(that, 'click', that.btnAgregarServicio_Post_click);
            controls.ddlArea.addEvent(that, 'change', that.cboMotive_Click);
            controls.ddlMotivo.addEvent(that, 'change', that.cboSubMotive_Click);

            controls.chk_Post_PenalidadPCS.addEvent(that, 'change', that.chk_Post_PenalidadPCS_change);
            controls.chkSentEmail.addEvent(that, 'change', that.chkSentEmail_change);
            controls.chkMantenerTopeConsumo.addEvent(that, 'change', that.chkMantenerTopeConsumo_change);
            controls.rdSinTopeConsumo.addEvent(that, 'change', that.rdSinTopeConsumo_change);
            controls.rdConTopeConsumo.addEvent(that, 'change', that.rdConTopeConsumo_change);
            controls.chkSinCostoTope.addEvent(that, 'change', that.chkSinCostoTope_change);
            controls.ddlTopesConsumo.addEvent(that, 'change', that.ddlTopesConsumo_change);
            controls.chkFidelizaPenalidad.addEvent(that, 'change', that.chkFidelizaPenalidad_change);
            controls.chkFidelizaPenalidadPCS.addEvent(that, 'change', that.chkFidelizaPenalidadPCS_change);
            controls.rdbPagoefectivo.addEvent(that, 'change', that.rdbPagoefectivo_change);
            controls.rdbNotaDebito.addEvent(that, 'change', that.rdbNotaDebito_change);

            controls.txt_CobroApadece_Post.addEvent(that, 'keyup', that.txt_CobroApadece_Post_onkeyup);

            controls.txt_Post_MontoFidelizaApadece.addEvent(that, 'blur', that.txt_Post_MontoFidelizaApadece_blur);
            controls.txt_CobroApadece_Post.addEvent(that, 'blur', that.txt_CobroApadece_Post_blur);
            controls.txt_Post_PenalidadPCS.addEvent(that, 'blur', that.txt_Post_PenalidadPCS_blur);
            controls.txt_Post_TotalFidelizacionPenalidadPCS.addEvent(that, 'blur', that.txt_Post_TotalFidelizacionPenalidadPCS_blur);

            controls.txt_Post_MontoFidelizaApadece.addEvent(that, 'keypress', that.txt_Post_MontoFidelizaApadece_keypress);
            controls.txt_CobroApadece_Post.addEvent(that, 'keypress', that.txt_CobroApadece_Post_Keypress);
            controls.txt_Post_PenalidadPCS.addEvent(that, 'keypress', that.txt_Post_PenalidadPCS_keypress);
            controls.txt_Post_TotalFidelizacionPenalidadPCS.addEvent(that, 'keypress', that.txt_Post_TotalFidelizacionPenalidadPCS_keypress);
            controls.lblNuevoPlan_Post.hide();
            that.maximizarWindow();
            that.loadSessionData();
            
        },
        loadSessionData: function () {
            //Redirect ini  2.0
            var that = this,
            controls = this.getControls();
            
            controls.lblTitle.text("CAMBIO DE PLAN");
            //console.log"Redireccionó a la Transacion");
            var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
            //console.logSessionTransac);
            //Redirect fin
            
            Session.IDSESSION = Session.IDSESSION;
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;
                       
        },        
        mostrarMensaje: function () {
            alert("Se generará tipificación.","Informativo");
        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        
    loadValidatePage: function () {
            var that = this;
        
            var strUrlLogo = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';
            $.blockUI({
                message: '<div align="center"><img src="' + strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
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
    unLoadPage: function () {
        
        $.unblockUI()
        controls.btnGuardar.prop("disabled", true);        
                    
    },
    loadHeaderTransaction: function () {
            var that = this,
                controls = that.getControls();
            controls.lblCustomerContact.text(Session.DATACUSTOMER.CustomerContact);
            controls.lblCustomerType.text(Session.DATACUSTOMER.CustomerType);
            controls.lblPhoneNumber.text(Session.DATACUSTOMER.Telephone);
            controls.lblPost_Cliente.text(Session.DATACUSTOMER.FullName);
            controls.lblPost_Documento.text(Session.DATACUSTOMER.DocumentNumber);
            controls.lbl_PostMigracionPlanActual.text(Session.DATASERVICE.Plan);
            controls.lblPost_FechaActivacion.text(Session.DATASERVICE.ActivationDate);
            controls.lblPost_CicloFacturacion.text(Session.DATACUSTOMER.objPostDataAccount.BillingCycle);
            controls.lblPost_LimiteCredito.text(Session.DATACUSTOMER.objPostDataAccount.CreditLimit);
            controls.lblPost_EstadoLinea.text(Session.DATASERVICE.StateLine);
            controls.lblPost_RepresentanteLegal.text(Session.DATACUSTOMER.LegalAgent);

            //controls.lblPost_TipoAcuerdo.text("Nommal"); //Se carga del apadece
            //controls.lblDueDate.text("") //método CargarReciboDetallado        
            //controls.lblPost_TopeConsumoActual.text("TopePrueba"); //Se carga con tope de consumo actual

        },
        loadConfigVariables: function () {
            var that = this, controls = that.getControls();
            $.app.ajax({
                type: 'POST',
                url: '/Transactions/Postpaid/PlanMigration/GetMessage',
                data: {},
                success: function (response) {
                    that.strConfigVariables.strLineType = response[0];
                    that.strConfigVariables.strMsgNotPlan = response[1];
                    that.strConfigVariables.strLineTypeAbrv = response[2];
                    that.strConfigVariables.strFlagPlatform = response[3];
                    that.strConfigVariables.strTransAuditFchProg = response[4];
                    that.strConfigVariables.strTransAuditChkFidelize = response[5];
                    that.strConfigVariables.strTransAuditNotCost = response[6];
                    that.strConfigVariables.strPermissionCheck = response[7];
                    that.strConfigVariables.strPermissionFchProg = response[8];
                    that.strConfigVariables.strDiferenceMountCF = response[9];
                    that.strConfigVariables.strPermissionNotTop = response[10];
                    that.strConfigVariables.strCodServAdditional = response[11];
                    that.strConfigVariables.strCustomerType = response[12];
                    that.loadPage();
                }
            });
        },
        loadPage: function(){
            var that = this, controls = that.getControls();

            //Método para obtener tipo de linea.
            var strCurrentCustomerType = Session.DATACUSTOMER.CustomerType;
            strCurrentCustomerType = strCurrentCustomerType.substring(0, 2);
            strCurrentCustomerType = strCurrentCustomerType.toUpperCase();
            var strLineTypeAbv = that.strConfigVariables.strLineTypeAbrv;
            if (strLineTypeAbv.indexOf(strCurrentCustomerType) < 0) {
                alert(that.strConfigVariables.strMsgNotPlan,"Alerta");
                parent.window.close();
            }
            
            if (Session.DATACUSTOMER.FlagPlatform == that.strConfigVariables.strFlagPlatform) {
                that.strConsumerControl = 'SI';
            } else {
                that.strConsumerControl = 'NO';
            }

            if (that.strConsumerControl == "SI") {
                controls.trTopeConsumo.hide();
                controls.trBolsaSoles.hide();
            } else {
                controls.trTopeConsumo.show();
                controls.trBolsaSoles.show();
            }

            //controls.lblNuevoPlan_Post.attr("style", "display:none");
            controls.lblNuevoPlan_Post.hide();
            //controls.btn_Post_ValidarBoleta.attr("style", "display:none");
            controls.btn_Post_ValidarBoleta.hide();
            controls.btnConstancia.prop('disabled', true);
            controls.dTextCorreo.hide();
            controls.idtrLeyenda.hide();
            that.validacionTope()

            controls.divPCS.hide();
            controls.txt_Post_TotalPenalidadCobrar.prop('disabled', true);

            that.ChargeMain();
            //that.GetValidationProgDeudaBloqSuspResponse();
            that.loadDataMigration();
            that.Inicio();
            that.getCACDAC();
            //that.InitArea();
            //that.getLoadtblServiciosAsociadosPlan_Post();
            //that.getConfigTfiBam();
            //that.getTypification();
        },
        loadTransaction: function(){

        },
        ChargeMain: function () {
            var that = this,
                controls = that.getControls();
            
            var objChargeMain = {};
            objChargeMain.strIdSession = Session.IDSESSION;
            objChargeMain.ConsumerControl = that.strConsumerControl;
            objChargeMain.codTipoCliente = Session.DATACUSTOMER.CodCustomerType;
            objChargeMain.telephone = Session.TELEPHONE;
            objChargeMain.contratoId = Session.DATACUSTOMER.ContractID;
            objChargeMain.HidCorporativo = Session.HidCorporativo;
            objChargeMain.FlagPlataforma = Session.DATASERVICE.FlagPlatform;
            objChargeMain.TipoCliente = Session.DATACUSTOMER.CustomerType;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Postpaid/PlanMigration/ChargeMain',
                data: JSON.stringify(objChargeMain),
                success: function (response) {
                    that.createDropdownddlTopesConsumo(response.data);
                    Session.hidOpTopeConsOrd = response.data.hidOpTopeConsOrd;
                    Session.hidOpTopeConsCod = response.data.hidOpTopeConsCod;
                    Session.hidOpTopeConsDesc = response.data.hidOpTopeConsDesc;
                    Session.hidOpTopeCons5Soles = response.data.hidOpTopeCons5Soles;
                    Session.hidValidaTipoCliente = response.data.hidValidaTipoCliente;
                    Session.hidCobroApadece = response.data.hidCobroApadece;
                    Session.hidCargoFijoTotalPlan = response.data.hidCargoFijoTotalPlan;
                    Session.CodPlanClaroConexionChip = response.data.CodPlanClaroConexionChip;
                    Session.gConstMsjPlanClaroConexionChip = response.data.gConstMsjPlanClaroConexionChip;
                    Session.TipoClienteAplicacion = response.data.TipoClienteAplica;
                    Session.gCodCostoCero = response.data.gCodCostoCero;
                    Session.CodPlanSinTopeConsAutorizacion = response.data.CodPlanSinTopeConsAutorizacion;
                    Session.hidOpTopeCodAutomatico = response.data.hidOpTopeCodAutomatico;
                    Session.hidConsumerControl = response.data.hidConsumerControl;
                    Session.hidCorporativo = response.data.hidCorporativo;
                    Session.hidFechaActual = response.data.hidFechaActual;
                    Session.hidFechaLimite = response.data.hidFechaLimite;
                    //controles
                    controls.txt_RegistroApadece_Post.val(response.data.txtMontoSIGA);
                    controls.txt_CobroApadece_Post.val(response.data.txtCobroApadece);
                    controls.txt_CobroApadece_Post.attr('disabled', response.data.txtCobroApadeceEnable);
                    controls.chkFidelizaPenalidad.attr('disabled', response.data.chkFideliza);
                    controls.chk_Post_OCC.prop('checked', response.data.chkOcc);
                    controls.txt_Post_MontoFidelizaApadece.val(response.data.txtMontoFidelizaApadece);
                    controls.txt_Post_TotalPenalidadCobrar.val(response.data.txtTotalApadeceCobrar);
                    controls.txt_Post_PenalidadPCS.val(response.data.txtCobroPenalidadPCS);
                    controls.txt_Post_TotalFidelizacionPenalidadPCS.val(response.data.txtMontoPenalidadPCS);
                    controls.txt_Post_TotalPenalidadPCSCobrar.val(response.data.txtTotalPenalidadPCS);

                    controls.strConfigPlantaforma = response.data.strConfigPlantaforma;
                    
                    if (response.result[0] != "POSTPAGO") {
                        alert(result[1]);
                        window.close();
                    }
                }
            });
        },
        GetValidationProgDeudaBloqSuspResponse: function () {
            var that = this, objValidateType = {};
            objValidateType.strIdSession = Session.IDSESSION;
            objValidateType.contract = Session.DATACUSTOMER.CustomerContact;
            objValidateType.phoneNumber = Session.DATACUSTOMER.Telephone;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objValidateType),
                url: '/Transactions/Postpaid/PlanMigration/GetValidationProgDeudaBloqSuspResponse',
                success: function (response) {
                    var respuesta = response.data.RespuestaValidacion.split('|');
                    if (strResultValid != "") {
                        if (respuesta[0] == "1") {
                            alert(respuesta[1],"Alerta");
                            parent.window.close();
                            return;
                        } else if (respuesta[0] == "2") {
                            if (respuesta[1].split(';').length == 1) {
                                alert(respuesta[1],"Alerta");
                                parent.window.close();
                                return;
                            } else {
                                that.IsBloqAllowConfiguration(respuesta[1].split(';')[1], function () {
                                    var isblog = Session.IsBloqAllowConfiguration;
                                    if (isblog[0] != "true") {
                                        var alert = respuesta[1].split(";")[0];
                                        if (isblog[1].length > 0) {
                                            alert = alert + ":" + isblog[1];
                                        }
                                    }
                                });
                            }
                        }

                    }
                }
            });
        },
        IsBloqAllowConfiguration: function (bloq) {
            var objIsBloqAllowConfiguration = {};
            objIsBloqAllowConfiguration.strIdSession = Session.IDSESSION;
            objIsBloqAllowConfiguration.Bloq = Session.DATACUSTOMER.CustomerContact;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objIsBloqAllowConfiguration),
                url: '/Transactions/Postpaid/PlanMigration/GetIsBloqAllowConfiguration',
                success: function (response) {
                    Session.IsBloqAllowConfiguration = response;//[0] bool to string--//[1] descripcion
                }
            });
        },
        loadDataMigration: function () {
            var controls = this.getControls();
            var that = this;
            that.loadTransactionType();
            that.loadApplicationDate();
        },
        loadTransactionType: function () {
            var controls = this.getControls(),
                that = this,
                strCustomerType;

            if (Session.DATACUSTOMER.FlagPlatform == that.strConfigVariables.strFlagPlatform) {
                that.strTransactionTypi = "TRANSACCION_MIGRACION_CTRL_POST";
            } else {
                that.strTransactionTypi = "TRANSACCION_MIGRACION_PLAN_COMBO";
            }

            strCustomerType = that.strConfigVariables.strCustomerType.split("|")[0];

            if (that.strConsumerControl = 'SI') {
                that.strCorporate = 0;
            }
            else if (Session.DATACUSTOMER.CustomerTypeCode == strCustomerType) {
                that.strCorporate = 0;
            }
            else {
                that.strCorporate = 1;
            }
        },
        loadApplicationDate: function () {
            var controls = this.getControls();
            var objGetDateActual = {};
            objGetDateActual.dateBill = Session.DATACUSTOMER.objPostDataAccount.BillingCycle;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Postpaid/PlanMigration/GetDateActual',
                data: JSON.stringify(objGetDateActual),
                success: function (response) {
                    that.strApplicationDate = response.data;
                    controls.Post_txtFechaAplicacion.val(response.data);
                }
            });
        },
        getTypification: function () {
            var that = this,
                controls = that.getControls(),
                objTypi = {};

            objTypi.strIdSession = Session.IDSESSION;
            objTypi.strTransactionName = that.strTransactionTypi

            $.app.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: "/CommonServices/GetTypification",
                data: JSON.stringify(objTypi),
                success: function (result) {
                    var list = result.ListTypification;
                    if (result.ListTypification.length > 0) {
                        that.strClaseCode = list[0].CLASE_CODE;
                        that.strSubClaseCode = list[0].SUBCLASE_CODE;
                        that.strTipo = list[0].TIPO;
                        that.strClase = list[0].CLASE;
                        that.strSubClase = list[0].SUBCLASE;

                        that.getBusinessRules();
                    } else {
                        controls.btnGuardar.prop("disabled", true);
                        controls.btnConstancia.prop("disabled", true);
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

        getBusinessRules: function () {
            var that = this, controls = that.getControls();
            controls.divReglasMigracionPlan.empty().html('');
            $.app.ajax({
                type: "POST",
                url: "/CommonServices/GetBusinessRules",
                data: {
                    strIdSession: Session.IDSESSION,
                    strSubClase: that.strSubClaseCode
                },
                success: function (result) {
                    if (result.data.ListBusinessRules != null) {
                        var list = result.data.ListBusinessRules;
                        if (list.length > 0) {
                            controls.divReglasMigracionPlan.html(list[0].REGLA);
                        }
                    }

                }
            });
        },
        validacionTope: function () {
            var controls = this.getControls();
            controls.rdSinTopeConsumo.prop('checked', true);
            controls.ddlTopesConsumo.attr('disabled', true);
            if (Session.hidCodSerActuals == '') {
                controls.chkMantenerTopeConsumo.attr('disabled', true);
                controls.chkMantenerTopeConsumo.prop('checked', false);
            } else {
                controls.chkMantenerTopeConsumo.attr('disabled', false);
            }
        },
        Inicio: function () {
            var controls = this.getControls();
            var that = this;

            if (Session.hidCobroApadece != 0) {
                controls.chkFidelizaApadece.attr('disabled', false);
                controls.txt_Post_MontoFidelizaApadece.attr('disabled', false);
                controls.txt_Post_TotalPenalidadCobrar.val(parseFloat(Session.hidCobroApadece) - parseFloat(controls.txt_Post_MontoFidelizaApadece.val()));

                if (controls.txt_Post_MontoFidelizaApadece.val() != 0) {
                    controls.chk_Post_OCC.prop('checked', true);
                    controls.chk_Post_OCC.val('1');
                }
                else {
                    controls.chk_Post_OCC.prop('checked', false);
                    controls.chk_Post_OCC.val('0');
                }
            }
            else {
                controls.chkFidelizaApadece.attr('disabled', true);
                controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
                controls.txt_Post_TotalPenalidadCobrar.val('0');
            }

            if (controls.chkFidelizaApadece.is(':checked')) {
                controls.txt_Post_MontoFidelizaApadece.attr('disabled', false);

            }
            else {
                controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
                controls.txt_Post_TotalPenalidadCobrar.attr('disabled', true);
                controls.txt_Post_MontoFidelizaApadece.val("0")
                controls.txt_Post_TotalPenalidadCobrar.val(Session.hidCobroApadece);

            }
            if (parseInt(controls.txt_CobroApadece_Post.val() != "" ? controls.txt_CobroApadece_Post.val() : 0) > 0) {
                controls.chk_Post_OCC.prop('checked', true);
            } else {
                controls.chk_Post_OCC.prop('checked', false);
            }

            if ((controls.txt_Post_PenalidadPCS.val() == 0) || (controls.txt_Post_PenalidadPCS.val() == '')) {
                controls.rdbNotaDebito.prop('checked', false);
                controls.chkFidelizaPenalidadPCS.attr('disabled', true);
                controls.chkFidelizaPenalidadPCS.prop('checked', false);
                controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
                controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                controls.ddlArea.attr('disabled', true);
                controls.ddlMotivo.attr('disabled', true);
                controls.ddlSubMotivo.attr('disabled', true);
                controls.ddlArea.val('0');
                controls.ddlMotivo.val('0');
                controls.ddlSubMotivo.val('0');
                Session.hidFormaPago = '';
            }

            $('#idTrNuevoPlan').attr("style", "display:none");
            controls.idTRListaPlanCombo.attr("style", "display:none");
            controls.dvValidacionBoleta.attr("style", "display:none");
        },
        disabled_all: function () {
            var controls = this.getControls();
            controls.chk_Post_PenalidadPCS.attr('disabled', true);
            controls.txtFechaAplicacion.attr('disabled', true);
            controls.txt_CobroApadece_Post.attr('disabled', true);
            controls.chkFidelizaApadece.attr('disabled', true);
            controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
            controls.txt_Post_PenalidadPCS.attr('disabled', true);
            controls.chkFidelizaPenalidadPCS.attr('disabled', true);
            controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
            controls.ddlArea.attr('disabled', true);
            controls.ddlMotivo.attr('disabled', true);
            controls.ddlSubMotivo.attr('disabled', true);
            controls.txtNota.attr('disabled', true);

            controls.btn_PostNuevoPlan.attr("disabled", "disabled");
            controls.rdSinTopeConsumo.attr("disabled", "disabled");
            controls.rdConTopeConsumo.attr("disabled", "disabled");
            controls.ddlTopesConsumo.attr("disabled", "disabled");
            controls.chkSinCostoTope.attr("disabled", "disabled");
            controls.idTRListaPlanCombo.attr("style", "display:none");
            controls.chkServActuales.attr("disabled", "disabled");
            controls.chkServActuales.attr("checked", "false");
        },


        rdSinTopeConsumo_change: function () {
            var that = this;
            //Modificar Roberto :  falta agregar la autorizacion de la ventana de autentificacion
            that.f_checkedTopes('SIN');
        },
        rdConTopeConsumo_change: function () {
            var that = this;
            that.f_checkedTopes('CON');
        },
        rdbPagoefectivo_change: function () {
            var that = this;
            that.f_checked('rdbPagoefectivo');
        },
        rdbNotaDebito_change: function () {
            var that = this;
            that.f_checked('rdbNotaDebito');
        },
        chkSinCostoTope_change: function () {
            var that = this;
            that.OpcionTope();
        },
        chkFidelizaPenalidad_change: function () {
            var that = this;
            that.f_checked('chkFidelizaApadece');
        },
        chkFidelizaPenalidadPCS_change: function () {
            var that = this;
            that.f_checked('chkFidelizaPenalidadPCS');
        },
        ddlTopesConsumo_change: function () {
            var controls = this.getControls();
            var that = this;

            that.EligeCosto();

        },
        chk_Post_PenalidadPCS_change: function () {
            var that = this;
            that.f_checked('chkDivPenalidadPCS')
        },
        chkSentEmail_change: function () {
            var controls = this.getControls();
            if (controls.chkSentEmail.is(':checked')) {
                controls.dTextCorreo.attr("style", "display:block");
                controls.txtPost_SendforEmail.val(Session.email)//Modificar Roberto
            }
            else {
                controls.dTextCorreo.attr("style", "display:none");
                controls.txtPost_SendforEmail.val("")
            }
        },
        chkMantenerTopeConsumo_change: function () {
            var that = this;
            that.f_checkedTopes('MANT');//Modificar
        },

        txt_Post_PenalidadPCS_keypress: function () {
            var controls = this.getControls();
            controls.txt_Post_PenalidadPCS.numeric(',');
        },
        txt_Post_MontoFidelizaApadece_keypress: function () {
            var controls = this.getControls();
            controls.txt_Post_MontoFidelizaApadece.numeric(',');
        },
        txt_Post_TotalFidelizacionPenalidadPCS_keypress: function () {
            var controls = this.getControls();
            controls.txt_Post_TotalFidelizacionPenalidadPCS.numeric(',');
        },
        txt_CobroApadece_Post_Keypress: function () {
            var that = this;
            var controls = this.getControls();
            controls.txt_CobroApadece_Post.numeric(',');
        },

        txt_CobroApadece_Post_blur: function () {

            var that = this;
            that.reemplazar('txt_CobroApadece_Post');
        },
        txt_Post_MontoFidelizaApadece_blur: function () {
            var that = this;
            that.f_checked('txtMontoFidelizaApadece');
            that.reemplazar('txt_Post_MontoFidelizaApadece');
        },
        txt_Post_PenalidadPCS_blur: function () {
            var that = this;
            that.f_checked('txtCobroPenalidadPCS');
            that.reemplazar('txt_Post_PenalidadPCS');
        },
        txt_Post_TotalFidelizacionPenalidadPCS_blur: function () {
            var that = this;
            that.f_checked('txtMontoPenalidadPCS');
            that.reemplazar('txt_Post_TotalFidelizacionPenalidadPCS');
        },


        btnAgregarServicio_Post_click: function () {
            var that = this;
            var pMontoConIgv = 0;
            var pIgv = that.strGlobalVariables.IGVPercent;
            var controls = this.getControls();
            var dblTotalCF = 0;
            
            var json_obj = {}; var count = 0;
            var datos = String($('#tblServiciosAsociadosPlan_Post tr').find('input[type=radio]:checked').val()).split('_');

            if (datos != undefined) {
                var count = $('#tblServiciosAsociadoAgregadosPlan_Post').find('tbody tr').length;
                var countEmpty = 0;
                if (count > 0) {
                    if (that.f_validaMotivoExcluyentes(datos[2])) {
                        alert('Tiene un servicio ya agregado con el mismo motivo de servicio excluyente, debe borrar primero antes de asignar este nuevo servicio excluyente.',"Alerta");
                        return false;
                    }
                }
                controls.IdTrArmaPlanesCombos.attr("style", "display:block");
                var trHTML = '';
                trHTML += '<tr><td class="col-md-2" align="center">' + '<a class="glyphicon glyphicon-remove" id="ServAgregados_' + datos[0] + '"></a>' +
                                 '</td><td class="col-md-2"  style="display:none;">' + datos[0] +
                                 '</td><td class="col-md-2">' + datos[1] +
                                 '</td><td class="col-md-2"  style="display:none;">' + datos[2] +
                                 '</td><td class="col-md-2">' + datos[3] +
                                 '</td><td class="col-md-2"  style="display:none;">' + (datos[4] == 'null' ? '' : datos[4]) +
                                 '</td><td class="col-md-2">' + (datos[5] == "null" ? '' : datos[5]) +
                                 '</td><td class="col-md-2">' + datos[6] +
                                 '</td></tr>';
                $('#tblServiciosAsociadoAgregadosPlan_Post tbody').append(trHTML);
                $("#ServAgregados_" + datos[0]).click(function () {
                    that.f_Eliminarcelda("ServAgregados_" + datos[0], datos[0]);
                });
                $('#tblServiciosAsociadosPlan_Post tr').find('input[type=radio]:checked').attr("disabled", true).prop("checked", false);

                controls.lblMensajeCantidadServiciosAgr.text('Cantidad de Servicios Agregados :  ' + (count + 1));

                $('#tblServiciosAsociadoAgregadosPlan_Post tr').each(function () {
                    if ($(this).find("td").eq(6).html() != '') {
                        if (parseFloat($(this).find("td").eq(6).html()) > 0) {
                            dblTotalCF = dblTotalCF + parseFloat($(this).find("td").eq(6).html());
                        }
                    }
                });

                dblTotalCF = dblTotalCF + Session.dblCargoFijoBase;
                controls.lblPost_CargoTotalDelPlanSin.text(dblTotalCF.toFixed(2));
                pMontoConIgv = (parseFloat(dblTotalCF) * parseFloat(pIgv)).toFixed(2);
                controls.lblPost_CargoTotalDelPlanCon.text(pMontoConIgv);
                
            } else { alert("No ha seleccionado ningún registro","Alerta"); }

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
        btn_PostNuevoPlan_click: function () {
            var controls = this.getControls();
            var that = this;
            var strUrlTemplate = window.location.href + '/Home/DialogTemplate';
            $.window.open({
                id: 1111,
                modal: true,
                template: strUrlTemplate,
                title: "SELECCIONE EL NUEVO PLAN",
                url: '/Transactions/Postpaid/PlanMigration/PostpaidPlansMigrations',
                width: 1024,
                height: 600,
                buttons: {
                    Seleccionar: {
                        click: function (sender, args) {
                            //var fila = $(":input[name=optionsRadios]:checked").val();
                            var ModalConfirm = this;
                            var rowPost = $('#tblServiciosNuevoMigracionPlan_Post').DataTable().rows({ selected: true }).data();
                            var item = rowPost[0];

                            if (item == "" || item == undefined || item == null) {
                                alert('Debe seleccionar un Plan.', this.strTitleMessage);
                                return;
                            }
                            //var res = fila.split("|");
                            Session.NuevoPlanMigradoPost = JSON.parse(JSON.stringify({ COD_PROD: item.COD_PROD, TMCODE: item.TMCODE, DESC_PLAN: item.DESC_PLAN, VERSION: item.VERSION, CAT_PROD: item.CAT_PROD, COD_CARTA_INFO: item.COD_CARTA_INFO, FECHA_INI_VIG: item.FECHA_INI_VIG, FECHA_FIN_VIG: item.FECHA_FIN_VIG, ID_TIPO_PROD: item.ID_TIPO_PROD, USUARIO: item.USUARIO }));
                            //console.logSession.NuevoPlanMigradoPost)
                            //Session.NuevoPlanMigradoPost = JSON.parse(JSON.stringify({ COD_PROD: res[0], TMCODE: res[1], DESC_PLAN: res[2], VERSION: res[3], CAT_PROD: res[4], COD_CARTA_INFO: res[5], FECHA_INI_VIG: res[6], FECHA_FIN_VIG: res[7], ID_TIPO_PROD: res[8], USUARIO: res[9] }));
                            //console.logSession.NuevoPlanMigradoPost)
                            confirm("¿Está seguro de seleccionar el plan " + item.DESC_PLAN + "?", 'Confirmar', function (result) {
                                if (result == true) {
                                    controls.tblServiciosAsociadosPlan_Post.DataTable().clear().draw();
                                    $('#tblServiciosAsociadoAgregadosPlan_Post tbody').html("");
                                    that.SelectionPlan(item);
                                    ModalConfirm.close();
                                }
                            });

                        }
                    },
                    Cancelar: {
                        click: function (sender, args) {
                            this.close();
                        }
                    }
                }
            });
        },
        btnGuardar_click: function () {
            
            var that = this,
            controls = that.getControls();

            if (controls.chkSinCostoTope.is(':checked')) {
                Session.hidCheckSinCostoFinal = '1';
            } else {
                Session.hidCheckSinCostoFinal = '0';
            }

            if (controls.chkSentEmail.is(':checked')) {
                if (that.ValidarCorreo() == false)
                { return false; }
            }
            that.loadValidatePage();
            if (Session.hidTipoPlan == '1') {
                Session.hidValorTotalNuevoPlan = Session.dblCargoFijoBase;
            } else if (Session.hidTipoPlan == '3') {
                Session.hidValorTotalNuevoPlan2 = controls.lblPost_CargoTotalDelPlanSin.text();
            } else {
                Session.hidValorTotalNuevoPlan2 = Session.dblTotalCF;
            }

            var intResult = 0;
            Session.hidCodSerArmaPost = "";
            Session.hidObtenerDatosGrilla = "";
            if (controls.chk_Post_OCC.is(':checked')) {
                Session.hidOCC = "1";
                Session.hidFormaPagoApadece = "OCC";
            } else {
                Session.hidOCC = "0";
                Session.hidFormaPagoApadece = "";
            }
            Session.hidTotPenalPCS = controls.txt_Post_TotalPenalidadPCSCobrar.val();
            Session.hidCodMotivo = controls.ddlMotivo.val();
            Session.hidCodSubMotiv = controls.ddlSubMotivo.val();
            Session.hidDesMotivo = controls.ddlMotivo.text();
            Session.hidDesSubMotivo = controls.ddlSubMotivo.text();
            Session.hidTotalApadece = controls.txt_Post_TotalPenalidadCobrar.val();
            Session.hidTotalPCS = controls.txt_Post_TotalPenalidadPCSCobrar.val();

            if (controls.chckApadeceNoAplica.is(':checked')) {
                Session.hidchckApadeceNoAplica = "1";
            } else {
                Session.hidchckApadeceNoAplica = "0";
            }

            if ($.trim(controls.txtPlanNuevoAgregado.text()) == '') {
                alert('Por favor usted debe asignar un Plan para la migración.',"Alerta");
                that.unLoadPage();
                return false;
            }


            if ((!controls.rdConTopeConsumo.is(':checked')) && (!controls.rdSinTopeConsumo.is(':checked'))) {

                alert('Por favor seleccione un Tope de Consumo.',"Alerta");
                that.unLoadPage();
                return false;
            }

            if (controls.rdConTopeConsumo.is(':checked')) {
                if (controls.ddlTopesConsumo.val() == "") {
                    alert('Por favor seleccione un Tope de Consumo.',"Alerta");
                    that.unLoadPage();
                    return false;
                }
            }

            if (Session.hidTipoPlan == '3') {
                if ($('#ContenedorServiciosAsociadoAgregadosPlan_Post tr').length <= 1) {
                    alert('Debe seleccionar y agregar un Servicio para poder programar la migración.',"Alerta");
                    that.unLoadPage();
                    return false;
                }
            }


            if (that.f_validaFecha() == true) { } else {
                that.unLoadPage();
                return false;
            };


            if ((controls.chkFidelizaApadece.is(':checked')) && (controls.txt_Post_MontoFidelizaApadece.val() == '0.00')) {
                that.unLoadPage();
                alert('Por favor, ingrese un monto diferente a 0 para la Fidelización APADECE',"Alerta");
                controls.txt_Post_MontoFidelizaApadece.focus();
                return false;
            }
            if ((controls.chkFidelizaPenalidadPCS.is(':checked')) && (controls.txt_Post_TotalFidelizacionPenalidadPCS.val() == '0.00')) {
                that.unLoadPage();
                alert('Por favor, ingrese un monto diferente a 0 para la Fidelización PCS',"Alerta");
                controls.txt_Post_TotalFidelizacionPenalidadPCS.focus();
                return false;
            }

            if ((controls.chk_Post_PenalidadPCS.is(':checked')) && (parseFloat(controls.txt_Post_PenalidadPCS.val()) == 0.00)) {
                that.unLoadPage();
                alert('Por favor, ingrese un monto diferente a 0 para el Monto Penalidad PCS o desmarque el Check de Penalidad PCS si en caso no aplica.',"Alerta");
                controls.txt_Post_PenalidadPCS.val().focus();
                return false;
            }
            if (controls.chk_Post_PenalidadPCS.is(':checked')) {
                if (parseFloat(controls.txt_Post_PenalidadPCS.val()) > 0) {
                    if (controls.ddlArea.val() == "") {
                        that.unLoadPage();
                        alert('Por favor seleccione un tipo de Área.',"Alerta");
                        controls.ddlArea.focus();
                        return false;
                    }
                    if (controls.ddlMotivo.val() == "") {
                        that.unLoadPage();
                        alert('Por favor seleccione un tipo de Motivo.',"Alerta");
                        controls.ddlMotivo.focus();
                        return false;
                    }
                    if (controls.ddlSubMotivo.val() == "") {
                        that.unLoadPage();
                        alert('Por favor seleccione un tipo de Sub Motivo.',"Alerta");
                        controls.ddlSubMotivo.focus();
                        return false;
                    }
                }
            }
            if (controls.txtNota.val().length > 4000) {
                that.unLoadPage();
                alert('La longitud de las Notas no pueden exceder los 4000 caracteres.',"Alerta");
                return false;
            }

            if (controls.cbo_PostMigrationCacDac.val() == "") {
                that.unLoadPage();
                alert('Por favor seleccione un CAC/DAC.');
                controls.cbo_PostMigrationCacDac.focus();
                return false;
            }

            confirm("¿Estás seguro de programar la Migración de Plan?", 'Confirmar', function (result) {
                if (result == true) {
                    var telef = Session.DATACUSTOMER.Telephone;
                    var contrat = Session.DATACUSTOMER.ContractID;
                    if (Session.hidTipoPlan == '3') {
                        var strCodSer = '';
                        var strTotal = '';

                        if ($('#ContenedorServiciosAsociadoAgregadosPlan_Post tbody tr').length > 0) {
                            $('#ContenedorServiciosAsociadoAgregadosPlan_Post tbody tr').each(function () {
                                var strCodSerPlan = '';
                                var strNombreServ = '';
                                var strMotivoExcl = '';
                                var strCargoFijo = '';
                                var strPeriodo = '';

                                if ($(this).find("td").eq(1).html() != null) {
                                    if ($.trim($(this).find("td").eq(1).html()) != '') {
                                        strCodSer = strCodSer + $(this).find("td").eq(1).html() + '|';
                                    }
                                }
                                if ($(this).find("td").eq(1).html() != null) {
                                    strCodSerPlan = $(this).find("td").eq(1).html() + ';';
                                    strNombreServ = $(this).find("td").eq(2).html() + ';';
                                    strMotivoExcl = $(this).find("td").eq(4).html() + ';';
                                    strCargoFijo = $(this).find("td").eq(6).html() + ';';
                                    strPeriodo = $(this).find("td").eq(7).html();

                                    strTotal = strTotal + strCodSerPlan + strNombreServ + strMotivoExcl + strCargoFijo + strPeriodo + '|';
                                }

                            });
                            Session.hidObtenerDatosGrilla = strTotal;
                        }
                        if ($.trim(strCodSer) != '') {
                            Session.hidCodSerArmaPost = strCodSer;
                        }

                    } else {
                        Session.hidCodSerArmaPost = "";
                    }
                    ///Inicio guardar
                    var objValidateType = {};
                    objValidateType.strIdSession = Session.IDSESSION;
                    objValidateType.contract = Session.DATACUSTOMER.ContractID;
                    objValidateType.phoneNumber = Session.DATACUSTOMER.Telephone;
                    objValidateType.PRY = 'CTC';
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: JSON.stringify(objValidateType),
                        url: '/Transactions/Postpaid/PlanMigration/GetValidationMigration',
                        success: function (response) {
                            ////////
                            var respuesta = response.data.RespuestaValidacion.split(';');
                            if (respuesta != "") {
                                if (respuesta[0] == "0") {
                                    if ((controls.chkFidelizaApadece.is(':checked')) || (controls.chkFidelizaPenalidadPCS.is(':checked'))) {
                                        if (Session.Fideliza == false) {
                                            ValidaPermisoCheck();
                                            if (Session.Fideliza == true) {
                                                that.SaveTransaction();
                                            }
                                            else {
                                                that.unLoadPage();
                                                return;
                                            }
                                        }
                                        else {
                                            that.SaveTransaction();
                                        }
                                    }
                                    else {
                                        that.SaveTransaction();
                                    }
                                }
                                else {
                                    that.unLoadPage();
                                    alert(result.split(';')[1],"Informativo");
                                    return false;
                                }
                            } else {
                            }
                        }

                    });
                        
                }
                else {

                    that.unLoadPage();
                    return false;
                }
            });
            //Validando Deuda,Bloqueo,Suspensión/Bloqueos, Programaciones Pendientes
            
        },
        
        SaveTransaction: function ()
        {

           
            var that = this,controls=that.getControls(), objSaveType = {};
            //implentar logica guardar
            var PlanMigrationTransac = {};
            PlanMigrationTransac.hidFormaPago = Session.hidFormaPago;
            PlanMigrationTransac.hidConsumerControl = Session.hidConsumerControl;
            PlanMigrationTransac.hidDescripProducto = Session.hidDescripProducto;
            PlanMigrationTransac.hidValorTotalNuevoPlan = Session.hidValorTotalNuevoPlan;
            PlanMigrationTransac.hidValorTotalNuevoPlan2 = Session.hidValorTotalNuevoPlan2;
            PlanMigrationTransac.hidTotalApadece = Session.hidTotalApadece == "" ? 0 : Session.hidTotalApadece;
            PlanMigrationTransac.hidFormaPagoApadece = Session.hidFormaPagoApadece;
            PlanMigrationTransac.hidTotalPCS = Session.hidTotalPCS;
            PlanMigrationTransac.hidTelefono = Session.DATACUSTOMER.Telephone;
            PlanMigrationTransac.hidTipoPlan = Session.hidTipoPlan;
            PlanMigrationTransac.hidCodSerActuals = Session.hidCodSerActuals;
            PlanMigrationTransac.hidFlgAutomatico = Session.hidFlgAutomatico;
            PlanMigrationTransac.hidFlgCincoSoles = Session.hidFlgCincoSoles;
            PlanMigrationTransac.hiFlgAdicionalTope = Session.hiFlgAdicionalTope;
            PlanMigrationTransac.hidCheckSinCostoFinal = Session.hidCheckSinCostoFinal;
            PlanMigrationTransac.hidOpTopeConsDesc = Session.hidOpTopeConsDesc;
            PlanMigrationTransac.hidCartaInfor = Session.hidCartaInfor;
            PlanMigrationTransac.hidObtenerDatosGrilla = Session.hidObtenerDatosGrilla;
            PlanMigrationTransac.hidCodSerArmaPost = Session.hidCodSerArmaPost;
            PlanMigrationTransac.hidCodProd = Session.hidCodProd;
            PlanMigrationTransac.hidCobroApadece = Session.hidCobroApadece;
            PlanMigrationTransac.hidchckApadeceNoAplica = Session.hidchckApadeceNoAplica;
            PlanMigrationTransac.hidOpTopeConsCod = Session.hidOpTopeConsCod;
            PlanMigrationTransac.hidTotPenalPCS = Session.hidTotPenalPCS;
            PlanMigrationTransac.hidCodMotivo = Session.hidCodMotivo;
            PlanMigrationTransac.hidCodSubMotiv = Session.hidCodSubMotiv;
            PlanMigrationTransac.hidCargoFijoTotalPlan = Session.hidCargoFijoTotalPlan;
            PlanMigrationTransac.hidAuditFechProg = that.strConfigVariables.strTransAuditFchProg;
            PlanMigrationTransac.hidAuditChckFideliza = that.strConfigVariables.strTransAuditChkFidelize;
            PlanMigrationTransac.hidAuditChckSinCosto = that.strConfigVariables.strTransAuditNotCost;

            PlanMigrationTransac.Post_txtFechaAplicacion = controls.Post_txtFechaAplicacion.val();
            PlanMigrationTransac.txt_CobroApadece_Post = controls.txt_CobroApadece_Post.val() == "" ? 0 : controls.txt_CobroApadece_Post.val();
            PlanMigrationTransac.chkFidelizaPenalidad = controls.chkFidelizaPenalidad.is(':checked') ? 1 : "";
            PlanMigrationTransac.txt_Post_PenalidadPCS = controls.txt_Post_PenalidadPCS.val() == "" ? 0 : controls.txt_Post_PenalidadPCS.val();
            PlanMigrationTransac.chkFidelizaPenalidadPCS = controls.chkFidelizaPenalidadPCS.is(':checked') ? 1 : 0;
            PlanMigrationTransac.cbo_PostMigrationCacDac = controls.cbo_PostMigrationCacDac[0][controls.cbo_PostMigrationCacDac.val()].text;//SIACPO_DAC1 Modificar roberto
            PlanMigrationTransac.cbo_PostMigrationCacDacVal = controls.cbo_PostMigrationCacDac.val();
            PlanMigrationTransac.txt_Post_TotalPenalidadCobrar = controls.txt_Post_TotalPenalidadCobrar.val();//txtMontoFidelizaApadece
            PlanMigrationTransac.txt_Post_TotalFidelizacionPenalidadPCS = controls.txt_Post_TotalFidelizacionPenalidadPCS.val();
            PlanMigrationTransac.txt_Post_NumeroBoleta = controls.txt_Post_NumeroBoleta.val();
            PlanMigrationTransac.lblPost_TopeConsumoActual = controls.lblPost_TopeConsumoActual.text();
            PlanMigrationTransac.rdSinTopeConsumo = controls.rdSinTopeConsumo.is(':checked') ? 1 : 0;
            PlanMigrationTransac.rdConTopeConsumo = controls.rdConTopeConsumo.is(':checked') ? 1 : 0;
            PlanMigrationTransac.chk_Post_OCC = controls.chk_Post_OCC.is(':checked') ? 1 : 0;
            PlanMigrationTransac.rdConTopeConsumoText = controls.rdConTopeConsumo.text();
            PlanMigrationTransac.ddlTopesConsumoText = controls.ddlTopesConsumo[0][controls.ddlTopesConsumo.val()].text;
            PlanMigrationTransac.ddlTopesConsumoVal = controls.ddlTopesConsumo.val(); 
            PlanMigrationTransac.chkMantenerTopeConsumo = controls.chkMantenerTopeConsumo.is(':checked') ? 1 : 0;
            PlanMigrationTransac.ddlAreaVal = controls.ddlArea.val();
            PlanMigrationTransac.lblPost_Documento = controls.lblPost_Documento.text();
            PlanMigrationTransac.chk_Post_PenalidadPCS = controls.chk_Post_PenalidadPCS.is(':checked') ? 1 : 0;
            PlanMigrationTransac.chkSentEmail = controls.chkSentEmail.is(':checked') ? 1 : 0;
            PlanMigrationTransac.txtNota = controls.txtNota.val();
            PlanMigrationTransac.User = Session.CODUSER;
            PlanMigrationTransac.strClaseCode = that.strClaseCode;
            PlanMigrationTransac.strSubClaseCode = that.strSubClaseCode;
            PlanMigrationTransac.strTipo = that.strTipo;
            PlanMigrationTransac.strClase = that.strClase;
            PlanMigrationTransac.strSubClase = that.strSubClase;


            objSaveType.strIdSession = Session.IDSESSION;
            objSaveType.objDatacustomer = Session.DATACUSTOMER;
            objSaveType.objHiddenControl = PlanMigrationTransac;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objSaveType),
                url: '/Transactions/Postpaid/PlanMigration/SavePlanMigration',
                success: function (response) {
                    if (response.data) {

                    }
                }
            });
        },

        txt_CobroApadece_Post_onkeyup: function () {
            var that = this;
            that.f_checked('txtCobroApadece');
        },

        ComparaFecha: function (fechainicio, fechafin, flag) {
            var comp1 = fechainicio.substr(6, 4) + '' + fechainicio.substr(3, 2) + '' + fechainicio.substr(0, 2);
            var comp2 = fechafin.substr(6, 4) + '' + fechafin.substr(3, 2) + '' + fechafin.substr(0, 2);
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
        },
        f_validaFecha: function () {
            var that = this,
                controls = that.getControls();
            var fechaAplicacion = controls.Post_txtFechaAplicacion.val();
            var fechaActual = Session.hidFechaActual;

            if (fechaAplicacion == '') {
                alert('Fecha no válida. Debe ingresar el formato (DD/MM/AAAA)',"Alerta");
                // modificar ValidaPermiso();
                return false;
            }
            else {
                if (that.validarFecha('Post_txtFechaAplicacion') == false) {
                    // modificar ValidaPermiso();
                    return false;
                }

                if (!that.ComparaFecha(fechaActual, fechaAplicacion, '1')) {
                    alert('La fecha de Aplicación es menor o igual a la fecha actual',"Alerta");
                    // modificar ValidaPermiso();
                    return false;
                }

                var varCicloFacturacionCliente = Session.DATASERVICECicloFacturacion;

                var vFechaActual = Session.hidFechaActual;
                var vDiaActual = vFechaActual.substring(0, 2);
                var vMesActual = vFechaActual.substring(3, 5);
                var vAnioActual = vFechaActual.substring(6, 10);
                var vFechaActualFinal = vAnioActual + vMesActual + vDiaActual;


                var vFechFactAsig = Session.FECHAAPLICACIONMIGRACIONPLAN.split("/")[2] + Session.FECHAAPLICACIONMIGRACIONPLAN.split("/")[1] + Session.FECHAAPLICACIONMIGRACIONPLAN.split("/")[0];


                var vFechaAplicacion = controls.Post_txtFechaAplicacion.val();
                var vDiaAplicacion = vFechaAplicacion.substring(0, 2);
                var vMesAplicacion = vFechaAplicacion.substring(3, 5);
                var vAnioAplicacion = vFechaAplicacion.substring(6, 10);
                var vFechaFinalControl = vAnioAplicacion + vMesAplicacion + vDiaAplicacion;



                var vFechaLimite = '';
                var vDiaLimite = '';
                var vMesLimite = '';
                var vAnioLimite = '';

                if (parseInt(varCicloFacturacionCliente) > parseInt(vDiaActual)) {
                    vFechaLimite = varCicloFacturacionCliente + '/' + vMesActual + '/' + vAnioActual;
                    vDiaLimite = varCicloFacturacionCliente;
                    vMesLimite = vMesActual;
                    vAnioLimite = vAnioActual;
                    vFechaLimite = vAnioLimite + vMesLimite + varCicloFacturacionCliente;
                } else {
                    vDiaLimite = varCicloFacturacionCliente;
                    if (parseInt(vMesActual) == 12) {
                        vMesLimite = '01';
                        vAnioLimite = (parseInt(vAnioActual) + 1);
                    } else {
                        vMesLimite = (parseInt(vMesActual) + 1);
                        if (parseInt(vMesLimite) < 10) {
                            vMesLimite = '0' + vMesLimite;
                        }
                        vAnioLimite = parseInt(vAnioActual);
                    }
                    vFechaLimite = varCicloFacturacionCliente + '/' + vMesLimite + '/' + vAnioLimite;
                    vFechaLimite = vAnioLimite + vMesLimite + varCicloFacturacionCliente;
                }

                if (parseInt(vFechaFinalControl) > parseInt(vFechFactAsig)) {
                    alert('La fecha Seleccionada es mayor a la fecha Límite. Por favor ingresar una fecha como máximo un día menor a la fecha de Facturación.',"Alerta");
                    ValidaPermiso();
                    return false;
                }
                if (parseInt(vFechFactAsig) > parseInt(vFechaLimite)) {
                    if (parseInt(vFechaFinalControl) <= parseInt(vFechaLimite)) {
                        alert('Ha excedido el plazo máximo de programación, la fecha seleccionada debe ser como máximo el último día del ciclo de facturación próximo.',"Alerta");
                        ValidaPermiso();
                        return false;
                    }
                }
                return true;
            }
        },
        mask: function (InString, Mask) {
            var that = this;
            var LenStr = InString.length;
            var LenMsk = Mask.length;
            if ((LenStr == 0) || (LenMsk == 0))
                return 0;
            if (LenStr != LenMsk)
                return 0;
            var TempString = ""
            var Count = 0;
            for (Count = 0; Count <= InString.length; Count++) {
                var StrChar = InString.substring(Count, Count + 1);
                var MskChar = Mask.substring(Count, Count + 1);
                if (MskChar == '#') {
                    if (!that.isNumberChar(StrChar))
                        return 0;
                }
                else if (MskChar == '*') {
                }
                else {
                    if (MskChar != StrChar)
                        return 0;
                }
            }
            return 1;
        },
        validateDateMask: function (strDate) {
            var that = this;
            if (that.mask(strDate, '##/##/####') != 1)
                return false;
            else
                return true;
        },
        validarFecha: function (oControl) {
            
            var that = this;
            var Day, Month, Year;
            var Fecha = $("#" + oControl);
            //var Fecha = xname.value;
            var valor = Fecha.val();
            var controlValida;
            controlValida = Fecha.id;

            if (that.validateDateMask(valor) == false) {
                alert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)',"Alerta");
                return false;
            }

            Day = that.getvalue(valor, 1, "/");
            Month = that.getvalue(valor, 2, "/");
            Year = that.getvalue(valor, 3, "/");

            if (($.isNumeric(Day) && $.isNumeric(Month) && $.isNumeric(Year) && (Year.length == 4) && (Day.length <= 2) && (Month.length <= 2)) || ((Month == 2) && (Day <= 29))) {
                if ((Day != 0) && (Month != 0) && (Year != 0) && (Month <= 12) && (Day <= 31) && (Month != 2)) {

                    if (Month == 4 || Month == 6 || Month == 9 || Month == 11) {
                        if (Day > 30) {
                            alert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)',"Alerta");
                            return false;
                        }
                    } else if (Month == 1 || Month == 3 || Month == 5 || Month == 7 || Month == 8 || Month == 10 || Month == 12) {
                        if (Day > 31) {
                            alert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)',"Alerta");
                            return false;
                        }
                    }
                    return true;
                }
                else if ((Month == 2) && (Day <= 29) && ((Year % 4) == 0) && ((Year % 100) != 0))
                    return true;
                else if ((Month == 2) && (Day <= 29) && ((Year % 400) == 0))
                    return true;
                else if ((Month == 2) && (Day <= 28))
                    return true;
                else {
                    if (Month > 12) {
                        alert('El campo de mes debe ser como maximo 12.',"Alerta");
                    }
                    else if (Year.length != 4) {
                        alert("El a�o debe tener 4 cifras.","Alerta");
                    }
                    else if ((Month == 2) && (Day == 29) && ((Year % 4) == 0) && (Year % 100) == 0) {
                        alert('A�o no bisiesto.',"Alerta");
                    }
                    else {
                        alert('Fecha no valida',"Alerta");
                    }
                    if (Fecha.disabled == false)
                        Fecha.focus();

                    Fecha.select();
                    return false;
                }
            }
            else {
                alert('Fecha no valida. Debe ingresar el formato (DD/MM/AAAA)',"Alerta");
                return false;
            }
        },
        isNumberChar: function (InString) {
            if (InString.length != 1)
                return (false);
            var RefString = "1234567890";
            if (RefString.indexOf(InString, 0) == -1)
                return (false);
            return (true);
        },
        getvalue: function (strData, intFieldNumber, separator) {
            var intCurrentField, intFoundPos, strValue, strNames;
            var bool = false;
            strNames = strData;
            intCurrentField = 0;
            while ((intCurrentField != intFieldNumber) && !bool) {
                intFoundPos = strNames.indexOf(separator);
                intCurrentField = intCurrentField + 1;
                if (intFoundPos != 0) {
                    strValue = strNames.substring(0, intFoundPos);
                    strNames = strNames.substring(intFoundPos + 1, strNames.length);
                }
                else {
                    if (intCurrentField == intFieldNumber)
                        strValue = strNames;
                    else
                        strValue = "";
                    bool = true;
                }
            }
            if (strValue != "")
                return strValue;
            else
                return strNames;
        },
        ValidarCorreo: function () {
            var controls = this.getControls();
            if (controls.txtPost_SendforEmail.val() == '') {
                alert('Debe ingresar el correo.',"Alerta");
                return false;
            }
            else {
                regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test(controls.txtPost_SendforEmail.val());
                if (blvalidar == false) {
                    alert('Correo Incorrecto',"Alerta");
                    controls.txtPost_SendforEmail.focus();
                    return false;
                } else { return true; }
            }
        },

        getKey: function (key) {
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/CommonServices/GetConfig',
                async: false,
                data: { strIdSession: "12312312", Key: key },//Modificar Roberto
                success: function (response) {
                    if (key == "strCodTipoCli") {
                        Session.CODTIPOCLIENTEMIGRACIONPLAN == response.data;
                    }
                }
            });
        },
        ValidarApadece2: function (p_dblValorTotNuevoPlan) {//Moficar Roberto Todo este metodo
            var ValorTotalPlan = document.getElementById('hidCargoFijoTotalPlan').value;
            var valorDiferencia = 0;
            var valorDiferenciaCF = that.strConfigVariables.strDiferenceMountCF;

            var pIgvCF = that.strGlobalVariables.IGVPercent;
            var pMontoConIgvCF = 0;

            if (isNaN(p_dblValorTotNuevoPlan)) {
                document.getElementById('chckApadeceNoAplica').checked = false;
            } else {
                if (p_dblValorTotNuevoPlan != '') {
                    if (parseFloat(p_dblValorTotNuevoPlan) >= parseFloat(ValorTotalPlan)) {
                        document.getElementById('chckApadeceNoAplica').checked = true;
                        $('#chkFidelizaApadece').attr("disabled", "disabled");
                        $('#txtCobroApadece').attr("disabled", "disabled");
                        controls.txt_Post_TotalPenalidadCobrar.val('0');
                        f_ValidaEspecificField();
                    } else {
                        pMontoConIgvCF = (parseFloat(p_dblValorTotNuevoPlan) * parseFloat(pIgvCF)).toFixed(2);
                        valorDiferencia = (parseFloat(ValorTotalPlan) - parseFloat(pMontoConIgvCF)).toFixed(2);

                        if (parseFloat(valorDiferencia) <= parseFloat(valorDiferenciaCF)) {
                            document.getElementById('chckApadeceNoAplica').checked = true;
                            $('#chkFidelizaApadece').attr("disabled", "disabled");
                            $('#txtCobroApadece').attr("disabled", "disabled");
                            controls.txt_Post_TotalPenalidadCobrar.val('0');
                            f_ValidaEspecificField();
                        } else {
                            document.getElementById('chckApadeceNoAplica').checked = false;
                            f_ValidaEspecificField();
                        }
                    }
                } else {
                    document.getElementById('chckApadeceNoAplica').checked = false;
                    f_ValidaEspecificField();
                }
            }
        },
        f_validaMotivoExcluyentes: function (strcoExcl) {
            $('#tblServiciosAsociadoAgregadosPlan_Post tr').each(function () {
                if ($(this).find("td").eq(3).html() == strcoExcl) {
                    intResp = 1;
                }
            });
            if (intResp == 0) {
                return false
            } else { return true; }
        },
        f_Eliminarcelda: function (id, codigo) {
            var that = this,
                controls = that.getControls();
            var pIgv = that.strGlobalVariables.IGVPercent;
            var pMontoConIgv = 0;
            var dblTotalCF = 0;
            $('#' + id).parent().parent().remove();
            var codRadio = "optionsRadiosAsociados_" + codigo;
            $('#tblServiciosAsociadosPlan_Post tr').find('td:eq(0)').each(function () {
                var $this = $(this);
                if ($this[0].firstElementChild.id == codRadio) {
                    $this.parent().find('input[type = radio]').attr("disabled", false);
                }
            });
            controls.lblMensajeCantidadServiciosAgr.text('Cantidad de Servicios Agregados :  ' + ($('#tblServiciosAsociadoAgregadosPlan_Post').find('tbody tr').length));

            $('#tblServiciosAsociadoAgregadosPlan_Post tr').each(function () {
                if ($(this).find("td").eq(6).html() != '') {
                    if (parseFloat($(this).find("td").eq(6).html()) > 0) {
                        dblTotalCF = dblTotalCF + parseFloat($(this).find("td").eq(6).html());
                    }
                }
            });

            dblTotalCF = dblTotalCF + Session.dblCargoFijoBase;
            controls.lblPost_CargoTotalDelPlanSin.text(dblTotalCF.toFixed(2));
            pMontoConIgv = (parseFloat(dblTotalCF) * parseFloat(pIgv)).toFixed(2);
            controls.lblPost_CargoTotalDelPlanCon.text(pMontoConIgv);
        },
        f_BorrarServicio: function (p_CodigoSer) {
            $('#tblServiciosAsociadoAgregadosPlan_Post tr').each(function () {
                if ($(this).find("td").eq(1).html() == p_CodigoSer) {
                    $(this).remove();
                }
            });
        },
        
        EligeCosto: function () {
            var that = this,
                controls = that.getControls();
            var $this = $(this);
            if (Session.hidFlgAutomatico == '1' && Session.hidFlgCincoSoles == '1') {
                if (controls.ddlTopesConsumo.val() == Session.hidCodSerActuals) {
                    if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCons5Soles) {
                        if (Session.hiFlgAdicionalTope == '0') {
                            alert('Debe elegir un tope diferente a ' + controls.lblPost_TopeConsumoActual.text() + ', \n ya que es un tope consumo actual.',"Alerta");
                            controls.ddlTopesConsumo.val();
                            controls.chkSinCostoTope.prop('checked', false);
                            controls.chkSinCostoTope.attr('disabled', true);
                        } else {
                            controls.chkSinCostoTope.prop('checked', true);
                            that.OpcionTope();
                            controls.chkSinCostoTope.prop('checked', true);
                            controls.chkSinCostoTope.attr('disabled', true);
                        }
                    } else if (controls.ddlTopesConsumo.val() == Session.hidCodSerActuals) {
                        alert('Debe elegir un tope diferente a ' + controls.lblPost_TopeConsumoActual.text() + ', \n ya que es un tope consumo actual.',"Alerta");
                        controls.ddlTopesConsumo.val();
                        controls.chkSinCostoTope.prop('checked', false);
                        controls.chkSinCostoTope.attr('disabled', true);
                    }
                    return;
                }
                if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCons5Soles) {
                    if (Session.hiFlgAdicionalTope == '0') {
                        controls.chkSinCostoTope.prop('checked', false);
                        controls.chkSinCostoTope.attr('disabled', true);
                    } else {
                        if ((Session.hidCodSerActuals == that.strConfigVariables.strCodServAdditional) && (Session.hiFlgAdicionalTope == '1')) {
                            controls.chkSinCostoTope.attr('disabled', true);
                        } else {
                            controls.chkSinCostoTope.attr('disabled', false);
                        }
                    }
                } else {
                    controls.chkSinCostoTope.prop('checked', false);
                    controls.chkSinCostoTope.attr('disabled', true);
                }
            } else {

                controls.chkSinCostoTope.prop('checked', false);
                if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCodAutomatico && Session.hidOpTopeCodAutomatico == '0') {

                    alert('Este servicio NO está configurado al plan seleccionado.',"Alerta");
                    controls.ddlTopesConsumo.val();
                    controls.chkSinCostoTope.prop('checked', false);
                    controls.chkSinCostoTope.attr('disabled', true);
                    return;
                }

                if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCons5Soles && Session.hidOpTopeCons5Soles == '0') {
                    controls.chkSinCostoTope.prop('checked', true);
                    controls.chkSinCostoTope.attr('disabled', true);
                }
                if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCons5Soles && Session.hidOpTopeCons5Soles == '1') {
                    controls.chkSinCostoTope.prop('checked', false);
                    controls.chkSinCostoTope.attr('disabled', false);
                }

            }
            if (Session.hidValidaTipoCliente == '2') {
                controls.chkSinCostoTope.prop('checked', false);
                controls.chkSinCostoTope.attr('disabled', true);
            }
            if (controls.ddlTopesConsumo.val() == that.strConfigVariables.strCodServAdditional && Session.hidValidaTipoCliente == '1') {
                controls.chkSinCostoTope.prop('checked', false);
                controls.chkSinCostoTope.attr('disabled', true);
            }
            if (controls.ddlTopesConsumo.val() == Session.hidOpTopeCodAutomatico && Session.hidValidaTipoCliente == '1') {
                controls.chkSinCostoTope.prop('checked', false);
                controls.chkSinCostoTope.attr('disabled', true);
            }
        },
        reemplazar: function (id) {
            var that = $("#" + id);
            var doc = that.val();
            var temp = doc.split('.');

            if (doc.indexOf(".") > -1) {
                if ((temp[0] == '') && (temp[1] == '')) {
                    that.val(doc.replace('.', '0.00'));
                }
                if ((temp[0] == '') && (temp[1] != '')) {
                    if (temp[1].length == 1) {
                        that.val('0' + '.' + temp[1] + '0');
                    } else {
                        that.val('0' + '.' + temp[1]);
                    }
                }
                if ((temp[0] != '') && (temp[1] == '')) {
                    that.val(parseFloat(temp[0]) + '.' + '00');
                }
                if ((temp[0] != '') && (temp[1] != '')) {
                    if (temp[1].length == 1) {
                        that.val(parseFloat(temp[0]) + '.' + temp[1] + '0');
                    } else {
                        that.val(parseFloat(temp[0]) + '.' + temp[1]);
                    }
                }
            } else {
                if (doc != '') {
                    that.val(parseFloat(temp) + '.00');
                } else {
                    that.val('0.00');
                }
            }
        },
        OpcionTope: function () {
            var that = this;
            var controls = that.getControls();
            var objAcceso = Session.hidAccesoPagina; //Modificar Roberto - Eliminar o reemplazar por la llave correcta
            var objTopeSinCosto = that.strConfigVariables.strPermissionNotTop;
            if (controls.chkSinCostoTope.is(':checked')) {
                if (objAcceso.indexOf(objTopeSinCosto) == -1) {
                    alert('Usted No tiene Autorización para activar Check Sin costo, por favor comuníquese con su supervisor.',"Alerta");
                    // ValidaLogin('gConstkeySinCostoT');
                } else {
                    controls.chkSinCostoTope.prop('checked', true);
                }
            } else {
                controls.ddlTopesConsumo.attr("disabled", false);
            }
        },
        f_checkedTopes: function (valorT) {
            var that = this,
                controls = that.getControls();
            switch (valorT) {

                case 'SIN':
                    Session.HidTopeConsumo = "SIN";
                    controls.ddlTopesConsumo.attr("disabled", true);
                    controls.chkSinCostoTope.prop('checked', false);
                    controls.chkSinCostoTope.attr("disabled", true);
                    controls.ddlTopesConsumo.val();
                    $.ajaxSetup({ cache: false });
                    var objPlansServices = {};
                    objPlansServices.strIdSession = Session.IDSESSION;
                    objPlansServices.TMCODE = Session.NuevoPlanMigradoPost.TMCODE;
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        async: false,
                        url: '/Transactions/Postpaid/PlanMigration/GetPlansServices',
                        data: JSON.stringify(objPlansServices),
                        success: function (response) {
                            var flgAutomaticoTemp = '0';
                            var flgCincoSolesTemp = '0';
                            var flgAdicionalTemp = '0';
                            Session.hidFlgAutomatico = '0';
                            Session.hidFlgCincoSoles = '0';
                            Session.hiFlgAdicionalTope = '0';
                            if (response.data != '') {
                                if (response.data.FlgTopeAutomatico != undefined) {
                                    flgAutomaticoTemp = response.data.FlgTopeAutomatico;
                                    flgCincoSolesTemp = response.data.FlgCincoSoles;
                                    flgAdicionalTemp = response.data.FlgAdicionales;
                                    Session.hidFlgAutomatico = flgAutomaticoTemp;
                                    Session.hidFlgCincoSoles = flgCincoSolesTemp;
                                    Session.hiFlgAdicionalTope = flgAdicionalTemp;
                                    if (flgAutomaticoTemp == '0' && flgCincoSolesTemp == '0' && flgAdicionalTemp == '0') {
                                        controls.txt_rdSinTopeConsumo.text('Configurado según su plan tarifario');
                                        controls.txt_rdSinTopeConsumo.attr('style', 'width:300px');
                                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                                        controls.chkMantenerTopeConsumo.prop('checked', false);
                                        controls.rdConTopeConsumo.attr("disabled", true);
                                    } else {
                                        controls.chkMantenerTopeConsumo.attr('disabled', false);
                                        controls.rdConTopeConsumo.attr("disabled", false);
                                        controls.txt_rdSinTopeConsumo.text('Sin Tope de Consumo');
                                    }
                                    if (Session.hidCodSerActuals == Session.hidOpTopeCodAutomatico && flgAutomaticoTemp == '0') {
                                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                                        controls.chkMantenerTopeConsumo.prop('checked', false);
                                    } else if (Session.hidCodSerActuals == Session.hidOpTopeCons5Soles && flgCincoSolesTemp == '0') {
                                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                                        controls.chkMantenerTopeConsumo.prop('checked', false);
                                    } else if (Session.hidCodSerActuals == that.strConfigVariables.strCodServAdditional && flgAdicionalTemp == '0') {
                                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                                        controls.chkMantenerTopeConsumo.prop('checked', false);
                                    } else {
                                        if (flgAutomaticoTemp == '0' && flgCincoSolesTemp == '0' && flgAdicionalTemp == '0') {
                                            controls.chkMantenerTopeConsumo.attr('disabled', true);
                                            controls.chkMantenerTopeConsumo.prop('checked', false);
                                        } else {
                                            controls.chkMantenerTopeConsumo.attr('disabled', false);
                                        }
                                    }
                                    if (Session.hidCodSerActuals == '') {
                                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                                        controls.chkMantenerTopeConsumo.prop('checked', false);
                                    }
                                } else {
                                    flgAutomaticoTemp = '0';
                                    flgCincoSolesTemp = '0';
                                    flgAdicionalTemp = '0';
                                    Session.hidFlgAutomatico = flgAutomaticoTemp;
                                    Session.hidFlgCincoSoles = flgCincoSolesTemp;
                                    Session.hiFlgAdicionalTope = flgAdicionalTemp;
                                    controls.chkMantenerTopeConsumo.attr('disabled', true);
                                    controls.chkMantenerTopeConsumo.prop('checked', false);
                                    controls.rdConTopeConsumo.attr("disabled", true);
                                }
                            } else {
                                flgAutomaticoTemp = '0';
                                flgCincoSolesTemp = '0';
                                flgAdicionalTemp = '0';
                                Session.hidFlgAutomatico = flgAutomaticoTemp;
                                Session.hidFlgCincoSoles = flgCincoSolesTemp;
                                Session.hiFlgAdicionalTope = flgAdicionalTemp;
                                controls.chkMantenerTopeConsumo.attr('disabled', true);
                                controls.chkMantenerTopeConsumo.prop('checked', false);
                                controls.rdConTopeConsumo.attr("disabled", true);
                            }
                        }
                    });

                    if (Session.hidFamilia != Session.CodPlanSinTopeConsAutorizacion && Session.hidCodSerActuals == Session.gCodCostoCero) {
                        controls.chkMantenerTopeConsumo.attr('disabled', true);
                        controls.chkMantenerTopeConsumo.prop('checked', false);
                    }

                    break;

                case 'CON':
                    Session.HidTopeConsumo = "CON";
                    $.ajaxSetup({ cache: false });
                    if (Session.NuevoPlanMigradoPost.TMCODE == '' || Session.NuevoPlanMigradoPost.TMCODE == undefined) {
                        alert('Debe seleccionar primero el Nuevo Plan para poder seleccionar esta opción.',"Alerta");
                        controls.rdConTopeConsumo.prop('checked', false);
                        controls.rdSinTopeConsumo.prop('checked', true);
                        return false;
                    }

                    controls.ddlTopesConsumo.attr("disabled", false);
                    var objPlansServices = {};
                    objPlansServices.strIdSession = Session.IDSESSION;
                    objPlansServices.TMCODE = Session.NuevoPlanMigradoPost.TMCODE;
                    $.app.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        async: false,
                        url: '/Transactions/Postpaid/PlanMigration/GetPlansServices',
                        data: JSON.stringify(objPlansServices),
                        success: function (response) {
                            var flgAutomatico = '0';
                            var flgCincoSoles = '0';
                            var flgAdicionalTope = '0';

                            Session.hidFlgAutomatico = '0';
                            Session.hidFlgCincoSoles = '0';
                            Session.hiFlgAdicionalTope = '0';
                            if (response.data != '') {
                                if (response.data.FlgTopeAutomatico != undefined) {
                                    flgAutomatico = response.data.FlgTopeAutomatico;
                                    flgCincoSoles = response.data.FlgCincoSoles;
                                    flgAdicionalTope = response.data.FlgAdicionales;
                                    Session.hidFlgAutomatico = flgAutomatico;
                                    Session.hidFlgCincoSoles = flgCincoSoles;
                                    Session.hiFlgAdicionalTope = flgAdicionalTope;
                                } else {
                                    flgAutomatico = '0';
                                    flgCincoSoles = '0';
                                    flgAdicionalTope = '0';
                                    Session.hidFlgAutomatico = flgAutomatico;
                                    Session.hidFlgCincoSoles = flgCincoSoles;
                                    Session.hiFlgAdicionalTope = flgAdicionalTope;
                                }
                            } else {
                                flgAutomatico = '0';
                                flgCincoSoles = '0';
                                flgAdicionalTope = '0';
                                Session.hidFlgAutomatico = flgAutomatico;
                                Session.hidFlgCincoSoles = flgCincoSoles;
                                Session.hiFlgAdicionalTope = flgAdicionalTope;
                            }
                        }
                    });

                    break;

                case 'MANT':
                    Session.HidTopeConsumo = "CON";
                    if (controls.chkMantenerTopeConsumo.is(':checked')) {
                        controls.ddlTopesConsumo.val();
                        controls.ddlTopesConsumo.attr("disabled", true);
                        controls.chkSinCostoTope.prop('checked', false);
                        controls.chkSinCostoTope.attr("disabled", true);
                        controls.rdSinTopeConsumo.prop('checked', false);
                        controls.rdSinTopeConsumo.attr("disabled", true);
                        controls.rdConTopeConsumo.prop('checked', false);
                        controls.rdConTopeConsumo.attr("disabled", true);
                    } else {
                        if (Session.hidFlgAutomatico == '0' && Session.hidFlgCincoSoles == '0' && Session.hiFlgAdicionalTope == '0') {
                            controls.ddlTopesConsumo.val();
                            controls.ddlTopesConsumo.attr("disabled", true);
                            controls.chkSinCostoTope.prop('checked', false);
                            controls.chkSinCostoTope.attr("disabled", true);
                            controls.rdSinTopeConsumo.prop('checked', false);
                            controls.rdSinTopeConsumo.attr("disabled", true);
                            controls.rdConTopeConsumo.prop('checked', false);
                            controls.rdConTopeConsumo.attr("disabled", true);
                        } else {
                            controls.rdSinTopeConsumo.attr("disabled", false);
                            controls.rdSinTopeConsumo.prop('checked', false);
                            controls.rdConTopeConsumo.attr("disabled", false);
                        }
                    }
                    break;
            }
        },
        getConfigTfiBam: function () {
            var objShow = {};
            objShow.strIdSession = Session.IDSESSION;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                url: '/Transactions/Postpaid/PlanMigration/GetConfigBamTFI',
                data: JSON.stringify(objShow),
                success: function (response) {
                    Session.PlanBAM = response.data[1];
                    Session.PlanTFI = response.data[2];
                    Session.TipoClienteAplicacion = response.data[0];
                }
            });
        },
        createDropdownddlTopesConsumo: function (response) {
            var that = this,
                controls = that.getControls();

            controls.ddlTopesConsumo.empty().html('');
            controls.ddlTopesConsumo.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response != null) {
                $.each(response.listItemOpcTope, function (index, value) {
                    controls.ddlTopesConsumo.append($('<option>', { value: value.Codigo, html: value.Descripcion }));
                });
            }
        },
        InitArea: function () {
            var that = this, objArea = {};
            objArea.strIdSession = Session.IDSESSION;

            $.ajax({
                url: '/CommonServices/GetArea',
                data: JSON.stringify(objArea),
                type: 'POST',
                contentType: "application/json; charset=utf-8",
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
        getCACDAC: function () {
            var that = this, objCacDacType = {};
            objCacDacType.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objCacDacType),
                url: '/Transactions/CommonServices/GetCacDacType',
                success: function (response) {
                    that.createDropdownCACDAC(response);
                }
            });
        },
        createDropdownCACDAC: function (response) {
            var that = this,
                controls = that.getControls();
            controls.cbo_PostMigrationCacDac.append($('<option>', { value: '', html: 'Seleccionar' }));

            if (response.data != null) {
                $.each(response.data.CacDacTypes, function (index, value) {
                    controls.cbo_PostMigrationCacDac.append($('<option>', { value: value.Code, html: value.Description }));
                });
            }
        },
        GetTblServiciosAsociadoAgregadosPlan_Post: function (response) {
            var controls = this.getControls();
            var that = this;
            //console.logresponse);
            that.tblServiciosAsociadoAgregadosPlan_Post = controls.tblServiciosAsociadoAgregadosPlan_Post.DataTable({
                "scrollY": "200px",
                "info": false,
                "scrollCollapse": true,
                "paging": false,
                "select": "single",
                "searching": false,
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página.",
                    "zeroRecords": "No existen datos",
                    "info": " ",
                    "infoEmpty": " ",
                    "infoFiltered": "(filtered from _MAX_ total records)"
                }
            });
        },
        getLoadtblServiciosAsociadosPlan_Post: function (response) {
            var controls = this.getControls();
            var that = this;
            that.tblServiciosAsociadosPlan_Post = controls.tblServiciosAsociadosPlan_Post.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollY: 250,
                scrollX: true,
                scrollCollapse: true,
                data: response,
                destroy: true,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                },
                columns: [
                   { "data": null },
                   { "data": "CO_SER" },
                   { "data": "DE_SER" },
                   { "data": "CO_EXCL" },
                   { "data": "DE_EXCL" },
                   { "data": "TMCODE" },
                   { "data": "CARGO_FIJO" },
                   { "data": "PERIODOS" },
                ],
                "columnDefs": [
                        {
                            targets: 0,
                            render: function (data, type, full, meta) {
                                var $rb = $('<input>', {
                                    type: 'radio',
                                    value: full.CO_SER + '_' + full.DE_SER + '_' + full.CO_EXCL + '_' + full.DE_EXCL + '_' + full.TMCODE + '_' + full.CARGO_FIJO + '_' + full.PERIODOS + '_'
                                    , name: 'optionsRadiosAsociados',
                                    id: "optionsRadiosAsociados_" + full.CO_SER
                                });
                                return $rb[0].outerHTML;
                            }
                        },
                        {
                            targets: [1, 3, 5],
                            visible: false
                        },
                ]
            });
        },
        SetAgregaPlanArmaTuPost: function (COD_PROD, TMCODE, DESC_PLAN, VERSION, CAT_PROD, COD_CARTA_INFO, FECHA_INI_VIG, FECHA_FIN_VIG, ID_TIPO_PROD, USUARIO, strCostoCFbase, strDesCFBase, PLAN_LOCAL, MODALIDAD, FAMILIA) {
            var controls = this.getControls();
            var that = this;
            Session.hidCartaInfor = COD_CARTA_INFO;
            Session.hidDescripProducto = DESC_PLAN;
            Session.hidPlanLocal = PLAN_LOCAL;
            controls.txt_rdSinTopeConsumo.html("Sin Tope de Consumo");
            controls.txtPlanNuevoAgregado.text(DESC_PLAN).css("color", "#d9534f");
            controls.lblNuevoPlan_Post.show();
            controls.rdConTopeConsumo.attr("disabled", false);
            controls.rdSinTopeConsumo.prop("checked", false);
            controls.rdConTopeConsumo.prop("checked", false);
            controls.chkSinCostoTope.prop("checked", false);
            controls.txt_Post_MontoFidelizaApadece.val("0");
            controls.txt_Post_TotalPenalidadCobrar.val(Session.hidCobroApadece);
            Session.HidTopeConsumo = "CON";
            Session.hidCodProd = COD_PROD + ";" + TMCODE + ";";
            //Modificar Roberto - Implementar logica para ocultar y mostrar controles
            if (ID_TIPO_PROD == "01") {
                controls.trIDCantidadCFijo2.attr("style", "display:none");
                controls.trIDCargoFijo2.attr("style", "display:none");
                controls.idTRListaPlanCombo.attr("style", "display:none");
                controls.tbCargoFijoPlanBase.attr("style", "display:block");
                controls.idTrNuevoPlan.attr("style", "display:block");
                that.ConsultProductBasePlan(1, COD_PROD);
                Session.hidTipoPlan = 1;
                if (Session.FlagFidelizacion == '0') {
                    if (TMCODE == Session.CodPlanClaroConexionChip.split("|")[0] || TMCODE == Session.CodPlanClaroConexionChip.split("|")[1] || TMCODE == Session.CodPlanClaroConexionChip.split("|")[2]) {
                        if (controls.txt_CobroApadece_Post.val() == '0') {
                            alert(Session.gConstMsjPlanClaroConexionChip,"Alerta");
                            controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
                            controls.chkFidelizaPenalidad.prop("checked", false);
                        }
                        controls.chkFidelizaPenalidad.attr("disabled", true);
                    }
                    else {
                        controls.chkFidelizaPenalidad.prop("checked", false);
                        controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
                        controls.chkFidelizaPenalidad.attr("disabled", false);
                    }
                } else {
                    if (TMCODE == Session.CodPlanClaroConexionChip.split("|")[0] || TMCODE == Session.CodPlanClaroConexionChip.split("|")[1] || TMCODE == Session.CodPlanClaroConexionChip.split("|")[2]) {
                        controls.chkFidelizaPenalidad.attr("disabled", false);
                    }
                }
            }
            else if (ID_TIPO_PROD == "02") {
                controls.idTRListaPlanCombo.attr("style", "display:block");
                controls.IdTrArmaPlanesCombos.attr("style", "display:none");
                controls.trIDCantidadCFijo2.attr("style", "display:block");
                controls.trIDCargoFijo2.attr("style", "display:block");
                controls.tblAgregarBoton.attr("style", "display:none");
                controls.tbCargoFijoPlanBase.attr("style", "display:none");
                controls.idTrNuevoPlan.attr("style", "display:none");
                that.ConsultProductBasePlan(2, COD_PROD);
                Session.hidTipoPlan = 2;
                controls.lblPost_CargoFijoDescNuevoPlanBase.text(strDesCFBase.toUpperCase()).css("color", "#d9534f");;
                controls.lblPost_CargoFijoNuevoPlanBase.text(strCostoCFbase.toUpperCase());
            }
            else if (ID_TIPO_PROD == "03") {
                controls.idTRListaPlanCombo.attr("style", "display:block");
                controls.IdTrArmaPlanesCombos.attr("style", "display:none");
                controls.trIDCantidadCFijo2.attr("style", "display:block");
                controls.trIDCargoFijo2.attr("style", "display:block");
                controls.tblAgregarBoton.attr("style", "display:block");
                controls.tbCargoFijoPlanBase.attr("style", "display:none");
                controls.idTrNuevoPlan.attr("style", "display:none");
                that.ConsultProductBasePlan(3, COD_PROD);
                Session.hidTipoPlan = 3;
                controls.lblPost_CargoFijoDescNuevoPlanBase.text(strDesCFBase.toUpperCase()).css("color", "#d9534f");;
                controls.lblPost_CargoFijoNuevoPlanBase.text(strCostoCFbase);
            }

            if (Session.DATACUSTOMER.TipoCliente == Session.TipoClienteAplicacion && Session.hidCodSerActuals == Session.gCodCostoCero) {//Modificar el hidcodseractuals
                controls.chkMantenerTopeConsumo.attr("disabled", true);
                controls.chkMantenerTopeConsumo.prop("checked", false);
            }
            Session.hidModalidad = MODALIDAD;
            Session.hidFamilia = FAMILIA;
        },
        IsPlanTFI: function (CodPlanTarifario, CodPlanesTarifariosTFI) {
            var datos = new Array();
            var existe = false;
            datos = CodPlanesTarifariosTFI.split('|');
            var plan = CodPlanTarifario;
            for (var i = 0; i < datos.length; i++) {
                if (plan == datos[i])

                { existe = true; break; }
            }
            return existe;
        },
        IsPlanBAM: function (CodPlanTarifario, CodPlanesTarifariosBAM) {
            var datos = new Array();
            var existe = false;
            datos = CodPlanesTarifariosBAM.split('|');
            var plan = CodPlanTarifario;
            for (var i = 0; i < datos.length; i++) {
                if (plan == datos[i])

                { existe = true; break; }
            }
            return existe;
        },
        ConsultProductBasePlan: function (codigo, codigoProducto) {
            var controls = this.getControls();
            var objPlan = {};
            var that = this;
            objPlan.strIdSession = Session.IDSESSION;
            objPlan.CodProducto = codigoProducto;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: false,
                data: JSON.stringify(objPlan),
                url: '/Transactions/Postpaid/PlanMigration/GetServByTransCodeProductResponse',
                success: function (response) {
                    var pMontoConIgv;
                    var dblCargoFijo;
                    that.getLoadtblServiciosAsociadosPlan_Post(response.data.lstTopConsumption);
                    if (codigo == "01") {
                        controls.lblPost_CargoFijoTotalPlanSin.text(response.data.CargoFijoPorPlan);
                        pMontoConIgv = (parseFloat(response.data.CargoFijoPorPlan) * parseFloat(that.strGlobalVariables.IGVPercent)).toFixed(2);//modificar roberto
                        controls.lblPost_CargoFijoTotalPlanCon.text(pMontoConIgv);
                        that.ValidaApadece2(response.data.CargoFijoPorPlan);
                        if (controls.txt_RegistroApadece_Post.val() > 0) {
                            if (that.IsPlanBAM(Session.codigoPlanTarifario, Session.PlanBAM) || that.IsPlanTFI(Session.codigoPlanTarifario, Session.PlanTFI)) {
                                if (controls.txt_RegistroApadece_Post.val() == "")
                                { controls.txt_RegistroApadece_Post.val("0"); }
                                controls.txt_CobroApadece_Post.val(controls.txt_RegistroApadece_Post.val());
                                Session.hidCobroApadece = controls.txt_CobroApadece_Post.val();
                                controls.txt_Post_TotalPenalidadCobrar.val((redondear((parseFloat(Session.hidCobroApadece) - parseFloat(controls.txt_Post_TotalPenalidadCobrar.val())), 2)).toFixed(2));
                            }
                            else {
                                that.f_obtenerReintegroEquipo(pMontoConIgv);
                            }
                        }
                    }
                    else if (codigo == "02") {
                        var dblCargoFijoProd = ($.trim(response.data.TotalCargoFijo) != '' ? parseFloat(response.data.TotalCargoFijo) : 0);
                        Session.dblCargoFijoBase = parseFloat(response.data.CargoFijoPorPlan);
                        var dblCFTotal = dblCargoFijoProd;
                        controls.lblPost_CargoTotalDelPlanSin.text(dblCFTotal.toFixed(2));
                        dblCargoFijo = dblCFTotal;
                        pMontoConIgv = (parseFloat(dblCFTotal) * parseFloat(that.strGlobalVariables.IGVPercent)).toFixed(2);
                        controls.lblPost_CargoTotalDelPlanCon.text(pMontoConIgv);
                        that.ValidaApadece2(dblCFTotal.toFixed(2));
                        that.tblServiciosAsociadosPlan_Post.column(0).visible(false);
                    }
                    else if (codigo == '03') {
                        Session.dblCargoFijoBase = 0;
                        Session.dblCargoFijoBase = parseFloat(response.data.CargoFijoPorPlan);
                        controls.lblPost_CargoTotalDelPlanSin.text(Session.dblCargoFijoBase.toFixed(2));
                        dblCargoFijo = Session.dblCargoFijoBase;
                        pMontoConIgv = (parseFloat(Session.dblCargoFijoBase) * parseFloat(that.strGlobalVariables.IGVPercent)).toFixed(2);
                        controls.lblPost_CargoTotalDelPlanCon.text(pMontoConIgv);
                        that.ValidaApadece2(Session.dblCargoFijoBase.toFixed(2));
                        that.tblServiciosAsociadosPlan_Post.column(0).visible(true);
                    }
                    if (codigo == "02" || codigo == "03") {

                        if (codigo == "02") {
                            //$("#tblServiciosAsociadosPlan_Post").find('thead th').eq(0).attr("style", "display:none");
                            //$('#tblServiciosAsociadosPlan_Post').find('tbody tr').eq(0).attr("style", "display:none");
                            if (response.data.NroRegistro != '') {
                                if (parseInt(response.data.NroRegistro) > 0) {
                                    controls.lblMensajeCantidadServiciosAgr.text('Cantidad de Servicios Asignados al Plan: ' + (parseInt(response.data.NroRegistro) - 1));
                                } else {
                                    controls.lblMensajeCantidadServiciosAgr.text('Cantidad de Servicios Asignados al Plan: ' + (parseInt(0)));
                                }
                            }
                        } else if (codigo == '03') {
                            //$("#tblServiciosAsociadosPlan_Post").find('thead th').eq(0).attr("style", "display:block");
                            //$('#tblServiciosAsociadosPlan_Post').find('tbody tr').eq(0).attr("style", "display:block");
                            controls.lblMensajeCantidadServiciosAgr.text('Cantidad de Servicios Agregados : 0');
                        }
                    }
                }
            });
        },
        ValidaApadece2: function (p_dblValorTotNuevoPlan) {
            var controls = this.getControls();
            var that = this;
            var ValorTotalPlan = Session.hidCargoFijoTotalPlan;
            var valorDiferencia = 0;
            var valorDiferenciaCF = that.strConfigVariables.strDiferenceMountCF;
            var pIgvCF = that.strGlobalVariables.IGVPercent;
            var pMontoConIgvCF = 0;
            if (isNaN(p_dblValorTotNuevoPlan)) {
                controls.chckApadeceNoAplica.prop("checked", false);
            } else {
                if (p_dblValorTotNuevoPlan != '') {
                    if (parseFloat(p_dblValorTotNuevoPlan) >= parseFloat(ValorTotalPlan)) {
                        controls.chckApadeceNoAplica.prop("checked", true);
                        controls.chkFidelizaApadece.attr("disabled", "disabled");
                        controls.txt_CobroApadece_Post.attr("disabled", "disabled");
                        controls.txt_Post_TotalPenalidadCobrar.val('0');
                        that.f_ValidaEspecificField();
                    } else {
                        pMontoConIgvCF = (parseFloat(p_dblValorTotNuevoPlan) * parseFloat(pIgvCF)).toFixed(2);
                        valorDiferencia = (parseFloat(ValorTotalPlan) - parseFloat(pMontoConIgvCF)).toFixed(2);

                        if (parseFloat(valorDiferencia) <= parseFloat(valorDiferenciaCF)) {
                            controls.chckApadeceNoAplica.prop("checked", true);
                            controls.chkFidelizaApadece.attr("disabled", "disabled");
                            controls.txt_CobroApadece_Post.attr("disabled", "disabled");
                            controls.txt_Post_TotalPenalidadCobrar.val('0');
                            that.f_ValidaEspecificField();
                        } else {
                            controls.chckApadeceNoAplica.prop("checked", false);
                            that.f_ValidaEspecificField();
                        }
                    }
                } else {
                    controls.chckApadeceNoAplica.prop("checked", false);
                    controls.chckApadeceNoAplica.prop("checked", false);
                    that.f_ValidaEspecificField();
                }
            }

        },
        f_ValidaEspecificField: function () {
            var that = this;
            var controls = this.getControls();
            if (controls.chckApadeceNoAplica.is(':checked')) {
                controls.txt_CobroApadece_Post.attr("disabled", "disabled");
                controls.chkFidelizaApadece.attr("checked", false);
                that.f_checked('chkFidelizaApadece');
                controls.chkFidelizaApadece.attr("disabled", "disabled");
                controls.txt_Post_TotalPenalidadCobrar.val('0');
            } else {
                if (Session.HidCorporativo != '1') {
                    controls.txt_CobroApadece_Post.attr("disabled", "disabled");
                } else {
                    controls.txt_CobroApadece_Post.removeAttr('disabled');
                }
                if (parseFloat(controls.txt_CobroApadece_Post.val()) > 0) {
                    controls.chkFidelizaApadece.removeAttr('disabled');
                    controls.txt_Post_TotalPenalidadCobrar.val(controls.txt_CobroApadece_Post.val());
                    that.f_checked('txtMontoFidelizaApadece');
                } else {
                    controls.chkFidelizaApadece.attr("disabled", "disabled");
                }
            }
        },
        f_checked: function (control) {
            var controls = this.getControls();
            var chkControl = document.getElementById(control);
            switch (control) {
                case 'txtCobroApadece':
                    if ((parseFloat(controls.txt_CobroApadece_Post.val()) >= 0) && (Session.HidCorporativo == '1')) {
                        Session.hidCobroApadece = controls.txt_CobroApadece_Post.val();
                        controls.chkFidelizaApadece.attr("disabled", false);
                        if (parseFloat(controls.txt_CobroApadece_Post.val()) > 0) {
                            controls.chk_Post_OCC.prop("checked", true);
                        } else {
                            controls.chk_Post_OCC.prop("checked", false);
                        }
                        var varMontoFidelizaApadece = controls.txt_Post_MontoFidelizaApadece.val() == "" ? 0 : controls.txt_Post_MontoFidelizaApadece.val();
                        controls.txt_CobroApadece_Post.val((parseFloat(Session.hidCobroApadece) - parseFloat(varMontoFidelizaApadece)).toFixed(2));

                        if (controls.txt_CobroApadece_Post.val() < 0) {
                            alert('Por favor, ingrese un monto mayor o igual al Monto Fidelización APADECE',"Alerta");
                            controls.txt_CobroApadece_Post.val() = controls.txt_CobroApadece_Post.val();
                            controls.txt_Post_MontoFidelizaApadece.val('0.00');
                        }
                    }
                    else if (controls.txt_CobroApadece_Post.val() == '') {
                        controls.txt_CobroApadece_Post.val('0');
                        controls.txt_CobroApadece_Post.val('0');
                        controls.chk_Post_OCC.prop("checked", false);
                    }
                    break;
                case 'chkFidelizaApadece':
                    if (controls.chkFidelizaApadece.is(':checked')) {
                        controls.txt_Post_MontoFidelizaApadece.attr('disabled', false);
                        controls.txt_Post_MontoFidelizaApadece.focus();
                    } else {
                        Session.hidApadecNoAplica = '0';
                        controls.txt_Post_MontoFidelizaApadece.attr('disabled', true);
                        controls.txt_Post_TotalPenalidadCobrar.attr("disabled", true);
                        controls.txt_Post_MontoFidelizaApadece.val((Session.hidCobroApadece - Session.hidCobroApadece).toFixed(2));
                        controls.txt_Post_TotalPenalidadCobrar.val((parseFloat(Session.hidCobroApadece))).toFixed(2);
                        Session.Fideliza = false;
                    }
                    break;
                case 'txtMontoFidelizaApadece':
                    if (controls.txt_Post_MontoFidelizaApadece.val() == 0) {
                    }
                    else if (parseFloat(controls.txt_Post_MontoFidelizaApadece.val()) > parseFloat(Session.hidCobroApadece)) {
                        alert('Por favor, ingrese un monto menor o igual al Cobro x Reintegro de Equipo(APADECE)',"Alerta");
                        controls.txt_Post_MontoFidelizaApadece.val('0');
                        controls.txt_Post_TotalPenalidadCobrar.val((redondear((Session.hidCobroApadece - controls.txt_Post_MontoFidelizaApadece.val()), 2)).toFixed(2));
                        controls.txt_Post_MontoFidelizaApadece.focus();
                    } else {
                        controls.txt_Post_TotalPenalidadCobrar.val((redondear((Session.hidCobroApadece - controls.txt_Post_MontoFidelizaApadece.val()), 2)).toFixed(2));

                    }
                    break;
                case 'txtCobroPenalidadPCS':
                    if (controls.txt_Post_PenalidadPCS.val() == 0 || controls.txt_Post_PenalidadPCS.val() == '') {
                        controls.rdbNotaDebito.prop('checked', false);
                        controls.chkFidelizaPenalidadPCS.attr('disabled', true);
                        controls.chkFidelizaPenalidadPCS.prop('checked', false);
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
                        controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                        controls.ddlArea.attr('disabled', true);
                        controls.ddlMotivo.attr('disabled', true);
                        controls.ddlSubMotivo.attr('disabled', true);
                        controls.ddlArea.val('0');
                        controls.ddlMotivo.val('0');
                        controls.ddlSubMotivo.val('0');
                        Session.hidFormaPago = '';
                    }
                    else if (parseFloat(controls.txt_Post_TotalFidelizacionPenalidadPCS.val()) > parseFloat(controls.txt_Post_PenalidadPCS.val())) {
                        alert('Por favor, ingrese un monto menor o igual al Cobro x Penalidad PCS',"Alerta");
                        controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.focus();
                    }
                    else {
                        controls.chkFidelizaPenalidadPCS.attr('disabled', false);
                        controls.rdbNotaDebito.prop('checked', true);
                        Session.hidFormaPago = 'D';
                        controls.ddlArea.attr('disabled', false);
                        controls.ddlMotivo.attr('disabled', false);
                        controls.ddlSubMotivo.attr('disabled', false);
                        if (controls.ddlArea.val() == 0) {
                            controls.ddlArea.val('0');
                            controls.ddlMotivo.val('0');
                            controls.ddlSubMotivo.val('0');
                        }
                        controls.txt_Post_TotalPenalidadPCSCobrar.val((controls.txt_Post_PenalidadPCS.val() - controls.txt_Post_TotalFidelizacionPenalidadPCS.val()).toFixed(2));
                    }
                    if (parseFloat(controls.txt_Post_PenalidadPCS.val()) > 0) {
                        controls.lblArea.text("Área(*):");
                        controls.lblMotivo.text("Motivo(*):");
                        controls.lblSubMotivo.text("SubMotivo(*):");
                        controls.idtrLeyenda.css("display", "block");

                    } else {
                        controls.lblArea.text("Área:");
                        controls.lblMotivo.text("Motivo:");
                        controls.lblSubMotivo.text("SubMotivo:");
                        controls.idtrLeyenda.css("display", "none");
                    }
                    break;
                case 'chkFidelizaPenalidadPCS':
                    if (controls.chkFidelizaPenalidadPCS.is(':checked')) {
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', false);
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.focus();
                    } else {
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
                        controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val((controls.txt_Post_PenalidadPCS.val() - controls.txt_Post_PenalidadPCS.val()).toFixed(2));
                        Session.Fideliza = false;
                    }
                    break;
                case 'txtMontoPenalidadPCS':
                    if (controls.txt_Post_TotalFidelizacionPenalidadPCS.val() == 0) {
                    } else if (parseFloat(controls.txt_Post_TotalFidelizacionPenalidadPCS.val()) > parseFloat(controls.txt_Post_PenalidadPCS.val())) {
                        alert('Por favor, ingrese un monto menor o igual al Cobro x Penalidad PCS',"Alerta");
                        controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.focus();
                    } else {
                        controls.txt_Post_TotalPenalidadPCSCobrar.val((controls.txt_Post_PenalidadPCS.val() - controls.txt_Post_TotalFidelizacionPenalidadPCS.val()).toFixed(2));
                    }
                    break;
                case 'rdbNotaDebito':
                    if (controls.rdbNotaDebito.is(':checked')) {
                        controls.ddlArea.attr('disabled', false);
                        controls.ddlMotivo.attr('disabled', false);
                        controls.ddlSubMotivo.attr('disabled', false);
                        Session.hidFormaPago = 'D';
                    }
                    break;
                case 'chkDivPenalidadPCS':
                    if (controls.chk_Post_PenalidadPCS.is(':checked')) {
                        controls.divPCS.css("display", "block");
                        controls.rdbNotaDebito.prop('checked', false);
                        controls.chkFidelizaPenalidadPCS.attr('disabled', true);
                        controls.chkFidelizaPenalidadPCS.prop('checked', false);
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
                        controls.txt_Post_TotalPenalidadPCSCobrar.val(controls.txt_Post_PenalidadPCS.val());
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                        controls.ddlArea.attr('disabled', true);
                        controls.ddlMotivo.attr('disabled', true);
                        controls.ddlSubMotivo.attr('disabled', true);
                        controls.ddlArea.val('0');
                        controls.ddlMotivo.val('0');
                        controls.ddlSubMotivo.val('0');
                        Session.hidFormaPago = '';
                    } else {
                        controls.rdbNotaDebito.prop('checked', false)
                        controls.chkFidelizaPenalidadPCS.prop('checked', false);
                        controls.ddlArea.val('0');
                        controls.ddlMotivo.val('0');
                        controls.ddlSubMotivo.val('0');
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.attr('disabled', true);
                        controls.txt_Post_PenalidadPCS.val('0');
                        controls.txt_Post_TotalPenalidadPCSCobrar.val('0');
                        controls.txt_Post_TotalFidelizacionPenalidadPCS.val('0');
                        Session.hidFormaPago = '';
                        controls.divPCS.css("display", "none");
                        Session.hidFormaPago = '';
                    }
                    break;
            }
        },
        
        f_validarSoloNumeros: function (e) {
            var CaracteresPermitidos = "0123456789.";
            var key = String.fromCharCode(window.event.keyCode);
            var valid = new String(CaracteresPermitidos);
            var ok = "no";
            for (var i = 0; i < valid.length; i++) {
                if (key == valid.substring(i, i + 1))
                    ok = "yes";
            }
            if ((key > 0x60) && (key < 0x7B))
                e.keyCode = 0;
        },
        f_obtenerReintegroEquipo: function (strCargoFijoNuevo) {

        },
        ConsultDetailProduct: function (cod) {

        },
        SelectionPlan: function (item) {
            var controls = this.getControls();
            var that = this;
            var strPlanLocal = $("#ddlMigracionFamilia").val();
            var strModalidad = $("#ddlMigracionModalidad").val();
            var strFamilia = $("#ddlMigracionFamilia").val();
            if (item.ID_TIPO_PROD == "02" || item.ID_TIPO_PROD == "03") {
                $.app.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    async: false,
                    url: '/Transactions/Postpaid/PlanMigration/GetFixedCostBasePlan',
                    data: JSON.stringify({ strIdSession: Session.IDSESSION, CodigoProduct: item.COD_PROD, IdProduct: item.ID_TIPO_PROD, CategoriaProducto: item.CAT_PROD, DescriptionPlan: item.DESC_PLAN }),
                    success: function (response) {
                        var respuesta = response.data.DescriptionOrigenPlan;
                        if (respuesta != null && respuesta.length > 0) {
                            if (item.ID_TIPO_PROD == "02")//Tipo seleccion 01=basico, 02=combos, 03=arma tu plan
                            {
                                that.SetAgregaPlanArmaTuPost(item.COD_PROD, item.TMCODE, item.DESC_PLAN, item.VERSION, item.CAT_PROD, item.COD_CARTA_INFO, item.FECHA_INI_VIG, item.FECHA_FIN_VIG, item.ID_TIPO_PROD, item.USUARIO, parseFloat(respuesta.split("|")[0]).toFixed(2), respuesta.split("|")[1], strPlanLocal, strModalidad, strFamilia);
                            }
                            else if (item.ID_TIPO_PROD == "03") {
                                that.SetAgregaPlanArmaTuPost(item.COD_PROD, item.TMCODE, item.DESC_PLAN, item.VERSION, item.CAT_PROD, item.COD_CARTA_INFO, item.FECHA_INI_VIG, item.FECHA_FIN_VIG, item.ID_TIPO_PROD, item.USUARIO, parseFloat(respuesta.split("|")[0]).toFixed(2), respuesta.split("|")[1], strPlanLocal, strModalidad, strFamilia);
                            }
                        }
                    }
                });
            }
            if (item.ID_TIPO_PROD == "01") {
                that.SetAgregaPlanArmaTuPost(item.COD_PROD, item.TMCODE, item.DESC_PLAN, item.VERSION, item.CAT_PROD, item.COD_CARTA_INFO, item.FECHA_INI_VIG, item.FECHA_FIN_VIG, item.ID_TIPO_PROD, item.USUARIO, '', '', strPlanLocal, strModalidad, strFamilia);
            }
        },
        strTransactionTypi: '',
        strClaseCode: '',
        strSubClaseCode: '',
        strTipo: '',
        strClase: '',
        strSubClase: '',
        strConsumerControl: '',
        strCorporate: '',
        strApplicationDate: '',
        strGlobalVariables: {},
        strConfigVariables: {}
    };

    $.fn.PostPlanMigrationDetail = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('PostPlanMigrationDetail'),
                options = $.extend({}, $.fn.PostPlanMigrationDetail.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('PostPlanMigrationDetail', data);
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
    $.fn.PostPlanMigrationDetail.defaults = {
    }

    //$('#PostPlanMigration').PostPlanMigrationDetail();
    $('#divBody').PostPlanMigrationDetail(); //Redirect ini  3.0
})(jQuery);
