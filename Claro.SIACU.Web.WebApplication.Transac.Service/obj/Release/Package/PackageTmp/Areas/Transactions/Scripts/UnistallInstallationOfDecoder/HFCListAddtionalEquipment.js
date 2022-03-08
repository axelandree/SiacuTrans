(function ($, undefined) {


    var Form = function ($element, options) {
        $.extend(this, $.fn.HFCListAddtionalEquipment.defaults, $element.data(), typeof options === 'object' && options);

        this.setControls({
            form: $element,

            //Tablas
            tblEquiposAdicionales: $('#tblEquiposAdicionales', $element),

            chkEquiposAdicionales: $('#chkEquiposAdicionales', $element),
        });
    }

    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
                control = this.getControls();

            control.chkEquiposAdicionales.addEvent(that, 'change', that.cbxEquiposAdicionales_Click);

            that.render();
        },
        render: function () {
            var that = this,
                control = that.getControls();

            var image = window.location.protocol + '//' + window.location.host + '/Images/loading2.gif';
            var load = '<div id="dvLoadModal" class="modalInsDes" style="display:none;" align="center"><span><img src="' + image + '" width="25" height="25" />  Cargando ....</span></div>';
            $('.modal-dialog').append(load);

            that.IniLoadAddtionalEquipment();
        },

        IniLoadAddtionalEquipment: function () {
            var that = this,
                control = that.getControls();

            var tblEquiposAdicionales = control.tblEquiposAdicionales.dataTable({
                "scrollY": "225px",
                "scrollCollapse": true,
                "bLengthChange": true,
                "searching": true,
                "bProcessing": true,
                "bAutoWidth": false,
                "bDestroy": true,
                "bPaginate": false,
                "sPaginationType": "full_numbers",
                "oLanguage": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ Registros.",
                    "sSearch": "Buscar: ",
                    "sZeroRecords": "No existen datos.",
                    "sInfo": "Mostrando _START_ hasta _END_ de _TOTAL_ registros.",
                    "sInfoEmpty": "No hay registros para mostrar.",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sPrevious": "Anterior",
                        "sNext": "Siguiente",
                        "sLast": "Último"
                    },
                    "sEmptyTable": "No existen datos"
                },
                'aoColumnDefs': [
                        { "aTargets": [0], "sClass": "text-center", "bSortable": false },
                        { "aTargets": [1] },
                        { "aTargets": [2], "sClass": "text-right" },
                        { "aTargets": [3], "bVisible": false },
                        { "aTargets": [4], "bVisible": false },
                        { "aTargets": [5], "bVisible": false },
                ]
            });

            that.GetAddtionalEquipment(tblEquiposAdicionales);
        },

        GetAddtionalEquipment: function (tabla) {
            //$("#btnAgregaCore").hide(); Revisar
            //$("#btnAgregaEquipo").show(); Revisa
            var that = this,
                control = that.getControls(),
                param = {};
            
            $('#dvLoadModal').show();

            tabla.fnClearTable();

            param.strIdSession = HFCPOST_Session.Url.IDSESSION;
            param.idplan = HFCPOST_Session.CodigoPlan;
            param.coid = HFCPOST_Session.CoID;

            $.ajax({
                type: "POST",
                url: "/Transactions/HFC/UnistallInstallationOfDecoder/GetAddtionalEquipment",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                error: function (data) {
                    $('#dvLoadModal').hide();
                    alert("Error al recuperar datos.", "Alerta");
                },
                success: function (response) {
                    var filas = response.data,
                        index = 0;

                    HFCPOST_Session.ListaEquiposAdicionalesServer = filas;

                    if (filas != null) {
                        $.each(filas, function (key, value) {
                            var grupo = filas[index].CodGrupoServ;
                            var tipoServicio = filas[index].CodTipoServ;
                            var id = filas[index].CodServSisact;
                            var descripcion = filas[index].DesServSisact;
                            var solucion = filas[index].Solucion;
                            var equipo = filas[index].Equipo;
                            var costo = that.Round((parseFloat(filas[index].CF) * parseFloat(HFCPOST_Session.igv)), 2).toFixed(2);
                            var cantidad = filas[index].CantidadEquipo;
                            var identificador = descripcion + id + equipo;
                            var sncod = filas[index].SNCode;
                            var spcod = filas[index].SPCode;
                            var tipoequi = filas[index].TipoEquipo;
                            var tmcode = filas[index].TmCode;
                            var mdsap = filas[index].MatvDesSap;

                            var deshabilitar = "";

                            if (grupo == "8") {
                                var radio = "<input id='servEquipo" + "_" + id + "' name='group10' type='checkbox' " + deshabilitar + " />";
                                tabla.fnAddData([radio, descripcion, costo, equipo, cantidad, mdsap]);
                                $("#servEquipo" + "_" + id).click(function () {
                                    var isChecked = $(this)[0].checked;
                                    that.setSelectedEquipment(descripcion + id + equipo, isChecked);
                                });
                            }

                            index++;
                        });

                        //Recorriendo tablas y comparando si ya fue agregado
                        $("#tblEquipos tbody tr").each(function (index) {
                            /*alert('1->' + $(this).find("td").eq(1).text());
                            alert('2->' + descripcion);*/
                            var idEquipoResumen = "";
                            var descripcionEquipoResumen = $(this).find("td").eq(1).text();
                            if ($(this).find("td").length > 1) {
                                var SNSPCodServicioGrupoSer = $(this).find("td :input[type=hidden]").val().split(',');
                                idEquipoResumen = SNSPCodServicioGrupoSer[2];
                            }

                            $("#tblEquiposAdicionales tbody tr").each(function (index) {
                                //alert('1->' + descripcionEquipoResumen);
                                //alert('2->' + $(this).find("td").eq(1).text());
                                if (descripcionEquipoResumen == $(this).find("td").eq(1).text()) {
                                    //alert("Iguales " + idEquipoResumen);
                                    $("#servEquipo_" + idEquipoResumen).attr('disabled', 'disabled')
                                    HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
                                }
                            });
                            /*if ($(this).find("td").eq(1).text() == descripcion) {
                            deshabilitar = "disabled";
                            }*/
                        });
                    }

                    $('#dvLoadModal').hide();
                }
            });
        },

        getControls: function () {
            return this.m_controls || {};
        },

        setControls: function (value) {
            this.m_controls = value;
        },

        setSelectedEquipment: function (identificador, isChecked) {
            var equiposSeleccionados = HFCPOST_Session.CodEquipAlSelec.substring(1, HFCPOST_Session.CodEquipAlSelec.length).split('|');
            var count = 0;

            if (isChecked) {
                for (var i = 0 ; i < equiposSeleccionados.length ; i++) {
                    if (identificador == equiposSeleccionados[i])
                        count++;
                }

                if (count == 0)
                    HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + identificador;
            } else {
                HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
                for (var i = 0 ; i < equiposSeleccionados.length ; i++) {
                    if (identificador != equiposSeleccionados[i])
                        HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + equiposSeleccionados[i];
                }
            }


            //HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + identificador;
        },

        loadPage: function () {
            $.blockUI({
                message: '<div align="center"><img src="' + this.strUrlLogo + '" width="25" height="25" /> Cargando ... </div>',
                css: {
                    border: 'none',
                    padding: '15px',
                    backgroundColor: '#000',
                    '-webkit-border-radius': '10px',
                    '-moz-border-radius': '10px',
                    opacity: .5,
                    color: '#fff',
                }
            });
        },

        strUrlLogo: window.location.protocol + '//' + window.location.host + '/Images/loading2.gif',

        Round: function (cantidad, decimales) {
            //donde: decimales > 2
            var cantidad = parseFloat(cantidad);
            var decimales = parseFloat(decimales);
            decimales = (!decimales ? 2 : decimales);
            return Math.round(cantidad * Math.pow(10, decimales)) / Math.pow(10, decimales);
        },

        cbxEquiposAdicionales_Click: function () {
            var that = this,
                control = that.getControls(),
                lista = HFCPOST_Session.ListaEquiposAdicionalesServer,
                isChecked = control.chkEquiposAdicionales[0].checked;

            if (isChecked) {
                //$("#tblEquiposAdicionales tbody tr td input[type=checkbox]").prop("checked", true);
                
                var descripcionEquipo = HFCPOST_Session.strVariableEmpty;
                $("#tblEquipos tbody tr").each(function (index) {
                    descripcionEquipo = descripcionEquipo + '§' + $(this).find("td").eq(1).text();
                });

                descripcionEquipo = descripcionEquipo.substring(1, descripcionEquipo.length).split('§');
                $("#tblEquiposAdicionales tbody tr").each(function (index) {
                    if ($.inArray($(this).find("td").eq(1).text(), descripcionEquipo) == -1) {
                        $(this).find("td :input[type=checkbox]").prop("checked", true);
                    }
                });

                $.each(lista, function (key, value) {
                    var grupo = value.CodGrupoServ;
                    var descripcion = value.DesServSisact;
                    var id = value.CodServSisact;
                    var equipo = value.Equipo;
                    var identificador = descripcion + id + equipo;

                    if (grupo == "8") {                        
                        if ($.inArray(descripcion, descripcionEquipo) == -1) {
                            HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.CodEquipAlSelec + '|' + identificador;
                        }
                    }
                });
            }
            else {
                $("#tblEquiposAdicionales tbody tr td input[type=checkbox]").prop("checked", false);
                HFCPOST_Session.CodEquipAlSelec = HFCPOST_Session.strVariableEmpty;
            }
        },
    };

    $.fn.HFCListAddtionalEquipment = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('HFCListAddtionalEquipment'),
                options = $.extend({}, $.fn.HFCListAddtionalEquipment.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('HFCListAddtionalEquipment', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {
                data.init();
                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };

    $.fn.HFCListAddtionalEquipment.defaults = {
    }

    $('#HFCListAddtionalEquipment').HFCListAddtionalEquipment();

})(jQuery);