window.addEventListener('message', function (e) {
    //alert('validacion: ' + e.data);
    window.opener.console.log('mensaje bio pre');
    window.opener.console.log(e);
    //if (e.origin != Session.domainBiometria) return false;
    //debugger;
    try {
        var origin = (typeof window.location.origin == 'undefined' || window.location.origin == null || window.location.origin == '' ? '*' : window.location.origin);
        window.opener.postMessage(e.data, origin);
        //window.opener.objson.Response = e.data;
    } catch (ex) {
        window.opener.console.log('error reenvio');
        window.opener.console.log(ex);
    }

    //window.returnValue = e.data;
    //window.close();
});
(function ($, undefined) {
    'use strict';

    $(document).ready(function () {

        //var timer = setInterval(function () {
        //    cantidad = prompt("Ingrese la cantidad de representantes legales", "3");
        //    if (typeof cantidad != 'undefined' && cantidad != null) {
        //        if (parseInt(cantidad) > 0) clearInterval(timer);
        //    }
        //}, 1000);
        //window.opener.$.ValidationBiometrica.getValues();
        var url = '';
        if (typeof window.opener.Session.UrlRedirecStr != 'undefined' && window.opener.Session.UrlRedirecStr != null && window.opener.Session.UrlRedirecStr != '') {
            url = window.opener.Session.UrlRedirecStr;
        }
       $('#ifrmBiometriaPre').attr('src', url);
        $('#divBody div.transaction-header').attr('style', 'display:none;');
    });



})(jQuery, null);