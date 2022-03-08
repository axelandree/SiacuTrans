window.removeEventListener('message', null);
window.addEventListener('message', function (e) {
    //alert('validacion: ' + e.data);
    //if (e.origin != Session.domainBiometria) return false;
    //debugger;
    window.opener.console.log('resp no biometria');
    window.opener.console.log(e);
    try {
        window.opener.sessionStorage.setItem('responseNoBiometria', e.data);
        var origin =(typeof window.location.origin=='undefined' || window.location.origin==null || window.location.origin=='' ? '*':window.location.origin);
        window.opener.postMessage(e.data, origin);
    } catch (ex) {
        console.log('error no bio');
        console.log(ex);
    }
   
    //window.returnValue = e.data;
    //window.close();
});
$(document).ready(function () {
    //window.opener.$.ValidacionNoBiometria.getValues();
    //debugger;
    var url = sessionStorage.getItem('redirect');
    //if (typeof window.opener.Session.UrlRedirecStr == 'undefined') {
    //    window.opener.Session.UrlRedirecStr = '';
    //}
    $('#ifrmNoBiometria').attr('src', url); //window.opener.Session.UrlRedirecStr
    $('#divBody div.transaction-header').attr('style', 'display:none;');
});

