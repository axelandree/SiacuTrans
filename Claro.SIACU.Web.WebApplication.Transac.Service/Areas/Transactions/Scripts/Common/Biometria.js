var winBiometria;
var winBiometriaPre;
(function () {
    $.fn.extend($, {
        ValidationBiometrica: {
            getValues: function () {
                //debugger;
                winBiometria.Session.ACCION = Session.UrlRedirecStr;
                winBiometria.dialogArguments = (typeof Session.objBiometria != 'undefined' && Session.objBiometria != null ? Session.objBiometria : null);
                winBiometria.returnvalue = null;
                var origenBiometria = '';
                if (typeof Session.domainBiometria != 'undefined' && Session.domainBiometria != null && Session.domainBiometria != '') {
                    origenBiometria = Session.domainBiometria;
                }
                winBiometria.Session.domainBiometria = origenBiometria;
                winBiometria.Session.objBiometria = Session.objBiometria;
            },
            getValidationJsonBiometria: function () {
                var obj = sessionStorage.getItem('jsonBiometria');
                if (typeof obj == 'undefined' || obj == null || obj == '') {
                    obj = new Object();
                } else {
                    obj = JSON.parse(obj);
                }
                if (typeof Session.objBiometria == 'undefined' || Session.objBiometria == null) {
                    if (typeof obj != 'undefined' && obj != null) {
                    Session.objBiometria = obj;
                    } else {
                        Session.objBiometria = new  Object();
                    }
                }
            },
            getConfigBiometria: function () {
                var js = this.getJsonBiometria();
                js.Request = "{strIdSession:'" + Session.IDSESSION + "', strCodUsuario:'" + js.CodigoUsuario + "',strCodTipoCAC:'" + js.CodigoTipoCAC + "',strCodCAC:'" + js.CodigoCAC + "',strIdPadre:'" + js.IdPadre + "',strNumDoc:'" + js.NumeroDocumento + "',strTipoDoc:'" + js.TipoDocumento + "',strCanalPermitido:'" + js.NombreTipoCAC + "'}";
                Session.objBiometria.Request = js.Request;
                if (typeof Session.objBiometria.ConfiguracionBiometrica == 'undefined' || Session.objBiometria.ConfiguracionBiometrica == null) {

                    $.ajax({
                        async: false,
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        data: js.Request,
                        url: '/Transactions/Common/getConfigBiometria',
                        error: function (xhr, status, error) {
                            alert('Error al Obtener Configuracion Biometrica');
                        },
                        success: function (response) {
                            if (typeof response.data.data != 'undefined' && typeof response.data.data != null && response.data.data.length > 0) {
                                Session.objBiometria.ConfiguracionBiometrica = {};
                                Session.objBiometria.ConfiguracionBiometrica = response.data.data[0];
                            }
                            Session.jsonConfigBiometria = response.data; 
                            sessionStorage.setItem('jsonBiometria', JSON.stringify(Session.objBiometria));
                        }
                    });
                }
            },
            init: function (fn_succes, fn_closed, obj) {
                var that = this,
                 controls = this.getControls();
             
                that.loadSessionData();

                var IsPrepaId = !(typeof obj == 'undefined' || obj == null || obj == '');
                var action = '';
                if (!IsPrepaId) {
                    this.getConfigBiometria();
                    this.getSessionBiometriaPostpaid();
                    $.redirectApp.GetParamsData("SU_ACP_REA_QUI", "POSTPAGO");
                    action = '/Transactions/Common/ValidationBiometria';
                } else {
                    this.getSessionBiometriaPrepaid(obj)
                    Session.objBiometria = { IsSIACU: true };
                    this.getMsjBiometria();
                    $.redirectApp.GetParamsData("SU_SIACA_REA_QU", "PREPAGO");
                    action = '/Transactions/Common/ValidationBiometriaPrepaid';
                }
                var width = 750;
                var height = 700;
                var left = ((screen.width - parseInt(width)) / 2);
                var top = ((screen.height - parseInt(height)) / 2);
                var opc = 'location=si,scrollbars=yes,titlebar=no,resizable=yes,toolbar=no, menubar=yes,width=' + width + ',height=' + height;

                Session.objBiometria.IsRedirect = true;
                var strurl = window.location.protocol + '//' + window.location.host;
                strurl = strurl + action;
                winBiometria = window.open(strurl, '_Biometria', opc);
                $.blockUI({
                    message: '<div align="center"><img src="../../Images/loading2.gif"  width="25" height="25" /> Cargando .... </div>',
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
                $(winBiometria).on('blur',function (e) { winBiometria.focus(); });
                var Isclosed = false;
                var message = null;
                var fn = function (e) {
                    console.log('message biometria');
                    console.log(e);
                    var origin = (typeof window.location.origin == 'undefined' || window.location.origin == null || window.location.origin == '' ? Session.domainBiometria : window.location.origin);
                    if (e.origin != origin) {
                        $.unblockUI();
                        return false;
                    }
                    if (message == null) message = e.data;
                    window.removeEventListener("message", fn);
                    if (typeof Session.IsSendMsj == 'undefined' || Session.IsSendMsj == null) {
                        Session.IsSendMsj = true;
                    } else { Session.IsSendMsj = false; }
                    if (typeof message == 'undefined' || message == null || message == '') {
                        Isclosed = false;
                    } else {
                        Isclosed = true;
                        if (message != null && message != '' && Session.IsSendMsj == true) {
                        fn_succes(message);
                            $.unblockUI();
                            Session.IsSendMsj = null;
                            Session.objBiometria = null;
                        }
                    }
                    sessionStorage.removeItem('jsonBiometria');
                };
                window.removeEventListener("message", fn);
                window.addEventListener('message', fn, false);
                var timer = setInterval(function () {
                    if (typeof winBiometria != 'undefined' && winBiometria != null) {
                        try {
                        if (winBiometria.closed) {
                            clearInterval(timer);
                                if (!Isclosed && $.isFunction(fn_closed)) {
                                    fn_closed();
                                    $.unblockUI();
                                }
                        }
                        } catch (ex) {
                        }
                      
                    }
                }, 1000);

            },
            loadSessionData: function () {
                var controls = this.getControls();
                var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
                Session.IDSESSION = SessionTransac.UrlParams.IDSESSION;
            },
            getMsjBiometria: function () {
                $.ajax({
                    async: false,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Common/getMsjBiometria',
                    error: function (xhr, status, error) {
                        console.log('Error al Obtener Mensajes de Biometria');
                    },
                    success: function (response) {
                        Session.objBiometria.strMensajeValidacionBiometrica1 = response.data.strMensajeValidacionBiometrica1;
                        Session.objBiometria.strMensajeValidacionBiometrica2 = response.data.strMensajeValidacionBiometrica2;
                        Session.objBiometria.strMensajeValidacionBiometrica3 = response.data.strMensajeValidacionBiometrica3;
                        Session.objBiometria.strMensajeValidacionBiometrica4 = response.data.strMensajeValidacionBiometrica4;
                        Session.objBiometria.strMensajeValidacionBiometricaMenos2 = response.data.strMensajeValidacionBiometricaMenos2;
                        Session.objBiometria.strMensajeValidacionBiometricaMenos4 = response.data.strMensajeValidacionBiometricaMenos4;
                        Session.objBiometria.strMensajeValidacionBiometricaMenos5 = response.data.strMensajeValidacionBiometricaMenos5;
                        Session.objBiometria.strMensajeValidacionBiometrica0 = response.data.strMensajeValidacionBiometrica0;
                        Session.objBiometria.strMensajeValidacionBiometricaOtros = response.data.strMensajeValidacionBiometricaOtros;
                        Session.objBiometria.Url = response.data.Url;
                        Session.objBiometria.objValoresBiometria = {
                            TipDocAutorizado: response.data.strCodigoTipoDocumentoDNIValidacionBiometrica
                        };
                        Session.domainBiometria = response.data.strDomainBiometria;
                    }
                });
            },
            getJsonBiometria: function () {
                var objInfoBioTrazabilidad = new Object();
                var that = this;
                var strurl = window.location.protocol + '//' + window.location.host;
                var parameters = {};
                var requestSession = JSON.parse(sessionStorage.getItem("SessionTransac"));
                this.getValidationJsonBiometria();
                parameters.strIdSession = requestSession.SessionParams.USERACCESS.userId;
                parameters.strCodeUser = requestSession.SessionParams.USERACCESS.login;
                $.ajax({
                    async: false,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    data: JSON.stringify(parameters),
                    url: '/Transactions/CommonServices/GetUsers',
                    error: function (xhr, status, error) {
                        console.log(xhr);
                    },
                    success: function (results) {
                        Session.objBiometria.IdPadre = '0';
                        Session.objBiometria.CodigoTipoCAC = results.CodeTypeCac;
                        Session.objBiometria.CodigoCAC = results.CodeCac;
                        Session.objBiometria.CodigoUsuario = results.CodeUser;
                        Session.objBiometria.NombreTipoCAC = results.TypeCac;
                        Session.objBiometria.NombreCAC = results.Cac;
                        Session.objBiometria.CanalPermitido = results.TypeCac;
                        Session.objBiometria.FlagResultado = false;
                        Session.objBiometria.MotivoVal = '';
                        that.getMsjBiometria();
                    }
                });
                return Session.objBiometria;
            },
            getSessionBiometriaPostpaid: function () {
                var obj = Session.objBiometria;
                var jsSession = JSON.parse(sessionStorage.getItem("SessionTransac"));
                if (typeof Session.Parameters == 'undefined' || Session.Parameters == null) {
                    Session.Parameters = jsSession;
                }

                if (typeof Session.Parameters.UrlParams == 'undefined' || Session.Parameters.UrlParams == null) {
                    Session.Parameters.UrlParams = new Object();
                    Session.Parameters.UrlParams = {};
                }
                Session.Parameters.UrlParams.pstrTransaccionOrigen = obj.TransaccionOrigen;
                Session.Parameters.UrlParams.PSTRFLAGOTRAPERSONA = obj.FlagOtraPersona;
                Session.Parameters.UrlParams.TELEFONO = obj.Telefono;
                Session.Parameters.SessionParams.DATASERVICE.NumberCellPhone = obj.Telefono;
                Session.Parameters.SessionParams.DATASERVICE.NumberDocument = obj.NumeroDocumento;
                Session.Parameters.SessionParams.DATACUSTOMER.Name = obj.Nombres;
                Session.Parameters.SessionParams.DATACUSTOMER.LastName = obj.Apellidos;
                Session.Parameters.UrlParams.NRODOC = obj.NumeroDocumento;
            },
            getSessionBiometriaPrepaid: function (obj) {
                var jsSession = JSON.parse(sessionStorage.getItem("SessionTransac"));
                if (typeof Session.Parameters == 'undefined' || Session.Parameters == null) {
                    Session.Parameters = jsSession;
                }

                if (typeof Session.Parameters.UrlParams == 'undefined' || Session.Parameters.UrlParams == null) {
                    Session.Parameters.UrlParams = new Object();
                    Session.Parameters.UrlParams = {};
                }
                Session.Parameters.UrlParams.PSTRTRANSACCIONORIGEN = obj.TransaccionOrigen;
                Session.Parameters.UrlParams.PSTRFLAGOTRAPERSONA = obj.FlagOtraPersona;
                Session.Parameters.UrlParams.TELEFONO = obj.Telefono;
                Session.Parameters.UrlParams.PSTRDC = obj.NumeroDocumento;
                Session.Parameters.UrlParams.PSTRNC = obj.Nombres;
                Session.Parameters.UrlParams.PSTRAC = obj.Apellidos;
                Session.Parameters.SessionParams.DATASERVICE.NumberCellPhone = obj.Telefono;
                Session.Parameters.SessionParams.DATACUSTOMER.NumberDocument = obj.NumeroDocumento;
                Session.Parameters.UrlParams.NRODOC = obj.NumeroDocumento;
            },
            initBioPrepaid: function (obj, fn_succes, fn_closed) {
                this.getSessionBiometriaPrepaid(obj)
                Session.objBiometria = { IsSIACU: true };
                this.getMsjBiometria();
                $.redirectApp.GetParamsData("SU_SIACA_REA_QU", "PREPAGO");
                var opc = 'width:' + "380" + 'px;height:' + "500" + 'px;resizable=no,menubar=no';
                var strurl = window.location.protocol + '//' + window.location.host;
                strurl = strurl + '/Transactions/Common/ValidationBiometriaPrepaid';
                winBiometriaPre = window.open(strurl, '_BiometriaPre', opc);
                var Isclosed = false;
                var message = null;
                var fn = function (e) {
                    var origin = (typeof window.location.origin == 'undefined' || window.location.origin == null || window.location.origin == '' ? Session.domainBiometria : window.location.origin);
                    if (e.origin != origin) return false;
                    message = e.data;
                    if (typeof message == 'undefined' || message == null || message == '') {
                        Isclosed = false;
                    } else {
                        Isclosed = true;
                        fn_succes(message);
                    }
                    sessionStorage.removeItem('jsonBiometriaPre');
                };
                window.removeEventListener("message", fn);
                window.addEventListener('message', fn, false);
                var timer = setInterval(function () {
                    if (typeof winBiometriaPre != 'undefined' && winBiometriaPre != null) {
                        try {
                            if (winBiometriaPre.closed) {
                                clearInterval(timer);
                                if (!Isclosed && $.isFunction(fn_closed)) {
                                    fn_closed();
                                }

                            }
                        } catch (ex) {
                        }

                    }
                }, 1000);
            },
            getControls: function () {
                return this.m_controls || {};
            },
            setControls: function (value) {
                this.m_controls = value;
            },
        }
    });

})();
