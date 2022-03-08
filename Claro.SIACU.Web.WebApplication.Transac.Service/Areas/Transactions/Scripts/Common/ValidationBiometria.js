window.addEventListener('message', function (e) {
    //alert('validacion: ' + e.data);
    window.opener.console.log('mensaje bio');
    window.opener.console.log(e);
    //if (e.origin != Session.domainBiometria) return false;
    //debugger;
    try {
        window.removeEventListener('message', function () { });
        var origin = (typeof window.location.origin == 'undefined' || window.location.origin == null || window.location.origin == '' ? '*' : window.location.origin);
        window.opener.postMessage(e.data, origin);
        //window.opener.objson.Response=e.data;
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


        window.opener.$.ValidationBiometrica.getValues();     
        $('#ifrmBiometria').attr('src', Session.ACCION);       
        $('#divBody div.transaction-header').attr('style', 'display:none;');
    });

  

})(jQuery, null);