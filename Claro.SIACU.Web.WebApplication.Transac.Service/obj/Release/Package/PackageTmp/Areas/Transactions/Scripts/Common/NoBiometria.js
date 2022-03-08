var winNoBiometria;
(function () {
    $.fn.extend($, {
        ValidacionNoBiometria: {
          
            getSessionNoBiometria: function () {
                var obj=Session.objBiometria;
                var jsSession = JSON.parse(sessionStorage.getItem("SessionTransac"));
                if (typeof Session.Parameters == 'undefined' || Session.Parameters == null) {
                    Session.Parameters = jsSession;
                }

                if (typeof Session.Parameters.UrlParams == 'undefined' || Session.Parameters.UrlParams == null) {
                    Session.Parameters.UrlParams = new Object();
                    Session.Parameters.UrlParams = {};
                }
                Session.Parameters.UrlParams.numDoc = obj.NumeroDocumento;
                Session.Parameters.UrlParams.puntoVenta = (typeof obj.NombreTipoCAC=='undefined' || obj.NombreTipoCAC==null ? '' : obj.NombreTipoCAC);
                Session.Parameters.UrlParams.pstrTransaccionOrigen = obj.TransaccionOrigen;
                Session.Parameters.UrlParams.pstrFlagOtraPersona = obj.FlagOtraPersona;
                Session.Parameters.UrlParams.pstrEscenario = '';
                Session.Parameters.UrlParams.pstrDC = obj.NumeroDocumento;
                Session.Parameters.UrlParams.pstrNC = obj.objDatosSolicitante.Nombres;
                Session.Parameters.UrlParams.pstrAC = obj.objDatosSolicitante.Apellidos;
                Session.Parameters.UrlParams.rnd = (new Date()).getTime();
                Session.Parameters.UrlParams.MotivoVal = obj.MotivoVal;
                Session.Parameters.UrlParams.TELEFONO = obj.Telefono;
                Session.Parameters.SessionParams.DATASERVICE.NumberCellPhone = obj.Telefono;
                Session.objBiometria.Telefono = obj.Telefono;
            },
            getValues: function () {
                winNoBiometria.Session.ACCION = Session.UrlRedirecStr;
                winNoBiometria.dialogArguments = (typeof Session.objBiometria != 'undefined' && Session.objBiometria != null ? Session.objBiometria : null);
                winNoBiometria.returnvalue = null;
                var origenBiometria = '';
                if (typeof Session.domainBiometria != 'undefined' && Session.domainBiometria != null && Session.domainBiometria != '') {
                    origenBiometria = Session.domainBiometria;
                }
                winNoBiometria.Session.domainBiometria = origenBiometria;
                winNoBiometria.Session.objBiometria = Session.objBiometria;
            },
            getMsjBiometria: function () {
                $.ajax({
                    async: false,
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: 'json',
                    url: '/Transactions/Common/getMsjBiometria',
                    error: function (xhr, status, error) {
                        console.log(xhr);
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
            init: function (obj,fn_succes, fn_closed,IsPrepaid) {
                var action = '';
                if (Session.objBiometria == 'undefined' || Session.objBiometria == null || Session.objBiometria == '') {
                    Session.objBiometria = new Object();
                }
                if (Session.domainBiometria == 'undefined' || Session.domainBiometria == null || Session.domainBiometria=='')this.getMsjBiometria();
                if (typeof IsPrepaid != 'undefined' && IsPrepaid != null && IsPrepaid != '') {
                    if (IsPrepaid) {
                        Session.objBiometria.TransaccionOrigen = obj.TransaccionOrigen;
                        Session.objBiometria.MotivoVal = obj.MotivoVal;
                        this.getSessionNoBiometria();
                        $.redirectApp.GetParamsData("SU_SIACA_NO_BIO", "PREPAGO");
                        sessionStorage.setItem('redirect', Session.UrlRedirecStr);
                        action = '/Transactions/Common/ValidationNoBiometriaPrepaId';
                    }
                } else {
                $.ValidationBiometrica.getConfigBiometria();
                Session.objBiometria.TransaccionOrigen = obj.TransaccionOrigen;
                Session.objBiometria.MotivoVal = obj.MotivoVal;
                this.getSessionNoBiometria();
                $.redirectApp.GetParamsData("SU_ACP_NOT_BIO", "POSTPAGO");
                sessionStorage.setItem('redirect', Session.UrlRedirecStr);
                    action='/Transactions/Common/ValidationNoBiometria';
                }
                var width = 950;
                var height = 750;
                var left = ((screen.width - parseInt(width)) / 2);
                var top = ((screen.height - parseInt(height)) / 2);
                var opc = 'location=si,scrollbars=yes,titlebar=no,resizable=yes,toolbar=no, menubar=yes,width=' + width + ',height=' + height;
                Session.objBiometria.IsRedirect = true;
                var strurl = window.location.protocol + '//' + window.location.host;
                strurl = strurl + action;
                winNoBiometria = null;
                winNoBiometria = window.open(strurl, '_NoBiometria', opc);
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
                $(winNoBiometria).on('blur', function (e) { winNoBiometria.focus(); });
                var Isclosed = false;
                var message = null;
                var fn = function (e) {
                    console.log('message Nobiometria');
                    console.log(e);
                    var origin = (typeof window.location.origin == 'undefined' || window.location.origin == null || window.location.origin == '' ? Session.domainBiometria : window.location.origin);
                    if (e.origin != origin) {
                        $.unblockUI();
                        return false;
                    } 
                    window.removeEventListener("message", fn);
                    if (message == null) message = e.data;
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
                        } 
                        
                    }
                };
                window.removeEventListener("message", fn);
                window.addEventListener('message', fn, false);
                var timer4 = setInterval(function () {
                    if (typeof winNoBiometria != 'undefined' && winNoBiometria != null) {
                        
                        try {
                            if (winNoBiometria.closed) {
                                console.log('console.log(winNoBiometria)');
                                console.log(winNoBiometria);
                                clearInterval(timer4);
                                if (!Isclosed) {
                                    if ($.isFunction(fn_closed)) {
                                        fn_closed();
                                        $.unblockUI();
                                    }
                                }
                            }
                        }catch(ex){
                        }
                     
                    }
                }, 1000);

            }

        }
    });

})();
