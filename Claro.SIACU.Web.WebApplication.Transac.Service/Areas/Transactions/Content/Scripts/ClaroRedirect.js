var objSessionsParams = {};

(function ($, undefined) {

    var w = window.innerWidth;
    var h = window.innerHeight;
    var leftScreen = (screen.width - w) / 2;
    var topScreen = (screen.height - h) / 2;
    var specs = "location=no,status=no,toolbar=no,directories=no,menubar=no,scrollbars=yes,resizable=yes,width=" + w + ",height=" + h + ",left=" + leftScreen + ",top=" + topScreen;

    var Redirect = function ($element, options) {

    }

    Redirect.prototype = {
        constructor: Redirect,
        init: function () {
            var that = this,
                controls = this.getControls();

            Session.ORIGINTYPE = '';
            that.render();
        },
        GetParamsData: function (option, targetApp) {
            Session.OPTIONREDIRECT = option;
            SetDataRedirect(Session);
            var urlGetParam = window.location.href + "/Redirect/GetSessionsParams";
            var objData = {};
            $.app.ajax({

                type: 'POST',
                url: urlGetParam,
                data: { strIdSession: Session.IDSESSION, strOption: option, strApplication: targetApp },
                dataType: 'json',
                error: function (data, status) {
                    alert("La página no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.");
                },
                success: function (data) {
                    if (data.length > 0) {
                        objData = $.redirect.builtDataJson(data);
                        $.redirect.sendRedirectionData(targetApp, option, objData);
                    }
                    else {
                        alert("No se encontró la página.");
                    }
                },
            });
        },
        builtDataJson: function (objSessionsParams) {
            var objParameters = {};
            var objSessions = {};
            var objRegistro;
            var objDatos = {};
            var objSessionDataTemp = {};
            var property = {};
            var strPropiedades;
            var strJSONParamsSessions = {};

            for (var i = 0, j = objSessionsParams.length; i < j; i++) {
                objRegistro = objSessionsParams[i];

                objDatos = Session[objRegistro.session_name];
                if (objRegistro.option_type == 1) {//Sessions 
                    if (Session[objRegistro.session_name] != null && Session[objRegistro.session_name] != undefined) {
                        if ($.trim(objRegistro.prop_session).length > 0) {
                            strPropiedades = objRegistro.prop_session.split('|');
                            for (var x = 0, o = strPropiedades.length; x < o; x++) {
                                property = strPropiedades[x];
                                objSessionDataTemp[property] = objDatos[property];
                            }
                            objSessions[objRegistro.session_name] = objSessionDataTemp;
                            objSessionDataTemp = {};
                        } else {
                            objSessions[objRegistro.session_name] = Session[objRegistro.session_name];
                        }
                    }
                    
                } else {//Parameters URL 
                    if (Session[objRegistro.session_name] != null && Session[objRegistro.session_name] != undefined) {
                        objParameters[objRegistro.session_name] = Session[objRegistro.session_name];
                        //Asigna un valor al parametro de URL
                        if (objRegistro.value_Session != null) {
                            if (objRegistro.value_Session.length > 0) {
                                objParameters[objRegistro.session_name] = objRegistro.value_Session
                            }
                        }
                    }
                    else {
                        if (objRegistro.value_Session != null && objRegistro.value_Session != undefined) {
                            if (objRegistro.value_Session.length > 0) {
                                objParameters[objRegistro.session_name] = objRegistro.value_Session
                            }
                        }
                    }
                }
            }

            strJSONParamsSessions = " { \"UrlParams\" : " + ((Object.keys(objParameters).length > 0) ? JSON.stringify(objParameters) : "{}") + ", \"SessionParams\":" + ((Object.keys(objSessions).length > 0) ? JSON.stringify(objSessions) : "{}") + " }";
            return strJSONParamsSessions;
        },
        buildUrlRedirect: function (urlDest, jsonParameters) {
            var objparameters = JSON.parse(jsonParameters);
            var objurl = objparameters.UrlParams;
            var urlDest = urlDest.concat('?', Object.keys(objurl).map(function (key) {
                return encodeURIComponent(key) + '=' + encodeURIComponent(objurl[key]);
            }).join('&'));
            return urlDest;
        },
        validateRedirectionCommunication: function (strSequence) {
           
            var urlRedirectVal = window.location.href + "/Redirect/ValidateRedirect";

            $.app.ajax({

                type: 'POST',
                url: urlRedirectVal,
                data: { strIdSession: Session.IDSESSION, sequence: strSequence },
                dataType: 'json',
                error: function (data, status) {
                    alert("La página no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.");
                },
                success: function (data) {
                    if (data.length > 0) {
                        var urlDest = data[0];
                        var jsonParameters = data[2];
                        var urlDestExt = $.redirect.buildUrlRedirect(urlDest, jsonParameters);
                        window.open(urlDestExt, '', specs).focus();
                        

                    }
                    else {
                        alert("No se encontró la página.");
                    }
                },
            });
        },
        sendRedirectionData: function (targetApp, option, objData) {

           
            var urlRedirect = window.location.href + "/Redirect/RedirectApp";

            $.app.ajax({

                type: 'POST',
                url: urlRedirect,
                data: JSON.stringify({ strIdSession: Session.IDSESSION, strAppDest: targetApp, strOption: option, strData: objData }),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                async: true,
                cache: false,
                error: function (data, status) {
                    alert("No se pudo redireccionar a la página solicitada. Intente nuevamente en breves." + data.responseText);
                },
                success: function (data) {
                    if (data.length > 0) {
                        var url = data[0];
                        var sequence = data[1];
                        if (!url) {
                            $.redirect.validateRedirectionCommunication(sequence);
                        } else {
                            var dest = window.open(url + "?secuencia=" + sequence, '', specs);
                            dest.focus();
                        }
                    }
                    else {
                        alert("No se pudo redireccionar a la página solicitada. Intente nuevamente en breves." + data.responseText);
                    }
                },
            });
        }
    }
      $.fn.extend($, {
        redirect: new Redirect()
    });


})(jQuery, null);

