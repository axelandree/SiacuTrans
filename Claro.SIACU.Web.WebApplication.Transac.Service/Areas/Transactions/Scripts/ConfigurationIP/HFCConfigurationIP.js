(function ($, undefined) {
   

    //Load Session
    var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));

    //console.logSessionTransac);
    var vFLAG_FRANJA = "0";
    var ConfigurationIPModel = {};
    ConfigurationIPModel.strRouteSiteInitial = "";
    ConfigurationIPModel.strMessageCustomerContractEmpty = "";
    ConfigurationIPModel.strMessageNotServiceInternet = "";
    ConfigurationIPModel.strRutaPdf = "";
    ConfigurationIPModel.strNroSOT = "";
    ConfigurationIPModel.strCaseId = "";
    ConfigurationIPModel.strFileName = "";
    ConfigurationIPModel.ConstanceXml = "";
    ConfigurationIPModel.gConstKeyPreguntaGenConfIP_IPFIJA = "";
    ConfigurationIPModel.gConstKeyPreguntaGenConfIP_PUERTO25 = "";
    ConfigurationIPModel.MessageConfirmacion = "";
    ConfigurationIPModel.CONTENIDO_COMERCIAL2 = "";
    ConfigurationIPModel.IPFIJA = "";
    ConfigurationIPModel.PUERTO25 = "";
    ConfigurationIPModel.strServidorLeerPDF = "";
    ConfigurationIPModel.COSTO_INSTALACION = "";
    
    ConfigurationIPModel.Planes_EVO = "";
    ConfigurationIPModel.Planes_Full_HD_Digital_30MB = "";
    ConfigurationIPModel.gConstKeyPlanActualCliente = "";

    ConfigurationIPModel.strEstadoContratoInactivo = "";
    ConfigurationIPModel.strEstadoContratoSuspendido = "";
    ConfigurationIPModel.strEstadoContratoReservado = "";

    ConfigurationIPModel.strMsjEstadoContratoInactivo = "";
    ConfigurationIPModel.strMsjEstadoContratoSuspendido = "";
    ConfigurationIPModel.strMsjEstadoContratoReservado = "";

    ConfigurationIPModel.strMensajeTransaccionFTTH = ""; 
    
    ConfigurationIPModel.strPlanoFTTH = ""; 


    var Form = function ($element, options) {

        $.extend(this, $.fn.HFCConfigurationIP.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element

            // ComboBox
            , cboCacDac                 : $('#ddlCACDAC', $element)
            , cboMotivoSot              : $('#cboMotivoSot', $element)
            , cboJobTypes               : $('#cboJobTypes', $element)
            , cboType                   : $("#cboType", $element)
            , cboBranchCustomer         : $("#cboBranchCustomer", $element)

            // Modal
            , ModalLoading              : $('#ModalLoading', $element)

            //Label - Customer
            , lblContact                : $("#lblContact", $element)
            , lblContract               : $('#lblContract', $element)
            , lblCustomerName           : $("#lblCustomerName", $element)
            , lblIdentificationDocument : $("#lblIdentificationDocument", $element)
            , lblTypeCustomer           : $("#lblTypeCustomer", $element)
            , lblDateActivation         : $("#lblDateActivation", $element)
            , lblCycleBilling           : $("#lblCycleBilling", $element)
            , lblReprLegal              : $("#lblReprLegal", $element)
            , lblDocReprLegal           : $("#lblDocReprLegal", $element)

            //Label - Direccion de Instalación
            , lblAddress                : $("#lblAddress", $element)
            , lblAddressNote            : $("#lblAddressNote", $element)
            , lblPais                   : $("#lblPais", $element)
            , lblDepartamento           : $("#lblDepartamento", $element)
            , lblProvincia              : $("#lblProvincia", $element)
            , lblDistrito               : $("#lblDistrito", $element)
            , lblCodePlans              : $("#lblCodePlans", $element)
            , lblUbigeo                 : $("#lblUbigeo", $element)

            // Tabla
            , tbConfigurationIP         : $("#tbConfigurationIP", $element)

            // CheckBox
             , chk_SendMail             : $("#chk_SendMail", $element)
            , chk_Fidelidad             : $("#chk_Fidelidad", $element)
            , txt_monto                 : $("#txt_monto", $element)
            // TextBox
             , txtNote                  : $("#txtNote", $element)
             , txt_SendMail             : $('#txt_SendMail', $element)
             , txt_phone_references     : $('#txt_phone_references')

            //Botones
            , btnSave                   : $("#btnSave", $element)
            , btnClose                  : $("#btnClose", $element)
            , btnConstancia             : $("#btnConstancia", $element)

            , lblTitle                  : $('#lblTitle', $element)   //Redirect 1.0

            // TR
            , trType                    : $("#trType", $element)

            , spnMainTitle              : $('#spnMainTitle',$element)

            , lblHCFNroSot              : $("#lblHCFNroSot", $element)
        });
    }

    Form.prototype = {
        constructor: Form,

        setControls: function (value) {
            this.m_controls = value;
        },
        getControls: function () {
            return this.m_controls || {};
        },

        init: function () {
           
            var that = this,
                controls = this.getControls();
            that.f_Loading();
            controls.chk_SendMail.addEvent(that, 'click', that.EmailActive_Click);
            controls.cboJobTypes.addEvent(that, 'change', that.JobType_Change);
            controls.btnSave.addEvent(that, 'click', that.Save_Click);
            controls.btnClose.addEvent(that, 'click', that.Close_Click);
            controls.btnConstancia.addEvent(that, 'click', that.Constacia_Click);
            controls.chk_Fidelidad.addEvent(that, 'click', that.f_ActiveFidelidad);

            controls.cboCacDac.addEvent(that, 'change', that.cboCacDac_Change);
            that.windowAutoSize();
          
            that.maximizarWindow();
        
            that.LoadDataSession();

        },
        LoadDataSession: function () {
            var that = this,
               controls = this.getControls();

            controls.spnMainTitle.text("CONFIGURACIÓN DE SERVICIOS");
            Session.IDSESSION = '20170518150515737831';
            Session.DATACUSTOMER = SessionTransac.SessionParams.DATACUSTOMER;
            Session.DATASERVICE = SessionTransac.SessionParams.DATASERVICE;
            Session.USERACCESS = SessionTransac.SessionParams.USERACCESS;

            if (Session.DATACUSTOMER.Email != null) {
                controls.txt_SendMail.val(Session.DATACUSTOMER.Email);
                }

            that.LoadVariables();
            
        },
        LoadVariables: function () {
            var that = this,
                 controls = that.getControls(),
                objModel = {};

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/LoadVariables";
            objModel.strIdSession = Session.IDSESSION;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(objModel),
                url: myUrl,
                async: false,
                success: function (response) {

                    Session.HIDDEN.hdnServName = response[0];
                    Session.HIDDEN.hdnLocalAdd = response[1];
                    Session.HIDDEN.UserHostName = response[2];
                    Session.CustomerRequestId = response[3];

                    Session.MESSAGEHFC.tituloPagina = response[4];
                    Session.MESSAGEHFC.MessageConfirm = response[5];

                    Session.MESSAGEHFC.Message1 = response[6];
                    Session.MESSAGEHFC.Message2 = response[7];
                    Session.MESSAGEHFC.Message8 = response[8];
                    Session.MESSAGEHFC.Message9 = response[9];
                    Session.MESSAGEHFC.Message10 = response[10];
                    Session.MESSAGEHFC.Message11 = response[11];
                    Session.MESSAGEHFC.MessageExito = response[12];

                    ConfigurationIPModel.gConstKeyPreguntaGenConfIP_IPFIJA = response[13];
                    ConfigurationIPModel.gConstKeyPreguntaGenConfIP_PUERTO25 = response[14];
                    ConfigurationIPModel.IPFIJA = response[15];
                    ConfigurationIPModel.PUERTO25 = response[16];
                    ConfigurationIPModel.strServidorLeerPDF = response[17];
                    ConfigurationIPModel.CONTENIDO_COMERCIAL2 = response[18];
                    ConfigurationIPModel.COSTO_INSTALACION = response[19];

                    ConfigurationIPModel.Planes_EVO = response[20];
                    ConfigurationIPModel.Planes_Full_HD_Digital_30MB = response[21];
                    ConfigurationIPModel.gConstKeyPlanActualCliente = response[22];
                    
                    ConfigurationIPModel.strEstadoContratoInactivo = response[23];
                    ConfigurationIPModel.strEstadoContratoSuspendido = response[24];
                    ConfigurationIPModel.strEstadoContratoReservado = response[25];

                    ConfigurationIPModel.strMsjEstadoContratoInactivo = response[26];
                    ConfigurationIPModel.strMsjEstadoContratoSuspendido = response[27];
                    ConfigurationIPModel.strMsjEstadoContratoReservado = response[28];
                    //Evalenzs
                    ConfigurationIPModel.strMensajeTransaccionFTTH = response[29];
                    ConfigurationIPModel.strPlanoFTTH = response[30];
                    that.f_ValidatePage();

                }
            });
        },
        f_ValidatePage: function () {
            var that = this,
                controls = this.getControls();

            //RONALDRR - INI - CONFIGURACION DE SERVICIOS

            var strPlano = Session.DATACUSTOMER.PlaneCodeInstallation;
            var strPlanoFTTH = ConfigurationIPModel.strPlanoFTTH;
                        
            if (strPlano.search(strPlanoFTTH) > 0 && ConfigurationIPModel.strMensajeTransaccionFTTH != '') {
                alert(ConfigurationIPModel.strMensajeTransaccionFTTH.replace('{0}', "CONFIGURACIÓN DE SERVICIOS"), "Alerta", function () {//Activación y Desactivación de Servicios Adicionales
                    parent.window.close();
                });
                return false;
            }
            //RONALDRR - FIN

            if (Session.DATASERVICE.StateLine == ConfigurationIPModel.strEstadoContratoInactivo) {
                alert(ConfigurationIPModel.strMsjEstadoContratoInactivo, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }
            if (Session.DATASERVICE.StateLine == ConfigurationIPModel.strEstadoContratoReservado) {
                alert(ConfigurationIPModel.strMsjEstadoContratoReservado, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }
            if (Session.DATASERVICE.StateLine == ConfigurationIPModel.strEstadoContratoSuspendido) {
                alert(ConfigurationIPModel.strMsjEstadoContratoSuspendido, 'Alerta', function () {
                    parent.window.close();
                });
                that.f_blockForm();
            }

            that.render();
        },
        render: function () {
            var that = this, controls = this.getControls();
            that.f_Loading();
        
            
            that.f_PageLoad();
            
            that.InitMotiveDat();
            that.InitJobType();
            that.InitBranchCustomer();
            that.InitCacDat();
            that.InitPluginGrid();

        },
        f_PageLoad: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                oUserAccess = Session.USERACCESS,
                oDataService = Session.DATASERVICE;
               //  oDatLine = Session.DATLINE;

                

                if (oUserAccess == null || oCustomer == null) {
                    that.Close_Click();
                    opener.parent.top.location.href = ConfigurationIPModel.strRouteSiteInitial;
                }
                if (oUserAccess.userId == null || oUserAccess.userId == '0' || oUserAccess.userId == '' || oUserAccess.userId == '&nbsp;') {
                    that.Close_Click();
                    opener.parent.top.location.href = ConfigurationIPModel.strRouteSiteInitial;
                }
                if (oCustomer.ContractID == null || oCustomer.ContractID == '0' || oCustomer.ContractID == '' || oCustomer.ContractID == '&nbsp;') {
                    alert(ConfigurationIPModel.strMessageCustomerContractEmpty, "Alerta", function () { that.Close_Click(); });
                }

                //that.Validate_Services();
                if (oDataService.InternetValue == 'F' || oDataService.InternetValue == null || oDataService.InternetValue=='' ) {
                    // alert("No tiene servicio de Internet", "Aviso", function () { that.Close_Click(); });
                }
                document.getElementById(controls.txt_SendMail.attr("id")).style.display = 'none';
                controls.btnConstancia.prop("disabled", true);
                that.LoadDataCustomer();
                that.LoadDataTransaction();
                controls.txt_monto.val(ConfigurationIPModel.COSTO_INSTALACION);
        },
        f_blockForm: function () {
            var that = this,
                controls = this.getControls();

            controls.btnSave.prop('disabled', true);
            controls.btnConstancia.prop('disabled', true);
        },
        LoadDataCustomer: function () {
            var that = this,
               controls = that.getControls(),
               oCustomer = Session.DATACUSTOMER,
               oHidden = Session.HIDDEN;
              // oDatLine = Session.DATLINE;

            //controls.lblContact.text(oCustomer.Telephone);
            //controls.lblTypeCustomer.text(oCustomer.CustomerType);
            //controls.lblContract.text(oCustomer.ContractID);
            //controls.lblCustomerName.text(oCustomer.FullName);
            //controls.lblDocReprLegal.text(oCustomer.LegalCode);
            //controls.lblReprLegal.text(oCustomer.LegalAgent);
            //controls.lblIdentificationDocument.text(oCustomer.DNIRUC);

            //controls.lblDateActivation.text(Session.DATACUSTOMER.ActivationDate);
            //controls.lblCycleBilling.text(oCustomer.CustomerContact);
            ////console.logoCustomer);

        },
        LoadDataTransaction: function () {
            var that = this,
              controls = that.getControls(),
              oCustomer = Session.DATACUSTOMER,
              oHidden = Session.HIDDEN;
            //  oDatLine = Session.DATLINE;

            //controls.lblAddress.text(oCustomer.LegalAddress);
            //controls.lblAddressNote.text(oCustomer.LegalUrbanization);
            //controls.lblDepartamento.text(oCustomer.LegalDepartament);
            //controls.lblDistrito.text(oCustomer.LegalDistrict);
            //controls.lblCodePlans.text(oCustomer.PlaneCode);
            //controls.lblPais.text(oCustomer.LegalCountry);
            //controls.lblProvincia.text(oCustomer.LegalProvince);
            //controls.lblUbigeo.text(oCustomer.InstallUbigeo);

        },
           
        InitMotiveDat: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = Session.IDSESSION;

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/CommonServices/GetMotiveSot";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {

                    controls.cboMotivoSot.html("");
                    controls.cboMotivoSot.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data.CacDacTypes != null) {
                        $.each(response.data.CacDacTypes, function (index, value) {

                            if (Session.CustomerRequestId == value.Code) {
                                controls.cboMotivoSot.append($('<option>', { value: value.Code, html: value.Description }));
                                controls.cboMotivoSot.val(Session.CustomerRequestId);
                                controls.cboMotivoSot.prop("disabled", true);
                            }


                        });



                    }

                },
                error: function (request, status, error) {
                    $.unblockUI();
                    //console.logerror);
                    alert(error, "Alerta");
                }
            });


        },
        InitJobType: function () {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = Session.IDSESSION;

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetJobTypeConfIp";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {
                    controls.cboJobTypes.html("");
                    controls.cboJobTypes.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboJobTypes.append($('<option>', { value: value.TIPTRA + '+' + value.FLAG_ACTIVA, html: value.DESCRIPCION }));
                        });
                    }
                },

                error: function (request, status, error) {
                    //console.logerror);
                    $.unblockUI();
                    alert(error, "Alerta");
                }
            });


        },
        InitTypeConfIp: function (JobType) {
            var that = this,
                controls = that.getControls(),
                model = {};
            model.strIdSession = Session.IDSESSION;
            model.strJobType = JobType;

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetTypeConfIp";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {

                    controls.cboType.html("");
                    controls.cboType.append($('<option>', { value: '-1', html: 'Seleccionar' }));
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {
                            controls.cboType.append($('<option>', { value: value.CODIGON, html: value.DESCRIP }));
                        });
                        controls.cboType.val("2");
                    }
                },
                error: function (request, status, error) {
                    //console.logerror);
                    $.unblockUI();
                    alert(error, "Alerta");
                }
            });
        },
        InitBranchCustomer: function () {
            var that = this,
                controls = that.getControls(),
                oCustomer = Session.DATACUSTOMER,
                model = {};
            model.strIdSession = Session.IDSESSION;
            model.strCustomerId = oCustomer.CustomerID;

            var myUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetBranchCustomer";
            $.ajax({
                url: myUrl,
                data: JSON.stringify(model),
                type: 'POST',
                contentType: "application/json charset=utf-8;",
                dataType: "json",
                success: function (response) {

                    controls.cboBranchCustomer.html("");
                    if (response.data != null) {
                        $.each(response.data, function (index, value) {

                            controls.cboBranchCustomer.append($('<option>', { value: value.CODSOLOT, html: value.SUCURSAL }));
                        });
                    }
                },
                error: function (request, status, error) {
                    //console.logerror);
                    $.unblockUI();
                    alert(error, "Alerta");
                }
            });
        },
        InitCacDat: function () {
            var that = this,
                controls = that.getControls(),
                objCacDacType = {};

            var parameters = {};
            parameters.strIdSession = Session.IDSESSION;
            parameters.strCodeUser = Session.USERACCESS.login;
            objCacDacType.strIdSession = Session.IDSESSION;
            
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(parameters),
                url: window.location.protocol + '//' + window.location.host + '/Transactions/CommonServices/GetUsers',
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
            //    url: '/Transactions/CommonServices/GetCacDacType',
            //    success: function (response) {
            //        controls.cboCacDac.html("");
            //        controls.cboCacDac.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            //        if (response.data != null) {
            //            $.each(response.data.CacDacTypes, function (index, value) {

            //                controls.cboCacDac.append($('<option>', { value: value.Code, html: value.Description }));
            //            });
            //        }
            //    }
            //});
        },
        InitDataLine: function () {
            var that = this,
              oCustomer = Session.DATACUSTOMER,
              controls = this.getControls();

            var model = {};
            model.strIdSession = Session.IDSESSION;
            model.vContratId = oCustomer.ContractID;

            var bResult = false;

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetDataLine",
                async: false,
                success: function (response) {

                    var bool = false;

                    if (response != null) {                     
                        if (response.data.strflag == '1')
                        {
                            bool = true;
                        }
                    }
                    bResult = bool;                   
                }
            });

            return bResult;

        },
        Validate_Services : function(){
            var that = this,
                   controls = that.getControls(),
                   oCustomer = Session.DATACUSTOMER,
                   oUserAccess = Session.USERACCESS,
                   oDataService = Session.DATASERVICE;

            var model = {};
            model.strCoId = oCustomer.ContractID;
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetCommercialService",
                success: function (response) {
                     
                    if (response != null) {
                            
                        $.each(response, function (i, r) {
                            if (r.ESTADO == "") {
                                alert("El estado del servicio no se encuentra definido.", "Alerta", function () { that.Close_Click(); });
                                return;
                            }
                        });
                        
                    }
                }
            });

        },
        InitPluginGrid: function () {
            var that = this,
               controls = that.getControls();

            tbConfigurationIP = controls.tbConfigurationIP.DataTable({
                info: false,
                select: "single",
                paging: false,
                searching: false,
                scrollX: true,
                scrollY: 300,
                scrollCollapse: true,
                destroy: true,
                language: {
                    lengthMenu: "Mostrar _MENU_ registros por página.",
                    zeroRecords: "No existen datos",
                    info: " ",
                    infoEmpty: " ",
                    infoFiltered: "(filtered from _MAX_ total records)"
                }
            });

            that.GetProductDetailt();
        },
        GetProductDetailt: function () {
            var that = this,
              controls = that.getControls(),
              strUrl = '',
              oCustomer = Session.DATACUSTOMER,
              model = {};



            model.strIdSession = Session.IDSESSION;
            model.vCustomerId = oCustomer.CustomerID;
            model.vContratId = oCustomer.ContractID;
            tbConfigurationIP.clear().draw();

            strUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetProductDetailt";
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                data: JSON.stringify(model),
                url: strUrl,
                error: function (data) {
                    alert("Error JS : en llamar al GetProductDetailt.", "Alerta");
                    $.unblockUI();
                },
                success: function (response) {
                    if (response.data != null) {
                        if (response.data.length > 0) {
                            $.each(response.data, function (i, r) {
                                tbConfigurationIP.row.add([
                                    r.codigo_material
                                  , r.codigo_sap
                                  , r.numero_serie
                                  , r.macadress
                                  , r.descripcion_material
                                  , r.tipo_equipo
                                  , r.id_producto
                                  , r.modelo
                                  , r.convertertype
                                  , r.servicio_principal
                                  , r.headend
                                  , r.ephomeexchange
                                  , r.numero
                                ]).draw(false);
                            });


                        }
                    }



                }
            });

        },

        f_Loading: function () {
            var that = this,
             controls = that.getControls();


            $.blockUI({
                message: controls.ModalLoading,
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
        f_ValidateMail: function () {
            var that = this,
             controls = that.getControls();

            if ($.trim(controls.txt_SendMail.val()) == '') {
                if (document.getElementById('chk_SendMail').checked == true) {

                    alert(Session.MESSAGEHFC.Message1, "Alerta");
                    return false;
                } else {
                    return true;
                }
            } else {
                var regx = /^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$/;
                var blvalidar = regx.test(document.getElementById('txt_SendMail').value);
                if (blvalidar == false) {

                    alert(Session.MESSAGEHFC.Message2, "Alerta");
                    document.getElementById('txt_SendMail').focus();
                    return false;
                } else { return true; }
            }

        },
        f_ValidateLoadPase: function () {
            var that = this,
             controls = that.getControls(),
             strUrl = '',
             oHidden = Session.HIDDEN,
             oCustomer = Session.DATACUSTOMER,
             oDataLine = Session.DATLINE,
             oUSERACCESS = Session.USERACCESS;

            if (oCustomer.ContractID == null) {

                parent.window.close();
            }
            if (oCustomer.ContractID === 'undefined') {

                parent.window.close();
            }
            if (oDataLine.telefonia == "F") {
                alert("No tiene servicio de Internet.", "Alerta");
                parent.window.close();
            }

            if (oUSERACCESS.userId == null && oCustomer==null) {
                parent.window.close();
            }
            if (oUSERACCESS.userId === 'undefined' && oCustomer == null) {

                parent.window.close();
            }
            
            //undefined

        },

        EmailActive_Click: function () {
            var that = this,
            controls = that.getControls();
            var objEnviarEmail = document.getElementById(controls.chk_SendMail.attr("id"));
            if (objEnviarEmail.checked == true) {
                document.getElementById(controls.txt_SendMail.attr("id")).style.display = '';

            } else {
                document.getElementById(controls.txt_SendMail.attr("id")).style.display = 'none';
            }
        },
        f_ActiveFidelidad: function () {
            var that = this,
            controls = that.getControls();
            var chkFidelidad = document.getElementById(controls.chk_Fidelidad.attr("id"));
            if (chkFidelidad.checked == true) {
                controls.txt_monto.val("0.00");
            } else {
                controls.txt_monto.val(ConfigurationIPModel.COSTO_INSTALACION);
            }
        },

        Save_Click: function () {
            var that = this,
             controls = that.getControls(),
             strUrl = '',
             oHidden = Session.HIDDEN,
             oCustomer = Session.DATACUSTOMER,
             oDataLine = Session.DATLINE,
             oDataService = Session.DATASERVICE,
             oUserAccess = Session.USERACCESS,
             model = {};

            if (that.f_ValidateMail()) {

                if (controls.cboCacDac.val() == "-1" || controls.cboCacDac.val() == "") {
                    alert(Session.MESSAGEHFC.Message8, "Alerta");
                    return false;
                }
                
                var strJobType = controls.cboJobTypes.val().split("+");

                if (strJobType[0] == "-1" || strJobType[0] == "") {
                    alert("Necesita seleccionar el tipo de trabajo.", "Alerta");
                    return false;
                }
               

                if (strJobType[1] == "1") {
                    if (controls.cboType.val() == "" || controls.cboType.val() == "-1") {
                        alert("No se ha encontrado un Tipo de Servicio.", "Alerta");
                        return false;
                    }
                }
                //else {
                //    alert("El tipo de trabajo no esta activo");
                //    return false;
                //}

               
                if (controls.cboBranchCustomer.val() == "" || controls.cboBranchCustomer.val() == "-1") {
                    alert("No se ha encontrado una sucursal.", "Alerta");
                    return false;
                }


                if (strJobType[0] == ConfigurationIPModel.IPFIJA) {
                    ConfigurationIPModel.MessageConfirmacion = ConfigurationIPModel.gConstKeyPreguntaGenConfIP_IPFIJA;
                }
                else {
                    if (strJobType[0] == ConfigurationIPModel.PUERTO25) {
                        ConfigurationIPModel.MessageConfirmacion = ConfigurationIPModel.gConstKeyPreguntaGenConfIP_PUERTO25;
                    }
                    else {
                        ConfigurationIPModel.MessageConfirmacion = "Está seguro de Grabar un CONFIGURACION DE SERVICIOS?";
                    }

                    ConfigurationIPModel.MessageConfirmacion = ConfigurationIPModel.gConstKeyPreguntaGenConfIP_PUERTO25;
                }


   

                confirm(ConfigurationIPModel.MessageConfirmacion, 'Confirmar', function (result) {

                    if (result == true) {
                        that.f_Loading();
                       
                        
                        model.IdSession = Session.IDSESSION;
                        model.strCustomerId = oCustomer.CustomerID;
                        model.strTelephone = oCustomer.Telephone;
                        //model.strModality = oCustomer.Modality
                        model.strModality = 'Presencial';
                        model.strUser = oUserAccess.login;
                        model.strName = oCustomer.Name;
                        model.strLastName = oCustomer.LastName;
                        model.strFirtName = oCustomer.Name;
                        model.strNameComplete = oCustomer.FullName;
                        model.strBusinessName = oCustomer.BusinessName;
                        model.strDocumentType = oCustomer.DocumentType;
                        model.strDocumentNumber = oCustomer.DocumentNumber;
                        model.strAddress = controls.lblAddress.text();
                        model.strDistrict = oCustomer.District;
                        model.strDepartament = oCustomer.Departament;
                        model.strProvince = oCustomer.Province;
                        model.strFullName = oCustomer.FullName;

                        model.strLegalDepartament = oCustomer.LegalDepartament;
                        model.strLegalProvince = oCustomer.LegalProvince;
                        model.strLegalDistrict = oCustomer.LegalDistrict;
                         
                        model.strLegalBuilding = oCustomer.LegalUrbanization;
                        model.strLegalRepresentation = oCustomer.LegalRepresentation;
                        //model.strTypeDoc = oCustomer.DocumentType;
                        //strNroDoc = oCustomer.DocumentNumber;

                        model.strContractId = oCustomer.ContractID;
                        model.strAddressInst = controls.lblAddress.text();
                        model.strUbigeoInst = oCustomer.InstallUbigeo;
                        model.strCodePlanInst = oCustomer.CodeCenterPopulate;
                        model.strPlan = oDataService.Plan;
    
                        //model.strTransaction = "ConfiguracionIP";
                        //model.CodeTipification = "ConfiguracionIP";

                        model.strJobTypes = strJobType[0];
                        model.strDescJobType = $('#cboJobTypes option:selected').text();
                        model.strMotiveSot = controls.cboMotivoSot.val();
                        model.strDescMotiveSot = $('#cboMotivoSot option:selected').text();
                        model.strServices = controls.cboType.val();
                        model.strDescServices = $('#cboType option:selected').text();

                        model.strNote = controls.txtNote.val();
                        model.strEmail = controls.txt_SendMail.val();
                        model.strSn = oHidden.hdnServName;
                        model.strIpServidor = oHidden.hdnLocalAdd;

                        model.strCacDac = controls.cboCacDac.val();
                        model.strDescCacDac = $('#ddlCACDAC option:selected').text();

                        if (document.getElementById('chk_SendMail').checked == true) {
                            model.bSendMail = true;
                        }
                        else {
                            model.bSendMail = false;
                        }

                        model.strBranchCustomer = controls.cboBranchCustomer.val();
                        model.strDescBranchCustomer = $('#cboBranchCustomer option:selected').text();

                        model.strTipoTransaccion = $('#cboJobTypes option:selected').text();
                       
                        model.strMsisdn = oDataService.MSISDN;
                       // model.strAccount = oCustomer.Account;//"7.1828917";
                       // model.strContactCode = oCustomer.ContactCode;
                        model.strFLAG_REGISTRADO  = 1;
                        model.strSiteCode = oCustomer.SiteCode;
                        model.strCodSolot = controls.cboBranchCustomer.val();
                        model.strBIRTHDAY = oCustomer.BirthDate;
                        //model.strCodinssrv = "277783";
                        model.strFormatConstancy =  ConfigurationIPModel.ConstanceXml;

                        strUrl = window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/ConfigurationIPSave";

                        $.app.ajax({
                            type: 'POST',
                            contentType: "application/json; charset=utf-8",
                            dataType: 'json',
                            data: JSON.stringify(model),
                            url: strUrl,
                            error: function () {
                                alert("No se pudo ejecutar la transacción. Informe o vuelva a intentar.", "Alerta");
                            },
                            success: function (response) {                             
                                try {
                                    if (response.oResponseStatus.strUbicacionError != null) {                                       
                                        if (response.oResponseStatus.strCodigoRespuesta == "0") {

                                            alert(response.strDescripcionRespuesta, "Informativo");

                                            controls.btnSave.prop("disabled", true);
                                            controls.btnConstancia.prop("disabled", false);
                                            controls.lblHCFNroSot.text(response.strNroSOT);

                                            ConfigurationIPModel.strRutaPdf = response.strRutaConstacy;
                                            ConfigurationIPModel.strNroSOT = response.strNroSOT;
                                            ConfigurationIPModel.strCaseId = response.strIdInteraccion;

                                            try {
                                                if (response.strFileName != null) {
                                                    ConfigurationIPModel.strFileName = response.strFileName;
                                                }
                                            } catch (e) {

                                            }
                                        }
                                        else {
                                            alert(response.oResponseStatus.strUbicacionError, "Alerta");

                                            controls.btnSave.prop("disabled", false);
                                            controls.btnConstancia.prop("disabled", true);
                                        }
                                    }
                                    else {
                                        controls.btnSave.prop("disabled", false);
                                        alert("No se pudo ejecutar la transacción. Informe o vuelva a intentar.", "Alerta");
                                    }                                 
                                } catch (e) {

                                }                              
                            }
                       
                        });

                    }
                });
                
            }

        },
        Close_Click: function () {
            parent.window.close();
        },
        Constacia_Click: function () {
            var that = this,
            controls = that.getControls(),
            strUrl = '',
            oHidden = Session.HIDDEN,
            oCustomer = Session.DATACUSTOMER,
            oDataLine = Session.DATLINE,
            oDataService = Session.DATASERVICE,
            oUserAccess = Session.USERACCESS;
 
            //that.strPDFRoute = "output//POSTVENTA//POSTVENTA_TRASLADO_INTERNO_EXTERNO_DTH//1002030_25_10_2016_POSTVENTA_TRASLADO_INTERNO_EXTERNO_DTH_0.pdf
            if (ConfigurationIPModel.strRutaPdf != "") {

                var newRoute = ConfigurationIPModel.strRutaPdf.substring(ConfigurationIPModel.strRutaPdf.indexOf('SIACUNICO'));
                newRoute = newRoute.replace(new RegExp('/', 'g'), '\\');
                newRoute = ConfigurationIPModel.strServidorLeerPDF + newRoute;
                ReadRecordSharedFile(Session.IDSESSION, newRoute);
            }
            else {
                alert('Error para generar la constancia de atención.', "Alerta");
            }

           

        },
        JobType_Change: function () {
            var that = this,
                controls = that.getControls(),
                model = {};

            var strJobType = controls.cboJobTypes.val().split("+");

         
            if (strJobType[0] == ConfigurationIPModel.IPFIJA) {
                if (!that.InitDataLine()) {
                    alert(ConfigurationIPModel.gConstKeyPlanActualCliente);
                    controls.btnSave.prop("disabled", true);
                    return;
                }
                else {
                    controls.btnSave.prop("disabled", false);
                }
            }
            else {
                controls.btnSave.prop("disabled", false);
            }


            if (strJobType[1] == 1) {
                that.InitTypeConfIp(strJobType[0]);
            }
            else {
                controls.cboType.html("");
                controls.cboType.append($('<option>', { value: '-1', html: 'Seleccionar' }));
            }
            
            that.GetConstanceTemplateLabels();
        },
        cboCacDac_Change: function () {
            var that = this,
               controls = that.getControls();

            if (controls.cboCacDac.val() != "" || controls.cboCacDac.val() != "-1" || controls.cboCacDac.val() != null) {
                that.GetConstanceTemplateLabels();
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

        GetConstanceTemplateLabels: function () {
            var that = this;
            var controls = that.getControls();
            that.CreateValueList();
            $.ajax({
                type: "POST",
                url: window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/GetConstanceTemplateLabels",
                data: {},
                //async: false,
                error: function (xhr, status, error) { },
                success: function (data) {
                    var data = data.data;
                    that.CreateValueList();
                    that.CreateDictionaryForXmlReplace(data);

                    data: { dict: that.dict }
                    $.ajax({
                        type: 'POST',
                        url: window.location.protocol + '//' + window.location.host + "/Transactions/HFC/ConfigurationIP/CreateConstanceXmlString",
                        data: that.dict,
                        //async: false,
                        error: function (xhr, status, error) { },
                        success: function (data) {
                            
                            ConfigurationIPModel.ConstanceXml = data.data;
                        }
                    });
                }
            });
        },
        CreateDictionaryForXmlReplace: function (data) {
            var that = this;
            var controls = that.getControls();
            $.each(data, function (index, item) {
 
                that.dict['[' + index + '].Key'] = item;
                that.dict['[' + index + '].Value'] = that.dictValues[index];
            });
        },
        CreateValueList: function () {
            var that = this,
            controls = that.getControls(),
            strUrl = '',
            oHidden = Session.HIDDEN,
            oCustomer = Session.DATACUSTOMER,
            oDataLine = Session.DATLINE,
            oDataService = Session.DATASERVICE,
            oUserAccess = Session.USERACCESS;
            that.dictValues = [];

            that.dictValues.push("CONFIGURACION_IP"),//0 FORMATO_TRANSACCION
            that.dictValues.push($('#ddlCACDAC option:selected').text());//01 CENTRO_ATENCION_AREA
            that.dictValues.push(oCustomer.BusinessName);//03 TITULAR_CLIENTE
            that.dictValues.push(oCustomer.LegalAgent);//02 REPRES_LEGAL
            that.dictValues.push(oCustomer.DocumentType);//04 TIPO_DOC_IDENTIDAD
            that.dictValues.push(oCustomer.DocumentNumber);//10 NRO_DOC_IDENTIDAD
            that.dictValues.push(moment(new Date()).format('DD/MM/YYYY'));//07 FECHA_TRANSACCION_PROGRAM dia de hoy
            that.dictValues.push("{interaccion}");//08 CASO_INTER
            that.dictValues.push(oCustomer.ContractID);//05 CONTRATO
            that.dictValues.push($('#cboJobTypes option:selected').text())//06 TRANSACCION DESCRIPCION
            that.dictValues.push("0.00");//17 MONTO_PCS va en blanco
            that.dictValues.push(controls.lblAddress.text());//09  
            that.dictValues.push(controls.lblAddressNote.text());//11 ID_CU_ID
            that.dictValues.push(controls.lblDepartamento.text());//12 ESCENARIO_MIGRACION
            that.dictValues.push(controls.lblDistrito.text());//13 NUEVO_PLAN
            that.dictValues.push(controls.lblCodePlans.text());//14 CF_TOTAL_NUEVO
            that.dictValues.push(controls.lblPais.text());//15 FECHA_EJECUCION fecha de compromiso en agendamiento
            that.dictValues.push(controls.lblProvincia.text());//16 MONTO_REINTEGRO
            that.dictValues.push(ConfigurationIPModel.CONTENIDO_COMERCIAL2);//18 TOPE_CONSUMO  va en blanco
            that.dictValues.push("{nrosot}");//18 TOPE_CONSUMO  va en blanco
        },

        dict: {},
        dictValues: [],
        strUrl: (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).substring(0,
            (window.location.href.substring(0, window.location.href.lastIndexOf('/'))).lastIndexOf('/'))
    };


    $.fn.HFCConfigurationIP = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCConfigurationIP'),
                options = $.extend({}, $.fn.HFCConfigurationIP.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCConfigurationIP', data);
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

     
    $.fn.HFCConfigurationIP.defaults = {
    }
    $('#divBody').HFCConfigurationIP();



})(jQuery);
