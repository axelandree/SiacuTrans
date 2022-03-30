var SessionTransac = JSON.parse(sessionStorage.getItem("SessionTransac"));
console.log('PREPAGO');
console.log(SessionTransac);
(function ($, undefined) {
    'use strict';
    var Form = function ($element, options) {
        $.extend(this, $.fn.DiscardListPre.defaults, $element.data(), typeof options == 'object' && options);
        this.setControls({
            form: $element
               , divContenedor: $('#divContenedor', $element)
        });
    };
    Form.prototype = {
        constructor: Form,
        init: function () {
            var that = this,
            controls = that.getControls();
            that.render();
        },
        render: function () {
            var that = this,
            controls = that.getControls();
            that.ValidarAcceso(function (callback) {
                if (callback) {
                    that.GetInfoTableDiscard();
                }
            });
        },
        CrearControl: {
            Table: function (id, classname) {
                var divTable = document.createElement("table");
                divTable.className = classname;
                divTable.id = id;
                return divTable;
            },
            Div: function (id, classname) {
                var divRow = document.createElement("div");
                divRow.className = classname;
                divRow.id = id;
                return divRow;
            },
            tr: function (id, classname) {
                var divRow = document.createElement("tr");
                divRow.className = classname;
                divRow.id = id;
                return divRow;
            },
            tbody: function (id) {
                var divRow = document.createElement("tbody");
                divRow.id = id;
                return divRow;
            },
            td: function (id, width) {
                var divRow = document.createElement("td");
                divRow.id = id;
                divRow.width = width;
                return divRow;
            },
            thead: function () {
                var divthead = document.createElement("thead");
                return divthead;
            },
            Label: function (id, classname, value) {
                var lab = document.createElement("label");
                lab.className = classname + ' line ';
                lab.id = id;
                lab.innerHTML = '&nbsp;&nbsp;' + value + '&nbsp;&nbsp;';
                return lab;
            },
            ul: function (id, classname) {
                var divRow = document.createElement("ul");
                divRow.className = classname;
                divRow.id = id;
                return divRow;
            },
            li: function (id, classname) {
                var divRow = document.createElement("li");
                divRow.className = classname;
                divRow.id = id;
                return divRow;
            },
            a: function (id, value) {
                var divRow = document.createElement("a");
                divRow.href = '#' + id;
                divRow.innerHTML = '&nbsp;&nbsp;' + value + '&nbsp;&nbsp;';
                return divRow;
            },
            th: function (value) {
                var divRow = document.createElement("th");
                divRow.innerHTML = '&nbsp;&nbsp;' + value + '&nbsp;&nbsp;';
                return divRow;
            },
            span: function (id, classname) {
                var divRow = document.createElement("span");
                divRow.className = classname;
                divRow.id = id;
                return divRow;
            },
            btn: function (id, values, clasname) {
                var divRow = document.createElement("input");
                divRow.className = clasname;
                divRow.id = id;
                divRow.type = 'button';
                divRow.value = values;
                return divRow;
            },
            center: function () {
                var lab = document.createElement("center");
                return lab;
            },
            c_Element: function (id, values, clasname, element, type) {
                var divRow = document.createElement(element);
                divRow.className = clasname;
                divRow.id = id;
                divRow.type = type;
                divRow.value = values;
                return divRow;
            }
        },
        OcultarBdRe: 0,
        CadenaDescartes: "",
        list_idDescarte: "",
        contadorCliksStr: "",
        ContBloqSiacUDBDesactivo: "0",
        ContBloqSiacUDBActivo: "0",
        ContBloqSiacUDBServErr: "0",
        ContBloqSiacUDBTotal: "0",
        BannerGrupo: '986',
        BannerAccion: 'S',
        BannerUsuario: '',
        BannerFecha: '',
        BannerDescripcion: '',
        BannerPermiso: '',
        BannerAccionBtn: '',

        ValidarAcceso: function (callback) {

            var that = this,
            controls = that.getControls();

            var AccesoProductoPermitido = false;
            var Mensaje = '';
            if ($.isEmptyObject(SessionTransac.SessionParams) == false) {

                var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;

                if (oCustomer.Application == 'PREPAID') {
                    AccesoProductoPermitido = true;
                    callback(true);
                } else {
                    AccesoProductoPermitido = false;
                    Mensaje = 'La página no se encuentra disponible. Favor primero realizar la búsqueda desde SIAC UNICO; Nota: Lista de descartes disponibles solo para Líneas Móviles.';
                }
            }

            if (AccesoProductoPermitido == false) {
                confirmConsultarDescartes(Mensaje, 'Confirmar', function (result) {
                    if (result == true) {

                        $('#btnconfirmYesCallCut').attr('data-loading-text', "<i class='fa fa-spinner fa-spin '></i> Cargando");
                        $('#btnconfirmYesCallCut').button('loading');

                        callback(true);
                    } else {
                        callback(false);
                        that.btnClose_click();
                    }


                });
            }
        },
        OcultarBodyDetRec: function () {
            var that = this;
            if (that.OcultarBdRe == 0) {
                document.getElementById("divTabletBody18").removeAttribute('style');
                that.OcultarBdRe = 1;
            } else {
                document.getElementById("divTabletBody18").setAttribute('style', 'display:none');
                that.OcultarBdRe = 0;
            }
        },
        MetbtnActualizartab: function (e) {
            var that = this;
            var lista = that.CadenaDescartes.split(";");
            var nom_Descarbtn = $(e).attr('id');

            that.contadorCliksStr = nom_Descarbtn + "," + that.contadorCliksStr;

            var contador = 0;
            $.grep(that.contadorCliksStr.split(","), function (Item) {
                if (Item.length > 0) {
                    if (Item == nom_Descarbtn) {
                        contador++;
                    }
                }
            });

            document.getElementById(nom_Descarbtn).setAttribute('style', 'display:none');

            var idGrupo = "";
            var list_idLabel = "";

            if (lista.length > 0) {
                $.grep(lista, function (id) {
                    if (id.length > 0) {
                        var id_Descar = id.split(",")[2];
                        if (nom_Descarbtn == id_Descar) {
                            idGrupo = id.split(",")[0];
                        }
                    }
                });

                var idDescarte = "";
                $.grep(lista, function (id) {
                    if (id.length > 0) {
                        if (idGrupo == id.split(",")[0]) {
                            if (id.split(",")[1] != idDescarte) {
                                idDescarte = id.split(",")[1];
                                that.list_idDescarte = idDescarte + "," + that.list_idDescarte;
                            }
                            list_idLabel = id.split(",") + ";" + list_idLabel;
                        }
                    }
                });
                that.list_idDescarte = that.list_idDescarte.substring(0, (that.list_idDescarte.length - 1));

                list_idLabel = list_idLabel.split(";");

                var cont2 = 0;
                $.grep(list_idLabel, function (item) {
                    if (item.length > 0) {
                        if (item.split(",").length <= 4) {
                            var carga = item.split(",")[3];
                            document.getElementById(carga).innerHTML = '<center><img src="../../Images/loading.gif" width="15px" height="15px"></center>';
                        } else {
                            var carga = item.split(",")[5];
                            document.getElementById(carga).innerHTML = '<center><img src="../../Images/loading.gif" width="15px" height="15px"></center>';
                        }
                    }
                });

                that.GetInfoGroupAndDiscard(function () {

                    if (that.dataListDescartes.listaDescartes.length > 0) {
                        $.grep(that.dataListDescartes.listaDescartes, function (value) {
                            if (value.id_grupo == "16") {
                                if (value.flag_OK == '1') {
                                    that.ContBloqSiacUDBDesactivo++;
                                }
                            }
                        });
                    }

                    var contUDB = 0;

                    if (that.dataListDescartes.listaDescartes.length > 0) {
                        $.grep(that.dataListDescartes.listaDescartes, function (value) {
                            $.grep(list_idLabel, function (valueItemls) {
                                if (valueItemls.split(",")[1] == value.id_descarte) {
                                    if (valueItemls.split(",").length <= 4) {
                                        if (value.id_grupo == "16" && that.ContBloqSiacUDBDesactivo == that.ContBloqSiacUDBTotal) {
                                            if (contUDB == 0) {

                                                $("#divTabletBody16 tr").remove();

                                                var divTabletBodytr = document.createElement("tr");
                                                divTabletBodytr.id = 'divTabletBodytr16';
                                                var divTabletBodytrFirstTD = document.createElement("td");
                                                divTabletBodytrFirstTD.id = 'divTabletBodytrFirstTD16'
                                                divTabletBodytrFirstTD.width = '30%'
                                                var divTabletBodytrFirstTDLabel = document.createElement("label");
                                                divTabletBodytrFirstTDLabel.id = 'divTabletBodytrFirstTDLabel16';
                                                divTabletBodytrFirstTDLabel.className = 'control-label text-success line';
                                                divTabletBodytrFirstTDLabel.innerHTML = 'No tiene bloqueos activos';
                                                var centerlbl = document.createElement("center")
                                                var divTabletBody = document.getElementById('divTabletBody16');
                                                var divTable = document.getElementById('divTable16');

                                                centerlbl.appendChild(divTabletBodytrFirstTDLabel);
                                                divTabletBodytrFirstTD.appendChild(centerlbl);
                                                divTabletBodytr.appendChild(divTabletBodytrFirstTD);
                                                divTabletBody.appendChild(divTabletBodytr);
                                                divTable.appendChild(divTabletBody);

                                                contUDB = 1;
                                            }
                                        } else {
                                            var lblDato = valueItemls.split(",")[3];
                                            var ColorTexto = '';
                                            if (value.flag_OK != null) {
                                                if (value.flag_OK == '0') {
                                                    ColorTexto = 'text-danger';
                                                } else if (value.flag_OK == '1') {
                                                    ColorTexto = 'text-success';
                                                }
                                            }
                                            document.getElementById(lblDato).removeAttribute('class');
                                            document.getElementById(lblDato).setAttribute('class', 'control-label ' + ColorTexto + ' line ');
                                            document.getElementById(lblDato).innerHTML = '&nbsp;&nbsp;' + value.descarteValor;
                                            if (value.flag_Error != 0) {
                                                cont2++;
                                            }
                                        }
                                    } else {
                                        if ((valueItemls.split(",")[1] == 17 || valueItemls.split(",")[1] == 25 || valueItemls.split(",")[1] == 29) && value.flag_Error == 0) {
                                            if (value.descarteListaValor != null) {
                                                if (value.descarteListaValor.length > 0) {
                                                    var divTabletBody = document.getElementById('divTabletBody' + valueItemls.split(",")[0]); //armamos fila
                                                    var divTabletBodytr = document.getElementById(valueItemls.split(",")[6]);

                                                    document.getElementById(valueItemls.split(",")[4]).setAttribute('rowspan', value.descarteListaValor.length);
                                                    document.getElementById(valueItemls.split(",")[4]).removeAttribute('width');
                                                    document.getElementById(valueItemls.split(",")[4]).setAttribute('width', '50%');

                                                    $.each(value.descarteListaValor, function (index, valueItem) {
                                                        var divTabletBodytrSecondCombinadoTD1 = document.createElement("td");
                                                        divTabletBodytrSecondCombinadoTD1.id = 'divTabletBodytrSecondCombinadoTD1' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTD1.width = "25%";

                                                        var divTabletBodytrSecondCombinadoTDLabel1 = document.createElement("label");
                                                        divTabletBodytrSecondCombinadoTDLabel1.className = 'control-label text-success line'
                                                        divTabletBodytrSecondCombinadoTDLabel1.id = 'divTabletBodytrSecondCombinadoTDLabel1' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTDLabel1.innerHTML = '&nbsp;&nbsp;' + valueItem.nombre + '&nbsp;&nbsp;';

                                                        var divTabletBodytrSecondCombinadoTD2 = document.createElement("td");
                                                        divTabletBodytrSecondCombinadoTD2.id = 'divTabletBodytrSecondCombinadoTD2' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTD2.width = "5%";

                                                        var divTabletBodytrSecondCombinadoTDLabel2 = document.createElement("label");
                                                        divTabletBodytrSecondCombinadoTDLabel2.className = 'control-label text-success line'
                                                        divTabletBodytrSecondCombinadoTDLabel2.id = 'divTabletBodytrSecondCombinadoTDLabel2' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTDLabel2.innerHTML = '&nbsp;&nbsp;' + valueItem.valor + '&nbsp;&nbsp;';

                                                        var divTabletBodytrSecondCombinadoTD3 = document.createElement("td");
                                                        divTabletBodytrSecondCombinadoTD3.id = 'divTabletBodytrSecondCombinadoTD3' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTD3.width = "15%";

                                                        var divTabletBodytrSecondCombinadoTDLabel3 = document.createElement("label");
                                                        divTabletBodytrSecondCombinadoTDLabel3.className = 'control-label text-success line'
                                                        divTabletBodytrSecondCombinadoTDLabel3.id = 'divTabletBodytrSecondCombinadoTDLabel3' + valueItemls.split(",")[0];
                                                        divTabletBodytrSecondCombinadoTDLabel3.innerHTML = '&nbsp;&nbsp;' + valueItem.fechaVencimiento + '&nbsp;&nbsp;';

                                                        var ColorValor = valueItem.valor.toUpperCase();
                                                        ColorValor = ColorValor.replace(/ /g, "");

                                                        if (valueItem.nombre.indexOf("GPRS") >= 0 && ColorValor.indexOf("MB") >= 0) {
                                                            ColorValor = ColorValor.replace(/MB/g, "");
                                                            if (ColorValor <= 10) {
                                                                ColorValor = 0;
                                                            }
                                                        } else {
                                                            ColorValor = ColorValor.replace(/:/g, "");
                                                            ColorValor = ColorValor.replace(/GB/g, "");
                                                            ColorValor = ColorValor.replace(/MB/g, "");
                                                            ColorValor = ColorValor.replace(/-/g, "0");
                                                            ColorValor = ColorValor.replace(/ILIMITADO/g, "1");
                                                        }

                                                        if (ColorValor <= 0) {
                                                            divTabletBodytrSecondCombinadoTDLabel1.setAttribute("style", "color: #a94442");
                                                            divTabletBodytrSecondCombinadoTDLabel2.setAttribute("style", "color: #a94442");
                                                            divTabletBodytrSecondCombinadoTDLabel3.setAttribute("style", "color: #a94442");
                                                        }

                                                        if (index == 0) {

                                                            divTabletBodytrSecondCombinadoTD1 = document.getElementById(valueItemls.split(",")[3]);

                                                            divTabletBodytrSecondCombinadoTD1.removeAttribute('width');//
                                                            divTabletBodytrSecondCombinadoTD1.removeAttribute('colspan');//
                                                            divTabletBodytrSecondCombinadoTD1.setAttribute('width', "25%");//

                                                            divTabletBodytrSecondCombinadoTDLabel1 = document.getElementById(valueItemls.split(",")[5]);
                                                            divTabletBodytrSecondCombinadoTDLabel1.removeAttribute('id');
                                                            divTabletBodytrSecondCombinadoTDLabel1.removeAttribute('class');
                                                            divTabletBodytrSecondCombinadoTDLabel1.setAttribute('class', 'control-label text-success');
                                                            divTabletBodytrSecondCombinadoTDLabel1.innerHTML = '&nbsp;&nbsp;' + valueItem.nombre + '&nbsp;&nbsp;';

                                                            divTabletBodytrSecondCombinadoTD2.appendChild(divTabletBodytrSecondCombinadoTDLabel2);
                                                            divTabletBodytrSecondCombinadoTD3.appendChild(divTabletBodytrSecondCombinadoTDLabel3);
                                                            divTabletBodytr.appendChild(divTabletBodytrSecondCombinadoTD2);
                                                            divTabletBodytr.appendChild(divTabletBodytrSecondCombinadoTD3);
                                                        } else {
                                                            divTabletBodytrSecondCombinadoTD1.appendChild(divTabletBodytrSecondCombinadoTDLabel1);
                                                            divTabletBodytrSecondCombinadoTD2.appendChild(divTabletBodytrSecondCombinadoTDLabel2);
                                                            divTabletBodytrSecondCombinadoTD3.appendChild(divTabletBodytrSecondCombinadoTDLabel3);
                                                            divTabletBodytr.appendChild(divTabletBodytrSecondCombinadoTD1);
                                                            divTabletBodytr.appendChild(divTabletBodytrSecondCombinadoTD2);
                                                            divTabletBodytr.appendChild(divTabletBodytrSecondCombinadoTD3);
                                                        }

                                                        if (index <= value.descarteListaValor.length + 1) {
                                                            divTabletBodytr = document.createElement("tr");
                                                            divTabletBodytr.className = '';
                                                            divTabletBodytr.id = 'divTabletBodytr' + valueItemls.split(",")[0];
                                                            divTabletBody.appendChild(divTabletBodytr);
                                                        }
                                                    });
                                                }
                                            }
                                        }

                                        if (value.flag_Error != 0) {
                                            var carga = valueItemls.split(",")[5];
                                            document.getElementById(carga).innerHTML = 'Error disponibilidad de Servicio.';
                                            cont2++;
                                        }
                                    }
                                }
                            });
                        });
                    } else {
                        cont2++;
                        $.grep(list_idLabel, function (item) {
                            if (item.length > 0) {
                                if (item.split(",").length <= 4) {
                                    var carga = item.split(",")[3];
                                    document.getElementById(carga).innerHTML = 'Error disponibilidad de Servicio.';
                                } else {
                                    var carga = item.split(",")[5];
                                    document.getElementById(carga).innerHTML = 'Error disponibilidad de Servicio.';
                                }
                            }
                        });
                    }

                    if (cont2 > 0) {
                        document.getElementById(nom_Descarbtn).removeAttribute('style');
                    }

                    if (contador >= 2) {
                        document.getElementById(nom_Descarbtn).setAttribute('style', 'display:none');
                    }
                });

                that.list_idDescarte = "";
            }
        },

        MetBannerActualizar: function (e) {
            console.log("MetBannerActualizar");
            var that = this;
            that.BannerAccion = 'U';
            var idEliminarBan = $(e).attr('id');

            if (idEliminarBan == "btnFallaElim") {
                document.getElementById("txtFalla").value = "";
                that.BannerAccionBtn = 'E';
            }
            that.BannerDescripcion = document.getElementById("txtFalla").value;
            that.MetBannerDescartesAcBus();
        },

        MetBannerDescartesAcBus: function () {
            console.log("MetBannerDescartesAcBus");
            var that = this;

            if (that.BannerDescripcion == "") {
                that.BannerDescripcion = " ";
            }

            var objRequest = {
                strIdSession: Session.IDSESSION,
                bannerDescartesRequest: {
                    descripcion: that.BannerDescripcion,
                    usuario: that.BannerUsuario,
                    fecha: that.BannerFecha,
                    grupo: that.BannerGrupo,
                    accion: that.BannerAccion
                }
            };

            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                url: '/Transactions/Fixed/Discard/BannerDescartesAcBus',
                data: JSON.stringify(objRequest),
                success: function (response) {
                    if (response != null) {
                        if (response.data != null) {
                            if (response.data.bannerDescartesResponse != null) {
                                if (response.data.bannerDescartesResponse.responseAudit != null) {
                                    if (response.data.bannerDescartesResponse.responseAudit.codigoRespuesta == "0") {
                                        if (response.data.bannerDescartesResponse.responseData != null) {
                                            if (response.data.bannerDescartesResponse.responseData.descripcion != null) {
                                                var strdescripcion = document.getElementById('txtFalla');
                                                if (response.data.bannerDescartesResponse.responseData.descripcion.trim() != "") {
                                                    strdescripcion.value = response.data.bannerDescartesResponse.responseData.descripcion.trim();
                                                } else {
                                                    if (that.BannerPermiso == "1") {
                                                        strdescripcion.value = response.data.bannerDescartesResponse.responseData.descripcion.trim();
                                                    } else {
                                                        var DivBanner = document.getElementById('idBanner');
                                                        DivBanner.style.display = 'none';
                                                    }
                                                }
                                            }
                                        }
                                        if (that.BannerAccion == 'U') {
                                            if (that.BannerAccionBtn == 'E') {
                                                alert("Banner Eliminado Correctamente");
                                            } else {
                                                alert("Banner Actualizado Correctamente");
                                            }
                                        }
                                    } else {
                                        if (that.BannerAccion == 'U') {
                                            if (that.BannerAccionBtn == 'E') {
                                                alert("Banner No Eliminado");
                                            } else {
                                                alert("Banner No Actualizado");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                error: function (msger) {
                    console.log(msger);
                    var strdescripcion = document.getElementById('txtFalla');
                    strdescripcion.value = "";
                    callback();
                }
            });
        },

        GetInfoTableDiscard: function () {

            var that = this,
            controls = this.getControls();

            that.GetInfoGroupAndDiscard(function () {

                var divul = '';
                var countTab = 0;
                var divtabcontent = '';
                var tabActiveIn = '';

                var IdGrupoLlamadaPrepago = GetKeyConfig("strIdGrupoLlamadaPrepago");
                var IdGrupoInternetPrepago = GetKeyConfig("strIdGrupoInternetPrepago");
                var IdGrupoSMSPostPrepago = GetKeyConfig("strIdGrupoSMSPostPrepago");

                //INI: INICIATIVA- BANNER
                var divBanner = $(that.CrearControl.c_Element('idBanner', '', 'c_Banner', 'div', ''));
                var banInput = $(that.CrearControl.c_Element('txtFalla', '', 'txtFallas', 'input', 'text'));
                divBanner.append(banInput);

                var perfilesPermitidosBanner = GetKeyConfig("PerfilesPermitidosContinue").split('|');
                var objUserAccess = SessionTransac.SessionParams.USERACCESS;
                $.grep(perfilesPermitidosBanner, function (value) {
                    if (value == objUserAccess.profileId) {
                        var banbtnElim = $(that.CrearControl.c_Element('btnFallaElim', 'Eliminar', 'btnFallaBann', 'input', 'button'));
                        var banbtnActu = $(that.CrearControl.c_Element('btnFallaAct', 'Actualizar', 'btnFallaBann', 'input', 'button'));
                        banbtnActu.addEvent(that, 'click', that.MetBannerActualizar);
                        banbtnElim.addEvent(that, 'click', that.MetBannerActualizar);
                        var Bfecha = new Date();
                        that.BannerFecha = Bfecha.getDate() + "/" + (Bfecha.getMonth() + 1) + "/" + Bfecha.getFullYear();
                        that.BannerUsuario = objUserAccess.login;
                        that.BannerPermiso = '1';
                        divBanner.append(banbtnElim);
                        divBanner.append(banbtnActu);
                    } else {
                        banInput.attr("disabled", "");
                    }
                });
                controls.divContenedor.append(divBanner);
                that.MetBannerDescartesAcBus();

                //FIN: INICIATIVA- BANNER
                $.grep(that.dataListDescartes.listaGrupos, function (ItemGroup) {

                    var classActive = '';
                    if (ItemGroup.degriIdGrupo == IdGrupoLlamadaPrepago || ItemGroup.degriIdGrupo == IdGrupoInternetPrepago || ItemGroup.degriIdGrupo == IdGrupoSMSPostPrepago) {

                        if (countTab == 0) {
                            divul = $(that.CrearControl.ul('divul' + ItemGroup.degriIdGrupo, 'nav nav-tabs')); //armamos fila   
                            divul.attr("role", "tablist");
                            divul.attr("style", "cursor: pointer");
                            divul.attr("data-tabs", "tabs");
                        }

                        if (countTab == 0) {
                            classActive = 'active';
                        } else {
                            classActive = '';
                        }

                        var divli = $(that.CrearControl.li('divli' + ItemGroup.degriIdGrupo, classActive + ' text-center col-md-4')); //armamos fila
                        divli.attr("style", "padding: 1px");

                        var divlia = $(that.CrearControl.a('divtab' + ItemGroup.degriIdGrupo, ItemGroup.degrvDescripcion)); //armamos fila
                        divlia.attr("role", "tab");
                        divlia.attr("data-toggle", "tab");
                        divlia.attr("data-parent", "divDashboardTabs");

                        var IconoSpan = '';
                        if (ItemGroup.degriIdGrupo == IdGrupoLlamadaPrepago) {
                            IconoSpan = 'glyphicon glyphicon-earphone';
                        } else if (ItemGroup.degriIdGrupo == IdGrupoInternetPrepago) {
                            IconoSpan = 'glyphicon glyphicon-globe';
                        } else if (ItemGroup.degriIdGrupo == IdGrupoSMSPostPrepago) {
                            IconoSpan = 'glyphicon glyphicon-envelope';
                        }

                        var divliaSpan = $(that.CrearControl.span('divliaSpan' + ItemGroup.degriIdGrupo, IconoSpan)); //armamos fila
                        divlia.append(divliaSpan);
                        divli.append(divlia);
                        divul.append(divli);
                        controls.divContenedor.append(divul);

                        if (countTab == 0) {
                            divtabcontent = $(that.CrearControl.Div('divtabcontent' + ItemGroup.degriIdGrupo, 'tab-content')); //armamos fila                            

                            tabActiveIn = ' active in';
                        } else {
                            tabActiveIn = '';
                        }

                        var divtab = $(that.CrearControl.Div('divtab' + ItemGroup.degriIdGrupo, 'tab-pane fade' + tabActiveIn)); //armamos fila  

                        var divColumnFirstPanelCombinadoBody = $(that.CrearControl.Div('divColumnFirstPanelCombinadoBody' + ItemGroup.degriIdGrupo, 'col-md-1')); //armamos fila 
                        var divColumnSecondPanelCombinadoBody = $(that.CrearControl.Div('divColumnSecondPanelCombinadoBody' + ItemGroup.degriIdGrupo, 'col-md-10')); //armamos fila 
                        var divColumnTerceroPanelCombinadoBody = $(that.CrearControl.Div('divColumnTerceroPanelCombinadoBody' + ItemGroup.degriIdGrupo, 'col-md-1')); //armamos fila 

                        divtab.append(divColumnFirstPanelCombinadoBody);

                        var divTable = $(that.CrearControl.Table('divTable' + ItemGroup.degriIdGrupo, 'table table-hover table-bordered  table-condensed claro-modal-tabla  dataTable no-footer')); //armamos fila
                        var divTablethead = $(that.CrearControl.thead('divTablethead' + ItemGroup.degriIdGrupo, 'table table-bordered')); //armamos fila
                        var divTabletr = $(that.CrearControl.tr('divTabletr' + ItemGroup.degriIdGrupo, 'btn-plomo')); //armamos fila

                        var divTabletrTh = $(that.CrearControl.th(ItemGroup.degrvDescripcion)); //armamos fila
                        divTabletrTh.attr("scope", "col");
                        divTabletrTh.attr("colspan", "5");
                        divTabletrTh.attr("width", "100%");
                        divTabletrTh.attr("style", "text-align:center");

                        divTabletr.append(divTabletrTh);
                        divTablethead.append(divTabletr);
                        divTable.append(divTablethead);

                        var divTabletBody = $(that.CrearControl.tbody('divTabletBody' + ItemGroup.degriIdGrupo)); //armamos fila

                        var ListaDescartesGrupo = $.grep(that.dataListDescartes.listaDescartes, function (data) {
                            return data.id_grupo == ItemGroup.degriIdGrupo;
                        });

                        if (ListaDescartesGrupo != null) {
                            if (ListaDescartesGrupo.length > 0) {
                                var conTitu = 0;
                                $.each(ListaDescartesGrupo, function (index, value) {

                                    var PorcentajeFirstCelda = '30%';
                                    var PorcentajeSecondCelda = '30%';
                                    var FlagCombinacionCelda = false;
                                    var RowSpanCeldaCombinada = '1';
                                    if (value.descarteListaValor != null) {
                                        if (value.descarteListaValor.length > 0) {
                                            PorcentajeFirstCelda = '50%';
                                            PorcentajeSecondCelda = '15%';
                                            FlagCombinacionCelda = true;
                                            RowSpanCeldaCombinada = value.descarteListaValor.length;
                                        }
                                    }

                                    var idLabel = 'divTabletBodytrSecondTDLabel' + ItemGroup.degriIdGrupo,
									idBodytrSecond = 'divTabletBodytrSecondTD' + ItemGroup.degriIdGrupo,
									idBodytrFirst = 'divTabletBodytrFirstTD' + ItemGroup.degriIdGrupo,
									idBodytr = 'divTabletBodytr' + ItemGroup.degriIdGrupo;

                                    if (value.flag_Error != 0) {
                                        if (conTitu == 0) {
                                            var btnTitu = $(that.CrearControl.btn(value.nombre_variable, "", "btnActualizar"));
                                            btnTitu.addEvent(that, 'click', that.MetbtnActualizartab);
                                            divTabletrTh.append("<h43>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h43>");
                                            divTabletrTh.append(btnTitu);
                                        }
                                        conTitu++;
                                        idLabel = 'divTabletBodytrSecondTDLabel' + ItemGroup.degriIdGrupo + conTitu;
                                        if (value.id_descarte == "17" || value.id_descarte == "25" || value.id_descarte == "29") {
                                            idBodytrSecond = 'divTabletBodytrSecondTD' + ItemGroup.degriIdGrupo + conTitu;
                                            idBodytrFirst = 'divTabletBodytrFirstTD' + ItemGroup.degriIdGrupo + conTitu;
                                            idBodytr = 'divTabletBodytr' + ItemGroup.degriIdGrupo + conTitu;
                                            var MetTex = ItemGroup.degriIdGrupo + "," + value.id_descarte + "," + value.nombre_variable + "," + idBodytrSecond + "," + idBodytrFirst + "," + idLabel + "," + idBodytr;
                                            that.CadenaDescartes = MetTex + ";" + that.CadenaDescartes;
                                        } else {
                                            var MetTex = ItemGroup.degriIdGrupo + "," + value.id_descarte + "," + value.nombre_variable + "," + idLabel;
                                            that.CadenaDescartes = MetTex + ";" + that.CadenaDescartes;
                                        }
                                    }

                                    var divTabletBodytr = $(that.CrearControl.tr(idBodytr, '')); //armamos fila
                                    var divTabletBodytrFirstTD = $(that.CrearControl.td(idBodytrFirst, PorcentajeFirstCelda)); //armamos fila
                                    divTabletBodytrFirstTD.attr("colspan", "2");

                                    if (FlagCombinacionCelda) {
                                        divTabletBodytrFirstTD.attr("rowspan", RowSpanCeldaCombinada);
                                    }

                                    var divTabletBodytrFirstTDLabel = $(that.CrearControl.Label('divTabletBodytrFirstTDLabel' + ItemGroup.degriIdGrupo, 'control-label', value.desc_descarte)); //armamos fila
                                    divTabletBodytrFirstTD.append(divTabletBodytrFirstTDLabel);
                                    divTabletBodytr.append(divTabletBodytrFirstTD);

                                    if (FlagCombinacionCelda) {
                                        $.each(value.descarteListaValor, function (index, valueItem) {

                                            var divTabletBodytrSecondCombinadoTD1 = $(that.CrearControl.td('divTabletBodytrSecondCombinadoTD1' + ItemGroup.degriIdGrupo, "25%")); //armamos fila
                                            var divTabletBodytrSecondCombinadoTDLabel1 = $(that.CrearControl.Label('divTabletBodytrSecondCombinadoTDLabel1' + ItemGroup.degriIdGrupo, 'control-label text-success', valueItem.nombre)); //armamos fila

                                            var divTabletBodytrSecondCombinadoTD2 = $(that.CrearControl.td('divTabletBodytrSecondCombinadoTD2' + ItemGroup.degriIdGrupo, "5%")); //armamos fila
                                            var divTabletBodytrSecondCombinadoTDLabel2 = $(that.CrearControl.Label('divTabletBodytrSecondCombinadoTDLabel2' + ItemGroup.degriIdGrupo, 'control-label text-success', valueItem.valor)); //armamos fila

                                            var divTabletBodytrSecondCombinadoTD3 = $(that.CrearControl.td('divTabletBodytrSecondCombinadoTD3' + ItemGroup.degriIdGrupo, "15%")); //armamos fila
                                            var divTabletBodytrSecondCombinadoTDLabel3 = $(that.CrearControl.Label('divTabletBodytrSecondCombinadoTDLabel3' + ItemGroup.degriIdGrupo, 'control-label text-success', valueItem.fechaVencimiento)); //armamos fila

                                            var ColorValor = valueItem.valor.toUpperCase();
                                            ColorValor = ColorValor.replace(/ /g, "");

                                            if (valueItem.nombre.indexOf("GPRS") >= 0 && ColorValor.indexOf("MB") >= 0) {
                                                ColorValor = ColorValor.replace(/MB/g, "");
                                                if (ColorValor <= 10) {
                                                    ColorValor = 0;
                                                }
                                            } else {
                                                ColorValor = ColorValor.replace(/:/g, "");
                                                ColorValor = ColorValor.replace(/GB/g, "");
                                                ColorValor = ColorValor.replace(/MB/g, "");
                                                ColorValor = ColorValor.replace(/-/g, "0");
                                                ColorValor = ColorValor.replace(/ILIMITADO/g, "1");
                                            }

                                            if (ColorValor <= 0) {
                                                divTabletBodytrSecondCombinadoTDLabel1.attr("style", "color: #a94442");
                                                divTabletBodytrSecondCombinadoTDLabel2.attr("style", "color: #a94442");
                                                divTabletBodytrSecondCombinadoTDLabel3.attr("style", "color: #a94442");
                                            }

                                            divTabletBodytrSecondCombinadoTD1.append(divTabletBodytrSecondCombinadoTDLabel1);
                                            divTabletBodytr.append(divTabletBodytrSecondCombinadoTD1);

                                            divTabletBodytrSecondCombinadoTD2.append(divTabletBodytrSecondCombinadoTDLabel2);
                                            divTabletBodytr.append(divTabletBodytrSecondCombinadoTD2);

                                            divTabletBodytrSecondCombinadoTD3.append(divTabletBodytrSecondCombinadoTDLabel3);
                                            divTabletBodytr.append(divTabletBodytrSecondCombinadoTD3);
                                            divTabletBody.append(divTabletBodytr);

                                            if (index <= value.descarteListaValor.length + 1) {
                                                divTabletBodytr = $(that.CrearControl.tr('divTabletBodytr' + ItemGroup.degriIdGrupo, '')); //armamos fila
                                                divTabletBody.append(divTabletBodytr);
                                            }
                                        });


                                    } else {
                                        var ColorTexto = '';

                                        if (value.flag_OK != null) {
                                            if (value.flag_OK == '0') {
                                                ColorTexto = 'text-danger';
                                            } else if (value.flag_OK == '1') {
                                                ColorTexto = 'text-success';
                                            }
                                        }

                                        var divTabletBodytrSecondTDLabel = $(that.CrearControl.Label(idLabel, 'control-label ' + ColorTexto, value.descarteValor == null ? '' : value.descarteValor)); //armamos fila
                                        var divTabletBodytrSecondTD = $(that.CrearControl.td(idBodytrSecond, PorcentajeSecondCelda)); //armamos fila
                                        divTabletBodytrSecondTD.append(divTabletBodytrSecondTDLabel);
                                        divTabletBodytrSecondTD.attr("colspan", "3");

                                        divTabletBodytr.append(divTabletBodytrSecondTD);
                                        divTabletBody.append(divTabletBodytr);
                                    }

                                    divTable.append(divTabletBody);
                                });
                            }
                        }

                        divColumnSecondPanelCombinadoBody.append(divTable);
                        divtab.append(divColumnSecondPanelCombinadoBody);

                        divtab.append(divColumnTerceroPanelCombinadoBody);

                        var divRow = $(that.CrearControl.Div('divRow' + ItemGroup.degriIdGrupo, 'row'));
                        divtab.append(divRow);
                        divtabcontent.append(divtab);
                        controls.divContenedor.append(divtabcontent);

                        countTab = countTab + 1;

                    } else {

                        var divPanel = $(that.CrearControl.Div('divPanel' + ItemGroup.degriIdGrupo, 'panel')); //armamos fila
                        //var divPanelHead = $(that.CrearControl.Div('divPanelHead' + ItemGroup.degriIdGrupo, 'panel-heading')); //armamos fila
                        ////label label-primary
                        //var divPanelHead_Label = $(that.CrearControl.Label('divPanelHead_Label' + ItemGroup.degriIdGrupo, 'control-label', ''));
                        //divPanelHead.append(divPanelHead_Label);
                        //divPanel.append(divPanelHead);

                        var divPanelBody = $(that.CrearControl.Div('divPanelBody' + ItemGroup.degriIdGrupo, 'panel-body')); //armamos fila 
                        var divColumnFirstPanelBody = $(that.CrearControl.Div('divColumnFirstPanelBody' + ItemGroup.degriIdGrupo, 'col-md-2')); //armamos fila 
                        var divColumnSecondPanelBody = $(that.CrearControl.Div('divColumnSecondPanelBody' + ItemGroup.degriIdGrupo, 'col-md-8')); //armamos fila 
                        var divColumnTerceroPanelBody = $(that.CrearControl.Div('divColumnTerceroPanelBody' + ItemGroup.degriIdGrupo, 'col-md-2')); //armamos fila 

                        divPanelBody.append(divColumnFirstPanelBody);


                        var divTable = $(that.CrearControl.Table('divTable' + ItemGroup.degriIdGrupo, 'table table-hover table-bordered  table-condensed claro-modal-tabla  dataTable no-footer')); //armamos fila
                        var divTablethead = $(that.CrearControl.thead('divTablethead' + ItemGroup.degriIdGrupo, 'table table-bordered')); //armamos fila
                        var divTabletr = $(that.CrearControl.tr('divTabletr' + ItemGroup.degriIdGrupo, 'btn-plomo')); //armamos fila

                        var divTabletrTh = $(that.CrearControl.th(ItemGroup.degrvDescripcion)); //armamos fila
                        divTabletrTh.attr("scope", "col");
                        divTabletrTh.attr("style", "text-align:center");

                        var divTabletBody = $(that.CrearControl.tbody('divTabletBody' + ItemGroup.degriIdGrupo)); //armamos fila

                        divTabletr.append(divTabletrTh);

                        if (ItemGroup.degriIdGrupo == 18) {
                            divTabletrTh.attr("width", "50%");
                            var divTabletrTh2 = $(that.CrearControl.th("Ver Información adicional")); //armamos fila
                            divTabletrTh2.append('<img src="../../Images/senalar.png" width="27px"/>');
                            divTabletrTh2.attr("scope", "col");
                            divTabletrTh2.attr("width", "50%");
                            divTabletrTh2.attr("style", "text-align:center; cursor:pointer");
                            divTabletrTh2.addEvent(that, 'click', that.OcultarBodyDetRec);

                            divTabletr.append(divTabletrTh2);
                            divTabletBody.attr("style", "display:none");
                        } else {
                            divTabletrTh.attr("width", "100%");
                            divTabletrTh.attr("colspan", "5");
                        }

                        divTablethead.append(divTabletr);
                        divTable.append(divTablethead);

                        var ListaDescartesGrupo = $.grep(that.dataListDescartes.listaDescartes, function (data) {
                            return data.id_grupo == ItemGroup.degriIdGrupo;
                        });

                        if (ListaDescartesGrupo != null) {
                            if (ListaDescartesGrupo.length > 0) {
                                $.each(ListaDescartesGrupo, function (ind, val) {
                                    if (ItemGroup.degriIdGrupo == "16") {
                                        that.ContBloqSiacUDBTotal++;
                                        if (val.flag_OK == '1') {
                                            that.ContBloqSiacUDBDesactivo++;
                                        }
                                    }
                                });

                                var conTitu = 0;
                                var contUDB = 0;

                                $.each(ListaDescartesGrupo, function (index, value) {
                                    var ColorTexto = '';
                                    if (ItemGroup.degriIdGrupo == "16" && that.ContBloqSiacUDBDesactivo == that.ContBloqSiacUDBTotal) {
                                        if (contUDB == 0) {
                                            ColorTexto = 'text-success';
                                            var divTabletBodytr = $(that.CrearControl.tr('divTabletBodytr' + ItemGroup.degriIdGrupo, '')); //armamos fila
                                            var divTabletBodytrFirstTD = $(that.CrearControl.td('divTabletBodytrFirstTD' + ItemGroup.degriIdGrupo, '30%')); //armamos fila
                                            var divTabletBodytrFirstTDLabel = $(that.CrearControl.Label('divTabletBodytrFirstTDLabel' + ItemGroup.degriIdGrupo, 'control-label ' + ColorTexto, '&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + "No tiene bloqueos activos")); //armamos fila
                                            var centerlbl = $(that.CrearControl.center());

                                            centerlbl.append(divTabletBodytrFirstTDLabel);
                                            divTabletBodytrFirstTD.append(centerlbl);
                                            divTabletBodytr.append(divTabletBodytrFirstTD);
                                            divTabletBody.append(divTabletBodytr);
                                            divTable.append(divTabletBody);
                                            contUDB = 1;
                                        }
                                    } else {

                                        var divTabletBodytr = $(that.CrearControl.tr('divTabletBodytr' + ItemGroup.degriIdGrupo, '')); //armamos fila
                                        var divTabletBodytrFirstTD = $(that.CrearControl.td('divTabletBodytrFirstTD' + ItemGroup.degriIdGrupo, '30%')); //armamos fila
                                        var divTabletBodytrSecondTD = $(that.CrearControl.td('divTabletBodytrSecondTD' + ItemGroup.degriIdGrupo, '30%')); //armamos fila
                                        var divTabletBodytrFirstTDLabel = $(that.CrearControl.Label('divTabletBodytrFirstTDLabel' + ItemGroup.degriIdGrupo, 'control-label ', value.desc_descarte)); //armamos fila

                                        if (value.flag_OK != null) {
                                            if (value.flag_OK == '0') {
                                                ColorTexto = 'text-danger';
                                            } else if (value.flag_OK == '1') {
                                                ColorTexto = 'text-success';
                                            }
                                        }

                                        var idLabel = 'divTabletBodytrSecondTDLabel' + ItemGroup.degriIdGrupo;

                                        if (value.flag_Error != 0) {
                                            if (conTitu == 0) {
                                                var btnTitu = $(that.CrearControl.btn(value.nombre_variable, "", "btnActualizar"));
                                                btnTitu.addEvent(that, 'click', that.MetbtnActualizartab);
                                                divTabletrTh.append("<h43>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h43>");
                                                divTabletrTh.append(btnTitu);
                                            }
                                            conTitu++;
                                            idLabel = 'divTabletBodytrSecondTDLabel' + ItemGroup.degriIdGrupo + conTitu;
                                            var MetTex = ItemGroup.degriIdGrupo + "," + value.id_descarte + "," + value.nombre_variable + "," + idLabel;
                                            that.CadenaDescartes = MetTex + ";" + that.CadenaDescartes;
                                        }

                                        var divTabletBodytrSecondTDLabel = $(that.CrearControl.Label(idLabel, 'control-label ' + ColorTexto, value.descarteValor == null ? '' : value.descarteValor)); //armamos fila

                                        divTabletBodytrFirstTD.append(divTabletBodytrFirstTDLabel);
                                        divTabletBodytrSecondTD.append(divTabletBodytrSecondTDLabel);
                                        divTabletBodytr.append(divTabletBodytrFirstTD);
                                        divTabletBodytr.append(divTabletBodytrSecondTD);
                                        divTabletBody.append(divTabletBodytr);
                                        divTable.append(divTabletBody);
                                    }
                                });
                            }
                        }

                        divColumnSecondPanelBody.append(divTable);
                        divPanelBody.append(divColumnSecondPanelBody);
                        divPanelBody.append(divColumnTerceroPanelBody);
                        divPanel.append(divPanelBody);
                        controls.divContenedor.append(divPanel);
                    }
                });
            });
        },
        GetInfoGroupAndDiscard: function (callback) {

            var that = this,
           controls = this.getControls();

            if (that.CadenaDescartes == "") {
                showLoading('Cargando información de descartes.');
            }

            var oCustomer = SessionTransac.SessionParams.DATACUSTOMER;
            var ContractID = '';
            var Telefono = '';
            var TipoLinea = '';
            if ($.isEmptyObject(oCustomer) == false) {

                if (oCustomer.ContractID != undefined) {
                    if (oCustomer.ContractID != null) {
                        ContractID = oCustomer.ContractID;
                    }
                }

                Telefono = ((oCustomer.Telephone == null || oCustomer.Telephone == '') ?
                        (oCustomer.TelephoneCustomer == null || oCustomer.TelephoneCustomer == '') ? '' : oCustomer.TelephoneCustomer
                        : oCustomer.Telephone);

                if (oCustomer.Application == 'POSTPAID') { TipoLinea = 'POSTPAGO' }
                if (oCustomer.Application == 'PREPAID') { TipoLinea = 'PREPAGO' }

            }

            //INI: INICIATIVA-871
            that.listaDescarteListaValor = [];
            if (SessionTransac.SessionParams.DATASERVICE.listBalance.length > 0) {
                $.each(SessionTransac.SessionParams.DATASERVICE.listBalance, function (index, value) {
                    var descarteListaValor = {
                        idDescarte: "",
                        nombre: "",
                        valor: "",
                        medida: "",
                        fechaVencimiento: "",
                        esCabecera: ""
                    }

                    if (value.CommercialName == 'Bono por Recarga - Minutos Nacionales') {
                        descarteListaValor.idDescarte = "45",
                        descarteListaValor.nombre = value.CommercialName;
                        descarteListaValor.valor = value.Balance;
                        descarteListaValor.medida = "";
                        descarteListaValor.fechaVencimiento = value.Expiration;
                        descarteListaValor.esCabecera = ""
                        that.listaDescarteListaValor.push(descarteListaValor);
                    }
                    else if (value.CommercialName == 'Bono por recarga - SMS') {
                        descarteListaValor.idDescarte = "53",
                        descarteListaValor.nombre = value.CommercialName;
                        descarteListaValor.valor = value.Balance;
                        descarteListaValor.medida = "";
                        descarteListaValor.fechaVencimiento = value.Expiration;
                        descarteListaValor.esCabecera = "";
                        that.listaDescarteListaValor.push(descarteListaValor);
                    }
                });
            };
            //FIN: INICIATIVA-871

            var varConsultDiscardRTIRequest = {
                consultarDescartesRtiPrePostRequest: {
                    coId: ContractID,
                    msisdn: Telefono,
                    tipoLinea: TipoLinea
                }
            };

            var objReqConsultDiscardRTIRequest = {
                strIdSession: Session.IDSESSION,
                MessageRequest: {
                    Body: varConsultDiscardRTIRequest
                },
                objParametrosSesion: { //INICIATIVA-871
                    EstadoPortabilidad: SessionTransac.SessionParams.DATASERVICE.oPortability.Answer,
                    ListaDescartesValor: that.listaDescarteListaValor
                },
                listIddescarte: that.list_idDescarte
            };

            console.log('Request Descartes');
            console.log(JSON.stringify(objReqConsultDiscardRTIRequest))

            // controls.btnbuscar.button('loading');
            $.app.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                //async: false,
                url: '/Transactions/Fixed/Discard/ConsultDiscardRTI',
                data: JSON.stringify(objReqConsultDiscardRTIRequest),
                complete: function () {
                    hideLoading();
                },
                success: function (response) {
                    console.log(response);

                    if (response.data != null) {

                        that.dataListDescartes.listaDescartes = [];
                        that.dataListDescartes.listaGrupos = [];

                        if (response.data.listaDescartes != null) {
                            if (response.data.listaDescartes.length > 0) {
                                $.each(response.data.listaDescartes, function (index, value) {

                                    var listaDescartes = {
                                        id_descarte: "",
                                        nombre_variable: "",
                                        desc_descarte: "",
                                        tipo_descarte: "",
                                        flag_descarte: "",
                                        orden_descarte: "",
                                        fecha_reg: "",
                                        id_grupo: "",
                                        flag_OK: "",
                                        flag_Error: "",
                                        descarteValor: "",
                                        descarteListaValor: ""
                                    }

                                    listaDescartes.id_descarte = value.id_descarte,
                                    listaDescartes.nombre_variable = value.nombre_variable;
                                    listaDescartes.desc_descarte = value.desc_descarte;
                                    listaDescartes.tipo_descarte = value.tipo_descarte;
                                    listaDescartes.flag_descarte = value.flag_descarte;
                                    listaDescartes.orden_descarte = value.orden_descarte;
                                    listaDescartes.fecha_reg = value.fecha_reg;
                                    listaDescartes.id_grupo = value.id_grupo;
                                    listaDescartes.flag_OK = value.flag_OK;
                                    listaDescartes.flag_Error = value.flag_Error;
                                    listaDescartes.descarteValor = value.descarteValor;
                                    listaDescartes.descarteListaValor = value.descarteListaValor;

                                    that.dataListDescartes.listaDescartes.push(listaDescartes);

                                });
                            }
                        }

                        if (response.data.listaGrupos != null) {
                            if (response.data.listaGrupos.length > 0) {
                                $.each(response.data.listaGrupos, function (index, value) {

                                    var listaGrupos = {
                                        degriIdGrupo: "",
                                        degrvDescripcion: "",
                                        degrvTipo: "",
                                        degriFlagVisual: "",
                                        degriOrden: "",
                                        degrvUsuReg: "",
                                        degrdFecReg: "",
                                        degrvUsuMod: "",
                                        degrdFecMod: "",
                                        degriEstadoReg: ""
                                    }

                                    listaGrupos.degriIdGrupo = value.degriIdGrupo,
                                    listaGrupos.degrvDescripcion = value.degrvDescripcion;
                                    listaGrupos.degrvTipo = value.degrvTipo;
                                    listaGrupos.degriFlagVisual = value.degriFlagVisual;
                                    listaGrupos.degriOrden = value.degriOrden;
                                    listaGrupos.degrvUsuReg = value.degrvUsuReg;
                                    listaGrupos.degrdFecReg = value.degrdFecReg;
                                    listaGrupos.degrvUsuMod = value.degrvUsuMod;
                                    listaGrupos.degrdFecMod = value.degrdFecMod;
                                    listaGrupos.degriEstadoReg = value.degriEstadoReg;

                                    that.dataListDescartes.listaGrupos.push(listaGrupos);

                                });
                            }
                        }
                        console.log("dataListDescartes");
                        console.log(that.dataListDescartes);
                        callback();
                    }

                },
                error: function (msger) {
                    callback();
                    console.log(msger);
                }
            });
        },
        dataListDescartes: {
            listaGrupos: [],
            listaDescartes: [],

        },
        getControls: function () {
            return this.m_controls || {};
        },
        setControls: function (value) {
            this.m_controls = value;
        },
        btnClose_click: function () {
            window.close();
        },
        strUrl: window.location.protocol + '//' + window.location.host,
    };
    $.fn.DiscardListPre = function () {
        var option = arguments[0],
            args = arguments,
            value,
            allowedMethods = [];

        this.each(function () {

            var $this = $(this),
                data = $this.data('DiscardListPre'),
                options = $.extend({}, $.fn.DiscardListPre.defaults,
                    $this.data(), typeof option === 'object' && option);

            if (!data) {
                data = new Form($this, options);
                $this.data('RecordEquipment', data);
            }

            if (typeof option === 'string') {
                if ($.inArray(option, allowedMethods) < 0) {
                    throw "Unknown method: " + option;
                }
                value = data[option](args[1]);
            } else {

                var timeReady = setInterval(function () {
                    if (!!$.fn.addEvent) {
                        clearInterval(timeReady);
                        data.init();
                    }
                }, 100);

                if (args[1]) {
                    value = data[args[1]].apply(data, [].slice.call(args, 2));
                }
            }
        });

        return value || this;
    };
    $.fn.DiscardListPre.defaults = {}
    $('#ContentDiscardListPre').DiscardListPre();

})(jQuery);