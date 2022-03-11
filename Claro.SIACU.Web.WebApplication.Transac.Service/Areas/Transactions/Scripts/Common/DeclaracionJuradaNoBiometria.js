var strOpcion;
var motivo
(function ($, undefined) {
    'use strict';

    $(document).ready(function () {
        $("#chkPerNatu").prop("checked", true);
        $("#chkDNI").prop("checked", true);
        motivo = $("#hidMotivo").val();
        if (motivo == 1) {
            $("#chkCausa1").prop("checked", true);
        }
        else if (motivo == 2) {
            $("#chkCausa2").prop("checked", true);
        }
        else if (motivo == 3) {
            $("#chkCausa3").prop("checked", true);
        }
        
        
       
        
        $('#btnImprimir').click(function () {
            window.print();
        });
        $('#btnCerrar').click(function () {
            window.close();
        });
    });

})(jQuery, null);
