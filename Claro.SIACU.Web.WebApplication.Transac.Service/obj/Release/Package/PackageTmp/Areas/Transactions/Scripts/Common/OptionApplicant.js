var strOpcion;
(function ($, undefined) {
    'use strict';

    $(document).ready(function () {
        $('#rbtnRRLL').attr('checked','checked');
        $('#btnAceptar').click(function () {

            if ($("#rbtnRRLL").is(':checked')) {
                window.opener.Session.InitValidation = '1';
            } else if ($("#rbtnCartaPoder").is(':checked')) {
                window.opener.Session.InitValidation = '2';
            }
            window.close();
        });

        $('#btnCancelar').off('click').on('click',function () {
            window.close();
        });
    });

})(jQuery, null);