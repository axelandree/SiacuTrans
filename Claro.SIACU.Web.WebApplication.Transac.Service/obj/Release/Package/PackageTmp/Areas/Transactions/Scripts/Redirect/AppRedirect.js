var w = window.innerWidth;
var h = window.innerHeight;
var specs = "location=yes,status=yes,toolbar=yes,directories=yes,menubar=no,scrollbars=yes,resizable=yes,width=" + w + ",height=" + h + ",fullscreen=no";
var RedirectApp = function ($element, options) {

}

RedirectApp.prototype = {
    constructor: RedirectApp,
    init: function () {

    },
    getRoute: function () {
        return window.location.protocol + '//' + window.location.host;  //window.location.href;
    },
    GetParamsData: function (option, targetApp) {
        var urlGetParam = this.getRoute() + "/Transactions/Redirect/GetSessionsParams";
        var objData = {};
        $.app.ajax({
            async: false,
            type: 'POST',
            url: urlGetParam,
            data: { strIdSession: Session.IDSESSION, strOption: option, strApplication: targetApp },
            dataType: 'json',
            error: function (data, status) {
                alert("La página no se encuentra disponible en estos momentos. Vuelva intentarlo en breve.");
            },
            success: function (data) {
                if (data.length > 0) {
                    var jsSession = JSON.parse(sessionStorage.getItem("SessionTransac"));
                    if (typeof Session.Parameters == 'undefined' || Session.Parameters == null) {
                        Session.Parameters = jsSession;
                    }

                    objData = $.redirectApp.builtDataJson(data);
                    if (option == 'SU_ACP_REA_QUI' || option == 'SU_SIACA_NO_BIO' || option == 'SU_SIACA_REA_QU' || option == 'SU_ACP_NOT_BIO') {
                        objson = JSON.parse(objData);
                        objson.objBiometria = JSON.stringify(Session.objBiometria);
                        objData = JSON.stringify(objson);
                    }

                    $.redirectApp.sendRedirectionData(targetApp, option, objData);
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
        var Sessions = [];
        for (var i = 0, j = objSessionsParams.length; i < j; i++) {
            objRegistro = objSessionsParams[i];

            objDatos = Session.Parameters.SessionParams[objRegistro.session_name];
            if (objRegistro.option_type == 1) {//Sessions 
                if (Session.Parameters.SessionParams[objRegistro.session_name] != null && Session.Parameters.SessionParams[objRegistro.session_name] != undefined) {
                    if ($.trim(objRegistro.prop_session).length > 0) {
                        strPropiedades = objRegistro.prop_session.split('|');
                        for (var x = 0, o = strPropiedades.length; x < o; x++) {
                            property = strPropiedades[x];
                            objSessionDataTemp[property] = objDatos[property];
                        }
                        objSessions[objRegistro.session_name] = objSessionDataTemp;
                        objSessionDataTemp = {};
                    } else {
                        objSessions[objRegistro.session_name] = Session.Parameters.SessionParams[objRegistro.session_name];
                    }
                }

            } else {//Parameters URL 
                if (Session.Parameters.UrlParams[objRegistro.session_name] != null && Session.Parameters.UrlParams[objRegistro.session_name] != undefined) {
                    objParameters[objRegistro.session_name] = Session.Parameters.UrlParams[objRegistro.session_name];
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
        urlDest = urlDest.concat('?', Object.keys(objurl).map(function (key) {
            return encodeURIComponent(key) + '=' + encodeURIComponent(objurl[key]);
        }).join('&'));
        return urlDest;
    },
    validateRedirectionCommunication: function (strSequence) {
        var urlRedirectVal = this.getRoute() + "/Transactions/Redirect/GetRedirect";
        $.app.ajax({
            async: false,
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
                    var urlDestExt = $.redirectApp.buildUrlRedirect(urlDest, jsonParameters);
                    console.log(urlDestExt);
                    window.open(urlDestExt, '', specs);
                    window.moveTo(0, 0);
                    window.resizeTo(screen.availWidth, screen.availHeight);

                }
                else {
                    alert("No se encontró la página.");
                }
            },
        });
    },
    sendRedirectionData: function (targetApp, option, objData) {

        var urlRedirect = this.getRoute() + "/Transactions/Redirect/RedirectApp";
        $.app.ajax({

            type: 'POST',
            url: urlRedirect,
            data: JSON.stringify({ strIdSession: Session.IDSESSION, strAppDest: targetApp, strOption: option, strData: objData }),
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            async: false,
            cache: false,
            error: function (data, status) {
                alert("No se pudo redireccionar a la página solicitada. Intente nuevamente en breves." + data.responseText);
            },
            success: function (data) {
                if (data.length > 0) {
                    var url = data[0];
                    var sequence = data[1];
                    if (!url) {
                        $.redirectApp.validateRedirectionCommunication(sequence);
                    } else {
                        Session.UrlRedirecStr = url + "?secuencia=" + sequence;

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
    redirectApp: new RedirectApp()
});
