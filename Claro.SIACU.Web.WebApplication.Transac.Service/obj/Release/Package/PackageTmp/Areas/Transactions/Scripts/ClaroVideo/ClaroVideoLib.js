function GetKeyConfig(KeyName) {

    var objFilterType = {
        strIdSession: Session.IDSESSION,
        strfilterKeyName: KeyName
    };

    var value = '';
    $.app.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        async: false,
        dataType: 'json',
        url: '/Transactions/Fixed/ClaroVideo/GetKeyConfig',
        data: JSON.stringify(objFilterType),
        success: function (response) {
            if (response.data != null)
                value = response.data;
        }
    }
    );
    return value;
}

function GetFilterDateMonth(Meses, FlagFormat, FormatSeparador, FormatInverse, callback) {

   
    var desde = '';
    var d = new Date(), months = 12;
    var hasta = '';

    if (FormatInverse == 0) {
        hasta = AboveZero(d.getDate()) + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + d.getFullYear();
    } else {
        hasta = d.getFullYear() + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + AboveZero(d.getDate());
    }

    months = parseInt(Meses);
    console.log('Cantidad de meses atras: ' + months);

    if (months >= 12) {

        let ao = months / 12;

        if (FormatInverse == 0) {
            desde = AboveZero(d.getDate()) + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + (d.getFullYear() - ao);
        } else {
            desde = (d.getFullYear() - ao) + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + AboveZero(d.getDate());
        }

    } else {

        if (months > 0) {

            let m = d.getMonth() + 1;


            if (m == months) {

                if (FormatInverse == 0) {
                    desde = AboveZero(d.getDate()) + FormatSeparador + "12" + FormatSeparador + (d.getFullYear() - 1);
                } else {
                    desde = (d.getFullYear() - 1) + FormatSeparador + "12" + FormatSeparador + AboveZero(d.getDate());
                }

            } else if (m > months) {

                if (FormatInverse == 0) {
                    desde = AboveZero(d.getDate()) + FormatSeparador + (AboveZero(d.getMonth() + 1 - months)) + FormatSeparador + d.getFullYear();
                } else {
                    desde = d.getFullYear() + FormatSeparador + (AboveZero(d.getMonth() + 1 - months)) + FormatSeparador + AboveZero(d.getDate());
                }

            } else {
                let resto = months - m;
                if (FormatInverse == 0) {
                    desde = AboveZero(d.getDate()) + FormatSeparador + AboveZero(12 - resto) + FormatSeparador + (d.getFullYear() - 1);
                } else {
                    desde = (d.getFullYear() - 1) + FormatSeparador + AboveZero(12 - resto) + FormatSeparador + AboveZero(d.getDate());
                }
            }

        } else {

            if (FormatInverse == 0) {
                desde = AboveZero(d.getDate()) + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + (d.getFullYear() - 1);
            } else {
                desde = (d.getFullYear() - 1) + FormatSeparador + (AboveZero(d.getMonth() + 1)) + FormatSeparador + AboveZero(d.getDate());
            }
        }

      

        var IntervalDate = {
            desde: desde + (FlagFormat == 0 ? '' : GetKeyConfig("strFilterFormatInicio")),
            hasta: hasta + (FlagFormat == 0 ? '' : GetKeyConfig("strFilterFormatFin"))
        }

        console.log('Desde: ' + desde + ' Hasta: ' + hasta);
      
        callback(IntervalDate);
    }

}
//CLARO VIDEO
function confirmSuscripcion(ProductoPermitido, message, title, callbackOk, callbackCancel, callbackClose) {
    $.window.open({
        autoSize: true,
        url: '',
        title: title,
        text: message,
        modal: true,
        controlBox: false,
        maximizeBox: false,
        minimizeBox: false,
        width: '400px',
        height: '300px',
        buttons: {
            Aceptar: {
                class: 'btn transaction-button btn-sm',
                id: 'btnconfirmYesCallCut',
                click: function (sender, args) {

                    if (ProductoPermitido) { // valida si es POSTPAGO -HFC- PREPAGO y FTTH
                        this.close();
                        if (callbackOk != null) {
                            callbackOk.call(this, true);
                        }

                        if (callbackClose != null) {
                            callbackClose.call(this, true);
                        }

                    } else { //CASO CONTRARIO SE CIERRA LA PANTALLA
                        this.close();
                        if (callbackOk != null) {
                            callbackOk.call(this, false);
                        }

                    }
                }
            },
            Cancelar: {
                id: 'btnconfirmCancelCallCut',
                click: function (sender, args) {
                    this.close();
                    if (callbackOk != null) {
                        callbackOk.call(this, false);
                    }
                }
            }
        }
    });
}

function showLoading(Mensaje) {
    $.blockUI({
        message: Mensaje,
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
}
function AboveZero(i) {

    if (i < 10) {
        i = '0' + i;
    }
    return i;
}

function hideLoading() {
    $.unblockUI();
}

function alertPromocionClaroVideo(message, title, callback) {


    $.window.open({
        id: 'idModalPromocion',
        autoSize: true,
        url: '',
        title: title,
        text: message,
        modal: true,
        controlBox: false,
        maximizeBox: false,
        minimizeBox: false,
        width: '400px',
        height: '300px',
        buttons: {
            Cerrar: {
                id: 'btnconfirmCerrarPromotion',
                click: function (sender, args) {
                    this.close();
                    if (callback != null) {
                        callback.call(this, false);
                    }
                }
            }
        }
    });

}


function FormatBodyEmailClaroVideo(Cliente, Motivo, Destinatario) {

    var meses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
    var f = new Date();
    var FechaActual = (f.getDate() + " de " + meses[f.getMonth()] + " de " + f.getFullYear());

    var Html = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional //EN"'
    Html = Html + '"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">'
    Html = Html + '<html xmlns="http://www.w3.org/1999/xhtml">'
    Html = Html + '<head>'
    Html = Html + '  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />'
    Html = Html + '  <meta name="viewport" content="width=device-width, initial-scale=1">'
    Html = Html + '  <!--[if gte mso 9]>'
    Html = Html + '  <style type="text/css">'
    Html = Html + '    table { border-collapse:collapse; mso-table-lspace:0pt; mso-table-rspace:0pt; border:0; }'
    Html = Html + 'table td { border-collapse:collapse; font-size:1px; line-height:1px; } '
    Html = Html + '</style>'
    Html = Html + '<![endif]-->'
    Html = Html + '<title>Claro</title>'
    Html = Html + '<style type="text/css">'
    Html = Html + '  * {'
    Html = Html + '      padding: 0;'
    Html = Html + 'margin: 0;'
    Html = Html + '}'
    Html = Html + 'body{'
    Html = Html + ' width:100% !important;'
    Html = Html + '-webkit-text-size-adjust:100%;'
    Html = Html + ' -ms-text-size-adjust:100%; '
    Html = Html + ' margin:0;'
    Html = Html + 'padding:0;'
    Html = Html + '} '
    Html = Html + '   @media screen and (max-width: 650px) {'
    Html = Html + '      table,tbody,thead,tr,td {'
    Html = Html + '          display: block;'
    Html = Html + '          width: auto;'
    Html = Html + '          height: auto;'
    Html = Html + '          text-align: center;'
    Html = Html + '     }'
    Html = Html + '      .letraP {'
    Html = Html + '          padding-bottom: 15px !important;'
    Html = Html + '      }'
    Html = Html + '     .borrarBr br {'
    Html = Html + '         display: none !important;'
    Html = Html + '     }'
    Html = Html + '     td.heading {'
    Html = Html + '         padding-top: 10px;'
    Html = Html + '         padding-bottom: 10px;'
    Html = Html + '      }'
    Html = Html + '      img {'
    Html = Html + '          display: block;'
    Html = Html + '         max-width: 100%;'
    Html = Html + '         height: auto;'
    Html = Html + '         margin: auto;'
    Html = Html + '     }'
    Html = Html + '      table.master {'
    Html = Html + '          margin-left: 5px !important;'
    Html = Html + '         margin-right: 5px !important;'
    Html = Html + '     }'
    Html = Html + '     p.none,'
    Html = Html + '     td.none {'
    Html = Html + '         display: none !important;'
    Html = Html + '     }'
    Html = Html + '     td.show {'
    Html = Html + '         display: block !important;'
    Html = Html + '     }'
    Html = Html + '     td.padding {'
    Html = Html + '         padding: 0 !important;'
    Html = Html + '     }'
    Html = Html + '      td.center {'
    Html = Html + '          text-align: center;'
    Html = Html + '     }'
    Html = Html + '     td.cta {'
    Html = Html + '         width: 200px;'
    Html = Html + '         margin-left: auto;'
    Html = Html + '         margin-right: auto;'
    Html = Html + '      }'
    Html = Html + '     td.number {'
    Html = Html + '         padding-top: 5px !important;'
    Html = Html + '     }'
    Html = Html + '     td.number .mont {'
    Html = Html + '         font-size: 32px !important;'
    Html = Html + '     }'
    Html = Html + '     td.button {'
    Html = Html + '         width: auto;'
    Html = Html + '         margin: auto;'
    Html = Html + '     }'
    Html = Html + '     td.copy {'
    Html = Html + '         text-align: justify;'
    Html = Html + '      }'
    Html = Html + '      td.foobox {'
    Html = Html + '          padding-left: 15px;'
    Html = Html + '         padding-right: 15px;'
    Html = Html + '     }'
    Html = Html + '     td.foobox br {'
    Html = Html + '         display: none;'
    Html = Html + '      }'
    Html = Html + '     td.inline {'
    Html = Html + '         display: inline-block;'
    Html = Html + '     }'
    Html = Html + '     tr.inline {'
    Html = Html + '         display: inline-block;'
    Html = Html + '     }'
    Html = Html + '     td.width {'
    Html = Html + '          width: 49%;'
    Html = Html + '          padding: 0 !important;'
    Html = Html + '          display: inline-block !important;'
    Html = Html + '      }'
    Html = Html + '      td.width1 {'
    Html = Html + '          width: 55%;'
    Html = Html + '          padding: 0 !important;'
    Html = Html + '          display: inline-block !important;'
    Html = Html + '       }'
    Html = Html + '       td.boton {'
    Html = Html + '           padding: 0 !important;'
    Html = Html + '       }'
    Html = Html + '       td.pago1 {'
    Html = Html + '           padding-right: 5px;'
    Html = Html + '       }'
    Html = Html + '      td.pago2 {'
    Html = Html + '          padding-left: 5px;'
    Html = Html + '      }'
    Html = Html + '       td.boton a {'
    Html = Html + '           padding: 8px 0 !important;'
    Html = Html + '           font-size: 14px !important;'
    Html = Html + '       }'
    Html = Html + '       td.icon {'
    Html = Html + '          padding-right: 3px !important;'
    Html = Html + '      }'
    Html = Html + '      img.image {'
    Html = Html + '           height: 65px;'
    Html = Html + '       }'
    Html = Html + '      td.custom {'
    Html = Html + '          padding: 5px 8px !important;'
    Html = Html + '      }'
    Html = Html + '      td.custom table,'
    Html = Html + '      td.custom tr,'
    Html = Html + '      td.custom td {'
    Html = Html + '           text-align: left;'
    Html = Html + '       }'
    Html = Html + '       td.custom p {'
    Html = Html + '           font-size: 12px !important;'
    Html = Html + '       }'
    Html = Html + '      td.radio {'
    Html = Html + '          padding-left: 20px;'
    Html = Html + '          padding-right: 5px;'
    Html = Html + '      }'
    Html = Html + '       td.custom td.padding {'
    Html = Html + '           padding-top: 2px !important;'
    Html = Html + '           padding-bottom: 2px !important;'
    Html = Html + '      }'
    Html = Html + '       td.total {'
    Html = Html + '           padding-left: 0 !important;'
    Html = Html + '           padding-bottom: 0 !important;'
    Html = Html + '       }'
    Html = Html + '       .menos-separacion2{ padding-top: 20px !important; padding-bottom: 20px !important; }'
    Html = Html + '      td.number p {'
    Html = Html + '           font-size: 38px !important;'
    Html = Html + '       }'
    Html = Html + '        img.personalizado {'
    Html = Html + '           width: 100%;'
    Html = Html + '          display: block;'
    Html = Html + '      }'
    Html = Html + '      .imgP-- {'
    Html = Html + '          width: 230px !important;'
    Html = Html + '      }'
    Html = Html + '      .pb___ {'
    Html = Html + '           padding-bottom: 15px !important;'
    Html = Html + '           font-size: 13px;'
    Html = Html + '       }'
    Html = Html + '       .imgpt{'
    Html = Html + '           padding-top: 15px;'
    Html = Html + '      }'
    Html = Html + '      td.header  {'
    Html = Html + '          padding-top: 15px !important;'
    Html = Html + '          padding-bottom: 15px !important;'
    Html = Html + '      }'
    Html = Html + '      td.foot p {'
    Html = Html + '           font-size: 13px !important;'
    Html = Html + '       }'
    Html = Html + '       td.table {'
    Html = Html + '           padding-left: 5px;'
    Html = Html + '           padding-right: 5px;'
    Html = Html + '           padding-top: 5px !important;'
    Html = Html + '      }'
    Html = Html + '       td.standard p {'
    Html = Html + '           font-size: 13px !important;'
    Html = Html + '       }'
    Html = Html + '       .titulo1{ font-size: 19px !important; }'
    Html = Html + '      .sumilla{  font-size: 11px !important; padding-bottom: 5px !important;  }'
    Html = Html + '      .sumilla br{ display: none !important; }'
    Html = Html + '      .no-border{ border-right: 0 !important }'
    Html = Html + '      .navega-rapido{ padding: 0 !important;   margin: 0 2px 10px 2px !important; }'
    Html = Html + '       .navega-rapido img{ width: 130px }'
    Html = Html + '       .solucion-movil{  margin: 0 auto !important; }'
    Html = Html + '      .nueva-red{ padding: 5px 0 0 0 !important; }'
    Html = Html + '      td.padd-r-l{ padding: 0 5px !important; width: 40% !important; display: inline-block !important;}'
    Html = Html + '      .separador-banner{ padding-top: 30px !important; padding-bottom: 0px !important; }'
    Html = Html + '      .menos-separacion{ padding-top: 10px !important; padding-bottom: 0px !important; }'
    Html = Html + '      .pb{'
    Html = Html + '           padding-bottom: 10px !important;'
    Html = Html + '       }'
    Html = Html + '      .pt__ {'
    Html = Html + '          padding-top: 20px !important;'
    Html = Html + '      }'
    Html = Html + '       .plr_ {'
    Html = Html + '          padding-left: 10px !important;'
    Html = Html + '           padding-right: 10px !important;'
    Html = Html + '       }'
    Html = Html + '       .img_h {'
    Html = Html + '           height: 0 !important;'
    Html = Html + '       }'
    Html = Html + '       .legal{ font-size: 9px !important; }'
    Html = Html + '       .no-border-right{ border-right: 0 !important }'
    Html = Html + '      .head-nombre{ padding-top: 10px !important }'
    Html = Html + '      .nombre{ text-align: center !important; font-size: 22px !important ;  padding-bottom: 15px; padding-top: 5px !important;}'
    Html = Html + '      .head-precio{ padding-top: 0!important }'
    Html = Html + '      .precio{ font-size: 30px !important; }'
    Html = Html + '      .table td{ display: inline-block; }'
    Html = Html + '      .ico-formnas-pago{ height: 30px !important; width: auto !important; }'
    Html = Html + '      .separador-banner {'
    Html = Html + '           padding-top: 0px !important;'
    Html = Html + '           margin: 0 auto !important;'
    Html = Html + '      }'
    Html = Html + '       .pt-{'
    Html = Html + '          padding-top: 25px !important;'
    Html = Html + '  }'
    Html = Html + ' .pt-- {'
    Html = Html + ' padding-top: 20px !important;'
    Html = Html + ' padding-bottom: 10px !important;'
    Html = Html + ' }'
    Html = Html + ' .pbv2- {'
    Html = Html + ' padding-bottom: 0 !important;'
    Html = Html + ' }'
    Html = Html + '  .iw{'
    Html = Html + ' width: 100% !important;'
    Html = Html + '   }'
    Html = Html + ' .imgp____ {'
    Html = Html + '  width: 100% !important;'
    Html = Html + ' }'
    Html = Html + ' }'
    Html = Html + ' </style>'
    Html = Html + ' </head>'
    Html = Html + ' <body>'
    Html = Html + ' <table class="master" cellpadding="0" cellspacing="0" border="0" align="center" width="650" style="font-family:Arial,Helvetica,sans-serif;color:#000000;margin:auto;line-height:1.2;">'
    Html = Html + '   <tr>'
    Html = Html + '     <!-- legal superior -->'
    Html = Html + '     <td align="center" style="padding:10px 0;display:block; font-family:Arial,Helvetica,sans-serif;font-size:9px;">'
    Html = Html + '      <br/>'
    Html = Html + '      Para garantizar la entrega de los e-mails a tu bandeja de entrada, por favor a&ntilde;ade a tu libreta de direcciones nuestra direcci&oacute;n:<br/> '
    Html = Html + '      <a href="mailto:newsletter@mailing.claro.com.pe"  target="_blank" title="newsletter@mailing.claro.com.pe" style="color:#003bc2;">newsletter@mailing.claro.com.pe</a>'
    Html = Html + '    </td>'
    Html = Html + '    <!-- / legal superior -->'
    Html = Html + '  </tr>'
    Html = Html + '  <tr>'
    Html = Html + '    <!-- cenefa -->'
    Html = Html + '    <td bgcolor="#d22e28">'
    Html = Html + '      <img style="display:block;" src="http://static.claro.com.pe/mailing/havas/2019/ene/02/peliculas/header.jpg" alt="Claro" width="650" border="0">'
    Html = Html + '    </td>'
    Html = Html + '     <!-- / cenefa -->'
    Html = Html + '   </tr>'
    Html = Html + '  <tr>'
    Html = Html + '    <td class="head-nombre" valign="top" style="">'
    Html = Html + '      <!-- header -->'
    Html = Html + '      <table width="100%" cellpadding="0" cellspacing="0" border="0">'
    Html = Html + '        <tr>'
    Html = Html + '          <td style="PADDING-BOTTOM:20px">'
    Html = Html + '            <p align="left"><img style="BORDER-LEFT-WIDTH:0px;BORDER-RIGHT-WIDTH:0px;BORDER-BOTTOM-WIDTH:0px;DISPLAY:block;BORDER-TOP-WIDTH:0px" alt="Hola" src="https://ci3.googleusercontent.com/proxy/srfBW9ExEonDU3bJBAa1MPLVjn4s25gfArQ87lyQED-P26G6NdGoRASCXw-le_ttCWNZRsvAOzRyuV9POXIuRZiDBGXDyfrcrjIyrz32AUGTjRY7GmI3gT03RCZZvi4=s0-d-e1-ft#http://static.claro.com.pe/mailing/havas/2019/may/29/recibo_digital/hola.jpg" width="72" height="32" class="CToWUd"></p> '
    Html = Html + '          </td> '
    Html = Html + '         </tr>'
    Html = Html + '         <tr>'
    Html = Html + '           <br>'
    Html = Html + '           <td style="FONT-SIZE:30px;FONT-FAMILY:Arial,Helvetica,sans-serif;FONT-WEIGHT:bold;COLOR:#d32d27;TEXT-ALIGN:left;PADDING-TOP:0px;DISPLAY:block">'
    Html = Html + '             ' + Cliente + ''
    Html = Html + '           </td>  '
    Html = Html + '           <td class="nombre" style="font-family:Arial, Helvetica, sans-serif;color: #555;font-size: 14px;font-weight: normal;display: block;padding-top: 0;text-align: left;padding-top: 30px; padding-left: 7px; padding-right: 7px;; padding-bottom: 30px;"> '
    Html = Html + '             Le adjuntamos la constancia de la solicitud de ' + Motivo + ' de envío de promociones que realizaste el d&iacute;a ' + FechaActual + '. '
    Html = Html + '           </td>'
    Html = Html + '         </tr>'
    Html = Html + '       </table>'
    Html = Html + '       <!-- / header -->'
    Html = Html + '     </td>'
    Html = Html + '   </tr>'
    Html = Html + '   <!-- / consultas -->'
    Html = Html + '   <tr>'
    Html = Html + '     <td style="padding-bottom: 10px; padding-top: 10px;" >'
    Html = Html + '       <table width="100%" cellpadding="0" cellspacing="0" border="0">'
    Html = Html + '         <tr>'
    Html = Html + '           <td bgcolor="#f4f4f4" style="padding: 0 15px; ">'
    Html = Html + '             <table width="100%" cellpadding="0" cellspacing="0" border="0">'
    Html = Html + '               <tr>'
    Html = Html + '                 <td style="font-family:Arial,Helvetica,sans-serif;font-size:13px;color:#818181;margin:0;padding:25px 0 20px 0; text-align: center;">'
    Html = Html + '                   Si tienes alguna duda o consulta, comun&iacute;cate con nosotros:'
    Html = Html + ' </td>'
    Html = Html + ' </tr>'
    Html = Html + ' <tr>'
    Html = Html + ' <td style="padding-bottom: 30px;">'
    Html = Html + '   <table class="comunicate" width="100%" cellpadding="0" cellspacing="0" border="0">'
    Html = Html + '     <tr>'
    Html = Html + '       <td valign="top" width="31.33333%">'
    Html = Html + '         <table cellpadding="0" cellspacing="0" border="0" width="100%" class="table">'
    Html = Html + '           <tr>'
    Html = Html + '             <td valign="top"><img style="display:block;" src="http://static.claro.com.pe/mailing/havas/2019/ene/02/peliculas/ico-cell.jpg" alt="" width="28" height="42" border="0" /></td>'
    Html = Html + '             <td valign="bottom">'
    Html = Html + '               <div style="font-family:Arial,Helvetica,sans-serif;font-size:18px; color: #818181; font-weight: bolder; display: block; line-height: 18px;">123</div>'
    Html = Html + '               <div style="font-family:Arial,Helvetica,sans-serif;font-size:13px; color: #818181">Desde cualquier m&oacute;vil Claro.</div>'
    Html = Html + '             </td>'
    Html = Html + '           </tr>'
    Html = Html + '         </table>'
    Html = Html + '       </td>'
    Html = Html + '       <td valign="top"   width="33.33333%">'
    Html = Html + '         <table cellpadding="0" cellspacing="0" border="0" width="100%" class="table">'
    Html = Html + '           <tr>'
    Html = Html + '             <td align="right"><img style="display:block;" src="http://static.claro.com.pe/mailing/havas/2019/ene/02/peliculas/ico-phone.jpg" alt="" width="33" height="42" border="0" /></td>'
    Html = Html + '             <td valign="bottom">'
    Html = Html + '               <div class="pt-" style="font-family:Arial,Helvetica,sans-serif;font-size:18px; color: #818181; font-weight: bolder; display: block; line-height: 23px;">0800 00 123</div>'
    Html = Html + '               <div style="font-family:Arial,Helvetica,sans-serif;font-size:13px; color: #818181"> Desde cualquier fijo.</div>'
    Html = Html + '             </td>'
    Html = Html + '           </tr>'
    Html = Html + '         </table>'
    Html = Html + '       </td>'
    Html = Html + '       <td valign="top" width="33.33333%">'
    Html = Html + '         <table cellpadding="0" cellspacing="0" border="0" width="100%" class="table">'
    Html = Html + '           <tr>'
    Html = Html + '             <td><img style="display:block;" src="http://static.claro.com.pe/mailing/havas/2019/ene/02/peliculas/ico-call.jpg" alt="" width="36" height="43" border="0" /></td>'
    Html = Html + '             <td valign="bottom">'
    Html = Html + '              <div class="pt-" style="font-family:Arial,Helvetica,sans-serif;font-size:18px; color: #818181; font-weight: bolder; display: block; line-height: 23px;">01 620 0123</div>'
    Html = Html + '              <div style="font-family:Arial,Helvetica,sans-serif;font-size:13px; color: #818181">Desde cualquier m&oacute;vil o fijo.</div>'
    Html = Html + '            </td>'
    Html = Html + '           </tr>'
    Html = Html + '         </table>'
    Html = Html + '       </td>'
    Html = Html + '     </tr>'
    Html = Html + '   </table>'
    Html = Html + '  </td>'
    Html = Html + ' </tr>'
    Html = Html + ' </table>'
    Html = Html + ' </td>'
    Html = Html + ' </tr>'
    Html = Html + ' </table>'
    Html = Html + ' </td>'
    Html = Html + ' </tr>'
    Html = Html + ' <tr>'
    Html = Html + ' <td bgcolor="#d32d27" align="center" style="padding-top:3px;padding-bottom:3px;">'
    Html = Html + ' <p style="font-size:13px;color:#ffffff;font-family:arial,helvetica,sans-serif;margin:0;padding:0;">'
    Html = Html + ' Por favor, no responda sobre este correo'
    Html = Html + ' </p>'
    Html = Html + ' </td>'
    Html = Html + ' </tr>'
    Html = Html + ' <tr>'
    Html = Html + ' <td style="padding: 10px 0;text-align:justify;">'
    Html = Html + '   <p style="margin: 0;display: block;font-size:10px;color:#626262;font-family:Arial, Helvetica, sans-serif;margin-bottom: 10px;">M&aacute;s informaci&oacute;n sobre las consultas y operaciones que puedes realizar seg&uacute;n tus servicios, as&iacute; como los dispositivos compatibles en: <a href="https://www.miclaro.com.pe" target="_blank" style="color:#003bc2">miclaro.com.pe</a>.</p>'
    Html = Html + ' <p style="margin: 0;display: block;font-size:10px;color:#626262;font-family:Arial, Helvetica, sans-serif;margin-bottom: 10px;">Ingresa a la gu&iacute;a telef&oacute;nica digital:  <a href="http://www.claro.com.pe/directorio-de-abonados-fijos/" target="_blank" style="color:#003bc2">www.claro.com.pe/directorio-de-abonados-fijos/</a>.</p>'
    Html = Html + ' <p style="margin: 0;display: block;font-size:10px;color:#626262;font-family:Arial, Helvetica, sans-serif;">Este e-mail fue enviado a <a href="#" target="_blank" style="color:#003bc2;">' + Destinatario + '</a>.  Todos los derechos reservados, Claro 2019. Am&eacute;rica M&oacute;vil Per&uacute; S.A.C. Av. Nicol&aacute;s Arriola Nro 480, Lima 13 - Per&uacute;. Tel&eacute;fono (511) 613-1000. Su privacidad es importante para nosotros. Por favor revise nuestras  <a href="http://www.claro.com.pe/personas/movil/proteccion_datos/" target="_blank" style="color:#003bc2">Pol&iacute;ticas de Privacidad</a>.</p>'
    Html = Html + ' </td>'
    Html = Html + ' </tr>'
    Html = Html + ' </table>'
    Html = Html + ' </body>'
    Html = Html + ' </html>'

    return Html;
}